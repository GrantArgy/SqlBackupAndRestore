using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  public partial class MainForm : Form
  {

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

    private void ChangeSqlServer()
    {
      using (var dlg = new SqlConnectionForm())
      {
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
          //RefreshSqlServer();
          //SuggestDatabase();
          //SaveSettings();
        }
      }
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
