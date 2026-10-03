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
    public partial class FrmProductSearch : Form
    {
        public int SelectedProductID { get; private set; }
        public string SelectedProductCode { get; private set; }
        public string SelectedProductName { get; private set; }
        public decimal SelectedSalePrice { get; private set; }
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;
        public FrmProductSearch()
        {
            InitializeComponent();
        }

        private void LoadProducts(string searchText = "")
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                SELECT
                    P.ProductID,
                    P.ProductCode,
                    P.Barcode,
                    P.ProductName,
                    U.UnitName,
                    P.SalePrice

                FROM dbo.Products P

                INNER JOIN dbo.Units U
                    ON U.UnitID = P.UnitID

                WHERE P.IsActive = 1

                  AND
                  (
                        @Search = ''
                        OR P.ProductCode LIKE @SearchPattern
                        OR P.Barcode LIKE @SearchPattern
                        OR P.ProductName LIKE @SearchPattern
                  )

                ORDER BY
                    P.ProductName;";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@Search",
                            SqlDbType.NVarChar, 200).Value =
                            searchText.Trim();

                        cmd.Parameters.Add(
                            "@SearchPattern",
                            SqlDbType.NVarChar, 202).Value =
                            "%" + searchText.Trim() + "%";

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                dgvProducts.DataSource = dt;

                FormatProductSearchGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Product Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void FormatProductSearchGrid()
        {
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.MultiSelect = false;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            if (dgvProducts.Columns.Contains("ProductID"))
                dgvProducts.Columns["ProductID"].Visible = false;

            if (dgvProducts.Columns.Contains("ProductCode"))
            {
                dgvProducts.Columns["ProductCode"].HeaderText =
                    "Code";

                dgvProducts.Columns["ProductCode"].Width =
                    100;
            }

            if (dgvProducts.Columns.Contains("Barcode"))
            {
                dgvProducts.Columns["Barcode"].HeaderText =
                    "Barcode";

                dgvProducts.Columns["Barcode"].Width =
                    140;
            }

            if (dgvProducts.Columns.Contains("ProductName"))
            {
                dgvProducts.Columns["ProductName"].HeaderText =
                    "Product";

                dgvProducts.Columns["ProductName"].Width =
                    250;
            }

            if (dgvProducts.Columns.Contains("UnitName"))
            {
                dgvProducts.Columns["UnitName"].HeaderText =
                    "Unit";

                dgvProducts.Columns["UnitName"].Width =
                    100;
            }

            if (dgvProducts.Columns.Contains("SalePrice"))
            {
                dgvProducts.Columns["SalePrice"].HeaderText =
                    "Sale Price";

                dgvProducts.Columns["SalePrice"].Width =
                    110;

                dgvProducts.Columns["SalePrice"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvProducts.Columns["SalePrice"]
                    .DefaultCellStyle.Format =
                    "N2";
            }

            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.RowTemplate.Height = 28;
        }

        private void SelectProduct()
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a product.",
                    "Product Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvProducts.CurrentRow;

            SelectedProductID =
                Convert.ToInt32(
                    row.Cells["ProductID"].Value);

            SelectedProductCode =
                row.Cells["ProductCode"].Value
                    ?.ToString();

            SelectedProductName =
                row.Cells["ProductName"].Value
                    ?.ToString();

            SelectedSalePrice =
                Convert.ToDecimal(
                    row.Cells["SalePrice"].Value);

            DialogResult =
                DialogResult.OK;

            Close();
        }

        private void FrmProductSearch_Load(
            object sender,
            EventArgs e)
        {
            LoadProducts();

            txtSearch.Focus();
        }

        private void btnSelect_Click(
            object sender,
            EventArgs e)
        {
            SelectProduct();
        }

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void dgvProducts_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            SelectProduct();
        }

        private void dgvProducts_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                SelectProduct();
            }
        }

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}
