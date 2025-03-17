using SqlBackupAndRestore.Properties;
using SqlBackupAndRestore.Sql;
using SqlBackupAndRestore.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
      ClientSize = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height + RestorePanel.Location.Y);
      BackupPanel.Size = new Size(RestorePanel.Size.Width, RestorePanel.Size.Height);
      showBackupOrRestorePanel(true);
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
      using (var dlg = new AboutForm())
      {
        dlg.ShowDialog(this);
      }
    }

    private void ChangeSqlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

    private void DatabaseOrFile_Validated(object sender, EventArgs e)
    {
      if (sender is ComboBox cb && cb.Name == BackupSourceDatabaseList.Name)
        suggestBackupDesinationFile();
      else if (sender is TextBox tb && tb.Name == RestoreSourceFile.Name)
        suggestRestoreDestinationDatabase();
      refreshBackupAndRestoreDatabaseButtons();
    }

    private async void RestoreDatabaseButton_Click(object sender, EventArgs e)
    {
      await restoreDatabaseButton();
    }

    private async void BackupDatabaseButton_Click(object sender, EventArgs e)
    {
      await backupDatabaseButton();
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
      try
      {
        var file = BackupDestinationFile.Text.Trim();
        var database = BackupSourceDatabaseList.Text.Trim();

        BackupDestinationFile.Text = SqlBackup.GetSuggestedBackupFileName(connectionInfo, file, database);
      }
      catch
      {
        BackupDestinationFile.Text = string.Empty;
      }
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
                                     Directory.Exists(Path.GetDirectoryName(BackupDestinationFile.Text.Trim())) &&
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
          if (isSource)
            suggestRestoreDestinationDatabase();
          return dlg.FileName;
        }
        return fileName;
      }
    }

    private void configureSetFileAssociationOnLoad()
    {
      try
      {
        if (Settings.Default.AssociateBackupFiles && FileAssociation.IsAssociated() == false)
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
            runAssociation();
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

    private void runAssociation()
    {
      try
      {
        Uri uri = new Uri(ApplicationHelper.ApplicationAssembly.GetName().CodeBase);
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
          UseShellExecute = true,
          WorkingDirectory = Environment.CurrentDirectory,
          FileName = uri.LocalPath,
          Arguments = "qfa"
        };

        if (Environment.OSVersion.Version.Major >= 6)
        {
          startInfo.Verb = "runas";
        }

        Process.Start(startInfo).WaitForExit();
      }
      catch (Win32Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private bool backupNotRunning = true;
    private CancellationTokenSource backupCancellationTokenSource = null;

    private async Task backupDatabaseButton()
    {
      try
      {
        if (backupNotRunning)
        {
          BackupStatusLabel.Text = "";
          BackupProgress.Value = 0;
          BackupStatusLabel.Visible = true;
          BackupProgress.Visible = true;
          BackupDatabaseButton.Text = "Cancel";
          BackupDatabaseButton.Image = Resources.Cancel;
          backupCancellationTokenSource = new CancellationTokenSource();
          await SqlBackup.BackupAsync(connectionInfo, BackupSourceDatabaseList.Text.Trim(), BackupDestinationFile.Text, setBackupStatus, setBackupProgress, backupCancellationTokenSource.Token);
        }
        else
        {
          setRestoreStatus("Waiting to cancel...");
          restoreCancellationTokenSource.Cancel();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"{ex.Message} {ex.InnerException?.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        BackupDatabaseButton.Text = "Backup";
        BackupDatabaseButton.Image = Resources.Start;
        BackupStatusLabel.Visible = false;
        BackupProgress.Visible = false;
      }
    }

    private void setBackupStatus(string status)
    {
      if (BackupStatusLabel.InvokeRequired)
      {
        Action safewrite = delegate { setBackupStatus(status); };
        BackupStatusLabel.Invoke(safewrite);
      }
      else
      {
        BackupStatusLabel.Text = status;
      }
    }

    private void setBackupProgress(int percent)
    {
      if (BackupProgress.InvokeRequired)
      {
        Action safewrite = delegate { setBackupProgress(percent); };
        BackupProgress.Invoke(safewrite);
      }
      else
      {
        BackupProgress.Value = percent > 100 ? 100 : percent;
      }

    }

    private bool restoreNotRunning = true;
    private CancellationTokenSource restoreCancellationTokenSource = null;

    private async Task restoreDatabaseButton()
    {
      try
      {
        if (restoreNotRunning)
        {
          RestoreStatusLabel.Text = "";
          RestoreProgress.Value = 0;
          RestoreStatusLabel.Visible = true;
          RestoreProgress.Visible = true;
          RestoreDatabaseButton.Text = "Cancel";
          RestoreDatabaseButton.Image = Resources.Cancel;
          restoreCancellationTokenSource = new CancellationTokenSource();
          await SqlRestore.RestoreAsync(connectionInfo, RestoreDestinationDatabaseList.Text.Trim(), RestoreSourceFile.Text, setRestoreStatus, setRestoreProgress, restoreCancellationTokenSource.Token);
        }
        else
        {
          setRestoreStatus("Waiting to cancel...");
          restoreCancellationTokenSource.Cancel();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"{ex.Message} {ex.InnerException?.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        RestoreDatabaseButton.Text = "Restore";
        RestoreDatabaseButton.Image = Resources.Start;
        RestoreStatusLabel.Visible = false;
        RestoreProgress.Visible = false;
      }
    }

    private void setRestoreStatus(string status)
    {
      if (RestoreStatusLabel.InvokeRequired)
      {
        Action safewrite = delegate { setRestoreStatus(status); };
        RestoreStatusLabel.Invoke(safewrite);
      }
      else
      {
        RestoreStatusLabel.Text = status;
      }
    }

    private void setRestoreProgress(int percent)
    {
      if (RestoreProgress.InvokeRequired)
      {
        Action safewrite = delegate { setRestoreProgress(percent); };
        RestoreProgress.Invoke(safewrite);
      }
      else
      {
        RestoreProgress.Value = percent > 100 ? 100 : percent;
      }

    }

    #endregion
  }
}
