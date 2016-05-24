using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stazkainos.Models
{
    public class CapitalCalculator : CompareModel
    {
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }


        public Tuple<Decimal, Decimal> GetIncome(List<FundValue> fundList)
        {
           
            
            return new Tuple<decimal, decimal>(0,0);
        }

    }

    
}