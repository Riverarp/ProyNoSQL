﻿using MongoDB.Bson;
using MongoDB.Driver;
using ProyectoNoSQL_Api.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProyectoNoSQL_Api.Controllers
{
    public class InicioController : ApiController
    {
        private readonly IMongoCollection<DatosPersonales> datosPersonalesCollection;


        public InicioController()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionMongo"];
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("Personas");
            datosPersonalesCollection = database.GetCollection<DatosPersonales>("DatosPersonales");
        }

      


        [HttpGet]
        [Route("DatosPersonales/Mostrar")]
        public ConfirmacionDatosPersonales ConsultarDatosPersonales()
        {
            var respuesta = new ConfirmacionDatosPersonales();

            try
            {
               
                    var datos = datosPersonalesCollection.Find(_ => true).ToList();

                    if (datos.Count > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.Datos = datos;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "No se encontraron resultados";
                    }
                
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }
        [HttpGet]
        [Route("DatosPersonales/Mostrar/{id}")]
        public ConfirmacionDatosPersonales ConsultarUnDato(string id)
        {
            var respuesta = new ConfirmacionDatosPersonales();

            try
            {
                var dato = datosPersonalesCollection.Find(d => d.Id == id).FirstOrDefault();

                if (dato != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Detalle = string.Empty;
                    respuesta.Dato =  dato; 
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Detalle = "No se encontraron resultados";
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }
        [HttpPost]
        [Route("DatosPersonales/Nuevo")]
        public Confirmacion NuevoDatosPersonales(DatosPersonales datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                datosPersonalesCollection.InsertOne(datosPersonales);

                respuesta.Codigo = 0;
                respuesta.Detalle = string.Empty;
            }
            catch (Exception ex)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema: " + ex.Message;
            }

            return respuesta;
        }

        [HttpPost]
        [Route("DatosPersonales/Editar")]
        public async Task<Confirmacion> EditarDatosPersonales(DatosPersonales datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<DatosPersonales>.Filter.Eq("_id", ObjectId.Parse(datosPersonales.Id));
                var update = Builders<DatosPersonales>.Update
                    .Set("Cedula", datosPersonales.Cedula)
                    .Set("Nombre", datosPersonales.Nombre)
                    .Set("Apellido_P", datosPersonales.ApellidoP)
                    .Set("Apellido_M", datosPersonales.ApellidoM)
                    .Set("Edad", datosPersonales.Edad)
                    .Set("Genero", datosPersonales.Genero)
                    .Set("Fecha", datosPersonales.Fecha);

                var result = await datosPersonalesCollection.UpdateOneAsync(filter, update);

                if (result.ModifiedCount > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Detalle = "Datos personales actualizados correctamente";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Detalle = "No se encontró ningún registro para actualizar";
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema: " + ex.Message;
            }

            return respuesta;
        }
        [HttpPost]
        [Route("DatosPersonales/Eliminar")]
        public async Task<Confirmacion> EliminarDatosPersonalesAsync(DatosPersonales datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<DatosPersonales>.Filter.Eq("_id", ObjectId.Parse(datosPersonales.Id));
                var resultado = await datosPersonalesCollection.DeleteOneAsync(filter);

                if (resultado.DeletedCount == 1)
                {
                    respuesta.Codigo = 0;
                    respuesta.Detalle = string.Empty;
                }
                else {
                    respuesta.Codigo = -1;
                    respuesta.Detalle ="No se pudo eliminar";
                }

               
            }
            catch (Exception ex)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema: " + ex.Message;
            }

            return respuesta;
        }
    }
}
