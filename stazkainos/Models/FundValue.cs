using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace stazkainos.Models
{
    [Table("dbo.FundVals")]
    public class FundValue
    {
        
        public int Id { get; set; }
        public DateTime fundDate { get; set; }
        public double value { get; set; }

        public FundValue(DateTime ndate, double nvalue)
        {
            fundDate = ndate;
            value = nvalue;
        }

        public FundValue()
        {
        }
    }
}