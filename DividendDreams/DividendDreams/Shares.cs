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
        public Shares(bool edit, string id, string dividendPriceID)
        {
            ID = id;
            Edit = edit;
            DividendPriceID = dividendPriceID;
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
            if (Edit)
            {
                DividendStocks.UpdateShare(txtPurchasePrice.Text, txtNumberOfShares.Text, ddlAction.Text, DividendPriceID);
            }
            else
            {
                DividendStocks.NewShare(txtPurchasePrice.Text, txtNumberOfShares.Text, ddlAction.Text, ID);
            }
            MainMenu._Dividends.LoadDividendStock();
            Program.MainMenu.LoadCurrentDividends();
            this.Close();
        }

        public void GetSharePrice()
        {
            decimal TotalSharePrice = Convert.ToDecimal(txtPurchasePrice.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
            MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
        }

        private void btnGetPrice_Click(object sender, EventArgs e)
        {
            GetSharePrice();
        }
    }
}
