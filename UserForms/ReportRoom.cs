using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class ReportRoom : uBase
    {
        public ReportRoom()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ReportRoom_Load);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
        }

        void ReportRoom_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
            initDropDownRoomStatus();
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            lookUpEditRoomFrom.Enabled = true;

            DataTable RoomTable = BusinessLogicBridge.DataStore.getAllRoom(lookUpEditBuilding.EditValue.To<int>(), "All");
            lookUpEditRoomFrom.Properties.DisplayMember = "coderef";
            lookUpEditRoomFrom.Properties.ValueMember = "room_id";
            lookUpEditRoomFrom.Properties.NullText = getLanguage("_select_room");
            lookUpEditRoomFrom.Properties.DataSource = RoomTable;
            lookUpEditRoomFrom.ItemIndex = 0;

            lookUpEditRoomTo.Enabled = true;
            lookUpEditRoomTo.Properties.DisplayMember = "coderef";
            lookUpEditRoomTo.Properties.ValueMember = "room_id";
            lookUpEditRoomTo.Properties.NullText = getLanguage("_select_room");
            lookUpEditRoomTo.Properties.DataSource = RoomTable;
            lookUpEditRoomTo.ItemIndex = RoomTable.Rows.Count;

            bttExport.Enabled   = true;
            bttPrint.Enabled    = true;
        }

        #region Function

        protected void ExportExcelManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.room PrintRoom = new DXWindowsApplication2.ReportDocuments.room();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Room_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintRoom.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

            PrintRoom.ExportToXls(pathname);
            PrintRoom.ShowPreview();
        }

        protected void ExportPDFManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.room PrintRoom = new DXWindowsApplication2.ReportDocuments.room();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Room_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintRoom.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

            PrintRoom.ExportToPdf(pathname);
            PrintRoom.ShowPreview();
        }


        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }

        void initDropDownRoomStatus() {

            DataTable RoomStatusTable = BusinessLogicBridge.DataStore.getAllRoomStatus();

            RoomStatusTable.Rows.Add(0, "All Status");

            lookUpEditRoomStatus.Properties.DisplayMember = "room_status_label";
            lookUpEditRoomStatus.Properties.ValueMember = "room_status";
            lookUpEditRoomStatus.Properties.NullText = getLanguage("_select_building");
            lookUpEditRoomStatus.Properties.DataSource = RoomStatusTable;
            lookUpEditRoomStatus.EditValue = 0;
        }

        private DataTable validateData()
        {
            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));


            if (lookUpEditBuilding.EditValue == null)
            {
                label = lbBuilding.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditBuilding.Focus();
                    focus = true;
                }
            }

            return _ValidateTable;
        }


        #endregion

        private void bttExport_Click(object sender, EventArgs e)
        {
            DataTable _ValidateTable = validateData();
            if (_ValidateTable.Rows.Count > 0)
            {
                String message = "";
                for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                {
                    message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                }
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }

            DataTable RoomTable = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), lookUpEditRoomStatus.EditValue.To<int>(), lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());

            if (RoomTable.Rows.Count > 0)
            {
                ExportExcelManual(RoomTable);
            }
            else {
                utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                return;
            }
        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            DataTable _ValidateTable = validateData();
            if (_ValidateTable.Rows.Count > 0)
            {
                String message = "";
                for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                {
                    message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                }
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }

            DataTable RoomTable = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), lookUpEditRoomStatus.EditValue.To<int>(), lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());

            if (RoomTable.Rows.Count > 0)
            {
                ExportPDFManual(RoomTable);
            }
            else
            {
                utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                return;
            }
        }
    }
}
