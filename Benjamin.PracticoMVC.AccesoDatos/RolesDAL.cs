using Benjamin.PracticoMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using Dapper;


namespace Benjamin.PracticoMVC.AccesoDatos
{
    public class RolesDAL : IRoles
    {
        string cadenaConexion = @"Data Source=NOTEBENJA;Initial Catalog=db_practico_benjamin;Integrated Security=True";

        public IList<Entidades.Roles> ObtenerTodos()
        {

            var lista = new List<Entidades.Roles>();

            StringBuilder consultaSQL = new StringBuilder();
            /*
            SELECT Id, Descripcion
            FROM Roles
            */
            consultaSQL.Append("SELECT Id, Descripcion ");
            consultaSQL.Append("FROM Roles ");



            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Roles>(consultaSQL.ToString()).ToList();
            }

            return lista;
        }


    }
}
