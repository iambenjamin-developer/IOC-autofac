using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;

namespace Benjamin.PracticoMVC.AccesoDatos
{
    public class UsuariosDAL
    {
        string cadenaConexion = @"Data Source=NOTEBENJA;Initial Catalog=db_practico_benjamin;Integrated Security=True";

        public List<Entidades.Join_UsuariosClientes> Listar()
        {
            /*
            SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL,
            USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS,
            USUARIOS.FechaCreacion AS FECHA_CREACION, USUARIOS.Activo AS ACTIVO
            FROM USUARIOS
            INNER JOIN ROLES ON
            Usuarios.IdRol = Roles.Id
            */

            List<Entidades.Join_UsuariosClientes> lista = new List<Entidades.Join_UsuariosClientes>();

            StringBuilder consultaSQL = new StringBuilder();
            /*
            SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL,
            USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS,
            USUARIOS.FechaCreacion AS FECHA_CREACION, USUARIOS.Activo AS ACTIVO
            FROM USUARIOS
            INNER JOIN ROLES ON
            Usuarios.IdRol = Roles.Id
            */
            consultaSQL.Append("SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL, ");
            consultaSQL.Append("USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS, ");
            consultaSQL.Append("USUARIOS.FechaCreacion AS FECHA_CREACION, USUARIOS.Activo AS ACTIVO ");
            consultaSQL.Append("FROM USUARIOS ");
            consultaSQL.Append("INNER JOIN ROLES ON ");
            consultaSQL.Append("Usuarios.IdRol = Roles.Id ");


            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Join_UsuariosClientes>(consultaSQL.ToString()).ToList();
            }

            return lista;

        }

        public int Crear(Entidades.Usuarios obj)
        {


            int filasAfectadas = 0;

            SqlConnection conexion = new SqlConnection(cadenaConexion);


            conexion.Open();

            //como vamos a realizar dos inserciones debemos hacerlo con una transaccion
            var transaccion = conexion.BeginTransaction();


            try
            {


                StringBuilder consultaSQL1 = new StringBuilder();
                /*
                UPDATE Usuarios
                SET IdRol = @IdRol, Usuario = @Usuario, Nombre = @Nombre,
                Apellido = @Apellido, Password = @Password, PasswordSalt = @PasswordSalt,
                FechaCreacion = @FechaCreacion, Activo = @Activo
                WHERE Id = @Id
                 */
                consultaSQL1.Append("UPDATE Usuarios ");
                consultaSQL1.Append("SET IdRol = @IdRol, Usuario = @Usuario, Nombre = @Nombre, ");
                consultaSQL1.Append("Apellido = @Apellido, Password = @Password, PasswordSalt = @PasswordSalt, ");
                consultaSQL1.Append("FechaCreacion = @FechaCreacion, Activo = @Activo ");
                consultaSQL1.Append("WHERE Id = @Id ");


                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {
                           Id = obj.Id,
                           IdRol = obj.IdRol,
                           Usuario = obj.Usuario,
                           Nombre = obj.Nombre,
                           Apellido = obj.Apellido,
                           Password = "ClaveHash" + obj.Usuario,
                           PasswordSalt = "ClaveSalt" + obj.Usuario,
                           FechaCreacion = DateTime.Now,
                           Activo = obj.Activo
                       }
                       , transaction: transaccion);


                transaccion.Commit();
            }
            catch (Exception ex)
            {
                // en caso que haya un error en el medio de la funcion
                //lanzamos codigo de error 0 y realizamos un rollback para que los datos
                //no se reflejen en la base de datos
                filasAfectadas = 0;
                transaccion.Rollback();

            }
            finally
            {
                //si el procedimiento salio bien o mal, siempre se debe cerrar la conexion
                conexion.Close();
            }

            // si el resultado de filasafectadas es 1 es porque salio OK
            return filasAfectadas;
        }

        public int Editar(Entidades.Usuarios obj)
        {



            int filasAfectadas = 0;

            SqlConnection conexion = new SqlConnection(cadenaConexion);


            conexion.Open();

            //como vamos a realizar dos inserciones debemos hacerlo con una transaccion
            var transaccion = conexion.BeginTransaction();


            try
            {



                //primer consulta que inserta un nuevo usuario admin o cliente
                StringBuilder consultaSQL1 = new StringBuilder();

                consultaSQL1.Append("UPDATE Usuarios ");
                consultaSQL1.Append("SET IdRol = @idRolParametro,  ");
                consultaSQL1.Append("Nombre = @nombreParametro, Apellido = @apellidoParametro, ");
                consultaSQL1.Append("Activo = @activoParametro ");
                consultaSQL1.Append("WHERE ID = @idParametro ");



                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {
                           Id = obj.Id,
                           IdRol = obj.IdRol,
                           Usuario = obj.Usuario,
                           Nombre = obj.Nombre,
                           Apellido = obj.Apellido,
                           Password = "ClaveHash" + obj.Usuario,
                           PasswordSalt = "ClaveSalt" + obj.Usuario,
                           FechaCreacion = DateTime.Now,
                           Activo = obj.Activo
                       }
                       , transaction: transaccion);


                transaccion.Commit();
            }
            catch (Exception ex)
            {
                // en caso que haya un error en el medio de la funcion
                //lanzamos codigo de error 0 y realizamos un rollback para que los datos
                //no se reflejen en la base de datos
                filasAfectadas = 0;
                transaccion.Rollback();

            }
            finally
            {
                //si el procedimiento salio bien o mal, siempre se debe cerrar la conexion
                conexion.Close();
            }

            // si el resultado de filasafectadas es 1 es porque salio OK
            return filasAfectadas;

        }





    }
}
