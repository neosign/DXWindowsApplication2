namespace DXWindowsApplication2.UserForms
{
    partial class ReportIncome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportIncome));
            this.gbIncome = new DevExpress.XtraEditors.GroupControl();
            this.gridControlReciept = new DevExpress.XtraGrid.GridControl();
            this.gridViewReciept = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRoom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcContractType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRoomRent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWaterUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWaterBaht = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcElectricUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcElectricBaht = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAddition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSummation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbIncomeBox = new System.Windows.Forms.GroupBox();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.lbDueDate = new DevExpress.XtraEditors.LabelControl();
            this.bttExport = new DevExpress.XtraEditors.SimpleButton();
            this.lbToDate = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.dateEditTodate = new DevExpress.XtraEditors.DateEdit();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEditContractType = new DevExpress.XtraEditors.LookUpEdit();
            this.lbRoomFrom = new DevExpress.XtraEditors.LabelControl();
            this.lbRoomTypeRent = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditRoomFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lbRoomTo = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditRoomTo = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).BeginInit();
            this.gbIncome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReciept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReciept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.gbIncomeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditContractType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbIncome
            // 
            this.gbIncome.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbIncome.AppearanceCaption.Options.UseFont = true;
            this.gbIncome.Controls.Add(this.gridControlReciept);
            this.gbIncome.Controls.Add(this.gbIncomeBox);
            this.gbIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIncome.Location = new System.Drawing.Point(7, 7);
            this.gbIncome.Name = "gbIncome";
            this.gbIncome.Padding = new System.Windows.Forms.Padding(10);
            this.gbIncome.Size = new System.Drawing.Size(677, 569);
            this.gbIncome.TabIndex = 1;
            this.gbIncome.Text = "Income";
            // 
            // gridControlReciept
            // 
            this.gridControlReciept.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridControlReciept.Location = new System.Drawing.Point(12, 357);
            this.gridControlReciept.MainView = this.gridViewReciept;
            this.gridControlReciept.Name = "gridControlReciept";
            this.gridControlReciept.Size = new System.Drawing.Size(653, 200);
            this.gridControlReciept.TabIndex = 277;
            this.gridControlReciept.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewReciept,
            this.gridView2});
            this.gridControlReciept.Visible = false;
            // 
            // gridViewReciept
            // 
            this.gridViewReciept.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRoom,
            this.gcContractType,
            this.gcReceiptNo,
            this.gcReceiptCreateDate,
            this.gcRoomRent,
            this.gcWaterUnit,
            this.gcWaterBaht,
            this.gcElectricUnit,
            this.gcElectricBaht,
            this.gcPhone,
            this.gcAddition,
            this.gcSummation});
            this.gridViewReciept.GridControl = this.gridControlReciept;
            this.gridViewReciept.Name = "gridViewReciept";
            this.gridViewReciept.OptionsView.ShowGroupPanel = false;
            // 
            // gcRoom
            // 
            this.gcRoom.Caption = "ห้อง";
            this.gcRoom.FieldName = "rec_trans_roomlabel";
            this.gcRoom.Name = "gcRoom";
            this.gcRoom.Visible = true;
            this.gcRoom.VisibleIndex = 0;
            // 
            // gcContractType
            // 
            this.gcContractType.Caption = "ประเภทการเช่า";
            this.gcContractType.FieldName = "contract_type_text";
            this.gcContractType.Name = "gcContractType";
            this.gcContractType.Visible = true;
            this.gcContractType.VisibleIndex = 1;
            this.gcContractType.Width = 78;
            // 
            // gcReceiptNo
            // 
            this.gcReceiptNo.Caption = "เลขที่ใบเสร็จ";
            this.gcReceiptNo.FieldName = "rec_trans_number";
            this.gcReceiptNo.Name = "gcReceiptNo";
            this.gcReceiptNo.Visible = true;
            this.gcReceiptNo.VisibleIndex = 2;
            this.gcReceiptNo.Width = 69;
            // 
            // gcReceiptCreateDate
            // 
            this.gcReceiptCreateDate.Caption = "วันที่ออกใบเสร็จ";
            this.gcReceiptCreateDate.FieldName = "rec_trans_datecreated";
            this.gcReceiptCreateDate.Name = "gcReceiptCreateDate";
            this.gcReceiptCreateDate.Visible = true;
            this.gcReceiptCreateDate.VisibleIndex = 3;
            this.gcReceiptCreateDate.Width = 84;
            // 
            // gcRoomRent
            // 
            this.gcRoomRent.Caption = "ค่าเช่า";
            this.gcRoomRent.FieldName = "rec_trans_roomprice";
            this.gcRoomRent.Name = "gcRoomRent";
            this.gcRoomRent.Visible = true;
            this.gcRoomRent.VisibleIndex = 4;
            this.gcRoomRent.Width = 53;
            // 
            // gcWaterUnit
            // 
            this.gcWaterUnit.Caption = "จำนวนหน่วยน้ำ";
            this.gcWaterUnit.FieldName = "rec_trans_wmeter_unit";
            this.gcWaterUnit.Name = "gcWaterUnit";
            this.gcWaterUnit.Visible = true;
            this.gcWaterUnit.VisibleIndex = 5;
            this.gcWaterUnit.Width = 92;
            // 
            // gcWaterBaht
            // 
            this.gcWaterBaht.Caption = "จำนวนเงินค่าน้ำ(บาท)";
            this.gcWaterBaht.FieldName = "rec_trans_wmeter_price";
            this.gcWaterBaht.Name = "gcWaterBaht";
            this.gcWaterBaht.Visible = true;
            this.gcWaterBaht.VisibleIndex = 6;
            this.gcWaterBaht.Width = 115;
            // 
            // gcElectricUnit
            // 
            this.gcElectricUnit.Caption = "จำนวนหน่วยไฟ";
            this.gcElectricUnit.FieldName = "rec_trans_emeter_unit";
            this.gcElectricUnit.Name = "gcElectricUnit";
            this.gcElectricUnit.Visible = true;
            this.gcElectricUnit.VisibleIndex = 7;
            this.gcElectricUnit.Width = 97;
            // 
            // gcElectricBaht
            // 
            this.gcElectricBaht.Caption = "จำนวนเงินค่าไฟ(บาท)";
            this.gcElectricBaht.FieldName = "rec_trans_emeter_price";
            this.gcElectricBaht.Name = "gcElectricBaht";
            this.gcElectricBaht.Visible = true;
            this.gcElectricBaht.VisibleIndex = 8;
            this.gcElectricBaht.Width = 108;
            // 
            // gcPhone
            // 
            this.gcPhone.Caption = "ค่าโทรศัพท์(บาท)";
            this.gcPhone.FieldName = "rec_trans_phone_price";
            this.gcPhone.Name = "gcPhone";
            this.gcPhone.Visible = true;
            this.gcPhone.VisibleIndex = 9;
            this.gcPhone.Width = 89;
            // 
            // gcAddition
            // 
            this.gcAddition.Caption = "ค่าใช้จ่ายเพิ่มเติม(บาท)";
            this.gcAddition.Name = "gcAddition";
            this.gcAddition.Visible = true;
            this.gcAddition.VisibleIndex = 10;
            this.gcAddition.Width = 114;
            // 
            // gcSummation
            // 
            this.gcSummation.Caption = "รวม(บาท)";
            this.gcSummation.DisplayFormat.FormatString = "n2";
            this.gcSummation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcSummation.FieldName = "rec_trans_sumprice_net";
            this.gcSummation.Name = "gcSummation";
            this.gcSummation.Visible = true;
            this.gcSummation.VisibleIndex = 11;
            this.gcSummation.Width = 54;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlReciept;
            this.gridView2.Name = "gridView2";
            // 
            // gbIncomeBox
            // 
            this.gbIncomeBox.Controls.Add(this.bttPrint);
            this.gbIncomeBox.Controls.Add(this.lbDueDate);
            this.gbIncomeBox.Controls.Add(this.bttExport);
            this.gbIncomeBox.Controls.Add(this.lbToDate);
            this.gbIncomeBox.Controls.Add(this.lookUpEditBuilding);
            this.gbIncomeBox.Controls.Add(this.dateEditTodate);
            this.gbIncomeBox.Controls.Add(this.lbBuilding);
            this.gbIncomeBox.Controls.Add(this.dateEditFromDate);
            this.gbIncomeBox.Controls.Add(this.lookUpEditContractType);
            this.gbIncomeBox.Controls.Add(this.lbRoomFrom);
            this.gbIncomeBox.Controls.Add(this.lbRoomTypeRent);
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomFrom);
            this.gbIncomeBox.Controls.Add(this.lbRoomTo);
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomTo);
            this.gbIncomeBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIncomeBox.Location = new System.Drawing.Point(12, 32);
            this.gbIncomeBox.Name = "gbIncomeBox";
            this.gbIncomeBox.Size = new System.Drawing.Size(653, 295);
            this.gbIncomeBox.TabIndex = 276;
            this.gbIncomeBox.TabStop = false;
            this.gbIncomeBox.Text = "Income";
            // 
            // bttPrint
            // 
            this.bttPrint.Image = global::DXWindowsApplication2.Properties.Resources.print;
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(321, 215);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(124, 55);
            this.bttPrint.TabIndex = 275;
            this.bttPrint.Text = "พิมพ์";
            this.bttPrint.Visible = false;
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // lbDueDate
            // 
            this.lbDueDate.Location = new System.Drawing.Point(48, 153);
            this.lbDueDate.Name = "lbDueDate";
            this.lbDueDate.Padding = new System.Windows.Forms.Padding(10);
            this.lbDueDate.Size = new System.Drawing.Size(94, 33);
            this.lbDueDate.TabIndex = 260;
            this.lbDueDate.Text = "วันที่ตัดรอบบิล :";
            // 
            // bttExport
            // 
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttExport.Location = new System.Drawing.Point(147, 215);
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(124, 55);
            this.bttExport.TabIndex = 274;
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // lbToDate
            // 
            this.lbToDate.Location = new System.Drawing.Point(277, 153);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Padding = new System.Windows.Forms.Padding(10);
            this.lbToDate.Size = new System.Drawing.Size(38, 33);
            this.lbToDate.TabIndex = 261;
            this.lbToDate.Text = "ถึง :";
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(147, 55);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditBuilding.TabIndex = 272;
            // 
            // dateEditTodate
            // 
            this.dateEditTodate.EditValue = null;
            this.dateEditTodate.Location = new System.Drawing.Point(321, 160);
            this.dateEditTodate.Name = "dateEditTodate";
            this.dateEditTodate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTodate.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditTodate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditTodate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTodate.Size = new System.Drawing.Size(124, 20);
            this.dateEditTodate.TabIndex = 263;
            // 
            // lbBuilding
            // 
            this.lbBuilding.Location = new System.Drawing.Point(85, 48);
            this.lbBuilding.Name = "lbBuilding";
            this.lbBuilding.Padding = new System.Windows.Forms.Padding(10);
            this.lbBuilding.Size = new System.Drawing.Size(56, 33);
            this.lbBuilding.TabIndex = 271;
            this.lbBuilding.Text = "อาคาร :";
            // 
            // dateEditFromDate
            // 
            this.dateEditFromDate.EditValue = null;
            this.dateEditFromDate.Location = new System.Drawing.Point(147, 160);
            this.dateEditFromDate.Name = "dateEditFromDate";
            this.dateEditFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFromDate.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFromDate.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFromDate.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFromDate.Size = new System.Drawing.Size(124, 20);
            this.dateEditFromDate.TabIndex = 262;
            // 
            // lookUpEditContractType
            // 
            this.lookUpEditContractType.Location = new System.Drawing.Point(147, 91);
            this.lookUpEditContractType.Name = "lookUpEditContractType";
            this.lookUpEditContractType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditContractType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("contracttype_label", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("contracttype_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditContractType.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditContractType.TabIndex = 270;
            // 
            // lbRoomFrom
            // 
            this.lbRoomFrom.Location = new System.Drawing.Point(95, 119);
            this.lbRoomFrom.Name = "lbRoomFrom";
            this.lbRoomFrom.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoomFrom.Size = new System.Drawing.Size(46, 33);
            this.lbRoomFrom.TabIndex = 265;
            this.lbRoomFrom.Text = "ห้อง :";
            // 
            // lbRoomTypeRent
            // 
            this.lbRoomTypeRent.Location = new System.Drawing.Point(45, 84);
            this.lbRoomTypeRent.Name = "lbRoomTypeRent";
            this.lbRoomTypeRent.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoomTypeRent.Size = new System.Drawing.Size(96, 33);
            this.lbRoomTypeRent.TabIndex = 269;
            this.lbRoomTypeRent.Text = "ประเภทการเช่า :";
            // 
            // lookUpEditRoomFrom
            // 
            this.lookUpEditRoomFrom.Enabled = false;
            this.lookUpEditRoomFrom.Location = new System.Drawing.Point(147, 126);
            this.lookUpEditRoomFrom.Name = "lookUpEditRoomFrom";
            this.lookUpEditRoomFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomFrom.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomFrom.TabIndex = 266;
            // 
            // lbRoomTo
            // 
            this.lbRoomTo.Location = new System.Drawing.Point(277, 119);
            this.lbRoomTo.Name = "lbRoomTo";
            this.lbRoomTo.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoomTo.Size = new System.Drawing.Size(38, 33);
            this.lbRoomTo.TabIndex = 268;
            this.lbRoomTo.Text = "ถึง :";
            // 
            // lookUpEditRoomTo
            // 
            this.lookUpEditRoomTo.Enabled = false;
            this.lookUpEditRoomTo.Location = new System.Drawing.Point(321, 126);
            this.lookUpEditRoomTo.Name = "lookUpEditRoomTo";
            this.lookUpEditRoomTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomTo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomTo.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomTo.TabIndex = 267;
            // 
            // ReportIncome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbIncome);
            this.Name = "ReportIncome";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(691, 583);
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).EndInit();
            this.gbIncome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReciept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReciept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.gbIncomeBox.ResumeLayout(false);
            this.gbIncomeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditContractType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbIncome;
        private System.Windows.Forms.GroupBox gbIncomeBox;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.LabelControl lbDueDate;
        private DevExpress.XtraEditors.SimpleButton bttExport;
        private DevExpress.XtraEditors.LabelControl lbToDate;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;
        private DevExpress.XtraEditors.DateEdit dateEditTodate;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.DateEdit dateEditFromDate;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditContractType;
        private DevExpress.XtraEditors.LabelControl lbRoomFrom;
        private DevExpress.XtraEditors.LabelControl lbRoomTypeRent;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomFrom;
        private DevExpress.XtraEditors.LabelControl lbRoomTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomTo;
        private DevExpress.XtraGrid.GridControl gridControlReciept;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewReciept;
        private DevExpress.XtraGrid.Columns.GridColumn gcRoom;
        private DevExpress.XtraGrid.Columns.GridColumn gcContractType;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptNo;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptCreateDate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gcRoomRent;
        private DevExpress.XtraGrid.Columns.GridColumn gcWaterUnit;
        private DevExpress.XtraGrid.Columns.GridColumn gcWaterBaht;
        private DevExpress.XtraGrid.Columns.GridColumn gcElectricUnit;
        private DevExpress.XtraGrid.Columns.GridColumn gcElectricBaht;
        private DevExpress.XtraGrid.Columns.GridColumn gcPhone;
        private DevExpress.XtraGrid.Columns.GridColumn gcAddition;
        private DevExpress.XtraGrid.Columns.GridColumn gcSummation;


    }
}
