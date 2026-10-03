namespace Inventory_System_Pro
{
    partial class FrmRolesPermission
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
            this.scUserRoles = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSaveRoles = new System.Windows.Forms.Button();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPermissions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.scUserRoles)).BeginInit();
            this.scUserRoles.Panel1.SuspendLayout();
            this.scUserRoles.Panel2.SuspendLayout();
            this.scUserRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).BeginInit();
            this.SuspendLayout();
            // 
            // scUserRoles
            // 
            this.scUserRoles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scUserRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scUserRoles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scUserRoles.Location = new System.Drawing.Point(0, 0);
            this.scUserRoles.Margin = new System.Windows.Forms.Padding(4);
            this.scUserRoles.Name = "scUserRoles";
            // 
            // scUserRoles.Panel1
            // 
            this.scUserRoles.Panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.scUserRoles.Panel1.Controls.Add(this.btnClose);
            this.scUserRoles.Panel1.Controls.Add(this.btnRefresh);
            this.scUserRoles.Panel1.Controls.Add(this.btnSaveRoles);
            this.scUserRoles.Panel1.Controls.Add(this.cboRole);
            this.scUserRoles.Panel1.Controls.Add(this.label1);
            this.scUserRoles.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // scUserRoles.Panel2
            // 
            this.scUserRoles.Panel2.Controls.Add(this.dgvPermissions);
            this.scUserRoles.Size = new System.Drawing.Size(1136, 606);
            this.scUserRoles.SplitterDistance = 300;
            this.scUserRoles.SplitterWidth = 5;
            this.scUserRoles.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Inventory_System_Pro.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_window_close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(26, 553);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(250, 38);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Image = global::Inventory_System_Pro.Properties.Resources.Pixelkit_Flat_Jewels_Refresh_24;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(26, 509);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(250, 38);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSaveRoles
            // 
            this.btnSaveRoles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveRoles.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as_24;
            this.btnSaveRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRoles.Location = new System.Drawing.Point(26, 465);
            this.btnSaveRoles.Name = "btnSaveRoles";
            this.btnSaveRoles.Size = new System.Drawing.Size(250, 38);
            this.btnSaveRoles.TabIndex = 1;
            this.btnSaveRoles.Text = "&Save Permissions";
            this.btnSaveRoles.UseVisualStyleBackColor = true;
            this.btnSaveRoles.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(26, 47);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(250, 26);
            this.cboRole.TabIndex = 0;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select User";
            // 
            // dgvPermissions
            // 
            this.dgvPermissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPermissions.Location = new System.Drawing.Point(0, 0);
            this.dgvPermissions.Name = "dgvPermissions";
            this.dgvPermissions.Size = new System.Drawing.Size(827, 602);
            this.dgvPermissions.TabIndex = 0;
            // 
            // FrmRolesPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 606);
            this.Controls.Add(this.scUserRoles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRolesPermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roles Permission";
            this.Load += new System.EventHandler(this.FrmRolePermission_Load);
            this.scUserRoles.Panel1.ResumeLayout(false);
            this.scUserRoles.Panel1.PerformLayout();
            this.scUserRoles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scUserRoles)).EndInit();
            this.scUserRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scUserRoles;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSaveRoles;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPermissions;
    }
}