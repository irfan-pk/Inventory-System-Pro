using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmUnits : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private int _unitID;

        public FrmUnits()
        {
            InitializeComponent();
        }

        private void FrmUnits_Load(object sender, EventArgs e)
        {
            ClearUnitFields();
            LoadUnits();
        }

        private void LoadUnits()
        {
            try
            {
                var table = new DataTable();

                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand("dbo.sp_Unit_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ActiveOnly", SqlDbType.Bit)
                        .Value = false;

                    using (var adapter = new SqlDataAdapter(command))
                        adapter.Fill(table);
                }

                dgvUnits.DataSource = table;

                if (dgvUnits.Columns.Contains("UnitID"))
                {
                    dgvUnits.Columns["UnitID"].Visible = true;
                    DataGridViewColumn col = dgvUnits.Columns["UnitID"];
                    col.HeaderText = "Unit ID";
                    col.Width = 100;
                    col.DefaultCellStyle .Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvUnits.Columns.Contains("UnitName"))
                {
                    DataGridViewColumn col = dgvUnits.Columns["UnitName"];
                    col.HeaderText = "Unit Name";
                    col.Width = 220;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvUnits.Columns.Contains("ShortName"))
                {
                    DataGridViewColumn col = dgvUnits.Columns["ShortName"];
                    col.HeaderText = "Short Name";
                    col.Width = 520;
                }

                if (dgvUnits.Columns.Contains("IsActive"))
                {
                    DataGridViewColumn col = dgvUnits.Columns["IsActive"];
                    col.HeaderText = "Active";
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                dgvUnits.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void dgvUnits_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedUnit();
        }

        private void LoadSelectedUnit()
        {
            if (dgvUnits.CurrentRow == null)
                return;

            var row = dgvUnits.CurrentRow.DataBoundItem as DataRowView;
            if (row == null)
                return;

            _unitID = Convert.ToInt32(row["UnitID"]);
            txtUnitName.Text = Convert.ToString(row["UnitName"]);

            txtShortName.Text = row["ShortName"] == DBNull.Value
                ? string.Empty
                : Convert.ToString(row["ShortName"]);

            chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);
            btnDeactivate.Enabled = chkIsActive.Checked;
        }

        private bool ValidateUnit()
        {
            string unitName = txtUnitName.Text.Trim();
            string shortName = txtShortName.Text.Trim();

            if (unitName.Length == 0)
            {
                MessageBox.Show(
                    "Enter a unit name.",
                    "Unit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitName.Focus();
                return false;
            }

            if (unitName.Length > 50)
            {
                MessageBox.Show(
                    "Unit name cannot exceed 50 characters.",
                    "Unit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitName.Focus();
                return false;
            }

            if (shortName.Length > 20)
            {
                MessageBox.Show(
                    "Short name cannot exceed 20 characters.",
                    "Unit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtShortName.Focus();
                return false;
            }

            return true;
        }

        private void SaveUnit()
        {
            if (!ValidateUnit())
                return;

            try
            {
                // Determine whether to call the insert or update stored procedure.
                string procedure = _unitID == 0
                    ? "dbo.sp_Unit_Save"
                    : "dbo.sp_Unit_Update";
                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UnitID", SqlDbType.Int).Value =
                        _unitID == 0 ? (object)DBNull.Value : _unitID;

                    command.Parameters.Add(
                        "@UnitName",
                        SqlDbType.NVarChar,
                        50).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtUnitName.Text.Trim());

                    command.Parameters.Add(
                        "@ShortName",
                        SqlDbType.NVarChar,
                        20).Value =
                        string.IsNullOrWhiteSpace(txtShortName.Text)
                            ? (object)DBNull.Value
                            : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtShortName.Text.Trim());

                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                        chkIsActive.Checked;

                    connection.Open();

                    // The procedure returns UnitID for insert and update.
                    _unitID = Convert.ToInt32(command.ExecuteScalar());
                }

                int savedUnitID = _unitID;
                LoadUnits();
                SelectUnitRow(savedUnitID);

                MessageBox.Show(
                    "Unit saved.",
                    "Unit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex) when (
                ex.Number == 2601 ||
                ex.Number == 2627 ||
                ex.Number == 50021)
            {
                MessageBox.Show(
                    "That unit name already exists.",
                    "Unit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void DeactivateUnit()
        {
            if (_unitID == 0)
                return;

            if (MessageBox.Show(
                    "Deactivate this unit?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand("dbo.sp_Unit_SetActive", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UnitID", SqlDbType.Int)
                        .Value = _unitID;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit)
                        .Value = false;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                LoadUnits();
                ClearUnitFields();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void SelectUnitRow(int unitID)
        {
            foreach (DataGridViewRow row in dgvUnits.Rows)
            {
                if (Convert.ToInt32(row.Cells["UnitID"].Value) == unitID)
                {
                    dgvUnits.CurrentCell = row.Cells["UnitName"];
                    row.Selected = true;
                    LoadSelectedUnit();
                    return;
                }
            }
        }

        private void ClearUnitFields()
        {
            dgvUnits.ClearSelection();
            _unitID = 0;
            txtUnitName.Clear();
            txtShortName.Clear();
            chkIsActive.Checked = false;
            btnDeactivate.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("UNIT.NEW"))
            {
                MessageBox.Show("You do not have permission to create units.");
                return;
            }
            ClearUnitFields();
            txtUnitName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("UNIT.SAVE"))
            {
                MessageBox.Show("You do not have permission to save units.");
                return;
            }
            SaveUnit();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("UNIT.DELETE"))
            {
                MessageBox.Show("You do not have permission to deactivate units.");
                return;
            }
            DeactivateUnit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUnits();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Unit Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}