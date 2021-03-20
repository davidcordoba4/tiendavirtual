using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaVirtual.Entities.Models;

namespace TiendaVirtual.Entities.Interfaces
{
    public interface ITiendaVirtualRepositorio {

        /// <summary>
        /// Método ObtenerOrdenesTienda.
        /// </summary>        
        /// <returns>Task{List{Orders}}</returns>
        Task<List<Orders>> ObtenerOrdenesTienda();

        /// <summary>método Insertar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        void Insertar<T>(T entidad) where T : class;

        /// <summary>método Actualizar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        void Actualizar<T>(T entidad) where T : class;

        /// <summary>método Eliminar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        void Eliminar<T>(T entidad) where T : class;

        /// <summary>Método ObtenerPorId.</summary>
        /// <param name="id">Parámetro id.</param>
        /// <returns>Retorna tipo entidad a consultar.</returns>
        Task<T> ObtenerPorId<T>(int id) where T : class;

        /// <summary>
        /// Método ProcesarEstadoOrden.
        /// </summary>        
        /// <returns>Task{Status}</returns>
        Task<Status> ProcesarEstadoOrden(string descripcionEstado);
    }
}
