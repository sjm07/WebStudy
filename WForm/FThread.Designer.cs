namespace WForm
{
    partial class FThread
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
            this.txtNoPara = new System.Windows.Forms.TextBox();
            this.txtHavePara = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNoPara
            // 
            this.txtNoPara.Location = new System.Drawing.Point(13, 13);
            this.txtNoPara.Name = "txtNoPara";
            this.txtNoPara.Size = new System.Drawing.Size(478, 25);
            this.txtNoPara.TabIndex = 0;
            // 
            // txtHavePara
            // 
            this.txtHavePara.Location = new System.Drawing.Point(12, 53);
            this.txtHavePara.Name = "txtHavePara";
            this.txtHavePara.Size = new System.Drawing.Size(478, 25);
            this.txtHavePara.TabIndex = 1;
            // 
            // FThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 329);
            this.Controls.Add(this.txtHavePara);
            this.Controls.Add(this.txtNoPara);
            this.Name = "FThread";
            this.Text = "线程";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNoPara;
        private System.Windows.Forms.TextBox txtHavePara;
    }
}