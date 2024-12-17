using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SqlBackupAndRestore.Sql
{
  internal static class SqlBackup
  {

    #region Static Methods

    public static async Task BackupAsync(SqlConnectionInfo connectionInfo, string databaseName, string backupFile, Action<string> setStatus, Action<int> updateProgress, CancellationToken token)
    {
      try
      {
        var parameters = new List<SqlParameter>();
        var restoreQuery = getBackupQuery(connectionInfo, backupFile, databaseName, parameters);
        setStatus($"Backing up {databaseName} database...");
        if (token.IsCancellationRequested) return;
        await backupDatabaseAsync(connectionInfo, databaseName, backupFile, restoreQuery, parameters, updateProgress);
        if (token.IsCancellationRequested) return;
      }
      finally
      {
        setStatus(string.Empty);
      }
    }

    public static string GetSuggestedBackupFileName(SqlConnectionInfo connectionInfo, string backupFile, string databaseName)
    {
      try
      {
        if (connectionInfo.DatabaseExists(databaseName) == false)
          return string.Empty;

        var fileName = Path.GetFileName(backupFile);
        string directory = "";

        if (string.IsNullOrWhiteSpace(backupFile) == false)
          directory = Path.GetDirectoryName(backupFile);

        if (Directory.Exists(directory) == false)
          directory = getSqlServerInstanceBackupDirectory(connectionInfo);

        if (string.IsNullOrWhiteSpace(fileName) || fileName.IndexOf(databaseName, StringComparison.InvariantCultureIgnoreCase) < 0)
          fileName = $"{DateTime.Today:yyyy-MM-dd}-{databaseName}.bak";

        return Path.Combine(directory, fileName);
      }
      catch
      {
        return backupFile;
      }
    }

    #endregion

    #region Helper Methods

    private static async Task backupDatabaseAsync(SqlConnectionInfo connectionInfo, string databaseName, string backupFile, string restoreQuery, List<SqlParameter> parameters, Action<int> updateProgress)
    {
      using (var cn = connectionInfo.GetConnection(10000))
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
          cmd.CommandText = restoreQuery;
          cmd.CommandType = CommandType.Text;
          cmd.Parameters.AddRange(parameters.ToArray());
          await cmd.ExecuteNonQueryAsync();
        }
      }
    }

    private static string getBackupQuery(SqlConnectionInfo connectionInfo, string backupFile, string databaseName, List<SqlParameter> parameters)
    {
      
      var builder = new StringBuilder();

      builder.Append($"BACKUP DATABASE [{databaseName}] TO DISK = @Path WITH NAME = 'Full Backup', COPY_ONLY, STATS = 10, COMPRESSION");
      parameters.Add(new SqlParameter("@Path", backupFile));

      return builder.ToString();
    }

    private static string getSqlServerInstanceBackupDirectory(SqlConnectionInfo connectionInfo)
    {
      using (var cn = connectionInfo.GetConnection(null))
      {
        using (var cmd = cn.CreateCommand())
        {
          cmd.CommandText = "SELECT SERVERPROPERTY('InstanceDefaultBackupPath');";
          cmd.CommandType = CommandType.Text;
          return (string)cmd.ExecuteScalar();
        }
      }
    }

    #endregion

  }
}
