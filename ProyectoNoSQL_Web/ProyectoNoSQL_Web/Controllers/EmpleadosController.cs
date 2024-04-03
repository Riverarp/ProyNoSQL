
using ProyectoNoSQL_Web.Entidades;
using ProyectoNoSQL_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoNoSQL_Web.Controllers
{
    public class EmpleadosController : Controller
    {
        EmpleadosModel modelo = new EmpleadosModel();
        [HttpGet]
        public ActionResult Consultar()
        {
            var respuesta = modelo.ConsultarDatosPersonales();

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Empleado>());
            }
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            var datosPersonales = new Empleado();
            return View(datosPersonales);
        }

        [HttpPost]
        public ActionResult Nuevo(Empleado entidad)
        {

            var respuesta = modelo.NuevoDatosPersonales(entidad);

            if (respuesta.Codigo == 0)
                return RedirectToAction("Consultar", "Empleados");
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Actualizar(string id)
        {
            var respuesta = modelo.ConsultarUnDato(id);

            if (respuesta.Codigo == 0)
                return View(respuesta.Dato);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new Empleado());
            }
        }


        [HttpPost]
        public ActionResult Actualizar(Empleado entidad)
        {
            var respuesta = modelo.Editar(entidad);

            if (respuesta.Codigo == 0)
                return RedirectToAction("Consultar", "Empleados");
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Eliminar(string id)
        {


            var respuesta = modelo.Eliminar(id);

            if (respuesta.Codigo == 0)
                return RedirectToAction("Consultar", "Empleados");
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }
    }
}
