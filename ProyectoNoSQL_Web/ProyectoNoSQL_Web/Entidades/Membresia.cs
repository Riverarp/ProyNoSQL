using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Web.Entidades
{
    public class Membresia
    {
       
        public string Id { get; set; }

       
        public string TipoMembresia { get; set; }

        public string Descripcion { get; set; }

        public int Precio { get; set; }

    
      

    }

    public class ConfirmacionMembresia
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<Membresia> Datos { get; set; }
        public Membresia Dato { get; set; }
    }
}