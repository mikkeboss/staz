using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.FileIO;
namespace stazkainos.Models
{
    public class CSVReader
    {
       public  List<FundValue> Parse()
        {
            using (TextFieldParser parser = new TextFieldParser(@"D:\data.csv"))
            {
                List<FundValue> fundList = new List<FundValue>();
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    try
                    {
                        if (fields != null)
                        {
                            var date = DateTime.ParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            var value = double.Parse(fields[1], CultureInfo.InvariantCulture);
                            fundList.Add(new FundValue(date,value));
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.Out.WriteLine(ex.ToString());
                    }
                }
                return fundList;
            }

        }
    }
}