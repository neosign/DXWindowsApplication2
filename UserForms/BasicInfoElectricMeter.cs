using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoElectricMeter : DevExpress.XtraEditors.XtraUserControl
    {
        public static XtraMessageBoxForm AddPanel;
        public static XtraMessageBoxForm UpdatePanel;
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public static GridView gridViewNick;

        public static int temp_meter_id;
        public static int room_id;

        public BasicInfoElectricMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            DataTable _electricityMeterTable = new DataTable();
            DataTable ElectricityMeterTbl = BusinessLogicBridge.DataStore.getElectricityMeter();
            ElectricityMeterTbl.Columns.Add("colElecStatus_text", typeof(string));
            ElectricityMeterTbl.Columns.Add("colCutStatus_text", typeof(string));

            gridViewNick = gridView3;
            gridViewNick.OptionsBehavior.ReadOnly = true;

            
            for (int i = 0; i < ElectricityMeterTbl.Rows.Count; i++)
            {
                string change = ElectricityMeterTbl.Rows[i]["meter_status"].ToString();
                string cut      = ElectricityMeterTbl.Rows[i]["meter_cut"].ToString();
                
                if (cut == "1")
                {
                    ElectricityMeterTbl.Rows[i]["colCutStatus_text"] = "ต่อไฟ";
                   
                }
                else
                {
                    ElectricityMeterTbl.Rows[i]["colCutStatus_text"] = "ตัดไฟ";                 

                }
                gridView3.RowCellStyle +=new RowCellStyleEventHandler(gridView3_RowCellStyle);


                if (change == "1")
                {
                    ElectricityMeterTbl.Rows[i]["colElecStatus_text"] = "การสื่อสารสมบูรณ์";
                    
                }
                else
                {
                    ElectricityMeterTbl.Rows[i]["colElecStatus_text"] = "การสื่อสารผิดพลาด";
                    
                }
            }

            //ElectricityMeterTbl
            gridControlNick = gridControl1;
            gridControlNick.DataSource = ElectricityMeterTbl;
            gridControlNick.UseEmbeddedNavigator = true;

            gridViewNick.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewNick.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

            gridViewNick.FocusedRowChanged +=new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewNick_FocusedRowChanged);
        }

        void gridViewNick_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            try
            {
                int[] rowIndex = gridViewNick.GetSelectedRows();

                DataRow CurrentRow = gridViewNick.GetDataRow(rowIndex[0]);

                temp_meter_id = Convert.ToInt16(CurrentRow["meter_id"]);
                room_id = Convert.ToInt16(CurrentRow["room_id"]);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }
        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            var oView = (GridView)sender;
            if (e.Column.FieldName == "colElecStatus_text")
            {
                
                var ElectricValue = oView.GetRowCellDisplayText(e.RowHandle, "colElecStatus_text").ToString();

                if (ElectricValue == "การสื่อสารผิดพลาด")
                {
                    e.Appearance.BackColor = Color.FromName("Red");
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.Appearance.ForeColor = Color.FromName("White");
                }
            }

            if (e.Column.FieldName == "colCutStatus_text")
            {
                var CutValue = oView.GetRowCellDisplayText(e.RowHandle, "colCutStatus_text").ToString();
                if (CutValue == "ตัดไฟ")
                {
                    e.Appearance.BackColor = Color.FromName("Red");
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.Appearance.ForeColor = Color.FromName("White");
                }
            }
        }

        public static void gridView3_RowStyle(object sender,DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["colCutStatus_text"]);
                if (category == "ตัดไฟ")
                {
                    e.Appearance.BackColor = Color.FromName("Red");
                    e.Appearance.BackColor2 = Color.SeaShell;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AddPanel = new XtraMessageBoxForm();
            AddPanel.StartPosition = FormStartPosition.CenterScreen;

            BasicInfoElectricMeterAdd UserControl = new BasicInfoElectricMeterAdd();
            AddPanel.Width = (UserControl.Width + 16);
            AddPanel.Height = UserControl.Height;
            AddPanel.Controls.Add(UserControl);
            AddPanel.Show();
        }

        public static void AddPanel_ControlRemoved()
        {
            DataTable ElectricityMeterTbl = BusinessLogicBridge.DataStore.getElectricityMeter();

            ElectricityMeterTbl.Columns.Add("colElecStatus_text", typeof(string));
            ElectricityMeterTbl.Columns.Add("colCutStatus_text", typeof(string));


            for (int i = 0; i < ElectricityMeterTbl.Rows.Count; i++)
            {
                string change = ElectricityMeterTbl.Rows[i]["meter_status"].ToString();
                string cut = ElectricityMeterTbl.Rows[i]["meter_cut"].ToString();

                if (cut == "1")
                {
                    ElectricityMeterTbl.Rows[i]["colCutStatus_text"] = "ต่อไฟ";

                }
                else
                {
                    ElectricityMeterTbl.Rows[i]["colCutStatus_text"] = "ตัดไฟ";

                }
                //gridViewNick.RowCellStyle += new RowCellStyleEventHandler(gridView3_RowCellStyle);


                if (change == "1")
                {
                    ElectricityMeterTbl.Rows[i]["colElecStatus_text"] = "การสื่อสารสมบูรณ์";

                }
                else
                {
                    ElectricityMeterTbl.Rows[i]["colElecStatus_text"] = "การสื่อสารผิดพลาด";

                }
            }
            gridControlNick.DataSource = ElectricityMeterTbl;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("ยืนยันการลบข้อมูล", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                BusinessLogicBridge.DataStore.delElectricMeter(temp_meter_id, room_id);
                AddPanel_ControlRemoved();
                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int[] rowIndex = gridView3.GetSelectedRows();

            DataRow CurrentRow = gridView3.GetDataRow(rowIndex[0]);

            int room_id =  Convert.ToInt16(CurrentRow["room_id"]);

            UpdatePanel = new XtraMessageBoxForm();
            UpdatePanel.StartPosition = FormStartPosition.CenterScreen;

            BasicInfoElectricMeterUpdate UserControl = new BasicInfoElectricMeterUpdate(room_id);
            UpdatePanel.Width = (UserControl.Width + 16);
            UpdatePanel.Height = UserControl.Height;
            UpdatePanel.Controls.Add(UserControl);
            UpdatePanel.Show();

            //getRoomById

        }
    }
}
