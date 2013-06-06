namespace DXWindowsApplication2.UserForms
{
    partial class PopupAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupAbout));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlCopyright = new DevExpress.XtraEditors.LabelControl();
            this.labelControlVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelControlSoftwareName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlCompanyAddress = new DevExpress.XtraEditors.LabelControl();
            this.labelControlCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.richTextBoxWarning = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl1.ContentImage")));
            this.panelControl1.ContentImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(5);
            this.panelControl1.Size = new System.Drawing.Size(234, 196);
            this.panelControl1.TabIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(20, 20);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(784, 200);
            this.panelControl2.TabIndex = 3;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.ContentImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelControl4.Controls.Add(this.richTextBoxWarning);
            this.panelControl4.Controls.Add(this.labelControlCopyright);
            this.panelControl4.Controls.Add(this.labelControlVersion);
            this.panelControl4.Controls.Add(this.labelControlSoftwareName);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(236, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Padding = new System.Windows.Forms.Padding(5);
            this.panelControl4.Size = new System.Drawing.Size(546, 196);
            this.panelControl4.TabIndex = 3;
            // 
            // labelControlCopyright
            // 
            this.labelControlCopyright.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControlCopyright.Location = new System.Drawing.Point(8, 88);
            this.labelControlCopyright.Name = "labelControlCopyright";
            this.labelControlCopyright.Size = new System.Drawing.Size(55, 13);
            this.labelControlCopyright.TabIndex = 2;
            this.labelControlCopyright.Text = "Copyright";
            // 
            // labelControlVersion
            // 
            this.labelControlVersion.Location = new System.Drawing.Point(8, 58);
            this.labelControlVersion.Name = "labelControlVersion";
            this.labelControlVersion.Size = new System.Drawing.Size(60, 13);
            this.labelControlVersion.TabIndex = 1;
            this.labelControlVersion.Text = "Version 1.00";
            // 
            // labelControlSoftwareName
            // 
            this.labelControlSoftwareName.Appearance.Font = new System.Drawing.Font("AngsanaUPC", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelControlSoftwareName.Location = new System.Drawing.Point(8, 3);
            this.labelControlSoftwareName.Name = "labelControlSoftwareName";
            this.labelControlSoftwareName.Size = new System.Drawing.Size(206, 49);
            this.labelControlSoftwareName.TabIndex = 0;
            this.labelControlSoftwareName.Text = "[ New Billing Name ]";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControlCompanyAddress);
            this.panelControl3.Controls.Add(this.labelControlCompanyName);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(20, 226);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(784, 86);
            this.panelControl3.TabIndex = 4;
            // 
            // labelControlCompanyAddress
            // 
            this.labelControlCompanyAddress.Location = new System.Drawing.Point(5, 24);
            this.labelControlCompanyAddress.Name = "labelControlCompanyAddress";
            this.labelControlCompanyAddress.Size = new System.Drawing.Size(39, 13);
            this.labelControlCompanyAddress.TabIndex = 5;
            this.labelControlCompanyAddress.Text = "Address";
            // 
            // labelControlCompanyName
            // 
            this.labelControlCompanyName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControlCompanyName.Location = new System.Drawing.Point(5, 5);
            this.labelControlCompanyName.Name = "labelControlCompanyName";
            this.labelControlCompanyName.Size = new System.Drawing.Size(88, 13);
            this.labelControlCompanyName.TabIndex = 4;
            this.labelControlCompanyName.Text = "Company name";
            // 
            // richTextBoxWarning
            // 
            this.richTextBoxWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.richTextBoxWarning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxWarning.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBoxWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.richTextBoxWarning.Location = new System.Drawing.Point(8, 120);
            this.richTextBoxWarning.Name = "richTextBoxWarning";
            this.richTextBoxWarning.ReadOnly = true;
            this.richTextBoxWarning.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxWarning.Size = new System.Drawing.Size(426, 72);
            this.richTextBoxWarning.TabIndex = 4;
            this.richTextBoxWarning.Text = "";
            // 
            // PopupAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 332);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupAbout";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "[ Software Name ]";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl labelControlVersion;
        private DevExpress.XtraEditors.LabelControl labelControlSoftwareName;
        private DevExpress.XtraEditors.LabelControl labelControlCopyright;
        private DevExpress.XtraEditors.LabelControl labelControlCompanyAddress;
        private DevExpress.XtraEditors.LabelControl labelControlCompanyName;
        private System.Windows.Forms.RichTextBox richTextBoxWarning;


    }
}
