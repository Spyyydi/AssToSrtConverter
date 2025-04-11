namespace Ass_to_Srt_roles_allocator
{
    partial class BatchMainActorsSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchMainActorsSelector));
            this.lstAllocedActors = new System.Windows.Forms.ListBox();
            this.lstMainActors = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblMainActors = new System.Windows.Forms.Label();
            this.lblAllocedActors = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstAllocedActors
            // 
            this.lstAllocedActors.FormattingEnabled = true;
            this.lstAllocedActors.HorizontalScrollbar = true;
            this.lstAllocedActors.Location = new System.Drawing.Point(3, 21);
            this.lstAllocedActors.Name = "lstAllocedActors";
            this.lstAllocedActors.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllocedActors.Size = new System.Drawing.Size(183, 264);
            this.lstAllocedActors.Sorted = true;
            this.lstAllocedActors.TabIndex = 0;
            this.lstAllocedActors.SelectedIndexChanged += new System.EventHandler(this.lstAllocedActors_SelectedIndexChanged);
            // 
            // lstMainActors
            // 
            this.lstMainActors.FormattingEnabled = true;
            this.lstMainActors.HorizontalScrollbar = true;
            this.lstMainActors.Location = new System.Drawing.Point(248, 21);
            this.lstMainActors.Name = "lstMainActors";
            this.lstMainActors.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMainActors.Size = new System.Drawing.Size(183, 238);
            this.lstMainActors.Sorted = true;
            this.lstMainActors.TabIndex = 2;
            this.lstMainActors.SelectedIndexChanged += new System.EventHandler(this.lstMainActors_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(3, 291);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(428, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(248, 262);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMove
            // 
            this.btnMove.Enabled = false;
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMove.Location = new System.Drawing.Point(192, 138);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(50, 30);
            this.btnMove.TabIndex = 1;
            this.btnMove.Text = ">>";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(341, 262);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblMainActors
            // 
            this.lblMainActors.AutoSize = true;
            this.lblMainActors.Location = new System.Drawing.Point(245, 5);
            this.lblMainActors.Name = "lblMainActors";
            this.lblMainActors.Size = new System.Drawing.Size(136, 13);
            this.lblMainActors.TabIndex = 6;
            this.lblMainActors.Text = "Main actors: 0 | Selected: 0";
            // 
            // lblAllocedActors
            // 
            this.lblAllocedActors.AutoSize = true;
            this.lblAllocedActors.Location = new System.Drawing.Point(0, 5);
            this.lblAllocedActors.Name = "lblAllocedActors";
            this.lblAllocedActors.Size = new System.Drawing.Size(157, 13);
            this.lblAllocedActors.TabIndex = 7;
            this.lblAllocedActors.Text = "Allocated actors: 0 | Selected: 0";
            // 
            // BatchMainActorsSelector
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(435, 321);
            this.ControlBox = false;
            this.Controls.Add(this.lblAllocedActors);
            this.Controls.Add(this.lblMainActors);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstMainActors);
            this.Controls.Add(this.lstAllocedActors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchMainActorsSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main actors selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstAllocedActors;
        private System.Windows.Forms.ListBox lstMainActors;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblMainActors;
        private System.Windows.Forms.Label lblAllocedActors;
    }
}