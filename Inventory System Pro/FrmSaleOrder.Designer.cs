namespace Inventory_System_Pro
{
    partial class FrmSaleOrder
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
            this.dtpSODate = new System.Windows.Forms.DateTimePicker();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scMaster = new System.Windows.Forms.SplitContainer();
            this.btnLoadSaleOrder = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSONumber = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.Label();
            this.dgvSaleOrderDetails = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).BeginInit();
            this.scMaster.Panel1.SuspendLayout();
            this.scMaster.Panel2.SuspendLayout();
            this.scMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpSODate
            // 
            this.dtpSODate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSODate.Location = new System.Drawing.Point(636, 15);
            this.dtpSODate.Name = "dtpSODate";
            this.dtpSODate.Size = new System.Drawing.Size(280, 24);
            this.dtpSODate.TabIndex = 1;
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(636, 54);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(334, 26);
            this.cmbWarehouse.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(359, 57);
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
            this.txtNetAmount.Location = new System.Drawing.Point(452, 52);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(163, 29);
            this.txtNetAmount.TabIndex = 16;
            this.txtNetAmount.Text = "0";
            this.txtNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(545, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Warehouse";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(157, 55);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(331, 24);
            this.cmbCustomer.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(361, 17);
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
            this.txtTaxAmount.Location = new System.Drawing.Point(452, 12);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(163, 29);
            this.txtTaxAmount.TabIndex = 14;
            this.txtTaxAmount.Text = "0";
            this.txtTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.Location = new System.Drawing.Point(157, 52);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(163, 29);
            this.txtDiscountAmount.TabIndex = 12;
            this.txtDiscountAmount.Text = "0";
            this.txtDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(561, 17);
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
            this.label5.Location = new System.Drawing.Point(77, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Customer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(84, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Discount";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "S.O Number";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(48, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "Gross Amount";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(157, 92);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(813, 24);
            this.txtRemarks.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Bell MT", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1168, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sale Order";
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
            this.scMaster.Panel2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.scMaster.Panel2.Controls.Add(this.btnLoadSaleOrder);
            this.scMaster.Panel2.Controls.Add(this.btnSearch);
            this.scMaster.Panel2.Controls.Add(this.txtRemarks);
            this.scMaster.Panel2.Controls.Add(this.label2);
            this.scMaster.Panel2.Controls.Add(this.dtpSODate);
            this.scMaster.Panel2.Controls.Add(this.label4);
            this.scMaster.Panel2.Controls.Add(this.cmbWarehouse);
            this.scMaster.Panel2.Controls.Add(this.label6);
            this.scMaster.Panel2.Controls.Add(this.cmbCustomer);
            this.scMaster.Panel2.Controls.Add(this.label5);
            this.scMaster.Panel2.Controls.Add(this.txtSONumber);
            this.scMaster.Panel2.Controls.Add(this.label1);
            this.scMaster.Panel2.ForeColor = System.Drawing.Color.White;
            this.scMaster.Size = new System.Drawing.Size(1170, 187);
            this.scMaster.SplitterDistance = 42;
            this.scMaster.TabIndex = 5;
            // 
            // btnLoadSaleOrder
            // 
            this.btnLoadSaleOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadSaleOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSaleOrder.Image = global::Inventory_System_Pro.Properties.Resources.Double_J_Design_Super_Mono_3d_Load_download_24;
            this.btnLoadSaleOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadSaleOrder.Location = new System.Drawing.Point(1052, 82);
            this.btnLoadSaleOrder.Name = "btnLoadSaleOrder";
            this.btnLoadSaleOrder.Size = new System.Drawing.Size(108, 37);
            this.btnLoadSaleOrder.TabIndex = 6;
            this.btnLoadSaleOrder.Text = "&Load S.O";
            this.btnLoadSaleOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadSaleOrder.UseVisualStyleBackColor = true;
            this.btnLoadSaleOrder.Click += new System.EventHandler(this.btnLoadSaleOrder_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::Inventory_System_Pro.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(1052, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(108, 37);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "Remarks";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSONumber
            // 
            this.txtSONumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSONumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSONumber.Location = new System.Drawing.Point(157, 19);
            this.txtSONumber.Name = "txtSONumber";
            this.txtSONumber.ReadOnly = true;
            this.txtSONumber.Size = new System.Drawing.Size(155, 24);
            this.txtSONumber.TabIndex = 0;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtGrossAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrossAmount.Location = new System.Drawing.Point(157, 12);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.Size = new System.Drawing.Size(163, 29);
            this.txtGrossAmount.TabIndex = 10;
            this.txtGrossAmount.Text = "0";
            this.txtGrossAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSaleOrderDetails
            // 
            this.dgvSaleOrderDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSaleOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSaleOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSaleOrderDetails.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvSaleOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvSaleOrderDetails.MultiSelect = false;
            this.dgvSaleOrderDetails.Name = "dgvSaleOrderDetails";
            this.dgvSaleOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaleOrderDetails.Size = new System.Drawing.Size(1168, 442);
            this.dgvSaleOrderDetails.TabIndex = 0;
            this.dgvSaleOrderDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleOrderDetails_CellContentClick);
            this.dgvSaleOrderDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellDoubleClick);
            this.dgvSaleOrderDetails.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleOrderDetails_CellEnter);
            this.dgvSaleOrderDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleOrderDetails_CellValueChanged);
            this.dgvSaleOrderDetails.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSaleOrderDetails_CurrentCellDirtyStateChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 193);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSaleOrderDetails);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnApprove);
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
            this.splitContainer1.Size = new System.Drawing.Size(1170, 548);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_File_Delete_24;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1052, 53);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 37);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.Image = global::Inventory_System_Pro.Properties.Resources.Designcontest_Ecommerce_Business_Approve_24;
            this.btnApprove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApprove.Location = new System.Drawing.Point(938, 53);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(108, 37);
            this.btnApprove.TabIndex = 2;
            this.btnApprove.Text = "&Approve";
            this.btnApprove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as1;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1052, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 37);
            this.btnSave.TabIndex = 1;
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
            this.btnNew.Location = new System.Drawing.Point(938, 10);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(108, 37);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // FrmSaleOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 741);
            this.Controls.Add(this.scMaster);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSaleOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale Order";
            this.Load += new System.EventHandler(this.FrmSaleOrder_Load);
            this.scMaster.Panel1.ResumeLayout(false);
            this.scMaster.Panel2.ResumeLayout(false);
            this.scMaster.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMaster)).EndInit();
            this.scMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderDetails)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpSODate;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtNetAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCustomer;
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
        private System.Windows.Forms.TextBox txtSONumber;
        private System.Windows.Forms.Label txtGrossAmount;
        private System.Windows.Forms.DataGridView dgvSaleOrderDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoadSaleOrder;
    }
}