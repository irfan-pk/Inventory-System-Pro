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

namespace Inventory_System_Pro
{
    public partial class FrmCashAccountLedger : Form
    {
        private int CurrentCashAccountID = 0;

        private decimal CurrentOpeningBalance = 0m;
        private decimal CurrentTotalDebit = 0m;
        private decimal CurrentTotalCredit = 0m;
        private decimal CurrentClosingBalance = 0m;

        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;
        public FrmCashAccountLedger()
        {
            InitializeComponent();
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

            cboCashAccount.DataSource = null;

            cboCashAccount.DisplayMember = "AccountDisplay";
            cboCashAccount.ValueMember = "CashAccountID";

            DataColumn displayColumn =
                dt.Columns.Add("AccountDisplay", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                row["AccountDisplay"] =
                    row["AccountCode"].ToString()
                    + " - "
                    + row["AccountName"].ToString();
            }

            cboCashAccount.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                cboCashAccount.SelectedIndex = 0;
                CurrentCashAccountID =
                    Convert.ToInt32(cboCashAccount.SelectedValue);
            }
        }

        private void ClearLedger()
        {
            CurrentCashAccountID = 0;

            CurrentOpeningBalance = 0m;
            CurrentTotalDebit = 0m;
            CurrentTotalCredit = 0m;
            CurrentClosingBalance = 0m;

            lblOpeningBalance.Text = "0.00";
            lblTotalDebit.Text = "0.00";
            lblTotalCredit.Text = "0.00";
            lblClosingBalance.Text = "0.00";

            //lblSupplierName.Text = "";
            lblStatementPeriod.Text = "";

            dgvCashLedger.DataSource = null;
        }

