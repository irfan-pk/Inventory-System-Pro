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
    public partial class FrmCashAccountMaster : Form
    {
        private int _selectedCashAccountId;
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmCashAccountMaster()
        {
            InitializeComponent();
        }

        private void FrmCashAccountMaster_Load(object sender, EventArgs e)
        {
            //cboAccountType.Items.AddRange(new object[] { "CASH", "BANK" });
            cboAccountType.SelectedIndex = -1;
            chkIsActive.Checked = true;

            LoadCashAccounts();
            ClearFields();
        }

        private void LoadCashAccounts()
        {
            using (var cn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("dbo.sp_CashAccount_List", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ActiveOnly", SqlDbType.Bit).Value = false;

                var table = new DataTable();
                using (var adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(table);

                dgvCashAccounts.DataSource = table;

                if (dgvCashAccounts.Columns.Contains("CashAccountID"))
                    dgvCashAccounts.Columns["CashAccountID"].Visible = false;

                if (dgvCashAccounts.Columns.Contains("OpeningBalance"))
                    dgvCashAccounts.Columns["OpeningBalance"]
                        .DefaultCellStyle.Format = "N2";
            }
        }

        private void dgvCashAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCashAccounts.CurrentRow == null)
                return;

            var row = dgvCashAccounts.CurrentRow.DataBoundItem as DataRowView;
            if (row == null)
                return;

            _selectedCashAccountId = Convert.ToInt32(row["CashAccountID"]);
            txtAccountCode.Text = Convert.ToString(row["AccountCode"]);
            txtAccountName.Text = Convert.ToString(row["AccountName"]);
            cboAccountType.SelectedItem = Convert.ToString(row["AccountType"]);
            //nudOpeningBalance.Value = Convert.ToDecimal(row["OpeningBalance"]);
            txtOpeningBalance.Text = Convert.ToDecimal(row["OpeningBalance"]).ToString("N2");
            chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);

            btnDeactivate.Enabled = chkIsActive.Checked;
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtAccountCode.Text) ||
                string.IsNullOrWhiteSpace(txtAccountName.Text) ||
                cboAccountType.SelectedItem == null)
            {
                MessageBox.Show("Enter the code, name, and account type.");
                return false;
            }

            if (txtAccountCode.Text.Trim().Length > 30 ||
                txtAccountName.Text.Trim().Length > 100)
            {
                MessageBox.Show("The account code or name is too long.");
                return false;
            }

            return true;
        }

        private void SaveAccount()
        {
            if (!ValidateFields())
                return;

            bool isNew = _selectedCashAccountId == 0;
            string procedure = isNew
                ? "dbo.sp_CashAccount_Insert"
                : "dbo.sp_CashAccount_Update";

            using (var cn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand(procedure, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (!isNew)
                    cmd.Parameters.Add("@CashAccountID", SqlDbType.Int)
                        .Value = _selectedCashAccountId;

                cmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 30)
                    .Value = txtAccountCode.Text.Trim();
                cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100)
                    .Value = txtAccountName.Text.Trim();
                cmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 10)
                    .Value = cboAccountType.SelectedItem.ToString();

                var openingBalance =
                    cmd.Parameters.Add("@OpeningBalance", SqlDbType.Decimal);
                openingBalance.Precision = 18;
                openingBalance.Scale = 2;
                openingBalance.Value = Convert.ToDecimal(txtOpeningBalance.Text);

                cmd.Parameters.Add("@IsActive", SqlDbType.Bit)
                    .Value = chkIsActive.Checked;

                cn.Open();

                if (isNew)
                    _selectedCashAccountId = Convert.ToInt32(cmd.ExecuteScalar());
                else
                    cmd.ExecuteNonQuery();
            }

            LoadCashAccounts();
            ClearFields();
        }

        private void DeactivateAccount()
        {
            if (_selectedCashAccountId == 0)
                return;

            using (var cn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("dbo.sp_CashAccount_SetActive", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CashAccountID", SqlDbType.Int)
                    .Value = _selectedCashAccountId;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadCashAccounts();
            ClearFields();
        }

        private void ClearFields()
        {
            _selectedCashAccountId = 0;
            txtAccountCode.Clear();
            txtAccountName.Clear();
            cboAccountType.SelectedIndex = -1;
            txtOpeningBalance.Text = "0.00";
            chkIsActive.Checked = true;
            btnDeactivate.Enabled = false;
            dgvCashAccounts.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("CASH_ACCOUNT.SAVE"))
            {
                MessageBox.Show("You do not have permission to save cash accounts.");
                return;
            }
            SaveAccount();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("CASH_ACCOUNT.DELETE"))
            {
                MessageBox.Show("You do not have permission to deactivate cash accounts.");
                return;
            }
            DeactivateAccount();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("CASH_ACCOUNT.NEW"))
            {
                MessageBox.Show("You do not have permission to create new cash accounts.");
                return;
            }
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(!AppSession.HasPermission("CASH_ACCOUNT.UPDATE"))
            {
                MessageBox.Show("You do not have permission to update cash accounts.");
                return;
            }
            if (_selectedCashAccountId <= 0)
            {
                MessageBox.Show("Please select a cash account first.");
                return;
            }
            UpdateAccount();
        }

        private void UpdateAccount()
        {
            if (_selectedCashAccountId == 0)
            {
                MessageBox.Show("Please select a cash account first.");
                return;
            }

            if (!ValidateFields())
                return;

            if (!decimal.TryParse(txtOpeningBalance.Text, out decimal openingBalance))
            {
                MessageBox.Show("Enter a valid opening balance.");
                txtOpeningBalance.Focus();
                return;
            }

            using (var cn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("dbo.sp_CashAccount_Update", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CashAccountID", SqlDbType.Int)
                    .Value = _selectedCashAccountId;

                cmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 30)
                    .Value = txtAccountCode.Text.Trim();

                cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100)
                    .Value = txtAccountName.Text.Trim();

                cmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 10)
                    .Value = cboAccountType.SelectedItem.ToString();

                var pOpeningBalance =
                    cmd.Parameters.Add("@OpeningBalance", SqlDbType.Decimal);

                pOpeningBalance.Precision = 18;
                pOpeningBalance.Scale = 2;
                pOpeningBalance.Value = openingBalance;

                cmd.Parameters.Add("@IsActive", SqlDbType.Bit)
                    .Value = chkIsActive.Checked;

                cn.Open();

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cash account updated successfully.");

            LoadCashAccounts();
            ClearFields();
        }
    }
}
