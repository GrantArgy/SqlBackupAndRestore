namespace SqlBackupAndRestore
{
  partial class SqlConnectionForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.TestConnectionButton = new System.Windows.Forms.Button();
      this.ServerCredentialsGroup = new System.Windows.Forms.GroupBox();
      this.Password = new System.Windows.Forms.TextBox();
      this.UserName = new System.Windows.Forms.TextBox();
      this.PasswordLabel = new System.Windows.Forms.Label();
      this.UserNameLabel = new System.Windows.Forms.Label();
      this.SqlServerAuthentication = new System.Windows.Forms.RadioButton();
      this.WindowsAuthentication = new System.Windows.Forms.RadioButton();
      this.ServerList = new System.Windows.Forms.ComboBox();
      this.ServerNameLabel = new System.Windows.Forms.Label();
      this.ServerCredentialsGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(340, 206);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(259, 206);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 10;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // TestConnectionButton
      // 
      this.TestConnectionButton.Location = new System.Drawing.Point(11, 206);
      this.TestConnectionButton.Name = "TestConnectionButton";
      this.TestConnectionButton.Size = new System.Drawing.Size(112, 23);
      this.TestConnectionButton.TabIndex = 9;
      this.TestConnectionButton.Text = "Test Connection";
      this.TestConnectionButton.UseVisualStyleBackColor = true;
      this.TestConnectionButton.Click += new System.EventHandler(this.TestConnectionButton_Click);
      // 
      // ServerCredentialsGroup
      // 
      this.ServerCredentialsGroup.Controls.Add(this.Password);
      this.ServerCredentialsGroup.Controls.Add(this.UserName);
      this.ServerCredentialsGroup.Controls.Add(this.PasswordLabel);
      this.ServerCredentialsGroup.Controls.Add(this.UserNameLabel);
      this.ServerCredentialsGroup.Controls.Add(this.SqlServerAuthentication);
      this.ServerCredentialsGroup.Controls.Add(this.WindowsAuthentication);
      this.ServerCredentialsGroup.Location = new System.Drawing.Point(10, 51);
      this.ServerCredentialsGroup.Name = "ServerCredentialsGroup";
      this.ServerCredentialsGroup.Size = new System.Drawing.Size(405, 149);
      this.ServerCredentialsGroup.TabIndex = 8;
      this.ServerCredentialsGroup.TabStop = false;
      this.ServerCredentialsGroup.Text = "Log on to the server";
      // 
      // Password
      // 
      this.Password.Enabled = false;
      this.Password.Location = new System.Drawing.Point(119, 99);
      this.Password.Name = "Password";
      this.Password.PasswordChar = '*';
      this.Password.Size = new System.Drawing.Size(278, 20);
      this.Password.TabIndex = 5;
      // 
      // UserName
      // 
      this.UserName.Enabled = false;
      this.UserName.Location = new System.Drawing.Point(119, 73);
      this.UserName.Name = "UserName";
      this.UserName.Size = new System.Drawing.Size(278, 20);
      this.UserName.TabIndex = 4;
      // 
      // PasswordLabel
      // 
      this.PasswordLabel.AutoSize = true;
      this.PasswordLabel.Enabled = false;
      this.PasswordLabel.Location = new System.Drawing.Point(57, 102);
      this.PasswordLabel.Name = "PasswordLabel";
      this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
      this.PasswordLabel.TabIndex = 3;
      this.PasswordLabel.Text = "Password:";
      // 
      // UserNameLabel
      // 
      this.UserNameLabel.AutoSize = true;
      this.UserNameLabel.Enabled = false;
      this.UserNameLabel.Location = new System.Drawing.Point(50, 76);
      this.UserNameLabel.Name = "UserNameLabel";
      this.UserNameLabel.Size = new System.Drawing.Size(63, 13);
      this.UserNameLabel.TabIndex = 2;
      this.UserNameLabel.Text = "User Name:";
      // 
      // SqlServerAuthentication
      // 
      this.SqlServerAuthentication.AutoSize = true;
      this.SqlServerAuthentication.Location = new System.Drawing.Point(31, 46);
      this.SqlServerAuthentication.Name = "SqlServerAuthentication";
      this.SqlServerAuthentication.Size = new System.Drawing.Size(173, 17);
      this.SqlServerAuthentication.TabIndex = 1;
      this.SqlServerAuthentication.Text = "Use SQL Server Authentication";
      this.SqlServerAuthentication.UseVisualStyleBackColor = true;
      this.SqlServerAuthentication.CheckedChanged += new System.EventHandler(this.SqlServerAuthentication_CheckedChanged);
      // 
      // WindowsAuthentication
      // 
      this.WindowsAuthentication.AutoSize = true;
      this.WindowsAuthentication.Checked = true;
      this.WindowsAuthentication.Location = new System.Drawing.Point(31, 23);
      this.WindowsAuthentication.Name = "WindowsAuthentication";
      this.WindowsAuthentication.Size = new System.Drawing.Size(162, 17);
      this.WindowsAuthentication.TabIndex = 0;
      this.WindowsAuthentication.TabStop = true;
      this.WindowsAuthentication.Text = "Use Windows Authentication";
      this.WindowsAuthentication.UseVisualStyleBackColor = true;
      // 
      // ServerList
      // 
      this.ServerList.FormattingEnabled = true;
      this.ServerList.Location = new System.Drawing.Point(86, 13);
      this.ServerList.Name = "ServerList";
      this.ServerList.Size = new System.Drawing.Size(329, 21);
      this.ServerList.TabIndex = 7;
      // 
      // ServerNameLabel
      // 
      this.ServerNameLabel.AutoSize = true;
      this.ServerNameLabel.Location = new System.Drawing.Point(8, 16);
      this.ServerNameLabel.Name = "ServerNameLabel";
      this.ServerNameLabel.Size = new System.Drawing.Size(72, 13);
      this.ServerNameLabel.TabIndex = 6;
      this.ServerNameLabel.Text = "Server Name:";
      // 
      // SqlConnectionForm
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(425, 235);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.TestConnectionButton);
      this.Controls.Add(this.ServerCredentialsGroup);
      this.Controls.Add(this.ServerList);
      this.Controls.Add(this.ServerNameLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SqlConnectionForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Connect to SQL Server";
      this.ServerCredentialsGroup.ResumeLayout(false);
      this.ServerCredentialsGroup.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button TestConnectionButton;
    private System.Windows.Forms.GroupBox ServerCredentialsGroup;
    private System.Windows.Forms.TextBox Password;
    private System.Windows.Forms.TextBox UserName;
    private System.Windows.Forms.Label PasswordLabel;
    private System.Windows.Forms.Label UserNameLabel;
    private System.Windows.Forms.RadioButton SqlServerAuthentication;
    private System.Windows.Forms.RadioButton WindowsAuthentication;
    private System.Windows.Forms.ComboBox ServerList;
    private System.Windows.Forms.Label ServerNameLabel;
  }
}