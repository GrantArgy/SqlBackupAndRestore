using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SqlBackupAndRestore.Sql
{
  internal static class SqlRestore
  {

    #region Static Methods

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
