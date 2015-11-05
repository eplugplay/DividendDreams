using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

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

    public static class ISynchronizeInvokeExtensions
    {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
}
