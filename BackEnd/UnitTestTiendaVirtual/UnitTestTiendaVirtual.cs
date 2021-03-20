using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;
using TiendaVirtual.API.Controllers;
using TiendaVirtual.Core.Interface;
using TiendaVirtual.Core.Services;
using TiendaVirtual.Entities.Interfaces;
using TiendaVirtual.Entities.Models;

namespace UnitTestTiendaVirtual
{
    [TestClass]
    public class UnitTestTiendaVirtual
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-co");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-co");
            HttpWorkerRequest InitWorkerRequest = new SimpleWorkerRequest("", "", "", "", new StringWriter(CultureInfo.InvariantCulture));
            System.Web.HttpContext.Current = new HttpContext(InitWorkerRequest);
            System.Web.HttpContext.Current.Request.Browser = new HttpBrowserCapabilities();
            System.Web.HttpContext.Current.Request.Browser.Capabilities = new Dictionary<string, string> { { "requiresPostRedirectionHandling", "false" } };
        }

        [TestMethod]
        public void PruebaValidarObtenerOrdenesTienda()
        {
            var mockRepositorio = new Mock<ITiendaVirtualRepositorio>();
            var respuestaMock = new List<Orders>() {
                new Orders{ Created_At = DateTime.Now, Customer_Email = "davidcordoba4@gmail.com",
                    Customer_Mobile = "3207362669", Id = 1, Customer_Name = "David Alejandro",
                    Request_Id = "1784048", Status_Id = 1,
                    OrderStatus = new Status(){ Id = 1, Status_Description = "PENDING" } },
                new Orders{ Created_At = DateTime.Now, Customer_Email = "carlosmario54@gmail.com",
                    Customer_Mobile = "3217542453", Id = 1, Customer_Name = "Carlos Mario",
                    Request_Id = "1784049", Status_Id = 3,
                    OrderStatus = new Status(){ Id = 3, Status_Description = "APPROVED" } },
                new Orders{ Created_At = DateTime.Now, Customer_Email = "eugeniocordoba21@gmail.com",
                    Customer_Mobile = "3207322352", Id = 1, Customer_Name = "Eugenio Alberto",
                    Request_Id = "1784050", Status_Id = 4,
                    OrderStatus = new Status(){ Id = 4, Status_Description = "REJECTED" }}};
            mockRepositorio.Setup(x => x.ObtenerOrdenesTienda()).Returns(Task.FromResult(respuestaMock));
            var tiendaVirtualServicio = new TiendaVirtualServicio(mockRepositorio.Object);
            var controlador = new ApiTiendaVirtualController(tiendaVirtualServicio);
            var peticionControlador = new HttpRequestMessage()
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty),
                                              System.Text.Encoding.UTF8)
            };
            var contextoControlador = new HttpControllerContext();
            peticionControlador.SetConfiguration(new HttpConfiguration());
            peticionControlador.SetRequestContext(new HttpRequestContext());
            var entornoOwin = new Dictionary<string, object>();
            var contexto = new OwinContext(entornoOwin);
            contexto.Request.RemoteIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(Ip => Ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            peticionControlador.SetOwinContext(contexto);
            contextoControlador.Request = peticionControlador;
            controlador.ControllerContext = contextoControlador;
            controlador.Configuration = new HttpConfiguration();
            // Act on Test: Si devuelve OK. Comprobar que se retornan los resultados correspondientes para la prueba realizada.
            var resultado = controlador.ObtenerOrdenesTienda().Result;
            var respuesta = resultado.ExecuteAsync(CancellationToken.None).Result;
            List<Orders> resultados;
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Orders>>(respuesta.Content.ReadAsStringAsync().Result);
            }
            else
            {
                resultados = new List<Orders>();
            }
            Assert.IsTrue(resultados.All((registro) => registro.Equals(respuestaMock[resultados.IndexOf(registro)])));
        }

        [TestMethod]
        public void PruebaValidarObtenerOrdenesTiendaSinRegistrosBD()
        {
            var mockRepositorio = new Mock<ITiendaVirtualRepositorio>();
            var respuestaMock = new List<Orders>();
            mockRepositorio.Setup(x => x.ObtenerOrdenesTienda()).Returns(Task.FromResult(respuestaMock));
            var tiendaVirtualServicio = new TiendaVirtualServicio(mockRepositorio.Object);
            var controlador = new ApiTiendaVirtualController(tiendaVirtualServicio);
            var peticionControlador = new HttpRequestMessage()
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty),
                                              System.Text.Encoding.UTF8)
            };
            var contextoControlador = new HttpControllerContext();
            peticionControlador.SetConfiguration(new HttpConfiguration());
            peticionControlador.SetRequestContext(new HttpRequestContext());
            var entornoOwin = new Dictionary<string, object>();
            var contexto = new OwinContext(entornoOwin);
            contexto.Request.RemoteIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(Ip => Ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            peticionControlador.SetOwinContext(contexto);
            contextoControlador.Request = peticionControlador;
            controlador.ControllerContext = contextoControlador;
            controlador.Configuration = new HttpConfiguration();
            // Act on Test: Si devuelve OK. Comprobar que se retornan los resultados correspondientes para la prueba realizada.
            var resultado = controlador.ObtenerOrdenesTienda().Result;
            var respuesta = resultado.ExecuteAsync(CancellationToken.None).Result;
            List<Orders> resultados = new List<Orders>();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Orders>>(respuesta.Content.ReadAsStringAsync().Result);
            }
            Assert.IsTrue(respuesta.StatusCode == HttpStatusCode.OK && resultados.Count == respuestaMock.Count);
        }

        [TestMethod]
        public void PruebaValidarObtenerOrdenTiendaPagoAprovado()
        {
            var mockRepositorio = new Mock<ITiendaVirtualRepositorio>();
            var respuestaMock = new Orders
            {
                Created_At = DateTime.Now,
                Customer_Email = "carlosmario54@gmail.com",
                Customer_Mobile = "3217542453",
                Id = 1,
                Customer_Name = "Carlos Mario",
                Request_Id = "1784049",
                Status_Id = 3,
                OrderStatus = new Status() { Id = 3, Status_Description = "APPROVED" }
            };
            mockRepositorio.Setup(x => x.ObtenerPorId<Orders>(It.IsAny<int>())).Returns(Task.FromResult(respuestaMock));
            mockRepositorio.Setup(x => x.ObtenerPorId<Status>(It.IsAny<int>())).Returns(Task.FromResult(respuestaMock.OrderStatus));
            var tiendaVirtualServicio = new TiendaVirtualServicio(mockRepositorio.Object);
            var controlador = new ApiTiendaVirtualController(tiendaVirtualServicio);
            var peticionControlador = new HttpRequestMessage()
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(respuestaMock.Id),
                                              System.Text.Encoding.UTF8)
            };
            var contextoControlador = new HttpControllerContext();
            peticionControlador.SetConfiguration(new HttpConfiguration());
            peticionControlador.SetRequestContext(new HttpRequestContext());
            var entornoOwin = new Dictionary<string, object>();
            var contexto = new OwinContext(entornoOwin);
            contexto.Request.RemoteIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(Ip => Ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            peticionControlador.SetOwinContext(contexto);
            contextoControlador.Request = peticionControlador;
            controlador.ControllerContext = contextoControlador;
            controlador.Configuration = new HttpConfiguration();
            // Act on Test: Si devuelve OK. Comprobar que se retornan los resultados correspondientes para la prueba realizada.
            var resultado = controlador.ObtenerOrdenTienda(respuestaMock.Id).Result;
            var respuesta = resultado.ExecuteAsync(CancellationToken.None).Result;
            Orders resultadoRespuesta = new Orders();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                resultadoRespuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<Orders>(respuesta.Content.ReadAsStringAsync().Result);
            }
            Assert.IsTrue(respuesta.StatusCode == HttpStatusCode.OK && respuestaMock.Equals(resultadoRespuesta));
        }

        [TestMethod]
        public void PruebaCrearOrdenValidarEstadoInicial()
        {
            var mockRepositorio = new Mock<ITiendaVirtualRepositorio>();
            var estadosPedido = new List<Status>() {
                new Status { Id=1,Status_Description= "PENDING" },
                new Status { Id=2,Status_Description= "FAILED" },
                new Status { Id=3,Status_Description= "APPROVED" },
                new Status { Id=4,Status_Description= "REJECTED" },
                new Status { Id=5,Status_Description= "ERROR" }
            };
            var respuestaMock = new Orders
            {
                Created_At = DateTime.Now,
                Customer_Email = "davidcordoba4@gmail.com",
                Customer_Mobile = "3207362669",
                Id = 1,
                Customer_Name = "David Alejandro",
                Status_Id = 1,
                OrderStatus = new Status() { Id = 1, Status_Description = "PENDING" }
            };
            mockRepositorio.Setup(x => x.ObtenerPorId<Orders>(It.IsAny<int>())).Returns(Task.FromResult(respuestaMock));
            mockRepositorio.Setup(x => x.ObtenerPorId<Status>(It.IsAny<int>())).Returns(Task.FromResult(respuestaMock.OrderStatus));
            mockRepositorio.Setup(x => x.Insertar(It.IsAny<Orders>())).Callback<Orders>((entidad)=>{ entidad.Id = 1; });
            mockRepositorio.Setup(x => x.Actualizar(It.IsAny<Orders>())).Callback<Orders>((entidad) => { return; });
            mockRepositorio.Setup(x => x.Eliminar(It.IsAny<Orders>())).Callback<Orders>((entidad) => { return; });
            mockRepositorio.Setup(x => x.ProcesarEstadoOrden(It.IsAny<string>()))
                .Returns<string>((descripcionEstado) => 
                {
                    return Task.FromResult(estadosPedido.FirstOrDefault(x=>x.Status_Description==descripcionEstado));
                });
            var tiendaVirtualServicio = new TiendaVirtualServicio(mockRepositorio.Object);
            var controlador = new ApiTiendaVirtualController(tiendaVirtualServicio);
            var peticion = new Orders
            {
                Customer_Email = "davidcordoba4@gmail.com",
                Customer_Mobile = "3207362669",
                Customer_Name = "David Alejandro",
                UrlRaiz = "http://localhost:4200"
            };
            var peticionControlador = new HttpRequestMessage()
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(peticion),
                                              System.Text.Encoding.UTF8)
            };
            var contextoControlador = new HttpControllerContext();
            peticionControlador.SetConfiguration(new HttpConfiguration());
            peticionControlador.SetRequestContext(new HttpRequestContext());
            peticionControlador.Headers.UserAgent.Add(new ProductInfoHeaderValue("AgentePrueba", "1.0"));
            var entornoOwin = new Dictionary<string, object>();
            var contexto = new OwinContext(entornoOwin);
            contexto.Request.RemoteIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(Ip => Ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            peticionControlador.SetOwinContext(contexto);
            contextoControlador.Request = peticionControlador;
            controlador.ControllerContext = contextoControlador;
            controlador.Configuration = new HttpConfiguration();
            // Act on Test: Si devuelve OK. Comprobar que se retornan los resultados correspondientes para la prueba realizada.
            var resultado = controlador.CrearOrdenPedido(peticion).Result;
            var respuesta = resultado.ExecuteAsync(CancellationToken.None).Result;
            var ordenPedidoCreada = new Orders();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                ordenPedidoCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<Orders>(respuesta.Content.ReadAsStringAsync().Result);
                respuestaMock.Request_Id = ordenPedidoCreada.Request_Id;
            }
            resultado = controlador.ObtenerOrdenTienda(ordenPedidoCreada.Id).Result;
            respuesta = resultado.ExecuteAsync(CancellationToken.None).Result;
            var ordenPedidoEstadoInicial = new Orders();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                ordenPedidoEstadoInicial = Newtonsoft.Json.JsonConvert.DeserializeObject<Orders>(respuesta.Content.ReadAsStringAsync().Result);
            }
            Assert.IsTrue(respuesta.StatusCode == HttpStatusCode.OK && ordenPedidoCreada.OrderStatus.Equals(respuestaMock.OrderStatus)
                && ordenPedidoCreada.Id == ordenPedidoEstadoInicial.Id && ordenPedidoCreada.Request_Id == ordenPedidoEstadoInicial.Request_Id);
        }
    }
}