using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using stazkainos.Models;

namespace stazkainos.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FundValue> Funds { get; set; }
    }
}