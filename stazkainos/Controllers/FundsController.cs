using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using stazkainos.DAL;
using stazkainos.Models;

namespace stazkainos.Controllers
{
    public class FundsController : Controller
    {
        private List<FundValue> fundList;
        public FundsController()
        {
            DatabaseHandler db = new DatabaseHandler();
            fundList = db.GetFundValues();
        }
        public ActionResult FundsTable()
        {
            return View(fundList);
        }

        public ActionResult FundsChart()
        {
            var xDates = fundList.Select(i => i.fundDate.ToShortDateString()).ToArray();


            var doublevals = fundList.Select(i => i.value).ToArray();
            object[] serie = doublevals.Cast<object>().ToArray();

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("Fundusz")
        .SetXAxis(new XAxis
        {
            Categories = xDates,
            TickInterval = 50,
            Labels = new XAxisLabels
            {
                Align = HorizontalAligns.Right,
                Rotation = -90
            }
        }).SetTitle(new Title
        {
            Text = "Fundusz"
        })
        .SetSeries(new Series
        {
            Name = "Wartość",
            Data = new Data(serie)
        });

           return View(chart);
           
        }

        [HttpPost]
        public ActionResult FundsChart(string val)//string start, string stop)
        {
            string[] dates = val.Split('-');
            DateTime startDate = DateTime.ParseExact(dates[0], "MM/dd/yyyy ", CultureInfo.InvariantCulture);
            DateTime stopDate = DateTime.ParseExact(dates[1], " MM/dd/yyyy", CultureInfo.InvariantCulture);
            var cutList = from FundVal in fundList where FundVal.fundDate > startDate && FundVal.fundDate < stopDate select FundVal;
            List<FundValue> trimList = cutList.ToList();
            var xDates = trimList.Select(i => i.fundDate.ToShortDateString()).ToArray();
            var doublevals = trimList.Select(i => i.value).ToArray();
            object[] serie = doublevals.Cast<object>().ToArray();
            int interval;
            if (trimList.Count/80 > 1)
                interval = trimList.Count/80;
            else
                interval = 1;
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("Fundusz")
       .SetXAxis(new XAxis
       {
           Categories = xDates,
           TickInterval = interval,
           Labels = new XAxisLabels
           {
               Align = HorizontalAligns.Right,
               Rotation = -90
           }
       }).SetTitle(new Title
       {
           Text = "Fundusz"
       })
       .SetSeries(new Series
       {
           Name = "Wartość",
           Data = new Data(serie)
       });

            return View(chart);
       }

    }
}
