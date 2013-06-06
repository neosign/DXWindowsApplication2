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
    public partial class ReportPhoneConsummation : uBase
    {
        public ReportPhoneConsummation()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ReportPhoneConsummation_Load);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
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

        }

        void ReportPhoneConsummation_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }

        protected void ExportExcelConsumationManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.phoneConsumation PrintElectric = new DXWindowsApplication2.ReportDocuments.phoneConsumation();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Phone_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            //PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
        }

        protected void ExportExcelDetailManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.phoneDetail PrintElectric = new DXWindowsApplication2.ReportDocuments.phoneDetail();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "PhoneDetail_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            //PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
        }        

        protected void ExportExcelChargeManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.phoneCharge PrintElectric = new DXWindowsApplication2.ReportDocuments.phoneCharge();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "PhoneCharge_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            //PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
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

            #region check case report view
            
                if (dateEditFromDate.EditValue == null)
                {
                    label = lbDuedate.Text;
                    message = getLanguage("_msg_1001");
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        dateEditFromDate.Focus();
                        focus = true;
                    }
                }

                if (dateEditTodate.EditValue == null)
                {
                    label = lbTodate.Text;
                    message = getLanguage("_msg_1001");
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        dateEditTodate.Focus();
                        focus = true;
                    }
                }
            
            
            #endregion

            return _ValidateTable;
        }

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


            DataTable Room = new DataTable();

            if (radioGroupPhoneType.SelectedIndex == 0)
            {

                if (radioGroupSumType.SelectedIndex == 0)
                {
                    Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                    ExportExcelConsumationManual(Room);
                }
                else
                {
                    Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                    ExportExcelChargeManual(Room);
                }
            }
            else {
                Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                ExportExcelDetailManual(Room);
            }
        }

        private void radioGroupPhoneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupPhoneType.SelectedIndex == 1)
            {
                gbSummation.Enabled = false;
            }
            else {
                gbSummation.Enabled = true;
            }
        }
    }
}
