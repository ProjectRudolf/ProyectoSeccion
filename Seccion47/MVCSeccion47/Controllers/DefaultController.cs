using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSeccion47.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Principal()
        {
            return View();
        }
    }
}