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
    public partial class PopupWarning : uFormBase
    {
        public PopupWarning()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupWarning_Load);
            //
            this.bttSetting.Click += new EventHandler(bttSetting_Click);
        }

        void bttSetting_Click(object sender, EventArgs e)
        {
            utilClass.showPopUpWarningSetting(this);
            //
            initList();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            initList();
            //
            setLangThis();
        }

        void PopupWarning_Load(object sender, EventArgs e)
        {
            initList();
            //
            setLangThis();
        }

        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        void initList()
        {
             DataTable warningItem = utilClass.getWarningList();
             
             gridControlList.DataSource = warningItem;
        }

        public void setLangThis()
        {
            //
            this.Text = getLanguage("_warning_form");
            //
            this.groupControlWarning.Text = getLanguage("_warning_list");
            //
            // Grid
            this.list_id.Caption = getLanguage("_no");
            //
            this.list_id.Visible = false;
            //
            this.list_date.Caption = getLanguage("_warning_date");
            this.list_name.Caption = getLanguage("_list");
            this.list_roomname.Caption = getLanguage("_room_name");
            this.list_detail.Caption = getLanguage("_warning_detail");
            //
            bttSetting.Text = getLanguage("_warning_setting");
        }
    }
}
