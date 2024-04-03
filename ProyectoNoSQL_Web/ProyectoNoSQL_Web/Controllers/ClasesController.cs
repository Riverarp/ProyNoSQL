using ProyectoNoSQL_Web.Entidades;
using ProyectoNoSQL_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoNoSQL_Web.Controllers
{
    public class ClasesController : Controller
    {
        ClasesModel modelo = new ClasesModel();
        [HttpGet]
        public ActionResult Index()
        {
            var respuesta = modelo.ConsultarDatosClase();

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Clase>());
            }
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            var datosPersonales = new Clase();
            return View(datosPersonales);
        }

        [HttpPost]
        public ActionResult Nuevo(Clase entidad)
        {

            var respuesta = modelo.NuevoDatosClase(entidad);

            if (respuesta.Codigo == 0)
                return RedirectToAction("Index", "Clases");
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
                return View(new Clase());
            }
        }


        [HttpPost]
        public ActionResult Actualizar(Clase entidad)
        {
            var respuesta = modelo.Editar(entidad);

            if (respuesta.Codigo == 0)
                return RedirectToAction("Index", "Clases");
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
                return RedirectToAction("Index", "Clases");
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }
    }
}
