using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Benjamin.PracticoMVC.WebApp.Models.Usuarios
{
    public class UsuariosModel
    {

        public List<Entidades.Join_UsuariosClientes> ListaDeUsuarios { get; set; }

        public SelectList ListaDeRoles { get; set; }

        public DateTime Hora { get; set; }

        public string Mensaje { get; set; }


    }
}