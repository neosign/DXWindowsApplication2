namespace DXWindowsApplication2.UserForms
{
    partial class ListContract
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbListContract = new DevExpress.XtraEditors.GroupControl();
            this.panelControl13 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlContract = new DevExpress.XtraGrid.GridControl();
            this.gridViewContract = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumCheckbox = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoomName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenant = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDatecreted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCancelDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColFlagTypeContract = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.lbBuilding = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.checkEditCheckAll = new DevExpress.XtraEditors.CheckEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.lbTodate = new DevExpress.XtraEditors.LabelControl();
            this.lbFromdate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.repositoryItemDateEditIssueDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbListContract)).BeginInit();
            this.gbListContract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl13)).BeginInit();
            this.panelControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            this.panelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCheckAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditIssueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditIssueDate.VistaTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gbListContract
            // 
            this.gbListContract.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbListContract.AppearanceCaption.Options.UseFont = true;
            this.gbListContract.Controls.Add(this.panelControl13);
            this.gbListContract.Controls.Add(this.panelControl12);
            this.gbListContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbListContract.Location = new System.Drawing.Point(7, 7);
            this.gbListContract.Name = "gbListContract";
            this.gbListContract.Size = new System.Drawing.Size(1160, 641);
            this.gbListContract.TabIndex = 1;
            this.gbListContract.Text = "รายการใบแจ้งหนี้และการชำระเงิน";
            // 
            // panelControl13
            // 
            this.panelControl13.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl13.Appearance.Options.UseBackColor = true;
            this.panelControl13.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl13.Controls.Add(this.gridControlContract);
            this.panelControl13.Controls.Add(this.panelControl1);
            this.panelControl13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl13.Location = new System.Drawing.Point(2, 95);
            this.panelControl13.Name = "panelControl13";
            this.panelControl13.Size = new System.Drawing.Size(1156, 544);
            this.panelControl13.TabIndex = 3;
            // 
            // gridControlContract
            // 
            this.gridControlContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlContract.Location = new System.Drawing.Point(0, 0);
            this.gridControlContract.MainView = this.gridViewContract;
            this.gridControlContract.Name = "gridControlContract";
            this.gridControlContract.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditIssueDate});
            this.gridControlContract.Size = new System.Drawing.Size(1156, 478);
            this.gridControlContract.TabIndex = 2;
            this.gridControlContract.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewContract});
            // 
            // gridViewContract
            // 
            this.gridViewContract.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumCheckbox,
            this.colContractNumber,
            this.colType,
            this.colRoomName,
            this.colTenant,
            this.colLastname,
            this.colDatecreted,
            this.colStatus,
            this.colCancelDate,
            this.ColFlagTypeContract});
            this.gridViewContract.GridControl = this.gridControlContract;
            this.gridViewContract.Name = "gridViewContract";
            this.gridViewContract.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewContract.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewContract.OptionsFind.AlwaysVisible = true;
            this.gridViewContract.OptionsFind.ShowCloseButton = false;
            this.gridViewContract.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumCheckbox
            // 
            this.gridColumCheckbox.Caption = " ";
            this.gridColumCheckbox.FieldName = "checkbox";
            this.gridColumCheckbox.Name = "gridColumCheckbox";
            this.gridColumCheckbox.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumCheckbox.OptionsColumn.AllowMove = false;
            this.gridColumCheckbox.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumCheckbox.Visible = true;
            this.gridColumCheckbox.VisibleIndex = 0;
            this.gridColumCheckbox.Width = 20;
            // 
            // colContractNumber
            // 
            this.colContractNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colContractNumber.AppearanceHeader.Options.UseFont = true;
            this.colContractNumber.Caption = "เลขที่";
            this.colContractNumber.FieldName = "check_in_label";
            this.colContractNumber.Name = "colContractNumber";
            this.colContractNumber.OptionsColumn.AllowEdit = false;
            this.colContractNumber.OptionsColumn.AllowFocus = false;
            this.colContractNumber.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colContractNumber.OptionsColumn.AllowMove = false;
            this.colContractNumber.OptionsColumn.ReadOnly = true;
            this.colContractNumber.Visible = true;
            this.colContractNumber.VisibleIndex = 1;
            this.colContractNumber.Width = 122;
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colType.AppearanceHeader.Options.UseFont = true;
            this.colType.Caption = "ประเภท";
            this.colType.FieldName = "contacttype_text";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsColumn.AllowFocus = false;
            this.colType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colType.OptionsColumn.AllowMove = false;
            this.colType.OptionsColumn.ReadOnly = true;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 130;
            // 
            // colRoomName
            // 
            this.colRoomName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colRoomName.AppearanceHeader.Options.UseFont = true;
            this.colRoomName.Caption = "ชื่อห้อง";
            this.colRoomName.FieldName = "room_label";
            this.colRoomName.Name = "colRoomName";
            this.colRoomName.OptionsColumn.AllowEdit = false;
            this.colRoomName.OptionsColumn.AllowFocus = false;
            this.colRoomName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colRoomName.OptionsColumn.AllowMove = false;
            this.colRoomName.OptionsColumn.ReadOnly = true;
            this.colRoomName.Visible = true;
            this.colRoomName.VisibleIndex = 3;
            this.colRoomName.Width = 156;
            // 
            // colTenant
            // 
            this.colTenant.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTenant.AppearanceHeader.Options.UseFont = true;
            this.colTenant.Caption = "ชื่อผู้เช่า";
            this.colTenant.FieldName = "tenant_name";
            this.colTenant.Name = "colTenant";
            this.colTenant.OptionsColumn.AllowEdit = false;
            this.colTenant.OptionsColumn.AllowFocus = false;
            this.colTenant.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTenant.OptionsColumn.AllowMove = false;
            this.colTenant.OptionsColumn.ReadOnly = true;
            this.colTenant.Visible = true;
            this.colTenant.VisibleIndex = 4;
            this.colTenant.Width = 165;
            // 
            // colLastname
            // 
            this.colLastname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLastname.AppearanceHeader.Options.UseFont = true;
            this.colLastname.Caption = "นามสกุล";
            this.colLastname.FieldName = "tenant_surname";
            this.colLastname.Name = "colLastname";
            this.colLastname.OptionsColumn.AllowEdit = false;
            this.colLastname.OptionsColumn.AllowFocus = false;
            this.colLastname.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLastname.OptionsColumn.AllowMove = false;
            this.colLastname.OptionsColumn.ReadOnly = true;
            this.colLastname.Visible = true;
            this.colLastname.VisibleIndex = 5;
            this.colLastname.Width = 139;
            // 
            // colDatecreted
            // 
            this.colDatecreted.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDatecreted.AppearanceHeader.Options.UseFont = true;
            this.colDatecreted.Caption = "วันที่ทำ";
            this.colDatecreted.ColumnEdit = this.repositoryItemDateEditIssueDate;
            this.colDatecreted.FieldName = "check_in_date";
            this.colDatecreted.Name = "colDatecreted";
            this.colDatecreted.OptionsColumn.AllowEdit = false;
            this.colDatecreted.OptionsColumn.AllowFocus = false;
            this.colDatecreted.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDatecreted.OptionsColumn.AllowMove = false;
            this.colDatecreted.OptionsColumn.ReadOnly = true;
            this.colDatecreted.Visible = true;
            this.colDatecreted.VisibleIndex = 6;
            this.colDatecreted.Width = 117;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.Caption = "สถานะ";
            this.colStatus.FieldName = "contactstatus_text";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 7;
            this.colStatus.Width = 134;
            // 
            // colCancelDate
            // 
            this.colCancelDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCancelDate.AppearanceHeader.Options.UseFont = true;
            this.colCancelDate.Caption = "วันที่ปิด/ยกเลิก";
            this.colCancelDate.DisplayFormat.FormatString = "T";
            this.colCancelDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCancelDate.FieldName = "closed_date";
            this.colCancelDate.Name = "colCancelDate";
            this.colCancelDate.OptionsColumn.AllowEdit = false;
            this.colCancelDate.OptionsColumn.AllowFocus = false;
            this.colCancelDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCancelDate.OptionsColumn.AllowMove = false;
            this.colCancelDate.OptionsColumn.ReadOnly = true;
            this.colCancelDate.Visible = true;
            this.colCancelDate.VisibleIndex = 8;
            this.colCancelDate.Width = 155;
            // 
            // ColFlagTypeContract
            // 
            this.ColFlagTypeContract.Caption = "FlagTypeContract";
            this.ColFlagTypeContract.FieldName = "flag_type_contract";
            this.ColFlagTypeContract.Name = "ColFlagTypeContract";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bttPrint);
            this.panelControl1.Controls.Add(this.bttCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 478);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1156, 66);
            this.panelControl1.TabIndex = 295;
            // 
            // bttPrint
            // 
            this.bttPrint.Image = global::DXWindowsApplication2.Properties.Resources.print;
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(5, 6);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(161, 55);
            this.bttPrint.TabIndex = 22;
            this.bttPrint.Text = "พิมพ์ทั้งหมดที่เลือก";
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // bttCancel
            // 
            this.bttCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(990, 6);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(161, 55);
            this.bttCancel.TabIndex = 21;
            this.bttCancel.Text = "ยกเลิกทั้งหมดที่เลือก";
            this.bttCancel.Visible = false;
            // 
            // panelControl12
            // 
            this.panelControl12.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl12.Appearance.Options.UseBackColor = true;
            this.panelControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl12.Controls.Add(this.lbBuilding);
            this.panelControl12.Controls.Add(this.lookUpEditBuilding);
            this.panelControl12.Controls.Add(this.checkEditCheckAll);
            this.panelControl12.Controls.Add(this.dateEditTo);
            this.panelControl12.Controls.Add(this.lbTodate);
            this.panelControl12.Controls.Add(this.lbFromdate);
            this.panelControl12.Controls.Add(this.dateEditFrom);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl12.Location = new System.Drawing.Point(2, 22);
            this.panelControl12.Name = "panelControl12";
            this.panelControl12.Size = new System.Drawing.Size(1156, 73);
            this.panelControl12.TabIndex = 2;
            // 
            // lbBuilding
            // 
            this.lbBuilding.Location = new System.Drawing.Point(451, 13);
            this.lbBuilding.Name = "lbBuilding";
            this.lbBuilding.Size = new System.Drawing.Size(36, 13);
            this.lbBuilding.TabIndex = 315;
            this.lbBuilding.Text = "อาคาร :";
            // 
            // lookUpEditBuilding
            // 
            this.lookUpEditBuilding.Location = new System.Drawing.Point(493, 10);
            this.lookUpEditBuilding.Name = "lookUpEditBuilding";
            this.lookUpEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_label", "อาคาร")});
            this.lookUpEditBuilding.Size = new System.Drawing.Size(138, 20);
            this.lookUpEditBuilding.TabIndex = 314;
            // 
            // checkEditCheckAll
            // 
            this.checkEditCheckAll.Location = new System.Drawing.Point(16, 44);
            this.checkEditCheckAll.Name = "checkEditCheckAll";
            this.checkEditCheckAll.Properties.Caption = "เลือกทั้งหมด";
            this.checkEditCheckAll.Size = new System.Drawing.Size(160, 19);
            this.checkEditCheckAll.TabIndex = 7;
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(282, 10);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(163, 20);
            this.dateEditTo.TabIndex = 4;
            // 
            // lbTodate
            // 
            this.lbTodate.Location = new System.Drawing.Point(238, 13);
            this.lbTodate.Name = "lbTodate";
            this.lbTodate.Size = new System.Drawing.Size(38, 13);
            this.lbTodate.TabIndex = 3;
            this.lbTodate.Text = "ถึงวันที่ :";
            // 
            // lbFromdate
            // 
            this.lbFromdate.Location = new System.Drawing.Point(18, 13);
            this.lbFromdate.Name = "lbFromdate";
            this.lbFromdate.Size = new System.Drawing.Size(45, 13);
            this.lbFromdate.TabIndex = 2;
            this.lbFromdate.Text = "จากวันที่ :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(69, 10);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(163, 20);
            this.dateEditFrom.TabIndex = 0;
            // 
            // repositoryItemDateEditIssueDate
            // 
            this.repositoryItemDateEditIssueDate.AutoHeight = false;
            this.repositoryItemDateEditIssueDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEditIssueDate.Mask.EditMask = "dd/MM/yyyy";
            this.repositoryItemDateEditIssueDate.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemDateEditIssueDate.Name = "repositoryItemDateEditIssueDate";
            this.repositoryItemDateEditIssueDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // ListContract
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbListContract);
            this.Name = "ListContract";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(1174, 655);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbListContract)).EndInit();
            this.gbListContract.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl13)).EndInit();
            this.panelControl13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            this.panelControl12.ResumeLayout(false);
            this.panelControl12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCheckAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditIssueDate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditIssueDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl gbListContract;
        private DevExpress.XtraEditors.PanelControl panelControl13;
        private DevExpress.XtraGrid.GridControl gridControlContract;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewContract;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colRoomName;
        private DevExpress.XtraGrid.Columns.GridColumn colTenant;
        private DevExpress.XtraGrid.Columns.GridColumn colLastname;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumCheckbox;
        private DevExpress.XtraEditors.PanelControl panelControl12;
        private DevExpress.XtraEditors.LabelControl lbBuilding;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditBuilding;
        private DevExpress.XtraEditors.CheckEdit checkEditCheckAll;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LabelControl lbTodate;
        private DevExpress.XtraEditors.LabelControl lbFromdate;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDatecreted;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCancelDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraGrid.Columns.GridColumn ColFlagTypeContract;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditIssueDate;
    }
}
