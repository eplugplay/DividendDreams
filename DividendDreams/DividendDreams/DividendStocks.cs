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
                        cmd.CommandText = "SELECT ds.id, ds.anndividend, dp.numberofshares, dp.purchaseprice, dp.purchaseaction, ds.dividendpercent FROM dividendstocks ds JOIN dividendprice dp ON ds.id=dp.dividendstockid WHERE ds.stockactive='true' order by ds.id, ds.exdividend";
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
                        cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.capsize, ds.anndividend, dp.numberofshares, ds.dividendpercent, ds.stockactive, dp.purchaseprice, dp.purchaseaction, ds.dripcost, ds.dripinitialcost, ds.drip, ds.exdividend, ds.dripnotes, ds.paydate FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    // if shares data doesn't exist (not bought yet)
                    if (dt.Rows.Count == 0)
                    {
                        using (var cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.capsize, ds.anndividend, ds.dividendpercent, ds.stockactive, ds.dripcost, ds.dripinitialcost, ds.drip, ds.exdividend, ds.dripnotes, ds.paydate FROM dividendstocks ds WHERE ds.id=@id";
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
            decimal transactionPrice = (decimal)9.99;
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
                        //cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid AND purchaseaction='bought'";
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchaseaction FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["purchaseaction"].ToString() == "bought")
                        {
                            numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                            price = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                            totalPrice += ((decimal)numShares * price);
                        }
                        else
                        {
                            numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                            price = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                            totalPrice -= ((decimal)numShares * price);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void GetDividendPrice(string dividendstockid, out decimal totalDividendPrice, out decimal quarterlyDividendPrice, out decimal monthlyDividendPrice, out decimal originalDripCost, out decimal dripCost, out bool drip)
        {
            DataTable dt = new DataTable();
            totalDividendPrice = 0;
            quarterlyDividendPrice = 0;
            monthlyDividendPrice = 0;
            drip = false;
            decimal dripinitial = 0;
            originalDripCost = GetDripCost(dividendstockid, out drip, out dripinitial);
            dripCost = originalDripCost;

            int numShares = 0;
            decimal yield = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchaseaction FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                        yield = GetDividendYield(dividendstockid, cnn);
                        if (dt.Rows[i]["purchaseaction"].ToString() == "bought")
                        {
                            totalDividendPrice += ((decimal)numShares * yield);
                        }
                        else
                        {
                            totalDividendPrice -= ((decimal)numShares * yield);
                        }
                    }
                    if (drip)
                    {
                        totalDividendPrice -= (dripCost * (decimal)numShares) + dripinitial;
                    }
                }
                quarterlyDividendPrice = totalDividendPrice / 3;
                monthlyDividendPrice = totalDividendPrice / 12;
            }
            catch (Exception e)
            {

            }
        }

        public static decimal GetDripCost(string id, out bool drip, out decimal dripinitial)
        {
            decimal dripCost = 0;
            dripinitial = 0;
            drip = false;
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT dripinitialcost, dripcost, drip FROM dividendstocks WHERE id=@id";
                    cmd.Parameters.AddWithValue("id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            try
                            {
                                dripinitial = Convert.ToDecimal(rdr["dripinitialcost"].ToString());
                                dripCost = Convert.ToDecimal(rdr["dripcost"]);
                            }
                            catch
                            {

                            }
                            drip = rdr["drip"].ToString() == "true" ? true : false;
                        }
                    }
                }
            }
            return dripCost;
        }

        public static decimal GetDividendYield(string id, MySqlConnection cnn)
        {
            decimal yield = 0;
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = "SELECT anndividend FROM dividendstocks WHERE id=@id";
                cmd.Parameters.AddWithValue("id", id);
                yield = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return yield;
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
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchaseaction, purchasedate FROM dividendprice WHERE id=@id";
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
                        cmd.CommandText = "SELECT dp.id, CONCAT(dp.purchasedate, \" - \", dp.purchaseaction) as purchasedate FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id ORDER BY purchasedate";
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
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT id, symbol, stockname, industry, ROUND(anndividend, 2) as anndividend, ROUND(dividendpercent, 2) as dividendpercent, exdividend, paydate FROM dividendstocks WHERE stockactive=@stockactive order by id";
                        //cmd.CommandText = "SELECT ds.id, dp.purchaseprice, ds.symbol, ds.stockname, ds.industry, dp.numberofshares, ROUND(ds.anndividend, 2) as anndividend, ROUND(ds.dividendpercent, 2) as dividendpercent FROM dividendstocks ds JOIN dividendprice dp on ds.id = dp.dividendstockid WHERE ds.stockactive=@stockactive AND dp.purchaseaction='bought' order by ds.id";
                        cmd.Parameters.AddWithValue("stockactive", stockactive);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dtTemp);
                    }

                    dtTemp = BuildDividendTable(dtTemp);
                    dtTemp = AddPrice(dtTemp, cnn);
                    DataView drv = dtTemp.DefaultView;
                    drv.Sort = "symbol asc";
                    dtTemp = drv.ToTable();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        string exDiv = dtTemp.Rows[i]["exdividend"].ToString() == "" ? ")" : ")  -  " + dtTemp.Rows[i]["exdividend"].ToString();
                        string payDiv = dtTemp.Rows[i]["paydate"].ToString() == "" ? "" : "  -  " + dtTemp.Rows[i]["paydate"].ToString();
                        DataRow dr = dtDividends.NewRow();
                        dr["id"] = Convert.ToInt32(dtTemp.Rows[i]["id"]);
                        dr["symbolName"] = dtTemp.Rows[i]["symbol"].ToString() + "  -  (" + dtTemp.Rows[i]["stockname"].ToString() + ")  -  " + dtTemp.Rows[i]["industry"].ToString() + "  -  " + dtTemp.Rows[i]["numberofshares"].ToString() + " Shares  -  $" + dtTemp.Rows[i]["anndividend"].ToString() + "  -  (" + dtTemp.Rows[i]["dividendpercent"].ToString() + "%" + exDiv + payDiv;
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

        public static DataTable BuildDividendTable(DataTable dt)
        {
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("id", typeof(int));
            dtFinal.Columns.Add("symbol", typeof(string));
            dtFinal.Columns.Add("stockname", typeof(string));
            dtFinal.Columns.Add("industry", typeof(string));
            dtFinal.Columns.Add("numberofshares", typeof(int));
            dtFinal.Columns.Add("anndividend", typeof(string));
            dtFinal.Columns.Add("dividendpercent", typeof(string));
            dtFinal.Columns.Add("exdividend", typeof(string));
            dtFinal.Columns.Add("paydate", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtFinal.NewRow();
                dr["id"] = Convert.ToInt32(dt.Rows[i]["id"]);
                dr["symbol"] = dt.Rows[i]["symbol"].ToString();
                dr["stockname"] = dt.Rows[i]["stockname"].ToString();
                dr["industry"] = dt.Rows[i]["industry"].ToString();
                //dr["numberofshares"] = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                dr["anndividend"] = Convert.ToDecimal(dt.Rows[i]["anndividend"]);
                dr["dividendpercent"] = Convert.ToDecimal(dt.Rows[i]["dividendpercent"]);
                dr["exdividend"] = dt.Rows[i]["exdividend"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[i]["exdividend"]).ToString("MM/dd/yyyy");
                dr["paydate"] = dt.Rows[i]["paydate"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[i]["paydate"]).ToString("MM/dd/yyyy");
                //dr["symbolName"] = dt.Rows[i]["symbol"].ToString() + "  -  (" + dt.Rows[i]["stockname"].ToString() + ")  -  " + dt.Rows[i]["industry"].ToString() + "  -  " + dt.Rows[i]["numberofshares"].ToString() + " Shares  -  $" + dt.Rows[i]["anndividend"].ToString() + "  -  (" + dt.Rows[i]["dividendpercent"].ToString() + "%)";
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
                    cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE purchaseaction='bought' AND dividendstockid=@dividendstockid order by dividendstockid";
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
                //dr["symbolName"] = dt.Rows[i]["symbol"].ToString() + "  -  (" + dt.Rows[i]["stockname"].ToString() + ")  -  " + dt.Rows[i]["industry"].ToString() + "  -  " + dt.Rows[i]["numberofshares"].ToString() + " Shares  -  $" + dt.Rows[i]["anndividend"].ToString() + "  -  (" + dt.Rows[i]["dividendpercent"].ToString() + "%)";
                //dtFinal.Rows.Add(dr);
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

        public static string NewDividendStock(string symbol, string stockname, string industry, string boughtshareprice, string anndividend, string numberofshares, 
                                            string dividendpercent, string capsize, string dripcost, string dripinitialcost, string drip, DateTime exdividend, string dripnotes, DateTime paydate)
        {
            string ID = "0";
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO dividendstocks (symbol, stockname, industry, anndividend, dividendpercent, capsize, dripinitialcost, dripcost, drip, exdividend, dripnotes, stockactive, paydate) 
                                        VALUES (@symbol, @stockname, @industry, @anndividend, @dividendpercent, @capsize, @dripinitialcost, @dripcost, @drip, @exdividend, dripnotes, 'false', paydate); SELECT LAST_INSERT_ID();";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("boughtshareprice", boughtshareprice);
                        cmd.Parameters.AddWithValue("anndividend", anndividend);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("dividendpercent", dividendpercent);
                        cmd.Parameters.AddWithValue("capsize", capsize);
                        cmd.Parameters.AddWithValue("dripinitialcost", dripinitialcost);
                        cmd.Parameters.AddWithValue("dripcost", dripcost);
                        cmd.Parameters.AddWithValue("exdividend", exdividend);
                        cmd.Parameters.AddWithValue("dripnotes", dripnotes);
                        cmd.Parameters.AddWithValue("drip", drip);
                        cmd.Parameters.AddWithValue("paydate", paydate);
                        ID = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return ID;
        }

        public static void UpdateDividendStock(string id, string symbol, string stockname, string industry, string anndividend, string dividendpercent, string capsize,
                                                string dripinitialcost, string dripcost, string drip, DateTime exdividend, string dripnotes, DateTime paydate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE dividendstocks SET symbol=@symbol, stockname=@stockname, industry=@industry, anndividend=@anndividend, dividendpercent=@dividendpercent, 
                                            capsize=@capsize, dripcost=@dripcost, dripinitialcost=@dripinitialcost, drip=@drip, exdividend=@exdividend, dripnotes=@dripnotes, paydate=@paydate WHERE id=@id";
                        cmd.Parameters.AddWithValue("symbol", symbol);
                        cmd.Parameters.AddWithValue("stockname", stockname);
                        cmd.Parameters.AddWithValue("industry", industry);
                        cmd.Parameters.AddWithValue("anndividend", anndividend);
                        cmd.Parameters.AddWithValue("dividendpercent", dividendpercent);
                        cmd.Parameters.AddWithValue("capsize", capsize);
                        cmd.Parameters.AddWithValue("dripinitialcost", dripinitialcost);
                        cmd.Parameters.AddWithValue("dripcost", dripcost);
                        cmd.Parameters.AddWithValue("drip", drip);
                        cmd.Parameters.AddWithValue("exdividend", exdividend);
                        cmd.Parameters.AddWithValue("dripnotes", dripnotes);
                        cmd.Parameters.AddWithValue("paydate", paydate);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void NewShare(string purchaseprice, string numberofshares, string purchaseaction, string dividendstockid, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO dividendprice (purchaseprice, numberofshares, purchaseaction, dividendstockid, purchasedate) VALUES (@purchaseprice, @numberofshares, @purchaseaction, @dividendstockid, @purchasedate)";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchaseaction", purchaseaction);
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

        public static void UpdateShare(string purchaseprice, string numberofshares, string purchaseaction, string id, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendprice SET purchaseprice=@purchaseprice, numberofshares=@numberofshares, purchaseaction=@purchaseaction, purchasedate=@purchasedate WHERE id=@id";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchaseaction", purchaseaction);
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
    }
}
