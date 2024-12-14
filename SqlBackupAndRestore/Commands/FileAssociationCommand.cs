using CommandLine;
using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Utilities;
using System;

namespace SqlBackupAndRestore.Commands
{
  [Verb("fileassoc", HelpText = "Associates bak files with the executable to automatically load restore UI.")]
  internal sealed class FileAssociationCommand : ICommand
  {

    #region ICommand

    void ICommand.Execute()
    {
      if (FileAssociation.IsAssociated())
        Console.WriteLine(".bak files are already associated.");
      else if (ApplicationHelper.IsAdministrator())
        FileAssociation.SetAssociation();
      else
        Console.WriteLine("Console must be running as administrator to perform this command!");
    }

    #endregion
  }
}
