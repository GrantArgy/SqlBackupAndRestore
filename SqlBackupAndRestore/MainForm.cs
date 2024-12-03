using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  internal sealed partial class MainForm : Form
  {

    #region Properties

    private SqlConnectionInfo ConnectionInfo { get; set; } = new SqlConnectionInfo();

    #endregion

    #region Constructors

    public MainForm()
    {
      InitializeComponent();
    }

    #endregion

    #region Method Overrides

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      ClientSize = new System.Drawing.Size(458, 310);
      LoadSettings();
      InitialiseSqlServer();
      RefreshSqlServerDetails();
      RefreshBackupAndRestoreDatabaseButtons();
    }

    #endregion

    #region Events

    private void RestoreMenuItem_Click(object sender, EventArgs e)
    {
      BackupPanel.Visible = false;
      RestorePanel.Visible = true;
      RestorePanel.Location = new Point(0, 27);
      RestoreMenuItem.Checked = true;
      BackupMenuItem.Checked = false;
    }

    private void BackupMenuItem_Click(object sender, EventArgs e)
    {
      RestorePanel.Visible = false;
      BackupPanel.Visible = true;
      BackupPanel.Location = new Point(0, 27);
      BackupMenuItem.Checked = true;
      RestoreMenuItem.Checked = false;
    }

    private void SetFileAssociationMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show(this, "Set file Association", Application.ProductName);
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show(this, "About message", Application.ProductName);
    }

    private void RestoreDestinationChangeSqlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      ChangeSqlServer();
    }

    private void BackupSourceChangeSqlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      ChangeSqlServer();
    }

    private void RestoreBrowseFileButton_Click(object sender, EventArgs e)
    {
      RestoreSourceFile.Text = BrowseFile(RestoreSourceFile.Text, true);
    }

    private void BackupBrowseFileButton_Click(object sender, EventArgs e)
    {
      BackupDestinationFile.Text = BrowseFile(BackupDestinationFile.Text, false);
    }

    #endregion

    #region Helper Methods

    private void LoadSettings()
    {
      ConnectionInfo.Server = Settings.Default.SqlServer;
      ConnectionInfo.IntegratedSecurity = Settings.Default.SqlIntegratedSecurity;
      ConnectionInfo.UserName = Settings.Default.SqlUserName;
      ConnectionInfo.Password = Settings.Default.SqlPassword;

      RestoreSourceFile.Text = Settings.Default.RestoreSourceFile;
      RestoreDestinationDatabaseList.Text = Settings.Default.RestoreDestinationDatabaseName;
      BackupDestinationFile.Text = Settings.Default.BackupDestinationFile;
      BackupSourceDatabaseList.Text= Settings.Default.BackupSourceDatabaseName;
    }

    private void SaveSettings()
    {
      Settings.Default.SqlServer = ConnectionInfo.Server;
      Settings.Default.SqlIntegratedSecurity = ConnectionInfo.IntegratedSecurity;
      Settings.Default.SqlUserName = ConnectionInfo.UserName;
      Settings.Default.SqlPassword = ConnectionInfo.Password;

      Settings.Default.RestoreSourceFile = RestoreSourceFile.Text;
      Settings.Default.RestoreDestinationDatabaseName = RestoreDestinationDatabaseList.Text;
      Settings.Default.BackupDestinationFile = BackupDestinationFile.Text;
      Settings.Default.BackupSourceDatabaseName = BackupSourceDatabaseList.Text;
      Settings.Default.Save();
    }

    private void ChangeSqlServer()
    {
      using (var dlg = new SqlConnectionForm(ConnectionInfo))
      {
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          RefreshSqlServerDetails();
          SuggestRestoreDestinationDatabase();
          RefreshBackupAndRestoreDatabaseButtons();
          SaveSettings();
        }
      }
    }

    private void SuggestBackupDesinationFile()
    {

    }

    private void SuggestRestoreDestinationDatabase()
    {
      string sourceFile = string.Empty;
      List<string> databaseList = new List<string>();

      try
      {
        sourceFile = RestoreSourceFile.Text.Trim();
        databaseList = RestoreDestinationDatabaseList.Items.Cast<string>().ToList();

        if (string.IsNullOrWhiteSpace(sourceFile) == false && File.Exists(sourceFile))
          RestoreDestinationDatabaseList.Text = SqlRestore.GetSuggestedRestoreDatabaseName(ConnectionInfo, sourceFile, databaseList);
      }
      catch
      {
        RestoreDestinationDatabaseList.Text = string.Empty;
      }
    }

    private void InitialiseSqlServer()
    {
      if (string.IsNullOrEmpty(ConnectionInfo.Server))
      {
        string error = string.Empty;
        var server = new SqlConnectionInfo { IntegratedSecurity = true };
        try
        {
          Cursor.Current = Cursors.WaitCursor;
          server.Server = ".";
          if (server.CanOpenConnection(out error, 1) == false)
          {
            server.Server = @".\SQLEXPRESS";
            if (server.CanOpenConnection(out error, 1) == false)
            {
              throw new ApplicationException("no sql server found");
            }
          }
        }
        catch
        {
          server.Server = string.Empty;
        }
        finally
        {
          Cursor.Current = Cursors.Arrow;
        }
        if (string.IsNullOrEmpty(server.Server) == false)
        {
          ConnectionInfo = server;
          SaveSettings();
        }
      }
    }

    private void RefreshSqlServerDetails()
    {
      RestoreDestinationSqlServer.Text = ConnectionInfo.Server;
      BackupSourceSqlServer.Text = RestoreDestinationSqlServer.Text;

      RestoreDestinationChangeSqlServer.Text = string.IsNullOrEmpty(ConnectionInfo.Server) ? "Connect" : "Change";
      BackupSourceChangeSqlServer.Text = RestoreDestinationChangeSqlServer.Text;

      List<string> databases = string.IsNullOrEmpty(ConnectionInfo.Server) ? new List<string>() : ConnectionInfo.GetDatabases();
      RestoreDestinationDatabaseList.Items.Clear();
      RestoreDestinationDatabaseList.Items.AddRange(databases.Where(obj => ConnectionInfo.IsSystemDatabase(obj) == false).ToArray<object>());
      BackupSourceDatabaseList.Items.Clear();
      BackupSourceDatabaseList.Items.AddRange(databases.Where(obj => ConnectionInfo.IsSystemDatabase(obj) == false).ToArray<object>());
    }

    private void RefreshBackupAndRestoreDatabaseButtons()
    {
      RestoreDatabaseButton.Enabled = RestoreSourceFile.Text.Trim().Length > 0 &&
                                      Directory.Exists(Path.GetFullPath(BackupDestinationFile.Text.Trim())) &&
                                      RestoreDestinationDatabaseList.Text.Trim().Length > 0 &&
                                      ConnectionInfo.Server.Trim().Length > 0;
      
      BackupDatabaseButton.Enabled = BackupDestinationFile.Text.Trim().Length > 0 &&
                                     File.Exists(BackupDestinationFile.Text.Trim()) &&                            
                                     BackupSourceDatabaseList.Text.Trim().Length > 0 &&
                                     ConnectionInfo.Server.Trim().Length > 0;
    }

    private string BrowseFile(string fileName, bool isSource)
    {
      using (var dlg = new OpenFileDialog())
      {
        if (isSource)
        {
          dlg.Filter = "BAK or ZIP files|*.bak;*.zip|All files|*.*";
          dlg.Title = "Source Backup File";
        }
        else
        {
          dlg.Filter = "BAK files|*.bak|All files|*.*";
          dlg.Title = "Destination Backup File";
        }

        try { dlg.InitialDirectory = Path.GetDirectoryName(fileName); }
        catch { }

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          return dlg.FileName;
        }
        return fileName;
      }
    }

    #endregion

  }
}
