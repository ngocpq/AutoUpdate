using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdaterTest.Utils
{
    public static class AsmUtils
    {
        private static Version GetAssemblyVersion(string asmPath)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(asmPath);
            return asm.GetName().Version;
        }
        private static int CompareAssemblyVersion(string asmPath1, string asmPath2)
        {            
            Version v1 = GetAssemblyVersion(asmPath1);
            Version v2 = GetAssemblyVersion(asmPath2);
            return v1.CompareTo(v2);
        }
        private static int CompareVersion(string version1, string version2)
        {
            Version v1 = new Version(version1);
            Version v2 = new Version(version2);
            return v1.CompareTo(v2);
        }
    }
}
