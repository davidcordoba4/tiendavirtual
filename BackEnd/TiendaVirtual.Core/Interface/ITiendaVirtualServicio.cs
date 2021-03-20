using TiendaVirtual.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TiendaVirtual.Core.Interface
{
    public interface ITiendaVirtualServicio
    {
        /// <summary>
        /// Método ObtenerOrdenesTienda.
        /// </summary>        
        /// <returns>Task{List{Orders}}</returns>
        Task<List<Orders>> ObtenerOrdenesTienda();

        /// <summary>
        /// Método ObtenerOrdenTienda.
        /// </summary>        
        /// <returns>Task{Orders}</returns>
        Task<Orders> ObtenerOrdenTienda(int id);

        /// <summary>
        /// Método CrearOrdenPedido.
        /// </summary>        
        /// <returns>Task{Orders}</returns>
        Task<Orders> CrearOrdenPedido(Orders ordenPedido, string direccionIP, string agenteUsuario);
    }
}
