using System;
using System.Collections.Generic;
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
        
           
            var doublevals = fundList.Select(i => i.value ).ToArray();
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
