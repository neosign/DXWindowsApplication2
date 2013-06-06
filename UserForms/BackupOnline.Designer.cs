namespace DXWindowsApplication2.UserForms
{
    partial class BackupOnline
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.textEditServer = new DevExpress.XtraEditors.TextEdit();
            this.textEditUsername = new DevExpress.XtraEditors.TextEdit();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControlUsername = new DevExpress.XtraEditors.LabelControl();
            this.labelControlServerName = new DevExpress.XtraEditors.LabelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.bttEdit = new DevExpress.XtraEditors.SimpleButton();
            this.panelEnable = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEnable)).BeginInit();
            this.panelEnable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(7, 366);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(864, 26);
            this.panelControl1.TabIndex = 17;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.xtraScrollableControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(864, 359);
            this.groupControl1.TabIndex = 20;
            this.groupControl1.Text = "สำรองข้อมูลออนไลน์";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Controls.Add(this.bttEdit);
            this.xtraScrollableControl1.Controls.Add(this.bttCancel);
            this.xtraScrollableControl1.Controls.Add(this.labelControlPassword);
            this.xtraScrollableControl1.Controls.Add(this.labelControlUsername);
            this.xtraScrollableControl1.Controls.Add(this.labelControlServerName);
            this.xtraScrollableControl1.Controls.Add(this.bttSave);
            this.xtraScrollableControl1.Controls.Add(this.panelEnable);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(860, 335);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(284, 141);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 35);
            this.bttCancel.TabIndex = 310;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(3, 85);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.PasswordChar = '*';
            this.textEditPassword.Size = new System.Drawing.Size(237, 20);
            this.textEditPassword.TabIndex = 309;
            // 
            // textEditServer
            // 
            this.textEditServer.Location = new System.Drawing.Point(3, 7);
            this.textEditServer.Name = "textEditServer";
            this.textEditServer.Size = new System.Drawing.Size(237, 20);
            this.textEditServer.TabIndex = 308;
            // 
            // textEditUsername
            // 
            this.textEditUsername.Location = new System.Drawing.Point(3, 46);
            this.textEditUsername.Name = "textEditUsername";
            this.textEditUsername.Size = new System.Drawing.Size(237, 20);
            this.textEditUsername.TabIndex = 306;
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Location = new System.Drawing.Point(48, 94);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlPassword.Size = new System.Drawing.Size(65, 33);
            this.labelControlPassword.TabIndex = 305;
            this.labelControlPassword.Text = "รหัสผ่าน :";
            // 
            // labelControlUsername
            // 
            this.labelControlUsername.Location = new System.Drawing.Point(53, 55);
            this.labelControlUsername.Name = "labelControlUsername";
            this.labelControlUsername.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlUsername.Size = new System.Drawing.Size(60, 33);
            this.labelControlUsername.TabIndex = 304;
            this.labelControlUsername.Text = "ชื่อผู้ใช้ :";
            // 
            // labelControlServerName
            // 
            this.labelControlServerName.Location = new System.Drawing.Point(27, 16);
            this.labelControlServerName.Name = "labelControlServerName";
            this.labelControlServerName.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlServerName.Size = new System.Drawing.Size(86, 33);
            this.labelControlServerName.TabIndex = 303;
            this.labelControlServerName.Text = "ชื่อเซิร์ฟเวอร์ :";
            // 
            // bttSave
            // 
            this.bttSave.Location = new System.Drawing.Point(203, 141);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(75, 35);
            this.bttSave.TabIndex = 302;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(7, 7);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(864, 359);
            this.panelControl2.TabIndex = 18;
            // 
            // bttEdit
            // 
            this.bttEdit.Location = new System.Drawing.Point(122, 141);
            this.bttEdit.Name = "bttEdit";
            this.bttEdit.Size = new System.Drawing.Size(75, 35);
            this.bttEdit.TabIndex = 311;
            this.bttEdit.Text = "แก้ไข";
            this.bttEdit.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // panelEnable
            // 
            this.panelEnable.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelEnable.Appearance.Options.UseBackColor = true;
            this.panelEnable.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelEnable.Controls.Add(this.textEditUsername);
            this.panelEnable.Controls.Add(this.textEditServer);
            this.panelEnable.Controls.Add(this.textEditPassword);
            this.panelEnable.Location = new System.Drawing.Point(119, 16);
            this.panelEnable.Name = "panelEnable";
            this.panelEnable.Size = new System.Drawing.Size(347, 111);
            this.panelEnable.TabIndex = 312;
            // 
            // BackupOnline
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "BackupOnline";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(878, 399);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEnable)).EndInit();
            this.panelEnable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.TextEdit textEditServer;
        private DevExpress.XtraEditors.TextEdit textEditUsername;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.LabelControl labelControlUsername;
        private DevExpress.XtraEditors.LabelControl labelControlServerName;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton bttEdit;
        private DevExpress.XtraEditors.PanelControl panelEnable;


    }
}
