using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmRolesPermission : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private bool _loading;

        public FrmRolesPermission()
        {
            InitializeComponent();
        }
        
        private void FrmRolePermission_Load(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_ROLE.ASSIGN_PERMISSION"))
            {
                MessageBox.Show("You do not have permission to manage role permissions.");
                //Close();
                return;
            }

            try
            {
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load roles: " + ex.Message);
            }
        }

        private void LoadRoles()
        {
            _loading = true;

            var roles = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.sp_AppRole_ListActive", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                adapter.Fill(roles);
            }

            cboRole.DataSource = null;
            cboRole.DisplayMember = "RoleName";
            cboRole.ValueMember = "RoleID";
            cboRole.DataSource = roles;

            _loading = false;

            if (cboRole.Items.Count > 0)
                LoadPermissionsForSelectedRole();
        }

        private int? GetSelectedRoleID()
        {
            if (cboRole.SelectedValue == null)
                return null;

            if (int.TryParse(Convert.ToString(cboRole.SelectedValue), out int roleID))
                return roleID;

            return null;
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
                LoadPermissionsForSelectedRole();
        }

        private void LoadPermissionsForSelectedRole()
        {
            int? roleID = GetSelectedRoleID();

            if (roleID == null)
            {
                dgvPermissions.DataSource = null;
                return;
            }

            var permissions = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(
                "dbo.sp_AppRolePermission_List", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID.Value;
                adapter.Fill(permissions);
            }

            dgvPermissions.DataSource = permissions;
            FormatPermissionGrid();
        }

        private void FormatPermissionGrid()
        {
            dgvPermissions.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvPermissions.AllowUserToAddRows = false;
            dgvPermissions.AllowUserToDeleteRows = false;
            dgvPermissions.RowHeadersVisible = false;
            dgvPermissions.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPermissions.MultiSelect = false;

            if (dgvPermissions.Columns.Contains("PermissionID"))
                dgvPermissions.Columns["PermissionID"].Visible = false;

            if (dgvPermissions.Columns.Contains("PermissionKey"))
            {
                dgvPermissions.Columns["PermissionKey"].HeaderText = "Permission Key";
                dgvPermissions.Columns["PermissionKey"].ReadOnly = true;
                dgvPermissions.Columns["PermissionKey"].FillWeight = 25;
            }

            if (dgvPermissions.Columns.Contains("ModuleName"))
            {
                dgvPermissions.Columns["ModuleName"].HeaderText = "Module";
                dgvPermissions.Columns["ModuleName"].ReadOnly = true;
                dgvPermissions.Columns["ModuleName"].FillWeight = 18;
            }

            if (dgvPermissions.Columns.Contains("ActionName"))
            {
                dgvPermissions.Columns["ActionName"].HeaderText = "Action";
                dgvPermissions.Columns["ActionName"].ReadOnly = true;
                dgvPermissions.Columns["ActionName"].FillWeight = 15;
            }

            if (dgvPermissions.Columns.Contains("DisplayName"))
            {
                dgvPermissions.Columns["DisplayName"].HeaderText = "Permission";
                dgvPermissions.Columns["DisplayName"].ReadOnly = true;
                dgvPermissions.Columns["DisplayName"].FillWeight = 30;
            }

            if (dgvPermissions.Columns.Contains("IsAllowed"))
            {
                dgvPermissions.Columns["IsAllowed"].HeaderText = "Allowed";
                dgvPermissions.Columns["IsAllowed"].ReadOnly = false;
                dgvPermissions.Columns["IsAllowed"].FillWeight = 12;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_ROLE.ASSIGN_PERMISSION"))
            {
                MessageBox.Show("You do not have permission to manage role permissions.");
                return;
            }

            int? roleID = GetSelectedRoleID();

            if (roleID == null)
            {
                MessageBox.Show("Select a role first.");
                return;
            }

            dgvPermissions.EndEdit();

            var permissionIDs = new DataTable();
            permissionIDs.Columns.Add("PermissionID", typeof(int));

            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                if (row.IsNewRow)
                    continue;

                bool isAllowed =
                    row.Cells["IsAllowed"].Value != DBNull.Value &&
                    Convert.ToBoolean(row.Cells["IsAllowed"].Value);

                if (isAllowed)
                {
                    permissionIDs.Rows.Add(
                        Convert.ToInt32(row.Cells["PermissionID"].Value));
                }
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(
                    "dbo.sp_AppRolePermission_Save", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value =
                        roleID.Value;

                    var permissionsParameter = command.Parameters.Add(
                        "@PermissionIDs", SqlDbType.Structured);

                    permissionsParameter.TypeName = "dbo.PermissionIDList";
                    permissionsParameter.Value = permissionIDs;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Role permissions saved.");
                LoadPermissionsForSelectedRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save permissions: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh roles: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
