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
    public class EmpleadoManController : Controller
    {
        // GET: EmpleadoMan
        public ActionResult Index()
        {
            Session["NumRegistro"] = 0;
            Session["TamPagina"] = 3;
            return View();
        }
        // POST: EmpleadoMan
        [HttpPost]
        public ActionResult Index(String Apellido, String Nombre)
        {
            Session["FiltroApellido"] = Apellido;
            Session["FiltroNombre"] = Nombre;
            cnEmpleado ocnEmpleado = new cnEmpleado();
            int NumRegistros = ocnEmpleado.NumRegistrosFiltro(Apellido, Nombre);
            if (NumRegistros > 0)
                Session["NumRegistrosFiltro"] = NumRegistros;
            return RedirectToAction("Lista");
        }

        // GET: EmpleadoMan
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
                String Apellido = Session["FiltroApellido"].ToString();
                String Nombre = Session["FiltroNombre"].ToString();
                int NumRegistro = (int)Session["NumRegistro"];
                int TamPagina = (int)Session["TamPagina"];
       //         Int16 NumRegistro = 2, TamPagina = 3;
                lenEmpleado = ocnEmpleado.ListarFiltro(NumRegistro, TamPagina, Apellido, Nombre);
        //        lenEmpleado = ocnEmpleado.Listar();
                res.datos = lenEmpleado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }
            int NumRegFil = (int)Session["NumRegistrosFiltro"];
            ViewBag.NumRegistrosFiltro = NumRegFil;
            ViewBag.respuesta = res;
            return View();
        }

       // GET: EmpleadoMan
        public ActionResult SiguientePagina()
        {

            int NumRegistro = (int)Session["NumRegistro"];
            int TamPagina = (int)Session["TamPagina"];
            int RegistroMostrar = NumRegistro + TamPagina;
            if ((int)Session["NumRegistrosFiltro"] > ((int)Session["NumRegistro"] + TamPagina))
            {
                Session["NumRegistro"] = RegistroMostrar;
            }
            return RedirectToAction("Lista");
        }

        // GET: EmpleadoMan
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }
        // POST: EmpleadoMan
        [HttpPost]
        public ActionResult Crear(String Apellido, String Nombre)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new enEmpleado()
            };
            enEmpleado oenEmpleado = new enEmpleado
            {
                Apellido = Apellido,
                Nombre = Nombre
            };
            int resultado = -1;
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                resultado = ocnEmpleado.AltaEmpleado(oenEmpleado);
                res.datos = resultado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }
            if (res.OK)
            {
                return RedirectToAction("Lista");
            }
            else
            {
                ViewBag.respuesta = res;
                return View();
            }
        }

        // GET: EmpleadoMan
        public ActionResult Detalle(int? ID)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new enEmpleado()
            };

            enEmpleado oenEmpleado = new enEmpleado();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                oenEmpleado = ocnEmpleado.ConsEmpleadoPorID(ID);
                res.datos = oenEmpleado;
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
        // GET: EmpleadoMan
        public ActionResult Editar(int? ID)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new enEmpleado()
            };

            enEmpleado oenEmpleado = new enEmpleado();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                oenEmpleado = ocnEmpleado.ConsEmpleadoPorID(ID);
                res.datos = oenEmpleado;
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
        // POST: EmpleadoMan
        [HttpPost]
        public ActionResult Editar(int? IdEmpleado, String Apellido, String Nombre)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new enEmpleado()
            };
            enEmpleado oenEmpleado = new enEmpleado { 
                IdEmpleado = IdEmpleado,
                Apellido = Apellido,
                Nombre = Nombre
            };
            int resultado = -1;
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                resultado = ocnEmpleado.ModiEmpleado(oenEmpleado);
                res.datos = resultado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }
            if (res.OK)
            {
                return RedirectToAction("Lista");
            }
            else
            {
                ViewBag.respuesta = res;
                return View();
            }
        }
        // GET: EmpleadoMan
        public ActionResult Borrar(int? ID)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new enEmpleado()
            };
            int resultado = -1;
      //      enEmpleado oenEmpleado = new enEmpleado();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                resultado = ocnEmpleado.BajaEmpleado(ID);
        //        resultado = -1;
                res.datos = resultado;
             }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            } 
            if (res.OK && resultado == 1)
            {
                return RedirectToAction("Lista");
            }
            else
            {
                ViewBag.respuesta = res;
                return View();
            }
        }
    }
}