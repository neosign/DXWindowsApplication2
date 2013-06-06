namespace DXWindowsApplication2.UserForms
{
    partial class PopupSelectTenant
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
            this.groupControlTenantList = new DevExpress.XtraEditors.GroupControl();
            this.gridControlTenant = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tenant_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_prefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_surname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_status_label = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_province_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_distict_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_modified_date = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTenantList)).BeginInit();
            this.groupControlTenantList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTenant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControlTenantList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(347, 403);
            this.panelControl2.TabIndex = 342;
            // 
            // groupControlTenantList
            // 
            this.groupControlTenantList.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlTenantList.AppearanceCaption.Options.UseFont = true;
            this.groupControlTenantList.Controls.Add(this.gridControlTenant);
            this.groupControlTenantList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlTenantList.Location = new System.Drawing.Point(0, 0);
            this.groupControlTenantList.Name = "groupControlTenantList";
            this.groupControlTenantList.Size = new System.Drawing.Size(347, 403);
            this.groupControlTenantList.TabIndex = 1;
            this.groupControlTenantList.Text = "รายการผู้เช่า";
            // 
            // gridControlTenant
            // 
            this.gridControlTenant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTenant.Location = new System.Drawing.Point(2, 22);
            this.gridControlTenant.MainView = this.gridView1;
            this.gridControlTenant.Name = "gridControlTenant";
            this.gridControlTenant.Size = new System.Drawing.Size(343, 379);
            this.gridControlTenant.TabIndex = 1;
            this.gridControlTenant.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.tenant_id,
            this.tenant_prefix,
            this.tenant_name,
            this.tenant_surname,
            this.tenant_status_label,
            this.tenant_province_id,
            this.tenant_distict_id,
            this.tenant_modified_date});
            this.gridView1.GridControl = this.gridControlTenant;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.ShowCloseButton = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tenant_id
            // 
            this.tenant_id.Caption = "tenant_id";
            this.tenant_id.FieldName = "tenant_id";
            this.tenant_id.Name = "tenant_id";
            this.tenant_id.OptionsColumn.AllowEdit = false;
            this.tenant_id.OptionsColumn.AllowFocus = false;
            this.tenant_id.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_id.OptionsColumn.AllowMove = false;
            // 
            // tenant_prefix
            // 
            this.tenant_prefix.AppearanceCell.Options.UseTextOptions = true;
            this.tenant_prefix.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_prefix.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_prefix.AppearanceHeader.Options.UseFont = true;
            this.tenant_prefix.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_prefix.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_prefix.Caption = "คำนำหน้า";
            this.tenant_prefix.FieldName = "prefix_th_label";
            this.tenant_prefix.Name = "tenant_prefix";
            this.tenant_prefix.OptionsColumn.AllowEdit = false;
            this.tenant_prefix.OptionsColumn.AllowFocus = false;
            this.tenant_prefix.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_prefix.OptionsColumn.AllowMove = false;
            this.tenant_prefix.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_prefix.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.tenant_prefix.Visible = true;
            this.tenant_prefix.VisibleIndex = 0;
            this.tenant_prefix.Width = 55;
            // 
            // tenant_name
            // 
            this.tenant_name.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_name.AppearanceHeader.Options.UseFont = true;
            this.tenant_name.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_name.Caption = "ชื่อ";
            this.tenant_name.FieldName = "tenant_name";
            this.tenant_name.Name = "tenant_name";
            this.tenant_name.OptionsColumn.AllowEdit = false;
            this.tenant_name.OptionsColumn.AllowFocus = false;
            this.tenant_name.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_name.OptionsColumn.AllowMove = false;
            this.tenant_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_name.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.tenant_name.Visible = true;
            this.tenant_name.VisibleIndex = 1;
            // 
            // tenant_surname
            // 
            this.tenant_surname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_surname.AppearanceHeader.Options.UseFont = true;
            this.tenant_surname.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_surname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_surname.Caption = "นามสกุล";
            this.tenant_surname.FieldName = "tenant_surname";
            this.tenant_surname.Name = "tenant_surname";
            this.tenant_surname.OptionsColumn.AllowEdit = false;
            this.tenant_surname.OptionsColumn.AllowFocus = false;
            this.tenant_surname.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_surname.OptionsColumn.AllowMove = false;
            this.tenant_surname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_surname.Visible = true;
            this.tenant_surname.VisibleIndex = 2;
            this.tenant_surname.Width = 84;
            // 
            // tenant_status_label
            // 
            this.tenant_status_label.AppearanceCell.Options.UseTextOptions = true;
            this.tenant_status_label.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_status_label.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_status_label.AppearanceHeader.Options.UseFont = true;
            this.tenant_status_label.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_status_label.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_status_label.Caption = "สถานะ";
            this.tenant_status_label.FieldName = "tenant_status_label";
            this.tenant_status_label.Name = "tenant_status_label";
            this.tenant_status_label.OptionsColumn.AllowEdit = false;
            this.tenant_status_label.OptionsColumn.AllowFocus = false;
            this.tenant_status_label.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_status_label.OptionsColumn.AllowMove = false;
            this.tenant_status_label.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_status_label.Visible = true;
            this.tenant_status_label.VisibleIndex = 3;
            // 
            // tenant_province_id
            // 
            this.tenant_province_id.Caption = "tenant_province_id";
            this.tenant_province_id.FieldName = "tenant_province_id";
            this.tenant_province_id.Name = "tenant_province_id";
            this.tenant_province_id.OptionsColumn.AllowEdit = false;
            this.tenant_province_id.OptionsColumn.AllowFocus = false;
            this.tenant_province_id.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_province_id.OptionsColumn.AllowMove = false;
            // 
            // tenant_distict_id
            // 
            this.tenant_distict_id.Caption = "tenant_distict_id";
            this.tenant_distict_id.FieldName = "tenant_distict_id";
            this.tenant_distict_id.Name = "tenant_distict_id";
            this.tenant_distict_id.OptionsColumn.AllowEdit = false;
            this.tenant_distict_id.OptionsColumn.AllowFocus = false;
            this.tenant_distict_id.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_distict_id.OptionsColumn.AllowMove = false;
            // 
            // tenant_modified_date
            // 
            this.tenant_modified_date.Caption = "date_modified";
            this.tenant_modified_date.FieldName = "tenant_modified_date";
            this.tenant_modified_date.Name = "tenant_modified_date";
            this.tenant_modified_date.OptionsColumn.AllowEdit = false;
            this.tenant_modified_date.OptionsColumn.AllowFocus = false;
            this.tenant_modified_date.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_modified_date.OptionsColumn.AllowMove = false;
            // 
            // PopupSelectTenant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 403);
            this.Controls.Add(this.panelControl2);
            this.Name = "PopupSelectTenant";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTenantList)).EndInit();
            this.groupControlTenantList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTenant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControlTenantList;
        private DevExpress.XtraGrid.GridControl gridControlTenant;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_prefix;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_name;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_surname;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_status_label;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_province_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_distict_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_modified_date;

    }
}
