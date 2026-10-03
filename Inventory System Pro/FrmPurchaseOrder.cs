using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro 
{
    public partial class FrmPurchaseOrder : Form
    {
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        private int CurrentPOID = 0;
        private DataTable _products;

        public FrmPurchaseOrder()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void FrmPurchaseOrder_Load(object sender, EventArgs e)
        {
            SetupDetailsGrid();

            LoadSuppliers();
            LoadWarehouses();

            NewPurchaseOrder();
        }

        // =========================================================
        // SETUP GRID
        // =========================================================

        private void SetupDetailsGrid()
        {
            dgvDetails.Columns.Clear();

            dgvDetails.AllowUserToAddRows = true;
            dgvDetails.AllowUserToDeleteRows = true;
            dgvDetails.AllowUserToResizeRows = false;

            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.SelectionMode =
                DataGridViewSelectionMode.CellSelect;

            dgvDetails.MultiSelect = false;
            dgvDetails.RowHeadersVisible = false;

            // -----------------------------------------------------
            // Product
            // -----------------------------------------------------

            DataGridViewComboBoxColumn productColumn =
                new DataGridViewComboBoxColumn();

            productColumn.Name = "ProductID";
            productColumn.HeaderText = "Product";
            productColumn.DisplayMember = "ProductName";
            productColumn.ValueMember = "ProductID";
            productColumn.Width = 300;
            _products = LoadProductTable();
            productColumn.DataSource = _products;

            dgvDetails.Columns.Add(productColumn);

            // -----------------------------------------------------
            // Ordered Qty
            // -----------------------------------------------------

            DataGridViewTextBoxColumn qtyColumn =
                new DataGridViewTextBoxColumn();

            qtyColumn.Name = "OrderedQty";
            qtyColumn.HeaderText = "Qty";
            qtyColumn.Width = 150;
            qtyColumn.ReadOnly = true;

            dgvDetails.Columns.Add(qtyColumn);

            // -----------------------------------------------------
            // Unit Price
            // -----------------------------------------------------

            DataGridViewTextBoxColumn priceColumn =
                new DataGridViewTextBoxColumn();

            priceColumn.Name = "UnitPrice";
            priceColumn.HeaderText = "Unit Price";
            priceColumn.Width = 150;
            priceColumn.ReadOnly = true;
            priceColumn.DefaultCellStyle.Format = "N4";

            dgvDetails.Columns.Add(priceColumn);

            // -----------------------------------------------------
            // Discount
            // -----------------------------------------------------

            DataGridViewTextBoxColumn discountColumn =
                new DataGridViewTextBoxColumn();

            discountColumn.Name = "DiscountAmount";
            discountColumn.HeaderText = "Discount";
            discountColumn.Width = 150;
            discountColumn.ReadOnly = true;
            discountColumn.DefaultCellStyle.Format = "N2";

            dgvDetails.Columns.Add(discountColumn);

            // -----------------------------------------------------
            // Tax
            // -----------------------------------------------------

            DataGridViewTextBoxColumn taxColumn =
                new DataGridViewTextBoxColumn();

            taxColumn.Name = "TaxAmount";
            taxColumn.HeaderText = "Tax";
            taxColumn.Width = 150;
            taxColumn.ReadOnly = true;
            taxColumn.DefaultCellStyle.Format = "N2";

            dgvDetails.Columns.Add(taxColumn);

            // -----------------------------------------------------
            // Line Total
            // -----------------------------------------------------

            DataGridViewTextBoxColumn totalColumn =
                new DataGridViewTextBoxColumn();

            totalColumn.Name = "LineTotal";
            totalColumn.HeaderText = "Line Total";
            totalColumn.Width = 265;
            totalColumn.ReadOnly = true;
            totalColumn.DefaultCellStyle.Format = "N2";
            totalColumn.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetails.Columns.Add(totalColumn);

            // -----------------------------------------------------
            // Events
            // -----------------------------------------------------

            dgvDetails.CellValueChanged +=
                dgvDetails_CellValueChanged;

            dgvDetails.CurrentCellDirtyStateChanged +=
                dgvDetails_CurrentCellDirtyStateChanged;

            dgvDetails.RowsRemoved +=
                dgvDetails_RowsRemoved;

            dgvDetails.EditingControlShowing +=
                dgvDetails_EditingControlShowing;

            dgvDetails.EditMode =
                DataGridViewEditMode.EditOnKeystrokeOrF2;

            dgvDetails.KeyDown +=
                dgvDetails_KeyDown;

            dgvDetails.RowsAdded +=
                dgvDetails_RowsAdded;

            // -----------------------------------------------------
            // Numeric alignment
            // -----------------------------------------------------

            dgvDetails.Columns["OrderedQty"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetails.Columns["UnitPrice"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetails.Columns["DiscountAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetails.Columns["TaxAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetails.Columns["LineTotal"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;


            // -----------------------------------------------------
            // Add Delete button as LAST column
            // -----------------------------------------------------

            if (!dgvDetails.Columns.Contains("Delete"))
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

                dgvDetails.Columns.Add(deleteColumn);
            }


            // -----------------------------------------------------
            // Header alignment + disable sorting
            // -----------------------------------------------------

            foreach (DataGridViewColumn column
                     in dgvDetails.Columns)
            {
                column.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dgvDetails_RowsAdded(
            object sender,
            DataGridViewRowsAddedEventArgs e)
            {
            for (int i = e.RowIndex;
                 i < e.RowIndex + e.RowCount;
                 i++)
            {
                if (i >= dgvDetails.Rows.Count)
                    continue;

                DataGridViewRow row =
                    dgvDetails.Rows[i];

                if (row.IsNewRow)
                    continue;

                SetDetailEntryEnabled(row, false);

                row.Cells["OrderedQty"].Value = null;
                row.Cells["UnitPrice"].Value = null;
                row.Cells["DiscountAmount"].Value = 0m;
                row.Cells["TaxAmount"].Value = 0m;
                row.Cells["LineTotal"].Value = 0m;
            }
        }

        private void ProductSelected(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            if (row.IsNewRow)
                return;

            object productValue =
                row.Cells["ProductID"].Value;

            if (productValue == null ||
                productValue == DBNull.Value)
            {
                SetDetailEntryEnabled(row, false);

                row.Cells["OrderedQty"].Value = null;
                row.Cells["UnitPrice"].Value = null;
                row.Cells["DiscountAmount"].Value = 0m;
                row.Cells["TaxAmount"].Value = 0m;
                row.Cells["LineTotal"].Value = 0m;

                return;
            }

            int productID;

            if (!int.TryParse(
                productValue.ToString(),
                out productID))
            {
                SetDetailEntryEnabled(row, false);
                return;
            }

            // -------------------------------------------------
            // Prevent duplicate product in the same PO
            // -------------------------------------------------

            //if (ProductAlreadyExists(
            //    productID,
            //    rowIndex))
            //{
            //    MessageBox.Show(
            //        "This product has already been added.",
            //        "Duplicate Product",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Warning);

            //    row.Cells["ProductID"].Value = null;

            //    SetDetailEntryEnabled(row, false);

            //    return;
            //}

            // -------------------------------------------------
            // Prevent duplicate product
            // -------------------------------------------------

            if (ProductAlreadyExists(productID, rowIndex))
            {
                MessageBox.Show(
                    "This product has already been added.",
                    "Duplicate Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Clear the invalid selection
                row.Cells["ProductID"].Value = null;

                row.Cells["OrderedQty"].Value = null;
                row.Cells["UnitPrice"].Value = null;
                row.Cells["DiscountAmount"].Value = 0m;
                row.Cells["TaxAmount"].Value = 0m;
                row.Cells["LineTotal"].Value = 0m;

                SetDetailEntryEnabled(row, false);

                CalculateTotals();

                return;
            }

            // -------------------------------------------------
            // Find product in ComboBox DataSource
            // -------------------------------------------------

            // -------------------------------------------------
            // Find selected product
            // -------------------------------------------------

            if (_products == null ||
                _products.Rows.Count == 0)
            {
                SetDetailEntryEnabled(row, false);
                return;
            }

            DataRow[] found =
                _products.Select(
                    "ProductID = " + productID);

            if (found.Length == 0)
            {
                SetDetailEntryEnabled(row, false);
                return;
            }

            DataRow product =
                found[0];

            // -------------------------------------------------
            // Enable entry fields
            // -------------------------------------------------

            SetDetailEntryEnabled(row, true);

            // Default quantity
            row.Cells["OrderedQty"].Value =
                1.000m;

            // Purchase price from Products table
            row.Cells["UnitPrice"].Value =
                product["PurchasePrice"] == DBNull.Value
                    ? 0m
                    : Convert.ToDecimal(
                        product["PurchasePrice"]);

            // Defaults
            row.Cells["DiscountAmount"].Value =
                0m;

            row.Cells["TaxAmount"].Value =
                0m;

            CalculateLineTotal(rowIndex);
        }

        private void SetDetailEntryEnabled(
            DataGridViewRow row,
            bool enabled)
        {
            row.Cells["OrderedQty"].ReadOnly = !enabled;
            row.Cells["UnitPrice"].ReadOnly = !enabled;
            row.Cells["DiscountAmount"].ReadOnly = !enabled;
            row.Cells["TaxAmount"].ReadOnly = !enabled;

            // Line Total is always calculated by the program
            row.Cells["LineTotal"].ReadOnly = true;
        }

        private void dgvDetails_KeyDown(
        object sender,
        KeyEventArgs e)
        {

        }

        private void CalculateLineTotal(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            decimal qty =
                GetDecimal(row, "OrderedQty");

            decimal unitPrice =
                GetDecimal(row, "UnitPrice");

            decimal discount =
                GetDecimal(row, "DiscountAmount");

            decimal tax =
                GetDecimal(row, "TaxAmount");

            decimal lineTotal =
                (qty * unitPrice)
                - discount
                + tax;

            row.Cells["LineTotal"].Value =
                lineTotal;
        }

        private bool ProductAlreadyExists(
        int productID,
        int currentRowIndex)
            {
                foreach (DataGridViewRow row
                            in dgvDetails.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    if (row.Index == currentRowIndex)
                        continue;

                    object value =
                        row.Cells["ProductID"].Value;

                    if (value == null ||
                        value == DBNull.Value)
                        continue;

                    int existingProductID;

                    if (int.TryParse(
                        value.ToString(),
                        out existingProductID))
                    {
                        if (existingProductID == productID)
                            return true;
                    }
                }

                    return false;
                }

        // =========================================================
        // PRODUCT TABLE
        // =========================================================

        private DataTable LoadProductTable()
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
                        PurchasePrice
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

        // =========================================================
        // SUPPLIERS
        // =========================================================

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT
                SupplierID,
                SupplierName
                FROM dbo.Suppliers
                WHERE IsActive = 1
                ORDER BY SupplierName", cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                cmbSupplier.DataSource = null;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
                cmbSupplier.DataSource = dt;

                if (dt.Rows.Count > 0)
                    cmbSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Supplier Loading Error",
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

        // =========================================================
        // NEW PURCHASE ORDER
        // =========================================================

        private void NewPurchaseOrder()
        {
            CurrentPOID = 0;

            // Header
            txtPONumber.Clear();
            dtpPODate.Value = DateTime.Today;
            txtRemarks.Clear();

            // Supplier / Warehouse
            if (cmbSupplier.Items.Count > 0)
                cmbSupplier.SelectedIndex = 0;

            if (cmbWarehouse.Items.Count > 0)
                cmbWarehouse.SelectedIndex = 0;

            // Details
            dgvDetails.CancelEdit();
            dgvDetails.Rows.Clear();

            // Totals
            ClearTotals();

            // Buttons
            btnApprove.Enabled = false;

            // Start new PO
            txtPONumber.Focus();
        }

        // =========================================================
        // CLEAR TOTALS
        // =========================================================

        private void ClearTotals()
        {
            lblGrossAmount.Text = "0.00";
            lblDiscountAmount.Text = "0.00";
            lblTaxAmount.Text = "0.00";
            lblNetAmount.Text = "0.00";
        }

        // =========================================================
        // GRID VALUE CHANGE
        // =========================================================

        private void dgvDetails_CellValueChanged(
        object sender,
        DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.RowIndex >= dgvDetails.Rows.Count)
                return;

            // Product changed
            if (e.ColumnIndex ==
                dgvDetails.Columns["ProductID"].Index)
            {
                ProductSelected(e.RowIndex);
            }

            CalculateLineTotal(e.RowIndex);

            CalculateTotals();
        }

        // =========================================================
        // COMBOBOX COMMIT
        // =========================================================

        private void dgvDetails_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (dgvDetails.IsCurrentCellDirty)
            {
                dgvDetails.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        // =========================================================
        // ROW REMOVED
        // =========================================================

        private void dgvDetails_RowsRemoved(
            object sender,
            DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotals();
        }

        // =========================================================
        // EDITING CONTROL
        // =========================================================

        private void dgvDetails_EditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;

            if (tb == null)
                return;

            tb.KeyPress -= Decimal_KeyPress;
            tb.KeyPress += Decimal_KeyPress;
        }

        // =========================================================
        // DECIMAL VALIDATION
        // =========================================================

        private void Decimal_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb == null)
                return;

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.' &&
                !tb.Text.Contains("."))
                return;

            e.Handled = true;
        }

        // =========================================================
        // CALCULATE LINE
        // =========================================================

        private void CalculateLine(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            if (row.IsNewRow)
                return;

            decimal qty =
                GetDecimal(row, "OrderedQty");

            decimal price =
                GetDecimal(row, "UnitPrice");

            decimal discount =
                GetDecimal(row, "DiscountAmount");

            decimal tax =
                GetDecimal(row, "TaxAmount");

            decimal gross =
                qty * price;

            decimal lineTotal =
                gross - discount + tax;

            if (lineTotal < 0)
                lineTotal = 0;

            row.Cells["LineTotal"].Value =
                lineTotal.ToString("N2");
        }

        // =========================================================
        // CALCULATE TOTALS
        // =========================================================

        private void CalculateTotals()
        {
            decimal gross = 0;
            decimal discount = 0;
            decimal tax = 0;

            foreach (DataGridViewRow row
                     in dgvDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row, "OrderedQty");

                decimal price =
                    GetDecimal(row, "UnitPrice");

                decimal rowDiscount =
                    GetDecimal(row, "DiscountAmount");

                decimal rowTax =
                    GetDecimal(row, "TaxAmount");

                gross += qty * price;
                discount += rowDiscount;
                tax += rowTax;
            }

            decimal net =
                gross - discount + tax;

            if (net < 0)
                net = 0;

            lblGrossAmount.Text =
                gross.ToString("N2");

            lblDiscountAmount.Text =
                discount.ToString("N2");

            lblTaxAmount.Text =
                tax.ToString("N2");

            lblNetAmount.Text =
                net.ToString("N2");
        }

        // =========================================================
        // GET DECIMAL FROM GRID
        // =========================================================

        private decimal GetDecimal(
            DataGridViewRow row,
            string columnName)
        {
            object value =
                row.Cells[columnName].Value;

            if (value == null ||
                value == DBNull.Value)
                return 0;

            decimal result;

            if (decimal.TryParse(
                    value.ToString(),
                    out result))
            {
                return result;
            }

            return 0;
        }

        // =========================================================
        // VALIDATE HEADER
        // =========================================================

        private bool ValidateHeader()
        {
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a supplier.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbSupplier.Focus();
                return false;
            }

            if (cmbWarehouse.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a warehouse.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbWarehouse.Focus();
                return false;
            }

            return true;
        }

        // =========================================================
        // BUILD TVP
        // =========================================================

        private DataTable BuildDetailsTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(
                "ProductID",
                typeof(int));

            dt.Columns.Add(
                "OrderedQty",
                typeof(decimal));

            dt.Columns.Add(
                "UnitPrice",
                typeof(decimal));

            dt.Columns.Add(
                "DiscountAmount",
                typeof(decimal));

            dt.Columns.Add(
                "TaxAmount",
                typeof(decimal));

            foreach (DataGridViewRow row
                     in dgvDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                object productValue =
                    row.Cells["ProductID"].Value;

                if (productValue == null ||
                    productValue == DBNull.Value)
                    continue;

                int productID;

                if (!int.TryParse(
                        productValue.ToString(),
                        out productID))
                    continue;

                decimal qty =
                    GetDecimal(row, "OrderedQty");

                decimal price =
                    GetDecimal(row, "UnitPrice");

                decimal discount =
                    GetDecimal(row, "DiscountAmount");

                decimal tax =
                    GetDecimal(row, "TaxAmount");

                if (qty <= 0)
                {
                    throw new Exception(
                        "Ordered quantity must be greater than zero.");
                }

                if (price < 0)
                {
                    throw new Exception(
                        "Unit price cannot be negative.");
                }

                if (discount < 0)
                {
                    throw new Exception(
                        "Discount cannot be negative.");
                }

                if (tax < 0)
                {
                    throw new Exception(
                        "Tax cannot be negative.");
                }

                DataRow dr = dt.NewRow();

                dr["ProductID"] = productID;
                dr["OrderedQty"] = qty;
                dr["UnitPrice"] = price;
                dr["DiscountAmount"] = discount;
                dr["TaxAmount"] = tax;

                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count == 0)
            {
                throw new Exception(
                    "Purchase Order must contain at least one item.");
            }

            return dt;
        }

        // =========================================================
        // SAVE PURCHASE ORDER
        // =========================================================

        private void SavePurchaseOrder()
        {
            if (!ValidateHeader())
                return;

            try
            {
                DataTable details =
                    BuildDetailsTable();

                MessageBox.Show("Rows being saved: " + details.Rows.Count,
                                "PO Details Debug",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                int supplierID =
                    Convert.ToInt32(
                        cmbSupplier.SelectedValue);

                int warehouseID =
                    Convert.ToInt32(
                        cmbWarehouse.SelectedValue);

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_PurchaseOrder_Save",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@PODate",
                            SqlDbType.Date).Value =
                            dtpPODate.Value.Date;

                        // PONumber is now generated by SQL Server.
                        // Do NOT send @PONumber from C#.

                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            supplierID;

                        cmd.Parameters.Add(
                            "@WarehouseID",
                            SqlDbType.Int).Value =
                            warehouseID;

                        cmd.Parameters.Add(
                            "@Remarks",
                            SqlDbType.NVarChar,
                            500).Value =
                            string.IsNullOrWhiteSpace(
                                txtRemarks.Text)
                                ? (object)DBNull.Value
                                : txtRemarks.Text.Trim();

                        // -------------------------------------------------
                        // IMPORTANT:
                        // Replace this with your actual logged-in UserID
                        // when authentication is connected.
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@CreatedBy",
                            SqlDbType.Int).Value = 1;

                        SqlParameter tvp =
                            cmd.Parameters.AddWithValue(
                                "@Details",
                                details);

                        tvp.SqlDbType =
                            SqlDbType.Structured;

                        tvp.TypeName =
                            "dbo.PurchaseOrderDetailType";

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // -----------------------------------------
                                // GET GENERATED POID
                                // -----------------------------------------

                                if (reader["POID"] !=
                                    DBNull.Value)
                                {
                                    CurrentPOID =
                                        Convert.ToInt32(
                                            reader["POID"]);
                                }

                                // -----------------------------------------
                                // GET GENERATED PO NUMBER
                                // -----------------------------------------

                                if (reader["PONumber"] !=
                                    DBNull.Value)
                                {
                                    txtPONumber.Text =
                                        reader["PONumber"]
                                            .ToString();
                                }

                                // -----------------------------------------
                                // CHECK RESULT
                                // -----------------------------------------

                                string result =
                                    reader["Result"]
                                        .ToString();

                                if (result != "SUCCESS")
                                {
                                    MessageBox.Show(
                                        result,
                                        "Purchase Order",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Purchase Order was not saved.",
                                    "Purchase Order",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }
                        }
                    }
                }

                // ---------------------------------------------
                // SUCCESS
                // ---------------------------------------------

                MessageBox.Show(
                    "Purchase Order saved successfully.\n\n" +
                    "PO Number: " + txtPONumber.Text +
                    "\nPOID: " + CurrentPOID,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnApprove.Enabled =
                    CurrentPOID > 0;
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
                    "Purchase Order Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // APPROVE PURCHASE ORDER
        // =========================================================

        private void ApprovePurchaseOrder()
        {
            if (CurrentPOID <= 0)
            {
                MessageBox.Show(
                    "Save the Purchase Order first.",
                    "Approval",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Approve Purchase Order " +
                    txtPONumber.Text.Trim() +
                    "?\n\nApproval does NOT create stock.",
                    "Confirm Approval",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_PurchaseOrder_Approve",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@POID",
                            SqlDbType.Int).Value =
                            CurrentPOID;

                        // Replace with actual logged-in UserID.
                        cmd.Parameters.Add(
                            "@ApprovedBy",
                            SqlDbType.Int).Value = 1;

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Purchase Order approved successfully.\n\n" +
                    "POID: " + CurrentPOID +
                    "\n\nNo stock has been posted yet.",
                    "Approved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnApprove.Enabled = false;
                btnSave.Enabled = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Approval Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Approval Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NEW BUTTON
        // =========================================================

        private void btnNew_Click(
            object sender,
            EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_ORDER.NEW"))
            {
                MessageBox.Show("You do not have permission to create new purchase orders.");
                return;
            }
            NewPurchaseOrder();
            btnSave.Enabled = true;
        }

        // =========================================================
        // SAVE BUTTON
        // =========================================================

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_ORDER.SAVE"))
            {
                MessageBox.Show("You do not have permission to save purchase orders.");
                return;
            }
            SavePurchaseOrder();
        }

        // =========================================================
        // APPROVE BUTTON
        // =========================================================

        private void btnApprove_Click(
            object sender,
            EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_ORDER.APPROVE"))
            {
                MessageBox.Show("You do not have permission to create new purchase orders.");
                return;
            }
            ApprovePurchaseOrder();
        }

        // =========================================================
        // CLOSE BUTTON
        // =========================================================

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        // =========================================================
        // DATAGRID CELL CONTENT CLICK
        // =========================================================
        private void dgvDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            // Don't modify an already saved Purchase Order
            if (CurrentPOID > 0)
            {
                MessageBox.Show(
                    "This purchase order cannot be modified.",
                    "Purchase Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Never delete the new-row placeholder
            if (dgvDetails.Rows[e.RowIndex].IsNewRow)
                return;

            dgvDetails.Rows.RemoveAt(e.RowIndex);

            // Your existing PO total calculation
            CalculateTotals();
        }

        private void dgvDetails_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvDetails.Columns[e.ColumnIndex].Name != "ProductID")
                return;

            if (dgvDetails.Rows[e.RowIndex].IsNewRow)
                return;

            if (dgvDetails.ReadOnly)
                return;

            dgvDetails.CurrentCell =
                dgvDetails.Rows[e.RowIndex].Cells["ProductID"];

            dgvDetails.BeginEdit(true);

            if (dgvDetails.EditingControl
                is DataGridViewComboBoxEditingControl combo)
            {
                combo.DroppedDown = true;
            }
        }
        private bool RequirePermission(string permissionKey)
        {
            if (AppSession.HasPermission(permissionKey))
                return true;

            MessageBox.Show("You do not have permission to perform this action.");
            return false;
        }
    }
}