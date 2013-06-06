namespace DXWindowsApplication2.UserForms
{
    partial class ReportPhoneConsummation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPhoneConsummation));
            this.gbPhone = new DevExpress.XtraEditors.GroupControl();
            this.gbPhoneBox = new System.Windows.Forms.GroupBox();
            this.lookUpEditRoomTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditRoomFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControldetail = new DevExpress.XtraEditors.LabelControl();
            this.gbSummation = new System.Windows.Forms.GroupBox();
            this.radioGroupSumType = new DevExpress.XtraEditors.RadioGroup();
            this.dateEditFromDate = new DevExpress.XtraEditors.DateEdit();
            this.radioGroupPhoneType = new DevExpress.XtraEditors.RadioGroup();
            this.dateEditTodate = new DevExpress.XtraEditors.DateEdit();
            this.lbDuedate = new DevExpress.XtraEditors.LabelControl();
            this.lbTodate = new DevExpress.XtraEditors.LabelControl();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bttExport = new DevExpress.XtraEditors.SimpleButton();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.lbRoom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gbPhone)).BeginInit();
            this.gbPhone.SuspendLayout();
            this.gbPhoneBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            this.gbSummation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupSumType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupPhoneType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPhone
            // 
            this.gbPhone.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbPhone.AppearanceCaption.Options.UseFont = true;
            this.gbPhone.Controls.Add(this.gbPhoneBox);
            this.gbPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPhone.Location = new System.Drawing.Point(7, 7);
            this.gbPhone.Name = "gbPhone";
            this.gbPhone.Padding = new System.Windows.Forms.Padding(10);
            this.gbPhone.Size = new System.Drawing.Size(677, 607);
            this.gbPhone.TabIndex = 1;
            this.gbPhone.Text = "Phone Consummation";
            // 
            // gbPhoneBox
            // 
            this.gbPhoneBox.Controls.Add(this.lookUpEditRoomTo);
            this.gbPhoneBox.Controls.Add(this.lookUpEditRoomFrom);
            this.gbPhoneBox.Controls.Add(this.lookUpEditBuilding);
            this.gbPhoneBox.Controls.Add(this.labelControldetail);
            this.gbPhoneBox.Controls.Add(this.gbSummation);
            this.gbPhoneBox.Controls.Add(this.dateEditFromDate);
            this.gbPhoneBox.Controls.Add(this.radioGroupPhoneType);
            this.gbPhoneBox.Controls.Add(this.dateEditTodate);
            this.gbPhoneBox.Controls.Add(this.lbDuedate);
            this.gbPhoneBox.Controls.Add(this.lbTodate);
            this.gbPhoneBox.Controls.Add(this.bttPrint);
            this.gbPhoneBox.Controls.Add(this.bttExport);
            this.gbPhoneBox.Controls.Add(this.lbBuilding);
            this.gbPhoneBox.Controls.Add(this.lbRoom);
            this.gbPhoneBox.Controls.Add(this.labelControl1);
            this.gbPhoneBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPhoneBox.Location = new System.Drawing.Point(12, 32);
            this.gbPhoneBox.Name = "gbPhoneBox";
            this.gbPhoneBox.Size = new System.Drawing.Size(653, 424);
            this.gbPhoneBox.TabIndex = 276;
            this.gbPhoneBox.TabStop = false;
            this.gbPhoneBox.Text = "Phone Consummation";
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
            this.lookUpEditRoomTo.TabIndex = 288;
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
            this.lookUpEditRoomFrom.TabIndex = 287;
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(173, 36);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "building_id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", " ")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditBuilding.TabIndex = 286;
            // 
            // labelControldetail
            // 
            this.labelControldetail.Location = new System.Drawing.Point(51, 268);
            this.labelControldetail.Name = "labelControldetail";
            this.labelControldetail.Padding = new System.Windows.Forms.Padding(10);
            this.labelControldetail.Size = new System.Drawing.Size(72, 33);
            this.labelControldetail.TabIndex = 278;
            this.labelControldetail.Text = "รายละเอียด";
            // 
            // gbSummation
            // 
            this.gbSummation.Controls.Add(this.radioGroupSumType);
            this.gbSummation.Location = new System.Drawing.Point(51, 179);
            this.gbSummation.Name = "gbSummation";
            this.gbSummation.Size = new System.Drawing.Size(583, 83);
            this.gbSummation.TabIndex = 277;
            this.gbSummation.TabStop = false;
            this.gbSummation.Text = "ยอดรวม";
            // 
            // radioGroupSumType
            // 
            this.radioGroupSumType.EditValue = 1;
            this.radioGroupSumType.Location = new System.Drawing.Point(60, 11);
            this.radioGroupSumType.Name = "radioGroupSumType";
            this.radioGroupSumType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupSumType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupSumType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupSumType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "การโทร"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "ค่าโทร")});
            this.radioGroupSumType.Size = new System.Drawing.Size(191, 66);
            this.radioGroupSumType.TabIndex = 279;
            // 
            // dateEditFromDate
            // 
            this.dateEditFromDate.EditValue = null;
            this.dateEditFromDate.Location = new System.Drawing.Point(173, 123);
            this.dateEditFromDate.Name = "dateEditFromDate";
            this.dateEditFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFromDate.Properties.DisplayFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFromDate.Properties.EditFormat.FormatString = "{0:dd MMMM yyyy}";
            this.dateEditFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFromDate.Properties.Mask.EditMask = "dd MMMM yyyy";
            this.dateEditFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFromDate.Size = new System.Drawing.Size(124, 20);
            this.dateEditFromDate.TabIndex = 262;
            // 
            // radioGroupPhoneType
            // 
            this.radioGroupPhoneType.EditValue = ((short)(0));
            this.radioGroupPhoneType.Location = new System.Drawing.Point(21, 136);
            this.radioGroupPhoneType.Name = "radioGroupPhoneType";
            this.radioGroupPhoneType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupPhoneType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupPhoneType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupPhoneType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "รายเดือน"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "รายวัน")});
            this.radioGroupPhoneType.Size = new System.Drawing.Size(24, 201);
            this.radioGroupPhoneType.TabIndex = 264;
            this.radioGroupPhoneType.SelectedIndexChanged += new System.EventHandler(this.radioGroupPhoneType_SelectedIndexChanged);
            // 
            // dateEditTodate
            // 
            this.dateEditTodate.EditValue = null;
            this.dateEditTodate.Location = new System.Drawing.Point(347, 123);
            this.dateEditTodate.Name = "dateEditTodate";
            this.dateEditTodate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTodate.Properties.Mask.EditMask = "dd MMMM yyyy";
            this.dateEditTodate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditTodate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTodate.Size = new System.Drawing.Size(124, 20);
            this.dateEditTodate.TabIndex = 263;
            // 
            // lbDuedate
            // 
            this.lbDuedate.Location = new System.Drawing.Point(120, 116);
            this.lbDuedate.Name = "lbDuedate";
            this.lbDuedate.Padding = new System.Windows.Forms.Padding(10);
            this.lbDuedate.Size = new System.Drawing.Size(47, 33);
            this.lbDuedate.TabIndex = 260;
            this.lbDuedate.Text = "วันที่ :";
            // 
            // lbTodate
            // 
            this.lbTodate.Location = new System.Drawing.Point(303, 116);
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
            this.bttPrint.Location = new System.Drawing.Point(51, 338);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(124, 55);
            this.bttPrint.TabIndex = 275;
            this.bttPrint.Text = "พิมพ์";
            this.bttPrint.Visible = false;
            // 
            // bttExport
            // 
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttExport.Location = new System.Drawing.Point(268, 338);
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(124, 55);
            this.bttExport.TabIndex = 274;
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // lbBuilding
            // 
            this.lbBuilding.Location = new System.Drawing.Point(111, 29);
            this.lbBuilding.Name = "lbBuilding";
            this.lbBuilding.Padding = new System.Windows.Forms.Padding(10);
            this.lbBuilding.Size = new System.Drawing.Size(56, 33);
            this.lbBuilding.TabIndex = 271;
            this.lbBuilding.Text = "อาคาร :";
            // 
            // lbRoom
            // 
            this.lbRoom.Location = new System.Drawing.Point(121, 73);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoom.Size = new System.Drawing.Size(46, 33);
            this.lbRoom.TabIndex = 265;
            this.lbRoom.Text = "ห้อง :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(303, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(38, 33);
            this.labelControl1.TabIndex = 268;
            this.labelControl1.Text = "ถึง :";
            // 
            // ReportPhoneConsummation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbPhone);
            this.Name = "ReportPhoneConsummation";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(691, 621);
            ((System.ComponentModel.ISupportInitialize)(this.gbPhone)).EndInit();
            this.gbPhone.ResumeLayout(false);
            this.gbPhoneBox.ResumeLayout(false);
            this.gbPhoneBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            this.gbSummation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupSumType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupPhoneType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTodate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbPhone;
        private System.Windows.Forms.GroupBox gbPhoneBox;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.LabelControl lbDuedate;
        private DevExpress.XtraEditors.SimpleButton bttExport;
        private DevExpress.XtraEditors.LabelControl lbTodate;
        private DevExpress.XtraEditors.DateEdit dateEditTodate;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.DateEdit dateEditFromDate;
        private DevExpress.XtraEditors.LabelControl lbRoom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroupPhoneType;
        private DevExpress.XtraEditors.LabelControl labelControldetail;
        private System.Windows.Forms.GroupBox gbSummation;
        private DevExpress.XtraEditors.RadioGroup radioGroupSumType;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomFrom;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;


    }
}
