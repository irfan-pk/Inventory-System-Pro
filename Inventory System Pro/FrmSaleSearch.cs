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
    public partial class FrmSaleSearch : Form
    {
        public int SelectedSaleID { get; private set; }
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;

        public FrmSaleSearch()
        {
            InitializeComponent();
        }

        private void LoadSales(string searchText = "")
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT
            s.SaleID,
            s.SaleNumber,
            s.SaleDate,
            c.CustomerName,
            w.WarehouseName,
            so.SONumber,
            s.Status,
            s.NetAmount,
            s.PaidAmount,
            s.BalanceAmount
            FROM dbo.Sales AS s

            INNER JOIN dbo.Customers AS c
                ON c.CustomerID = s.CustomerID

            INNER JOIN dbo.Warehouses AS w
                ON w.WarehouseID = s.WarehouseID

            LEFT JOIN dbo.SaleOrders AS so
                ON so.SOID = s.SOID

            WHERE
                s.SaleNumber LIKE '%' + @Search + '%'
                OR c.CustomerName LIKE '%' + @Search + '%'
                OR ISNULL(so.SONumber, '') LIKE '%' + @Search + '%'

            ORDER BY s.SaleID DESC;", cn))
            {
                cmd.Parameters.Add(
                    "@Search",
                    SqlDbType.NVarChar,
                    100).Value = searchText ?? "";

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dgvSales.DataSource = dt;

            FormatSalesGrid();
        }

        private void FormatSalesGrid()
        {
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;

            dgvSales.ReadOnly = true;
            dgvSales.MultiSelect = false;
            dgvSales.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvSales.RowHeadersVisible = false;

            dgvSales.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            if (dgvSales.Columns.Contains("SaleID"))
                dgvSales.Columns["SaleID"].Visible = false;

            if (dgvSales.Columns.Contains("SaleNumber"))
            {
                dgvSales.Columns["SaleNumber"].HeaderText = "Sale No";
                dgvSales.Columns["SaleNumber"].Width = 120;
            }

            if (dgvSales.Columns.Contains("SaleDate"))
            {
                dgvSales.Columns["SaleDate"].HeaderText = "Date";
                dgvSales.Columns["SaleDate"].Width = 100;
                dgvSales.Columns["SaleDate"].DefaultCellStyle.Format =
                    "yyyy-MM-dd";
            }

            if (dgvSales.Columns.Contains("CustomerName"))
            {
                dgvSales.Columns["CustomerName"].HeaderText = "Customer";
                dgvSales.Columns["CustomerName"].Width = 220;
            }

            if (dgvSales.Columns.Contains("WarehouseName"))
            {
                dgvSales.Columns["WarehouseName"].HeaderText = "Warehouse";
                dgvSales.Columns["WarehouseName"].Width = 197;
            }

            if (dgvSales.Columns.Contains("SONumber"))
            {
                dgvSales.Columns["SONumber"].HeaderText = "S.O No";
                dgvSales.Columns["SONumber"].Width = 120;
            }

            if (dgvSales.Columns.Contains("Status"))
            {
                dgvSales.Columns["Status"].HeaderText = "Status";
                dgvSales.Columns["Status"].Width = 100;
            }

            if (dgvSales.Columns.Contains("NetAmount"))
            {
                dgvSales.Columns["NetAmount"].HeaderText = "Net Amount";
                dgvSales.Columns["NetAmount"].Width = 110;
                dgvSales.Columns["NetAmount"].DefaultCellStyle.Format =
                    "N2";
                dgvSales.Columns["NetAmount"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvSales.Columns.Contains("PaidAmount"))
            {
                dgvSales.Columns["PaidAmount"].HeaderText = "Paid";
                dgvSales.Columns["PaidAmount"].Width = 100;
                dgvSales.Columns["PaidAmount"].DefaultCellStyle.Format =
                    "N2";
                dgvSales.Columns["PaidAmount"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvSales.Columns.Contains("BalanceAmount"))
            {
                dgvSales.Columns["BalanceAmount"].HeaderText = "Balance";
                dgvSales.Columns["BalanceAmount"].Width = 100;
                dgvSales.Columns["BalanceAmount"].DefaultCellStyle.Format =
                    "N2";
                dgvSales.Columns["BalanceAmount"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            foreach (DataGridViewColumn col in dgvSales.Columns)
            {
                col.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            dgvSales.RowTemplate.Height = 28;
        }

        private void SelectCurrentSale()
        {
            if (dgvSales.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a Sale.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            object value =
                dgvSales.CurrentRow.Cells["SaleID"].Value;

            if (value == null || value == DBNull.Value)
            {
                MessageBox.Show(
                    "Invalid Sale.",
                    "Sale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SelectedSaleID = Convert.ToInt32(value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadSales(txtSearch.Text.Trim());
            }
        }

        private void FrmSaleSearch_Load(object sender, EventArgs e)
        {
            LoadSales();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSales(txtSearch.Text.Trim());
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentSale();
        }

        private void dgvSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            SelectCurrentSale();
        }
    }
}
