namespace SqlBackupAndRestore
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.MainMenu = new System.Windows.Forms.MenuStrip();
      this.CommandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.BackupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.RestoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SetFileAssociationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.RestorePanel = new System.Windows.Forms.Panel();
      this.RestoreStatusLabel = new System.Windows.Forms.Label();
      this.RestoreProgress = new System.Windows.Forms.ProgressBar();
      this.RestoreDatabaseButton = new System.Windows.Forms.Button();
      this.REstoreSourceGroup = new System.Windows.Forms.GroupBox();
      this.RestoreBrowseFileButton = new System.Windows.Forms.Button();
      this.RestoreSourceFile = new System.Windows.Forms.TextBox();
      this.RestoreSourceLabel = new System.Windows.Forms.Label();
      this.RestoreDestinationGroup = new System.Windows.Forms.GroupBox();
      this.RestoreDestinationDatabaseList = new System.Windows.Forms.ComboBox();
      this.RestoreDestinationChangeSqlServer = new System.Windows.Forms.LinkLabel();
      this.RestoreDestinationSqlServer = new System.Windows.Forms.Label();
      this.RestoreDestinationDatabaseLabel = new System.Windows.Forms.Label();
      this.RestoreDestinationSqlServerLabel = new System.Windows.Forms.Label();
      this.RestoreHeaderPanel = new System.Windows.Forms.Panel();
      this.RestoreMessageLabel = new System.Windows.Forms.Label();
      this.RestoreHeaderLabel = new System.Windows.Forms.Label();
      this.BackupPanel = new System.Windows.Forms.Panel();
      this.BackupStatusLabel = new System.Windows.Forms.Label();
      this.BackupProgress = new System.Windows.Forms.ProgressBar();
      this.BackupDatabaseButton = new System.Windows.Forms.Button();
      this.BackupDestinationGroup = new System.Windows.Forms.GroupBox();
      this.BackupBrowseFileButton = new System.Windows.Forms.Button();
      this.BackupDestinationFile = new System.Windows.Forms.TextBox();
      this.BackupDestinationLabel = new System.Windows.Forms.Label();
      this.BackupSourceGroup = new System.Windows.Forms.GroupBox();
      this.BackupSourceDatabaseList = new System.Windows.Forms.ComboBox();
      this.BackupSourceChangeSqlServer = new System.Windows.Forms.LinkLabel();
      this.BackupSourceSqlServer = new System.Windows.Forms.Label();
      this.BackupSourceDatabaseLabel = new System.Windows.Forms.Label();
      this.BackupSourceSqlServerLabel = new System.Windows.Forms.Label();
      this.BackupHeaderPanel = new System.Windows.Forms.Panel();
      this.BackupMessageLabel = new System.Windows.Forms.Label();
      this.BackupHeaderLabel = new System.Windows.Forms.Label();
      this.MainMenu.SuspendLayout();
      this.RestorePanel.SuspendLayout();
      this.REstoreSourceGroup.SuspendLayout();
      this.RestoreDestinationGroup.SuspendLayout();
      this.RestoreHeaderPanel.SuspendLayout();
      this.BackupPanel.SuspendLayout();
      this.BackupDestinationGroup.SuspendLayout();
      this.BackupSourceGroup.SuspendLayout();
      this.BackupHeaderPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainMenu
      // 
      this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CommandMenuItem,
            this.HelpMenuItem});
      this.MainMenu.Location = new System.Drawing.Point(0, 0);
      this.MainMenu.Name = "MainMenu";
      this.MainMenu.Size = new System.Drawing.Size(1029, 24);
      this.MainMenu.TabIndex = 0;
      this.MainMenu.Text = "menuStrip1";
      // 
      // CommandMenuItem
      // 
      this.CommandMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackupMenuItem,
            this.RestoreMenuItem,
            this.SetFileAssociationMenuItem});
      this.CommandMenuItem.Name = "CommandMenuItem";
      this.CommandMenuItem.Size = new System.Drawing.Size(81, 20);
      this.CommandMenuItem.Text = "Commands";
      // 
      // BackupMenuItem
      // 
      this.BackupMenuItem.Name = "BackupMenuItem";
      this.BackupMenuItem.Size = new System.Drawing.Size(180, 22);
      this.BackupMenuItem.Text = "Backup";
      this.BackupMenuItem.Click += new System.EventHandler(this.BackupMenuItem_Click);
      // 
      // RestoreMenuItem
      // 
      this.RestoreMenuItem.Checked = true;
      this.RestoreMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.RestoreMenuItem.Name = "RestoreMenuItem";
      this.RestoreMenuItem.Size = new System.Drawing.Size(180, 22);
      this.RestoreMenuItem.Text = "Restore";
      this.RestoreMenuItem.Click += new System.EventHandler(this.RestoreMenuItem_Click);
      // 
      // SetFileAssociationMenuItem
      // 
      this.SetFileAssociationMenuItem.Name = "SetFileAssociationMenuItem";
      this.SetFileAssociationMenuItem.Size = new System.Drawing.Size(180, 22);
      this.SetFileAssociationMenuItem.Text = "Set File Association";
      this.SetFileAssociationMenuItem.Click += new System.EventHandler(this.SetFileAssociationMenuItem_Click);
      // 
      // HelpMenuItem
      // 
      this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
      this.HelpMenuItem.Name = "HelpMenuItem";
      this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
      this.HelpMenuItem.Text = "Help";
      // 
      // AboutMenuItem
      // 
      this.AboutMenuItem.Name = "AboutMenuItem";
      this.AboutMenuItem.Size = new System.Drawing.Size(180, 22);
      this.AboutMenuItem.Text = "About";
      this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
      // 
      // RestorePanel
      // 
      this.RestorePanel.Controls.Add(this.RestoreStatusLabel);
      this.RestorePanel.Controls.Add(this.RestoreProgress);
      this.RestorePanel.Controls.Add(this.RestoreDatabaseButton);
      this.RestorePanel.Controls.Add(this.REstoreSourceGroup);
      this.RestorePanel.Controls.Add(this.RestoreDestinationGroup);
      this.RestorePanel.Controls.Add(this.RestoreHeaderPanel);
      this.RestorePanel.Location = new System.Drawing.Point(0, 27);
      this.RestorePanel.Name = "RestorePanel";
      this.RestorePanel.Size = new System.Drawing.Size(458, 283);
      this.RestorePanel.TabIndex = 1;
      // 
      // RestoreStatusLabel
      // 
      this.RestoreStatusLabel.AutoSize = true;
      this.RestoreStatusLabel.Location = new System.Drawing.Point(25, 243);
      this.RestoreStatusLabel.Name = "RestoreStatusLabel";
      this.RestoreStatusLabel.Size = new System.Drawing.Size(0, 13);
      this.RestoreStatusLabel.TabIndex = 12;
      this.RestoreStatusLabel.Visible = false;
      // 
      // RestoreProgress
      // 
      this.RestoreProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreProgress.Location = new System.Drawing.Point(21, 259);
      this.RestoreProgress.Name = "RestoreProgress";
      this.RestoreProgress.Size = new System.Drawing.Size(297, 10);
      this.RestoreProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.RestoreProgress.TabIndex = 11;
      this.RestoreProgress.Visible = false;
      // 
      // RestoreDatabaseButton
      // 
      this.RestoreDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreDatabaseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.RestoreDatabaseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDatabaseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.RestoreDatabaseButton.Location = new System.Drawing.Point(324, 243);
      this.RestoreDatabaseButton.Name = "RestoreDatabaseButton";
      this.RestoreDatabaseButton.Size = new System.Drawing.Size(123, 31);
      this.RestoreDatabaseButton.TabIndex = 10;
      this.RestoreDatabaseButton.Text = "Restore";
      this.RestoreDatabaseButton.UseVisualStyleBackColor = true;
      // 
      // REstoreSourceGroup
      // 
      this.REstoreSourceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.REstoreSourceGroup.Controls.Add(this.RestoreBrowseFileButton);
      this.REstoreSourceGroup.Controls.Add(this.RestoreSourceFile);
      this.REstoreSourceGroup.Controls.Add(this.RestoreSourceLabel);
      this.REstoreSourceGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.REstoreSourceGroup.Location = new System.Drawing.Point(12, 77);
      this.REstoreSourceGroup.Name = "REstoreSourceGroup";
      this.REstoreSourceGroup.Size = new System.Drawing.Size(435, 57);
      this.REstoreSourceGroup.TabIndex = 9;
      this.REstoreSourceGroup.TabStop = false;
      this.REstoreSourceGroup.Text = "Source (Backup)";
      // 
      // RestoreBrowseFileButton
      // 
      this.RestoreBrowseFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreBrowseFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreBrowseFileButton.Location = new System.Drawing.Point(392, 20);
      this.RestoreBrowseFileButton.Name = "RestoreBrowseFileButton";
      this.RestoreBrowseFileButton.Size = new System.Drawing.Size(36, 24);
      this.RestoreBrowseFileButton.TabIndex = 2;
      this.RestoreBrowseFileButton.Text = "...";
      this.RestoreBrowseFileButton.UseVisualStyleBackColor = true;
      this.RestoreBrowseFileButton.Click += new System.EventHandler(this.RestoreBrowseFileButton_Click);
      // 
      // RestoreSourceFile
      // 
      this.RestoreSourceFile.AcceptsReturn = true;
      this.RestoreSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreSourceFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.RestoreSourceFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.RestoreSourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreSourceFile.Location = new System.Drawing.Point(84, 23);
      this.RestoreSourceFile.Name = "RestoreSourceFile";
      this.RestoreSourceFile.Size = new System.Drawing.Size(302, 20);
      this.RestoreSourceFile.TabIndex = 1;
      // 
      // RestoreSourceLabel
      // 
      this.RestoreSourceLabel.AutoSize = true;
      this.RestoreSourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreSourceLabel.Location = new System.Drawing.Point(6, 26);
      this.RestoreSourceLabel.Name = "RestoreSourceLabel";
      this.RestoreSourceLabel.Size = new System.Drawing.Size(72, 13);
      this.RestoreSourceLabel.TabIndex = 0;
      this.RestoreSourceLabel.Text = "File (bak, zip):";
      // 
      // RestoreDestinationGroup
      // 
      this.RestoreDestinationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreDestinationGroup.Controls.Add(this.RestoreDestinationDatabaseList);
      this.RestoreDestinationGroup.Controls.Add(this.RestoreDestinationChangeSqlServer);
      this.RestoreDestinationGroup.Controls.Add(this.RestoreDestinationSqlServer);
      this.RestoreDestinationGroup.Controls.Add(this.RestoreDestinationDatabaseLabel);
      this.RestoreDestinationGroup.Controls.Add(this.RestoreDestinationSqlServerLabel);
      this.RestoreDestinationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationGroup.Location = new System.Drawing.Point(12, 148);
      this.RestoreDestinationGroup.Name = "RestoreDestinationGroup";
      this.RestoreDestinationGroup.Size = new System.Drawing.Size(435, 89);
      this.RestoreDestinationGroup.TabIndex = 8;
      this.RestoreDestinationGroup.TabStop = false;
      this.RestoreDestinationGroup.Text = "Destination (Database)";
      // 
      // RestoreDestinationDatabaseList
      // 
      this.RestoreDestinationDatabaseList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreDestinationDatabaseList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.RestoreDestinationDatabaseList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.RestoreDestinationDatabaseList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationDatabaseList.FormattingEnabled = true;
      this.RestoreDestinationDatabaseList.Location = new System.Drawing.Point(84, 50);
      this.RestoreDestinationDatabaseList.Name = "RestoreDestinationDatabaseList";
      this.RestoreDestinationDatabaseList.Size = new System.Drawing.Size(344, 21);
      this.RestoreDestinationDatabaseList.TabIndex = 5;
      // 
      // RestoreDestinationChangeSqlServer
      // 
      this.RestoreDestinationChangeSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.RestoreDestinationChangeSqlServer.AutoSize = true;
      this.RestoreDestinationChangeSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationChangeSqlServer.Location = new System.Drawing.Point(384, 25);
      this.RestoreDestinationChangeSqlServer.Name = "RestoreDestinationChangeSqlServer";
      this.RestoreDestinationChangeSqlServer.Size = new System.Drawing.Size(44, 13);
      this.RestoreDestinationChangeSqlServer.TabIndex = 4;
      this.RestoreDestinationChangeSqlServer.TabStop = true;
      this.RestoreDestinationChangeSqlServer.Text = "Change";
      this.RestoreDestinationChangeSqlServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RestoreDestinationChangeSqlServer_LinkClicked);
      // 
      // RestoreDestinationSqlServer
      // 
      this.RestoreDestinationSqlServer.AutoSize = true;
      this.RestoreDestinationSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationSqlServer.Location = new System.Drawing.Point(81, 25);
      this.RestoreDestinationSqlServer.Name = "RestoreDestinationSqlServer";
      this.RestoreDestinationSqlServer.Size = new System.Drawing.Size(86, 13);
      this.RestoreDestinationSqlServer.TabIndex = 2;
      this.RestoreDestinationSqlServer.Text = ".\\SQLEXPRESS";
      // 
      // RestoreDestinationDatabaseLabel
      // 
      this.RestoreDestinationDatabaseLabel.AutoSize = true;
      this.RestoreDestinationDatabaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationDatabaseLabel.Location = new System.Drawing.Point(22, 53);
      this.RestoreDestinationDatabaseLabel.Name = "RestoreDestinationDatabaseLabel";
      this.RestoreDestinationDatabaseLabel.Size = new System.Drawing.Size(56, 13);
      this.RestoreDestinationDatabaseLabel.TabIndex = 1;
      this.RestoreDestinationDatabaseLabel.Text = "Database:";
      // 
      // RestoreDestinationSqlServerLabel
      // 
      this.RestoreDestinationSqlServerLabel.AutoSize = true;
      this.RestoreDestinationSqlServerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreDestinationSqlServerLabel.Location = new System.Drawing.Point(13, 25);
      this.RestoreDestinationSqlServerLabel.Name = "RestoreDestinationSqlServerLabel";
      this.RestoreDestinationSqlServerLabel.Size = new System.Drawing.Size(65, 13);
      this.RestoreDestinationSqlServerLabel.TabIndex = 0;
      this.RestoreDestinationSqlServerLabel.Text = "SQL Server:";
      // 
      // RestoreHeaderPanel
      // 
      this.RestoreHeaderPanel.BackColor = System.Drawing.Color.White;
      this.RestoreHeaderPanel.Controls.Add(this.RestoreMessageLabel);
      this.RestoreHeaderPanel.Controls.Add(this.RestoreHeaderLabel);
      this.RestoreHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.RestoreHeaderPanel.Location = new System.Drawing.Point(0, 0);
      this.RestoreHeaderPanel.Name = "RestoreHeaderPanel";
      this.RestoreHeaderPanel.Size = new System.Drawing.Size(458, 62);
      this.RestoreHeaderPanel.TabIndex = 0;
      // 
      // RestoreMessageLabel
      // 
      this.RestoreMessageLabel.AutoSize = true;
      this.RestoreMessageLabel.Location = new System.Drawing.Point(102, 5);
      this.RestoreMessageLabel.Name = "RestoreMessageLabel";
      this.RestoreMessageLabel.Size = new System.Drawing.Size(344, 52);
      this.RestoreMessageLabel.TabIndex = 1;
      this.RestoreMessageLabel.Text = resources.GetString("RestoreMessageLabel.Text");
      // 
      // RestoreHeaderLabel
      // 
      this.RestoreHeaderLabel.AutoSize = true;
      this.RestoreHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RestoreHeaderLabel.Location = new System.Drawing.Point(8, 18);
      this.RestoreHeaderLabel.Name = "RestoreHeaderLabel";
      this.RestoreHeaderLabel.Size = new System.Drawing.Size(75, 24);
      this.RestoreHeaderLabel.TabIndex = 0;
      this.RestoreHeaderLabel.Text = "Restore";
      // 
      // BackupPanel
      // 
      this.BackupPanel.Controls.Add(this.BackupStatusLabel);
      this.BackupPanel.Controls.Add(this.BackupProgress);
      this.BackupPanel.Controls.Add(this.BackupDatabaseButton);
      this.BackupPanel.Controls.Add(this.BackupDestinationGroup);
      this.BackupPanel.Controls.Add(this.BackupSourceGroup);
      this.BackupPanel.Controls.Add(this.BackupHeaderPanel);
      this.BackupPanel.Location = new System.Drawing.Point(469, 27);
      this.BackupPanel.Name = "BackupPanel";
      this.BackupPanel.Size = new System.Drawing.Size(458, 283);
      this.BackupPanel.TabIndex = 2;
      this.BackupPanel.Visible = false;
      // 
      // BackupStatusLabel
      // 
      this.BackupStatusLabel.AutoSize = true;
      this.BackupStatusLabel.Location = new System.Drawing.Point(25, 243);
      this.BackupStatusLabel.Name = "BackupStatusLabel";
      this.BackupStatusLabel.Size = new System.Drawing.Size(0, 13);
      this.BackupStatusLabel.TabIndex = 15;
      this.BackupStatusLabel.Visible = false;
      // 
      // BackupProgress
      // 
      this.BackupProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupProgress.Location = new System.Drawing.Point(21, 259);
      this.BackupProgress.Name = "BackupProgress";
      this.BackupProgress.Size = new System.Drawing.Size(297, 10);
      this.BackupProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.BackupProgress.TabIndex = 14;
      this.BackupProgress.Visible = false;
      // 
      // BackupDatabaseButton
      // 
      this.BackupDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupDatabaseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BackupDatabaseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupDatabaseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BackupDatabaseButton.Location = new System.Drawing.Point(324, 243);
      this.BackupDatabaseButton.Name = "BackupDatabaseButton";
      this.BackupDatabaseButton.Size = new System.Drawing.Size(123, 31);
      this.BackupDatabaseButton.TabIndex = 13;
      this.BackupDatabaseButton.Text = "Backup";
      this.BackupDatabaseButton.UseVisualStyleBackColor = true;
      // 
      // BackupDestinationGroup
      // 
      this.BackupDestinationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupDestinationGroup.Controls.Add(this.BackupBrowseFileButton);
      this.BackupDestinationGroup.Controls.Add(this.BackupDestinationFile);
      this.BackupDestinationGroup.Controls.Add(this.BackupDestinationLabel);
      this.BackupDestinationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupDestinationGroup.Location = new System.Drawing.Point(12, 180);
      this.BackupDestinationGroup.Name = "BackupDestinationGroup";
      this.BackupDestinationGroup.Size = new System.Drawing.Size(435, 57);
      this.BackupDestinationGroup.TabIndex = 10;
      this.BackupDestinationGroup.TabStop = false;
      this.BackupDestinationGroup.Text = "Destination (Backup)";
      // 
      // BackupBrowseFileButton
      // 
      this.BackupBrowseFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupBrowseFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupBrowseFileButton.Location = new System.Drawing.Point(392, 20);
      this.BackupBrowseFileButton.Name = "BackupBrowseFileButton";
      this.BackupBrowseFileButton.Size = new System.Drawing.Size(36, 24);
      this.BackupBrowseFileButton.TabIndex = 2;
      this.BackupBrowseFileButton.Text = "...";
      this.BackupBrowseFileButton.UseVisualStyleBackColor = true;
      this.BackupBrowseFileButton.Click += new System.EventHandler(this.BackupBrowseFileButton_Click);
      // 
      // BackupDestinationFile
      // 
      this.BackupDestinationFile.AcceptsReturn = true;
      this.BackupDestinationFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupDestinationFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.BackupDestinationFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.BackupDestinationFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupDestinationFile.Location = new System.Drawing.Point(84, 23);
      this.BackupDestinationFile.Name = "BackupDestinationFile";
      this.BackupDestinationFile.Size = new System.Drawing.Size(302, 20);
      this.BackupDestinationFile.TabIndex = 1;
      // 
      // BackupDestinationLabel
      // 
      this.BackupDestinationLabel.AutoSize = true;
      this.BackupDestinationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupDestinationLabel.Location = new System.Drawing.Point(6, 26);
      this.BackupDestinationLabel.Name = "BackupDestinationLabel";
      this.BackupDestinationLabel.Size = new System.Drawing.Size(53, 13);
      this.BackupDestinationLabel.TabIndex = 0;
      this.BackupDestinationLabel.Text = "File (bak):";
      // 
      // BackupSourceGroup
      // 
      this.BackupSourceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupSourceGroup.Controls.Add(this.BackupSourceDatabaseList);
      this.BackupSourceGroup.Controls.Add(this.BackupSourceChangeSqlServer);
      this.BackupSourceGroup.Controls.Add(this.BackupSourceSqlServer);
      this.BackupSourceGroup.Controls.Add(this.BackupSourceDatabaseLabel);
      this.BackupSourceGroup.Controls.Add(this.BackupSourceSqlServerLabel);
      this.BackupSourceGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceGroup.Location = new System.Drawing.Point(12, 77);
      this.BackupSourceGroup.Name = "BackupSourceGroup";
      this.BackupSourceGroup.Size = new System.Drawing.Size(435, 89);
      this.BackupSourceGroup.TabIndex = 9;
      this.BackupSourceGroup.TabStop = false;
      this.BackupSourceGroup.Text = "Source (Database)";
      // 
      // BackupSourceDatabaseList
      // 
      this.BackupSourceDatabaseList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupSourceDatabaseList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.BackupSourceDatabaseList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.BackupSourceDatabaseList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceDatabaseList.FormattingEnabled = true;
      this.BackupSourceDatabaseList.Location = new System.Drawing.Point(84, 50);
      this.BackupSourceDatabaseList.Name = "BackupSourceDatabaseList";
      this.BackupSourceDatabaseList.Size = new System.Drawing.Size(344, 21);
      this.BackupSourceDatabaseList.TabIndex = 5;
      // 
      // BackupSourceChangeSqlServer
      // 
      this.BackupSourceChangeSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BackupSourceChangeSqlServer.AutoSize = true;
      this.BackupSourceChangeSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceChangeSqlServer.Location = new System.Drawing.Point(384, 25);
      this.BackupSourceChangeSqlServer.Name = "BackupSourceChangeSqlServer";
      this.BackupSourceChangeSqlServer.Size = new System.Drawing.Size(44, 13);
      this.BackupSourceChangeSqlServer.TabIndex = 4;
      this.BackupSourceChangeSqlServer.TabStop = true;
      this.BackupSourceChangeSqlServer.Text = "Change";
      this.BackupSourceChangeSqlServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BackupSourceChangeSqlServer_LinkClicked);
      // 
      // BackupSourceSqlServer
      // 
      this.BackupSourceSqlServer.AutoSize = true;
      this.BackupSourceSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceSqlServer.Location = new System.Drawing.Point(81, 25);
      this.BackupSourceSqlServer.Name = "BackupSourceSqlServer";
      this.BackupSourceSqlServer.Size = new System.Drawing.Size(86, 13);
      this.BackupSourceSqlServer.TabIndex = 2;
      this.BackupSourceSqlServer.Text = ".\\SQLEXPRESS";
      // 
      // BackupSourceDatabaseLabel
      // 
      this.BackupSourceDatabaseLabel.AutoSize = true;
      this.BackupSourceDatabaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceDatabaseLabel.Location = new System.Drawing.Point(22, 53);
      this.BackupSourceDatabaseLabel.Name = "BackupSourceDatabaseLabel";
      this.BackupSourceDatabaseLabel.Size = new System.Drawing.Size(56, 13);
      this.BackupSourceDatabaseLabel.TabIndex = 1;
      this.BackupSourceDatabaseLabel.Text = "Database:";
      // 
      // BackupSourceSqlServerLabel
      // 
      this.BackupSourceSqlServerLabel.AutoSize = true;
      this.BackupSourceSqlServerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupSourceSqlServerLabel.Location = new System.Drawing.Point(13, 25);
      this.BackupSourceSqlServerLabel.Name = "BackupSourceSqlServerLabel";
      this.BackupSourceSqlServerLabel.Size = new System.Drawing.Size(65, 13);
      this.BackupSourceSqlServerLabel.TabIndex = 0;
      this.BackupSourceSqlServerLabel.Text = "SQL Server:";
      // 
      // BackupHeaderPanel
      // 
      this.BackupHeaderPanel.BackColor = System.Drawing.Color.White;
      this.BackupHeaderPanel.Controls.Add(this.BackupMessageLabel);
      this.BackupHeaderPanel.Controls.Add(this.BackupHeaderLabel);
      this.BackupHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.BackupHeaderPanel.Location = new System.Drawing.Point(0, 0);
      this.BackupHeaderPanel.Name = "BackupHeaderPanel";
      this.BackupHeaderPanel.Size = new System.Drawing.Size(458, 62);
      this.BackupHeaderPanel.TabIndex = 1;
      // 
      // BackupMessageLabel
      // 
      this.BackupMessageLabel.AutoSize = true;
      this.BackupMessageLabel.Location = new System.Drawing.Point(102, 5);
      this.BackupMessageLabel.Name = "BackupMessageLabel";
      this.BackupMessageLabel.Size = new System.Drawing.Size(344, 52);
      this.BackupMessageLabel.TabIndex = 2;
      this.BackupMessageLabel.Text = resources.GetString("BackupMessageLabel.Text");
      // 
      // BackupHeaderLabel
      // 
      this.BackupHeaderLabel.AutoSize = true;
      this.BackupHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BackupHeaderLabel.Location = new System.Drawing.Point(8, 18);
      this.BackupHeaderLabel.Name = "BackupHeaderLabel";
      this.BackupHeaderLabel.Size = new System.Drawing.Size(73, 24);
      this.BackupHeaderLabel.TabIndex = 0;
      this.BackupHeaderLabel.Text = "Backup";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1029, 345);
      this.Controls.Add(this.BackupPanel);
      this.Controls.Add(this.RestorePanel);
      this.Controls.Add(this.MainMenu);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.MainMenu;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SQL Backup and Restore";
      this.MainMenu.ResumeLayout(false);
      this.MainMenu.PerformLayout();
      this.RestorePanel.ResumeLayout(false);
      this.RestorePanel.PerformLayout();
      this.REstoreSourceGroup.ResumeLayout(false);
      this.REstoreSourceGroup.PerformLayout();
      this.RestoreDestinationGroup.ResumeLayout(false);
      this.RestoreDestinationGroup.PerformLayout();
      this.RestoreHeaderPanel.ResumeLayout(false);
      this.RestoreHeaderPanel.PerformLayout();
      this.BackupPanel.ResumeLayout(false);
      this.BackupPanel.PerformLayout();
      this.BackupDestinationGroup.ResumeLayout(false);
      this.BackupDestinationGroup.PerformLayout();
      this.BackupSourceGroup.ResumeLayout(false);
      this.BackupSourceGroup.PerformLayout();
      this.BackupHeaderPanel.ResumeLayout(false);
      this.BackupHeaderPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip MainMenu;
    private System.Windows.Forms.ToolStripMenuItem CommandMenuItem;
    private System.Windows.Forms.ToolStripMenuItem BackupMenuItem;
    private System.Windows.Forms.ToolStripMenuItem RestoreMenuItem;
    private System.Windows.Forms.ToolStripMenuItem SetFileAssociationMenuItem;
    private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
    private System.Windows.Forms.Panel RestorePanel;
    private System.Windows.Forms.Panel RestoreHeaderPanel;
    private System.Windows.Forms.Label RestoreHeaderLabel;
    private System.Windows.Forms.Panel BackupPanel;
    private System.Windows.Forms.Label RestoreStatusLabel;
    private System.Windows.Forms.ProgressBar RestoreProgress;
    private System.Windows.Forms.Button RestoreDatabaseButton;
    private System.Windows.Forms.GroupBox REstoreSourceGroup;
    private System.Windows.Forms.Button RestoreBrowseFileButton;
    private System.Windows.Forms.TextBox RestoreSourceFile;
    private System.Windows.Forms.Label RestoreSourceLabel;
    private System.Windows.Forms.GroupBox RestoreDestinationGroup;
    private System.Windows.Forms.ComboBox RestoreDestinationDatabaseList;
    private System.Windows.Forms.LinkLabel RestoreDestinationChangeSqlServer;
    private System.Windows.Forms.Label RestoreDestinationSqlServer;
    private System.Windows.Forms.Label RestoreDestinationDatabaseLabel;
    private System.Windows.Forms.Label RestoreDestinationSqlServerLabel;
    private System.Windows.Forms.Panel BackupHeaderPanel;
    private System.Windows.Forms.Label BackupHeaderLabel;
    private System.Windows.Forms.Label BackupStatusLabel;
    private System.Windows.Forms.ProgressBar BackupProgress;
    private System.Windows.Forms.Button BackupDatabaseButton;
    private System.Windows.Forms.GroupBox BackupDestinationGroup;
    private System.Windows.Forms.Button BackupBrowseFileButton;
    private System.Windows.Forms.TextBox BackupDestinationFile;
    private System.Windows.Forms.Label BackupDestinationLabel;
    private System.Windows.Forms.GroupBox BackupSourceGroup;
    private System.Windows.Forms.ComboBox BackupSourceDatabaseList;
    private System.Windows.Forms.LinkLabel BackupSourceChangeSqlServer;
    private System.Windows.Forms.Label BackupSourceSqlServer;
    private System.Windows.Forms.Label BackupSourceDatabaseLabel;
    private System.Windows.Forms.Label BackupSourceSqlServerLabel;
    private System.Windows.Forms.Label RestoreMessageLabel;
    private System.Windows.Forms.Label BackupMessageLabel;
  }
}

