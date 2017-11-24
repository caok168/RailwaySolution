namespace BNYTool
{
    partial class TxtToCitForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTxtPath = new System.Windows.Forms.TextBox();
            this.btnSelecttxtPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectCitPath = new System.Windows.Forms.Button();
            this.txtCitPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原始txt路径：";
            // 
            // txtTxtPath
            // 
            this.txtTxtPath.Location = new System.Drawing.Point(95, 22);
            this.txtTxtPath.Name = "txtTxtPath";
            this.txtTxtPath.ReadOnly = true;
            this.txtTxtPath.Size = new System.Drawing.Size(291, 21);
            this.txtTxtPath.TabIndex = 1;
            // 
            // btnSelecttxtPath
            // 
            this.btnSelecttxtPath.Location = new System.Drawing.Point(402, 20);
            this.btnSelecttxtPath.Name = "btnSelecttxtPath";
            this.btnSelecttxtPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelecttxtPath.TabIndex = 2;
            this.btnSelecttxtPath.Text = "选择路径";
            this.btnSelecttxtPath.UseVisualStyleBackColor = true;
            this.btnSelecttxtPath.Click += new System.EventHandler(this.btnSelecttxtPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "导出cit路径：";
            // 
            // btnSelectCitPath
            // 
            this.btnSelectCitPath.Location = new System.Drawing.Point(402, 70);
            this.btnSelectCitPath.Name = "btnSelectCitPath";
            this.btnSelectCitPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCitPath.TabIndex = 5;
            this.btnSelectCitPath.Text = "选择路径";
            this.btnSelectCitPath.UseVisualStyleBackColor = true;
            // 
            // txtCitPath
            // 
            this.txtCitPath.Location = new System.Drawing.Point(95, 72);
            this.txtCitPath.Name = "txtCitPath";
            this.txtCitPath.Size = new System.Drawing.Size(291, 21);
            this.txtCitPath.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(402, 115);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "开始导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(290, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消导出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 115);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(212, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Visible = false;
            // 
            // TxtToCitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 150);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelectCitPath);
            this.Controls.Add(this.txtCitPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelecttxtPath);
            this.Controls.Add(this.txtTxtPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TxtToCitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Txt转Cit";
            this.Load += new System.EventHandler(this.TxtToCitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTxtPath;
        private System.Windows.Forms.Button btnSelecttxtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectCitPath;
        private System.Windows.Forms.TextBox txtCitPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}