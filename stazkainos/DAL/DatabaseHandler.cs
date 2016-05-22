using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using stazkainos.Models;

namespace stazkainos.DAL
{
    public class DatabaseHandler
    {
        private const string ConnectionString =
            "Data Source=SQL5013.myASP.NET;Initial Catalog=DB_9FE98E_staz;User Id=DB_9FE98E_staz_admin;Password=haslostaz123;";
        public int GetFunds()
        {
            List<FundValue> result;

            using (var con = new SqlConnection(ConnectionString))
            {
                using (var sqlCommand = new SqlCommand("dbo.GetFunds", con))
                {
                    using (var a = new SqlDataAdapter(sqlCommand))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                       con.Open();

                        var t = new DataTable();
                        a.Fill(t);

                       // result = CustomMapper<KitchenCategories>.Map(t);
                    }
                }
            }

            //var category = result.FirstOrDefault();

            return 1; //category.Id;
        }

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