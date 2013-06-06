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
    public partial class ListContract : uBase
    {
        public ListContract()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.Load += new EventHandler(ListContract_Load);
            dateEditFrom.EditValueChanged += new EventHandler(dateEditFrom_EditValueChanged);
            dateEditTo.EditValueChanged += new EventHandler(dateEditTo_EditValueChanged);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
        }

        public override void Refresh()
        {
            base.Refresh();
            initDropDownBuilding();
            initDropdownDateFrom();
            loadContract();
            setLangThis();
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEditFrom.EditValue != null && dateEditTo.EditValue !=null)
                loadContract();
        }

        void dateEditTo_EditValueChanged(object sender, EventArgs e)
        {
            loadContract();
        }

        void dateEditFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEditTo.EditValue!=null)
            loadContract();
        }

        void ListContract_Load(object sender, EventArgs e)
        {
            initDropDownBuilding();
            initDropdownDateFrom();
            loadContract();
            setLangThis();
        }

        public void initDropDownBuilding()
        {
            DataTable Buildings = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DataSource = Buildings;
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");

            if (Buildings.Rows.Count>0)
            lookUpEditBuilding.EditValue = Buildings.Rows[0]["building_id"];
        }

        void initDropdownDateFrom() {

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            dateEditFrom.EditValue = dt;

            dateEditTo.EditValue = dt;
        }

        void loadContract() {

            DataTable Contract = BusinessLogicBridge.DataStore.getContractListByBuildingLabel(lookUpEditBuilding.Text, dateEditFrom.EditValue.To<DateTime>(), dateEditTo.EditValue.To<DateTime>());

            DataTable ContractDT = new DataTable();

            ContractDT.Columns.Add("check_in_id", typeof(int));
            ContractDT.Columns.Add("checkbox",typeof(bool));
            ContractDT.Columns.Add("check_in_label",typeof(string));
            ContractDT.Columns.Add("room_label", typeof(string));
            ContractDT.Columns.Add("tenant_name", typeof(string));
            ContractDT.Columns.Add("tenant_surname", typeof(string));
            ContractDT.Columns.Add("check_in_date", typeof(DateTime));
            ContractDT.Columns.Add("contacttype_text", typeof(string));
            ContractDT.Columns.Add("contactstatus_text", typeof(string));
            ContractDT.Columns.Add("closed_date", typeof(string));
            ContractDT.Columns.Add("flag_type_contract", typeof(int));

            DataTable Booking = BusinessLogicBridge.DataStore.getReserveListByBuildingLabel(lookUpEditBuilding.Text, dateEditFrom.EditValue.To<DateTime>(), dateEditTo.EditValue.To<DateTime>());

            DataTable BookingDT = new DataTable();

            BookingDT.Columns.Add("check_in_id", typeof(int));
            BookingDT.Columns.Add("checkbox", typeof(bool));
            BookingDT.Columns.Add("check_in_label", typeof(string));
            BookingDT.Columns.Add("room_label", typeof(string));
            BookingDT.Columns.Add("tenant_name", typeof(string));
            BookingDT.Columns.Add("tenant_surname", typeof(string));
            BookingDT.Columns.Add("check_in_date", typeof(DateTime));
            BookingDT.Columns.Add("contacttype_text", typeof(string));
            BookingDT.Columns.Add("contactstatus_text", typeof(string));
            BookingDT.Columns.Add("closed_date", typeof(string));
            BookingDT.Columns.Add("flag_type_contract", typeof(int));

            //check_in_contracttype
            //contact_status

            for (int i = 0; i < Contract.Rows.Count; i++)
            {
                DataRow ContractRow = ContractDT.NewRow();
            ContractRow["checkbox"] = false;
            ContractRow["check_in_id"] = Contract.Rows[i]["check_in_id"];
            ContractRow["check_in_label"] = Contract.Rows[i]["check_in_label"];
            
            ContractRow["room_label"] = Contract.Rows[i]["room_label"];
            ContractRow["tenant_name"] = Contract.Rows[i]["tenant_name"];
            ContractRow["tenant_surname"] = Contract.Rows[i]["tenant_surname"];
            ContractRow["check_in_date"] = Contract.Rows[i]["check_in_date"];
                                              
                switch (Contract.Rows[i]["check_in_contracttype"].To<int>())
                {
                    case 1:
                        ContractRow["contacttype_text"]  = getLanguage("_daily");
                        break;
                    case 3:
                        ContractRow["contacttype_text"]  = getLanguage("_monthly");
                        break;
                    default:
                        ContractRow["contacttype_text"]  = getLanguage("_monthly");
                        break;
                }

                if (Contract.Rows[i]["contact_status"].To<int>() == 1)
                {
                    ContractRow["contactstatus_text"] = getLanguage("_rentroom");
                }
                else
                {
                    ContractRow["contactstatus_text"] = getLanguage("_close_contract");
                    ContractRow["closed_date"] = Contract.Rows[i]["check_in_closeddate"];
                }

                ContractRow["flag_type_contract"] = 1;

                ContractDT.Rows.Add(ContractRow);
            }

            for (int i = 0; i < Booking.Rows.Count; i++)
            {
                DataRow BookingRow = BookingDT.NewRow();
                BookingRow["checkbox"] = false;
                BookingRow["check_in_id"] = Booking.Rows[i]["reserve_id"];
                BookingRow["check_in_label"] = Booking.Rows[i]["reserve_number"];
                BookingRow["room_label"] = Booking.Rows[i]["room_label"];
                BookingRow["tenant_name"] = Booking.Rows[i]["tenant_name"];
                BookingRow["tenant_surname"] = Booking.Rows[i]["tenant_surname"];
                BookingRow["check_in_date"] = Booking.Rows[i]["reserve_create_date"];
                
                BookingRow["contacttype_text"] = getLanguage("_booking");
                BookingRow["contactstatus_text"] = getLanguage("_booking");
                BookingRow["closed_date"] = "";

                BookingRow["flag_type_contract"] = 2;
                BookingDT.Rows.Add(BookingRow);
            }

            ContractDT.Merge(BookingDT);

            gridControlContract.DataSource = ContractDT;
        }
      
        void setLangThis() {
            gbListContract.Text = getLanguage("_contract_list");
            lbFromdate.Text = getLanguage("_from");
            lbTodate.Text = getLanguage("_to");
            lbBuilding.Text = getLanguage("_building");
            checkEditCheckAll.Text = getLanguage("_selectall");

            colContractNumber.Caption = getLanguage("_contract_no");
            colType.Caption = getLanguage("_type");
            colRoomName.Caption = getLanguage("_room_name");
            colTenant.Caption = getLanguage("_first_name_tenant");
            colLastname.Caption = getLanguage("_lastname");
            colDatecreted.Caption = getLanguage("_issue_date");
            colStatus.Caption = getLanguage("_status");
            colCancelDate.Caption = getLanguage("_closed_cancelled_date");

            bttPrint.Text = getLanguage("_print");
            bttCancel.Text = getLanguage("_cancel");
        }

        //void printPreviewContract()
        //{

        //    PrintDocuments.contract PrintContract = new DXWindowsApplication2.PrintDocuments.contract();

        //    DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
        //    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

        //    string pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Contract", textEditContractNo.EditValue.ToString() + ".pdf");

        //    PrintContract.loopGenDataRow(check_in_id);

        //    PrintContract.ExportToPdf(pathname);
        //    PrintContract.ShowPreview();
        //}

        private void bttPrint_Click(object sender, EventArgs e)
        {
            string pathname = "";
            string filePath = "";
            DataTable GeneralInfo = new DataTable();
            DataTable GridTableCheckbox = new DataTable();

            GridTableCheckbox = ((DataTable)gridControlContract.DataSource);
            PrintDocuments.contract PrintContract = new DXWindowsApplication2.PrintDocuments.contract();

            for (int i = 0; i < GridTableCheckbox.Rows.Count; i++)
            {
                if ((bool)(GridTableCheckbox.Rows[i]["checkbox"]) == true)
                {
                    if (GridTableCheckbox.Rows[i]["flag_type_contract"].To<int>() ==  1)
                    {   

                        // Checkin
                        GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                        pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Contract", GridTableCheckbox.Rows[i].Table.Rows[0]["check_in_label"].ToString() + ".pdf");
                        PrintContract.loopGenDataRow(GridTableCheckbox.Rows[i]["check_in_id"].To<int>());

                        PrintContract.ShowPreview();
                    }
                    else {

                        // Booking
                        PrintDocuments.booking PrintBooking = new DXWindowsApplication2.PrintDocuments.booking();

                        int reserve_id = GridTableCheckbox.Rows[i]["check_in_id"].To<int>();

                        //reserve_id
                        PrintBooking.loopGenDataRow(reserve_id);
                        PrintBooking.ShowPreview();
                    }

                }
            }
            loadContract();
        }
    }
}

