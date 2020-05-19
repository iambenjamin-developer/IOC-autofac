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
        public ActionResult Listar()
        {

            var model = new Models.Usuarios.UsuariosModel();

            model.ListaDeUsuarios = usuariosServicio.ObtenerTodos();

            var datosComboBox = rolesServicio.ObtenerTodos();
            var comboBox = new SelectList(datosComboBox, "Id", "Descripcion");
            model.ListaDeRoles = comboBox;

            return View(model);

        }



        // GET: Usuarios/Details/5
        public ActionResult Detalles(int id)
        {
            return View();
        }



        // GET: Usuarios/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Crear(Models.Usuarios.UsuariosModel modelo)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
