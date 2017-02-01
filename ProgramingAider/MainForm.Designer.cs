namespace ProgramingAider {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPrgDir = new System.Windows.Forms.Button();
            this.buttonIDE = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCmpDirTo = new System.Windows.Forms.Button();
            this.buttonCmpDirFrom = new System.Windows.Forms.Button();
            this.buttonReadMe = new System.Windows.Forms.Button();
            this.buttonCompress = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonBrowser = new System.Windows.Forms.Button();
            this.buttonUploader = new System.Windows.Forms.Button();
            this.buttonPbDir = new System.Windows.Forms.Button();
            this.buttonEditor = new System.Windows.Forms.Button();
            this.comboBoxSolution = new System.Windows.Forms.ComboBox();
            this.textBoxNewVer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonMoveZip = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonExe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLastVer = new System.Windows.Forms.TextBox();
            this.buttonMoveExe = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPrgDir);
            this.groupBox1.Controls.Add(this.buttonIDE);
            this.groupBox1.Location = new System.Drawing.Point(12, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(76, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Programing";
            // 
            // buttonPrgDir
            // 
            this.buttonPrgDir.Location = new System.Drawing.Point(6, 88);
            this.buttonPrgDir.Name = "buttonPrgDir";
            this.buttonPrgDir.Size = new System.Drawing.Size(64, 64);
            this.buttonPrgDir.TabIndex = 5;
            this.buttonPrgDir.UseVisualStyleBackColor = true;
            this.buttonPrgDir.Click += new System.EventHandler(this.buttonPrgDir_Click);
            // 
            // buttonIDE
            // 
            this.buttonIDE.Location = new System.Drawing.Point(6, 18);
            this.buttonIDE.Name = "buttonIDE";
            this.buttonIDE.Size = new System.Drawing.Size(64, 64);
            this.buttonIDE.TabIndex = 4;
            this.buttonIDE.UseVisualStyleBackColor = true;
            this.buttonIDE.Click += new System.EventHandler(this.buttonIDE_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCmpDirTo);
            this.groupBox2.Controls.Add(this.buttonCmpDirFrom);
            this.groupBox2.Controls.Add(this.buttonReadMe);
            this.groupBox2.Controls.Add(this.buttonCompress);
            this.groupBox2.Location = new System.Drawing.Point(132, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 162);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Distribution";
            // 
            // buttonCmpDirTo
            // 
            this.buttonCmpDirTo.Location = new System.Drawing.Point(77, 88);
            this.buttonCmpDirTo.Name = "buttonCmpDirTo";
            this.buttonCmpDirTo.Size = new System.Drawing.Size(64, 64);
            this.buttonCmpDirTo.TabIndex = 10;
            this.buttonCmpDirTo.UseVisualStyleBackColor = true;
            this.buttonCmpDirTo.Click += new System.EventHandler(this.buttonCmpDirTo_Click);
            // 
            // buttonCmpDirFrom
            // 
            this.buttonCmpDirFrom.Location = new System.Drawing.Point(6, 88);
            this.buttonCmpDirFrom.Name = "buttonCmpDirFrom";
            this.buttonCmpDirFrom.Size = new System.Drawing.Size(64, 64);
            this.buttonCmpDirFrom.TabIndex = 9;
            this.buttonCmpDirFrom.UseVisualStyleBackColor = true;
            this.buttonCmpDirFrom.Click += new System.EventHandler(this.buttonCmpDirFrom_Click);
            // 
            // buttonReadMe
            // 
            this.buttonReadMe.Location = new System.Drawing.Point(6, 18);
            this.buttonReadMe.Name = "buttonReadMe";
            this.buttonReadMe.Size = new System.Drawing.Size(64, 64);
            this.buttonReadMe.TabIndex = 8;
            this.buttonReadMe.UseVisualStyleBackColor = true;
            this.buttonReadMe.Click += new System.EventHandler(this.buttonReadMe_Click);
            // 
            // buttonCompress
            // 
            this.buttonCompress.Location = new System.Drawing.Point(76, 18);
            this.buttonCompress.Name = "buttonCompress";
            this.buttonCompress.Size = new System.Drawing.Size(64, 64);
            this.buttonCompress.TabIndex = 3;
            this.buttonCompress.Text = "Compress";
            this.toolTip1.SetToolTip(this.buttonCompress, "Compress");
            this.buttonCompress.UseVisualStyleBackColor = true;
            this.buttonCompress.Click += new System.EventHandler(this.buttonCompress_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonBrowser);
            this.groupBox3.Controls.Add(this.buttonUploader);
            this.groupBox3.Controls.Add(this.buttonPbDir);
            this.groupBox3.Controls.Add(this.buttonEditor);
            this.groupBox3.Location = new System.Drawing.Point(322, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 162);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Publishing";
            // 
            // buttonBrowser
            // 
            this.buttonBrowser.Location = new System.Drawing.Point(76, 88);
            this.buttonBrowser.Name = "buttonBrowser";
            this.buttonBrowser.Size = new System.Drawing.Size(64, 64);
            this.buttonBrowser.TabIndex = 12;
            this.buttonBrowser.UseVisualStyleBackColor = true;
            this.buttonBrowser.Click += new System.EventHandler(this.buttonBrowser_Click);
            // 
            // buttonUploader
            // 
            this.buttonUploader.Location = new System.Drawing.Point(76, 18);
            this.buttonUploader.Name = "buttonUploader";
            this.buttonUploader.Size = new System.Drawing.Size(64, 64);
            this.buttonUploader.TabIndex = 11;
            this.buttonUploader.UseVisualStyleBackColor = true;
            this.buttonUploader.Click += new System.EventHandler(this.buttonUploader_Click);
            // 
            // buttonPbDir
            // 
            this.buttonPbDir.Location = new System.Drawing.Point(6, 88);
            this.buttonPbDir.Name = "buttonPbDir";
            this.buttonPbDir.Size = new System.Drawing.Size(64, 64);
            this.buttonPbDir.TabIndex = 10;
            this.buttonPbDir.UseVisualStyleBackColor = true;
            this.buttonPbDir.Click += new System.EventHandler(this.buttonPbDir_Click);
            // 
            // buttonEditor
            // 
            this.buttonEditor.Location = new System.Drawing.Point(6, 18);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Size = new System.Drawing.Size(64, 64);
            this.buttonEditor.TabIndex = 9;
            this.buttonEditor.UseVisualStyleBackColor = true;
            this.buttonEditor.Click += new System.EventHandler(this.buttonEditor_Click);
            // 
            // comboBoxSolution
            // 
            this.comboBoxSolution.DisplayMember = "(None)";
            this.comboBoxSolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSolution.FormattingEnabled = true;
            this.comboBoxSolution.Items.AddRange(new object[] {
            "(None)"});
            this.comboBoxSolution.Location = new System.Drawing.Point(76, 18);
            this.comboBoxSolution.MaxDropDownItems = 20;
            this.comboBoxSolution.Name = "comboBoxSolution";
            this.comboBoxSolution.Size = new System.Drawing.Size(374, 20);
            this.comboBoxSolution.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBoxSolution, "Solution");
            this.comboBoxSolution.SelectedIndexChanged += new System.EventHandler(this.comboBoxSolution_SelectedIndexChanged);
            // 
            // textBoxNewVer
            // 
            this.textBoxNewVer.Location = new System.Drawing.Point(310, 44);
            this.textBoxNewVer.Name = "textBoxNewVer";
            this.textBoxNewVer.Size = new System.Drawing.Size(88, 19);
            this.textBoxNewVer.TabIndex = 5;
            this.textBoxNewVer.Leave += new System.EventHandler(this.textBoxVer_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "New Ver.";
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(404, 46);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(46, 16);
            this.checkBoxAuto.TabIndex = 7;
            this.checkBoxAuto.Text = "auto";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            this.checkBoxAuto.CheckedChanged += new System.EventHandler(this.checkBoxAuto_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.toolTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(475, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitEToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitEToolStripMenuItem.Text = "Exit(&X)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.exitEToolStripMenuItem_Click);
            // 
            // toolTToolStripMenuItem
            // 
            this.toolTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.toolTToolStripMenuItem.Name = "toolTToolStripMenuItem";
            this.toolTToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.toolTToolStripMenuItem.Text = "Tool(&T)";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.optionToolStripMenuItem.Text = "Option(&O)...";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // buttonMoveZip
            // 
            this.buttonMoveZip.Location = new System.Drawing.Point(284, 141);
            this.buttonMoveZip.Name = "buttonMoveZip";
            this.buttonMoveZip.Size = new System.Drawing.Size(32, 64);
            this.buttonMoveZip.TabIndex = 12;
            this.buttonMoveZip.Text = "→";
            this.toolTip1.SetToolTip(this.buttonMoveZip, "Move zip and Rename");
            this.buttonMoveZip.UseVisualStyleBackColor = true;
            this.buttonMoveZip.Click += new System.EventHandler(this.buttonMoveZip_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonExe);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textBoxLastVer);
            this.groupBox4.Controls.Add(this.comboBoxSolution);
            this.groupBox4.Controls.Add(this.checkBoxAuto);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBoxNewVer);
            this.groupBox4.Location = new System.Drawing.Point(12, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(456, 90);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "About";
            // 
            // buttonExe
            // 
            this.buttonExe.Location = new System.Drawing.Point(6, 18);
            this.buttonExe.Name = "buttonExe";
            this.buttonExe.Size = new System.Drawing.Size(64, 64);
            this.buttonExe.TabIndex = 11;
            this.buttonExe.UseVisualStyleBackColor = true;
            this.buttonExe.Click += new System.EventHandler(this.buttonExe_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Last Ver.";
            // 
            // textBoxLastVer
            // 
            this.textBoxLastVer.Location = new System.Drawing.Point(159, 44);
            this.textBoxLastVer.Name = "textBoxLastVer";
            this.textBoxLastVer.ReadOnly = true;
            this.textBoxLastVer.Size = new System.Drawing.Size(88, 19);
            this.textBoxLastVer.TabIndex = 9;
            // 
            // buttonMoveExe
            // 
            this.buttonMoveExe.Location = new System.Drawing.Point(94, 141);
            this.buttonMoveExe.Name = "buttonMoveExe";
            this.buttonMoveExe.Size = new System.Drawing.Size(32, 64);
            this.buttonMoveExe.TabIndex = 11;
            this.buttonMoveExe.Text = "→";
            this.buttonMoveExe.UseVisualStyleBackColor = true;
            this.buttonMoveExe.Click += new System.EventHandler(this.buttonMoveExe_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 292);
            this.Controls.Add(this.buttonMoveZip);
            this.Controls.Add(this.buttonMoveExe);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Programing Aider";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCompress;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxSolution;
        private System.Windows.Forms.TextBox textBoxNewVer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAuto;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonMoveExe;
        private System.Windows.Forms.Button buttonMoveZip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLastVer;
        private System.Windows.Forms.Button buttonIDE;
        private System.Windows.Forms.Button buttonPrgDir;
        private System.Windows.Forms.Button buttonReadMe;
        private System.Windows.Forms.Button buttonCmpDirFrom;
        private System.Windows.Forms.Button buttonCmpDirTo;
        private System.Windows.Forms.Button buttonEditor;
        private System.Windows.Forms.Button buttonPbDir;
        private System.Windows.Forms.Button buttonUploader;
        private System.Windows.Forms.Button buttonBrowser;
        private System.Windows.Forms.Button buttonExe;
    }
}

