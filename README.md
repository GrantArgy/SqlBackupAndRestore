# SqlBackupAndRestore

SqlBackuAndRestore is a windows application that easily backs up and restores a local SQL server. It is quick and easy to learn. When you open the first time you will get prompted to associate .bak files and if you accept you will be able to double click the *.bak file which will launch the application automatically for you to use.

It also has a command line to perform all the same functionality.

## Features
- [x] Local backups of SQL databases
- [x] Local restore of databases
- [x] Command line backup of databases
- [x] Command line restore of databases

## Getting Started

Simply install the application

### UI Usage

On first load the application will ask if you want to associate .bak files with this application. I suggest you do as it makes restoring an SQL database alot easier simply by double clicking the .bak file. You will be presented with the UI to check details and you still need to manually restore from that point, it doesn't automatically do it for good reason.

### Command Line

```cmd
SqlBackupAndRestore.exe help
```

```text
  backup       Backup local Sql Server database

  fileassoc    Associates bak files with the executable to automatically load restore UI.

  restore      Restore local Sql Server database

  help         Display more information on a specific command.

  version      Display version information.
```

```cmd
SqlBackupAndRestore.exe backup
```

```text
  -s, --localSqlServer    Required. Local Sql Server name

  -u, --userName          (Default: ) SQL Server username

  -p, --password          (Default: ) SQL Server password

  -b, --backupFile        Required. Backup file

  -d, --database          Required. Database name
```


```cmd
SqlBackupAndRestore.exe restore
```

```text
  -s, --localSqlServer    Required. Local Sql Server name

  -u, --userName          (Default: ) SQL Server username

  -p, --password          (Default: ) SQL Server password

  -b, --backupFile        Required. Backup file

  -d, --database          Required. Database name

  --help                  Display this help screen.

  --version               Display version information.
```

