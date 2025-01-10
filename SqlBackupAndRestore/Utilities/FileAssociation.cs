using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace SqlBackupAndRestore.Utilities
{
  internal static class FileAssociation
  {

    #region Private Variables

    private static readonly string associationKey = $"{ApplicationHelper.Name}.RestoreBackupFile";
    private static readonly string executable = ApplicationHelper.ApplicationAssembly.Location;
    private static readonly string executableCommand = $"\"{executable}\" ui \"%1\"";
    private static readonly string fileExtension = ".bak";

    #endregion

    #region Static Methods

    internal static bool IsAssociated()
    {
      using (var key = Registry.ClassesRoot.OpenSubKey(fileExtension))
      {
        var association = (string)key?.GetValue(string.Empty, string.Empty);
        if (associationKey != association)
          return false;
      }

      using (var key = Registry.ClassesRoot.OpenSubKey($@"{associationKey}\shell\open\command"))
      {
        var command = (string)key?.GetValue(string.Empty, string.Empty);
        if (executableCommand.Equals(command, StringComparison.InvariantCultureIgnoreCase) == false)
          return false;
      }

      return true;
    }

    internal static void SetAssociation()
    {
      using (var key = Registry.ClassesRoot.CreateSubKey(fileExtension))
      {
        key?.SetValue(string.Empty, associationKey, RegistryValueKind.String);
      }

      using (var key = Registry.ClassesRoot.CreateSubKey(associationKey))
      {
        key?.SetValue(string.Empty, "SQL Server Database Backup", RegistryValueKind.String);
        using (var subKey = key?.CreateSubKey("DefaultIcon"))
        {
          subKey?.SetValue(string.Empty, $"{executable}", RegistryValueKind.String);
        }
        using (var subKey = key?.CreateSubKey(@"shell\open\command"))
        {
          subKey?.SetValue(string.Empty, executableCommand, RegistryValueKind.String);
        }
      }
      SHChangeNotify(0x8000000, 0x0, IntPtr.Zero, IntPtr.Zero);
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    #endregion

  }
}
