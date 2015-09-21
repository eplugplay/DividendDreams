namespace DividendDreams
{
    partial class Dividends
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.lblSymbol = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.txtStockName = new System.Windows.Forms.TextBox();
            this.lblSharePrice = new System.Windows.Forms.Label();
            this.txtSharePrice = new System.Windows.Forms.TextBox();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.txtIndustry = new System.Windows.Forms.TextBox();
            this.lblNumberShare = new System.Windows.Forms.Label();
            this.txtNumberOfShares = new System.Windows.Forms.TextBox();
            this.lblAnnualDividend = new System.Windows.Forms.Label();
            this.txtAnnualDividend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDividendPercent = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gpDividendInfo = new System.Windows.Forms.GroupBox();
            this.btnDividendPrice = new System.Windows.Forms.Button();
            this.ddlCapSize = new System.Windows.Forms.ComboBox();
            this.lblCapSize = new System.Windows.Forms.Label();
            this.btnGetSharePrice = new System.Windows.Forms.Button();
            this.ddlSharePurchaseDate = new System.Windows.Forms.ComboBox();
            this.lblSharePurchaseDate = new System.Windows.Forms.Label();
            this.btnNewShares = new System.Windows.Forms.Button();
            this.btnEditShares = new System.Windows.Forms.Button();
            this.btnDeleteShares = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gpDividendInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(107, 21);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(210, 20);
            this.txtSymbol.TabIndex = 0;
            // 
            // lblSymbol
            // 
            this.lblSymbol.AutoSize = true;
            this.lblSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSymbol.Location = new System.Drawing.Point(19, 24);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(51, 13);
            this.lblSymbol.TabIndex = 1;
            this.lblSymbol.Text = "Symbol:";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockName.Location = new System.Drawing.Point(324, 24);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(80, 13);
            this.lblStockName.TabIndex = 3;
            this.lblStockName.Text = "Stock Name:";
            // 
            // txtStockName
            // 
            this.txtStockName.Location = new System.Drawing.Point(399, 21);
            this.txtStockName.Name = "txtStockName";
            this.txtStockName.Size = new System.Drawing.Size(199, 20);
            this.txtStockName.TabIndex = 1;
            // 
            // lblSharePrice
            // 
            this.lblSharePrice.AutoSize = true;
            this.lblSharePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSharePrice.Location = new System.Drawing.Point(13, 47);
            this.lblSharePrice.Name = "lblSharePrice";
            this.lblSharePrice.Size = new System.Drawing.Size(104, 13);
            this.lblSharePrice.TabIndex = 7;
            this.lblSharePrice.Text = "Purchased Price:";
            // 
            // txtSharePrice
            // 
            this.txtSharePrice.Location = new System.Drawing.Point(125, 44);
            this.txtSharePrice.Name = "txtSharePrice";
            this.txtSharePrice.ReadOnly = true;
            this.txtSharePrice.Size = new System.Drawing.Size(210, 20);
            this.txtSharePrice.TabIndex = 6;
            this.txtSharePrice.TabStop = false;
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndustry.Location = new System.Drawing.Point(20, 50);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(56, 13);
            this.lblIndustry.TabIndex = 5;
            this.lblIndustry.Text = "Industry:";
            // 
            // txtIndustry
            // 
            this.txtIndustry.Location = new System.Drawing.Point(107, 47);
            this.txtIndustry.Name = "txtIndustry";
            this.txtIndustry.Size = new System.Drawing.Size(210, 20);
            this.txtIndustry.TabIndex = 2;
            // 
            // lblNumberShare
            // 
            this.lblNumberShare.AutoSize = true;
            this.lblNumberShare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberShare.Location = new System.Drawing.Point(25, 72);
            this.lblNumberShare.Name = "lblNumberShare";
            this.lblNumberShare.Size = new System.Drawing.Size(77, 13);
            this.lblNumberShare.TabIndex = 11;
            this.lblNumberShare.Text = "# of Shares:";
            // 
            // txtNumberOfShares
            // 
            this.txtNumberOfShares.Location = new System.Drawing.Point(125, 69);
            this.txtNumberOfShares.Name = "txtNumberOfShares";
            this.txtNumberOfShares.ReadOnly = true;
            this.txtNumberOfShares.Size = new System.Drawing.Size(210, 20);
            this.txtNumberOfShares.TabIndex = 7;
            this.txtNumberOfShares.TabStop = false;
            // 
            // lblAnnualDividend
            // 
            this.lblAnnualDividend.AutoSize = true;
            this.lblAnnualDividend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnnualDividend.Location = new System.Drawing.Point(19, 76);
            this.lblAnnualDividend.Name = "lblAnnualDividend";
            this.lblAnnualDividend.Size = new System.Drawing.Size(70, 13);
            this.lblAnnualDividend.TabIndex = 9;
            this.lblAnnualDividend.Text = "Ann. Divid:";
            // 
            // txtAnnualDividend
            // 
            this.txtAnnualDividend.Location = new System.Drawing.Point(107, 73);
            this.txtAnnualDividend.Name = "txtAnnualDividend";
            this.txtAnnualDividend.Size = new System.Drawing.Size(210, 20);
            this.txtAnnualDividend.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Dividend %:";
            // 
            // txtDividendPercent
            // 
            this.txtDividendPercent.Location = new System.Drawing.Point(399, 73);
            this.txtDividendPercent.Name = "txtDividendPercent";
            this.txtDividendPercent.Size = new System.Drawing.Size(199, 20);
            this.txtDividendPercent.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(528, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gpDividendInfo
            // 
            this.gpDividendInfo.Controls.Add(this.btnDividendPrice);
            this.gpDividendInfo.Controls.Add(this.ddlCapSize);
            this.gpDividendInfo.Controls.Add(this.lblCapSize);
            this.gpDividendInfo.Controls.Add(this.btnGetSharePrice);
            this.gpDividendInfo.Controls.Add(this.btnSave);
            this.gpDividendInfo.Controls.Add(this.txtSymbol);
            this.gpDividendInfo.Controls.Add(this.label1);
            this.gpDividendInfo.Controls.Add(this.lblSymbol);
            this.gpDividendInfo.Controls.Add(this.txtDividendPercent);
            this.gpDividendInfo.Controls.Add(this.txtStockName);
            this.gpDividendInfo.Controls.Add(this.lblStockName);
            this.gpDividendInfo.Controls.Add(this.txtIndustry);
            this.gpDividendInfo.Controls.Add(this.lblAnnualDividend);
            this.gpDividendInfo.Controls.Add(this.lblIndustry);
            this.gpDividendInfo.Controls.Add(this.txtAnnualDividend);
            this.gpDividendInfo.Location = new System.Drawing.Point(5, 5);
            this.gpDividendInfo.Name = "gpDividendInfo";
            this.gpDividendInfo.Size = new System.Drawing.Size(609, 137);
            this.gpDividendInfo.TabIndex = 15;
            this.gpDividendInfo.TabStop = false;
            this.gpDividendInfo.Text = "Shares Info:";
            // 
            // btnDividendPrice
            // 
            this.btnDividendPrice.Location = new System.Drawing.Point(413, 99);
            this.btnDividendPrice.Name = "btnDividendPrice";
            this.btnDividendPrice.Size = new System.Drawing.Size(109, 23);
            this.btnDividendPrice.TabIndex = 18;
            this.btnDividendPrice.TabStop = false;
            this.btnDividendPrice.Text = "Get Dividend Price";
            this.btnDividendPrice.UseVisualStyleBackColor = true;
            this.btnDividendPrice.Click += new System.EventHandler(this.btnDividendPrice_Click);
            // 
            // ddlCapSize
            // 
            this.ddlCapSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCapSize.FormattingEnabled = true;
            this.ddlCapSize.Items.AddRange(new object[] {
            "Small Cap",
            "Mid Cap",
            "Large Cap",
            "ETF",
            "ETN"});
            this.ddlCapSize.Location = new System.Drawing.Point(399, 47);
            this.ddlCapSize.Name = "ddlCapSize";
            this.ddlCapSize.Size = new System.Drawing.Size(199, 21);
            this.ddlCapSize.TabIndex = 3;
            // 
            // lblCapSize
            // 
            this.lblCapSize.AutoSize = true;
            this.lblCapSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapSize.Location = new System.Drawing.Point(324, 50);
            this.lblCapSize.Name = "lblCapSize";
            this.lblCapSize.Size = new System.Drawing.Size(61, 13);
            this.lblCapSize.TabIndex = 17;
            this.lblCapSize.Text = "Cap Size:";
            // 
            // btnGetSharePrice
            // 
            this.btnGetSharePrice.Location = new System.Drawing.Point(332, 99);
            this.btnGetSharePrice.Name = "btnGetSharePrice";
            this.btnGetSharePrice.Size = new System.Drawing.Size(75, 23);
            this.btnGetSharePrice.TabIndex = 15;
            this.btnGetSharePrice.TabStop = false;
            this.btnGetSharePrice.Text = "Get Price";
            this.btnGetSharePrice.UseVisualStyleBackColor = true;
            this.btnGetSharePrice.Click += new System.EventHandler(this.btnGetSharePrice_Click);
            // 
            // ddlSharePurchaseDate
            // 
            this.ddlSharePurchaseDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSharePurchaseDate.FormattingEnabled = true;
            this.ddlSharePurchaseDate.Location = new System.Drawing.Point(125, 17);
            this.ddlSharePurchaseDate.Name = "ddlSharePurchaseDate";
            this.ddlSharePurchaseDate.Size = new System.Drawing.Size(210, 21);
            this.ddlSharePurchaseDate.TabIndex = 6;
            this.ddlSharePurchaseDate.SelectedIndexChanged += new System.EventHandler(this.ddlSharePurchaseDate_SelectedIndexChanged);
            // 
            // lblSharePurchaseDate
            // 
            this.lblSharePurchaseDate.AutoSize = true;
            this.lblSharePurchaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSharePurchaseDate.Location = new System.Drawing.Point(13, 20);
            this.lblSharePurchaseDate.Name = "lblSharePurchaseDate";
            this.lblSharePurchaseDate.Size = new System.Drawing.Size(95, 13);
            this.lblSharePurchaseDate.TabIndex = 20;
            this.lblSharePurchaseDate.Text = "Purchase Date:";
            // 
            // btnNewShares
            // 
            this.btnNewShares.Location = new System.Drawing.Point(85, 95);
            this.btnNewShares.Name = "btnNewShares";
            this.btnNewShares.Size = new System.Drawing.Size(75, 23);
            this.btnNewShares.TabIndex = 21;
            this.btnNewShares.TabStop = false;
            this.btnNewShares.Text = "New Shares";
            this.btnNewShares.UseVisualStyleBackColor = true;
            this.btnNewShares.Click += new System.EventHandler(this.btnNewShares_Click);
            // 
            // btnEditShares
            // 
            this.btnEditShares.Location = new System.Drawing.Point(166, 95);
            this.btnEditShares.Name = "btnEditShares";
            this.btnEditShares.Size = new System.Drawing.Size(75, 23);
            this.btnEditShares.TabIndex = 22;
            this.btnEditShares.TabStop = false;
            this.btnEditShares.Text = "Edit Shares";
            this.btnEditShares.UseVisualStyleBackColor = true;
            this.btnEditShares.Click += new System.EventHandler(this.btnEditShares_Click);
            // 
            // btnDeleteShares
            // 
            this.btnDeleteShares.Location = new System.Drawing.Point(247, 95);
            this.btnDeleteShares.Name = "btnDeleteShares";
            this.btnDeleteShares.Size = new System.Drawing.Size(88, 23);
            this.btnDeleteShares.TabIndex = 23;
            this.btnDeleteShares.TabStop = false;
            this.btnDeleteShares.Text = "Delete Shares";
            this.btnDeleteShares.UseVisualStyleBackColor = true;
            this.btnDeleteShares.Click += new System.EventHandler(this.btnDeleteShares_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteShares);
            this.groupBox1.Controls.Add(this.btnEditShares);
            this.groupBox1.Controls.Add(this.ddlSharePurchaseDate);
            this.groupBox1.Controls.Add(this.btnNewShares);
            this.groupBox1.Controls.Add(this.txtSharePrice);
            this.groupBox1.Controls.Add(this.txtNumberOfShares);
            this.groupBox1.Controls.Add(this.lblNumberShare);
            this.groupBox1.Controls.Add(this.lblSharePurchaseDate);
            this.groupBox1.Controls.Add(this.lblSharePrice);
            this.groupBox1.Location = new System.Drawing.Point(620, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 130);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shares Options:";
            // 
            // Dividends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 152);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpDividendInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Dividends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Dividend Stock";
            this.Load += new System.EventHandler(this.Dividends_Load);
            this.gpDividendInfo.ResumeLayout(false);
            this.gpDividendInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label lblSymbol;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.TextBox txtStockName;
        private System.Windows.Forms.Label lblSharePrice;
        private System.Windows.Forms.TextBox txtSharePrice;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.TextBox txtIndustry;
        private System.Windows.Forms.Label lblNumberShare;
        private System.Windows.Forms.TextBox txtNumberOfShares;
        private System.Windows.Forms.Label lblAnnualDividend;
        private System.Windows.Forms.TextBox txtAnnualDividend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDividendPercent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gpDividendInfo;
        private System.Windows.Forms.Button btnGetSharePrice;
        private System.Windows.Forms.Label lblCapSize;
        private System.Windows.Forms.ComboBox ddlCapSize;
        private System.Windows.Forms.Button btnDividendPrice;
        private System.Windows.Forms.Label lblSharePurchaseDate;
        private System.Windows.Forms.ComboBox ddlSharePurchaseDate;
        private System.Windows.Forms.Button btnEditShares;
        private System.Windows.Forms.Button btnNewShares;
        private System.Windows.Forms.Button btnDeleteShares;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}