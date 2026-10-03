namespace Inventory_System_Pro
{
    partial class FrmCustomerLedger
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
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvCustomerPaymentDetails = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnPrintStatement = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
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
            this.dgvCustomerLedger = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblStatementPeriod = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPaymentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailTitle.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblDetailTitle.Location = new System.Drawing.Point(3, 3);
            this.lblDetailTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(143, 24);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Payment Details";
            this.lblDetailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDetailTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 328);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1143, 29);
            this.panel1.TabIndex = 57;
            // 
            // dgvCustomerPaymentDetails
            // 
            this.dgvCustomerPaymentDetails.AllowUserToAddRows = false;
            this.dgvCustomerPaymentDetails.AllowUserToDeleteRows = false;
            this.dgvCustomerPaymentDetails.BackgroundColor = System.Drawing.Color.PaleTurquoise;
            this.dgvCustomerPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomerPaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerPaymentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomerPaymentDetails.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvCustomerPaymentDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomerPaymentDetails.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCustomerPaymentDetails.Name = "dgvCustomerPaymentDetails";
            this.dgvCustomerPaymentDetails.ReadOnly = true;
            this.dgvCustomerPaymentDetails.Size = new System.Drawing.Size(1141, 221);
            this.dgvCustomerPaymentDetails.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer2.Location = new System.Drawing.Point(0, 357);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvCustomerPaymentDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnPrintStatement);
            this.splitContainer2.Panel2.Controls.Add(this.btnExportPDF);
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
            this.splitContainer2.Size = new System.Drawing.Size(1143, 320);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 56;
            // 
            // btnPrintStatement
            // 
            this.btnPrintStatement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintStatement.Image = global::Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_Document_Preview_24;
            this.btnPrintStatement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintStatement.Location = new System.Drawing.Point(1018, 10);
            this.btnPrintStatement.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintStatement.Name = "btnPrintStatement";
            this.btnPrintStatement.Size = new System.Drawing.Size(114, 34);
            this.btnPrintStatement.TabIndex = 1;
            this.btnPrintStatement.Text = "&Preview";
            this.btnPrintStatement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintStatement.UseVisualStyleBackColor = true;
            this.btnPrintStatement.Click += new System.EventHandler(this.btnPrintStatement_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Office_Export_24;
            this.btnExportPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportPDF.Location = new System.Drawing.Point(1018, 48);
            this.btnExportPDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(114, 34);
            this.btnExportPDF.TabIndex = 3;
            this.btnExportPDF.Text = "&Export";
            this.btnExportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // txtDetailNet
            // 
            this.txtDetailNet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailNet.Location = new System.Drawing.Point(742, 15);
            this.txtDetailNet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDetailNet.Name = "txtDetailNet";
            this.txtDetailNet.Size = new System.Drawing.Size(118, 27);
            this.txtDetailNet.TabIndex = 58;
            this.txtDetailNet.Text = "0";
            this.txtDetailNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailGross
            // 
            this.txtDetailGross.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailGross.Location = new System.Drawing.Point(495, 52);
            this.txtDetailGross.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDetailGross.Name = "txtDetailGross";
            this.txtDetailGross.Size = new System.Drawing.Size(118, 27);
            this.txtDetailGross.TabIndex = 57;
            this.txtDetailGross.Text = "0";
            this.txtDetailGross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailDiscount
            // 
            this.txtDetailDiscount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailDiscount.Location = new System.Drawing.Point(619, 14);
            this.txtDetailDiscount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDetailDiscount.Name = "txtDetailDiscount";
            this.txtDetailDiscount.Size = new System.Drawing.Size(118, 27);
            this.txtDetailDiscount.TabIndex = 55;
            this.txtDetailDiscount.Text = "0";
            this.txtDetailDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailTax
            // 
            this.txtDetailTax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailTax.Location = new System.Drawing.Point(619, 52);
            this.txtDetailTax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDetailTax.Name = "txtDetailTax";
            this.txtDetailTax.Size = new System.Drawing.Size(118, 27);
            this.txtDetailTax.TabIndex = 53;
            this.txtDetailTax.Text = "0";
            this.txtDetailTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailQty
            // 
            this.txtDetailQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDetailQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetailQty.Location = new System.Drawing.Point(495, 15);
            this.txtDetailQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDetailQty.Name = "txtDetailQty";
            this.txtDetailQty.Size = new System.Drawing.Size(118, 27);
            this.txtDetailQty.TabIndex = 51;
            this.txtDetailQty.Text = "0";
            this.txtDetailQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(26, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.txtClosingBalance.Location = new System.Drawing.Point(126, 51);
            this.txtClosingBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtClosingBalance.Name = "txtClosingBalance";
            this.txtClosingBalance.Size = new System.Drawing.Size(118, 27);
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
            this.btnClear.Location = new System.Drawing.Point(900, 48);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(114, 34);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(259, 18);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.btnLoad.Image = global::Inventory_System_Pro.Properties.Resources.Double_J_Design_Super_Mono_3d_Load_download_24;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(900, 10);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(114, 34);
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
            this.txtTotalDebit.Location = new System.Drawing.Point(353, 14);
            this.txtTotalDebit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.Size = new System.Drawing.Size(118, 27);
            this.txtTotalDebit.TabIndex = 22;
            this.txtTotalDebit.Text = "0";
            this.txtTotalDebit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(254, 56);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.txtTotalCredit.Location = new System.Drawing.Point(353, 52);
            this.txtTotalCredit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.Size = new System.Drawing.Size(118, 27);
            this.txtTotalCredit.TabIndex = 20;
            this.txtTotalCredit.Text = "0";
            this.txtTotalCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 18);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.txtOpeningBalance.Location = new System.Drawing.Point(126, 14);
            this.txtOpeningBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(118, 27);
            this.txtOpeningBalance.TabIndex = 18;
            this.txtOpeningBalance.Text = "0";
            this.txtOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCustomerLedger
            // 
            this.dgvCustomerLedger.BackgroundColor = System.Drawing.Color.MintCream;
            this.dgvCustomerLedger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomerLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomerLedger.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvCustomerLedger.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomerLedger.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCustomerLedger.Name = "dgvCustomerLedger";
            this.dgvCustomerLedger.Size = new System.Drawing.Size(1141, 237);
            this.dgvCustomerLedger.TabIndex = 3;
            this.dgvCustomerLedger.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomerLedger_CellDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 72;
            this.label4.Text = "From Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCustomer
            // 
            this.cboCustomer.DisplayMember = "ProductCode";
            this.cboCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(106, 12);
            this.cboCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(237, 26);
            this.cboCustomer.TabIndex = 0;
            this.cboCustomer.ValueMember = "ProductId";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(15, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 71;
            this.label5.Text = "Customer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.splitContainer1.Panel1.Controls.Add(this.lblStatementPeriod);
            this.splitContainer1.Panel1.Controls.Add(this.lblCustomerName);
            this.splitContainer1.Panel1.Controls.Add(this.dtpToDate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFromDate);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cboCustomer);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCustomerLedger);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 328);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 55;
            // 
            // lblStatementPeriod
            // 
            this.lblStatementPeriod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatementPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatementPeriod.Location = new System.Drawing.Point(706, 11);
            this.lblStatementPeriod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatementPeriod.Name = "lblStatementPeriod";
            this.lblStatementPeriod.Size = new System.Drawing.Size(294, 27);
            this.lblStatementPeriod.TabIndex = 77;
            this.lblStatementPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(358, 12);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(334, 27);
            this.lblCustomerName.TabIndex = 76;
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(472, 48);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(293, 24);
            this.dtpToDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(399, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 74;
            this.label1.Text = "To Date";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(106, 48);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(289, 24);
            this.dtpFromDate.TabIndex = 1;
            // 
            // FrmCustomerLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 677);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "FrmCustomerLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Ledger";
            this.Load += new System.EventHandler(this.FrmCustomerLedger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPaymentDetails)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerLedger)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvCustomerPaymentDetails;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label txtDetailNet;
        private System.Windows.Forms.Label txtDetailGross;
        private System.Windows.Forms.Label txtDetailDiscount;
        private System.Windows.Forms.Label txtDetailTax;
        private System.Windows.Forms.Label txtDetailQty;
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
        private System.Windows.Forms.DataGridView dgvCustomerLedger;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnPrintStatement;
        private System.Windows.Forms.Label lblStatementPeriod;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
    }
}