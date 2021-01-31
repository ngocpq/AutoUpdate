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

        List<IWindowManager> OpeningWindows = new List<IWindowManager>();

        Dictionary<string, AppDomain> ActiveAppDomains = new Dictionary<string, AppDomain>();

        Dictionary<string, List<AppDomain>> Assembly2AppDomains = new Dictionary<string, List<AppDomain>>();

        private void SaveWorkspace()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IWindowManager w in this.OpeningWindows)
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

        public WorkingSpace(WorkspaceManagerConfig config)
        {
            this._Config = config;
        }

        public WorkingSpace(String config)
        {
        }

        protected WorkingSpace() { }

        public AppDomain CreateAppDomain(string name)
        {
            AppDomain appDomain;
            if (this.ActiveAppDomains.ContainsKey(name))
                appDomain = this.ActiveAppDomains[name];
            else
            {
                appDomain = AppDomain.CreateDomain(name);
                this.ActiveAppDomains[name] = appDomain;
                appDomain.DomainUnload += AppDomain_DomainUnload; ;
                appDomain.AssemblyLoad += AppDomain_AssemblyLoad;
            }
            return appDomain;
        }

        private void AppDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            AppDomain appDomain = sender as AppDomain;
            if (appDomain == null)
                return;

            string asmLocation = args.LoadedAssembly.Location;
            List<AppDomain> appDomains;
            if (this.Assembly2AppDomains.ContainsKey(asmLocation))
            {
                appDomains = Assembly2AppDomains[asmLocation];
            }
            else
            {
                appDomains = new List<AppDomain>();
                Assembly2AppDomains[asmLocation] = appDomains;
            }
            if (!appDomains.Contains(appDomain))
                appDomains.Add(appDomain);
        }

        private void AppDomain_DomainUnload(object sender, EventArgs e)
        {
            //TODO: remove unloaded appDomain from the active list

        }

        #region Static members

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

        public static WorkingSpace GetCurrentWorkspace()
        {
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


        #endregion


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
