using MongoDB.Bson;
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
    
    public class ClaseController : ApiController
    {
        private readonly IMongoCollection<Clase> ClasesCollection;

        public ClaseController()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionMongo"];
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("Gimnasio");
            ClasesCollection = database.GetCollection<Clase>("Clase");
        }




        [HttpGet]
        [Route("Clases/Mostrar")]
        public ConfirmacionClase ConsultarDatosClase()
        {
            var respuesta = new ConfirmacionClase();

            try
            {

                var datos = ClasesCollection.Find(_ => true).ToList();

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
        [Route("Clases/MostrarUno")]
        public ConfirmacionClase ConsultarUnDato(string id)
        {
            var respuesta = new ConfirmacionClase();

            try
            {
                var dato = ClasesCollection.Find(d => d.Id == id).FirstOrDefault();

                if (dato != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Detalle = string.Empty;
                    respuesta.Dato = dato;
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
        [Route("Clases/Nuevo")]
        public Confirmacion NuevoDatosClase(Clase datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                ClasesCollection.InsertOne(datosPersonales);

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

        [HttpPut]
        [Route("Clases/Editar")]
        public async Task<Confirmacion> EditarDatosClase(Clase datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Clase>.Filter.Eq("_id", ObjectId.Parse(datosPersonales.Id));
                var update = Builders<Clase>.Update
                    .Set(c => c.NombreClase, datosPersonales.NombreClase)
                    .Set(c => c.Descripcion, datosPersonales.Descripcion)
                    .Set(c => c.Horario, datosPersonales.Horario)
                    .Set(c => c.Instructor, datosPersonales.Instructor)
                    .Set(c => c.Duracion, datosPersonales.Duracion);

                var result = await ClasesCollection.UpdateOneAsync(filter, update);

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

        [HttpDelete]
        [Route("Clases/Eliminar")]
        public async Task<Confirmacion> EliminarDatosClaseAsync(string id)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Clase>.Filter.Eq("_id", ObjectId.Parse(id));
                var resultado = await ClasesCollection.DeleteOneAsync(filter);

                if (resultado.DeletedCount == 1)
                {
                    respuesta.Codigo = 0;
                    respuesta.Detalle = string.Empty;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Detalle = "No se pudo eliminar";
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
