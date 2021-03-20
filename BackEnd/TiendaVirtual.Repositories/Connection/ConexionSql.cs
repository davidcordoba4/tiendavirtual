using System.Data.SqlClient;
using System.Configuration;

namespace TiendaVirtual.Repositories.Connection
{
    public class ConexionSql
    {
        /// <summary>
        /// Método ObtenerConnection
        /// </summary>        
        /// <returns>SqlConnection</returns>
        public SqlConnection ObtenerConexion()
        {            
            var str = this.ObtenerConexionString();
            var con = new SqlConnection(str);
            con.Open();
            return con;
        }
        /// <summary>
        /// Método ObtenerConexionString
        /// </summary>      
        /// <returns>string</returns>
        private string ObtenerConexionString()
        {
            var conexionString = ConfigurationManager.ConnectionStrings["BDTiendaVirtual"].ToString();
            return conexionString;
        }

        public void DisposeCon(SqlConnection con)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Dispose();
        }
    }
}
