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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mMCFileWLDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accessibleNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRAMToROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eraseMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToPLCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDiagnosticBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTungstenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbPlc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpPlcInformation = new System.Windows.Forms.GroupBox();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.lblModuleTypeName = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.blockList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codeDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interfaceDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.grpPlcInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pLCToolStripMenuItem,
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
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
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
            this.mMCFileWLDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mMCFileWLDToolStripMenuItem.Text = "Import...";
            this.mMCFileWLDToolStripMenuItem.Click += new System.EventHandler(this.mMCFileWLDToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            // pLCToolStripMenuItem
            // 
            this.pLCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accessibleNodesToolStripMenuItem,
            this.copyRAMToROMToolStripMenuItem,
            this.compressMemoryToolStripMenuItem,
            this.eraseMemoryToolStripMenuItem,
            this.downloadToPLCToolStripMenuItem,
            this.cPUModeToolStripMenuItem,
            this.viewDiagnosticBufferToolStripMenuItem});
            this.pLCToolStripMenuItem.Name = "pLCToolStripMenuItem";
            this.pLCToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.pLCToolStripMenuItem.Text = "PLC";
            // 
            // accessibleNodesToolStripMenuItem
            // 
            this.accessibleNodesToolStripMenuItem.Name = "accessibleNodesToolStripMenuItem";
            this.accessibleNodesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.accessibleNodesToolStripMenuItem.Text = "Accessible Nodes";
            // 
            // copyRAMToROMToolStripMenuItem
            // 
            this.copyRAMToROMToolStripMenuItem.Enabled = false;
            this.copyRAMToROMToolStripMenuItem.Name = "copyRAMToROMToolStripMenuItem";
            this.copyRAMToROMToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copyRAMToROMToolStripMenuItem.Text = "Copy RAM to ROM";
            this.copyRAMToROMToolStripMenuItem.Click += new System.EventHandler(this.copyRAMToROMToolStripMenuItem_Click);
            // 
            // compressMemoryToolStripMenuItem
            // 
            this.compressMemoryToolStripMenuItem.Enabled = false;
            this.compressMemoryToolStripMenuItem.Name = "compressMemoryToolStripMenuItem";
            this.compressMemoryToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.compressMemoryToolStripMenuItem.Text = "Compress Memory";
            this.compressMemoryToolStripMenuItem.Click += new System.EventHandler(this.compressMemoryToolStripMenuItem_Click);
            // 
            // eraseMemoryToolStripMenuItem
            // 
            this.eraseMemoryToolStripMenuItem.Enabled = false;
            this.eraseMemoryToolStripMenuItem.Name = "eraseMemoryToolStripMenuItem";
            this.eraseMemoryToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.eraseMemoryToolStripMenuItem.Text = "Erase Memory";
            this.eraseMemoryToolStripMenuItem.Click += new System.EventHandler(this.eraseMemoryToolStripMenuItem_Click);
            // 
            // downloadToPLCToolStripMenuItem
            // 
            this.downloadToPLCToolStripMenuItem.Enabled = false;
            this.downloadToPLCToolStripMenuItem.Name = "downloadToPLCToolStripMenuItem";
            this.downloadToPLCToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.downloadToPLCToolStripMenuItem.Text = "Download to PLC";
            this.downloadToPLCToolStripMenuItem.Click += new System.EventHandler(this.downloadToPLCToolStripMenuItem_Click);
            // 
            // cPUModeToolStripMenuItem
            // 
            this.cPUModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.cPUModeToolStripMenuItem.Name = "cPUModeToolStripMenuItem";
            this.cPUModeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.cPUModeToolStripMenuItem.Text = "CPU Mode";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Enabled = false;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // viewDiagnosticBufferToolStripMenuItem
            // 
            this.viewDiagnosticBufferToolStripMenuItem.Enabled = false;
            this.viewDiagnosticBufferToolStripMenuItem.Name = "viewDiagnosticBufferToolStripMenuItem";
            this.viewDiagnosticBufferToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.viewDiagnosticBufferToolStripMenuItem.Text = "View Diagnostic Buffer";
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
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(273, 27);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 21);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbPlc
            // 
            this.cmbPlc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlc.FormattingEnabled = true;
            this.cmbPlc.Items.AddRange(new object[] {
            "Add New..."});
            this.cmbPlc.Location = new System.Drawing.Point(45, 27);
            this.cmbPlc.Name = "cmbPlc";
            this.cmbPlc.Size = new System.Drawing.Size(222, 21);
            this.cmbPlc.TabIndex = 19;
            this.cmbPlc.SelectedIndexChanged += new System.EventHandler(this.cmbPlc_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "PLC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpPlcInformation
            // 
            this.grpPlcInformation.Controls.Add(this.lblModuleName);
            this.grpPlcInformation.Controls.Add(this.lblModuleTypeName);
            this.grpPlcInformation.Controls.Add(this.lblSerialNumber);
            this.grpPlcInformation.Controls.Add(this.label1);
            this.grpPlcInformation.Controls.Add(this.lblModel);
            this.grpPlcInformation.Controls.Add(this.blockList);
            this.grpPlcInformation.Enabled = false;
            this.grpPlcInformation.Location = new System.Drawing.Point(13, 55);
            this.grpPlcInformation.Name = "grpPlcInformation";
            this.grpPlcInformation.Size = new System.Drawing.Size(605, 282);
            this.grpPlcInformation.TabIndex = 21;
            this.grpPlcInformation.TabStop = false;
            this.grpPlcInformation.Text = "PLC Information";
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.Location = new System.Drawing.Point(6, 150);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(73, 13);
            this.lblModuleName.TabIndex = 27;
            this.lblModuleName.Text = "Module Name";
            // 
            // lblModuleTypeName
            // 
            this.lblModuleTypeName.AutoSize = true;
            this.lblModuleTypeName.Location = new System.Drawing.Point(6, 112);
            this.lblModuleTypeName.Name = "lblModuleTypeName";
            this.lblModuleTypeName.Size = new System.Drawing.Size(100, 13);
            this.lblModuleTypeName.TabIndex = 26;
            this.lblModuleTypeName.Text = "Module Type Name";
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Location = new System.Drawing.Point(6, 66);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(73, 13);
            this.lblSerialNumber.TabIndex = 25;
            this.lblSerialNumber.Text = "Serial Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 24;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(6, 19);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(36, 13);
            this.lblModel.TabIndex = 23;
            this.lblModel.Text = "Model";
            // 
            // blockList
            // 
            this.blockList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.title,
            this.author,
            this.loadSize,
            this.codeDate,
            this.interfaceDate});
            this.blockList.FullRowSelect = true;
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
            this.blockList.Size = new System.Drawing.Size(420, 257);
            this.blockList.TabIndex = 22;
            this.blockList.UseCompatibleStateImageBehavior = false;
            this.blockList.View = System.Windows.Forms.View.Details;
            this.blockList.ItemActivate += new System.EventHandler(this.blockList_ItemActivate);
            // 
            // name
            // 
            this.name.Text = "Name";
            // 
            // title
            // 
            this.title.Text = "Title";
            // 
            // author
            // 
            this.author.Text = "Author";
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
            this.ClientSize = new System.Drawing.Size(630, 343);
            this.Controls.Add(this.grpPlcInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPlc);
            this.Controls.Add(this.btnConnect);
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

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mMCFileWLDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutTungstenToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbPlc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpPlcInformation;
        private System.Windows.Forms.ListView blockList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader loadSize;
        private System.Windows.Forms.ColumnHeader codeDate;
        private System.Windows.Forms.ColumnHeader interfaceDate;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ColumnHeader author;
        private System.Windows.Forms.ToolStripMenuItem pLCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accessibleNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRAMToROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressMemoryToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem eraseMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToPLCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDiagnosticBufferToolStripMenuItem;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.Label lblModuleTypeName;
    }
}

