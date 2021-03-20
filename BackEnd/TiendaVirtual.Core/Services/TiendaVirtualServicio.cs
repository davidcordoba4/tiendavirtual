using TiendaVirtual.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaVirtual.Core.Interface;
using TiendaVirtual.Entities.Interfaces;
using System.Configuration;
using System;
using PlacetoPay.Integrations.Library.CSharp.Contracts;
using PlacetoPay.Integrations.Library.CSharp.Entities;
using PlacetoPay.Integrations.Library.CSharp.Message;
using System.Globalization;

//-----------------------------------------------------------------------
// <summary>Servicio Tienda Virtual</summary>
//-----------------------------------------------------------------------
namespace TiendaVirtual.Core.Services
{
    using P2P = PlacetoPay.Integrations.Library.CSharp.PlacetoPay;
    public class TiendaVirtualServicio : ITiendaVirtualServicio {
        const double TOTAL_PAGO = 70000;
        const string PRODUCTO = "Camiseta";
        const string ESTADOINICIALORDEN = "PENDING";
        const string ESTADO_OK_CREACION_ORDENPEDIDO = "OK";
        const string ESTADO_OK_PAGO_APROVADO = "APPROVED";
        public ITiendaVirtualRepositorio tiendaVirtualRepositorio { get; set; }

        public TiendaVirtualServicio(ITiendaVirtualRepositorio tiendaVirtualRepositorio)
        {
            this.tiendaVirtualRepositorio = tiendaVirtualRepositorio;
        }

        /// <summary>
        /// Método ObtenerOrdenesTienda.
        /// </summary>        
        /// <returns>Task{List{Orders}}</returns>
        public async Task<List<Orders>> ObtenerOrdenesTienda()
        {
            return await this.tiendaVirtualRepositorio.ObtenerOrdenesTienda();
        }

        /// <summary>
        /// Método ObtenerOrdenTienda.
        /// </summary>        
        /// <returns>Task{Orders}</returns>
        public async Task<Orders> ObtenerOrdenTienda(int id)
        {
            var ordenPedido = await this.tiendaVirtualRepositorio.ObtenerPorId<Orders>(id);
            ordenPedido = await this.ActualizarEstadoPedido(ordenPedido);
            return await Task.FromResult(ordenPedido);
        }

        /// <summary>
        /// Método ActualizarEstadoPedido.
        /// </summary>        
        /// <returns>Task{Orders}</returns>
        private async Task<Orders> ActualizarEstadoPedido(Orders ordenPedido)
        {
            var estadoActualOrden = await this.tiendaVirtualRepositorio.ObtenerPorId<Entities.Models.Status>(ordenPedido.Status_Id);
            if (estadoActualOrden.Status_Description == ESTADO_OK_PAGO_APROVADO)
            {
                ordenPedido.OrderStatus = estadoActualOrden;
                return await Task.FromResult(ordenPedido);
            }
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var gateWay = new P2P(ConfigurationManager.AppSettings["Login_Ws_PlaceToPay"],
                ConfigurationManager.AppSettings["TranKey_Ws_PlaceToPay"],
                new Uri(ConfigurationManager.AppSettings["Url_Ws_PlaceToPay"]), Gateway.TP_REST);
            RedirectInformation response = gateWay.Query(ordenPedido.Request_Id);
            var estadoOrden = await this.tiendaVirtualRepositorio.ProcesarEstadoOrden(response.Status.status);
            ordenPedido.OrderStatus = estadoOrden;
            if (estadoOrden.Id != ordenPedido.Status_Id)
            {
                ordenPedido.Status_Id = estadoOrden.Id;
                ordenPedido.Updated_At = DateTime.Now;
                this.tiendaVirtualRepositorio.Actualizar(ordenPedido);
            }
            return await Task.FromResult(ordenPedido);
        }

        /// <summary>
        /// Método CrearOrdenPedido.
        /// </summary>        
        /// <returns>Task{Orders}</returns>
        public async Task<Orders> CrearOrdenPedido(Orders ordenPedido, string direccionIP, string agenteUsuario)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var gateWay = new P2P(ConfigurationManager.AppSettings["Login_Ws_PlaceToPay"], 
                ConfigurationManager.AppSettings["TranKey_Ws_PlaceToPay"], 
                new Uri(ConfigurationManager.AppSettings["Url_Ws_PlaceToPay"]), Gateway.TP_REST);
            Amount montoPago = new Amount(TOTAL_PAGO);
            Payment pagoPedido = new Payment(PRODUCTO, PRODUCTO, montoPago);
            var tiempoExpiracionPago = DateTime.Now.AddMinutes(10);
            ordenPedido.OrderStatus = await this.tiendaVirtualRepositorio.ProcesarEstadoOrden(ESTADOINICIALORDEN);
            ordenPedido.Status_Id = ordenPedido.OrderStatus.Id;
            ordenPedido.Created_At = DateTime.Now;
            this.tiendaVirtualRepositorio.Insertar(ordenPedido);
            var urlRedireccionamientoLocal = string.Format("{0}/{1}{2}", ordenPedido.UrlRaiz, "ordenes/visualizarestado/false/", ordenPedido.Id);
            RedirectRequest solicitudRedireccionamiento = new RedirectRequest(pagoPedido, urlRedireccionamientoLocal, direccionIP, 
                agenteUsuario, tiempoExpiracionPago.ToString("s", CultureInfo.InvariantCulture));
            solicitudRedireccionamiento.Buyer = new Person(string.Empty, string.Empty, ordenPedido.Customer_Name,
                string.Empty, ordenPedido.Customer_Email, mobile: ordenPedido.Customer_Mobile);
            solicitudRedireccionamiento.Locale = "es_CO";
            solicitudRedireccionamiento.CancelUrl = urlRedireccionamientoLocal;
            RedirectResponse response = gateWay.Request(solicitudRedireccionamiento);
            if (response.Status.status != ESTADO_OK_CREACION_ORDENPEDIDO)
            {
                this.tiendaVirtualRepositorio.Eliminar(ordenPedido);
                throw new Exception(response.Status.Message);
            }
            ordenPedido.Request_Id = response.RequestId;
            ordenPedido.UrlProcesamiento = response.ProcessUrl;
            this.tiendaVirtualRepositorio.Actualizar(ordenPedido);
            return await Task.FromResult(ordenPedido);
        }
    }
}