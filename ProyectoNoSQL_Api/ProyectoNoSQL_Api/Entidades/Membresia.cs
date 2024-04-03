using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Api.Entidades
{
    public class Membresia
    {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [BsonElement("tipo_membresia")]
            public string TipoMembresia { get; set; }

            [BsonElement("precio")]
            public int Precio { get; set; }

            [BsonElement("descripcion")]
            public string Descripcion { get; set; }

        }

        public class ConfirmacionMembresia
        {
            public int Codigo { get; set; }
            public string Detalle { get; set; }
            public List<Membresia> Datos { get; set; }
            public Membresia Dato { get; set; }
        }
    
}