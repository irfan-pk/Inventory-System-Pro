namespace Inventory_System_Pro
{
    partial class FrmUserRoleAssignment
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
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.scUserRoles)).BeginInit();
            this.scUserRoles.Panel1.SuspendLayout();
            this.scUserRoles.Panel2.SuspendLayout();
            this.scUserRoles.SuspendLayout();
            this.SuspendLayout();
            // 
            // scUserRoles
            // 
            this.scUserRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scUserRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scUserRoles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scUserRoles.Location = new System.Drawing.Point(0, 0);
            this.scUserRoles.Margin = new System.Windows.Forms.Padding(4);
            this.scUserRoles.Name = "scUserRoles";
            // 
            // scUserRoles.Panel1
            // 
            this.scUserRoles.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.scUserRoles.Panel1.Controls.Add(this.btnClose);
            this.scUserRoles.Panel1.Controls.Add(this.btnRefresh);
            this.scUserRoles.Panel1.Controls.Add(this.btnSaveRoles);
            this.scUserRoles.Panel1.Controls.Add(this.cboUser);
            this.scUserRoles.Panel1.Controls.Add(this.label1);
            this.scUserRoles.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // scUserRoles.Panel2
            // 
            this.scUserRoles.Panel2.Controls.Add(this.clbRoles);
            this.scUserRoles.Size = new System.Drawing.Size(789, 376);
            this.scUserRoles.SplitterDistance = 300;
            this.scUserRoles.SplitterWidth = 5;
            this.scUserRoles.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Inventory_System_Pro.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_window_close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(26, 313);
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
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::Inventory_System_Pro.Properties.Resources.Pixelkit_Flat_Jewels_Refresh_24;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(26, 269);
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
            this.btnSaveRoles.ForeColor = System.Drawing.Color.White;
            this.btnSaveRoles.Image = global::Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as1;
            this.btnSaveRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRoles.Location = new System.Drawing.Point(26, 225);
            this.btnSaveRoles.Name = "btnSaveRoles";
            this.btnSaveRoles.Size = new System.Drawing.Size(250, 38);
            this.btnSaveRoles.TabIndex = 1;
            this.btnSaveRoles.Text = "&Save Roles";
            this.btnSaveRoles.UseVisualStyleBackColor = true;
            this.btnSaveRoles.Click += new System.EventHandler(this.btnSaveRoles_Click);
            // 
            // cboUser
            // 
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(26, 47);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(250, 28);
            this.cboUser.TabIndex = 0;
            this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select User";
            // 
            // clbRoles
            // 
            this.clbRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(0, 0);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(482, 374);
            this.clbRoles.TabIndex = 0;
            // 
            // FrmUserRoleAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 376);
            this.Controls.Add(this.scUserRoles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmUserRoleAssignment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Role Assignment";
            this.Load += new System.EventHandler(this.FrmUserRoleAssignment_Load);
            this.scUserRoles.Panel1.ResumeLayout(false);
            this.scUserRoles.Panel1.PerformLayout();
            this.scUserRoles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scUserRoles)).EndInit();
            this.scUserRoles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scUserRoles;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSaveRoles;
    }
}