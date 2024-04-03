
using ProyectoNoSQL_Web.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace ProyectoNoSQL_Web.Models
{
    public class EmpleadosModel
    {

        public string url = ConfigurationManager.AppSettings["urlApi"];

        public ConfirmacionEmpleado ConsultarDatosPersonales()
        {
            using (var client = new HttpClient())
            {
                url += "Empleados/Mostrar";
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionEmpleado>().Result;
                else
                    return null;
            }
        }

        public Confirmacion NuevoDatosPersonales(Empleado entidad)
        {
            using (var client = new HttpClient())
            {
                url += "Empleados/Nuevo";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionEmpleado ConsultarUnDato(string id)
        {
            using (var client = new HttpClient())
            {
                url += "Empleados/MostrarUno?id=" + id;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionEmpleado>().Result;
                else
                    return null;
            }
        }
        public Confirmacion Editar(Empleado entidad)
        {
            using (var client = new HttpClient())
            {
                url += "Empleados/Editar";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
                else
                    return null;
            }
        }
        public Confirmacion Eliminar(string id)
        {
            using (var client = new HttpClient())
            {
                url += "Empleados/Eliminar?id=" + id;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
                else
                    return null;
            }
        }
    }
}