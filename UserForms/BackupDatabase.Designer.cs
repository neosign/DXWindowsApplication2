namespace DXWindowsApplication2.UserForms
{
    partial class BackupDatabase
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
            this.groupControlTitle = new DevExpress.XtraEditors.GroupControl();
            this.panelControlAuto = new DevExpress.XtraEditors.PanelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.textEditDatapath = new DevExpress.XtraEditors.TextEdit();
            this.timeEditEveryDay = new DevExpress.XtraEditors.TimeEdit();
            this.rbEveryDay = new System.Windows.Forms.RadioButton();
            this.labelControlDataPath = new DevExpress.XtraEditors.LabelControl();
            this.labelControlTimeRemark = new DevExpress.XtraEditors.LabelControl();
            this.rbEveryBoot = new System.Windows.Forms.RadioButton();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelImportRemark = new DevExpress.XtraEditors.LabelControl();
            this.bttImport = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlUser = new DevExpress.XtraEditors.PanelControl();
            this.bttBackup = new DevExpress.XtraEditors.SimpleButton();
            this.labelBackupRemark = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTitle)).BeginInit();
            this.groupControlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAuto)).BeginInit();
            this.panelControlAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatapath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditEveryDay.Properties)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUser)).BeginInit();
            this.panelControlUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControlTitle
            // 
            this.groupControlTitle.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlTitle.AppearanceCaption.Options.UseFont = true;
            this.groupControlTitle.Controls.Add(this.panelControl1);
            this.groupControlTitle.Controls.Add(this.panelControlUser);
            this.groupControlTitle.Controls.Add(this.panelControlAuto);
            this.groupControlTitle.Controls.Add(this.checkBoxAuto);
            this.groupControlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlTitle.Location = new System.Drawing.Point(0, 0);
            this.groupControlTitle.Name = "groupControlTitle";
            this.groupControlTitle.Size = new System.Drawing.Size(938, 470);
            this.groupControlTitle.TabIndex = 17;
            this.groupControlTitle.Text = "สำรอง/นำเข้า ฐานข้อมูล";
            // 
            // panelControlAuto
            // 
            this.panelControlAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControlAuto.Controls.Add(this.bttSave);
            this.panelControlAuto.Controls.Add(this.bttSelectFile);
            this.panelControlAuto.Controls.Add(this.textEditDatapath);
            this.panelControlAuto.Controls.Add(this.timeEditEveryDay);
            this.panelControlAuto.Controls.Add(this.rbEveryDay);
            this.panelControlAuto.Controls.Add(this.labelControlDataPath);
            this.panelControlAuto.Controls.Add(this.labelControlTimeRemark);
            this.panelControlAuto.Controls.Add(this.rbEveryBoot);
            this.panelControlAuto.Location = new System.Drawing.Point(18, 49);
            this.panelControlAuto.Name = "panelControlAuto";
            this.panelControlAuto.Size = new System.Drawing.Size(909, 80);
            this.panelControlAuto.TabIndex = 2;
            // 
            // bttSave
            // 
            this.bttSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttSave.Enabled = false;
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.save;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(834, 18);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(70, 55);
            this.bttSave.TabIndex = 439;
            this.bttSave.Text = "บันทึก";
            // 
            // bttSelectFile
            // 
            this.bttSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttSelectFile.Location = new System.Drawing.Point(660, 50);
            this.bttSelectFile.Name = "bttSelectFile";
            this.bttSelectFile.Size = new System.Drawing.Size(75, 23);
            this.bttSelectFile.TabIndex = 438;
            this.bttSelectFile.Text = "เลือก";
            // 
            // textEditDatapath
            // 
            this.textEditDatapath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditDatapath.Enabled = false;
            this.textEditDatapath.EnterMoveNextControl = true;
            this.textEditDatapath.Location = new System.Drawing.Point(101, 53);
            this.textEditDatapath.Name = "textEditDatapath";
            this.textEditDatapath.Size = new System.Drawing.Size(553, 20);
            this.textEditDatapath.TabIndex = 437;
            // 
            // timeEditEveryDay
            // 
            this.timeEditEveryDay.EditValue = new System.DateTime(2012, 3, 19, 0, 0, 0, 0);
            this.timeEditEveryDay.Location = new System.Drawing.Point(101, 26);
            this.timeEditEveryDay.Name = "timeEditEveryDay";
            this.timeEditEveryDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEditEveryDay.Size = new System.Drawing.Size(100, 20);
            this.timeEditEveryDay.TabIndex = 436;
            // 
            // rbEveryDay
            // 
            this.rbEveryDay.AutoSize = true;
            this.rbEveryDay.Location = new System.Drawing.Point(19, 29);
            this.rbEveryDay.Name = "rbEveryDay";
            this.rbEveryDay.Size = new System.Drawing.Size(76, 17);
            this.rbEveryDay.TabIndex = 0;
            this.rbEveryDay.Text = "ทุกวัน เวลา";
            this.rbEveryDay.UseVisualStyleBackColor = true;
            // 
            // labelControlDataPath
            // 
            this.labelControlDataPath.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlDataPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDataPath.Location = new System.Drawing.Point(19, 60);
            this.labelControlDataPath.Name = "labelControlDataPath";
            this.labelControlDataPath.Size = new System.Drawing.Size(76, 13);
            this.labelControlDataPath.TabIndex = 305;
            this.labelControlDataPath.Text = "ที่จัดเก็บ :";
            // 
            // labelControlTimeRemark
            // 
            this.labelControlTimeRemark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTimeRemark.Location = new System.Drawing.Point(207, 31);
            this.labelControlTimeRemark.Name = "labelControlTimeRemark";
            this.labelControlTimeRemark.Size = new System.Drawing.Size(492, 13);
            this.labelControlTimeRemark.TabIndex = 305;
            this.labelControlTimeRemark.Text = "(กรณีเปิดคอมพิวเตอร์ช้า เกินเวลาที่กำหนด ระบบจะสำรองฐานข้อมูลเมื่อเปิดคอมพิวเตอร์" +
                ")";
            // 
            // rbEveryBoot
            // 
            this.rbEveryBoot.AutoSize = true;
            this.rbEveryBoot.Checked = true;
            this.rbEveryBoot.Location = new System.Drawing.Point(19, 6);
            this.rbEveryBoot.Name = "rbEveryBoot";
            this.rbEveryBoot.Size = new System.Drawing.Size(149, 17);
            this.rbEveryBoot.TabIndex = 0;
            this.rbEveryBoot.TabStop = true;
            this.rbEveryBoot.Text = "ทุกครั้งเมื่อเปิดคอมพิวเตอร์";
            this.rbEveryBoot.UseVisualStyleBackColor = true;
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(18, 26);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(141, 17);
            this.checkBoxAuto.TabIndex = 1;
            this.checkBoxAuto.Text = "สำรองฐานข้อมูลอัตโนมัติ";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Controls.Add(this.groupControlTitle);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(7, 7);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(938, 470);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.labelImportRemark);
            this.panelControl1.Controls.Add(this.bttImport);
            this.panelControl1.Location = new System.Drawing.Point(5, 241);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(922, 100);
            this.panelControl1.TabIndex = 310;
            // 
            // labelImportRemark
            // 
            this.labelImportRemark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelImportRemark.Location = new System.Drawing.Point(18, 67);
            this.labelImportRemark.Name = "labelImportRemark";
            this.labelImportRemark.Size = new System.Drawing.Size(257, 13);
            this.labelImportRemark.TabIndex = 309;
            this.labelImportRemark.Text = "(นำเข้าฐานข้อมูลจากไฟล์ที่สำรองไว้)";
            // 
            // bttImport
            // 
            this.bttImport.Image = global::DXWindowsApplication2.Properties.Resources.edit;
            this.bttImport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttImport.Location = new System.Drawing.Point(18, 6);
            this.bttImport.Name = "bttImport";
            this.bttImport.Size = new System.Drawing.Size(164, 55);
            this.bttImport.TabIndex = 308;
            this.bttImport.Text = "นำเข้าฐานข้อมูลสำรอง";
            // 
            // panelControlUser
            // 
            this.panelControlUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControlUser.Controls.Add(this.bttBackup);
            this.panelControlUser.Controls.Add(this.labelBackupRemark);
            this.panelControlUser.Location = new System.Drawing.Point(5, 135);
            this.panelControlUser.Name = "panelControlUser";
            this.panelControlUser.Size = new System.Drawing.Size(922, 100);
            this.panelControlUser.TabIndex = 309;
            // 
            // bttBackup
            // 
            this.bttBackup.Image = global::DXWindowsApplication2.Properties.Resources.edit;
            this.bttBackup.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttBackup.Location = new System.Drawing.Point(18, 6);
            this.bttBackup.Name = "bttBackup";
            this.bttBackup.Size = new System.Drawing.Size(164, 55);
            this.bttBackup.TabIndex = 20;
            this.bttBackup.Text = "สำรองฐานข้อมูลโดยผู้ใช้";
            // 
            // labelBackupRemark
            // 
            this.labelBackupRemark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelBackupRemark.Location = new System.Drawing.Point(18, 67);
            this.labelBackupRemark.Name = "labelBackupRemark";
            this.labelBackupRemark.Size = new System.Drawing.Size(205, 13);
            this.labelBackupRemark.TabIndex = 305;
            this.labelBackupRemark.Text = "(สำรองฐานข้อมูลปัจจุบัน)";
            // 
            // BackupDatabase
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Name = "BackupDatabase";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(952, 484);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTitle)).EndInit();
            this.groupControlTitle.ResumeLayout(false);
            this.groupControlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAuto)).EndInit();
            this.panelControlAuto.ResumeLayout(false);
            this.panelControlAuto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatapath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditEveryDay.Properties)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUser)).EndInit();
            this.panelControlUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlTitle;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private System.Windows.Forms.CheckBox checkBoxAuto;
        private DevExpress.XtraEditors.PanelControl panelControlAuto;
        private System.Windows.Forms.RadioButton rbEveryBoot;
        private System.Windows.Forms.RadioButton rbEveryDay;
        private DevExpress.XtraEditors.TimeEdit timeEditEveryDay;
        private DevExpress.XtraEditors.LabelControl labelControlTimeRemark;
        private DevExpress.XtraEditors.SimpleButton bttSelectFile;
        private DevExpress.XtraEditors.TextEdit textEditDatapath;
        private DevExpress.XtraEditors.LabelControl labelControlDataPath;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelImportRemark;
        private DevExpress.XtraEditors.SimpleButton bttImport;
        private DevExpress.XtraEditors.PanelControl panelControlUser;
        private DevExpress.XtraEditors.SimpleButton bttBackup;
        private DevExpress.XtraEditors.LabelControl labelBackupRemark;
    }
}
