using System.Data.Entity;
using stazkainos.Models;

namespace stazkainos.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FundValue> Funds { get; set; }
    }
}