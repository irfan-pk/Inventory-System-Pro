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
    public partial class FrmSaleReturn : Form 
    {
        private int _saleID = 0;
        private int _saleReturnID = 0;
        private bool _isSaved = false;
        private int _saleCustomerID = 0;
        private int _saleWarehouseID = 0;
        private int _createdBy = 1;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmSaleReturn()
        {
            InitializeComponent();
        }

        // -----------------------------------------------------------------
        // Procedures for Sale Return
        // -----------------------------------------------------------------
        private void GenerateNextReturnNumber()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT ISNULL(
                                    MAX(
                                        TRY_CONVERT(
                                            INT,
                                            SUBSTRING(ReturnNumber, 5, LEN(ReturnNumber))
                                        )
                                    ), 0
                                    ) + 1
                                    FROM dbo.SaleReturns
                                    WHERE ReturnNumber LIKE 'RET-%'", cn))
                {
                    cn.Open();

                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());

                    txtSaleReturnNo.Text =
                        $"RET-{nextNumber:000000}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to generate return number.\n\n" +
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void NewSaleReturn()
        {
            _saleID = 0;
            _saleReturnID = 0;
            _isSaved = false;

            UnlockSaleReturnForNew();
            GenerateNextReturnNumber();

            dtpSaleReturnDate.Value = DateTime.Today;

            txtSaleNumber.Clear();
            txtCustomer.Clear();
            txtRemarks.Clear();

            txtGrossAmount.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtReturnQty.Text = "0.00";
            txtReturnQty.Text = "0.00";

            dgvSaleReturnDetails.Rows.Clear();

            txtSaleNumber.Focus();
        }

        private void SearchSaleForReturn()
        {
            string saleNumber = txtSearchSale.Text.Trim();

            if (string.IsNullOrWhiteSpace(saleNumber))
            {
                MessageBox.Show(
                    "Please enter sale number.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSearchSale.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT
                s.SaleID,
                s.SaleNumber,
                s.SaleDate,
                s.CustomerID,
                c.CustomerName,
                s.WarehouseID,
                w.WarehouseName
            FROM dbo.Sales s
            INNER JOIN dbo.Customers c
                ON c.CustomerID = s.CustomerID
            INNER JOIN dbo.Warehouses w
                ON w.WarehouseID = s.WarehouseID
            WHERE s.SaleNumber = @SaleNumber
              AND s.Status = 'POSTED';", cn))
                {
                    cmd.Parameters.Add(
                        "@SaleNumber",
                        SqlDbType.NVarChar, 30).Value =
                        saleNumber;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show(
                                "Sale not found.",
                                "Sale Return",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        _saleID =
                            Convert.ToInt32(
                                reader["SaleID"]);

                        txtSaleNumber.Text =
                            reader["SaleNumber"].ToString();

                        dtpSaleReturnDate.Value =
                            DateTime.Today;

                        txtCustomer.Text =
                            reader["CustomerName"].ToString();

                        cboWarehouse.Text =
                            reader["WarehouseID"].ToString();

                        // Keep these IDs for saving
                        _saleCustomerID =
                            Convert.ToInt32(
                                reader["CustomerID"]);

                        _saleWarehouseID =
                            Convert.ToInt32(
                                reader["WarehouseID"]);
                    }
                }

                LoadSaleReturnDetails(_saleID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadSaleReturnDetails(int saleID)
        {
            if (dgvSaleReturnDetails.Columns.Count == 0)
            {
                ConfigureSaleReturnDetailsGrid();
            }
            dgvSaleReturnDetails.Rows.Clear();

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                SELECT
                    sd.SaleDetailID,
                    sd.ProductID,
                    p.ProductCode,
                    p.ProductName,
                    sd.Qty AS SoldQty,
                    sd.UnitPrice,
                    sd.DiscountAmount,
                    sd.TaxAmount,

                    ISNULL(
                        (
                            SELECT SUM(srd.Qty)
                            FROM dbo.SaleReturnDetails srd
                            INNER JOIN dbo.SaleReturns sr
                                ON sr.SaleReturnID =
                                   srd.SaleReturnID
                            WHERE srd.SaleDetailID =
                                  sd.SaleDetailID
                              AND sr.Status = 'POSTED'
                        ), 0
                    ) AS PreviouslyReturnedQty

                FROM dbo.SaleDetails sd
                INNER JOIN dbo.Products p
                    ON p.ProductID = sd.ProductID

                WHERE sd.SaleID = @SaleID
                ORDER BY sd.SaleDetailID;", cn))
                {
                    cmd.Parameters.Add(
                        "@SaleID",
                        SqlDbType.Int).Value =
                        saleID;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal soldQty =
                                Convert.ToDecimal(
                                    reader["SoldQty"]);

                            decimal previousReturned =
                                Convert.ToDecimal(
                                    reader["PreviouslyReturnedQty"]);

                            decimal availableQty =
                                soldQty - previousReturned;

                            // Fully returned items don't need
                            // to appear in the return grid.
                            if (availableQty <= 0)
                                continue;

                            int rowIndex =
                                dgvSaleReturnDetails.Rows.Add();

                            DataGridViewRow row =
                                dgvSaleReturnDetails
                                .Rows[rowIndex];

                            row.Cells["SaleDetailID"].Value =
                                reader["SaleDetailID"];

                            row.Cells["ProductID"].Value =
                                reader["ProductID"];

                            row.Cells["ProductCode"].Value =
                                reader["ProductCode"];

                            row.Cells["ProductName"].Value =
                                reader["ProductName"];

                            row.Cells["SoldQty"].Value =
                                soldQty;

                            row.Cells["PreviouslyReturnedQty"].Value =
                                previousReturned;

                            row.Cells["AvailableReturnQty"].Value =
                                availableQty;

                            row.Cells["ReturnQty"].Value =
                                0.000m;

                            row.Cells["UnitPrice"].Value =
                                reader["UnitPrice"];

                            row.Cells["DiscountAmount"].Value =
                                reader["DiscountAmount"];

                            row.Cells["TaxAmount"].Value =
                                reader["TaxAmount"];

                            row.Cells["NetAmount"].Value =
                                0.00m;

                            row.Cells["CostPrice"].Value =
                                0.00m;

                            row.Cells["CostAmount"].Value =
                                0.00m;
                        }
                    }
                }

                CalculateSaleReturnTotals();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculateSaleReturnTotals()
        {
            decimal gross = 0m;
            decimal discount = 0m;
            decimal tax = 0m;
            decimal net = 0m;
            decimal totalReturnQty = 0m;

            foreach (DataGridViewRow row
                     in dgvSaleReturnDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal soldQty =
                    GetDecimal(row.Cells["SoldQty"].Value);

                decimal availableQty =
                    GetDecimal(row.Cells["AvailableReturnQty"].Value);

                decimal qty =
                    GetDecimal(row.Cells["ReturnQty"].Value);

                decimal unitPrice =
                    GetDecimal(row.Cells["UnitPrice"].Value);

                decimal originalDiscount =
                    GetDecimal(row.Cells["DiscountAmount"].Value);

                decimal originalTax =
                    GetDecimal(row.Cells["TaxAmount"].Value);

                if (qty < 0)
                    qty = 0;

                if (qty > availableQty)
                    qty = availableQty;

                row.Cells["ReturnQty"].Value = qty;

                decimal lineGross =
                    qty * unitPrice;

                decimal lineDiscount = 0m;
                decimal lineTax = 0m;

                if (soldQty > 0)
                {
                    lineDiscount =
                        originalDiscount *
                        (qty / soldQty);

                    lineTax =
                        originalTax *
                        (qty / soldQty);
                }

                decimal lineNet =
                    lineGross -
                    lineDiscount +
                    lineTax;

                row.Cells["NetAmount"].Value =
                    Math.Round(lineNet, 2);

                gross += lineGross;
                discount += lineDiscount;
                tax += lineTax;
                net += lineNet;
                totalReturnQty += qty;
            }

            txtGrossAmount.Text =
                gross.ToString("N2");

            txtDiscount.Text =
                discount.ToString("N2");

            txtTaxAmount.Text =
                tax.ToString("N2");

            txtNetAmount.Text =
                net.ToString("N2");

            txtReturnQty.Text =
                totalReturnQty.ToString("N3");

            txtRefundAmount.Text =
                net.ToString("N2");
        }

        private decimal GetDecimal(object value)
        {
            if (value == null ||
                value == DBNull.Value)
                return 0m;

            if (decimal.TryParse(
                value.ToString(),
                out decimal result))
            {
                return result;
            }

            return 0m;
        }

        private void ConfigureSaleReturnDetailsGrid()
        {
            dgvSaleReturnDetails.DataSource = null;
            //dgvSaleReturnDetails.Columns.Clear();

            dgvSaleReturnDetails.AutoGenerateColumns = false;
            dgvSaleReturnDetails.AllowUserToAddRows = false;
            dgvSaleReturnDetails.AllowUserToDeleteRows = false;
            dgvSaleReturnDetails.ReadOnly = false;
            dgvSaleReturnDetails.MultiSelect = false;
            dgvSaleReturnDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Hidden IDs
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "SaleDetailID",
                    HeaderText = "Sale Detail ID",
                    Visible = false
                });

            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductID",
                    HeaderText = "Product ID",
                    Visible = false
                });

            // Product Code
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductCode",
                    HeaderText = "Code",
                    ReadOnly = true,
                    Width = 100
                });

            // Product Name
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product",
                    ReadOnly = true,
                    Width = 220
                });

            // Sold Qty
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "SoldQty",
                    HeaderText = "Sold Qty",
                    ReadOnly = true,
                    Width = 90
                });

            // Previously Returned
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "PreviouslyReturnedQty",
                    HeaderText = "Previous Return",
                    ReadOnly = true,
                    Width = 110
                });

            // Available
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "AvailableReturnQty",
                    HeaderText = "Available",
                    ReadOnly = true,
                    Width = 100
                });

            // Return Qty - ONLY EDITABLE COLUMN
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ReturnQty",
                    HeaderText = "Return Qty",
                    ReadOnly = false,
                    Width = 100
                });

            // Unit Price
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "UnitPrice",
                    HeaderText = "Unit Price",
                    ReadOnly = true,
                    Width = 100
                });

            // Discount
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "DiscountAmount",
                    HeaderText = "Discount",
                    ReadOnly = true,
                    Width = 100
                });

            // Tax
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "TaxAmount",
                    HeaderText = "Tax",
                    ReadOnly = true,
                    Width = 90
                });

            // Net Amount
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "NetAmount",
                    HeaderText = "Net Amount",
                    ReadOnly = true,
                    Width = 110
                });

            // Cost Price
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "CostPrice",
                    HeaderText = "Cost Price",
                    ReadOnly = true,
                    Width = 100
                });

            // Cost Amount
            dgvSaleReturnDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "CostAmount",
                    HeaderText = "Cost Amount",
                    ReadOnly = true,
                    Width = 110
                });

            // Number formats
            dgvSaleReturnDetails.Columns["SoldQty"]
                .DefaultCellStyle.Format = "N3";

            dgvSaleReturnDetails.Columns["PreviouslyReturnedQty"]
                .DefaultCellStyle.Format = "N3";

            dgvSaleReturnDetails.Columns["AvailableReturnQty"]
                .DefaultCellStyle.Format = "N3";

            dgvSaleReturnDetails.Columns["ReturnQty"]
                .DefaultCellStyle.Format = "N3";

            dgvSaleReturnDetails.Columns["UnitPrice"]
                .DefaultCellStyle.Format = "N4";

            dgvSaleReturnDetails.Columns["DiscountAmount"]
                .DefaultCellStyle.Format = "N2";

            dgvSaleReturnDetails.Columns["TaxAmount"]
                .DefaultCellStyle.Format = "N2";

            dgvSaleReturnDetails.Columns["NetAmount"]
                .DefaultCellStyle.Format = "N2";

            dgvSaleReturnDetails.Columns["CostPrice"]
                .DefaultCellStyle.Format = "N4";

            dgvSaleReturnDetails.Columns["CostAmount"]
                .DefaultCellStyle.Format = "N2";

            // Add Remove button column
            // Numeric columns - right aligned
            string[] numericColumns =
            {
    "SoldQty",
    "PreviouslyReturnedQty",
    "AvailableReturnQty",
    "ReturnQty",
    "UnitPrice",
    "DiscountAmount",
    "TaxAmount",
    "NetAmount",
    "CostPrice",
    "CostAmount"
};

            foreach (string columnName in numericColumns)
            {
                if (dgvSaleReturnDetails.Columns.Contains(columnName))
                {
                    dgvSaleReturnDetails.Columns[columnName]
                        .DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Text columns - left aligned
            if (dgvSaleReturnDetails.Columns.Contains("ProductCode"))
            {
                dgvSaleReturnDetails.Columns["ProductCode"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvSaleReturnDetails.Columns.Contains("ProductName"))
            {
                dgvSaleReturnDetails.Columns["ProductName"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
            }

            // Header alignment
            dgvSaleReturnDetails.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Add Delete button as the LAST column
            if (!dgvSaleReturnDetails.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteColumn =
                    new DataGridViewButtonColumn();

                deleteColumn.Name = "Delete";
                deleteColumn.HeaderText = "";
                deleteColumn.Text = "✕";
                deleteColumn.UseColumnTextForButtonValue = true;
                deleteColumn.Width = 45;
                deleteColumn.FlatStyle = FlatStyle.Flat;
                deleteColumn.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                dgvSaleReturnDetails.Columns.Add(deleteColumn);
            }

            // Register event only once
            //dgvSaleReturnDetails.CellContentClick -=
            //    dgvSaleReturnDetails_CellContentClick;

            //dgvSaleReturnDetails.CellContentClick +=
            //    dgvSaleReturnDetails_CellContentClick;
        }

        private DataTable BuildSaleReturnDetailsTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SaleDetailID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("DiscountAmount", typeof(decimal));
            dt.Columns.Add("TaxAmount", typeof(decimal));

            return dt;
        }

        private DataTable GetSaleReturnDetailsTable()
        {
            DataTable dt = BuildSaleReturnDetailsTable();

            foreach (DataGridViewRow row in dgvSaleReturnDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty = GetDecimal(
                    row.Cells["ReturnQty"].Value);

                // Ignore products with zero return quantity
                if (qty <= 0)
                    continue;

                decimal availableQty = GetDecimal(
                    row.Cells["AvailableReturnQty"].Value);

                // Final client-side protection
                if (qty > availableQty)
                {
                    MessageBox.Show(
                        $"Return quantity cannot exceed available quantity.\n\n" +
                        $"Product: {row.Cells["ProductName"].Value}\n" +
                        $"Available: {availableQty:N3}",
                        "Sale Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return null;
                }

                DataRow dr = dt.NewRow();

                dr["SaleDetailID"] =
                    Convert.ToInt32(
                        row.Cells["SaleDetailID"].Value);

                dr["ProductID"] =
                    Convert.ToInt32(
                        row.Cells["ProductID"].Value);

                dr["Qty"] = qty;

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

        private void SaveSaleReturn()
        {
            try
            {
                // -----------------------------
                // Basic validation
                // -----------------------------

                if (_saleID <= 0)
                {
                    MessageBox.Show(
                        "Please search and select a sale first.",
                        "Sale Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    txtSaleReturnNo.Text))
                {
                    MessageBox.Show(
                        "Return number is required.",
                        "Sale Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSaleReturnNo.Focus();
                    return;
                }

                DataTable details =
                    GetSaleReturnDetailsTable();

                if (details == null)
                    return;

                if (details.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Please enter at least one return quantity.",
                        "Sale Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                decimal refundAmount =
                    GetDecimal(txtRefundAmount.Text);

                // -----------------------------
                // Execute stored procedure
                // -----------------------------

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(
                           "dbo.sp_SaleReturn_Save",
                           cn))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@ReturnDate",
                        SqlDbType.Date).Value =
                        dtpSaleReturnDate.Value.Date;

                    cmd.Parameters.Add(
                        "@ReturnNumber",
                        SqlDbType.NVarChar,
                        60).Value =
                        txtSaleReturnNo.Text.Trim();

                    cmd.Parameters.Add(
                        "@SaleID",
                        SqlDbType.Int).Value =
                        _saleID;

                    cmd.Parameters.Add(
                        "@CustomerID",
                        SqlDbType.Int).Value =
                        _saleCustomerID;

                    cmd.Parameters.Add(
                        "@WarehouseID",
                        SqlDbType.Int).Value =
                        _saleWarehouseID;

                    SqlParameter refundParameter =
                        cmd.Parameters.Add(
                            "@RefundAmount",
                            SqlDbType.Decimal);

                    refundParameter.Precision = 18;
                    refundParameter.Scale = 2;
                    refundParameter.Value =
                        refundAmount;

                    cmd.Parameters.Add(
                        "@Remarks",
                        SqlDbType.NVarChar,
                        1000).Value =
                        string.IsNullOrWhiteSpace(
                            txtRemarks.Text)
                            ? (object)DBNull.Value
                            : txtRemarks.Text.Trim();

                    cmd.Parameters.Add(
                        "@CreatedBy",
                        SqlDbType.Int).Value =
                        _createdBy;

                    // -----------------------------
                    // TVP
                    // -----------------------------

                    SqlParameter detailsParameter =
                        cmd.Parameters.Add(
                            "@Details",
                            SqlDbType.Structured);

                    detailsParameter.TypeName =
                        "dbo.SaleReturnDetailType";

                    detailsParameter.Value =
                        details;

                    // -----------------------------
                    // Execute
                    // -----------------------------

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _saleReturnID =
                                Convert.ToInt32(
                                    reader["SaleReturnID"]);

                            string returnNumber =
                                reader["ReturnNumber"]
                                .ToString();

                            _isSaved = true;

                            LockSaleReturnAfterSave();

                            MessageBox.Show(
                                "Sale return saved successfully.\n\n" +
                                "Return Number: " +
                                returnNumber,
                                "Sale Return",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UnlockSaleReturnForNew()
        {
            txtSaleReturnNo.ReadOnly = false;
            txtSaleNumber.ReadOnly = false;
            txtRemarks.ReadOnly = false;

            dtpSaleReturnDate.Enabled = true;

            txtSearchSale.ReadOnly = false;
            btnSearch.Enabled = true;

            dgvSaleReturnDetails.ReadOnly = false;

            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void LockSaleReturnAfterSave()
        {
            // Header
            txtSaleReturnNo.ReadOnly = true;
            txtSaleNumber.ReadOnly = true;
            txtRemarks.ReadOnly = true;

            // Date
            dtpSaleReturnDate.Enabled = false;

            // Search
            txtSearchSale.ReadOnly = true;
            btnSearch.Enabled = false;

            // Grid
            dgvSaleReturnDetails.ReadOnly = true;
            dgvSaleReturnDetails.AllowUserToAddRows = false;
            dgvSaleReturnDetails.AllowUserToDeleteRows = false;

            // Buttons
            btnSave.Enabled = false;
            btnPrint.Enabled = true;

            // New remains available
            btnNew.Enabled = true;
        }

        // -----------------------------------------------------------------
        // Form Events
        // -----------------------------------------------------------------
        private void FrmSaleReturn_Load(object sender, EventArgs e)
        {
            ConfigureSaleReturnDetailsGrid();
            NewSaleReturn();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_RETURN.NEW"))
            {
                MessageBox.Show("You do not have permission to create new sale returns.");
                return;
            }
            NewSaleReturn();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("SALE_RETURN.SEARCH"))
            {
                MessageBox.Show("You do not have permission to search sale returns.");
                return;
            }
            SearchSaleForReturn();
        }
        private void dgvSaleReturnDetails_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvSaleReturnDetails.Columns[e.ColumnIndex].Name
                == "ReturnQty")
            {
                CalculateSaleReturnTotals();
            }
        }
        private void dgvSaleReturnDetails_CellValidating(
    object sender,
    DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvSaleReturnDetails.Columns[e.ColumnIndex].Name
                != "ReturnQty")
                return;

            decimal qty;

            if (!decimal.TryParse(
                e.FormattedValue?.ToString(),
                out qty))
            {
                MessageBox.Show(
                    "Please enter a valid return quantity.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
                return;
            }

            decimal availableQty =
                GetDecimal(
                    dgvSaleReturnDetails.Rows[e.RowIndex]
                    .Cells["AvailableReturnQty"].Value);

            if (qty < 0)
            {
                MessageBox.Show(
                    "Return quantity cannot be negative.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
                return;
            }

            if (qty > availableQty)
            {
                MessageBox.Show(
                    $"Maximum return quantity is {availableQty:N3}.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
            }
        }
        private void dgvSaleReturnDetails_CellEndEdit(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvSaleReturnDetails.Columns[e.ColumnIndex].Name
                == "ReturnQty")
            {
                CalculateSaleReturnTotals();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_RETURN.SAVE"))
            {
                MessageBox.Show("You do not have permission to save sale returns.");
                return;
            }
            SaveSaleReturn();
        }

        private void btnPrint_Click(
    object sender,
    EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_RETURN.PRINT"))
            {
                MessageBox.Show("You do not have permission to print sale returns.");
                return;
            }
            if (!_isSaved || _saleReturnID <= 0)
            {
                MessageBox.Show(
                    "Please save the sale return before printing.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string folder =
                    Path.Combine(
                        Application.StartupPath,
                        "PDFReports");

                Directory.CreateDirectory(folder);

                string filePath =
                    Path.Combine(
                        folder,
                        $"SaleReturn_{txtSaleReturnNo.Text.Trim()}.pdf");

                SaleReturnPdfGenerator.GenerateSaleReturnPdf(
                    _saleReturnID,
                    ConnString,
                    filePath);

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
                    "Unable to generate Sale Return PDF.\n\n" +
                    ex.Message,
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportPDF_Click(
    object sender,
    EventArgs e)
        {
            if (!AppSession.HasPermission("SALE_RETURN.EXPORT_PDF"))
            {
                MessageBox.Show("You do not have permission to export sale returns to PDF.");
                return;
            }
            if (!_isSaved || _saleReturnID <= 0)
            {
                MessageBox.Show(
                    "Please save the sale return before exporting.",
                    "Sale Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (SaveFileDialog dialog =
                   new SaveFileDialog())
            {
                dialog.Title =
                    "Export Sale Return PDF";

                dialog.Filter =
                    "PDF Files (*.pdf)|*.pdf";

                dialog.DefaultExt = "pdf";
                dialog.AddExtension = true;

                dialog.FileName =
                    $"SaleReturn_{txtSaleReturnNo.Text.Trim()}.pdf";

                if (dialog.ShowDialog(this)
                    != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SaleReturnPdfGenerator.GenerateSaleReturnPdf(
                        _saleReturnID,
                        ConnString,
                        dialog.FileName);

                    MessageBox.Show(
                        "Sale Return PDF exported successfully.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Unable to export Sale Return PDF.\n\n" +
                        ex.Message,
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSaleReturnDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvSaleReturnDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            // Do not allow modification after saving/posting
            if (_saleReturnID > 0)
            {
                MessageBox.Show(
                    "This sales return cannot be modified.",
                    "Sales Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (e.RowIndex >= dgvSaleReturnDetails.Rows.Count)
                return;

            dgvSaleReturnDetails.Rows.RemoveAt(e.RowIndex);

            // Existing calculation method
            CalculateSaleReturnTotals();
        }
    }
}
