using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaVirtual.Entities.Models;

namespace TiendaVirtual.Entities.Interfaces
{
    public interface ITiendaVirtualRepositorio {

        /// <summary>
        /// M�todo ObtenerOrdenesTienda.
        /// </summary>        
        /// <returns>Task{List{Orders}}</returns>
        Task<List<Orders>> ObtenerOrdenesTienda();

        /// <summary>m�todo Insertar.</summary>
        /// <param name="entidad">par�metro entidad.</param>
        void Insertar<T>(T entidad) where T : class;

        /// <summary>m�todo Actualizar.</summary>
        /// <param name="entidad">par�metro entidad.</param>
        void Actualizar<T>(T entidad) where T : class;

        /// <summary>m�todo Eliminar.</summary>
        /// <param name="entidad">par�metro entidad.</param>
        void Eliminar<T>(T entidad) where T : class;

        /// <summary>M�todo ObtenerPorId.</summary>
        /// <param name="id">Par�metro id.</param>
        /// <returns>Retorna tipo entidad a consultar.</returns>
        Task<T> ObtenerPorId<T>(int id) where T : class;

        /// <summary>
        /// M�todo ProcesarEstadoOrden.
        /// </summary>        
        /// <returns>Task{Status}</returns>
        Task<Status> ProcesarEstadoOrden(string descripcionEstado);
    }
}
