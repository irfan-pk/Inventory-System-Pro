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
    public partial class FrmCustomer : Form 
    {
        private int _selectedCustomerID = 0;
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void LoadCustomers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                    SELECT
                        CustomerID,
                        CustomerCode,
                        CustomerName,
                        ContactPerson,
                        Phone,
                        Address,
                        NTN,
                        IsActive
                    FROM dbo.Customers
                    ORDER BY CustomerName;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                dgvCustomers.DataSource = dt;

                FormatCustomersGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatCustomersGrid()
        {
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.RowHeadersVisible = false;

            // Prevent automatic resizing from overriding our widths
            dgvCustomers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            // -----------------------------------------
            // Hidden columns
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("CustomerID"))
            {
                dgvCustomers.Columns["CustomerID"].Visible = false;
            }

            // -----------------------------------------
            // Customer Code
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("CustomerCode"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["CustomerCode"];

                col.HeaderText = "Code";
                col.Width = 120;
                col.MinimumWidth = 80;
            }

            // -----------------------------------------
            // Customer Name
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("CustomerName"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["CustomerName"];

                col.HeaderText = "Customer";
                col.Width = 220;
                col.MinimumWidth = 150;
            }

            // -----------------------------------------
            // Contact Person
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("ContactPerson"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["ContactPerson"];

                col.HeaderText = "Contact";
                col.Width = 160;
                col.MinimumWidth = 120;
            }

            // -----------------------------------------
            // Phone
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("Phone"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["Phone"];

                col.HeaderText = "Phone";
                col.Width = 130;
                col.MinimumWidth = 100;
            }

            // -----------------------------------------
            // Address
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("Address"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["Address"];

                col.HeaderText = "Address";
                col.Width = 250;
                col.MinimumWidth = 150;
            }

            // -----------------------------------------
            // NTN
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("NTN"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["NTN"];

                col.HeaderText = "NTN";
                col.Width = 120;
                col.MinimumWidth = 100;
            }

            // -----------------------------------------
            // Active
            // -----------------------------------------

            if (dgvCustomers.Columns.Contains("IsActive"))
            {
                DataGridViewColumn col =
                    dgvCustomers.Columns["IsActive"];

                col.HeaderText = "Active";
                col.Width = 70;
                col.MinimumWidth = 60;

                col.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            // -----------------------------------------
            // General formatting
            // -----------------------------------------

            dgvCustomers.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvCustomers.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvCustomers.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvCustomers.RowTemplate.Height = 28;

            dgvCustomers.Columns["CustomerName"].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvCustomers.Columns["CustomerName"].FillWeight = 35;

            dgvCustomers.Columns["Address"].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvCustomers.Columns["Address"].FillWeight = 40;
        }

        private void ClearCustomerFields()
        {
            txtCustomerCode.Clear();
            txtCustomerName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtNTN.Clear();

            chkIsActive.Checked = true;

            txtCustomerCode.Focus();
        }

        private void SetCustomerEditMode(bool editing)
        {
            btnSave.Enabled = !editing;
            btnUpdate.Enabled = editing;
            btnDelete.Enabled = editing;
        }

        private bool ValidateCustomer()
        {
            if (string.IsNullOrWhiteSpace(
                    txtCustomerCode.Text))
            {
                MessageBox.Show(
                    "Please enter customer code.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCustomerCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                    txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Please enter customer name.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCustomerName.Focus();
                return false;
            }

            return true;
        }

        private bool CustomerCodeExists(
            string customerCode)
        {
            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
            SELECT COUNT(*)
            FROM dbo.Customers
            WHERE CustomerCode = @CustomerCode;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@CustomerCode",
                        SqlDbType.NVarChar, 50).Value =
                        customerCode.Trim();

                    cn.Open();

                    return Convert.ToInt32(
                        cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private void SaveCustomer()
        {
            if (!ValidateCustomer())
                return;

            string customerCode =
                txtCustomerCode.Text.Trim();

            if (CustomerCodeExists(customerCode))
            {
                MessageBox.Show(
                    "Customer code already exists.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCustomerCode.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                INSERT INTO dbo.Customers
                (
                    CustomerCode,
                    CustomerName,
                    ContactPerson,
                    Phone,
                    Address,
                    NTN,
                    IsActive
                )
                VALUES
                (
                    @CustomerCode,
                    @CustomerName,
                    @ContactPerson,
                    @Phone,
                    @Address,
                    @NTN,
                    @IsActive
                );";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@CustomerCode",
                            SqlDbType.NVarChar, 50).Value =
                            customerCode;

                        cmd.Parameters.Add(
                            "@CustomerName",
                            SqlDbType.NVarChar, 150).Value =
                            txtCustomerName.Text.Trim();

                        cmd.Parameters.Add(
                            "@ContactPerson",
                            SqlDbType.NVarChar, 100).Value =
                            string.IsNullOrWhiteSpace(
                                txtContactPerson.Text)
                                ? (object)DBNull.Value
                                : txtContactPerson.Text.Trim();

                        cmd.Parameters.Add(
                            "@Phone",
                            SqlDbType.NVarChar, 30).Value =
                            string.IsNullOrWhiteSpace(
                                txtPhone.Text)
                                ? (object)DBNull.Value
                                : txtPhone.Text.Trim();

                        cmd.Parameters.Add(
                            "@Address",
                            SqlDbType.NVarChar, 250).Value =
                            string.IsNullOrWhiteSpace(
                                txtAddress.Text)
                                ? (object)DBNull.Value
                                : txtAddress.Text.Trim();

                        cmd.Parameters.Add(
                            "@NTN",
                            SqlDbType.NVarChar, 50).Value =
                            string.IsNullOrWhiteSpace(
                                txtNTN.Text)
                                ? (object)DBNull.Value
                                : txtNTN.Text.Trim();

                        cmd.Parameters.Add(
                            "@IsActive",
                            SqlDbType.Bit).Value =
                            chkIsActive.Checked;

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Customer saved successfully.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCustomers();

                ClearCustomerFields();

                SetCustomerEditMode(false);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateCustomer()
        {
            if (_selectedCustomerID <= 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateCustomer())
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                UPDATE dbo.Customers
                SET
                    CustomerCode = @CustomerCode,
                    CustomerName = @CustomerName,
                    ContactPerson = @ContactPerson,
                    Phone = @Phone,
                    Address = @Address,
                    NTN = @NTN,
                    IsActive = @IsActive
                WHERE CustomerID = @CustomerID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@CustomerID",
                            SqlDbType.Int).Value =
                            _selectedCustomerID;

                        cmd.Parameters.Add(
                            "@CustomerCode",
                            SqlDbType.NVarChar, 50).Value =
                            txtCustomerCode.Text.Trim();

                        cmd.Parameters.Add(
                            "@CustomerName",
                            SqlDbType.NVarChar, 150).Value =
                            txtCustomerName.Text.Trim();

                        cmd.Parameters.Add(
                            "@ContactPerson",
                            SqlDbType.NVarChar, 100).Value =
                            string.IsNullOrWhiteSpace(
                                txtContactPerson.Text)
                                ? (object)DBNull.Value
                                : txtContactPerson.Text.Trim();

                        cmd.Parameters.Add(
                            "@Phone",
                            SqlDbType.NVarChar, 30).Value =
                            string.IsNullOrWhiteSpace(
                                txtPhone.Text)
                                ? (object)DBNull.Value
                                : txtPhone.Text.Trim();

                        cmd.Parameters.Add(
                            "@Address",
                            SqlDbType.NVarChar, 250).Value =
                            string.IsNullOrWhiteSpace(
                                txtAddress.Text)
                                ? (object)DBNull.Value
                                : txtAddress.Text.Trim();

                        cmd.Parameters.Add(
                            "@NTN",
                            SqlDbType.NVarChar, 50).Value =
                            string.IsNullOrWhiteSpace(
                                txtNTN.Text)
                                ? (object)DBNull.Value
                                : txtNTN.Text.Trim();

                        cmd.Parameters.Add(
                            "@IsActive",
                            SqlDbType.Bit).Value =
                            chkIsActive.Checked;

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Customer updated successfully.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCustomers();

                ClearCustomerFields();

                _selectedCustomerID = 0;

                SetCustomerEditMode(false);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeactivateCustomer()
        {
            if (_selectedCustomerID <= 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Deactivate this customer?\n\n" +
                    "Existing transactions will remain محفوظ.",
                    "Deactivate Customer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                UPDATE dbo.Customers
                SET IsActive = 0
                WHERE CustomerID = @CustomerID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@CustomerID",
                            SqlDbType.Int).Value =
                            _selectedCustomerID;

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Customer deactivated successfully.",
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCustomers();

                ClearCustomerFields();

                _selectedCustomerID = 0;

                SetCustomerEditMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SearchCustomers()
        {
            string search =
                txtSearchCustomer.Text.Trim();

            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    CustomerID,
                    CustomerCode,
                    CustomerName,
                    ContactPerson,
                    Phone,
                    Address,
                    NTN,
                    IsActive
                FROM dbo.Customers
                WHERE
                    CustomerCode LIKE @Search
                    OR CustomerName LIKE @Search
                    OR ContactPerson LIKE @Search
                    OR Phone LIKE @Search
                    OR NTN LIKE @Search
                ORDER BY
                    CustomerName;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@Search",
                            SqlDbType.NVarChar, 200).Value =
                            "%" + search + "%";

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                dgvCustomers.DataSource = dt;

                FormatCustomersGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------
        // Event Handlers
        // ------------------------------------------------------
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            ClearCustomerFields();
            SetCustomerEditMode(false);
        }

        private void btnNew_Click(
            object sender,
            EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER.NEW"))
            {
                MessageBox.Show("You do not have permission to create purchase receipts.");
                return;
            }
            txtSearchCustomer.Clear();

            _selectedCustomerID = 0;

            ClearCustomerFields();

            SetCustomerEditMode(false);

            LoadCustomers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER.SAVE"))
            {
                MessageBox.Show("You do not have permission to save customers.");
                return;
            }
            SaveCustomer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER.UPDATE"))
            {
                MessageBox.Show("You do not have permission to update customers.");
                return;
            }
            if(_selectedCustomerID <= 0)
            {
                MessageBox.Show("Please select a customer id first.");
                return;
            }
            UpdateCustomer();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER.DELETE"))
            {
                MessageBox.Show("You do not have permission to deactivate customers.");
                return;
            }
            DeactivateCustomer();
        }

        private void txtSearchCustomer_TextChanged(
            object sender,
            EventArgs e)
        {
            SearchCustomers();
        }

        private void dgvCustomers_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvCustomers.Rows[e.RowIndex];

            if (row.Cells["CustomerID"].Value == null)
                return;

            _selectedCustomerID =
                Convert.ToInt32(
                    row.Cells["CustomerID"].Value);

            txtCustomerCode.Text =
                row.Cells["CustomerCode"].Value?.ToString();

            txtCustomerName.Text =
                row.Cells["CustomerName"].Value?.ToString();

            txtContactPerson.Text =
                row.Cells["ContactPerson"].Value?.ToString();

            txtPhone.Text =
                row.Cells["Phone"].Value?.ToString();

            txtAddress.Text =
                row.Cells["Address"].Value?.ToString();

            txtNTN.Text =
                row.Cells["NTN"].Value?.ToString();

            chkIsActive.Checked =
                row.Cells["IsActive"].Value != null &&
                Convert.ToBoolean(
                    row.Cells["IsActive"].Value);

            SetCustomerEditMode(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
    
