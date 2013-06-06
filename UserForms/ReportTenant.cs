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
    public partial class ReportTenant : uBase
    {
        public ReportTenant()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ReportTenant_Load);
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


            bttExport.Enabled = true;
            bttPrint.Enabled = true;

        }

        void ReportTenant_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
            if (lookUpEditBuilding.EditValue==null)
            {
                bttExport.Enabled = false;
                bttPrint.Enabled = false;
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

            return _ValidateTable;
        }



        #region Function

        protected void ExportExcelManual(DataTable TenantTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = "";

            switch (radioGroupTenantType.EditValue.To<int>()) {                 
                case 1 :   
                    // Room Reserved
                    ReportDocuments.tenant_reserved PrintTenantReserved = new DXWindowsApplication2.ReportDocuments.tenant_reserved();
                    PrintTenantReserved.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantBooking_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                    PrintTenantReserved.ExportToXls(pathname);
                    PrintTenantReserved.ShowPreview();
                    break;
                case 2:
                    // Room Cancel Booking
                    ReportDocuments.tenant_cancelbooking PrintTenantCancel = new DXWindowsApplication2.ReportDocuments.tenant_cancelbooking();
                    PrintTenantCancel.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantCancel_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                    PrintTenantCancel.ExportToXls(pathname);
                    PrintTenantCancel.ShowPreview();
                    break;
                case 3:
                    // Room Rent
                    ReportDocuments.tenant PrintTenant = new DXWindowsApplication2.ReportDocuments.tenant();
                    PrintTenant.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantRental_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                    PrintTenant.ExportToXls(pathname);
                    PrintTenant.ShowPreview();
                    break;                
                case 5:
                    // Room Inform Leave
                    ReportDocuments.tenant_informleave PrintTenantInformLeave = new DXWindowsApplication2.ReportDocuments.tenant_informleave();
                    PrintTenantInformLeave.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantInformLeave_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                    PrintTenantInformLeave.ExportToXls(pathname);
                    PrintTenantInformLeave.ShowPreview();
                    break;

                default:
                    ReportDocuments.tenant PrintTenant2 = new DXWindowsApplication2.ReportDocuments.tenant();
                    PrintTenant2.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantRental_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");

                    PrintTenant2.ExportToXls(pathname);
                    PrintTenant2.ShowPreview();
                    break;
            }

        }

        protected void ExportPDFManual(DataTable TenantTable)
        {
            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            if (Directory.Exists(Path.Combine(filePath, "Report")) == false)
            {
                Directory.CreateDirectory(Path.Combine(filePath, "Report"));
            }

            string pathname = "";

            switch (radioGroupTenantType.EditValue.To<int>())
            {
                case 1:
                    // Room Reserved
                    ReportDocuments.tenant_reserved PrintTenantReserved = new DXWindowsApplication2.ReportDocuments.tenant_reserved();
                    PrintTenantReserved.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantBooking_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                    PrintTenantReserved.ExportToPdf(pathname);
                    PrintTenantReserved.ShowPreview();
                    break;
                case 2:
                    // Room Cancel Booking
                    ReportDocuments.tenant_cancelbooking PrintTenantCancel = new DXWindowsApplication2.ReportDocuments.tenant_cancelbooking();
                    PrintTenantCancel.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantCancel_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                    PrintTenantCancel.ExportToPdf(pathname);
                    PrintTenantCancel.ShowPreview();
                    break;
                case 3:
                    // Room Rent
                    ReportDocuments.tenant PrintTenant = new DXWindowsApplication2.ReportDocuments.tenant();
                    PrintTenant.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantRental_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                    PrintTenant.ExportToPdf(pathname);
                    PrintTenant.ShowPreview();
                    break;
                case 5:
                    // Room Inform Leave
                    ReportDocuments.tenant_informleave PrintTenantInformLeave = new DXWindowsApplication2.ReportDocuments.tenant_informleave();
                    PrintTenantInformLeave.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantInformLeave_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                    PrintTenantInformLeave.ExportToPdf(pathname);
                    PrintTenantInformLeave.ShowPreview();
                    break;

                default:
                    ReportDocuments.tenant PrintTenant2 = new DXWindowsApplication2.ReportDocuments.tenant();
                    PrintTenant2.loopGenDataRow(TenantTable, lookUpEditBuilding.Text, lookUpEditRoomFrom.Text, lookUpEditRoomTo.Text);

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Report", "TenantRental_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                    PrintTenant2.ExportToPdf(pathname);
                    PrintTenant2.ShowPreview();
                    break;
            }

        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
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

            DataTable TenantTable = BusinessLogicBridge.DataStore.getReportTenant(lookUpEditBuilding.EditValue.To<int>(), radioGroupTenantType.EditValue.To<int>(), lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());

            if (TenantTable.Rows.Count > 0)
            {
                ExportExcelManual(TenantTable);
            }
            else
            {
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

            DataTable TenantTable = BusinessLogicBridge.DataStore.getReportTenant(lookUpEditBuilding.EditValue.To<int>(), radioGroupTenantType.EditValue.To<int>(), lookUpEditRoomFrom.EditValue.To<int>(), lookUpEditRoomTo.EditValue.To<int>());

            if (TenantTable.Rows.Count > 0)
            {
                ExportPDFManual(TenantTable);
            }
            else
            {
                utilClass.showPopupMessegeBox(this, "Data 0 Record", getLanguage("_softwarename"), "info");
                return;
            }
        }


    }
}
