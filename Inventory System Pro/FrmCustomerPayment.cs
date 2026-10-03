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
    public partial class FrmCustomerPayment : Form
    {
        private int CurrentCustomerID = 0;
        private int CurrentCashAccountID = 0;

        private decimal CurrentCustomerBalance = 0m;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;
        public FrmCustomerPayment()
        {
            InitializeComponent();
        }

        private void LoadCustomers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                CustomerID,
                CustomerCode,
                CustomerName
                FROM dbo.Customers
                WHERE IsActive = 1
                ORDER BY CustomerName;", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            DataColumn displayColumn =
                dt.Columns.Add("CustomerDisplay", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                row["CustomerDisplay"] =
                    row["CustomerCode"].ToString()
                    + " - "
                    + row["CustomerName"].ToString();
            }

            cboCustomer.DataSource = null;

            cboCustomer.DisplayMember = "CustomerDisplay";
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.DataSource = dt;

            if (dt.Rows.Count > 0)
                cboCustomer.SelectedIndex = -1;
        }

        private void LoadCashAccounts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                    CashAccountID,
                    AccountCode,
                    AccountName
                FROM dbo.CashAccounts
                WHERE AccountType = 'CASH'
                  AND IsActive = 1
                ORDER BY CashAccountID;", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            DataColumn displayColumn =
                dt.Columns.Add("AccountDisplay", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                row["AccountDisplay"] =
                    row["AccountCode"].ToString()
                    + " - "
                    + row["AccountName"].ToString();
            }

            cboCashAccount.DataSource = null;
            
            cboCashAccount.DisplayMember = "AccountDisplay";
            cboCashAccount.ValueMember = "CashAccountID";
            cboCashAccount.DataSource = dt;

            if (dt.Rows.Count > 0)
                cboCashAccount.SelectedIndex = 0;
        }

        private void NewPayment()
        {
            CurrentCustomerID = 0;
            CurrentCashAccountID = 0;
            CurrentCustomerBalance = 0m;

            dtpPaymentDate.Value = DateTime.Today;

            txtAmount.Clear();
            txtRemarks.Clear();

            // Customer must remain blank
            if (cboCustomer.Items.Count > 0)
                cboCustomer.SelectedIndex = -1;

            // Cash account can default to the first account
            if (cboCashAccount.Items.Count > 0)
                cboCashAccount.SelectedIndex = 0;

            lblCustomerBalance.Text = "0.00";
            lblBalanceAfter.Text = "0.00";
            lblPaymentStatus.Text = "";

            if (lblPaymentNumber != null)
                lblPaymentNumber.Text = "";

            dgvPaymentHistory.Rows.Clear();

            btnSave.Enabled = true;
        }

        private void CalculateBalanceAfter()
        {
            decimal paymentAmount = 0m;

            decimal.TryParse(
                txtAmount.Text.Trim(),
                out paymentAmount);

            decimal balanceAfter =
                CurrentCustomerBalance - paymentAmount;

            lblBalanceAfter.Text =
                balanceAfter.ToString("N2");

            if (balanceAfter <= 0.01m &&
                balanceAfter >= -0.01m)
            {
                lblPaymentStatus.Text = "SETTLED";
            }
            else if (balanceAfter < 0)
            {
                lblPaymentStatus.Text = "ADVANCE";
            }
            else
            {
                lblPaymentStatus.Text = "BALANCE";
            }
        }

        private void SetupPaymentHistoryGrid()
        {
            dgvPaymentHistory.AutoGenerateColumns = false;
            dgvPaymentHistory.Columns.Clear();

            dgvPaymentHistory.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "TranDate",
                    HeaderText = "Date",
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Format = "dd-MM-yyyy"
                    },
                    Width = 150
                });

            dgvPaymentHistory.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "PaymentNumber",
                    HeaderText = "Payment No.",
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    },
                    Width = 200
                });

            dgvPaymentHistory.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Amount",
                    HeaderText = "Amount",
                    Width = 110
                });

            dgvPaymentHistory.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Remarks",
                    HeaderText = "Remarks",
                    Width = 537
                });

            dgvPaymentHistory.ReadOnly = true;
            dgvPaymentHistory.AllowUserToAddRows = false;
            dgvPaymentHistory.AllowUserToDeleteRows = false;
            dgvPaymentHistory.AllowUserToResizeRows = false;
            dgvPaymentHistory.MultiSelect = false;

            dgvPaymentHistory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPaymentHistory.RowHeadersVisible = false;

            dgvPaymentHistory.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvPaymentHistory.RowTemplate.Height = 28;

            dgvPaymentHistory.Columns["Amount"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvPaymentHistory.Columns["Amount"]
                .DefaultCellStyle.Format = "N2";
        }

        private void LoadPaymentHistory()
        {
            if (CurrentCustomerID <= 0)
            {
                dgvPaymentHistory.Rows.Clear();
                return;
            }

            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                TranDate,
                ReferenceNo AS PaymentNumber,
                CreditAmount AS Amount,
                Remarks
                FROM dbo.CustomerLedger
                WHERE CustomerID = @CustomerID
                  AND TranType = 'CUSTOMER_PAYMENT'
                ORDER BY CustomerLedgerID DESC;", cn))
            {
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int)
                    .Value = CurrentCustomerID;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dgvPaymentHistory.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int index = dgvPaymentHistory.Rows.Add();

                dgvPaymentHistory.Rows[index].Cells["TranDate"].Value =
                    Convert.ToDateTime(row["TranDate"])
                        .ToString("dd-MM-yyyy");

                dgvPaymentHistory.Rows[index].Cells["PaymentNumber"].Value =
                    row["PaymentNumber"] == DBNull.Value
                        ? ""
                        : row["PaymentNumber"].ToString();

                dgvPaymentHistory.Rows[index].Cells["Amount"].Value =
                    Convert.ToDecimal(row["Amount"]);

                dgvPaymentHistory.Rows[index].Cells["Remarks"].Value =
                    row["Remarks"] == DBNull.Value
                        ? ""
                        : row["Remarks"].ToString();
            }
        }

        private void LoadCustomerBalance()
        {
            if (cboCustomer.SelectedValue == null)
            {
                CurrentCustomerID = 0;
                CurrentCustomerBalance = 0m;

                lblCustomerBalance.Text = "0.00";
                lblBalanceAfter.Text = "0.00";
                lblPaymentStatus.Text = "";

                return;
            }

            if (!int.TryParse(
                cboCustomer.SelectedValue.ToString(),
                out int customerID))
            {
                CurrentCustomerID = 0;
                CurrentCustomerBalance = 0m;

                lblCustomerBalance.Text = "0.00";
                lblBalanceAfter.Text = "0.00";
                lblPaymentStatus.Text = "";

                return;
            }

            CurrentCustomerID = customerID;

            decimal totalDebit = 0m;
            decimal totalCredit = 0m;

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT
                    ISNULL(SUM(DebitAmount), 0) AS TotalDebit,
                    ISNULL(SUM(CreditAmount), 0) AS TotalCredit
                FROM dbo.CustomerLedger
                WHERE CustomerID = @CustomerID;", cn))
            {
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int)
                    .Value = CurrentCustomerID;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        totalDebit =
                            Convert.ToDecimal(dr["TotalDebit"]);

                        totalCredit =
                            Convert.ToDecimal(dr["TotalCredit"]);
                    }
                }
            }

            CurrentCustomerBalance =
                totalDebit - totalCredit;

            lblCustomerBalance.Text =
                CurrentCustomerBalance.ToString("N2");

            CalculateBalanceAfter();
            LoadPaymentHistory();
        }

        private void SavePayment()
        {
            string remarks = txtRemarks.Text.Trim();

            // Customer validation
            if (CurrentCustomerID <= 0)
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboCustomer.Focus();
                return;
            }

            // Cash account validation
            if (cboCashAccount.SelectedValue == null ||
                !int.TryParse(
                    cboCashAccount.SelectedValue.ToString(),
                    out int cashAccountID) ||
                cashAccountID <= 0)
            {
                MessageBox.Show(
                    "Please select a valid cash account.",
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboCashAccount.Focus();
                return;
            }

            CurrentCashAccountID = cashAccountID;

            // Amount validation
            if (!decimal.TryParse(
                txtAmount.Text.Trim(),
                out decimal amount) ||
                amount <= 0)
            {
                MessageBox.Show(
                    "Payment amount must be greater than zero.",
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtAmount.Focus();
                return;
            }

            // Remarks validation
            if (string.IsNullOrWhiteSpace(remarks))
            {
                MessageBox.Show(
                    "Remarks are required.",
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRemarks.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(
                           "dbo.sp_CustomerPayment_Save",
                           cn))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@PaymentDate",
                        SqlDbType.Date).Value =
                        dtpPaymentDate.Value.Date;

                    cmd.Parameters.Add(
                        "@CustomerID",
                        SqlDbType.Int).Value =
                        CurrentCustomerID;

                    cmd.Parameters.Add(
                        "@CashAccountID",
                        SqlDbType.Int).Value =
                        CurrentCashAccountID;

                    SqlParameter amountParameter =
                        cmd.Parameters.Add(
                            "@Amount",
                            SqlDbType.Decimal);

                    amountParameter.Precision = 18;
                    amountParameter.Scale = 2;
                    amountParameter.Value = amount;

                    cmd.Parameters.Add(
                        "@Remarks",
                        SqlDbType.NVarChar,
                        500).Value =
                        remarks;

                    cmd.Parameters.Add(
                        "@CreatedBy",
                        SqlDbType.Int).Value = 1;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show(
                                "Payment was not saved.",
                                "Customer Payment",
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
                                "Customer Payment",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        // Payment number generated by SQL Server
                        lblPaymentNumber.Text =
                            reader["PaymentNumber"].ToString();

                        // Updated customer balance
                        decimal customerBalance =
                            Convert.ToDecimal(
                                reader["CustomerBalance"]);

                        CurrentCustomerBalance =
                            customerBalance;

                        lblCustomerBalance.Text =
                            customerBalance.ToString("N2");

                        lblBalanceAfter.Text =
                            customerBalance.ToString("N2");

                        if (customerBalance > 0)
                        {
                            lblPaymentStatus.Text =
                                "BALANCE";
                        }
                        else if (customerBalance == 0)
                        {
                            lblPaymentStatus.Text =
                                "SETTLED";
                        }
                        else
                        {
                            lblPaymentStatus.Text =
                                "ADVANCE";
                        }
                    }
                }

                MessageBox.Show(
                    "Customer payment saved successfully.\n\n" +
                    "Payment No: " +
                    lblPaymentNumber.Text +
                    "\nAmount: " +
                    amount.ToString("N2"),
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Refresh payment history
                LoadPaymentHistory();

                // Prevent accidental duplicate save
                btnSave.Enabled = false;
                NewPayment();
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
                    "Customer Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FrmCustomerPayment_Load(object sender, EventArgs e)
        {
            dtpPaymentDate.Value = DateTime.Today;
            SetupPaymentHistoryGrid();
            LoadCustomers();
            LoadCashAccounts();
            cboCustomer.SelectedIndex = -1;
            NewPayment();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCustomerBalance();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateBalanceAfter();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER_PAYMENT.SAVE"))
            {
                MessageBox.Show("You do not have permission to save customer payments.");
                return;
            }
            SavePayment();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("CUSTOMER_PAYMENT.NEW"))
            {
                MessageBox.Show("You do not have permission to create customer payments.");
                return;
            }
            NewPayment();
        }
    }
}
