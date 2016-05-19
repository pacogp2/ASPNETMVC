using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInicio2016.Models;

namespace WebInicio2016.Controllers
{
    public class EmpleadoEFController : Controller
    {
        private conNorthwind db = new conNorthwind();
        // GET: EmpleadoEF
        public ActionResult Lista()
        {
            var empleados = (from emp in db.Employees select emp).ToList();
        //    List<Employees> empleados = (from emp in db.Employees select emp).ToList();
            ViewBag.empleados = empleados;
            return View();
        }
    }
}