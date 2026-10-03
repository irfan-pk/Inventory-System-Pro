using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace Inventory_System_Pro
{
    public partial class FrmProductStock : Form
    {
        private readonly string ConnString =
            ConfigurationManager.ConnectionStrings[
                "InventoryConnection"].ConnectionString;

        public FrmProductStock()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void FrmProductStock_Load(object sender, EventArgs e)
        {
            SetupListView();

            LoadWarehouses();

            if (cmbWarehouse.Items.Count > 0)
                cmbWarehouse.SelectedIndex = 0;

            LoadStock();
        }

        // =========================================================
        // LISTVIEW
        // =========================================================

        private void SetupListView()
        {
            lvStock.View = View.Details;
            lvStock.FullRowSelect = true;
            lvStock.GridLines = true;
            lvStock.MultiSelect = false;

            lvStock.Columns.Clear();

            lvStock.Columns.Add("ProductID", 0);
            lvStock.Columns.Add("Code", 100);
            lvStock.Columns.Add("Product Name", 180);
            lvStock.Columns.Add("Unit", 70);
            lvStock.Columns.Add("Warehouse", 120);
            lvStock.Columns.Add("Current Qty", 90);
            lvStock.Columns.Add("Last Cost", 90);
            lvStock.Columns.Add("Stock Value", 100);
            lvStock.Columns.Add("Sale Price", 90);
            lvStock.Columns.Add("Potential Sales", 110);
            lvStock.Columns.Add("Potential Profit", 110);
            lvStock.Columns.Add("Reorder Level", 100);
            lvStock.Columns.Add("Status", 90);
        }

        // =========================================================
        // WAREHOUSES
        // =========================================================

        private void LoadWarehouses()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            WarehouseID,
                            WarehouseName
                        FROM dbo.Warehouses
                        WHERE IsActive = 1
                        ORDER BY WarehouseName";

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

                cmbWarehouse.DataSource = dt;
                cmbWarehouse.DisplayMember = "WarehouseName";
                cmbWarehouse.ValueMember = "WarehouseID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Warehouse Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD STOCK
        // =========================================================

        private void LoadStock()
        {
            try
            {
                lvStock.Items.Clear();

                decimal totalQty = 0;
                decimal totalStockValue = 0;
                decimal totalPotentialSales = 0;
                decimal totalPotentialProfit = 0;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_ProductStock_Get",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        // -------------------------------------------------
                        // PRODUCT ID
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@ProductID",
                            SqlDbType.Int).Value =
                            DBNull.Value;

                        // -------------------------------------------------
                        // WAREHOUSE
                        // -------------------------------------------------

                        if (cmbWarehouse.SelectedValue != null &&
                            int.TryParse(
                                cmbWarehouse.SelectedValue.ToString(),
                                out int warehouseID))
                        {
                            cmd.Parameters.Add(
                                "@WarehouseID",
                                SqlDbType.Int).Value =
                                warehouseID;
                        }
                        else
                        {
                            cmd.Parameters.Add(
                                "@WarehouseID",
                                SqlDbType.Int).Value =
                                DBNull.Value;
                        }

                        // -------------------------------------------------
                        // SEARCH
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@SearchText",
                            SqlDbType.NVarChar,
                            100).Value =
                            string.IsNullOrWhiteSpace(
                                txtSearch.Text)
                                ? (object)DBNull.Value
                                : txtSearch.Text.Trim();

                        // -------------------------------------------------
                        // LOW STOCK
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@OnlyLowStock",
                            SqlDbType.Bit).Value =
                            chkLowStock.Checked;

                        // -------------------------------------------------
                        // OUT OF STOCK
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@OnlyOutOfStock",
                            SqlDbType.Bit).Value =
                            chkOutOfStock.Checked;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productID =
                                    Convert.ToInt32(
                                        reader["ProductID"]);

                                decimal currentQty =
                                    reader["CurrentQty"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["CurrentQty"]);

                                decimal lastCost =
                                    reader["LastUnitCost"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["LastUnitCost"]);

                                decimal stockValue =
                                    reader["StockValue"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["StockValue"]);

                                decimal salePrice =
                                    reader["SalePrice"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["SalePrice"]);

                                decimal potentialSales =
                                    reader["PotentialSalesValue"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PotentialSalesValue"]);

                                decimal potentialProfit =
                                    reader["PotentialGrossProfit"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["PotentialGrossProfit"]);

                                decimal reorderLevel =
                                    reader["ReorderLevel"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["ReorderLevel"]);

                                ListViewItem item =
                                    new ListViewItem(
                                        productID.ToString());

                                item.SubItems.Add(
                                    reader["ProductCode"]
                                    .ToString());

                                item.SubItems.Add(
                                    reader["ProductName"]
                                    .ToString());

                                item.SubItems.Add(
                                    reader["UnitName"] == DBNull.Value
                                        ? ""
                                        : reader["UnitName"].ToString());

                                item.SubItems.Add(
                                    reader["WarehouseName"] ==
                                    DBNull.Value
                                        ? ""
                                        : reader["WarehouseName"]
                                            .ToString());

                                item.SubItems.Add(
                                    currentQty.ToString("N3"));

                                item.SubItems.Add(
                                    lastCost.ToString("N4"));

                                item.SubItems.Add(
                                    stockValue.ToString("N2"));

                                item.SubItems.Add(
                                    salePrice.ToString("N2"));

                                item.SubItems.Add(
                                    potentialSales.ToString("N2"));

                                item.SubItems.Add(
                                    potentialProfit.ToString("N2"));

                                item.SubItems.Add(
                                    reorderLevel.ToString("N3"));

                                item.SubItems.Add(
                                    reader["StockStatus"]
                                    .ToString());

                                lvStock.Items.Add(item);

                                totalQty += currentQty;
                                totalStockValue += stockValue;
                                totalPotentialSales +=
                                    potentialSales;
                                totalPotentialProfit +=
                                    potentialProfit;
                            }
                        }
                    }
                }

                lblCurrentQty.Text =
                    totalQty.ToString("N3");

                lblStockValue.Text =
                    totalStockValue.ToString("N2");

                lblPotentialSales.Text =
                    totalPotentialSales.ToString("N2");

                lblPotentialProfit.Text =
                    totalPotentialProfit.ToString("N2");
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
                    "Stock Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // DECIMAL HELPER
        // =========================================================

        //private decimal GetDecimal(
        //    SqlDataReader reader,
        //    string columnName)
        //{
        //    if (reader[columnName] == DBNull.Value)
        //        return 0;

        //    return Convert.ToDecimal(
        //        reader[columnName]);
        //}

        // =========================================================
        // SEARCH
        // =========================================================

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadStock();
        }

        // =========================================================
        // WAREHOUSE CHANGE
        // =========================================================

        private void cmbWarehouse_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbWarehouse.SelectedValue == null)
                return;

            if (cmbWarehouse.SelectedValue is DataRowView)
                return;

            LoadStock();
        }

        // =========================================================
        // LOW STOCK
        // =========================================================

        private void chkLowStock_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (chkLowStock.Checked)
                chkOutOfStock.Checked = false;

            LoadStock();
        }

        // =========================================================
        // OUT OF STOCK
        // =========================================================

        private void chkOutOfStock_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (chkOutOfStock.Checked)
                chkLowStock.Checked = false;

            LoadStock();
        }

        // =========================================================
        // REFRESH
        // =========================================================

        private void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            txtSearch.Clear();

            chkLowStock.Checked = false;
            chkOutOfStock.Checked = false;

            LoadStock();
        }

        // =========================================================
        // CLOSE
        // =========================================================

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}
