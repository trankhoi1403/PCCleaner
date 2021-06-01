namespace Cleaner.UserControls
{
    partial class FileScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.rtxtRight = new System.Windows.Forms.RichTextBox();
            this.rtxtLeft = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rtxtLeft);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 188);
            this.panel1.TabIndex = 0;
            // 
            // btnDel
            // 
            this.btnDel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(0, 165);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(298, 23);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "Clean";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(298, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.rtxtRight);
            this.panel2.Controls.Add(this.lblTimeLeft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(193, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(105, 142);
            this.panel2.TabIndex = 4;
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.BackColor = System.Drawing.Color.PaleGreen;
            this.lblTimeLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTimeLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeLeft.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTimeLeft.Location = new System.Drawing.Point(0, 0);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(101, 24);
            this.lblTimeLeft.TabIndex = 4;
            this.lblTimeLeft.Text = "1.02:34:59";
            this.lblTimeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtxtRight
            // 
            this.rtxtRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtRight.Location = new System.Drawing.Point(0, 24);
            this.rtxtRight.Name = "rtxtRight";
            this.rtxtRight.ReadOnly = true;
            this.rtxtRight.Size = new System.Drawing.Size(101, 114);
            this.rtxtRight.TabIndex = 5;
            this.rtxtRight.Text = "";
            // 
            // rtxtLeft
            // 
            this.rtxtLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLeft.Location = new System.Drawing.Point(0, 23);
            this.rtxtLeft.Name = "rtxtLeft";
            this.rtxtLeft.ReadOnly = true;
            this.rtxtLeft.Size = new System.Drawing.Size(193, 142);
            this.rtxtLeft.TabIndex = 5;
            this.rtxtLeft.Text = "";
            // 
            // FileScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FileScreen";
            this.Size = new System.Drawing.Size(308, 198);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtxtRight;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.RichTextBox rtxtLeft;
    }
}
