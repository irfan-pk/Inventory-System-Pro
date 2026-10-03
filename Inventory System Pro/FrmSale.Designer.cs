namespace Inventory_System_Pro
{
    partial class FrmSales
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
            this.dtpSaleDate = new System.Windows.Forms.DateTimePicker();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scMaster = new System.Windows.Forms.SplitContainer();
            this.txtSONumber = new System.Windows.Forms.TextBox();
            this.labelSO = new System.Windows.Forms.Label();
            this.txtSaleNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.Label();
            this.dgvSaleDetails = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBalanceAmount = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoadSaleOrder = new System.Windows.Forms.Button();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).BeginInit();
            this.scMaster.Panel1.SuspendLayout();
            this.scMaster.Panel2.SuspendLayout();
            this.scMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpSaleDate
            // 
            this.dtpSaleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSaleDate.Location = new System.Drawing.Point(704, 17);
            this.dtpSaleDate.Name = "dtpSaleDate";
            this.dtpSaleDate.Size = new System.Drawing.Size(280, 24);
            this.dtpSaleDate.TabIndex = 2;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(704, 55);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(280, 26);
            this.cboWarehouse.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(268, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Net Amount";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.Location = new System.Drawing.Point(359, 54);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(133, 29);
            this.txtNetAmount.TabIndex = 16;
            this.txtNetAmount.Text = "0";
            this.txtNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(613, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Warehouse";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCustomer
            // 
            this.cboCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(225, 55);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(331, 26);
            this.cboCustomer.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(266, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tax Amount";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAmount.Location = new System.Drawing.Point(359, 16);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(133, 29);
            this.txtTaxAmount.TabIndex = 14;
            this.txtTaxAmount.Text = "0";
            this.txtTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.Location = new System.Drawing.Point(120, 54);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(133, 29);
            this.txtDiscountAmount.TabIndex = 12;
            this.txtDiscountAmount.Text = "0";
            this.txtDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(629, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "S.O Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(146, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Customer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(47, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Discount";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Gross Amount";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(225, 96);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(759, 24);
            this.txtRemarks.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Bell MT", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1168, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cash Sale Order";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.scMaster.Panel2.BackColor = System.Drawing.Color.Thistle;
            this.scMaster.Panel2.Controls.Add(this.txtSONumber);
            this.scMaster.Panel2.Controls.Add(this.labelSO);
            this.scMaster.Panel2.Controls.Add(this.txtSaleNumber);
            this.scMaster.Panel2.Controls.Add(this.label12);
            this.scMaster.Panel2.Controls.Add(this.txtRemarks);
            this.scMaster.Panel2.Controls.Add(this.label2);
            this.scMaster.Panel2.Controls.Add(this.dtpSaleDate);
            this.scMaster.Panel2.Controls.Add(this.label4);
            this.scMaster.Panel2.Controls.Add(this.cboWarehouse);
            this.scMaster.Panel2.Controls.Add(this.label6);
            this.scMaster.Panel2.Controls.Add(this.cboCustomer);
            this.scMaster.Panel2.Controls.Add(this.label5);
            this.scMaster.Size = new System.Drawing.Size(1170, 187);
            this.scMaster.SplitterDistance = 42;
            this.scMaster.TabIndex = 5;
            // 
            // txtSONumber
            // 
            this.txtSONumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSONumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSONumber.Location = new System.Drawing.Point(422, 17);
            this.txtSONumber.Name = "txtSONumber";
            this.txtSONumber.ReadOnly = true;
            this.txtSONumber.Size = new System.Drawing.Size(134, 24);
            this.txtSONumber.TabIndex = 1;
            // 
            // labelSO
            // 
            this.labelSO.AutoSize = true;
            this.labelSO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSO.Location = new System.Drawing.Point(373, 20);
            this.labelSO.Name = "labelSO";
            this.labelSO.Size = new System.Drawing.Size(46, 18);
            this.labelSO.TabIndex = 48;
            this.labelSO.Text = "S.O #";
            this.labelSO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleNumber
            // 
            this.txtSaleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleNumber.Location = new System.Drawing.Point(225, 17);
            this.txtSaleNumber.Name = "txtSaleNumber";
            this.txtSaleNumber.ReadOnly = true;
            this.txtSaleNumber.Size = new System.Drawing.Size(145, 24);
            this.txtSaleNumber.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(128, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 18);
            this.label12.TabIndex = 47;
            this.label12.Text = "Sale Number";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "Remarks";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProductSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductSearch.Location = new System.Drawing.Point(61, 198);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(155, 26);
            this.txtProductSearch.TabIndex = 0;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtGrossAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrossAmount.Location = new System.Drawing.Point(120, 16);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.Size = new System.Drawing.Size(133, 29);
            this.txtGrossAmount.TabIndex = 10;
            this.txtGrossAmount.Text = "0";
            this.txtGrossAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvSaleDetails
            // 
            this.dgvSaleDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSaleDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSaleDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSaleDetails.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvSaleDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvSaleDetails.Name = "dgvSaleDetails";
            this.dgvSaleDetails.Size = new System.Drawing.Size(1168, 399);
            this.dgvSaleDetails.TabIndex = 0;
            this.dgvSaleDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleDetails_CellContentClick);
            this.dgvSaleDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleDetails_CellValueChanged);
            this.dgvSaleDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSaleDetails_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 236);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSaleDetails);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer1.Panel2.Controls.Add(this.txtPaidAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtBalanceAmount);
            this.splitContainer1.Panel2.Controls.Add(this.btnClear);
            this.splitContainer1.Panel2.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Panel2.Controls.Add(this.btnNew);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.txtNetAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.txtTaxAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.txtDiscountAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.txtGrossAmount);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Size = new System.Drawing.Size(1170, 505);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 6;
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaidAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaidAmount.Location = new System.Drawing.Point(601, 18);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(133, 24);
            this.txtPaidAmount.TabIndex = 25;
            this.txtPaidAmount.Text = "0.00";
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(502, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 16);
            this.label13.TabIndex = 24;
            this.label13.Text = "Paid Amount";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(532, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Balance";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBalanceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceAmount.Location = new System.Drawing.Point(601, 54);
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.Size = new System.Drawing.Size(133, 29);
            this.txtBalanceAmount.TabIndex = 22;
            this.txtBalanceAmount.Text = "0";
            this.txtBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::Inventory_System_Pro.Properties.Resources.Seanau_Email_Clear_24;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(1036, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(124, 37);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "&Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::Inventory_System_Pro.Properties.Resources.printer_24px;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(906, 53);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(124, 37);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1036, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 37);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Flatastic_4_Add_item1;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(906, 10);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(124, 37);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnLoadSaleOrder
            // 
            this.btnLoadSaleOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadSaleOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSaleOrder.ForeColor = System.Drawing.Color.White;
            this.btnLoadSaleOrder.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Pretty_Office_12_Mailbox_message_received_21;
            this.btnLoadSaleOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadSaleOrder.Location = new System.Drawing.Point(1037, 194);
            this.btnLoadSaleOrder.Name = "btnLoadSaleOrder";
            this.btnLoadSaleOrder.Size = new System.Drawing.Size(124, 37);
            this.btnLoadSaleOrder.TabIndex = 3;
            this.btnLoadSaleOrder.Text = "&Load S.O";
            this.btnLoadSaleOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadSaleOrder.UseVisualStyleBackColor = true;
            this.btnLoadSaleOrder.Click += new System.EventHandler(this.btnLoadSaleOrder_Click);
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectProduct.ForeColor = System.Drawing.Color.White;
            this.btnSelectProduct.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Edit_24;
            this.btnSelectProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectProduct.Location = new System.Drawing.Point(226, 193);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(145, 37);
            this.btnSelectProduct.TabIndex = 1;
            this.btnSelectProduct.Text = "&Search Product";
            this.btnSelectProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectProduct.UseVisualStyleBackColor = false;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Inventory_System_Pro.Properties.Resources.Ampeross_Qetto_2_Search_24;
            this.label1.Location = new System.Drawing.Point(23, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 37);
            this.label1.TabIndex = 22;
            this.label1.Text = "      ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = global::Inventory_System_Pro.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(907, 194);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(124, 37);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1170, 741);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnLoadSaleOrder);
            this.Controls.Add(this.btnSelectProduct);
            this.Controls.Add(this.scMaster);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txtProductSearch);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Order";
            this.Load += new System.EventHandler(this.FrmSale_Load);
            this.scMaster.Panel1.ResumeLayout(false);
            this.scMaster.Panel2.ResumeLayout(false);
            this.scMaster.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).EndInit();
            this.scMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleDetails)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpSaleDate;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtNetAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtTaxAmount;
        private System.Windows.Forms.Label txtDiscountAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer scMaster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label txtGrossAmount;
        private System.Windows.Forms.DataGridView dgvSaleDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtBalanceAmount;
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.TextBox txtSaleNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnLoadSaleOrder;
        private System.Windows.Forms.TextBox txtSONumber;
        private System.Windows.Forms.Label labelSO;
        private System.Windows.Forms.Button btnSearch;
    }
}