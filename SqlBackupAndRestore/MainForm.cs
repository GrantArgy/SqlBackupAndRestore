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
      ClientSize = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height);
      BackupPanel.Size = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height);
      AskToSetFileAssociationOnLoad();
      LoadSettings();
      InitialiseSqlServer();
      RefreshSqlServerDetails();
      RefreshBackupAndRestoreDatabaseButtons();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      SaveSettings();
    }

    #endregion

    #region Events

    private void RestoreMenuItem_Click(object sender, EventArgs e)
    {
      SuspendLayout();
      BackupPanel.Visible = false;
      RestorePanel.Visible = true;
      RestorePanel.Location = new Point(0, 27);
      RestoreMenuItem.Checked = true;
      BackupMenuItem.Checked = false;
      ResumeLayout();
    }

    private void BackupMenuItem_Click(object sender, EventArgs e)
    {
      SuspendLayout();
      RestorePanel.Visible = false;
      BackupPanel.Visible = true;
      BackupPanel.Location = new Point(0, 27);
      BackupMenuItem.Checked = true;
      RestoreMenuItem.Checked = false;
      ResumeLayout();
    }

    private void SetFileAssociationMenuItem_Click(object sender, EventArgs e)
    {
      FileAssociation.RunAssociation();
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
      RefreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupBrowseFileButton_Click(object sender, EventArgs e)
    {
      BackupDestinationFile.Text = BrowseFile(BackupDestinationFile.Text, false);
      RefreshBackupAndRestoreDatabaseButtons();
    }

    private void RestoreDestinationDatabaseList_TextChanged(object sender, EventArgs e)
    {
      RefreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupSourceDatabaseList_TextChanged(object sender, EventArgs e)
    {
      RefreshBackupAndRestoreDatabaseButtons();
    }

    private void RestoreSourceFile_Validated(object sender, EventArgs e)
    {
      RefreshBackupAndRestoreDatabaseButtons();
    }

    private void BackupDestinationFile_Validated(object sender, EventArgs e)
    {
      RefreshBackupAndRestoreDatabaseButtons();
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

      SetFileAssociationMenuItem.Enabled = FileAssociation.IsAssociated() == false;
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
                                      File.Exists(RestoreSourceFile.Text.Trim()) &&
                                      RestoreDestinationDatabaseList.Text.Trim().Length > 0 &&
                                      ConnectionInfo.Server.Trim().Length > 0;
      
      BackupDatabaseButton.Enabled = BackupDestinationFile.Text.Trim().Length > 0 &&
                                     Directory.Exists(Path.GetFullPath(BackupDestinationFile.Text.Trim())) &&
                                     BackupSourceDatabaseList.Text.Trim().Length > 0 &&
                                     ConnectionInfo.Server.Trim().Length > 0;
    }

    private string BrowseFile(string fileName, bool isSource)
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

    private void AskToSetFileAssociationOnLoad()
    {
      if (Settings.Default.AssociateBackupFiles)
      {
        try
        {
          if (FileAssociation.IsAssociated() == false)
          {
            if (MessageBox.Show(this, $"{Application.ProductName} is not associated with *.bak files. Would you like to create this association now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
              if (ApplicationDetails.IsAdministrator())
                FileAssociation.SetAssociation();
              else
                FileAssociation.RunAssociation();
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
    }


    #endregion

  }
}
