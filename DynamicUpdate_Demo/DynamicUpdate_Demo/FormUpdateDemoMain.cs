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
    public partial class FormUpdateDemoMain : Form
    {
        public FormUpdateDemoMain()
        {
            InitializeComponent();
        }

        private void btnUpdateSample1_Click(object sender, EventArgs e)
        {
            //1) check if the newer version is available
            //2) download the new version
            //3) stop the current version if it is running
            //4) replace the assembly file (update dll)
            //5) reload and start the assambly
            
        }
    }
}
