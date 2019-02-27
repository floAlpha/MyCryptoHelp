namespace MyCryptoHelp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
            this.Do = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.pwd = new System.Windows.Forms.TextBox();
            this.StrText = new System.Windows.Forms.TextBox();
            this.uString = new System.Windows.Forms.TextBox();
            this.StrEncrypt = new System.Windows.Forms.Button();
            this.StrrDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Do
            // 
            this.Do.Location = new System.Drawing.Point(58, 196);
            this.Do.Name = "Do";
            this.Do.Size = new System.Drawing.Size(75, 23);
            this.Do.TabIndex = 0;
            this.Do.Text = "加密文件";
            this.Do.UseVisualStyleBackColor = true;
            this.Do.Click += new System.EventHandler(this.Do_Click);
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(223, 196);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(75, 23);
            this.Undo.TabIndex = 1;
            this.Undo.Text = "解密文件";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入密钥";
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(95, 140);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '*';
            this.pwd.Size = new System.Drawing.Size(229, 21);
            this.pwd.TabIndex = 3;
            // 
            // StrText
            // 
            this.StrText.Location = new System.Drawing.Point(27, 32);
            this.StrText.Name = "StrText";
            this.StrText.Size = new System.Drawing.Size(204, 21);
            this.StrText.TabIndex = 5;
            this.StrText.Text = "加密文本无需密钥";
            // 
            // uString
            // 
            this.uString.Location = new System.Drawing.Point(27, 70);
            this.uString.Name = "uString";
            this.uString.Size = new System.Drawing.Size(204, 21);
            this.uString.TabIndex = 7;
            this.uString.Text = "解密文本无需密钥";
            // 
            // StrEncrypt
            // 
            this.StrEncrypt.Location = new System.Drawing.Point(249, 32);
            this.StrEncrypt.Name = "StrEncrypt";
            this.StrEncrypt.Size = new System.Drawing.Size(75, 23);
            this.StrEncrypt.TabIndex = 8;
            this.StrEncrypt.Text = "加密文本";
            this.StrEncrypt.UseVisualStyleBackColor = true;
            this.StrEncrypt.Click += new System.EventHandler(this.StrEncrypt_Click);
            // 
            // StrrDecrypt
            // 
            this.StrrDecrypt.Location = new System.Drawing.Point(249, 70);
            this.StrrDecrypt.Name = "StrrDecrypt";
            this.StrrDecrypt.Size = new System.Drawing.Size(75, 23);
            this.StrrDecrypt.TabIndex = 9;
            this.StrrDecrypt.Text = "解密文本";
            this.StrrDecrypt.UseVisualStyleBackColor = true;
            this.StrrDecrypt.Click += new System.EventHandler(this.StrrDecrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 236);
            this.Controls.Add(this.StrrDecrypt);
            this.Controls.Add(this.StrEncrypt);
            this.Controls.Add(this.uString);
            this.Controls.Add(this.StrText);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.Do);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "基于DES算法的加密程序V1.0（杨恒）";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Do;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.TextBox StrText;
        private System.Windows.Forms.TextBox uString;
        private System.Windows.Forms.Button StrEncrypt;
        private System.Windows.Forms.Button StrrDecrypt;
    }
}

