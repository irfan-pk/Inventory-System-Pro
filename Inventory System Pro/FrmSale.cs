using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmSales : Form
    {
        private int _currentSaleID = 0;
        private int _currentSOID = 0;
        private string _currentSONumber = "";
        private string _currentSOStatus = "";
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmSales()
        {
            InitializeComponent();
        }
        // --------------------------------------------------
        // Procedure to prepare the form for a new sale entry
        // --------------------------------------------------
        private void PrepareNewSale()
        {
            _currentSaleID = 0;
            _currentSOID = 0;
            _currentSONumber = "";
            _currentSOStatus = "";
            txtSONumber.Text = "";

            dtpSaleDate.Value = DateTime.Now;

            cboCustomer.SelectedIndex = -1;
            cboWarehouse.SelectedIndex = -1;

            dgvSaleDetails.Rows.Clear();

            txtGrossAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtPaidAmount.Text = "0.00";
            txtBalanceAmount.Text = "0.00";

            LoadNextSaleNumber();

            txtSaleNumber.Focus();
        }

        private void FormatSaleDetailsGrid()
        {
            dgvSaleDetails.AllowUserToAddRows = false;
            dgvSaleDetails.AllowUserToDeleteRows = true;
            dgvSaleDetails.RowHeadersVisible = false;
            dgvSaleDetails.MultiSelect = false;
            dgvSaleDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvSaleDetails.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            // Sales Order Detail ID
            if (dgvSaleDetails.Columns.Contains("SODetailID"))
            {
                dgvSaleDetails.Columns["SODetailID"].HeaderText =
                    "SO Detail ID";

                dgvSaleDetails.Columns["SODetailID"].Width =
                    100;

                dgvSaleDetails.Columns["SODetailID"].Visible =
                    false;

                dgvSaleDetails.Columns["SODetailID"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            // Product ID
            if (dgvSaleDetails.Columns.Contains("ProductID"))
            {
                dgvSaleDetails.Columns["ProductID"].Visible = false;
            }

            // Product Code
            if (dgvSaleDetails.Columns.Contains("ProductCode"))
            {
                dgvSaleDetails.Columns["ProductCode"].HeaderText =
                    "Code";

                dgvSaleDetails.Columns["ProductCode"].Width =
                    100;
            }

            // Product Name
            if (dgvSaleDetails.Columns.Contains("ProductName"))
            {
                dgvSaleDetails.Columns["ProductName"].HeaderText =
                    "Product";

                dgvSaleDetails.Columns["ProductName"].Width =
                    250;
            }

            // Quantity
            if (dgvSaleDetails.Columns.Contains("Qty"))
            {
                dgvSaleDetails.Columns["Qty"].HeaderText =
                    "Qty";

                dgvSaleDetails.Columns["Qty"].Width =
                    90;

                dgvSaleDetails.Columns["Qty"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleDetails.Columns["Qty"]
                    .DefaultCellStyle.Format =
                    "N3";
            }

            // Unit Price
            if (dgvSaleDetails.Columns.Contains("UnitPrice"))
            {
                dgvSaleDetails.Columns["UnitPrice"].HeaderText =
                    "Unit Price";

                dgvSaleDetails.Columns["UnitPrice"].Width =
                    110;

                dgvSaleDetails.Columns["UnitPrice"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleDetails.Columns["UnitPrice"]
                    .DefaultCellStyle.Format =
                    "N2";
            }

            // Discount
            if (dgvSaleDetails.Columns.Contains("DiscountAmount"))
            {
                dgvSaleDetails.Columns["DiscountAmount"].HeaderText =
                    "Discount";

                dgvSaleDetails.Columns["DiscountAmount"].Width =
                    110;

                dgvSaleDetails.Columns["DiscountAmount"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleDetails.Columns["DiscountAmount"]
                    .DefaultCellStyle.Format =
                    "N2";
            }

            // Tax
            if (dgvSaleDetails.Columns.Contains("TaxAmount"))
            {
                dgvSaleDetails.Columns["TaxAmount"].HeaderText =
                    "Tax";

                dgvSaleDetails.Columns["TaxAmount"].Width =
                    100;

                dgvSaleDetails.Columns["TaxAmount"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleDetails.Columns["TaxAmount"]
                    .DefaultCellStyle.Format =
                    "N2";
            }

            // Net
            if (dgvSaleDetails.Columns.Contains("NetAmount"))
            {
                dgvSaleDetails.Columns["NetAmount"].HeaderText =
                    "Net Amount";

                dgvSaleDetails.Columns["NetAmount"].Width =
                    120;

                dgvSaleDetails.Columns["NetAmount"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvSaleDetails.Columns["NetAmount"]
                    .DefaultCellStyle.Format =
                    "N2";
            }

            dgvSaleDetails.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvSaleDetails.RowTemplate.Height = 28;

            // Formatting the grid after adding columns
            // Header alignment
            dgvSaleDetails.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Add Delete button as the last column
            if (!dgvSaleDetails.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteColumn =
                    new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "",
                        Text = "✕",
                        UseColumnTextForButtonValue = true,
                        Width = 45,
                        FlatStyle = FlatStyle.Flat,
                        SortMode =
                        DataGridViewColumnSortMode.NotSortable
                    };

                dgvSaleDetails.Columns.Add(deleteColumn);
            }
            dgvSaleDetails.RowTemplate.Height = 28;
        }

        private void AddProductToSale(
            int productID,
            string productCode,
            string productName,
            decimal unitPrice)
        {
            // Safety check
            if (dgvSaleDetails.Columns.Count == 0)
            {
                ConfigureSaleDetailsGrid();
            }

            // Check whether product already exists
            foreach (DataGridViewRow existingRow
                     in dgvSaleDetails.Rows)
            {
                if (existingRow.IsNewRow)
                    continue;

                if (existingRow.Cells["ProductID"].Value != null &&
                    Convert.ToInt32(
                        existingRow.Cells["ProductID"].Value)
                        == productID)
                {
                    MessageBox.Show(
                        "This product is already in the sale.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    existingRow.Selected = true;

                    return;
                }
            }

            int rowIndex =
                dgvSaleDetails.Rows.Add();

            DataGridViewRow row =
                dgvSaleDetails.Rows[rowIndex];

            row.Cells["ProductID"].Value =
                productID;

            row.Cells["ProductCode"].Value =
                productCode;

            row.Cells["ProductName"].Value =
                productName;

            row.Cells["Qty"].Value =
                1.000m;

            row.Cells["UnitPrice"].Value =
                unitPrice;

            row.Cells["DiscountAmount"].Value =
                0.00m;

            row.Cells["TaxAmount"].Value =
                0.00m;

            row.Cells["NetAmount"].Value =
                unitPrice;

            CalculateSaleTotals();

            dgvSaleDetails.CurrentCell =
                row.Cells["Qty"];

            dgvSaleDetails.BeginEdit(true);
        }

        private void CalculateSaleRow(
            DataGridViewRow row)
        {
            decimal qty =
                GetDecimal(
                    row.Cells["Qty"].Value);

            decimal unitPrice =
                GetDecimal(
                    row.Cells["UnitPrice"].Value);

            decimal discount =
                GetDecimal(
                    row.Cells["DiscountAmount"].Value);

            decimal tax =
                GetDecimal(
                    row.Cells["TaxAmount"].Value);

            decimal gross =
                qty * unitPrice;

            decimal net =
                gross - discount + tax;

            row.Cells["NetAmount"].Value =
                net;
        }

        private decimal GetDecimal(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return 0m;
            }


            if (decimal.TryParse(
                    value.ToString(),
                    out decimal result))
            {
                return result;
            }

            return 0m;
        }

        private void CalculateSaleTotals()
        {
            decimal gross = 0m;
            decimal discount = 0m;
            decimal tax = 0m;
            decimal net = 0m;

            foreach (DataGridViewRow row
                     in dgvSaleDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(
                        row.Cells["Qty"].Value);

                decimal unitPrice =
                    GetDecimal(
                        row.Cells["UnitPrice"].Value);

                decimal rowDiscount =
                    GetDecimal(
                        row.Cells["DiscountAmount"].Value);

                decimal rowTax =
                    GetDecimal(
                        row.Cells["TaxAmount"].Value);

                decimal rowGross =
                    qty * unitPrice;

                decimal rowNet =
                    rowGross -
                    rowDiscount +
                    rowTax;

                gross += rowGross;
                discount += rowDiscount;
                tax += rowTax;
                net += rowNet;
            }

            txtGrossAmount.Text =
                gross.ToString("N2");

            txtDiscountAmount.Text =
                discount.ToString("N2");

            txtTaxAmount.Text =
                tax.ToString("N2");

            txtNetAmount.Text =
                net.ToString("N2");

            decimal paid =
                GetDecimal(txtPaidAmount.Text);

            decimal balance =
                net - paid;

            txtBalanceAmount.Text =
                balance.ToString("N2");
        }

        private bool ValidateSale()
        {
            if (string.IsNullOrWhiteSpace(txtSaleNumber.Text))
            {
                MessageBox.Show(
                    "Please enter sale number.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSaleNumber.Focus();
                return false;
            }

            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select customer.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboCustomer.Focus();
                return false;
            }

            if (cboWarehouse.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select warehouse.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboWarehouse.Focus();
                return false;
            }

            if (dgvSaleDetails.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Please add at least one product.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            foreach (DataGridViewRow row
                     in dgvSaleDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row.Cells["Qty"].Value);

                decimal unitPrice =
                    GetDecimal(row.Cells["UnitPrice"].Value);

                if (qty <= 0)
                {
                    MessageBox.Show(
                        "Quantity must be greater than zero.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleDetails.CurrentCell =
                        row.Cells["Qty"];

                    dgvSaleDetails.BeginEdit(true);

                    return false;
                }

                if (unitPrice < 0)
                {
                    MessageBox.Show(
                        "Unit price cannot be negative.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvSaleDetails.CurrentCell =
                        row.Cells["UnitPrice"];

                    dgvSaleDetails.BeginEdit(true);

                    return false;
                }

                decimal discount =
                    GetDecimal(
                        row.Cells["DiscountAmount"].Value);

                decimal tax =
                    GetDecimal(
                        row.Cells["TaxAmount"].Value);

                if (discount < 0)
                {
                    MessageBox.Show(
                        "Discount cannot be negative.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                if (tax < 0)
                {
                    MessageBox.Show(
                        "Tax cannot be negative.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                decimal gross =
                    qty * unitPrice;

                if (discount > gross)
                {
                    MessageBox.Show(
                        "Discount cannot be greater than the item amount.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }
            }

            decimal netAmount =
                GetDecimal(txtNetAmount.Text);

            decimal paidAmount =
                GetDecimal(txtPaidAmount.Text);

            if (paidAmount < 0)
            {
                MessageBox.Show(
                    "Paid amount cannot be negative.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPaidAmount.Focus();
                return false;
            }

            if (paidAmount > netAmount)
            {
                MessageBox.Show(
                    "Paid amount cannot be greater than net amount.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPaidAmount.Focus();
                return false;
            }

            return true;
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
                    CustomerCode + ' - ' + CustomerName
                        AS CustomerDisplay
                FROM dbo.Customers
                WHERE IsActive = 1
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

                cboCustomer.DataSource = null;

                cboCustomer.DisplayMember =
                    "CustomerDisplay";

                cboCustomer.ValueMember =
                    "CustomerID";

                cboCustomer.DataSource = dt;

                cboCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Load Customers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    WarehouseID,
                    WarehouseName
                FROM dbo.Warehouses
                WHERE IsActive = 1
                ORDER BY WarehouseName;";

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

                cboWarehouse.DataSource = null;

                cboWarehouse.DisplayMember =
                    "WarehouseName";

                cboWarehouse.ValueMember =
                    "WarehouseID";

                cboWarehouse.DataSource = dt;

                cboWarehouse.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Load Warehouses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private DataTable BuildSaleDetailsTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SODetailID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("DiscountAmount", typeof(decimal));
            dt.Columns.Add("TaxAmount", typeof(decimal));

            foreach (DataGridViewRow row in dgvSaleDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                DataRow dr = dt.NewRow();

                int soDetailID = 0;

                // Sales Order delivery
                if (_currentSOID > 0)
                {
                    if (row.Cells["SODetailID"].Value != null &&
                        row.Cells["SODetailID"].Value != DBNull.Value)
                    {
                        int.TryParse(
                            row.Cells["SODetailID"].Value.ToString(),
                            out soDetailID);
                    }
                }

                dr["SODetailID"] = soDetailID;

                dr["ProductID"] =
                    Convert.ToInt32(
                        row.Cells["ProductID"].Value);

                dr["Qty"] =
                    GetDecimal(
                        row.Cells["Qty"].Value);

                dr["UnitPrice"] =
                    GetDecimal(
                        row.Cells["UnitPrice"].Value);

                dr["DiscountAmount"] =
                    GetDecimal(
                        row.Cells["DiscountAmount"].Value);

                dr["TaxAmount"] =
                    GetDecimal(
                        row.Cells["TaxAmount"].Value);

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void SaveSale()
        {
            // Capture Sales Order state before any validation or grid processing
            int saleOrderID = _currentSOID;
            string saleOrderNumber = _currentSONumber;
            string saleOrderStatus = _currentSOStatus;

            if (!ValidateSale())
                return;

            if (!int.TryParse(
                    cboCustomer.SelectedValue?.ToString(),
                    out int customerID))
            {
                MessageBox.Show(
                    "Invalid customer.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    cboWarehouse.SelectedValue?.ToString(),
                    out int warehouseID))
            {
                MessageBox.Show(
                    "Invalid warehouse.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // -----------------------------------------
                // DETAIL DATA
                // -----------------------------------------

                DataTable details =
                    BuildSaleDetailsTable();

                // -----------------------------------------
                // DEBUGGING INFORMATION
                // -----------------------------------------

                //MessageBox.Show(
                //        "DEBUG BEFORE SQL\r\n\r\n" +
                //        "SOID = " + _currentSOID +
                //        "\r\nSO Number = " + _currentSONumber +
                //        "\r\nSO Status = " + _currentSOStatus +
                //        "\r\nSODetailID = " + details.Rows[0]["SODetailID"].ToString(),
                //        "DEBUG",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);

                //if (_currentSOID > 0)
                //{
                //    MessageBox.Show(
                //        "SOID = " + _currentSOID +
                //        "\nSO Number = " + _currentSONumber +
                //        "\nSO Status = " + _currentSOStatus +
                //        "\nSODetailID = " +
                //        details.Rows[0]["SODetailID"].ToString(),
                //        "SO Delivery Debug",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);
                //}

                // -----------------------------------------
                // SALES ORDER VALIDATION
                // -----------------------------------------

                if (saleOrderID  > 0)
                {
                    if (string.IsNullOrWhiteSpace(saleOrderNumber ))
                    {
                        MessageBox.Show(
                            "Sales Order information is missing.",
                            "Sales Order",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    if (saleOrderStatus != "APPROVED" &&
                        saleOrderStatus != "PARTIAL")
                    {
                        MessageBox.Show(
                            "Only APPROVED or PARTIAL Sales Orders can be delivered.",
                            "Sales Order",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    // Every detail row must have a valid SODetailID.
                    foreach (DataRow detailRow in details.Rows)
                    {
                        int soDetailID =
                            Convert.ToInt32(
                                detailRow["SODetailID"]);

                        if (soDetailID <= 0)
                        {
                            MessageBox.Show(
                                "Invalid Sales Order Detail ID found.",
                                "Sales Order",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }
                }

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_Sale_Save",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        // -----------------------------------------
                        // HEADER
                        // -----------------------------------------

                        cmd.Parameters.Add(
                            "@SaleDate",
                            SqlDbType.Date).Value =
                            dtpSaleDate.Value.Date;

                        cmd.Parameters.Add(
                            "@SaleNumber",
                            SqlDbType.NVarChar, 30).Value =
                            txtSaleNumber.Text.Trim();

                        cmd.Parameters.Add(
                            "@CustomerID",
                            SqlDbType.Int).Value =
                            customerID;

                        cmd.Parameters.Add(
                            "@WarehouseID",
                            SqlDbType.Int).Value =
                            warehouseID;

                        // -----------------------------------------
                        // SALES ORDER
                        // -----------------------------------------

                        cmd.Parameters.Add(
                            "@SOID",
                            SqlDbType.Int).Value =
                            saleOrderID > 0
                                ? (object)saleOrderID
                                : DBNull.Value;

                        // -----------------------------------------
                        // PAYMENT
                        // -----------------------------------------

                        SqlParameter paidParameter =
                            cmd.Parameters.Add(
                                "@PaidAmount",
                                SqlDbType.Decimal);

                        paidParameter.Precision = 18;
                        paidParameter.Scale = 2;
                        paidParameter.Value =
                            GetDecimal(txtPaidAmount.Text);

                        // -----------------------------------------
                        // REMARKS
                        // -----------------------------------------

                        cmd.Parameters.Add(
                            "@Remarks",
                            SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrWhiteSpace(
                                txtRemarks.Text)
                                ? (object)DBNull.Value
                                : txtRemarks.Text.Trim();

                        // -----------------------------------------
                        // CREATED BY
                        // -----------------------------------------

                        cmd.Parameters.Add(
                            "@CreatedBy",
                            SqlDbType.Int).Value =
                            1; // change to logged-in user ID

                        // -----------------------------------------
                        // TVP
                        // -----------------------------------------

                        SqlParameter detailParameter =
                            cmd.Parameters.Add(
                                "@Details",
                                SqlDbType.Structured);

                        detailParameter.TypeName =
                            "dbo.SaleDetailType";

                        detailParameter.Value =
                            details;

                        // -----------------------------------------
                        // EXECUTE
                        // -----------------------------------------

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Sale could not be saved.",
                                    "Sale",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            string result =
                                reader["Result"]?.ToString();

                            if (result != "SUCCESS")
                            {
                                MessageBox.Show(
                                    "Sale could not be saved.",
                                    "Sale",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            long saleID =
                                Convert.ToInt64(
                                    reader["SaleID"]);

                            string saleNumber =
                                reader["SaleNumber"]?.ToString();

                            decimal gross =
                                GetDecimal(
                                    reader["GrossAmount"]);

                            decimal savedDiscount =
                                GetDecimal(
                                    reader["DiscountAmount"]);

                            decimal savedTax =
                                GetDecimal(
                                    reader["TaxAmount"]);

                            decimal net =
                                GetDecimal(
                                    reader["NetAmount"]);

                            decimal paid =
                                GetDecimal(
                                    reader["PaidAmount"]);

                            decimal balance =
                                GetDecimal(
                                    reader["BalanceAmount"]);

                            // -----------------------------------------
                            // DISPLAY TOTALS
                            // -----------------------------------------

                            txtGrossAmount.Text =
                                gross.ToString("N2");

                            txtDiscountAmount.Text =
                                savedDiscount.ToString("N2");

                            txtTaxAmount.Text =
                                savedTax.ToString("N2");

                            txtNetAmount.Text =
                                net.ToString("N2");

                            txtPaidAmount.Text =
                                paid.ToString("N2");

                            txtBalanceAmount.Text =
                                balance.ToString("N2");

                            _currentSaleID =
                                (int)saleID;

                            // -----------------------------------------
                            // SUCCESS MESSAGE
                            // -----------------------------------------

                            string message =
                                "Sale saved successfully.\n\n" +
                                "Sale No: " +
                                saleNumber +
                                "\nNet Amount: " +
                                net.ToString("N2") +
                                "\nPaid: " +
                                paid.ToString("N2") +
                                "\nBalance: " +
                                balance.ToString("N2");

                            if (saleOrderID > 0)
                            {
                                message +=
                                    "\n\nSales Order: " +
                                    saleOrderNumber;
                            }

                            MessageBox.Show(
                                message,
                                "Sale",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            btnSave.Enabled = true;
                            btnPrint.Enabled = true;
                        }
                    }
                }
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
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PrintSale(int saleID)
        {
            // ------------------------------------------------------------------------------------
            // Check if company information is available before proceeding with the print operation
            // ------------------------------------------------------------------------------------
            DataRow company = GetCompany();

            if (company == null)
            {
                MessageBox.Show(
                    "No active company information found.",
                    "Print Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string companyName =
    company["CompanyName"] == DBNull.Value
        ? ""
        : company["CompanyName"].ToString();

            string companyAddress =
                company["Address"] == DBNull.Value
                    ? ""
                    : company["Address"].ToString();

            string companyPhone =
                company["Phone"] == DBNull.Value
                    ? ""
                    : company["Phone"].ToString();

            string companyEmail =
                company["Email"] == DBNull.Value
                    ? ""
                    : company["Email"].ToString();

            string companyNTN =
                company["NTN"] == DBNull.Value
                    ? ""
                    : company["NTN"].ToString();

            string companySTRN =
                company["STRN"] == DBNull.Value
                    ? ""
                    : company["STRN"].ToString();

            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    S.SaleID,
                    S.SaleNumber,
                    S.SaleDate,
                    S.CustomerID,
                    C.CustomerCode,
                    C.CustomerName,
                    S.WarehouseID,
                    W.WarehouseName,
                    S.SOID,
                    SO.SONumber,
                    S.GrossAmount,
                    S.DiscountAmount,
                    S.TaxAmount,
                    S.NetAmount,
                    S.PaidAmount,
                    S.BalanceAmount
                FROM dbo.Sales AS S

                INNER JOIN dbo.Customers AS C
                    ON C.CustomerID = S.CustomerID

                INNER JOIN dbo.Warehouses AS W
                    ON W.WarehouseID = S.WarehouseID

                LEFT JOIN dbo.SaleOrders AS SO
                    ON SO.SOID = S.SOID

                WHERE S.SaleID = @SaleID;";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SaleID",
                            SqlDbType.Int).Value = saleID;

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Sale record not found.",
                        "Print Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataRow sale = dt.Rows[0];

                DataTable details = GetSaleDetails(saleID);

                if (details.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No sale details found.",
                        "Print Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // -------------------------------------------------
                // Create PDF
                // -------------------------------------------------

                PdfDocument document = new PdfDocument();

                document.Info.Title =
                    "Sale - " + sale["SaleNumber"].ToString();

                PdfPage page = document.AddPage();

                page.Size = PdfSharp.PageSize.A4;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                // -------------------------------------------------
                // Fonts
                // -------------------------------------------------

                XFont fontTitle =
                    new XFont(
                        "Arial",
                        18,
                        XFontStyleEx.Bold);

                XFont fontHeader =
                    new XFont(
                        "Arial",
                        10,
                        XFontStyleEx.Bold);

                XFont fontNormal =
                    new XFont(
                        "Arial",
                        9,
                        XFontStyleEx.Regular);

                XFont fontSmall =
                    new XFont(
                        "Arial",
                        8,
                        XFontStyleEx.Regular);

                // -------------------------------------------------
                // Page setup
                // -------------------------------------------------

                double left = 40;
                double right = page.Width.Point - 40;
                double y = 40;
                double footerY = page.Height.Point - 55;

                // -------------------------------------------------
                // Company Header
                // -------------------------------------------------

                double companyHeaderHeight = 25;

                // Company Name
                gfx.DrawString(
                    companyName,
                    fontTitle,
                    XBrushes.Black,
                    new XRect(
                        left,
                        y,
                        right - left,
                        companyHeaderHeight),
                    XStringFormats.TopCenter);

                y += 28;

                // Address
                if (!string.IsNullOrWhiteSpace(companyAddress))
                {
                    gfx.DrawString(
                        companyAddress,
                        fontNormal,
                        XBrushes.Black,
                        new XRect(
                            left,
                            y,
                            right - left,
                            18),
                        XStringFormats.TopCenter);

                    y += 18;
                }

                // Phone + Email
                string contactLine = "";

                if (!string.IsNullOrWhiteSpace(companyPhone))
                    contactLine = "Phone: " + companyPhone;

                if (!string.IsNullOrWhiteSpace(companyEmail))
                {
                    if (!string.IsNullOrWhiteSpace(contactLine))
                        contactLine += "    |    ";

                    contactLine += "Email: " + companyEmail;
                }

                if (!string.IsNullOrWhiteSpace(contactLine))
                {
                    gfx.DrawString(
                        contactLine,
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            left,
                            y,
                            right - left,
                            16),
                        XStringFormats.TopCenter);

                    y += 16;
                }

                // NTN + STRN
                string taxLine = "";

                if (!string.IsNullOrWhiteSpace(companyNTN))
                    taxLine = "NTN: " + companyNTN;

                if (!string.IsNullOrWhiteSpace(companySTRN))
                {
                    if (!string.IsNullOrWhiteSpace(taxLine))
                        taxLine += "    |    ";

                    taxLine += "STRN: " + companySTRN;
                }

                if (!string.IsNullOrWhiteSpace(taxLine))
                {
                    gfx.DrawString(
                        taxLine,
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            left,
                            y,
                            right - left,
                            16),
                        XStringFormats.TopCenter);

                    y += 18;
                }

                // Separator
                gfx.DrawLine(
                    XPens.Black,
                    left,
                    y,
                    right,
                    y);

                y += 15;

                // -------------------------------------------------
                // Professional Sale Information
                // -------------------------------------------------

                double infoBoxHeight = 82;

                // Outer information box
                gfx.DrawRectangle(
                    XPens.Black,
                    left,
                    y,
                    right - left,
                    infoBoxHeight);

                // -------------------------------------------------
                // SALE INVOICE
                // -------------------------------------------------

                gfx.DrawString(
                    "SALE INVOICE",
                    fontTitle,
                    XBrushes.Black,
                    new XRect(
                        left + 10,
                        y + 8,
                        220,
                        22),
                    XStringFormats.TopLeft);

                // -------------------------------------------------
                // Sale Number
                // -------------------------------------------------

                gfx.DrawString(
                    "Sale No:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        left + 300,
                        y + 8,
                        60,
                        16),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    sale["SaleNumber"].ToString(),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        left + 360,
                        y + 8,
                        120,
                        16),
                    XStringFormats.TopLeft);

                // -------------------------------------------------
                // Customer Code
                // -------------------------------------------------

                gfx.DrawString(
                    "Customer Code:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        left + 10,
                        y + 35,
                        90,
                        16),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    sale["CustomerCode"].ToString(),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        left + 100,
                        y + 35,
                        180,
                        16),
                    XStringFormats.TopLeft);

                // -------------------------------------------------
                // Date
                // -------------------------------------------------

                gfx.DrawString(
                    "Date:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        left + 300,
                        y + 27,
                        60,
                        16),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDateTime(
                        sale["SaleDate"])
                        .ToString("yyyy-MM-dd"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        left + 360,
                        y + 27,
                        120,
                        16),
                    XStringFormats.TopLeft);

                // -------------------------------------------------
                // Customer
                // -------------------------------------------------

                gfx.DrawString(
                    "Customer:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        left + 10,
                        y + 57,
                        90,
                        16),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    sale["CustomerName"].ToString(),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        left + 100,
                        y + 57,
                        180,
                        16),
                    XStringFormats.TopLeft);

                // -------------------------------------------------
                // S.O Number
                // -------------------------------------------------

                if (sale["SONumber"] != DBNull.Value &&
                    !string.IsNullOrWhiteSpace(
                        sale["SONumber"].ToString()))
                {
                    gfx.DrawString(
                        "S.O No:",
                        fontHeader,
                        XBrushes.Black,
                        new XRect(
                            left + 300,
                            y + 46,
                            60,
                            16),
                        XStringFormats.TopLeft);

                    gfx.DrawString(
                        sale["SONumber"].ToString(),
                        fontNormal,
                        XBrushes.Black,
                        new XRect(
                            left + 360,
                            y + 46,
                            120,
                            16),
                        XStringFormats.TopLeft);
                }

                // -------------------------------------------------
                // Warehouse
                // -------------------------------------------------

                gfx.DrawString(
                    "Warehouse:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        left + 300,
                        y + 64,
                        70,
                        16),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    sale["WarehouseName"].ToString(),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        left + 370,
                        y + 64,
                        120,
                        16),
                    XStringFormats.TopLeft);

                // Move below information box
                y += infoBoxHeight + 15;

                // -------------------------------------------------
                // Professional Detail Table
                // -------------------------------------------------

                double tableWidth = right - left;

                // -------------------------------------------------
                // Column positions
                // -------------------------------------------------

                double colCode = left;
                double colProduct = left + 65;
                double colQty = left + 230;
                double colPrice = left + 280;
                double colDiscount = left + 330;
                double colTax = left + 400;
                double colNet = left + 490;

                // -------------------------------------------------
                // Column widths
                // -------------------------------------------------

                double widthCode = 65;
                double widthProduct = 215;
                double widthQty = 55;
                double widthPrice = 65;
                double widthDiscount = 65;
                double widthTax = 35;
                double widthNet = right - colNet;

                // -------------------------------------------------
                // Table Header
                // -------------------------------------------------

                double headerHeight = 24;

                gfx.DrawRectangle(
                    XPens.Black,
                    left,
                    y,
                    tableWidth,
                    headerHeight);

                // Header labels

                gfx.DrawString(
                    "Code",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colCode + 4,
                        y + 5,
                        widthCode - 8,
                        15),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    "Product",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colProduct + 4,
                        y + 5,
                        widthProduct - 8,
                        15),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    "Qty",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colQty,
                        y + 5,
                        widthQty,
                        15),
                    XStringFormats.TopRight);

                gfx.DrawString(
                    "Price",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colPrice,
                        y + 5,
                        widthPrice,
                        15),
                    XStringFormats.TopRight);

                gfx.DrawString(
                    "Disc.",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colDiscount,
                        y + 5,
                        widthDiscount,
                        15),
                    XStringFormats.TopRight);

                gfx.DrawString(
                    "Tax",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colTax,
                        y + 5,
                        widthTax,
                        15),
                    XStringFormats.TopRight);

                gfx.DrawString(
                    "Net",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        colNet,
                        y + 5,
                        widthNet - 4,
                        15),
                    XStringFormats.TopRight);

                y += headerHeight;

                // -------------------------------------------------
                // Detail Rows
                // -------------------------------------------------

                foreach (DataRow detail in details.Rows)
                {
                    double rowHeight = 22;

                    // -------------------------------------------------
                    // Page overflow
                    // -------------------------------------------------

                    if (y + rowHeight > page.Height.Point - 150)
                    {
                        gfx.Dispose();

                        page = document.AddPage();
                        page.Size = PdfSharp.PageSize.A4;

                        gfx = XGraphics.FromPdfPage(page);

                        y = 40;

                        gfx.DrawString(
                            "SALE - " + sale["SaleNumber"].ToString(),
                            fontHeader,
                            XBrushes.Black,
                            left,
                            y);

                        y += 25;

                        // Re-draw table header on new page

                        gfx.DrawRectangle(
                            XPens.Black,
                            left,
                            y,
                            tableWidth,
                            headerHeight);

                        gfx.DrawString(
                            "Code",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colCode + 4,
                                y + 5,
                                widthCode - 8,
                                15),
                            XStringFormats.TopLeft);

                        gfx.DrawString(
                            "Product",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colProduct + 4,
                                y + 5,
                                widthProduct - 8,
                                15),
                            XStringFormats.TopLeft);

                        gfx.DrawString(
                            "Qty",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colQty,
                                y + 5,
                                widthQty,
                                15),
                            XStringFormats.TopRight);

                        gfx.DrawString(
                            "Price",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colPrice,
                                y + 5,
                                widthPrice,
                                15),
                            XStringFormats.TopRight);

                        gfx.DrawString(
                            "Disc.",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colDiscount,
                                y + 5,
                                widthDiscount,
                                15),
                            XStringFormats.TopRight);

                        gfx.DrawString(
                            "Tax",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colTax,
                                y + 5,
                                widthTax,
                                15),
                            XStringFormats.TopRight);

                        gfx.DrawString(
                            "Net",
                            fontHeader,
                            XBrushes.Black,
                            new XRect(
                                colNet,
                                y + 5,
                                widthNet - 4,
                                15),
                            XStringFormats.TopRight);

                        y += headerHeight;
                    }

                    // -------------------------------------------------
                    // Row separator
                    // -------------------------------------------------

                    gfx.DrawLine(
                        XPens.LightGray,
                        left,
                        y + rowHeight,
                        right,
                        y + rowHeight);

                    // -------------------------------------------------
                    // Product Code
                    // -------------------------------------------------

                    gfx.DrawString(
                        detail["ProductCode"].ToString(),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colCode + 4,
                            y + 5,
                            widthCode - 8,
                            15),
                        XStringFormats.TopLeft);

                    // -------------------------------------------------
                    // Product Name
                    // -------------------------------------------------

                    gfx.DrawString(
                        detail["ProductName"].ToString(),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colProduct + 4,
                            y + 5,
                            widthProduct - 8,
                            15),
                        XStringFormats.TopLeft);

                    // -------------------------------------------------
                    // Quantity
                    // -------------------------------------------------

                    gfx.DrawString(
                        Convert.ToDecimal(
                            detail["Qty"]).ToString("N3"),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colQty,
                            y + 5,
                            widthQty,
                            15),
                        XStringFormats.TopRight);

                    // -------------------------------------------------
                    // Price
                    // -------------------------------------------------

                    gfx.DrawString(
                        Convert.ToDecimal(
                            detail["UnitPrice"]).ToString("N2"),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colPrice,
                            y + 5,
                            widthPrice,
                            15),
                        XStringFormats.TopRight);

                    // -------------------------------------------------
                    // Discount
                    // -------------------------------------------------

                    gfx.DrawString(
                        Convert.ToDecimal(
                            detail["DiscountAmount"]).ToString("N2"),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colDiscount,
                            y + 5,
                            widthDiscount,
                            15),
                        XStringFormats.TopRight);

                    // -------------------------------------------------
                    // Tax
                    // -------------------------------------------------

                    gfx.DrawString(
                        Convert.ToDecimal(
                            detail["TaxAmount"]).ToString("N2"),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colTax,
                            y + 5,
                            widthTax,
                            15),
                        XStringFormats.TopRight);

                    // -------------------------------------------------
                    // Net
                    // -------------------------------------------------

                    gfx.DrawString(
                        Convert.ToDecimal(
                            detail["NetAmount"]).ToString("N2"),
                        fontSmall,
                        XBrushes.Black,
                        new XRect(
                            colNet,
                            y + 5,
                            widthNet - 4,
                            15),
                        XStringFormats.TopRight);

                    y += rowHeight;
                }

                // -------------------------------------------------
                // Totals
                // -------------------------------------------------

                y += 15;

                double totalsX =
                    left + 350;

                gfx.DrawString(
                    "Gross Amount:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["GrossAmount"]).ToString("N2"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 18;

                gfx.DrawString(
                    "Discount:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["DiscountAmount"]).ToString("N2"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 18;

                gfx.DrawString(
                    "Tax:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["TaxAmount"]).ToString("N2"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 20;

                gfx.DrawLine(
                    XPens.LightGray,
                    totalsX - 30,
                    y,
                    totalsX + 165,
                    y);

                y += 8;

                gfx.DrawString(
                    "NET AMOUNT:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["NetAmount"]).ToString("N2"),
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 22;

                gfx.DrawString(
                    "Paid:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["PaidAmount"]).ToString("N2"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 18;

                gfx.DrawString(
                    "Balance:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        100,
                        18),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        sale["BalanceAmount"]).ToString("N2"),
                    fontNormal,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 20;

                gfx.DrawLine(
                    XPens.Black,
                    totalsX - 30,
                    y,
                    totalsX + 165,
                    y);

                y += 10;

                // -------------------------------------------------
                // Payment Status
                // -------------------------------------------------

                decimal netAmount =
                    Convert.ToDecimal(sale["NetAmount"]);

                decimal paidAmount =
                    Convert.ToDecimal(sale["PaidAmount"]);

                decimal balanceAmount =
                    Convert.ToDecimal(sale["BalanceAmount"]);

                string paymentStatus;

                if (balanceAmount <= 0.01m)
                {
                    paymentStatus = "PAID";
                }
                else if (paidAmount > 0)
                {
                    paymentStatus = "PARTIALLY PAID";
                }
                else
                {
                    paymentStatus = "UNPAID";
                }

                // Status label
                gfx.DrawString(
                    "PAYMENT STATUS:",
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX - 30,
                        y,
                        105,
                        18),
                    XStringFormats.TopLeft);

                // Status value
                gfx.DrawString(
                    paymentStatus,
                    fontHeader,
                    XBrushes.Black,
                    new XRect(
                        totalsX + 62,
                        y,
                        100,
                        18),
                    XStringFormats.TopRight);

                y += 25;

                // -------------------------------------------------
                // Professional Footer
                // -------------------------------------------------

                // Footer separator
                gfx.DrawLine(
                    XPens.Black,
                    left,
                    footerY,
                    right,
                    footerY);

                // Footer text
                gfx.DrawString(
                    "Generated by Inventory System Pro",
                    fontSmall,
                    XBrushes.Black,
                    new XRect(
                        left,
                        footerY + 8,
                        250,
                        15),
                    XStringFormats.TopLeft);

                // Page number
                //gfx.DrawString(
                //    "Page " + document.Pages.Count,
                //    fontSmall,
                //    XBrushes.Black,
                //    new XRect(
                //        right - 300,
                //        footerY + 8,
                //        100,
                //        15),
                //    XStringFormats.TopRight);

                gfx.Dispose();

                // -------------------------------------------------
                // Save PDF
                // -------------------------------------------------

                string folder =
                    Path.Combine(
                        Application.StartupPath,
                        "SaleReports");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName =
                    "Sale_" +
                    sale["SaleNumber"].ToString() +
                    ".pdf";

                string filePath =
                    Path.Combine(
                        folder,
                        fileName);

                // -------------------------------------------------
                // Add Footer to Every Page
                // -------------------------------------------------

                int totalPages = document.Pages.Count;

                for (int i = 0; i < totalPages; i++)
                {
                    PdfPage footerPage = document.Pages[i];

                    using (XGraphics footerGfx =
                           XGraphics.FromPdfPage(
                               footerPage,
                               XGraphicsPdfPageOptions.Append))
                    {

                        // Footer separator
                        footerGfx.DrawLine(
                            XPens.Black,
                            left,
                            footerY,
                            right,
                            footerY);

                        // Generated by
                        footerGfx.DrawString(
                            "Generated by LastHope Micro Tech.",
                            fontSmall,
                            XBrushes.Black,
                            new XRect(
                                left,
                                footerY + 8,
                                250,
                                15),
                            XStringFormats.TopLeft);

                        // Page X of Y
                        footerGfx.DrawString(
                            "Page " + (i + 1) + " of " + totalPages,
                            fontSmall,
                            XBrushes.Black,
                            new XRect(
                                right - 100,
                                footerY + 8,
                                100,
                                15),
                            XStringFormats.TopRight);
                    }
                }

                document.Save(filePath);

                document.Close();

                // -------------------------------------------------
                // Open PDF
                // -------------------------------------------------

                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Print Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private DataTable GetSaleDetails(int saleID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
        SELECT
            SD.ProductID,
            P.ProductCode,
            P.ProductName,
            SD.Qty,
            SD.UnitPrice,
            SD.DiscountAmount,
            SD.TaxAmount,
            (
                (SD.Qty * SD.UnitPrice)
                - SD.DiscountAmount
                + SD.TaxAmount
            ) AS NetAmount
        FROM dbo.SaleDetails AS SD
        INNER JOIN dbo.Products AS P
            ON P.ProductID = SD.ProductID
        WHERE SD.SaleID = @SaleID
        ORDER BY SD.SaleDetailID;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@SaleID",
                        SqlDbType.Int).Value = saleID;

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void OpenProductSelection()
        {
            using (FrmProductSearch frm =
                   new FrmProductSearch())
            {
                if (frm.ShowDialog() ==
                    DialogResult.OK)
                {
                    AddProductToSale(
                        frm.SelectedProductID,
                        frm.SelectedProductCode,
                        frm.SelectedProductName,
                        frm.SelectedSalePrice);
                }
            }
        }

        private DataRow GetCompany()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            using (SqlCommand cmd =
                   new SqlCommand(@"
                SELECT TOP 1
                    CompanyID,
                    CompanyName,
                    Address,
                    Phone,
                    Email,
                    NTN,
                    STRN
                FROM dbo.Company
                WHERE IsActive = 1
                ORDER BY CompanyID;", cn))
            {
                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }

        private void LoadNextSaleNumber()
        {
            try
            {
                int nextNumber = 1;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT ISNULL(
                    MAX(
                        TRY_CONVERT(
                            INT,
                            REPLACE(SaleNumber, 'SAL-', '')
                        )
                    ),
                    0
                ) + 1
                FROM dbo.Sales
                WHERE SaleNumber LIKE 'SAL-%';";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cn.Open();

                        nextNumber =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());
                    }
                }

                txtSaleNumber.Text =
                    "SAL-" + nextNumber.ToString("D6");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private decimal GetAvailableStock(int productID, int warehouseID)
        {
            decimal stock = 0;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    ISNULL(SUM(QtyIn), 0)
                    -
                    ISNULL(SUM(QtyOut), 0)
                FROM dbo.StockLedger
                WHERE ProductID = @ProductID
                  AND WarehouseID = @WarehouseID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@ProductID",
                            SqlDbType.Int).Value =
                            productID;

                        cmd.Parameters.Add(
                            "@WarehouseID",
                            SqlDbType.Int).Value =
                            warehouseID;

                        cn.Open();

                        object result =
                            cmd.ExecuteScalar();

                        if (result != null &&
                            result != DBNull.Value)
                        {
                            stock =
                                Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return stock;
        }

        private void ConfigureSaleDetailsGrid()
        {
            dgvSaleDetails.DataSource = null;
            dgvSaleDetails.Columns.Clear();

            dgvSaleDetails.AutoGenerateColumns = false;
            dgvSaleDetails.AllowUserToAddRows = false;
            dgvSaleDetails.AllowUserToDeleteRows = false;
            dgvSaleDetails.ReadOnly = false;
            dgvSaleDetails.MultiSelect = false;
            dgvSaleDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // ProductID
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductID",
                    HeaderText = "Product ID",
                    Visible = false
                });

            // SODetailID
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "SODetailID",
                    HeaderText = "SO Detail ID",
                    Visible = false
                });

            // Product Code
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductCode",
                    HeaderText = "Code",
                    ReadOnly = true,
                    Width = 100
                });

            // Product Name
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product",
                    ReadOnly = true,
                    Width = 250
                });

            // Quantity
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Qty",
                    HeaderText = "Qty",
                    Width = 80
                });

            // Unit Price
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "UnitPrice",
                    HeaderText = "Unit Price",
                    Width = 110
                });

            // Discount
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "DiscountAmount",
                    HeaderText = "Discount",
                    Width = 100
                });

            // Tax
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "TaxAmount",
                    HeaderText = "Tax",
                    Width = 100
                });

            // Net Amount
            dgvSaleDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "NetAmount",
                    HeaderText = "Net Amount",
                    ReadOnly = true,
                    Width = 120
                });

            // Numeric formatting
            dgvSaleDetails.Columns["Qty"]
                .DefaultCellStyle.Format = "N3";

            dgvSaleDetails.Columns["UnitPrice"]
                .DefaultCellStyle.Format = "N4";

            dgvSaleDetails.Columns["DiscountAmount"]
                .DefaultCellStyle.Format = "N2";

            dgvSaleDetails.Columns["TaxAmount"]
                .DefaultCellStyle.Format = "N2";

            dgvSaleDetails.Columns["NetAmount"]
                .DefaultCellStyle.Format = "N2";
        }

        // --------------------------------------------------
        // Form load event handler
        // --------------------------------------------------

        private void FrmSale_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadWarehouses();
            ConfigureSaleDetailsGrid();
            PrepareNewSale();
            FormatSaleDetailsGrid();
        }

        // --------------------------------------------------
        // Event Handlers
        // --------------------------------------------------

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("SALE.NEW"))
            {
                MessageBox.Show(
                    "You do not have permission to create a new sale.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (dgvSaleDetails.Rows.Count > 0)
            {
                DialogResult result =
                    MessageBox.Show(
                        "Start a new sale?\n\nCurrent unsaved details will be cleared.",
                        "New Sale",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }
            PrepareNewSale();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("SALE.SAVE"))
            {
                MessageBox.Show(
                    "You do not have permission to save sales.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            // Commit any active cell/row edit before reading grid values
            if (dgvSaleDetails.IsCurrentCellInEditMode)
            {
                dgvSaleDetails.EndEdit();
            }

            dgvSaleDetails.CommitEdit(
                DataGridViewDataErrorContexts.Commit);

            if (!ValidateSale())
                return;

            SaveSale();
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvSaleDetails.CurrentRow == null)
                return;

            if (dgvSaleDetails.CurrentRow.IsNewRow)
                return;

            DialogResult result =
                MessageBox.Show(
                    "Remove selected item?",
                    "Sale",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            dgvSaleDetails.Rows.Remove(
                dgvSaleDetails.CurrentRow);

            CalculateSaleTotals();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("SALE.PRINT"))
            {
                MessageBox.Show(
                    "You do not have permission to print sales.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (_currentSaleID <= 0)
            {
                MessageBox.Show(
                    "Please save the sale before printing.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            PrintSale(_currentSaleID);
        }

        // -------------------------------------------------------
        // Event handler for the KeyDown event of the DataGridView
        // -------------------------------------------------------

        private void dgvSaleDetails_CellEndEdit(
    object sender,
    DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.RowIndex >= dgvSaleDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvSaleDetails.Rows[e.RowIndex];

            if (row.IsNewRow)
                return;

            if (e.ColumnIndex !=
                dgvSaleDetails.Columns["Qty"].Index)
            {
                CalculateSaleRow(row);
                CalculateSaleTotals();
                return;
            }

            if (!int.TryParse(
                    cboWarehouse.SelectedValue?.ToString(),
                    out int warehouseID))
            {
                MessageBox.Show(
                    "Please select a warehouse first.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int productID =
                Convert.ToInt32(
                    row.Cells["ProductID"].Value);

            decimal requestedQty =
                GetDecimal(
                    row.Cells["Qty"].Value);

            if (requestedQty <= 0)
            {
                MessageBox.Show(
                    "Quantity must be greater than zero.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                row.Cells["Qty"].Value = 0;

                CalculateSaleRow(row);
                CalculateSaleTotals();

                return;
            }

            decimal availableQty =
                GetAvailableStock(
                    productID,
                    warehouseID);

            if (requestedQty > availableQty)
            {
                MessageBox.Show(
                    "Insufficient stock.\n\n" +
                    "Available: " +
                    availableQty.ToString("N3") +
                    "\nRequested: " +
                    requestedQty.ToString("N3"),
                    "Insufficient Stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                row.Cells["Qty"].Value =
                    availableQty;

                CalculateSaleRow(row);
                CalculateSaleTotals();

                return;
            }

            CalculateSaleRow(row);
            CalculateSaleTotals();
        }

        private void LoadSaleOrderForDelivery(int soID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                SO.SOID,
                SO.SONumber,
                SO.SODate,
                SO.CustomerID,
                SO.WarehouseID,
                SO.Status,
                SOD.SODetailID,
                SOD.ProductID,
                P.ProductCode,
                P.ProductName,
                SOD.OrderedQty,
                SOD.DeliveredQty,
                SOD.OrderedQty - SOD.DeliveredQty AS RemainingQty,
                SOD.UnitPrice,
                SOD.DiscountAmount,
                SOD.TaxAmount
                FROM dbo.SaleOrders AS SO
                INNER JOIN dbo.SaleOrderDetails AS SOD
                ON SOD.SOID = SO.SOID
                INNER JOIN dbo.Products AS P
                ON P.ProductID = SOD.ProductID
                WHERE SO.SOID = @SOID
                AND SO.Status IN ('APPROVED', 'PARTIAL')
                AND SOD.OrderedQty > SOD.DeliveredQty
                ORDER BY SOD.SODetailID;", con))
                {
                    cmd.Parameters.Add("@SOID", SqlDbType.Int).Value = soID;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show(
                                "This Sales Order has no remaining quantity for delivery.",
                                "Sales Order",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }

                        dgvSaleDetails.Rows.Clear();

                        bool firstRow = true;

                        while (reader.Read())
                        {
                            if (firstRow)
                            {
                                _currentSOID = Convert.ToInt32(reader["SOID"]);
                                _currentSONumber = reader["SONumber"].ToString();
                                _currentSOStatus = reader["Status"].ToString();
                                txtSONumber.Text = _currentSONumber;

                                // Load header values
                                cboCustomer.SelectedValue =
                                    Convert.ToInt32(reader["CustomerID"]);

                                cboWarehouse.SelectedValue =
                                    Convert.ToInt32(reader["WarehouseID"]);

                                firstRow = false;
                            }

                            decimal remainingQty =
                                Convert.ToDecimal(reader["RemainingQty"]);

                            int rowIndex = dgvSaleDetails.Rows.Add();

                            DataGridViewRow row =
                                dgvSaleDetails.Rows[rowIndex];

                            // Hidden ProductID
                            row.Cells["ProductID"].Value =
                                reader["ProductID"];

                            row.Cells["ProductCode"].Value =
                                reader["ProductCode"];

                            row.Cells["ProductName"].Value =
                                reader["ProductName"];

                            // Remaining quantity is what can be delivered
                            row.Cells["Qty"].Value =
                                remainingQty;

                            row.Cells["UnitPrice"].Value =
                                reader["UnitPrice"];

                            row.Cells["DiscountAmount"].Value =
                                reader["DiscountAmount"];

                            row.Cells["TaxAmount"].Value =
                                reader["TaxAmount"];

                            decimal unitPrice =
                                Convert.ToDecimal(reader["UnitPrice"]);

                            decimal discount =
                                Convert.ToDecimal(reader["DiscountAmount"]);

                            decimal tax =
                                Convert.ToDecimal(reader["TaxAmount"]);

                            decimal net =
                                (remainingQty * unitPrice)
                                - discount
                                + tax;

                            row.Cells["NetAmount"].Value = net;

                            // IMPORTANT:
                            // Store the original SODetailID.
                            // SaveSale() will later send this to sp_Sale_Save.
                            if (dgvSaleDetails.Columns.Contains("SODetailID"))
                            {
                                row.Cells["SODetailID"].Value =
                                    reader["SODetailID"];
                            }
                        }
                    }
                }

                //txtSaleNumber.Text = LoadNextSaleNumber();
                LoadNextSaleNumber();
                CalculateSaleTotals();

                MessageBox.Show(
                    "Sales Order loaded successfully.\r\n\r\n" +
                    "SO Number: " + _currentSONumber +
                    "\r\nStatus: " + _currentSOStatus,
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading Sales Order:\r\n" + ex.Message,
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
}

        private void dgvSaleDetails_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            // F2 = Product Search
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;

                OpenProductSelection();

                return;
            }

            // Delete = Remove selected item
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvSaleDetails.CurrentRow == null)
                    return;

                if (dgvSaleDetails.CurrentRow.IsNewRow)
                    return;

                dgvSaleDetails.Rows.Remove(
                    dgvSaleDetails.CurrentRow);

                CalculateSaleTotals();

                e.Handled = true;
            }
        }

        private void dgvSaleDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvSaleDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            // Don't allow modification after sale is saved
            if (_currentSaleID > 0)
            {
                MessageBox.Show(
                    "This sale cannot be modified.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (e.RowIndex >= dgvSaleDetails.Rows.Count)
                return;

            dgvSaleDetails.Rows.RemoveAt(e.RowIndex);

            // Use your existing sale total calculation
            CalculateSaleTotals();
        }

        private void dgvSaleDetails_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.RowIndex >= dgvSaleDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvSaleDetails.Rows[e.RowIndex];

            CalculateSaleRow(row);

            CalculateSaleTotals();
        }

        private void txtPaidAmount_TextChanged(
            object sender,
            EventArgs e)
        {
            CalculateSaleTotals();
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            OpenProductSelection();
        }

        private void btnLoadSaleOrder_Click(object sender, EventArgs e)
        {
            using (FrmApprovedSaleOrderSearch frm = new FrmApprovedSaleOrderSearch())
            {
                if(!AppSession.HasPermission("SALE.LOAD"))
                {
                    MessageBox.Show(
                        "You do not have permission to load Sales Orders.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                if (frm.SelectedSOID <= 0)
                {
                    MessageBox.Show(
                        "Please select a valid Sales Order.",
                        "Sales Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                LoadSaleOrderForDelivery(frm.SelectedSOID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _currentSOID = 0;
            _currentSONumber = "";
            _currentSOStatus = "";
            _currentSaleID = 0;
            _currentSOID = 0;
            _currentSONumber = string.Empty;
            _currentSOStatus = string.Empty;
            cboCustomer.SelectedIndex = -1;
            cboWarehouse.SelectedIndex = -1;
            txtSONumber.Text = "";
            dgvSaleDetails.Rows.Clear();
            txtRemarks.Clear();
            txtPaidAmount.Text = "0.00";
            txtGrossAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtBalanceAmount.Text = "0.00";
            LoadNextSaleNumber();
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("SALE.SEARCH"))
            {
                MessageBox.Show("You do not have permission to search sales.");
                return;
            }
            using (FrmSaleSearch frm = new FrmSaleSearch())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                if (frm.SelectedSaleID <= 0)
                {
                    MessageBox.Show(
                        "Invalid Sale selected.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                LoadSale(frm.SelectedSaleID);
            }
        }

        private void LoadSale(int selectedSaleID)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT
                    S.SaleID,
                    S.SaleNumber,
                    S.SaleDate,
                    S.CustomerID,
                    S.WarehouseID,
                    S.SOID,
                    S.Status,
                    S.GrossAmount,
                    S.DiscountAmount,
                    S.TaxAmount,
                    S.NetAmount,
                    S.PaidAmount,
                    S.BalanceAmount,
                    S.Remarks
                    FROM dbo.Sales AS S
                    WHERE S.SaleID = @SaleID;", cn))
                {
                    cmd.Parameters.Add(
                        "@SaleID",
                        SqlDbType.Int).Value = selectedSaleID;

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Sale record not found.",
                        "Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataRow sale = dt.Rows[0];

                // --------------------------------------------------
                // HEADER
                // --------------------------------------------------

                _currentSaleID =
                    Convert.ToInt32(sale["SaleID"]);

                _currentSOID =
                    sale["SOID"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(sale["SOID"]);

                txtSaleNumber.Text =
                    sale["SaleNumber"].ToString();

                dtpSaleDate.Value =
                    Convert.ToDateTime(sale["SaleDate"]);

                cboCustomer.SelectedValue =
                    Convert.ToInt32(sale["CustomerID"]);

                cboWarehouse.SelectedValue =
                    Convert.ToInt32(sale["WarehouseID"]);

                txtRemarks.Text =
                    sale["Remarks"] == DBNull.Value
                        ? ""
                        : sale["Remarks"].ToString();

                // --------------------------------------------------
                // SALES ORDER NUMBER
                // --------------------------------------------------

                txtSONumber.Text = "";

                _currentSONumber = "";
                _currentSOStatus = "";

                if (_currentSOID > 0)
                {
                    using (SqlConnection cn =
                           new SqlConnection(ConnString))
                    using (SqlCommand cmd =
                           new SqlCommand(@"
                SELECT
                    SONumber,
                    Status
                FROM dbo.SaleOrders
                WHERE SOID = @SOID;", cn))
                    {
                        cmd.Parameters.Add(
                            "@SOID",
                            SqlDbType.Int).Value =
                            _currentSOID;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _currentSONumber =
                                    reader["SONumber"].ToString();

                                _currentSOStatus =
                                    reader["Status"].ToString();

                                txtSONumber.Text =
                                    _currentSONumber;
                            }
                        }
                    }
                }

                // --------------------------------------------------
                // TOTALS
                // --------------------------------------------------

                txtGrossAmount.Text =
                    GetDecimal(
                        sale["GrossAmount"])
                    .ToString("N2");

                txtDiscountAmount.Text =
                    GetDecimal(
                        sale["DiscountAmount"])
                    .ToString("N2");

                txtTaxAmount.Text =
                    GetDecimal(
                        sale["TaxAmount"])
                    .ToString("N2");

                txtNetAmount.Text =
                    GetDecimal(
                        sale["NetAmount"])
                    .ToString("N2");

                txtPaidAmount.Text =
                    GetDecimal(
                        sale["PaidAmount"])
                    .ToString("N2");

                txtBalanceAmount.Text =
                    GetDecimal(
                        sale["BalanceAmount"])
                    .ToString("N2");

                // --------------------------------------------------
                // DETAILS
                // --------------------------------------------------

                DataTable details =
                    new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
            SELECT
                SD.ProductID,
                P.ProductCode,
                P.ProductName,
                SD.Qty,
                SD.UnitPrice,
                SD.DiscountAmount,
                SD.TaxAmount,
                (
                    (SD.Qty * SD.UnitPrice)
                    - SD.DiscountAmount
                    + SD.TaxAmount
                ) AS NetAmount
            FROM dbo.SaleDetails AS SD
            INNER JOIN dbo.Products AS P
                ON P.ProductID = SD.ProductID
            WHERE SD.SaleID = @SaleID
            ORDER BY SD.SaleDetailID;", cn))
                {
                    cmd.Parameters.Add(
                        "@SaleID",
                        SqlDbType.Int).Value =
                        selectedSaleID;

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        da.Fill(details);
                    }
                }

                dgvSaleDetails.Rows.Clear();

                foreach (DataRow detail in details.Rows)
                {
                    int rowIndex =
                        dgvSaleDetails.Rows.Add();

                    DataGridViewRow row =
                        dgvSaleDetails.Rows[rowIndex];

                    row.Cells["ProductID"].Value =
                        detail["ProductID"];

                    row.Cells["SODetailID"].Value =
                        0;

                    row.Cells["ProductCode"].Value =
                        detail["ProductCode"];

                    row.Cells["ProductName"].Value =
                        detail["ProductName"];

                    row.Cells["Qty"].Value =
                        detail["Qty"];

                    row.Cells["UnitPrice"].Value =
                        detail["UnitPrice"];

                    row.Cells["DiscountAmount"].Value =
                        detail["DiscountAmount"];

                    row.Cells["TaxAmount"].Value =
                        detail["TaxAmount"];

                    row.Cells["NetAmount"].Value =
                        detail["NetAmount"];
                }

                // --------------------------------------------------
                // POSTED SALE = VIEW ONLY
                // --------------------------------------------------

                dgvSaleDetails.ReadOnly = true;
                dgvSaleDetails.AllowUserToDeleteRows = false;

                btnSave.Enabled = false;
                btnSelectProduct.Enabled = false;

                // Existing Print button can be used
                btnPrint.Enabled = true;

                MessageBox.Show(
                    "Sale loaded successfully.\r\n\r\n" +
                    "Sale No: " + txtSaleNumber.Text +
                    (_currentSOID > 0
                        ? "\r\nS.O No: " + _currentSONumber
                        : ""),
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
