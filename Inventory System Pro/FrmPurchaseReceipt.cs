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
    public partial class FrmPurchaseReceipt : Form
    {

        private int CurrentSupplierID = 0;
        private int CurrentWarehouseID = 0;
        private int CurrentPurchaseID = 0;
        private int CurrentPOID = 0;
        private bool _loadingPurchaseOrder = false;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;
        public FrmPurchaseReceipt()
        {
            InitializeComponent();
            cmbPurchaseOrder.SelectedIndexChanged +=
                cmbPurchaseOrder_SelectedIndexChanged;

            dgvDetails.CellValidating +=
                dgvDetails_CellValidating;

            dgvDetails.CurrentCellDirtyStateChanged +=
                dgvDetails_CurrentCellDirtyStateChanged;

            txtPaidAmount.TextChanged +=
                txtPaidAmount_TextChanged;

            txtPaidAmount.KeyPress +=
                txtPaidAmount_KeyPress;

            btnReceive.Click +=
                btnReceive_Click;
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PURCHASE_RECEIPT.RECEIVE"))
            {
                MessageBox.Show("You do not have permission to create purchase receipts.");
                return;
            }
            SavePurchase();
        }

        private void txtPaidAmount_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.' &&
                !txtPaidAmount.Text.Contains("."))
                return;

            e.Handled = true;
        }

        private void cmbPurchaseOrder_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbPurchaseOrder.SelectedIndex < 0)
                return;

            if (cmbPurchaseOrder.SelectedValue == null)
                return;

            if (cmbPurchaseOrder.SelectedValue
                is DataRowView)
                return;

            if (!int.TryParse(
                cmbPurchaseOrder.SelectedValue.ToString(),
                out int poID))
                return;

            LoadPurchaseOrder(poID);
        }

        private void LoadPurchaseOrder(int poID)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                            SELECT
                                PO.POID,
                                PO.PONumber,
                                PO.SupplierID,
                                S.SupplierName,
                                PO.WarehouseID,
                                W.WarehouseName
                            FROM dbo.PurchaseOrders PO
                            INNER JOIN dbo.Suppliers S
                                ON S.SupplierID = PO.SupplierID
                            INNER JOIN dbo.Warehouses W
                                ON W.WarehouseID = PO.WarehouseID
                            WHERE PO.POID = @POID
                              AND PO.Status IN ('OPEN', 'PARTIAL');";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@POID",
                            SqlDbType.Int).Value = poID;

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
                        "The selected Purchase Order is not available for receipt.",
                        "Purchase Order",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    dgvDetails.Rows.Clear();

                    txtSupplier.Clear();
                    txtWarehouse.Clear();

                    btnReceive.Enabled = false;

                    return;
                }

                DataRow header = dt.Rows[0];

                CurrentPOID =
                    Convert.ToInt32(
                        header["POID"]);

                CurrentSupplierID =
                    Convert.ToInt32(
                        header["SupplierID"]);

                CurrentWarehouseID =
                    Convert.ToInt32(
                        header["WarehouseID"]);

                txtSupplier.Text =
                    header["SupplierName"].ToString();

                txtWarehouse.Text =
                    header["WarehouseName"].ToString();

                LoadPurchaseOrderDetails(CurrentPOID);

                btnReceive.Enabled =
                    dgvDetails.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Load Purchase Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseOrderDetails(int poID)
        {
            _loadingPurchaseOrder = true;

            try
            {
                dgvDetails.Rows.Clear();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    D.PODetailID,
                    D.ProductID,
                    P.ProductCode,
                    P.ProductName,

                    D.OrderedQty,
                    D.ReceivedQty,

                    D.OrderedQty - D.ReceivedQty
                        AS RemainingQty,

                    D.UnitPrice,
                    D.DiscountAmount,
                    D.TaxAmount

                FROM dbo.PurchaseOrderDetails D

                INNER JOIN dbo.Products P
                    ON P.ProductID = D.ProductID

                WHERE D.POID = @POID
                  AND D.ReceivedQty < D.OrderedQty

                ORDER BY D.PODetailID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@POID",
                            SqlDbType.Int).Value = poID;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex =
                                    dgvDetails.Rows.Add();

                                DataGridViewRow row =
                                    dgvDetails.Rows[rowIndex];

                                row.Cells["PODetailID"].Value =
                                    reader["PODetailID"];

                                row.Cells["ProductID"].Value =
                                    reader["ProductID"];

                                row.Cells["ProductCode"].Value =
                                    reader["ProductCode"];

                                row.Cells["ProductName"].Value =
                                    reader["ProductName"];

                                decimal orderedQty =
                                    Convert.ToDecimal(reader["OrderedQty"]);

                                decimal receivedQty =
                                    Convert.ToDecimal(reader["ReceivedQty"]);

                                decimal remainingQty =
                                    orderedQty - receivedQty;

                                row.Cells["OrderedQty"].Value =
                                    orderedQty;

                                row.Cells["ReceivedQty"].Value =
                                    receivedQty;

                                row.Cells["RemainingQty"].Value =
                                    remainingQty;

                                row.Cells["Qty"].Value = 0;

                                row.Cells["UnitPrice"].Value =
                                    reader["UnitPrice"];

                                row.Cells["DiscountAmount"].Value =
                                    reader["DiscountAmount"];

                                row.Cells["TaxAmount"].Value =
                                    reader["TaxAmount"];

                                row.Cells["LineTotal"].Value = 0;
                            }
                        }
                    }
                }

                CalculateTotals();
            }
            finally
            {
                _loadingPurchaseOrder = false;
            }
        }

        private void dgvDetails_CellValidating(
            object sender,
            DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvDetails.Columns[e.ColumnIndex].Name != "Qty")
                return;

            DataGridViewRow row =
                dgvDetails.Rows[e.RowIndex];

            decimal orderedQty =
                GetDecimal(row, "OrderedQty");

            decimal receivedQty =
                GetDecimal(row, "ReceivedQty");

            decimal remainingQty =
                orderedQty - receivedQty;

            string text =
                e.FormattedValue?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(text))
                text = "0";

            if (!decimal.TryParse(text, out decimal qty))
            {
                MessageBox.Show(
                    "Please enter a valid quantity.",
                    "Invalid Quantity",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
                return;
            }

            if (qty < 0)
            {
                MessageBox.Show(
                    "Receive quantity cannot be negative.",
                    "Invalid Quantity",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
                return;
            }

            if (qty > remainingQty)
            {
                MessageBox.Show(
                    "Receive quantity cannot be greater than the remaining quantity.\n\n" +
                    "Remaining: " +
                    remainingQty.ToString("N3"),
                    "Invalid Quantity",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
                return;
            }
        }

        private void FrmPurchaseReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                dtpPurchaseDate.Value = DateTime.Today;

                txtPurchaseNumber.ReadOnly = true;
                txtSupplier.ReadOnly = true;
                txtWarehouse.ReadOnly = true;

                txtGrossAmount.ReadOnly = true;
                txtDiscountAmount.ReadOnly = true;
                txtTaxAmount.ReadOnly = true;
                txtNetAmount.ReadOnly = true;
                txtBalanceAmount.ReadOnly = true;

                SetupDetailsGrid();

                LoadPurchaseOrders();

                GeneratePurchaseNumber();

                ClearReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

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

        private void dgvDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loadingPurchaseOrder)
                return;

            if (e.RowIndex < 0)
                return;

            if (e.RowIndex >= dgvDetails.Rows.Count)
                return;

            string columnName =
                dgvDetails.Columns[e.ColumnIndex].Name;

            ValidateReceiveQuantity(e.RowIndex);

            if (columnName == "Qty" ||
                columnName == "UnitPrice" ||
                columnName == "DiscountAmount" ||
                columnName == "TaxAmount")
            {
                CalculateLineTotal(e.RowIndex);
                CalculateTotals();
            }
        }

        private decimal GetDecimal(DataGridViewRow row, string columnName)
        {
            if (row == null)
                return 0;

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

        private void CalculateLineTotal(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            decimal qty =
                GetDecimal(row, "Qty");

            decimal unitPrice =
                GetDecimal(row, "UnitPrice");

            decimal discount =
                GetDecimal(row, "DiscountAmount");

            decimal tax =
                GetDecimal(row, "TaxAmount");

            decimal orderedQty =
                GetDecimal(row, "OrderedQty");

            if (orderedQty <= 0 || qty <= 0)
            {
                row.Cells["LineTotal"].Value = 0;
                return;
            }

            // Allocate PO discount/tax proportionally
            decimal discountPerUnit =
                discount / orderedQty;

            decimal taxPerUnit =
                tax / orderedQty;

            decimal receiveDiscount =
                discountPerUnit * qty;

            decimal receiveTax =
                taxPerUnit * qty;

            decimal lineTotal =
                (qty * unitPrice)
                - receiveDiscount
                + receiveTax;

            if (lineTotal < 0)
                lineTotal = 0;

            row.Cells["LineTotal"].Value =
                lineTotal;
        }

        private void LoadPurchaseOrders()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                            SELECT
                                POID,
                                PONumber
                            FROM dbo.PurchaseOrders
                            WHERE Status IN ('OPEN', 'PARTIAL')
                            ORDER BY PODate DESC, POID DESC;";

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

            cmbPurchaseOrder.DataSource = null;

            cmbPurchaseOrder.DisplayMember =
                "PONumber";

            cmbPurchaseOrder.ValueMember =
                "POID";

            cmbPurchaseOrder.DataSource = dt;

            cmbPurchaseOrder.SelectedIndex = -1;
        }

        private void CalculateTotals()
        {
            decimal gross = 0;
            decimal discount = 0;
            decimal tax = 0;
            decimal net = 0;

            foreach (DataGridViewRow row
                     in dgvDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row, "Qty");

                decimal unitPrice =
                    GetDecimal(row, "UnitPrice");

                decimal orderedQty =
                    GetDecimal(row, "OrderedQty");

                decimal poDiscount =
                    GetDecimal(row, "DiscountAmount");

                decimal poTax =
                    GetDecimal(row, "TaxAmount");

                if (qty <= 0 || orderedQty <= 0)
                    continue;

                decimal lineGross =
                    qty * unitPrice;

                decimal lineDiscount =
                    (poDiscount / orderedQty) * qty;

                decimal lineTax =
                    (poTax / orderedQty) * qty;

                decimal lineNet =
                    lineGross
                    - lineDiscount
                    + lineTax;

                gross += lineGross;
                discount += lineDiscount;
                tax += lineTax;
                net += lineNet;
            }

            txtGrossAmount.Text =
                gross.ToString("N2");

            txtDiscountAmount.Text =
                discount.ToString("N2");

            txtTaxAmount.Text =
                tax.ToString("N2");

            txtNetAmount.Text =
                net.ToString("N2");

            CalculateBalance();
        }

        private void CalculateBalance()
        {
            decimal netAmount = 0;
            decimal paidAmount = 0;

            decimal.TryParse(
                txtNetAmount.Text,
                out netAmount);

            decimal.TryParse(
                txtPaidAmount.Text,
                out paidAmount);

            decimal balance =
                netAmount - paidAmount;

            txtBalanceAmount.Text =
                balance.ToString("N2");
        }

        private void txtPaidAmount_TextChanged(
            object sender,
            EventArgs e)
        {
            CalculateBalance();
        }

        private void GeneratePurchaseNumber()
        {
            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                            SELECT
                                ISNULL(
                                    MAX(
                                        TRY_CONVERT(
                                            INT,
                                            RIGHT(PurchaseNumber, 6)
                                        )
                                    ),
                                    0
                                ) + 1
                            FROM dbo.Purchases
                            WHERE PurchaseNumber LIKE 'PUR-%';";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cn.Open();

                    int nextNumber =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    txtPurchaseNumber.Text =
                        "PUR-" +
                        nextNumber.ToString("D6");
                }
            }
        }

        private void ClearReceipt()
        {
            CurrentPurchaseID = 0;
            CurrentPOID = 0;

            dtpPurchaseDate.Value =
                DateTime.Today;

            cmbPurchaseOrder.SelectedIndex = -1;

            txtSupplier.Clear();
            txtWarehouse.Clear();

            txtPaidAmount.Text = "0.00";

            txtRemarks.Clear();

            txtGrossAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtBalanceAmount.Text = "0.00";

            dgvDetails.Rows.Clear();

            GeneratePurchaseNumber();

            btnReceive.Enabled = false;
        }

        private void SetupDetailsGrid()
        {
            dgvDetails.Columns.Clear();

            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.AllowUserToDeleteRows = false;
            dgvDetails.AllowUserToResizeRows = false;

            dgvDetails.AutoGenerateColumns = false;

            dgvDetails.RowHeadersVisible = false;

            dgvDetails.SelectionMode =
                DataGridViewSelectionMode.CellSelect;

            dgvDetails.MultiSelect = false;

            dgvDetails.EditMode =
                DataGridViewEditMode.EditOnKeystrokeOrF2;

            // -------------------------------------------------
            // Product ID
            // -------------------------------------------------

            AddGridTextColumn(
                "ProductID",
                "ID",
                25,
                true);

            // -------------------------------------------------
            // Product Code
            // -------------------------------------------------

            AddGridTextColumn(
                "ProductCode",
                "Code",
                100,
                true);

            // -------------------------------------------------
            // Product Name
            // -------------------------------------------------

            AddGridTextColumn(
                "ProductName",
                "Product",
                220,
                true);

            // -------------------------------------------------
            // Ordered Qty
            // -------------------------------------------------

            AddGridTextColumn(
                "OrderedQty",
                "Ordered",
                90,
                true,
                "N3");

            // -------------------------------------------------
            // Received Qty
            // -------------------------------------------------

            AddGridTextColumn(
                "ReceivedQty",
                "Received",
                100,
                true,
                "N3");

            // -------------------------------------------------
            // Remaining Qty
            // -------------------------------------------------

            AddGridTextColumn(
                "RemainingQty",
                "Remaining",
                100,
                true,
                "N3");

            // -------------------------------------------------
            // Receive Qty - ONLY EDITABLE COLUMN
            // -------------------------------------------------

            AddGridTextColumn(
                "Qty",
                "Receive Qty",
                110,
                false,
                "N3");

            // -------------------------------------------------
            // Unit Price
            // -------------------------------------------------

            AddGridTextColumn(
                "UnitPrice",
                "Unit Price",
                110,
                true,
                "N4");

            // -------------------------------------------------
            // Discount
            // -------------------------------------------------

            AddGridTextColumn(
                "DiscountAmount",
                "Discount",
                70,
                true,
                "N2");

            // -------------------------------------------------
            // Tax
            // -------------------------------------------------

            AddGridTextColumn(
                "TaxAmount",
                "Tax",
                70,
                true,
                "N2");

            // -------------------------------------------------
            // Line Total
            // -------------------------------------------------

            AddGridTextColumn(
                "LineTotal",
                "Line Total",
                130,
                true,
                "N2");

            // -------------------------------------------------
            // Hidden PODetailID
            // -------------------------------------------------

            DataGridViewTextBoxColumn poDetailColumn =
                new DataGridViewTextBoxColumn();

            poDetailColumn.Name =
                "PODetailID";

            poDetailColumn.HeaderText =
                "PODetailID";

            poDetailColumn.Visible = false;

            poDetailColumn.ReadOnly = true;

            dgvDetails.Columns.Add(
                poDetailColumn);

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
            deleteColumn.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            dgvDetails.Columns.Add(deleteColumn);

            // -------------------------------------------------
            // Grid formatting
            // -------------------------------------------------

            foreach (DataGridViewColumn column
                     in dgvDetails.Columns)
            {
                column.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                column.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            // Text columns left aligned

            dgvDetails.Columns["ProductCode"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvDetails.Columns["ProductName"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // -------------------------------------------------
            // Events
            // -------------------------------------------------

            dgvDetails.CellEndEdit +=
                dgvDetails_CellEndEdit;

            dgvDetails.CurrentCellDirtyStateChanged +=
                dgvDetails_CurrentCellDirtyStateChanged;

            dgvDetails.KeyDown +=
                dgvDetails_KeyDown;
        }

        private void dgvDetails_KeyDown(
                    object sender,
                    KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F2)
                return;

            if (dgvDetails.CurrentCell == null)
                return;

            if (dgvDetails.CurrentCell.ReadOnly)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            dgvDetails.BeginEdit(true);
        }

        private void AddGridTextColumn(
            string name,
            string header,
            int width,
            bool readOnly,
            string format = null)
        {
            DataGridViewTextBoxColumn column =
                new DataGridViewTextBoxColumn();

            column.Name = name;
            column.HeaderText = header;
            column.Width = width;
            column.ReadOnly = readOnly;

            if (!string.IsNullOrEmpty(format))
                column.DefaultCellStyle.Format = format;

            dgvDetails.Columns.Add(column);
        }

        private void dgvDetails_CellEndEdit(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            string columnName =
                dgvDetails.Columns[e.ColumnIndex].Name;

            if (columnName != "Qty")
                return;

            dgvDetails.CommitEdit(
                DataGridViewDataErrorContexts.Commit);

            ValidateReceiveQuantity(e.RowIndex);

            CalculateLineTotal(e.RowIndex);

            CalculateTotals();
        }

        private DataTable BuildPurchaseDetailsTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("PODetailID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("DiscountAmount", typeof(decimal));
            dt.Columns.Add("TaxAmount", typeof(decimal));

            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row, "Qty");

                // Ignore lines that user is not receiving
                if (qty <= 0)
                    continue;

                int podetailID =
                    Convert.ToInt32(
                        row.Cells["PODetailID"].Value);

                int productID =
                    Convert.ToInt32(
                        row.Cells["ProductID"].Value);

                decimal orderedQty =
                    GetDecimal(row, "OrderedQty");

                decimal poDiscount =
                    GetDecimal(row, "DiscountAmount");

                decimal poTax =
                    GetDecimal(row, "TaxAmount");

                decimal unitPrice =
                    GetDecimal(row, "UnitPrice");

                // Allocate PO discount/tax proportionally
                decimal discount =
                    orderedQty > 0
                        ? Math.Round(
                            (poDiscount / orderedQty) * qty,
                            2)
                        : 0;

                decimal tax =
                    orderedQty > 0
                        ? Math.Round(
                            (poTax / orderedQty) * qty,
                            2)
                        : 0;

                DataRow detail =
                    dt.NewRow();

                detail["PODetailID"] = podetailID;
                detail["ProductID"] = productID;
                detail["Qty"] = qty;
                detail["UnitPrice"] = unitPrice;
                detail["DiscountAmount"] = discount;
                detail["TaxAmount"] = tax;

                dt.Rows.Add(detail);
            }

            return dt;
        }

        private bool ValidatePurchaseReceipt(
            out decimal paidAmount)
        {
            paidAmount = 0;

            if (CurrentPOID <= 0)
            {
                MessageBox.Show(
                    "Please select a Purchase Order.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            bool hasQty = false;

            foreach (DataGridViewRow row
                     in dgvDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row, "Qty");

                decimal orderedQty =
                    GetDecimal(row, "OrderedQty");

                decimal receivedQty =
                    GetDecimal(row, "ReceivedQty");

                decimal remaining =
                    orderedQty - receivedQty;

                if (qty > 0)
                {
                    hasQty = true;

                    if (qty > remaining)
                    {
                        MessageBox.Show(
                            "Receive quantity cannot exceed remaining quantity.",
                            "Purchase Receipt",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                }
            }

            if (!hasQty)
            {
                MessageBox.Show(
                    "Enter Receive Qty for at least one product.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (!decimal.TryParse(
                txtPaidAmount.Text.Trim(),
                out paidAmount))
            {
                MessageBox.Show(
                    "Enter a valid paid amount.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPaidAmount.Focus();

                return false;
            }

            if (paidAmount < 0)
            {
                MessageBox.Show(
                    "Paid amount cannot be negative.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            decimal netAmount =
                GetTextBoxDecimal(txtNetAmount);

            if (paidAmount > netAmount)
            {
                MessageBox.Show(
                    "Paid amount cannot exceed the purchase amount.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }
        private decimal GetTextBoxDecimal(TextBox textBox)
        {
            if (textBox == null)
                return 0;

            if (decimal.TryParse(
                textBox.Text.Trim(),
                out decimal value))
            {
                return value;
            }

            return 0;
        }

        private void SavePurchase()
        {
            if (!ValidatePurchaseReceipt(
                out decimal paidAmount))
            {
                return;
            }

            try
            {
                DataTable details =
                    BuildPurchaseDetailsTable();

                if (details.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No purchase items were selected.",
                        "Purchase Receipt",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int supplierID =
                    CurrentSupplierID;

                int warehouseID =
                    CurrentWarehouseID;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_Purchase_Save",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@PurchaseDate",
                            SqlDbType.Date).Value =
                            dtpPurchaseDate.Value.Date;

                        cmd.Parameters.Add(
                            "@PurchaseNumber",
                            SqlDbType.NVarChar,
                            30).Value =
                            txtPurchaseNumber.Text.Trim();

                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            supplierID;

                        cmd.Parameters.Add(
                            "@WarehouseID",
                            SqlDbType.Int).Value =
                            warehouseID;

                        cmd.Parameters.Add(
                            "@POID",
                            SqlDbType.Int).Value =
                            CurrentPOID;

                        cmd.Parameters.Add(
                            "@PaidAmount",
                            SqlDbType.Decimal).Value =
                            paidAmount;

                        cmd.Parameters[
                            "@PaidAmount"]
                            .Precision = 18;

                        cmd.Parameters[
                            "@PaidAmount"]
                            .Scale = 2;

                        cmd.Parameters.Add(
                            "@Remarks",
                            SqlDbType.NVarChar,
                            500).Value =
                            string.IsNullOrWhiteSpace(
                                txtRemarks.Text)
                                ? (object)DBNull.Value
                                : txtRemarks.Text.Trim();

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
                            "dbo.PurchaseDetailType";

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Purchase was not saved.",
                                    "Purchase Receipt",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            string result =
                                reader["Result"]
                                    .ToString();

                            if (result != "SUCCESS")
                            {
                                MessageBox.Show(
                                    result,
                                    "Purchase Receipt",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            CurrentPurchaseID =
                                Convert.ToInt32(
                                    reader["PurchaseID"]);

                            txtGrossAmount.Text =
                                Convert.ToDecimal(
                                    reader["GrossAmount"])
                                    .ToString("N2");

                            txtDiscountAmount.Text =
                                Convert.ToDecimal(
                                    reader["DiscountAmount"])
                                    .ToString("N2");

                            txtTaxAmount.Text =
                                Convert.ToDecimal(
                                    reader["TaxAmount"])
                                    .ToString("N2");

                            txtNetAmount.Text =
                                Convert.ToDecimal(
                                    reader["NetAmount"])
                                    .ToString("N2");

                            txtPaidAmount.Text =
                                Convert.ToDecimal(
                                    reader["PaidAmount"])
                                    .ToString("N2");

                            txtBalanceAmount.Text =
                                Convert.ToDecimal(
                                    reader["BalanceAmount"])
                                    .ToString("N2");
                        }
                    }
                }

                MessageBox.Show(
                    "Purchase received successfully.\n\n" +
                    "Purchase No: " +
                    txtPurchaseNumber.Text +
                    "\nPurchase ID: " +
                    CurrentPurchaseID,
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadPurchaseOrders();

                ClearReceipt();
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
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearReceipt();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvDetails.Columns[e.ColumnIndex].Name != "Delete")
                return;

            // Do not allow modification after posting
            if (dgvDetails.ReadOnly)
            {
                MessageBox.Show(
                    "This purchase receipt cannot be modified.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (e.RowIndex >= dgvDetails.Rows.Count)
                return;

            dgvDetails.Rows.RemoveAt(e.RowIndex);

            CalculateTotals();
        }

        private void CalculateRemaining(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            decimal orderedQty =
                GetDecimal(row, "OrderedQty");

            decimal receivedQty =
                GetDecimal(row, "ReceivedQty");

            decimal receiveQty =
                GetDecimal(row, "Qty");

            // Remaining after this receipt
            decimal remaining =
                orderedQty - receivedQty - receiveQty;

            if (remaining < 0)
                remaining = 0;

            row.Cells["RemainingQty"].Value =
                remaining;
        }

        private void ValidateReceiveQuantity(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvDetails.Rows.Count)
                return;

            DataGridViewRow row =
                dgvDetails.Rows[rowIndex];

            decimal orderedQty =
                GetDecimal(row, "OrderedQty");

            decimal receivedQty =
                GetDecimal(row, "ReceivedQty");

            decimal qty =
                GetDecimal(row, "Qty");

            // Available quantity BEFORE current receipt
            decimal availableQty =
                orderedQty - receivedQty;

            if (availableQty < 0)
                availableQty = 0;

            if (qty < 0)
            {
                row.Cells["Qty"].Value = 0;

                MessageBox.Show(
                    "Receive quantity cannot be negative.",
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (qty > availableQty)
            {
                row.Cells["Qty"].Value =
                    availableQty;

                MessageBox.Show(
                    "Receive quantity cannot exceed the remaining quantity.\n\n" +
                    "Remaining Qty: " +
                    availableQty.ToString("N3"),
                    "Purchase Receipt",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            // Now update displayed remaining
            CalculateRemaining(rowIndex);
        }

        private void ApplyPermissions()
        {
            //btnNew.Enabled =
            //    AppSession.HasPermission("PURCHASE_RECEIPT.CREATE");

            //btnPrint.Enabled =
            //    AppSession.HasPermission("PURCHASE_RECEIPT.PRINT");

            //btnExport.Enabled =
            //    AppSession.HasPermission("PURCHASE_RECEIPT.EXPORT");
        }
    }
}
