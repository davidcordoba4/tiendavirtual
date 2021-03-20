using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using TiendaVirtual.Entities.Interfaces;
using TiendaVirtual.Entities.Models;
using Dapper;
using TiendaVirtual.Repositories.Connection;
using Dapper.Contrib.Extensions;
using System;

//-----------------------------------------------------------------------
// <Author>David Córdoba</Author>
// <summary>Repositorio Tienda Virtual</summary>
//----------------------------------------------------------------------- 
namespace TiendaVirtual.Repositories.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TiendaVirtualRepositorio : ITiendaVirtualRepositorio
    {
        public readonly ConexionSql conexionSql;
        public TiendaVirtualRepositorio()
        {
            this.conexionSql = new ConexionSql();
        }

        /// <summary>
        /// Método ObtenerOrdenesTienda.
        /// </summary>        
        /// <returns>Task{List{Orders}}</returns>
        public async Task<List<Orders>> ObtenerOrdenesTienda()
        {
            using (var db = this.conexionSql.ObtenerConexion())
            {
                var sql = @"[dbo].[ObtenerOrdenesTienda]";
                using (var qm = db.QueryMultiple(sql, commandType: CommandType.StoredProcedure, commandTimeout: int.MaxValue))
                {
                    List<Orders> Ordenes = qm.Read<Orders>().ToList();
                    List<Status> estadosOrdenes = qm.Read<Status>().ToList();
                    Parallel.ForEach(
                        Ordenes,
                        (item) =>
                        {
                            item.OrderStatus = estadosOrdenes.FirstOrDefault(s => s.Id == item.Status_Id);
                        });

                    return await Task.FromResult(Ordenes);
                }
            }
        }

        /// <summary>método Insertar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        public void Insertar<T>(T entidad) where T : class
        {
            using (var db = this.conexionSql.ObtenerConexion())
            {
                db.Insert(entidad);
            }
        }

        /// <summary>método Actualizar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        public void Actualizar<T>(T entidad) where T : class
        {
            using (var db = this.conexionSql.ObtenerConexion())
            {
                db.Update(entidad);
            }
        }

        /// <summary>método Eliminar.</summary>
        /// <param name="entidad">parámetro entidad.</param>
        public void Eliminar<T>(T entidad) where T : class
        {
            using (var db = this.conexionSql.ObtenerConexion())
            {
                db.Delete(entidad);
            }
        }

        /// <summary>Método ObtenerPorId.</summary>
        /// <param name="id">Parámetro id.</param>
        /// <returns>Retorna tipo entidad a consultar.</returns>
        public async Task<T> ObtenerPorId<T>(int id) where T : class
        {
            var entidad = default(T);
            using (var db = this.conexionSql.ObtenerConexion())
            {
                entidad = db.Get<T>(id);
            }
            return await Task.FromResult(entidad);
        }

        /// <summary>
        /// Método ProcesarEstadoOrden.
        /// </summary>        
        /// <returns>Task{Status}</returns>
        public async Task<Status> ProcesarEstadoOrden(string descripcionEstado)
        {
            using (var db = this.conexionSql.ObtenerConexion())
            {
                var sql = @"[dbo].[ProcesarEstadoOrden]";
                var resultado = db.Query<Status>(sql, new { descripcionEstado }, commandType: CommandType.StoredProcedure, commandTimeout: int.MaxValue).FirstOrDefault();
                return await Task.FromResult(resultado);
            }
        }
    }
}
