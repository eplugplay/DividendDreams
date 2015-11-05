using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DividendDreams
{
    public partial class Dividends : Form
    {
        static public Shares _Shares;
        public bool Edit { get; set; }
        public string ID { get; set; }
        bool CurrentDiv {get;set;}
        string Symbol { get; set; }
        public Dividends(bool edit, string id, bool currentDiv, string symbol)
        {
            Edit = edit;
            ID = id;
            CurrentDiv = currentDiv;
            Symbol = symbol;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateAll())
            {
                return;
            }
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            if (Edit)
            {
                DividendStocks.UpdateDividendStock(ID, txtSymbol.Text, txtStockName.Text, ddlIndustry.Text);
                ReloadMainDividends();
                Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
                pw.Close();
                this.Close();
            }
            else
            {
                ID = DividendStocks.NewDividendStock(txtSymbol.Text, txtStockName.Text, ddlIndustry.Text);
                Program.MainMenu.LoadAllDividends();
                Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
                pw.Close();
                this.Close();
            }
        }

        public void ReloadMainDividends()
        {
            if (CurrentDiv)
            {
                Program.MainMenu.LoadCurrentDividends();
            }
            else
            {
                Program.MainMenu.LoadAllDividends();
            }
        }

        private void Dividends_Load(object sender, EventArgs e)
        {
            ddlIndustry.SelectedIndex = 0;
            if (Edit)
            {
                LoadDividendStock();
                btnSave.Text = "Update";
                this.Text = "Edit Dividend Stock";
            }
            else
            {
                HideTextBoxes();
                gpSharesOptions.Enabled = false;
                btnSave.Text = "Save";
            }
        }

        public void HideTextBoxes()
        {
            txtAnnualDividend.Enabled = false;
            txtMarketCap.Enabled = false;
            txtDividendPercent.Enabled = false;
            txtExDividend.Enabled = false;
            txtPERatio.Enabled = false;
            txtPayDate.Enabled = false;
            txtOpenPrice.Enabled = false;
            txt52WeekHigh.Enabled = false;
            txt52WeekLow.Enabled = false;
            txtCurrentPrice.Enabled = false;
            txtDayRange.Enabled = false;
        }

        public void LoadDividendStock()
        {
            DataTable dt = DividendStocks.GetDividend(ID);
            txtSymbol.Text = dt.Rows[0]["symbol"].ToString();
            txtStockName.Text = dt.Rows[0]["stockname"].ToString();
            ddlIndustry.SelectedIndex = ddlIndustry.FindString(dt.Rows[0]["industry"].ToString());
            txtAnnualDividend.Text = YahooFinance.GetValues(Symbol, "d", false);
            txtDividendPercent.Text = YahooFinance.GetValues(Symbol, "y", false);
            txtMarketCap.Text = YahooFinance.GetValues(Symbol, "j1", false);
            txtExDividend.Text = YahooFinance.GetValues(Symbol, "q", false);
            txtPayDate.Text = YahooFinance.GetValues(Symbol, "r1", false);
            txtPERatio.Text = YahooFinance.GetValues(Symbol, "r", false);
            txtDayRange.Text = YahooFinance.GetValues(Symbol, "m", false);
            txt52WeekLow.Text = YahooFinance.GetValues(Symbol, "j", false);
            txt52WeekHigh.Text = YahooFinance.GetValues(Symbol, "k", false);
            txtCurrentPrice.Text = YahooFinance.GetValues(Symbol, "a", false);
            txtOpenPrice.Text = YahooFinance.GetValues(Symbol, "o", false);
            // load purchase dates
            LoadPurchaseDates();
            LoadPurchaseData();
        }

        public void LoadPurchaseDates()
        {
            txtSharePrice.Clear();
            txtNumberOfShares.Clear();
            ddlSharePurchaseDate.SelectedIndexChanged -= ddlSharePurchaseDate_SelectedIndexChanged;
            ddlSharePurchaseDate.DataSource = DividendStocks.GetDividendActionDate(ID);
            ddlSharePurchaseDate.DisplayMember = "purchasedate";
            ddlSharePurchaseDate.ValueMember = "id";
            if (ddlSharePurchaseDate.Text != "")
            {
                btnNewShares.Enabled = false;
                btnDeleteShares.Enabled = true;
                btnEditShares.Enabled = true;
                btnGetSharePrice.Enabled = true;
                btnDividendPrice.Enabled = true;
            }
            else
            {
                btnNewShares.Enabled = true;
                btnDeleteShares.Enabled = false;
                btnEditShares.Enabled = false;
                btnGetSharePrice.Enabled = false;
                btnDividendPrice.Enabled = false;
            }
            ddlSharePurchaseDate.SelectedIndexChanged += ddlSharePurchaseDate_SelectedIndexChanged;
        }

        private void ddlSharePurchaseDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSharePurchaseDate.SelectedIndex != -1)
            {
                LoadPurchaseData();
            }
        }

        public void LoadPurchaseData()
        {
            if (ddlSharePurchaseDate.SelectedValue != null)
            {
                DataTable dt = DividendStocks.GetPurchasePrice(ddlSharePurchaseDate.SelectedValue.ToString());
                txtSharePrice.Text = dt.Rows[0]["purchaseprice"].ToString();
                txtNumberOfShares.Text = dt.Rows[0]["numberofshares"].ToString();
            }
        }

        public bool ValidateAll()
        {
            if (txtSymbol.Text == "")
            {
                MessageBox.Show("Please enter symbol.");
                return false;
            }
            if (txtStockName.Text == "")
            {
                MessageBox.Show("Please enter stock name.");
                return false;
            }
            if (ddlIndustry.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Industry.");
                return false;
            }
            return true;
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal TotalSharePrice = 0;
                TotalSharePrice = Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtSharePrice.Text);
                MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
            }
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal TotalDividendPrice = Convert.ToDecimal(txtAnnualDividend.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
                decimal QuarterlyDividendPrice = TotalDividendPrice / 4;
                decimal MonthlyDividendPrice = TotalDividendPrice / 12;
                MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\nQuarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\nMonthly: $" + Math.Round(MonthlyDividendPrice, 2));
            }
        }


        public void OpenSharesForm(bool edit)
        {
            if (_Shares == null || _Shares.IsDisposed)
            {
                _Shares = new Shares(edit, ID, ddlSharePurchaseDate.SelectedValue == null ? "" : ddlSharePurchaseDate.SelectedValue.ToString(), CurrentDiv);
                _Shares.Show();
            }
            else
            {
                if (_Shares.WindowState == FormWindowState.Minimized)
                {
                    _Shares.WindowState = FormWindowState.Normal;
                }
                else
                {
                    _Shares.BringToFront();
                }
            }
        }


        private void btnNewShares_Click(object sender, EventArgs e)
        {
            OpenSharesForm(false);
        }

        private void btnEditShares_Click(object sender, EventArgs e)
        {
            if (txtSharePrice.Text != "")
            {
                OpenSharesForm(true);
            }
        }

        private void btnDeleteShares_Click(object sender, EventArgs e)
        {
            if (txtSharePrice.Text != "")
            {
                if (MessageBox.Show("Delete?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PleaseWait pw = new PleaseWait();
                    pw.Show();
                    Application.DoEvents();
                    DividendStocks.DeleteShare(ddlSharePurchaseDate.SelectedValue.ToString());
                    LoadPurchaseDates();
                    LoadPurchaseData();
                    ReloadMainDividends();
                    pw.Close();
                }
            }
        }
    }
}
