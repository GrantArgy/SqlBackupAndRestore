using CommandLine;
using SqlBackupAndRestore.Sql;
using SqlBackupAndRestore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SqlBackupAndRestore.Commands
{
  [Verb("restore", HelpText = "Restore local Sql Server database")]
  internal sealed class RestoreCommand : ICommand
  {

    [Option('s', "localSqlServer", Required = true, HelpText = "Local Sql Server name")]
    public string SqlServer { get; set; }

    [Option('u', "userName", HelpText = "SQL Server username", Default = "")]
    public string UserName { get; set; }

    [Option('p', "password", HelpText = "SQL Server password", Default = "")]
    public string Password { get; set; }

    [Option('b', "backupFile", Required = true, HelpText = "Backup file")]
    public string BackupFile { get; set; }

    [Option('d', "database", Required = true, HelpText = "Database name")]
    public string Database { get; set; }

    private void setStatus(string message)
    {
      Console.WriteLine(message);
    }

    private void setProgress(int percent)
    {
      int max = 100;
      int progress = (int)((double)percent / max * 100);
      int numFilled = (int)((double)percent / max * 50);
      string progressBar = $"|{new string('=', numFilled)}{new string(' ', 50 - numFilled)}| {progress}%";
      Console.CursorVisible = false;
      Console.SetCursorPosition(0, Console.CursorTop);
      Console.Write(progressBar);
    }

    #region ICommand

    void ICommand.Execute()
    {
      var connectionInfo = new SqlConnectionInfo()
      {
        Server = SqlServer,
        IntegratedSecurity = String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password),
        UserName = UserName,
        Password = Password
      };
      string errorMessage;

      if (SqlConnectionInfo.IsLocalServer(SqlServer) == false)
      {
        Console.WriteLine("Not a valid local Sql server");
      }
      else if (connectionInfo.CanOpenConnection(out errorMessage, null) == false)
      {
        Console.WriteLine(errorMessage);
      }
      else if (File.Exists(BackupFile) == false)
      {
        Console.WriteLine("File does not exist");
      }
      else
      {
        Console.CursorVisible = false;
        Console.WriteLine("Restore starting!");
        var token = new CancellationToken();
        SqlRestore.RestoreAsync(connectionInfo, Database, BackupFile, setStatus, setProgress, token).Wait();
        Console.WriteLine("Restore completed!");
        Console.CursorVisible = true;
      }

    }

    #endregion

    #region Helper Methods



    #endregion
  }
}
