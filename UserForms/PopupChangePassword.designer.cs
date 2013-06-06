namespace DXWindowsApplication2.UserForms
{
    partial class PopupChangePassword
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupControlText = new DevExpress.XtraEditors.GroupControl();
            this.labelControlRequired = new DevExpress.XtraEditors.LabelControl();
            this.labelControlOldPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNewPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControlConfirmPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditOldPassword = new DevExpress.XtraEditors.TextEdit();
            this.textEditConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.textEditNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.groupControlButton = new DevExpress.XtraEditors.GroupControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlText)).BeginInit();
            this.groupControlText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOldPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlButton)).BeginInit();
            this.groupControlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.pictureBox1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(145, 275);
            this.panelControl2.TabIndex = 342;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DXWindowsApplication2.Properties.Resources.preview;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(139, 151);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupControlText
            // 
            this.groupControlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlText.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlText.AppearanceCaption.Options.UseFont = true;
            this.groupControlText.Controls.Add(this.labelControlRequired);
            this.groupControlText.Controls.Add(this.labelControlOldPassword);
            this.groupControlText.Controls.Add(this.labelControlNewPassword);
            this.groupControlText.Controls.Add(this.labelControlConfirmPassword);
            this.groupControlText.Controls.Add(this.labelControl4);
            this.groupControlText.Controls.Add(this.labelControl3);
            this.groupControlText.Controls.Add(this.labelControl2);
            this.groupControlText.Controls.Add(this.textEditOldPassword);
            this.groupControlText.Controls.Add(this.textEditConfirmPassword);
            this.groupControlText.Controls.Add(this.textEditNewPassword);
            this.groupControlText.Location = new System.Drawing.Point(151, 12);
            this.groupControlText.Name = "groupControlText";
            this.groupControlText.Size = new System.Drawing.Size(313, 142);
            this.groupControlText.TabIndex = 343;
            this.groupControlText.Text = "เปลี่ยนรหัสผ่าน";
            // 
            // labelControlRequired
            // 
            this.labelControlRequired.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlRequired.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlRequired.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlRequired.Location = new System.Drawing.Point(38, 110);
            this.labelControlRequired.Name = "labelControlRequired";
            this.labelControlRequired.Size = new System.Drawing.Size(70, 13);
            this.labelControlRequired.TabIndex = 470;
            this.labelControlRequired.Text = "* โปรดระบุ";
            // 
            // labelControlOldPassword
            // 
            this.labelControlOldPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlOldPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlOldPassword.Location = new System.Drawing.Point(5, 39);
            this.labelControlOldPassword.Name = "labelControlOldPassword";
            this.labelControlOldPassword.Size = new System.Drawing.Size(103, 13);
            this.labelControlOldPassword.TabIndex = 471;
            this.labelControlOldPassword.Text = "รหัสผ่านเดิม :";
            // 
            // labelControlNewPassword
            // 
            this.labelControlNewPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlNewPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlNewPassword.Location = new System.Drawing.Point(5, 65);
            this.labelControlNewPassword.Name = "labelControlNewPassword";
            this.labelControlNewPassword.Size = new System.Drawing.Size(103, 13);
            this.labelControlNewPassword.TabIndex = 472;
            this.labelControlNewPassword.Text = "รหัสผ่านใหม่ :";
            // 
            // labelControlConfirmPassword
            // 
            this.labelControlConfirmPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlConfirmPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlConfirmPassword.Location = new System.Drawing.Point(5, 91);
            this.labelControlConfirmPassword.Name = "labelControlConfirmPassword";
            this.labelControlConfirmPassword.Size = new System.Drawing.Size(103, 13);
            this.labelControlConfirmPassword.TabIndex = 473;
            this.labelControlConfirmPassword.Text = "ยืนยันรหัสผ่านใหม่ :";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Location = new System.Drawing.Point(300, 87);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(6, 13);
            this.labelControl4.TabIndex = 468;
            this.labelControl4.Text = "*";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Location = new System.Drawing.Point(300, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 469;
            this.labelControl3.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Location = new System.Drawing.Point(300, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 13);
            this.labelControl2.TabIndex = 467;
            this.labelControl2.Text = "*";
            // 
            // textEditOldPassword
            // 
            this.textEditOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditOldPassword.EditValue = "";
            this.textEditOldPassword.Location = new System.Drawing.Point(114, 32);
            this.textEditOldPassword.Name = "textEditOldPassword";
            this.textEditOldPassword.Properties.MaxLength = 20;
            this.textEditOldPassword.Size = new System.Drawing.Size(180, 20);
            this.textEditOldPassword.TabIndex = 464;
            // 
            // textEditConfirmPassword
            // 
            this.textEditConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditConfirmPassword.EditValue = "";
            this.textEditConfirmPassword.Location = new System.Drawing.Point(114, 84);
            this.textEditConfirmPassword.Name = "textEditConfirmPassword";
            this.textEditConfirmPassword.Properties.MaxLength = 20;
            this.textEditConfirmPassword.Properties.PasswordChar = '*';
            this.textEditConfirmPassword.Size = new System.Drawing.Size(180, 20);
            this.textEditConfirmPassword.TabIndex = 466;
            // 
            // textEditNewPassword
            // 
            this.textEditNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditNewPassword.EditValue = "";
            this.textEditNewPassword.Location = new System.Drawing.Point(114, 58);
            this.textEditNewPassword.Name = "textEditNewPassword";
            this.textEditNewPassword.Properties.MaxLength = 20;
            this.textEditNewPassword.Properties.PasswordChar = '*';
            this.textEditNewPassword.Size = new System.Drawing.Size(180, 20);
            this.textEditNewPassword.TabIndex = 465;
            // 
            // groupControlButton
            // 
            this.groupControlButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlButton.Controls.Add(this.bttSave);
            this.groupControlButton.Controls.Add(this.bttCancel);
            this.groupControlButton.Location = new System.Drawing.Point(151, 182);
            this.groupControlButton.Name = "groupControlButton";
            this.groupControlButton.ShowCaption = false;
            this.groupControlButton.Size = new System.Drawing.Size(313, 81);
            this.groupControlButton.TabIndex = 344;
            this.groupControlButton.Text = "groupControl2";
            // 
            // bttSave
            // 
            this.bttSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.savedisk;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(83, 13);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(70, 55);
            this.bttSave.TabIndex = 463;
            this.bttSave.Text = "บันทึก";
            // 
            // bttCancel
            // 
            this.bttCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(159, 13);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(70, 55);
            this.bttCancel.TabIndex = 462;
            this.bttCancel.Text = "ยกเลิก";
            // 
            // PopupChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 275);
            this.Controls.Add(this.groupControlButton);
            this.Controls.Add(this.groupControlText);
            this.Controls.Add(this.panelControl2);
            this.Name = "PopupChangePassword";
            this.Text = "เปลี่ยนรหัสผ่าน";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlText)).EndInit();
            this.groupControlText.ResumeLayout(false);
            this.groupControlText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOldPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlButton)).EndInit();
            this.groupControlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControlText;
        private DevExpress.XtraEditors.LabelControl labelControlRequired;
        private DevExpress.XtraEditors.LabelControl labelControlOldPassword;
        private DevExpress.XtraEditors.LabelControl labelControlNewPassword;
        private DevExpress.XtraEditors.LabelControl labelControlConfirmPassword;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditOldPassword;
        private DevExpress.XtraEditors.TextEdit textEditConfirmPassword;
        private DevExpress.XtraEditors.TextEdit textEditNewPassword;
        private DevExpress.XtraEditors.GroupControl groupControlButton;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}
