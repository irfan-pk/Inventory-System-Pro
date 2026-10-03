Namespace Inventory_System_Pro
    Partial Class FrmProductMaster
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <paramname="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            scMaster = New Windows.Forms.SplitContainer()
            label3 = New Windows.Forms.Label()
            txtProductName = New Windows.Forms.TextBox()
            button4 = New Windows.Forms.Button()
            btnUpdate = New Windows.Forms.Button()
            btnSave = New Windows.Forms.Button()
            btnNew = New Windows.Forms.Button()
            chkIsActive = New Windows.Forms.CheckBox()
            label10 = New Windows.Forms.Label()
            txtReorderLevel = New Windows.Forms.TextBox()
            label9 = New Windows.Forms.Label()
            txtSalePrice = New Windows.Forms.TextBox()
            label8 = New Windows.Forms.Label()
            txtPurchasePrice = New Windows.Forms.TextBox()
            label7 = New Windows.Forms.Label()
            cmbUnit = New Windows.Forms.ComboBox()
            label6 = New Windows.Forms.Label()
            cmbCategory = New Windows.Forms.ComboBox()
            label5 = New Windows.Forms.Label()
            label4 = New Windows.Forms.Label()
            txtBarcode = New Windows.Forms.TextBox()
            label2 = New Windows.Forms.Label()
            txtProductCode = New Windows.Forms.TextBox()
            label1 = New Windows.Forms.Label()
            splitContainer1 = New Windows.Forms.SplitContainer()
            txtSearch = New Windows.Forms.TextBox()
            label20 = New Windows.Forms.Label()
            lvProducts = New Windows.Forms.ListView()
            ProductId = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            ProductCode = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Barcode = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Productname = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Category = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Unit = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            PurchasePrice = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            SalePrice = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            ReorderLevel = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Active = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            CType(scMaster, ComponentModel.ISupportInitialize).BeginInit()
            scMaster.Panel1.SuspendLayout()
            scMaster.Panel2.SuspendLayout()
            scMaster.SuspendLayout()
            CType(splitContainer1, ComponentModel.ISupportInitialize).BeginInit()
            splitContainer1.Panel1.SuspendLayout()
            splitContainer1.Panel2.SuspendLayout()
            splitContainer1.SuspendLayout()
            SuspendLayout()
            ' 
            ' scMaster
            ' 
            scMaster.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            scMaster.Dock = Windows.Forms.DockStyle.Top
            scMaster.Location = New Drawing.Point(0, 0)
            scMaster.Name = "scMaster"
            scMaster.Orientation = Windows.Forms.Orientation.Horizontal
            ' 
            ' scMaster.Panel1
            ' 
            scMaster.Panel1.Controls.Add(label3)
            ' 
            ' scMaster.Panel2
            ' 
            scMaster.Panel2.Controls.Add(txtProductName)
            scMaster.Panel2.Controls.Add(button4)
            scMaster.Panel2.Controls.Add(btnUpdate)
            scMaster.Panel2.Controls.Add(btnSave)
            scMaster.Panel2.Controls.Add(btnNew)
            scMaster.Panel2.Controls.Add(chkIsActive)
            scMaster.Panel2.Controls.Add(label10)
            scMaster.Panel2.Controls.Add(txtReorderLevel)
            scMaster.Panel2.Controls.Add(label9)
            scMaster.Panel2.Controls.Add(txtSalePrice)
            scMaster.Panel2.Controls.Add(label8)
            scMaster.Panel2.Controls.Add(txtPurchasePrice)
            scMaster.Panel2.Controls.Add(label7)
            scMaster.Panel2.Controls.Add(cmbUnit)
            scMaster.Panel2.Controls.Add(label6)
            scMaster.Panel2.Controls.Add(cmbCategory)
            scMaster.Panel2.Controls.Add(label5)
            scMaster.Panel2.Controls.Add(label4)
            scMaster.Panel2.Controls.Add(txtBarcode)
            scMaster.Panel2.Controls.Add(label2)
            scMaster.Panel2.Controls.Add(txtProductCode)
            scMaster.Panel2.Controls.Add(label1)
            scMaster.Size = New Drawing.Size(1170, 336)
            scMaster.SplitterDistance = 57
            scMaster.TabIndex = 0
            ' 
            ' label3
            ' 
            label3.BackColor = Drawing.Color.CornflowerBlue
            label3.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            label3.Dock = Windows.Forms.DockStyle.Fill
            label3.Font = New Drawing.Font("Bell MT", 26.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, 0)
            label3.Location = New Drawing.Point(0, 0)
            label3.Name = "label3"
            label3.Size = New Drawing.Size(1168, 55)
            label3.TabIndex = 0
            label3.Text = "PRODUCT MASTER"
            label3.TextAlign = Drawing.ContentAlignment.MiddleCenter
            ' 
            ' txtProductName
            ' 
            txtProductName.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtProductName.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtProductName.Location = New Drawing.Point(157, 50)
            txtProductName.Name = "txtProductName"
            txtProductName.Size = New Drawing.Size(967, 24)
            txtProductName.TabIndex = 2
            ' 
            ' button4
            ' 
            button4.FlatStyle = Windows.Forms.FlatStyle.Popup
            button4.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            button4.Image = Global.Inventory_System_Pro.Properties.Resources.Hopstarter_Soft_Scraps_File_Delete_24
            button4.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            button4.Location = New Drawing.Point(714, 219)
            button4.Name = "button4"
            button4.Size = New Drawing.Size(147, 37)
            button4.TabIndex = 11
            button4.Text = "&Cancel"
            button4.UseVisualStyleBackColor = True
            AddHandler button4.Click, New EventHandler(AddressOf btnCancel_Click)
            ' 
            ' btnUpdate
            ' 
            btnUpdate.FlatStyle = Windows.Forms.FlatStyle.Popup
            btnUpdate.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            btnUpdate.Image = Global.Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Edit_24
            btnUpdate.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            btnUpdate.Location = New Drawing.Point(561, 219)
            btnUpdate.Name = "btnUpdate"
            btnUpdate.Size = New Drawing.Size(147, 37)
            btnUpdate.TabIndex = 10
            btnUpdate.Text = "&Update"
            btnUpdate.UseVisualStyleBackColor = True
            AddHandler btnUpdate.Click, New EventHandler(AddressOf btnUpdate_Click)
            ' 
            ' btnSave
            ' 
            btnSave.FlatStyle = Windows.Forms.FlatStyle.Popup
            btnSave.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            btnSave.Image = Global.Inventory_System_Pro.Properties.Resources.Bokehlicia_Captiva_Disk_save_as_24
            btnSave.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            btnSave.Location = New Drawing.Point(408, 219)
            btnSave.Name = "btnSave"
            btnSave.Size = New Drawing.Size(147, 37)
            btnSave.TabIndex = 9
            btnSave.Text = "&Save"
            btnSave.UseVisualStyleBackColor = True
            AddHandler btnSave.Click, New EventHandler(AddressOf btnSave_Click)
            ' 
            ' btnNew
            ' 
            btnNew.FlatStyle = Windows.Forms.FlatStyle.Popup
            btnNew.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            btnNew.Image = Global.Inventory_System_Pro.Properties.Resources.Custom_Icon_Design_Flatastic_4_Add_item_24
            btnNew.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            btnNew.Location = New Drawing.Point(255, 219)
            btnNew.Name = "btnNew"
            btnNew.Size = New Drawing.Size(147, 37)
            btnNew.TabIndex = 8
            btnNew.Text = "&New"
            btnNew.UseVisualStyleBackColor = True
            AddHandler btnNew.Click, New EventHandler(AddressOf btnNew_Click)
            ' 
            ' chkIsActive
            ' 
            chkIsActive.Anchor = Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Left Or Windows.Forms.AnchorStyles.Right

            chkIsActive.BackgroundImageLayout = Windows.Forms.ImageLayout.Center
            chkIsActive.CheckAlign = Drawing.ContentAlignment.MiddleCenter
            chkIsActive.Font = New Drawing.Font("Microsoft Sans Serif", 12F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            chkIsActive.Location = New Drawing.Point(504, 146)
            chkIsActive.Name = "chkIsActive"
            chkIsActive.Size = New Drawing.Size(30, 49)
            chkIsActive.TabIndex = 38
            chkIsActive.UseVisualStyleBackColor = True
            ' 
            ' label10
            ' 
            label10.AutoSize = True
            label10.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label10.Location = New Drawing.Point(459, 161)
            label10.Name = "label10"
            label10.Size = New Drawing.Size(47, 18)
            label10.TabIndex = 37
            label10.Text = "Active"
            label10.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' txtReorderLevel
            ' 
            txtReorderLevel.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtReorderLevel.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtReorderLevel.Location = New Drawing.Point(157, 158)
            txtReorderLevel.Name = "txtReorderLevel"
            txtReorderLevel.Size = New Drawing.Size(252, 24)
            txtReorderLevel.TabIndex = 7
            ' 
            ' label9
            ' 
            label9.AutoSize = True
            label9.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label9.Location = New Drawing.Point(53, 161)
            label9.Name = "label9"
            label9.Size = New Drawing.Size(100, 18)
            label9.TabIndex = 35
            label9.Text = "Reorder Level"
            label9.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' txtSalePrice
            ' 
            txtSalePrice.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtSalePrice.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtSalePrice.Location = New Drawing.Point(512, 122)
            txtSalePrice.Name = "txtSalePrice"
            txtSalePrice.Size = New Drawing.Size(252, 24)
            txtSalePrice.TabIndex = 6
            ' 
            ' label8
            ' 
            label8.AutoSize = True
            label8.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label8.Location = New Drawing.Point(433, 125)
            label8.Name = "label8"
            label8.Size = New Drawing.Size(75, 18)
            label8.TabIndex = 33
            label8.Text = "Sale Price"
            label8.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' txtPurchasePrice
            ' 
            txtPurchasePrice.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtPurchasePrice.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtPurchasePrice.Location = New Drawing.Point(157, 122)
            txtPurchasePrice.Name = "txtPurchasePrice"
            txtPurchasePrice.Size = New Drawing.Size(252, 24)
            txtPurchasePrice.TabIndex = 5
            ' 
            ' label7
            ' 
            label7.AutoSize = True
            label7.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label7.Location = New Drawing.Point(44, 125)
            label7.Name = "label7"
            label7.Size = New Drawing.Size(109, 18)
            label7.TabIndex = 31
            label7.Text = "Purchase Price"
            label7.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' cmbUnit
            ' 
            cmbUnit.FlatStyle = Windows.Forms.FlatStyle.Popup
            cmbUnit.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            cmbUnit.FormattingEnabled = True
            cmbUnit.Location = New Drawing.Point(512, 84)
            cmbUnit.Name = "cmbUnit"
            cmbUnit.Size = New Drawing.Size(252, 26)
            cmbUnit.TabIndex = 4
            ' 
            ' label6
            ' 
            label6.AutoSize = True
            label6.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label6.Location = New Drawing.Point(472, 87)
            label6.Name = "label6"
            label6.Size = New Drawing.Size(34, 18)
            label6.TabIndex = 29
            label6.Text = "Unit"
            label6.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' cmbCategory
            ' 
            cmbCategory.FlatStyle = Windows.Forms.FlatStyle.Popup
            cmbCategory.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            cmbCategory.FormattingEnabled = True
            cmbCategory.Location = New Drawing.Point(157, 84)
            cmbCategory.Name = "cmbCategory"
            cmbCategory.Size = New Drawing.Size(252, 26)
            cmbCategory.TabIndex = 3
            ' 
            ' label5
            ' 
            label5.AutoSize = True
            label5.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label5.Location = New Drawing.Point(83, 87)
            label5.Name = "label5"
            label5.Size = New Drawing.Size(68, 18)
            label5.TabIndex = 27
            label5.Text = "Category"
            label5.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' label4
            ' 
            label4.AutoSize = True
            label4.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label4.Location = New Drawing.Point(50, 52)
            label4.Name = "label4"
            label4.Size = New Drawing.Size(104, 18)
            label4.TabIndex = 26
            label4.Text = "Product Name"
            label4.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' txtBarcode
            ' 
            txtBarcode.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtBarcode.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtBarcode.Location = New Drawing.Point(904, 16)
            txtBarcode.Name = "txtBarcode"
            txtBarcode.Size = New Drawing.Size(220, 24)
            txtBarcode.TabIndex = 1
            ' 
            ' label2
            ' 
            label2.AutoSize = True
            label2.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label2.Location = New Drawing.Point(836, 19)
            label2.Name = "label2"
            label2.Size = New Drawing.Size(64, 18)
            label2.TabIndex = 24
            label2.Text = "Barcode"
            ' 
            ' txtProductCode
            ' 
            txtProductCode.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtProductCode.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            txtProductCode.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtProductCode.Location = New Drawing.Point(157, 16)
            txtProductCode.Name = "txtProductCode"
            txtProductCode.Size = New Drawing.Size(252, 24)
            txtProductCode.TabIndex = 0
            ' 
            ' label1
            ' 
            label1.AutoSize = True
            label1.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label1.Location = New Drawing.Point(54, 19)
            label1.Name = "label1"
            label1.Size = New Drawing.Size(100, 18)
            label1.TabIndex = 22
            label1.Text = "Product Code"
            label1.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' splitContainer1
            ' 
            splitContainer1.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            splitContainer1.Dock = Windows.Forms.DockStyle.Bottom
            splitContainer1.Location = New Drawing.Point(0, 339)
            splitContainer1.Name = "splitContainer1"
            splitContainer1.Orientation = Windows.Forms.Orientation.Horizontal
            ' 
            ' splitContainer1.Panel1
            ' 
            splitContainer1.Panel1.BackColor = Drawing.Color.LightSteelBlue
            splitContainer1.Panel1.Controls.Add(txtSearch)
            splitContainer1.Panel1.Controls.Add(label20)
            ' 
            ' splitContainer1.Panel2
            ' 
            splitContainer1.Panel2.Controls.Add(lvProducts)
            splitContainer1.Size = New Drawing.Size(1170, 402)
            splitContainer1.SplitterDistance = 68
            splitContainer1.TabIndex = 1
            ' 
            ' txtSearch
            ' 
            txtSearch.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            txtSearch.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            txtSearch.Location = New Drawing.Point(150, 17)
            txtSearch.Name = "txtSearch"
            txtSearch.Size = New Drawing.Size(220, 24)
            txtSearch.TabIndex = 0
            AddHandler txtSearch.TextChanged, New EventHandler(AddressOf txtSearch_TextChanged)
            ' 
            ' label20
            ' 
            label20.AutoSize = True
            label20.Font = New Drawing.Font("Microsoft Sans Serif", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            label20.Location = New Drawing.Point(91, 20)
            label20.Name = "label20"
            label20.Size = New Drawing.Size(55, 18)
            label20.TabIndex = 0
            label20.Text = "Search"
            label20.TextAlign = Drawing.ContentAlignment.MiddleRight
            ' 
            ' lvProducts
            ' 
            lvProducts.BorderStyle = Windows.Forms.BorderStyle.None
            lvProducts.Columns.AddRange(New Windows.Forms.ColumnHeader() {ProductId, ProductCode, Barcode, Productname, Category, Unit, PurchasePrice, SalePrice, ReorderLevel, Active})
            lvProducts.Dock = Windows.Forms.DockStyle.Fill
            lvProducts.FullRowSelect = True
            lvProducts.GridLines = True
            lvProducts.HideSelection = False
            lvProducts.Location = New Drawing.Point(0, 0)
            lvProducts.MultiSelect = False
            lvProducts.Name = "lvProducts"
            lvProducts.Size = New Drawing.Size(1168, 328)
            lvProducts.TabIndex = 0
            lvProducts.UseCompatibleStateImageBehavior = False
            lvProducts.View = Windows.Forms.View.Details
            AddHandler lvProducts.SelectedIndexChanged, New EventHandler(AddressOf lvProducts_SelectedIndexChanged)
            ' 
            ' ProductId
            ' 
            ProductId.Text = "Product Id"
            ProductId.Width = 100
            ' 
            ' ProductCode
            ' 
            ProductCode.Text = "Product Code"
            ProductCode.Width = 104
            ' 
            ' Barcode
            ' 
            Barcode.Text = "Barcode"
            Barcode.Width = 100
            ' 
            ' Productname
            ' 
            Productname.Text = "Product Name"
            Productname.Width = 200
            ' 
            ' Category
            ' 
            Category.Text = "Category"
            Category.TextAlign = Windows.Forms.HorizontalAlignment.Center
            Category.Width = 100
            ' 
            ' Unit
            ' 
            Unit.Text = "Unit"
            Unit.TextAlign = Windows.Forms.HorizontalAlignment.Center
            Unit.Width = 100
            ' 
            ' PurchasePrice
            ' 
            PurchasePrice.Text = "Purchase Price"
            PurchasePrice.TextAlign = Windows.Forms.HorizontalAlignment.Right
            PurchasePrice.Width = 150
            ' 
            ' SalePrice
            ' 
            SalePrice.Text = "Sale Price"
            SalePrice.TextAlign = Windows.Forms.HorizontalAlignment.Right
            SalePrice.Width = 150
            ' 
            ' ReorderLevel
            ' 
            ReorderLevel.Text = "Reorder Level"
            ReorderLevel.TextAlign = Windows.Forms.HorizontalAlignment.Right
            ReorderLevel.Width = 100
            ' 
            ' Active
            ' 
            Active.Text = "Active"
            Active.TextAlign = Windows.Forms.HorizontalAlignment.Center
            ' 
            ' FrmProductMaster
            ' 
            AutoScaleDimensions = New Drawing.SizeF(8F, 16F)
            AutoScaleMode = Windows.Forms.AutoScaleMode.Font
            ClientSize = New Drawing.Size(1170, 741)
            Controls.Add(splitContainer1)
            Controls.Add(scMaster)
            Font = New Drawing.Font("Microsoft Sans Serif", 9.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, 0)
            FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            Margin = New Windows.Forms.Padding(4)
            MaximizeBox = False
            Name = "FrmProductMaster"
            StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            Text = "Product Master"
            AddHandler Load, New EventHandler(AddressOf FrmProductMaster_Load)
            scMaster.Panel1.ResumeLayout(False)
            scMaster.Panel2.ResumeLayout(False)
            scMaster.Panel2.PerformLayout()
            CType(scMaster, ComponentModel.ISupportInitialize).EndInit()
            scMaster.ResumeLayout(False)
            splitContainer1.Panel1.ResumeLayout(False)
            splitContainer1.Panel1.PerformLayout()
            splitContainer1.Panel2.ResumeLayout(False)
            CType(splitContainer1, ComponentModel.ISupportInitialize).EndInit()
            splitContainer1.ResumeLayout(False)
            ResumeLayout(False)

        End Sub

#End Region

        Private scMaster As Windows.Forms.SplitContainer
        Private label3 As Windows.Forms.Label
        Private splitContainer1 As Windows.Forms.SplitContainer
        Private txtSearch As Windows.Forms.TextBox
        Private label20 As Windows.Forms.Label
        Private lvProducts As Windows.Forms.ListView
        Private ProductId As Windows.Forms.ColumnHeader
        Private ProductCode As Windows.Forms.ColumnHeader
        Private Barcode As Windows.Forms.ColumnHeader
        Private Productname As Windows.Forms.ColumnHeader
        Private Category As Windows.Forms.ColumnHeader
        Private Unit As Windows.Forms.ColumnHeader
        Private PurchasePrice As Windows.Forms.ColumnHeader
        Private SalePrice As Windows.Forms.ColumnHeader
        Private ReorderLevel As Windows.Forms.ColumnHeader
        Private Active As Windows.Forms.ColumnHeader
        Private txtProductName As Windows.Forms.TextBox
        Private button4 As Windows.Forms.Button
        Private btnUpdate As Windows.Forms.Button
        Private btnSave As Windows.Forms.Button
        Private btnNew As Windows.Forms.Button
        Private chkIsActive As Windows.Forms.CheckBox
        Private label10 As Windows.Forms.Label
        Private txtReorderLevel As Windows.Forms.TextBox
        Private label9 As Windows.Forms.Label
        Private txtSalePrice As Windows.Forms.TextBox
        Private label8 As Windows.Forms.Label
        Private txtPurchasePrice As Windows.Forms.TextBox
        Private label7 As Windows.Forms.Label
        Private cmbUnit As Windows.Forms.ComboBox
        Private label6 As Windows.Forms.Label
        Private cmbCategory As Windows.Forms.ComboBox
        Private label5 As Windows.Forms.Label
        Private label4 As Windows.Forms.Label
        Private txtBarcode As Windows.Forms.TextBox
        Private label2 As Windows.Forms.Label
        Private txtProductCode As Windows.Forms.TextBox
        Private label1 As Windows.Forms.Label
    End Class
End Namespace
