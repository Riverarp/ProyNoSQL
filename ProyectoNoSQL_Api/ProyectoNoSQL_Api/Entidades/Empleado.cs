using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Api.Entidades
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [BsonElement("genero")]
        public string Genero { get; set; }

        [BsonElement("direccion")]
        public string Direccion { get; set; }

        [BsonElement("telefono")]
        public string Telefono { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("fecha_inicio_empleo")]
        public DateTime FechaInicioEmpleo { get; set; }

        [BsonElement("salario")]
        public int Salario { get; set; }

    }

    public class ConfirmacionEmpleado
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<Empleado> Datos { get; set; }
        public Empleado Dato { get; set; }
    }
}
