using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      ClientSize = new System.Drawing.Size(458, 310);
    }

    private void restoreMenuItem_Click(object sender, EventArgs e)
    {
      BackupPanel.Visible = false;
      RestorePanel.Visible = true;
      RestorePanel.Location = new Point(0, 27);
      restoreMenuItem.Checked = true;
      backupMenuItem.Checked = false;
    }

    private void backupMenuItem_Click(object sender, EventArgs e)
    {
      RestorePanel.Visible = false;
      BackupPanel.Visible = true;
      BackupPanel.Location = new Point(0, 27);
      backupMenuItem.Checked = true;
      restoreMenuItem.Checked = false;
    }
  }
}
