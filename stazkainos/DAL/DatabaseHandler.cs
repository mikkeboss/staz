using System;
using System.Collections.Generic;
using System.Linq;
using stazkainos.Models;

namespace stazkainos.DAL
{
    public class DatabaseHandler
    {
        public int PopulateDb(List<FundValue> data)
        {
            for (int i = 0; i < data.Count; i += 20)
            {
                using (var context = new DatabaseContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Configuration.AutoDetectChangesEnabled = false;

                            for (int j = i; j < data.Count && j<data.Count; j++)
                                context.Funds.Add(data[j]);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            return 1;
                        }
                    }
                }
                
            }
            return 0;
        }

        public List<FundValue> GetFundValues()
        {
            List<FundValue> values;
            using (var context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand(@"Select * from dbo.FundVals");
                values = context.Funds.ToList();
            }
            return values;
        }
    }
}