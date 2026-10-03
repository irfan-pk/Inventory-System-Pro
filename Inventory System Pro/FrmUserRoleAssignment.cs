using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmUserRoleAssignment : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private bool _loading;

        public FrmUserRoleAssignment()
        {
            InitializeComponent();
        }

        private sealed class RoleItem
        {
            public int RoleID { get; set; }
            public string RoleName { get; set; }

            public override string ToString()
            {
                return RoleName;
            }
        }

        private void FrmUserRoleAssignment_Load(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_USER.ASSIGN_ROLE"))
            {
                MessageBox.Show("You do not have permission to assign roles.");
                return;
            }

            try
            {
                LoadRoles();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load role assignments: " + ex.Message);
            }
        }

        private void LoadUsers()
        {
            _loading = true;

            var table = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.sp_AppUser_List", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ActiveOnly", SqlDbType.Bit).Value = true;
                adapter.Fill(table);
            }

            table.Columns.Add("DisplayText", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                row["DisplayText"] =
                    row["LoginName"] + " - " + row["UserName"];
            }

            cboUser.DataSource = null;
            cboUser.DisplayMember = "DisplayText";
            cboUser.ValueMember = "UserID";
            cboUser.DataSource = table;

            _loading = false;

            if (cboUser.Items.Count > 0)
                LoadSelectedUserRoles();
            else
                ClearRoleChecks();
        }

        private void LoadRoles()
        {
            clbRoles.Items.Clear();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(
                "dbo.sp_AppRole_ListActive", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clbRoles.Items.Add(new RoleItem
                        {
                            RoleID = Convert.ToInt32(reader["RoleID"]),
                            RoleName = reader["RoleName"].ToString()
                        });
                    }
                }
            }
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            LoadSelectedUserRoles();
        }

        private int? GetSelectedUserID()
        {
            if (cboUser.SelectedValue == null)
                return null;

            if (int.TryParse(
                Convert.ToString(cboUser.SelectedValue), out int userID))
            {
                return userID;
            }

            return null;
        }

        private void LoadSelectedUserRoles()
        {
            int? userID = GetSelectedUserID();

            if (userID == null)
            {
                ClearRoleChecks();
                return;
            }

            var assignedRoleIDs = new HashSet<int>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(
                "dbo.sp_AppUser_GetRoles", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID.Value;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assignedRoleIDs.Add(
                            Convert.ToInt32(reader["RoleID"]));
                    }
                }
            }

            for (int i = 0; i < clbRoles.Items.Count; i++)
            {
                var role = (RoleItem)clbRoles.Items[i];
                clbRoles.SetItemChecked(
                    i, assignedRoleIDs.Contains(role.RoleID));
            }
        }

        private void ClearRoleChecks()
        {
            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, false);
        }

        private void btnSaveRoles_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("APP_USER.ASSIGN_ROLE"))
            {
                MessageBox.Show("You do not have permission to assign roles.");
                return;
            }

            int? userID = GetSelectedUserID();

            if (userID == null)
            {
                MessageBox.Show("Select a user first.");
                return;
            }

            if (clbRoles.CheckedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show(
                    "No roles are selected. Saving will remove this user's roles. Continue?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            var roleIDs = new DataTable();
            roleIDs.Columns.Add("RoleID", typeof(int));

            foreach (var item in clbRoles.CheckedItems)
            {
                var role = (RoleItem)item;
                roleIDs.Rows.Add(role.RoleID);
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(
                    "dbo.sp_AppUser_SaveRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value =
                        userID.Value;
                    command.Parameters.Add("@AssignedBy", SqlDbType.Int).Value =
                        AppSession.UserID;

                    var rolesParameter = command.Parameters.Add(
                        "@RoleIDs", SqlDbType.Structured);

                    rolesParameter.TypeName = "dbo.RoleIDList";
                    rolesParameter.Value = roleIDs;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Roles saved for the selected user.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save roles: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRoles();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}