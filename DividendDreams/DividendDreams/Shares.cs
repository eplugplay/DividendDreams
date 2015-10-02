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
    public partial class Shares : Form
    {
        public bool Edit { get; set; }
        public string ID { get; set; }
        public string DividendPriceID { get; set; }
        public bool CurrentDiv { get; set; }
        public Shares(bool edit, string id, string dividendPriceID, bool currentDiv)
        {
            ID = id;
            Edit = edit;
            DividendPriceID = dividendPriceID;
            CurrentDiv = currentDiv;
            InitializeComponent();
        }

        private void Shares_Load(object sender, EventArgs e)
        {
            ddlAction.SelectedIndex = 0;
            if (Edit)
            {
                LoadSharesInfo();
                btnSave.Text = "Update";
            }
        }

        public void LoadSharesInfo()
        {
            DataTable dt = DividendStocks.GetSharePriceInfo(DividendPriceID);
            txtPurchasePrice.Text = dt.Rows[0]["purchaseprice"].ToString();
            txtNumberOfShares.Text = dt.Rows[0]["numberofshares"].ToString();
            ddlAction.SelectedIndex = ddlAction.FindString(dt.Rows[0]["purchaseaction"].ToString());
            dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[0]["purchasedate"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text == "")
            {
                MessageBox.Show("Please enter number of shares.");
                return;
            }
            try
            {
                int.Parse(txtNumberOfShares.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtNumberOfShares.Focus();
                return;
            }
            if (txtPurchasePrice.Text == "")
            {
                MessageBox.Show("Please enter purchase price.");
                return;
            }
            try
            {
                decimal.Parse(txtPurchasePrice.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtPurchasePrice.Focus();
                return;
            }
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            if (Edit)
            {
                DividendStocks.UpdateShare(txtPurchasePrice.Text, txtNumberOfShares.Text, ddlAction.Text, DividendPriceID, dtpPurchaseDate.Value);
            }
            else
            {
                DividendStocks.NewShare(txtPurchasePrice.Text, txtNumberOfShares.Text, ddlAction.Text, ID, dtpPurchaseDate.Value);
            }
            MainMenu._Dividends.LoadDividendStock();
            LoadAllMainDividends();
            SelectCurrentStock();
            pw.Close();
            this.Close();
        }

        public void SelectCurrentStock()
        {
            if (CurrentDiv)
            {
                Program.MainMenu.lbCurrentDividends.ClearSelected();
                Program.MainMenu.lbCurrentDividends.SelectedValue = Convert.ToInt32(ID);
            }
            else
            {
                Program.MainMenu.lbAllDividends.ClearSelected();
                Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
            }
        }

        public void LoadAllMainDividends()
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

        public void GetSharePrice()
        {
            decimal transactionPrice = (decimal)9.99;
            decimal TotalSharePrice = 0;
            if (ddlAction.Text.ToLower().Contains("bought"))
            {
                TotalSharePrice = (Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtPurchasePrice.Text)) + transactionPrice;
            }
            else
            {
                TotalSharePrice = (Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtPurchasePrice.Text)) - transactionPrice;
            }
            //decimal transactionPrice = (decimal)9.99;
            //decimal TotalSharePrice = (Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtPurchasePrice.Text)) + transactionPrice;
            MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
        }

        private void btnGetPrice_Click(object sender, EventArgs e)
        {
            GetSharePrice();
        }
    }
}
