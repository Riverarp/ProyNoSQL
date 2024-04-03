using ProyectoNoSQL_Web.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;

namespace ProyectoNoSQL_Web.Models
{
    public class MembresiasModel
    {

        public string url = ConfigurationManager.AppSettings["urlApi"];

        public ConfirmacionMembresia ConsultarMembresia()
        {
            using (var client = new HttpClient())
            {
                url += "Membresias/Mostrar";
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionMembresia>().Result;
                else
                    return null;
            }
        }

        public Confirmacion NuevaMembresia(Membresia entidad)
        {
            using (var client = new HttpClient())
            {
                url += "Membresias/Nuevo";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionMembresia ConsultarUnDato(string id)
        {
            using (var client = new HttpClient())
            {
                url += "Membresias/MostrarUno?id=" + id;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionMembresia>().Result;
                else
                    return null;
            }
        }
        public Confirmacion Editar(Membresia entidad)
        {
            using (var client = new HttpClient())
            {
                url += "Membresias/Editar";
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
                url += "Membresias/Eliminar?id=" + id;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
                else
                    return null;
            }
        }
    }
}