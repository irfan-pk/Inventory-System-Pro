namespace Inventory_System_Pro
{
    partial class FrmPurchaseReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearchSale = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPurchaseNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReturnNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scMaster = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtReturnQty = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.Label();
            this.dgvPurchaseReturnDetails = new System.Windows.Forms.DataGridView();
            this.PurchaseDetailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchasedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviouslyReturnedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableReturnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtRefundAmount = new System.Windows.Forms.TextBox();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtGrossAmount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).BeginInit();
            this.scMaster.Panel1.SuspendLayout();
            this.scMaster.Panel2.SuspendLayout();
            this.scMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReturnDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearchSale
            // 
            this.txtSearchSale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchSale.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchSale.Location = new System.Drawing.Point(227, 10);
            this.txtSearchSale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchSale.Name = "txtSearchSale";
            this.txtSearchSale.Size = new System.Drawing.Size(279, 26);
            this.txtSearchSale.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 18);
            this.label5.TabIndex = 53;
            this.label5.Text = "Search Purchase";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPurchaseNumber
            // 
            this.txtPurchaseNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurchaseNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseNumber.Location = new System.Drawing.Point(227, 85);
            this.txtPurchaseNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPurchaseNumber.Name = "txtPurchaseNumber";
            this.txtPurchaseNumber.Size = new System.Drawing.Size(279, 24);
            this.txtPurchaseNumber.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 18);
            this.label1.TabIndex = 49;
            this.label1.Text = "Purchase Number";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReturnNumber
            // 
            this.txtReturnNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReturnNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnNumber.Location = new System.Drawing.Point(227, 48);
            this.txtReturnNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReturnNumber.Name = "txtReturnNumber";
            this.txtReturnNumber.ReadOnly = true;
            this.txtReturnNumber.Size = new System.Drawing.Size(279, 24);
            this.txtReturnNumber.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(47, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 18);
            this.label12.TabIndex = 47;
            this.label12.Text = "Purchase Return #";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(693, 85);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(503, 24);
            this.txtRemarks.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(593, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "Remarks";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnDate.Location = new System.Drawing.Point(693, 10);
            this.dtpReturnDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(351, 24);
            this.dtpReturnDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(544, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "Purchase Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(227, 124);
            this.cboWarehouse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(280, 24);
            this.cboWarehouse.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(108, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Warehouse";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scMaster
            // 
            this.scMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.scMaster.Location = new System.Drawing.Point(0, 0);
            this.scMaster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scMaster.Name = "scMaster";
            this.scMaster.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMaster.Panel1
            // 
            this.scMaster.Panel1.Controls.Add(this.label3);
            // 
            // scMaster.Panel2
            // 
            this.scMaster.Panel2.BackColor = System.Drawing.Color.Bisque;
            this.scMaster.Panel2.Controls.Add(this.cboSupplier);
            this.scMaster.Panel2.Controls.Add(this.label14);
            this.scMaster.Panel2.Controls.Add(this.txtSearchSale);
            this.scMaster.Panel2.Controls.Add(this.label5);
            this.scMaster.Panel2.Controls.Add(this.txtPurchaseNumber);
            this.scMaster.Panel2.Controls.Add(this.label1);
            this.scMaster.Panel2.Controls.Add(this.txtReturnNumber);
            this.scMaster.Panel2.Controls.Add(this.label12);
            this.scMaster.Panel2.Controls.Add(this.txtRemarks);
            this.scMaster.Panel2.Controls.Add(this.label2);
            this.scMaster.Panel2.Controls.Add(this.dtpReturnDate);
            this.scMaster.Panel2.Controls.Add(this.label4);
            this.scMaster.Panel2.Controls.Add(this.cboWarehouse);
            this.scMaster.Panel2.Controls.Add(this.label6);
            this.scMaster.Size = new System.Drawing.Size(1268, 233);
            this.scMaster.SplitterDistance = 59;
            this.scMaster.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Brown;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Bell MT", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1266, 57);
            this.label3.TabIndex = 0;
            this.label3.Text = "Purchase Return Order";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSupplier
            // 
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(693, 48);
            this.cboSupplier.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(351, 24);
            this.cboSupplier.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(604, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 18);
            this.label14.TabIndex = 55;
            this.label14.Text = "Supplier";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReturnQty
            // 
            this.txtReturnQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnQty.ForeColor = System.Drawing.Color.DarkRed;
            this.txtReturnQty.Location = new System.Drawing.Point(676, 17);
            this.txtReturnQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReturnQty.Name = "txtReturnQty";
            this.txtReturnQty.Size = new System.Drawing.Size(133, 24);
            this.txtReturnQty.TabIndex = 25;
            this.txtReturnQty.Text = "0.00";
            this.txtReturnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(587, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 18);
            this.label13.TabIndex = 24;
            this.label13.Text = "Return Qty.";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(615, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 18);
            this.label7.TabIndex = 23;
            this.label7.Text = "Refund";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(325, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "Net Amount";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.Location = new System.Drawing.Point(417, 57);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(133, 30);
            this.txtNetAmount.TabIndex = 16;
            this.txtNetAmount.Text = "0";
            this.txtNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(324, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tax Amount";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAmount.Location = new System.Drawing.Point(417, 14);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(133, 30);
            this.txtTaxAmount.TabIndex = 14;
            this.txtTaxAmount.Text = "0";
            this.txtTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(82, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Discount";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.Location = new System.Drawing.Point(155, 57);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(133, 30);
            this.txtDiscountAmount.TabIndex = 12;
            this.txtDiscountAmount.Text = "0";
            this.txtDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPurchaseReturnDetails
            // 
            this.dgvPurchaseReturnDetails.AllowUserToAddRows = false;
            this.dgvPurchaseReturnDetails.AllowUserToDeleteRows = false;
            this.dgvPurchaseReturnDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPurchaseReturnDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchaseReturnDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseReturnDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PurchaseDetailID,
            this.ProductID,
            this.ProductCode,
            this.ProductName,
            this.PurchasedQty,
            this.PreviouslyReturnedQty,
            this.AvailableReturnQty,
            this.ReturnQty,
            this.UnitPrice,
            this.DiscountAmount,
            this.TaxAmount,
            this.NetAmount});
            this.dgvPurchaseReturnDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseReturnDetails.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvPurchaseReturnDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvPurchaseReturnDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPurchaseReturnDetails.Name = "dgvPurchaseReturnDetails";
            this.dgvPurchaseReturnDetails.RowHeadersVisible = false;
            this.dgvPurchaseReturnDetails.Size = new System.Drawing.Size(1266, 396);
            this.dgvPurchaseReturnDetails.TabIndex = 0;
            this.dgvPurchaseReturnDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseReturnDetails_CellContentClick);
            this.dgvPurchaseReturnDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseReturnDetails_CellEndEdit);
            this.dgvPurchaseReturnDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseReturnDetails_CellValueChanged);
            // 
            // PurchaseDetailID
            // 
            this.PurchaseDetailID.HeaderText = "ID";
            this.PurchaseDetailID.Name = "PurchaseDetailID";
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "Prod. ID";
            this.ProductID.Name = "ProductID";
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "Code";
            this.ProductCode.Name = "ProductCode";
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "Name";
            this.ProductName.Name = "ProductName";
            // 
            // PurchasedQty
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N3";
            dataGridViewCellStyle1.NullValue = "0.000";
            this.PurchasedQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.PurchasedQty.HeaderText = "Qty.";
            this.PurchasedQty.Name = "PurchasedQty";
            // 
            // PreviouslyReturnedQty
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = "0.000";
            this.PreviouslyReturnedQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.PreviouslyReturnedQty.HeaderText = "Prv. Ret. Qty";
            this.PreviouslyReturnedQty.Name = "PreviouslyReturnedQty";
            this.PreviouslyReturnedQty.Width = 105;
            // 
            // AvailableReturnQty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = "0.000";
            this.AvailableReturnQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.AvailableReturnQty.HeaderText = "Avail. Ret Qty";
            this.AvailableReturnQty.Name = "AvailableReturnQty";
            this.AvailableReturnQty.Width = 105;
            // 
            // ReturnQty
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.ReturnQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.ReturnQty.HeaderText = "Return";
            this.ReturnQty.Name = "ReturnQty";
            // 
            // UnitPrice
            // 
            this.UnitPrice.HeaderText = "Price";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.Width = 105;
            // 
            // DiscountAmount
            // 
            this.DiscountAmount.HeaderText = "Discount";
            this.DiscountAmount.Name = "DiscountAmount";
            // 
            // TaxAmount
            // 
            this.TaxAmount.HeaderText = "Tax";
            this.TaxAmount.Name = "TaxAmount";
            // 
            // NetAmount
            // 
            this.NetAmount.HeaderText = "Net";
            this.NetAmount.Name = "NetAmount";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(44, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "Gross Amount";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 238);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvPurchaseReturnDetails);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightCoral;
            this.splitContainer1.Panel2.Controls.Add(this.txtRefundAmount);
            this.splitContainer1.Panel2.Controls.Add(this.btnExportPDF);
            this.splitContainer1.Panel2.Controls.Add(this.txtReturnQty);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.btnSearch);
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
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Size = new System.Drawing.Size(1268, 503);
            this.splitContainer1.SplitterDistance = 398;
            this.splitContainer1.TabIndex = 27;
            // 
            // txtRefundAmount
            // 
            this.txtRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRefundAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefundAmount.ForeColor = System.Drawing.Color.DarkRed;
            this.txtRefundAmount.Location = new System.Drawing.Point(676, 60);
            this.txtRefundAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRefundAmount.Name = "txtRefundAmount";
            this.txtRefundAmount.Size = new System.Drawing.Size(133, 24);
            this.txtRefundAmount.TabIndex = 27;
            this.txtRefundAmount.Text = "0.00";
            this.txtRefundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Office_Export_24;
            this.btnExportPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportPDF.Location = new System.Drawing.Point(889, 55);
            this.btnExportPDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(117, 37);
            this.btnExportPDF.TabIndex = 2;
            this.btnExportPDF.Text = "&Export";
            this.btnExportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_Document_Preview_24;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(1137, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(117, 37);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearchPurchase_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::Inventory_System_Pro.Properties.Resources.print_24px_vector;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(1137, 54);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(117, 37);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1013, 54);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Flatastic_4_Add_item1;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(1013, 11);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(117, 37);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtGrossAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrossAmount.Location = new System.Drawing.Point(155, 14);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.Size = new System.Drawing.Size(133, 30);
            this.txtGrossAmount.TabIndex = 10;
            this.txtGrossAmount.Text = "0";
            this.txtGrossAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmPurchaseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 741);
            this.Controls.Add(this.scMaster);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmPurchaseReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Return";
            this.Load += new System.EventHandler(this.FrmPurchaseReturn_Load);
            this.scMaster.Panel1.ResumeLayout(false);
            this.scMaster.Panel2.ResumeLayout(false);
            this.scMaster.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).EndInit();
            this.scMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReturnDetails)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchSale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPurchaseNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReturnNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.SplitContainer scMaster;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReturnQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtNetAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtTaxAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtDiscountAmount;
        private System.Windows.Forms.DataGridView dgvPurchaseReturnDetails;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label txtGrossAmount;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRefundAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseDetailID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchasedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreviouslyReturnedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableReturnQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
    }
}