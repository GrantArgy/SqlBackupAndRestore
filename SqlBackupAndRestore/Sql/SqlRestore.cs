using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Threading;
using SqlBackupAndRestore.Properties;

namespace SqlBackupAndRestore.Sql
{
  internal static class SqlRestore
  {

    #region Static Methods

    public static async Task RestoreAsync(SqlConnectionInfo connectionInfo, string databaseName, string backupFile, Action<string> setStatus, Action<int> updateProgress, CancellationToken token)
    {
      bool fromArchive = false;
      bool databaseExists = true;
      try
      {
        backupFile = captureBackupFile(connectionInfo, backupFile, out fromArchive, setStatus);
        if (token.IsCancellationRequested) return;
        var parameters = new List<SqlParameter>();
        var restoreQuery = getRestoreQuery(connectionInfo, backupFile, databaseName, parameters, out databaseExists);
        setStatus($"Restoring {databaseName} database...");
        if (token.IsCancellationRequested) return;
        if (databaseExists)
          await setDatabaseToOfflineModeAsync(connectionInfo, databaseName);
        if (token.IsCancellationRequested) return;
        await restoreDatabaseAsync(connectionInfo, databaseName, backupFile, restoreQuery, parameters, updateProgress);
        if (token.IsCancellationRequested) return;
      }
      finally
      {
        if (fromArchive && !string.IsNullOrEmpty(backupFile))
        {
          try { File.Delete(backupFile); }
          catch { }
        }
        setStatus(string.Empty);
      }
    }

    public static string GetSuggestedRestoreDatabaseName()
    {
      SqlConnectionInfo connectionInfo = new SqlConnectionInfo();
      connectionInfo.Server = Settings.Default.SqlServer;
      connectionInfo.IntegratedSecurity = Settings.Default.SqlIntegratedSecurity;
      connectionInfo.UserName = Settings.Default.SqlUserName;
      connectionInfo.Password = Settings.Default.SqlPassword;
      return GetSuggestedRestoreDatabaseName(connectionInfo, Settings.Default.RestoreSourceFile, connectionInfo.GetDatabases());
    }

    public static string GetSuggestedRestoreDatabaseName(SqlConnectionInfo connectionInfo, string backupFile, List<string> databaseList)
    {
      string databaseName = string.Empty;
      if (connectionInfo.CanOpenConnection(out string error, 1) == false)
        return databaseName;

      try
      {
        using (var cn = connectionInfo.GetConnection(null))
        {
          using (var cmd = cn.CreateCommand())
          {
            cmd.CommandTimeout = 10000;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"RESTORE HEADERONLY FROM DISK = {"@Path"}";
            cmd.Parameters.AddWithValue("@Path", backupFile);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
              if (dr.Read())
                databaseName = dr["DatabaseName"] as string;
            }
          }
        }
      }
      catch
      {
        databaseName = getSuggestedRestoreDatabaseNameByBackupFile(databaseList, backupFile);
      }

      return databaseName;
    }

    #endregion

    #region Helper Methods 

    private static async Task restoreDatabaseAsync(SqlConnectionInfo connectionInfo, string databaseName, string backupFile, string restoreQuery, List<SqlParameter> parameters, Action<int> updateProgress)
    {
      using (var cn = connectionInfo.GetConnection(null))
      {
        cn.FireInfoMessageEventOnUserErrors = true;
        cn.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
        {
          var match = Regex.Match(e.Message, @"(\d{1,3}) percent");
          if (match.Success)
            updateProgress(Convert.ToInt32(match.Value.Replace(" percent", "")));
        };

        using (var cmd = cn.CreateCommand())
        {
          cmd.CommandTimeout = 0;
          cmd.CommandText = restoreQuery;
          cmd.CommandType = CommandType.Text;
          cmd.Parameters.AddRange(parameters.ToArray());
          await cmd.ExecuteNonQueryAsync();
        }
      }
    }

