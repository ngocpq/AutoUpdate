using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicUpdate_Demo
{
    public partial class FormUpdateDemoMain : Form
    {
        public FormUpdateDemoMain()
        {
            InitializeComponent();
        }

        //string asmFilePath1 = @"E:\Workspace\ERPIA\Demo\DynamicUpdate_Demo\ControlLibrary2\bin\Debug\ControlLibrary2.dll";
        //string asmFilePath1 = Path.Combine(AppContext.BaseDirectory, "ControlLibrary2.dll");
        //string form1ClassName = "ControlLibrary2.Form1";
        //string asm1Version = "";

        Dictionary<AppDomain, bool> appDomainState = new Dictionary<AppDomain, bool>();

        AppDomain appDomain1;
        bool unloaded = true;

        int count1 = 0;
        Form form1;
        private void btnUpdateSample1_Click(object sender, EventArgs e)
        {
            //1) check if the newer version is available
            //2) download the new version
            //3) stop the current version if it is running
            //4) replace the assembly file (update dll)
            //5) reload and start the assambly       

            if (CompareAssemblyVersion(lblCurrentVersion.Text, lblNewVersion.Text) >= 0)
            {
                MessageBox.Show("Current version is the later version");
                return;
            }

            if (appDomain1 != null) {
                splitContainerMainBody.Panel2.Controls.Remove(form1);
                AppDomain.Unload(appDomain1);
                //replace dll
                int retry = 0;
                while (appDomain1 != null &&  !unloaded)
                {
                    Thread.Sleep(300);
                    retry++;
                }
                string backupFile = txtCurrentAsmFilePath.Text + ".bak";                
                File.Copy(txtNewAsmFile.Text, txtCurrentAsmFilePath.Text, true);
            }
            if (loadAsm(txtCurrentAsmFilePath.Text))
                MessageBox.Show("Updated");
        }

        public static Version GetAssemblyVersion(string asmPath)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(asmPath);
            return asm.GetName().Version;
        }
        public static int CompareAssemblyVersion(string strV1, string strV2)
        {
            Version v1 = new Version(strV1);
            Version v2 = new Version(strV2);
            return v1.CompareTo(v2);
        }

        private void FormUpdateDemoMain_Load(object sender, EventArgs e)
        {
            txtCurrentAsmFilePath.Text = Path.Combine(AppContext.BaseDirectory, "ControlLibrary2.dll");
            txtClassName.Text = "ControlLibrary2.Form1";
            lblCurrentVersion.Text = GetAssemblyVersion(txtCurrentAsmFilePath.Text).ToString();
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            if (loadAsm(txtCurrentAsmFilePath.Text))
                MessageBox.Show("Loaded");
        }

        bool loadAsm(string newAsmFilePath)
        {
            string version = GetAssemblyVersion(newAsmFilePath).ToString();

            txtCurrentAsmFilePath.Text = newAsmFilePath;
            lblCurrentVersion.Text = version;
            try
            {
                appDomain1 = AppDomain.CreateDomain("appDomain1:" + count1);
                unloaded = false;
                appDomain1.DomainUnload += AppDomain1_DomainUnload1;
                object obj = appDomain1.CreateInstanceFromAndUnwrap(txtCurrentAsmFilePath.Text, txtClassName.Text);
                if (obj is Form)
                {
                    form1 = (Form)obj;
                    //form1.TopLevel = false;
                    //form1.FormBorderStyle = FormBorderStyle.None;
                    //form1.Dock = DockStyle.Fill;
                    //panel1.Controls.Add(form1);
                    form1.Show();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return false;
        }

        private void AppDomain1_DomainUnload1(object sender, EventArgs e)
        {
            appDomain1 = null;
            unloaded = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dll files (*.dll)|*.dll|Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;

            dlg.InitialDirectory = AppContext.BaseDirectory;
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            txtNewAsmFile.Text = dlg.FileName;
            lblNewVersion.Text = GetAssemblyVersion(txtNewAsmFile.Text).ToString();
        }
    }
}
