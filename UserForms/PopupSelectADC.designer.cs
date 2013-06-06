namespace DXWindowsApplication2.UserForms
{
    partial class PopupSelectADC
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bttOk = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlTenant = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tenant_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_surname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_status_label = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_province_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_distict_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenant_modified_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grid_ADC_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTenantList)).BeginInit();
            this.groupControlTenantList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTenant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControlTenantList);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(347, 342);
            this.panelControl2.TabIndex = 342;
            // 
            // groupControlTenantList
            // 
            this.groupControlTenantList.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlTenantList.AppearanceCaption.Options.UseFont = true;
            this.groupControlTenantList.Controls.Add(this.panelControl1);
            this.groupControlTenantList.Controls.Add(this.gridControlTenant);
            this.groupControlTenantList.Controls.Add(this.panelControl3);
            this.groupControlTenantList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlTenantList.Location = new System.Drawing.Point(0, 0);
            this.groupControlTenantList.Name = "groupControlTenantList";
            this.groupControlTenantList.Padding = new System.Windows.Forms.Padding(10);
            this.groupControlTenantList.ShowCaption = false;
            this.groupControlTenantList.Size = new System.Drawing.Size(347, 342);
            this.groupControlTenantList.TabIndex = 1;
            this.groupControlTenantList.Text = "เลือกเอดีซีที่ต้องการแทนที่";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.bttCancel);
            this.panelControl1.Controls.Add(this.bttOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(12, 264);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(323, 63);
            this.panelControl1.TabIndex = 2;
            // 
            // bttCancel
            // 
            this.bttCancel.Location = new System.Drawing.Point(218, 23);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(75, 23);
            this.bttCancel.TabIndex = 1;
            this.bttCancel.Text = "Cancel";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // bttOk
            // 
            this.bttOk.Location = new System.Drawing.Point(31, 23);
            this.bttOk.Name = "bttOk";
            this.bttOk.Size = new System.Drawing.Size(75, 23);
            this.bttOk.TabIndex = 0;
            this.bttOk.Text = "OK";
            this.bttOk.Click += new System.EventHandler(this.bttOk_Click);
            // 
            // gridControlTenant
            // 
            this.gridControlTenant.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControlTenant.Location = new System.Drawing.Point(12, 65);
            this.gridControlTenant.MainView = this.gridView1;
            this.gridControlTenant.Margin = new System.Windows.Forms.Padding(20);
            this.gridControlTenant.Name = "gridControlTenant";
            this.gridControlTenant.Size = new System.Drawing.Size(323, 199);
            this.gridControlTenant.TabIndex = 1;
            this.gridControlTenant.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.tenant_id,
            this.tenant_name,
            this.tenant_surname,
            this.tenant_status_label,
            this.tenant_province_id,
            this.tenant_distict_id,
            this.tenant_modified_date,
            this.grid_ADC_ID});
            this.gridView1.GridControl = this.gridControlTenant;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
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
            // tenant_name
            // 
            this.tenant_name.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_name.AppearanceHeader.Options.UseFont = true;
            this.tenant_name.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_name.Caption = "Name";
            this.tenant_name.FieldName = "device_adc_name";
            this.tenant_name.Name = "tenant_name";
            this.tenant_name.OptionsColumn.AllowEdit = false;
            this.tenant_name.OptionsColumn.AllowFocus = false;
            this.tenant_name.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_name.OptionsColumn.AllowMove = false;
            this.tenant_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_name.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.tenant_name.Visible = true;
            this.tenant_name.VisibleIndex = 0;
            // 
            // tenant_surname
            // 
            this.tenant_surname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tenant_surname.AppearanceHeader.Options.UseFont = true;
            this.tenant_surname.AppearanceHeader.Options.UseTextOptions = true;
            this.tenant_surname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenant_surname.Caption = "Serial";
            this.tenant_surname.FieldName = "device_adc_serial";
            this.tenant_surname.Name = "tenant_surname";
            this.tenant_surname.OptionsColumn.AllowEdit = false;
            this.tenant_surname.OptionsColumn.AllowFocus = false;
            this.tenant_surname.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_surname.OptionsColumn.AllowMove = false;
            this.tenant_surname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_surname.Visible = true;
            this.tenant_surname.VisibleIndex = 1;
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
            this.tenant_status_label.Caption = "MAC Address";
            this.tenant_status_label.FieldName = "device_adc_mac";
            this.tenant_status_label.Name = "tenant_status_label";
            this.tenant_status_label.OptionsColumn.AllowEdit = false;
            this.tenant_status_label.OptionsColumn.AllowFocus = false;
            this.tenant_status_label.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.tenant_status_label.OptionsColumn.AllowMove = false;
            this.tenant_status_label.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.tenant_status_label.Visible = true;
            this.tenant_status_label.VisibleIndex = 2;
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
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(12, 12);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(323, 53);
            this.panelControl3.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(58, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(207, 23);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "เลือกเอดีซีที่ต้องการแทนที่";
            // 
            // grid_ADC_ID
            // 
            this.grid_ADC_ID.Caption = "ADC_ID";
            this.grid_ADC_ID.FieldName = "device_adc_id";
            this.grid_ADC_ID.Name = "grid_ADC_ID";
            // 
            // PopupSelectADC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 342);
            this.Controls.Add(this.panelControl2);
            this.Name = "PopupSelectADC";
            this.Text = "Replace";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTenantList)).EndInit();
            this.groupControlTenantList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTenant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControlTenantList;
        private DevExpress.XtraGrid.GridControl gridControlTenant;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_name;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_surname;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_status_label;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_province_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_distict_id;
        private DevExpress.XtraGrid.Columns.GridColumn tenant_modified_date;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.SimpleButton bttOk;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn grid_ADC_ID;

    }
}
