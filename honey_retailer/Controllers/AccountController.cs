using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using honey_retailer.Models;

namespace honey_retailer.Controllers
{
    public class AccountController : Controller
    {
        FormsAuthenticationProvider authProvider = new FormsAuthenticationProvider();

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильное имя пользователя или пароль");
                    TempData["message"] = "Ошибка аутентификации!";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}