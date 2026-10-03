using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmCategory : Form
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private int _categoryID;

        public FrmCategory()
        {
            InitializeComponent();
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            ClearCategoryFields();
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var table = new DataTable();

                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand("dbo.sp_Category_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ActiveOnly", SqlDbType.Bit)
                        .Value = false;

                    using (var adapter = new SqlDataAdapter(command))
                        adapter.Fill(table);
                }

                dgvCategories.DataSource = table;

                if (dgvCategories.Columns.Contains("CategoryID"))
                {
                    DataGridViewColumn col = dgvCategories.Columns["CategoryID"];
                    col.Visible = true;
                    col.HeaderText = "Category ID";
                    col.Width  = 150;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvCategories.Columns.Contains("CategoryHead"))
                {
                    DataGridViewColumn col = dgvCategories.Columns["CategoryHead"];
                    col.HeaderText = "Category Head";
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvCategories.Columns.Contains("CategoryName"))
                {
                    DataGridViewColumn col = dgvCategories.Columns["CategoryName"];
                    col.HeaderText = "Category Name";
                    col.AutoSizeMode  = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvCategories.Columns.Contains("IsActive"))
                {
                    DataGridViewColumn col = dgvCategories.Columns["IsActive"];
                    col.HeaderText = "Active";
                    col.HeaderCell.Style.Alignment  = DataGridViewContentAlignment.MiddleCenter;
                }

                dgvCategories.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedCategory();
        }

        private void LoadSelectedCategory()
        {
            if (dgvCategories.CurrentRow == null)
                return;

            var row = dgvCategories.CurrentRow.DataBoundItem as DataRowView;
            if (row == null)
                return;

            _categoryID = Convert.ToInt32(row["CategoryID"]);
            txtCategoryHead.Text = Convert.ToString(row["CategoryHead"]);
            txtCategoryName.Text = Convert.ToString(row["CategoryName"]);
            chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);
            btnDeactivate.Enabled = chkIsActive.Checked;
        }

        private bool ValidateCategory()
        {
            string categoryName = txtCategoryName.Text.Trim();
            string categoryHead = txtCategoryHead.Text.Trim();

            if (categoryHead.Length == 0)
            {
                MessageBox.Show(
                    "Enter a category head.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryHead.Focus();
                return false;
            }

            if (categoryHead.Length > 100)
            {
                MessageBox.Show(
                    "Category head cannot exceed 100 characters.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return false;
            }

            if (categoryName.Length == 0)
            {
                MessageBox.Show(
                    "Enter a category name.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return false;
            }

            if (categoryName.Length > 250)
            {
                MessageBox.Show(
                    "Category name cannot exceed 100 characters.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        private void SaveCategory()
        {
            if (!ValidateCategory())
                return;

            try
            {
                // determine whether to call the insert or update stored procedure
                string procedure = _categoryID == 0
                    ? "dbo.sp_Category_Save"
                    : "dbo.sp_Category_Update";
                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                        _categoryID == 0
                            ? (object)DBNull.Value
                            : _categoryID;

                    command.Parameters.Add(
                        "@CategoryHead",
                        SqlDbType.NVarChar,
                        250).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCategoryHead.Text.Trim());

                    command.Parameters.Add(
                        "@CategoryName",
                        SqlDbType.NVarChar,
                        250).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCategoryName.Text.Trim());

                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                        chkIsActive.Checked;

                    connection.Open();

                    // The procedure returns CategoryID for insert and update.
                    _categoryID = Convert.ToInt32(command.ExecuteScalar());
                }

                LoadCategories();
                SelectCategoryRow(_categoryID);

                MessageBox.Show(
                    "Category saved.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                MessageBox.Show(
                    "That category name already exists.",
                    "Category",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void DeactivateCategory()
        {
            if (_categoryID == 0)
                return;

            if (MessageBox.Show(
                    "Deactivate this category?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command =
                    new SqlCommand("dbo.sp_Category_SetActive", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CategoryID", SqlDbType.Int)
                        .Value = _categoryID;

                    command.Parameters.Add("@IsActive", SqlDbType.Bit)
                        .Value = false;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                LoadCategories();
                ClearCategoryFields();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void SelectCategoryRow(int categoryID)
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (Convert.ToInt32(row.Cells["CategoryID"].Value) == categoryID)
                {
                    dgvCategories.CurrentCell = row.Cells["CategoryName"];
                    row.Selected = true;
                    LoadSelectedCategory();
                    return;
                }
            }
        }

        private void ClearCategoryFields()
        {
            dgvCategories.ClearSelection();
            _categoryID = 0;
            chkIsActive.Checked = false;
            btnDeactivate.Enabled = false;
            txtCategoryHead.Clear();
            txtCategoryName.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CATEGORY.NEW"))
            {
                MessageBox.Show("You do not have permission to create categories.");
                return;
            }
            ClearCategoryFields();
            txtCategoryHead.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CATEGORY.SAVE"))
            {
                MessageBox.Show("You do not have permission to save categories.");
                return;
            }
            SaveCategory();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CATEGORY.DELETE"))
            {
                MessageBox.Show("You do not have permission to deactivate categories.");
                return;
            }
            DeactivateCategory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Category Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}