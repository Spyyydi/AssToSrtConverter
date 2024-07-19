namespace Ass_to_Srt_roles_allocator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTipFileName = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTipActorsLoaded = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipAllocatedActors = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConvert = new System.Windows.Forms.TabPage();
            this.lblAllocatedActors = new System.Windows.Forms.Label();
            this.chkActorsPerLine = new System.Windows.Forms.CheckBox();
            this.chkSeparateActors = new System.Windows.Forms.CheckBox();
            this.btnSaveDeleteDubers = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblLoadStatus = new System.Windows.Forms.Label();
            this.lblConvertionStatus = new System.Windows.Forms.Label();
            this.btnRemoveLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstToChange = new System.Windows.Forms.ListBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnToAlloc = new System.Windows.Forms.Button();
            this.cmbDubers = new System.Windows.Forms.ComboBox();
            this.cmbActors = new System.Windows.Forms.ComboBox();
            this.btnAddDuber = new System.Windows.Forms.Button();
            this.btnRemoveDuber = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tabSyncActors = new System.Windows.Forms.TabPage();
            this.btnOpenReport = new System.Windows.Forms.Button();
            this.richSyncReport = new System.Windows.Forms.RichTextBox();
            this.chkKeepActors = new System.Windows.Forms.CheckBox();
            this.btnSaveAss = new System.Windows.Forms.Button();
            this.btnLoadAss = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.grpAssWithActors = new System.Windows.Forms.GroupBox();
            this.lblFileNameAssWithActors = new System.Windows.Forms.Label();
            this.btnSelectAssWithActors = new System.Windows.Forms.Button();
            this.lblSubLinesAssWithActors = new System.Windows.Forms.Label();
            this.lblNotAllocdActorsNumAssWithActors = new System.Windows.Forms.Label();
            this.lblAllocActorsNumAssWithActors = new System.Windows.Forms.Label();
            this.grpOrigAss = new System.Windows.Forms.GroupBox();
            this.lblFileNameOrigAss = new System.Windows.Forms.Label();
            this.lblSubLinesOrigAss = new System.Windows.Forms.Label();
            this.btnSelectOrigAss = new System.Windows.Forms.Button();
            this.lblNotAllocdActorsNumOrigAss = new System.Windows.Forms.Label();
            this.lblAllocActorsNumOrigAss = new System.Windows.Forms.Label();
            this.toolTipFileNameOrigAss = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFileNameAssWithActors = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabConvert.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabSyncActors.SuspendLayout();
            this.grpAssWithActors.SuspendLayout();
            this.grpOrigAss.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // toolTipFileName
            // 
            this.toolTipFileName.AutoPopDelay = 5000;
            this.toolTipFileName.InitialDelay = 500;
            this.toolTipFileName.ReshowDelay = 100;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.MaximumSize = new System.Drawing.Size(0, 300);
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowItemToolTips = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip.Text = "Not allocated actors";
            this.contextMenuStrip.Click += new System.EventHandler(this.contextMenuStrip_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabConvert);
            this.tabControl1.Controls.Add(this.tabSyncActors);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 285);
            this.tabControl1.TabIndex = 21;
            // 
            // tabConvert
            // 
            this.tabConvert.AllowDrop = true;
            this.tabConvert.BackColor = System.Drawing.SystemColors.Control;
            this.tabConvert.Controls.Add(this.lblAllocatedActors);
            this.tabConvert.Controls.Add(this.chkActorsPerLine);
            this.tabConvert.Controls.Add(this.chkSeparateActors);
            this.tabConvert.Controls.Add(this.btnSaveDeleteDubers);
            this.tabConvert.Controls.Add(this.btnImport);
            this.tabConvert.Controls.Add(this.btnExport);
            this.tabConvert.Controls.Add(this.lblLoadStatus);
            this.tabConvert.Controls.Add(this.lblConvertionStatus);
            this.tabConvert.Controls.Add(this.btnRemoveLog);
            this.tabConvert.Controls.Add(this.groupBox1);
            this.tabConvert.Controls.Add(this.btnConvert);
            this.tabConvert.Controls.Add(this.btnToAlloc);
            this.tabConvert.Controls.Add(this.cmbDubers);
            this.tabConvert.Controls.Add(this.cmbActors);
            this.tabConvert.Controls.Add(this.btnAddDuber);
            this.tabConvert.Controls.Add(this.btnRemoveDuber);
            this.tabConvert.Controls.Add(this.lblFilePath);
            this.tabConvert.Controls.Add(this.btnSelectFile);
            this.tabConvert.Location = new System.Drawing.Point(4, 22);
            this.tabConvert.Name = "tabConvert";
            this.tabConvert.Padding = new System.Windows.Forms.Padding(3);
            this.tabConvert.Size = new System.Drawing.Size(392, 259);
            this.tabConvert.TabIndex = 0;
            this.tabConvert.Text = "Convert";
            this.tabConvert.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabConvert_DragDrop);
            this.tabConvert.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabConvert_DragEnter);
            this.tabConvert.DragOver += new System.Windows.Forms.DragEventHandler(this.tabConvert_DragOver);
            this.tabConvert.DragLeave += new System.EventHandler(this.tabConvert_DragLeave);
            // 
            // lblAllocatedActors
            // 
            this.lblAllocatedActors.AutoSize = true;
            this.lblAllocatedActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllocatedActors.Location = new System.Drawing.Point(12, 111);
            this.lblAllocatedActors.Name = "lblAllocatedActors";
            this.lblAllocatedActors.Size = new System.Drawing.Size(85, 28);
            this.lblAllocatedActors.TabIndex = 38;
            this.lblAllocatedActors.Text = "Allocated actors\r\n0/0";
            this.lblAllocatedActors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkActorsPerLine
            // 
            this.chkActorsPerLine.AutoSize = true;
            this.chkActorsPerLine.Enabled = false;
            this.chkActorsPerLine.Location = new System.Drawing.Point(162, 129);
            this.chkActorsPerLine.Name = "chkActorsPerLine";
            this.chkActorsPerLine.Size = new System.Drawing.Size(93, 17);
            this.chkActorsPerLine.TabIndex = 37;
            this.chkActorsPerLine.Text = "Actors per line";
            this.chkActorsPerLine.UseVisualStyleBackColor = true;
            this.chkActorsPerLine.CheckedChanged += new System.EventHandler(this.chkActorsPerLine_CheckedChanged);
            // 
            // chkSeparateActors
            // 
            this.chkSeparateActors.AutoSize = true;
            this.chkSeparateActors.Enabled = false;
            this.chkSeparateActors.Location = new System.Drawing.Point(162, 106);
            this.chkSeparateActors.Name = "chkSeparateActors";
            this.chkSeparateActors.Size = new System.Drawing.Size(94, 17);
            this.chkSeparateActors.TabIndex = 36;
            this.chkSeparateActors.Text = "Separate SRT";
            this.chkSeparateActors.UseVisualStyleBackColor = true;
            this.chkSeparateActors.CheckedChanged += new System.EventHandler(this.chkSeparateActors_CheckedChanged);
            // 
            // btnSaveDeleteDubers
            // 
            this.btnSaveDeleteDubers.Enabled = false;
            this.btnSaveDeleteDubers.Location = new System.Drawing.Point(336, 73);
            this.btnSaveDeleteDubers.Name = "btnSaveDeleteDubers";
            this.btnSaveDeleteDubers.Size = new System.Drawing.Size(40, 21);
            this.btnSaveDeleteDubers.TabIndex = 35;
            this.btnSaveDeleteDubers.TabStop = false;
            this.btnSaveDeleteDubers.Text = "Save";
            this.btnSaveDeleteDubers.UseVisualStyleBackColor = true;
            this.btnSaveDeleteDubers.Click += new System.EventHandler(this.btnSaveDeleteDubers_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(262, 233);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(55, 20);
            this.btnImport.TabIndex = 34;
            this.btnImport.TabStop = false;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(321, 233);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(55, 20);
            this.btnExport.TabIndex = 33;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblLoadStatus
            // 
            this.lblLoadStatus.AutoSize = true;
            this.lblLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoadStatus.ForeColor = System.Drawing.Color.Red;
            this.lblLoadStatus.Location = new System.Drawing.Point(263, 38);
            this.lblLoadStatus.Name = "lblLoadStatus";
            this.lblLoadStatus.Size = new System.Drawing.Size(107, 20);
            this.lblLoadStatus.TabIndex = 32;
            this.lblLoadStatus.Text = "Actors loaded";
            this.lblLoadStatus.ForeColorChanged += new System.EventHandler(this.lblLoadStatus_ForeColorChanged);
            this.lblLoadStatus.DoubleClick += new System.EventHandler(this.lblLoadStatus_DoubleClick);
            // 
            // lblConvertionStatus
            // 
            this.lblConvertionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConvertionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConvertionStatus.Location = new System.Drawing.Point(262, 116);
            this.lblConvertionStatus.Name = "lblConvertionStatus";
            this.lblConvertionStatus.Size = new System.Drawing.Size(121, 23);
            this.lblConvertionStatus.TabIndex = 31;
            this.lblConvertionStatus.Text = "Converted";
            this.lblConvertionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConvertionStatus.DoubleClick += new System.EventHandler(this.lblConvertionStatus_DoubleClick);
            // 
            // btnRemoveLog
            // 
            this.btnRemoveLog.Enabled = false;
            this.btnRemoveLog.Location = new System.Drawing.Point(262, 207);
            this.btnRemoveLog.Name = "btnRemoveLog";
            this.btnRemoveLog.Size = new System.Drawing.Size(116, 20);
            this.btnRemoveLog.TabIndex = 27;
            this.btnRemoveLog.TabStop = false;
            this.btnRemoveLog.Text = "Remove";
            this.btnRemoveLog.UseVisualStyleBackColor = true;
            this.btnRemoveLog.Click += new System.EventHandler(this.btnRemoveLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstToChange);
            this.groupBox1.Location = new System.Drawing.Point(6, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 104);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To change";
            // 
            // lstToChange
            // 
            this.lstToChange.AllowDrop = true;
            this.lstToChange.DisplayMember = "String";
            this.lstToChange.FormattingEnabled = true;
            this.lstToChange.Location = new System.Drawing.Point(6, 13);
            this.lstToChange.Name = "lstToChange";
            this.lstToChange.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstToChange.Size = new System.Drawing.Size(235, 82);
            this.lstToChange.TabIndex = 0;
            this.lstToChange.TabStop = false;
            this.lstToChange.ValueMember = "String";
            this.lstToChange.SelectedIndexChanged += new System.EventHandler(this.lstToChange_SelectedIndexChanged);
            this.lstToChange.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstToChange_DragDrop);
            this.lstToChange.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstToChange_DragEnter);
            this.lstToChange.DragOver += new System.Windows.Forms.DragEventHandler(this.lstToChange_DragOver);
            this.lstToChange.DragLeave += new System.EventHandler(this.lstToChange_DragLeave);
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Transparent;
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(262, 160);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(116, 41);
            this.btnConvert.TabIndex = 29;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnToAlloc
            // 
            this.btnToAlloc.Enabled = false;
            this.btnToAlloc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToAlloc.Location = new System.Drawing.Point(120, 100);
            this.btnToAlloc.Name = "btnToAlloc";
            this.btnToAlloc.Size = new System.Drawing.Size(25, 52);
            this.btnToAlloc.TabIndex = 28;
            this.btnToAlloc.Text = "↓";
            this.btnToAlloc.UseVisualStyleBackColor = true;
            this.btnToAlloc.Click += new System.EventHandler(this.btnToAlloc_Click);
            // 
            // cmbDubers
            // 
            this.cmbDubers.FormattingEnabled = true;
            this.cmbDubers.Location = new System.Drawing.Point(6, 73);
            this.cmbDubers.Name = "cmbDubers";
            this.cmbDubers.Size = new System.Drawing.Size(250, 21);
            this.cmbDubers.TabIndex = 26;
            this.cmbDubers.TextUpdate += new System.EventHandler(this.cmbDubers_TextUpdate);
            // 
            // cmbActors
            // 
            this.cmbActors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActors.FormattingEnabled = true;
            this.cmbActors.Location = new System.Drawing.Point(6, 37);
            this.cmbActors.Name = "cmbActors";
            this.cmbActors.Size = new System.Drawing.Size(250, 21);
            this.cmbActors.TabIndex = 24;
            this.cmbActors.SelectedIndexChanged += new System.EventHandler(this.cmbActors_SelectedIndexChanged);
            // 
            // btnAddDuber
            // 
            this.btnAddDuber.Enabled = false;
            this.btnAddDuber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddDuber.Location = new System.Drawing.Point(297, 73);
            this.btnAddDuber.Name = "btnAddDuber";
            this.btnAddDuber.Size = new System.Drawing.Size(32, 21);
            this.btnAddDuber.TabIndex = 23;
            this.btnAddDuber.TabStop = false;
            this.btnAddDuber.Text = "+";
            this.btnAddDuber.UseVisualStyleBackColor = true;
            this.btnAddDuber.Click += new System.EventHandler(this.btnAddDuber_Click);
            // 
            // btnRemoveDuber
            // 
            this.btnRemoveDuber.Enabled = false;
            this.btnRemoveDuber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveDuber.Location = new System.Drawing.Point(262, 73);
            this.btnRemoveDuber.Name = "btnRemoveDuber";
            this.btnRemoveDuber.Size = new System.Drawing.Size(32, 21);
            this.btnRemoveDuber.TabIndex = 22;
            this.btnRemoveDuber.TabStop = false;
            this.btnRemoveDuber.Text = "-";
            this.btnRemoveDuber.UseVisualStyleBackColor = true;
            this.btnRemoveDuber.Click += new System.EventHandler(this.btnRemoveDuber_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoEllipsis = true;
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(87, 13);
            this.lblFilePath.MaximumSize = new System.Drawing.Size(310, 13);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(58, 13);
            this.lblFilePath.TabIndex = 25;
            this.lblFilePath.Text = "File name: ";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(6, 8);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 21;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tabSyncActors
            // 
            this.tabSyncActors.BackColor = System.Drawing.SystemColors.Control;
            this.tabSyncActors.Controls.Add(this.btnOpenReport);
            this.tabSyncActors.Controls.Add(this.richSyncReport);
            this.tabSyncActors.Controls.Add(this.chkKeepActors);
            this.tabSyncActors.Controls.Add(this.btnSaveAss);
            this.tabSyncActors.Controls.Add(this.btnLoadAss);
            this.tabSyncActors.Controls.Add(this.btnSync);
            this.tabSyncActors.Controls.Add(this.grpAssWithActors);
            this.tabSyncActors.Controls.Add(this.grpOrigAss);
            this.tabSyncActors.Location = new System.Drawing.Point(4, 22);
            this.tabSyncActors.Name = "tabSyncActors";
            this.tabSyncActors.Padding = new System.Windows.Forms.Padding(3);
            this.tabSyncActors.Size = new System.Drawing.Size(392, 259);
            this.tabSyncActors.TabIndex = 1;
            this.tabSyncActors.Text = "Sync actors";
            // 
            // btnOpenReport
            // 
            this.btnOpenReport.Enabled = false;
            this.btnOpenReport.Location = new System.Drawing.Point(286, 230);
            this.btnOpenReport.Name = "btnOpenReport";
            this.btnOpenReport.Size = new System.Drawing.Size(80, 23);
            this.btnOpenReport.TabIndex = 7;
            this.btnOpenReport.Text = "Open report";
            this.btnOpenReport.UseVisualStyleBackColor = true;
            this.btnOpenReport.Click += new System.EventHandler(this.btnOpenReport_Click);
            // 
            // richSyncReport
            // 
            this.richSyncReport.BackColor = System.Drawing.SystemColors.Control;
            this.richSyncReport.Location = new System.Drawing.Point(6, 127);
            this.richSyncReport.Name = "richSyncReport";
            this.richSyncReport.ReadOnly = true;
            this.richSyncReport.Size = new System.Drawing.Size(382, 71);
            this.richSyncReport.TabIndex = 6;
            this.richSyncReport.TabStop = false;
            this.richSyncReport.Text = "";
            this.richSyncReport.TextChanged += new System.EventHandler(this.richSyncReport_TextChanged);
            // 
            // chkKeepActors
            // 
            this.chkKeepActors.AutoSize = true;
            this.chkKeepActors.Enabled = false;
            this.chkKeepActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkKeepActors.Location = new System.Drawing.Point(9, 204);
            this.chkKeepActors.Name = "chkKeepActors";
            this.chkKeepActors.Size = new System.Drawing.Size(146, 20);
            this.chkKeepActors.TabIndex = 2;
            this.chkKeepActors.Text = "Keep existing actors";
            this.chkKeepActors.UseVisualStyleBackColor = true;
            // 
            // btnSaveAss
            // 
            this.btnSaveAss.Enabled = false;
            this.btnSaveAss.Location = new System.Drawing.Point(200, 230);
            this.btnSaveAss.Name = "btnSaveAss";
            this.btnSaveAss.Size = new System.Drawing.Size(80, 23);
            this.btnSaveAss.TabIndex = 4;
            this.btnSaveAss.Text = "Save";
            this.btnSaveAss.UseVisualStyleBackColor = true;
            this.btnSaveAss.Click += new System.EventHandler(this.btnSaveAss_Click);
            // 
            // btnLoadAss
            // 
            this.btnLoadAss.Enabled = false;
            this.btnLoadAss.Location = new System.Drawing.Point(28, 230);
            this.btnLoadAss.Name = "btnLoadAss";
            this.btnLoadAss.Size = new System.Drawing.Size(80, 23);
            this.btnLoadAss.TabIndex = 5;
            this.btnLoadAss.Text = "Load";
            this.btnLoadAss.UseVisualStyleBackColor = true;
            this.btnLoadAss.Click += new System.EventHandler(this.btnLoadAss_Click);
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(114, 230);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(80, 23);
            this.btnSync.TabIndex = 3;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // grpAssWithActors
            // 
            this.grpAssWithActors.Controls.Add(this.lblFileNameAssWithActors);
            this.grpAssWithActors.Controls.Add(this.btnSelectAssWithActors);
            this.grpAssWithActors.Controls.Add(this.lblSubLinesAssWithActors);
            this.grpAssWithActors.Controls.Add(this.lblNotAllocdActorsNumAssWithActors);
            this.grpAssWithActors.Controls.Add(this.lblAllocActorsNumAssWithActors);
            this.grpAssWithActors.Location = new System.Drawing.Point(197, 6);
            this.grpAssWithActors.Name = "grpAssWithActors";
            this.grpAssWithActors.Size = new System.Drawing.Size(191, 115);
            this.grpAssWithActors.TabIndex = 1;
            this.grpAssWithActors.TabStop = false;
            this.grpAssWithActors.Text = "ASS with actors";
            this.grpAssWithActors.DragDrop += new System.Windows.Forms.DragEventHandler(this.grpAssWithActors_DragDrop);
            this.grpAssWithActors.DragEnter += new System.Windows.Forms.DragEventHandler(this.grpAssWithActors_DragEnter);
            this.grpAssWithActors.DragOver += new System.Windows.Forms.DragEventHandler(this.grpAssWithActors_DragOver);
            this.grpAssWithActors.DragLeave += new System.EventHandler(this.grpAssWithActors_DragLeave);
            // 
            // lblFileNameAssWithActors
            // 
            this.lblFileNameAssWithActors.AutoSize = true;
            this.lblFileNameAssWithActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFileNameAssWithActors.Location = new System.Drawing.Point(6, 43);
            this.lblFileNameAssWithActors.Name = "lblFileNameAssWithActors";
            this.lblFileNameAssWithActors.Size = new System.Drawing.Size(72, 16);
            this.lblFileNameAssWithActors.TabIndex = 7;
            this.lblFileNameAssWithActors.Text = "File name: ";
            // 
            // btnSelectAssWithActors
            // 
            this.btnSelectAssWithActors.Location = new System.Drawing.Point(9, 20);
            this.btnSelectAssWithActors.Name = "btnSelectAssWithActors";
            this.btnSelectAssWithActors.Size = new System.Drawing.Size(80, 20);
            this.btnSelectAssWithActors.TabIndex = 0;
            this.btnSelectAssWithActors.Text = "Select File";
            this.btnSelectAssWithActors.UseVisualStyleBackColor = true;
            this.btnSelectAssWithActors.Click += new System.EventHandler(this.btnSelectAssWithActors_Click);
            // 
            // lblSubLinesAssWithActors
            // 
            this.lblSubLinesAssWithActors.AutoSize = true;
            this.lblSubLinesAssWithActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSubLinesAssWithActors.Location = new System.Drawing.Point(6, 59);
            this.lblSubLinesAssWithActors.Name = "lblSubLinesAssWithActors";
            this.lblSubLinesAssWithActors.Size = new System.Drawing.Size(107, 16);
            this.lblSubLinesAssWithActors.TabIndex = 5;
            this.lblSubLinesAssWithActors.Text = "Total sub lines: 0";
            // 
            // lblNotAllocdActorsNumAssWithActors
            // 
            this.lblNotAllocdActorsNumAssWithActors.AutoSize = true;
            this.lblNotAllocdActorsNumAssWithActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNotAllocdActorsNumAssWithActors.Location = new System.Drawing.Point(6, 91);
            this.lblNotAllocdActorsNumAssWithActors.Name = "lblNotAllocdActorsNumAssWithActors";
            this.lblNotAllocdActorsNumAssWithActors.Size = new System.Drawing.Size(140, 16);
            this.lblNotAllocdActorsNumAssWithActors.TabIndex = 4;
            this.lblNotAllocdActorsNumAssWithActors.Text = "Not allocated actors: 0";
            // 
            // lblAllocActorsNumAssWithActors
            // 
            this.lblAllocActorsNumAssWithActors.AutoSize = true;
            this.lblAllocActorsNumAssWithActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllocActorsNumAssWithActors.Location = new System.Drawing.Point(6, 75);
            this.lblAllocActorsNumAssWithActors.Name = "lblAllocActorsNumAssWithActors";
            this.lblAllocActorsNumAssWithActors.Size = new System.Drawing.Size(117, 16);
            this.lblAllocActorsNumAssWithActors.TabIndex = 3;
            this.lblAllocActorsNumAssWithActors.Text = "Allocated actors: 0";
            // 
            // grpOrigAss
            // 
            this.grpOrigAss.Controls.Add(this.lblFileNameOrigAss);
            this.grpOrigAss.Controls.Add(this.lblSubLinesOrigAss);
            this.grpOrigAss.Controls.Add(this.btnSelectOrigAss);
            this.grpOrigAss.Controls.Add(this.lblNotAllocdActorsNumOrigAss);
            this.grpOrigAss.Controls.Add(this.lblAllocActorsNumOrigAss);
            this.grpOrigAss.Location = new System.Drawing.Point(6, 6);
            this.grpOrigAss.Name = "grpOrigAss";
            this.grpOrigAss.Size = new System.Drawing.Size(191, 115);
            this.grpOrigAss.TabIndex = 0;
            this.grpOrigAss.TabStop = false;
            this.grpOrigAss.Text = "ASS to put actors";
            this.grpOrigAss.DragDrop += new System.Windows.Forms.DragEventHandler(this.grpOrigAss_DragDrop);
            this.grpOrigAss.DragEnter += new System.Windows.Forms.DragEventHandler(this.grpOrigAss_DragEnter);
            this.grpOrigAss.DragOver += new System.Windows.Forms.DragEventHandler(this.grpOrigAss_DragOver);
            this.grpOrigAss.DragLeave += new System.EventHandler(this.grpOrigAss_DragLeave);
            // 
            // lblFileNameOrigAss
            // 
            this.lblFileNameOrigAss.AutoSize = true;
            this.lblFileNameOrigAss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFileNameOrigAss.Location = new System.Drawing.Point(6, 43);
            this.lblFileNameOrigAss.Name = "lblFileNameOrigAss";
            this.lblFileNameOrigAss.Size = new System.Drawing.Size(72, 16);
            this.lblFileNameOrigAss.TabIndex = 6;
            this.lblFileNameOrigAss.Text = "File name: ";
            // 
            // lblSubLinesOrigAss
            // 
            this.lblSubLinesOrigAss.AutoSize = true;
            this.lblSubLinesOrigAss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSubLinesOrigAss.Location = new System.Drawing.Point(6, 59);
            this.lblSubLinesOrigAss.Name = "lblSubLinesOrigAss";
            this.lblSubLinesOrigAss.Size = new System.Drawing.Size(107, 16);
            this.lblSubLinesOrigAss.TabIndex = 2;
            this.lblSubLinesOrigAss.Text = "Total sub lines: 0";
            // 
            // btnSelectOrigAss
            // 
            this.btnSelectOrigAss.Location = new System.Drawing.Point(9, 20);
            this.btnSelectOrigAss.Name = "btnSelectOrigAss";
            this.btnSelectOrigAss.Size = new System.Drawing.Size(80, 20);
            this.btnSelectOrigAss.TabIndex = 0;
            this.btnSelectOrigAss.Text = "Select File";
            this.btnSelectOrigAss.UseVisualStyleBackColor = true;
            this.btnSelectOrigAss.Click += new System.EventHandler(this.btnSelectOrigAss_Click);
            // 
            // lblNotAllocdActorsNumOrigAss
            // 
            this.lblNotAllocdActorsNumOrigAss.AutoSize = true;
            this.lblNotAllocdActorsNumOrigAss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNotAllocdActorsNumOrigAss.Location = new System.Drawing.Point(6, 91);
            this.lblNotAllocdActorsNumOrigAss.Name = "lblNotAllocdActorsNumOrigAss";
            this.lblNotAllocdActorsNumOrigAss.Size = new System.Drawing.Size(140, 16);
            this.lblNotAllocdActorsNumOrigAss.TabIndex = 1;
            this.lblNotAllocdActorsNumOrigAss.Text = "Not allocated actors: 0";
            // 
            // lblAllocActorsNumOrigAss
            // 
            this.lblAllocActorsNumOrigAss.AutoSize = true;
            this.lblAllocActorsNumOrigAss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllocActorsNumOrigAss.Location = new System.Drawing.Point(6, 75);
            this.lblAllocActorsNumOrigAss.Name = "lblAllocActorsNumOrigAss";
            this.lblAllocActorsNumOrigAss.Size = new System.Drawing.Size(117, 16);
            this.lblAllocActorsNumOrigAss.TabIndex = 0;
            this.lblAllocActorsNumOrigAss.Text = "Allocated actors: 0";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 284);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASS to SRT";
            this.tabControl1.ResumeLayout(false);
            this.tabConvert.ResumeLayout(false);
            this.tabConvert.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabSyncActors.ResumeLayout(false);
            this.tabSyncActors.PerformLayout();
            this.grpAssWithActors.ResumeLayout(false);
            this.grpAssWithActors.PerformLayout();
            this.grpOrigAss.ResumeLayout(false);
            this.grpOrigAss.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTipFileName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolTip toolTipActorsLoaded;
        private System.Windows.Forms.ToolTip toolTipAllocatedActors;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConvert;
        private System.Windows.Forms.Label lblAllocatedActors;
        private System.Windows.Forms.CheckBox chkActorsPerLine;
        private System.Windows.Forms.CheckBox chkSeparateActors;
        private System.Windows.Forms.Button btnSaveDeleteDubers;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblLoadStatus;
        private System.Windows.Forms.Label lblConvertionStatus;
        private System.Windows.Forms.Button btnRemoveLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstToChange;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnToAlloc;
        private System.Windows.Forms.ComboBox cmbDubers;
        private System.Windows.Forms.ComboBox cmbActors;
        private System.Windows.Forms.Button btnAddDuber;
        private System.Windows.Forms.Button btnRemoveDuber;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TabPage tabSyncActors;
        private System.Windows.Forms.GroupBox grpAssWithActors;
        private System.Windows.Forms.GroupBox grpOrigAss;
        private System.Windows.Forms.Button btnSaveAss;
        private System.Windows.Forms.Button btnLoadAss;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label lblSubLinesOrigAss;
        private System.Windows.Forms.Label lblNotAllocdActorsNumOrigAss;
        private System.Windows.Forms.Label lblAllocActorsNumOrigAss;
        private System.Windows.Forms.Label lblSubLinesAssWithActors;
        private System.Windows.Forms.Label lblNotAllocdActorsNumAssWithActors;
        private System.Windows.Forms.Label lblAllocActorsNumAssWithActors;
        private System.Windows.Forms.Button btnSelectAssWithActors;
        private System.Windows.Forms.Button btnSelectOrigAss;
        private System.Windows.Forms.Label lblFileNameOrigAss;
        private System.Windows.Forms.CheckBox chkKeepActors;
        private System.Windows.Forms.Label lblFileNameAssWithActors;
        private System.Windows.Forms.Button btnOpenReport;
        private System.Windows.Forms.RichTextBox richSyncReport;
        private System.Windows.Forms.ToolTip toolTipFileNameOrigAss;
        private System.Windows.Forms.ToolTip toolTipFileNameAssWithActors;
    }
}

