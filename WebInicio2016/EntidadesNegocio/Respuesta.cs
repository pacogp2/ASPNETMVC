using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInicio2016.EntidadesNegocio
{
    public class Respuesta
    {
        public int IdEmpleado { get; set; }
        public Boolean OK    { get; set; }
        public String mensaje    { get; set; }
        public String redirection     { get; set; }
        public Object datos   { get; set; }
    }
}