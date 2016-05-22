using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using stazkainos.DAL;

namespace stazkainos.Models
{
    public class CSVReader
    {
        public List<FundValue> Parse()
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
                            FundValue insertval = new FundValue();

                            insertval.value = double.Parse(fields[1], CultureInfo.InvariantCulture);
                            insertval.fundDate = DateTime.ParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            insertval.fundDate=insertval.fundDate.Value.AddHours(12);
                           
                            fundList.Add(insertval);
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