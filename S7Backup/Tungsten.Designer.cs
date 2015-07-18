namespace Tungsten
{
    partial class Tungsten
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("OB", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("FC", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("FB", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("DB", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("SFC", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("SFB", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("SDB", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tungsten));
            this.btnUpload = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mMCFileWLDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTungstenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnErase = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnStartPlc = new System.Windows.Forms.Button();
            this.btnStopPlc = new System.Windows.Forms.Button();
            this.btnGetRunMode = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnViewDiagnosticBuffer = new System.Windows.Forms.Button();
            this.cmbPlc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpPlcInformation = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.blockList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codeDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interfaceDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.grpPlcInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(424, 27);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(200, 50);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload to PC";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMCFileWLDToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "Memory Card File";
            // 
            // mMCFileWLDToolStripMenuItem
            // 
            this.mMCFileWLDToolStripMenuItem.Name = "mMCFileWLDToolStripMenuItem";
            this.mMCFileWLDToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mMCFileWLDToolStripMenuItem.Text = "Import...";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutTungstenToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            // 
            // aboutTungstenToolStripMenuItem
            // 
            this.aboutTungstenToolStripMenuItem.Name = "aboutTungstenToolStripMenuItem";
            this.aboutTungstenToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.aboutTungstenToolStripMenuItem.Text = "About Tungsten";
            this.aboutTungstenToolStripMenuItem.Click += new System.EventHandler(this.aboutTungstenToolStripMenuItem_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(424, 83);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(200, 50);
            this.btnDownload.TabIndex = 8;
            this.btnDownload.Text = "Download to PLC";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnErase
            // 
            this.btnErase.Enabled = false;
            this.btnErase.Location = new System.Drawing.Point(424, 139);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(200, 50);
            this.btnErase.TabIndex = 9;
            this.btnErase.Text = "Erase";
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(224, 27);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 70);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnStartPlc
            // 
            this.btnStartPlc.Enabled = false;
            this.btnStartPlc.Location = new System.Drawing.Point(424, 195);
            this.btnStartPlc.Name = "btnStartPlc";
            this.btnStartPlc.Size = new System.Drawing.Size(95, 50);
            this.btnStartPlc.TabIndex = 12;
            this.btnStartPlc.Text = "Start PLC";
            this.btnStartPlc.UseVisualStyleBackColor = true;
            this.btnStartPlc.Click += new System.EventHandler(this.btnStartCpu_Click);
            // 
            // btnStopPlc
            // 
            this.btnStopPlc.Enabled = false;
            this.btnStopPlc.Location = new System.Drawing.Point(529, 195);
            this.btnStopPlc.Name = "btnStopPlc";
            this.btnStopPlc.Size = new System.Drawing.Size(95, 50);
            this.btnStopPlc.TabIndex = 13;
            this.btnStopPlc.Text = "Stop PLC";
            this.btnStopPlc.UseVisualStyleBackColor = true;
            this.btnStopPlc.Click += new System.EventHandler(this.btnStopCpu_Click);
            // 
            // btnGetRunMode
            // 
            this.btnGetRunMode.Enabled = false;
            this.btnGetRunMode.Location = new System.Drawing.Point(494, 307);
            this.btnGetRunMode.Name = "btnGetRunMode";
            this.btnGetRunMode.Size = new System.Drawing.Size(101, 23);
            this.btnGetRunMode.TabIndex = 14;
            this.btnGetRunMode.Text = "Get Run Mode";
            this.btnGetRunMode.UseVisualStyleBackColor = true;
            this.btnGetRunMode.Click += new System.EventHandler(this.btnGetRunMode_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 27);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(100, 70);
            this.btnScan.TabIndex = 16;
            this.btnScan.Text = "Scan for PLCs";
            this.btnScan.UseVisualStyleBackColor = true;
            // 
            // btnViewDiagnosticBuffer
            // 
            this.btnViewDiagnosticBuffer.Enabled = false;
            this.btnViewDiagnosticBuffer.Location = new System.Drawing.Point(424, 251);
            this.btnViewDiagnosticBuffer.Name = "btnViewDiagnosticBuffer";
            this.btnViewDiagnosticBuffer.Size = new System.Drawing.Size(200, 50);
            this.btnViewDiagnosticBuffer.TabIndex = 18;
            this.btnViewDiagnosticBuffer.Text = "View Diagnostic Buffer";
            this.btnViewDiagnosticBuffer.UseVisualStyleBackColor = true;
            // 
            // cmbPlc
            // 
            this.cmbPlc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlc.FormattingEnabled = true;
            this.cmbPlc.Items.AddRange(new object[] {
            "Add New..."});
            this.cmbPlc.Location = new System.Drawing.Point(102, 103);
            this.cmbPlc.Name = "cmbPlc";
            this.cmbPlc.Size = new System.Drawing.Size(222, 21);
            this.cmbPlc.TabIndex = 19;
            this.cmbPlc.SelectedIndexChanged += new System.EventHandler(this.cmbPlc_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "PLC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpPlcInformation
            // 
            this.grpPlcInformation.Controls.Add(this.Label3);
            this.grpPlcInformation.Controls.Add(this.blockList);
            this.grpPlcInformation.Enabled = false;
            this.grpPlcInformation.Location = new System.Drawing.Point(13, 337);
            this.grpPlcInformation.Name = "grpPlcInformation";
            this.grpPlcInformation.Size = new System.Drawing.Size(605, 199);
            this.grpPlcInformation.TabIndex = 21;
            this.grpPlcInformation.TabStop = false;
            this.grpPlcInformation.Text = "PLC Information";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 19);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(36, 13);
            this.Label3.TabIndex = 23;
            this.Label3.Text = "Model";
            // 
            // blockList
            // 
            this.blockList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.loadSize,
            this.codeDate,
            this.interfaceDate});
            listViewGroup1.Header = "OB";
            listViewGroup1.Name = "ob";
            listViewGroup2.Header = "FC";
            listViewGroup2.Name = "fc";
            listViewGroup3.Header = "FB";
            listViewGroup3.Name = "fb";
            listViewGroup4.Header = "DB";
            listViewGroup4.Name = "db";
            listViewGroup5.Header = "SFC";
            listViewGroup5.Name = "sfc";
            listViewGroup6.Header = "SFB";
            listViewGroup6.Name = "sfb";
            listViewGroup7.Header = "SDB";
            listViewGroup7.Name = "sdb";
            this.blockList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7});
            this.blockList.Location = new System.Drawing.Point(179, 19);
            this.blockList.Name = "blockList";
            this.blockList.Size = new System.Drawing.Size(420, 174);
            this.blockList.TabIndex = 22;
            this.blockList.UseCompatibleStateImageBehavior = false;
            this.blockList.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Name";
            // 
            // loadSize
            // 
            this.loadSize.Text = "Load Size";
            // 
            // codeDate
            // 
            this.codeDate.Text = "Code Date";
            // 
            // interfaceDate
            // 
            this.interfaceDate.Text = "Interface Date";
            // 
            // Tungsten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 548);
            this.Controls.Add(this.grpPlcInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPlc);
            this.Controls.Add(this.btnViewDiagnosticBuffer);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnGetRunMode);
            this.Controls.Add(this.btnStopPlc);
            this.Controls.Add(this.btnStartPlc);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnErase);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tungsten";
            this.Text = "Tungsten";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpPlcInformation.ResumeLayout(false);
            this.grpPlcInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mMCFileWLDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnErase;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutTungstenToolStripMenuItem;
        private System.Windows.Forms.Button btnStartPlc;
        private System.Windows.Forms.Button btnStopPlc;
        private System.Windows.Forms.Button btnGetRunMode;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnViewDiagnosticBuffer;
        private System.Windows.Forms.ComboBox cmbPlc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpPlcInformation;
        private System.Windows.Forms.ListView blockList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader loadSize;
        private System.Windows.Forms.ColumnHeader codeDate;
        private System.Windows.Forms.ColumnHeader interfaceDate;
        private System.Windows.Forms.Label Label3;
    }
}

