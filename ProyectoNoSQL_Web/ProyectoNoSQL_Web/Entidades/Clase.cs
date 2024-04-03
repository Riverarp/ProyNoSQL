using ProyectoNoSQL_Web.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Web.Entidades
{
    public class Clase
    {
      
        public string Id { get; set; }

        public string NombreClase { get; set; }

        public string Descripcion { get; set; }

    
        public string Horario { get; set; }

        public string Instructor { get; set; }

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
