using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;

namespace Benjamin.PracticoMVC.AccesoDatos
{
    public class UsuariosDAL : IUsuarios
    {
        string cadenaConexion = @"Data Source=NOTEBENJA;Initial Catalog=db_practico_benjamin;Integrated Security=True";

        public List<Entidades.Join_UsuariosClientes> ObtenerTodos()
        {

            List<Entidades.Join_UsuariosClientes> lista = new List<Entidades.Join_UsuariosClientes>();

            StringBuilder consultaSQL = new StringBuilder();
            /*
            SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL,
            USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS,
            USUARIOS.FechaCreacion AS FECHA_CREACION, --USUARIOS.Activo AS ACTIVO
            CASE  
            WHEN Activo = 1 THEN 'Activo'   
            ELSE 'Baja'  
            END  AS ESTADO
            FROM USUARIOS
            INNER JOIN ROLES ON
            Usuarios.IdRol = Roles.Id
            */
            consultaSQL.Append("SELECT USUARIOS.Id AS ID_USUARIO, Roles.Descripcion AS ROL, ");
            consultaSQL.Append("USUARIOS.Usuario AS USERNAME, USUARIOS.Nombre AS NOMBRES, USUARIOS.Apellido AS APELLIDOS, ");
            consultaSQL.Append("USUARIOS.FechaCreacion AS FECHA_CREACION, ");
            consultaSQL.Append("CASE ");
            consultaSQL.Append("WHEN Activo = 1 THEN 'Activo' ");
            consultaSQL.Append("ELSE 'Baja' ");
            consultaSQL.Append("END  AS ESTADO ");
            consultaSQL.Append("FROM USUARIOS ");
            consultaSQL.Append("INNER JOIN ROLES ON ");
            consultaSQL.Append("Usuarios.IdRol = Roles.Id ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Join_UsuariosClientes>(consultaSQL.ToString()).ToList();
            }

            return lista;

        }

        public Entidades.Usuarios Obtener(int id)
        {
            Entidades.Usuarios obj = new Entidades.Usuarios();
            StringBuilder consultaSQL = new StringBuilder();
            /*
            SELECT Id, IdRol, Usuario, Nombre, Apellido, 
            Password, PasswordSalt, FechaCreacion, Activo
            FROM Usuarios
            WHERE Id = 1
            */
            consultaSQL.Append("SELECT Id, IdRol, Usuario, Nombre, Apellido,  ");
            consultaSQL.Append("Password, PasswordSalt, FechaCreacion, Activo ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("WHERE Id = @Id ");


            using (var connection = new SqlConnection(cadenaConexion))
            {
                obj = connection.QuerySingleOrDefault<Entidades.Usuarios>(consultaSQL.ToString(), new { Id = id });
            }

            return obj;
        }

        public int Agregar(Entidades.Usuarios obj)
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
                INSERT INTO Usuarios (IdRol, Usuario, Nombre, Apellido, 
                Password, PasswordSalt, FechaCreacion, Activo)
                VALUES  (@IdRol, @Usuario, @Nombre, @Apellido, 
                @Password, @PasswordSalt, @FechaCreacion, @Activo)
                */
                consultaSQL1.Append("INSERT INTO Usuarios (IdRol, Usuario, Nombre, Apellido, ");
                consultaSQL1.Append("Password, PasswordSalt, FechaCreacion, Activo) ");
                consultaSQL1.Append("VALUES  (@IdRol, @Usuario, @Nombre, @Apellido, ");
                consultaSQL1.Append("@Password, @PasswordSalt, @FechaCreacion, @Activo) ");



                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {
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

        public int Actualizar(Entidades.Usuarios obj)
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
                /*
                UPDATE Usuarios
                SET IdRol = @IdRol, Nombre = @Nombre,
                Apellido = @Apellido, Password = @Password, PasswordSalt = @PasswordSalt,
                FechaCreacion = @FechaCreacion, Activo = @Activo
                WHERE Id = @Id
                */
                consultaSQL1.Append("UPDATE Usuarios ");
                consultaSQL1.Append("SET IdRol = @IdRol, Nombre = @Nombre, ");
                consultaSQL1.Append("Apellido = @Apellido, Password = @Password, PasswordSalt = @PasswordSalt, ");
                consultaSQL1.Append("FechaCreacion = @FechaCreacion, Activo = @Activo ");
                consultaSQL1.Append("WHERE Id = @Id ");



                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {

                           IdRol = obj.IdRol,
                           Nombre = obj.Nombre,
                           Apellido = obj.Apellido,
                           Password = "ClaveHash" + obj.Usuario,
                           PasswordSalt = "ClaveSalt" + obj.Usuario,
                           FechaCreacion = DateTime.Now,
                           Activo = obj.Activo,
                           Id = obj.Id
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

        public int EjecutarBaja(int id)
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
                /*
                UPDATE Usuarios
                SET Activo = 0
                WHERE Id = 1
                */
                consultaSQL1.Append("UPDATE Usuarios ");
                consultaSQL1.Append("SET Activo = 0 ");
                consultaSQL1.Append("WHERE Id = @Id ");


                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new { Id = id }
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
