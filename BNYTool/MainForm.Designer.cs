namespace BNYTool
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.里程 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.综合里程 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.速度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.左垂力 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.左横力 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.右垂力 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.右横力 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车体横加 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车体垂加 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车体纵加 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.陀螺仪 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.构架垂 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.构架横 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.左轴箱加 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.右轴箱加 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.曲率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtBNYFilePath = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.txtGoPage = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBatchExportCit = new System.Windows.Forms.Button();
            this.btnCreateCitFile = new System.Windows.Forms.Button();
            this.btnExportTxt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_StartMile = new System.Windows.Forms.Label();
            this.lbl_EndMile = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.时间,
            this.里程,
            this.综合里程,
            this.速度,
            this.左垂力,
            this.左横力,
            this.右垂力,
            this.右横力,
            this.车体横加,
            this.车体垂加,
            this.车体纵加,
            this.陀螺仪,
            this.构架垂,
            this.构架横,
            this.左轴箱加,
            this.右轴箱加,
            this.曲率,
            this.ALD});
            this.dataGridView1.Location = new System.Drawing.Point(12, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1103, 449);
            this.dataGridView1.TabIndex = 0;
            // 
            // 时间
            // 
            this.时间.Frozen = true;
            this.时间.HeaderText = "时间";
            this.时间.Name = "时间";
            // 
            // 里程
            // 
            this.里程.Frozen = true;
            this.里程.HeaderText = "里程";
            this.里程.Name = "里程";
            // 
            // 综合里程
            // 
            this.综合里程.Frozen = true;
            this.综合里程.HeaderText = "综合里程";
            this.综合里程.Name = "综合里程";
            // 
            // 速度
            // 
            this.速度.Frozen = true;
            this.速度.HeaderText = "速度";
            this.速度.Name = "速度";
            // 
            // 左垂力
            // 
            this.左垂力.HeaderText = "左垂力";
            this.左垂力.Name = "左垂力";
            // 
            // 左横力
            // 
            this.左横力.HeaderText = "左横力";
            this.左横力.Name = "左横力";
            // 
            // 右垂力
            // 
            this.右垂力.HeaderText = "右垂力";
            this.右垂力.Name = "右垂力";
            // 
            // 右横力
            // 
            this.右横力.HeaderText = "右横力";
            this.右横力.Name = "右横力";
            // 
            // 车体横加
            // 
            this.车体横加.HeaderText = "车体横加";
            this.车体横加.Name = "车体横加";
            // 
            // 车体垂加
            // 
            this.车体垂加.HeaderText = "车体垂加";
            this.车体垂加.Name = "车体垂加";
            // 
            // 车体纵加
            // 
            this.车体纵加.HeaderText = "车体纵加";
            this.车体纵加.Name = "车体纵加";
            // 
            // 陀螺仪
            // 
            this.陀螺仪.HeaderText = "陀螺仪";
            this.陀螺仪.Name = "陀螺仪";
            // 
            // 构架垂
            // 
            this.构架垂.HeaderText = "构架垂";
            this.构架垂.Name = "构架垂";
            // 
            // 构架横
            // 
            this.构架横.HeaderText = "构架横";
            this.构架横.Name = "构架横";
            // 
            // 左轴箱加
            // 
            this.左轴箱加.HeaderText = "左轴箱加";
            this.左轴箱加.Name = "左轴箱加";
            // 
            // 右轴箱加
            // 
            this.右轴箱加.HeaderText = "右轴箱加";
            this.右轴箱加.Name = "右轴箱加";
            // 
            // 曲率
            // 
            this.曲率.HeaderText = "曲率";
            this.曲率.Name = "曲率";
            // 
            // ALD
            // 
            this.ALD.HeaderText = "ALD";
            this.ALD.Name = "ALD";
            // 
            // txtBNYFilePath
            // 
            this.txtBNYFilePath.Location = new System.Drawing.Point(80, 12);
            this.txtBNYFilePath.Name = "txtBNYFilePath";
            this.txtBNYFilePath.Size = new System.Drawing.Size(741, 21);
            this.txtBNYFilePath.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(864, 10);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选 择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(970, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查 询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(265, 17);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "<<上一页";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(485, 17);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一页>>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Location = new System.Drawing.Point(363, 17);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.ReadOnly = true;
            this.txtCurrentPage.Size = new System.Drawing.Size(101, 21);
            this.txtCurrentPage.TabIndex = 6;
            // 
            // txtGoPage
            // 
            this.txtGoPage.Location = new System.Drawing.Point(594, 17);
            this.txtGoPage.Name = "txtGoPage";
            this.txtGoPage.Size = new System.Drawing.Size(73, 21);
            this.txtGoPage.TabIndex = 7;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(683, 17);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "跳  转";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "总页数：";
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(96, 19);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(0, 12);
            this.lblTotalPage.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnBatchExportCit);
            this.panel1.Controls.Add(this.btnCreateCitFile);
            this.panel1.Controls.Add(this.btnExportTxt);
            this.panel1.Controls.Add(this.txtGoPage);
            this.panel1.Controls.Add(this.lblTotalPage);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.txtCurrentPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 536);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1189, 65);
            this.panel1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1102, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "txt转cit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBatchExportCit
            // 
            this.btnBatchExportCit.Location = new System.Drawing.Point(998, 17);
            this.btnBatchExportCit.Name = "btnBatchExportCit";
            this.btnBatchExportCit.Size = new System.Drawing.Size(91, 23);
            this.btnBatchExportCit.TabIndex = 13;
            this.btnBatchExportCit.Text = "批量生成cit";
            this.btnBatchExportCit.UseVisualStyleBackColor = true;
            this.btnBatchExportCit.Click += new System.EventHandler(this.btnBatchExportCit_Click);
            // 
            // btnCreateCitFile
            // 
            this.btnCreateCitFile.Location = new System.Drawing.Point(886, 17);
            this.btnCreateCitFile.Name = "btnCreateCitFile";
            this.btnCreateCitFile.Size = new System.Drawing.Size(95, 23);
            this.btnCreateCitFile.TabIndex = 12;
            this.btnCreateCitFile.Text = "生成cit文件";
            this.btnCreateCitFile.UseVisualStyleBackColor = true;
            this.btnCreateCitFile.Click += new System.EventHandler(this.btnCreateCitFile_Click);
            // 
            // btnExportTxt
            // 
            this.btnExportTxt.Location = new System.Drawing.Point(800, 17);
            this.btnExportTxt.Name = "btnExportTxt";
            this.btnExportTxt.Size = new System.Drawing.Size(75, 23);
            this.btnExportTxt.TabIndex = 11;
            this.btnExportTxt.Text = "导  出";
            this.btnExportTxt.UseVisualStyleBackColor = true;
            this.btnExportTxt.Click += new System.EventHandler(this.btnExportTxt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "起始里程：";
            // 
            // lbl_StartMile
            // 
            this.lbl_StartMile.AutoSize = true;
            this.lbl_StartMile.Location = new System.Drawing.Point(89, 45);
            this.lbl_StartMile.Name = "lbl_StartMile";
            this.lbl_StartMile.Size = new System.Drawing.Size(0, 12);
            this.lbl_StartMile.TabIndex = 13;
            // 
            // lbl_EndMile
            // 
            this.lbl_EndMile.AutoSize = true;
            this.lbl_EndMile.Location = new System.Drawing.Point(253, 45);
            this.lbl_EndMile.Name = "lbl_EndMile";
            this.lbl_EndMile.Size = new System.Drawing.Size(0, 12);
            this.lbl_EndMile.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "终止里程：";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 601);
            this.Controls.Add(this.lbl_EndMile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_StartMile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtBNYFilePath);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BNY工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtBNYFilePath;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.TextBox txtGoPage;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 里程;
        private System.Windows.Forms.DataGridViewTextBoxColumn 综合里程;
        private System.Windows.Forms.DataGridViewTextBoxColumn 速度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 左垂力;
        private System.Windows.Forms.DataGridViewTextBoxColumn 左横力;
        private System.Windows.Forms.DataGridViewTextBoxColumn 右垂力;
        private System.Windows.Forms.DataGridViewTextBoxColumn 右横力;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车体横加;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车体垂加;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车体纵加;
        private System.Windows.Forms.DataGridViewTextBoxColumn 陀螺仪;
        private System.Windows.Forms.DataGridViewTextBoxColumn 构架垂;
        private System.Windows.Forms.DataGridViewTextBoxColumn 构架横;
        private System.Windows.Forms.DataGridViewTextBoxColumn 左轴箱加;
        private System.Windows.Forms.DataGridViewTextBoxColumn 右轴箱加;
        private System.Windows.Forms.DataGridViewTextBoxColumn 曲率;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALD;
        private System.Windows.Forms.Button btnCreateCitFile;
        private System.Windows.Forms.Button btnExportTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_StartMile;
        private System.Windows.Forms.Label lbl_EndMile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBatchExportCit;
        internal System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button button1;
    }
}