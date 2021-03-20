using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TiendaVirtual.Shared
{
    /// <summary>
    /// Clase para manejar errores personalizados con formato correcto para el response
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    /// 
    public class ErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }

        public ErrorResult(HttpRequestMessage request, HttpStatusCode statusCode, string[] message)
        {
            this.Request = request;
            this.statusCode = statusCode;
            this.message = JsonConvert.SerializeObject(message);
        }

        public ErrorResult(HttpRequestMessage request, HttpStatusCode statusCode, string message)
        {
            this.Request = request;
            this.statusCode = statusCode;
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Request.CreateErrorResponse(this.statusCode, this.message));
        }
    }
}