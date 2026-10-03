using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Inventory_System_Pro.Program;

namespace Inventory_System_Pro
{
    public partial class FrmProductMaster : Form
    {
        private int CurrentProductID = 0;
        private bool IsEditMode = false;

        private readonly string ConnString =
                ConfigurationManager.ConnectionStrings["InventoryConnection"].ConnectionString;

        public FrmProductMaster()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void FrmProductMaster_Load(object sender, EventArgs e)
        {
            SetupListView();

            LoadCategories();
            LoadUnits();
            LoadProducts();

            SetNewMode();
        }

        // =========================================================
        // LISTVIEW SETUP
        // =========================================================

        private void SetupListView()
        {
            lvProducts.View = View.Details;
            lvProducts.FullRowSelect = true;
            lvProducts.GridLines = true;
            lvProducts.MultiSelect = false;

            lvProducts.Columns.Clear();

            lvProducts.Columns.Add("ProductID", 0);
            lvProducts.Columns.Add("Code", 100);
            lvProducts.Columns.Add("Barcode", 110);
            lvProducts.Columns.Add("Product Name", 180);
            lvProducts.Columns.Add("Category", 100);
            lvProducts.Columns.Add("Unit", 80);
            lvProducts.Columns.Add("Purchase", 90);
            lvProducts.Columns.Add("Sale", 90);
            lvProducts.Columns.Add("Reorder", 80);
            lvProducts.Columns.Add("Active", 60);
        }

        // =========================================================
        // LOAD CATEGORIES
        // =========================================================

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            CategoryID,
                            CategoryName
                        FROM dbo.Categories
                        WHERE IsActive = 1
                        ORDER BY CategoryName";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        cmbCategory.DataSource = dt;
                        cmbCategory.DisplayMember = "CategoryName";
                        cmbCategory.ValueMember = "CategoryID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Category Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD UNITS
        // =========================================================

