using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Inventory_System_Pro
{
    public partial class FrmPurchaseSearch : Form
    {
        private readonly string ConnString =
            ConfigurationManager
                .ConnectionStrings["InventoryConnection"]
                .ConnectionString;
        public int SelectedPurchaseID { get; private set; }
        public string SelectedPurchaseNumber { get; private set; }
        public int SelectedSupplierID { get; private set; }
        public int SelectedWarehouseID { get; private set; }

        public FrmPurchaseSearch()
        {
            InitializeComponent();

            dgvPurchases.AutoGenerateColumns = false;
            dgvPurchases.AllowUserToAddRows = false;
            dgvPurchases.AllowUserToDeleteRows = false;
            dgvPurchases.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPurchases.MultiSelect = false;
        }

        private void FrmPurchaseSearch_Load(
            object sender,
            EventArgs e)
        {
            LoadPurchases();
        }

        private void txtSearch_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadPurchases(txtSearch.Text.Trim());
        }

        private void LoadPurchases(
            string searchText = "")
        {
            try
            {
                dgvPurchases.Rows.Clear();

                using (SqlConnection cn =
                       new SqlConnection(ConnString))
                using (SqlCommand cmd =
                       new SqlCommand(@"
                                    SELECT
                                    p.PurchaseID,
                                    p.PurchaseNumber,
                                    p.PurchaseDate,
                                    p.SupplierID,
                                    s.SupplierName,
                                    p.WarehouseID,
                                    w.WarehouseName,
                                    p.NetAmount,
                                    p.Status
                                    FROM dbo.Purchases p
                                    INNER JOIN dbo.Suppliers s
                                        ON s.SupplierID = p.SupplierID
                                    INNER JOIN dbo.Warehouses w
                                        ON w.WarehouseID = p.WarehouseID
                                    WHERE
                                    p.Status IN ('POSTED', 'RECEIVED')
                                    AND
                                    (
                                        @Search = ''
                                        OR p.PurchaseNumber LIKE
                                           '%' + @Search + '%'
                                        OR s.SupplierName LIKE
                                           '%' + @Search + '%'
                                    )
                                    ORDER BY
                                    p.PurchaseID DESC;",
                                cn))
                {
                    cmd.Parameters.Add(
                        "@Search",
                        SqlDbType.NVarChar,
                        100).Value =
                        searchText;

                    cn.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex =
                                dgvPurchases.Rows.Add();

                            DataGridViewRow row =
                                dgvPurchases.Rows[rowIndex];

                            row.Cells["PurchaseID"].Value =
                                reader["PurchaseID"];

                            row.Cells["PurchaseNumber"].Value =
                                reader["PurchaseNumber"];

                            row.Cells["PurchaseDate"].Value =
                                Convert.ToDateTime(
                                    reader["PurchaseDate"])
                                    .ToString("dd-MMM-yyyy");

                            row.Cells["SupplierID"].Value =
                                reader["SupplierID"];

                            row.Cells["SupplierName"].Value =
                                reader["SupplierName"];

                            row.Cells["WarehouseID"].Value =
                                reader["WarehouseID"];

                            row.Cells["WarehouseName"].Value =
                                reader["WarehouseName"];

                            row.Cells["NetAmount"].Value =
                                Convert.ToDecimal(
                                    reader["NetAmount"])
                                    .ToString("N2");

                            row.Cells["Status"].Value =
                                reader["Status"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load purchases.\n\n" +
                    ex.Message,
                    "Purchase Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSelect_Click(
            object sender,
            EventArgs e)
        {
            if (dgvPurchases.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a purchase.",
                    "Purchase Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SelectedPurchaseID =
                Convert.ToInt32(
                    dgvPurchases.CurrentRow
                        .Cells["PurchaseID"].Value);

            SelectedPurchaseNumber =
                Convert.ToString(
                    dgvPurchases.CurrentRow
                        .Cells["PurchaseNumber"].Value);

            SelectedSupplierID =
                Convert.ToInt32(
                    dgvPurchases.CurrentRow
                        .Cells["SupplierID"].Value);

            SelectedWarehouseID =
                Convert.ToInt32(
                    dgvPurchases.CurrentRow
                        .Cells["WarehouseID"].Value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dgvPurchases_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            btnSelect_Click(sender, e);
        }
    }
}