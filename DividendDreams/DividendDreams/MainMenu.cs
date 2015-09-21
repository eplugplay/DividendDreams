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
            decimal DividendStockValue = 0;
            decimal YearDiv = 0;
            decimal QuarterDiv = 0;
            decimal MonthlyDiv = 0;
            DataTable dt = DividendStocks.GetCurrentDividends();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal ShareNum = Convert.ToDecimal(dt.Rows[i]["numberofshares"]);
                decimal AnnDiv = Convert.ToDecimal(dt.Rows[i]["annDividend"]);
                decimal purchaseprice = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                YearDiv += (ShareNum * AnnDiv);
                DividendStockValue += (ShareNum * purchaseprice);
            }
            QuarterDiv = (YearDiv / 4);
            MonthlyDiv = (YearDiv / 12);
            MessageBox.Show("Portfolio Value: $" + Math.Round(DividendStockValue, 2) + "\n\n" + "Annual Dividend: $" + Math.Round(YearDiv, 2) + "\n\n" + "Quarterly Dividend: $" + Math.Round(QuarterDiv, 2) + "\n\n" + "Monthly Dividend: $" + Math.Round(MonthlyDiv, 2));
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
            foreach (DataRowView drv in lb.SelectedItems)
            {
                DividendStocks.UpdateDividendStock(drv.Row["id"].ToString(), stockActive);
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

        public void GetDividendPrice(ListBox lb)
        {
            DataTable dt = DividendStocks.GetDividend(lb.SelectedValue.ToString());
            decimal NumberOfShares = Convert.ToDecimal(dt.Rows[0]["numberofshares"]);
            decimal AnnDividend = Convert.ToDecimal(dt.Rows[0]["annDividend"]);
            decimal TotalDividendPrice = AnnDividend * NumberOfShares;
            decimal QuarterlyDividendPrice = TotalDividendPrice / 3;
            decimal MonthlyDividendPrice = TotalDividendPrice / 12;
            MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\n" + "Quarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\n" + "Monthly: $" + Math.Round(MonthlyDividendPrice, 2));
        }

        public void GetSharePrice(ListBox lb)
        {
            DataTable dt = DividendStocks.GetDividend(lb.SelectedValue.ToString());
            decimal NumberOfShares = Convert.ToDecimal(dt.Rows[0]["numberofshares"]);
            decimal SharePrice = Convert.ToDecimal(dt.Rows[0]["purchaseprice"]);
            decimal totalSharePrice = SharePrice * NumberOfShares;
            MessageBox.Show("$" + Math.Round(totalSharePrice, 2).ToString());
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            GetSharePrice(lbCurrentDividends);
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            GetDividendPrice(lbCurrentDividends);
        }

        private void btnGetAllPrice_Click(object sender, EventArgs e)
        {
            GetSharePrice(lbAllDividends);
        }

        private void btnGetAllDivPrice_Click(object sender, EventArgs e)
        {
            GetDividendPrice(lbAllDividends);
        }
    }
}