        private void LoadCashLedger()
        {
            if (cboCashAccount.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a cash account.",
                    "Cash Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                cboCashAccount.SelectedValue.ToString(),
                out int cashAccountID))
            {
                MessageBox.Show(
                    "Invalid cash account.",
                    "Cash Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show(
                    "From date cannot be greater than To date.",
                    "Cash Ledger",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CurrentCashAccountID = cashAccountID;

            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(
                "dbo.sp_CashAccount_Ledger",
                cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CashAccountID", SqlDbType.Int)
                    .Value = CurrentCashAccountID;

                cmd.Parameters.Add("@FromDate", SqlDbType.Date)
                    .Value = dtpFromDate.Value.Date;

                cmd.Parameters.Add("@ToDate", SqlDbType.Date)
                    .Value = dtpToDate.Value.Date;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dgvCashLedger.DataSource = dt;

            CalculateCashLedgerSummary(dt);
            FormatCashLedgerGrid();

            lblStatementPeriod.Text =
                "Period: "
                + dtpFromDate.Value.ToString("yyyy-MM-dd")
                + " to "
                + dtpToDate.Value.ToString("yyyy-MM-dd");
        }

        private void CalculateCashLedgerSummary(DataTable dt)
        {
            decimal openingBalance = 0m;
            decimal totalDebit = 0m;
            decimal totalCredit = 0m;
            decimal closingBalance = 0m;

            if (dt.Rows.Count > 0)
            {
                DataRow firstRow = dt.Rows[0];

                if (firstRow["OpeningBalance"] != DBNull.Value)
                {
                    openingBalance =
                        Convert.ToDecimal(firstRow["OpeningBalance"]);
                }
            }

            if (dt.Rows.Count > 0)
            {
                totalDebit = Convert.ToDecimal(
                    dt.Compute("SUM(DebitAmount)", ""));

                totalCredit = Convert.ToDecimal(
                    dt.Compute("SUM(CreditAmount)", ""));
            }

            closingBalance =
                openingBalance
                + totalDebit
                - totalCredit;

            CurrentOpeningBalance = openingBalance;
            CurrentTotalDebit = totalDebit;
            CurrentTotalCredit = totalCredit;
            CurrentClosingBalance = closingBalance;

            lblOpeningBalance.Text =
                openingBalance.ToString("N2");

            lblTotalDebit.Text =
                totalDebit.ToString("N2");

            lblTotalCredit.Text =
                totalCredit.ToString("N2");

            lblClosingBalance.Text =
                closingBalance.ToString("N2");
        }

        private void FormatCashLedgerGrid()
        {
            if (dgvCashLedger.Columns.Count == 0)
                return;

            dgvCashLedger.AutoGenerateColumns = true;
            dgvCashLedger.ReadOnly = true;
            dgvCashLedger.AllowUserToAddRows = false;
            dgvCashLedger.AllowUserToDeleteRows = false;
            dgvCashLedger.AllowUserToResizeRows = false;
            dgvCashLedger.MultiSelect = false;
            dgvCashLedger.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCashLedger.RowHeadersVisible = false;
            dgvCashLedger.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            if (dgvCashLedger.Columns.Contains("CashTransactionID"))
                dgvCashLedger.Columns["CashTransactionID"].Visible = false;

            //if (dgvCashLedger.Columns.Contains("ReferenceID"))
            //    dgvCashLedger.Columns["ReferenceID"].Visible = false;

            if (dgvCashLedger.Columns.Contains("CreatedBy"))
                dgvCashLedger.Columns["CreatedBy"].Visible = false;

            if (dgvCashLedger.Columns.Contains("CreatedDate"))
                dgvCashLedger.Columns["CreatedDate"].Visible = false;

            if (dgvCashLedger.Columns.Contains("OpeningBalance"))
                dgvCashLedger.Columns["OpeningBalance"].Visible = false;

            if (dgvCashLedger.Columns.Contains("TranDate"))
            {
                dgvCashLedger.Columns["TranDate"].HeaderText = "Date";
                dgvCashLedger.Columns["TranDate"].Width = 100;
                dgvCashLedger.Columns["TranDate"].DefaultCellStyle.Format =
                    "yyyy-MM-dd";
            }

            if (dgvCashLedger.Columns.Contains("TranType"))
            {
                dgvCashLedger.Columns["TranType"].HeaderText = "Transaction";
                dgvCashLedger.Columns["TranType"].Width = 225;
            }

            if (dgvCashLedger.Columns.Contains("ReferenceID"))
            {
                dgvCashLedger.Columns["ReferenceID"].HeaderText = "Ref.ID";
                dgvCashLedger.Columns["ReferenceID"].Width = 65;
                dgvCashLedger.Columns["ReferenceID"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvCashLedger.Columns.Contains("ReferenceNo"))
            {
                dgvCashLedger.Columns["ReferenceNo"].HeaderText = "Reference #";
                dgvCashLedger.Columns["ReferenceNo"].Width = 120;
            }

            if (dgvCashLedger.Columns.Contains("DebitAmount"))
            {
                dgvCashLedger.Columns["DebitAmount"].HeaderText = "Debit";
                dgvCashLedger.Columns["DebitAmount"].Width = 100;
                dgvCashLedger.Columns["DebitAmount"].DefaultCellStyle.Format =
                    "N2";
                dgvCashLedger.Columns["DebitAmount"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCashLedger.Columns.Contains("CreditAmount"))
            {
                dgvCashLedger.Columns["CreditAmount"].HeaderText = "Credit";
                dgvCashLedger.Columns["CreditAmount"].Width = 100;
                dgvCashLedger.Columns["CreditAmount"].DefaultCellStyle.Format =
                    "N2";
                dgvCashLedger.Columns["CreditAmount"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCashLedger.Columns.Contains("Remarks"))
            {
                dgvCashLedger.Columns["Remarks"].HeaderText = "Remarks";
                dgvCashLedger.Columns["Remarks"].Width = 325;
            }

            if (dgvCashLedger.Columns.Contains("RunningBalance"))
            {
                dgvCashLedger.Columns["RunningBalance"].HeaderText =
                    "Running Balance";
                dgvCashLedger.Columns["RunningBalance"].Width = 125;
                dgvCashLedger.Columns["RunningBalance"].DefaultCellStyle.Format =
                    "N2";
                dgvCashLedger.Columns["RunningBalance"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            dgvCashLedger.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvCashLedger.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font(
                    dgvCashLedger.Font,
                    System.Drawing.FontStyle.Bold);

            dgvCashLedger.RowTemplate.Height = 28;

            dgvCashLedger.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;
        }

        private void FrmCashAccountLedger_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                1);

            dtpToDate.Value = DateTime.Today;

            LoadCashAccounts();

            ClearLedger();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCashLedger();
        }
    }
}
