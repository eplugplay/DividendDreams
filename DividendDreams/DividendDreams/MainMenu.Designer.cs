namespace DividendDreams
{
    partial class MainMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbAllDividends = new System.Windows.Forms.ListBox();
            this.lbCurrentDividends = new System.Windows.Forms.ListBox();
            this.gpDividendStocks = new System.Windows.Forms.GroupBox();
            this.btnDividendPrice = new System.Windows.Forms.Button();
            this.btnGetSharePrice = new System.Windows.Forms.Button();
            this.lblTotalAllDividends = new System.Windows.Forms.Label();
            this.lblTotalPortfolioDividends = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.gpDividendStocks.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1373, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // lbAllDividends
            // 
            this.lbAllDividends.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAllDividends.FormattingEnabled = true;
            this.lbAllDividends.ItemHeight = 15;
            this.lbAllDividends.Location = new System.Drawing.Point(16, 36);
            this.lbAllDividends.Name = "lbAllDividends";
            this.lbAllDividends.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllDividends.Size = new System.Drawing.Size(636, 289);
            this.lbAllDividends.TabIndex = 1;
            // 
            // lbCurrentDividends
            // 
            this.lbCurrentDividends.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentDividends.FormattingEnabled = true;
            this.lbCurrentDividends.ItemHeight = 15;
            this.lbCurrentDividends.Location = new System.Drawing.Point(691, 36);
            this.lbCurrentDividends.Name = "lbCurrentDividends";
            this.lbCurrentDividends.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbCurrentDividends.Size = new System.Drawing.Size(636, 289);
            this.lbCurrentDividends.TabIndex = 2;
            this.lbCurrentDividends.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCurrentDividends_MouseDoubleClick);
            // 
            // gpDividendStocks
            // 
            this.gpDividendStocks.Controls.Add(this.btnDividendPrice);
            this.gpDividendStocks.Controls.Add(this.btnGetSharePrice);
            this.gpDividendStocks.Controls.Add(this.lblTotalAllDividends);
            this.gpDividendStocks.Controls.Add(this.lblTotalPortfolioDividends);
            this.gpDividendStocks.Controls.Add(this.label2);
            this.gpDividendStocks.Controls.Add(this.label1);
            this.gpDividendStocks.Controls.Add(this.btnCalculate);
            this.gpDividendStocks.Controls.Add(this.btnRemove);
            this.gpDividendStocks.Controls.Add(this.btnAdd);
            this.gpDividendStocks.Controls.Add(this.lbAllDividends);
            this.gpDividendStocks.Controls.Add(this.lbCurrentDividends);
            this.gpDividendStocks.Location = new System.Drawing.Point(12, 37);
            this.gpDividendStocks.Name = "gpDividendStocks";
            this.gpDividendStocks.Size = new System.Drawing.Size(1348, 363);
            this.gpDividendStocks.TabIndex = 3;
            this.gpDividendStocks.TabStop = false;
            this.gpDividendStocks.Text = "Dividend Stocks";
            // 
            // btnDividendPrice
            // 
            this.btnDividendPrice.Location = new System.Drawing.Point(1218, 10);
            this.btnDividendPrice.Name = "btnDividendPrice";
            this.btnDividendPrice.Size = new System.Drawing.Size(109, 23);
            this.btnDividendPrice.TabIndex = 20;
            this.btnDividendPrice.TabStop = false;
            this.btnDividendPrice.Text = "Get Dividend Price";
            this.btnDividendPrice.UseVisualStyleBackColor = true;
            this.btnDividendPrice.Click += new System.EventHandler(this.btnDividendPrice_Click);
            // 
            // btnGetSharePrice
            // 
            this.btnGetSharePrice.Location = new System.Drawing.Point(1137, 10);
            this.btnGetSharePrice.Name = "btnGetSharePrice";
            this.btnGetSharePrice.Size = new System.Drawing.Size(75, 23);
            this.btnGetSharePrice.TabIndex = 19;
            this.btnGetSharePrice.TabStop = false;
            this.btnGetSharePrice.Text = "Get Price";
            this.btnGetSharePrice.UseVisualStyleBackColor = true;
            this.btnGetSharePrice.Click += new System.EventHandler(this.btnGetSharePrice_Click);
            // 
            // lblTotalAllDividends
            // 
            this.lblTotalAllDividends.AutoSize = true;
            this.lblTotalAllDividends.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAllDividends.Location = new System.Drawing.Point(112, 18);
            this.lblTotalAllDividends.Name = "lblTotalAllDividends";
            this.lblTotalAllDividends.Size = new System.Drawing.Size(14, 13);
            this.lblTotalAllDividends.TabIndex = 9;
            this.lblTotalAllDividends.Text = "0";
            // 
            // lblTotalPortfolioDividends
            // 
            this.lblTotalPortfolioDividends.AutoSize = true;
            this.lblTotalPortfolioDividends.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPortfolioDividends.Location = new System.Drawing.Point(748, 20);
            this.lblTotalPortfolioDividends.Name = "lblTotalPortfolioDividends";
            this.lblTotalPortfolioDividends.Size = new System.Drawing.Size(14, 13);
            this.lblTotalPortfolioDividends.TabIndex = 8;
            this.lblTotalPortfolioDividends.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Not In Portfolio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(688, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Portfolio:";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(1215, 331);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(112, 23);
            this.btnCalculate.TabIndex = 5;
            this.btnCalculate.Text = "Calculate Results";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(658, 229);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "←";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(658, 86);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "→";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 404);
            this.Controls.Add(this.gpDividendStocks);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpDividendStocks.ResumeLayout(false);
            this.gpDividendStocks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.GroupBox gpDividendStocks;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.ListBox lbAllDividends;
        public System.Windows.Forms.ListBox lbCurrentDividends;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalPortfolioDividends;
        private System.Windows.Forms.Label lblTotalAllDividends;
        private System.Windows.Forms.Button btnDividendPrice;
        private System.Windows.Forms.Button btnGetSharePrice;
    }
}

