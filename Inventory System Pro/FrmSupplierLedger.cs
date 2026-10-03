using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Inventory_System_Pro
{
    public partial class FrmSupplierLedger : Form
    {
        private DataTable _statementTable;
        private int _printRowIndex;
        private decimal _printTotalDebit;
        private decimal _printTotalCredit;
        private decimal _printOpeningBalance;
        private decimal _printClosingBalance;
        private int CurrentSupplierID = 0;
        private decimal CurrentSupplierOpeningBalance = 0m;
        //int pageNumber = 1;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmSupplierLedger()
        {
            InitializeComponent();
            SetupPurchaseDetailsGrid();
            //SetupPaymentDetailsGrid();
            dgvSupplierLedger.CellDoubleClick +=
                    dgvSupplierLedger_CellDoubleClick;
        }

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                                SELECT
                                SupplierID,
                                SupplierCode,
                                SupplierName,
                                OpeningBalance
                                FROM dbo.Suppliers
                                WHERE IsActive = 1
                                ORDER BY SupplierName;";

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

                cboSupplier.DataSource = null;

                cboSupplier.DisplayMember =
                    "SupplierName";

                cboSupplier.ValueMember =
                    "SupplierID";

                cboSupplier.DataSource = dt;

                cboSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Load Suppliers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadSupplierLedger()
        {
            if (cboSupplier.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a supplier.",
                    "Supplier Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    cboSupplier.SelectedValue.ToString(),
                    out int supplierID))
            {
                MessageBox.Show(
                    "Invalid supplier.",
                    "Supplier Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DateTime fromDate =
                dtpFromDate.Value.Date;

            DateTime toDate =
                dtpToDate.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show(
                    "From date cannot be greater than To date.",
                    "Supplier Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DataTable dt = new DataTable();

                decimal openingBalance = 0;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    cn.Open();

                    // -------------------------------------------------
                    // 1. OPENING BALANCE
                    // -------------------------------------------------

                    string openingSql = @"
                                        SELECT
                                            ISNULL(S.OpeningBalance, 0)
                                            +
                                            ISNULL(
                                                (
                                                    SELECT SUM(
                                                        SL.DebitAmount -
                                                        SL.CreditAmount
                                                    )
                                                    FROM dbo.SupplierLedger SL
                                                    WHERE SL.SupplierID = S.SupplierID
                                                      AND SL.TranDate < @FromDate
                                                ),
                                                0
                                            )
                                        FROM dbo.Suppliers S
                                        WHERE S.SupplierID = @SupplierID;";

                    using (SqlCommand cmd =
                           new SqlCommand(openingSql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            supplierID;

                        cmd.Parameters.Add(
                            "@FromDate",
                            SqlDbType.DateTime2).Value =
                            fromDate;

                        object result =
                            cmd.ExecuteScalar();

                        openingBalance =
                            result == null ||
                            result == DBNull.Value
                                ? 0m
                                : Convert.ToDecimal(result);
                    }


                    // -------------------------------------------------
                    // 2. PERIOD TRANSACTIONS
                    // -------------------------------------------------

                    string sql = @"
                                SELECT
                                    SL.SupplierLedgerID,
                                    SL.TranDate,
                                    SL.ReferenceNo,
                                    SL.TranType,
                                    SL.Remarks,
                                    SL.DebitAmount,
                                    SL.CreditAmount

                                FROM dbo.SupplierLedger SL

                                WHERE SL.SupplierID = @SupplierID

                                    AND SL.TranDate >= @FromDate

                                    AND SL.TranDate <
                                        DATEADD(
                                            DAY,
                                            1,
                                            @ToDate
                                        )

                                ORDER BY
                                    SL.TranDate,
                                    SL.SupplierLedgerID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            supplierID;

                        cmd.Parameters.Add(
                            "@FromDate",
                            SqlDbType.DateTime2).Value =
                            fromDate;

                        cmd.Parameters.Add(
                            "@ToDate",
                            SqlDbType.DateTime2).Value =
                            toDate;

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }


                // -------------------------------------------------
                // 3. ADD OPENING BALANCE TO GRID
                // -------------------------------------------------

                DataTable displayTable =
                    new DataTable();

                displayTable.Columns.Add(
                    "SupplierLedgerID",
                    typeof(long));

                displayTable.Columns.Add(
                    "TranDate",
                    typeof(DateTime));

                displayTable.Columns.Add(
                    "ReferenceNo",
                    typeof(string));

                displayTable.Columns.Add(
                    "TranType",
                    typeof(string));

                displayTable.Columns.Add(
                    "Remarks",
                    typeof(string));

                displayTable.Columns.Add(
                    "DebitAmount",
                    typeof(decimal));

                displayTable.Columns.Add(
                    "CreditAmount",
                    typeof(decimal));

                displayTable.Columns.Add(
                    "Balance",
                    typeof(decimal));


                // Opening row
                DataRow openingRow =
                    displayTable.NewRow();

                openingRow["SupplierLedgerID"] =
                    DBNull.Value;

                openingRow["TranDate"] =
                    fromDate;

                openingRow["ReferenceNo"] =
                    "";

                openingRow["TranType"] =
                    "OPENING";

                openingRow["Remarks"] =
                    "Opening Balance";

                openingRow["DebitAmount"] =
                    0;

                openingRow["CreditAmount"] =
                    0;

                openingRow["Balance"] =
                    openingBalance;

                displayTable.Rows.Add(openingRow);


                // -------------------------------------------------
                // 4. RUNNING BALANCE
                // -------------------------------------------------

                decimal runningBalance =
                    openingBalance;

                decimal totalDebit = 0;
                decimal totalCredit = 0;

                foreach (DataRow sourceRow
                         in dt.Rows)
                {
                    decimal debit =
                        Convert.ToDecimal(
                            sourceRow["DebitAmount"]);

                    decimal credit =
                        Convert.ToDecimal(
                            sourceRow["CreditAmount"]);

                    runningBalance +=
                        debit - credit;

                    totalDebit += debit;
                    totalCredit += credit;

                    DataRow row =
                        displayTable.NewRow();

                    row["SupplierLedgerID"] =
                        sourceRow["SupplierLedgerID"];

                    row["TranDate"] =
                        sourceRow["TranDate"];

                    row["ReferenceNo"] =
                        sourceRow["ReferenceNo"];

                    row["TranType"] =
                        sourceRow["TranType"];

                    row["Remarks"] =
                        sourceRow["Remarks"];

                    row["DebitAmount"] =
                        debit;

                    row["CreditAmount"] =
                        credit;

                    row["Balance"] =
                        runningBalance;

                    displayTable.Rows.Add(row);
                }


                // -------------------------------------------------
                // 5. DISPLAY
                // -------------------------------------------------

                dgvSupplierLedger.DataSource =
                    displayTable;

                FormatSupplierLedgerGrid();


                // -------------------------------------------------
                // 6. TOTALS
                // -------------------------------------------------

                txtOpeningBalance.Text =
                    openingBalance.ToString("N2");

                txtTotalDebit.Text =
                    totalDebit.ToString("N2");

                txtTotalCredit.Text =
                    totalCredit.ToString("N2");

                txtClosingBalance.Text =
                    runningBalance.ToString("N2");
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
                    "Supplier Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatSupplierLedgerGrid()
        {
            if (dgvSupplierLedger.Columns.Count == 0)
                return;

            dgvSupplierLedger.AutoGenerateColumns = true;
            dgvSupplierLedger.AllowUserToAddRows = false;
            dgvSupplierLedger.AllowUserToDeleteRows = false;
            dgvSupplierLedger.ReadOnly = true;
            dgvSupplierLedger.RowHeadersVisible = false;
            dgvSupplierLedger.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSupplierLedger.MultiSelect = false;

            // -------------------------------------------------
            // COLUMN HEADERS
            // -------------------------------------------------

            dgvSupplierLedger.Columns["SupplierLedgerID"]
                .HeaderText = "ID";

            dgvSupplierLedger.Columns["TranDate"]
                .HeaderText = "Date";

            dgvSupplierLedger.Columns["ReferenceNo"]
                .HeaderText = "Reference";

            dgvSupplierLedger.Columns["TranType"]
                .HeaderText = "Transaction";

            dgvSupplierLedger.Columns["Remarks"]
                .HeaderText = "Remarks";

            dgvSupplierLedger.Columns["DebitAmount"]
                .HeaderText = "Debit";

            dgvSupplierLedger.Columns["CreditAmount"]
                .HeaderText = "Credit";

            dgvSupplierLedger.Columns["Balance"]
                .HeaderText = "Balance";


            // -------------------------------------------------
            // HIDE INTERNAL ID
            // -------------------------------------------------

            dgvSupplierLedger.Columns["SupplierLedgerID"]
                .Visible = false;


            // -------------------------------------------------
            // DATE FORMAT
            // -------------------------------------------------

            dgvSupplierLedger.Columns["TranDate"]
                .DefaultCellStyle.Format =
                "dd-MMM-yyyy";


            // -------------------------------------------------
            // AMOUNT FORMAT
            // -------------------------------------------------

            dgvSupplierLedger.Columns["DebitAmount"]
                .DefaultCellStyle.Format =
                "N2";

            dgvSupplierLedger.Columns["CreditAmount"]
                .DefaultCellStyle.Format =
                "N2";

            dgvSupplierLedger.Columns["Balance"]
                .DefaultCellStyle.Format =
                "N2";


            // -------------------------------------------------
            // ALIGNMENT
            // -------------------------------------------------

            dgvSupplierLedger.Columns["TranDate"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvSupplierLedger.Columns["ReferenceNo"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvSupplierLedger.Columns["TranType"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvSupplierLedger.Columns["Remarks"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;


            // ONLY NUMERIC DATA RIGHT ALIGNED

            dgvSupplierLedger.Columns["DebitAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvSupplierLedger.Columns["CreditAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvSupplierLedger.Columns["Balance"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;


            // -------------------------------------------------
            // HEADER ALIGNMENT
            // -------------------------------------------------

            foreach (DataGridViewColumn column
                     in dgvSupplierLedger.Columns)
            {
                column.SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }


            // -------------------------------------------------
            // WIDTHS
            // -------------------------------------------------

            dgvSupplierLedger.Columns["TranDate"]
                .Width = 100;

            dgvSupplierLedger.Columns["ReferenceNo"]
                .Width = 130;

            dgvSupplierLedger.Columns["TranType"]
                .Width = 150;

            dgvSupplierLedger.Columns["Remarks"]
                .Width = 300;

            dgvSupplierLedger.Columns["DebitAmount"]
                .Width = 110;

            dgvSupplierLedger.Columns["CreditAmount"]
                .Width = 110;

            dgvSupplierLedger.Columns["Balance"]
                .Width = 120;


            // -------------------------------------------------
            // OPENING BALANCE ROW
            // -------------------------------------------------

            foreach (DataGridViewRow row
                     in dgvSupplierLedger.Rows)
            {
                if (row.Cells["TranType"].Value != null &&
                    row.Cells["TranType"].Value.ToString()
                        == "OPENING")
                {
                    row.DefaultCellStyle.Font =
                        new Font(
                            dgvSupplierLedger.Font,
                            FontStyle.Bold);

                    break;
                }
            }
        }

        private void SetSupplierLedgerHeader()
        {
            if (cboSupplier.SelectedValue == null)
            {
                lblSupplierName.Text =
                    "Supplier Ledger";

                return;
            }

            DataRowView drv =
                cboSupplier.SelectedItem as DataRowView;

            if (drv != null)
            {
                lblSupplierName.Text =
                    "Supplier Ledger - " +
                    drv["SupplierName"].ToString();
            }
        }

        private void SetStatementPeriod()
        {
            lblStatementPeriod.Text =
                "Period: " +
                dtpFromDate.Value.ToString("dd-MMM-yyyy") +
                " to " +
                dtpToDate.Value.ToString("dd-MMM-yyyy");
        }

        private void dgvSupplierLedger_CellDoubleClick(
                object sender,
                DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvSupplierLedger.Rows[e.RowIndex];

            string tranType =
                row.Cells["TranType"].Value?
                .ToString()
                .Trim();

            string referenceNo =
                row.Cells["ReferenceNo"].Value?
                .ToString()
                .Trim();

            // Clear both detail grids
            dgvPurchaseDetails.Rows.Clear();
            //dgvPaymentDetails.Rows.Clear();

            if (string.IsNullOrWhiteSpace(tranType))
                return;

            // ---------------------------------------------
            // OPENING BALANCE
            // ---------------------------------------------

            if (tranType.Equals(
                "OPENING",
                StringComparison.OrdinalIgnoreCase))
            {
                dgvPurchaseDetails.Rows.Clear();
                dgvPurchaseDetails.Columns.Clear();

                lblDetailTitle.Text =
                    "Opening Balance";

                return;
            }

            // ---------------------------------------------
            // PURCHASE
            // ---------------------------------------------

            if (tranType.Equals(
                    "PURCHASE",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(referenceNo))
                    return;

                lblDetailTitle.Text =
                    "Purchase Details - " + referenceNo;
                LoadPurchaseDetails(referenceNo);

                return;
            }

            // ---------------------------------------------
            // SUPPLIER PAYMENT
            // ---------------------------------------------

            if (tranType.Equals(
                "SUPPLIER_PAYMENT",
                StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(referenceNo))
                    return;

                if (row.Cells["TranDate"].Value == null ||
                    row.Cells["TranDate"].Value == DBNull.Value)
                    return;

                DateTime paymentDate =
                    Convert.ToDateTime(
                        row.Cells["TranDate"].Value);

                lblDetailTitle.Text =
                    "Payment Details - " + referenceNo;

                LoadPaymentDetails(
                    referenceNo,
                    paymentDate);

                return;
            }

            // OPENING or any other transaction
            dgvPurchaseDetails.Rows.Clear();
            dgvPurchaseDetails.Columns.Clear();
            lblDetailTitle.Text = "Transaction Details";

        }

        private decimal GetDecimal(
                DataGridViewRow row,
                string columnName)
        {
            if (row == null)
                return 0m;

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

        private void LoadSupplierOpeningBalance(int supplierID)
        {
            CurrentSupplierOpeningBalance = 0m;

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                            SELECT OpeningBalance
                            FROM dbo.Suppliers
                            WHERE SupplierID = @SupplierID;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@SupplierID",
                        SqlDbType.Int).Value =
                        supplierID;

                    cn.Open();

                    object value =
                        cmd.ExecuteScalar();

                    if (value != null &&
                        value != DBNull.Value)
                    {
                        CurrentSupplierOpeningBalance =
                            Convert.ToDecimal(value);
                    }
                }
            }
        }

        private void cboSupplier_SelectedIndexChanged(
                    object sender,
                    EventArgs e)
        {
            if (cboSupplier.SelectedIndex < 0)
                return;

            if (cboSupplier.SelectedValue == null)
                return;

            if (cboSupplier.SelectedValue is DataRowView)
                return;

            int supplierID;

            if (!int.TryParse(
                cboSupplier.SelectedValue.ToString(),
                out supplierID))
                return;

            CurrentSupplierID = supplierID;

            SetSupplierLedgerHeader();
            SetStatementPeriod();

            LoadSupplierOpeningBalance(CurrentSupplierID);

            LoadSupplierLedger();

            dgvPurchaseDetails.DataSource = null;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgvSupplierLedger.DataSource = null;
            dgvPurchaseDetails.Rows.Clear();
            dgvPurchaseDetails.Columns.Clear();
            lblDetailTitle.Text = "Transaction Details";
            LoadSupplierLedger();
        }

        private void LoadPurchaseDetails(string purchaseNumber)
        {
            dgvPurchaseDetails.Rows.Clear();

            if (string.IsNullOrWhiteSpace(purchaseNumber))
                return;

            SetupPurchaseDetailsGrid();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                            SELECT
                                P.PurchaseID,
                                P.PurchaseNumber,
                                P.PurchaseDate,

                                D.PurchaseDetailID,
                                D.ProductID,

                                PR.ProductCode,
                                PR.ProductName,

                                D.Qty,
                                D.UnitPrice,
                                D.DiscountAmount,
                                D.TaxAmount,
                                D.NetAmount

                            FROM dbo.Purchases P

                            INNER JOIN dbo.PurchaseDetails D
                                ON D.PurchaseID = P.PurchaseID

                            INNER JOIN dbo.Products PR
                                ON PR.ProductID = D.ProductID

                            WHERE P.PurchaseNumber = @PurchaseNumber

                            ORDER BY D.PurchaseDetailID;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@PurchaseNumber",
                        SqlDbType.NVarChar,
                        60).Value =
                        purchaseNumber.Trim();

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex =
                                dgvPurchaseDetails.Rows.Add();

                            DataGridViewRow row =
                                dgvPurchaseDetails.Rows[rowIndex];

                            row.Cells["PurchaseID"].Value =
                                reader["PurchaseID"];

                            row.Cells["PurchaseNumber"].Value =
                                reader["PurchaseNumber"];

                            row.Cells["PurchaseDate"].Value =
                                reader["PurchaseDate"];

                            row.Cells["PurchaseDetailID"].Value =
                                reader["PurchaseDetailID"];

                            row.Cells["ProductID"].Value =
                                reader["ProductID"];

                            row.Cells["ProductCode"].Value =
                                reader["ProductCode"];

                            row.Cells["ProductName"].Value =
                                reader["ProductName"];

                            row.Cells["Qty"].Value =
                                reader["Qty"];

                            row.Cells["UnitPrice"].Value =
                                reader["UnitPrice"];

                            row.Cells["DiscountAmount"].Value =
                                reader["DiscountAmount"];

                            row.Cells["TaxAmount"].Value =
                                reader["TaxAmount"];

                            row.Cells["NetAmount"].Value =
                                reader["NetAmount"];
                        }
                    }
                }
            }
            CalculatePurchaseDetailTotals();
        }

        private void CalculatePurchaseDetailTotals()
        {
            decimal totalQty = 0;
            decimal gross = 0;
            decimal discount = 0;
            decimal tax = 0;
            decimal net = 0;

            foreach (DataGridViewRow row
                     in dgvPurchaseDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal qty =
                    GetDecimal(row, "Qty");

                decimal unitPrice =
                    GetDecimal(row, "UnitPrice");

                decimal rowDiscount =
                    GetDecimal(row, "DiscountAmount");

                decimal rowTax =
                    GetDecimal(row, "TaxAmount");

                decimal rowNet =
                    GetDecimal(row, "NetAmount");

                totalQty += qty;

                gross += qty * unitPrice;

                discount += rowDiscount;

                tax += rowTax;

                net += rowNet;
            }

            txtDetailQty.Text =
                totalQty.ToString("N3");

            txtDetailGross.Text =
                gross.ToString("N2");

            txtDetailDiscount.Text =
                discount.ToString("N2");

            txtDetailTax.Text =
                tax.ToString("N2");

            txtDetailNet.Text =
                net.ToString("N2");
        }

        private void SetupPurchaseDetailsGrid()
        {
            dgvPurchaseDetails.Rows.Clear();
            dgvPurchaseDetails.Columns.Clear();

            dgvPurchaseDetails.AutoGenerateColumns = false;
            dgvPurchaseDetails.AllowUserToAddRows = false;
            dgvPurchaseDetails.AllowUserToDeleteRows = false;
            dgvPurchaseDetails.AllowUserToResizeRows = false;
            dgvPurchaseDetails.ReadOnly = true;
            dgvPurchaseDetails.RowHeadersVisible = false;

            dgvPurchaseDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPurchaseDetails.MultiSelect = false;

            // Hidden technical columns
            AddDetailColumn("PurchaseID", "Purchase ID", 0, false);
            AddDetailColumn("PurchaseNumber", "Purchase Number", 0, false);
            AddDetailColumn("PurchaseDate", "Purchase Date", 0, false);
            AddDetailColumn("PurchaseDetailID", "Detail ID", 0, false);
            AddDetailColumn("ProductID", "Product ID", 0, false);

            // Visible columns
            AddDetailColumn("ProductCode", "Code", 90, true);
            AddDetailColumn("ProductName", "Product", 220, true);
            AddDetailColumn("Qty", "Qty", 90, true, "N3");
            AddDetailColumn("UnitPrice", "Unit Price", 110, true, "N4");
            AddDetailColumn("DiscountAmount", "Discount", 100, true, "N2");
            AddDetailColumn("TaxAmount", "Tax", 100, true, "N2");
            AddDetailColumn("NetAmount", "Net", 120, true, "N2");

            AlignDetailColumns();
        }

        private void AlignDetailColumns()
        {
            string[] rightAligned =
            {
                "Qty",
                "UnitPrice",
                "DiscountAmount",
                "TaxAmount",
                "NetAmount",
                "Amount"
            };

            foreach (string columnName in rightAligned)
            {
                if (dgvPurchaseDetails.Columns.Contains(columnName))
                {
                    dgvPurchaseDetails.Columns[columnName]
                        .DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvPurchaseDetails.Columns.Contains("ProductCode"))
            {
                dgvPurchaseDetails.Columns["ProductCode"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvPurchaseDetails.Columns.Contains("ProductName"))
            {
                dgvPurchaseDetails.Columns["ProductName"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void AddDetailColumn(
            string name,
            string headerText,
            int width,
            bool visible,
            string format = null)
        {
            DataGridViewTextBoxColumn column =
                new DataGridViewTextBoxColumn();

            column.Name = name;
            column.HeaderText = headerText;
            column.Width = width;
            column.Visible = visible;
            column.ReadOnly = true;

            column.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            column.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            if (!string.IsNullOrWhiteSpace(format))
            {
                column.DefaultCellStyle.Format = format;
            }

            dgvPurchaseDetails.Columns.Add(column);
        }

        private void FrmSupplierLedger_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(
            DateTime.Today.Year,
            DateTime.Today.Month,
            1);

            dtpToDate.Value = DateTime.Today;
            dgvSupplierLedger.DataSource = null;
            dgvPurchaseDetails.Rows.Clear();
            lblStatementPeriod.Text = "";
            lblSupplierName.Text = "";
            ClearDetailTotals();
            LoadSuppliers();
            ClearSupplierLedger();
        }

        private void SetupPaymentDetailsGrid()
        {
            dgvPurchaseDetails.Rows.Clear();
            dgvPurchaseDetails.Columns.Clear();

            dgvPurchaseDetails.AutoGenerateColumns = false;
            dgvPurchaseDetails.AllowUserToAddRows = false;
            dgvPurchaseDetails.AllowUserToDeleteRows = false;
            dgvPurchaseDetails.AllowUserToResizeRows = false;
            dgvPurchaseDetails.ReadOnly = true;
            dgvPurchaseDetails.RowHeadersVisible = false;

            dgvPurchaseDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPurchaseDetails.MultiSelect = false;

            AddDetailColumn(
                "PaymentNumber",
                "Payment No.",
                120,
                true);

            AddDetailColumn(
                "PaymentDate",
                "Payment Date",
                120,
                true,
                "dd-MMM-yyyy");

            AddDetailColumn(
                "CashAccount",
                "Cash Account",
                160,
                true);

            AddDetailColumn(
                "Amount",
                "Amount",
                120,
                true,
                "N2");

            AddDetailColumn(
                "Remarks",
                "Remarks",
                350,
                true);

            // Amount right aligned
            dgvPurchaseDetails.Columns["Amount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadPaymentDetails(
            string paymentNumber,
            DateTime paymentDate)
        {
            ClearDetailTotals();

            if (string.IsNullOrWhiteSpace(paymentNumber))
                return;

            SetupPaymentDetailsGrid();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
            SELECT
                SL.ReferenceNo AS PaymentNumber,
                SL.TranDate AS PaymentDate,
                CA.AccountName AS CashAccount,
                SL.CreditAmount AS Amount,
                SL.Remarks

            FROM dbo.SupplierLedger AS SL

            LEFT JOIN dbo.CashTransactions AS CT
                ON CT.ReferenceNo = SL.ReferenceNo
               AND CT.TranType = 'SUPPLIER_PAYMENT'
               AND CAST(CT.TranDate AS DATE) =
                   CAST(SL.TranDate AS DATE)

            LEFT JOIN dbo.CashAccounts AS CA
                ON CA.CashAccountID = CT.CashAccountID

            WHERE SL.TranType = 'SUPPLIER_PAYMENT'
              AND SL.ReferenceNo = @PaymentNumber
              AND CAST(SL.TranDate AS DATE) = @PaymentDate;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@PaymentNumber",
                        SqlDbType.NVarChar,
                        60).Value =
                        paymentNumber.Trim();

                    cmd.Parameters.Add(
                        "@PaymentDate",
                        SqlDbType.Date).Value =
                        paymentDate.Date;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex =
                                dgvPurchaseDetails.Rows.Add();

                            DataGridViewRow row =
                                dgvPurchaseDetails.Rows[rowIndex];

                            row.Cells["PaymentNumber"].Value =
                                reader["PaymentNumber"];

                            row.Cells["PaymentDate"].Value =
                                reader["PaymentDate"];

                            row.Cells["CashAccount"].Value =
                                reader["CashAccount"];

                            row.Cells["Amount"].Value =
                                reader["Amount"];

                            row.Cells["Remarks"].Value =
                                reader["Remarks"];
                        }
                    }
                }
            }
        }

        private void ClearDetailTotals()
        {
            txtDetailQty.Text = "0.000";
            txtDetailGross.Text = "0.00";
            txtDetailDiscount.Text = "0.00";
            txtDetailTax.Text = "0.00";
            txtDetailNet.Text = "0.00";
        }

        private DataTable GetSupplierStatement(
    int supplierID,
    DateTime fromDate,
    DateTime toDate)
        {
            DataTable statement = new DataTable();

            statement.Columns.Add(
                "TranDate",
                typeof(DateTime));

            statement.Columns.Add(
                "ReferenceNo",
                typeof(string));

            statement.Columns.Add(
                "TranType",
                typeof(string));

            statement.Columns.Add(
                "Remarks",
                typeof(string));

            statement.Columns.Add(
                "DebitAmount",
                typeof(decimal));

            statement.Columns.Add(
                "CreditAmount",
                typeof(decimal));

            statement.Columns.Add(
                "Balance",
                typeof(decimal));


            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                cn.Open();

                // -------------------------------------------------
                // 1. OPENING BALANCE
                // -------------------------------------------------

                decimal openingBalance = 0;

                string openingSql = @"
            SELECT
                ISNULL(
                    SUM(DebitAmount - CreditAmount),
                    0
                )
            FROM dbo.SupplierLedger

            WHERE SupplierID = @SupplierID

              AND TranDate < @FromDate;";

                using (SqlCommand cmd =
                       new SqlCommand(openingSql, cn))
                {
                    cmd.Parameters.Add(
                        "@SupplierID",
                        SqlDbType.Int).Value =
                        supplierID;

                    cmd.Parameters.Add(
                        "@FromDate",
                        SqlDbType.DateTime2).Value =
                        fromDate;

                    openingBalance =
                        Convert.ToDecimal(
                            cmd.ExecuteScalar());
                }


                // -------------------------------------------------
                // 2. OPENING ROW
                // -------------------------------------------------

                DataRow openingRow =
                    statement.NewRow();

                openingRow["TranDate"] =
                    fromDate;

                openingRow["ReferenceNo"] =
                    "";

                openingRow["TranType"] =
                    "OPENING";

                openingRow["Remarks"] =
                    "Opening Balance";

                openingRow["DebitAmount"] =
                    0m;

                openingRow["CreditAmount"] =
                    0m;

                openingRow["Balance"] =
                    openingBalance;

                statement.Rows.Add(openingRow);


                // -------------------------------------------------
                // 3. PERIOD TRANSACTIONS
                // -------------------------------------------------

                string sql = @"
            SELECT
                SL.TranDate,
                SL.ReferenceNo,
                SL.TranType,
                SL.Remarks,
                SL.DebitAmount,
                SL.CreditAmount

            FROM dbo.SupplierLedger SL

            WHERE SL.SupplierID = @SupplierID

              AND SL.TranDate >= @FromDate

              AND SL.TranDate <
                    DATEADD(
                        DAY,
                        1,
                        @ToDate
                    )

            ORDER BY
                SL.TranDate,
                SL.SupplierLedgerID;";


                decimal runningBalance =
                    openingBalance;


                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@SupplierID",
                        SqlDbType.Int).Value =
                        supplierID;

                    cmd.Parameters.Add(
                        "@FromDate",
                        SqlDbType.DateTime2).Value =
                        fromDate;

                    cmd.Parameters.Add(
                        "@ToDate",
                        SqlDbType.DateTime2).Value =
                        toDate;


                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal debit =
                                reader["DebitAmount"] == DBNull.Value
                                    ? 0m
                                    : Convert.ToDecimal(
                                        reader["DebitAmount"]);

                            decimal credit =
                                reader["CreditAmount"] == DBNull.Value
                                    ? 0m
                                    : Convert.ToDecimal(
                                        reader["CreditAmount"]);


                            runningBalance +=
                                debit - credit;


                            DataRow row =
                                statement.NewRow();

                            row["TranDate"] =
                                reader["TranDate"];

                            row["ReferenceNo"] =
                                reader["ReferenceNo"];

                            row["TranType"] =
                                reader["TranType"];

                            row["Remarks"] =
                                reader["Remarks"];

                            row["DebitAmount"] =
                                debit;

                            row["CreditAmount"] =
                                credit;

                            row["Balance"] =
                                runningBalance;

                            statement.Rows.Add(row);
                        }
                    }
                }
            }


            return statement;
        }

        private DataTable BuildCurrentSupplierStatement()
        {
            if (cboSupplier.SelectedValue == null)
                throw new Exception("Please select a supplier.");

            if (!int.TryParse(
                    cboSupplier.SelectedValue.ToString(),
                    out int supplierID))
            {
                throw new Exception("Invalid supplier.");
            }

            DateTime fromDate =
                dtpFromDate.Value.Date;

            DateTime toDate =
                dtpToDate.Value.Date;

            if (fromDate > toDate)
                throw new Exception(
                    "From date cannot be greater than To date.");

            return GetSupplierStatement(
                supplierID,
                fromDate,
                toDate);
        }

        private void btnTestStatement_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt =
                    BuildCurrentSupplierStatement();

                // Check if supplier is selected and valid
                if (!int.TryParse(
                                cboSupplier.SelectedValue?.ToString(),
                                out int supplierID))
                {
                    MessageBox.Show(
                        "Invalid supplier.",
                        "PDF Export",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataTable supplierInfo =
                    GetSupplierInfo(supplierID);

                if (supplierInfo.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Supplier information could not be found.",
                        "PDF Export",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    $"Statement rows: {dt.Rows.Count}",
                    "Supplier Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Supplier Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnPrintStatement_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                _statementTable =
                    BuildCurrentSupplierStatement();

                if (_statementTable.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No transactions found for the selected period.",
                        "Supplier Statement",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                CalculatePrintTotals();

                _printRowIndex = 0;

                PrintDocument printDocument =
                    new PrintDocument();

                printDocument.DocumentName =
                    "Supplier Statement";

                printDocument.PrintPage +=
                    PrintSupplierStatementPage;

                using (PrintPreviewDialog preview =
                       new PrintPreviewDialog())
                {
                    preview.Document =
                        printDocument;

                    preview.WindowState =
                        FormWindowState.Maximized;

                    preview.ShowDialog();
                }

                printDocument.PrintPage -=
                    PrintSupplierStatementPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Supplier Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CalculatePrintTotals()
        {
            _printTotalDebit = 0m;
            _printTotalCredit = 0m;
            _printOpeningBalance = 0m;
            _printClosingBalance = 0m;

            if (_statementTable.Rows.Count == 0)
                return;

            // Opening balance
            DataRow openingRow =
                _statementTable.Rows[0];

            _printOpeningBalance =
                Convert.ToDecimal(
                    openingRow["Balance"]);

            foreach (DataRow row
                     in _statementTable.Rows)
            {
                _printTotalDebit +=
                    Convert.ToDecimal(
                        row["DebitAmount"]);

                _printTotalCredit +=
                    Convert.ToDecimal(
                        row["CreditAmount"]);
            }

            // Last row contains the closing balance
            DataRow lastRow =
                _statementTable.Rows[
                    _statementTable.Rows.Count - 1];

            _printClosingBalance =
                Convert.ToDecimal(
                    lastRow["Balance"]);
        }

        private void PrintSupplierStatementPage(
    object sender,
    PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float left = e.MarginBounds.Left;
            float top = e.MarginBounds.Top;
            float pageWidth = e.MarginBounds.Width;
            float bottom = e.MarginBounds.Bottom;

            using (Font titleFont =
                   new Font(
                       "Arial",
                       16,
                       FontStyle.Bold))

            using (Font headerFont =
                   new Font(
                       "Arial",
                       9,
                       FontStyle.Bold))

            using (Font normalFont =
                   new Font(
                       "Arial",
                       9,
                       FontStyle.Regular))

            using (Font boldFont =
                   new Font(
                       "Arial",
                       9,
                       FontStyle.Bold))

            using (Pen pen =
                   new Pen(Color.Black))

            {
                float y = top;

                // ---------------------------------------------
                // TITLE
                // ---------------------------------------------

                string title =
                    "SUPPLIER STATEMENT";

                SizeF titleSize =
                    g.MeasureString(
                        title,
                        titleFont);

                g.DrawString(
                    title,
                    titleFont,
                    Brushes.Black,
                    left +
                    (pageWidth - titleSize.Width) / 2,
                    y);

                y += 35;


                // ---------------------------------------------
                // SUPPLIER INFORMATION
                // ---------------------------------------------

                string supplierName =
                    cboSupplier.Text;

                g.DrawString(
                    "Supplier: " + supplierName,
                    boldFont,
                    Brushes.Black,
                    left,
                    y);

                y += 20;

                g.DrawString(
                    "Period: " +
                    dtpFromDate.Value.ToString("dd-MMM-yyyy") +
                    " to " +
                    dtpToDate.Value.ToString("dd-MMM-yyyy"),
                    normalFont,
                    Brushes.Black,
                    left,
                    y);

                y += 30;


                // ---------------------------------------------
                // OPENING BALANCE
                // ---------------------------------------------

                g.DrawString(
                    "Opening Balance:",
                    boldFont,
                    Brushes.Black,
                    left,
                    y);

                g.DrawString(
                    _printOpeningBalance.ToString("N2"),
                    boldFont,
                    Brushes.Black,
                    left + pageWidth - 100,
                    y);

                y += 25;


                // ---------------------------------------------
                // TABLE HEADER
                // ---------------------------------------------

                float dateWidth = 75;
                float referenceWidth = 95;
                float typeWidth = 100;
                float debitWidth = 85;
                float creditWidth = 85;
                float balanceWidth = 95;

                float remarksWidth =
                    pageWidth -
                    dateWidth -
                    referenceWidth -
                    typeWidth -
                    debitWidth -
                    creditWidth -
                    balanceWidth;


                g.DrawLine(
                    pen,
                    left,
                    y,
                    left + pageWidth,
                    y);

                y += 5;


                g.DrawString(
                    "Date",
                    headerFont,
                    Brushes.Black,
                    left,
                    y);

                g.DrawString(
                    "Reference",
                    headerFont,
                    Brushes.Black,
                    left + dateWidth,
                    y);

                g.DrawString(
                    "Transaction",
                    headerFont,
                    Brushes.Black,
                    left +
                    dateWidth +
                    referenceWidth,
                    y);

                g.DrawString(
                    "Remarks",
                    headerFont,
                    Brushes.Black,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth,
                    y);

                g.DrawString(
                    "Debit",
                    headerFont,
                    Brushes.Black,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth,
                    y);

                g.DrawString(
                    "Credit",
                    headerFont,
                    Brushes.Black,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth +
                    debitWidth,
                    y);

                g.DrawString(
                    "Balance",
                    headerFont,
                    Brushes.Black,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth +
                    debitWidth +
                    creditWidth,
                    y);

                y += 20;

                g.DrawLine(
                    pen,
                    left,
                    y,
                    left + pageWidth,
                    y);

                y += 5;


                // ---------------------------------------------
                // TRANSACTION ROWS
                // ---------------------------------------------

                while (_printRowIndex <
                       _statementTable.Rows.Count)
                {
                    DataRow row =
                        _statementTable.Rows[
                            _printRowIndex];

                    float rowHeight = 20;

                    if (y + rowHeight > bottom - 70)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    DateTime tranDate =
                        Convert.ToDateTime(
                            row["TranDate"]);

                    string reference =
                        row["ReferenceNo"]?
                        .ToString();

                    string tranType =
                        row["TranType"]?
                        .ToString();

                    string remarks =
                        row["Remarks"]?
                        .ToString();

                    decimal debit =
                        Convert.ToDecimal(
                            row["DebitAmount"]);

                    decimal credit =
                        Convert.ToDecimal(
                            row["CreditAmount"]);

                    decimal balance =
                        Convert.ToDecimal(
                            row["Balance"]);


                    g.DrawString(
                        tranDate.ToString("dd-MMM-yy"),
                        normalFont,
                        Brushes.Black,
                        left,
                        y);

                    g.DrawString(
                        reference,
                        normalFont,
                        Brushes.Black,
                        left + dateWidth,
                        y);

                    g.DrawString(
                        tranType,
                        normalFont,
                        Brushes.Black,
                        left +
                        dateWidth +
                        referenceWidth,
                        y);

                    g.DrawString(
                        remarks,
                        normalFont,
                        Brushes.Black,
                        left +
                        dateWidth +
                        referenceWidth +
                        typeWidth,
                        y);

                    DrawRightAligned(
                        g,
                        debit.ToString("N2"),
                        normalFont,
                        left +
                        dateWidth +
                        referenceWidth +
                        typeWidth +
                        remarksWidth,
                        debitWidth,
                        y);

                    DrawRightAligned(
                        g,
                        credit.ToString("N2"),
                        normalFont,
                        left +
                        dateWidth +
                        referenceWidth +
                        typeWidth +
                        remarksWidth +
                        debitWidth,
                        creditWidth,
                        y);

                    DrawRightAligned(
                        g,
                        balance.ToString("N2"),
                        normalFont,
                        left +
                        dateWidth +
                        referenceWidth +
                        typeWidth +
                        remarksWidth +
                        debitWidth +
                        creditWidth,
                        balanceWidth,
                        y);

                    y += rowHeight;

                    _printRowIndex++;
                }


                // ---------------------------------------------
                // TOTALS
                // ---------------------------------------------

                y += 10;

                g.DrawLine(
                    pen,
                    left,
                    y,
                    left + pageWidth,
                    y);

                y += 10;

                g.DrawString(
                    "Total Debit:",
                    boldFont,
                    Brushes.Black,
                    new PointF(left, y));

                DrawRightAligned(
                    g,
                    _printTotalDebit.ToString("N2"),
                    boldFont,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth,
                    debitWidth,
                    y);

                y += 20;

                g.DrawString(
                    "Total Credit:",
                    boldFont,
                    Brushes.Black,
                    new PointF(left, y));

                DrawRightAligned(
                    g,
                    _printTotalCredit.ToString("N2"),
                    boldFont,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth +
                    debitWidth,
                    creditWidth,
                    y);

                y += 20;

                g.DrawString(
                    "Closing Balance:",
                    boldFont,
                    Brushes.Black,
                    new PointF(left, y));

                DrawRightAligned(
                    g,
                    _printClosingBalance.ToString("N2"),
                    boldFont,
                    left +
                    dateWidth +
                    referenceWidth +
                    typeWidth +
                    remarksWidth +
                    debitWidth +
                    creditWidth,
                    balanceWidth,
                    y);

                e.HasMorePages = false;
            }
        }

        private void DrawRightAligned(
            Graphics g,
            string text,
            Font font,
            float x,
            float width,
            float y)
        {
            SizeF size =
                g.MeasureString(
                    text,
                    font);

            g.DrawString(
                text,
                font,
                Brushes.Black,
                x + width - size.Width,
                y);
        }

        private void btnExportPDF_Click(
                                        object sender,
                                        EventArgs e)
        {
            try
            {
                DataTable statement =
                    BuildCurrentSupplierStatement();

                if (statement.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No statement data available.",
                        "Supplier Statement",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                using (SaveFileDialog dialog =
                       new SaveFileDialog())
                {
                    dialog.Title =
                        "Save Supplier Statement";

                    dialog.Filter =
                        "PDF Files (*.pdf)|*.pdf";

                    dialog.FileName =
                        "SupplierStatement_" +
                        cboSupplier.Text.Trim() +
                        "_" +
                        dtpFromDate.Value.ToString("yyyy-MM-dd") +
                        "_to_" +
                        dtpToDate.Value.ToString("yyyy-MM-dd") +
                        ".pdf";

                    if (dialog.ShowDialog() !=
                        DialogResult.OK)
                    {
                        return;
                    }

                    CreateSupplierStatementPdf(
                        statement,
                        GetSupplierInfo(Convert.ToInt32(cboSupplier.SelectedValue)).Rows[0],
                        dialog.FileName);

                    MessageBox.Show(
                        "Supplier statement PDF created successfully.",
                        "Supplier Statement",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "PDF Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private DataTable GetSupplierInfo(int supplierID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
            SELECT
                SupplierCode,
                SupplierName,
                ContactPerson,
                Phone,
                Address,
                NTN
            FROM dbo.Suppliers
            WHERE SupplierID = @SupplierID;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@SupplierID",
                        SqlDbType.Int).Value =
                        supplierID;

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void CreateSupplierStatementPdf(DataTable statement, DataRow supplierInfo, string fileName)
        {
            PdfDocument document =
                new PdfDocument();

            document.Info.Title =
                "Supplier Statement";

            document.Info.Author =
                "Inventory System Pro";

            int pageNumber = 1;

            PdfPage page =
                document.AddPage();

            page.Size =
                PdfSharp.PageSize.A4;

            page.Orientation =
                PdfSharp.PageOrientation.Landscape;

            XGraphics gfx =
                XGraphics.FromPdfPage(page);

            XFont titleFont =
                new XFont(
                    "Arial",
                    16,
                    XFontStyleEx.Bold);

            XFont headerFont =
                new XFont(
                    "Arial",
                    8,
                    XFontStyleEx.Bold);

            XFont normalFont =
                new XFont(
                    "Arial",
                    8,
                    XFontStyleEx.Regular);

            XFont boldFont =
                new XFont(
                    "Arial",
                    8,
                    XFontStyleEx.Bold);

            double left = 35;
            double top = 30;
            double pageWidth = page.Width.Point;
            double right = pageWidth - 30;

            double y = top;

            // -------------------------------------------------
            // TITLE
            // -------------------------------------------------

            gfx.DrawString(
                "SUPPLIER STATEMENT",
                titleFont,
                XBrushes.Black,
                new XRect(
                    left,
                    y,
                    right - left,
                    25),
                XStringFormats.Center);

            y += 35;


            // -------------------------------------------------
            // SUPPLIER INFORMATION
            // -------------------------------------------------

            string supplierCode =
                supplierInfo["SupplierCode"]?.ToString() ?? "";

            string supplierName =
                supplierInfo["SupplierName"]?.ToString() ?? "";

            string contactPerson =
                supplierInfo["ContactPerson"]?.ToString() ?? "";

            string phone =
                supplierInfo["Phone"]?.ToString() ?? "";

            string address =
                supplierInfo["Address"]?.ToString() ?? "";

            string ntn =
                supplierInfo["NTN"]?.ToString() ?? "";

            gfx.DrawString(
                "Supplier Code: " + supplierCode,
                boldFont,
                XBrushes.Black,
                left,
                y);

            gfx.DrawString(
                "Supplier Name: " + supplierName,
                boldFont,
                XBrushes.Black,
                left + 220,
                y);

            y += 15;

            gfx.DrawString(
                "Contact Person: " + contactPerson,
                normalFont,
                XBrushes.Black,
                left,
                y);

            gfx.DrawString(
                "Phone: " + phone,
                normalFont,
                XBrushes.Black,
                left + 220,
                y);

            y += 15;

            gfx.DrawString(
                "Address: " + address,
                normalFont,
                XBrushes.Black,
                left,
                y);

            y += 15;

            gfx.DrawString(
                "NTN: " + ntn,
                normalFont,
                XBrushes.Black,
                left,
                y);

            y += 18;

            gfx.DrawString(
                "Period: " +
                dtpFromDate.Value.ToString("dd-MMM-yyyy") +
                " to " +
                dtpToDate.Value.ToString("dd-MMM-yyyy"),
                normalFont,
                XBrushes.Black,
                left,
                y);

            y += 25;

            // -------------------------------------------------
            // OPENING BALANCE
            // -------------------------------------------------

            decimal openingBalance =
                Convert.ToDecimal(
                    statement.Rows[0]["Balance"]);

            gfx.DrawString(
                "Opening Balance:",
                boldFont,
                XBrushes.Black,
                left,
                y);

            gfx.DrawString(
                openingBalance.ToString("N2"),
                boldFont,
                XBrushes.Black,
                right - 80,
                y);

            y += 25;


            // -------------------------------------------------
            // COLUMN WIDTHS
            // -------------------------------------------------

            double dateWidth = 65;
            double referenceWidth = 90;
            double typeWidth = 125;
            double remarksWidth = 250;
            double debitWidth = 80;
            double creditWidth = 80;
            double balanceWidth = 90;


            // -------------------------------------------------
            // HEADER
            // -------------------------------------------------

            gfx.DrawLine(
                XPens.Black,
                left,
                y,
                right,
                y);

            y += 14;

            double x = left;

            gfx.DrawString(
                "Date",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += dateWidth;

            gfx.DrawString(
                "Reference",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += referenceWidth;

            gfx.DrawString(
                "Transaction",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += typeWidth;

            gfx.DrawString(
                "Remarks",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += remarksWidth;
            x += debitWidth - 20;

            gfx.DrawString(
                "Debit",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += debitWidth - 4;

            gfx.DrawString(
                "Credit",
                headerFont,
                XBrushes.Black,
                x,
                y);

            x += creditWidth + 2;

            gfx.DrawString(
                "Balance",
                headerFont,
                XBrushes.Black,
                x,
                y);

            y += 6;

            gfx.DrawLine(
                XPens.Black,
                left,
                y,
                right,
                y);

            y += 14;


            // -------------------------------------------------
            // TRANSACTIONS
            // -------------------------------------------------

            decimal totalDebit = 0m;
            decimal totalCredit = 0m;

            foreach (DataRow row in statement.Rows)
            {
                if (y > page.Height.Point - 85)
                {
                    // Finish current page
                    DrawPdfFooter(
                        gfx,
                        page,
                        normalFont,
                        pageNumber);

                    gfx.Dispose();

                    // Create next page
                    page =
                        document.AddPage();

                    page.Size =
                        PdfSharp.PageSize.A4;

                    page.Orientation =
                        PdfSharp.PageOrientation.Landscape;

                    gfx =
                        XGraphics.FromPdfPage(page);

                    pageNumber++;

                    y = top;
                }

                DateTime tranDate =
                    Convert.ToDateTime(
                        row["TranDate"]);

                string reference =
                    row["ReferenceNo"]?
                    .ToString();

                string tranType =
                    row["TranType"]?
                    .ToString();

                string remarks =
                    row["Remarks"]?
                    .ToString();

                decimal debit =
                    Convert.ToDecimal(
                        row["DebitAmount"]);

                decimal credit =
                    Convert.ToDecimal(
                        row["CreditAmount"]);

                decimal balance =
                    Convert.ToDecimal(
                        row["Balance"]);

                totalDebit += debit;
                totalCredit += credit;


                x = left;

                gfx.DrawString(
                    tranDate.ToString("dd-MMM-yy"),
                    normalFont,
                    XBrushes.Black,
                    x,
                    y);

                x += dateWidth;

                gfx.DrawString(
                    reference,
                    normalFont,
                    XBrushes.Black,
                    x,
                    y);

                x += referenceWidth;

                gfx.DrawString(
                    tranType,
                    normalFont,
                    XBrushes.Black,
                    x,
                    y);

                x += typeWidth;

                gfx.DrawString(
                    remarks,
                    normalFont,
                    XBrushes.Black,
                    x,
                    y);

                x += remarksWidth;

                DrawPdfRight(
                    gfx,
                    debit.ToString("N2"),
                    normalFont,
                    x,
                    debitWidth,
                    y);

                x += debitWidth;

                DrawPdfRight(
                    gfx,
                    credit.ToString("N2"),
                    normalFont,
                    x,
                    creditWidth,
                    y);

                x += creditWidth;

                DrawPdfRight(
                    gfx,
                    balance.ToString("N2"),
                    normalFont,
                    x,
                    balanceWidth,
                    y);

                y += 16;
            }


            // -------------------------------------------------
            // TOTALS
            // -------------------------------------------------

            decimal closingBalance =
                Convert.ToDecimal(
                    statement.Rows[
                        statement.Rows.Count - 1]["Balance"]);

            y += 8;

            gfx.DrawLine(
                XPens.Black,
                left,
                y,
                right,
                y);

            y += 18;

            gfx.DrawString(
                "Total Debit:",
                boldFont,
                XBrushes.Black,
                left,
                y);

            DrawPdfRight(
                gfx,
                totalDebit.ToString("N2"),
                boldFont,
                right -
                debitWidth -
                creditWidth -
                balanceWidth,
                debitWidth,
                y);

            y += 18;

            gfx.DrawString(
                "Total Credit:",
                boldFont,
                XBrushes.Black,
                left,
                y);

            DrawPdfRight(
                gfx,
                totalCredit.ToString("N2"),
                boldFont,
                right -
                creditWidth -
                balanceWidth,
                creditWidth,
                y);

            y += 18;

            gfx.DrawString(
                "Closing Balance:",
                boldFont,
                XBrushes.Black,
                left,
                y);

            DrawPdfRight(
                gfx,
                closingBalance.ToString("N2"),
                boldFont,
                right -
                balanceWidth,
                balanceWidth,
                y);

            DrawPdfFooter(
                gfx,
                page,
                normalFont,
                pageNumber);

            gfx.Dispose();

            document.Save(fileName);
        }

        private void DrawPdfRight(
                XGraphics gfx,
                string text,
                XFont font,
                double x,
                double width,
                double y)
        {
            XSize size =
                gfx.MeasureString(
                    text,
                    font);

            gfx.DrawString(
                text,
                font,
                XBrushes.Black,
                new XRect(
                    x,
                    y - 10,
                    width,
                    15),
                XStringFormats.TopRight);
        }

    private void DrawPdfFooter(
                XGraphics gfx,
                PdfPage page,
                XFont font,
                int pageNumber)
        {
            double left = 35;
            double right = page.Width.Point - 35;
            double y = page.Height.Point - 25;

            // Top separator line
            gfx.DrawLine(
                XPens.LightGray,
                left,
                y - 8,
                right,
                y - 8);

            // Generated date/time
            string generated =
                "Generated: " +
                DateTime.Now.ToString(
                    "dd-MMM-yyyy HH:mm");

            gfx.DrawString(
                generated,
                font,
                XBrushes.Gray,
                new XRect(
                    left,
                    y,
                    200,
                    12),
                XStringFormats.TopLeft);

            // Page number
            string pageText =
                "Page " + pageNumber;

            gfx.DrawString(
                pageText,
                font,
                XBrushes.Gray,
                new XRect(
                    right - 100,
                    y,
                    100,
                    12),
                XStringFormats.TopRight);
        }

        private void ClearSupplierLedger()
        {
            dgvSupplierLedger.DataSource = null;
            dgvSupplierLedger.Rows.Clear();

            dgvPurchaseDetails.DataSource = null;
            dgvPurchaseDetails.Rows.Clear();

            txtOpeningBalance.Text = "0.00";
            txtTotalDebit.Text = "0.00";
            txtTotalCredit.Text = "0.00";
            txtClosingBalance.Text = "0.00";

            txtDetailQty.Text = "0.00";
            txtDetailGross.Text = "0.00";
            txtDetailDiscount.Text = "0.00";
            txtDetailTax.Text = "0.00";
            txtDetailNet.Text = "0.00";

            lblSupplierName.Text = "";
            lblStatementPeriod.Text = "";

            if (cboSupplier.Items.Count > 0)
                cboSupplier.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSupplierLedger();
        }
    }
}
