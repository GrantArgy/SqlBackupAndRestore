using CommandLine;
using CommandLine.Text;
using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Utilities;
using System;
using System.Collections.Generic;

namespace SqlBackupAndRestore.Commands
{
  [Verb("fileassoc", HelpText = "Associates bak files with the executable to automatically load restore UI.")]
  internal sealed class FileAssociationCommand : ICommand
  {

    [Usage()]
    public static IEnumerable<Example> Examples
    {
      get
      {
        return new List<Example>() {
          new Example("Associate bak files", new RestoreCommand())
        };
      }
    }

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
