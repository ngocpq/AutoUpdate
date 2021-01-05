using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary2
{
    public class FormMarshalRefObject1 : MarshalByRefObject, FormMarshalRefObject
    {
        Form1 form1;
        public FormMarshalRefObject1()
        {
            form1 = new Form1();
        }
        
        public void Show()
        {
            form1.Show();
            form1.Text += "AppDomain: " + AppDomain.CurrentDomain.FriendlyName;
        }


    }
}
