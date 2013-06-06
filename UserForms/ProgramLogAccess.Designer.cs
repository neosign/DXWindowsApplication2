namespace DXWindowsApplication2.UserForms
{
    partial class ProgramLogAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramLogAccess));
            this.gridColumnGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControlHistory = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnUsername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDateAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTimeAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnList = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRoom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTenant = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControlFilter = new DevExpress.XtraEditors.PanelControl();
            this.bttSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
            this.labelControlTo = new DevExpress.XtraEditors.LabelControl();
            this.labelControlStart = new DevExpress.XtraEditors.LabelControl();
            this.panelControlPrint = new DevExpress.XtraEditors.PanelControl();
            this.bttPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlHistory)).BeginInit();
            this.groupControlHistory.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).BeginInit();
            this.panelControlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlPrint)).BeginInit();
            this.panelControlPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumnGroup
            // 
            this.gridColumnGroup.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnGroup.AppearanceHeader.Options.UseFont = true;
            this.gridColumnGroup.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnGroup.Caption = "กลุ่มผู้ใช้";
            this.gridColumnGroup.FieldName = "group_name";
            this.gridColumnGroup.Name = "gridColumnGroup";
            this.gridColumnGroup.OptionsColumn.AllowEdit = false;
            this.gridColumnGroup.OptionsColumn.AllowFocus = false;
            this.gridColumnGroup.Visible = true;
            this.gridColumnGroup.VisibleIndex = 2;
            this.gridColumnGroup.Width = 154;
            // 
            // groupControlHistory
            // 
            this.groupControlHistory.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlHistory.AppearanceCaption.Options.UseFont = true;
            this.groupControlHistory.Controls.Add(this.xtraScrollableControl1);
            this.groupControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlHistory.Location = new System.Drawing.Point(0, 0);
            this.groupControlHistory.Name = "groupControlHistory";
            this.groupControlHistory.Size = new System.Drawing.Size(920, 410);
            this.groupControlHistory.TabIndex = 24;
            this.groupControlHistory.Text = "ประวัติข้อมูลการใช้งาน";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.panelControl5);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(916, 386);
            this.xtraScrollableControl1.TabIndex = 30;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.panelControl1);
            this.panelControl5.Controls.Add(this.panelControlPrint);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(916, 386);
            this.panelControl5.TabIndex = 33;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gridControl2);
            this.panelControl1.Controls.Add(this.panelControlFilter);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(916, 321);
            this.panelControl1.TabIndex = 4;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 31);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(916, 290);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNo,
            this.gridColumnUsername,
            this.gridColumnName,
            this.gridColumnGroup,
            this.gridColumnDateAccess,
            this.gridColumnTimeAccess,
            this.gridColumnList,
            this.gridColumnStatus,
            this.gridColumnRoom,
            this.gridColumnTenant});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            this.gridView2.OptionsFind.ShowCloseButton = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnNo
            // 
            this.gridColumnNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnNo.AppearanceHeader.Options.UseFont = true;
            this.gridColumnNo.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNo.Caption = "ลำดับ";
            this.gridColumnNo.FieldName = "log_id";
            this.gridColumnNo.Name = "gridColumnNo";
            this.gridColumnNo.OptionsColumn.AllowEdit = false;
            this.gridColumnNo.OptionsColumn.AllowFocus = false;
            this.gridColumnNo.Width = 70;
            // 
            // gridColumnUsername
            // 
            this.gridColumnUsername.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnUsername.AppearanceHeader.Options.UseFont = true;
            this.gridColumnUsername.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnUsername.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnUsername.Caption = "ชื่อผู้ใช้ (เข้าระบบ)";
            this.gridColumnUsername.FieldName = "username";
            this.gridColumnUsername.Name = "gridColumnUsername";
            this.gridColumnUsername.OptionsColumn.AllowEdit = false;
            this.gridColumnUsername.OptionsColumn.AllowFocus = false;
            this.gridColumnUsername.Visible = true;
            this.gridColumnUsername.VisibleIndex = 0;
            this.gridColumnUsername.Width = 154;
            // 
            // gridColumnName
            // 
            this.gridColumnName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnName.AppearanceHeader.Options.UseFont = true;
            this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnName.Caption = "ชื่อผู้ใช้";
            this.gridColumnName.FieldName = "name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.OptionsColumn.AllowFocus = false;
            this.gridColumnName.OptionsColumn.AllowMove = false;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 165;
            // 
            // gridColumnDateAccess
            // 
            this.gridColumnDateAccess.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnDateAccess.AppearanceHeader.Options.UseFont = true;
            this.gridColumnDateAccess.Caption = "วันที่";
            this.gridColumnDateAccess.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumnDateAccess.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnDateAccess.FieldName = "date";
            this.gridColumnDateAccess.Name = "gridColumnDateAccess";
            this.gridColumnDateAccess.OptionsColumn.AllowEdit = false;
            this.gridColumnDateAccess.OptionsColumn.AllowFocus = false;
            this.gridColumnDateAccess.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDateAccess.OptionsColumn.AllowMove = false;
            this.gridColumnDateAccess.OptionsColumn.ReadOnly = true;
            this.gridColumnDateAccess.Visible = true;
            this.gridColumnDateAccess.VisibleIndex = 3;
            // 
            // gridColumnTimeAccess
            // 
            this.gridColumnTimeAccess.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnTimeAccess.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnTimeAccess.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnTimeAccess.AppearanceHeader.Options.UseFont = true;
            this.gridColumnTimeAccess.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnTimeAccess.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnTimeAccess.Caption = "เวลา";
            this.gridColumnTimeAccess.FieldName = "time";
            this.gridColumnTimeAccess.Name = "gridColumnTimeAccess";
            this.gridColumnTimeAccess.OptionsColumn.AllowEdit = false;
            this.gridColumnTimeAccess.OptionsColumn.AllowFocus = false;
            this.gridColumnTimeAccess.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.gridColumnTimeAccess.Visible = true;
            this.gridColumnTimeAccess.VisibleIndex = 4;
            this.gridColumnTimeAccess.Width = 134;
            // 
            // gridColumnList
            // 
            this.gridColumnList.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnList.AppearanceHeader.Options.UseFont = true;
            this.gridColumnList.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnList.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnList.Caption = "รายการ";
            this.gridColumnList.FieldName = "action";
            this.gridColumnList.Name = "gridColumnList";
            this.gridColumnList.OptionsColumn.AllowEdit = false;
            this.gridColumnList.OptionsColumn.AllowFocus = false;
            this.gridColumnList.OptionsColumn.AllowMove = false;
            this.gridColumnList.Visible = true;
            this.gridColumnList.VisibleIndex = 5;
            this.gridColumnList.Width = 291;
            // 
            // gridColumnStatus
            // 
            this.gridColumnStatus.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnStatus.AppearanceHeader.Options.UseFont = true;
            this.gridColumnStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnStatus.Caption = "สถานะ";
            this.gridColumnStatus.FieldName = "status";
            this.gridColumnStatus.Name = "gridColumnStatus";
            this.gridColumnStatus.OptionsColumn.AllowEdit = false;
            this.gridColumnStatus.OptionsColumn.AllowFocus = false;
            // 
            // gridColumnRoom
            // 
            this.gridColumnRoom.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnRoom.AppearanceHeader.Options.UseFont = true;
            this.gridColumnRoom.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnRoom.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnRoom.Caption = "Room";
            this.gridColumnRoom.FieldName = "room_label";
            this.gridColumnRoom.Name = "gridColumnRoom";
            // 
            // gridColumnTenant
            // 
            this.gridColumnTenant.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnTenant.AppearanceHeader.Options.UseFont = true;
            this.gridColumnTenant.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnTenant.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnTenant.Caption = "Tenant";
            this.gridColumnTenant.FieldName = "tenant_fullname";
            this.gridColumnTenant.Name = "gridColumnTenant";
            // 
            // panelControlFilter
            // 
            this.panelControlFilter.Controls.Add(this.bttSubmit);
            this.panelControlFilter.Controls.Add(this.dateEditEnd);
            this.panelControlFilter.Controls.Add(this.dateEditStart);
            this.panelControlFilter.Controls.Add(this.labelControlTo);
            this.panelControlFilter.Controls.Add(this.labelControlStart);
            this.panelControlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlFilter.Location = new System.Drawing.Point(0, 0);
            this.panelControlFilter.Name = "panelControlFilter";
            this.panelControlFilter.Size = new System.Drawing.Size(916, 31);
            this.panelControlFilter.TabIndex = 2;
            // 
            // bttSubmit
            // 
            this.bttSubmit.Location = new System.Drawing.Point(392, 3);
            this.bttSubmit.Name = "bttSubmit";
            this.bttSubmit.Size = new System.Drawing.Size(75, 23);
            this.bttSubmit.TabIndex = 435;
            this.bttSubmit.Text = "Submit";
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = new System.DateTime(2011, 7, 18, 10, 11, 22, 586);
            this.dateEditEnd.Location = new System.Drawing.Point(243, 6);
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.DisplayFormat.FormatString = "dd MMMM yyyy";
            this.dateEditEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEnd.Properties.EditFormat.FormatString = "dd MMMM yyyy";
            this.dateEditEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEnd.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEnd.Size = new System.Drawing.Size(129, 20);
            this.dateEditEnd.TabIndex = 434;
            // 
            // dateEditStart
            // 
            this.dateEditStart.EditValue = new System.DateTime(2011, 7, 18, 10, 11, 22, 586);
            this.dateEditStart.Location = new System.Drawing.Point(48, 5);
            this.dateEditStart.Name = "dateEditStart";
            this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStart.Properties.DisplayFormat.FormatString = "dd MMMM yyyy";
            this.dateEditStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditStart.Properties.EditFormat.FormatString = "dd MMMM yyyy";
            this.dateEditStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditStart.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStart.Size = new System.Drawing.Size(122, 20);
            this.dateEditStart.TabIndex = 434;
            // 
            // labelControlTo
            // 
            this.labelControlTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTo.Location = new System.Drawing.Point(206, 12);
            this.labelControlTo.Name = "labelControlTo";
            this.labelControlTo.Size = new System.Drawing.Size(31, 13);
            this.labelControlTo.TabIndex = 0;
            this.labelControlTo.Text = "ถึง";
            // 
            // labelControlStart
            // 
            this.labelControlStart.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlStart.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlStart.Location = new System.Drawing.Point(5, 12);
            this.labelControlStart.Name = "labelControlStart";
            this.labelControlStart.Size = new System.Drawing.Size(37, 13);
            this.labelControlStart.TabIndex = 0;
            this.labelControlStart.Text = "วันที่ :";
            // 
            // panelControlPrint
            // 
            this.panelControlPrint.Controls.Add(this.bttPrint);
            this.panelControlPrint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlPrint.Location = new System.Drawing.Point(0, 321);
            this.panelControlPrint.Name = "panelControlPrint";
            this.panelControlPrint.Size = new System.Drawing.Size(916, 65);
            this.panelControlPrint.TabIndex = 435;
            // 
            // bttPrint
            // 
            this.bttPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttPrint.Image = ((System.Drawing.Image)(resources.GetObject("bttPrint.Image")));
            this.bttPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttPrint.Location = new System.Drawing.Point(788, 5);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(123, 55);
            this.bttPrint.TabIndex = 365;
            this.bttPrint.Text = "พิมพ์";
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControlHistory);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(7, 7);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(920, 410);
            this.panelControl3.TabIndex = 29;
            // 
            // ProgramLogAccess
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Name = "ProgramLogAccess";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(934, 424);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlHistory)).EndInit();
            this.groupControlHistory.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).EndInit();
            this.panelControlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlPrint)).EndInit();
            this.panelControlPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroup;
        private DevExpress.XtraEditors.GroupControl groupControlHistory;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsername;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTimeAccess;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraEditors.PanelControl panelControlFilter;
        private DevExpress.XtraEditors.LabelControl labelControlStart;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraEditors.DateEdit dateEditStart;
        private DevExpress.XtraEditors.LabelControl labelControlTo;
        private DevExpress.XtraEditors.PanelControl panelControlPrint;
        private DevExpress.XtraEditors.SimpleButton bttPrint;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnList;
        private DevExpress.XtraEditors.SimpleButton bttSubmit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDateAccess;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRoom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTenant;

    }
}
