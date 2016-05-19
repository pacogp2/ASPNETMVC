using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// http://lduenash.blogspot.com.es/2015/03/el-demo-del-dia-paginacion-y-ordenacion.html
/// 
///
namespace WebInicio2016.EntidadesNegocio
{
    public class enEmpleado
    {
        private int? idEmpleado;
        private string apellido; 
        private string nombre;
        private DateTime fechaNacimiento; 
        private byte[] foto; 

        public int? IdEmpleado
        { 
          get {  return idEmpleado;   }
          set {  idEmpleado = value; }
        }
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        public byte[] Foto
        {
            get { return foto; }
            set { foto = value; }
        }
    }
 /*   public class enEmpleado
    {
        public int? IdEmpleado { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] Foto { get; set; }
    }  */
}