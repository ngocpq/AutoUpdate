using Bingo.Update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdaterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool confirm = true;
            //UpdateManager.CheckForUpdateBaseCode = "https://drive.google.com/drive/folders/111J86uzldaUretRTdaG4p7JzF16Uzvio/Form1.application";
            bool isUpdateSuccess = UpdateManager.CheckForUpdate(confirm);
            if (confirm == true && isUpdateSuccess == false)
                MessageBox.Show("This is the latest version", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
