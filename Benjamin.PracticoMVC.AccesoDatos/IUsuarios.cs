using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benjamin.PracticoMVC.AccesoDatos
{
   public interface IUsuarios
    {

        List<Entidades.Join_UsuariosClientes> ObtenerTodos();

        Entidades.Usuarios Obtener(int id);

        int Agregar(Entidades.Usuarios obj);

        int Actualizar(Entidades.Usuarios obj);

        int EjecutarBaja(int id);

        Entidades.Join_UsuariosClientes Detalle(int id);


    }
}
