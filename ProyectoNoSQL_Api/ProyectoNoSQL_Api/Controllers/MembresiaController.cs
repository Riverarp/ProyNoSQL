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
using static ProyectoNoSQL_Api.Entidades.Membresia;

namespace ProyectoNoSQL_Api.Controllers
{
    public class MembresiaController : ApiController
    {
        private readonly IMongoCollection<Membresia> MembresiasCollection;


        public MembresiaController()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionMongo"];
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("Gimnasio");
            MembresiasCollection = database.GetCollection<Membresia>("membresias");
        }




        [HttpGet]
        [Route("Membresias/Mostrar")]
        public ConfirmacionMembresia ConsultarMembresia()
        {
            var respuesta = new ConfirmacionMembresia();

            try
            {

                var datos = MembresiasCollection.Find(_ => true).ToList();

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
        [Route("Membresias/MostrarUno")]
        public ConfirmacionMembresia ConsultarUnDato(string id)
        {
            var respuesta = new ConfirmacionMembresia();

            try
            {
                var dato = MembresiasCollection.Find(d => d.Id == id).FirstOrDefault();

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
        [Route("Membresias/Nuevo")]
        public Confirmacion NuevoMembresia(Membresia datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                MembresiasCollection.InsertOne(datosPersonales);

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
        [Route("Membresias/Editar")]
        public async Task<Confirmacion> EditarDatosPersonales(Membresia datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Membresia>.Filter.Eq("_id", ObjectId.Parse(datosPersonales.Id));
                var update = Builders<Membresia>.Update
                    .Set(c => c.TipoMembresia, datosPersonales.TipoMembresia)
                    .Set(c => c.Descripcion, datosPersonales.Descripcion)
                    .Set(c => c.Precio, datosPersonales.Precio);

                var result = await MembresiasCollection.UpdateOneAsync(filter, update);

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
        [Route("Membresias/Eliminar")]
        public async Task<Confirmacion> EliminarDatosAsync(string id)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Membresia>.Filter.Eq("_id", ObjectId.Parse(id));
                var resultado = await MembresiasCollection.DeleteOneAsync(filter);

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

