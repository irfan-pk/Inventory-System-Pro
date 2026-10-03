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
    public partial class FrmPurchaseReturn : Form
    {
        private int _purchaseReturnID = 0;
        private int _purchaseID = 0;
        private int _supplierID = 0;
        private int _warehouseID = 0;

        //private bool _isSaved = false;
        private int _createdBy = 1;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmPurchaseReturn()
        {
            InitializeComponent();
            txtRefundAmount.ReadOnly = true;
            dgvPurchaseReturnDetails.AllowUserToAddRows = false;
            dgvPurchaseReturnDetails.AllowUserToDeleteRows = false;
            dgvPurchaseReturnDetails.AutoGenerateColumns = false;
            dgvPurchaseReturnDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchaseReturnDetails.MultiSelect = false;
        }

        // ----------------------------------------
        // Purchase Return Methods
        // ----------------------------------------

        private void InitializePurchaseReturnForm()
        {
            _purchaseReturnID = 0;
            _purchaseID = 0;
            _supplierID = 0;
            _warehouseID = 0;
            //_isSaved = false;

            dtpReturnDate.Value = DateTime.Today;

            txtReturnNumber.ReadOnly = true;
            txtPurchaseNumber.ReadOnly = true;

            //txtGrossAmount.ReadOnly = true;
            //txtDiscountAmount.ReadOnly = true;
            //txtTaxAmount.ReadOnly = true;
            //txtNetAmount.ReadOnly = true;

            txtRefundAmount.Text = "0.00";

            dgvPurchaseReturnDetails.Rows.Clear();

            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            btnExportPDF.Enabled = false;

            SetupPurchaseReturnGrid();

            GenerateNextPurchaseReturnNumber();
        }

        private void GenerateNextPurchaseReturnNumber()
        {
            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                    SELECT
                        'PRT-' +
                        RIGHT(
                            '000000' +
                            CAST(
                                ISNULL(
                                    MAX(PurchaseReturnID), 0
                                ) + 1
                                AS VARCHAR(6)
                            ),
                            6
                        )
                    FROM dbo.PurchaseReturns;",
                            cn))
                {
                    cn.Open();

                    object result = cmd.ExecuteScalar();

                    txtReturnNumber.Text =
                        result == null ||
                        result == DBNull.Value
                            ? "PRT-000001"
                            : result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to generate Purchase Return Number.\n\n" +
                    ex.Message,
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetupPurchaseReturnGrid()
        {
            // Numeric columns
            dgvPurchaseReturnDetails.Columns["PurchasedQty"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["PreviouslyReturnedQty"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["AvailableReturnQty"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["ReturnQty"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["UnitPrice"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["DiscountAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["TaxAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPurchaseReturnDetails.Columns["NetAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            // Text columns
            dgvPurchaseReturnDetails.Columns["ProductCode"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvPurchaseReturnDetails.Columns["ProductName"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Headers
            foreach (DataGridViewColumn column in dgvPurchaseReturnDetails.Columns)
            {
                column.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            // Add Delete button only once
            if (!dgvPurchaseReturnDetails.Columns.Contains("Delete"))
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

                dgvPurchaseReturnDetails.Columns.Add(deleteColumn);
            }
        }

        private void LoadPurchases(string searchText = "")
        {
            try
            {
                dgvPurchaseReturnDetails.Rows.Clear();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                        SELECT
                        p.PurchaseID,
                        p.PurchaseNumber,
                        p.PurchaseDate,
                        p.SupplierID,
                        s.SupplierName,
                        p.WarehouseID,
                        w.WarehouseName,
                        p.NetAmount,
                        p.Status
                        FROM dbo.Purchases p
                        INNER JOIN dbo.Suppliers s
                            ON s.SupplierID = p.SupplierID
                        INNER JOIN dbo.Warehouses w
                            ON w.WarehouseID = p.WarehouseID
                        WHERE
                        p.Status IN ('POSTED', 'RECEIVED')
                        AND
                        (
                            @Search = ''
                            OR p.PurchaseNumber LIKE '%' + @Search + '%'
                            OR s.SupplierName LIKE '%' + @Search + '%'
                        )
                        ORDER BY p.PurchaseID DESC;",
                            cn))
                {
                    cmd.Parameters.Add(
                        "@Search",
                        SqlDbType.NVarChar, 100).Value =
                        searchText.Trim();

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int row =
                                dgvPurchaseReturnDetails.Rows.Add();

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "PurchaseID"].Value =
                                reader["PurchaseID"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "PurchaseNumber"].Value =
                                reader["PurchaseNumber"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "PurchaseDate"].Value =
                                Convert.ToDateTime(
                                    reader["PurchaseDate"])
                                    .ToString("dd-MMM-yyyy");

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "SupplierID"].Value =
                                reader["SupplierID"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "SupplierName"].Value =
                                reader["SupplierName"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "WarehouseID"].Value =
                                reader["WarehouseID"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "WarehouseName"].Value =
                                reader["WarehouseName"];

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "NetAmount"].Value =
                                Convert.ToDecimal(
                                    reader["NetAmount"])
                                    .ToString("N2");

                            dgvPurchaseReturnDetails.Rows[row].Cells[
                                "Status"].Value =
                                reader["Status"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load purchases.\n\n" +
                    ex.Message,
                    "Purchase Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenPurchaseSelection()
        {
            using (FrmPurchaseSearch frm =
                   new FrmPurchaseSearch())
            {
                if (frm.ShowDialog(this) !=
                    DialogResult.OK)
                {
                    return;
                }

                _purchaseID =
                    frm.SelectedPurchaseID;

                _supplierID =
                    frm.SelectedSupplierID;

                _warehouseID =
                    frm.SelectedWarehouseID;

                txtPurchaseNumber.Text =
                    frm.SelectedPurchaseNumber;

                LoadPurchaseDetails();
            }
        }

        private void LoadPurchaseDetails()
        {
            if (_purchaseID <= 0)
            {
                MessageBox.Show(
                    "Invalid Purchase ID.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                dgvPurchaseReturnDetails.Rows.Clear();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                    SELECT
                        pd.PurchaseDetailID,
                        pd.ProductID,
                        p.ProductCode,
                        p.ProductName,
                        pd.Qty AS PurchasedQty,

                        ISNULL(
                            (
                                SELECT SUM(prd.Qty)
                                FROM dbo.PurchaseReturnDetails prd
                                INNER JOIN dbo.PurchaseReturns pr
                                    ON pr.PurchaseReturnID =
                                       prd.PurchaseReturnID
                                WHERE prd.PurchaseDetailID =
                                      pd.PurchaseDetailID
                                  AND pr.Status = 'POSTED'
                            ), 0
                        ) AS PreviouslyReturnedQty,

                        pd.UnitPrice,
                        pd.DiscountAmount,
                        pd.TaxAmount

                    FROM dbo.PurchaseDetails pd

                    INNER JOIN dbo.Products p
                        ON p.ProductID = pd.ProductID

                    WHERE pd.PurchaseID = @PurchaseID

                    ORDER BY pd.PurchaseDetailID;",
                            cn))
                {
                    cmd.Parameters.Add(
                        "@PurchaseID",
                        SqlDbType.Int).Value =
                        _purchaseID;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal purchasedQty =
                                Convert.ToDecimal(
                                    reader["PurchasedQty"]);

                            decimal returnedQty =
                                Convert.ToDecimal(
                                    reader["PreviouslyReturnedQty"]);

                            decimal availableQty =
                                purchasedQty - returnedQty;

                            // Don't add completely returned items.
                            if (availableQty <= 0)
                                continue;

                            int rowIndex =
                                dgvPurchaseReturnDetails.Rows.Add();

                            DataGridViewRow row =
                                dgvPurchaseReturnDetails
                                    .Rows[rowIndex];

                            row.Cells["PurchaseDetailID"].Value =
                                reader["PurchaseDetailID"];

                            row.Cells["ProductID"].Value =
                                reader["ProductID"];

                            row.Cells["ProductCode"].Value =
                                reader["ProductCode"];

                            row.Cells["ProductName"].Value =
                                reader["ProductName"];

                            row.Cells["PurchasedQty"].Value =
                                purchasedQty;

                            row.Cells["PreviouslyReturnedQty"].Value =
                                returnedQty;

                            row.Cells["AvailableReturnQty"].Value =
                                availableQty;

                            row.Cells["ReturnQty"].Value =
                                0.000m;

                            row.Cells["UnitPrice"].Value =
                                Convert.ToDecimal(
                                    reader["UnitPrice"]);

                            row.Cells["DiscountAmount"].Value =
                                Convert.ToDecimal(
                                    reader["DiscountAmount"]);

                            row.Cells["TaxAmount"].Value =
                                Convert.ToDecimal(
                                    reader["TaxAmount"]);

                            row.Cells["NetAmount"].Value =
                                0.00m;
                        }
                    }
                }

                LoadSuppliers();
                LoadWarehouses();

                CalculatePurchaseReturnTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load purchase details.\n\n" +
                    ex.Message,
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculatePurchaseReturnTotals()
        {
            decimal grossAmount = 0m;
            decimal discountAmount = 0m;
            decimal taxAmount = 0m;
            decimal netAmount = 0m;

            foreach (DataGridViewRow row in dgvPurchaseReturnDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal returnQty = 0m;
                decimal unitPrice = 0m;
                decimal discount = 0m;
                decimal tax = 0m;
                decimal purchasedQty = 0m;

                decimal.TryParse(
                    Convert.ToString(row.Cells["ReturnQty"].Value),
                    out returnQty);

                if (returnQty <= 0m)
                {
                    row.Cells["NetAmount"].Value = 0.00m;
                    continue;
                }

                decimal.TryParse(
                    Convert.ToString(row.Cells["UnitPrice"].Value),
                    out unitPrice);

                decimal.TryParse(
                    Convert.ToString(row.Cells["DiscountAmount"].Value),
                    out discount);

                decimal.TryParse(
                    Convert.ToString(row.Cells["TaxAmount"].Value),
                    out tax);

                decimal.TryParse(
                    Convert.ToString(row.Cells["PurchasedQty"].Value),
                    out purchasedQty);

                // ----------------------------------------
                // Return Gross
                // ----------------------------------------

                decimal gross = returnQty * unitPrice;

                // ----------------------------------------
                // Proportionate Discount / Tax
                // ----------------------------------------

                decimal detailDiscount = 0m;
                decimal detailTax = 0m;

                if (purchasedQty > 0m)
                {
                    decimal ratio = returnQty / purchasedQty;

                    detailDiscount =
                        Math.Round(discount * ratio, 2);

                    detailTax =
                        Math.Round(tax * ratio, 2);
                }

                // ----------------------------------------
                // Return Net
                // ----------------------------------------

                decimal detailNet =
                    gross -
                    detailDiscount +
                    detailTax;

                detailNet =
                    Math.Round(detailNet, 2);

                // ----------------------------------------
                // Grid line amount
                // ----------------------------------------

                row.Cells["NetAmount"].Value =
                    detailNet.ToString("N2");

                // ----------------------------------------
                // Totals
                // ----------------------------------------

                grossAmount += gross;
                discountAmount += detailDiscount;
                taxAmount += detailTax;
                netAmount += detailNet;
            }

            // ----------------------------------------
            // Header totals
            // ----------------------------------------

            txtGrossAmount.Text =
                grossAmount.ToString("N2");

            txtDiscountAmount.Text =
                discountAmount.ToString("N2");

            txtTaxAmount.Text =
                taxAmount.ToString("N2");

            txtNetAmount.Text =
                netAmount.ToString("N2");

            // ----------------------------------------
            // AUTOMATIC REFUND
            // ----------------------------------------

            txtRefundAmount.Text =
                netAmount.ToString("N2");
        }

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                    SELECT
                        SupplierID,
                        SupplierName
                    FROM dbo.Suppliers
                    WHERE IsActive = 1
                    ORDER BY SupplierName;",
                            cn))
                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                cboSupplier.DataSource = dt;
                cboSupplier.DisplayMember = "SupplierName";
                cboSupplier.ValueMember = "SupplierID";

                cboSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load suppliers.\n\n" +
                    ex.Message,
                    "Purchase Return",
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
                using (SqlCommand cmd =
                       new SqlCommand(@"
                    SELECT
                        WarehouseID,
                        WarehouseName
                    FROM dbo.Warehouses
                    WHERE IsActive = 1
                    ORDER BY WarehouseName;",
                            cn))
                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                cboWarehouse.DataSource = dt;
                cboWarehouse.DisplayMember = "WarehouseName";
                cboWarehouse.ValueMember = "WarehouseID";

                cboWarehouse.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load warehouses.\n\n" +
                    ex.Message,
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool SavePurchaseReturn()
        {
            try
            {
                DataTable details = new DataTable();

                // This DataTable is for the SQL Server TVP.
                // It is NOT your DataGridView.
                details.Columns.Add("PurchaseDetailID", typeof(int));
                details.Columns.Add("ProductID", typeof(int));
                details.Columns.Add("Qty", typeof(decimal));
                details.Columns.Add("UnitPrice", typeof(decimal));
                details.Columns.Add("DiscountAmount", typeof(decimal));
                details.Columns.Add("TaxAmount", typeof(decimal));

                foreach (DataGridViewRow row in dgvPurchaseReturnDetails.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    decimal returnQty = 0m;

                    decimal.TryParse(
                        Convert.ToString(row.Cells["ReturnQty"].Value),
                        out returnQty);

                    if (returnQty <= 0m)
                        continue;

                    int purchaseDetailID =
                        Convert.ToInt32(
                            row.Cells["PurchaseDetailID"].Value);

                    int productID =
                        Convert.ToInt32(
                            row.Cells["ProductID"].Value);

                    decimal purchasedQty = 0m;
                    decimal availableQty = 0m;
                    decimal unitPrice = 0m;
                    decimal discount = 0m;
                    decimal tax = 0m;

                    decimal.TryParse(
                        Convert.ToString(
                            row.Cells["PurchasedQty"].Value),
                        out purchasedQty);

                    decimal.TryParse(
                        Convert.ToString(
                            row.Cells["AvailableReturnQty"].Value),
                        out availableQty);

                    decimal.TryParse(
                        Convert.ToString(
                            row.Cells["UnitPrice"].Value),
                        out unitPrice);

                    decimal.TryParse(
                        Convert.ToString(
                            row.Cells["DiscountAmount"].Value),
                        out discount);

                    decimal.TryParse(
                        Convert.ToString(
                            row.Cells["TaxAmount"].Value),
                        out tax);

                    // Final client-side quantity check
                    if (returnQty > availableQty)
                    {
                        MessageBox.Show(
                            "Return quantity cannot exceed available quantity.\n\n" +
                            "Product: " +
                            Convert.ToString(
                                row.Cells["ProductName"].Value) +
                            "\nAvailable: " +
                            availableQty.ToString("N3") +
                            "\nReturn: " +
                            returnQty.ToString("N3"),
                            "Purchase Return",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }

                    decimal detailDiscount = 0m;
                    decimal detailTax = 0m;

                    if (purchasedQty > 0m)
                    {
                        decimal ratio =
                            returnQty / purchasedQty;

                        detailDiscount =
                            Math.Round(
                                discount * ratio,
                                2);

                        detailTax =
                            Math.Round(
                                tax * ratio,
                                2);
                    }

                    details.Rows.Add(
                        purchaseDetailID,
                        productID,
                        returnQty,
                        unitPrice,
                        detailDiscount,
                        detailTax);
                }

                if (details.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Please enter at least one return quantity.",
                        "Purchase Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                // ----------------------------------------
                // Header values
                // ----------------------------------------

                decimal refundAmount = 0m;

                decimal.TryParse(
                    txtRefundAmount.Text.Trim(),
                    out refundAmount);

                string remarks =
                    string.IsNullOrWhiteSpace(txtRemarks.Text)
                        ? null
                        : txtRemarks.Text.Trim();

                // ----------------------------------------
                // Execute stored procedure
                // ----------------------------------------

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(
                           "dbo.sp_PurchaseReturn_Save",
                           cn))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@ReturnDate",
                        SqlDbType.Date).Value =
                        dtpReturnDate.Value.Date;

                    cmd.Parameters.Add(
                        "@ReturnNumber",
                        SqlDbType.NVarChar,
                        30).Value =
                        txtReturnNumber.Text.Trim();

                    cmd.Parameters.Add(
                        "@PurchaseID",
                        SqlDbType.Int).Value =
                        _purchaseID;

                    cmd.Parameters.Add(
                        "@SupplierID",
                        SqlDbType.Int).Value =
                        _supplierID;

                    cmd.Parameters.Add(
                        "@WarehouseID",
                        SqlDbType.Int).Value =
                        _warehouseID;

                    SqlParameter pRefund =
                        cmd.Parameters.Add(
                            "@RefundAmount",
                            SqlDbType.Decimal);

                    pRefund.Precision = 18;
                    pRefund.Scale = 2;
                    pRefund.Value = refundAmount;

                    cmd.Parameters.Add(
                        "@Remarks",
                        SqlDbType.NVarChar,
                        500).Value =
                        (object)remarks ??
                        DBNull.Value;

                    cmd.Parameters.Add(
                        "@CreatedBy",
                        SqlDbType.Int).Value =
                        _createdBy;

                    SqlParameter pDetails =
                        cmd.Parameters.Add(
                            "@Details",
                            SqlDbType.Structured);

                    pDetails.TypeName =
                        "dbo.PurchaseReturnDetailType";

                    pDetails.Value = details;

                    cn.Open();

                    object result =
                        cmd.ExecuteScalar();

                    if (result == null ||
                        result == DBNull.Value)
                    {
                        MessageBox.Show(
                            "Purchase Return was not saved because " +
                            "the stored procedure did not return a PurchaseReturnID.",
                            "Purchase Return",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return false;
                    }

                    _purchaseReturnID =
                        Convert.ToInt32(result);
                }

                //_isSaved = true;

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Database error while saving Purchase Return.\n\n" +
                    ex.Message,
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save Purchase Return.\n\n" +
                    ex.Message,
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private void CalculatePurchaseReturnRow(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvPurchaseReturnDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvPurchaseReturnDetails.Rows[rowIndex];

            decimal returnQty = 0m;
            decimal purchasedQty = 0m;
            decimal unitPrice = 0m;
            decimal discount = 0m;
            decimal tax = 0m;

            decimal.TryParse(
                Convert.ToString(row.Cells["ReturnQty"].Value),
                out returnQty);

            decimal.TryParse(
                Convert.ToString(row.Cells["PurchasedQty"].Value),
                out purchasedQty);

            decimal.TryParse(
                Convert.ToString(row.Cells["UnitPrice"].Value),
                out unitPrice);

            decimal.TryParse(
                Convert.ToString(row.Cells["DiscountAmount"].Value),
                out discount);

            decimal.TryParse(
                Convert.ToString(row.Cells["TaxAmount"].Value),
                out tax);

            if (returnQty <= 0m || purchasedQty <= 0m)
            {
                row.Cells["NetAmount"].Value = 0.00m;
                return;
            }

            decimal gross =
                returnQty * unitPrice;

            decimal ratio =
                returnQty / purchasedQty;

            decimal returnDiscount =
                Math.Round(discount * ratio, 2);

            decimal returnTax =
                Math.Round(tax * ratio, 2);

            decimal net =
                gross -
                returnDiscount +
                returnTax;

            row.Cells["NetAmount"].Value =
                Math.Round(net, 2);
        }

        private bool ValidatePurchaseReturn()
        {
            if (_purchaseID <= 0)
            {
                MessageBox.Show(
                    "Please select a purchase first.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (_supplierID <= 0)
            {
                MessageBox.Show(
                    "Please select a supplier.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (_warehouseID <= 0)
            {
                MessageBox.Show(
                    "Please select a warehouse.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReturnNumber.Text))
            {
                MessageBox.Show(
                    "Return number is required.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            bool hasReturnQty = false;

            foreach (DataGridViewRow row
                     in dgvPurchaseReturnDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal returnQty = 0m;
                decimal availableQty = 0m;

                decimal.TryParse(
                    Convert.ToString(
                        row.Cells["ReturnQty"].Value),
                    out returnQty);

                decimal.TryParse(
                    Convert.ToString(
                        row.Cells["AvailableReturnQty"].Value),
                    out availableQty);

                if (returnQty < 0)
                {
                    MessageBox.Show(
                        "Return quantity cannot be negative.",
                        "Purchase Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                if (returnQty > availableQty)
                {
                    MessageBox.Show(
                        "Return quantity cannot exceed available quantity.\n\n" +
                        "Product: " +
                        Convert.ToString(
                            row.Cells["ProductName"].Value) +
                        "\nAvailable: " +
                        availableQty.ToString("N3") +
                        "\nReturn: " +
                        returnQty.ToString("N3"),
                        "Purchase Return",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                if (returnQty > 0)
                    hasReturnQty = true;
            }

            if (!hasReturnQty)
            {
                MessageBox.Show(
                    "Please enter a return quantity for at least one product.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            CalculatePurchaseReturnTotals();

            decimal netAmount = 0m;
            decimal refundAmount = 0m;

            decimal.TryParse(
                txtNetAmount.Text,
                out netAmount);

            decimal.TryParse(
                txtRefundAmount.Text,
                out refundAmount);

            if (netAmount <= 0)
            {
                MessageBox.Show(
                    "Return net amount must be greater than zero.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (refundAmount < 0)
            {
                MessageBox.Show(
                    "Refund amount cannot be negative.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (refundAmount > netAmount)
            {
                MessageBox.Show(
                    "Refund amount cannot exceed return net amount.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // --------------------------------------
        // Form Events
        // --------------------------------------

        private void FrmPurchaseReturn_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
            LoadSuppliers();
            InitializePurchaseReturnForm();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RETURN.NEW"))
            {
                MessageBox.Show("You do not have permission to create new purchase returns.");
                return;
            }
            InitializePurchaseReturnForm();
        }

        private void btnSearchPurchase_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RETURN.SEARCH"))
            {
                MessageBox.Show("You do not have permission to search purchases.");
                return;
            }
            OpenPurchaseSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RETURN.SAVE"))
            {
                MessageBox.Show("You do not have permission to save purchase returns.");
                return;
            }
            if (!ValidatePurchaseReturn())
                return;

            if (SavePurchaseReturn())
            {
                MessageBox.Show(
                    "Purchase Return saved successfully.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnSave.Enabled = false;
                btnPrint.Enabled = true;
                btnExportPDF.Enabled = true;

                dgvPurchaseReturnDetails.ReadOnly = true;

                cboSupplier.Enabled = false;
                cboWarehouse.Enabled = false;

                //txtRefundAmount.ReadOnly = true;
                txtRemarks.ReadOnly = true;
            }
        }

        // --------------------------------------
        // Grid Events Handler
        // --------------------------------------
        private void dgvPurchaseReturnDetails_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvPurchaseReturnDetails.Columns[e.ColumnIndex].Name
                == "ReturnQty")
            {
                CalculatePurchaseReturnTotals();
            }
        }
        private void dgvPurchaseReturnDetails_CellEndEdit(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvPurchaseReturnDetails.Columns[e.ColumnIndex].Name
                != "ReturnQty")
                return;

            DataGridViewRow row =
                dgvPurchaseReturnDetails.Rows[e.RowIndex];

            decimal returnQty = 0m;
            decimal availableQty = 0m;

            decimal.TryParse(
                Convert.ToString(row.Cells["ReturnQty"].Value),
                out returnQty);

            decimal.TryParse(
                Convert.ToString(row.Cells["AvailableReturnQty"].Value),
                out availableQty);

            if (returnQty < 0m)
            {
                MessageBox.Show(
                    "Return quantity cannot be negative.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                row.Cells["ReturnQty"].Value = 0.000m;
                returnQty = 0m;
            }

            if (returnQty > availableQty)
            {
                MessageBox.Show(
                    "Return quantity cannot exceed available quantity.\n\n" +
                    "Available: " + availableQty.ToString("N3"),
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                row.Cells["ReturnQty"].Value = availableQty;
            }

            CalculatePurchaseReturnRow(e.RowIndex);
            CalculatePurchaseReturnTotals();
        }

        private void dgvPurchaseReturnDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvPurchaseReturnDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            // Do not allow modification after the return is saved
            if (_purchaseReturnID > 0)
            {
                MessageBox.Show(
                    "This purchase return cannot be modified.",
                    "Purchase Return",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (e.RowIndex >= dgvPurchaseReturnDetails.Rows.Count)
                return;

            dgvPurchaseReturnDetails.Rows.RemoveAt(e.RowIndex);

            CalculatePurchaseReturnTotals();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RETURN.EXPORT"))
            {
                MessageBox.Show("You do not have permission to export purchase returns to PDF.");
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RETURN.PRINT"))
            {
                MessageBox.Show("You do not have permission to print purchase returns.");
                return;
            }
        }
    }
}
