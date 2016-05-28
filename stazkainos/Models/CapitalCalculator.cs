using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;

namespace stazkainos.Models
{

    public class CapitalCalculator : CompareModel
    {
        public DotNet.Highcharts.Highcharts Chart;
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        private const int Capitalization = 12;
        public Tuple<Decimal, Decimal> GetIncome(List<FundValue> fundList)
        {
            var startandstop = GetValidDate(fundList);
            Tuple<FundValue,FundValue> dates = new Tuple<FundValue, FundValue>(startandstop.ElementAt(0),
                startandstop.ElementAt(startandstop.Count - 1));
            var fundIncome = GetFundIncome(dates);
            var depositIncome = GetDepositIncome(dates);
            var partialFundIncome = GetFundPartialIncomeList(startandstop);
            var partialDepositIncome = GetPartialDepositIncome(CountMonths(startandstop.ElementAt(0).fundDate,startandstop.ElementAt(startandstop.Count - 1).fundDate));
            GetChart(startandstop, partialFundIncome, partialDepositIncome);
            return new Tuple<decimal, decimal>(fundIncome, decimal.Round(depositIncome,2));
        }

        private decimal GetDepositIncome(Tuple<FundValue, FundValue> dates)
        {
            int numberOfMonths;
            if (dates.Item2.fundDate.Day < dates.Item1.fundDate.Day)
                numberOfMonths = CountMonths(dates.Item1.fundDate,dates.Item2.fundDate)- 1;
            else
                numberOfMonths = CountMonths(dates.Item1.fundDate, dates.Item2.fundDate);
            if (numberOfMonths == 0)
                return 0;
            var income = GetPartialDepositIncome(numberOfMonths);
            return income.ElementAt(income.Count-1);
        }

        private List<decimal> GetPartialDepositIncome(int numberOfMonths)
        {
            List<decimal> monthlyIncome = new List<decimal>();
            decimal totalIncome = Money;
            if(numberOfMonths>0)
                for (int counter = 0; counter < numberOfMonths; counter++)
                {
                    totalIncome += (decimal)(decimal.ToDouble(totalIncome)*(Percent/Capitalization));
                    monthlyIncome.Add(decimal.Round(totalIncome-Money,2));
                }
            else
                for (int counter = 0; counter < numberOfMonths; counter++)
                {
                    monthlyIncome.Add(0);
                }
            return monthlyIncome;
        }

        private static int CountMonths(DateTime start, DateTime end)
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
            return decimal.Round(income-Money,2);
        }

        private List<decimal> GetFundPartialIncomeList(List<FundValue> fundList)
        {
            List<decimal> partialIncome = new List<decimal>();
            double unitcount = decimal.ToDouble(Money) / fundList.ElementAt(0).value;
            foreach (var fund in fundList)
            {
                decimal valueToAdd=(decimal)(fund.value * unitcount)-Money;
                partialIncome.Add(decimal.Round(valueToAdd,2));
            }
            return partialIncome;
        }

      
        public void GetChart(List<FundValue> datesList,List<decimal> fund, List<decimal> deposit)
        {
            ChartDataBuilder cdb = new ChartDataBuilder();
            cdb.ProcessData(datesList.Select(x => x.fundDate).ToList(), fund, deposit);
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("Porownanie");
            Series s1 = new Series
            {
                Data = cdb.FundData,
                Name = "Fundusz"
            };
            Series s2 = new Series
            {
                Data = cdb.DepositData,
                Name = "Lokata"
            };
            Series[] serie = new Series[2];
            serie[0] = s1;
            serie[1] = s2;
            var startandstop = GetValidDate(datesList);
            int interval;
            if (startandstop.Count / 80 > 1)
                interval = startandstop.Count / 80;
            else
                interval = 1;

            var xlabels = cdb.ChartData.Select(x => x.Date.ToShortDateString()).ToArray();
            chart.SetXAxis(new XAxis
            {
                Categories = xlabels,
                TickInterval = interval,
                Labels = new XAxisLabels
                {
                    Align = HorizontalAligns.Right,
                    Rotation = -90
                }
            }).SetSeries(serie);
            chart.SetTitle(new Title
            {
                Text = "Porównanie lokaty i funduszu"
            });
            
            Chart= chart;
        }
    }

    
}