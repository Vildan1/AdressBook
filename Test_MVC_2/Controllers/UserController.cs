using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test_MVC_2.Models;

namespace Test_MVC_2.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        Test_MVC_2Entities db = new Test_MVC_2Entities();

        [AllowAnonymous] // Tüm kullanıcılar login sayfasına ulşabilmesi için bu tanımı yaptık.
        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            //UserManager ile login olma
            var loggedUser = db.Users.Where(x => x.Email == user.Email && x.Sifre == user.Sifre).FirstOrDefault();
            if (loggedUser != null)
            {
                FormsAuthentication.SetAuthCookie(loggedUser.Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}