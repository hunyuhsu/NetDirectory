namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_DestDirPath = new System.Windows.Forms.TextBox();
            this.txb_Access = new System.Windows.Forms.TextBox();
            this.txb_Password = new System.Windows.Forms.TextBox();
            this.btn_Initial = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_DisConnect = new System.Windows.Forms.Button();
            this.btn_ClearMsg = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rtb_MsgInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dest Dir";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Access";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txb_DestDirPath
            // 
            this.txb_DestDirPath.Location = new System.Drawing.Point(73, 6);
            this.txb_DestDirPath.Name = "txb_DestDirPath";
            this.txb_DestDirPath.Size = new System.Drawing.Size(432, 25);
            this.txb_DestDirPath.TabIndex = 3;
            // 
            // txb_Access
            // 
            this.txb_Access.Location = new System.Drawing.Point(73, 38);
            this.txb_Access.Name = "txb_Access";
            this.txb_Access.Size = new System.Drawing.Size(186, 25);
            this.txb_Access.TabIndex = 4;
            // 
            // txb_Password
            // 
            this.txb_Password.Location = new System.Drawing.Point(331, 37);
            this.txb_Password.Name = "txb_Password";
            this.txb_Password.Size = new System.Drawing.Size(174, 25);
            this.txb_Password.TabIndex = 5;
            // 
            // btn_Initial
            // 
            this.btn_Initial.Location = new System.Drawing.Point(512, 6);
            this.btn_Initial.Name = "btn_Initial";
            this.btn_Initial.Size = new System.Drawing.Size(75, 57);
            this.btn_Initial.TabIndex = 6;
            this.btn_Initial.Text = "Initial";
            this.btn_Initial.UseVisualStyleBackColor = true;
            this.btn_Initial.Click += new System.EventHandler(this.btn_Initial_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(73, 69);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(81, 39);
            this.btn_Connect.TabIndex = 7;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_DisConnect
            // 
            this.btn_DisConnect.Location = new System.Drawing.Point(162, 69);
            this.btn_DisConnect.Name = "btn_DisConnect";
            this.btn_DisConnect.Size = new System.Drawing.Size(97, 39);
            this.btn_DisConnect.TabIndex = 8;
            this.btn_DisConnect.Text = "DisConnect";
            this.btn_DisConnect.UseVisualStyleBackColor = true;
            this.btn_DisConnect.Click += new System.EventHandler(this.btn_DisConnect_Click);
            // 
            // btn_ClearMsg
            // 
            this.btn_ClearMsg.Location = new System.Drawing.Point(512, 69);
            this.btn_ClearMsg.Name = "btn_ClearMsg";
            this.btn_ClearMsg.Size = new System.Drawing.Size(75, 39);
            this.btn_ClearMsg.TabIndex = 9;
            this.btn_ClearMsg.Text = "Clear";
            this.btn_ClearMsg.UseVisualStyleBackColor = true;
            this.btn_ClearMsg.Click += new System.EventHandler(this.btn_ClearMsg_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // rtb_MsgInfo
            // 
            this.rtb_MsgInfo.BackColor = System.Drawing.SystemColors.InfoText;
            this.rtb_MsgInfo.Location = new System.Drawing.Point(15, 114);
            this.rtb_MsgInfo.Name = "rtb_MsgInfo";
            this.rtb_MsgInfo.Size = new System.Drawing.Size(572, 239);
            this.rtb_MsgInfo.TabIndex = 11;
            this.rtb_MsgInfo.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 357);
            this.Controls.Add(this.rtb_MsgInfo);
            this.Controls.Add(this.btn_ClearMsg);
            this.Controls.Add(this.btn_DisConnect);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_Initial);
            this.Controls.Add(this.txb_Password);
            this.Controls.Add(this.txb_Access);
            this.Controls.Add(this.txb_DestDirPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_DestDirPath;
        private System.Windows.Forms.TextBox txb_Access;
        private System.Windows.Forms.TextBox txb_Password;
        private System.Windows.Forms.Button btn_Initial;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_DisConnect;
        private System.Windows.Forms.Button btn_ClearMsg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RichTextBox rtb_MsgInfo;
    }
}

