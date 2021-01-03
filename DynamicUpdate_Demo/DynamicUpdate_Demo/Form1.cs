using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicUpdate_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (appDomain == null)
            {
                MessageBox.Show("Not initialized");
                return;
            }
            //AppDomain appDomain = AppDomain.CurrentDomain;
            MessageBox.Show("current appDomain: " + appDomain.FriendlyName);
        }

        AppDomain appDomain = null;

        Form currForm = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("File path must be entered first");
                return;
            }
            if (textBox2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Class  name must be entered first");
                return;
            }
            count++;
            appDomain = AppDomain.CreateDomain("app domain: "+count);
            string asmFilePath = textBox1.Text.Trim();
            string typeName = textBox2.Text.Trim();
            object obj = appDomain.CreateInstanceFromAndUnwrap(asmFilePath, typeName);

            if (obj is Form)
            {
                currForm = (Form)obj;
                currForm.Show();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dll files (*.dll)|*.dll|Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;

            dlg.InitialDirectory = AppContext.BaseDirectory;
            if (dlg.ShowDialog(this) == DialogResult.OK)
                textBox1.Text = dlg.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (appDomain == null)
            {
                MessageBox.Show("Not initialized");
                return;
            }
            AppDomain.Unload(appDomain);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currForm == null)
                return;
            currForm.Show();
        }

        private void btn_LoadAssembly_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("File path must be entered first");
                return;
            }
            if (textBox2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Class  name must be entered first");
                return;
            }
            count++;
            string asmFilePath = textBox1.Text.Trim();
            string typeName = textBox2.Text.Trim();
            
            object obj = appDomain.CreateInstanceFromAndUnwrap(asmFilePath, typeName);

            if (obj is Form)
            {
                currForm = (Form)obj;
                currForm.Show();
            }
        }
    }
}
