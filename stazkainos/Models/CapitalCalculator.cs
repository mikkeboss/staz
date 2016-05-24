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
            var startandstop = GetValidDate(fundList);
            var fundincome = GetFundIncome(startandstop);
            return new Tuple<decimal, decimal>(fundincome, 0);
        }

        private Tuple<FundValue, FundValue> GetValidDate(List<FundValue> fundList)
        {
            var starttrim = from fundval in fundList where fundval.fundDate >= StartDate select fundval;
            var stoptrim = (from fundval in starttrim where fundval.fundDate <= StopDate select fundval).ToList();

                 return new Tuple<FundValue, FundValue>(stoptrim.ElementAt(0), stoptrim.ElementAt(stoptrim.Count-1));
        }

        private decimal GetFundIncome(Tuple<FundValue, FundValue> dates)
        {
            double unitcount =decimal.ToDouble(this.Money)/dates.Item1.value;
            decimal income = (decimal)(unitcount*dates.Item2.value);
            return income;
        }
    }

    
}