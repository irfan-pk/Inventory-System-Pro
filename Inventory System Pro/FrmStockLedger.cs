using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Inventory_System_Pro
{
    public partial class FrmStockLedger : Form
    {
        private readonly string ConnString =
            ConfigurationManager.ConnectionStrings[
                "InventoryConnection"].ConnectionString;

        public FrmStockLedger()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void FrmStockLedger_Load(object sender, EventArgs e)
        {
            SetupListView();
            SetupTransactionTypes();

            LoadProducts();
            LoadWarehouses();

            dtpFromDate.Value =
                DateTime.Today.AddDays(-30);

            dtpToDate.Value =
                DateTime.Today;

            LoadLedger();
        }

        // =========================================================
        // LISTVIEW
        // =========================================================

        private void SetupListView()
        {
            lvLedger.View = View.Details;
            lvLedger.FullRowSelect = true;
            lvLedger.GridLines = true;
            lvLedger.MultiSelect = false;

            lvLedger.Columns.Clear();

            lvLedger.Columns.Add("Date", 135);
            lvLedger.Columns.Add("Transaction", 150);
            lvLedger.Columns.Add("Reference No", 210);
            lvLedger.Columns.Add("Product Code", 100);
            lvLedger.Columns.Add("Product Name", 180);
            lvLedger.Columns.Add("Warehouse", 120);
            lvLedger.Columns.Add("Qty In", 85);
            lvLedger.Columns.Add("Qty Out", 85);
            lvLedger.Columns.Add("Unit Cost", 90);
            lvLedger.Columns.Add("Balance Qty", 100);
            lvLedger.Columns.Add("Balance Value", 110);
            lvLedger.Columns.Add("Average Cost", 110);
            lvLedger.Columns.Add("COGS", 100);
            lvLedger.Columns.Add("Remarks", 250);
        }

        // =========================================================
        // TRANSACTION TYPES
        // =========================================================

        private void SetupTransactionTypes()
        {
            cmbTranType.Items.Clear();

            cmbTranType.Items.Add("ALL");
            cmbTranType.Items.Add("PURCHASE");
            cmbTranType.Items.Add("PURCHASE_RETURN");
            cmbTranType.Items.Add("PURCHASE_RETURN_CANCEL");
            cmbTranType.Items.Add("SALE");
            cmbTranType.Items.Add("SALE_RETURN");
            cmbTranType.Items.Add("SALE_RETURN_CANCEL");

            cmbTranType.SelectedIndex = 0;
        }

        // =========================================================
        // PRODUCTS
        // =========================================================

        private void LoadProducts()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            ProductID,
                            ProductCode,
                            ProductName
                        FROM dbo.Products
                        WHERE IsActive = 1
                        ORDER BY ProductCode";

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

                DataRow allRow = dt.NewRow();

                allRow["ProductID"] = 0;
                allRow["ProductCode"] = "ALL";
                allRow["ProductName"] = "All Products";

                dt.Rows.InsertAt(allRow, 0);

                cmbProduct.DataSource = dt;
                cmbProduct.DisplayMember = "ProductCode";
                cmbProduct.ValueMember = "ProductID";
                cmbProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Product Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

                DataRow allRow = dt.NewRow();

                allRow["WarehouseID"] = 0;
                allRow["WarehouseName"] = "All Warehouses";

                dt.Rows.InsertAt(allRow, 0);

                cmbWarehouse.DataSource = dt;
                cmbWarehouse.DisplayMember =
                    "WarehouseName";
                cmbWarehouse.ValueMember =
                    "WarehouseID";

                cmbWarehouse.SelectedIndex = 0;
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
        // LOAD LEDGER
        // =========================================================

        private void LoadLedger()
        {
            try
            {
                lvLedger.Items.Clear();

                decimal totalQtyIn = 0;
                decimal totalQtyOut = 0;
                decimal totalCOGS = 0;

                decimal closingQty = 0;
                decimal closingValue = 0;
                decimal averageCost = 0;

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_StockLedger_Get",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        // -------------------------------------------------
                        // PRODUCT
                        // -------------------------------------------------

                        int productID = 0;

                        if (cmbProduct.SelectedValue != null &&
                            !(cmbProduct.SelectedValue
                                is DataRowView))
                        {
                            int.TryParse(
                                cmbProduct.SelectedValue.ToString(),
                                out productID);
                        }

                        cmd.Parameters.Add(
                            "@ProductID",
                            SqlDbType.Int).Value =
                            productID == 0
                                ? (object)DBNull.Value
                                : productID;

                        // -------------------------------------------------
                        // WAREHOUSE
                        // -------------------------------------------------

                        int warehouseID = 0;

                        if (cmbWarehouse.SelectedValue != null &&
                            !(cmbWarehouse.SelectedValue
                                is DataRowView))
                        {
                            int.TryParse(
                                cmbWarehouse.SelectedValue.ToString(),
                                out warehouseID);
                        }

                        cmd.Parameters.Add(
                            "@WarehouseID",
                            SqlDbType.Int).Value =
                            warehouseID == 0
                                ? (object)DBNull.Value
                                : warehouseID;

                        // -------------------------------------------------
                        // FROM DATE
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@FromDate",
                            SqlDbType.Date).Value =
                            dtpFromDate.Value.Date;

                        // -------------------------------------------------
                        // TO DATE
                        // -------------------------------------------------

                        cmd.Parameters.Add(
                            "@ToDate",
                            SqlDbType.Date).Value =
                            dtpToDate.Value.Date;

                        // -------------------------------------------------
                        // TRANSACTION TYPE
                        // -------------------------------------------------

                        string tranType =
                            cmbTranType.SelectedItem
                                ?.ToString();

                        cmd.Parameters.Add(
                            "@TranType",
                            SqlDbType.VarChar,
                            30).Value =
                            string.IsNullOrWhiteSpace(tranType) ||
                            tranType == "ALL"
                                ? (object)DBNull.Value
                                : tranType;

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

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal qtyIn =
                                    reader["QtyIn"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["QtyIn"]);

                                decimal qtyOut =
                                    reader["QtyOut"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["QtyOut"]);

                                decimal unitCost =
                                    reader["UnitCost"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["UnitCost"]);

                                decimal balanceQty =
                                    reader["BalanceQty"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["BalanceQty"]);

                                decimal balanceValue =
                                    reader["BalanceValue"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["BalanceValue"]);

                                decimal avgCost =
                                    reader["AverageCost"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["AverageCost"]);

                                decimal cogs =
                                    reader["COGSValue"] ==
                                    DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(
                                            reader["COGSValue"]);

                                DateTime tranDate =
                                    Convert.ToDateTime(
                                        reader["TranDate"]);

                                ListViewItem item =
                                    new ListViewItem(
                                        tranDate.ToString(
                                            "yyyy-MM-dd HH:mm"));

                                item.SubItems.Add(
                                    reader["TranType"]
                                        .ToString());

                                item.SubItems.Add(
                                    reader["ReferenceNo"] ==
                                    DBNull.Value
                                        ? ""
                                        : reader["ReferenceNo"]
                                            .ToString());

                                item.SubItems.Add(
                                    reader["ProductCode"]
                                        .ToString());

                                item.SubItems.Add(
                                    reader["ProductName"]
                                        .ToString());

                                item.SubItems.Add(
                                    reader["WarehouseName"] ==
                                    DBNull.Value
                                        ? ""
                                        : reader["WarehouseName"]
                                            .ToString());

                                item.SubItems.Add(
                                    qtyIn.ToString("N3"));

                                item.SubItems.Add(
                                    qtyOut.ToString("N3"));

                                item.SubItems.Add(
                                    unitCost.ToString("N4"));

                                item.SubItems.Add(
                                    balanceQty.ToString("N3"));

                                item.SubItems.Add(
                                    balanceValue.ToString("N2"));

                                item.SubItems.Add(
                                    avgCost.ToString("N6"));

                                item.SubItems.Add(
                                    cogs.ToString("N2"));

                                item.SubItems.Add(
                                    reader["Remarks"] ==
                                    DBNull.Value
                                        ? ""
                                        : reader["Remarks"]
                                            .ToString());

                                lvLedger.Items.Add(item);

                                totalQtyIn += qtyIn;
                                totalQtyOut += qtyOut;
                                totalCOGS += cogs;

                                // Last row becomes closing position.
                                closingQty = balanceQty;
                                closingValue =
                                    balanceValue;
                                averageCost = avgCost;
                            }
                        }
                    }
                }

                // =====================================================
                // SUMMARY
                // =====================================================

                lblTotalQtyIn.Text =
                    totalQtyIn.ToString("N3");

                lblTotalQtyOut.Text =
                    totalQtyOut.ToString("N3");

                lblTotalCOGS.Text =
                    totalCOGS.ToString("N2");

                lblClosingQty.Text =
                    closingQty.ToString("N3");

                lblClosingValue.Text =
                    closingValue.ToString("N2");

                lblAverageCost.Text =
                    averageCost.ToString("N6");
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
                    "Ledger Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // SEARCH
        // =========================================================

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadLedger();
        }

        // =========================================================
        // PRODUCT CHANGE
        // =========================================================

        private void cmbProduct_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbProduct.SelectedValue == null)
                return;

            if (cmbProduct.SelectedValue
                is DataRowView)
                return;

            LoadLedger();
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

            if (cmbWarehouse.SelectedValue
                is DataRowView)
                return;

            LoadLedger();
        }

        // =========================================================
        // TRANSACTION TYPE
        // =========================================================

        private void cmbTranType_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbTranType.SelectedIndex < 0)
                return;

            LoadLedger();
        }

        // =========================================================
        // DATE CHANGE
        // =========================================================

        private void dtpFromDate_ValueChanged(
            object sender,
            EventArgs e)
        {
            if (dtpFromDate.Value.Date >
                dtpToDate.Value.Date)
            {
                dtpToDate.Value =
                    dtpFromDate.Value;
            }

            LoadLedger();
        }

        private void dtpToDate_ValueChanged(
            object sender,
            EventArgs e)
        {
            if (dtpToDate.Value.Date <
                dtpFromDate.Value.Date)
            {
                dtpFromDate.Value =
                    dtpToDate.Value;
            }

            LoadLedger();
        }

        // =========================================================
        // SEARCH BUTTON
        // =========================================================

        private void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            LoadLedger();
        }

        // =========================================================
        // REFRESH
        // =========================================================

        private void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            txtSearch.Clear();

            cmbProduct.SelectedIndex = 0;
            cmbWarehouse.SelectedIndex = 0;
            cmbTranType.SelectedIndex = 0;

            dtpFromDate.Value =
                DateTime.Today.AddDays(-30);

            dtpToDate.Value =
                DateTime.Today;

            LoadLedger();
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