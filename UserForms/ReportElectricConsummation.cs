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
using System.Threading;

namespace DXWindowsApplication2.UserForms
{
    public partial class ReportElectricConsummation : uBase
    {
        public ReportElectricConsummation()
        {
            InitializeComponent();


            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ReportElectricConsummation_Load);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
            radioGroupMeterType.SelectedIndexChanged += new EventHandler(radioGroupMeterType_SelectedIndexChanged);

            radioGroupMeter.SelectedIndexChanged += new EventHandler(radioGroupMeter_SelectedIndexChanged);
            lookUpEditRoomFrom.EditValueChanged += new EventHandler(lookUpEditRoomFrom_EditValueChanged);

        }

        void lookUpEditRoomFrom_EditValueChanged(object sender, EventArgs e)
        {

            if (radioGroupMeterType.SelectedIndex == 2) {
                lookUpEditRoomTo.EditValue = lookUpEditRoomFrom.EditValue;
            }
        }

        void radioGroupMeter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupMeter.SelectedIndex == 0)
            {
                panelControlUnitMeter.Enabled = false;
                dateEditUnitHour.Enabled = true;
            }
            else {
                panelControlUnitMeter.Enabled = true;
                dateEditUnitHour.Enabled = false;
            }
        }

        void radioGroupMeterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lookUpEditBuilding.EditValue != null)
            {
                if (radioGroupMeterType.SelectedIndex == 0)
                {
                    lookUpEditRoomTo.Enabled = true;
                    dateEditFromDate.Enabled = true;
                    dateEditTodate.Enabled = true;

                    gbDetail.Enabled = false;
                }
                else
                {
                    lookUpEditRoomTo.EditValue = lookUpEditRoomFrom.EditValue;
                    lookUpEditRoomTo.Enabled = false;
                    dateEditFromDate.Enabled = false;
                    dateEditTodate.Enabled = false;

                    gbDetail.Enabled = true;
                }
            }
            else {
                dateEditFromDate.Enabled = false;
                dateEditTodate.Enabled = false;
                dateEditFromDate.Enabled = false;
                dateEditTodate.Enabled = false;
                gbDetail.Enabled = false;
            }
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

            if (radioGroupMeterType.SelectedIndex == 1)
            {
                lookUpEditRoomTo.EditValue = lookUpEditRoomFrom.EditValue;
                lookUpEditRoomTo.Enabled = false;
                dateEditFromDate.Enabled = false;
                dateEditTodate.Enabled = false;

                gbDetail.Enabled = true;
            }
            else {
                dateEditFromDate.Enabled = true;
                dateEditTodate.Enabled = true;
            }


        }

        void ReportElectricConsummation_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
        }

        #region setup

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }

        protected void ExportExcelManual(DataTable RoomTable)
        {
            DataTable GeneralInfo   = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath         = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath                = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.electricAccumulate PrintElectric = new DXWindowsApplication2.ReportDocuments.electricAccumulate();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Electric_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

            PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
        }

        protected void ExportExcelHourManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            DataTable MeterInfoDT = BusinessLogicBridge.DataStore.getEMeterById(RoomTable.Rows[0]["current_electricity_id"].To<int>());

            string pathname = "";

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }


            ReportDocuments.electricHourUnit PrintElectric = new DXWindowsApplication2.ReportDocuments.electricHourUnit();

                pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "ElectricUnitHour_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditHour.EditValue.To<DateTime>());

                //PrintElectric.ExportToXls(pathname);
                PrintElectric.ShowPreview();
            
            // Building, roomFrom, roomTo, dateFrom, dateTo


        }

        protected void ExportExcelUnitHourManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            DataTable MeterInfoDT = BusinessLogicBridge.DataStore.getEMeterById(RoomTable.Rows[0]["current_electricity_id"].To<int>());

            string pathname = "";

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            if (MeterInfoDT.Rows[0]["meter_models"].ToString().Contains("SX") == true)
            {

                ReportDocuments.electricHourSX PrintElectric = new DXWindowsApplication2.ReportDocuments.electricHourSX();

                pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "ElectricHourSX_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditUnitHour.EditValue.To<DateTime>());

                //PrintElectric.ExportToXls(pathname);
                PrintElectric.ShowPreview();
            }
            else {
                ReportDocuments.electricHourMX PrintElectric = new DXWindowsApplication2.ReportDocuments.electricHourMX();

                pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "ElectricHourMX_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditUnitHour.EditValue.To<DateTime>());

                //PrintElectric.ExportToXls(pathname);
                PrintElectric.ShowPreview();
            }
            // Building, roomFrom, roomTo, dateFrom, dateTo

            
        }

        protected void ExportExcelDailyManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.electricDay PrintElectric = new DXWindowsApplication2.ReportDocuments.electricDay();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "ElectricDaily_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditDay.EditValue.To<DateTime>());

            //PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
        }

        protected void ExportExcelMonthlyManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.electricMonth PrintElectric = new DXWindowsApplication2.ReportDocuments.electricMonth();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "ElectricMonthly_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditMonthFrom.EditValue.To<DateTime>(), dateEditMonthTo.EditValue.To<DateTime>());

            //PrintElectric.ExportToXls(pathname);
            PrintElectric.ShowPreview();
        }



        protected void ExportPDFManual(DataTable RoomTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.electricAccumulate PrintElectric = new DXWindowsApplication2.ReportDocuments.electricAccumulate();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Electric_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintElectric.loopGenDataRow(RoomTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);


            PrintElectric.ExportToPdf(pathname);
            PrintElectric.ShowPreview();
        }

        #endregion

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
            if (radioGroupMeterType.SelectedIndex == 0)
            {
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
            }
            else {
                if (radioGroupMeter.SelectedIndex == 1) {

                    switch (radioGroupMonthHour.SelectedIndex) { 
                        
                        case 0:
                            if (dateEditHour.EditValue == null)
                            {
                                label = radioGroupMonthHour.Properties.Items[radioGroupMonthHour.SelectedIndex].ToString();
                                message = getLanguage("_msg_1001");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    dateEditHour.Focus();
                                    focus = true;
                                }
                            }
                            break;
                        case 1:
                            if (dateEditDay.EditValue == null)
                            {
                                label = radioGroupMonthHour.Properties.Items[radioGroupMonthHour.SelectedIndex].ToString();
                                message = getLanguage("_msg_1001");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    dateEditDay.Focus();
                                    focus = true;
                                }
                            }
                            break;
                        case 2:
                            if (dateEditMonthFrom.EditValue == null)
                            {
                                label = radioGroupMonthHour.Properties.Items[radioGroupMonthHour.SelectedIndex].ToString();
                                message = getLanguage("_msg_1001");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    dateEditMonthFrom.Focus();
                                    focus = true;
                                }
                            }

                            if (dateEditMonthTo.EditValue == null)
                            {
                                label = labelControlMonthTo.Text; //radioGroupMonthHour.Properties.Items[radioGroupMonthHour.SelectedIndex].ToString();
                                message = getLanguage("_msg_1001");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    dateEditMonthTo.Focus();
                                    focus = true;
                                }
                            }                            
                            break;
                        default :
                            break;
                        
                    }
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

            if (radioGroupMeterType.SelectedIndex == 0)
            {
                // where buildingID , roomIDFrom , roomIDTo, DatetimeFrom
                DataTable EMaxFromDT = BusinessLogicBridge.DataStore.getReportEMax(lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditFromDate.EditValue.ToString());
                // where buildingID , roomIDFrom , roomIDTo, DatetimeTo
                DataTable EMaxToDT = BusinessLogicBridge.DataStore.getReportEMax(lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditTodate.EditValue.ToString());

                DataTable EmaxReportDT = new DataTable();

                EmaxReportDT.Columns.Add("report_roomLabel", typeof(string));
                EmaxReportDT.Columns.Add("report_meterModel", typeof(string));
                EmaxReportDT.Columns.Add("report_meterSerial", typeof(string));
                EmaxReportDT.Columns.Add("report_beginRecord", typeof(string));
                EmaxReportDT.Columns.Add("report_beginUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_endRecord", typeof(string));
                EmaxReportDT.Columns.Add("report_endUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_totalUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_percentage", typeof(string));

                double sumTotal = 0;
                double totalUnit = 0;


                // Find Sumation of total
                // to
                for (int i = 0; i < EMaxToDT.Rows.Count; i++)
                {
                    // from

                    totalUnit = DXWindowsApplication2.UserForms.utilClass.CalculateUnitEWMeter(EMaxToDT.Rows[i]["TotalUnit"].To<double>() , EMaxFromDT.Rows[i]["TotalUnit"].To<double>());
                    if (totalUnit < 0)
                        totalUnit = 0.0;

                    sumTotal += totalUnit;
                }

                double percentage = 0;
                double percentageAll = 0;
                // to
                for (int i = 0; i < EMaxToDT.Rows.Count; i++)
                {
                    // from

                    totalUnit = DXWindowsApplication2.UserForms.utilClass.CalculateUnitEWMeter(EMaxToDT.Rows[i]["TotalUnit"].To<double>() , EMaxFromDT.Rows[i]["TotalUnit"].To<double>());

                    if (totalUnit < 0)
                    {
                        totalUnit = 0.0;

                        percentage = (totalUnit / sumTotal) * 100;

                        percentageAll += percentage;

                        EmaxReportDT.Rows.Add(
                            EMaxFromDT.Rows[i]["room_label"].ToString(),
                            EMaxFromDT.Rows[i]["meter_models"].ToString(),
                            EMaxFromDT.Rows[i]["meter_serial"].ToString(),
                            dateEditFromDate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxFromDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            dateEditTodate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxToDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            "N/A",
                            "N/A"
                            );
                    }
                    else
                    {

                        percentage = (totalUnit / sumTotal) * 100;

                        percentageAll += percentage;

                        EmaxReportDT.Rows.Add(
                            EMaxFromDT.Rows[i]["room_label"].ToString(),
                            EMaxFromDT.Rows[i]["meter_models"].ToString(),
                            EMaxFromDT.Rows[i]["meter_serial"].ToString(),
                            dateEditFromDate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxFromDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            dateEditTodate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxToDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            totalUnit.ToString("N2"),
                            percentage.ToString("N2")
                            );
                    }

                }

                percentageAll = Math.Ceiling(percentageAll);

                if (EmaxReportDT.Rows.Count > 0)
                {
                    ExportExcelManual(EmaxReportDT);
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                    return;
                }

            }
            else {

                if (radioGroupMeter.SelectedIndex == 1)
                {

                    DataTable Room = new DataTable("room");

                    switch (radioGroupMonthHour.SelectedIndex)
                    {

                        case 0: // Hour
                            Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                            ExportExcelHourManual(Room);
                            break;
                        case 1: // Day
                            Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                            ExportExcelDailyManual(Room);
                            break;
                        case 2: // Between length
                            Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                            ExportExcelMonthlyManual(Room);
                            break;
                        default:
                            break;

                    }
                    Room.Dispose();
                }
                else {
                    DataTable Room = new DataTable("room");
                    Room = BusinessLogicBridge.DataStore.getReportRoom(lookUpEditBuilding.EditValue.To<int>(), 0, lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());
                    ExportExcelUnitHourManual(Room);
                }

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

            if (radioGroupMeterType.SelectedIndex == 0)
            {
                // where buildingID , roomIDFrom , roomIDTo, DatetimeFrom
                DataTable EMaxFromDT = BusinessLogicBridge.DataStore.getReportEMax(lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditFromDate.EditValue.ToString());
                // where buildingID , roomIDFrom , roomIDTo, DatetimeTo
                DataTable EMaxToDT = BusinessLogicBridge.DataStore.getReportEMax(lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>(), dateEditTodate.EditValue.ToString());

                DataTable EmaxReportDT = new DataTable();

                EmaxReportDT.Columns.Add("report_roomLabel", typeof(string));
                EmaxReportDT.Columns.Add("report_meterModel", typeof(string));
                EmaxReportDT.Columns.Add("report_meterSerial", typeof(string));
                EmaxReportDT.Columns.Add("report_beginRecord", typeof(string));
                EmaxReportDT.Columns.Add("report_beginUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_endRecord", typeof(string));
                EmaxReportDT.Columns.Add("report_endUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_totalUnit", typeof(string));
                EmaxReportDT.Columns.Add("report_percentage", typeof(string));

                double sumTotal = 0;
                double totalUnit = 0;


                // Find Sumation of total
                // to
                for (int i = 0; i < EMaxToDT.Rows.Count; i++)
                {
                    // from

                    totalUnit = EMaxToDT.Rows[i]["TotalUnit"].To<double>() - EMaxFromDT.Rows[i]["TotalUnit"].To<double>();
                    if (totalUnit < 0)
                        totalUnit = 0.0;

                    sumTotal += totalUnit;
                }

                double percentage = 0;
                double percentageAll = 0;
                // to
                for (int i = 0; i < EMaxToDT.Rows.Count; i++)
                {
                    // from

                    totalUnit = EMaxToDT.Rows[i]["TotalUnit"].To<double>() - EMaxFromDT.Rows[i]["TotalUnit"].To<double>();

                    if (totalUnit < 0)
                    {
                        totalUnit = 0.0;

                        percentage = (totalUnit / sumTotal) * 100;

                        percentageAll += percentage;

                        EmaxReportDT.Rows.Add(
                            EMaxFromDT.Rows[i]["room_label"].ToString(),
                            EMaxFromDT.Rows[i]["meter_models"].ToString(),
                            EMaxFromDT.Rows[i]["meter_serial"].ToString(),
                            dateEditFromDate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxFromDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            dateEditTodate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxToDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            "N/A",
                            "N/A"
                            );
                    }
                    else
                    {

                        percentage = (totalUnit / sumTotal) * 100;

                        percentageAll += percentage;

                        EmaxReportDT.Rows.Add(
                            EMaxFromDT.Rows[i]["room_label"].ToString(),
                            EMaxFromDT.Rows[i]["meter_models"].ToString(),
                            EMaxFromDT.Rows[i]["meter_serial"].ToString(),
                            dateEditFromDate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxFromDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            dateEditTodate.EditValue.To<DateTime>().ToString("yyyy/MM/dd"),
                            EMaxToDT.Rows[i]["TotalUnit"].To<double>().ToString("N2"),
                            totalUnit.ToString("N2"),
                            percentage.ToString("N2")
                            );
                    }

                }

                percentageAll = Math.Ceiling(percentageAll);

                if (EmaxReportDT.Rows.Count > 0)
                {
                    ExportPDFManual(EmaxReportDT);
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                    return;
                }

            }
        }

    }
}
