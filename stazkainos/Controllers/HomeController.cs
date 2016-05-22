using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stazkainos.DAL;
using stazkainos.Models;
using System.Data.SqlClient;
using EntityState = System.Data.Entity.EntityState;

namespace stazkainos.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            CSVReader reader = new CSVReader();
            List<FundValue> fundList = new List<FundValue>();
            fundList = reader.Parse();
           // DatabaseContext context = new DatabaseContext();
           // var a =context.Database.ExecuteSqlCommand("GetFunds");
           // var f = context.Funds;
           // context.Funds.AddRange(fundList);
           // context.SaveChanges();
           // var ff = context.Funds;
            DatabaseHandler db = new DatabaseHandler();
            var temp=db.GetFundValues();

           
            
            





            // context.Dispose();
            return View(temp);
        }

    }
}
