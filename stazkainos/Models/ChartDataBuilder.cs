using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Highcharts.Helpers;

namespace stazkainos.Models
{
    public class ChartDataBuilder
    {
        public List<ChartData> ChartData { get; set; }
        public Data FundData { get; set; }
        public Data DepositData { get; set; }
        public ChartDataBuilder()
        {
            ChartData=new List<ChartData>();
        }
        public void ProcessData(List<DateTime> dates, List<decimal> fund, List<decimal> deposit)
        {
            for (int counter = 0; counter < dates.Count; counter++)
            {
                ChartData.Add(new ChartData()
                {
                    Date = dates.ElementAt(counter),
                    Fund = fund.ElementAt(counter)
                });
            }
            DateTime capitalizationDate = ChartData.ElementAt(0).Date;
            for (int k = 0; k < deposit.Count; k++)
            {
                int index = ChartData.FindIndex(r => r.Date == capitalizationDate);
                if (index == -1)
                {
                    int insertPosition = ChartData.FindLastIndex(r => r.Date < capitalizationDate) + 1;
                    ChartData.Insert(insertPosition, new ChartData()
                    {
                        Date = capitalizationDate,
                        Deposit = deposit.ElementAt(k),
                        Fund = decimal.Round(ChartData.ElementAt(insertPosition).Fund,2)
                    });
                }
                else
                {
                    ChartData.ElementAt(index).Deposit = deposit.ElementAt(k);
                 }
                capitalizationDate = capitalizationDate.AddMonths(1);
            }
            decimal depositVal = 0;
            for (int l = 0; l < ChartData.Count; l++)
            {
                if (ChartData.ElementAt(l).Deposit == 0)
                    ChartData.ElementAt(l).Deposit = depositVal;
                else
                {
                    depositVal = ChartData.ElementAt(l).Deposit;
                }
            }
            FundData = new Data(ChartData.Select(x=>x.Fund).Cast<object>().ToArray());
            DepositData = new Data(ChartData.Select(x => x.Deposit).Cast<object>().ToArray());
        }
       
    }
}