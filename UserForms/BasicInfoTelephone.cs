using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoTelephone : DevExpress.XtraEditors.XtraUserControl
    {
        public static XtraMessageBoxForm AddPanel;
        public static XtraMessageBoxForm UpdatePanel;
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public static GridView gridViewNick;

        public static int temp_phone_id;
        public static int room_id;

        public BasicInfoTelephone()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            DataTable PhoneMeterTbl = BusinessLogicBridge.DataStore.getPhoneMeter();
           
            gridControl2.DataSource = PhoneMeterTbl;

            gridControlNick = gridControl2;

            gridViewNick = gridView2;
            gridViewNick.OptionsBehavior.ReadOnly = true;

            gridViewNick.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewNick.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewNick.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewNick_FocusedRowChanged);

        }

        void gridViewNick_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            try
            {
                int[] rowIndex = gridViewNick.GetSelectedRows();

                DataRow CurrentRow = gridViewNick.GetDataRow(rowIndex[0]);

                temp_phone_id = Convert.ToInt16(CurrentRow["phone_id"]);
                room_id = Convert.ToInt16(CurrentRow["room_id"]);
            }
            catch { }
        }

        public static void AddPanel_ControlRemoved()
        {
            DataTable PhoneMeterTbl = BusinessLogicBridge.DataStore.getPhoneMeter();
            gridControlNick.DataSource = PhoneMeterTbl;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            AddPanel = new XtraMessageBoxForm();
            AddPanel.StartPosition = FormStartPosition.CenterScreen;

            BasicInfoTelephoneAdd UserControl = new BasicInfoTelephoneAdd();
            AddPanel.Width = (UserControl.Width + 16);
            AddPanel.Height = UserControl.Height;
            AddPanel.Controls.Add(UserControl);
            AddPanel.Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("ยืนยันการลบข้อมูล", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                BusinessLogicBridge.DataStore.delPhone(temp_phone_id, room_id);
                AddPanel_ControlRemoved();

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int[] rowIndex = gridViewNick.GetSelectedRows();

            DataRow CurrentRow = gridViewNick.GetDataRow(rowIndex[0]);

            int room_id = Convert.ToInt16(CurrentRow["room_id"]);

            UpdatePanel = new XtraMessageBoxForm();
            UpdatePanel.StartPosition = FormStartPosition.CenterScreen;

            BasicInfoTelephoneUpdate UserControl = new BasicInfoTelephoneUpdate(room_id);
            UpdatePanel.Width = (UserControl.Width + 16);
            UpdatePanel.Height = UserControl.Height;
            UpdatePanel.Controls.Add(UserControl);
            UpdatePanel.Show();
        }
    }
}
