using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TiendaVirtual.Shared;
using System.Web.Http.Description;
using TiendaVirtual.Core.Interface;
using TiendaVirtual.Entities.Models;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

//------------------------------------------------------------------------
// <summary>Controller to StatusController</summary>
//----------------------------------------------------------------------- 
namespace TiendaVirtual.API.Controllers
{
    public class ApiTiendaVirtualController : ApiController
    {
        private readonly ITiendaVirtualServicio tiendaVirtualServicio;

        public ApiTiendaVirtualController(ITiendaVirtualServicio tiendaVirtualServicio)
        {
            this.tiendaVirtualServicio = tiendaVirtualServicio;
        }

        /// <summary>
        /// Consultar Ordenes Tienda
        /// </summary>
        /// <returns>Retorna respuesta en objeto Task de tipo IHttpActionResult</returns>
        [HttpPost] 
        [ResponseType(typeof(IEnumerable<Orders>))]
        [System.Web.Http.Route("api/tiendavirtual/obtenerordenestienda")]
        public async Task<IHttpActionResult> ObtenerOrdenesTienda()
        {
            try
            {
                return this.Ok(await this.tiendaVirtualServicio.ObtenerOrdenesTienda());
            }
            catch (Exception ex)
            {
                var error = string.Format("Se ha presentado la siguiente excepción no controlada: {0}", ex.Message);
                return new ErrorResult(this.Request, HttpStatusCode.InternalServerError, error);
            }
        }

        /// <summary>
        /// Consultar Orden Tienda por id
        /// </summary>
        /// <returns>Retorna respuesta en objeto Task de tipo IHttpActionResult</returns>
        [HttpPost]
        [ResponseType(typeof(Orders))]
        [System.Web.Http.Route("api/tiendavirtual/obtenerordentienda")]
        public async Task<IHttpActionResult> ObtenerOrdenTienda([FromBody]int id)
        {
            try
            {
                return this.Ok(await this.tiendaVirtualServicio.ObtenerOrdenTienda(id));
            }
            catch (Exception ex)
            {
                var error = string.Format("Se ha presentado la siguiente excepción no controlada: {0}", ex.Message);
                return new ErrorResult(this.Request, HttpStatusCode.InternalServerError, error);
            }
        }

        /// <summary>
        /// CrearOrdenPedido
        /// </summary>
        /// <returns>Retorna respuesta en objeto Task de tipo IHttpActionResult</returns>
        [HttpPost]
        [ResponseType(typeof(Orders))]
        [System.Web.Http.Route("api/tiendavirtual/crearordenpedido")]
        public async Task<IHttpActionResult> CrearOrdenPedido([FromBody]Orders ordenPedido)
        {
            try
            {
                return this.Ok(await this.tiendaVirtualServicio.CrearOrdenPedido(ordenPedido, 
                    this.Request.GetOwinContext().Request.RemoteIpAddress, 
                    this.Request.Headers.UserAgent.ToString()));
            }
            catch (Exception ex)
            {
                var error = string.Format("Se ha presentado la siguiente excepción no controlada: {0}", ex.Message);
                return new ErrorResult(this.Request, HttpStatusCode.InternalServerError, error);
            }
        }
    }
}
