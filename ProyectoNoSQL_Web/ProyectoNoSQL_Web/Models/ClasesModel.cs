using ProyectoNoSQL_Web.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Web;

namespace ProyectoNoSQL_Web.Models
{
    public class ClasesModel
    { 
     public string url = ConfigurationManager.AppSettings["urlApi"];

    public ConfirmacionClase ConsultarDatosClase()
    {
        using (var client = new HttpClient())
        {
            url += "Clases/Mostrar";
            var respuesta = client.GetAsync(url).Result;

            if (respuesta.IsSuccessStatusCode)
                return respuesta.Content.ReadFromJsonAsync<ConfirmacionClase>().Result;
            else
                return null;
        }
    }

    public Confirmacion NuevoDatosClase(Clase entidad)
    {
        using (var client = new HttpClient())
        {
            url += "Clases/Nuevo";
            JsonContent jsonEntidad = JsonContent.Create(entidad);
            var respuesta = client.PostAsync(url, jsonEntidad).Result;

            if (respuesta.IsSuccessStatusCode)
                return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
            else
                return null;
        }
    }

    public ConfirmacionClase ConsultarUnDato(string id)
    {
        using (var client = new HttpClient())
        {
            url += "Clases/MostrarUno?id=" + id;
            var respuesta = client.GetAsync(url).Result;

            if (respuesta.IsSuccessStatusCode)
                return respuesta.Content.ReadFromJsonAsync<ConfirmacionClase>().Result;
            else
                return null;
        }
    }
    public Confirmacion Editar(Clase entidad)
    {
        using (var client = new HttpClient())
        {
            url += "Clases/Editar";
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
            url += "Clases/Eliminar?id=" + id;
            var respuesta = client.DeleteAsync(url).Result;

            if (respuesta.IsSuccessStatusCode)
                return respuesta.Content.ReadFromJsonAsync<Confirmacion>().Result;
            else
                return null;
        }
    }
}
}