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
    public class EmpleadoController : ApiController
    {
        private readonly IMongoCollection<Empleado> datosPersonalesCollection;


        public EmpleadoController()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionMongo"];
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("Gimnasio");
            datosPersonalesCollection = database.GetCollection<Empleado>("empleados");
        }




        [HttpGet]
        [Route("Empleados/Mostrar")]
        public ConfirmacionEmpleado ConsultarDatosPersonales()
        {
            var respuesta = new ConfirmacionEmpleado();

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
        [Route("Empleados/MostrarUno")]
        public ConfirmacionEmpleado ConsultarUnDato(string id)
        {
            var respuesta = new ConfirmacionEmpleado();

            try
            {
                var dato = datosPersonalesCollection.Find(d => d.Id == id).FirstOrDefault();

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
        [Route("Empleados/Nuevo")]
        public Confirmacion NuevoDatosPersonales(Empleado datosPersonales)
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

        [HttpPut]
        [Route("Empleados/Editar")]
        public async Task<Confirmacion> EditarDatosPersonales(Empleado datosPersonales)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Empleado>.Filter.Eq("_id", ObjectId.Parse(datosPersonales.Id));
                var update = Builders<Empleado>.Update
                    .Set(c => c.Nombre, datosPersonales.Nombre)
                    .Set(c => c.Apellido, datosPersonales.Apellido)
                    .Set(c => c.FechaNacimiento, datosPersonales.FechaNacimiento)
                    .Set(c => c.Genero, datosPersonales.Genero)
                    .Set(c => c.Direccion, datosPersonales.Direccion)
                    .Set(c => c.Telefono, datosPersonales.Telefono)
                    .Set(c => c.Email, datosPersonales.Email)
                    .Set(c => c.FechaInicioEmpleo, datosPersonales.FechaInicioEmpleo)
                    .Set(c => c.Salario, datosPersonales.Salario);

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

        [HttpDelete]
        [Route("Empleados/Eliminar")]
        public async Task<Confirmacion> EliminarDatosPersonalesAsync(string id)
        {
            var respuesta = new Confirmacion();

            try
            {
                var filter = Builders<Empleado>.Filter.Eq("_id", ObjectId.Parse(id));
                var resultado = await datosPersonalesCollection.DeleteOneAsync(filter);

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

