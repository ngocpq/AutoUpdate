using Bingo.Update;
using System;
using System.Windows.Forms;

namespace AutoUpdaterTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void CheckForUpdate(bool confirm)
        {
            try
            {
                string updateUrl = AppSettings.UpdateUrl;
                if (String.IsNullOrEmpty(updateUrl)) return;
                UpdateManager.CheckForUpdateBaseCode = updateUrl;
                bool isUpdateSuccess = UpdateManager.CheckForUpdate(confirm);
                if (confirm == false && isUpdateSuccess == false)
                    MessageBox.Show("This is the latest version", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                t += "d";
                MessageBox.Show("Cannot check for update. Please check your connection or configuration", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }


   
}
