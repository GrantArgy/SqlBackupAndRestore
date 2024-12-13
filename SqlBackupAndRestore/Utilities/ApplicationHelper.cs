using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace SqlBackupAndRestore.Utilities
{
  internal static class ApplicationHelper
  {

    //private static Assembly ApplicationAssembly = Assembly.GetExecutingAssembly();

    //private static FileVersionInfo FileDetails = FileVersionInfo.GetVersionInfo(ApplicationExecutable);

    public static bool IsAdministrator()
    {
      return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    //public static string ApplicationExecutable => ApplicationAssembly.Location;

    //public static string ProductName => Application.ProductName;

    //public static string Name => ApplicationAssembly.GetName().Name;

    //public static string FullName => ApplicationAssembly.GetName().FullName;

    //public static string ProductName1 => FileDetails.ProductName;

    //public static string ProductDescription => FileDetails.FileDescription;

    //public static string ProductVersion => Application.ProductName;

    //public static string AssemblyVersion => Application.ProductName;

    //public static AssemblyName AssemblyName => ApplicationAssembly.GetName();

  }
}
