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
            this.rtxtLeft = new System.Windows.Forms.RichTextBox();
            this.rtxtRight = new System.Windows.Forms.RichTextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rtxtLeft);
            this.panel1.Controls.Add(this.rtxtRight);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 188);
            this.panel1.TabIndex = 0;
            // 
            // rtxtLeft
            // 
            this.rtxtLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLeft.Location = new System.Drawing.Point(0, 23);
            this.rtxtLeft.Name = "rtxtLeft";
            this.rtxtLeft.ReadOnly = true;
            this.rtxtLeft.Size = new System.Drawing.Size(188, 142);
            this.rtxtLeft.TabIndex = 3;
            this.rtxtLeft.Text = "";
            // 
            // rtxtRight
            // 
            this.rtxtRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtxtRight.Location = new System.Drawing.Point(188, 23);
            this.rtxtRight.Name = "rtxtRight";
            this.rtxtRight.ReadOnly = true;
            this.rtxtRight.Size = new System.Drawing.Size(110, 142);
            this.rtxtRight.TabIndex = 2;
            this.rtxtRight.Text = "";
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
            // FileScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Name = "FileScreen";
            this.Size = new System.Drawing.Size(308, 198);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox rtxtLeft;
        private System.Windows.Forms.RichTextBox rtxtRight;
        public System.Windows.Forms.Timer timer1;
    }
}
