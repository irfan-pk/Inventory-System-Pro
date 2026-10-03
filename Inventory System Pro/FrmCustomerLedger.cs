using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Inventory_System_Pro
{
    public partial class FrmCustomerLedger : Form 
    {
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmCustomerLedger()
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
                            CustomerName
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
                    "CustomerName";

                cboCustomer.ValueMember =
                    "CustomerID";

                cboCustomer.DataSource =
                    dt;

                if (dt.Rows.Count > 0)
                {
                    cboCustomer.SelectedIndex = 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer Loading",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Customer Loading",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerLedger()
        {
            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "Customer Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    cboCustomer.SelectedValue.ToString(),
                    out int customerID))
            {
                MessageBox.Show(
                    "Invalid customer.",
                    "Customer Ledger",
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
                    "Customer Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DataTable dt =
                    new DataTable();

                decimal openingBalance = 0m;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    cn.Open();

                    // -------------------------------------------------
                    // 1. OPENING BALANCE
                    // -------------------------------------------------

                    string openingSql = @"
                SELECT
                    ISNULL(
                        SUM(DebitAmount - CreditAmount),
                        0
                    )
                FROM dbo.CustomerLedger

                WHERE CustomerID = @CustomerID

                  AND TranDate < @FromDate;";

                    using (SqlCommand cmd =
                           new SqlCommand(
                               openingSql,
                               cn))
                    {
                        cmd.Parameters.Add(
                            "@CustomerID",
                            SqlDbType.Int).Value =
                            customerID;

                        cmd.Parameters.Add(
                            "@FromDate",
                            SqlDbType.DateTime2).Value =
                            fromDate;

                        openingBalance =
                            Convert.ToDecimal(
                                cmd.ExecuteScalar());
                    }


                    // -------------------------------------------------
                    // 2. PERIOD TRANSACTIONS
                    // -------------------------------------------------

                    string sql = @"
                SELECT
                    CL.CustomerLedgerID,
                    CL.TranDate,
                    CL.ReferenceNo,
                    CL.TranType,
                    CL.Remarks,
                    CL.DebitAmount,
                    CL.CreditAmount

                FROM dbo.CustomerLedger CL

                WHERE CL.CustomerID = @CustomerID

                  AND CL.TranDate >= @FromDate

                  AND CL.TranDate <
                        DATEADD(
                            DAY,
                            1,
                            @ToDate
                        )

                ORDER BY
                    CL.TranDate,
                    CL.CustomerLedgerID;";

                    using (SqlCommand cmd =
                           new SqlCommand(
                               sql,
                               cn))
                    {
                        cmd.Parameters.Add(
                            "@CustomerID",
                            SqlDbType.Int).Value =
                            customerID;

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
                // 3. DISPLAY TABLE
                // -------------------------------------------------

                DataTable displayTable =
                    new DataTable();

                displayTable.Columns.Add(
                    "CustomerLedgerID",
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


                // -------------------------------------------------
                // 4. OPENING ROW
                // -------------------------------------------------

                DataRow openingRow =
                    displayTable.NewRow();

                openingRow["CustomerLedgerID"] =
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
                    0m;

                openingRow["CreditAmount"] =
                    0m;

                openingRow["Balance"] =
                    openingBalance;

                displayTable.Rows.Add(
                    openingRow);


                // -------------------------------------------------
                // 5. RUNNING BALANCE
                // -------------------------------------------------

                decimal runningBalance =
                    openingBalance;

                decimal totalDebit = 0m;
                decimal totalCredit = 0m;

                foreach (DataRow sourceRow
                         in dt.Rows)
                {
                    decimal debit =
                        sourceRow["DebitAmount"] ==
                        DBNull.Value
                            ? 0m
                            : Convert.ToDecimal(
                                sourceRow["DebitAmount"]);

                    decimal credit =
                        sourceRow["CreditAmount"] ==
                        DBNull.Value
                            ? 0m
                            : Convert.ToDecimal(
                                sourceRow["CreditAmount"]);


                    runningBalance +=
                        debit - credit;

                    totalDebit +=
                        debit;

                    totalCredit +=
                        credit;


                    DataRow row =
                        displayTable.NewRow();

                    row["CustomerLedgerID"] =
                        sourceRow["CustomerLedgerID"];

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

                    displayTable.Rows.Add(
                        row);
                }


                // -------------------------------------------------
                // 6. DISPLAY
                // -------------------------------------------------

                dgvCustomerLedger.DataSource =
                    displayTable;

                FormatCustomerLedgerGrid();


                // -------------------------------------------------
                // 7. TOTALS
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
                    "Customer Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatCustomerLedgerGrid()
        {
            dgvCustomerLedger.AutoGenerateColumns = true;
            dgvCustomerLedger.AllowUserToAddRows = false;
            dgvCustomerLedger.ReadOnly = true;
            dgvCustomerLedger.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            if (dgvCustomerLedger.Columns.Contains(
                    "CustomerLedgerID"))
            {
                dgvCustomerLedger.Columns[
                    "CustomerLedgerID"].Visible = false;
            }

            if (dgvCustomerLedger.Columns.Contains(
                    "TranDate"))
            {
                dgvCustomerLedger.Columns[
                    "TranDate"].HeaderText = "Date";

                dgvCustomerLedger.Columns[
                    "TranDate"].DefaultCellStyle.Format =
                    "dd-MMM-yyyy";
            }

            if (dgvCustomerLedger.Columns.Contains(
                    "ReferenceNo"))
            {
                dgvCustomerLedger.Columns[
                    "ReferenceNo"].HeaderText =
                    "Reference";
            }

            if (dgvCustomerLedger.Columns.Contains(
                    "TranType"))
            {
                dgvCustomerLedger.Columns[
                    "TranType"].HeaderText =
                    "Transaction";
            }

            if (dgvCustomerLedger.Columns.Contains(
                    "Remarks"))
            {
                dgvCustomerLedger.Columns[
                    "Remarks"].HeaderText =
                    "Remarks";
            }

            FormatCustomerLedgerAmountColumn(
                "DebitAmount",
                "Debit");

            FormatCustomerLedgerAmountColumn(
                "CreditAmount",
                "Credit");

            FormatCustomerLedgerAmountColumn(
                "Balance",
                "Balance");
        }

        private void FormatCustomerLedgerAmountColumn(
            string columnName,
            string headerText)
        {
            if (!dgvCustomerLedger.Columns.Contains(
                    columnName))
                return;

            DataGridViewColumn column =
                dgvCustomerLedger.Columns[
                    columnName];

            column.HeaderText =
                headerText;

            column.DefaultCellStyle.Format =
                "N2";

            column.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            column.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void FormatSaleAmountColumn(
            string columnName,
            string headerText)
        {
            if (!dgvCustomerPaymentDetails.Columns.Contains(columnName))
                return;

            DataGridViewColumn column =
                dgvCustomerPaymentDetails.Columns[columnName];

            column.HeaderText =
                headerText;

            column.DefaultCellStyle.Format =
                "N2";

            column.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            column.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadSaleDetails(string referenceNo)
        {
            if (string.IsNullOrWhiteSpace(referenceNo))
                return;

            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    S.SaleID,
                    S.SaleNumber,
                    S.SaleDate,

                    D.SaleDetailID,
                    D.ProductID,

                    P.ProductCode,
                    P.ProductName,

                    D.Qty,
                    D.UnitPrice,
                    D.DiscountAmount,
                    D.TaxAmount,
                    D.NetAmount

                FROM dbo.Sales S

                INNER JOIN dbo.SaleDetails D
                    ON D.SaleID = S.SaleID

                INNER JOIN dbo.Products P
                    ON P.ProductID = D.ProductID

                WHERE S.SaleNumber = @SaleNumber

                ORDER BY
                    D.SaleDetailID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SaleNumber",
                            SqlDbType.NVarChar, 50).Value =
                            referenceNo;

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                // Display in reusable details grid
                dgvCustomerPaymentDetails.DataSource = dt;

                FormatCustomerSaleDetailsGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Sale Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatCustomerSaleDetailsGrid()
        {
            dgvCustomerPaymentDetails.ReadOnly = true;
            dgvCustomerPaymentDetails.AllowUserToAddRows = false;
            dgvCustomerPaymentDetails.AutoGenerateColumns = true;
            dgvCustomerPaymentDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            HideCustomerDetailColumn("SaleID");
            HideCustomerDetailColumn("SaleDetailID");
            HideCustomerDetailColumn("ProductID");

            SetCustomerDetailHeader(
                "SaleNumber",
                "Sale Number");

            SetCustomerDetailHeader(
                "SaleDate",
                "Date");

            if (dgvCustomerPaymentDetails.Columns.Contains(
                    "SaleDate"))
            {
                dgvCustomerPaymentDetails.Columns[
                    "SaleDate"]
                    .DefaultCellStyle.Format =
                    "dd-MMM-yyyy";
            }

            SetCustomerDetailHeader(
                "ProductCode",
                "Code");

            SetCustomerDetailHeader(
                "ProductName",
                "Product");

            FormatCustomerDetailAmount(
                "Qty",
                "Qty",
                "N2");

            FormatCustomerDetailAmount(
                "UnitPrice",
                "Unit Price",
                "N2");

            FormatCustomerDetailAmount(
                "DiscountAmount",
                "Discount",
                "N2");

            FormatCustomerDetailAmount(
                "TaxAmount",
                "Tax",
                "N2");

            FormatCustomerDetailAmount(
                "NetAmount",
                "Net",
                "N2");
        }

        private void HideCustomerDetailColumn(
            string columnName)
        {
            if (dgvCustomerPaymentDetails.Columns.Contains(
                    columnName))
            {
                dgvCustomerPaymentDetails.Columns[
                    columnName].Visible = false;
            }
        }

        private void SetCustomerDetailHeader(
            string columnName,
            string headerText)
        {
            if (dgvCustomerPaymentDetails.Columns.Contains(
                    columnName))
            {
                dgvCustomerPaymentDetails.Columns[
                    columnName].HeaderText =
                    headerText;
            }
        }

        private void FormatCustomerDetailAmount(
            string columnName,
            string headerText,
            string format)
        {
            if (!dgvCustomerPaymentDetails.Columns.Contains(
                    columnName))
                return;

            DataGridViewColumn column =
                dgvCustomerPaymentDetails.Columns[columnName];

            column.HeaderText =
                headerText;

            column.DefaultCellStyle.Format =
                format;

            column.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            column.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
        }

        private void LoadPaymentDetails(string referenceNo)
        {
            if (string.IsNullOrWhiteSpace(referenceNo))
                return;

            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            CL.CustomerLedgerID,
                            CL.TranDate AS PaymentDate,
                            CL.ReferenceNo AS PaymentNumber,
                            C.CustomerCode,
                            C.CustomerName,
                            CA.AccountCode,
                            CA.AccountName,
                            CL.CreditAmount AS Amount,
                            CL.Remarks,
                            CL.CreatedBy,
                            CL.CreatedDate

                        FROM dbo.CustomerLedger AS CL

                        INNER JOIN dbo.Customers AS C
                            ON C.CustomerID = CL.CustomerID

                        LEFT JOIN dbo.CashTransactions AS CT
                            ON CT.ReferenceNo = CL.ReferenceNo
                           AND CT.TranType = 'CUSTOMER_PAYMENT'
                           AND CT.CashAccountID IS NOT NULL

                        LEFT JOIN dbo.CashAccounts AS CA
                            ON CA.CashAccountID = CT.CashAccountID

                        WHERE CL.TranType = 'CUSTOMER_PAYMENT'
                          AND CL.ReferenceNo = @PaymentNumber

                        ORDER BY
                            CL.TranDate,
                            CL.CustomerLedgerID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@PaymentNumber",
                            SqlDbType.NVarChar,
                            30).Value = referenceNo;

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                dgvCustomerPaymentDetails.DataSource = dt;

                FormatCustomerPaymentDetailsGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Payment Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Payment Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatCustomerPaymentDetailsGrid()
        {
            HideCustomerDetailColumn("PaymentID");

            SetCustomerDetailHeader(
                "PaymentNumber",
                "Payment No");

            SetCustomerDetailHeader(
                "PaymentDate",
                "Date");

            if (dgvCustomerPaymentDetails.Columns.Contains(
                    "PaymentDate"))
            {
                dgvCustomerPaymentDetails.Columns[
                    "PaymentDate"]
                    .DefaultCellStyle.Format =
                    "dd-MMM-yyyy";
            }

            FormatCustomerDetailAmount(
                "Amount",
                "Amount",
                "N2");

            SetCustomerDetailHeader(
                "PaymentMethod",
                "Payment Method");

            SetCustomerDetailHeader(
                "Remarks",
                "Remarks");
        }

        private void ClearCustomerDetailsGrid()
        {
            dgvCustomerPaymentDetails.DataSource = null;
            dgvCustomerPaymentDetails.Rows.Clear();
            //dgvCustomerDetails.Columns.Clear();

            //dgvCustomerDetails.AutoGenerateColumns = true;
            dgvCustomerPaymentDetails.ReadOnly = true;
            dgvCustomerPaymentDetails.AllowUserToAddRows = false;
            dgvCustomerPaymentDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        private DataTable BuildCurrentCustomerStatement()
        {
            if (dgvCustomerLedger.DataSource is DataTable dt)
            {
                return dt.Copy();
            }

            return null;
        }

        private bool ValidateCustomerStatement()
        {
            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (dgvCustomerLedger.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Please load the customer ledger first.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void PrintCustomerStatement()
        {
            if (!ValidateCustomerStatement())
                return;

            DataTable statement =
                BuildCurrentCustomerStatement();

            if (statement == null ||
                statement.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no customer ledger data to print.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    cboCustomer.SelectedValue?.ToString(),
                    out int customerID))
            {
                MessageBox.Show(
                    "Invalid customer.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataTable customerInfo =
                GetCustomerInfo(customerID);

            if (customerInfo.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Customer information could not be found.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (SaveFileDialog dialog =
                   new SaveFileDialog())
            {
                dialog.Filter =
                    "PDF Files (*.pdf)|*.pdf";

                dialog.FileName =
                    "CustomerStatement_" +
                    customerID +
                    "_" +
                    DateTime.Now.ToString(
                        "yyyyMMdd_HHmmss") +
                    ".pdf";

                if (dialog.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                try
                {
                    CreateCustomerStatementPdf(
                        statement,
                        customerInfo.Rows[0],
                        dialog.FileName);

                    DialogResult result =
                        MessageBox.Show(
                            "Customer statement PDF generated successfully.\n\n" +
                            "Do you want to print it now?",
                            "Customer Statement",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                        return;

                    using (PrintDialog printDialog =
                           new PrintDialog())
                    {
                        printDialog.UseEXDialog = true;

                        using (System.Drawing.Printing.PrintDocument printDocument =
                               new System.Drawing.Printing.PrintDocument())
                        {
                            printDocument.DocumentName =
                                "Customer Statement";

                            printDialog.Document =
                                printDocument;

                            if (printDialog.ShowDialog() !=
                                DialogResult.OK)
                            {
                                return;
                            }

                            try
                            {
                                System.Diagnostics.Process.Start(
                                    new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = dialog.FileName,
                                        Verb = "print",
                                        UseShellExecute = true
                                    });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                    "PDF was generated successfully, but it could not be sent to the printer.\n\n" +
                                    ex.Message,
                                    "Print Customer Statement",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Customer Statement",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void CreateCustomerStatementPdf(
            DataTable statement,
            DataRow customer,
            string fileName)
        {
            PdfDocument document =
                new PdfDocument();

            document.Info.Title =
                "Customer Statement";

            document.Info.Author =
                "Inventory System Pro";

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
                    18,
                    XFontStyleEx.Bold);

            XFont boldFont =
                new XFont(
                    "Arial",
                    9,
                    XFontStyleEx.Bold);

            XFont normalFont =
                new XFont(
                    "Arial",
                    8,
                    XFontStyleEx.Regular);

            XFont smallFont =
                new XFont(
                    "Arial",
                    7,
                    XFontStyleEx.Regular);

            double left = 35;
            double right = page.Width.Point - 35;
            double y = 35;

            int pageNumber = 1;

            // =====================================================
            // HEADER
            // =====================================================

            gfx.DrawString(
                "CUSTOMER STATEMENT",
                titleFont,
                XBrushes.Black,
                new XRect(
                    left,
                    y,
                    right - left,
                    25),
                XStringFormats.TopCenter);

            y += 32;

            string customerCode =
                customer["CustomerCode"]?.ToString() ?? "";

            string customerName =
                customer["CustomerName"]?.ToString() ?? "";

            string contactPerson =
                customer["ContactPerson"]?.ToString() ?? "";

            string phone =
                customer["Phone"]?.ToString() ?? "";

            string address =
                customer["Address"]?.ToString() ?? "";

            string ntn =
                customer["NTN"]?.ToString() ?? "";

            gfx.DrawString(
                "Customer Code: " + customerCode,
                boldFont,
                XBrushes.Black,
                left,
                y);

            gfx.DrawString(
                "Customer Name: " + customerName,
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
                left + 300,
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
                dtpFromDate.Value.ToString(
                    "dd-MMM-yyyy") +
                " to " +
                dtpToDate.Value.ToString(
                    "dd-MMM-yyyy"),
                normalFont,
                XBrushes.Black,
                left,
                y);

            y += 25;

            // =====================================================
            // TABLE HEADER
            // =====================================================

            double[] widths =
            {
        70,
        80,
        75,
        150,
        85,
        85,
        95
    };

            string[] headers =
            {
        "Date",
        "Reference",
        "Transaction",
        "Remarks",
        "Debit",
        "Credit",
        "Balance"
    };

            double tableLeft = left;

            DrawCustomerStatementTableHeader(
                gfx,
                headers,
                widths,
                tableLeft,
                ref y,
                boldFont);

            // =====================================================
            // DATA
            // =====================================================

            foreach (DataRow row in statement.Rows)
            {
                if (y > page.Height.Point - 85)
                {
                    DrawCustomerStatementFooter(
                        gfx,
                        page,
                        smallFont,
                        pageNumber);

                    gfx.Dispose();

                    page =
                        document.AddPage();

                    page.Size =
                        PdfSharp.PageSize.A4;

                    page.Orientation =
                        PdfSharp.PageOrientation.Landscape;

                    gfx =
                        XGraphics.FromPdfPage(page);

                    pageNumber++;

                    y = 35;

                    DrawCustomerStatementTableHeader(
                        gfx,
                        headers,
                        widths,
                        tableLeft,
                        ref y,
                        boldFont);
                }

                string date =
                    row["TranDate"] == DBNull.Value
                        ? ""
                        : Convert.ToDateTime(
                            row["TranDate"])
                            .ToString(
                                "dd-MMM-yyyy");

                string reference =
                    row["ReferenceNo"]?.ToString() ?? "";

                string tranType =
                    row["TranType"]?.ToString() ?? "";

                string remarks =
                    row["Remarks"]?.ToString() ?? "";

                string debit =
                    Convert.ToDecimal(
                        row["DebitAmount"])
                        .ToString("N2");

                string credit =
                    Convert.ToDecimal(
                        row["CreditAmount"])
                        .ToString("N2");

                string balance =
                    Convert.ToDecimal(
                        row["Balance"])
                        .ToString("N2");

                string[] values =
                {
            date,
            reference,
            tranType,
            remarks,
            debit,
            credit,
            balance
        };

                double x = tableLeft;

                for (int i = 0;
                     i < values.Length;
                     i++)
                {
                    XStringFormat format =
                        i >= 4
                            ? XStringFormats.TopRight
                            : XStringFormats.TopLeft;

                    gfx.DrawString(
                        values[i],
                        normalFont,
                        XBrushes.Black,
                        new XRect(
                            x + 3,
                            y,
                            widths[i] - 6,
                            15),
                        format);

                    x += widths[i];
                }

                gfx.DrawLine(
                    XPens.LightGray,
                    tableLeft,
                    y + 15,
                    tableLeft +
                        widths.Sum(),
                    y + 15);

                y += 18;
            }

            // =====================================================
            // TOTALS
            // =====================================================

            y += 10;

            decimal openingBalance = 0m;
            decimal totalDebit = 0m;
            decimal totalCredit = 0m;
            decimal closingBalance = 0m;

            if (statement.Rows.Count > 0)
            {
                DataRow openingRow =
                    statement.Rows[0];

                if (openingRow["Balance"] !=
                    DBNull.Value)
                {
                    openingBalance =
                        Convert.ToDecimal(
                            openingRow["Balance"]);
                }

                foreach (DataRow row in statement.Rows)
                {
                    totalDebit +=
                        Convert.ToDecimal(
                            row["DebitAmount"]);

                    totalCredit +=
                        Convert.ToDecimal(
                            row["CreditAmount"]);

                    closingBalance =
                        Convert.ToDecimal(
                            row["Balance"]);
                }
            }

            gfx.DrawString(
                "Opening Balance: " +
                openingBalance.ToString("N2"),
                boldFont,
                XBrushes.Black,
                left,
                y);

            y += 16;

            gfx.DrawString(
                "Total Debit: " +
                totalDebit.ToString("N2"),
                boldFont,
                XBrushes.Black,
                left,
                y);

            gfx.DrawString(
                "Total Credit: " +
                totalCredit.ToString("N2"),
                boldFont,
                XBrushes.Black,
                left + 180,
                y);

            y += 16;

            gfx.DrawString(
                "Closing Balance: " +
                closingBalance.ToString("N2"),
                boldFont,
                XBrushes.Black,
                left,
                y);

            // =====================================================
            // FOOTER
            // =====================================================

            DrawCustomerStatementFooter(
                gfx,
                page,
                smallFont,
                pageNumber);

            gfx.Dispose();

            document.Save(fileName);
        }

        private void DrawCustomerStatementTableHeader(
            XGraphics gfx,
            string[] headers,
            double[] widths,
            double left,
            ref double y,
            XFont font)
        {
            double x = left;

            for (int i = 0;
                 i < headers.Length;
                 i++)
            {
                gfx.DrawRectangle(
                    XBrushes.LightGray,
                    x,
                    y,
                    widths[i],
                    18);

                XStringFormat format =
                    i >= 4
                        ? XStringFormats.CenterRight
                        : XStringFormats.CenterLeft;

                gfx.DrawString(
                    headers[i],
                    font,
                    XBrushes.Black,
                    new XRect(
                        x + 3,
                        y + 3,
                        widths[i] - 6,
                        12),
                    format);

                x += widths[i];
            }

            y += 21;
        }

        private void DrawCustomerStatementFooter(
    XGraphics gfx,
    PdfPage page,
    XFont font,
    int pageNumber)
        {
            double left = 35;
            double right = page.Width.Point - 35;
            double y = page.Height.Point - 25;

            gfx.DrawLine(
                XPens.LightGray,
                left,
                y - 8,
                right,
                y - 8);

            gfx.DrawString(
                "Generated: " +
                DateTime.Now.ToString(
                    "dd-MMM-yyyy HH:mm"),
                font,
                XBrushes.Gray,
                new XRect(
                    left,
                    y,
                    200,
                    12),
                XStringFormats.TopLeft);

            gfx.DrawString(
                "Page " + pageNumber,
                font,
                XBrushes.Gray,
                new XRect(
                    right - 100,
                    y,
                    100,
                    12),
                XStringFormats.TopRight);
        }

        private void FrmCustomerLedger_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
                LoadCustomerLedger();
        }

        private void dgvCustomerLedger_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvCustomerLedger.Rows[e.RowIndex];

            string tranType =
                row.Cells["TranType"].Value?
                .ToString();

            string referenceNo =
                row.Cells["ReferenceNo"].Value?
                .ToString();

            if (string.IsNullOrWhiteSpace(tranType) ||
                string.IsNullOrWhiteSpace(referenceNo))
            {
                return;
            }

            tranType = tranType.Trim().ToUpper();
            referenceNo = referenceNo.Trim();

            if (tranType == "SALE")
            {
                LoadSaleDetails(referenceNo);
            }
            else if (tranType == "CUSTOMER_PAYMENT")
            {
                LoadPaymentDetails(referenceNo);
            }
        }

        private void btnPrintStatement_Click(object sender, EventArgs e)
        {
            PrintCustomerStatement();
        }

        private void btnExportPDF_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateCustomerStatement())
                return;

            DataTable statement =
                BuildCurrentCustomerStatement();

            if (statement == null ||
                statement.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no customer ledger data to export.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    cboCustomer.SelectedValue?.ToString(),
                    out int customerID))
            {
                MessageBox.Show(
                    "Invalid customer.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataTable customerInfo =
                GetCustomerInfo(customerID);

            if (customerInfo.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Customer information could not be found.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (SaveFileDialog dialog =
                   new SaveFileDialog())
            {
                dialog.Filter =
                    "PDF Files (*.pdf)|*.pdf";

                dialog.FileName =
                    "CustomerStatement_" +
                    customerID +
                    "_" +
                    DateTime.Now.ToString(
                        "yyyyMMdd_HHmmss") +
                    ".pdf";

                if (dialog.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                CreateCustomerStatementPdf(
                    statement,
                    customerInfo.Rows[0],
                    dialog.FileName);

                MessageBox.Show(
                    "Customer statement PDF generated successfully.",
                    "Customer Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private DataTable GetCustomerInfo(int customerID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            {
                string sql = @"
                    SELECT
                        CustomerCode,
                        CustomerName,
                        ContactPerson,
                        Phone,
                        Address,
                        NTN
                    FROM dbo.Customers
                    WHERE CustomerID = @CustomerID;";

                using (SqlCommand cmd =
                       new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@CustomerID",
                        SqlDbType.Int).Value = customerID;

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void ClearLedger()
        {
            //CurrentCashAccountID = 0;
            //CurrentOpeningBalance = 0m;
            //CurrentTotalDebit = 0m;
            //CurrentTotalCredit = 0m;
            //CurrentClosingBalance = 0m;

            cboCustomer.SelectedIndex = -1;
            lblCustomerName.Text = "";
            lblStatementPeriod.Text = "";

            dgvCustomerLedger.DataSource = null;
            dgvCustomerPaymentDetails.DataSource = null;

            txtOpeningBalance.Text = "0.00";
            txtTotalDebit.Text = "0.00";
            txtTotalCredit.Text = "0.00";
            txtClosingBalance.Text = "0.00";

            lblStatementPeriod.Text = "";
            lblCustomerName.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearLedger();
        }
    }
}
