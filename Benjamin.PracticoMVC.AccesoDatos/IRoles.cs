using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benjamin.PracticoMVC.AccesoDatos
{
    public interface IRoles
    {
        IList<Entidades.Roles> ObtenerTodos();
    }
}
