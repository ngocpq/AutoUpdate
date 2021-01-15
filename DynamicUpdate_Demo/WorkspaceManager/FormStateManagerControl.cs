using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkspaceManager
{
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [Designer("WorkspaceManager.FormStateManagerControl,System.Design")]
    public class FormStateManagerControl : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize, IWindowManager
    {
        event EventHandler DisplayStateSaving;
        event EventHandler DisplayStateLoading;

        public Form TargetForm { get; protected set; }

        public string TargetFormName { get { return TargetForm == null ? null : TargetForm.Name; } }        

        public void BeginInit()
        {
            
        }

        public void EndInit()
        {
            TargetForm = findForm();
            if (TargetForm == null)
                return;
            TargetForm.FormClosing += Target_FormClosing;
            TargetForm.Load += Target_FormLoad;            
        }

        protected void Load()
        {
            if (TargetForm == null)
                return;

            //IStorage storage = WorkspaceManager.GetStorage(this);
            //if (storage == null)
            //    return;

            //foreach (Control ctr in TargetForm.Controls)
            //{
            //    ControlLayoutSerializer layoutSerializer = WorkspaceManager.GetControlLayoutSerializer(ctr);
            //    if (layoutSerializer != null)
            //    {
            //        string layout = storage.Read(ctr.Name);
            //        layoutSerializer.ApplyLayout(layout);                    
            //    }                    
            //}
        }

        protected void Save()
        {
            if (TargetForm == null)
                return;

            //IStorage storage = WorkspaceManager.GetStorage(this);
            //if (storage == null)
            //    return;

            //foreach (Control ctr in TargetForm.Controls)
            //{
            //    ControlLayoutSerializer layoutSerializer = WorkspaceManager.GetControlLayoutSerializer(ctr);
            //    if (layoutSerializer != null)
            //    {
            //        string layout = layoutSerializer.ReadControlLayoutState(ctr);
            //        storage.Write(ctr.Name, layout);
            //    }
            //}
        }

        private void Target_FormLoad(object sender, EventArgs e)
        {
            MessageBox.Show("Loading form : "+TargetFormName);
            if (this.DisplayStateLoading != null)
                DisplayStateLoading(sender, e);
            WorkingSpace.AddToWorkspace(this);
        }

        private void Target_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Closing form : " + TargetFormName);
            if (this.DisplayStateSaving != null)
                DisplayStateSaving(sender, e);
            
            WorkingSpace.RemoveFromWorkspace(this);

        }

        private Form findForm()
        {
            if (this.Container == null || !(this.Container is Control))
                return null;

            Control parent = (Control)this.Container;
            while (parent != null)
            {
                if (parent is Form)
                    return (Form)parent;                
                parent = parent.Parent;
            }

            return null;
        }
    }

}
