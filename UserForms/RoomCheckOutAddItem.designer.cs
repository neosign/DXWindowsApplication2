namespace DXWindowsApplication2.UserForms
{
    partial class RoomCheckOutAddItem
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
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.textEditItemName = new DevExpress.XtraEditors.TextEdit();
            this.textEditItemUnitPrice = new DevExpress.XtraEditors.TextEdit();
            this.labelControlItemPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelControlItemName = new DevExpress.XtraEditors.LabelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.textEditItemUnit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupVat = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupVat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton3
            // 
            this.simpleButton3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.simpleButton3.Location = new System.Drawing.Point(0, 0);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(461, 23);
            this.simpleButton3.TabIndex = 285;
            this.simpleButton3.Text = "เพิ่มข้อมูลรายละเอียดค่าใช้จ่ายเพิ่มเติม";
            // 
            // textEditItemName
            // 
            this.textEditItemName.Location = new System.Drawing.Point(134, 29);
            this.textEditItemName.Name = "textEditItemName";
            this.textEditItemName.Properties.Mask.BeepOnError = true;
            this.textEditItemName.Properties.Mask.EditMask = "([A-Z|a-z|0-9|ก-๙]| )*";
            this.textEditItemName.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditItemName.Size = new System.Drawing.Size(243, 20);
            this.textEditItemName.TabIndex = 304;
            // 
            // textEditItemUnitPrice
            // 
            this.textEditItemUnitPrice.EditValue = 0;
            this.textEditItemUnitPrice.Location = new System.Drawing.Point(134, 81);
            this.textEditItemUnitPrice.Name = "textEditItemUnitPrice";
            this.textEditItemUnitPrice.Properties.Mask.BeepOnError = true;
            this.textEditItemUnitPrice.Properties.Mask.EditMask = "n2";
            this.textEditItemUnitPrice.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnitPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnitPrice.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnitPrice.TabIndex = 303;
            // 
            // labelControlItemPrice
            // 
            this.labelControlItemPrice.Location = new System.Drawing.Point(36, 88);
            this.labelControlItemPrice.Name = "labelControlItemPrice";
            this.labelControlItemPrice.Size = new System.Drawing.Size(92, 13);
            this.labelControlItemPrice.TabIndex = 302;
            this.labelControlItemPrice.Text = "จำนวนเงินต่อหน่วย :";
            // 
            // labelControlItemName
            // 
            this.labelControlItemName.Location = new System.Drawing.Point(47, 36);
            this.labelControlItemName.Name = "labelControlItemName";
            this.labelControlItemName.Size = new System.Drawing.Size(81, 13);
            this.labelControlItemName.TabIndex = 301;
            this.labelControlItemName.Text = "รายการค่าใช้จ่าย :";
            // 
            // bttSave
            // 
            this.bttSave.Location = new System.Drawing.Point(134, 137);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(75, 35);
            this.bttSave.TabIndex = 300;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(215, 137);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 35);
            this.bttCancel.TabIndex = 299;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(383, 36);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(20, 13);
            this.labelControl32.TabIndex = 305;
            this.labelControl32.Text = "บาท";
            // 
            // textEditItemUnit
            // 
            this.textEditItemUnit.EditValue = 0;
            this.textEditItemUnit.Location = new System.Drawing.Point(134, 55);
            this.textEditItemUnit.Name = "textEditItemUnit";
            this.textEditItemUnit.Properties.Mask.BeepOnError = true;
            this.textEditItemUnit.Properties.Mask.EditMask = "n0";
            this.textEditItemUnit.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnit.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnit.TabIndex = 306;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(66, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 307;
            this.labelControl1.Text = "จำนวนหน่วย :";
            // 
            // radioGroupVat
            // 
            this.radioGroupVat.EditValue = 1F;
            this.radioGroupVat.Location = new System.Drawing.Point(134, 107);
            this.radioGroupVat.Name = "radioGroupVat";
            this.radioGroupVat.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupVat.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupVat.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupVat.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1F, "คิดรวมกับยอดสุทธิ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2F, "คิดเพิ่มจากยอดสุทธิ")});
            this.radioGroupVat.Size = new System.Drawing.Size(265, 24);
            this.radioGroupVat.TabIndex = 309;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(383, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 313;
            this.labelControl4.Text = "บาท";
            // 
            // RoomCheckOutAddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.radioGroupVat);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEditItemUnit);
            this.Controls.Add(this.labelControl32);
            this.Controls.Add(this.textEditItemName);
            this.Controls.Add(this.textEditItemUnitPrice);
            this.Controls.Add(this.labelControlItemPrice);
            this.Controls.Add(this.labelControlItemName);
            this.Controls.Add(this.bttSave);
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.simpleButton3);
            this.Name = "RoomCheckOutAddItem";
            this.Size = new System.Drawing.Size(461, 206);
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupVat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.TextEdit textEditItemName;
        private DevExpress.XtraEditors.TextEdit textEditItemUnitPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemName;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraEditors.TextEdit textEditItemUnit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroupVat;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
