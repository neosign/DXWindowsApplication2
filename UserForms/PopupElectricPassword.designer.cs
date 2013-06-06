namespace DXWindowsApplication2.UserForms
{
    partial class PopupElectricPassword
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControlRemark = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNote = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(442, 191);
            this.panelControl2.TabIndex = 342;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bttSave);
            this.panelControl1.Controls.Add(this.bttCancel);
            this.panelControl1.Controls.Add(this.labelControlPassword);
            this.panelControl1.Controls.Add(this.textEditPassword);
            this.panelControl1.Controls.Add(this.labelControlRemark);
            this.panelControl1.Controls.Add(this.labelControlNote);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(418, 167);
            this.panelControl1.TabIndex = 466;
            // 
            // bttSave
            // 
            this.bttSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.savedisk;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(136, 107);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(70, 55);
            this.bttSave.TabIndex = 471;
            this.bttSave.Text = "บันทึก";
            // 
            // bttCancel
            // 
            this.bttCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(212, 107);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(70, 55);
            this.bttCancel.TabIndex = 470;
            this.bttCancel.Text = "ยกเลิก";
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPassword.Location = new System.Drawing.Point(5, 74);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Size = new System.Drawing.Size(70, 13);
            this.labelControlPassword.TabIndex = 469;
            this.labelControlPassword.Text = "รหัสผ่าน :";
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(81, 67);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.PasswordChar = '*';
            this.textEditPassword.Size = new System.Drawing.Size(183, 20);
            this.textEditPassword.TabIndex = 468;
            // 
            // labelControlText
            // 
            this.labelControlRemark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlRemark.Location = new System.Drawing.Point(5, 48);
            this.labelControlRemark.Name = "labelControlText";
            this.labelControlRemark.Size = new System.Drawing.Size(408, 13);
            this.labelControlRemark.TabIndex = 467;
            this.labelControlRemark.ForeColor = System.Drawing.Color.Red;
            this.labelControlRemark.Text = "** กรุณาทำการยืนยันรหัส่ผ่านของท่านเพื่อทำการตัดไฟ **";
            // 
            // labelControlNote
            // 
            this.labelControlNote.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlNote.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlNote.Location = new System.Drawing.Point(5, 5);
            this.labelControlNote.Name = "labelControlNote";
            this.labelControlNote.Size = new System.Drawing.Size(408, 36);
            this.labelControlNote.TabIndex = 466;
            this.labelControlNote.ForeColor = System.Drawing.Color.Red;
            this.labelControlNote.Text = "Note: การตัดไฟระบบจะงดจ่ายกระแสไฟฟ้าห้อง [Room Name] ชั่วคราว จนกว่าจะทำการสั่งต่" +
                "อไฟ ระบบจึงจะจ่ายกระแสไฟฟ้าตามปกติ";
            // 
            // PopupElectricPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 191);
            this.Controls.Add(this.panelControl2);
            this.Name = "PopupElectricPassword";
            this.Text = "ยืนยันหรัสผ่านเพื่อสั่งตัดกระแสไฟฟ้า";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.LabelControl labelControlRemark;
        private DevExpress.XtraEditors.LabelControl labelControlNote;


    }
}
