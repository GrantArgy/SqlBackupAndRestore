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

namespace SqlBackupAndRestore.Commands
{
  [Verb("restore", HelpText = "Restore local Sql Server database")]
  internal sealed class RestoreCommand : ICommand
  {

    [Option("localSqlServer", Required = true, HelpText = "Local Sql Server name")]
    public string SqlServer { get; set; }

    [Option('u', "userName", HelpText = "SQL Server username")]
    public string Username { get; set; }

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
        IntegratedSecurity = String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password),
        UserName = Username,
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

    }

    #endregion

    #region Helper Methods



    #endregion
  }
}
