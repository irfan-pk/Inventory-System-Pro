using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmSupplier : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private int _supplierID;

        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            ClearSupplierFields();
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            try
            {
                var table = new DataTable();

                using (var cn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.sp_Supplier_List", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ActiveOnly", SqlDbType.Bit).Value = false;

                    using (var adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(table);
                }

                dgvSuppliers.DataSource = table;

                if (dgvSuppliers.Columns.Contains("SupplierID"))
                    dgvSuppliers.Columns["SupplierID"].Visible = false;

                if (dgvSuppliers.Columns.Contains("OpeningBalance"))
                    dgvSuppliers.Columns["OpeningBalance"]
                        .DefaultCellStyle.Format = "N2";

                dgvSuppliers.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
                return;

            var row = dgvSuppliers.CurrentRow.DataBoundItem as DataRowView;
            if (row == null)
                return;

            _supplierID = Convert.ToInt32(row["SupplierID"]);
            txtSupplierCode.Text = Convert.ToString(row["SupplierCode"]);
            txtSupplierName.Text = Convert.ToString(row["SupplierName"]);
            txtContactPerson.Text = ReadOptionalText(row["ContactPerson"]);
            txtPhone.Text = ReadOptionalText(row["Phone"]);
            txtAddress.Text = ReadOptionalText(row["Address"]);
            txtNTN.Text = ReadOptionalText(row["NTN"]);
            //nudOpeningBalance.Value = Convert.ToDecimal(row["OpeningBalance"]);
            chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);
            btnDeactivate.Enabled = chkIsActive.Checked;
        }

        private static string ReadOptionalText(object value)
        {
            return value == DBNull.Value ? string.Empty : Convert.ToString(value);
        }

        private bool ValidateSupplier()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierCode.Text))
            {
                MessageBox.Show("Enter a supplier code.");
                txtSupplierCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Enter a supplier name.");
                txtSupplierName.Focus();
                return false;
            }

            return txtSupplierCode.Text.Trim().Length <= 50
                && txtSupplierName.Text.Trim().Length <= 200
                && txtContactPerson.Text.Trim().Length <= 150
                && txtPhone.Text.Trim().Length <= 50
                && txtAddress.Text.Trim().Length <= 300
                && txtNTN.Text.Trim().Length <= 50;
        }

        private void SaveSupplier()
        {
            if (!ValidateSupplier())
                return;

            try
            {
                // determine which stored procedure to call based on whether we're adding or updating
                string procedure = _supplierID == 0
                    ? "dbo.sp_Supplier_Save"
                    : "dbo.sp_Supplier_Update";
                using (var cn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(procedure, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value =
                        _supplierID == 0 ? (object)DBNull.Value : _supplierID;

                    cmd.Parameters.Add("@SupplierCode", SqlDbType.NVarChar, 50)
                        .Value = txtSupplierCode.Text.Trim();

                    cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 200)
                        .Value = txtSupplierName.Text.Trim();

                    AddOptionalText(cmd, "@ContactPerson", 150, txtContactPerson.Text);
                    AddOptionalText(cmd, "@Phone", 50, txtPhone.Text);
                    AddOptionalText(cmd, "@Address", 300, txtAddress.Text);
                    AddOptionalText(cmd, "@NTN", 50, txtNTN.Text);

                    var opening = cmd.Parameters.Add(
                        "@OpeningBalance", SqlDbType.Decimal);
                    opening.Precision = 18;
                    opening.Scale = 2;
                    opening.Value = 0;

                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit)
                        .Value = chkIsActive.Checked;

                    cn.Open();
                    _supplierID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                int savedSupplierID = _supplierID;
                LoadSuppliers();
                SelectSupplierRow(savedSupplierID);

                MessageBox.Show("Supplier saved.");
            }
            catch (SqlException ex) when (
                ex.Number == 2601 ||
                ex.Number == 2627 ||
                ex.Number == 51002)
            {
                MessageBox.Show(ex.Message, "Supplier");
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private static void AddOptionalText(
            SqlCommand cmd, string name, int size, string value)
        {
            cmd.Parameters.Add(name, SqlDbType.NVarChar, size).Value =
                string.IsNullOrWhiteSpace(value)
                    ? (object)DBNull.Value
                    : value.Trim();
        }

        private void DeactivateSupplier()
        {
            if (_supplierID == 0)
                return;

            if (MessageBox.Show(
                    "Deactivate this supplier?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var cn = new SqlConnection(_connectionString))
                using (var cmd =
                    new SqlCommand("dbo.sp_Supplier_SetActive", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value =
                        _supplierID;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadSuppliers();
                ClearSupplierFields();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void SelectSupplierRow(int supplierID)
        {
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (Convert.ToInt32(row.Cells["SupplierID"].Value) == supplierID)
                {
                    dgvSuppliers.CurrentCell = row.Cells["SupplierCode"];
                    row.Selected = true;
                    return;
                }
            }
        }

        private void ClearSupplierFields()
        {
            _supplierID = 0;
            txtSupplierCode.Clear();
            txtSupplierName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtNTN.Clear();
            //nudOpeningBalance.Value = 0;
            chkIsActive.Checked = true;
            btnDeactivate.Enabled = false;
            dgvSuppliers.ClearSelection();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SUPPLIERS.NEW"))
            {
                MessageBox.Show("You do not have permission to create new suppliers.");
                return;
            }
            ClearSupplierFields();
            txtSupplierCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SUPPLIERS.SAVE"))
            {
                MessageBox.Show("You do not have permission to save suppliers.");
                return;
            }
            SaveSupplier();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SUPPLIERS.DELETE"))
            {
                MessageBox.Show("You do not have permission to deactivate suppliers.");
                return;
            }
            DeactivateSupplier();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Supplier Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void scMaster_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}