using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dog_Management.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string query)
        {
            return View();
        }
        public ActionResult Template(string path)
        {
            return View(path);
        }
    }
}