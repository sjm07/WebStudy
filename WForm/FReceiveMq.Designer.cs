namespace WForm
{
    partial class FReceiveMq
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
            this.btnReceiveMq = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQueueName = new System.Windows.Forms.TextBox();
            this.txtReceiveMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReceiveMq
            // 
            this.btnReceiveMq.Location = new System.Drawing.Point(324, 26);
            this.btnReceiveMq.Name = "btnReceiveMq";
            this.btnReceiveMq.Size = new System.Drawing.Size(75, 29);
            this.btnReceiveMq.TabIndex = 0;
            this.btnReceiveMq.Text = "接收Mq";
            this.btnReceiveMq.UseVisualStyleBackColor = true;
            this.btnReceiveMq.Click += new System.EventHandler(this.btnReceiveMq_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "队列名称";
            // 
            // txtQueueName
            // 
            this.txtQueueName.Location = new System.Drawing.Point(141, 29);
            this.txtQueueName.Name = "txtQueueName";
            this.txtQueueName.Size = new System.Drawing.Size(163, 25);
            this.txtQueueName.TabIndex = 2;
            // 
            // txtReceiveMsg
            // 
            this.txtReceiveMsg.Location = new System.Drawing.Point(140, 76);
            this.txtReceiveMsg.Name = "txtReceiveMsg";
            this.txtReceiveMsg.Size = new System.Drawing.Size(163, 25);
            this.txtReceiveMsg.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "内容";
            // 
            // FReceiveMq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 253);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReceiveMsg);
            this.Controls.Add(this.txtQueueName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReceiveMq);
            this.Name = "FReceiveMq";
            this.Text = "FReceiveMq";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReceiveMq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQueueName;
        private System.Windows.Forms.TextBox txtReceiveMsg;
        private System.Windows.Forms.Label label2;
    }
}