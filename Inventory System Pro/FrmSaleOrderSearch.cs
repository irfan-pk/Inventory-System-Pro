using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventorySystemPro
{
    public partial class FrmSaleOrderSearch : Form
    {
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public int SelectedSOID { get; private set; }

        public FrmSaleOrderSearch()
        {
            InitializeComponent();
        }

        private void FrmSaleOrderSearch_Load(
            object sender,
            EventArgs e)
        {
            LoadPendingSalesOrders();
        }

        private void LoadPendingSalesOrders(string searchText = "")
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(ConnString))
            using (SqlCommand cmd =
                   new SqlCommand(@"
                SELECT
                    so.SOID,
                    so.SONumber,
                    so.SODate,
                    c.CustomerName,
                    w.WarehouseName,
                    so.Status

                FROM dbo.SaleOrders so

                INNER JOIN dbo.Customers c
                    ON c.CustomerID = so.CustomerID

                INNER JOIN dbo.Warehouses w
                    ON w.WarehouseID = so.WarehouseID

                WHERE so.Status = 'OPEN'
                  AND
                  (
                      so.SONumber LIKE '%' + @Search + '%'
                      OR c.CustomerName LIKE '%' + @Search + '%'
                  )

                ORDER BY so.SOID DESC;", cn))
            {
                cmd.Parameters.Add(
                    "@Search",
                    SqlDbType.NVarChar,
                    100).Value =
                    searchText ?? "";

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dgvOrders.DataSource = dt;

            FormatOrdersGrid();
        }

        private void FormatOrdersGrid()
        {
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;

            dgvOrders.ReadOnly = true;
            dgvOrders.MultiSelect = false;
            dgvOrders.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvOrders.RowHeadersVisible = false;
            dgvOrders.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            if (dgvOrders.Columns.Contains("SOID"))
                dgvOrders.Columns["SOID"].Visible = false;

            if (dgvOrders.Columns.Contains("SONumber"))
            {
                dgvOrders.Columns["SONumber"].HeaderText =
                    "S.O No";

                dgvOrders.Columns["SONumber"].Width = 120;
            }

            if (dgvOrders.Columns.Contains("SODate"))
            {
                dgvOrders.Columns["SODate"].HeaderText =
                    "Date";

                dgvOrders.Columns["SODate"].Width = 100;
            }

            if (dgvOrders.Columns.Contains("CustomerName"))
            {
                dgvOrders.Columns["CustomerName"].HeaderText =
                    "Customer";

                dgvOrders.Columns["CustomerName"].Width = 220;
            }

            if (dgvOrders.Columns.Contains("WarehouseName"))
            {
                dgvOrders.Columns["WarehouseName"].HeaderText =
                    "Warehouse";

                dgvOrders.Columns["WarehouseName"].Width = 180;
            }

            if (dgvOrders.Columns.Contains("Status"))
            {
                dgvOrders.Columns["Status"].HeaderText =
                    "Status";

                dgvOrders.Columns["Status"].Width = 100;
            }

            foreach (DataGridViewColumn col
                     in dgvOrders.Columns)
            {
                col.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            dgvOrders.RowTemplate.Height = 28;
        }

        private void SelectCurrentSalesOrder()
        {
            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a Sales Order.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            object value =
                dgvOrders.CurrentRow
                    .Cells["SOID"].Value;

            if (value == null ||
                value == DBNull.Value)
            {
                MessageBox.Show(
                    "Invalid Sales Order.",
                    "Sales Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SelectedSOID =
                Convert.ToInt32(value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void dgvOrders_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            SelectCurrentSalesOrder();
        }

        private void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            LoadPendingSalesOrders(txtSearch.Text.Trim());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                LoadPendingSalesOrders(txtSearch.Text.Trim());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentSalesOrder();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadPendingSalesOrders(
                txtSearch.Text.Trim());
        }
    }
}