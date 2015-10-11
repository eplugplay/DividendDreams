using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DividendDreams
{
    public partial class MainMenu : Form
    {
        public static Dividends _Dividends;
        public bool CurrentDiv { get; set; }
        public int ID { get; set; }
        public List<int> lstID = new List<int>();
        public bool HighlightActive { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDividends(false, "");
        }

        public void OpenDividends(bool edit, string id)
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            if (_Dividends == null || _Dividends.IsDisposed)
            {
                _Dividends = new Dividends(edit, id, CurrentDiv);
                _Dividends.Show();
            }
            else
            {
                if (_Dividends.WindowState == FormWindowState.Minimized)
                {
                    _Dividends.WindowState = FormWindowState.Normal;
                }
                else
                {
                    _Dividends.BringToFront();
                }
            }
            pw.Close();
        }

        public void CalculateResults()
        {
            decimal TransactionCount = 0;
            decimal TotalDividendCount = 0;
            decimal TotalDividendStockValue = 0;
            //decimal TotalBoughtPrice = 0;
            decimal YearDiv = 0;
            decimal QuarterDiv = 0;
            decimal MonthlyDiv = 0;
            decimal DividendTotalPercentage = 0;
            decimal TransactionFee = (decimal)9.99;
            DataTable dt = DividendStocks.GetCurrentDividends();
            decimal ShareNum = 0;
            decimal AnnDiv = 0;
            decimal Purchaseprice = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i]["id"].ToString();
                ShareNum = Convert.ToDecimal(dt.Rows[i]["numberofshares"]);
                AnnDiv = Convert.ToDecimal(dt.Rows[i]["annDividend"]);
                Purchaseprice = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                if (dt.Rows[i]["purchaseaction"].ToString() == "bought")
                {
                    TransactionCount++;
                    YearDiv += (ShareNum * AnnDiv);
                    //TotalBoughtPrice += (ShareNum * Purchaseprice);
                    TotalDividendStockValue += (ShareNum * Purchaseprice);
                }
                else
                {
                    TransactionCount++;
                    YearDiv -= (ShareNum * AnnDiv);
                    TotalDividendStockValue -= (ShareNum * Purchaseprice);
                }
                TotalDividendCount++;
                DividendTotalPercentage += Convert.ToDecimal(dt.Rows[i]["dividendpercent"]);
            }
            TransactionFee = TransactionFee * TransactionCount;
            decimal GrandTotalSpent = TotalDividendStockValue + TransactionFee;
            DividendTotalPercentage = DividendTotalPercentage / TotalDividendCount;
            QuarterDiv = (YearDiv / 4);
            MonthlyDiv = (YearDiv / 12);
            MessageBox.Show("Portfolio Value: $" + Math.Round(TotalDividendStockValue, 2) + "\n\nTotal Transaction Fee: $" + Math.Round(TransactionFee, 2) + "\n\nGrand Total Spent: $" + Math.Round(GrandTotalSpent, 2) + "\n\nAnnual Dividend: $" + Math.Round(YearDiv, 2) + "\n\n" + "Quarterly Dividend: $" + Math.Round(QuarterDiv, 2) + "\n\nMonthly Dividend: $" + Math.Round(MonthlyDiv, 2) + "\n\nTotal Dividend: " + Math.Round(DividendTotalPercentage, 2) + "%");
        }

        public void LoadCurrentDividends()
        {
            lbCurrentDividends.SelectedIndexChanged -= lbCurrentDividends_SelectedIndexChanged;
            int totalDividends = 0;
            DividendStocks.LoadDividends(lbCurrentDividends, "true", out totalDividends);
            lblTotalPortfolioDividends.Text = totalDividends.ToString();
            lbCurrentDividends.ClearSelected();
            lbCurrentDividends.SelectedIndexChanged += lbCurrentDividends_SelectedIndexChanged;
        }

        public void LoadAllDividends()
        {
            lbAllDividends.SelectedIndexChanged -= lbAllDividends_SelectedIndexChanged;
            int totalDividends = 0;
            DividendStocks.LoadDividends(lbAllDividends, "false", out totalDividends);
            lblTotalAllDividends.Text = totalDividends.ToString();
            lbAllDividends.ClearSelected();
            lbAllDividends.SelectedIndexChanged += lbAllDividends_SelectedIndexChanged;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            ddlIndustry.SelectedIndex = 0;
            ddlIndustryAll.SelectedIndex = 0;
            dtpPayDate.Format = DateTimePickerFormat.Custom;
            dtpPayDate.CustomFormat = "MM/yyyy";
            dtpPayDate.ShowUpDown = true;
            LoadAllDividends();
            LoadCurrentDividends();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateResults();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRemoveDividends(lbAllDividends, "true");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            AddRemoveDividends(lbCurrentDividends, "false");
        }

        public void AddRemoveDividends(ListBox lb, string stockActive)
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            lstID.Clear();
            int selectedItemsCount = lb.SelectedItems.Count;
            if (selectedItemsCount > 1)
            {
                foreach (DataRowView drv in lb.SelectedItems)
                {
                    DividendStocks.UpdateDividendStock(drv.Row["id"].ToString(), stockActive);
                    lstID.Add(Convert.ToInt32(drv.Row["id"]));
                }
            }
            else
            {
                DividendStocks.UpdateDividendStock(lb.SelectedValue.ToString(), stockActive);
            }
            LoadAllDividends();
            LoadCurrentDividends();
            SelectStocks(selectedItemsCount);
            pw.Close();
        }

        public List<int> SaveListBoxItems(ListBox lb)
        {
            List<int> toReturn = new List<int>();
            foreach (DataRowView drv in lb.Items)
            {
                toReturn.Add(Convert.ToInt32(drv.Row["id"]));
            }
            return toReturn;
        }

        public void SelectMultiple(ListBox lb)
        {
            List<int> lst = SaveListBoxItems(lb);
            for (int a = 0; a < lstID.Count; a++)
            {
                for (int b = 0; b < lst.Count; b++)
                {
                    if (lstID[a] == lst[b])
                    {
                        lb.SetSelected(b, true);
                    }
                }
            }
        }

        public void SelectStocks(int selectedItemsCount)
        {
            if (!CurrentDiv)
            {
                CurrentDiv = true;
                lbAllDividends.ClearSelected();
                lbCurrentDividends.SelectedIndexChanged -= lbCurrentDividends_SelectedIndexChanged;
                lbCurrentDividends.ClearSelected();
                if (selectedItemsCount == 1)
                {
                    lbCurrentDividends.SelectedValue = Convert.ToInt32(ID);
                }
                else
                {
                    SelectMultiple(lbCurrentDividends);
                }
                lbCurrentDividends.SelectedIndexChanged += lbCurrentDividends_SelectedIndexChanged;
            }
            else
            {
                CurrentDiv = false;
                lbCurrentDividends.ClearSelected();
                lbAllDividends.SelectedIndexChanged -= lbAllDividends_SelectedIndexChanged;
                lbAllDividends.ClearSelected();
                if (selectedItemsCount == 1)
                {
                    lbAllDividends.SelectedValue = Convert.ToInt32(ID);
                }
                else
                {
                    SelectMultiple(lbAllDividends);
                }
                lbAllDividends.SelectedIndexChanged += lbAllDividends_SelectedIndexChanged;
            }
        }

        private void lbCurrentDividends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbCurrentDividends.SelectedIndex != -1)
            {
                OpenDividends(true, lbCurrentDividends.SelectedValue.ToString());
            }
        }


        private void lbAllDividends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbAllDividends.SelectedIndex != -1)
            {
                OpenDividends(true, lbAllDividends.SelectedValue.ToString());
            }
        }

        public void GetDividendPrice(ListBox lb)
        {
            decimal TotalDividendPrice = 0;
            decimal QuarterlyDividendPrice = 0;
            decimal MonthlyDividendPrice = 0;
            decimal AutoDripCost = 0;
            decimal OriginalDripCost = 0;
            bool drip = false;
            DividendStocks.GetDividendPrice(lb.SelectedValue.ToString(), out TotalDividendPrice, out QuarterlyDividendPrice, out MonthlyDividendPrice, out OriginalDripCost, out AutoDripCost, out drip);
            string dripMsg = drip == true ? "\n\n Drip Cost: $" + Math.Round(OriginalDripCost, 2) : "";
            MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\nQuarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\nMonthly: $" + Math.Round(MonthlyDividendPrice, 2) + dripMsg);
        }

        public void GetSharePrice(ListBox lb)
        {
            decimal totalPrice = 0;
            DividendStocks.GetTotalSharePrice(lb.SelectedValue.ToString(), out totalPrice);
            MessageBox.Show("$" + Math.Round(totalPrice, 2).ToString());
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            if (lbCurrentDividends.SelectedIndex != -1)
            {
                GetSharePrice(lbCurrentDividends);
            }
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            if (lbCurrentDividends.SelectedIndex != -1)
            {
                GetDividendPrice(lbCurrentDividends);
            }
        }

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            Highlight(lbCurrentDividends, ddlIndustry, lblTotalPortfolioDividends, true);
        }

        public void SearchSymbol(TextBox tb, ListBox lb)
        {
            bool selectedOne = false;
            lb.ClearSelected();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                DataRowView drv = lb.Items[i] as DataRowView;
                if (drv["symbolName"].ToString().Contains(tb.Text.ToUpper()))
                {
                    selectedOne = true;
                    lb.SelectedIndices.Add(i);
                }
            }
            HighlightActive = false;
            if (!selectedOne)
            {
                MessageBox.Show("Not Found");
                tb.Clear();
            }
        }

        public void Highlight(ListBox lb, ComboBox ddl, Label lbl, bool showMsg)
        {
            lb.ClearSelected();
            decimal count = 0;
            decimal percentage = Convert.ToDecimal(lbl.Text);
            for (int i = 0; i < lb.Items.Count; i++)
            {
                DataRowView drv = lb.Items[i] as DataRowView;
                if (drv["symbolName"].ToString().Contains(ddl.Text))
                {
                    count++;
                    lb.SelectedIndices.Add(i);
                }
            }
            percentage = (count / percentage) * 100;
            HighlightActive = false;
            if (showMsg)
            {
                MessageBox.Show(ddl.Text + ": " + Math.Round(percentage, 2) + "%");
            }
        }


        private void btnNextPurchase_Click(object sender, EventArgs e)
        {
            HighlightActive = true;
            HighlightAllNextToBuy(lbAllDividends);
        }

        public void HighlightAllNextToBuy(ListBox lb)
        {
            lb.ClearSelected();
            chkNextBuy.CheckedChanged -= chkNextBuy_CheckedChanged;
            chkNextBuy.Checked = true;
            chkNextBuy.CheckedChanged += chkNextBuy_CheckedChanged;
            int cnt = 0;
            DataTable dt = DividendStocks.GetAllNextToBuy(ID);
            for (int i = 0; i < lb.Items.Count; i++)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRowView drv = lb.Items[i] as DataRowView;
                    if (drv["id"].Equals(dt.Rows[a]["id"]))
                    {
                        cnt++;
                        lb.SelectedIndices.Add(i);
                    }
                }
            }
            HighlightActive = false;
            MessageBox.Show(string.Format("{0} results.", cnt));
        }

        public void ShowIndustryPercentages(ListBox lb, Label lbl)
        {
            decimal portfolioCnt = Convert.ToDecimal(lbl.Text);
            decimal percentage = 0;
            List<string> lstIndustries = new List<string>() {"Consumer Discretionary", "Consumer Staples", "Energy", "Financials", "Health Care", "Industrials", "Information Technology", 
                                                            "Materials", "Telecommunication Services", "Utilities" };
            List<decimal> count = new List<decimal>();
            decimal cnt = 0;
            for (int i = 0; i < lstIndustries.Count; i++)
            {
                for (int a = 0; a < lb.Items.Count; a++)
                {
                    DataRowView drv = lb.Items[a] as DataRowView;
                    if (drv["symbolName"].ToString().Contains(lstIndustries[i].ToString()))
                    {
                        cnt++;
                    }
                }
                percentage = cnt == 0 ? 0 : (cnt / portfolioCnt) * 100;
                count.Add(percentage);
                cnt = 0;
            }
            string msg = "";
            for (int i = 0; i < lstIndustries.Count; i++)
            {
                msg += lstIndustries[i] + ": " + Math.Round(count[i], 2) + "%" + "\n\n";
            }
            msg = msg.Substring(0, msg.Length - 2);
            MessageBox.Show(msg);
        }

        private void btnHighlightAll_Click(object sender, EventArgs e)
        {
            HighlightActive = true;
            Highlight(lbAllDividends, ddlIndustryAll, lblTotalAllDividends, false);
        }

        private void btnCurrentIndustryPercentage_Click(object sender, EventArgs e)
        {
            ShowIndustryPercentages(lbCurrentDividends, lblTotalPortfolioDividends);
        }

        private void btnAllIndustryPercentages_Click(object sender, EventArgs e)
        {
            ShowIndustryPercentages(lbAllDividends, lblTotalAllDividends);
        }

        private void txtSearchSymbol_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchSymbol.Text != "")
            {
                SearchSymbol(txtSearchSymbol, lbCurrentDividends);
            }
            else
            {
                lbCurrentDividends.ClearSelected();
            }
        }

        private void txtSearchAllSymbol_TextChanged(object sender, EventArgs e)
        {
            HighlightActive = true;
            if (txtSearchAllSymbol.Text != "")
            {
                SearchSymbol(txtSearchAllSymbol, lbAllDividends);
            }
            else
            {
                lbAllDividends.ClearSelected();
            }
        }

        private void lbCurrentDividends_MouseClick(object sender, MouseEventArgs e)
        {
            lbAllDividends.ClearSelected();
            CurrentDiv = true;
        }

        private void lbAllDividends_MouseClick(object sender, MouseEventArgs e)
        {
            lbCurrentDividends.ClearSelected();
            CurrentDiv = false;
        }

        private void lbAllDividends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAllDividends.SelectedIndex != -1)
            {
                ID = Convert.ToInt32(lbAllDividends.SelectedValue);
                if (!HighlightActive)
                {
                    LoadNextToBuy();
                }
            }
        }

        public void LoadNextToBuy()
        {
            chkNextBuy.CheckedChanged -= chkNextBuy_CheckedChanged;
            chkNextBuy.Checked = DividendStocks.LoadNextPurchase(ID) == 1 ? true : false;
            chkNextBuy.CheckedChanged += chkNextBuy_CheckedChanged;
        }

        private void lbCurrentDividends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCurrentDividends.SelectedIndex != -1)
            {
                ID = Convert.ToInt32(lbCurrentDividends.SelectedValue);
            }
        }

        private void chkNextBuy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNextBuy.Checked)
            {
                DividendStocks.SaveNextPurchase(ID, 1);
            }
            else
            {
                DividendStocks.SaveNextPurchase(ID, 0);
            }
        }

        private void btnPayDate_Click(object sender, EventArgs e)
        {
            HighlightPayDate(lbCurrentDividends);
        }

        public void HighlightPayDate(ListBox lb)
        {
            lb.ClearSelected();
            decimal totalDiv = 0;
            decimal quarterlyDiv = 0;
            int cnt = 0;
            string monthYear = "";
            string dtpMonthYear = "";
            DataTable dt = DividendStocks.GetCurrentDividends();
            for (int i = 0; i < lb.Items.Count; i++)
            {
                DataRowView drv = lb.Items[i] as DataRowView;
                string[] date = drv["symbolName"].ToString().Split('*');
                if (date.Length == 2)
                {
                    monthYear = date[1].ToString();
                    date = monthYear.Split('/');
                    monthYear = date[0].Trim() + "/" + date[2];
                    dtpMonthYear = dtpPayDate.Value.ToString("MM/yyyy");
                    if (monthYear == dtpMonthYear)
                    {
                        cnt++;
                        lb.SelectedIndices.Add(i);
                        totalDiv += GetDiv(Convert.ToInt32(drv["id"]), dt);
                    }
                }
            }
            HighlightActive = false;
            quarterlyDiv = totalDiv / 4;
            if (cnt != 0)
            {
                MessageBox.Show(string.Format("{0} results\n\n" + "Date: {1} \n\n" + "This Month: ${2}\n\n" + "This Year: ${3}", cnt, dtpMonthYear,  Math.Round(quarterlyDiv, 2), Math.Round(totalDiv, 2)));
            }
            else
            {
                MessageBox.Show(string.Format("No Results for {0}", dtpMonthYear));
            }
        }

        public decimal GetDiv(int id, DataTable dt)
        {
            decimal div = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["id"]) == id)
                {
                    div = Convert.ToDecimal(dt.Rows[i]["anndividend"]) * Convert.ToDecimal(dt.Rows[i]["numberofshares"]);
                }
            }
            return div;
        }
    }
}
