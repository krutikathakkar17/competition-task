using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarsEduCertAutomation.Utilities
{
    internal class ProjectPathHelper
    {
        public static string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        public static string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        // public static string projectPath => AppDomain.CurrentDomain.BaseDirectory;
        public static string projectPath = new Uri(actualPath).LocalPath;
    }
}
