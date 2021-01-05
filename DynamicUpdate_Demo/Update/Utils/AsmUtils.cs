using System;
using System.Reflection;

namespace Update.Utils
{
    public static class AsmUtils
    {
        public static Version GetAssemblyVersion(string asmPath)
        {
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(asmPath);
            return assemblyName.Version;
        }

        public static int CompareAssemblyVersion(string asmPath1, string asmPath2)
        {            
            Version v1 = GetAssemblyVersion(asmPath1);
            Version v2 = GetAssemblyVersion(asmPath2);
            return v1.CompareTo(v2);
        }
        public static int CompareVersion(string version1, string version2)
        {
            Version v1 = new Version(version1);
            Version v2 = new Version(version2);
            return v1.CompareTo(v2);
        }
        public static Version GetCurrentVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
