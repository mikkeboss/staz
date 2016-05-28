using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using stazkainos.DAL;
using stazkainos.Models;

namespace stazkainos.Controllers
{
    public class CompareController : Controller
    {
        //
        // GET: /Compare/
        private List<FundValue> FundList;
        public CompareController()
        {
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
            if (ModelState.IsValid)
            {

                CapitalCalculator calc = new CapitalCalculator
                {
                    Money = model.Money,
                    Percent = model.Percent
                };
                string[] dates = model.Range.Split('-');
                calc.StartDate = DateTime.ParseExact(dates[0], "MM/dd/yyyy ", CultureInfo.InvariantCulture);
                calc.StopDate = DateTime.ParseExact(dates[1], " MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = calc.GetIncome(FundList);
                ViewBag.depositIncome = result.Item2;
                ViewBag.fundIncome = result.Item1;
                ViewBag.chart = calc.Chart;
                return View(model);
            }
            return View(model);
        }
    }
}
