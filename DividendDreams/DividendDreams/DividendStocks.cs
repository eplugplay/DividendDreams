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
    public static class DividendStocks
    {
        public static DataTable GetCurrentDividends()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ds.id, dp.numberofshares, dp.purchaseprice, ds.symbol FROM dividendstocks ds JOIN dividendprice dp ON ds.id=dp.dividendstockid WHERE ds.stockactive='true' order by ds.id";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetDividend(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, dp.numberofshares, ds.stockactive, dp.purchaseprice FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    // if shares data doesn't exist (not bought yet)
                    if (dt.Rows.Count == 0)
                    {
                        using (var cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.stockactive FROM dividendstocks ds WHERE ds.id=@id";
                            cmd.Parameters.AddWithValue("id", id);
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static void GetTotalSharePrice(string dividendstockid, out decimal totalPrice)
        {
            DataTable dt = new DataTable();
            //decimal transactionPrice = (decimal)9.99;
            totalPrice = 0;
            int numShares = 0;
            decimal price = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                        price = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                        totalPrice += ((decimal)numShares * price);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void GetDividendPrice(string symbol, string dividendstockid, out decimal totalDividendPrice, out decimal quarterlyDividendPrice, out decimal monthlyDividendPrice)
        {
            DataTable dt = new DataTable();
            totalDividendPrice = 0;
            quarterlyDividendPrice = 0;
            monthlyDividendPrice = 0;
            int numShares = 0;
            decimal yield = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                        yield = Convert.ToDecimal(YahooFinance.GetValues(symbol, "d", false));
                        totalDividendPrice += ((decimal)numShares * yield);
                    }
                }
                quarterlyDividendPrice = totalDividendPrice / 4;
                monthlyDividendPrice = totalDividendPrice / 12;
            }
            catch (Exception e)
            {

            }
        }

        public static DataTable GetSharePriceInfo(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchasedate FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetDividendActionDate(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT dp.id, dp.purchasedate FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id ORDER BY purchasedate";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetPurchasePrice(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static void LoadDividends(ListBox lb, string stockactive, out int totalDividends)
        {
            DataTable dtTemp = new DataTable();
            DataTable dtDividends = new DataTable();
            dtDividends.Columns.Add("id", typeof(int));
            dtDividends.Columns.Add("symbolName", typeof(string));
            totalDividends = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    dtTemp = GetCurrentStocksData(cnn, stockactive);
                    dtTemp = BuildDividendTable(dtTemp);
                    dtTemp = AddPrice(dtTemp, cnn);
                    DataView drv = dtTemp.DefaultView;
                    drv.Sort = "symbol asc";
                    dtTemp = drv.ToTable();
                    string Symbols = Uti.GetMultiSymbols(dtTemp);
                    string[] PayDivSplit = Uti.SplitStockData(YahooFinance.GetValues(Symbols, "r1", true));
                    string[] ExDivSplit = Uti.SplitStockData(YahooFinance.GetValues(Symbols, "q", true));
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        DataRow dr = dtDividends.NewRow();
                        dr["id"] = Convert.ToInt32(dtTemp.Rows[i]["id"]);
                        dr["symbolName"] = dtTemp.Rows[i]["symbol"].ToString() + "  -  (" + dtTemp.Rows[i]["stockname"].ToString() + ")  -  " + dtTemp.Rows[i]["industry"].ToString() + "  -  " + dtTemp.Rows[i]["numberofshares"].ToString() + " Shares " + " - Ex Div: " + ExDivSplit[i] + " - Pay Date: *" + PayDivSplit[i];
                        dtDividends.Rows.Add(dr);
                        totalDividends++;
                    }
                    lb.ValueMember = "id";
                    lb.DisplayMember = "symbolName";
                    lb.DataSource = dtDividends;
                }
            }
            catch (Exception e)
            {

            }
        }

        public static DataTable GetCurrentStocksData(MySqlConnection cnn, string stockactive)
        {
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = "SELECT ds.id, ds.symbol, ds.stockname, ds.industry FROM dividendstocks ds WHERE ds.stockactive=@stockactive order by id";
                cmd.Parameters.AddWithValue("stockactive", stockactive);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable BuildDividendTable(DataTable dt)
        {
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("id", typeof(int));
            dtFinal.Columns.Add("symbol", typeof(string));
            dtFinal.Columns.Add("stockname", typeof(string));
            dtFinal.Columns.Add("industry", typeof(string));
            dtFinal.Columns.Add("numberofshares", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtFinal.NewRow();
                dr["id"] = Convert.ToInt32(dt.Rows[i]["id"]);
                dr["symbol"] = dt.Rows[i]["symbol"].ToString();
                dr["stockname"] = dt.Rows[i]["stockname"].ToString();
                dr["industry"] = dt.Rows[i]["industry"].ToString();
                dtFinal.Rows.Add(dr);
            }
            return dtFinal;
        }

        public static DataTable AddPrice(DataTable dt, MySqlConnection cnn)
        {
            DataTable dtTemp = new DataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid order by dividendstockid";
                    cmd.Parameters.AddWithValue("dividendstockid", dt.Rows[i]["id"].ToString());
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtTemp);
                }
                int numShares = 0;
                for (int a = 0; a < dtTemp.Rows.Count; a++)
                {
                    numShares += Convert.ToInt32(dtTemp.Rows[a]["numberofshares"]);
                }

                dt.Rows[i]["numberofshares"] = numShares;
                dtTemp.Clear();
            }
            return dt;
        }


        public static void UpdateDividendStock(string id, string stockactive)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendstocks SET stockactive=@stockactive WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("stockactive", stockactive);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static string NewDividendStock(string symbol, string stockname, string industry, string boughtshareprice, string numberofshares)
        {
            string ID = "0";
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO dividendstocks (symbol, stockname, industry stockactive) VALUES (@symbol, @stockname, @industry 'false'); SELECT LAST_INSERT_ID();";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("boughtshareprice", boughtshareprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        ID = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return ID;
        }

        public static void UpdateDividendStock(string id, string symbol, string stockname, string industry)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE dividendstocks SET symbol=@symbol, stockname=@stockname, industry=@industry WHERE id=@id";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void NewShare(decimal purchaseprice, decimal numberofshares, string dividendstockid, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO dividendprice (purchaseprice, numberofshares, dividendstockid, purchasedate) VALUES (@purchaseprice, @numberofshares, @dividendstockid, @purchasedate)";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        cmd.Parameters.AddWithValue("purchasedate", purchasedate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateShare(decimal purchaseprice, decimal numberofshares, string id, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendprice SET purchaseprice=@purchaseprice, numberofshares=@numberofshares, purchasedate=@purchasedate WHERE id=@id";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchasedate", purchasedate);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void DeleteShare(string id)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static int LoadNextPurchase(int id)
        {
            int nextPurchase = 0;
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT nextpurchase FROM dividendstocks WHERE id=@id order by symbol";
                    cmd.Parameters.AddWithValue("id", id);
                    try
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            nextPurchase = Convert.ToInt32(rdr["nextpurchase"]);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return nextPurchase;
        }

        public static void SaveNextPurchase(int id, int nextPurchase)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendstocks SET nextpurchase=@nextpurchase WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("nextpurchase", nextPurchase);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        public static DataTable GetAllNextToBuy(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM dividendstocks WHERE nextpurchase='1'";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        //public static void Get(string id)
        //{
        //    try
        //    {
        //        using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
        //        {
        //            cnn.Open();
        //            using (var cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandText = "DELETE FROM dividendprice WHERE id=@id";
        //                cmd.Parameters.AddWithValue("id", id);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
    }
}
