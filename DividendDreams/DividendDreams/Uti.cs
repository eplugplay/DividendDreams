using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace DividendDreams
{
    public static class Uti
    {
        public static string[] SplitStockData(string val)
        {
            string[] split = val.Split('\n');
            return split;
        }

        public static string GetMultiSymbols(DataTable dt)
        {
            string toReturn = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                toReturn += dt.Rows[i]["symbol"].ToString() + "+";
            }
            toReturn = toReturn.Substring(0, toReturn.Length - 1);
            return toReturn;
        }

        public static string FilterDuplicates(string symbols)
        {
            string Symbols = "";
            string[] Split = symbols.Split('+');
            List<string> lstSymbols = new List<string>();
            for (int i = 0; i < Split.Length; i++)
            {
                if (!lstSymbols.Contains(Split[i]))
                {
                    lstSymbols.Add(Split[i]);
                }
            }
            for (int i = 0; i < lstSymbols.Count; i++)
            {
                Symbols += lstSymbols[i] + "+"; 
            }
            return Symbols = Symbols.Substring(0, Symbols.Length - 1);
        }
    }
}
