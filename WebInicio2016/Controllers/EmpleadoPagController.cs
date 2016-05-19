using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInicio2016.EntidadesNegocio;
using WebInicio2016.CapaNegocio;


namespace WebInicio2016.Controllers
{
    public class EmpleadoPagController : Controller
    {
        List<enEmpleado> lenPagina;
        List<enEmpleado> lenEmpleado;
        int indicePaginaActual;
        int indiceUltimaPagina;
        int registrosPorPagina = 3;

        // GET: EmpleadoPag
        private void mostrarPagina()
        {
            lenPagina = new List<enEmpleado>();
            int inicio = indicePaginaActual * registrosPorPagina;
            int fin = inicio + registrosPorPagina;
            for (int i = inicio; i < fin; i++)
            {
                if (i == lenEmpleado.Count) break;
                lenPagina.Add(lenEmpleado[i]);
            }
        }

        public ActionResult ListaPag()
        {
            cnEmpleado ocnEmpleado = new cnEmpleado();
            lenEmpleado = ocnEmpleado.Listar();
            Session["Empleados"] = lenEmpleado;
            indiceUltimaPagina = (lenEmpleado.Count / registrosPorPagina);
            if (lenEmpleado.Count % registrosPorPagina == 0) indiceUltimaPagina--;
            ViewBag.IndiceUltimaPagina = indiceUltimaPagina;
            mostrarPagina();
            return View(lenPagina);
        }

        public PartialViewResult Ordenar(string campo)
        {
            ViewBag.Campo = campo;
            lenEmpleado = (List<enEmpleado>)Session["Empleados"];
            int n = 0;
            string simbolo = "▲";
            if (TempData[campo] != null)
            {
                if (TempData[campo].Equals(0))
                {
                    n = 1;
                    simbolo = "▼";
                }
                else simbolo = "▲";
            }
            TempData[campo] = n;
            ViewBag.Simbolo = simbolo;
            if (n == 0) lenEmpleado = lenEmpleado.OrderBy
            (x => x.GetType().GetProperty(campo).GetValue(x, null)).ToList();
            else lenEmpleado = lenEmpleado.OrderByDescending
            (x => x.GetType().GetProperty(campo).GetValue(x, null)).ToList();
            Session["Empleados"] = lenEmpleado;
            if (TempData["indicePaginaActual"] != null)
            {
                indicePaginaActual = (int)TempData["indicePaginaActual"];
                TempData["indicePaginaActual"] = indicePaginaActual;
            }
            mostrarPagina();
            return PartialView("Tabla", lenPagina);
        }

        public PartialViewResult Paginar(int pagina)
        {
            lenEmpleado = (List<enEmpleado>)Session["Empleados"];
            indicePaginaActual = pagina;
            TempData["indicePaginaActual"] = indicePaginaActual;
            mostrarPagina();
            return PartialView("Tabla", lenPagina);
        }
    }
}