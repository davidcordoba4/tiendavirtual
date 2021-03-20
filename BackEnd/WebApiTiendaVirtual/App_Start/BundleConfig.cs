using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;

namespace TiendaVirtual.API
{
    [ExcludeFromCodeCoverage]
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar sólo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site_events").Include("~/Scripts/site.*"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
            "~/Content/datepicker/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery1_12_1").Include("~/Scripts/UI/1.12.1/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerynum").Include(
            "~/Scripts/jquery.numeric.js", "~/Scripts/jquery.mousewheel.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
