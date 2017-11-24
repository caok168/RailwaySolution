namespace BNYTool
{
    partial class BatchExportCitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchExportCitForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxUpDown = new System.Windows.Forms.ComboBox();
            this.cbxKmInc = new System.Windows.Forms.ComboBox();
            this.cbxRunDir = new System.Windows.Forms.ComboBox();
            this.cbxFileType = new System.Windows.Forms.ComboBox();
            this.traincodenewvalue = new System.Windows.Forms.TextBox();
            this.tracknamenewvalue = new System.Windows.Forms.TextBox();
            this.trackcodenewvalue = new System.Windows.Forms.TextBox();
            this.dataversionnewvalue = new System.Windows.Forms.TextBox();
            this.kmincvalue = new System.Windows.Forms.Label();
            this.rundirvalue = new System.Windows.Forms.Label();
            this.traincodevalue = new System.Windows.Forms.Label();
            this.dirvalue = new System.Windows.Forms.Label();
            this.tracknamevalue = new System.Windows.Forms.Label();
            this.trackcodevalue = new System.Windows.Forms.Label();
            this.dataversionvalue = new System.Windows.Forms.Label();
            this.datatypevalue = new System.Windows.Forms.Label();
            this.kminc = new System.Windows.Forms.Label();
            this.rundir = new System.Windows.Forms.Label();
            this.traincode = new System.Windows.Forms.Label();
            this.dir = new System.Windows.Forms.Label();
            this.trackname = new System.Windows.Forms.Label();
            this.trackcode = new System.Windows.Forms.Label();
            this.dataversion = new System.Windows.Forms.Label();
            this.datatype = new System.Windows.Forms.Label();
            this.btnSelectImportPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImportPath = new System.Windows.Forms.TextBox();
            this.btnSelectExportPath = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.labExport = new System.Windows.Forms.Label();
            this.btnBgeinExport = new System.Windows.Forms.Button();
            this.btnCancelExport = new System.Windows.Forms.Button();
            this.folderBrowserImportDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserExportDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxUpDown);
            this.groupBox1.Controls.Add(this.cbxKmInc);
            this.groupBox1.Controls.Add(this.cbxRunDir);
            this.groupBox1.Controls.Add(this.cbxFileType);
            this.groupBox1.Controls.Add(this.traincodenewvalue);
            this.groupBox1.Controls.Add(this.tracknamenewvalue);
            this.groupBox1.Controls.Add(this.trackcodenewvalue);
            this.groupBox1.Controls.Add(this.dataversionnewvalue);
            this.groupBox1.Controls.Add(this.kmincvalue);
            this.groupBox1.Controls.Add(this.rundirvalue);
            this.groupBox1.Controls.Add(this.traincodevalue);
            this.groupBox1.Controls.Add(this.dirvalue);
            this.groupBox1.Controls.Add(this.tracknamevalue);
            this.groupBox1.Controls.Add(this.trackcodevalue);
            this.groupBox1.Controls.Add(this.dataversionvalue);
            this.groupBox1.Controls.Add(this.datatypevalue);
            this.groupBox1.Controls.Add(this.kminc);
            this.groupBox1.Controls.Add(this.rundir);
            this.groupBox1.Controls.Add(this.traincode);
            this.groupBox1.Controls.Add(this.dir);
            this.groupBox1.Controls.Add(this.trackname);
            this.groupBox1.Controls.Add(this.trackcode);
            this.groupBox1.Controls.Add(this.dataversion);
            this.groupBox1.Controls.Add(this.datatype);
            this.groupBox1.Location = new System.Drawing.Point(20, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 254);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件信息";
            // 
            // cbxUpDown
            // 
            this.cbxUpDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUpDown.FormattingEnabled = true;
            this.cbxUpDown.Location = new System.Drawing.Point(361, 125);
            this.cbxUpDown.Name = "cbxUpDown";
            this.cbxUpDown.Size = new System.Drawing.Size(202, 20);
            this.cbxUpDown.TabIndex = 87;
            this.cbxUpDown.SelectedIndexChanged += new System.EventHandler(this.cbxUpDown_SelectedIndexChanged);
            // 
            // cbxKmInc
            // 
            this.cbxKmInc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKmInc.FormattingEnabled = true;
            this.cbxKmInc.Items.AddRange(new object[] {
            "",
            "0",
            "1"});
            this.cbxKmInc.Location = new System.Drawing.Point(361, 208);
            this.cbxKmInc.Name = "cbxKmInc";
            this.cbxKmInc.Size = new System.Drawing.Size(202, 20);
            this.cbxKmInc.TabIndex = 86;
            this.cbxKmInc.SelectedIndexChanged += new System.EventHandler(this.cbxKmInc_SelectedIndexChanged);
            // 
            // cbxRunDir
            // 
            this.cbxRunDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunDir.FormattingEnabled = true;
            this.cbxRunDir.Location = new System.Drawing.Point(361, 181);
            this.cbxRunDir.Name = "cbxRunDir";
            this.cbxRunDir.Size = new System.Drawing.Size(202, 20);
            this.cbxRunDir.TabIndex = 85;
            this.cbxRunDir.SelectedIndexChanged += new System.EventHandler(this.cbxRunDir_SelectedIndexChanged);
            // 
            // cbxFileType
            // 
            this.cbxFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFileType.FormattingEnabled = true;
            this.cbxFileType.Location = new System.Drawing.Point(361, 21);
            this.cbxFileType.Name = "cbxFileType";
            this.cbxFileType.Size = new System.Drawing.Size(202, 20);
            this.cbxFileType.TabIndex = 84;
            this.cbxFileType.SelectedIndexChanged += new System.EventHandler(this.cbxFileType_SelectedIndexChanged);
            // 
            // traincodenewvalue
            // 
            this.traincodenewvalue.Location = new System.Drawing.Point(361, 152);
            this.traincodenewvalue.MaxLength = 20;
            this.traincodenewvalue.Name = "traincodenewvalue";
            this.traincodenewvalue.Size = new System.Drawing.Size(202, 21);
            this.traincodenewvalue.TabIndex = 76;
            this.traincodenewvalue.Leave += new System.EventHandler(this.traincodenewvalue_Leave);
            this.traincodenewvalue.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.traincodenewvalue_PreviewKeyDown);
            // 
            // tracknamenewvalue
            // 
            this.tracknamenewvalue.Location = new System.Drawing.Point(361, 97);
            this.tracknamenewvalue.MaxLength = 20;
            this.tracknamenewvalue.Name = "tracknamenewvalue";
            this.tracknamenewvalue.Size = new System.Drawing.Size(202, 21);
            this.tracknamenewvalue.TabIndex = 74;
            this.tracknamenewvalue.Leave += new System.EventHandler(this.tracknamenewvalue_Leave);
            // 
            // trackcodenewvalue
            // 
            this.trackcodenewvalue.Location = new System.Drawing.Point(361, 71);
            this.trackcodenewvalue.MaxLength = 4;
            this.trackcodenewvalue.Name = "trackcodenewvalue";
            this.trackcodenewvalue.Size = new System.Drawing.Size(202, 21);
            this.trackcodenewvalue.TabIndex = 73;
            this.trackcodenewvalue.Leave += new System.EventHandler(this.trackcodenewvalue_Leave);
            this.trackcodenewvalue.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.trackcodenewvalue_PreviewKeyDown);
            // 
            // dataversionnewvalue
            // 
            this.dataversionnewvalue.Location = new System.Drawing.Point(361, 46);
            this.dataversionnewvalue.Name = "dataversionnewvalue";
            this.dataversionnewvalue.Size = new System.Drawing.Size(202, 21);
            this.dataversionnewvalue.TabIndex = 72;
            this.dataversionnewvalue.Leave += new System.EventHandler(this.dataversionnewvalue_Leave);
            this.dataversionnewvalue.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataversionnewvalue_PreviewKeyDown);
            // 
            // kmincvalue
            // 
            this.kmincvalue.AutoSize = true;
            this.kmincvalue.Location = new System.Drawing.Point(189, 215);
            this.kmincvalue.Name = "kmincvalue";
            this.kmincvalue.Size = new System.Drawing.Size(0, 12);
            this.kmincvalue.TabIndex = 66;
            // 
            // rundirvalue
            // 
            this.rundirvalue.AutoSize = true;
            this.rundirvalue.Location = new System.Drawing.Point(189, 187);
            this.rundirvalue.Name = "rundirvalue";
            this.rundirvalue.Size = new System.Drawing.Size(0, 12);
            this.rundirvalue.TabIndex = 65;
            // 
            // traincodevalue
            // 
            this.traincodevalue.AutoSize = true;
            this.traincodevalue.Location = new System.Drawing.Point(189, 158);
            this.traincodevalue.Name = "traincodevalue";
            this.traincodevalue.Size = new System.Drawing.Size(0, 12);
            this.traincodevalue.TabIndex = 62;
            // 
            // dirvalue
            // 
            this.dirvalue.AutoSize = true;
            this.dirvalue.Location = new System.Drawing.Point(189, 131);
            this.dirvalue.Name = "dirvalue";
            this.dirvalue.Size = new System.Drawing.Size(0, 12);
            this.dirvalue.TabIndex = 61;
            // 
            // tracknamevalue
            // 
            this.tracknamevalue.AutoSize = true;
            this.tracknamevalue.Location = new System.Drawing.Point(189, 102);
            this.tracknamevalue.Name = "tracknamevalue";
            this.tracknamevalue.Size = new System.Drawing.Size(0, 12);
            this.tracknamevalue.TabIndex = 60;
            // 
            // trackcodevalue
            // 
            this.trackcodevalue.AutoSize = true;
            this.trackcodevalue.Location = new System.Drawing.Point(189, 77);
            this.trackcodevalue.Name = "trackcodevalue";
            this.trackcodevalue.Size = new System.Drawing.Size(0, 12);
            this.trackcodevalue.TabIndex = 59;
            // 
            // dataversionvalue
            // 
            this.dataversionvalue.AutoSize = true;
            this.dataversionvalue.Location = new System.Drawing.Point(189, 55);
            this.dataversionvalue.Name = "dataversionvalue";
            this.dataversionvalue.Size = new System.Drawing.Size(0, 12);
            this.dataversionvalue.TabIndex = 58;
            // 
            // datatypevalue
            // 
            this.datatypevalue.AutoSize = true;
            this.datatypevalue.Location = new System.Drawing.Point(189, 29);
            this.datatypevalue.Name = "datatypevalue";
            this.datatypevalue.Size = new System.Drawing.Size(0, 12);
            this.datatypevalue.TabIndex = 57;
            // 
            // kminc
            // 
            this.kminc.AutoSize = true;
            this.kminc.Location = new System.Drawing.Point(14, 215);
            this.kminc.Name = "kminc";
            this.kminc.Size = new System.Drawing.Size(59, 12);
            this.kminc.TabIndex = 52;
            this.kminc.Text = "增减里程:";
            // 
            // rundir
            // 
            this.rundir.AutoSize = true;
            this.rundir.Location = new System.Drawing.Point(14, 187);
            this.rundir.Name = "rundir";
            this.rundir.Size = new System.Drawing.Size(59, 12);
            this.rundir.TabIndex = 51;
            this.rundir.Text = "检测方向:";
            // 
            // traincode
            // 
            this.traincode.AutoSize = true;
            this.traincode.Location = new System.Drawing.Point(14, 158);
            this.traincode.Name = "traincode";
            this.traincode.Size = new System.Drawing.Size(143, 12);
            this.traincode.TabIndex = 48;
            this.traincode.Text = "检测车号(20个字符以内):";
            // 
            // dir
            // 
            this.dir.AutoSize = true;
            this.dir.Location = new System.Drawing.Point(14, 131);
            this.dir.Name = "dir";
            this.dir.Size = new System.Drawing.Size(35, 12);
            this.dir.TabIndex = 47;
            this.dir.Text = "行别:";
            // 
            // trackname
            // 
            this.trackname.AutoSize = true;
            this.trackname.Location = new System.Drawing.Point(14, 102);
            this.trackname.Name = "trackname";
            this.trackname.Size = new System.Drawing.Size(131, 12);
            this.trackname.TabIndex = 46;
            this.trackname.Text = "线路名(20个字符以内):";
            // 
            // trackcode
            // 
            this.trackcode.AutoSize = true;
            this.trackcode.Location = new System.Drawing.Point(14, 77);
            this.trackcode.Name = "trackcode";
            this.trackcode.Size = new System.Drawing.Size(137, 12);
            this.trackcode.TabIndex = 45;
            this.trackcode.Text = "线路代码(4个字符以内):";
            // 
            // dataversion
            // 
            this.dataversion.AutoSize = true;
            this.dataversion.Location = new System.Drawing.Point(14, 55);
            this.dataversion.Name = "dataversion";
            this.dataversion.Size = new System.Drawing.Size(71, 12);
            this.dataversion.TabIndex = 44;
            this.dataversion.Text = "文件版本号:";
            // 
            // datatype
            // 
            this.datatype.AutoSize = true;
            this.datatype.Location = new System.Drawing.Point(14, 29);
            this.datatype.Name = "datatype";
            this.datatype.Size = new System.Drawing.Size(59, 12);
            this.datatype.TabIndex = 43;
            this.datatype.Text = "文件类型:";
            // 
            // btnSelectImportPath
            // 
            this.btnSelectImportPath.Location = new System.Drawing.Point(550, 13);
            this.btnSelectImportPath.Name = "btnSelectImportPath";
            this.btnSelectImportPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectImportPath.TabIndex = 1;
            this.btnSelectImportPath.Text = "选择目录";
            this.btnSelectImportPath.UseVisualStyleBackColor = true;
            this.btnSelectImportPath.Click += new System.EventHandler(this.btnSelectImportPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "导入bny目录：";
            // 
            // txtImportPath
            // 
            this.txtImportPath.Location = new System.Drawing.Point(89, 14);
            this.txtImportPath.Name = "txtImportPath";
            this.txtImportPath.ReadOnly = true;
            this.txtImportPath.Size = new System.Drawing.Size(442, 21);
            this.txtImportPath.TabIndex = 54;
            // 
            // btnSelectExportPath
            // 
            this.btnSelectExportPath.Location = new System.Drawing.Point(567, 319);
            this.btnSelectExportPath.Name = "btnSelectExportPath";
            this.btnSelectExportPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectExportPath.TabIndex = 55;
            this.btnSelectExportPath.Text = "选择目录";
            this.btnSelectExportPath.UseVisualStyleBackColor = true;
            this.btnSelectExportPath.Click += new System.EventHandler(this.btnSelectExportPath_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(99, 321);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(442, 21);
            this.txtExportPath.TabIndex = 57;
            // 
            // labExport
            // 
            this.labExport.AutoSize = true;
            this.labExport.Location = new System.Drawing.Point(21, 326);
            this.labExport.Name = "labExport";
            this.labExport.Size = new System.Drawing.Size(83, 12);
            this.labExport.TabIndex = 56;
            this.labExport.Text = "导出cit目录：";
            // 
            // btnBgeinExport
            // 
            this.btnBgeinExport.Location = new System.Drawing.Point(567, 371);
            this.btnBgeinExport.Name = "btnBgeinExport";
            this.btnBgeinExport.Size = new System.Drawing.Size(75, 23);
            this.btnBgeinExport.TabIndex = 58;
            this.btnBgeinExport.Text = "开始导出";
            this.btnBgeinExport.UseVisualStyleBackColor = true;
            this.btnBgeinExport.Click += new System.EventHandler(this.btnBgeinExport_Click);
            // 
            // btnCancelExport
            // 
            this.btnCancelExport.Location = new System.Drawing.Point(457, 371);
            this.btnCancelExport.Name = "btnCancelExport";
            this.btnCancelExport.Size = new System.Drawing.Size(75, 23);
            this.btnCancelExport.TabIndex = 59;
            this.btnCancelExport.Text = "取消导出";
            this.btnCancelExport.UseVisualStyleBackColor = true;
            this.btnCancelExport.Click += new System.EventHandler(this.btnCancelExport_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtImportPath);
            this.groupBox2.Controls.Add(this.btnSelectImportPath);
            this.groupBox2.Location = new System.Drawing.Point(12, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(630, 43);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 371);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Maximum = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(199, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 89;
            this.progressBar1.Visible = false;
            // 
            // BatchExportCitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 412);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelExport);
            this.Controls.Add(this.btnBgeinExport);
            this.Controls.Add(this.txtExportPath);
            this.Controls.Add(this.labExport);
            this.Controls.Add(this.btnSelectExportPath);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BatchExportCitForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量导出cit";
            this.Load += new System.EventHandler(this.BatchExportCitForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox traincodenewvalue;
        private System.Windows.Forms.TextBox tracknamenewvalue;
        private System.Windows.Forms.TextBox trackcodenewvalue;
        private System.Windows.Forms.TextBox dataversionnewvalue;
        private System.Windows.Forms.Label kmincvalue;
        private System.Windows.Forms.Label rundirvalue;
        private System.Windows.Forms.Label traincodevalue;
        private System.Windows.Forms.Label dirvalue;
        private System.Windows.Forms.Label tracknamevalue;
        private System.Windows.Forms.Label trackcodevalue;
        private System.Windows.Forms.Label dataversionvalue;
        private System.Windows.Forms.Label datatypevalue;
        private System.Windows.Forms.Label kminc;
        private System.Windows.Forms.Label rundir;
        private System.Windows.Forms.Label traincode;
        private System.Windows.Forms.Label dir;
        private System.Windows.Forms.Label trackname;
        private System.Windows.Forms.Label trackcode;
        private System.Windows.Forms.Label dataversion;
        private System.Windows.Forms.Label datatype;
        private System.Windows.Forms.Button btnSelectImportPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImportPath;
        private System.Windows.Forms.Button btnSelectExportPath;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label labExport;
        private System.Windows.Forms.ComboBox cbxFileType;
        private System.Windows.Forms.Button btnBgeinExport;
        private System.Windows.Forms.Button btnCancelExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserImportDialog;
        private System.Windows.Forms.ComboBox cbxRunDir;
        private System.Windows.Forms.ComboBox cbxKmInc;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserExportDialog;
        private System.Windows.Forms.ComboBox cbxUpDown;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}