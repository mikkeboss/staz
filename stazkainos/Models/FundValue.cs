using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stazkainos.Models
{
    public class FundValue
    {
        private DateTime date;
        private double value;

        public FundValue(DateTime date, double value)
        {
            this.date = date;
            this.value = value;
        }
    }
}