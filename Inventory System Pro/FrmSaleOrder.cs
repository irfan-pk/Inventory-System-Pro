using InventorySystemPro;
using PdfSharp.Quality;
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
    public partial class FrmSaleOrder : Form
    {
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public int SelectedSOID { get; private set; }
        private string _currentSONumber = string.Empty;
        private string _currentSOStatus = "";
        private int _currentSOID = 0;
        //private DataTable _products;
        private int CurrentUserID = 1; // Replace with actual user ID from your authentication system

        public FrmSaleOrder()
        {
            InitializeComponent();
            InitializeSaleOrderGrid();
            LoadCustomers();
            LoadWarehouses();
            LoadProductsIntoGrid();
        }

        // --------------------------------------------------------------------
        // Initialize the Sale Order DataGridView And Form Controls and Methods
        // --------------------------------------------------------------------

        private void InitializeSaleOrderGrid()
        {
            dgvSaleOrderDetails.Columns.Clear();
            dgvSaleOrderDetails.AutoGenerateColumns = false;
            dgvSaleOrderDetails.AllowUserToAddRows = true;
            dgvSaleOrderDetails.AllowUserToDeleteRows = true;
            dgvSaleOrderDetails.SelectionMode =
                DataGridViewSelectionMode.CellSelect;

            // -------------------------------------------------
            // Product
            // -------------------------------------------------
            DataGridViewComboBoxColumn productColumn =
                new DataGridViewComboBoxColumn();

            productColumn.Name = "ProductID";
            productColumn.HeaderText = "Product";
            productColumn.DataPropertyName = "ProductID";
            productColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            productColumn.FlatStyle = FlatStyle.Flat;
            productColumn.Width = 250;

            dgvSaleOrderDetails.Columns.Add(productColumn);

            // -------------------------------------------------
            // Product Code
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductCode",
                HeaderText = "Code",
                DataPropertyName = "ProductCode",
                ReadOnly = true,
                Width = 80
            });

            // -------------------------------------------------
            // Product Name
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                ReadOnly = true,
                Width = 250
            });

            // -------------------------------------------------
            // Ordered Quantity
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderedQty",
                HeaderText = "Qty",
                DataPropertyName = "OrderedQty",
                Width = 60
            });

            // -------------------------------------------------
            // Unit Price
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "Unit Price",
                DataPropertyName = "SalePrice",
                Width = 80
            });

            // -------------------------------------------------
            // Discount
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiscountAmount",
                HeaderText = "Discount",
                DataPropertyName = "DiscountAmount",
                Width = 80
            });

            // -------------------------------------------------
            // Tax
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxAmount",
                HeaderText = "Tax",
                DataPropertyName = "TaxAmount",
                Width = 80
            });

            // -------------------------------------------------
            // Net Amount
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NetAmount",
                HeaderText = "Net Amount",
                Width = 120,
                ReadOnly = true
            });

            // -------------------------------------------------
            // Delivered Qty
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DeliveredQty",
                HeaderText = "Delivered",
                DataPropertyName = "DeliveredQty",
                ReadOnly = true,
                Width = 85
            });

            // -------------------------------------------------
            // Hidden database detail ID
            // -------------------------------------------------
            dgvSaleOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SODetailID",
                DataPropertyName = "SODetailID",
                Visible = false
            });

            // -------------------------------------------------
            // Delete Item
            // -------------------------------------------------
            DataGridViewButtonColumn deleteColumn =
                new DataGridViewButtonColumn();

            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.Text = "✕";
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.Width = 40;
            deleteColumn.FlatStyle = FlatStyle.Flat;

            dgvSaleOrderDetails.Columns.Add(deleteColumn);
            // -------------------------------------------------
            // Align numeric columns to the right
            // -------------------------------------------------
            AlignSaleOrderNumericColumns();
        }

        private DataTable GetActiveProducts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                        SELECT
                        ProductID,
                        ProductCode,
                        ProductName,
                        SalePrice
                        FROM dbo.Products
                        WHERE IsActive = 1
                        ORDER BY ProductCode";

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

            return dt;
        }

        private void LoadProductsIntoGrid()
        {
            DataTable products = GetActiveProducts();

            DataGridViewComboBoxColumn productColumn =
                dgvSaleOrderDetails.Columns["ProductID"]
                as DataGridViewComboBoxColumn;

            if (productColumn == null)
                throw new InvalidOperationException(
                    "ProductID ComboBox column was not found.");

            productColumn.DataSource = products;
            productColumn.ValueMember = "ProductID";
            productColumn.DisplayMember = "ProductName";
        }

        // =========================================================
        // SUPPLIERS
        // =========================================================

        private void LoadCustomers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT
                CustomerID,
                CustomerName
                FROM dbo.Customers
                WHERE IsActive = 1
                ORDER BY CustomerName", cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                cmbCustomer.DataSource = null;
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.DataSource = dt;

                if (dt.Rows.Count > 0)
                    cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Customer Loading Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // WAREHOUSES
        // =========================================================

        private void LoadWarehouses()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                WarehouseID,
                WarehouseName
                FROM dbo.Warehouses
                WHERE IsActive = 1
                ORDER BY WarehouseName", cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                cmbWarehouse.DataSource = null;
                cmbWarehouse.DisplayMember = "WarehouseName";
                cmbWarehouse.ValueMember = "WarehouseID";
                cmbWarehouse.DataSource = dt;

                if (dt.Rows.Count > 0)
                    cmbWarehouse.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Warehouse Loading Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculateSaleOrderRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvSaleOrderDetails.Rows.Count)
                return;

            DataGridViewRow row = dgvSaleOrderDetails.Rows[rowIndex];

            if (row.IsNewRow)
                return;

            decimal qty = GetDecimalCell(row, "OrderedQty");
            decimal unitPrice = GetDecimalCell(row, "SalePrice");
            decimal discount = GetDecimalCell(row, "DiscountAmount");
            decimal tax = GetDecimalCell(row, "TaxAmount");

            decimal gross = qty * unitPrice;

            decimal net = gross - discount + tax;

            if (net < 0)
                net = 0;

            row.Cells["NetAmount"].Value = net;
        }

        private decimal GetDecimalCell(
            DataGridViewRow row,
            string columnName)
        {
            object value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
                return 0m;

            decimal result;

            if (decimal.TryParse(
                    value.ToString(),
                    out result))
            {
                return result;
            }

            return 0m;
        }

        private void CalculateSaleOrderTotals()
        {
            decimal gross = 0m;
            decimal discount = 0m;
            decimal tax = 0m;
            decimal net = 0m;

            foreach (DataGridViewRow row in dgvSaleOrderDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimalCell(row, "OrderedQty");

                decimal price =
                    GetDecimalCell(row, "SalePrice");

                decimal rowDiscount =
                    GetDecimalCell(row, "DiscountAmount");

                decimal rowTax =
                    GetDecimalCell(row, "TaxAmount");

                decimal rowGross = qty * price;

                gross += rowGross;
                discount += rowDiscount;
                tax += rowTax;

                net += rowGross - rowDiscount + rowTax;
            }

            txtGrossAmount.Text = gross.ToString("N2");
            txtDiscountAmount.Text = discount.ToString("N2");
            txtTaxAmount.Text = tax.ToString("N2");
            txtNetAmount.Text = net.ToString("N2");
        }

        private bool IsProductAlreadySelected(int productID, int currentRowIndex)
        {
            foreach (DataGridViewRow row in dgvSaleOrderDetails.Rows)
            {
                if (row.IsNewRow || row.Index == currentRowIndex)
                    continue;

                object value = row.Cells["ProductID"].Value;

                if (value == null || value == DBNull.Value)
                    continue;

                if (int.TryParse(value.ToString(), out int existingProductID))
                {
                    if (existingProductID == productID)
                        return true;
                }
            }

            return false;
        }

        private bool ValidateSaleOrderDetails()
        {
            int validRows = 0;

            foreach (DataGridViewRow row in dgvSaleOrderDetails.Rows)
            {
                // Ignore the DataGridView's automatic new row
                if (row.IsNewRow)
                    continue;

                object productValue = row.Cells["ProductID"].Value;

                // Ignore completely empty rows
                if (productValue == null ||
                    productValue == DBNull.Value ||
                    string.IsNullOrWhiteSpace(productValue.ToString()))
                {
                    continue;
                }

                validRows++;

                // ---------------------------------------------
                // Product
                // ---------------------------------------------
                if (!int.TryParse(
                        productValue.ToString(),
                        out int productID) ||
                    productID <= 0)
                {
                    MessageBox.Show(
                        "Please select a valid product.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["ProductID"];

                    return false;
                }

                // ---------------------------------------------
                // Ordered Quantity
                // ---------------------------------------------
                decimal qty =
                    GetDecimalCell(row, "OrderedQty");

                if (qty <= 0)
                {
                    MessageBox.Show(
                        "Ordered quantity must be greater than zero.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["OrderedQty"];

                    return false;
                }

                // ---------------------------------------------
                // Unit Price
                // ---------------------------------------------
                decimal unitPrice =
                    GetDecimalCell(row, "SalePrice");

                if (unitPrice < 0)
                {
                    MessageBox.Show(
                        "Sale price cannot be negative.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["SalePrice"];

                    return false;
                }

                // ---------------------------------------------
                // Discount
                // ---------------------------------------------
                decimal discount =
                    GetDecimalCell(row, "DiscountAmount");

                if (discount < 0)
                {
                    MessageBox.Show(
                        "Discount cannot be negative.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["DiscountAmount"];

                    return false;
                }

                // ---------------------------------------------
                // Tax
                // ---------------------------------------------
                decimal tax =
                    GetDecimalCell(row, "TaxAmount");

                if (tax < 0)
                {
                    MessageBox.Show(
                        "Tax cannot be negative.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["TaxAmount"];

                    return false;
                }

                // ---------------------------------------------
                // Discount cannot exceed gross
                // ---------------------------------------------
                decimal gross = qty * unitPrice;

                if (discount > gross)
                {
                    MessageBox.Show(
                        "Discount cannot be greater than the gross amount.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleOrderDetails.CurrentCell =
                        row.Cells["DiscountAmount"];

                    return false;
                }
            }

            // ---------------------------------------------
            // At least one product required
            // ---------------------------------------------
            if (validRows == 0)
            {
                MessageBox.Show(
                    "Please add at least one product.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void CommitSaleOrderGridEdit()
        {
            if (dgvSaleOrderDetails.IsCurrentCellDirty)
            {
                dgvSaleOrderDetails.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }

            dgvSaleOrderDetails.EndEdit();
        }

        private void SaveSaleOrder()
        {
            if (!DateTime.TryParse(dtpSODate.Text, out DateTime soDate))
            {
                MessageBox.Show(
                    "Invalid Sales Order date.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbCustomer.SelectedValue == null ||
                !int.TryParse(
                    cmbCustomer.SelectedValue.ToString(),
                    out int customerID))
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbWarehouse.SelectedValue == null ||
                !int.TryParse(
                    cmbWarehouse.SelectedValue.ToString(),
                    out int warehouseID))
            {
                MessageBox.Show(
                    "Please select a warehouse.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ---------------------------------------------------------
            // Determine whether this is NEW or UPDATE
            // ---------------------------------------------------------

            bool isNewOrder = _currentSOID == 0;

            bool isExistingOpenOrder =
                _currentSOID > 0 &&
                string.Equals(
                    _currentSOStatus,
                    "OPEN",
                    StringComparison.OrdinalIgnoreCase);

            if (!isNewOrder && !isExistingOpenOrder)
            {
                MessageBox.Show(
                    "Only a new or OPEN Sales Order can be saved.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ---------------------------------------------------------
            // Build TVP
            // ---------------------------------------------------------

            DataTable details = new DataTable();

            details.Columns.Add("ProductID", typeof(int));
            details.Columns.Add("OrderedQty", typeof(decimal));
            details.Columns.Add("UnitPrice", typeof(decimal));
            details.Columns.Add("DiscountAmount", typeof(decimal));
            details.Columns.Add("TaxAmount", typeof(decimal));

            foreach (DataGridViewRow row in dgvSaleOrderDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                object productValue =
                    row.Cells["ProductID"].Value;

                if (productValue == null ||
                    productValue == DBNull.Value ||
                    string.IsNullOrWhiteSpace(productValue.ToString()))
                {
                    continue;
                }

                int productID =
                    Convert.ToInt32(productValue);

                decimal orderedQty =
                    GetDecimalCell(row, "OrderedQty");

                decimal unitPrice =
                    GetDecimalCell(row, "SalePrice");

                decimal discount =
                    GetDecimalCell(row, "DiscountAmount");

                decimal tax =
                    GetDecimalCell(row, "TaxAmount");

                details.Rows.Add(
                    productID,
                    orderedQty,
                    unitPrice,
                    discount,
                    tax);
            }

            if (details.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Please add at least one product.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ---------------------------------------------------------
            // Select Stored Procedure
            // ---------------------------------------------------------

            string procedureName =
                isNewOrder
                    ? "dbo.sp_SaleOrder_Save"
                    : "dbo.sp_SaleOrder_Update";

            using (SqlConnection conn =
                   new SqlConnection(ConnString))
            using (SqlCommand cmd =
                   new SqlCommand(
                       procedureName,
                       conn))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                // -----------------------------------------------------
                // SOID - UPDATE ONLY
                // -----------------------------------------------------

                if (isExistingOpenOrder)
                {
                    cmd.Parameters.Add(
                        "@SOID",
                        SqlDbType.Int).Value =
                        _currentSOID;
                }

                // -----------------------------------------------------
                // Header
                // -----------------------------------------------------

                cmd.Parameters.Add(
                    "@SODate",
                    SqlDbType.Date).Value =
                    soDate.Date;

                cmd.Parameters.Add(
                    "@CustomerID",
                    SqlDbType.Int).Value =
                    customerID;

                cmd.Parameters.Add(
                    "@WarehouseID",
                    SqlDbType.Int).Value =
                    warehouseID;

                cmd.Parameters.Add(
                    "@Remarks",
                    SqlDbType.NVarChar,
                    500).Value =
                    string.IsNullOrWhiteSpace(txtRemarks.Text)
                        ? (object)DBNull.Value
                        : txtRemarks.Text.Trim();

                // -----------------------------------------------------
                // CreatedBy - NEW ONLY
                // -----------------------------------------------------

                if (isNewOrder)
                {
                    cmd.Parameters.Add(
                        "@CreatedBy",
                        SqlDbType.Int).Value =
                        CurrentUserID;
                }

                // -----------------------------------------------------
                // Details TVP
                // -----------------------------------------------------

                SqlParameter detailParameter =
                    cmd.Parameters.Add(
                        "@Details",
                        SqlDbType.Structured);

                detailParameter.TypeName =
                    "dbo.SaleOrderDetailType";

                detailParameter.Value =
                    details;

                // -----------------------------------------------------
                // Output parameters - NEW ONLY
                // -----------------------------------------------------

                SqlParameter soidParameter = null;
                SqlParameter soNumberParameter = null;

                if (isNewOrder)
                {
                    soidParameter =
                        cmd.Parameters.Add(
                            "@SOID",
                            SqlDbType.Int);

                    soidParameter.Direction =
                        ParameterDirection.Output;

                    soNumberParameter =
                        cmd.Parameters.Add(
                            "@SONumber",
                            SqlDbType.NVarChar,
                            30);

                    soNumberParameter.Direction =
                        ParameterDirection.Output;
                }

                // -----------------------------------------------------
                // Execute
                // -----------------------------------------------------

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // NEW SALES ORDER
                    // =================================================

                    if (isNewOrder)
                    {
                        int soID =
                            Convert.ToInt32(
                                soidParameter.Value);

                        string soNumber =
                            soNumberParameter.Value?.ToString();

                        txtSONumber.Text =
                            soNumber;

                        MessageBox.Show(
                            $"Sales Order {soNumber} saved successfully.",
                            "Sales Order",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // New order is now OPEN.
                        SetSaleOrderSavedState(soID);
                    }

                    // =================================================
                    // EXISTING OPEN SALES ORDER
                    // =================================================

                    else
                    {
                        // Existing order remains OPEN.
                        _currentSOStatus = "OPEN";

                        MessageBox.Show(
                            $"Sales Order {txtSONumber.Text} updated successfully.",
                            "Sales Order",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        SetSaleOrderEditable(true);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void SetSaleOrderSavedState(int soID)
        {
            _currentSOID = soID;
            _currentSOStatus = "OPEN";

            txtSONumber.ReadOnly = true;

            SetSaleOrderEditable(true);

            btnSave.Enabled = false;
            btnApprove.Enabled = true;
        }

        private void NewSaleOrder()
        {
            _currentSOID = 0;
            _currentSOStatus = "";

            txtSONumber.Clear();

            dtpSODate.Value = DateTime.Today;

            cmbCustomer.SelectedIndex = -1;
            cmbWarehouse.SelectedIndex = -1;

            txtRemarks.Clear();

            dgvSaleOrderDetails.Rows.Clear();

            txtGrossAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";

            dtpSODate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbWarehouse.Enabled = true;
            txtRemarks.ReadOnly = false;

            dgvSaleOrderDetails.ReadOnly = false;

            btnSave.Enabled = true;
            btnApprove.Enabled = false;
        }

        private bool ApproveSaleOrder()
        {
            if (_currentSOID <= 0)
            {
                MessageBox.Show(
                    "Please save the Sales Order first.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to approve Sales Order {txtSONumber.Text}?",
                "Approve Sales Order",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return false;

            using (SqlConnection conn =
                   new SqlConnection(ConnString))
            using (SqlCommand cmd =
                   new SqlCommand(
                       "dbo.sp_SaleOrder_Approve",
                       conn))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value =
                    _currentSOID;

                cmd.Parameters.Add(
                    "@ApprovedBy",
                    SqlDbType.Int).Value =
                    CurrentUserID;

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    // ---------------------------------------------
                    // Approval succeeded
                    // ---------------------------------------------

                    btnApprove.Enabled = false;
                    btnSave.Enabled = false;

                    txtSONumber.ReadOnly = true;
                    dtpSODate.Enabled = false;
                    cmbCustomer.Enabled = false;
                    cmbWarehouse.Enabled = false;
                    txtRemarks.ReadOnly = true;

                    dgvSaleOrderDetails.ReadOnly = true;

                    MessageBox.Show(
                        $"Sales Order {txtSONumber.Text} approved successfully.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Sales Order Approval",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Sales Order Approval",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }
            }
        }

        private void LoadSalesOrderForDelivery(int soID)
        {
            if (soID <= 0)
                return;

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT
            so.SOID,
            so.SONumber,
            so.SODate,
            so.CustomerID,
            so.WarehouseID,
            so.Status,
            so.Remarks
            FROM dbo.SaleOrders so
            WHERE so.SOID = @SOID
            AND so.Status IN ('APPROVED', 'PARTIAL');", cn))
            {
                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value = soID;

                try
                {
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            MessageBox.Show(
                                "Sales Order is not available.",
                                "Sales Order",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        // -----------------------------------------
                        // Store Sales Order information
                        // -----------------------------------------

                        _currentSOID =
                            Convert.ToInt32(dr["SOID"]);

                        _currentSOStatus =
                            dr["Status"].ToString();

                        _currentSONumber =
                            dr["SONumber"].ToString();

                        // -----------------------------------------
                        // Load Sales Order header
                        // -----------------------------------------

                        txtSONumber.Text =
                            _currentSONumber;

                        dtpSODate.Value =
                            Convert.ToDateTime(dr["SODate"]);

                        cmbCustomer.SelectedValue =
                            Convert.ToInt32(dr["CustomerID"]);

                        cmbWarehouse.SelectedValue =
                            Convert.ToInt32(dr["WarehouseID"]);

                        txtRemarks.Text =
                            dr["Remarks"] == DBNull.Value
                                ? ""
                                : dr["Remarks"].ToString();

                        //_currentSOStatus =
                        //          dr["Status"].ToString();
                    }

                    // ---------------------------------------------
                    // Load Sales Order details
                    // ---------------------------------------------

                    DataTable dt =
                        GetSalesOrderForDelivery(_currentSOID);

                    dgvSaleOrderDetails.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        int r =
                            dgvSaleOrderDetails.Rows.Add();

                        // Product
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductID"].Value =
                            row["ProductID"];

                        // Product Code
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductCode"].Value =
                            row["ProductCode"];

                        // Product Name
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductName"].Value =
                            row["ProductName"];

                        // Remaining quantity
                        // Actual column name is OrderedQty
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["OrderedQty"].Value =
                            row["RemainingQty"];

                        // Actual column name is SalePrice
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["SalePrice"].Value =
                            row["UnitPrice"];

                        // Discount
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["DiscountAmount"].Value =
                            row["DiscountAmount"];

                        // Tax
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["TaxAmount"].Value =
                            row["TaxAmount"];

                        // Delivered quantity
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["DeliveredQty"].Value =
                            row["DeliveredQty"];

                        // Detail ID
                        dgvSaleOrderDetails.Rows[r]
                            .Cells["SODetailID"].Value =
                            row["SODetailID"];

                        // Net Amount
                        decimal remainingQty =
                            Convert.ToDecimal(row["RemainingQty"]);

                        decimal unitPrice =
                            Convert.ToDecimal(row["UnitPrice"]);

                        decimal discount =
                            Convert.ToDecimal(row["DiscountAmount"]);

                        decimal tax =
                            Convert.ToDecimal(row["TaxAmount"]);

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["NetAmount"].Value =
                            (remainingQty * unitPrice)
                            - discount
                            + tax;
                    }

                    CalculateSaleOrderTotals();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Load Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Load Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private DataTable GetSalesOrderForDelivery(int soID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT
            sod.SODetailID,
            sod.SOID,
            sod.ProductID,

            p.ProductCode,
            p.ProductName,

            sod.OrderedQty,
            sod.DeliveredQty,

            (sod.OrderedQty - sod.DeliveredQty)
                AS RemainingQty,

            sod.UnitPrice,
            sod.DiscountAmount,
            sod.TaxAmount

        FROM dbo.SaleOrderDetails sod

        INNER JOIN dbo.Products p
            ON p.ProductID = sod.ProductID

        INNER JOIN dbo.SaleOrders so
            ON so.SOID = sod.SOID

        WHERE sod.SOID = @SOID
          AND so.Status IN ('APPROVED', 'PARTIAL')
          AND sod.DeliveredQty < sod.OrderedQty

        ORDER BY sod.SODetailID;", cn))
            {
                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value = soID;

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private void LoadPendingSalesOrders(string searchText = "")
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT
            so.SOID,
            so.SONumber,
            so.SODate,
            so.CustomerID,
            c.CustomerName,
            so.WarehouseID,
            w.WarehouseName,
            so.Status
        FROM dbo.SaleOrders so

        INNER JOIN dbo.Customers c
            ON c.CustomerID = so.CustomerID

        INNER JOIN dbo.Warehouses w
            ON w.WarehouseID = so.WarehouseID

        WHERE so.Status = 'OPEN'
          AND
          (
              so.SONumber LIKE '%' + @Search + '%'
              OR c.CustomerName LIKE '%' + @Search + '%'
          )

            ORDER BY so.SOID DESC;", cn))
            {
                cmd.Parameters.Add(
                    "@Search",
                    SqlDbType.NVarChar, 100).Value =
                    searchText ?? "";

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dgvSaleOrderDetails.DataSource = dt;
        }

        private DataTable GetSalesOrderDetails(int soID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT
            sod.SODetailID,
            sod.SOID,
            sod.ProductID,

            p.ProductCode,
            p.ProductName,

            sod.OrderedQty,
            sod.DeliveredQty,

            (sod.OrderedQty - sod.DeliveredQty)
                AS RemainingQty,

            sod.UnitPrice,
            sod.DiscountAmount,
            sod.TaxAmount

            FROM dbo.SaleOrderDetails sod

            INNER JOIN dbo.Products p
            ON p.ProductID = sod.ProductID

            WHERE sod.SOID = @SOID
              AND sod.DeliveredQty < sod.OrderedQty

            ORDER BY sod.SODetailID;", cn))
            {
                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value = soID;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private DataTable GetApprovedSalesOrders()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT
            so.SOID,
            so.SONumber,
            so.SODate,
            so.CustomerID,
            c.CustomerName,
            so.WarehouseID,
            w.WarehouseName,
            so.Remarks
            FROM dbo.SaleOrders so
            INNER JOIN dbo.Customers c
            ON c.CustomerID = so.CustomerID
            INNER JOIN dbo.Warehouses w
            ON w.WarehouseID = so.WarehouseID
            WHERE so.Status = 'APPROVED'
            ORDER BY so.SODate, so.SOID;", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            return dt;
        }

        private void LoadSalesOrder(int soID)
        {
            if (soID <= 0)
                return;

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT
            SOID,
            SONumber,
            SODate,
            CustomerID,
            WarehouseID,
            Status,
            Remarks
        FROM dbo.SaleOrders
        WHERE SOID = @SOID;", cn))
            {
                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value = soID;

                try
                {
                    cn.Open();

                    using (SqlDataReader dr =
                           cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            MessageBox.Show(
                                "Sales Order not found.",
                                "Sales Order",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        string status =
                            dr["Status"].ToString();

                        _currentSOStatus = status;

                        if (!status.Equals(
                                "OPEN",
                                StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show(
                                $"Sales Order {dr["SONumber"]} is already {status}.",
                                "Sales Order",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }

                        SetSaleOrderEditable(true);
                        
                        _currentSOID =
                            Convert.ToInt32(dr["SOID"]);

                        _currentSONumber =
                            dr["SONumber"].ToString();

                        txtSONumber.Text =
                            _currentSONumber;

                        dtpSODate.Value =
                            Convert.ToDateTime(dr["SODate"]);

                        cmbCustomer.SelectedValue =
                            Convert.ToInt32(dr["CustomerID"]);

                        cmbWarehouse.SelectedValue =
                            Convert.ToInt32(dr["WarehouseID"]);

                        txtRemarks.Text =
                            dr["Remarks"] == DBNull.Value
                                ? ""
                                : dr["Remarks"].ToString();
                    }

                    // Load existing SO details
                    DataTable dt =
                        GetOpenSalesOrderDetails(_currentSOID);

                    dgvSaleOrderDetails.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        int r =
                            dgvSaleOrderDetails.Rows.Add();

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["SODetailID"].Value =
                            row["SODetailID"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductID"].Value =
                            row["ProductID"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductCode"].Value =
                            row["ProductCode"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["ProductName"].Value =
                            row["ProductName"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["OrderedQty"].Value =
                            row["OrderedQty"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["SalePrice"].Value =
                            row["UnitPrice"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["DiscountAmount"].Value =
                            row["DiscountAmount"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["TaxAmount"].Value =
                            row["TaxAmount"];

                        dgvSaleOrderDetails.Rows[r]
                            .Cells["DeliveredQty"].Value =
                            row["DeliveredQty"];

                        if (dgvSaleOrderDetails.Columns.Contains(
                                "NetAmount"))
                        {
                            dgvSaleOrderDetails.Rows[r]
                                .Cells["NetAmount"].Value =
                                (
                                    Convert.ToDecimal(row["OrderedQty"])
                                    * Convert.ToDecimal(row["UnitPrice"])
                                )
                                - Convert.ToDecimal(row["DiscountAmount"])
                                + Convert.ToDecimal(row["TaxAmount"]);
                        }
                    }

                    CalculateSaleOrderTotals();

                    // Existing OPEN order can be reviewed/approved
                    txtSONumber.ReadOnly = true;

                    dtpSODate.Enabled = true;
                    cmbCustomer.Enabled = true;
                    cmbWarehouse.Enabled = true;

                    txtRemarks.ReadOnly = false;

                    dgvSaleOrderDetails.ReadOnly = false;

                    btnSave.Enabled = true;
                    btnApprove.Enabled = true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Load Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Load Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            txtSONumber.ReadOnly = true;

            dtpSODate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbWarehouse.Enabled = true;

            txtRemarks.ReadOnly = false;

            dgvSaleOrderDetails.ReadOnly = false;

            btnSave.Enabled = true;
            btnApprove.Enabled = true;
        }

        private void AlignSaleOrderNumericColumns()
        {
            string[] numericColumns =
            {
                "OrderedQty",
                "SalePrice",
                "DiscountAmount",
                "TaxAmount",
                "NetAmount",
                "DeliveredQty"
            };

            foreach (string columnName in numericColumns)
            {
                if (!dgvSaleOrderDetails.Columns.Contains(columnName))
                    continue;

                dgvSaleOrderDetails.Columns[columnName]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleOrderDetails.Columns[columnName]
                    .HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void RemoveSelectedItem()
        {
            if (dgvSaleOrderDetails.CurrentRow == null ||
                dgvSaleOrderDetails.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Please select an item to remove.",
                    "Remove Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int rowIndex = dgvSaleOrderDetails.CurrentRow.Index;

            dgvSaleOrderDetails.Rows.RemoveAt(rowIndex);

            CalculateSaleOrderTotals();
        }

        // ----------------------------------------------------------
        // Forms Load Event Handler
        private void FrmSaleOrder_Load(object sender, EventArgs e)
        {
            
            NewSaleOrder ();
        }

        // ----------------------------------------------------------
        // DataGridView Cell Value Changed Event Handler
        // ----------------------------------------------------------
        private void dgvSaleOrderDetails_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvSaleOrderDetails.Rows[e.RowIndex];

            if (row.IsNewRow)
                return;

            string columnName =
                dgvSaleOrderDetails.Columns[e.ColumnIndex].Name;

            // -------------------------------------------------
            // Product selected
            // -------------------------------------------------
            if (columnName == "ProductID")
            {
                if (row.Cells["ProductID"].Value == null ||
                    row.Cells["ProductID"].Value == DBNull.Value)
                    return;

                if (!int.TryParse(
                        row.Cells["ProductID"].Value.ToString(),
                        out int productID))
                    return;

                // Prevent duplicate product
                if (IsProductAlreadySelected(productID, e.RowIndex))
                {
                    MessageBox.Show(
                        "This product has already been added to the Sales Order.",
                        "Duplicate Product",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    row.Cells["ProductID"].Value = null;
                    return;
                }

                DataTable products = GetActiveProducts();

                DataRow[] found =
                    products.Select("ProductID = " + productID);

                if (found.Length == 0)
                    return;

                DataRow product = found[0];

                row.Cells["ProductCode"].Value =
                    product["ProductCode"];

                row.Cells["ProductName"].Value =
                    product["ProductName"];

                row.Cells["SalePrice"].Value =
                    product["SalePrice"];

                row.Cells["DeliveredQty"].Value = 0m;

                CalculateSaleOrderRow(e.RowIndex);
            }

            // -------------------------------------------------
            // Recalculate amount
            // -------------------------------------------------
            if (columnName == "OrderedQty" ||
                columnName == "SalePrice" ||
                columnName == "DiscountAmount" ||
                columnName == "TaxAmount" ||
                columnName == "ProductID")
            {
                CalculateSaleOrderRow(e.RowIndex);
            }

            CalculateSaleOrderTotals();
        }

        private void SetSaleOrderEditable(bool editable)
        {
            // SO Number is generated by the system
            txtSONumber.ReadOnly = true;

            dtpSODate.Enabled = editable;
            cmbCustomer.Enabled = editable;
            cmbWarehouse.Enabled = editable;

            txtRemarks.ReadOnly = !editable;

            dgvSaleOrderDetails.ReadOnly = !editable;

            btnSave.Enabled = editable;
            btnApprove.Enabled = editable;
        }

        private void dgvSaleOrderDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSaleOrderDetails.IsCurrentCellDirty)
            {
                dgvSaleOrderDetails.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvSaleOrderDetails_CellEnter(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvSaleOrderDetails.Columns[e.ColumnIndex].Name == "ProductID")
            {
                dgvSaleOrderDetails.BeginEdit(true);

                if (dgvSaleOrderDetails.EditingControl is ComboBox combo)
                {
                    combo.DroppedDown = true;
                }
            }
        }

        private void dgvSaleOrderDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvSaleOrderDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            if (!string.Equals(_currentSOStatus, "OPEN",
                               StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Approved or partial Sales Orders cannot be modified.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                SetSaleOrderEditable(false);
                return;
            }
            else
            {
                SetSaleOrderEditable(true);
            }

            dgvSaleOrderDetails.Rows.RemoveAt(e.RowIndex);

            CalculateSaleOrderTotals();
        }

        private DataTable GetOpenSalesOrderDetails(int soID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT
            sod.SODetailID,
            sod.SOID,
            sod.ProductID,

            p.ProductCode,
            p.ProductName,

            sod.OrderedQty,
            sod.DeliveredQty,

            (sod.OrderedQty - sod.DeliveredQty) AS RemainingQty,

            sod.UnitPrice,
            sod.DiscountAmount,
            sod.TaxAmount,

            (
                (sod.OrderedQty * sod.UnitPrice)
                - sod.DiscountAmount
                + sod.TaxAmount
            ) AS NetAmount

            FROM dbo.SaleOrderDetails sod

            INNER JOIN dbo.Products p
            ON p.ProductID = sod.ProductID

            WHERE sod.SOID = @SOID
              AND sod.OrderedQty > 0

            ORDER BY sod.SODetailID;", cn))
            {
                cmd.Parameters.Add(
                    "@SOID",
                    SqlDbType.Int).Value = soID;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // ----------------------------------------------------------
        // Forms Button Click Event Handlers
        // ----------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_ORDER.SAVE"))
            {
                MessageBox.Show("You do not have permission to save sales.");
                return;
            }
            CommitSaleOrderGridEdit();
            if (!ValidateSaleOrderDetails())
                return;
            SaveSaleOrder();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_ORDER.NEW"))
            {
                MessageBox.Show("You do not have permission to create new sales.");
                return;
            }
            NewSaleOrder();
            btnSave.Enabled = true;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_ORDER.APPROVE"))
            {
                MessageBox.Show("You do not have permission to approve sales.");
                return;
            }
            if (_currentSOID <= 0)
            {
                MessageBox.Show(
                    "Please save the Sales Order first.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ApproveSaleOrder();
        }

        private void dgvOrders_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            SelectedSOID = Convert.ToInt32(
                dgvSaleOrderDetails.Rows[e.RowIndex]
                    .Cells["SOID"].Value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_ORDER.SEARCH"))
            {
                MessageBox.Show("You do not have permission to search sales.");
                return;
            }
            using (FrmSaleOrderSearch frm =
                   new FrmSaleOrderSearch())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadSalesOrder(frm.SelectedSOID);
                }
            }
        }

        private void btnLoadSaleOrder_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_ORDER.LOAD"))
            {
                MessageBox.Show("You do not have permission to load sales.");
                return;
            }
            using (FrmApprovedSaleOrderSearch frm = new FrmApprovedSaleOrderSearch())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadSalesOrderForDelivery(
                        frm.SelectedSOID);
                }
            }
        }
    }
}
