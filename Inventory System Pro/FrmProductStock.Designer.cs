namespace Inventory_System_Pro
{
    partial class FrmProductStock
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPotentialProfit = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPotentialSales = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentQty = new System.Windows.Forms.Label();
            this.lvStock = new System.Windows.Forms.ListView();
            this.ProductId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Productname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Unit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Warehouse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentQty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lastcost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Stockvalue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Saleprice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Potentialsales = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Potentialprofit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReorderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkLowStock = new System.Windows.Forms.CheckBox();
            this.chkOutOfStock = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.label20);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.lvStock);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1299, 741);
            this.splitContainer1.SplitterDistance = 57;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(118, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 24);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(14, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 18);
            this.label20.TabIndex = 2;
            this.label20.Text = "Product Code";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumAquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblPotentialProfit);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lblPotentialSales);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblStockValue);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblCurrentQty);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 588);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1297, 90);
            this.panel2.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::Inventory_System_Pro.Properties.Resources.Pixelkit_Flat_Jewels_Refresh_24;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(997, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(135, 37);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_File_Delete1;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1138, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 37);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(302, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Potential Profit";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPotentialProfit
            // 
            this.lblPotentialProfit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPotentialProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPotentialProfit.Location = new System.Drawing.Point(412, 50);
            this.lblPotentialProfit.Name = "lblPotentialProfit";
            this.lblPotentialProfit.Size = new System.Drawing.Size(163, 29);
            this.lblPotentialProfit.TabIndex = 8;
            this.lblPotentialProfit.Text = "0";
            this.lblPotentialProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(300, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "Potential Sales";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPotentialSales
            // 
            this.lblPotentialSales.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPotentialSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPotentialSales.Location = new System.Drawing.Point(412, 10);
            this.lblPotentialSales.Name = "lblPotentialSales";
            this.lblPotentialSales.Size = new System.Drawing.Size(163, 29);
            this.lblPotentialSales.TabIndex = 6;
            this.lblPotentialSales.Text = "0";
            this.lblPotentialSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Stock Value";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStockValue
            // 
            this.lblStockValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockValue.Location = new System.Drawing.Point(117, 50);
            this.lblStockValue.Name = "lblStockValue";
            this.lblStockValue.Size = new System.Drawing.Size(163, 29);
            this.lblStockValue.TabIndex = 4;
            this.lblStockValue.Text = "0";
            this.lblStockValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current Qty";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentQty
            // 
            this.lblCurrentQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentQty.Location = new System.Drawing.Point(117, 10);
            this.lblCurrentQty.Name = "lblCurrentQty";
            this.lblCurrentQty.Size = new System.Drawing.Size(163, 29);
            this.lblCurrentQty.TabIndex = 0;
            this.lblCurrentQty.Text = "0";
            this.lblCurrentQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvStock
            // 
            this.lvStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvStock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProductId,
            this.Code,
            this.Productname,
            this.Unit,
            this.Warehouse,
            this.CurrentQty,
            this.Lastcost,
            this.Stockvalue,
            this.Saleprice,
            this.Potentialsales,
            this.Potentialprofit,
            this.ReorderLevel,
            this.Status});
            this.lvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStock.FullRowSelect = true;
            this.lvStock.GridLines = true;
            this.lvStock.HideSelection = false;
            this.lvStock.Location = new System.Drawing.Point(0, 66);
            this.lvStock.MultiSelect = false;
            this.lvStock.Name = "lvStock";
            this.lvStock.Size = new System.Drawing.Size(1297, 612);
            this.lvStock.TabIndex = 1;
            this.lvStock.UseCompatibleStateImageBehavior = false;
            this.lvStock.View = System.Windows.Forms.View.Details;
            // 
            // ProductId
            // 
            this.ProductId.Text = "Id";
            // 
            // Code
            // 
            this.Code.Text = "Code";
            this.Code.Width = 80;
            // 
            // Productname
            // 
            this.Productname.Text = "Product";
            this.Productname.Width = 200;
            // 
            // Unit
            // 
            this.Unit.Text = "Unit";
            this.Unit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Unit.Width = 80;
            // 
            // Warehouse
            // 
            this.Warehouse.Text = "Warehouse";
            this.Warehouse.Width = 120;
            // 
            // CurrentQty
            // 
            this.CurrentQty.Text = "Stock";
            this.CurrentQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CurrentQty.Width = 80;
            // 
            // Lastcost
            // 
            this.Lastcost.Text = "Last Cost";
            this.Lastcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Lastcost.Width = 100;
            // 
            // Stockvalue
            // 
            this.Stockvalue.Text = "Stock Value";
            this.Stockvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Stockvalue.Width = 100;
            // 
            // Saleprice
            // 
            this.Saleprice.Text = "Sale Price";
            this.Saleprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Saleprice.Width = 100;
            // 
            // Potentialsales
            // 
            this.Potentialsales.Text = "Potential Sales";
            this.Potentialsales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Potentialsales.Width = 120;
            // 
            // Potentialprofit
            // 
            this.Potentialprofit.Text = "Potential Profit";
            this.Potentialprofit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Potentialprofit.Width = 120;
            // 
            // ReorderLevel
            // 
            this.ReorderLevel.Text = "Reorder";
            this.ReorderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReorderLevel.Width = 80;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cmbWarehouse);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.chkLowStock);
            this.panel1.Controls.Add(this.chkOutOfStock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1297, 66);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1008, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Out of Stock";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(830, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "Low Stock";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(117, 19);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(352, 26);
            this.cmbWarehouse.TabIndex = 0;
            this.cmbWarehouse.Click += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(26, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 18);
            this.label5.TabIndex = 29;
            this.label5.Text = "Warehouse";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkLowStock
            // 
            this.chkLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLowStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkLowStock.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLowStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLowStock.Location = new System.Drawing.Point(805, 11);
            this.chkLowStock.Name = "chkLowStock";
            this.chkLowStock.Size = new System.Drawing.Size(34, 46);
            this.chkLowStock.TabIndex = 40;
            this.chkLowStock.UseVisualStyleBackColor = true;
            this.chkLowStock.CheckedChanged += new System.EventHandler(this.chkLowStock_CheckedChanged);
            // 
            // chkOutOfStock
            // 
            this.chkOutOfStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOutOfStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkOutOfStock.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOutOfStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOutOfStock.Location = new System.Drawing.Point(983, 11);
            this.chkOutOfStock.Name = "chkOutOfStock";
            this.chkOutOfStock.Size = new System.Drawing.Size(34, 46);
            this.chkOutOfStock.TabIndex = 42;
            this.chkOutOfStock.UseVisualStyleBackColor = true;
            this.chkOutOfStock.CheckedChanged += new System.EventHandler(this.chkOutOfStock_CheckedChanged);
            // 
            // FrmProductStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 741);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmProductStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Stock";
            this.Load += new System.EventHandler(this.FrmProductStock_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkOutOfStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkLowStock;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lvStock;
        private System.Windows.Forms.ColumnHeader ProductId;
        private System.Windows.Forms.ColumnHeader Code;
        private System.Windows.Forms.ColumnHeader Productname;
        private System.Windows.Forms.ColumnHeader Unit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCurrentQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPotentialProfit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPotentialSales;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.ColumnHeader Warehouse;
        private System.Windows.Forms.ColumnHeader CurrentQty;
        private System.Windows.Forms.ColumnHeader Lastcost;
        private System.Windows.Forms.ColumnHeader Stockvalue;
        private System.Windows.Forms.ColumnHeader Saleprice;
        private System.Windows.Forms.ColumnHeader Potentialsales;
        private System.Windows.Forms.ColumnHeader Potentialprofit;
        private System.Windows.Forms.ColumnHeader ReorderLevel;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
    }
}