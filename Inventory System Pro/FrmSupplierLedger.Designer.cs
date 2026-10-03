namespace Inventory_System_Pro
{
    partial class FrmSupplierLedger
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
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnPrintStatement = new System.Windows.Forms.Button();
            this.lblStatementPeriod = new System.Windows.Forms.Label();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvSupplierLedger = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvPurchaseDetails = new System.Windows.Forms.DataGridView();
            this.txtDetailNet = new System.Windows.Forms.Label();
            this.txtDetailGross = new System.Windows.Forms.Label();
            this.txtDetailDiscount = new System.Windows.Forms.Label();
            this.txtDetailTax = new System.Windows.Forms.Label();
            this.txtDetailQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtClosingBalance = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtTotalDebit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotalCredit = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.splitContainer1.Panel1.Controls.Add(this.btnExportPDF);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrintStatement);
            this.splitContainer1.Panel1.Controls.Add(this.lblStatementPeriod);
            this.splitContainer1.Panel1.Controls.Add(this.lblSupplierName);
            this.splitContainer1.Panel1.Controls.Add(this.dtpToDate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFromDate);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cboSupplier);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.White;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSupplierLedger);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 359);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Office_Export_24;
            this.btnExportPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportPDF.Location = new System.Drawing.Point(1055, 49);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(102, 39);
            this.btnExportPDF.TabIndex = 4;
            this.btnExportPDF.Text = "&Export";
            this.btnExportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnPrintStatement
            // 
            this.btnPrintStatement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintStatement.Image = global::Inventory_System_Pro.Properties.Resources.print_24px_vector;
            this.btnPrintStatement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintStatement.Location = new System.Drawing.Point(947, 49);
            this.btnPrintStatement.Name = "btnPrintStatement";
            this.btnPrintStatement.Size = new System.Drawing.Size(102, 39);
            this.btnPrintStatement.TabIndex = 3;
            this.btnPrintStatement.Text = "&Preview";
            this.btnPrintStatement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintStatement.UseVisualStyleBackColor = true;
            this.btnPrintStatement.Click += new System.EventHandler(this.btnPrintStatement_Click);
            // 
            // lblStatementPeriod
            // 
            this.lblStatementPeriod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatementPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatementPeriod.Location = new System.Drawing.Point(809, 12);
            this.lblStatementPeriod.Name = "lblStatementPeriod";
            this.lblStatementPeriod.Size = new System.Drawing.Size(348, 29);
            this.lblStatementPeriod.TabIndex = 77;
            this.lblStatementPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierName.Location = new System.Drawing.Point(396, 13);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(396, 29);
            this.lblSupplierName.TabIndex = 76;
            this.lblSupplierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(512, 54);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(280, 24);
            this.dtpToDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(445, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 74;
            this.label1.Text = "To Date";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(97, 54);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(280, 24);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 72;
            this.label4.Text = "From Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSupplier
            // 
            this.cboSupplier.DisplayMember = "ProductCode";
            this.cboSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(97, 13);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(280, 26);
            this.cboSupplier.TabIndex = 0;
            this.cboSupplier.ValueMember = "ProductId";
            this.cboSupplier.SelectedIndexChanged += new System.EventHandler(this.cboSupplier_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 71;
            this.label5.Text = "Supplier";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvSupplierLedger
            // 
            this.dgvSupplierLedger.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dgvSupplierLedger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSupplierLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupplierLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSupplierLedger.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvSupplierLedger.Location = new System.Drawing.Point(0, 0);
            this.dgvSupplierLedger.Name = "dgvSupplierLedger";
            this.dgvSupplierLedger.Size = new System.Drawing.Size(1168, 256);
            this.dgvSupplierLedger.TabIndex = 3;
            this.dgvSupplierLedger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSupplierLedger_CellDoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer2.Location = new System.Drawing.Point(0, 391);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvPurchaseDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Lavender;
            this.splitContainer2.Panel2.Controls.Add(this.txtDetailNet);
            this.splitContainer2.Panel2.Controls.Add(this.txtDetailGross);
            this.splitContainer2.Panel2.Controls.Add(this.txtDetailDiscount);
            this.splitContainer2.Panel2.Controls.Add(this.txtDetailTax);
            this.splitContainer2.Panel2.Controls.Add(this.txtDetailQty);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.txtClosingBalance);
            this.splitContainer2.Panel2.Controls.Add(this.btnClear);
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Panel2.Controls.Add(this.btnLoad);
            this.splitContainer2.Panel2.Controls.Add(this.txtTotalDebit);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.txtTotalCredit);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.txtOpeningBalance);
            this.splitContainer2.Size = new System.Drawing.Size(1170, 350);
            this.splitContainer2.SplitterDistance = 245;
            this.splitContainer2.TabIndex = 53;
            // 
            // dgvPurchaseDetails
            // 
            this.dgvPurchaseDetails.AllowUserToAddRows = false;
            this.dgvPurchaseDetails.AllowUserToDeleteRows = false;
            this.dgvPurchaseDetails.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgvPurchaseDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchaseDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseDetails.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvPurchaseDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvPurchaseDetails.Name = "dgvPurchaseDetails";
            this.dgvPurchaseDetails.ReadOnly = true;
            this.dgvPurchaseDetails.Size = new System.Drawing.Size(1168, 243);
            this.dgvPurchaseDetails.TabIndex = 0;
            // 
            // txtDetailNet
            // 
            this.txtDetailNet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailNet.Location = new System.Drawing.Point(826, 16);
            this.txtDetailNet.Name = "txtDetailNet";
            this.txtDetailNet.Size = new System.Drawing.Size(140, 29);
            this.txtDetailNet.TabIndex = 58;
            this.txtDetailNet.Text = "0";
            this.txtDetailNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailGross
            // 
            this.txtDetailGross.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailGross.Location = new System.Drawing.Point(534, 56);
            this.txtDetailGross.Name = "txtDetailGross";
            this.txtDetailGross.Size = new System.Drawing.Size(140, 29);
            this.txtDetailGross.TabIndex = 57;
            this.txtDetailGross.Text = "0";
            this.txtDetailGross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailDiscount
            // 
            this.txtDetailDiscount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailDiscount.Location = new System.Drawing.Point(680, 15);
            this.txtDetailDiscount.Name = "txtDetailDiscount";
            this.txtDetailDiscount.Size = new System.Drawing.Size(140, 29);
            this.txtDetailDiscount.TabIndex = 55;
            this.txtDetailDiscount.Text = "0";
            this.txtDetailDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailTax
            // 
            this.txtDetailTax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailTax.Location = new System.Drawing.Point(680, 57);
            this.txtDetailTax.Name = "txtDetailTax";
            this.txtDetailTax.Size = new System.Drawing.Size(140, 29);
            this.txtDetailTax.TabIndex = 53;
            this.txtDetailTax.Text = "0";
            this.txtDetailTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailQty
            // 
            this.txtDetailQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailQty.Location = new System.Drawing.Point(534, 16);
            this.txtDetailQty.Name = "txtDetailQty";
            this.txtDetailQty.Size = new System.Drawing.Size(140, 29);
            this.txtDetailQty.TabIndex = 51;
            this.txtDetailQty.Text = "0";
            this.txtDetailQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(31, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 18);
            this.label8.TabIndex = 25;
            this.label8.Text = "Closing Bal.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClosingBalance
            // 
            this.txtClosingBalance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtClosingBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClosingBalance.Location = new System.Drawing.Point(124, 55);
            this.txtClosingBalance.Name = "txtClosingBalance";
            this.txtClosingBalance.Size = new System.Drawing.Size(140, 29);
            this.txtClosingBalance.TabIndex = 24;
            this.txtClosingBalance.Text = "0";
            this.txtClosingBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::Inventory_System_Pro.Properties.Resources.Seanau_Email_Clear_24;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(1055, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 37);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(281, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "Total Debit";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLoad
            // 
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Pretty_Office_12_Mailbox_message_received_2_24;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(1055, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(102, 37);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "&Load";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTotalDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDebit.Location = new System.Drawing.Point(366, 15);
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.Size = new System.Drawing.Size(140, 29);
            this.txtTotalDebit.TabIndex = 22;
            this.txtTotalDebit.Text = "0";
            this.txtTotalDebit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(276, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 18);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total Credit";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTotalCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCredit.Location = new System.Drawing.Point(366, 57);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.Size = new System.Drawing.Size(140, 29);
            this.txtTotalCredit.TabIndex = 20;
            this.txtTotalCredit.Text = "0";
            this.txtTotalCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(26, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 18);
            this.label11.TabIndex = 19;
            this.label11.Text = "Opening Bal.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtOpeningBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningBalance.Location = new System.Drawing.Point(124, 15);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(140, 29);
            this.txtOpeningBalance.TabIndex = 18;
            this.txtOpeningBalance.Text = "0";
            this.txtOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDetailTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 32);
            this.panel1.TabIndex = 54;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblDetailTitle.Location = new System.Drawing.Point(4, 4);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(168, 24);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Transaction Details";
            this.lblDetailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSupplierLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1170, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitContainer2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSupplierLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Ledger";
            this.Load += new System.EventHandler(this.FrmSupplierLedger_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierLedger)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvPurchaseDetails;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtClosingBalance;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label txtTotalDebit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtTotalCredit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtOpeningBalance;
        private System.Windows.Forms.Label lblStatementPeriod;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvSupplierLedger;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label txtDetailNet;
        private System.Windows.Forms.Label txtDetailGross;
        private System.Windows.Forms.Label txtDetailDiscount;
        private System.Windows.Forms.Label txtDetailTax;
        private System.Windows.Forms.Label txtDetailQty;
        private System.Windows.Forms.Button btnPrintStatement;
        private System.Windows.Forms.Button btnExportPDF;
    }
}