using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_MVC_2.Controllers
{
    public class AccessController : Controller
    {
        // GET: Default
        public ActionResult Error()
        {
            return View();
        }
    }
}