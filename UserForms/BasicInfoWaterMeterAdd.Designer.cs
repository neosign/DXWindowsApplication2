namespace DXWindowsApplication2.UserForms
{
    partial class BasicInfoWaterMeterAdd
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
            this.labelElectricMeterDetail = new DevExpress.XtraEditors.LabelControl();
            this.memometer_detail = new DevExpress.XtraEditors.MemoEdit();
            this.gridLookUpEditRoom = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.room_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.room_label = new DevExpress.XtraGrid.Columns.GridColumn();
            this.room_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditFloor = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.txtmeter_label = new DevExpress.XtraEditors.TextEdit();
            this.labelElectricMeterModel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtmeter_model = new DevExpress.XtraEditors.TextEdit();
            this.labelElectricMeterSerial = new DevExpress.XtraEditors.LabelControl();
            this.labelElectricMeterLabel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtmeter_serial = new DevExpress.XtraEditors.TextEdit();
            this.labelElectricRoomNo = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelElectricFloor = new DevExpress.XtraEditors.LabelControl();
            this.labelElectricBuildingLabel = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.memometer_detail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditRoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFloor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_label.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_model.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_serial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelElectricMeterDetail
            // 
            this.labelElectricMeterDetail.Location = new System.Drawing.Point(10, 233);
            this.labelElectricMeterDetail.Name = "labelElectricMeterDetail";
            this.labelElectricMeterDetail.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricMeterDetail.Size = new System.Drawing.Size(115, 33);
            this.labelElectricMeterDetail.TabIndex = 312;
            this.labelElectricMeterDetail.Text = "รายละเอียดเพิ่มเติม :";
            // 
            // memometer_detail
            // 
            this.memometer_detail.Location = new System.Drawing.Point(131, 240);
            this.memometer_detail.Name = "memometer_detail";
            this.memometer_detail.Size = new System.Drawing.Size(261, 96);
            this.memometer_detail.TabIndex = 311;
            // 
            // gridLookUpEditRoom
            // 
            this.gridLookUpEditRoom.Location = new System.Drawing.Point(131, 106);
            this.gridLookUpEditRoom.Name = "gridLookUpEditRoom";
            this.gridLookUpEditRoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEditRoom.Properties.View = this.gridLookUpEdit2View;
            this.gridLookUpEditRoom.Size = new System.Drawing.Size(188, 20);
            this.gridLookUpEditRoom.TabIndex = 310;
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.room_id,
            this.room_label,
            this.room_no});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // room_id
            // 
            this.room_id.Caption = "room_id";
            this.room_id.FieldName = "room_id";
            this.room_id.Name = "room_id";
            // 
            // room_label
            // 
            this.room_label.Caption = "ชื่อห้อง";
            this.room_label.FieldName = "room_label";
            this.room_label.Name = "room_label";
            this.room_label.OptionsColumn.AllowEdit = false;
            this.room_label.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.room_label.Visible = true;
            this.room_label.VisibleIndex = 0;
            // 
            // room_no
            // 
            this.room_no.Caption = "หมายเลขห้อง";
            this.room_no.FieldName = "coderef";
            this.room_no.Name = "room_no";
            this.room_no.OptionsColumn.AllowEdit = false;
            this.room_no.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.room_no.Visible = true;
            this.room_no.VisibleIndex = 1;
            // 
            // lookUpEditFloor
            // 
            this.lookUpEditFloor.Location = new System.Drawing.Point(131, 73);
            this.lookUpEditFloor.Name = "lookUpEditFloor";
            this.lookUpEditFloor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditFloor.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("floor_label", "floor_label")});
            this.lookUpEditFloor.Size = new System.Drawing.Size(100, 20);
            this.lookUpEditFloor.TabIndex = 309;
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(131, 40);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_label", "อาคาร")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(100, 20);
            this.lookUpEditBuilding.TabIndex = 308;
            // 
            // txtmeter_label
            // 
            this.txtmeter_label.EditValue = "";
            this.txtmeter_label.Location = new System.Drawing.Point(131, 139);
            this.txtmeter_label.Name = "txtmeter_label";
            this.txtmeter_label.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtmeter_label.Properties.Appearance.Options.UseBackColor = true;
            this.txtmeter_label.Properties.DisplayFormat.FormatString = "#.##";
            this.txtmeter_label.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_label.Properties.EditFormat.FormatString = "#.##";
            this.txtmeter_label.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_label.Properties.Mask.EditMask = "([a-zA-Z0-9]*)";
            this.txtmeter_label.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtmeter_label.ShowToolTips = false;
            this.txtmeter_label.Size = new System.Drawing.Size(100, 20);
            this.txtmeter_label.TabIndex = 304;
            this.txtmeter_label.Tag = "";
            // 
            // labelElectricMeterModel
            // 
            this.labelElectricMeterModel.Location = new System.Drawing.Point(86, 198);
            this.labelElectricMeterModel.Name = "labelElectricMeterModel";
            this.labelElectricMeterModel.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricMeterModel.Size = new System.Drawing.Size(39, 33);
            this.labelElectricMeterModel.TabIndex = 300;
            this.labelElectricMeterModel.Text = "รุ่น :";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(237, 208);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(95, 13);
            this.labelControl12.TabIndex = 299;
            this.labelControl12.Text = "( ex : number only )";
            // 
            // txtmeter_model
            // 
            this.txtmeter_model.EditValue = "";
            this.txtmeter_model.Location = new System.Drawing.Point(131, 205);
            this.txtmeter_model.Name = "txtmeter_model";
            this.txtmeter_model.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtmeter_model.Properties.Appearance.Options.UseBackColor = true;
            this.txtmeter_model.Properties.DisplayFormat.FormatString = "#.##";
            this.txtmeter_model.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_model.Properties.EditFormat.FormatString = "#.##";
            this.txtmeter_model.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_model.Properties.Mask.EditMask = "([a-zA-Z0-9]*)";
            this.txtmeter_model.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtmeter_model.ShowToolTips = false;
            this.txtmeter_model.Size = new System.Drawing.Size(100, 20);
            this.txtmeter_model.TabIndex = 298;
            this.txtmeter_model.Tag = "";
            // 
            // labelElectricMeterSerial
            // 
            this.labelElectricMeterSerial.Location = new System.Drawing.Point(25, 165);
            this.labelElectricMeterSerial.Name = "labelElectricMeterSerial";
            this.labelElectricMeterSerial.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricMeterSerial.Size = new System.Drawing.Size(100, 33);
            this.labelElectricMeterSerial.TabIndex = 291;
            this.labelElectricMeterSerial.Text = "หมายเลขมิเตอร์ :";
            // 
            // labelElectricMeterLabel
            // 
            this.labelElectricMeterLabel.Location = new System.Drawing.Point(49, 132);
            this.labelElectricMeterLabel.Name = "labelElectricMeterLabel";
            this.labelElectricMeterLabel.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricMeterLabel.Size = new System.Drawing.Size(76, 33);
            this.labelElectricMeterLabel.TabIndex = 290;
            this.labelElectricMeterLabel.Text = "รหัสมิเตอร์ :";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(237, 175);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(95, 13);
            this.labelControl7.TabIndex = 289;
            this.labelControl7.Text = "( ex : number only )";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(237, 142);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(95, 13);
            this.labelControl8.TabIndex = 288;
            this.labelControl8.Text = "( ex : number only )";
            // 
            // txtmeter_serial
            // 
            this.txtmeter_serial.EditValue = "";
            this.txtmeter_serial.Location = new System.Drawing.Point(131, 172);
            this.txtmeter_serial.Name = "txtmeter_serial";
            this.txtmeter_serial.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtmeter_serial.Properties.Appearance.Options.UseBackColor = true;
            this.txtmeter_serial.Properties.DisplayFormat.FormatString = "#.##";
            this.txtmeter_serial.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_serial.Properties.EditFormat.FormatString = "#.##";
            this.txtmeter_serial.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtmeter_serial.Properties.Mask.EditMask = "([a-zA-Z0-9]*)";
            this.txtmeter_serial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtmeter_serial.ShowToolTips = false;
            this.txtmeter_serial.Size = new System.Drawing.Size(100, 20);
            this.txtmeter_serial.TabIndex = 286;
            this.txtmeter_serial.Tag = "";
            // 
            // labelElectricRoomNo
            // 
            this.labelElectricRoomNo.Location = new System.Drawing.Point(36, 99);
            this.labelElectricRoomNo.Name = "labelElectricRoomNo";
            this.labelElectricRoomNo.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricRoomNo.Size = new System.Drawing.Size(89, 33);
            this.labelElectricRoomNo.TabIndex = 265;
            this.labelElectricRoomNo.Text = "หมายเลขห้อง :";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(131, 354);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 37);
            this.simpleButton2.TabIndex = 260;
            this.simpleButton2.Text = "บันทึก";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(222, 354);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 37);
            this.simpleButton1.TabIndex = 259;
            this.simpleButton1.Text = "ยกเลิก";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelElectricMeterDetail);
            this.panelControl1.Controls.Add(this.memometer_detail);
            this.panelControl1.Controls.Add(this.gridLookUpEditRoom);
            this.panelControl1.Controls.Add(this.lookUpEditFloor);
            this.panelControl1.Controls.Add(this.lookUpEditBuilding);
            this.panelControl1.Controls.Add(this.txtmeter_label);
            this.panelControl1.Controls.Add(this.labelElectricMeterModel);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.txtmeter_model);
            this.panelControl1.Controls.Add(this.labelElectricMeterSerial);
            this.panelControl1.Controls.Add(this.labelElectricMeterLabel);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.txtmeter_serial);
            this.panelControl1.Controls.Add(this.labelElectricRoomNo);
            this.panelControl1.Controls.Add(this.labelElectricFloor);
            this.panelControl1.Controls.Add(this.labelElectricBuildingLabel);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.panelControl1.Size = new System.Drawing.Size(650, 550);
            this.panelControl1.TabIndex = 3;
            // 
            // labelElectricFloor
            // 
            this.labelElectricFloor.Location = new System.Drawing.Point(84, 66);
            this.labelElectricFloor.Name = "labelElectricFloor";
            this.labelElectricFloor.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricFloor.Size = new System.Drawing.Size(41, 33);
            this.labelElectricFloor.TabIndex = 264;
            this.labelElectricFloor.Text = "ชั้น :";
            // 
            // labelElectricBuildingLabel
            // 
            this.labelElectricBuildingLabel.Location = new System.Drawing.Point(69, 33);
            this.labelElectricBuildingLabel.Name = "labelElectricBuildingLabel";
            this.labelElectricBuildingLabel.Padding = new System.Windows.Forms.Padding(10);
            this.labelElectricBuildingLabel.Size = new System.Drawing.Size(56, 33);
            this.labelElectricBuildingLabel.TabIndex = 263;
            this.labelElectricBuildingLabel.Text = "อาคาร :";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.simpleButton3.Location = new System.Drawing.Point(10, 10);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(630, 23);
            this.simpleButton3.TabIndex = 283;
            this.simpleButton3.Text = "เพิ่มข้อมูลมิเตอร์น้ำประปา";
            // 
            // BasicInfoWaterMeterAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "BasicInfoWaterMeterAdd";
            this.Size = new System.Drawing.Size(650, 550);
            ((System.ComponentModel.ISupportInitialize)(this.memometer_detail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEditRoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFloor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_label.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_model.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeter_serial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelElectricMeterDetail;
        private DevExpress.XtraEditors.MemoEdit memometer_detail;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEditRoom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn room_id;
        private DevExpress.XtraGrid.Columns.GridColumn room_label;
        private DevExpress.XtraGrid.Columns.GridColumn room_no;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditFloor;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;
        private DevExpress.XtraEditors.TextEdit txtmeter_label;
        private DevExpress.XtraEditors.LabelControl labelElectricMeterModel;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtmeter_model;
        private DevExpress.XtraEditors.LabelControl labelElectricMeterSerial;
        private DevExpress.XtraEditors.LabelControl labelElectricMeterLabel;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtmeter_serial;
        private DevExpress.XtraEditors.LabelControl labelElectricRoomNo;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelElectricFloor;
        private DevExpress.XtraEditors.LabelControl labelElectricBuildingLabel;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
    }
}
