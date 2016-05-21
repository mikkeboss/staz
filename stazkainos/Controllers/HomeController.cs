using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stazkainos.Models;

namespace stazkainos.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            CSVReader reader = new CSVReader();
            reader.Parse();

            return View();
        }

    }
}
