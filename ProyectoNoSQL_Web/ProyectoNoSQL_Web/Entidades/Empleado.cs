
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoNoSQL_Web.Entidades
{
    public class Empleado
    {

        public string Id { get; set; }


        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

  
        public string Genero { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public DateTime FechaInicioEmpleo { get; set; }

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
