using System.Collections.Generic;
using System.Web.Mvc;
using stazkainos.DAL;
using stazkainos.Models;

namespace stazkainos.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //CSVReader reader = new CSVReader();
            //List<FundValue> fundList = new List<FundValue>();
            //fundList = reader.Parse();
            // DatabaseContext context = new DatabaseContext();
            return View();
        }

        
    }
}
