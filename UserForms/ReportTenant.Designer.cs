namespace DXWindowsApplication2.UserForms
{
    partial class ReportTenant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportTenant));
            this.gbTenant = new DevExpress.XtraEditors.GroupControl();
            this.gbTenantBox = new System.Windows.Forms.GroupBox();
            this.lookUpEditRoomTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditRoomFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.gbSummation = new System.Windows.Forms.GroupBox();
            this.radioGroupTenantType = new DevExpress.XtraEditors.RadioGroup();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bttExport = new DevExpress.XtraEditors.SimpleButton();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.lbRoom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gbTenant)).BeginInit();
            this.gbTenant.SuspendLayout();
            this.gbTenantBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            this.gbSummation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupTenantType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTenant
            // 
            this.gbTenant.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbTenant.AppearanceCaption.Options.UseFont = true;
            this.gbTenant.Controls.Add(this.gbTenantBox);
            this.gbTenant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTenant.Location = new System.Drawing.Point(7, 7);
            this.gbTenant.Name = "gbTenant";
            this.gbTenant.Padding = new System.Windows.Forms.Padding(10);
            this.gbTenant.Size = new System.Drawing.Size(677, 607);
            this.gbTenant.TabIndex = 1;
            this.gbTenant.Text = "Tenant";
            // 
            // gbTenantBox
            // 
            this.gbTenantBox.Controls.Add(this.lookUpEditRoomTo);
            this.gbTenantBox.Controls.Add(this.lookUpEditRoomFrom);
            this.gbTenantBox.Controls.Add(this.lookUpEditBuilding);
            this.gbTenantBox.Controls.Add(this.gbSummation);
            this.gbTenantBox.Controls.Add(this.bttPrint);
            this.gbTenantBox.Controls.Add(this.bttExport);
            this.gbTenantBox.Controls.Add(this.lbBuilding);
            this.gbTenantBox.Controls.Add(this.lbRoom);
            this.gbTenantBox.Controls.Add(this.labelControl1);
            this.gbTenantBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTenantBox.Location = new System.Drawing.Point(12, 32);
            this.gbTenantBox.Name = "gbTenantBox";
            this.gbTenantBox.Size = new System.Drawing.Size(653, 495);
            this.gbTenantBox.TabIndex = 276;
            this.gbTenantBox.TabStop = false;
            this.gbTenantBox.Text = "Tenant";
            // 
            // lookUpEditRoomTo
            // 
            this.lookUpEditRoomTo.Enabled = false;
            this.lookUpEditRoomTo.Location = new System.Drawing.Point(321, 92);
            this.lookUpEditRoomTo.Name = "lookUpEditRoomTo";
            this.lookUpEditRoomTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomTo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomTo.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomTo.TabIndex = 282;
            // 
            // lookUpEditRoomFrom
            // 
            this.lookUpEditRoomFrom.Enabled = false;
            this.lookUpEditRoomFrom.Location = new System.Drawing.Point(138, 92);
            this.lookUpEditRoomFrom.Name = "lookUpEditRoomFrom";
            this.lookUpEditRoomFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRoomFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("coderef", " "),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("room_id", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEditRoomFrom.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditRoomFrom.TabIndex = 281;
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(138, 55);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "building_id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", " ")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(124, 20);
            this.lookUpEditBuilding.TabIndex = 280;
            // 
            // gbSummation
            // 
            this.gbSummation.Controls.Add(this.radioGroupTenantType);
            this.gbSummation.Location = new System.Drawing.Point(95, 157);
            this.gbSummation.Name = "gbSummation";
            this.gbSummation.Size = new System.Drawing.Size(350, 232);
            this.gbSummation.TabIndex = 276;
            this.gbSummation.TabStop = false;
            this.gbSummation.Text = "เลือกประเภทผู้เช่า";
            // 
            // radioGroupTenantType
            // 
            this.radioGroupTenantType.EditValue = 3;
            this.radioGroupTenantType.Location = new System.Drawing.Point(77, 44);
            this.radioGroupTenantType.Name = "radioGroupTenantType";
            this.radioGroupTenantType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupTenantType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupTenantType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupTenantType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "พักอาศัย"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "จอง"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "แจ้งย้ายออก"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "ยกเลิกจอง")});
            this.radioGroupTenantType.Size = new System.Drawing.Size(248, 142);
            this.radioGroupTenantType.TabIndex = 267;
            // 
            // bttPrint
            // 
            this.bttPrint.Image = global::DXWindowsApplication2.Properties.Resources.print;
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(321, 434);
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
            this.bttExport.Location = new System.Drawing.Point(138, 434);
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(124, 55);
            this.bttExport.TabIndex = 274;
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // lbBuilding
            // 
            this.lbBuilding.Location = new System.Drawing.Point(76, 48);
            this.lbBuilding.Name = "lbBuilding";
            this.lbBuilding.Padding = new System.Windows.Forms.Padding(10);
            this.lbBuilding.Size = new System.Drawing.Size(56, 33);
            this.lbBuilding.TabIndex = 271;
            this.lbBuilding.Text = "อาคาร :";
            // 
            // lbRoom
            // 
            this.lbRoom.Location = new System.Drawing.Point(86, 85);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Padding = new System.Windows.Forms.Padding(10);
            this.lbRoom.Size = new System.Drawing.Size(46, 33);
            this.lbRoom.TabIndex = 265;
            this.lbRoom.Text = "ห้อง :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(277, 85);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(38, 33);
            this.labelControl1.TabIndex = 268;
            this.labelControl1.Text = "ถึง :";
            // 
            // ReportTenant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTenant);
            this.Name = "ReportTenant";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(691, 621);
            ((System.ComponentModel.ISupportInitialize)(this.gbTenant)).EndInit();
            this.gbTenant.ResumeLayout(false);
            this.gbTenantBox.ResumeLayout(false);
            this.gbTenantBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRoomFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            this.gbSummation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupTenantType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbTenant;
        private System.Windows.Forms.GroupBox gbTenantBox;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.SimpleButton bttExport;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.LabelControl lbRoom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox gbSummation;
        private DevExpress.XtraEditors.RadioGroup radioGroupTenantType;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRoomFrom;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;


    }
}
