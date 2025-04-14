using SqlBackupAndRestore.Sql;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SqlBackupAndRestore
{
  internal sealed partial class SqlConnectionForm : Form
  {

    #region Properties

    public SqlConnectionInfo ConnectionInfo { get; private set; }

    #endregion

    #region Constructors

    public SqlConnectionForm(SqlConnectionInfo connectionInfo)
    {
      InitializeComponent();
      ConnectionInfo = connectionInfo;
    }

    public SqlConnectionForm() : this(new SqlConnectionInfo()) { }

    #endregion

    #region Method Overrides

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      ServerList.Items.AddRange(ConnectionInfo.GetLocalSqlServers().ToArray<object>());

      ServerList.Text = ConnectionInfo.Server;
      UserName.Text = ConnectionInfo.UserName;
      Password.Text = ConnectionInfo.Password;
      WindowsAuthentication.Checked = ConnectionInfo.IntegratedSecurity;
      SqlServerAuthentication.Checked = ConnectionInfo.IntegratedSecurity == false;
    }

    #endregion

    #region Events

    private void TestConnectionButton_Click(object sender, EventArgs e)
    {
      TestSqlConnection(true);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (TestSqlConnection(false))
      {
        ConnectionInfo.Server = ServerList.Text?.Trim();
        ConnectionInfo.UserName = UserName.Text?.Trim();
        ConnectionInfo.Password = Password.Text?.Trim();
        ConnectionInfo.IntegratedSecurity = WindowsAuthentication.Checked;
        DialogResult = DialogResult.OK;
      }
    }

    private void SqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
    {
      UserNameLabel.Enabled = PasswordLabel.Enabled = UserName.Enabled = Password.Enabled = SqlServerAuthentication.Checked;
    }

    #endregion

    #region Helper Methods
    
    private bool TestSqlConnection(bool showSuccessMessage)
    {
      if (string.IsNullOrWhiteSpace(ServerList.Text))
        MessageBox.Show("Please selct a SQL Server", Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      if (SqlConnectionInfo.IsLocalServer(ServerList.Text?.Trim()) == false)
        MessageBox.Show("Sorry, you cannot restore to a remote SQL Server, only local. You have to run this tool on the same server where your target SQL Server is running.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      else if (IsValidSqlConnection(out string errorMessage))
      {
        if (showSuccessMessage) MessageBox.Show("Test connection succeeded", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        return true;
      }
      else
        MessageBox.Show(errorMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      
      return false;
    }

    private bool IsValidSqlConnection(out string errorMessage)
    {
      var server = new SqlConnectionInfo()
      {
        IntegratedSecurity = WindowsAuthentication.Checked,
        Server = ServerList.Text?.Trim(),
        UserName = UserName.Text?.Trim(),
        Password = Password.Text?.Trim()
      };

      Cursor.Current = Cursors.WaitCursor;
      errorMessage = "";
      bool flag = server.CanOpenConnection(out errorMessage, null);
      Cursor.Current = Cursors.Arrow;
      return flag;
    }

    #endregion
  }
}
