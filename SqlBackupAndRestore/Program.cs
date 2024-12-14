using CommandLine;
using SqlBackupAndRestore.Commands;
using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  internal static class Program
  {
    private static bool isUI = true;

    [STAThread]
    static void Main(string[] args)
    {
      //Quietly set file association call from UI
      if (args.Length == 1 && args[0] == "qfa")
      {
        if (ApplicationHelper.IsAdministrator())
          FileAssociation.SetAssociation();
        Environment.Exit(0);
      }

      //Handles double click of backup file as set by file association
      if (args.Length == 2 && args[0] == "ui" && File.Exists(args[1]))
      {
        Settings.Default.RestoreSourceFile = args[1];
        Settings.Default.Save();
      }
      else if (args.Length != 0)
      {
        isUI = false;
      }

      if (isUI)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
      }
      else
      {
        ConsoleManager.EnterConsole();
        ExecuteCommand(args);
        SendKeys.SendWait("{ENTER}");
        ConsoleManager.ReleaseConsole();
        Environment.Exit(0);
      }

    }

    private static void ExecuteCommand(string[] args)
    {
      var types = LoadVerbs();
      var result = Parser.Default.ParseArguments(args, types);
      result.WithParsed<ICommand>(o =>
      {
        o.Execute();
      });
    }

    private static Type[] LoadVerbs()
    {
      return ApplicationHelper.ApplicationAssembly.GetTypes()
        .Where(t => t.GetCustomAttribute<VerbAttribute>() != null && t.GetInterface(nameof(ICommand)) != null)
        .ToArray();
    }
  }
}
