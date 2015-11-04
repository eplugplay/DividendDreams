using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DividendDreams
{
    public static class YahooFinance
    {
        public static string GetValues(string symbol, string code)
        {
            string value = "";
            WebClient client = new WebClient();
            var url = string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}&f={1}", symbol, code);
            value = client.DownloadString(url);
            value = value.Replace("\"", "");
            value = value.Replace("\n", "");
            return value;
        }
    }
}
