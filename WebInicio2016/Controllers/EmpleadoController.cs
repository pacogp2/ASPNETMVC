using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInicio2016.EntidadesNegocio;
using WebInicio2016.CapaNegocio;

namespace WebInicio2016.Controllers
{
    //  Esta clase se crea para devolver la respuesta del controlador a la vista
    //  Está declarada en la carpeta EntidadesNegocio para usarla desde cualquier controlador
/*    public class Respuesta
    {
        public int IdEmpleado { get; set; }
        public Boolean OK { get; set; }
        public String mensaje { get; set; }
        public String redirection { get; set; }
        public Object datos { get; set; }
    }  */

    //  Conrolador para mostrar todos los empleados de la base de datos
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Lista()
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new List<enEmpleado>()
            };

            List<enEmpleado> lenEmpleado = new List<enEmpleado>();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                lenEmpleado = ocnEmpleado.Listar();
                res.datos = lenEmpleado;
                //        Session["Empleados"] = lenEmpleado;
            }
            catch (Exception ex)
            {  
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }  
            ViewBag.respuesta = res;
            return View();
        }
    }
}