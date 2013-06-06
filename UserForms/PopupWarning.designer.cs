namespace DXWindowsApplication2.UserForms
{
    partial class PopupWarning
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.list_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.list_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.list_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.list_roomname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.list_detail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.list_help = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControlWarning = new DevExpress.XtraEditors.GroupControl();
            this.bttSetting = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlWarning)).BeginInit();
            this.groupControlWarning.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gridControlList);
            this.panelControl2.Controls.Add(this.groupControlWarning);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(767, 403);
            this.panelControl2.TabIndex = 342;
            // 
            // gridControlList
            // 
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 69);
            this.gridControlList.MainView = this.gridView1;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.Size = new System.Drawing.Size(767, 334);
            this.gridControlList.TabIndex = 2;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.list_id,
            this.list_date,
            this.list_name,
            this.list_roomname,
            this.list_detail,
            this.list_help,
            this.gridColumnNew});
            this.gridView1.GridControl = this.gridControlList;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.ShowCloseButton = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.list_detail, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // list_id
            // 
            this.list_id.AppearanceCell.Options.UseTextOptions = true;
            this.list_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_id.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_id.AppearanceHeader.Options.UseFont = true;
            this.list_id.AppearanceHeader.Options.UseTextOptions = true;
            this.list_id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_id.Caption = "ลำดับ";
            this.list_id.FieldName = "list_id";
            this.list_id.Name = "list_id";
            this.list_id.OptionsColumn.AllowEdit = false;
            this.list_id.OptionsColumn.AllowFocus = false;
            this.list_id.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.list_id.OptionsColumn.AllowMove = false;
            this.list_id.Visible = true;
            this.list_id.VisibleIndex = 0;
            this.list_id.Width = 44;
            // 
            // list_date
            // 
            this.list_date.AppearanceCell.Options.UseTextOptions = true;
            this.list_date.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_date.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_date.AppearanceHeader.Options.UseFont = true;
            this.list_date.AppearanceHeader.Options.UseTextOptions = true;
            this.list_date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_date.Caption = "วันที่แจ้งเตือน";
            this.list_date.FieldName = "list_date";
            this.list_date.Name = "list_date";
            this.list_date.OptionsColumn.AllowEdit = false;
            this.list_date.OptionsColumn.AllowFocus = false;
            this.list_date.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.list_date.OptionsColumn.AllowMove = false;
            this.list_date.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.list_date.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.list_date.Visible = true;
            this.list_date.VisibleIndex = 2;
            this.list_date.Width = 102;
            // 
            // list_name
            // 
            this.list_name.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_name.AppearanceHeader.Options.UseFont = true;
            this.list_name.AppearanceHeader.Options.UseTextOptions = true;
            this.list_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_name.Caption = "รายการ";
            this.list_name.FieldName = "list_name";
            this.list_name.Name = "list_name";
            this.list_name.OptionsColumn.AllowEdit = false;
            this.list_name.OptionsColumn.AllowFocus = false;
            this.list_name.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.list_name.OptionsColumn.AllowMove = false;
            this.list_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.list_name.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.list_name.Visible = true;
            this.list_name.VisibleIndex = 3;
            this.list_name.Width = 174;
            // 
            // list_roomname
            // 
            this.list_roomname.AppearanceCell.Options.UseTextOptions = true;
            this.list_roomname.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_roomname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_roomname.AppearanceHeader.Options.UseFont = true;
            this.list_roomname.AppearanceHeader.Options.UseTextOptions = true;
            this.list_roomname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_roomname.Caption = "ชื่อห้อง";
            this.list_roomname.FieldName = "list_roomname";
            this.list_roomname.Name = "list_roomname";
            this.list_roomname.OptionsColumn.AllowEdit = false;
            this.list_roomname.OptionsColumn.AllowFocus = false;
            this.list_roomname.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.list_roomname.OptionsColumn.AllowMove = false;
            this.list_roomname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.list_roomname.Visible = true;
            this.list_roomname.VisibleIndex = 4;
            this.list_roomname.Width = 82;
            // 
            // list_detail
            // 
            this.list_detail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_detail.AppearanceHeader.Options.UseFont = true;
            this.list_detail.AppearanceHeader.Options.UseTextOptions = true;
            this.list_detail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_detail.Caption = "รายละเอียด";
            this.list_detail.FieldName = "list_detail";
            this.list_detail.Name = "list_detail";
            this.list_detail.OptionsColumn.AllowEdit = false;
            this.list_detail.OptionsColumn.AllowFocus = false;
            this.list_detail.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.list_detail.OptionsColumn.AllowMove = false;
            this.list_detail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.list_detail.Visible = true;
            this.list_detail.VisibleIndex = 5;
            this.list_detail.Width = 232;
            // 
            // list_help
            // 
            this.list_help.AppearanceCell.Image = global::DXWindowsApplication2.Properties.Resources.Add;
            this.list_help.AppearanceCell.Options.UseImage = true;
            this.list_help.AppearanceCell.Options.UseTextOptions = true;
            this.list_help.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_help.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_help.AppearanceHeader.Options.UseFont = true;
            this.list_help.AppearanceHeader.Options.UseTextOptions = true;
            this.list_help.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.list_help.Caption = "?";
            this.list_help.FieldName = "list_help_title";
            this.list_help.Name = "list_help";
            this.list_help.OptionsColumn.AllowEdit = false;
            this.list_help.OptionsColumn.AllowFocus = false;
            this.list_help.ToolTip = "list_help";
            this.list_help.Visible = true;
            this.list_help.VisibleIndex = 6;
            this.list_help.Width = 77;
            // 
            // gridColumnNew
            // 
            this.gridColumnNew.AppearanceCell.BorderColor = System.Drawing.Color.Transparent;
            this.gridColumnNew.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumnNew.AppearanceCell.Options.UseBorderColor = true;
            this.gridColumnNew.AppearanceCell.Options.UseForeColor = true;
            this.gridColumnNew.Caption = " ";
            this.gridColumnNew.FieldName = "new_item";
            this.gridColumnNew.Name = "gridColumnNew";
            this.gridColumnNew.OptionsColumn.AllowFocus = false;
            this.gridColumnNew.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnNew.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnNew.OptionsColumn.AllowMove = false;
            this.gridColumnNew.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnNew.OptionsColumn.ReadOnly = true;
            this.gridColumnNew.Visible = true;
            this.gridColumnNew.VisibleIndex = 1;
            this.gridColumnNew.Width = 38;
            // 
            // groupControlWarning
            // 
            this.groupControlWarning.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlWarning.AppearanceCaption.Options.UseFont = true;
            this.groupControlWarning.Controls.Add(this.bttSetting);
            this.groupControlWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlWarning.Location = new System.Drawing.Point(0, 0);
            this.groupControlWarning.Name = "groupControlWarning";
            this.groupControlWarning.Size = new System.Drawing.Size(767, 69);
            this.groupControlWarning.TabIndex = 1;
            this.groupControlWarning.Text = "รายการแจ้งเตือน";
            // 
            // bttSetting
            // 
            this.bttSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttSetting.Image = global::DXWindowsApplication2.Properties.Resources.savedisk;
            this.bttSetting.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.bttSetting.Location = new System.Drawing.Point(575, 25);
            this.bttSetting.Name = "bttSetting";
            this.bttSetting.Size = new System.Drawing.Size(187, 39);
            this.bttSetting.TabIndex = 464;
            this.bttSetting.Text = "ตั้งค่ารายการแจ้งเตือน";
            // 
            // PopupWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 403);
            this.Controls.Add(this.panelControl2);
            this.Name = "PopupWarning";
            this.Text = "แจ้งเตือน";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlWarning)).EndInit();
            this.groupControlWarning.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControlWarning;
        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn list_id;
        private DevExpress.XtraGrid.Columns.GridColumn list_date;
        private DevExpress.XtraGrid.Columns.GridColumn list_name;
        private DevExpress.XtraGrid.Columns.GridColumn list_roomname;
        private DevExpress.XtraGrid.Columns.GridColumn list_detail;
        private DevExpress.XtraEditors.SimpleButton bttSetting;
        private DevExpress.XtraGrid.Columns.GridColumn list_help;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNew;

    }
}
