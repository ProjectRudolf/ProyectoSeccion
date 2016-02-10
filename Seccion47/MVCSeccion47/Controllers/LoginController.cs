using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCSeccion47.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult UsersLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSuccess(Models.LoginUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.users == "" || model.pass == "")
                    {
                        return ViewBag.Message = "Datos invalidos";
                    }
                    else
                    {
                        if (model.users == "Admin" && model.pass == "Admin")
                        {
                            return RedirectToAction("Principal", "Default");
                        }
                        else
                        {
                            ViewBag.Message = "Datos invalidos";
                            return View("UsersLogin");
                        }
                    }
                }
                else
                {
                    return View("UsersLogin");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}