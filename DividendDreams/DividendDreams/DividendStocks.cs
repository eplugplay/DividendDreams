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
                        cmd.CommandText = "SELECT ds.anndividend, dp.numberofshares, dp.purchaseprice FROM dividendstocks ds JOIN dividendprice dp ON ds.id=dp.dividendstockid WHERE ds.stockactive='true' AND dp.purchaseaction='bought' order by ds.id";
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
                        cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.capsize, ds.anndividend, dp.numberofshares, ds.dividendpercent, ds.stockactive, dp.purchaseprice, dp.purchaseaction FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }


                    if (dt.Rows.Count == 0)
                    {
                        using (var cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.capsize, ds.anndividend, ds.dividendpercent, ds.stockactive FROM dividendstocks ds WHERE ds.id=@id";
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
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchaseaction FROM dividendprice WHERE id=@id";
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
            DataTable dt = new DataTable();
            totalDividends = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        //cmd.CommandText = "SELECT id, symbol FROM dividendstocks WHERE stockactive=@stockactive order by id";
                        //cmd.CommandText = "SELECT id, CONCAT(symbol, \"(\", stockname, \")/\", industry, \"/\", numberofshares, \" Shares/\", ROUND(anndividend, 3), \" Annual Dividend\") as symbolName FROM dividendstocks WHERE stockactive=@stockactive order by id";
                        //if (stockactive == "true")
                        //{
                        //    cmd.CommandText = "SELECT ds.id, dp.purchaseprice, CONCAT(ds.symbol, \" (\", ds.stockname, \")   -   \", ds.industry, \"   -   \", ds.numberofshares, \" Shares   -   $\", ROUND(ds.anndividend, 2), \" (\", ROUND(ds.dividendpercent, 2), \"%)\") as symbolName FROM dividendstocks ds JOIN dividendprice dp on ds.priceid = dp.id WHERE ds.stockactive=@stockactive order by ds.id";
                        //}
                        //else
                        //{
                            cmd.CommandText = "SELECT ds.id, CONCAT(ds.symbol, \" (\", ds.stockname, \")   -   \", ds.industry, \"   -   \", ds.numberofshares, \" Shares   -   $\", ROUND(ds.anndividend, 2), \" (\", ROUND(ds.dividendpercent, 2), \"%)\") as symbolName FROM dividendstocks ds WHERE ds.stockactive=@stockactive order by ds.id";
                        //}
                        cmd.Parameters.AddWithValue("stockactive", stockactive);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        lb.ValueMember = "id";
                        lb.DisplayMember = "symbolName";
                        lb.DataSource = dt;
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalDividends++;
                }
            }
            catch (Exception e)
            {

            }
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

        public static void NewDividendStock(string symbol, string stockname, string industry, string boughtshareprice, string anndividend, string numberofshares, string dividendpercent, string capsize)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO dividendstocks (symbol, stockname, industry, boughtshareprice, anndividend, numberofshares, dividendpercent, capsize, stockactive) 
                                        VALUES (@symbol, @stockname, @industry, @boughtshareprice, @anndividend, @numberofshares, @dividendpercent, @capsize, 'true')";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("boughtshareprice", boughtshareprice);
                        cmd.Parameters.AddWithValue("anndividend", anndividend);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("dividendpercent", dividendpercent);
                        cmd.Parameters.AddWithValue("capsize", capsize);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateDividendStock(string id, string symbol, string stockname, string industry, string boughtshareprice, string anndividend, string numberofshares, string dividendpercent, string capsize)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE dividendstocks SET symbol=@symbol, stockname=@stockname, industry=@industry, boughtshareprice=@boughtshareprice, anndividend=@anndividend, 
                                        numberofshares=@numberofshares, dividendpercent=@dividendpercent, capsize=@capsize WHERE id=@id";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("boughtshareprice", boughtshareprice);
                        cmd.Parameters.AddWithValue("anndividend", anndividend);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("dividendpercent", dividendpercent);
                        cmd.Parameters.AddWithValue("capsize", capsize);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void NewShare(string purchaseprice, string numberofshares, string purchaseaction, string dividendstockid)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO dividendprice (purchaseprice, numberofshares, purchaseaction, dividendstockid, purchasedate) VALUES (@purchaseprice, @numberofshares, @purchaseaction, @dividendstockid, NOW())";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchaseaction", purchaseaction);
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateShare(string purchaseprice, string numberofshares, string purchaseaction, string id)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendprice SET purchaseprice=@purchaseprice, numberofshares=@numberofshares, purchaseaction=@purchaseaction WHERE id=@id";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchaseaction", purchaseaction);
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
    }
}
