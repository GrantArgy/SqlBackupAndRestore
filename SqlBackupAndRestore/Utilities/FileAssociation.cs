using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SqlBackupAndRestore.Utilities
{
  internal static class FileAssociation
  {

    #region Private Variables

    private static Assembly ApplicationAssembly = Assembly.GetExecutingAssembly();
    private static string AssociationKey = $"{ApplicationAssembly.GetName().Name}.RestoreBackupFile";
    private static string ExecutableFile = ApplicationAssembly.Location;

    #endregion

    #region Static Methods

    internal static bool IsAssociated()
    {
      RegistryKey key = Registry.ClassesRoot.OpenSubKey(".bak");

      if (key == null)
        return false;

      if (key.GetValue(string.Empty, string.Empty).ToString() != AssociationKey)
      {
        key.Close();
        return false;
      }
      key.Close();

      key = Registry.ClassesRoot.OpenSubKey($@"{AssociationKey}\shell\open\command");

      if (key == null)
        return false;

      string str = $"{ExecutableFile} \"%1\"".ToLower();
      if (key.GetValue("", "").ToString().ToLower() != str)
      {
        key.Close();
        return false;
      }
      key.Close();

      return true;
    }

    internal static void SetAssociation()
    {
      RegistryKey key = Registry.ClassesRoot.CreateSubKey(".bak");

      if (key != null)
      {
        key.SetValue(string.Empty, AssociationKey, RegistryValueKind.String);
        key.Close();

        RegistryKey key2 = Registry.ClassesRoot.CreateSubKey(AssociationKey);
        if (key2 != null)
        {
          key2.SetValue(string.Empty, "SQL Server Database Backup", RegistryValueKind.String);

          key = key2.CreateSubKey("DefaultIcon");
          if (key != null)
          {
            key.SetValue(string.Empty, $"{ExecutableFile}", RegistryValueKind.String);
            key.Close();
            key = key2.CreateSubKey(@"shell\open\command");
            if (key != null)
            {
              key.SetValue(string.Empty, $"{ExecutableFile} \"%1\"", RegistryValueKind.String);
              key.Close();
              key2.Close();
            }
          }
        }
      }
    }

    internal static void RunAssociation()
    {
      try
      {
        Uri uri = new Uri(ApplicationAssembly.GetName().CodeBase);
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
          UseShellExecute = true,
          WorkingDirectory = Environment.CurrentDirectory,
          FileName = uri.LocalPath,
          Arguments = "fileassoc -q"
        };

        if (Environment.OSVersion.Version.Major >= 6)
        {
          startInfo.Verb = "runas";
        }

        Process.Start(startInfo).WaitForExit();
      }
      catch (Win32Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    #endregion

  }
}
