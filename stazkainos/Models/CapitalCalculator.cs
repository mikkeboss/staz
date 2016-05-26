using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stazkainos.Models
{
    //zrobione liczenie zysku z funduszu,  lista zysku czastkowego
    //TODO liczenie zysku z lokaty, lista dla kapitalizacji odsetek

    public class CapitalCalculator : CompareModel
    {
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        private const int Capitalisation = 12;
        public Tuple<Decimal, Decimal> GetIncome(List<FundValue> fundList)
        {
            var startandstop = GetValidDate(fundList);
            Tuple<FundValue,FundValue> dates = new Tuple<FundValue, FundValue>(startandstop.ElementAt(0),
                startandstop.ElementAt(startandstop.Count - 1));
            var fundIncome = GetFundIncome(dates);
            var depositIncome = GetDepositIncome(dates);
            return new Tuple<decimal, decimal>(fundIncome, decimal.Round(depositIncome,2));
        }

        private decimal GetDepositIncome(Tuple<FundValue, FundValue> dates)
        {
            int numberOfMonths;
            if (dates.Item2.fundDate.Day < dates.Item1.fundDate.Day)
                numberOfMonths = TotalMonths(dates.Item1.fundDate,dates.Item2.fundDate)- 1;
            else
                numberOfMonths = TotalMonths(dates.Item1.fundDate, dates.Item2.fundDate);
            var income = GetPartialDepositIncome(numberOfMonths);
            return income;
        }

        private decimal GetPartialDepositIncome(int numberOfMonths)
        {
            decimal totalIncome = Money;
            for (int counter = 0; counter < numberOfMonths; counter++)
            {
                totalIncome += (decimal)(decimal.ToDouble(totalIncome)*(Percent/Capitalisation));
            }
            return totalIncome-Money;
        }

        private static int TotalMonths(DateTime start, DateTime end)
        {
            return (end.Year * 12 + end.Month) - (start.Year * 12 + start.Month);
        }

        private List<FundValue> GetValidDate(List<FundValue> fundList)
        {
            var starttrim = from fundval in fundList where fundval.fundDate >= StartDate select fundval;
            var stoptrim = (from fundval in starttrim where fundval.fundDate <= StopDate select fundval).ToList();

            return stoptrim;
        }

        private decimal GetFundIncome(Tuple<FundValue, FundValue> dates)
        {
            double unitcount =decimal.ToDouble(this.Money)/dates.Item1.value;
            decimal income = (decimal)(unitcount*dates.Item2.value);
            return income-Money;
        }

        private List<decimal> GetFundPartialIncomeList(List<FundValue> fundList)
        {
            List<decimal> partialIncome = new List<decimal>();
            foreach (var fund in fundList)
            {
                partialIncome.Add((decimal)(fund.value * decimal.ToDouble(Money)));
            }
            return partialIncome;
        }
    }

    
}