    private static async Task setDatabaseToOfflineModeAsync(SqlConnectionInfo connectionInfo, string databaseName)
    {
      using (var cn = connectionInfo.GetConnection(null))
      {
        using (var cmd = cn.CreateCommand())
        {
          cmd.CommandText = $"ALTER DATABASE [{databaseName}] SET OFFLINE WITH ROLLBACK IMMEDIATE;";
          cmd.CommandType = CommandType.Text;
          await cmd.ExecuteNonQueryAsync();
        }
      }
    }

    private static string captureBackupFile(SqlConnectionInfo connectionInfo, string backupFile, out bool fromArchive, Action<string> setStatus)
    {
      fromArchive = false;
      if (Path.GetExtension(backupFile).ToLower() == ".zip")
      {
        string tmpBackupFile = null;
        try
        {
          setStatus($"Unzipping {Path.GetFileName(backupFile)}...");
          using (ZipArchive archive = ZipFile.OpenRead(backupFile))
          {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
              if (Path.GetExtension(entry.FullName).ToLower() == ".bak")
              {
                var path = Path.GetDirectoryName(backupFile) ?? Path.GetTempPath();
                if (!Directory.Exists(path))
                  Directory.CreateDirectory(path);

                entry.ExtractToFile(path);
                tmpBackupFile = Path.Combine(path, entry.Name);
                backupFile = tmpBackupFile;
                fromArchive = true;
              }
            }
          }
          setStatus($"Analyzing {Path.GetFileName(backupFile)}...");
          if (string.IsNullOrEmpty(tmpBackupFile))
          {
            throw new Exception("No file with .bak extension was found in the archive");
          }
        }
        catch (Exception exception)
        {
          throw new Exception("Can't unzip " + Path.GetFileName(backupFile) + ": " + exception.Message);
        }
      }
      return backupFile;
    }

    private static List<DatabaseFileInfo> GetBakupFiles(SqlConnectionInfo connectionInfo, string backupFile)
    {
      List<DatabaseFileInfo> lst = new List<DatabaseFileInfo>();

      using (var cn = connectionInfo.GetConnection(null))
      {
        using (var cmd = cn.CreateCommand())
        {
          cmd.CommandTimeout = 10000;
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = $"RESTORE FILELISTONLY FROM DISK='{backupFile.Replace("'", "''")}'";

          using (SqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              DatabaseFileInfo item = new DatabaseFileInfo
              {
                Name = dr.GetString(dr.GetOrdinal("LogicalName")),
                Path = dr.GetString(dr.GetOrdinal("PhysicalName"))
              };
              lst.Add(item);
            }
          }
        }
      }
      if (lst.Count == 0)
        throw new Exception("No database files found in the source backup file");
      return lst;
    }

    private static List<DatabaseFileInfo> GetDatabaseFiles(SqlConnectionInfo connectionInfo, string dbName)
    {
      List<DatabaseFileInfo> lst = new List<DatabaseFileInfo>();
      try
      {
        using (var cn = connectionInfo.GetConnection(null))
        {
          using (var cmd = cn.CreateCommand())
          {
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $@"SELECT fl.name, fl.filename
                               FROM 
                                      sys.sysdatabases db
                                    INNER JOIN 
                                      sys.sysaltfiles fl ON db.dbid = fl.dbid where db.name = '{dbName.Replace("'", "''")}'";

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
              while (dr.Read())
              {
                DatabaseFileInfo item = new DatabaseFileInfo
                {
                  Name = dr.GetString(dr.GetOrdinal("name")),
                  Path = dr.GetString(dr.GetOrdinal("filename"))
                };
                lst.Add(item);
              }
            }
          }
        }
      }
      catch { }
      return lst;
    }

    private static string getRestoreQuery(SqlConnectionInfo connectionInfo, string backupFile, string databaseName, List<SqlParameter> parameters, out bool databaseExists)
    {
      var sourceBackupFiles = GetBakupFiles(connectionInfo, backupFile);
      var destinationDatabaseFiles = GetDatabaseFiles(connectionInfo, databaseName);
      var builder = new StringBuilder();
      databaseExists = true;

      builder.Append($"RESTORE DATABASE [{databaseName}] FROM DISK = @Path WITH REPLACE, STATS = 10");
      parameters.Add(new SqlParameter("@Path", backupFile));

      int moveIdentifier = 0;

      if ((destinationDatabaseFiles.Count <= 0) || (destinationDatabaseFiles.Count > 2))
      {
        if (destinationDatabaseFiles.Count == 0)
        {
          databaseExists = false;
          List<DatabaseFileInfo> masterDatabaseFiles = GetDatabaseFiles(connectionInfo, "master");
          if (masterDatabaseFiles.Count > 0)
          {
            string masterDbPath = Path.GetDirectoryName(masterDatabaseFiles[0].Path) ?? Directory.GetCurrentDirectory();
            foreach (DatabaseFileInfo sourceFile in sourceBackupFiles)
            {
              var moveParam = $"@Move_{moveIdentifier}";
              var toParam = $"@To_{moveIdentifier}";
              builder.Append($", MOVE {moveParam} TO {toParam}");
              parameters.Add(new SqlParameter(moveParam, sourceFile.Name));
              var ps = (Path.GetExtension(sourceFile.Path) == ".ldf") ? "_log" : string.Empty;
              parameters.Add(new SqlParameter(toParam, $"{Path.Combine(masterDbPath, databaseName)}{ps}{Path.GetExtension(sourceFile.Path)}"));
              moveIdentifier++;
            }
          }
        }
      }
      else
      {
        foreach (DatabaseFileInfo sourceFile in sourceBackupFiles)
        {
          foreach (DatabaseFileInfo destinationFile in destinationDatabaseFiles)
          {
            if (Path.GetExtension(destinationFile.Path) == Path.GetExtension(sourceFile.Path))
            {
              var moveParam = $"@Move_{moveIdentifier}";
              var toParam = $"@To_{moveIdentifier}";
              builder.Append($", MOVE {moveParam} TO {toParam}");
              parameters.Add(new SqlParameter(moveParam, sourceFile.Name));
              parameters.Add(new SqlParameter(toParam, destinationFile.Path));
              moveIdentifier++;
            }
          }
        }
      }
      return builder.ToString();
    }

    private static string getSuggestedRestoreDatabaseNameByBackupFile(List<string> databaseList, string backupFile)
    {
      string databaseName = string.Empty;

      try
      {
        string fileName = Path.GetFileName(backupFile);
        var fileExtension = Path.GetExtension(fileName);

        string pattern = @"(?'db'.+?)(?'year'\d{4})(?'month'\d{2})(?'day'\d{2})(?'hour'\d{2})(?'minute'\d{2})(?'suffix'diff|log|logcopy|fullcopy)?\" + fileExtension;
        if (Regex.Match(fileName, pattern).Success == false)
          pattern = @"(?'db'.+?)(?'year'\d{4})(?'month'\d{2})(?'day'\d{2})(?'suffix'diff|log|logcopy|fullcopy)?\" + fileExtension;
        Match match = Regex.Match(fileName, pattern);

        if ((match.Success && (int.Parse(match.Groups["year"].Value) > 2007) && ((int.Parse(match.Groups["month"].Value) < 13) && (int.Parse(match.Groups["day"].Value) < 32))) && (((match.Groups["suffix"] == null) || string.IsNullOrEmpty(match.Groups["suffix"].Value)) || (match.Groups["suffix"].Value == "fullcopy")))
          databaseName = match.Groups["db"].Value;

        fileName = Path.GetFileNameWithoutExtension(fileName);
        if (string.IsNullOrWhiteSpace(databaseName))
          databaseName = databaseList.FirstOrDefault(obj => obj.ToLower() == fileName);

        if (string.IsNullOrWhiteSpace(databaseName))
          databaseName = databaseList.FirstOrDefault(obj => obj.ToLower().StartsWith(fileName));

      }
      catch { }

      return databaseName;
    }

    #endregion
  }
}
