namespace DXWindowsApplication2.UserForms
{
    partial class ReportRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportRoom));
            this.gbIncome = new DevExpress.XtraEditors.GroupControl();
            this.gbIncomeBox = new System.Windows.Forms.GroupBox();
            this.lookUpEditRoomTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditRoomFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lbStatus = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditRoomStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bttExport = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.lbRoom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).BeginInit();
            this.gbIncome.SuspendLayout();
            this.gbIncomeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
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
            this.gbIncome.Text = "Room";
            // 
            // gbIncomeBox
            // 
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomTo);
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomFrom);
            this.gbIncomeBox.Controls.Add(this.lbStatus);
            this.gbIncomeBox.Controls.Add(this.lookUpEditRoomStatus);
            this.gbIncomeBox.Controls.Add(this.bttPrint);
            this.gbIncomeBox.Controls.Add(this.bttExport);
            this.gbIncomeBox.Controls.Add(this.lookUpEditBuilding);
            this.gbIncomeBox.Controls.Add(this.lbBuilding);
            this.gbIncomeBox.Controls.Add(this.lbRoom);
            this.gbIncomeBox.Controls.Add(this.labelControl1);
            this.gbIncomeBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbIncomeBox.Location = new System.Drawing.Point(12, 32);
            this.gbIncomeBox.Name = "gbIncomeBox";
            this.gbIncomeBox.Size = new System.Drawing.Size(653, 327);
            this.gbIncomeBox.TabIndex = 276;
            this.gbIncomeBox.TabStop = false;
            this.gbIncomeBox.Text = "Room";
            // 
            // lookUpEditRoomTo
            // 
            this.lookUpEditRoomTo.Enabled = false;
            this.lookUpEditRoomTo.Location = new System.Drawing.Point(321, 96);
            this.lookUpEditRoomTo.Name = "lookUpEditRoomTo";
            this.lookUpEditRoomTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomTo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomTo.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomTo.TabIndex = 279;
            // 
            // lookUpEditRoomFrom
            // 
            this.lookUpEditRoomFrom.Enabled = false;
            this.lookUpEditRoomFrom.Location = new System.Drawing.Point(147, 96);
            this.lookUpEditRoomFrom.Name = "lookUpEditRoomFrom";
            this.lookUpEditRoomFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomFrom.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomFrom.TabIndex = 278;
            // 
            // lbStatus
            // 
            this.lbStatus.Location = new System.Drawing.Point(84, 132);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Padding = new System.Windows.Forms.Padding(10);
            this.lbStatus.Size = new System.Drawing.Size(57, 33);
            this.lbStatus.TabIndex = 276;
            this.lbStatus.Text = "สถานะ :";
            // 
            // lookUpEditRoomStatus
            // 
            this.lookUpEditRoomStatus.Location = new System.Drawing.Point(147, 139);
            this.lookUpEditRoomStatus.Name = "lookUpEditRoomStatus";
            this.lookUpEditRoomStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_status_label", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_status", "room_status", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomStatus.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomStatus.TabIndex = 277;
            // 
            // bttPrint
            // 
            this.bttPrint.Enabled = false;
            this.bttPrint.Image = global::DXWindowsApplication2.Properties.Resources.print;
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(321, 214);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(124, 55);
            this.bttPrint.TabIndex = 275;
            this.bttPrint.Text = "พิมพ์";
            this.bttPrint.Visible = false;
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // bttExport
            // 
            this.bttExport.Enabled = false;
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttExport.Location = new System.Drawing.Point(147, 214);
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(124, 55);
            this.bttExport.TabIndex = 274;
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(147, 55);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "building_id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", " ")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditBuilding.TabIndex = 272;
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
            // lbRoom
            // 
            this.lbRoom.Location = new System.Drawing.Point(95, 89);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoom.Size = new System.Drawing.Size(46, 33);
            this.lbRoom.TabIndex = 265;
            this.lbRoom.Text = "ห้อง :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(277, 89);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(38, 33);
            this.labelControl1.TabIndex = 268;
            this.labelControl1.Text = "ถึง :";
            // 
            // ReportRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbIncome);
            this.Name = "ReportRoom";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(691, 621);
            ((System.ComponentModel.ISupportInitialize)(this.gbIncome)).EndInit();
            this.gbIncome.ResumeLayout(false);
            this.gbIncomeBox.ResumeLayout(false);
            this.gbIncomeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbIncome;
        private System.Windows.Forms.GroupBox gbIncomeBox;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.SimpleButton bttExport;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.LabelControl lbRoom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lbStatus;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomStatus;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomFrom;


    }
}
