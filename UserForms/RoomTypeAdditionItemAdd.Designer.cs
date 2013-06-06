namespace DXWindowsApplication2.UserForms
{
    partial class RoomTypeAdditionItemAdd
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
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.titleTabAddition = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlbaht2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlbaht1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditPayType = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditVatType = new DevExpress.XtraEditors.LookUpEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.textEditDailyPrice = new DevExpress.XtraEditors.TextEdit();
            this.textEditMonthPrice = new DevExpress.XtraEditors.TextEdit();
            this.textEditItemName = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlRequired = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
            this.labelControlVatType = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDailyPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelControlMonthPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPayType = new DevExpress.XtraEditors.LabelControl();
            this.labelControlItemName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPayType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDailyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMonthPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttSave
            // 
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.savedisk;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(108, 272);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(70, 55);
            this.bttSave.TabIndex = 328;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // bttCancel
            // 
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(189, 272);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(70, 55);
            this.bttCancel.TabIndex = 327;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // titleTabAddition
            // 
            this.titleTabAddition.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.titleTabAddition.Appearance.Options.UseFont = true;
            this.titleTabAddition.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.titleTabAddition.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleTabAddition.Location = new System.Drawing.Point(0, 0);
            this.titleTabAddition.Name = "titleTabAddition";
            this.titleTabAddition.Size = new System.Drawing.Size(711, 23);
            this.titleTabAddition.TabIndex = 326;
            this.titleTabAddition.Text = "เพิ่มข้อมูลค่าใช้จ่ายเพิ่มเติม";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControlbaht2);
            this.panelControl1.Controls.Add(this.labelControlbaht1);
            this.panelControl1.Controls.Add(this.lookUpEditPayType);
            this.panelControl1.Controls.Add(this.lookUpEditVatType);
            this.panelControl1.Controls.Add(this.memoEditDescription);
            this.panelControl1.Controls.Add(this.textEditDailyPrice);
            this.panelControl1.Controls.Add(this.textEditMonthPrice);
            this.panelControl1.Controls.Add(this.textEditItemName);
            this.panelControl1.Controls.Add(this.bttSave);
            this.panelControl1.Controls.Add(this.bttCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(174, 23);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(537, 340);
            this.panelControl1.TabIndex = 335;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl9.Location = new System.Drawing.Point(379, 133);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(6, 13);
            this.labelControl9.TabIndex = 411;
            this.labelControl9.Text = "*";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Location = new System.Drawing.Point(379, 55);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(6, 13);
            this.labelControl8.TabIndex = 411;
            this.labelControl8.Text = "*";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl6.Location = new System.Drawing.Point(379, 29);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(6, 13);
            this.labelControl6.TabIndex = 411;
            this.labelControl6.Text = "*";
            // 
            // labelControlbaht2
            // 
            this.labelControlbaht2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlbaht2.Location = new System.Drawing.Point(400, 107);
            this.labelControlbaht2.Name = "labelControlbaht2";
            this.labelControlbaht2.Size = new System.Drawing.Size(41, 20);
            this.labelControlbaht2.TabIndex = 408;
            this.labelControlbaht2.Text = "บาท";
            // 
            // labelControlbaht1
            // 
            this.labelControlbaht1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlbaht1.Location = new System.Drawing.Point(400, 81);
            this.labelControlbaht1.Name = "labelControlbaht1";
            this.labelControlbaht1.Size = new System.Drawing.Size(41, 20);
            this.labelControlbaht1.TabIndex = 408;
            this.labelControlbaht1.Text = "บาท";
            // 
            // lookUpEditPayType
            // 
            this.lookUpEditPayType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpEditPayType.Location = new System.Drawing.Point(3, 55);
            this.lookUpEditPayType.Name = "lookUpEditPayType";
            this.lookUpEditPayType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditPayType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("paytype_label", " ")});
            this.lookUpEditPayType.Size = new System.Drawing.Size(370, 20);
            this.lookUpEditPayType.TabIndex = 340;
            // 
            // lookUpEditVatType
            // 
            this.lookUpEditVatType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpEditVatType.Location = new System.Drawing.Point(3, 134);
            this.lookUpEditVatType.Name = "lookUpEditVatType";
            this.lookUpEditVatType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditVatType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("vattype_label", " ")});
            this.lookUpEditVatType.Size = new System.Drawing.Size(370, 20);
            this.lookUpEditVatType.TabIndex = 340;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditDescription.EditValue = "";
            this.memoEditDescription.Location = new System.Drawing.Point(3, 160);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.MaxLength = 500;
            this.memoEditDescription.Size = new System.Drawing.Size(370, 96);
            this.memoEditDescription.TabIndex = 339;
            // 
            // textEditDailyPrice
            // 
            this.textEditDailyPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditDailyPrice.EditValue = "0.00";
            this.textEditDailyPrice.Location = new System.Drawing.Point(3, 107);
            this.textEditDailyPrice.Name = "textEditDailyPrice";
            this.textEditDailyPrice.Properties.Mask.BeepOnError = true;
            this.textEditDailyPrice.Properties.Mask.EditMask = "n2";
            this.textEditDailyPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditDailyPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditDailyPrice.Properties.MaxLength = 7;
            this.textEditDailyPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textEditDailyPrice.Size = new System.Drawing.Size(370, 20);
            this.textEditDailyPrice.TabIndex = 338;
            // 
            // textEditMonthPrice
            // 
            this.textEditMonthPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditMonthPrice.EditValue = "0";
            this.textEditMonthPrice.Location = new System.Drawing.Point(3, 81);
            this.textEditMonthPrice.Name = "textEditMonthPrice";
            this.textEditMonthPrice.Properties.Mask.BeepOnError = true;
            this.textEditMonthPrice.Properties.Mask.EditMask = "n2";
            this.textEditMonthPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditMonthPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditMonthPrice.Properties.MaxLength = 7;
            this.textEditMonthPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textEditMonthPrice.Size = new System.Drawing.Size(370, 20);
            this.textEditMonthPrice.TabIndex = 335;
            // 
            // textEditItemName
            // 
            this.textEditItemName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditItemName.Location = new System.Drawing.Point(3, 29);
            this.textEditItemName.Name = "textEditItemName";
            this.textEditItemName.Properties.Mask.BeepOnError = true;
            this.textEditItemName.Properties.Mask.EditMask = "([a-zA-Z0-9|ก-๙|\\\' \']){0,50}";
            this.textEditItemName.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditItemName.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditItemName.Properties.MaxLength = 50;
            this.textEditItemName.Size = new System.Drawing.Size(370, 20);
            this.textEditItemName.TabIndex = 337;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControlRequired);
            this.panelControl2.Controls.Add(this.labelControlDescription);
            this.panelControl2.Controls.Add(this.labelControlVatType);
            this.panelControl2.Controls.Add(this.labelControlDailyPrice);
            this.panelControl2.Controls.Add(this.labelControlMonthPrice);
            this.panelControl2.Controls.Add(this.labelControlPayType);
            this.panelControl2.Controls.Add(this.labelControlItemName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 23);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(168, 340);
            this.panelControl2.TabIndex = 336;
            // 
            // labelControlRequired
            // 
            this.labelControlRequired.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlRequired.Location = new System.Drawing.Point(115, 272);
            this.labelControlRequired.Name = "labelControlRequired";
            this.labelControlRequired.Size = new System.Drawing.Size(50, 13);
            this.labelControlRequired.TabIndex = 412;
            this.labelControlRequired.Text = "* โปรดระบุ";
            // 
            // labelControlDescription
            // 
            this.labelControlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlDescription.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDescription.Location = new System.Drawing.Point(60, 159);
            this.labelControlDescription.Name = "labelControlDescription";
            this.labelControlDescription.Size = new System.Drawing.Size(105, 20);
            this.labelControlDescription.TabIndex = 333;
            this.labelControlDescription.Text = "รายละเอียดเพิ่มเติม :";
            // 
            // labelControlVatType
            // 
            this.labelControlVatType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlVatType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlVatType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlVatType.Location = new System.Drawing.Point(60, 133);
            this.labelControlVatType.Name = "labelControlVatType";
            this.labelControlVatType.Size = new System.Drawing.Size(105, 20);
            this.labelControlVatType.TabIndex = 334;
            this.labelControlVatType.Text = "การคิดภาษี :";
            // 
            // labelControlDailyPrice
            // 
            this.labelControlDailyPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlDailyPrice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlDailyPrice.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDailyPrice.Location = new System.Drawing.Point(60, 107);
            this.labelControlDailyPrice.Name = "labelControlDailyPrice";
            this.labelControlDailyPrice.Size = new System.Drawing.Size(105, 20);
            this.labelControlDailyPrice.TabIndex = 335;
            this.labelControlDailyPrice.Text = "ราคารายวัน :";
            // 
            // labelControlMonthPrice
            // 
            this.labelControlMonthPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlMonthPrice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlMonthPrice.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlMonthPrice.Location = new System.Drawing.Point(60, 81);
            this.labelControlMonthPrice.Name = "labelControlMonthPrice";
            this.labelControlMonthPrice.Size = new System.Drawing.Size(105, 20);
            this.labelControlMonthPrice.TabIndex = 330;
            this.labelControlMonthPrice.Text = "ราคารายเดือน :";
            // 
            // labelControlPayType
            // 
            this.labelControlPayType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlPayType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlPayType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPayType.Location = new System.Drawing.Point(60, 55);
            this.labelControlPayType.Name = "labelControlPayType";
            this.labelControlPayType.Size = new System.Drawing.Size(105, 20);
            this.labelControlPayType.TabIndex = 331;
            this.labelControlPayType.Text = "รูปแบบค่าใช้จ่าย :";
            // 
            // labelControlItemName
            // 
            this.labelControlItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlItemName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlItemName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlItemName.Location = new System.Drawing.Point(60, 29);
            this.labelControlItemName.Name = "labelControlItemName";
            this.labelControlItemName.Size = new System.Drawing.Size(105, 20);
            this.labelControlItemName.TabIndex = 332;
            this.labelControlItemName.Text = "ชื่อรายการค่าใช้จ่าย :";
            // 
            // RoomTypeAdditionItemAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.titleTabAddition);
            this.Name = "RoomTypeAdditionItemAdd";
            this.Size = new System.Drawing.Size(711, 363);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPayType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDailyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMonthPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.SimpleButton titleTabAddition;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditVatType;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.TextEdit textEditDailyPrice;
        private DevExpress.XtraEditors.TextEdit textEditMonthPrice;
        private DevExpress.XtraEditors.TextEdit textEditItemName;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlDescription;
        private DevExpress.XtraEditors.LabelControl labelControlVatType;
        private DevExpress.XtraEditors.LabelControl labelControlDailyPrice;
        private DevExpress.XtraEditors.LabelControl labelControlMonthPrice;
        private DevExpress.XtraEditors.LabelControl labelControlPayType;
        private DevExpress.XtraEditors.LabelControl labelControlItemName;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditPayType;
        private DevExpress.XtraEditors.LabelControl labelControlbaht2;
        private DevExpress.XtraEditors.LabelControl labelControlbaht1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControlRequired;
    }
}
