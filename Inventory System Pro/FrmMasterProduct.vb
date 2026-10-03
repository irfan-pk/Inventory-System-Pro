Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration

Namespace Inventory_System_Pro
    Public Partial Class FrmProductMaster
        Inherits Form
        Private CurrentProductID As Integer = 0
        Private IsEditMode As Boolean = False

        Private ReadOnly ConnString As String = ConfigurationManager.ConnectionStrings("InventoryConnection").ConnectionString

        Public Sub New()
            InitializeComponent()
        End Sub

        ' =========================================================
        ' FORM LOAD
        ' =========================================================

        Private Sub FrmProductMaster_Load(sender As Object, e As EventArgs)
            SetupListView()

            LoadCategories()
            LoadUnits()
            LoadProducts()

            SetNewMode()
        End Sub

        ' =========================================================
        ' LISTVIEW SETUP
        ' =========================================================

        Private Sub SetupListView()
            lvProducts.View = View.Details
            lvProducts.FullRowSelect = True
            lvProducts.GridLines = True
            lvProducts.MultiSelect = False

            lvProducts.Columns.Clear()

            lvProducts.Columns.Add("ProductID", 0)
            lvProducts.Columns.Add("Code", 100)
            lvProducts.Columns.Add("Barcode", 110)
            lvProducts.Columns.Add("Product Name", 180)
            lvProducts.Columns.Add("Category", 100)
            lvProducts.Columns.Add("Unit", 80)
            lvProducts.Columns.Add("Purchase", 90)
            lvProducts.Columns.Add("Sale", 90)
            lvProducts.Columns.Add("Reorder", 80)
            lvProducts.Columns.Add("Active", 60)
        End Sub

        ' =========================================================
        ' LOAD CATEGORIES
        ' =========================================================

        Private Sub LoadCategories()
            Try
                Using cn As SqlConnection = New SqlConnection(ConnString)
                    Dim sql = "
                        SELECT
                            CategoryID,
                            CategoryName
                        FROM dbo.Categories
                        WHERE IsActive = 1
                        ORDER BY CategoryName"

                    Using cmd As SqlCommand = New SqlCommand(sql, cn)
                        Dim dt As DataTable = New DataTable()

                        Using da As SqlDataAdapter = New SqlDataAdapter(cmd)
                            da.Fill(dt)
                        End Using

                        cmbCategory.DataSource = dt
                        cmbCategory.DisplayMember = "CategoryName"
                        cmbCategory.ValueMember = "CategoryID"
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================
        ' LOAD UNITS
        ' =========================================================

        Private Sub LoadUnits()
            Try
                Using cn As SqlConnection = New SqlConnection(ConnString)
                    Dim sql = "
                        SELECT
                            UnitID,
                            UnitName,
                            ShortName
                        FROM dbo.Units
                        WHERE IsActive = 1
                        ORDER BY UnitName"

                    Using cmd As SqlCommand = New SqlCommand(sql, cn)
                        Dim dt As DataTable = New DataTable()

                        Using da As SqlDataAdapter = New SqlDataAdapter(cmd)
                            da.Fill(dt)
                        End Using

                        cmbUnit.DataSource = dt
                        cmbUnit.DisplayMember = "UnitName"
                        cmbUnit.ValueMember = "UnitID"
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Unit Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================
        ' LOAD PRODUCTS
        ' =========================================================

        Private Sub LoadProducts(Optional searchText As String = "")
            Try
                lvProducts.Items.Clear()

                Using cn As SqlConnection = New SqlConnection(ConnString)
                    Using cmd As SqlCommand = New SqlCommand("dbo.sp_Product_Get", cn)
                        cmd.CommandType = CommandType.StoredProcedure

                        If String.IsNullOrWhiteSpace(searchText) Then
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = DBNull.Value
                        Else
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = searchText.Trim()
                        End If

                        cn.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim item As ListViewItem = New ListViewItem(reader("ProductID").ToString())

                                item.SubItems.Add(reader("ProductCode").ToString())

                                item.SubItems.Add(If(reader("Barcode") Is DBNull.Value, "", reader("Barcode").ToString()))

                                item.SubItems.Add(reader("ProductName").ToString())

                                item.SubItems.Add(reader("CategoryName").ToString())

                                item.SubItems.Add(reader("ShortName").ToString())

                                item.SubItems.Add(Convert.ToDecimal(reader("PurchasePrice")).ToString("N2"))

                                item.SubItems.Add(Convert.ToDecimal(reader("SalePrice")).ToString("N2"))

                                item.SubItems.Add(Convert.ToDecimal(reader("ReorderLevel")).ToString("N3"))

                                item.SubItems.Add(If(Convert.ToBoolean(reader("IsActive")), "Yes", "No"))

                                lvProducts.Items.Add(item)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Product Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================
        ' NEW PRODUCT
        ' =========================================================

        Private Sub btnNew_Click(sender As Object, e As EventArgs)
            SetNewMode()
        End Sub

        Private Sub SetNewMode()
            CurrentProductID = 0
            IsEditMode = False

            txtProductCode.Clear()
            txtBarcode.Clear()
            txtProductName.Clear()

            txtPurchasePrice.Text = "0.00"
            txtSalePrice.Text = "0.00"
            txtReorderLevel.Text = "0.000"

            If cmbCategory.Items.Count > 0 Then cmbCategory.SelectedIndex = 0

            If cmbUnit.Items.Count > 0 Then cmbUnit.SelectedIndex = 0

            chkIsActive.Checked = True

            lvProducts.SelectedItems.Clear()

            txtProductCode.Focus()
        End Sub

        ' =========================================================
        ' SAVE / UPDATE
        ' =========================================================

        Private Sub btnSave_Click(sender As Object, e As EventArgs)
            If Not ValidateProduct() Then Return

            Try
                Using cn As SqlConnection = New SqlConnection(ConnString)
                    Using cmd As SqlCommand = New SqlCommand("dbo.sp_Product_Save", cn)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = If(IsEditMode, CurrentProductID, DBNull.Value)

                        cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 50).Value = txtProductCode.Text.Trim()

                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(txtBarcode.Text), DBNull.Value, txtBarcode.Text.Trim())

                        cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 200).Value = txtProductName.Text.Trim()

                        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = Convert.ToInt32(cmbCategory.SelectedValue)

                        cmd.Parameters.Add("@UnitID", SqlDbType.Int).Value = Convert.ToInt32(cmbUnit.SelectedValue)

                        Dim pPurchase = cmd.Parameters.Add("@PurchasePrice", SqlDbType.Decimal)

                        pPurchase.Precision = 18
                        pPurchase.Scale = 4
                        pPurchase.Value = Convert.ToDecimal(txtPurchasePrice.Text)

                        Dim pSale = cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal)

                        pSale.Precision = 18
                        pSale.Scale = 4
                        pSale.Value = Convert.ToDecimal(txtSalePrice.Text)

                        Dim pReorder = cmd.Parameters.Add("@ReorderLevel", SqlDbType.Decimal)

                        pReorder.Precision = 18
                        pReorder.Scale = 3
                        pReorder.Value = Convert.ToDecimal(txtReorderLevel.Text)

                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = chkIsActive.Checked

                        cn.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                CurrentProductID = Convert.ToInt32(reader("ProductID"))
                            End If
                        End Using
                    End Using
                End Using

                MessageBox.Show(If(IsEditMode, "Product updated successfully.", "Product saved successfully."), "Product Master", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadProducts()

                SetNewMode()
            Catch ex As SqlException
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================
        ' VALIDATION
        ' =========================================================

        Private Function ValidateProduct() As Boolean
            If String.IsNullOrWhiteSpace(txtProductCode.Text) Then
                MessageBox.Show("Product Code is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtProductCode.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtProductName.Text) Then
                MessageBox.Show("Product Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtProductName.Focus()
                Return False
            End If

            Dim purchasePrice As Decimal
            Dim salePrice As Decimal
            Dim reorderLevel As Decimal

            If Not Decimal.TryParse(txtPurchasePrice.Text, purchasePrice) Then
                MessageBox.Show("Enter a valid Purchase Price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtPurchasePrice.Focus()
                Return False
            End If

            If Not Decimal.TryParse(txtSalePrice.Text, salePrice) Then
                MessageBox.Show("Enter a valid Sale Price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtSalePrice.Focus()
                Return False
            End If

            If Not Decimal.TryParse(txtReorderLevel.Text, reorderLevel) Then
                MessageBox.Show("Enter a valid Reorder Level.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtReorderLevel.Focus()
                Return False
            End If

            If purchasePrice < 0 Then
                MessageBox.Show("Purchase Price cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtPurchasePrice.Focus()
                Return False
            End If

            If salePrice < 0 Then
                MessageBox.Show("Sale Price cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtSalePrice.Focus()
                Return False
            End If

            If reorderLevel < 0 Then
                MessageBox.Show("Reorder Level cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                txtReorderLevel.Focus()
                Return False
            End If

            If cmbCategory.SelectedValue Is Nothing OrElse Not Integer.TryParse(cmbCategory.SelectedValue.ToString(), __) Then
                MessageBox.Show("Select a Category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                cmbCategory.Focus()
                Return False
            End If

            If cmbUnit.SelectedValue Is Nothing OrElse Not Integer.TryParse(cmbUnit.SelectedValue.ToString(), __) Then
                MessageBox.Show("Select a Unit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                cmbUnit.Focus()
                Return False
            End If

            Return True
        End Function

        ' =========================================================
        ' SELECT PRODUCT FROM LIST
        ' =========================================================

        Private Sub lvProducts_SelectedIndexChanged(sender As Object, e As EventArgs)
            If lvProducts.SelectedItems.Count = 0 Then Return

            Dim item = lvProducts.SelectedItems(0)

            CurrentProductID = Convert.ToInt32(item.SubItems(0).Text)

            LoadProduct(CurrentProductID)
        End Sub

        ' =========================================================
        ' LOAD SINGLE PRODUCT
        ' =========================================================

        Private Sub LoadProduct(productID As Integer)
            Try
                Using cn As SqlConnection = New SqlConnection(ConnString)
                    Using cmd As SqlCommand = New SqlCommand("dbo.sp_Product_Get", cn)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID

                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = DBNull.Value

                        cn.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                IsEditMode = True

                                CurrentProductID = Convert.ToInt32(reader("ProductID"))

                                txtProductCode.Text = reader("ProductCode").ToString()

                                txtBarcode.Text = If(reader("Barcode") Is DBNull.Value, "", reader("Barcode").ToString())

                                txtProductName.Text = reader("ProductName").ToString()

                                cmbCategory.SelectedValue = Convert.ToInt32(reader("CategoryID"))

                                cmbUnit.SelectedValue = Convert.ToInt32(reader("UnitID"))

                                txtPurchasePrice.Text = Convert.ToDecimal(reader("PurchasePrice")).ToString("0.0000")

                                txtSalePrice.Text = Convert.ToDecimal(reader("SalePrice")).ToString("0.0000")

                                txtReorderLevel.Text = Convert.ToDecimal(reader("ReorderLevel")).ToString("0.000")

                                chkIsActive.Checked = Convert.ToBoolean(reader("IsActive"))
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Product Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================
        ' SEARCH
        ' =========================================================

        Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
            LoadProducts(txtSearch.Text.Trim())
        End Sub

        ' =========================================================
        ' CANCEL
        ' =========================================================

        Private Sub btnCancel_Click(sender As Object, e As EventArgs)
            SetNewMode()
        End Sub

        Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
            If CurrentProductID <= 0 Then
                MessageBox.Show("Please select a product to update.", "Product Master", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Return
            End If

            If Not ValidateProduct() Then Return

            IsEditMode = True

            btnSave_Click(sender, e)
            'SaveProduct(true);
        End Sub

        'private void SaveProduct(bool isUpdate)
        '{
        '    try
        '    {
        '        using (SqlConnection cn = new SqlConnection(ConnString))
        '        {
        '            using (SqlCommand cmd = new SqlCommand(
        '                "dbo.sp_Product_Save", cn))
        '            {
        '                cmd.CommandType = CommandType.StoredProcedure;

        '                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value =
        '                    isUpdate
        '                        ? CurrentProductID
        '                        : (object)DBNull.Value;

        '                cmd.Parameters.Add(
        '                    "@ProductCode",
        '                    SqlDbType.NVarChar,
        '                    50).Value =
        '                    txtProductCode.Text.Trim();

        '                cmd.Parameters.Add(
        '                    "@Barcode",
        '                    SqlDbType.NVarChar,
        '                    50).Value =
        '                    string.IsNullOrWhiteSpace(txtBarcode.Text)
        '                        ? (object)DBNull.Value
        '                        : txtBarcode.Text.Trim();

        '                cmd.Parameters.Add(
        '                    "@ProductName",
        '                    SqlDbType.NVarChar,
        '                    200).Value =
        '                    txtProductName.Text.Trim();

        '                cmd.Parameters.Add(
        '                    "@CategoryID",
        '                    SqlDbType.Int).Value =
        '                    Convert.ToInt32(cmbCategory.SelectedValue);

        '                cmd.Parameters.Add(
        '                    "@UnitID",
        '                    SqlDbType.Int).Value =
        '                    Convert.ToInt32(cmbUnit.SelectedValue);

        '                SqlParameter pPurchase =
        '                    cmd.Parameters.Add(
        '                        "@PurchasePrice",
        '                        SqlDbType.Decimal);

        '                pPurchase.Precision = 18;
        '                pPurchase.Scale = 4;
        '                pPurchase.Value =
        '                    Convert.ToDecimal(txtPurchasePrice.Text);

        '                SqlParameter pSale =
        '                    cmd.Parameters.Add(
        '                        "@SalePrice",
        '                        SqlDbType.Decimal);

        '                pSale.Precision = 18;
        '                pSale.Scale = 4;
        '                pSale.Value =
        '                    Convert.ToDecimal(txtSalePrice.Text);

        '                SqlParameter pReorder =
        '                    cmd.Parameters.Add(
        '                        "@ReorderLevel",
        '                        SqlDbType.Decimal);

        '                pReorder.Precision = 18;
        '                pReorder.Scale = 3;
        '                pReorder.Value =
        '                    Convert.ToDecimal(txtReorderLevel.Text);

        '                cmd.Parameters.Add(
        '                    "@IsActive",
        '                    SqlDbType.Bit).Value =
        '                    chkIsActive.Checked;

        '                cn.Open();

        '                using (SqlDataReader reader =
        '                       cmd.ExecuteReader())
        '                {
        '                    if (reader.Read())
        '                    {
        '                        CurrentProductID =
        '                            Convert.ToInt32(
        '                                reader["ProductID"]);
        '                    }
        '                }
        '            }
        '        }

        '        MessageBox.Show(
        '            isUpdate
        '                ? "Product updated successfully."
        '                : "Product saved successfully.",
        '            "Product Master",
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Information);

        '        LoadProducts();

        '        SetNewMode();
        '    }
        '    catch (SqlException ex)
        '    {
        '        MessageBox.Show(
        '            ex.Message,
        '            "Database Error",
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Error);
        '    }
        '    catch (Exception ex)
        '    {
        '        MessageBox.Show(
        '            ex.Message,
        '            "Error",
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Error);
        '    }
        '}
    End Class
End Namespace
