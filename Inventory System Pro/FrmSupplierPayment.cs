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
    public partial class FrmSupplierPayment : Form
    {
        private int CurrentCashAccountID = 0;
        private int CurrentSupplierID = 0;
        private decimal CurrentSupplierBalance = 0m;
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmSupplierPayment()
        {
            InitializeComponent();
        }

        private void FrmSupplierPayment_Load(object sender, EventArgs e)
        {
            dtpPaymentDate.Value = DateTime.Today;

            SetupPaymentHistoryGrid();

            LoadSuppliers();

            LoadCashAccounts();

            NewPayment();
        }

        private void LoadCashAccounts()
        {
            cmbCashAccount.Items.Clear();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT
            CashAccountID,
            AccountCode,
            AccountName
        FROM dbo.CashAccounts
        WHERE AccountType = 'CASH'
          AND IsActive = 1
        ORDER BY AccountCode;", cn))
            {
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int accountID = Convert.ToInt32(dr["CashAccountID"]);

                        string accountCode =
                            dr["AccountCode"] == DBNull.Value
                                ? ""
                                : dr["AccountCode"].ToString();

                        string accountName =
                            dr["AccountName"] == DBNull.Value
                                ? ""
                                : dr["AccountName"].ToString();

                        cmbCashAccount.Items.Add(
                            new CashAccountItem
                            {
                                CashAccountID = accountID,
                                DisplayText = accountCode + " - " + accountName
                            });
                    }
                }
            }

            if (cmbCashAccount.Items.Count > 0)
                cmbCashAccount.SelectedIndex = 0;
        }

        private bool GetSelectedCashAccount()
        {
            CurrentCashAccountID = 0;

            if (cmbCashAccount.SelectedItem == null)
                return false;

            CashAccountItem item =
                cmbCashAccount.SelectedItem as CashAccountItem;

            if (item == null)
                return false;

            CurrentCashAccountID = item.CashAccountID;

            return CurrentCashAccountID > 0;
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
                    SupplierName
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

                cmbSupplier.DataSource = null;

                cmbSupplier.DisplayMember =
                    "SupplierDisplay";

                cmbSupplier.ValueMember =
                    "SupplierID";

                DataColumn displayColumn =
                    new DataColumn(
                        "SupplierDisplay",
                        typeof(string));

                dt.Columns.Add(displayColumn);

                foreach (DataRow row in dt.Rows)
                {
                    row["SupplierDisplay"] =
                        row["SupplierCode"].ToString()
                        + " - "
                        + row["SupplierName"].ToString();
                }

                cmbSupplier.DataSource = dt;

                cmbSupplier.SelectedIndex = -1;
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

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedIndex < 0 ||
                    cmbSupplier.SelectedValue == null)
            {
                CurrentSupplierID = 0;
                CurrentSupplierBalance = 0;

                txtCurrentBalance.Text = "0.00";
                txtBalanceAfter.Text = "0.00";
                txtStatus.Text = "";

                dgvPaymentHistory.Rows.Clear();

                return;
            }

            if (!int.TryParse(
                cmbSupplier.SelectedValue.ToString(),
                out int supplierID))
            {
                return;
            }

            CurrentSupplierID = supplierID;

            LoadSupplierBalance();

            LoadPaymentHistory();

            CalculateBalanceAfter();

        }

        private void LoadSupplierBalance()
        {
            if (CurrentSupplierID <= 0)
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                        S.OpeningBalance
                        +
                        ISNULL(
                            (
                                SELECT SUM(
                                    SL.DebitAmount
                                    - SL.CreditAmount
                                )
                                FROM dbo.SupplierLedger SL
                                WHERE SL.SupplierID =
                                      S.SupplierID
                            ),
                            0
                        ) AS CurrentBalance
                        FROM dbo.Suppliers S
                        WHERE S.SupplierID = @SupplierID;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            CurrentSupplierID;

                        cn.Open();

                        object value =
                            cmd.ExecuteScalar();

                        CurrentSupplierBalance =
                            value == null ||
                            value == DBNull.Value
                                ? 0m
                                : Convert.ToDecimal(value);
                    }
                }

                txtCurrentBalance.Text =
                    CurrentSupplierBalance.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Supplier Balance",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateBalanceAfter();
        }

        private void CalculateBalanceAfter()
        {
            decimal payment = 0m;

            decimal.TryParse(
                txtPaymentAmount.Text.Trim(),
                out payment);

            if (payment < 0)
                payment = 0;

            decimal balanceAfter =
                CurrentSupplierBalance - payment;

            txtBalanceAfter.Text =
                balanceAfter.ToString("N2");

            if (balanceAfter > 0)
            {
                txtStatus.Text = "BALANCE";
            }
            else if (balanceAfter == 0)
            {
                txtStatus.Text = "SETTLED";
            }
            else
            {
                txtStatus.Text = "ADVANCE";
            }
        }

        private void NewPayment()
        {
            CurrentSupplierID = 0;
            CurrentSupplierBalance = 0m;
            CurrentCashAccountID = 0;

            txtPaymentNumber.Clear();

            dtpPaymentDate.Value =
                DateTime.Today;

            if (cmbSupplier.Items.Count > 0)
                cmbSupplier.SelectedIndex = -1;

            if (cmbCashAccount.Items.Count > 0)
                cmbCashAccount.SelectedIndex = 0;

            txtCurrentBalance.Text =
                "0.00";

            txtPaymentAmount.Text =
                "0.00";

            txtBalanceAfter.Text =
                "0.00";

            txtStatus.Clear();

            txtRemarks.Clear();

            dgvPaymentHistory.Rows.Clear();

            btnSave.Enabled = true;

            cmbSupplier.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SUPPLIER_PAYMENTS.NEW"))
            {
                MessageBox.Show("You do not have permission to create new supplier payments.");
                return;
            }
            NewPayment();
        }

        private void SavePayment()
        {
            string remarks = txtRemarks.Text.Trim();

            if (!GetSelectedCashAccount())
            {
                MessageBox.Show(
                    "Please select a valid cash account.",
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(remarks))
            {
                MessageBox.Show(
                    "Remarks are required.",
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRemarks.Focus();
                return;
            }

            if (CurrentSupplierID <= 0)
            {
                MessageBox.Show(
                    "Please select a supplier.",
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbSupplier.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtPaymentAmount.Text.Trim(),
                out decimal amount) ||
                amount <= 0)
            {
                MessageBox.Show(
                    "Payment amount must be greater than zero.",
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPaymentAmount.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_SupplierPayment_Save",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add("@PaymentDate", SqlDbType.Date).Value = dtpPaymentDate.Value.Date;

                        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = CurrentSupplierID;

                        cmd.Parameters.Add("@CashAccountID", SqlDbType.Int).Value = CurrentCashAccountID;

                        SqlParameter amountParameter =  cmd.Parameters.Add("@Amount", SqlDbType.Decimal);

                        amountParameter.Precision = 18;
                        amountParameter.Scale = 2;
                        amountParameter.Value = amount;

                        cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 1000).Value = string.IsNullOrWhiteSpace(txtRemarks.Text) ? (object)DBNull.Value : txtRemarks.Text.Trim();

                        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = 1;
                            
                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Payment was not saved.",
                                    "Supplier Payment",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            string result =
                                reader["Result"].ToString();

                            if (result != "SUCCESS")
                            {
                                MessageBox.Show(
                                    result,
                                    "Supplier Payment",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }

                            txtPaymentNumber.Text =
                                reader["PaymentNumber"]
                                .ToString();

                            decimal supplierBalance =
                                Convert.ToDecimal(
                                    reader["SupplierBalance"]);

                            CurrentSupplierBalance =
                                supplierBalance;

                            txtCurrentBalance.Text =
                                supplierBalance.ToString("N2");

                            txtBalanceAfter.Text =
                                supplierBalance.ToString("N2");

                            if (supplierBalance > 0)
                                txtStatus.Text = "BALANCE";
                            else if (supplierBalance == 0)
                                txtStatus.Text = "SETTLED";
                            else
                                txtStatus.Text = "ADVANCE";
                        }
                    }
                }

                MessageBox.Show(
                    "Supplier payment saved successfully.\n\n" +
                    "Payment No: " +
                    txtPaymentNumber.Text +
                    "\nAmount: " +
                    amount.ToString("N2"),
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadPaymentHistory();
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
                    "Supplier Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("SUPPLIER_PAYMENTS.SAVE"))
            {
                MessageBox.Show("You do not have permission to save supplier payments.");
                return;
            }
            SavePayment();
        }

        private void SetupPaymentHistoryGrid()
        {
            dgvPaymentHistory.Columns.Clear();

            dgvPaymentHistory.AllowUserToAddRows = false;
            dgvPaymentHistory.AllowUserToDeleteRows = false;
            dgvPaymentHistory.AllowUserToResizeRows = false;

            dgvPaymentHistory.ReadOnly = true;
            dgvPaymentHistory.AutoGenerateColumns = false;
            dgvPaymentHistory.RowHeadersVisible = false;

            dgvPaymentHistory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPaymentHistory.MultiSelect = false;

            AddHistoryColumn(
                "TranDate",
                "Date",
                150);

            AddHistoryColumn(
                "ReferenceNo",
                "Payment No.",
                200);

            AddHistoryColumn(
                "CreditAmount",
                "Amount",
                110);

            AddHistoryColumn(
                "Remarks",
                "Remarks",
                537);

            dgvPaymentHistory.Columns["CreditAmount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPaymentHistory.Columns["CreditAmount"]
                .DefaultCellStyle.Format = "N2";
        }

        private void AddHistoryColumn(
            string name,
            string header,
            int width)
        {
            DataGridViewTextBoxColumn column =
                new DataGridViewTextBoxColumn();

            column.Name = name;
            column.HeaderText = header;
            column.Width = width;
            
            column.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            dgvPaymentHistory.Columns.Add(column);
        }

        private void LoadPaymentHistory()
        {
            dgvPaymentHistory.Rows.Clear();

            if (CurrentSupplierID <= 0)
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            SupplierLedgerID,
                            TranDate,
                            ReferenceNo,
                            CreditAmount,
                            Remarks
                        FROM dbo.SupplierLedger
                        WHERE SupplierID = @SupplierID
                          AND TranType = 'SUPPLIER_PAYMENT'
                        ORDER BY
                            TranDate DESC,
                            SupplierLedgerID DESC;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@SupplierID",
                            SqlDbType.Int).Value =
                            CurrentSupplierID;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex =
                                    dgvPaymentHistory.Rows.Add();

                                DataGridViewRow row =
                                    dgvPaymentHistory.Rows[rowIndex];

                                row.Cells["TranDate"].Value =
                                    Convert.ToDateTime(
                                        reader["TranDate"])
                                        .ToString("dd-MMM-yyyy");

                                row.Cells["ReferenceNo"].Value =
                                    reader["ReferenceNo"] == DBNull.Value
                                        ? ""
                                        : reader["ReferenceNo"].ToString();

                                row.Cells["CreditAmount"].Value =
                                    Convert.ToDecimal(
                                        reader["CreditAmount"])
                                        .ToString("N2");

                                row.Cells["Remarks"].Value =
                                    reader["Remarks"] == DBNull.Value
                                        ? ""
                                        : reader["Remarks"].ToString();
                            }
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
                    "Payment History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    class CashAccountItem
    {
        public int CashAccountID { get; set; }
        public string DisplayText { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
