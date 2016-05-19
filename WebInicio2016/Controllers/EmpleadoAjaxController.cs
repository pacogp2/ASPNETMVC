using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInicio2016.EntidadesNegocio;
using WebInicio2016.CapaNegocio;
using SitioWebMVC.MisControles;

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
    public class EmpleadoAjaxController : Controller
    {
        // GET: EmpleadoMan
        public ActionResult Index()
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new List<enEmpleado>()
            };
            return View();
        }

        // GET: EmpleadoMan
        public JsonResult Lista(String Apellido, String Nombre)
        {
            Respuesta res = new Respuesta
            {
                IdEmpleado = 0,
                OK = true,
                mensaje = "",
                datos = new List<enEmpleado>()
            };
            int NumRegistro = 0;
            int TamPagina = 4;
            Session["NumRegistro"] = NumRegistro;
            Session["TamPagina"] = TamPagina;
            Session["FiltroApellido"] = Apellido;
            Session["FiltroNombre"] = Nombre;
            List<enEmpleado> lenEmpleado = new List<enEmpleado>();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                int NumRegistros = ocnEmpleado.NumRegistrosFiltro(Apellido, Nombre);
                if (NumRegistros > 0)
                {
                    res.IdEmpleado = NumRegistros;
                    Session["NumRegistrosFiltro"] = NumRegistros;
                }
                lenEmpleado = ocnEmpleado.ListarFiltro(NumRegistro, TamPagina, Apellido, Nombre);
                res.datos = lenEmpleado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }
            return this.Json(res, JsonRequestBehavior.AllowGet);
        }

        // GET: EmpleadoMan
        public JsonResult SiguientePagina()
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = new List<enEmpleado>()
            };
            int NumRegistro = (int)Session["NumRegistro"];
            int TamPagina = (int)Session["TamPagina"];
            String Apellido = Session["FiltroApellido"].ToString();
            String Nombre = Session["FiltroNombre"].ToString();

            int RegistroMostrar = NumRegistro + TamPagina;
            if ((int)Session["NumRegistrosFiltro"] > ((int)Session["NumRegistro"] + TamPagina))
            {
                Session["NumRegistro"] = RegistroMostrar;
                NumRegistro = RegistroMostrar;
            }
            List<enEmpleado> lenEmpleado = new List<enEmpleado>();
            try
            {
                cnEmpleado ocnEmpleado = new cnEmpleado();
                lenEmpleado = ocnEmpleado.ListarFiltro(NumRegistro, TamPagina, Apellido, Nombre);
                res.datos = lenEmpleado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }

            return this.Json(res, JsonRequestBehavior.AllowGet);
        }

        // GET: EmpleadoMan
        public JsonResult Crear(String Apellido, String Nombre)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = -1
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
                //        resultado = -1;
                res.datos = resultado;
            }
            catch (Exception ex)
            {
                res.OK = false;
                res.mensaje = "Error a mostrar = " + ex.Message;
            }
            return this.Json(res, JsonRequestBehavior.AllowGet);
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
            enEmpleado oenEmpleado = new enEmpleado
            {
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
        public JsonResult Borrar(int? ID)
        {
            Respuesta res = new Respuesta
            {
                OK = true,
                mensaje = "",
                datos = -1
            };
            int resultado = -1;
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
            return this.Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}