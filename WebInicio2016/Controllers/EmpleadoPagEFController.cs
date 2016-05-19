using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInicio2016.Models;

namespace WebInicio2016.Controllers
{
    public class EmpleadoPagEFController : Controller
    {
        private conNorthwind db = new conNorthwind();
        // GET: EmpleadoPagEF
        public ActionResult Lista()
        {
            List<Employees> empleados = (from emp in db.Employees select emp).ToList();
            ViewBag.empleados = empleados;
            return View();
        }
    }
}