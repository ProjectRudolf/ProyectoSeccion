using MVCSeccion47.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSeccion47.Controllers
{
    public class EstadoController : Controller
    {
        private SeccionBD db = new SeccionBD();
        // GET: Estado
        public ActionResult _ListaEstados()
        {
            var listaTipos = db.Estados;

            if (!listaTipos.Any())
            {
                return HttpNotFound();
            }

            return PartialView(listaTipos);
        }
    }
}