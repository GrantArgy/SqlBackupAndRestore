using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlBackupAndRestore.Commands
{
  [Verb("backup", HelpText = "Backup local Sql Server database")]
  internal sealed class BackupCommand : ICommand
  {

    [Option('s', "localSqlServer", Required = true, HelpText = "Local Sql Server name")]
    public string SqlServer { get; set; }

    [Option('u', "userName", HelpText = "SQL Server username")]
    public string Login { get; set; }

    [Option('p', "password", HelpText = "SQL Server password")]
    public string Password { get; set; }

    [Option('b', "backupFile", Required = true, HelpText = "Backup file")]
    public string BackupFile { get; set; }

    [Option('d', "database", Required = true, HelpText = "Database name")]
    public string Database { get; set; }

    #region ICommand

    void ICommand.Execute()
    {

    }

    #endregion
  }
}
