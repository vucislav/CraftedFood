using Core.DTOs;
using Core.Logic;
using System;
using System.Collections.Generic;
using System.IO;
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

                Session.Timeout = model.RememberMe ? 525600 : 525600; // URADITI: ovo drugo treba 20, ako ima remember me?
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

        [AllowAnonymous]
        public ActionResult AddMenu()
        {
            return View(new MenuModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddMenu(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                MenuLogic.Create(new MenuDTO
                {
                    KetteringId = model.KetteringId,
                    Name = model.Name
                });
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [AllowAnonymous]
        public ActionResult Menus(int id)
        {
            return View(MenuModel.GetMenusForKettering(id));
        }

        [AllowAnonymous]
        public ActionResult Menu(int id)
        {
            return View(new MenuModel(id));
        }

        [AllowAnonymous]
        public ActionResult AddMeal()
        {
            return View(new MealModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddMeal(MealModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    fileName = model.Image.FileName;
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    model.Image.SaveAs(path);
                }
                MealLogic.Create(new MealDTO
                {
                    MenuId = model.MenuId,
                    Title = model.Title,
                    Description = model.Description,
                    Image = fileName,
                    Quantity = model.Quantity,
                    UnitOfMeasureId = (int)model.UnitOfMeasures,
                    MealCategoryId = (int)model.MealCategories,
                    Price = model.Price
                });
            }
            return RedirectToAction("Menu", new { id = model.MenuId });
        }

        [AllowAnonymous]
        public ActionResult Meal(int id)
        {
            return View(new MealModel((int?)id));
        }

        private byte[] ConvertToByteArray(HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        [AllowAnonymous]
        public ActionResult getImg(string img)
        {
            return File(Server.MapPath("~/App_Data/" + img), "image/png");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddKetteringUser(int ketteringId, string username)
        {
            int? userId = UserLogic.GetIdByUsername(username);
            if (userId != null)
            {
                KetteringUserLogic.Create(new KetteringUserDTO
                {
                    KetteringId = ketteringId,
                    UserId = (int)userId
                }, Core.Enumerations.RoleEnum.User);
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddCompanyUser(int companyId, string username)
        {
            int? userId = UserLogic.GetIdByUsername(username);
            if (userId != null && userId != 0)
            {
                CompanyUserLogic.Create(new CompanyUserDTO
                {
                    CompanyId = companyId,
                    UserId = (int)userId
                }, Core.Enumerations.RoleEnum.User);
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [AllowAnonymous]
        public ActionResult Ratings(int id)
        {
            return View(RatingModel.GetRatingsForMeal(id));
        }

        [AllowAnonymous]
        public ActionResult AddRating(int id)
        {
            //URADITI: STA VEC TREBA
            return View(new RatingModel(id, 1));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddRating(RatingModel model)
        {
            if (ModelState.IsValid)
            {
                RatingLogic.Create(new RatingDTO
                {
                    MealId = model.MealId,
                    Mark = model.Mark,
                    Comment = model.Comment,
                    CompanyUserId = model.CompanyUserId
                });
            }
            return RedirectToAction("Ratings/" + model.MealId);
        }
        
        public ActionResult EditKettering(KetteringModel model)
        {
            KetteringLogic.Edit(new KetteringDTO
            {
                KetteringId = model.KetteringId,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Phone = model.Phone
            });
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult DeleteKettering(int id)
        {
            KetteringLogic.Delete(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult _PlaceOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                OrderLogic.Create(new OrderDTO
                {
                    MealId = model.MealId,
                    CompanyUserId = model.CompanyUserId,
                    Note = model.Note,
                    Comment = model.Comment,
                    Date = model.Date,
                });
            }
            return RedirectToAction("Meal/" + model.MealId);
        }

        [AllowAnonymous]
        public ActionResult Orders(int id)
        {
            return View(OrderModel.GetOrdersForCompanyUser(id));
            //return View(OrderModel.GetOrdersForKettering(id));
            //return View(OrderModel.GetOrdersForCompany(id));
        }

        [AllowAnonymous]
        public ActionResult DeleteOrder(int orderId, int companyUserId)
        {
            OrderLogic.Delete(orderId);
            return View("Orders", OrderModel.GetOrdersForCompanyUser(companyUserId));
        }
    }
}