namespace Inventory_System_Pro
{
    partial class FrmStockLedger
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
            this.scMaster = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTranType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvLedger = new System.Windows.Forms.ListView();
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Transaction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Reference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Product = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Warehouse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QtyIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QtyOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnitCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BalanceQty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BalanceValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AverageCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.COGS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Remarks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label12 = new System.Windows.Forms.Label();
            this.lblClosingValue = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblAverageCost = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClosingQty = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotalCOGS = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalQtyOut = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalQtyIn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).BeginInit();
            this.scMaster.Panel1.SuspendLayout();
            this.scMaster.Panel2.SuspendLayout();
            this.scMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMaster
            // 
            this.scMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.scMaster.Location = new System.Drawing.Point(0, 0);
            this.scMaster.Name = "scMaster";
            this.scMaster.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMaster.Panel1
            // 
            this.scMaster.Panel1.Controls.Add(this.label3);
            // 
            // scMaster.Panel2
            // 
            this.scMaster.Panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.scMaster.Panel2.Controls.Add(this.btnClose);
            this.scMaster.Panel2.Controls.Add(this.dtpToDate);
            this.scMaster.Panel2.Controls.Add(this.btnRefresh);
            this.scMaster.Panel2.Controls.Add(this.btnSearch);
            this.scMaster.Panel2.Controls.Add(this.label7);
            this.scMaster.Panel2.Controls.Add(this.dtpFromDate);
            this.scMaster.Panel2.Controls.Add(this.label4);
            this.scMaster.Panel2.Controls.Add(this.cmbTranType);
            this.scMaster.Panel2.Controls.Add(this.label2);
            this.scMaster.Panel2.Controls.Add(this.cmbWarehouse);
            this.scMaster.Panel2.Controls.Add(this.label6);
            this.scMaster.Panel2.Controls.Add(this.cmbProduct);
            this.scMaster.Panel2.Controls.Add(this.label5);
            this.scMaster.Panel2.Controls.Add(this.txtSearch);
            this.scMaster.Panel2.Controls.Add(this.label1);
            this.scMaster.Panel2.ForeColor = System.Drawing.Color.White;
            this.scMaster.Size = new System.Drawing.Size(1170, 243);
            this.scMaster.SplitterDistance = 75;
            this.scMaster.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Bell MT", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1168, 73);
            this.label3.TabIndex = 0;
            this.label3.Text = "Stock Ledger";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_File_Delete_24;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1021, 106);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(136, 37);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(636, 122);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(280, 24);
            this.dtpToDate.TabIndex = 5;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::Inventory_System_Pro.Properties.Resources.Graphicloads_100_Flat_2_Arrow_refresh_4_24;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(1021, 63);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(136, 37);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Pretty_Office_2_Search_24;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(1021, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(136, 37);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(571, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 18);
            this.label7.TabIndex = 43;
            this.label7.Text = "To Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(157, 122);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(291, 24);
            this.dtpFromDate.TabIndex = 4;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(72, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "From Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTranType
            // 
            this.cmbTranType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTranType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTranType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTranType.FormattingEnabled = true;
            this.cmbTranType.Items.AddRange(new object[] {
            "ALL",
            "PURCHASE",
            "PURCHASE_RETURN",
            "PURCHASE_RETURN_CANCEL",
            "SALE",
            "SALE_RETURN",
            "SALE_RETURN_CANCEL"});
            this.cmbTranType.Location = new System.Drawing.Point(157, 83);
            this.cmbTranType.Name = "cmbTranType";
            this.cmbTranType.Size = new System.Drawing.Size(291, 26);
            this.cmbTranType.TabIndex = 3;
            this.cmbTranType.SelectedIndexChanged += new System.EventHandler(this.cmbTranType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 18);
            this.label2.TabIndex = 40;
            this.label2.Text = "Tran. Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(636, 45);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(334, 26);
            this.cmbWarehouse.TabIndex = 2;
            this.cmbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(547, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Warehouse";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProduct
            // 
            this.cmbProduct.DisplayMember = "ProductCode";
            this.cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(157, 45);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(291, 26);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.ValueMember = "ProductId";
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(91, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Product";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(157, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(155, 24);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Product Search";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 248);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvLedger);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.lblClosingValue);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.lblAverageCost);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.lblClosingQty);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalCOGS);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalQtyOut);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalQtyIn);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Size = new System.Drawing.Size(1170, 493);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvLedger
            // 
            this.lvLedger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lvLedger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvLedger.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Date,
            this.Transaction,
            this.Reference,
            this.Code,
            this.Product,
            this.Warehouse,
            this.QtyIn,
            this.QtyOut,
            this.UnitCost,
            this.BalanceQty,
            this.BalanceValue,
            this.AverageCost,
            this.COGS,
            this.Remarks});
            this.lvLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLedger.FullRowSelect = true;
            this.lvLedger.GridLines = true;
            this.lvLedger.HideSelection = false;
            this.lvLedger.Location = new System.Drawing.Point(0, 0);
            this.lvLedger.MultiSelect = false;
            this.lvLedger.Name = "lvLedger";
            this.lvLedger.Size = new System.Drawing.Size(1168, 395);
            this.lvLedger.TabIndex = 1;
            this.lvLedger.UseCompatibleStateImageBehavior = false;
            this.lvLedger.View = System.Windows.Forms.View.Details;
            // 
            // Date
            // 
            this.Date.Text = "Date";
            // 
            // Transaction
            // 
            this.Transaction.Text = "Transaction";
            this.Transaction.Width = 100;
            // 
            // Reference
            // 
            this.Reference.Text = "Ref. No";
            this.Reference.Width = 100;
            // 
            // Code
            // 
            this.Code.Text = "Code";
            // 
            // Product
            // 
            this.Product.Text = "Product Name";
            this.Product.Width = 140;
            // 
            // Warehouse
            // 
            this.Warehouse.Text = "Warehouse";
            this.Warehouse.Width = 85;
            // 
            // QtyIn
            // 
            this.QtyIn.Text = "Qty. In";
            this.QtyIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QtyIn.Width = 80;
            // 
            // QtyOut
            // 
            this.QtyOut.Text = "Qty Out";
            this.QtyOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QtyOut.Width = 80;
            // 
            // UnitCost
            // 
            this.UnitCost.Text = "Unit Cost";
            this.UnitCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UnitCost.Width = 80;
            // 
            // BalanceQty
            // 
            this.BalanceQty.Text = "Bal. Qty.";
            this.BalanceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BalanceQty.Width = 80;
            // 
            // BalanceValue
            // 
            this.BalanceValue.Text = "Bal. Val.";
            this.BalanceValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BalanceValue.Width = 80;
            // 
            // AverageCost
            // 
            this.AverageCost.Text = "Avg. Cost";
            this.AverageCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AverageCost.Width = 80;
            // 
            // COGS
            // 
            this.COGS.Text = "C.O.G.S";
            this.COGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.COGS.Width = 80;
            // 
            // Remarks
            // 
            this.Remarks.Text = "Remrks";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(657, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 18);
            this.label12.TabIndex = 21;
            this.label12.Text = "Closing Value";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClosingValue
            // 
            this.lblClosingValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClosingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClosingValue.Location = new System.Drawing.Point(760, 50);
            this.lblClosingValue.Name = "lblClosingValue";
            this.lblClosingValue.Size = new System.Drawing.Size(163, 29);
            this.lblClosingValue.TabIndex = 20;
            this.lblClosingValue.Text = "0";
            this.lblClosingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(657, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 18);
            this.label14.TabIndex = 19;
            this.label14.Text = "Average Cost";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAverageCost
            // 
            this.lblAverageCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAverageCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageCost.Location = new System.Drawing.Point(760, 10);
            this.lblAverageCost.Name = "lblAverageCost";
            this.lblAverageCost.Size = new System.Drawing.Size(163, 29);
            this.lblAverageCost.TabIndex = 18;
            this.lblAverageCost.Text = "0";
            this.lblAverageCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(359, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "Closing Qty.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClosingQty
            // 
            this.lblClosingQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClosingQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClosingQty.Location = new System.Drawing.Point(452, 50);
            this.lblClosingQty.Name = "lblClosingQty";
            this.lblClosingQty.Size = new System.Drawing.Size(163, 29);
            this.lblClosingQty.TabIndex = 16;
            this.lblClosingQty.Text = "0";
            this.lblClosingQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(383, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "C.O.G.S";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCOGS
            // 
            this.lblTotalCOGS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalCOGS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCOGS.Location = new System.Drawing.Point(452, 10);
            this.lblTotalCOGS.Name = "lblTotalCOGS";
            this.lblTotalCOGS.Size = new System.Drawing.Size(163, 29);
            this.lblTotalCOGS.TabIndex = 14;
            this.lblTotalCOGS.Text = "0";
            this.lblTotalCOGS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(63, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Quantity Out";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalQtyOut
            // 
            this.lblTotalQtyOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalQtyOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQtyOut.Location = new System.Drawing.Point(157, 50);
            this.lblTotalQtyOut.Name = "lblTotalQtyOut";
            this.lblTotalQtyOut.Size = new System.Drawing.Size(163, 29);
            this.lblTotalQtyOut.TabIndex = 12;
            this.lblTotalQtyOut.Text = "0";
            this.lblTotalQtyOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(76, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "Quantity In";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalQtyIn
            // 
            this.lblTotalQtyIn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalQtyIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQtyIn.Location = new System.Drawing.Point(157, 10);
            this.lblTotalQtyIn.Name = "lblTotalQtyIn";
            this.lblTotalQtyIn.Size = new System.Drawing.Size(163, 29);
            this.lblTotalQtyIn.TabIndex = 10;
            this.lblTotalQtyIn.Text = "0";
            this.lblTotalQtyIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmStockLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 741);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.scMaster);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmStockLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Ledger";
            this.Load += new System.EventHandler(this.FrmStockLedger_Load);
            this.scMaster.Panel1.ResumeLayout(false);
            this.scMaster.Panel2.ResumeLayout(false);
            this.scMaster.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).EndInit();
            this.scMaster.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMaster;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTranType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvLedger;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClosingQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalCOGS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalQtyOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTotalQtyIn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblAverageCost;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader Transaction;
        private System.Windows.Forms.ColumnHeader Reference;
        private System.Windows.Forms.ColumnHeader Code;
        private System.Windows.Forms.ColumnHeader Product;
        private System.Windows.Forms.ColumnHeader Warehouse;
        private System.Windows.Forms.ColumnHeader QtyIn;
        private System.Windows.Forms.ColumnHeader QtyOut;
        private System.Windows.Forms.ColumnHeader UnitCost;
        private System.Windows.Forms.ColumnHeader BalanceQty;
        private System.Windows.Forms.ColumnHeader BalanceValue;
        private System.Windows.Forms.ColumnHeader AverageCost;
        private System.Windows.Forms.ColumnHeader COGS;
        private System.Windows.Forms.ColumnHeader Remarks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblClosingValue;
    }
}