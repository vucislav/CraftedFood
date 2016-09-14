using Core.DTOs;
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
    public class HomeController : MyBaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["user"] != null) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            int userId = UserLogic.AuthorizeUser(model.Login, model.Password);
            if (userId != 0)
            {
                Session["userId"] = userId; // URADITI: da se upamte svi potrebni podaci
                Session.Timeout = model.RememberMe ? 525600 : 20;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            UserLogic.ResetPasswordMail(model.Login);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult PasswordReset(Guid token)
        {
            return View(new PasswordResetModel(token));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel model)
        {
            // URADITI: silne one neke validacije za PasswordReset
            if (ModelState.IsValid)
            {
                UserLogic.ResetPassword(model.Guid, model.NewPassword);
            }
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                UserLogic.CreateUser(new UserDTO
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                });
            }
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View(CompanyModel.GetCompaniesForUser((int)Session["UserId"]));
        }


        public ActionResult Company(int id)
        {
            return View(new CompanyModel(id));
        }


        // URADITI: da moz se udje u keterin kompaniju
        public ActionResult Kettering(int id)
        {
            return View(new KetteringModel(id));
        }

        [HttpPost]
        public ActionResult CreateCompany(CompanyModel model)
        {
            if (model.IsKettering)
            {
                KetteringLogic.Create(new KetteringDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Phone = model.Phone
                }, (int)Session["userId"]);
            }
            else
            {
                CompanyLogic.Create(new CompanyDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Phone = model.Phone
                }, (int)Session["userId"]);
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditProfile()
        {
            return View(new UserModel((int)Session["userId"]));
        }

        [HttpPost]
        public ActionResult EditProfile(UserModel user)
        {
            UserLogic.EditProfile(new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Phone = user.Phone
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(UserModel user)
        {
            UserLogic.ChangePassword(user.UserId, user.OldPassword, user.Password);
            return RedirectToAction("Index");
        }
    }
}