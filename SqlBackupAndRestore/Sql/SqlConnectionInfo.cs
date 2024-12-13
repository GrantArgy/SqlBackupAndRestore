using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace SqlBackupAndRestore.Sql
{

  internal sealed class SqlConnectionInfo
  {

    #region Properties

    public string Server { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool IntegratedSecurity { get; set; } = true;

    #endregion

    #region Constructors

    public SqlConnectionInfo() { }

    #endregion

    #region Methods

    public SqlConnection GetConnection(int? connectionTimeout)
    {
      var builder = new SqlConnectionStringBuilder()
      {
        InitialCatalog = "Master",
        DataSource = Server,
        UserID = UserName,
        Password = Password,
        IntegratedSecurity = IntegratedSecurity,
        ConnectTimeout = connectionTimeout ?? 15
      };

      var connection = new SqlConnection(builder.ConnectionString);
      connection.Open();
      return connection;
    }

    public bool IsSystemDatabase(string databaseName)
    {
      var dbName = databaseName.ToLower().Trim();
      return dbName == "master" || dbName == "model" || dbName == "msdb" || dbName == "tempdb";
    }

    public List<string> GetLocalSqlServers()
    {
      var lst = new List<string>();
      try
      {
        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");
        if (key != null)
          lst.AddRange(key.GetValueNames().Select(obj => obj.Equals("MSSQLSERVER") ? "." : @".\" + obj));
      }
      catch { }
      return lst;
    }

    public static bool IsLocalServer(string machine)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(machine)) return false;

        int length = machine.LastIndexOf('\\');
        if (length > 0)
        {
          machine = machine.Substring(0, length);
        }
        if (machine.Trim() == ".")
        {
          return true;
        }
        IPHostEntry hostEntry = Dns.GetHostEntry(machine);
        IPHostEntry entry2 = Dns.GetHostEntry(Environment.MachineName);
        return (hostEntry.HostName == entry2.HostName);
      }
      catch (Exception)
      {
        return false;
      }
    }

    public bool CanOpenConnection(out string error, int? connectionTimeout)
    {
      error = "";
      try
      {
        using (var cn = GetConnection(connectionTimeout)) { };
      }
      catch (Exception ex)
      {
        error = ex.Message;
        return false;
      }
      return true;
    }

    public List<string> GetDatabases()
    {
      List<string> lst = new List<string>();
      if (CanOpenConnection(out string error, 1) == false)
        return lst;
      try
      {
        using (var cn = GetConnection(5))
        {
          using (var cmd = cn.CreateCommand())
          {
            cmd.CommandTimeout = 10000;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_databases";

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
              while (dr.Read())
              {
                if (IsSystemDatabase(dr.GetString(0)) == false)
                  lst.Add(dr.GetString(0));
              }
            }
          }
        }
      }
      catch { }
      return lst;
    }

    #endregion

  }
}
