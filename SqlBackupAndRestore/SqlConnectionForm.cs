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
  public partial class SqlConnectionForm : Form
  {

    #region Properties

    #endregion

    #region Constructors

    public SqlConnectionForm()
    {
      InitializeComponent();
    }

    #endregion

    #region Events

    private void TestConnectionButton_Click(object sender, EventArgs e)
    {

    }

    private void btnOK_Click(object sender, EventArgs e)
    {

    }

    private void SqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
    {
      UserNameLabel.Enabled = PasswordLabel.Enabled = UserName.Enabled = Password.Enabled = SqlServerAuthentication.Checked;
    }

    #endregion
  }
}
