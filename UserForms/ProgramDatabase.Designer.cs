namespace DXWindowsApplication2.UserForms
{
    partial class ProgramDatabase
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
            this.textEditDatabasePassword = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bttEdit = new DevExpress.XtraEditors.SimpleButton();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelUserName = new DevExpress.XtraEditors.LabelControl();
            this.labelServerName = new DevExpress.XtraEditors.LabelControl();
            this.panelEnable = new DevExpress.XtraEditors.PanelControl();
            this.textEditDatabaseUsername = new DevExpress.XtraEditors.TextEdit();
            this.textEditServerName = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabasePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEnable)).BeginInit();
            this.panelEnable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabaseUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditDatabasePassword
            // 
            this.textEditDatabasePassword.Location = new System.Drawing.Point(3, 97);
            this.textEditDatabasePassword.Name = "textEditDatabasePassword";
            this.textEditDatabasePassword.Properties.PasswordChar = '*';
            this.textEditDatabasePassword.Size = new System.Drawing.Size(279, 20);
            this.textEditDatabasePassword.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.xtraScrollableControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(858, 351);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "ตั้งค่าการเชื่อมต่อฐานข้อมูล";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Controls.Add(this.labelControl3);
            this.xtraScrollableControl1.Controls.Add(this.labelControl2);
            this.xtraScrollableControl1.Controls.Add(this.labelControl1);
            this.xtraScrollableControl1.Controls.Add(this.bttCancel);
            this.xtraScrollableControl1.Controls.Add(this.bttEdit);
            this.xtraScrollableControl1.Controls.Add(this.bttSave);
            this.xtraScrollableControl1.Controls.Add(this.labelPassword);
            this.xtraScrollableControl1.Controls.Add(this.labelUserName);
            this.xtraScrollableControl1.Controls.Add(this.labelServerName);
            this.xtraScrollableControl1.Controls.Add(this.panelEnable);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(854, 327);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Location = new System.Drawing.Point(51, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl3.Size = new System.Drawing.Size(26, 33);
            this.labelControl3.TabIndex = 305;
            this.labelControl3.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Location = new System.Drawing.Point(51, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl2.Size = new System.Drawing.Size(26, 33);
            this.labelControl2.TabIndex = 304;
            this.labelControl2.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(31, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(26, 33);
            this.labelControl1.TabIndex = 303;
            this.labelControl1.Text = "*";
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(308, 149);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 35);
            this.bttCancel.TabIndex = 6;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // bttEdit
            // 
            this.bttEdit.Location = new System.Drawing.Point(146, 149);
            this.bttEdit.Name = "bttEdit";
            this.bttEdit.Size = new System.Drawing.Size(75, 35);
            this.bttEdit.TabIndex = 4;
            this.bttEdit.Text = "แก้ไข";
            this.bttEdit.Click += new System.EventHandler(this.bttEdit_Click);
            // 
            // bttSave
            // 
            this.bttSave.Location = new System.Drawing.Point(227, 149);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(75, 35);
            this.bttSave.TabIndex = 5;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(72, 103);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Padding = new System.Windows.Forms.Padding(10);
            this.labelPassword.Size = new System.Drawing.Size(65, 33);
            this.labelPassword.TabIndex = 84;
            this.labelPassword.Text = "รหัสผ่าน :";
            // 
            // labelUserName
            // 
            this.labelUserName.Location = new System.Drawing.Point(77, 64);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Padding = new System.Windows.Forms.Padding(10);
            this.labelUserName.Size = new System.Drawing.Size(60, 33);
            this.labelUserName.TabIndex = 82;
            this.labelUserName.Text = "ชื่อผู้ใช้ :";
            // 
            // labelServerName
            // 
            this.labelServerName.Location = new System.Drawing.Point(51, 25);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Padding = new System.Windows.Forms.Padding(10);
            this.labelServerName.Size = new System.Drawing.Size(86, 33);
            this.labelServerName.TabIndex = 80;
            this.labelServerName.Text = "ชื่อเซิร์ฟเวอร์ :";
            // 
            // panelEnable
            // 
            this.panelEnable.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelEnable.Appearance.Options.UseBackColor = true;
            this.panelEnable.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelEnable.Controls.Add(this.textEditDatabaseUsername);
            this.panelEnable.Controls.Add(this.textEditServerName);
            this.panelEnable.Controls.Add(this.textEditDatabasePassword);
            this.panelEnable.Location = new System.Drawing.Point(143, 13);
            this.panelEnable.Name = "panelEnable";
            this.panelEnable.Size = new System.Drawing.Size(332, 130);
            this.panelEnable.TabIndex = 306;
            // 
            // textEditDatabaseUsername
            // 
            this.textEditDatabaseUsername.Location = new System.Drawing.Point(3, 58);
            this.textEditDatabaseUsername.Name = "textEditDatabaseUsername";
            this.textEditDatabaseUsername.Properties.Mask.EditMask = "([a-zA-Z0-9]*)";
            this.textEditDatabaseUsername.Size = new System.Drawing.Size(279, 20);
            this.textEditDatabaseUsername.TabIndex = 2;
            // 
            // textEditServerName
            // 
            this.textEditServerName.Location = new System.Drawing.Point(3, 19);
            this.textEditServerName.Name = "textEditServerName";
            this.textEditServerName.Properties.Mask.EditMask = "([a-zA-Z0-9]*)";
            this.textEditServerName.Size = new System.Drawing.Size(279, 20);
            this.textEditServerName.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(7, 7);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(858, 351);
            this.panelControl2.TabIndex = 16;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(7, 358);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(858, 26);
            this.panelControl1.TabIndex = 15;
            // 
            // ProgramDatabase
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "ProgramDatabase";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(872, 391);
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabasePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEnable)).EndInit();
            this.panelEnable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabaseUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditDatabasePassword;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.TextEdit textEditServerName;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.TextEdit textEditDatabaseUsername;
        private DevExpress.XtraEditors.LabelControl labelPassword;
        private DevExpress.XtraEditors.LabelControl labelUserName;
        private DevExpress.XtraEditors.LabelControl labelServerName;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.SimpleButton bttEdit;
        private DevExpress.XtraEditors.PanelControl panelEnable;

    }
}
