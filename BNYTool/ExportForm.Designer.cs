namespace BNYTool
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_StartMile = new System.Windows.Forms.TextBox();
            this.txt_EndMile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ExportPath = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btn_Select = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(13, 28);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(356, 334);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "通道号";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "通道名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 220;
            // 
            // cb_all
            // 
            this.cb_all.AutoSize = true;
            this.cb_all.Location = new System.Drawing.Point(12, 371);
            this.cb_all.Name = "cb_all";
            this.cb_all.Size = new System.Drawing.Size(60, 16);
            this.cb_all.TabIndex = 1;
            this.cb_all.Text = " 全 选";
            this.cb_all.UseVisualStyleBackColor = true;
            this.cb_all.CheckedChanged += new System.EventHandler(this.cb_all_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(220, 508);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导  出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开始里程：";
            // 
            // txt_StartMile
            // 
            this.txt_StartMile.Location = new System.Drawing.Point(84, 394);
            this.txt_StartMile.Name = "txt_StartMile";
            this.txt_StartMile.Size = new System.Drawing.Size(100, 21);
            this.txt_StartMile.TabIndex = 4;
            // 
            // txt_EndMile
            // 
            this.txt_EndMile.Location = new System.Drawing.Point(84, 421);
            this.txt_EndMile.Name = "txt_EndMile";
            this.txt_EndMile.Size = new System.Drawing.Size(100, 21);
            this.txt_EndMile.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束里程：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 453);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "文件路径：";
            // 
            // txt_ExportPath
            // 
            this.txt_ExportPath.Location = new System.Drawing.Point(84, 450);
            this.txt_ExportPath.Name = "txt_ExportPath";
            this.txt_ExportPath.Size = new System.Drawing.Size(204, 21);
            this.txt_ExportPath.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(102, 508);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "取  消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(294, 448);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(75, 23);
            this.btn_Select.TabIndex = 12;
            this.btn_Select.Text = "浏  览";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "文本文件|*.txt";
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 545);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txt_ExportPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_EndMile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_StartMile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cb_all);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出txt";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox cb_all;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_StartMile;
        private System.Windows.Forms.TextBox txt_EndMile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ExportPath;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}