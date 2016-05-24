using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stazkainos.DAL;
using stazkainos.Models;

namespace stazkainos.Controllers
{
    public class CompareController : Controller
    {
        //
        // GET: /Compare/
        private CompareModel Comparedata;
        private List<FundValue> FundList;
        public CompareController()
        {
            Comparedata=new CompareModel();
            //DatabaseHandler db = new DatabaseHandler();
            //FundList = db.GetFundValues();

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CompareModel model)
        {
            return View(model);
        }
    }
}
