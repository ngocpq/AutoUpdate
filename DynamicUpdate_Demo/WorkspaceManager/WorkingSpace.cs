using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkspaceManager
{
    public class WorkingSpace
    {
        static WorkingSpace _Instance;
        
        public static WorkingSpace Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new WorkingSpace();
                return _Instance;
            }
        }

        internal static void LoadWorkspace(string workspaceFile)
        {
            //TODO: load workspace from file
            MessageBox.Show("Read workspace file");            
            _Instance = new WorkingSpace(workspaceFile);

        }

        public static void ApplicationExit(object sender, EventArgs e)
        {
            MessageBox.Show("Application exiting");
            if (Instance == null)
                return;
            Instance.SaveWorkspace();
        }

        List<IWindowManager> OpeningWindows = new List<IWindowManager>();

        private void SaveWorkspace()
        {
            StringBuilder sb = new StringBuilder();
            foreach(IWindowManager w in this.OpeningWindows)
            {
                sb.Append("Form: " + w.ToString() + Environment.NewLine);
            }
            MessageBox.Show("#Openning windows: " + this.OpeningWindows.Count + Environment.NewLine + sb.ToString());
        }

        public static void Init(WorkspaceManagerConfig config)
        {
            _Instance = new WorkingSpace(config);
        }

        WorkspaceManagerConfig _Config;

        public WorkingSpace(WorkspaceManagerConfig config) {
            this._Config = config;
        }

        public WorkingSpace(String config)
        {
        }

        protected WorkingSpace() { }

        public static WorkingSpace GetCurrentWorkspace() {
            return Instance;
        }

        internal static ControlLayoutSerializer GetControlLayoutSerializer(Control ctr)
        {
            throw new NotImplementedException();
        }

        internal static IStorage GetStorage(FormStateManagerControl formStateManager)
        {
            throw new NotImplementedException();
        }

        public static void AddToWorkspace(IWindowManager window)
        {
            if (Instance == null)
                return;
            Instance.AddOpeningWindow(window);
        }
        public static void RemoveFromWorkspace(IWindowManager window)
        {
            if (Instance == null)
                return;
            Instance.RemoveOpeningWindow(window);

        }

        public void RemoveOpeningWindow(IWindowManager window)
        {
            if (!this.OpeningWindows.Contains(window))
                return;
            this.OpeningWindows.Remove(window);
        }

        public void AddOpeningWindow(IWindowManager window)
        {
            if (this.OpeningWindows.Contains(window))
                return;
            this.OpeningWindows.Add(window);
        }

        
    }
}
