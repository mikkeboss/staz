using System;
using System.Collections.Generic;
using System.Globalization;
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
            DatabaseHandler db = new DatabaseHandler();
            FundList = db.GetFundValues();

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CompareModel model)
        {
            CapitalCalculator calc = new CapitalCalculator();
            calc.Money = model.Money;
            calc.Percent = model.Percent;
            string[] dates = model.Range.Split('-');
            calc.StartDate = DateTime.ParseExact(dates[0], "MM/dd/yyyy ", CultureInfo.InvariantCulture);
            calc.StopDate = DateTime.ParseExact(dates[1], " MM/dd/yyyy", CultureInfo.InvariantCulture);
            calc.GetIncome(FundList);
            return View(model);
        }
    }
}
