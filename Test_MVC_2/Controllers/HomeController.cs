using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_MVC_2.Models;


namespace Test_MVC_2.Controllers
{
    [Authorize]
    // Sadece login olanlar görebilir
    public class HomeController : Controller
    {
        Test_MVC_2Entities db = new Test_MVC_2Entities();


        public ActionResult Index()
        {

            if (!User.IsInRole("U"))
            {
                var list = db.UserInfoes.Where(u => u.olusturanKullanici == User.Identity.Name).ToList(); // objeye çevirip attığımzda IEnumerable ile bunu yakalayıp liste halinde basabiliriz.
                return View(list);
            }
            else
            {
                var list = db.UserInfoes.ToList(); // objeye çevirip attığımzda IEnumerable ile bunu yakalayıp liste halinde basabiliriz.
                return View(list);
            }

            

            //db.UserInfoes.ToList(); --> direkt UserInfo sınıfını geri döndürür. View'a return ettiğimiz zaman IEnumerable ile bu modele erişemeyiz

        }


        [HttpGet]

       
        public ActionResult Add()
        {
            return View();
        }

        

        [HttpPost]

        public ActionResult Add(UserInfo user)
        {
            UserInfo test = new UserInfo();
            test.Ad = user.Ad;
            test.Soyad = user.Soyad;
            test.Mail = user.Mail;
            test.Telefon = user.Telefon;
            test.Adres = user.Adres;
            test.Il = user.Il;
            test.Ilce = user.Ilce;
            test.olusturanKullanici = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Set<UserInfo>().Add(test); // db.Entry(test).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);

        }


        
        public ActionResult Edit(int id)
        {
            var user = db.UserInfoes.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult EditSave(UserInfo userInfo)
        {
            var user = db.UserInfoes.Where(x => x.Id == userInfo.Id).FirstOrDefault();
            user.Ad = userInfo.Ad;
            user.Soyad = userInfo.Soyad;
            user.Mail = userInfo.Mail;
            user.Telefon = userInfo.Telefon;
            user.Adres = userInfo.Adres;
            user.Il = userInfo.Il;
            user.Ilce = userInfo.Ilce;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

            if (ModelState.IsValid)
            {
                db.Set<UserInfo>(); // db.Entry(test).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInfo);

        }


        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var user = db.UserInfoes.Where(x => x.Id == Id).FirstOrDefault();
            db.Set<UserInfo>().Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}