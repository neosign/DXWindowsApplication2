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
    public partial class BasicInfoWater : DevExpress.XtraEditors.XtraUserControl
    {
        public static XtraMessageBoxForm AddPanel;
        public static XtraMessageBoxForm UpdatePanel;
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public static GridView gridViewNick;

        public static int temp_water_id;
        public static int room_id;

        public BasicInfoWater()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            DataTable _waterMeterTable = new DataTable();
            DataTable WaterMeterTbl = BusinessLogicBridge.DataStore.getWaterMeter();
            gridControl2.DataSource = WaterMeterTbl;

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

                temp_water_id = Convert.ToInt16(CurrentRow["water_id"]);
                room_id = Convert.ToInt16(CurrentRow["room_id"]);
            }
            catch { }
        }

        public static void AddPanel_ControlRemoved()
        {
            DataTable WaterMeterTbl = BusinessLogicBridge.DataStore.getWaterMeter();
            gridControlNick.DataSource = WaterMeterTbl;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            AddPanel = new XtraMessageBoxForm();
            AddPanel.StartPosition = FormStartPosition.CenterScreen;

            BasicInfoWaterMeterAdd UserControl = new BasicInfoWaterMeterAdd();
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
                BusinessLogicBridge.DataStore.delWaterMeter(temp_water_id, room_id);
                AddPanel_ControlRemoved();

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            UpdatePanel = new XtraMessageBoxForm();
            UpdatePanel.StartPosition = FormStartPosition.CenterScreen;

            if (room_id == 0)
            {
                XtraMessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการแก้ไข");
            }
            else
            {
                BasicInfoWaterMeterUpdate UserControl = new BasicInfoWaterMeterUpdate(room_id);
                UpdatePanel.Width = (UserControl.Width + 16);
                UpdatePanel.Height = UserControl.Height;
                UpdatePanel.Controls.Add(UserControl);
                UpdatePanel.Show();
            }
        }
    }
}
