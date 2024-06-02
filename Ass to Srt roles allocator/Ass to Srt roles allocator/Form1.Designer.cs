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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnRemoveDuber = new System.Windows.Forms.Button();
            this.btnAddDuber = new System.Windows.Forms.Button();
            this.cmbActors = new System.Windows.Forms.ComboBox();
            this.cmbDubers = new System.Windows.Forms.ComboBox();
            this.btnToAlloc = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstToChange = new System.Windows.Forms.ListBox();
            this.btnRemoveLog = new System.Windows.Forms.Button();
            this.lblConvertionStatus = new System.Windows.Forms.Label();
            this.lblLoadStatus = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSaveDeleteDubers = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.chkSeparateActors = new System.Windows.Forms.CheckBox();
            this.chkActorsPerLine = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoEllipsis = true;
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(93, 17);
            this.lblFilePath.MaximumSize = new System.Drawing.Size(310, 13);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(58, 13);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "File name: ";
            // 
            // btnRemoveDuber
            // 
            this.btnRemoveDuber.Enabled = false;
            this.btnRemoveDuber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveDuber.Location = new System.Drawing.Point(268, 87);
            this.btnRemoveDuber.Name = "btnRemoveDuber";
            this.btnRemoveDuber.Size = new System.Drawing.Size(32, 21);
            this.btnRemoveDuber.TabIndex = 1;
            this.btnRemoveDuber.TabStop = false;
            this.btnRemoveDuber.Text = "-";
            this.btnRemoveDuber.UseVisualStyleBackColor = true;
            this.btnRemoveDuber.Click += new System.EventHandler(this.btnRemoveDuber_Click);
            // 
            // btnAddDuber
            // 
            this.btnAddDuber.Enabled = false;
            this.btnAddDuber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddDuber.Location = new System.Drawing.Point(303, 87);
            this.btnAddDuber.Name = "btnAddDuber";
            this.btnAddDuber.Size = new System.Drawing.Size(32, 21);
            this.btnAddDuber.TabIndex = 1;
            this.btnAddDuber.TabStop = false;
            this.btnAddDuber.Text = "+";
            this.btnAddDuber.UseVisualStyleBackColor = true;
            this.btnAddDuber.Click += new System.EventHandler(this.btnAddDuber_Click);
            // 
            // cmbActors
            // 
            this.cmbActors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActors.FormattingEnabled = true;
            this.cmbActors.Location = new System.Drawing.Point(12, 51);
            this.cmbActors.Name = "cmbActors";
            this.cmbActors.Size = new System.Drawing.Size(250, 21);
            this.cmbActors.TabIndex = 1;
            this.cmbActors.SelectedIndexChanged += new System.EventHandler(this.cmbActors_SelectedIndexChanged);
            // 
            // cmbDubers
            // 
            this.cmbDubers.FormattingEnabled = true;
            this.cmbDubers.Location = new System.Drawing.Point(12, 87);
            this.cmbDubers.Name = "cmbDubers";
            this.cmbDubers.Size = new System.Drawing.Size(250, 21);
            this.cmbDubers.TabIndex = 2;
            this.cmbDubers.TextChanged += new System.EventHandler(this.cmbDubers_TextUpdate);
            // 
            // btnToAlloc
            // 
            this.btnToAlloc.Enabled = false;
            this.btnToAlloc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToAlloc.Location = new System.Drawing.Point(126, 114);
            this.btnToAlloc.Name = "btnToAlloc";
            this.btnToAlloc.Size = new System.Drawing.Size(25, 52);
            this.btnToAlloc.TabIndex = 3;
            this.btnToAlloc.Text = "↓";
            this.btnToAlloc.UseVisualStyleBackColor = true;
            this.btnToAlloc.Click += new System.EventHandler(this.btnToAlloc_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Transparent;
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(268, 179);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(116, 41);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstToChange);
            this.groupBox1.Location = new System.Drawing.Point(12, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 106);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To change";
            // 
            // lstToChange
            // 
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
            // 
            // btnRemoveLog
            // 
            this.btnRemoveLog.Enabled = false;
            this.btnRemoveLog.Location = new System.Drawing.Point(268, 226);
            this.btnRemoveLog.Name = "btnRemoveLog";
            this.btnRemoveLog.Size = new System.Drawing.Size(116, 20);
            this.btnRemoveLog.TabIndex = 2;
            this.btnRemoveLog.TabStop = false;
            this.btnRemoveLog.Text = "Remove";
            this.btnRemoveLog.UseVisualStyleBackColor = true;
            this.btnRemoveLog.Click += new System.EventHandler(this.btnRemoveLog_Click);
            // 
            // lblConvertionStatus
            // 
            this.lblConvertionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConvertionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConvertionStatus.Location = new System.Drawing.Point(268, 130);
            this.lblConvertionStatus.Name = "lblConvertionStatus";
            this.lblConvertionStatus.Size = new System.Drawing.Size(121, 23);
            this.lblConvertionStatus.TabIndex = 12;
            this.lblConvertionStatus.Text = "Converted";
            this.lblConvertionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConvertionStatus.DoubleClick += new System.EventHandler(this.lblConvertionStatus_DoubleClick);
            // 
            // lblLoadStatus
            // 
            this.lblLoadStatus.AutoSize = true;
            this.lblLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoadStatus.ForeColor = System.Drawing.Color.Red;
            this.lblLoadStatus.Location = new System.Drawing.Point(269, 52);
            this.lblLoadStatus.Name = "lblLoadStatus";
            this.lblLoadStatus.Size = new System.Drawing.Size(107, 20);
            this.lblLoadStatus.TabIndex = 13;
            this.lblLoadStatus.Text = "Actors loaded";
            this.lblLoadStatus.ForeColorChanged += new System.EventHandler(this.lblLoadStatus_ForeColorChanged);
            this.lblLoadStatus.DoubleClick += new System.EventHandler(this.lblLoadStatus_DoubleClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(327, 252);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(55, 20);
            this.btnExport.TabIndex = 16;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(268, 252);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(55, 20);
            this.btnImport.TabIndex = 16;
            this.btnImport.TabStop = false;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSaveDeleteDubers
            // 
            this.btnSaveDeleteDubers.Enabled = false;
            this.btnSaveDeleteDubers.Location = new System.Drawing.Point(342, 87);
            this.btnSaveDeleteDubers.Name = "btnSaveDeleteDubers";
            this.btnSaveDeleteDubers.Size = new System.Drawing.Size(40, 21);
            this.btnSaveDeleteDubers.TabIndex = 17;
            this.btnSaveDeleteDubers.TabStop = false;
            this.btnSaveDeleteDubers.Text = "Save";
            this.btnSaveDeleteDubers.UseVisualStyleBackColor = true;
            this.btnSaveDeleteDubers.Click += new System.EventHandler(this.btnSaveDeleteDubers_Click);
            // 
            // chkSeparateActors
            // 
            this.chkSeparateActors.AutoSize = true;
            this.chkSeparateActors.Enabled = false;
            this.chkSeparateActors.Location = new System.Drawing.Point(168, 120);
            this.chkSeparateActors.Name = "chkSeparateActors";
            this.chkSeparateActors.Size = new System.Drawing.Size(94, 17);
            this.chkSeparateActors.TabIndex = 18;
            this.chkSeparateActors.Text = "Separate SRT";
            this.chkSeparateActors.UseVisualStyleBackColor = true;
            this.chkSeparateActors.CheckedChanged += new System.EventHandler(this.chkSeparateActors_CheckedChanged);
            // 
            // chkActorsPerLine
            // 
            this.chkActorsPerLine.AutoSize = true;
            this.chkActorsPerLine.Enabled = false;
            this.chkActorsPerLine.Location = new System.Drawing.Point(168, 143);
            this.chkActorsPerLine.Name = "chkActorsPerLine";
            this.chkActorsPerLine.Size = new System.Drawing.Size(93, 17);
            this.chkActorsPerLine.TabIndex = 19;
            this.chkActorsPerLine.Text = "Actors per line";
            this.chkActorsPerLine.UseVisualStyleBackColor = true;
            this.chkActorsPerLine.CheckedChanged += new System.EventHandler(this.chkActorsPerLine_CheckedChanged);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 284);
            this.Controls.Add(this.chkActorsPerLine);
            this.Controls.Add(this.chkSeparateActors);
            this.Controls.Add(this.btnSaveDeleteDubers);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblLoadStatus);
            this.Controls.Add(this.lblConvertionStatus);
            this.Controls.Add(this.btnRemoveLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnToAlloc);
            this.Controls.Add(this.cmbDubers);
            this.Controls.Add(this.cmbActors);
            this.Controls.Add(this.btnAddDuber);
            this.Controls.Add(this.btnRemoveDuber);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASS to SRT";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnRemoveDuber;
        private System.Windows.Forms.Button btnAddDuber;
        private System.Windows.Forms.ComboBox cmbActors;
        private System.Windows.Forms.ComboBox cmbDubers;
        private System.Windows.Forms.Button btnToAlloc;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveLog;
        private System.Windows.Forms.Label lblConvertionStatus;
        private System.Windows.Forms.ListBox lstToChange;
        private System.Windows.Forms.Label lblLoadStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSaveDeleteDubers;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox chkSeparateActors;
        private System.Windows.Forms.CheckBox chkActorsPerLine;
    }
}

