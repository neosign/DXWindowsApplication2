namespace DXWindowsApplication2.UserForms
{
    partial class TestControl
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
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBuildingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuildingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZoneName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFloor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoomNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenantName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenantLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWaterPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateInvoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceNet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colElectricPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhonePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView3;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(894, 440);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBuildingId,
            this.colBuildingName,
            this.colZoneName,
            this.colFloor,
            this.colRoomNumber,
            this.colTenantName,
            this.colTenantLastName,
            this.colWaterPrice,
            this.colDateInvoice,
            this.colPriceNet,
            this.colInvoiceNumber,
            this.colElectricPrice,
            this.colPhonePrice});
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsFind.AlwaysVisible = true;
            this.gridView3.OptionsFind.ShowCloseButton = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colBuildingId
            // 
            this.colBuildingId.Caption = "รหัสอาคาร";
            this.colBuildingId.FieldName = "colBuildingId";
            this.colBuildingId.Name = "colBuildingId";
            this.colBuildingId.Visible = true;
            this.colBuildingId.VisibleIndex = 0;
            this.colBuildingId.Width = 82;
            // 
            // colBuildingName
            // 
            this.colBuildingName.Caption = "ชื่ออาคาร";
            this.colBuildingName.FieldName = "colBuildingName";
            this.colBuildingName.Name = "colBuildingName";
            this.colBuildingName.Visible = true;
            this.colBuildingName.VisibleIndex = 1;
            this.colBuildingName.Width = 52;
            // 
            // colZoneName
            // 
            this.colZoneName.Caption = "โซน";
            this.colZoneName.FieldName = "colZoneName";
            this.colZoneName.Name = "colZoneName";
            this.colZoneName.Visible = true;
            this.colZoneName.VisibleIndex = 2;
            this.colZoneName.Width = 44;
            // 
            // colFloor
            // 
            this.colFloor.Caption = "ชั้น";
            this.colFloor.FieldName = "colFloor";
            this.colFloor.Name = "colFloor";
            this.colFloor.Visible = true;
            this.colFloor.VisibleIndex = 3;
            this.colFloor.Width = 23;
            // 
            // colRoomNumber
            // 
            this.colRoomNumber.Caption = "หมายเลขห้อง";
            this.colRoomNumber.FieldName = "colRoomNumber";
            this.colRoomNumber.Name = "colRoomNumber";
            this.colRoomNumber.Visible = true;
            this.colRoomNumber.VisibleIndex = 4;
            this.colRoomNumber.Width = 71;
            // 
            // colTenantName
            // 
            this.colTenantName.Caption = "ชื่อผู้เช่า";
            this.colTenantName.FieldName = "colTenantName";
            this.colTenantName.Name = "colTenantName";
            this.colTenantName.Visible = true;
            this.colTenantName.VisibleIndex = 5;
            this.colTenantName.Width = 46;
            // 
            // colTenantLastName
            // 
            this.colTenantLastName.Caption = "นามสกุล";
            this.colTenantLastName.FieldName = "colTenantLastName";
            this.colTenantLastName.Name = "colTenantLastName";
            this.colTenantLastName.Visible = true;
            this.colTenantLastName.VisibleIndex = 6;
            this.colTenantLastName.Width = 49;
            // 
            // colWaterPrice
            // 
            this.colWaterPrice.Caption = "ค่าน้ำประปา";
            this.colWaterPrice.FieldName = "colWaterPrice";
            this.colWaterPrice.Name = "colWaterPrice";
            this.colWaterPrice.Visible = true;
            this.colWaterPrice.VisibleIndex = 10;
            this.colWaterPrice.Width = 69;
            // 
            // colDateInvoice
            // 
            this.colDateInvoice.Caption = "วันที่ออกใบแจ้งหนี้";
            this.colDateInvoice.FieldName = "colDateInvoice";
            this.colDateInvoice.Name = "colDateInvoice";
            this.colDateInvoice.Visible = true;
            this.colDateInvoice.VisibleIndex = 8;
            this.colDateInvoice.Width = 94;
            // 
            // colPriceNet
            // 
            this.colPriceNet.Caption = "ยอดรวมสุทธิ";
            this.colPriceNet.FieldName = "colPriceNet";
            this.colPriceNet.Name = "colPriceNet";
            this.colPriceNet.Visible = true;
            this.colPriceNet.VisibleIndex = 12;
            this.colPriceNet.Width = 181;
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.Caption = "เลขที่ใบแจ้งหนี้";
            this.colInvoiceNumber.FieldName = "colInvoiceNumber";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.Visible = true;
            this.colInvoiceNumber.VisibleIndex = 7;
            this.colInvoiceNumber.Width = 79;
            // 
            // colElectricPrice
            // 
            this.colElectricPrice.Caption = "ค่าไฟฟ้า";
            this.colElectricPrice.FieldName = "colElectricPrice";
            this.colElectricPrice.Name = "colElectricPrice";
            this.colElectricPrice.Visible = true;
            this.colElectricPrice.VisibleIndex = 9;
            this.colElectricPrice.Width = 46;
            // 
            // colPhonePrice
            // 
            this.colPhonePrice.Caption = "ค่าโทรศัพท์";
            this.colPhonePrice.FieldName = "colPhonePrice";
            this.colPhonePrice.Name = "colPhonePrice";
            this.colPhonePrice.Visible = true;
            this.colPhonePrice.VisibleIndex = 11;
            this.colPhonePrice.Width = 61;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(898, 26);
            this.panelControl2.TabIndex = 40;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(898, 477);
            this.panelControl3.TabIndex = 41;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xtraScrollableControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(898, 477);
            this.groupControl1.TabIndex = 24;
            this.groupControl1.Text = "ข้อมูลใบแจ้งหนี้ทั้งหมด";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.panelControl5);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(894, 453);
            this.xtraScrollableControl1.TabIndex = 30;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.panelControl4);
            this.panelControl5.Controls.Add(this.gridControl1);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(894, 453);
            this.panelControl5.TabIndex = 33;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.simpleButton2);
            this.panelControl4.Controls.Add(this.simpleButton3);
            this.panelControl4.Controls.Add(this.simpleButton1);
            this.panelControl4.Location = new System.Drawing.Point(0, 353);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(593, 100);
            this.panelControl4.TabIndex = 6;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(215, 16);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(121, 45);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "พิมพ์เฉพาะที่เลือก";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(44, 16);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(121, 45);
            this.simpleButton3.TabIndex = 4;
            this.simpleButton3.Text = "พิมพ์ทั้งหมด";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(384, 16);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(121, 45);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "ยกเลิก";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 1200;
            this.serialPort1.DtrEnable = true;
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            this.serialPort1.RtsEnable = true;
            // 
            // TestControl
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl3);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(898, 477);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colBuildingId;
        private DevExpress.XtraGrid.Columns.GridColumn colBuildingName;
        private DevExpress.XtraGrid.Columns.GridColumn colZoneName;
        private DevExpress.XtraGrid.Columns.GridColumn colFloor;
        private DevExpress.XtraGrid.Columns.GridColumn colRoomNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colTenantName;
        private DevExpress.XtraGrid.Columns.GridColumn colTenantLastName;
        private DevExpress.XtraGrid.Columns.GridColumn colWaterPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDateInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceNet;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colElectricPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPhonePrice;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.IO.Ports.SerialPort serialPort1;














    }
}
