using System;
using System.Runtime.InteropServices;

namespace SqlBackupAndRestore.Utilities
{
  internal static class ConsoleManager
  {

    #region Static Methods

    internal static void EnterConsole()
    {
      AttachConsole(ATTACH_PARRENT);
    }

    internal static void ReleaseConsole()
    {
      FreeConsole();
    }

    #endregion

    #region DllImports

    private const UInt32 ATTACH_PARRENT = 0xFFFFFFFF;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int FreeConsole();

    [DllImport("kernel32", SetLastError = true)]
    private static extern bool AttachConsole(uint dwProcessId);

    #endregion
  }
}
