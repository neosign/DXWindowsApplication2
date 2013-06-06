using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public partial class PopupSelectTenant : uFormBase
    {
        public PopupSelectTenant()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupSelectTenant_Load);
            //
            this.gridView1.RowClick +=new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView1_RowClick);
            //
        }

        public DataRow drTenant = null;

        public override void Refresh()
        {
            base.Refresh();
            //
            initTenant();
            setLangThis();
        }

        void PopupSelectTenant_Load(object sender, EventArgs e)
        {            
            initTenant();
            setLangThis();
        }
        
        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            drTenant = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            //
            this.DialogResult = DialogResult.OK;
        }

        void initTenant()
        {
            DataTable TenantTable = BusinessLogicBridge.DataStore.Tenant_getUnique();
            gridControlTenant.DataSource = TenantTable;
        }

        public void setLangThis()
        {
            //
            this.Name = getLanguage("_tenant_info");
            //
            this.groupControlTenantList.Text = getLanguage("_tenant");
            //
            // Grid
            this.tenant_prefix.Caption = getLanguage("_prefix");
            this.tenant_prefix.FieldName = "prefix_" + current_lang + "_label";
            this.tenant_name.Caption = getLanguage("_firstname");
            this.tenant_surname.Caption = getLanguage("_lastname");
            this.tenant_status_label.Caption = getLanguage("_status");
            this.tenant_status_label.FieldName = "tenant_status_" + current_lang + "_label";
        }
    }
}
