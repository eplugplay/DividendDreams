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
    public partial class Dividends : Form
    {
        static public Shares _Shares;
        public bool Edit { get; set; }
        public string ID { get; set; }
        public Dividends(bool edit, string id)
        {
            Edit = edit;
            ID = id;
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
                DividendStocks.UpdateDividendStock(ID, txtSymbol.Text, txtStockName.Text, ddlIndustry.Text, txtAnnualDividend.Text, txtDividendPercent.Text, 
                    ddlCapSize.Text, txtDripCostInitial.Text, txtDripCost.Text, chkdrip.Checked == true ? "true" : "false", dtpExDividend.Value, txtDripNotes.Text);
                Program.MainMenu.LoadDividendStocks();
                pw.Close();
                this.Close();
            }
            else
            {
                DividendStocks.NewDividendStock(txtSymbol.Text, txtStockName.Text, ddlIndustry.Text, txtSharePrice.Text, txtAnnualDividend.Text, txtNumberOfShares.Text, 
                    txtDividendPercent.Text, ddlCapSize.Text, txtDripCost.Text, txtDripCostInitial.Text, chkdrip.Checked == true ? "true" : "false", dtpExDividend.Value, txtDripNotes.Text);
                Program.MainMenu.LoadDividendStocks();
                pw.Close();
                this.Close();
            }
        }

        private void Dividends_Load(object sender, EventArgs e)
        {
            ddlCapSize.SelectedIndex = 0;
            ddlIndustry.SelectedIndex = 0;
            if (Edit)
            {
                LoadDividendStock();
                btnSave.Text = "Update";
            }
            else
            {
                gpSharesOptions.Enabled = false;
                btnSave.Text = "Save";
            }
        }

        public void LoadDividendStock()
        {
            DataTable dt = DividendStocks.GetDividend(ID);
            txtSymbol.Text = dt.Rows[0]["symbol"].ToString();
            txtStockName.Text = dt.Rows[0]["stockname"].ToString();
            ddlIndustry.SelectedIndex = ddlIndustry.FindString(dt.Rows[0]["industry"].ToString());
            txtAnnualDividend.Text = dt.Rows[0]["anndividend"].ToString();
            txtDividendPercent.Text = dt.Rows[0]["dividendpercent"].ToString();
            ddlCapSize.SelectedIndex = ddlCapSize.FindString(dt.Rows[0]["capsize"].ToString());
            txtDripCostInitial.Text = dt.Rows[0]["dripinitialcost"].ToString();
            txtDripCost.Text = dt.Rows[0]["dripcost"].ToString();
            txtDripNotes.Text = dt.Rows[0]["dripnotes"].ToString();
            chkdrip.Checked = dt.Rows[0]["drip"].ToString() == "true" ? true : false;
            if (dt.Rows[0]["exdividend"] != DBNull.Value)
            {
                dtpExDividend.Value = Convert.ToDateTime(dt.Rows[0]["exdividend"]);
            }
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
            //if (txtSharePrice.Text == "")
            //{
            //    MessageBox.Show("Please enter share price.");
            //    return false;
            //}
            //try
            //{
            //    decimal.Parse(txtSharePrice.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("Please enter numbers only.");
            //    txtSharePrice.Focus();
            //    return false;
            //}
            if (txtAnnualDividend.Text == "")
            {
                MessageBox.Show("Please enter annual dividend.");
                return false;
            }
            try
            {
                decimal.Parse(txtAnnualDividend.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtAnnualDividend.Focus();
                return false;
            }
            //if (txtNumberOfShares.Text == "")
            //{
            //    MessageBox.Show("Please enter number of shares.");
            //    return false;
            //}
            //try
            //{
            //    int.Parse(txtNumberOfShares.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("Please enter numbers only.");
            //    txtNumberOfShares.Focus();
            //    return false;
            //}
            return true;
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal TotalSharePrice = Convert.ToDecimal(txtSharePrice.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
                MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
            }
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal AutoDripCost = 0;
                try
                {
                    AutoDripCost = (Convert.ToDecimal(txtDripCost.Text) * Convert.ToDecimal(txtNumberOfShares.Text)) + Convert.ToDecimal(txtDripCostInitial.Text);
                }
                catch
                {
                    AutoDripCost = 0;
                }
                decimal TotalDividendPrice = chkdrip.Checked == true ? (Convert.ToDecimal(txtAnnualDividend.Text) * Convert.ToDecimal(txtNumberOfShares.Text) - AutoDripCost) 
                    : Convert.ToDecimal(txtAnnualDividend.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
                decimal QuarterlyDividendPrice = TotalDividendPrice / 3;
                decimal MonthlyDividendPrice = TotalDividendPrice / 12;
                MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\n Quarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\n Monthly: $" + Math.Round(MonthlyDividendPrice, 2));
            }
        }


        public void OpenSharesForm(bool edit)
        {
            if (_Shares == null || _Shares.IsDisposed)
            {
                _Shares = new Shares(edit, ID, ddlSharePurchaseDate.SelectedValue == null ? "" : ddlSharePurchaseDate.SelectedValue.ToString());
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
                    DividendStocks.DeleteShare(ddlSharePurchaseDate.SelectedValue.ToString());
                    LoadPurchaseDates();
                    LoadPurchaseData();
                    Program.MainMenu.LoadCurrentDividends();
                }
            }
        }
    }
}
