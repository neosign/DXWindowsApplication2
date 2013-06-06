namespace DXWindowsApplication2.UserForms
{
    partial class loginform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginform));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.checkEditRemember = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlUsername = new DevExpress.XtraEditors.LabelControl();
            this.labelControlUserGroup = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditGroupAccount = new DevExpress.XtraEditors.LookUpEdit();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bttAdd = new DevExpress.XtraEditors.SimpleButton();
            this.textEditUsername = new DevExpress.XtraEditors.TextEdit();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelErrorMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRemember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditGroupAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Controls.Add(this.groupBoxLogin);
            this.panelControl1.Controls.Add(this.labelErrorMsg);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.panelControl1.Size = new System.Drawing.Size(412, 353);
            this.panelControl1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 82);
            this.panel1.TabIndex = 322;
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.checkEditRemember);
            this.groupBoxLogin.Controls.Add(this.panelControl2);
            this.groupBoxLogin.Controls.Add(this.bttCancel);
            this.groupBoxLogin.Controls.Add(this.bttAdd);
            this.groupBoxLogin.Controls.Add(this.textEditUsername);
            this.groupBoxLogin.Controls.Add(this.textEditPassword);
            this.groupBoxLogin.Location = new System.Drawing.Point(13, 134);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(387, 194);
            this.groupBoxLogin.TabIndex = 321;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "เข้าระบบ";
            // 
            // checkEditRemember
            // 
            this.checkEditRemember.Location = new System.Drawing.Point(129, 89);
            this.checkEditRemember.Name = "checkEditRemember";
            this.checkEditRemember.Properties.Caption = "Remember Password";
            this.checkEditRemember.Size = new System.Drawing.Size(136, 19);
            this.checkEditRemember.TabIndex = 329;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControlUsername);
            this.panelControl2.Controls.Add(this.labelControlUserGroup);
            this.panelControl2.Controls.Add(this.labelControlPassword);
            this.panelControl2.Controls.Add(this.lookUpEditGroupAccount);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(3, 17);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(123, 174);
            this.panelControl2.TabIndex = 328;
            // 
            // labelControlUsername
            // 
            this.labelControlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlUsername.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlUsername.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlUsername.Location = new System.Drawing.Point(0, 0);
            this.labelControlUsername.Name = "labelControlUsername";
            this.labelControlUsername.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlUsername.Size = new System.Drawing.Size(120, 33);
            this.labelControlUsername.TabIndex = 320;
            this.labelControlUsername.Text = "ชื่อผู้ใช้งาน :";
            // 
            // labelControlUserGroup
            // 
            this.labelControlUserGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlUserGroup.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlUserGroup.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlUserGroup.Location = new System.Drawing.Point(0, 65);
            this.labelControlUserGroup.Name = "labelControlUserGroup";
            this.labelControlUserGroup.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlUserGroup.Size = new System.Drawing.Size(120, 33);
            this.labelControlUserGroup.TabIndex = 321;
            this.labelControlUserGroup.Text = "ชื่อกลุ่มผู้ใช้งาน :";
            this.labelControlUserGroup.Visible = false;
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPassword.Location = new System.Drawing.Point(0, 33);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlPassword.Size = new System.Drawing.Size(120, 33);
            this.labelControlPassword.TabIndex = 322;
            this.labelControlPassword.Text = "รหัสผ่าน :";
            // 
            // lookUpEditGroupAccount
            // 
            this.lookUpEditGroupAccount.Location = new System.Drawing.Point(31, 99);
            this.lookUpEditGroupAccount.Name = "lookUpEditGroupAccount";
            this.lookUpEditGroupAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditGroupAccount.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("group_name", "กลุ่ม")});
            this.lookUpEditGroupAccount.Size = new System.Drawing.Size(56, 20);
            this.lookUpEditGroupAccount.TabIndex = 325;
            this.lookUpEditGroupAccount.Visible = false;
            // 
            // bttCancel
            // 
            this.bttCancel.Image = ((System.Drawing.Image)(resources.GetObject("bttCancel.Image")));
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(242, 128);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(100, 55);
            this.bttCancel.TabIndex = 327;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // bttAdd
            // 
            this.bttAdd.Image = ((System.Drawing.Image)(resources.GetObject("bttAdd.Image")));
            this.bttAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttAdd.Location = new System.Drawing.Point(132, 128);
            this.bttAdd.Name = "bttAdd";
            this.bttAdd.Size = new System.Drawing.Size(100, 55);
            this.bttAdd.TabIndex = 326;
            this.bttAdd.Text = "เข้าสู่ระบบ";
            this.bttAdd.Click += new System.EventHandler(this.bttAdd_Click);
            // 
            // textEditUsername
            // 
            this.textEditUsername.Location = new System.Drawing.Point(132, 24);
            this.textEditUsername.Name = "textEditUsername";
            this.textEditUsername.Properties.MaxLength = 20;
            this.textEditUsername.Size = new System.Drawing.Size(210, 20);
            this.textEditUsername.TabIndex = 323;
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(132, 57);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.MaxLength = 20;
            this.textEditPassword.Properties.PasswordChar = '*';
            this.textEditPassword.Size = new System.Drawing.Size(210, 20);
            this.textEditPassword.TabIndex = 324;
            // 
            // labelErrorMsg
            // 
            this.labelErrorMsg.AutoSize = true;
            this.labelErrorMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMsg.Location = new System.Drawing.Point(13, 98);
            this.labelErrorMsg.Name = "labelErrorMsg";
            this.labelErrorMsg.Padding = new System.Windows.Forms.Padding(10);
            this.labelErrorMsg.Size = new System.Drawing.Size(20, 33);
            this.labelErrorMsg.TabIndex = 320;
            // 
            // loginform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 353);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "loginform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[ SOFTWARE NAME ]";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBoxLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRemember.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditGroupAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label labelErrorMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.SimpleButton bttAdd;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditGroupAccount;
        private DevExpress.XtraEditors.TextEdit textEditUsername;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.LabelControl labelControlUserGroup;
        private DevExpress.XtraEditors.LabelControl labelControlUsername;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckEdit checkEditRemember;
    }
}
