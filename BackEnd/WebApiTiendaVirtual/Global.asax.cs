using Microsoft.Security.Application;
using MohammadYounes.Owin.Security.MixedAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TiendaVirtual.API;

//------------------------------------------------------------------------
// <summary>Global.asax.cs Application</summary>
//----------------------------------------------------------------------- 

namespace TiendaVirtual.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public WebApiApplication()
        {
            //this.RegisterMixedAuth();
        }
        protected void Application_Start()
        {
            //eliminar encabezado X-AspNetMvc-Version de respuesta
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Se captura error, si es de tipo HttpRequestValidationException indicando que se introdujo código malicioso y se redirecciona a la página
        /// de solicitud con el mensaje de error guardado en TempData["AlertError"]. El mensaje de excepción capturado se limpia para evitar que regrese
        /// código malicioso al cliente y vuelva a presentarse error al tratar de mostrar mensaje en el cliente; para ello se usa Sanitizer.GetSafeHtmlFragment 
        /// recomendado para limpiar de string código malicioso y que sea seguro para devolver al navegador.
        /// </summary>        
        /// <returns></returns>
        ///
        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            String ErrorStr = string.Format("{0}: {1}", "Se ha presentado el siguiente error", ((Server.GetLastError().InnerException != null) ? Server.GetLastError().InnerException.Message : Server.GetLastError().Message));
            var UrlRedirectError = string.Empty;
            if (ex is HttpRequestValidationException || ex.InnerException is HttpRequestValidationException)
            {
                ErrorStr = Sanitizer.GetSafeHtmlFragment(ErrorStr);
                UrlRedirectError = Request.Url.GetLeftPart(UriPartial.Path);
            }
            else
            {
                UrlRedirectError = "~/Home/Index";
            }

            Response.Clear();
            ErrorStr = ErrorStr.Replace("\\", "\\\\");
            ErrorStr = ErrorStr.Replace("'", "\\'");
            ErrorStr = ErrorStr.Replace("\r", string.Empty);
            ErrorStr = ErrorStr.Replace("\n", " ");
            if (ErrorStr.Substring(ErrorStr.Length - 1) == "(")
            {
                ErrorStr = ErrorStr.Substring(0, ErrorStr.Length - 1);
            }

            var msg = string.Empty;
            msg += "swal({" + string.Format("title: '{0}',text: '{1}',type: '{2}',confirmButtonText: 'Aceptar'", "Error!", ErrorStr, "error") + ",allowEscapeKey: false,allowOutsideClick: false})";

            if (Context.Session == null)
            {
                UrlRedirectError = string.Format("~/Home/Index?{0}={1}", "Error", ErrorStr);
            }
            else
            {
                var tempDataDictionary = Context.Session["__ControllerTempData"] as Dictionary<string, object>;
                if (tempDataDictionary == null)
                {
                    tempDataDictionary = new Dictionary<string, object>();
                }
                tempDataDictionary["AlertError"] = msg;
                HttpContext.Current.Session["__ControllerTempData"] = tempDataDictionary;
            }
            // Clear the error from the server
            Server.ClearError();
            Response.Redirect(UrlRedirectError);

            Response.End();
        }


        /// <summary>
        /// En caso de presentarse error tipo HttpRequestValidationException al leerse Request.QueryString o Request.Form la pila de excepción es de nuevo
        /// lanzada a Application_Error para que se muestre correctamente la excepción en caso de ataques xss.
        /// </summary>        
        /// <returns></returns>
        ///
        protected void Application_BeginRequest()
        {
            Request.ValidateInput();
            var q = Request.QueryString;
            var f = Request.Form;
        }

    }
}
