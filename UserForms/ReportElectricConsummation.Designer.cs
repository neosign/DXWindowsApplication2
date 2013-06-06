namespace DXWindowsApplication2.UserForms
{
    partial class ReportElectricConsummation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportElectricConsummation));
            this.gbIncome = new DevExpress.XtraEditors.GroupControl();
            this.gbIncomeBox = new System.Windows.Forms.GroupBox();
            this.lookUpEditRoomTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditRoomFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.lbRoom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupMeterType = new DevExpress.XtraEditors.RadioGroup();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.dateEditUnitHour = new DevExpress.XtraEditors.DateEdit();
            this.panelControlUnitMeter = new DevExpress.XtraEditors.PanelControl();
            this.dateEditHour = new DevExpress.XtraEditors.DateEdit();
            this.dateEditMonthFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControlMonthTo = new DevExpress.XtraEditors.LabelControl();
            this.dateEditMonthTo = new DevExpress.XtraEditors.DateEdit();
            this.radioGroupMonthHour = new DevExpress.XtraEditors.RadioGroup();
            this.dateEditDay = new DevExpress.XtraEditors.DateEdit();
            this.radioGroupMeter = new DevExpress.XtraEditors.RadioGroup();
            this.gbSummation = new System.Windows.Forms.GroupBox();
            this.dateEditFromDate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTodate = new DevExpress.XtraEditors.DateEdit();
            this.lbDuedate = new DevExpress.XtraEditors.LabelControl();
            this.lbTodate = new DevExpress.XtraEditors.LabelControl();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bttExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).BeginInit();
            this.gbIncome.SuspendLayout();
            this.gbIncomeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMeterType.Properties)).BeginInit();
            this.gbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditUnitHour.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditUnitHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUnitMeter)).BeginInit();
            this.panelControlUnitMeter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditHour.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMonthHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMeter.Properties)).BeginInit();
            this.gbSummation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbIncome
            // 
            this.gbIncome.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbIncome.AppearanceCaption.Options.UseFont = true;
            this.gbIncome.Controls.Add(this.gbIncomeBox);
            this.gbIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIncome.Location = new System.Drawing.Point(7, 7);
            this.gbIncome.Name = "gbIncome";
            this.gbIncome.Padding = new System.Windows.Forms.Padding(10);
            this.gbIncome.Size = new System.Drawing.Size(677, 607);
            this.gbIncome.TabIndex = 1;
            this.gbIncome.Text = "Electricity Consummation";
            // 
            // gbIncomeBox
            // 
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomTo);
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomFrom);
            this.gbIncomeBox.Controls.Add(this.lookUpEditBuilding);
            this.gbIncomeBox.Controls.Add(this.lbBuilding);
            this.gbIncomeBox.Controls.Add(this.lbRoom);
            this.gbIncomeBox.Controls.Add(this.labelControl1);
            this.gbIncomeBox.Controls.Add(this.radioGroupMeterType);
            this.gbIncomeBox.Controls.Add(this.gbDetail);
            this.gbIncomeBox.Controls.Add(this.gbSummation);
            this.gbIncomeBox.Controls.Add(this.bttPrint);
            this.gbIncomeBox.Controls.Add(this.bttExport);
            this.gbIncomeBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIncomeBox.Location = new System.Drawing.Point(12, 32);
            this.gbIncomeBox.Name = "gbIncomeBox";
            this.gbIncomeBox.Size = new System.Drawing.Size(653, 495);
            this.gbIncomeBox.TabIndex = 276;
            this.gbIncomeBox.TabStop = false;
            this.gbIncomeBox.Text = "Electricity Consummation";
            // 
            // lookUpEditRoomTo
            // 
            this.lookUpEditRoomTo.Enabled = false;
            this.lookUpEditRoomTo.Location = new System.Drawing.Point(347, 78);
            this.lookUpEditRoomTo.Name = "lookUpEditRoomTo";
            this.lookUpEditRoomTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomTo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomTo.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomTo.TabIndex = 285;
            // 
            // lookUpEditRoomFrom
            // 
            this.lookUpEditRoomFrom.Enabled = false;
            this.lookUpEditRoomFrom.Location = new System.Drawing.Point(173, 78);
            this.lookUpEditRoomFrom.Name = "lookUpEditRoomFrom";
            this.lookUpEditRoomFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomFrom.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomFrom.TabIndex = 284;
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(173, 37);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "building_id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", " ")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditBuilding.TabIndex = 283;
            // 
            // lbBuilding
            // 
            this.lbBuilding.Location = new System.Drawing.Point(111, 30);
            this.lbBuilding.Name = "lbBuilding";
            this.lbBuilding.Padding = new System.Windows.Forms.Padding(10);
            this.lbBuilding.Size = new System.Drawing.Size(56, 33);
            this.lbBuilding.TabIndex = 282;
            this.lbBuilding.Text = "อาคาร :";
            // 
            // lbRoom
            // 
            this.lbRoom.Location = new System.Drawing.Point(121, 71);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoom.Size = new System.Drawing.Size(46, 33);
            this.lbRoom.TabIndex = 280;
            this.lbRoom.Text = "ห้อง :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(303, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(38, 33);
            this.labelControl1.TabIndex = 281;
            this.labelControl1.Text = "ถึง :";
            // 
            // radioGroupMeterType
            // 
            this.radioGroupMeterType.EditValue = ((short)(0));
            this.radioGroupMeterType.Location = new System.Drawing.Point(7, 82);
            this.radioGroupMeterType.Name = "radioGroupMeterType";
            this.radioGroupMeterType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupMeterType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupMeterType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupMeterType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "รายเดือน"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "รายวัน")});
            this.radioGroupMeterType.Size = new System.Drawing.Size(24, 165);
            this.radioGroupMeterType.TabIndex = 264;
            // 
            // gbDetail
            // 
            this.gbDetail.Controls.Add(this.dateEditUnitHour);
            this.gbDetail.Controls.Add(this.panelControlUnitMeter);
            this.gbDetail.Controls.Add(this.radioGroupMeter);
            this.gbDetail.Enabled = false;
            this.gbDetail.Location = new System.Drawing.Point(32, 193);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(615, 206);
            this.gbDetail.TabIndex = 277;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "Detail";
            // 
            // dateEditUnitHour
            // 
            this.dateEditUnitHour.EditValue = null;
            this.dateEditUnitHour.Location = new System.Drawing.Point(155, 34);
            this.dateEditUnitHour.Name = "dateEditUnitHour";
            this.dateEditUnitHour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditUnitHour.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditUnitHour.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditUnitHour.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditUnitHour.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditUnitHour.Properties.Mask.EditMask = "dd MMMM yyyy";
            this.dateEditUnitHour.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditUnitHour.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditUnitHour.Size = new System.Drawing.Size(124, 20);
            this.dateEditUnitHour.TabIndex = 277;
            // 
            // panelControlUnitMeter
            // 
            this.panelControlUnitMeter.Controls.Add(this.dateEditHour);
            this.panelControlUnitMeter.Controls.Add(this.dateEditMonthFrom);
            this.panelControlUnitMeter.Controls.Add(this.labelControlMonthTo);
            this.panelControlUnitMeter.Controls.Add(this.dateEditMonthTo);
            this.panelControlUnitMeter.Controls.Add(this.radioGroupMonthHour);
            this.panelControlUnitMeter.Controls.Add(this.dateEditDay);
            this.panelControlUnitMeter.Enabled = false;
            this.panelControlUnitMeter.Location = new System.Drawing.Point(79, 92);
            this.panelControlUnitMeter.Name = "panelControlUnitMeter";
            this.panelControlUnitMeter.Size = new System.Drawing.Size(386, 101);
            this.panelControlUnitMeter.TabIndex = 276;
            // 
            // dateEditHour
            // 
            this.dateEditHour.EditValue = null;
            this.dateEditHour.Location = new System.Drawing.Point(76, 5);
            this.dateEditHour.Name = "dateEditHour";
            this.dateEditHour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditHour.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditHour.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditHour.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditHour.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditHour.Properties.Mask.EditMask = "dd MMMM yyyy";
            this.dateEditHour.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditHour.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditHour.Size = new System.Drawing.Size(124, 20);
            this.dateEditHour.TabIndex = 268;
            // 
            // dateEditMonthFrom
            // 
            this.dateEditMonthFrom.EditValue = null;
            this.dateEditMonthFrom.Location = new System.Drawing.Point(76, 69);
            this.dateEditMonthFrom.Name = "dateEditMonthFrom";
            this.dateEditMonthFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMonthFrom.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditMonthFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditMonthFrom.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditMonthFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditMonthFrom.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditMonthFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditMonthFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditMonthFrom.Size = new System.Drawing.Size(124, 20);
            this.dateEditMonthFrom.TabIndex = 274;
            // 
            // labelControlMonthTo
            // 
            this.labelControlMonthTo.Location = new System.Drawing.Point(206, 62);
            this.labelControlMonthTo.Name = "labelControlMonthTo";
            this.labelControlMonthTo.Padding = new System.Windows.Forms.Padding(10);
            this.labelControlMonthTo.Size = new System.Drawing.Size(38, 33);
            this.labelControlMonthTo.TabIndex = 261;
            this.labelControlMonthTo.Text = "ถึง :";
            // 
            // dateEditMonthTo
            // 
            this.dateEditMonthTo.EditValue = null;
            this.dateEditMonthTo.Location = new System.Drawing.Point(250, 69);
            this.dateEditMonthTo.Name = "dateEditMonthTo";
            this.dateEditMonthTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMonthTo.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditMonthTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditMonthTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditMonthTo.Size = new System.Drawing.Size(124, 20);
            this.dateEditMonthTo.TabIndex = 275;
            // 
            // radioGroupMonthHour
            // 
            this.radioGroupMonthHour.EditValue = ((short)(0));
            this.radioGroupMonthHour.Location = new System.Drawing.Point(4, -2);
            this.radioGroupMonthHour.Name = "radioGroupMonthHour";
            this.radioGroupMonthHour.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupMonthHour.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupMonthHour.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupMonthHour.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "ชั่วโมง"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "วัน"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "เดือน")});
            this.radioGroupMonthHour.Size = new System.Drawing.Size(66, 99);
            this.radioGroupMonthHour.TabIndex = 266;
            // 
            // dateEditDay
            // 
            this.dateEditDay.EditValue = null;
            this.dateEditDay.Location = new System.Drawing.Point(76, 37);
            this.dateEditDay.Name = "dateEditDay";
            this.dateEditDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDay.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditDay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditDay.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditDay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditDay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditDay.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditDay.Size = new System.Drawing.Size(124, 20);
            this.dateEditDay.TabIndex = 272;
            // 
            // radioGroupMeter
            // 
            this.radioGroupMeter.EditValue = ((short)(0));
            this.radioGroupMeter.Location = new System.Drawing.Point(42, 23);
            this.radioGroupMeter.Name = "radioGroupMeter";
            this.radioGroupMeter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupMeter.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupMeter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupMeter.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "ค่ามิเตอร์"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "หน่วยมิเตอร์")});
            this.radioGroupMeter.Size = new System.Drawing.Size(84, 73);
            this.radioGroupMeter.TabIndex = 265;
            // 
            // gbSummation
            // 
            this.gbSummation.Controls.Add(this.dateEditFromDate);
            this.gbSummation.Controls.Add(this.dateEditTodate);
            this.gbSummation.Controls.Add(this.lbDuedate);
            this.gbSummation.Controls.Add(this.lbTodate);
            this.gbSummation.Location = new System.Drawing.Point(32, 116);
            this.gbSummation.Name = "gbSummation";
            this.gbSummation.Size = new System.Drawing.Size(615, 71);
            this.gbSummation.TabIndex = 276;
            this.gbSummation.TabStop = false;
            this.gbSummation.Text = "Total/Summary Data";
            // 
            // dateEditFromDate
            // 
            this.dateEditFromDate.EditValue = null;
            this.dateEditFromDate.Enabled = false;
            this.dateEditFromDate.Location = new System.Drawing.Point(141, 29);
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
            // dateEditTodate
            // 
            this.dateEditTodate.EditValue = null;
            this.dateEditTodate.Enabled = false;
            this.dateEditTodate.Location = new System.Drawing.Point(315, 29);
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
            // lbDuedate
            // 
            this.lbDuedate.Location = new System.Drawing.Point(42, 22);
            this.lbDuedate.Name = "lbDuedate";
            this.lbDuedate.Padding = new System.Windows.Forms.Padding(10);
            this.lbDuedate.Size = new System.Drawing.Size(94, 33);
            this.lbDuedate.TabIndex = 260;
            this.lbDuedate.Text = "วันที่ตัดรอบบิล :";
            // 
            // lbTodate
            // 
            this.lbTodate.Location = new System.Drawing.Point(271, 22);
            this.lbTodate.Name = "lbTodate";
            this.lbTodate.Padding = new System.Windows.Forms.Padding(10);
            this.lbTodate.Size = new System.Drawing.Size(38, 33);
            this.lbTodate.TabIndex = 261;
            this.lbTodate.Text = "ถึง :";
            // 
            // bttPrint
            // 
            this.bttPrint.Image = global::DXWindowsApplication2.Properties.Resources.print;
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(32, 421);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(124, 55);
            this.bttPrint.TabIndex = 275;
            this.bttPrint.Text = "พิมพ์";
            this.bttPrint.Visible = false;
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // bttExport
            // 
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttExport.Location = new System.Drawing.Point(256, 421);
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(124, 55);
            this.bttExport.TabIndex = 274;
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // ReportElectricConsummation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbIncome);
            this.Name = "ReportElectricConsummation";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(691, 621);
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).EndInit();
            this.gbIncome.ResumeLayout(false);
            this.gbIncomeBox.ResumeLayout(false);
            this.gbIncomeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMeterType.Properties)).EndInit();
            this.gbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditUnitHour.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditUnitHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUnitMeter)).EndInit();
            this.panelControlUnitMeter.ResumeLayout(false);
            this.panelControlUnitMeter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditHour.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonthTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMonthHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMeter.Properties)).EndInit();
            this.gbSummation.ResumeLayout(false);
            this.gbSummation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbIncome;
        private System.Windows.Forms.GroupBox gbIncomeBox;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.LabelControl lbDuedate;
        private DevExpress.XtraEditors.SimpleButton bttExport;
        private DevExpress.XtraEditors.LabelControl lbTodate;
        private DevExpress.XtraEditors.DateEdit dateEditTodate;
        private DevExpress.XtraEditors.DateEdit dateEditFromDate;
        private System.Windows.Forms.GroupBox gbSummation;
        private System.Windows.Forms.GroupBox gbDetail;
        private DevExpress.XtraEditors.LabelControl labelControlMonthTo;
        private DevExpress.XtraEditors.RadioGroup radioGroupMeterType;
        private DevExpress.XtraEditors.RadioGroup radioGroupMonthHour;
        private DevExpress.XtraEditors.RadioGroup radioGroupMeter;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomFrom;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.LabelControl lbRoom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditHour;
        private DevExpress.XtraEditors.DateEdit dateEditMonthFrom;
        private DevExpress.XtraEditors.DateEdit dateEditMonthTo;
        private DevExpress.XtraEditors.DateEdit dateEditDay;
        private DevExpress.XtraEditors.PanelControl panelControlUnitMeter;
        private DevExpress.XtraEditors.DateEdit dateEditUnitHour;


    }
}
