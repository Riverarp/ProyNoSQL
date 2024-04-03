using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Api.Entidades
{
    public class Clase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre_clase")]
        public string NombreClase { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("horario")]
        public string Horario { get; set; }

        [BsonElement("instructor")]
        public string Instructor { get; set; }

        [BsonElement("duracion")]
        public int Duracion { get; set; }
    }
    public class ConfirmacionClase
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<Clase> Datos { get; set; }
        public Clase Dato { get; set; }
    }
}