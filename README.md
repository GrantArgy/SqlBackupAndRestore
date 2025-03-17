# SqlBackupAndRestore

SqlBackuAndRestore is a windows application that easily backs up and restores a local SQL server database. It is quick and easy to learn. When you open the first time you will get prompted to associate .bak files and if you accept you will be able to double click the *.bak file which will launch the application automatically for you to use.

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

The restore screen will appear as the default screen. You then setup your Sql connection info by clicking the change link and entering the relevant information. Once done you choose the file which can be a .bak file of a .zip file so long as the zip file has the .bak file inside. Once done this will determine the name of the database but you can change if required. Once ready simply hit the restore button. It will overwrite the database if it exists.

<img src="/images/RestoreBlank.png" alt="Restore Screen"/>

The backup screen can be selected from the menu. You then setup your Sql connection info by clicking the change link and entering the relevant information. Once done you choose the database which you wish to backup. Once done this will determine the name of the backup file based upon the database name and date, but you can change to whatever you like. Once ready simply hit the backup button. It will overwrite the database if it exists.

<img src="/images/BackupBlank.png" alt="Backup Screen"/>

The sql connect screen allows you to setup to any local sql server with relevant credentials. Once chosen this is then set as the default connection info when using the application, but can be changed anytime.

<img src="/images/SqlConnect.png" alt="Sql Connect Screen"/>

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
  Backup a database with windows authentication:
    SqlBackupAndRestore backup --backupFile MyDatabase.bak --database MyDatabase --localSqlServer MyLocalSQLServer
  Backup a database with Sql authentication:
    SqlBackupAndRestore backup --backupFile MyDatabase.bak --database MyDatabase --password password --localSqlServer MyLocalSQLServer --userName user

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
  USAGE:
  Restore a database with windows authentication:
    SqlBackupAndRestore restore --backupFile MyDatabase.bak --database MyDatabase --localSqlServer MyLocalSQLServer
  Restore a database with Sql authentication:
    SqlBackupAndRestore restore --backupFile MyDatabase.bak --database MyDatabase --password password --localSqlServer MyLocalSQLServer --userName user

  -s, --localSqlServer    Required. Local Sql Server name

  -u, --userName          (Default: ) SQL Server username

  -p, --password          (Default: ) SQL Server password

  -b, --backupFile        Required. Backup file

  -d, --database          Required. Database name

  --help                  Display this help screen.

  --version               Display version information.
```

