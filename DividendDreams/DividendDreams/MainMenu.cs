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
            if (_Dividends == null || _Dividends.IsDisposed)
            {
                _Dividends = new Dividends(edit, id);
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
        }

        public void CalculateResults()
        {
            decimal dividendCount = 0;
            decimal DividendStockValue = 0;
            decimal YearDiv = 0;
            decimal QuarterDiv = 0;
            decimal MonthlyDiv = 0;
            decimal DividendTotalPercentage = 0;
            DataTable dt = DividendStocks.GetCurrentDividends();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal ShareNum = Convert.ToDecimal(dt.Rows[i]["numberofshares"]);
                decimal AnnDiv = Convert.ToDecimal(dt.Rows[i]["annDividend"]);
                decimal purchaseprice = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                if (dt.Rows[i]["purchaseaction"].ToString() == "bought")
                {
                    YearDiv += (ShareNum * AnnDiv);
                    DividendStockValue += (ShareNum * purchaseprice);
                }
                else
                {
                    YearDiv -= (ShareNum * AnnDiv);
                    DividendStockValue -= (ShareNum * purchaseprice);
                }
                dividendCount++;
                DividendTotalPercentage += Convert.ToDecimal(dt.Rows[i]["dividendpercent"]);
            }
            DividendTotalPercentage = DividendTotalPercentage / dividendCount;
            QuarterDiv = (YearDiv / 4);
            MonthlyDiv = (YearDiv / 12);
            MessageBox.Show("Portfolio Value: $" + Math.Round(DividendStockValue, 2) + "\n\n" + "Annual Dividend: $" + Math.Round(YearDiv, 2) + "\n\n" + "Quarterly Dividend: $" + Math.Round(QuarterDiv, 2) + "\n\n" + "Monthly Dividend: $" + Math.Round(MonthlyDiv, 2) + "\n\n" + "Total Dividend: " + Math.Round(DividendTotalPercentage, 2) + "%");
        }

        public void LoadCurrentDividends()
        {
            int totalDividends = 0;
            DividendStocks.LoadDividends(lbCurrentDividends, "true", out totalDividends);
            lblTotalPortfolioDividends.Text = totalDividends.ToString();
        }

        public void LoadAllDividends()
        {
            int totalDividends = 0;
            DividendStocks.LoadDividends(lbAllDividends, "false", out totalDividends);
            lblTotalAllDividends.Text = totalDividends.ToString();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            ddlIndustry.SelectedIndex = 0;
            ddlIndustryAll.SelectedIndex = 0;
            LoadDividendStocks();
        }

        public void LoadDividendStocks()
        {
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
            if (lb.SelectedItems.Count > 1)
            {
                foreach (DataRowView drv in lb.SelectedItems)
                {
                    DividendStocks.UpdateDividendStock(drv.Row["id"].ToString(), stockActive);
                }
            }
            else
            {
                DividendStocks.UpdateDividendStock(lb.SelectedValue.ToString(), stockActive);
            }
            LoadDividendStocks();
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
            MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\n Quarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\n Monthly: $" + Math.Round(MonthlyDividendPrice, 2) + dripMsg);
        }

        public void GetSharePrice(ListBox lb)
        {
            decimal totalPrice = 0;
            DividendStocks.GetTotalSharePrice(lb.SelectedValue.ToString(), out totalPrice);
            MessageBox.Show("$" + Math.Round(totalPrice, 2).ToString());
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            GetSharePrice(lbCurrentDividends);
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            GetDividendPrice(lbCurrentDividends);
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
            if (!selectedOne)
            {
                MessageBox.Show("Not Found");
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
            if (showMsg)
            {
                MessageBox.Show(ddl.Text + ": " + Math.Round(percentage, 2) + "%");
            }
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
            if (txtSearchAllSymbol.Text != "")
            {
                SearchSymbol(txtSearchAllSymbol, lbAllDividends);
            }
            else
            {
                lbAllDividends.ClearSelected();
            }
        }
    }
}
