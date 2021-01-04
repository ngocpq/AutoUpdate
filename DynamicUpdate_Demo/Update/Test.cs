using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Bingo.Update
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All file |*.*|Application manifest|*.manifest|Deployment manifest|*.application";
            if (dlg.ShowDialog() == DialogResult.OK)
                txtPath.Text = dlg.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Chua nhap Path");
                return;
            }
            DeploymentManifest deploy = new DeploymentManifest(txtPath.Text);

            rtxtNoiDung.Text = "Name: " + deploy.AssemblyIdentity.name + "\nVersion" + deploy.AssemblyIdentity.version;
            rtxtNoiDung.Text += "\nCodebase: " + deploy.Deployment.deploymentProvider.codebase;
            rtxtNoiDung.Text += "\nMapFileExt: " + deploy.Deployment.mapFileExtensions.ToString();
            rtxtNoiDung.Text+="\nDependencies:\n";
            foreach(DependencyElement dep in deploy.Dependencys)
            {
                rtxtNoiDung.Text += "\n\tType:" + dep.DependencyAssembly.DependencyType;
                rtxtNoiDung.Text += "\n\tCodeBase:" + dep.DependencyAssembly.CodeBase;
                rtxtNoiDung.Text += "\n\tAssem Name:" + dep.DependencyAssembly.AssemblyIdentity.name;
                rtxtNoiDung.Text += "\n\tAssem Version:" + dep.DependencyAssembly.AssemblyIdentity.version;
                rtxtNoiDung.Text += "\n\t----------------";
            }
            rtxtNoiDung.Text += "\n******************************\n";
            ApplicationManifest app = deploy.ApplicationManifest;
            rtxtNoiDung.Text+="\nApplicationName: "+app.AssemblyIdentity.name;
            rtxtNoiDung.Text += "\nApplicationVersion: " + app.AssemblyIdentity.version;
            rtxtNoiDung.Text += "\nCommandFile: " + app.EntryPoint.CommandLineFile;
            rtxtNoiDung.Text += "\nCommandPara: " + app.EntryPoint.CommandLineParameters;
            rtxtNoiDung.Text += "\nDependencies:\n";
            foreach (DependencyElement dep in app.Dependencys)
            {
                if (dep.DependencyAssembly != null)
                {
                    rtxtNoiDung.Text += "\n\tType:" + dep.DependencyAssembly.DependencyType;
                    rtxtNoiDung.Text += "\n\tCodeBase:" + dep.DependencyAssembly.CodeBase;
                    rtxtNoiDung.Text += "\n\tAssem Name:" + dep.DependencyAssembly.AssemblyIdentity.name;
                    rtxtNoiDung.Text += "\n\tAssem Version:" + dep.DependencyAssembly.AssemblyIdentity.version;                    
                    rtxtNoiDung.Text += "\n\t----------------";
                }
            }
            

        }
    }
}
