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
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraPrinting;
using System.IO; 

namespace DXWindowsApplication2.UserForms
{
    public partial class ReportIncome : uBase
    {
        public ReportIncome()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ReportIncome_Load);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
        }

        void ReportIncome_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
            initDropDownContractType();
            
        }

        #region Function

        protected void ExportExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp =
               new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }
            System.Globalization.CultureInfo CurrentCI =
                 System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture =
                   new System.Globalization.CultureInfo("th-TH");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook =
           workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet =
               (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }

        protected void ExportExcelManual(DataTable RecieptTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.income PrintIncome = new DXWindowsApplication2.ReportDocuments.income();

            if (Directory.Exists(Path.Combine(filePath,"Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Income_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintIncome.loopGenDataRow(RecieptTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text, dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            PrintIncome.ExportToXls(pathname);
            PrintIncome.ShowPreview();
        }

        protected void ExportPDFManual(DataTable RecieptTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            ReportDocuments.income PrintIncome = new DXWindowsApplication2.ReportDocuments.income();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "Income_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

            // Building, roomFrom, roomTo, dateFrom, dateTo

            PrintIncome.loopGenDataRow(RecieptTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text, dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            PrintIncome.ExportToPdf(pathname);
            PrintIncome.ShowPreview();
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }
        
        void initDropDownContractType()
        {
            DataTable ContractTypeTbl = new DataTable();
            ContractTypeTbl.Columns.Add("contracttype_id", typeof(int));
            ContractTypeTbl.Columns.Add("contracttype_label", typeof(string));
            ContractTypeTbl.Rows.Add("1", getLanguage("_daily"));
            //ContractTypeTbl.Rows.Add("2", "รายสัปดาห์");
            ContractTypeTbl.Rows.Add("3", getLanguage("_monthly"));
            ContractTypeTbl.Rows.Add("4", getLanguage("_all"));
            lookUpEditContractType.Properties.DataSource = ContractTypeTbl;
            lookUpEditContractType.Properties.DisplayMember = "contracttype_label";
            lookUpEditContractType.Properties.ValueMember = "contracttype_id";
            lookUpEditContractType.Properties.NullText = "[" + getLanguage("_rental_type") + "]";
            lookUpEditContractType.EditValue = null;
            //
            lookUpEditContractType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("contracttype_label", 0, getLanguage("_rental_type")));
            lookUpEditContractType.EditValue = 4;
        }

        
        void setThisLang() {
            // Group Control
            this.lbBuilding.Text = getLanguage("_building");
            this.lbRoomTypeRent.Text = getLanguage("_language");
        }

        #endregion

        #region Event Button

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

            DataTable RecieptTable = BusinessLogicBridge.DataStore.getReportIncome(lookUpEditBuilding.Text, 1, 1, 100, dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            RecieptTable.Columns.Add("contract_type_text", typeof(string));

            for (int i = 0; i < RecieptTable.Rows.Count; i++)
            {

                if (RecieptTable.Rows[i]["check_in_contracttype"].To<int>() == 3)
                {
                    RecieptTable.Rows[i]["contract_type_text"] = getLanguage("_monthly");
                }
                else
                {
                    RecieptTable.Rows[i]["contract_type_text"] = getLanguage("_daily");
                }
            }

            gridControlReciept.DataSource = RecieptTable;

            if (RecieptTable.Rows.Count > 0)
            {
                ExportExcelManual(RecieptTable);
            }
            else
            {
                utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                return;
            }

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

            if (dateEditFromDate.EditValue == null)
            {
                label = lbDueDate.Text;
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
                label = lbToDate.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    dateEditTodate.Focus();
                    focus = true;
                }
            }

            return _ValidateTable;
        }


        void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            //DevExpress.XtraPrinting.TextBrick brick;

            //brick = e.Graph.DrawString("My Report", Color.Navy, new RectangleF(0, 0, 500, 40), DevExpress.XtraPrinting.BorderSide.None);

            //brick.Font = new Font("Arial", 16);

            //brick.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
        }

        #endregion

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

            DataTable RecieptTable = BusinessLogicBridge.DataStore.getReportIncome(lookUpEditBuilding.Text, 1, 1, 100, dateEditFromDate.EditValue.To<DateTime>(), dateEditTodate.EditValue.To<DateTime>());

            RecieptTable.Columns.Add("contract_type_text", typeof(string));

            for (int i = 0; i < RecieptTable.Rows.Count; i++)
            {

                if (RecieptTable.Rows[i]["check_in_contracttype"].To<int>() == 3)
                {
                    RecieptTable.Rows[i]["contract_type_text"] = getLanguage("_monthly");
                }
                else
                {
                    RecieptTable.Rows[i]["contract_type_text"] = getLanguage("_daily");
                }
            }

            gridControlReciept.DataSource = RecieptTable;

            if (RecieptTable.Rows.Count>0){
                ExportPDFManual(RecieptTable);
            }else {
                utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                return;
            }
        }

    }
}
