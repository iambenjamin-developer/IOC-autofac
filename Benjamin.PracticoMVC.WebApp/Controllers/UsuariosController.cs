using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Benjamin.PracticoMVC.WebApp.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly AccesoDatos.IUsuarios usuariosServicio;
        private readonly AccesoDatos.IRoles rolesServicio;

        public UsuariosController(AccesoDatos.IUsuarios srvUsuario, AccesoDatos.IRoles srvRoles)
        {
            usuariosServicio = srvUsuario;
            rolesServicio = srvRoles;
        }


        // GET: Usuarios
        public ActionResult Listar(Models.Usuarios.UsuariosModel modelParametro)
        {

            var model = new Models.Usuarios.UsuariosModel();

            //se asigna el mensaje si es que vino despues de crear el usuario
            model.Mensaje = modelParametro.Mensaje;

            model.ListaDeUsuarios = usuariosServicio.ObtenerTodos();



            return View(model);

        }






        // GET: Usuarios/Create
        public ActionResult Crear()
        {
            var model = new Models.Usuarios.UsuariosModel();

            var datosComboBox = rolesServicio.ObtenerTodos();
            var comboBox = new SelectList(datosComboBox, "Id", "Descripcion");
            model.ListaDeRoles = comboBox;

            return View(model);
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Crear(Models.Usuarios.UsuariosModel model)
        {
            try
            {

                //asignar el valor seleccionado al combobox
                model.UsuarioObjeto.IdRol = model.IdRolSeleccionado;

                // si se agrego correctamente devolvera el valor 1
                int filasAfectadas = usuariosServicio.Agregar(model.UsuarioObjeto);

                if (filasAfectadas == 1)
                {
                    model.Mensaje = "Usuario: " + model.UsuarioObjeto.Usuario +
                        " (" + model.UsuarioObjeto.Apellido + " " + model.UsuarioObjeto.Nombre
                        + ") agregad@ con éxito!";

                }

                return RedirectToAction("Listar", "Usuarios", model);
            }
            catch
            {
                return View();
            }
        }



        // GET: Usuarios/Details/5
        public ActionResult Detalles(int id)
        {
            return View();
        }


        // GET: Usuarios/Edit/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Editar(Models.Usuarios.UsuariosModel modelo)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Usuarios/Delete/5
        public ActionResult Eliminar(int id)
        {
            return View();
        }

    }
}