        private void LoadUnits()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnString))
                {
                    string sql = @"
                        SELECT
                            UnitID,
                            UnitName,
                            ShortName
                        FROM dbo.Units
                        WHERE IsActive = 1
                        ORDER BY UnitName";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da =
                               new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        cmbUnit.DataSource = dt;
                        cmbUnit.DisplayMember = "UnitName";
                        cmbUnit.ValueMember = "UnitID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unit Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD PRODUCTS
        // =========================================================

        private void LoadProducts(string searchText = "")
        {
            try
            {
                lvProducts.Items.Clear();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_Product_Get",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        if (string.IsNullOrWhiteSpace(searchText))
                        {
                            cmd.Parameters.Add(
                                "@Search",
                                SqlDbType.NVarChar,
                                200).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add(
                                "@Search",
                                SqlDbType.NVarChar,
                                200).Value =
                                searchText.Trim();
                        }

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item =
                                    new ListViewItem(
                                        reader["ProductID"]
                                        .ToString());

                                item.SubItems.Add(
                                    reader["ProductCode"]
                                    .ToString());

                                item.SubItems.Add(
                                    reader["Barcode"] == DBNull.Value
                                        ? ""
                                        : reader["Barcode"]
                                            .ToString());

                                item.SubItems.Add(
                                    reader["ProductName"]
                                    .ToString());

                                item.SubItems.Add(
                                    reader["CategoryName"]
                                    .ToString());

                                item.SubItems.Add(
                                    reader["ShortName"]
                                    .ToString());

                                item.SubItems.Add(
                                    Convert.ToDecimal(
                                        reader["PurchasePrice"])
                                    .ToString("N2"));

                                item.SubItems.Add(
                                    Convert.ToDecimal(
                                        reader["SalePrice"])
                                    .ToString("N2"));

                                item.SubItems.Add(
                                    Convert.ToDecimal(
                                        reader["ReorderLevel"])
                                    .ToString("N3"));

                                item.SubItems.Add(
                                    Convert.ToBoolean(
                                        reader["IsActive"])
                                        ? "Yes"
                                        : "No");

                                lvProducts.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Product Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NEW PRODUCT
        // =========================================================

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PRODUCT.NEW"))
            {
                MessageBox.Show("You do not have permission to create new products.");
                return;
            }
            SetNewMode();
        }

        private void SetNewMode()
        {
            CurrentProductID = 0;
            IsEditMode = false;

            txtProductCode.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();

            txtPurchasePrice.Text = "0.00";
            txtSalePrice.Text = "0.00";
            txtReorderLevel.Text = "0.000";

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            if (cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;

            chkIsActive.Checked = true;

            lvProducts.SelectedItems.Clear();

            txtProductCode.Focus();
        }

        // =========================================================
        // SAVE / UPDATE
        // =========================================================

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PRODUCT.SAVE"))
            {
                MessageBox.Show("You do not have permission to save products.");
                return;
            }
            if (!ValidateProduct())
                return;

            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_Product_Save",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@ProductID",
                            SqlDbType.Int).Value =
                            IsEditMode
                                ? CurrentProductID
                                : (object)DBNull.Value;

                        cmd.Parameters.Add(
                            "@ProductCode",
                            SqlDbType.NVarChar,
                            50).Value =
                            txtProductCode.Text.Trim();

                        cmd.Parameters.Add(
                            "@Barcode",
                            SqlDbType.NVarChar,
                            50).Value =
                            string.IsNullOrWhiteSpace(
                                txtBarcode.Text)
                                ? (object)DBNull.Value
                                : txtBarcode.Text.Trim();

                        cmd.Parameters.Add(
                            "@ProductName",
                            SqlDbType.NVarChar,
                            200).Value =
                            txtProductName.Text.Trim();

                        cmd.Parameters.Add(
                            "@CategoryID",
                            SqlDbType.Int).Value =
                            Convert.ToInt32(
                                cmbCategory.SelectedValue);

                        cmd.Parameters.Add(
                            "@UnitID",
                            SqlDbType.Int).Value =
                            Convert.ToInt32(
                                cmbUnit.SelectedValue);

                        SqlParameter pPurchase =
                            cmd.Parameters.Add(
                                "@PurchasePrice",
                                SqlDbType.Decimal);

                        pPurchase.Precision = 18;
                        pPurchase.Scale = 4;
                        pPurchase.Value =
                            Convert.ToDecimal(
                                txtPurchasePrice.Text);

                        SqlParameter pSale =
                            cmd.Parameters.Add(
                                "@SalePrice",
                                SqlDbType.Decimal);

                        pSale.Precision = 18;
                        pSale.Scale = 4;
                        pSale.Value =
                            Convert.ToDecimal(
                                txtSalePrice.Text);

                        SqlParameter pReorder =
                            cmd.Parameters.Add(
                                "@ReorderLevel",
                                SqlDbType.Decimal);

                        pReorder.Precision = 18;
                        pReorder.Scale = 3;
                        pReorder.Value =
                            Convert.ToDecimal(
                                txtReorderLevel.Text);

                        cmd.Parameters.Add(
                            "@IsActive",
                            SqlDbType.Bit).Value =
                            chkIsActive.Checked;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CurrentProductID =
                                    Convert.ToInt32(
                                        reader["ProductID"]);
                            }
                        }
                    }
                }

                MessageBox.Show(
                    IsEditMode
                        ? "Product updated successfully."
                        : "Product saved successfully.",
                    "Product Master",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();

                SetNewMode();
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
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // VALIDATION
        // =========================================================

        private bool ValidateProduct()
        {
            if (string.IsNullOrWhiteSpace(
                txtProductCode.Text))
            {
                MessageBox.Show(
                    "Product Code is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtProductName.Text))
            {
                MessageBox.Show(
                    "Product Name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductName.Focus();
                return false;
            }

            decimal purchasePrice;
            decimal salePrice;
            decimal reorderLevel;

            if (!decimal.TryParse(
                txtPurchasePrice.Text,
                out purchasePrice))
            {
                MessageBox.Show(
                    "Enter a valid Purchase Price.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPurchasePrice.Focus();
                return false;
            }

            if (!decimal.TryParse(
                txtSalePrice.Text,
                out salePrice))
            {
                MessageBox.Show(
                    "Enter a valid Sale Price.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSalePrice.Focus();
                return false;
            }

            if (!decimal.TryParse(
                txtReorderLevel.Text,
                out reorderLevel))
            {
                MessageBox.Show(
                    "Enter a valid Reorder Level.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReorderLevel.Focus();
                return false;
            }

            if (purchasePrice < 0)
            {
                MessageBox.Show(
                    "Purchase Price cannot be negative.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPurchasePrice.Focus();
                return false;
            }

            if (salePrice < 0)
            {
                MessageBox.Show(
                    "Sale Price cannot be negative.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSalePrice.Focus();
                return false;
            }

            if (reorderLevel < 0)
            {
                MessageBox.Show(
                    "Reorder Level cannot be negative.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReorderLevel.Focus();
                return false;
            }

            if (cmbCategory.SelectedValue == null ||
                !int.TryParse(
                    cmbCategory.SelectedValue.ToString(),
                    out _))
            {
                MessageBox.Show(
                    "Select a Category.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategory.Focus();
                return false;
            }

            if (cmbUnit.SelectedValue == null ||
                !int.TryParse(
                    cmbUnit.SelectedValue.ToString(),
                    out _))
            {
                MessageBox.Show(
                    "Select a Unit.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbUnit.Focus();
                return false;
            }

            return true;
        }

        // =========================================================
        // SELECT PRODUCT FROM LIST
        // =========================================================

        private void lvProducts_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (lvProducts.SelectedItems.Count == 0)
                return;

            ListViewItem item =
                lvProducts.SelectedItems[0];

            CurrentProductID =
                Convert.ToInt32(
                    item.SubItems[0].Text);

            LoadProduct(CurrentProductID);
        }

        // =========================================================
        // LOAD SINGLE PRODUCT
        // =========================================================

        private void LoadProduct(int productID)
        {
            try
            {
                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand(
                               "dbo.sp_Product_Get",
                               cn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@ProductID",
                            SqlDbType.Int).Value =
                            productID;

                        cmd.Parameters.Add(
                            "@Search",
                            SqlDbType.NVarChar,
                            200).Value =
                            DBNull.Value;

                        cn.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsEditMode = true;

                                CurrentProductID =
                                    Convert.ToInt32(
                                        reader["ProductID"]);

                                txtProductCode.Text =
                                    reader["ProductCode"]
                                    .ToString();

                                txtBarcode.Text =
                                    reader["Barcode"] ==
                                    DBNull.Value
                                        ? ""
                                        : reader["Barcode"]
                                            .ToString();

                                txtProductName.Text =
                                    reader["ProductName"]
                                    .ToString();

                                cmbCategory.SelectedValue =
                                    Convert.ToInt32(
                                        reader["CategoryID"]);

                                cmbUnit.SelectedValue =
                                    Convert.ToInt32(
                                        reader["UnitID"]);

                                txtPurchasePrice.Text =
                                    Convert.ToDecimal(
                                        reader["PurchasePrice"])
                                    .ToString("0.0000");

                                txtSalePrice.Text =
                                    Convert.ToDecimal(
                                        reader["SalePrice"])
                                    .ToString("0.0000");

                                txtReorderLevel.Text =
                                    Convert.ToDecimal(
                                        reader["ReorderLevel"])
                                    .ToString("0.000");

                                chkIsActive.Checked =
                                    Convert.ToBoolean(
                                        reader["IsActive"]);
                            }
                        }
                    }
                }
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
        // SEARCH
        // =========================================================

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadProducts(
                txtSearch.Text.Trim());
        }

        // =========================================================
        // CANCEL
        // =========================================================

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!AppSession.HasPermission("PRODUCT.UPDATE"))
            {
                MessageBox.Show("You do not have permission to update products.");
                return;
            }
            if (CurrentProductID <= 0)
            {
                MessageBox.Show(
                    "Please select a product to update.",
                    "Product Master",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateProduct())
                return;

            IsEditMode = true;

            btnSave_Click(sender,e);
        }
    }
}
