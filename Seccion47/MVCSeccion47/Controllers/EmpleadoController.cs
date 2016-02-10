using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCSeccion47.Models;
using System.Linq.Dynamic;

namespace MVCSeccion47.Controllers
{
    public class EmpleadoController : Controller
    {
        private SeccionBD db = new SeccionBD();

        // GET: Empleado
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            //Get parameters

            // get Start (paging start index) and length (page size for paging)
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Get Sort columns value
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            var contactName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var country = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;
            using (SeccionBD dc = new SeccionBD())
            {
                var v = (from E in dc.vtaEmpleado where E.Eliminacion == true select E);
                //SEARCHING...
                if (!string.IsNullOrEmpty(contactName))
                {
                    v = v.Where(a => a.Nombre.Contains(contactName));
                }
                if (!string.IsNullOrEmpty(country))
                {
                    v = v.Where(a => a.Ficha == country);
                }
                //SORTING...  (For sorting we need to add a reference System.Linq.Dynamic)
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }

                totalRecords = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);

            }
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            var query = db.Estados.Select(x => new SelectListItem
            {
                Value = x.EstadosID.ToString(),
                Text = x.Estado
            });
            SelectList list = new SelectList(query, "Value", "Text");
            ViewBag.Estados = list;
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "EmpleadoID,Ficha,Nombre,ApePa,ApeMa,RFC,SituacionContractual,UbicacionLaboral,Rol,Region,ClaveDepartamento,Nivel,ClaveAsiste,Domicilio,Estado,Ciudad,TelefonoCasa,TelefonoCelular,CorreoElectronico,Banco,Sucursal,NumeroCuenta,ClaveInterbancaria,Observaciones,FechaInserta,FechaModifica")]
        public ActionResult Create(Empleado empleado, FormCollection collection)
        {
            string situacion = collection["situacion"];
            string ubicacion = collection["ubicacion"];
            string region = collection["regionvalue"];
            string estado = collection["estadovalue"];
            string ciudad = collection["ciudadvalue"];

            empleado.SituacionContractual = situacion;
            empleado.UbicacionLaboral = ubicacion;
            empleado.Region = region;
            empleado.Estado = estado;
            empleado.Ciudad = ciudad;
            empleado.FechaInserta = DateTime.Now.Date;
            empleado.FechaModifica = DateTime.Now.Date;
            empleado.Eliminacion = true;

            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            var idestado = (from i in db.Estados where i.Estado == empleado.Estado select i.EstadosID).SingleOrDefault();
            SelectList ListEstados = new SelectList(db.Estados.ToList(), "EstadosID", "Estado", idestado);
            var ciudades = db.Municipios.Where(x => x.EstadosID == idestado).Select(x => new { x.MunicipiosID, x.Municipio }).ToList();
            var idciudad = ciudades.Where(x => x.Municipio == empleado.Ciudad).Select(x => new { x.MunicipiosID }).SingleOrDefault();
            SelectList ListCiudades = new SelectList(ciudades, "MunicipiosID", "Municipio", idciudad.MunicipiosID);
            ViewData["Estados"] = ListEstados;
            ViewData["Ciudades"] = ListCiudades;
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Empleado empleado, FormCollection collection)
        {
            //var draw = Request.Form.GetValues("situacion").FirstOrDefault();
            string situacion = Request.Form.GetValues("situacion").FirstOrDefault();
            string ubicacion = Request.Form.GetValues("ubicacion").FirstOrDefault();
            string region = Request.Form.GetValues("regionvalue").FirstOrDefault();
            string estado = Request.Form.GetValues("estadovalue").FirstOrDefault();
            string ciudad = Request.Form.GetValues("ciudadvalue").FirstOrDefault();

            empleado.SituacionContractual = situacion;
            empleado.UbicacionLaboral = ubicacion;
            empleado.Region = region;
            empleado.Estado = estado;
            empleado.Ciudad = ciudad;
            empleado.FechaModifica = DateTime.Now.Date;
            empleado.Eliminacion = true;
            if (empleado.UbicacionLaboral == "TIERRA")
                empleado.Rol = null;

            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            empleado.Eliminacion = false;
            db.Entry(empleado).State = EntityState.Modified;
            db.SaveChanges();
            return Json(empleado.Nombre, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult Ciudades(string stateId)
        {
            int statId;
            List<SelectListItem> districtNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(stateId))
            {
                statId = Convert.ToInt32(stateId);
                List<Municipios> districts = db.Municipios.Where(x => x.EstadosID == statId).ToList();
                districts.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.Municipio, Value = x.MunicipiosID.ToString() });
                });
            }
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }
    }
}
