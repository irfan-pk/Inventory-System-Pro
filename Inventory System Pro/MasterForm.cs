using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_System_Pro
{
    public partial class MasterForm : Form
    {
        //private int childFormNumber = 0;

        public MasterForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(Form frm, object sender, EventArgs e)
        {
            if (frm == null)
                return;

            frm.MdiParent = this;
            frm.Text = "(" + frm.Text + ")" + " Window";
            frm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmProductMaster(), sender, e);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            this.Close();
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSales(), sender, e);
        }

        private void customerToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCustomer(), sender, e);
        }

        private void purchaseToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmPurchaseOrder(), sender, e);
        }

        private void receiptPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmPurchaseReceipt(), sender, e);
        }

        private void returnToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmPurchaseReturn(), sender, e);
        }

        private void salereturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSaleReturn(), sender, e);
        }

        private void customerStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCustomerLedger(), sender, e);
        }

        private void supplierStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSupplierLedger(), sender, e);
        }

        private void stockStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmStockLedger(), sender, e);
        }

        private void saleorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSaleOrder(), sender, e);
        }

        private void saleStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmSales(), sender, e);
        }

        private void saleorderStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSaleOrder(), sender, e);
        }

        private void supplierpaymenttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSupplierPayment(), sender, e);
        }

        private void cashpaymenttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm (frm: new FrmCustomerPayment(), sender, e);
        }

        private void cashStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCashAccountLedger(), sender, e);
        }

        private void productStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmProductMaster(), sender, e);
        }

        private void 
            tripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmUnits(), sender, e);
        }

        private void categoryStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCategory(), sender, e);
        }

        private void cashPaymentMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCustomerPayment(), sender, e);
        }

        private void supplierPaymentMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSupplierPayment(), sender, e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmCustomer(), sender, e);
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSupplier(), sender, e);
        }

        private void unitStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmUnits(), sender, e);
        }

        private void supplierToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmSupplier(), sender, e);
        }

        private void UsersStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmAppUser(), sender, e);
        }

        private void RolesStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmUserRoleAssignment(), sender, e);
        }

        private void PermissionsStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewForm(frm: new FrmRolesPermission(), sender, e);
        }
    }
}
