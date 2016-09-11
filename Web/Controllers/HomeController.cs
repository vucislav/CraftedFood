using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Attributes;
using Web.Models;

namespace Web.Controllers
{
    [AuthorizeUser]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["user"] != null) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(LoginModel model)
        {
            int userId = UserLogic.AuthorizeUser(model.Login, model.Password);
            if (userId != 0)
            {
                Session["userId"] = userId; // URADITI: da se upamte svi potrebni podaci
                Session.Timeout = model.RememberMe ? 525600 : 20;
                return Json(true);
            }
            return Json(false);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}