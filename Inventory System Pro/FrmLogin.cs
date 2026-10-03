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
using System.Security.Cryptography;

namespace Inventory_System_Pro
{
    public partial class FrmLogin : Form
    {
        private readonly string ConnString =
            ConfigurationManager
            .ConnectionStrings["InventoryConnection"]
            .ConnectionString;

        public int AuthenticatedUserID { get; private set; }
        public string AuthenticatedUserName { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
            CancelButton = btnExit;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblLoginMessage.Text = "";
            txtLoginName.Focus();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginMessage.Text = "";
            Cursor.Current = Cursors.WaitCursor;
            if (string.IsNullOrWhiteSpace(txtLoginName.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                lblLoginMessage.Text = "Enter your login name and password.";
                Cursor.Current = Cursors.Default;
                return;
            }

            try
            {
                LoginUser user = GetLoginUser(txtLoginName.Text.Trim());

                if (user == null || !PasswordHasher.Verify(txtPassword.Text, user.PasswordHash))
                {
                    lblLoginMessage.Text = "Invalid login name or password.";
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    Cursor.Current = Cursors.Default;
                    return;
                }

                AuthenticatedUserID = user.UserID;
                AuthenticatedUserName = user.UserName;
                DialogResult = DialogResult.OK;
                Cursor.Current = Cursors.Default;
                Close();
            }
            catch
            {
                lblLoginMessage.Text = "Unable to connect to the database.";
                Cursor.Current = Cursors.Default;
            }
        }

        private LoginUser GetLoginUser(string loginName)
        {
            using (var cn = new SqlConnection(ConnString))
            using (var cmd = new SqlCommand("dbo.sp_AppUser_GetForLogin", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 100)
                    .Value = loginName;

                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new LoginUser
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        PasswordHash = Convert.ToString(reader["PasswordHash"])
                    };
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private sealed class LoginUser
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
        }
    }
}
