namespace DXWindowsApplication2.UserForms
{
    partial class PopUpRecieptItem
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
            this.titleTabAddition = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditItemUnit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.textEditItemUnitPrice = new DevExpress.XtraEditors.TextEdit();
            this.labelControlItemPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelControlItemName = new DevExpress.XtraEditors.LabelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditVatType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControlVatType = new DevExpress.XtraEditors.LabelControl();
            this.mruEditItemName = new DevExpress.XtraEditors.MRUEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruEditItemName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // titleTabAddition
            // 
            this.titleTabAddition.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.titleTabAddition.Appearance.Options.UseFont = true;
            this.titleTabAddition.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.titleTabAddition.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleTabAddition.Location = new System.Drawing.Point(0, 0);
            this.titleTabAddition.Name = "titleTabAddition";
            this.titleTabAddition.Size = new System.Drawing.Size(457, 23);
            this.titleTabAddition.TabIndex = 337;
            this.titleTabAddition.Text = "เพิ่มข้อมูลค่าใช้จ่ายเพิ่มเติม";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(385, 117);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 348;
            this.labelControl4.Text = "บาท";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(68, 91);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 346;
            this.labelControl1.Text = "จำนวนหน่วย :";
            // 
            // textEditItemUnit
            // 
            this.textEditItemUnit.EditValue = 0;
            this.textEditItemUnit.Location = new System.Drawing.Point(136, 84);
            this.textEditItemUnit.Name = "textEditItemUnit";
            this.textEditItemUnit.Properties.Mask.BeepOnError = true;
            this.textEditItemUnit.Properties.Mask.EditMask = "n0";
            this.textEditItemUnit.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnit.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnit.TabIndex = 345;
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(385, 87);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(20, 13);
            this.labelControl32.TabIndex = 344;
            this.labelControl32.Text = "บาท";
            // 
            // textEditItemUnitPrice
            // 
            this.textEditItemUnitPrice.EditValue = 0;
            this.textEditItemUnitPrice.Location = new System.Drawing.Point(136, 110);
            this.textEditItemUnitPrice.Name = "textEditItemUnitPrice";
            this.textEditItemUnitPrice.Properties.Mask.BeepOnError = true;
            this.textEditItemUnitPrice.Properties.Mask.EditMask = "n2";
            this.textEditItemUnitPrice.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnitPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnitPrice.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnitPrice.TabIndex = 342;
            // 
            // labelControlItemPrice
            // 
            this.labelControlItemPrice.Location = new System.Drawing.Point(38, 117);
            this.labelControlItemPrice.Name = "labelControlItemPrice";
            this.labelControlItemPrice.Size = new System.Drawing.Size(92, 13);
            this.labelControlItemPrice.TabIndex = 341;
            this.labelControlItemPrice.Text = "จำนวนเงินต่อหน่วย :";
            // 
            // labelControlItemName
            // 
            this.labelControlItemName.Location = new System.Drawing.Point(49, 65);
            this.labelControlItemName.Name = "labelControlItemName";
            this.labelControlItemName.Size = new System.Drawing.Size(81, 13);
            this.labelControlItemName.TabIndex = 340;
            this.labelControlItemName.Text = "รายการค่าใช้จ่าย :";
            // 
            // bttSave
            // 
            this.bttSave.Location = new System.Drawing.Point(223, 181);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(75, 50);
            this.bttSave.TabIndex = 339;
            this.bttSave.Text = "บันทึก";
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(304, 181);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 50);
            this.bttCancel.TabIndex = 338;
            this.bttCancel.Text = "ยกเลิก";
            // 
            // lookUpEditVatType
            // 
            this.lookUpEditVatType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpEditVatType.Location = new System.Drawing.Point(136, 139);
            this.lookUpEditVatType.Name = "lookUpEditVatType";
            this.lookUpEditVatType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditVatType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("vattype_label", " ")});
            this.lookUpEditVatType.Size = new System.Drawing.Size(243, 20);
            this.lookUpEditVatType.TabIndex = 350;
            // 
            // labelControlVatType
            // 
            this.labelControlVatType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlVatType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlVatType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlVatType.Location = new System.Drawing.Point(25, 139);
            this.labelControlVatType.Name = "labelControlVatType";
            this.labelControlVatType.Size = new System.Drawing.Size(105, 20);
            this.labelControlVatType.TabIndex = 349;
            this.labelControlVatType.Text = "การคิดภาษี :";
            // 
            // mruEditItemName
            // 
            this.mruEditItemName.Location = new System.Drawing.Point(136, 58);
            this.mruEditItemName.Name = "mruEditItemName";
            this.mruEditItemName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mruEditItemName.Size = new System.Drawing.Size(243, 20);
            this.mruEditItemName.TabIndex = 351;
            // 
            // PopUpCheckOutItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 265);
            this.Controls.Add(this.mruEditItemName);
            this.Controls.Add(this.lookUpEditVatType);
            this.Controls.Add(this.labelControlVatType);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEditItemUnit);
            this.Controls.Add(this.labelControl32);
            this.Controls.Add(this.textEditItemUnitPrice);
            this.Controls.Add(this.labelControlItemPrice);
            this.Controls.Add(this.labelControlItemName);
            this.Controls.Add(this.bttSave);
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.titleTabAddition);
            this.Name = "PopUpCheckOutItem";
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruEditItemName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton titleTabAddition;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditItemUnit;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraEditors.TextEdit textEditItemUnitPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemName;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditVatType;
        private DevExpress.XtraEditors.LabelControl labelControlVatType;
        private DevExpress.XtraEditors.MRUEdit mruEditItemName;
    }
}
