using Inventory_System_Pro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_System_Pro
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;

                AppSession.UserID = login.AuthenticatedUserID;
                AppSession.UserName = login.AuthenticatedUserName;

                // Load user permissions after successful login
                string connectionString =
                System.Configuration.ConfigurationManager
                    .ConnectionStrings["InventoryConnection"].ConnectionString;
                AppSession.LoadPermissions(connectionString);
            }

            Application.Run(new MasterForm());
        }


        // App Session class to hold the current user's information and permissions
        internal static class AppSession
        {
            public static int UserID { get; set; }
            public static string UserName { get; set; }

            private static HashSet<string> _permissions =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            public static bool HasPermission(string permissionKey)
            {
                return _permissions.Contains(permissionKey);
            }

            public static void LoadPermissions(string connectionString)
            {
                var loadedPermissions =
                    new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    "dbo.sp_AppUser_GetPermissions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loadedPermissions.Add(reader["PermissionKey"].ToString());
                        }
                    }
                }

                _permissions = loadedPermissions;
            }

            public static void Clear()
            {
                UserID = 0;
                UserName = null;
                _permissions.Clear();
            }
        }
    }
}
