using CommandLine;
using CommandLine.Text;
using SqlBackupAndRestore.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace SqlBackupAndRestore.Commands
{
  [Verb("backup", HelpText = "Backup local Sql Server database")]
  internal sealed class BackupCommand : ICommand
  {

    [Option('s', "localSqlServer", Required = true, HelpText = "Local Sql Server name")]
    public string SqlServer { get; set; }

    [Option('u', "userName", HelpText = "SQL Server username", Default = "")]
    public string UserName { get; set; }

    [Option('p', "password", HelpText = "SQL Server password", Default ="")]
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

    [Usage()]
    public static IEnumerable<Example> Examples
    {
      get
      {
        return new List<Example>() {
          new Example("Backup a database with windows authentication", new BackupCommand() { SqlServer = "MyLocalSQLServer", BackupFile = "MyDatabase.bak", Database = "MyDatabase"}),
          new Example("Backup a database with Sql authentication", new BackupCommand() { SqlServer = "MyLocalSQLServer", BackupFile = "MyDatabase.bak", Database = "MyDatabase", UserName = "user", Password = "password"})
        };
      }
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
      else if (connectionInfo.DatabaseExists(Database) == false)
      {
        Console.WriteLine("Database does not exist");
      }
      else if (Directory.Exists(Path.GetDirectoryName(BackupFile)) == false)
      {
        Console.WriteLine("Directory of backup file does not exist");
      }
      else
      {
        Console.CursorVisible = false;
        Console.WriteLine("Backup starting!");
        var token = new CancellationToken();
        SqlBackup.BackupAsync(connectionInfo, Database, BackupFile, setStatus, setProgress, token).Wait();
        Console.WriteLine("Backup completed!");
        Console.CursorVisible = true;
      }
    }

    #endregion
  }
}
