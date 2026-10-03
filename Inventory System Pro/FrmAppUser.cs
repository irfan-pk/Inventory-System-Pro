using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmAppUser : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private int _userID;

        public FrmAppUser()
        {
            InitializeComponent();
        }

        private void FrmAppUser_Load(object sender, EventArgs e)
        {
            //if (!)


            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            ClearUserFields();
            LoadUsers();

            // Load roles into the CheckedListBox
            bool canAssignRoles =
                AppSession.HasPermission("APP_USER.ASSIGN_ROLE");
        }

        private void LoadUsers()
        {
            try
            {
                var table = new DataTable();

                using (var cn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.sp_AppUser_List", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ActiveOnly", SqlDbType.Bit)
                        .Value = false;

                    using (var adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(table);
                }

                dgvAppUsers.DataSource = table;
                FormatGridColumns();

                if (dgvAppUsers.Columns.Contains("UserID"))
                    dgvAppUsers.Columns["UserID"].Visible = false;

                // Never include PasswordHash in the list procedure.
                dgvAppUsers.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void dgvAppUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppUsers.CurrentRow == null)
                return;

            var row = dgvAppUsers.CurrentRow.DataBoundItem as DataRowView;
            if (row == null)
                return;

            _userID = Convert.ToInt32(row["UserID"]);
            txtUserName.Text = Convert.ToString(row["UserName"]);
            txtLoginName.Text = Convert.ToString(row["LoginName"]);
            chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);

            // Password fields stay blank while editing.
            txtPassword.Clear();
            txtConfirmPassword.Clear();

            btnResetPassword.Enabled = true;
            btnDeactivate.Enabled = chkIsActive.Checked;
        }

        private bool ValidateUserDetails()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Enter the user's name.");
                txtUserName.Focus();
                return false;
            }

            if (txtUserName.Text.Trim().Length > 100)
            {
                MessageBox.Show("User name cannot exceed 100 characters.");
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLoginName.Text))
            {
                MessageBox.Show("Enter a login name.");
                txtLoginName.Focus();
                return false;
            }

            if (txtLoginName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Login name cannot exceed 100 characters.");
                txtLoginName.Focus();
                return false;
            }

            return true;
        }

        private bool ValidatePasswordEntry()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Enter a password.");
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("The passwords do not match.");
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return false;
            }

            return true;
        }

        private void SaveUser()
        {
            if (!ValidateUserDetails())
                return;

            bool isNewUser = _userID == 0;

            if (isNewUser && !ValidatePasswordEntry())
                return;

            string procedure = isNewUser
                ? "dbo.sp_AppUser_Insert"
                : "dbo.sp_AppUser_Update";

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(procedure, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (isNewUser)
                    {
                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100)
                            .Value = txtUserName.Text.Trim();

                        cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 100)
                            .Value = txtLoginName.Text.Trim();

                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 500)
                            .Value = PasswordHasher.Hash(txtPassword.Text);

                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit)
                            .Value = chkIsActive.Checked;
                    }
                    else
                    {
                        cmd.Parameters.Add("@UserID", SqlDbType.Int)
                            .Value = _userID;

                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100)
                            .Value = txtUserName.Text.Trim();

                        cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 100)
                            .Value = txtLoginName.Text.Trim();

                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit)
                            .Value = chkIsActive.Checked;
                    }

                    cn.Open();
                    _userID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                int savedUserID = _userID;
                LoadUsers();
                SelectUserRow(savedUserID);

                MessageBox.Show("User saved.");
            }
            catch (SqlException ex) when (
                ex.Number == 2601 ||
                ex.Number == 2627)
            {
                MessageBox.Show(
                    "That login name is already in use.",
                    "User",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void ResetUserPassword()
        {
            if (_userID == 0)
            {
                MessageBox.Show("Select a user first.");
                return;
            }

            if (!ValidatePasswordEntry())
                return;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                using (var cmd =
                    new SqlCommand("dbo.sp_AppUser_ChangePassword", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int)
                        .Value = _userID;

                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 500)
                        .Value = PasswordHasher.Hash(txtPassword.Text);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtPassword.Clear();
                txtConfirmPassword.Clear();

                MessageBox.Show("Password reset.");
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void DeactivateUser()
        {
            if (_userID == 0)
                return;

            if (_userID == AppSession.UserID)
            {
                MessageBox.Show("You cannot deactivate the account currently signed in.");
                return;
            }

            if (MessageBox.Show(
                    "Deactivate this user?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                using (var cmd =
                    new SqlCommand("dbo.sp_AppUser_SetActive", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = _userID;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadUsers();
                ClearUserFields();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void SelectUserRow(int userID)
        {
            foreach (DataGridViewRow row in dgvAppUsers.Rows)
            {
                if (Convert.ToInt32(row.Cells["UserID"].Value) == userID)
                {
                    dgvAppUsers.CurrentCell = row.Cells["LoginName"];
                    row.Selected = true;
                    return;
                }
            }
        }

        private void ClearUserFields()
        {
            dgvAppUsers.ClearSelection();
            _userID = 0;
            txtUserName.Clear();
            txtLoginName.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            chkIsActive.Checked = true;

            btnResetPassword.Enabled = false;
            btnDeactivate.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_USER.CREATE"))
            {
                MessageBox.Show("You do not have permission to create new users.");
                return;
            }
            ClearUserFields();
            txtUserName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("APP_USER.APPROVE"))
            {
                MessageBox.Show("You do not have permission to save users.");
                return;
            }
            SaveUser();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_USER.EDIT"))
            {
                MessageBox.Show("You do not have permission to edit users.");
                return;
            }
            ResetUserPassword();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_USER.DELETE"))
            {
                MessageBox.Show("You do not have permission to delete users.");
                return;
            }
            DeactivateUser();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "User Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void FormatGridColumns()
        {
            dgvAppUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppUsers.MultiSelect = false;
            dgvAppUsers.ReadOnly = true;
            dgvAppUsers.AllowUserToAddRows = false;
            dgvAppUsers.AllowUserToDeleteRows = false;
            dgvAppUsers.RowHeadersVisible = false;

            if (dgvAppUsers.Columns.Contains("UserID"))
                dgvAppUsers.Columns["UserID"].Visible = false;

            if (dgvAppUsers.Columns.Contains("UserName"))
            {
                dgvAppUsers.Columns["UserName"].HeaderText = "User Name";
                dgvAppUsers.Columns["UserName"].FillWeight = 25;
            }

            if (dgvAppUsers.Columns.Contains("LoginName"))
            {
                dgvAppUsers.Columns["LoginName"].HeaderText = "Login Name";
                dgvAppUsers.Columns["LoginName"].FillWeight = 25;
            }

            if (dgvAppUsers.Columns.Contains("IsActive"))
            {
                dgvAppUsers.Columns["IsActive"].HeaderText = "Active";
                dgvAppUsers.Columns["IsActive"].FillWeight = 12;
                dgvAppUsers.Columns["IsActive"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvAppUsers.Columns.Contains("CreatedDate"))
            {
                dgvAppUsers.Columns["CreatedDate"].HeaderText = "Created Date";
                dgvAppUsers.Columns["CreatedDate"].DefaultCellStyle.Format =
                    "yyyy-MM-dd HH:mm";
                dgvAppUsers.Columns["CreatedDate"].FillWeight = 25;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}