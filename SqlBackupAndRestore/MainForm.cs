using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Sql;
using SqlBackupAndRestore.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  internal sealed partial class MainForm : Form
  {

    #region Fields

    private SqlConnectionInfo connectionInfo = new SqlConnectionInfo();

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
      ClientSize = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height);
      BackupPanel.Size = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height);
      configureSetFileAssociationOnLoad();
      loadSettings();
      initialiseAndRefreshSqlServer();
      refreshBackupAndRestoreDatabaseButtons();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      saveSettings();
    }

    #endregion

    #region Events

    private void RestoreMenuItem_Click(object sender, EventArgs e)
    {
      showBackupOrRestorePanel(true);
    }

    private void BackupMenuItem_Click(object sender, EventArgs e)
    {
      showBackupOrRestorePanel(false);
    }

    private void SetFileAssociationMenuItem_Click(object sender, EventArgs e)
    {
      setFileAssociation();
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show(this, "About message", Application.ProductName);
    }

    private void RestoreDestinationChangeSqlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      changeSqlServer();
    }

    private void BackupSourceChangeSqlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      changeSqlServer();
    }

    private void RestoreBrowseFileButton_Click(object sender, EventArgs e)
    {
      RestoreSourceFile.Text = browseFile(RestoreSourceFile.Text, true);
      refreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupBrowseFileButton_Click(object sender, EventArgs e)
    {
      BackupDestinationFile.Text = browseFile(BackupDestinationFile.Text, false);
      refreshBackupAndRestoreDatabaseButtons();
    }

    private void RestoreDestinationDatabaseList_TextChanged(object sender, EventArgs e)
    {
      refreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupSourceDatabaseList_TextChanged(object sender, EventArgs e)
    {
      refreshBackupAndRestoreDatabaseButtons();
    }

    private void RestoreSourceFile_Validated(object sender, EventArgs e)
    {
      refreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupDestinationFile_Validated(object sender, EventArgs e)
    {
      refreshBackupAndRestoreDatabaseButtons();
    }

    #endregion

    #region Helper Methods

    private void loadSettings()
    {
      connectionInfo.Server = Settings.Default.SqlServer;
      connectionInfo.IntegratedSecurity = Settings.Default.SqlIntegratedSecurity;
      connectionInfo.UserName = Settings.Default.SqlUserName;
      connectionInfo.Password = Settings.Default.SqlPassword;

      RestoreSourceFile.Text = Settings.Default.RestoreSourceFile;
      RestoreDestinationDatabaseList.Text = Settings.Default.RestoreDestinationDatabaseName;
      BackupDestinationFile.Text = Settings.Default.BackupDestinationFile;
      BackupSourceDatabaseList.Text = Settings.Default.BackupSourceDatabaseName;

      SetFileAssociationMenuItem.Enabled = FileAssociation.IsAssociated() == false;
    }

    private void saveSettings()
    {
      Settings.Default.SqlServer = connectionInfo.Server;
      Settings.Default.SqlIntegratedSecurity = connectionInfo.IntegratedSecurity;
      Settings.Default.SqlUserName = connectionInfo.UserName;
      Settings.Default.SqlPassword = connectionInfo.Password;

      Settings.Default.RestoreSourceFile = RestoreSourceFile.Text;
      Settings.Default.RestoreDestinationDatabaseName = RestoreDestinationDatabaseList.Text;
      Settings.Default.BackupDestinationFile = BackupDestinationFile.Text;
      Settings.Default.BackupSourceDatabaseName = BackupSourceDatabaseList.Text;
      Settings.Default.Save();
    }

    private void changeSqlServer()
    {
      using (var dlg = new SqlConnectionForm(connectionInfo))
      {
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          refreshSqlServerDetails();
          suggestRestoreDestinationDatabase();
          refreshBackupAndRestoreDatabaseButtons();
          saveSettings();
        }
      }
    }

    private void suggestBackupDesinationFile()
    {

    }

    private void suggestRestoreDestinationDatabase()
    {
      string sourceFile = string.Empty;
      List<string> databaseList = new List<string>();

      try
      {
        sourceFile = RestoreSourceFile.Text.Trim();
        databaseList = RestoreDestinationDatabaseList.Items.Cast<string>().ToList();

        if (string.IsNullOrWhiteSpace(sourceFile) == false && File.Exists(sourceFile))
          RestoreDestinationDatabaseList.Text = SqlRestore.GetSuggestedRestoreDatabaseName(connectionInfo, sourceFile, databaseList);
      }
      catch
      {
        RestoreDestinationDatabaseList.Text = string.Empty;
      }
    }

    private void initialiseAndRefreshSqlServer()
    {
      if (string.IsNullOrEmpty(connectionInfo.Server))
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
              throw new ApplicationException("No sql server found");
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
          connectionInfo = server;
          saveSettings();
        }
      }
      refreshSqlServerDetails();
    }

    private void refreshSqlServerDetails()
    {
      RestoreDestinationSqlServer.Text = connectionInfo.Server;
      BackupSourceSqlServer.Text = connectionInfo.Server;

      RestoreDestinationChangeSqlServer.Text = string.IsNullOrEmpty(connectionInfo.Server) ? "Connect" : "Change";
      BackupSourceChangeSqlServer.Text = RestoreDestinationChangeSqlServer.Text;

      var databases = string.IsNullOrEmpty(connectionInfo.Server) ? new object[0] : connectionInfo.GetDatabases().ToArray<object>();
      RestoreDestinationDatabaseList.Items.Clear();
      RestoreDestinationDatabaseList.Items.AddRange(databases);
      BackupSourceDatabaseList.Items.Clear();
      BackupSourceDatabaseList.Items.AddRange(databases);
    }

    private void refreshBackupAndRestoreDatabaseButtons()
    {
      RestoreDatabaseButton.Enabled = RestoreSourceFile.Text.Trim().Length > 0 &&
                                      File.Exists(RestoreSourceFile.Text.Trim()) &&
                                      RestoreDestinationDatabaseList.Text.Trim().Length > 0 &&
                                      connectionInfo.Server.Trim().Length > 0; //server should already be validated

      BackupDatabaseButton.Enabled = BackupDestinationFile.Text.Trim().Length > 0 &&
                                     Directory.Exists(Path.GetFullPath(BackupDestinationFile.Text.Trim())) &&
                                     BackupSourceDatabaseList.Text.Trim().Length > 0 &&
                                     connectionInfo.Server.Trim().Length > 0; //server should already be validated
    }

    private string browseFile(string fileName, bool isSource)
    {
      using (var dlg = new OpenFileDialog())
      {
        if (isSource)
        {
          dlg.CheckFileExists = true;
          dlg.Filter = "BAK or ZIP files|*.bak;*.zip|All files|*.*";
          dlg.Title = "Source Backup File";
        }
        else
        {
          dlg.CheckFileExists = false;
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

    private void configureSetFileAssociationOnLoad()
    {
      try
      {
        if (Settings.Default.AssociateBackupFiles && FileAssociation.IsAssociated())
        {
          if (MessageBox.Show(this, $"{Application.ProductName} is not associated with *.bak files. Would you like to create this association now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            setFileAssociation();
          }
          else
          {
            Settings.Default.AssociateBackupFiles = false;
            Settings.Default.Save();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"{ex.Message} {ex.InnerException?.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);

      }
    }

    private void setFileAssociation()
    {
      try
      {
        if (FileAssociation.IsAssociated() == false)
        {
          if (ApplicationHelper.IsAdministrator())
            FileAssociation.SetAssociation();
          else
            FileAssociation.RunAssociation();
        }

        SetFileAssociationMenuItem.Enabled = false;

        if (Settings.Default.AssociateBackupFiles)
        {
          Settings.Default.AssociateBackupFiles = false;
          Settings.Default.Save();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"{ex.Message} {ex.InnerException?.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void showBackupOrRestorePanel(bool showRestore)
    {
      SuspendLayout();
      RestorePanel.Visible = showRestore;
      RestoreMenuItem.Checked = showRestore;
      BackupPanel.Visible = showRestore == false;
      BackupMenuItem.Checked = showRestore == false;
      if (showRestore)
        RestorePanel.Location = new Point(0, 27);
      else
        BackupPanel.Location = new Point(0, 27);
      ResumeLayout();
    }

    #endregion

  }
}
