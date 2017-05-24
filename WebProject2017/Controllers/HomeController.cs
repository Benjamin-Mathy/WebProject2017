using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject2017.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Welcome()
        {
            return View();
        }
    }
}