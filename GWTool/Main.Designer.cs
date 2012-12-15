namespace GWTool
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(35, 56);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(143, 26);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Drop an addon file anywhere\r\non this window to extract it";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(12, 108);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(190, 45);
            this.lblResult.TabIndex = 1;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Enabled = false;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point(93, 173);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(28, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "v0.1";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 194);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GWTool";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.DragEnter += Main_DragEnter;
            this.DragDrop += Main_DragDrop;

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblVersion;
    }
}

