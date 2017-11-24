namespace BNYTool
{
    partial class CITForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CITForm));
            this.currentcitfile = new System.Windows.Forms.Label();
            this.btn_CreateCit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_FolderPath = new System.Windows.Forms.TextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Select = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentcitfile
            // 
            this.currentcitfile.AutoSize = true;
            this.currentcitfile.Location = new System.Drawing.Point(202, 17);
            this.currentcitfile.Name = "currentcitfile";
            this.currentcitfile.Size = new System.Drawing.Size(0, 12);
            this.currentcitfile.TabIndex = 14;
            this.currentcitfile.Visible = false;
            // 
            // btn_CreateCit
            // 
            this.btn_CreateCit.Location = new System.Drawing.Point(380, 365);
            this.btn_CreateCit.Name = "btn_CreateCit";
            this.btn_CreateCit.Size = new System.Drawing.Size(75, 23);
            this.btn_CreateCit.TabIndex = 48;
            this.btn_CreateCit.Text = "生  成";
            this.btn_CreateCit.UseVisualStyleBackColor = true;
            this.btn_CreateCit.Click += new System.EventHandler(this.btn_CreateCit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "生成cit文件路径：";
            // 
            // txt_FolderPath
            // 
            this.txt_FolderPath.Location = new System.Drawing.Point(133, 299);
            this.txt_FolderPath.Name = "txt_FolderPath";
            this.txt_FolderPath.Size = new System.Drawing.Size(407, 21);
            this.txt_FolderPath.TabIndex = 55;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(212, 365);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 56;
            this.btn_Close.Text = "取  消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(559, 297);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(75, 23);
            this.btn_Select.TabIndex = 57;
            this.btn_Select.Text = "浏  览";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 254);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件头部信息";
            // 
            // cbxUpDown
            // 
            this.cbxUpDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUpDown.FormattingEnabled = true;
            this.cbxUpDown.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
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
            this.cbxRunDir.Items.AddRange(new object[] {
            "0",
            "1"});
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
            this.cbxFileType.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
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
            // 
            // dataversionnewvalue
            // 
            this.dataversionnewvalue.Location = new System.Drawing.Point(361, 46);
            this.dataversionnewvalue.Name = "dataversionnewvalue";
            this.dataversionnewvalue.Size = new System.Drawing.Size(202, 21);
            this.dataversionnewvalue.TabIndex = 72;
            this.dataversionnewvalue.Leave += new System.EventHandler(this.dataversionnewvalue_Leave);
            // 
            // kmincvalue
            // 
            this.kmincvalue.AutoSize = true;
            this.kmincvalue.Location = new System.Drawing.Point(250, 215);
            this.kmincvalue.Name = "kmincvalue";
            this.kmincvalue.Size = new System.Drawing.Size(0, 12);
            this.kmincvalue.TabIndex = 66;
            // 
            // rundirvalue
            // 
            this.rundirvalue.AutoSize = true;
            this.rundirvalue.Location = new System.Drawing.Point(250, 187);
            this.rundirvalue.Name = "rundirvalue";
            this.rundirvalue.Size = new System.Drawing.Size(0, 12);
            this.rundirvalue.TabIndex = 65;
            // 
            // traincodevalue
            // 
            this.traincodevalue.AutoSize = true;
            this.traincodevalue.Location = new System.Drawing.Point(250, 158);
            this.traincodevalue.Name = "traincodevalue";
            this.traincodevalue.Size = new System.Drawing.Size(0, 12);
            this.traincodevalue.TabIndex = 62;
            // 
            // dirvalue
            // 
            this.dirvalue.AutoSize = true;
            this.dirvalue.Location = new System.Drawing.Point(250, 131);
            this.dirvalue.Name = "dirvalue";
            this.dirvalue.Size = new System.Drawing.Size(0, 12);
            this.dirvalue.TabIndex = 61;
            // 
            // tracknamevalue
            // 
            this.tracknamevalue.AutoSize = true;
            this.tracknamevalue.Location = new System.Drawing.Point(250, 102);
            this.tracknamevalue.Name = "tracknamevalue";
            this.tracknamevalue.Size = new System.Drawing.Size(0, 12);
            this.tracknamevalue.TabIndex = 60;
            // 
            // trackcodevalue
            // 
            this.trackcodevalue.AutoSize = true;
            this.trackcodevalue.Location = new System.Drawing.Point(250, 77);
            this.trackcodevalue.Name = "trackcodevalue";
            this.trackcodevalue.Size = new System.Drawing.Size(0, 12);
            this.trackcodevalue.TabIndex = 59;
            // 
            // dataversionvalue
            // 
            this.dataversionvalue.AutoSize = true;
            this.dataversionvalue.Location = new System.Drawing.Point(250, 53);
            this.dataversionvalue.Name = "dataversionvalue";
            this.dataversionvalue.Size = new System.Drawing.Size(0, 12);
            this.dataversionvalue.TabIndex = 58;
            // 
            // datatypevalue
            // 
            this.datatypevalue.AutoSize = true;
            this.datatypevalue.Location = new System.Drawing.Point(250, 29);
            this.datatypevalue.Name = "datatypevalue";
            this.datatypevalue.Size = new System.Drawing.Size(0, 12);
            this.datatypevalue.TabIndex = 57;
            // 
            // kminc
            // 
            this.kminc.AutoSize = true;
            this.kminc.Location = new System.Drawing.Point(14, 215);
            this.kminc.Name = "kminc";
            this.kminc.Size = new System.Drawing.Size(179, 12);
            this.kminc.TabIndex = 52;
            this.kminc.Text = "增减里程(0 增里程, 1 减里程):";
            // 
            // rundir
            // 
            this.rundir.AutoSize = true;
            this.rundir.Location = new System.Drawing.Point(14, 187);
            this.rundir.Name = "rundir";
            this.rundir.Size = new System.Drawing.Size(131, 12);
            this.rundir.TabIndex = 51;
            this.rundir.Text = "检测方向(正 0, 反 1):";
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
            this.dir.Size = new System.Drawing.Size(179, 12);
            this.dir.TabIndex = 47;
            this.dir.Text = "行别(1 上行, 2 下行, 3 单线):";
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
            this.dataversion.Location = new System.Drawing.Point(14, 53);
            this.dataversion.Name = "dataversion";
            this.dataversion.Size = new System.Drawing.Size(227, 12);
            this.dataversion.TabIndex = 44;
            this.dataversion.Text = "文件版本号(20个字符以内，例如 x.x.x):";
            // 
            // datatype
            // 
            this.datatype.AutoSize = true;
            this.datatype.Location = new System.Drawing.Point(14, 29);
            this.datatype.Name = "datatype";
            this.datatype.Size = new System.Drawing.Size(215, 12);
            this.datatype.TabIndex = 43;
            this.datatype.Text = "文件类型(1 轨检, 2 动力学, 3 弓网):";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "cit 文件|*.cit";
            // 
            // CITForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 424);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_FolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_CreateCit);
            this.Controls.Add(this.currentcitfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CITForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出cit文件";
            this.Load += new System.EventHandler(this.CITForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label currentcitfile;
        private System.Windows.Forms.Button btn_CreateCit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_FolderPath;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxUpDown;
        private System.Windows.Forms.ComboBox cbxKmInc;
        private System.Windows.Forms.ComboBox cbxRunDir;
        private System.Windows.Forms.ComboBox cbxFileType;
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
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}