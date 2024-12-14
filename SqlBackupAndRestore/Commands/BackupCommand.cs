using CommandLine;
using SqlBackupAndRestore.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SqlBackupAndRestore.Commands
{
  [Verb("backup", HelpText = "Backup local Sql Server database")]
  internal sealed class BackupCommand : ICommand
  {

    [Option('s', "localSqlServer", Required = true, HelpText = "Local Sql Server name")]
    public string SqlServer { get; set; }

    [Option('u', "userName", HelpText = "SQL Server username")]
    public string UserName { get; set; }

    [Option('p', "password", HelpText = "SQL Server password")]
    public string Password { get; set; }

    [Option('b', "backupFile", Required = true, HelpText = "Backup file")]
    public string BackupFile { get; set; }

    [Option('d', "database", Required = true, HelpText = "Database name")]
    public string Database { get; set; }

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
      else if (connectionInfo.GetDatabases().Any(obj => obj.ToLower() == Database) == false)
      {
        Console.WriteLine("Database does not exist");
      }
      else if (Directory.Exists(Path.GetFullPath(BackupFile)) == false)
      {
        Console.WriteLine("Directory of backup file does not exist");
      }
      else
      {
        //Backup
      }
    }

    #endregion
  }
}
