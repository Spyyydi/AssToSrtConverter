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
            this.lblDuberLines = new System.Windows.Forms.Label();
            this.lblActorLines = new System.Windows.Forms.Label();
            this.btnConvertWithAssFormatting = new System.Windows.Forms.Button();
            this.btnLoadActorsToList = new System.Windows.Forms.Button();
            this.chkKeepNewLines = new System.Windows.Forms.CheckBox();
            this.lblAllocatedActors = new System.Windows.Forms.Label();
            this.chkActorsPerLine = new System.Windows.Forms.CheckBox();
            this.chkSeparateActors = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblActorsLoadStatus = new System.Windows.Forms.Label();
            this.lblConvertionStatus = new System.Windows.Forms.Label();
            this.btnRemoveLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstToChange = new System.Windows.Forms.ListBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnToAlloc = new System.Windows.Forms.Button();
            this.cmbDubers = new System.Windows.Forms.ComboBox();
            this.cmbActors = new System.Windows.Forms.ComboBox();
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
            this.tabBatchImport = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusBatch = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPathForBatch = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadAssNimport = new System.Windows.Forms.Button();
            this.lblBatchTotalActors = new System.Windows.Forms.Label();
            this.lblBatchLinesWithActors = new System.Windows.Forms.Label();
            this.btnReloadFiles = new System.Windows.Forms.Button();
            this.btnGenerateTimings = new System.Windows.Forms.Button();
            this.btnCreateImports = new System.Windows.Forms.Button();
            this.btnSelectMainActors = new System.Windows.Forms.Button();
            this.lblBatchAssSelected = new System.Windows.Forms.Label();
            this.lstAssFiles = new System.Windows.Forms.ListBox();
            this.btnSelectDirForBatch = new System.Windows.Forms.Button();
            this.toolTipFileNameOrigAss = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFileNameAssWithActors = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBatchPath = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabBatchConvert = new System.Windows.Forms.TabPage();
            this.btnSelectDirForBatchConvert = new System.Windows.Forms.Button();
            this.lblPathForBatchConvert = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSeparateActorsForBatchConvert = new System.Windows.Forms.CheckBox();
            this.chkActorsPerLineForBatchConvert = new System.Windows.Forms.CheckBox();
            this.chkKeepNewLinesForBatchConvert = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdKeepAssFormatting = new System.Windows.Forms.RadioButton();
            this.rdKeepntAssFormatting = new System.Windows.Forms.RadioButton();
            this.grpListForBatchConvert = new System.Windows.Forms.GroupBox();
            this.lstAssFilesForBatchConvert = new System.Windows.Forms.ListBox();
            this.btnConvertForBatchConvert = new System.Windows.Forms.Button();
            this.btnReloadFilesForBatchConvert = new System.Windows.Forms.Button();
            this.btnReportForBatchConvert = new System.Windows.Forms.Button();
            this.chkInOneFolderForBatchConvert = new System.Windows.Forms.CheckBox();
            this.lblConvertionStatusForBatchConvert = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabConvert.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabSyncActors.SuspendLayout();
            this.grpAssWithActors.SuspendLayout();
            this.grpOrigAss.SuspendLayout();
            this.tabBatchImport.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabBatchConvert.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpListForBatchConvert.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabBatchImport);
            this.tabControl1.Controls.Add(this.tabBatchConvert);
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
            this.tabConvert.Controls.Add(this.lblDuberLines);
            this.tabConvert.Controls.Add(this.lblActorLines);
            this.tabConvert.Controls.Add(this.btnConvertWithAssFormatting);
            this.tabConvert.Controls.Add(this.btnLoadActorsToList);
            this.tabConvert.Controls.Add(this.chkKeepNewLines);
            this.tabConvert.Controls.Add(this.lblAllocatedActors);
            this.tabConvert.Controls.Add(this.chkActorsPerLine);
            this.tabConvert.Controls.Add(this.chkSeparateActors);
            this.tabConvert.Controls.Add(this.btnImport);
            this.tabConvert.Controls.Add(this.btnExport);
            this.tabConvert.Controls.Add(this.lblActorsLoadStatus);
            this.tabConvert.Controls.Add(this.lblConvertionStatus);
            this.tabConvert.Controls.Add(this.btnRemoveLog);
            this.tabConvert.Controls.Add(this.groupBox1);
            this.tabConvert.Controls.Add(this.btnConvert);
            this.tabConvert.Controls.Add(this.btnToAlloc);
            this.tabConvert.Controls.Add(this.cmbDubers);
            this.tabConvert.Controls.Add(this.cmbActors);
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
            // lblDuberLines
            // 
            this.lblDuberLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDuberLines.Location = new System.Drawing.Point(6, 64);
            this.lblDuberLines.Name = "lblDuberLines";
            this.lblDuberLines.Size = new System.Drawing.Size(76, 21);
            this.lblDuberLines.TabIndex = 46;
            this.lblDuberLines.Text = "Lines: 0";
            this.lblDuberLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActorLines
            // 
            this.lblActorLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblActorLines.Location = new System.Drawing.Point(6, 37);
            this.lblActorLines.Name = "lblActorLines";
            this.lblActorLines.Size = new System.Drawing.Size(76, 21);
            this.lblActorLines.TabIndex = 45;
            this.lblActorLines.Text = "Lines: 0";
            this.lblActorLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConvertWithAssFormatting
            // 
            this.btnConvertWithAssFormatting.Enabled = false;
            this.btnConvertWithAssFormatting.Location = new System.Drawing.Point(262, 135);
            this.btnConvertWithAssFormatting.Name = "btnConvertWithAssFormatting";
            this.btnConvertWithAssFormatting.Size = new System.Drawing.Size(116, 23);
            this.btnConvertWithAssFormatting.TabIndex = 13;
            this.btnConvertWithAssFormatting.Text = "Keep ass formatting";
            this.btnConvertWithAssFormatting.UseVisualStyleBackColor = true;
            this.btnConvertWithAssFormatting.Click += new System.EventHandler(this.btnConvertWithAssFormatting_Click);
            // 
            // btnLoadActorsToList
            // 
            this.btnLoadActorsToList.Enabled = false;
            this.btnLoadActorsToList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadActorsToList.Location = new System.Drawing.Point(267, 64);
            this.btnLoadActorsToList.Name = "btnLoadActorsToList";
            this.btnLoadActorsToList.Size = new System.Drawing.Size(109, 21);
            this.btnLoadActorsToList.TabIndex = 6;
            this.btnLoadActorsToList.Text = "Load actors to list";
            this.btnLoadActorsToList.UseVisualStyleBackColor = true;
            this.btnLoadActorsToList.Click += new System.EventHandler(this.btnLoadActorsToList_Click);
            // 
            // chkKeepNewLines
            // 
            this.chkKeepNewLines.AutoSize = true;
            this.chkKeepNewLines.Checked = true;
            this.chkKeepNewLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepNewLines.Enabled = false;
            this.chkKeepNewLines.Location = new System.Drawing.Point(162, 135);
            this.chkKeepNewLines.Name = "chkKeepNewLines";
            this.chkKeepNewLines.Size = new System.Drawing.Size(98, 17);
            this.chkKeepNewLines.TabIndex = 11;
            this.chkKeepNewLines.Text = "Keep new lines";
            this.chkKeepNewLines.UseVisualStyleBackColor = true;
            this.chkKeepNewLines.CheckedChanged += new System.EventHandler(this.chkKeepNewLines_CheckedChanged);
            // 
            // lblAllocatedActors
            // 
            this.lblAllocatedActors.AutoSize = true;
            this.lblAllocatedActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllocatedActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllocatedActors.Location = new System.Drawing.Point(12, 104);
            this.lblAllocatedActors.Name = "lblAllocatedActors";
            this.lblAllocatedActors.Size = new System.Drawing.Size(95, 32);
            this.lblAllocatedActors.TabIndex = 38;
            this.lblAllocatedActors.Text = "Allocated actors\r\n0/0";
            this.lblAllocatedActors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkActorsPerLine
            // 
            this.chkActorsPerLine.AutoSize = true;
            this.chkActorsPerLine.Enabled = false;
            this.chkActorsPerLine.Location = new System.Drawing.Point(162, 113);
            this.chkActorsPerLine.Name = "chkActorsPerLine";
            this.chkActorsPerLine.Size = new System.Drawing.Size(93, 17);
            this.chkActorsPerLine.TabIndex = 10;
            this.chkActorsPerLine.Text = "Actors per line";
            this.chkActorsPerLine.UseVisualStyleBackColor = true;
            this.chkActorsPerLine.CheckedChanged += new System.EventHandler(this.chkActorsPerLine_CheckedChanged);
            // 
            // chkSeparateActors
            // 
            this.chkSeparateActors.AutoSize = true;
            this.chkSeparateActors.Enabled = false;
            this.chkSeparateActors.Location = new System.Drawing.Point(162, 91);
            this.chkSeparateActors.Name = "chkSeparateActors";
            this.chkSeparateActors.Size = new System.Drawing.Size(94, 17);
            this.chkSeparateActors.TabIndex = 9;
            this.chkSeparateActors.Text = "Separate SRT";
            this.chkSeparateActors.UseVisualStyleBackColor = true;
            this.chkSeparateActors.CheckedChanged += new System.EventHandler(this.chkSeparateActors_CheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(262, 233);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(55, 20);
            this.btnImport.TabIndex = 7;
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
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblActorsLoadStatus
            // 
            this.lblActorsLoadStatus.AutoSize = true;
            this.lblActorsLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblActorsLoadStatus.ForeColor = System.Drawing.Color.Red;
            this.lblActorsLoadStatus.Location = new System.Drawing.Point(269, 38);
            this.lblActorsLoadStatus.Name = "lblActorsLoadStatus";
            this.lblActorsLoadStatus.Size = new System.Drawing.Size(107, 20);
            this.lblActorsLoadStatus.TabIndex = 32;
            this.lblActorsLoadStatus.Text = "Actors loaded";
            this.lblActorsLoadStatus.ForeColorChanged += new System.EventHandler(this.lblLoadStatus_ForeColorChanged);
            // 
            // lblConvertionStatus
            // 
            this.lblConvertionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConvertionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConvertionStatus.Location = new System.Drawing.Point(261, 98);
            this.lblConvertionStatus.Name = "lblConvertionStatus";
            this.lblConvertionStatus.Size = new System.Drawing.Size(121, 24);
            this.lblConvertionStatus.TabIndex = 31;
            this.lblConvertionStatus.Text = "Converted";
            this.lblConvertionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRemoveLog
            // 
            this.btnRemoveLog.Enabled = false;
            this.btnRemoveLog.Location = new System.Drawing.Point(262, 207);
            this.btnRemoveLog.Name = "btnRemoveLog";
            this.btnRemoveLog.Size = new System.Drawing.Size(116, 20);
            this.btnRemoveLog.TabIndex = 5;
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
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actors list";
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
            this.lstToChange.Sorted = true;
            this.lstToChange.TabIndex = 0;
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
            this.btnConvert.TabIndex = 12;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnToAlloc
            // 
            this.btnToAlloc.Enabled = false;
            this.btnToAlloc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToAlloc.Location = new System.Drawing.Point(122, 94);
            this.btnToAlloc.Name = "btnToAlloc";
            this.btnToAlloc.Size = new System.Drawing.Size(25, 52);
            this.btnToAlloc.TabIndex = 3;
            this.btnToAlloc.Text = "↓";
            this.btnToAlloc.UseVisualStyleBackColor = true;
            this.btnToAlloc.Click += new System.EventHandler(this.btnToAlloc_Click);
            // 
            // cmbDubers
            // 
            this.cmbDubers.FormattingEnabled = true;
            this.cmbDubers.Location = new System.Drawing.Point(83, 64);
            this.cmbDubers.Name = "cmbDubers";
            this.cmbDubers.Size = new System.Drawing.Size(173, 21);
            this.cmbDubers.TabIndex = 2;
            this.cmbDubers.SelectedIndexChanged += new System.EventHandler(this.cmbDubers_TextUpdate);
            this.cmbDubers.TextUpdate += new System.EventHandler(this.cmbDubers_TextUpdate);
            // 
            // cmbActors
            // 
            this.cmbActors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActors.FormattingEnabled = true;
            this.cmbActors.Location = new System.Drawing.Point(83, 37);
            this.cmbActors.Name = "cmbActors";
            this.cmbActors.Size = new System.Drawing.Size(173, 21);
            this.cmbActors.TabIndex = 1;
            this.cmbActors.SelectedIndexChanged += new System.EventHandler(this.cmbActors_SelectedIndexChanged);
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
            this.btnSelectFile.TabIndex = 0;
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
            this.btnOpenReport.TabIndex = 6;
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
            this.lblSubLinesAssWithActors.Location = new System.Drawing.Point(5, 59);
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
            this.lblNotAllocdActorsNumAssWithActors.Size = new System.Drawing.Size(135, 16);
            this.lblNotAllocdActorsNumAssWithActors.TabIndex = 4;
            this.lblNotAllocdActorsNumAssWithActors.Text = "Lines without actors: 0";
            // 
            // lblAllocActorsNumAssWithActors
            // 
            this.lblAllocActorsNumAssWithActors.AutoSize = true;
            this.lblAllocActorsNumAssWithActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllocActorsNumAssWithActors.Location = new System.Drawing.Point(6, 75);
            this.lblAllocActorsNumAssWithActors.Name = "lblAllocActorsNumAssWithActors";
            this.lblAllocActorsNumAssWithActors.Size = new System.Drawing.Size(117, 16);
            this.lblAllocActorsNumAssWithActors.TabIndex = 3;
            this.lblAllocActorsNumAssWithActors.Text = "Lines with actors: 0";
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
            this.lblSubLinesOrigAss.Location = new System.Drawing.Point(5, 59);
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
            this.lblNotAllocdActorsNumOrigAss.Size = new System.Drawing.Size(135, 16);
            this.lblNotAllocdActorsNumOrigAss.TabIndex = 1;
            this.lblNotAllocdActorsNumOrigAss.Text = "Lines without actors: 0";
            // 
            // lblAllocActorsNumOrigAss
            // 
            this.lblAllocActorsNumOrigAss.AutoSize = true;
            this.lblAllocActorsNumOrigAss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllocActorsNumOrigAss.Location = new System.Drawing.Point(6, 75);
            this.lblAllocActorsNumOrigAss.Name = "lblAllocActorsNumOrigAss";
            this.lblAllocActorsNumOrigAss.Size = new System.Drawing.Size(117, 16);
            this.lblAllocActorsNumOrigAss.TabIndex = 0;
            this.lblAllocActorsNumOrigAss.Text = "Lines with actors: 0";
            // 
            // tabBatchImport
            // 
            this.tabBatchImport.BackColor = System.Drawing.SystemColors.Control;
            this.tabBatchImport.Controls.Add(this.statusStrip1);
            this.tabBatchImport.Controls.Add(this.lblPathForBatch);
            this.tabBatchImport.Controls.Add(this.groupBox2);
            this.tabBatchImport.Controls.Add(this.btnSelectDirForBatch);
            this.tabBatchImport.Location = new System.Drawing.Point(4, 22);
            this.tabBatchImport.Name = "tabBatchImport";
            this.tabBatchImport.Padding = new System.Windows.Forms.Padding(3);
            this.tabBatchImport.Size = new System.Drawing.Size(392, 259);
            this.tabBatchImport.TabIndex = 2;
            this.tabBatchImport.Text = "Batch import creator";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBatch});
            this.statusStrip1.Location = new System.Drawing.Point(3, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(386, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusBatch
            // 
            this.lblStatusBatch.Name = "lblStatusBatch";
            this.lblStatusBatch.Size = new System.Drawing.Size(242, 17);
            this.lblStatusBatch.Text = "Select directory or drag and drop it to the list";
            // 
            // lblPathForBatch
            // 
            this.lblPathForBatch.AutoEllipsis = true;
            this.lblPathForBatch.AutoSize = true;
            this.lblPathForBatch.Location = new System.Drawing.Point(111, 13);
            this.lblPathForBatch.MaximumSize = new System.Drawing.Size(286, 13);
            this.lblPathForBatch.Name = "lblPathForBatch";
            this.lblPathForBatch.Size = new System.Drawing.Size(35, 13);
            this.lblPathForBatch.TabIndex = 3;
            this.lblPathForBatch.Text = "Path: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadAssNimport);
            this.groupBox2.Controls.Add(this.lblBatchTotalActors);
            this.groupBox2.Controls.Add(this.lblBatchLinesWithActors);
            this.groupBox2.Controls.Add(this.btnReloadFiles);
            this.groupBox2.Controls.Add(this.btnGenerateTimings);
            this.groupBox2.Controls.Add(this.btnCreateImports);
            this.groupBox2.Controls.Add(this.btnSelectMainActors);
            this.groupBox2.Controls.Add(this.lblBatchAssSelected);
            this.groupBox2.Controls.Add(this.lstAssFiles);
            this.groupBox2.Location = new System.Drawing.Point(3, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 196);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ASS files list";
            // 
            // btnLoadAssNimport
            // 
            this.btnLoadAssNimport.Enabled = false;
            this.btnLoadAssNimport.Location = new System.Drawing.Point(308, 155);
            this.btnLoadAssNimport.Name = "btnLoadAssNimport";
            this.btnLoadAssNimport.Size = new System.Drawing.Size(72, 39);
            this.btnLoadAssNimport.TabIndex = 5;
            this.btnLoadAssNimport.Text = "Load to converter";
            this.btnLoadAssNimport.UseVisualStyleBackColor = true;
            this.btnLoadAssNimport.Click += new System.EventHandler(this.btnLoadAssNimport_Click);
            // 
            // lblBatchTotalActors
            // 
            this.lblBatchTotalActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBatchTotalActors.Location = new System.Drawing.Point(216, 157);
            this.lblBatchTotalActors.Name = "lblBatchTotalActors";
            this.lblBatchTotalActors.Size = new System.Drawing.Size(86, 35);
            this.lblBatchTotalActors.TabIndex = 8;
            this.lblBatchTotalActors.Text = "Actors number\r\n0";
            this.lblBatchTotalActors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBatchLinesWithActors
            // 
            this.lblBatchLinesWithActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBatchLinesWithActors.Location = new System.Drawing.Point(111, 157);
            this.lblBatchLinesWithActors.Name = "lblBatchLinesWithActors";
            this.lblBatchLinesWithActors.Size = new System.Drawing.Size(86, 35);
            this.lblBatchLinesWithActors.TabIndex = 7;
            this.lblBatchLinesWithActors.Text = "Lines allocated\r\n0/0";
            this.lblBatchLinesWithActors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReloadFiles
            // 
            this.btnReloadFiles.Enabled = false;
            this.btnReloadFiles.Location = new System.Drawing.Point(308, 120);
            this.btnReloadFiles.Name = "btnReloadFiles";
            this.btnReloadFiles.Size = new System.Drawing.Size(72, 35);
            this.btnReloadFiles.TabIndex = 4;
            this.btnReloadFiles.Text = "Reload files";
            this.btnReloadFiles.UseVisualStyleBackColor = true;
            this.btnReloadFiles.Click += new System.EventHandler(this.btnReloadFiles_Click);
            // 
            // btnGenerateTimings
            // 
            this.btnGenerateTimings.Enabled = false;
            this.btnGenerateTimings.Location = new System.Drawing.Point(308, 85);
            this.btnGenerateTimings.Name = "btnGenerateTimings";
            this.btnGenerateTimings.Size = new System.Drawing.Size(72, 35);
            this.btnGenerateTimings.TabIndex = 3;
            this.btnGenerateTimings.Text = "Generate timings";
            this.btnGenerateTimings.UseVisualStyleBackColor = true;
            this.btnGenerateTimings.Click += new System.EventHandler(this.btnGenerateTimings_Click);
            // 
            // btnCreateImports
            // 
            this.btnCreateImports.Enabled = false;
            this.btnCreateImports.Location = new System.Drawing.Point(308, 50);
            this.btnCreateImports.Name = "btnCreateImports";
            this.btnCreateImports.Size = new System.Drawing.Size(72, 35);
            this.btnCreateImports.TabIndex = 2;
            this.btnCreateImports.Text = "Create imports";
            this.btnCreateImports.UseVisualStyleBackColor = true;
            this.btnCreateImports.Click += new System.EventHandler(this.btnCreateImports_Click);
            // 
            // btnSelectMainActors
            // 
            this.btnSelectMainActors.Enabled = false;
            this.btnSelectMainActors.Location = new System.Drawing.Point(308, 15);
            this.btnSelectMainActors.Name = "btnSelectMainActors";
            this.btnSelectMainActors.Size = new System.Drawing.Size(72, 35);
            this.btnSelectMainActors.TabIndex = 1;
            this.btnSelectMainActors.Text = "Select main actors";
            this.btnSelectMainActors.UseVisualStyleBackColor = true;
            this.btnSelectMainActors.Click += new System.EventHandler(this.btnSelectMainActors_Click);
            // 
            // lblBatchAssSelected
            // 
            this.lblBatchAssSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBatchAssSelected.Location = new System.Drawing.Point(6, 157);
            this.lblBatchAssSelected.Name = "lblBatchAssSelected";
            this.lblBatchAssSelected.Size = new System.Drawing.Size(86, 35);
            this.lblBatchAssSelected.TabIndex = 2;
            this.lblBatchAssSelected.Text = "ASS selected\r\n0/0";
            this.lblBatchAssSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstAssFiles
            // 
            this.lstAssFiles.AllowDrop = true;
            this.lstAssFiles.DisplayMember = "String";
            this.lstAssFiles.FormattingEnabled = true;
            this.lstAssFiles.HorizontalScrollbar = true;
            this.lstAssFiles.Location = new System.Drawing.Point(6, 15);
            this.lstAssFiles.Name = "lstAssFiles";
            this.lstAssFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAssFiles.Size = new System.Drawing.Size(296, 134);
            this.lstAssFiles.Sorted = true;
            this.lstAssFiles.TabIndex = 0;
            this.lstAssFiles.ValueMember = "String";
            this.lstAssFiles.SelectedIndexChanged += new System.EventHandler(this.lstAssFiles_SelectedIndexChanged);
            this.lstAssFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstAssFiles_DragDrop);
            this.lstAssFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstAssFiles_DragEnter);
            this.lstAssFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.lstAssFiles_DragOver);
            this.lstAssFiles.DragLeave += new System.EventHandler(this.lstAssFiles_DragLeave);
            // 
            // btnSelectDirForBatch
            // 
            this.btnSelectDirForBatch.Location = new System.Drawing.Point(6, 8);
            this.btnSelectDirForBatch.Name = "btnSelectDirForBatch";
            this.btnSelectDirForBatch.Size = new System.Drawing.Size(99, 23);
            this.btnSelectDirForBatch.TabIndex = 0;
            this.btnSelectDirForBatch.Text = "Select directory";
            this.btnSelectDirForBatch.UseVisualStyleBackColor = true;
            this.btnSelectDirForBatch.Click += new System.EventHandler(this.btnSelectDirForBatch_Click);
            // 
            // tabBatchConvert
            // 
            this.tabBatchConvert.BackColor = System.Drawing.SystemColors.Control;
            this.tabBatchConvert.Controls.Add(this.lblConvertionStatusForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.chkInOneFolderForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.btnReportForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.btnReloadFilesForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.btnConvertForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.grpListForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.groupBox4);
            this.tabBatchConvert.Controls.Add(this.groupBox3);
            this.tabBatchConvert.Controls.Add(this.lblPathForBatchConvert);
            this.tabBatchConvert.Controls.Add(this.btnSelectDirForBatchConvert);
            this.tabBatchConvert.Location = new System.Drawing.Point(4, 22);
            this.tabBatchConvert.Name = "tabBatchConvert";
            this.tabBatchConvert.Padding = new System.Windows.Forms.Padding(3);
            this.tabBatchConvert.Size = new System.Drawing.Size(392, 259);
            this.tabBatchConvert.TabIndex = 3;
            this.tabBatchConvert.Text = "Batch convert";
            // 
            // btnSelectDirForBatchConvert
            // 
            this.btnSelectDirForBatchConvert.Location = new System.Drawing.Point(6, 8);
            this.btnSelectDirForBatchConvert.Name = "btnSelectDirForBatchConvert";
            this.btnSelectDirForBatchConvert.Size = new System.Drawing.Size(99, 23);
            this.btnSelectDirForBatchConvert.TabIndex = 0;
            this.btnSelectDirForBatchConvert.Text = "Select directory";
            this.btnSelectDirForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // lblPathForBatchConvert
            // 
            this.lblPathForBatchConvert.AutoEllipsis = true;
            this.lblPathForBatchConvert.AutoSize = true;
            this.lblPathForBatchConvert.Location = new System.Drawing.Point(111, 13);
            this.lblPathForBatchConvert.MaximumSize = new System.Drawing.Size(286, 13);
            this.lblPathForBatchConvert.Name = "lblPathForBatchConvert";
            this.lblPathForBatchConvert.Size = new System.Drawing.Size(35, 13);
            this.lblPathForBatchConvert.TabIndex = 1;
            this.lblPathForBatchConvert.Text = "Path: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkKeepNewLinesForBatchConvert);
            this.groupBox3.Controls.Add(this.chkActorsPerLineForBatchConvert);
            this.groupBox3.Controls.Add(this.chkSeparateActorsForBatchConvert);
            this.groupBox3.Location = new System.Drawing.Point(6, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 95);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Convert options";
            // 
            // chkSeparateActorsForBatchConvert
            // 
            this.chkSeparateActorsForBatchConvert.AutoSize = true;
            this.chkSeparateActorsForBatchConvert.Location = new System.Drawing.Point(7, 22);
            this.chkSeparateActorsForBatchConvert.Name = "chkSeparateActorsForBatchConvert";
            this.chkSeparateActorsForBatchConvert.Size = new System.Drawing.Size(94, 17);
            this.chkSeparateActorsForBatchConvert.TabIndex = 0;
            this.chkSeparateActorsForBatchConvert.Text = "Separate SRT";
            this.chkSeparateActorsForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // chkActorsPerLineForBatchConvert
            // 
            this.chkActorsPerLineForBatchConvert.AutoSize = true;
            this.chkActorsPerLineForBatchConvert.Location = new System.Drawing.Point(7, 47);
            this.chkActorsPerLineForBatchConvert.Name = "chkActorsPerLineForBatchConvert";
            this.chkActorsPerLineForBatchConvert.Size = new System.Drawing.Size(93, 17);
            this.chkActorsPerLineForBatchConvert.TabIndex = 1;
            this.chkActorsPerLineForBatchConvert.Text = "Actors per line";
            this.chkActorsPerLineForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // chkKeepNewLinesForBatchConvert
            // 
            this.chkKeepNewLinesForBatchConvert.AutoSize = true;
            this.chkKeepNewLinesForBatchConvert.Checked = true;
            this.chkKeepNewLinesForBatchConvert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepNewLinesForBatchConvert.Location = new System.Drawing.Point(7, 72);
            this.chkKeepNewLinesForBatchConvert.Name = "chkKeepNewLinesForBatchConvert";
            this.chkKeepNewLinesForBatchConvert.Size = new System.Drawing.Size(98, 17);
            this.chkKeepNewLinesForBatchConvert.TabIndex = 2;
            this.chkKeepNewLinesForBatchConvert.Text = "Keep new lines";
            this.chkKeepNewLinesForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdKeepntAssFormatting);
            this.groupBox4.Controls.Add(this.rdKeepAssFormatting);
            this.groupBox4.Location = new System.Drawing.Point(122, 161);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(116, 71);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Keep ass formatting";
            // 
            // rdKeepAssFormatting
            // 
            this.rdKeepAssFormatting.AutoSize = true;
            this.rdKeepAssFormatting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rdKeepAssFormatting.Location = new System.Drawing.Point(6, 19);
            this.rdKeepAssFormatting.Name = "rdKeepAssFormatting";
            this.rdKeepAssFormatting.Size = new System.Drawing.Size(49, 20);
            this.rdKeepAssFormatting.TabIndex = 0;
            this.rdKeepAssFormatting.Text = "Yes";
            this.rdKeepAssFormatting.UseVisualStyleBackColor = true;
            // 
            // rdKeepntAssFormatting
            // 
            this.rdKeepntAssFormatting.AutoSize = true;
            this.rdKeepntAssFormatting.Checked = true;
            this.rdKeepntAssFormatting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rdKeepntAssFormatting.Location = new System.Drawing.Point(6, 44);
            this.rdKeepntAssFormatting.Name = "rdKeepntAssFormatting";
            this.rdKeepntAssFormatting.Size = new System.Drawing.Size(43, 20);
            this.rdKeepntAssFormatting.TabIndex = 1;
            this.rdKeepntAssFormatting.TabStop = true;
            this.rdKeepntAssFormatting.Text = "No";
            this.rdKeepntAssFormatting.UseVisualStyleBackColor = true;
            // 
            // grpListForBatchConvert
            // 
            this.grpListForBatchConvert.Controls.Add(this.lstAssFilesForBatchConvert);
            this.grpListForBatchConvert.Location = new System.Drawing.Point(6, 37);
            this.grpListForBatchConvert.Name = "grpListForBatchConvert";
            this.grpListForBatchConvert.Size = new System.Drawing.Size(382, 120);
            this.grpListForBatchConvert.TabIndex = 1;
            this.grpListForBatchConvert.TabStop = false;
            this.grpListForBatchConvert.Text = "ASS files list (Selected: 0)";
            // 
            // lstAssFilesForBatchConvert
            // 
            this.lstAssFilesForBatchConvert.FormattingEnabled = true;
            this.lstAssFilesForBatchConvert.Location = new System.Drawing.Point(6, 19);
            this.lstAssFilesForBatchConvert.Name = "lstAssFilesForBatchConvert";
            this.lstAssFilesForBatchConvert.Size = new System.Drawing.Size(370, 95);
            this.lstAssFilesForBatchConvert.TabIndex = 0;
            // 
            // btnConvertForBatchConvert
            // 
            this.btnConvertForBatchConvert.Location = new System.Drawing.Point(244, 201);
            this.btnConvertForBatchConvert.Name = "btnConvertForBatchConvert";
            this.btnConvertForBatchConvert.Size = new System.Drawing.Size(144, 31);
            this.btnConvertForBatchConvert.TabIndex = 5;
            this.btnConvertForBatchConvert.Text = "Convert";
            this.btnConvertForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // btnReloadFilesForBatchConvert
            // 
            this.btnReloadFilesForBatchConvert.Location = new System.Drawing.Point(244, 233);
            this.btnReloadFilesForBatchConvert.Name = "btnReloadFilesForBatchConvert";
            this.btnReloadFilesForBatchConvert.Size = new System.Drawing.Size(71, 23);
            this.btnReloadFilesForBatchConvert.TabIndex = 7;
            this.btnReloadFilesForBatchConvert.Text = "Reload files";
            this.btnReloadFilesForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // btnReportForBatchConvert
            // 
            this.btnReportForBatchConvert.Location = new System.Drawing.Point(317, 233);
            this.btnReportForBatchConvert.Name = "btnReportForBatchConvert";
            this.btnReportForBatchConvert.Size = new System.Drawing.Size(71, 23);
            this.btnReportForBatchConvert.TabIndex = 6;
            this.btnReportForBatchConvert.Text = "Open report";
            this.btnReportForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // chkInOneFolderForBatchConvert
            // 
            this.chkInOneFolderForBatchConvert.AutoSize = true;
            this.chkInOneFolderForBatchConvert.Location = new System.Drawing.Point(122, 239);
            this.chkInOneFolderForBatchConvert.Name = "chkInOneFolderForBatchConvert";
            this.chkInOneFolderForBatchConvert.Size = new System.Drawing.Size(116, 17);
            this.chkInOneFolderForBatchConvert.TabIndex = 4;
            this.chkInOneFolderForBatchConvert.Text = "Put all in one folder";
            this.chkInOneFolderForBatchConvert.UseVisualStyleBackColor = true;
            // 
            // lblConvertionStatusForBatchConvert
            // 
            this.lblConvertionStatusForBatchConvert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConvertionStatusForBatchConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConvertionStatusForBatchConvert.ForeColor = System.Drawing.Color.Red;
            this.lblConvertionStatusForBatchConvert.Location = new System.Drawing.Point(244, 161);
            this.lblConvertionStatusForBatchConvert.Name = "lblConvertionStatusForBatchConvert";
            this.lblConvertionStatusForBatchConvert.Size = new System.Drawing.Size(144, 37);
            this.lblConvertionStatusForBatchConvert.TabIndex = 9;
            this.lblConvertionStatusForBatchConvert.Text = "Converted";
            this.lblConvertionStatusForBatchConvert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
            this.tabBatchImport.ResumeLayout(false);
            this.tabBatchImport.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabBatchConvert.ResumeLayout(false);
            this.tabBatchConvert.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpListForBatchConvert.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblActorsLoadStatus;
        private System.Windows.Forms.Label lblConvertionStatus;
        private System.Windows.Forms.Button btnRemoveLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstToChange;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnToAlloc;
        private System.Windows.Forms.ComboBox cmbDubers;
        private System.Windows.Forms.ComboBox cmbActors;
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
        private System.Windows.Forms.TabPage tabBatchImport;
        private System.Windows.Forms.Button btnSelectDirForBatch;
        private System.Windows.Forms.ListBox lstAssFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblBatchAssSelected;
        private System.Windows.Forms.Label lblPathForBatch;
        private System.Windows.Forms.Button btnSelectMainActors;
        private System.Windows.Forms.Button btnGenerateTimings;
        private System.Windows.Forms.Button btnCreateImports;
        private System.Windows.Forms.Button btnReloadFiles;
        private System.Windows.Forms.Label lblBatchTotalActors;
        private System.Windows.Forms.Label lblBatchLinesWithActors;
        private System.Windows.Forms.Button btnLoadAssNimport;
        private System.Windows.Forms.ToolTip toolTipBatchPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBatch;
        private System.Windows.Forms.CheckBox chkKeepNewLines;
        private System.Windows.Forms.Button btnLoadActorsToList;
        private System.Windows.Forms.Button btnConvertWithAssFormatting;
        private System.Windows.Forms.Label lblActorLines;
        private System.Windows.Forms.Label lblDuberLines;
        private System.Windows.Forms.TabPage tabBatchConvert;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdKeepntAssFormatting;
        private System.Windows.Forms.RadioButton rdKeepAssFormatting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkKeepNewLinesForBatchConvert;
        private System.Windows.Forms.CheckBox chkActorsPerLineForBatchConvert;
        private System.Windows.Forms.CheckBox chkSeparateActorsForBatchConvert;
        private System.Windows.Forms.Label lblPathForBatchConvert;
        private System.Windows.Forms.Button btnSelectDirForBatchConvert;
        private System.Windows.Forms.Button btnConvertForBatchConvert;
        private System.Windows.Forms.GroupBox grpListForBatchConvert;
        private System.Windows.Forms.ListBox lstAssFilesForBatchConvert;
        private System.Windows.Forms.Button btnReportForBatchConvert;
        private System.Windows.Forms.Button btnReloadFilesForBatchConvert;
        private System.Windows.Forms.Label lblConvertionStatusForBatchConvert;
        private System.Windows.Forms.CheckBox chkInOneFolderForBatchConvert;
    }
}

