using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        Assembly assembly;
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

            if (txtNewAsmFile.Text.Trim().Length == 0)
            {
                MessageBox.Show("Select new DLL first");
                return;
            }

            if (CompareAssemblyVersion(lblCurrentVersion.Text, lblNewVersion.Text) >= 0)
            {
                MessageBox.Show("Current version is the later version");
                return;
            }

            UnloadAndDeleteDll();            
            File.Copy(txtNewAsmFile.Text, txtCurrentAsmFilePath.Text, true);

            if (loadAsm(txtCurrentAsmFilePath.Text))
                MessageBox.Show("Updated");
        }

        public static Version GetAssemblyVersion(string asmPath)
        {
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(asmPath);
            return assemblyName.Version;            
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
            txtClassName.Text = "ControlLibrary2.FormMarshalRefObject1";
            lblCurrentVersion.Text = GetAssemblyVersion(txtCurrentAsmFilePath.Text).ToString();
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            loadAsm(txtCurrentAsmFilePath.Text);                
        }

        bool loadAsm(string asmPath)
        {
            string version = GetAssemblyVersion(asmPath).ToString();

            txtCurrentAsmFilePath.Text = asmPath;
            lblCurrentVersion.Text = version;
            try
            {
                //AppDomainSetup info = new AppDomainSetup();
                //info.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory; ;
                //info.LoaderOptimization = LoaderOptimization.SingleDomain;
                //info.ShadowCopyFiles = "true";
                //appDomain1 = AppDomain.CreateDomain("appDomain1:" + count1, null, info);

                appDomain1 = AppDomain.CreateDomain("appDomain1:" + count1);
                unloaded = false;
                appDomain1.DomainUnload += AppDomain1_DomainUnload1;

                string asmName = AssemblyName.GetAssemblyName(txtCurrentAsmFilePath.Text).FullName;
                System.Runtime.Remoting.ObjectHandle handler = appDomain1.CreateInstance(asmName, txtClassName.Text);
                object obj = handler.Unwrap();

                if (obj is CommonLib.FormMarshalRefObject)
                {
                    CommonLib.FormMarshalRefObject f1 = (CommonLib.FormMarshalRefObject)obj;
                    f1.Show();
                }
                else if (obj is Form)
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
            //appDomain1 = null;
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

        bool UnloadAndDeleteDll()
        {
            if (assembly != null)
            {
                //TODO: clean up memory
                assembly = null;
            }
            if (this.form1 != null)
            {
                panel1.Controls.Remove(form1);
                form1 = null;
            }
            //unload and rename dll
            if (appDomain1 != null)
            {
                AppDomain.Unload(appDomain1);
                GC.Collect();
                int retry = 0;
                while (appDomain1 != null && !unloaded)
                {
                    Thread.Sleep(300);
                    retry++;
                }
            }
            string currFile = txtCurrentAsmFilePath.Text;
            if (File.Exists(currFile))
            {                
                File.Delete(currFile);
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //unload and rename dll
            UnloadAndDeleteDll();
        }
    }
}
