namespace DXWindowsApplication2.UserForms
{
    partial class InvoiceAddItem
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditItemUnit = new DevExpress.XtraEditors.TextEdit();
            this.textEditItemUnitPrice = new DevExpress.XtraEditors.TextEdit();
            this.labelControlItemPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelControlItemName = new DevExpress.XtraEditors.LabelControl();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.AddItemHeader = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditVatType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControlVatType = new DevExpress.XtraEditors.LabelControl();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.mruEditItemName = new DevExpress.XtraEditors.MRUEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruEditItemName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(369, 103);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 325;
            this.labelControl4.Text = "บาท";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(52, 77);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 323;
            this.labelControl1.Text = "จำนวนหน่วย :";
            // 
            // textEditItemUnit
            // 
            this.textEditItemUnit.EditValue = 0;
            this.textEditItemUnit.Location = new System.Drawing.Point(120, 74);
            this.textEditItemUnit.Name = "textEditItemUnit";
            this.textEditItemUnit.Properties.Mask.BeepOnError = true;
            this.textEditItemUnit.Properties.Mask.EditMask = "n0";
            this.textEditItemUnit.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnit.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnit.TabIndex = 322;
            // 
            // textEditItemUnitPrice
            // 
            this.textEditItemUnitPrice.EditValue = 0;
            this.textEditItemUnitPrice.Location = new System.Drawing.Point(120, 100);
            this.textEditItemUnitPrice.Name = "textEditItemUnitPrice";
            this.textEditItemUnitPrice.Properties.Mask.BeepOnError = true;
            this.textEditItemUnitPrice.Properties.Mask.EditMask = "n2";
            this.textEditItemUnitPrice.Properties.Mask.IgnoreMaskBlank = false;
            this.textEditItemUnitPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditItemUnitPrice.Size = new System.Drawing.Size(243, 20);
            this.textEditItemUnitPrice.TabIndex = 319;
            // 
            // labelControlItemPrice
            // 
            this.labelControlItemPrice.Location = new System.Drawing.Point(22, 103);
            this.labelControlItemPrice.Name = "labelControlItemPrice";
            this.labelControlItemPrice.Size = new System.Drawing.Size(92, 13);
            this.labelControlItemPrice.TabIndex = 318;
            this.labelControlItemPrice.Text = "จำนวนเงินต่อหน่วย :";
            // 
            // labelControlItemName
            // 
            this.labelControlItemName.Location = new System.Drawing.Point(33, 51);
            this.labelControlItemName.Name = "labelControlItemName";
            this.labelControlItemName.Size = new System.Drawing.Size(81, 13);
            this.labelControlItemName.TabIndex = 317;
            this.labelControlItemName.Text = "รายการค่าใช้จ่าย :";
            // 
            // bttCancel
            // 
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(288, 166);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 53);
            this.bttCancel.TabIndex = 315;
            this.bttCancel.Text = "ยกเลิก";
            // 
            // AddItemHeader
            // 
            this.AddItemHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.AddItemHeader.Appearance.Options.UseFont = true;
            this.AddItemHeader.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.AddItemHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.AddItemHeader.Location = new System.Drawing.Point(0, 0);
            this.AddItemHeader.Name = "AddItemHeader";
            this.AddItemHeader.Size = new System.Drawing.Size(427, 23);
            this.AddItemHeader.TabIndex = 314;
            this.AddItemHeader.Text = "เพิ่มข้อมูลรายการค่าใช้จ่าย";
            // 
            // lookUpEditVatType
            // 
            this.lookUpEditVatType.Location = new System.Drawing.Point(120, 123);
            this.lookUpEditVatType.Name = "lookUpEditVatType";
            this.lookUpEditVatType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditVatType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("vattype_label", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("vattype_id", "Name12", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditVatType.Size = new System.Drawing.Size(243, 20);
            this.lookUpEditVatType.TabIndex = 326;
            // 
            // labelControlVatType
            // 
            this.labelControlVatType.Location = new System.Drawing.Point(56, 126);
            this.labelControlVatType.Name = "labelControlVatType";
            this.labelControlVatType.Size = new System.Drawing.Size(58, 13);
            this.labelControlVatType.TabIndex = 318;
            this.labelControlVatType.Text = "การคิดภาษี :";
            // 
            // bttSave
            // 
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.save;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(207, 166);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(75, 53);
            this.bttSave.TabIndex = 316;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // mruEditItemName
            // 
            this.mruEditItemName.Location = new System.Drawing.Point(120, 48);
            this.mruEditItemName.Name = "mruEditItemName";
            this.mruEditItemName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mruEditItemName.Size = new System.Drawing.Size(243, 20);
            this.mruEditItemName.TabIndex = 327;
            // 
            // InvoiceAddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mruEditItemName);
            this.Controls.Add(this.lookUpEditVatType);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEditItemUnit);
            this.Controls.Add(this.textEditItemUnitPrice);
            this.Controls.Add(this.labelControlVatType);
            this.Controls.Add(this.labelControlItemPrice);
            this.Controls.Add(this.labelControlItemName);
            this.Controls.Add(this.bttSave);
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.AddItemHeader);
            this.Name = "InvoiceAddItem";
            this.Size = new System.Drawing.Size(427, 250);
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemUnitPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditVatType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruEditItemName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditItemUnit;
        private DevExpress.XtraEditors.TextEdit textEditItemUnitPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemPrice;
        private DevExpress.XtraEditors.LabelControl labelControlItemName;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.SimpleButton AddItemHeader;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditVatType;
        private DevExpress.XtraEditors.LabelControl labelControlVatType;
        private DevExpress.XtraEditors.MRUEdit mruEditItemName;
    }
}
