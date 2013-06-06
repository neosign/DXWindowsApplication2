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
    public partial class RoomCheckin : uBase
    {
        public DataTable RoomTypeInfo;
        public static string prefix;
        public static int doc_id;
        public static int contract_start;
        public bool flagContractChangeStatus = false;
        private string button_event = "";
        private int action_key = 0;
        private int read_click = 0;
        private int record_manual_click = 0;
        int counter_checkin = 0;
        private int roomstatus;
        private int roomtype_id;
        //
        public static XtraMessageBoxForm AddPanel;
        public static DataTable ItemTableTemp;
        public static DataTable RoommateTableTemp;
        //
        public int selectRoomID = 0;
        public int check_in_id = 0;

        public RoomCheckin()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(RoomCheckin_Load);
            //
            bttEdit.Click += new EventHandler(bttEdit_Click);
            bttPrint.Click += new EventHandler(bttPrint_Click);
            bttSave.Click += new EventHandler(bttSave_Click);
            bttSelectTenant.Click += new EventHandler(bttSelectTenant_Click);
            bttCancel.Click += new EventHandler(bttCancel_Click);
            bttAddExpense.Click += new EventHandler(bttAddExpense_Click);
            bttAddTenant.Click += new EventHandler(bttAddTenant_Click);
            bttRemoveTenant.Click += new EventHandler(bttRemoveTenant_Click);
            //
            gridViewRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

            gridViewRoom.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewRoom_RowClick);

            lookUpEditContractType.EditValueChanged += new EventHandler(lookUpEditContractType_EditValueChanged);

            SaveClick +=new EventHandler(bttSave_Click);
        }

        void gridViewRoom_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            changeRow();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
            initDropDownContractType();
            //initDropDownPrefix();
            initDropDownBuilding();
            initDropDownFloor(1);
            initDropDownRoomType();
            //
            
            //

            if (bttSave.Enabled == true)
            {
                enabled(true);
            }
            else
            {

                initRoom();
                //
                if (selectRoomID != 0)
                {
                    DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlRoom.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;

                    DataTable xxx = ((DataTable)gridControlRoom.DataSource);
                    int flagid = 0;
                    for (int i = 0; i < xxx.Rows.Count; i++) {

                        if (xxx.Rows[i]["room_id"].To<int>() == selectRoomID)
                            flagid = i; 
                    
                    }
                    gridViewRoom.FocusedRowHandle = flagid;

                    //

                   //
                    selectRoomID = 0;
                }
                //
                changeRow();
            }

        }

        void RoomCheckin_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //
            DataTable DocumentConfigTable = BusinessLogicBridge.DataStore.RoomCheckIn_getDocumentConfig();
            if (DocumentConfigTable.Rows.Count > 0)
            {
                prefix = DocumentConfigTable.Rows[0]["doc_contact_prefix"].ToString();
                doc_id = DocumentConfigTable.Rows[0]["doc_config_id"].To<int>();
                contract_start = DocumentConfigTable.Rows[0]["doc_contact_start"].To<int>();
            }
            //
            initDropDownContractType();
            initRoom();
            initDropDownPrefix();
            initDropDownBuilding();
            initDropDownFloor(1);
            initDropDownRoomType();
            //
            setLangThis();
        }

        void bttRemoveTenant_Click(object sender, EventArgs e)
        {
            int[] rowIndex = gridView2.GetSelectedRows();
            if (rowIndex.Length == 0)
            {
                return;
            }
            //
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                DataRow CurrentRow = gridView2.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridView2.GetDataRow(0);
                }
                int rowOrder = Convert.ToInt32(CurrentRow[0]);
                //
                gridView2.DeleteRow(rowIndex[0]);
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");
            }
        }

        void bttAddTenant_Click(object sender, EventArgs e)
        {
            int room_id = 0;
            if(textEditRoomId.EditValue.ToString() != "")
                room_id = int.Parse(textEditRoomId.EditValue.ToString());
            //
            DialogResult dr = utilClass.showPopAddTenant(this, ref RoommateTableTemp, room_id);
            //
            if (dr == DialogResult.OK)
                initTenantRoommate();
        }

        void bttAddExpense_Click(object sender, EventArgs e)
        {
            ItemTableTemp = utilClass.showPopAddExpense(this, ItemTableTemp);
            //
            initItem();
        }

        void bttSelectTenant_Click(object sender, EventArgs e)
        {
            DataRow drSelectTenant = utilClass.showPopUpTenant(this);
            //
            if (drSelectTenant == null) return;
            //
            textEditTenantID.EditValue = drSelectTenant["tenant_id"].ToString();
            //
            lookUpEditPrefix.EditValue = drSelectTenant["tenant_prefix_id"];
            textEditName.EditValue = drSelectTenant["tenant_name"].ToString();
            textEditSurname.EditValue = drSelectTenant["tenant_surname"].ToString();
            dateEditBirthDay.EditValue = drSelectTenant["tenant_birthday"];
            textEditIDCard.EditValue = drSelectTenant["tenant_idcard_no"].ToString();
            textEditAddress.EditValue = drSelectTenant["tenant_address"].ToString();
            textEditProvince.EditValue = drSelectTenant["tenant_province_id"];
            textEditDistrict.EditValue = drSelectTenant["tenant_distict_id"];
            textEditCarLicence.EditValue = drSelectTenant["tenant_car"].ToString();
            textEditPostcode.EditValue = drSelectTenant["tenant_postcode"].ToString();
            textEditPhone.EditValue = drSelectTenant["tenant_phone"].ToString();
            textEditRemark.EditValue = drSelectTenant["tenant_note"].ToString();
            //
            drSelectTenant = null;
            //
            RoommateTableTemp = null;
            //
            initTenantRoommate();
        }

        public void setLangThis()
        {
            //
            this.Name = getLanguage("_tenant_info");
            //
            this.groupControlList.Text = getLanguage("_room_list");
            this.groupControlMoveIn.Text = getLanguage("_check_in");
            this.groupControlRental.Text = getLanguage("_rental");
            this.groupControlStartDate.Text = getLanguage("_recording_date");
            this.groupControlStartMeter.Text = getLanguage("_room_info");
            this.groupRoomInfo.Text = getLanguage("_room_info");
            this.groupExpense.Text = getLanguage("_addittional_cost");
            this.groupOccupie.Text = getLanguage("_occupier");
            //
            this.grid_floor_code.Caption = getLanguage("_floor");
            this.grid_room_status_label.Caption = getLanguage("_status");
            this.grid_room_name.Caption = getLanguage("_room_name");
            this.grid_building.Caption = getLanguage("_building");
            this.grid_roomtype_label.Caption = getLanguage("_room_type");

            //
            this.labelControlContractNo.Text = getLanguageWithColon("_contract_no");
            this.labelControlRentType.Text = getLanguageWithColon("_rental_type");
            this.labelControlReservePrice.Text = getLanguageWithColon("_book_amount");
            this.labelControlContractDate.Text = getLanguageWithColon("_contract_date");
            this.labelControlMinimum.Text = getLanguageWithColon("_minimum_rent_duration");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            //
            this.labelControlMonthlyRate.Text = getLanguageWithColon("_rent");
            this.labelControlBeforeRent.Text = getLanguageWithColon("_advance_charge");
            this.labelControlInsurance.Text = getLanguageWithColon("_insurance_charge");
            this.labelDaily.Text = getLanguageWithColon("_amount_day");
            //
            this.labelControlTitle.Text = getLanguageWithColon("_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlSurname.Text = getLanguageWithColon("_lastname");
            this.labelControlIDCard.Text = getLanguageWithColon("_idcard_passport");
            this.labelControlBirthday.Text = getLanguageWithColon("_birthday");
            this.labelControlAddress.Text = getLanguageWithColon("_address");
            this.labelControlProvince.Text = getLanguageWithColon("_province");
            this.labelControlDistrict.Text = getLanguageWithColon("_district");
            this.labelControlPostcode.Text = getLanguageWithColon("_zipcode");
            this.labelControlPhone.Text = getLanguageWithColon("_tel");
            this.labelControlCarLicence.Text = getLanguageWithColon("_car_license");
            this.labelControlRemark.Text = getLanguageWithColon("_remark");
            this.labelControlRequired.Text = getLanguage("_required");
            //
            this.groupBoxValue.Text = getLanguage("_record_meter");
            this.groupControlStartMeter.Text = getLanguage("_meter_start");

            //
            this.labelControlWaterMeter.Text = getLanguage("_water");
            this.labelControlElectricMeter.Text = getLanguage("_electricity");
            this.labelControlPhoneMeter.Text = getLanguage("_telephone");
            //
            this.bttReadMeter.Text = getLanguage("_read_current");

            //Grid
            this.gridColumnOrder.Caption = getLanguage("_no");
            this.gridColumnList.Caption = getLanguage("_item");
            this.gridColumnDaily.Caption = getLanguage("_price_per_day");
            this.gridColumnMonthly.Caption = getLanguage("_price_per_month");
            this.gridColumnType.Caption = getLanguage("_payment_format");
            this.gridColumnStatus.Caption = getLanguage("_status");
            //
            this.grid_tenant_prefix.Caption = getLanguage("_prefix");
            this.grid_tenant_prefix.FieldName = "prefix_" + current_lang + "_label";
            this.grid_tenant_firstname.Caption = getLanguage("_firstname");
            this.grid_tenant_surname.Caption = getLanguage("_lastname");
            this.grid_tenant_idcard.Caption = getLanguage("_idcard_passport");
            this.grid_tenant_tel.Caption = getLanguage("_tel");
            //
            this.bttSelectTenant.Text = getLanguage("_select_tenant");
            this.bttAddTenant.Text = getLanguage("_add");
            this.bttRemoveTenant.Text = getLanguage("_delete");
            this.bttAddExpense.Text = getLanguage("_add");
            this.bttPrint.Text = getLanguage("_print_contract");
            //
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            //
            this.labelControlMonth.Text = getLanguage("_month");
            this.labelControlMonth2.Text = getLanguage("_month");
            this.labelControlBath1.Text = getLanguage("_baht");
            this.labelControlBath2.Text = getLanguage("_baht");

        }

        #region Setup
        void initRoom()
        {   
            string genContractNo = "";
            DataTable RoomTbl = BusinessLogicBridge.DataStore.RoomCheckIn_getRoom();
            gridControlRoom.DataSource = RoomTbl;
            if (RoomTbl.Rows.Count > 0)
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(0);
                roomtype_id = Convert.ToInt32(CurrentRow["roomtype_id"].ToString());
                
                DataTable DocumentConfigTable = BusinessLogicBridge.DataStore.RoomCheckIn_getDocumentConfig();
                if (DocumentConfigTable.Rows.Count > 0)
                {
                    prefix = DocumentConfigTable.Rows[0]["doc_contact_prefix"].ToString();
                    doc_id = DocumentConfigTable.Rows[0]["doc_config_id"].To<int>();
                    contract_start = DocumentConfigTable.Rows[0]["doc_contact_start"].To<int>();
                }

                // Check format Contract No
                flagContractChangeStatus = BusinessLogicBridge.DataStore.CheckContractPrefixChanged(doc_id);
                if (flagContractChangeStatus==true)
                    genContractNo = prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');
                else
                    genContractNo = prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');
                
                textEditContractNo.EditValue = genContractNo;
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditRoomCode.EditValue = CurrentRow["coderef"].ToString();
                textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();

                //readElectricMeter(textEditRoomId.EditValue.To<int>());
                //readWaterMeter(textEditRoomId.EditValue.To<int>());
                initItem();
            }
            else
            {
                enabled(false);

                if (RoomTypeInfo.Rows.Count==0)
                 bttEdit.Enabled = false;
            }
        }
        void initTenantRoommate()
        {
            if (textEditRoomId.EditValue.ToString() == "")
                textEditRoomId.EditValue = 0;
            //
            int roomTempID = textEditRoomId.EditValue.To<int>();
            //
            if (RoommateTableTemp == null)
                RoommateTableTemp = BusinessLogicBridge.DataStore.getRoommateByRoomID(roomTempID, current_lang);
            //
            gridControlRoommate.DataSource = RoommateTableTemp;
            //
            if (bttEdit.Enabled == false)
                bttRemoveTenant.Enabled = RoommateTableTemp.Rows.Count > 0;
        }
        void initDropDownContractType()
        {
            DataTable ContractTypeTbl = new DataTable();
            // Define Column
            ContractTypeTbl.Columns.Add("contracttype_id", typeof(int));
            ContractTypeTbl.Columns.Add("contracttype_label", typeof(string));

            RoomTypeInfo = BusinessLogicBridge.DataStore.getRoomTypeByID(roomtype_id);

            if (RoomTypeInfo.Rows.Count > 0){

                // Define From Room Type

                if (RoomTypeInfo.Rows[0]["roomtype_month_checked"].To<bool>() == true)
                {
                    ContractTypeTbl.Rows.Add(3, getLanguage("_roomtype_monthly"));
                }

                if (RoomTypeInfo.Rows[0]["roomtype_daily_checked"].To<bool>() == true)
                {
                    ContractTypeTbl.Rows.Add(1, getLanguage("_roomtype_daily"));
                }
            }
            
            lookUpEditContractType.Properties.DisplayMember = "contracttype_label";
            lookUpEditContractType.Properties.ValueMember = "contracttype_id";
            lookUpEditContractType.Properties.NullText = "[" + getLanguage("_rental_type") + "]";
            lookUpEditContractType.EditValue = null;

            lookUpEditContractType.Properties.DataSource = ContractTypeTbl;
        }

        void initDropDownPrefix()
        {
            lookUpEditPrefix.Properties.DataSource = BusinessLogicBridge.DataStore.getAllPrefix();
            lookUpEditPrefix.Properties.DisplayMember = "prefix_" + current_lang + "_label";
            lookUpEditPrefix.Properties.ValueMember = "prefix_id";
            lookUpEditPrefix.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("prefix_" + current_lang + "_label", 0, getLanguage("_prefix")));

            lookUpEditPrefix.Properties.NullText = getLanguage("_select_prefix");
        }
        void initDropDownRoomType()
        {
            lookUpEditRoomTypeId.Properties.DisplayMember = "roomtype_label";
            lookUpEditRoomTypeId.Properties.ValueMember = "roomtype_id";
            lookUpEditRoomTypeId.Properties.DataSource = BusinessLogicBridge.DataStore.getAllRoomType();
        }
        void initDropDownBuilding()
        {
            DataTable Buildings = BusinessLogicBridge.DataStore.getAllBuilding(1);
            lookUpEditBuildingId.Properties.DataSource = Buildings;
            lookUpEditBuildingId.Properties.DisplayMember = "building_label";
            lookUpEditBuildingId.Properties.ValueMember = "building_id";
            lookUpEditBuildingId.Properties.NullText = getLanguage("_select_building");
        }
        void initDropDownFloor(int building_id)
        {
            DataTable Floor = BusinessLogicBridge.DataStore.getAllFloorByBuilding(building_id);
            lookUpEditFloorId.Properties.DataSource = Floor;
            lookUpEditFloorId.Properties.DisplayMember = "floor_code";
            lookUpEditFloorId.Properties.ValueMember = "floor_id";
            lookUpEditFloorId.Properties.NullText = getLanguage("_select_floor");
        }
        void initItem()
        {
            int room_id = textEditRoomId.EditValue.To<int>();

            //DataTable ItemTable = BusinessLogicBridge.DataStore.RoomInfo_getItem(room_id);

            DataTable ItemTable = BusinessLogicBridge.DataStore.RoomCheckIn_getItemByRoomtypeId(roomtype_id);

            ItemTable.Columns.Add("item_order", typeof(int));
            ItemTable.Columns.Add("item_type_label", typeof(String));
            ItemTable.Columns.Add("check_box", typeof(Boolean));
            //
            if (ItemTableTemp != null)
                ItemTable.Merge(ItemTableTemp);
            //
            for (int i = 0; i < ItemTable.Rows.Count; i++)
            {
                ItemTable.Rows[i]["item_order"] = i + 1;

                //
                if (int.Parse(ItemTable.Rows[i]["item_type"].ToString()) == 1)
                {
                    ItemTable.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_monthly");
                }
                else
                {
                    ItemTable.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_onetime");
                }

                if (ItemTable.Rows[i]["status"].ToString() == "")
                {
                    ItemTable.Rows[i]["check_box"] = false;
                }
                else
                {
                    ItemTable.Rows[i]["check_box"] = true;
                }

                //ItemTable.Rows[i]["item_datecreate"] = ItemTable.Rows[i]["item_datecreate"].To<DateTime>().ToString("yyyy-MM-dd H:m:s");

            }
            //
            if (ItemTableTemp == null)
                ItemTableTemp = ItemTable.Clone();
            //
            gridControlExpense.DataSource = ItemTable;
        }
        #endregion

        #region Action Extra
        private DataTable validateDate()
        {
            String label;
            String message;
            DataTable _Error = new DataTable();
            _Error.Columns.Add("label", typeof(String));
            _Error.Columns.Add("message", typeof(String));
            if (textEditRoomId.EditValue.ToString() == "")
            {
                label = labelControlRoomName.Text;
                message = getLanguage("_choose_room");
                _Error.Rows.Add(label, message);
            }
            if (lookUpEditContractType.EditValue == null)
            {
                label = labelControlRentType.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
                //
                return _Error;
            }
            if (textEditName.EditValue.ToString() == "")
            {
                label = labelControlName.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditSurname.EditValue.ToString() == "")
            {
                label = labelControlSurname.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditIDCard.EditValue.ToString() == "")
            {
                label = labelControlIDCard.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditAddress.EditValue.ToString() == "")
            {
                label = labelControlAddress.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditProvince.EditValue.ToString() == "")
            {
                label = labelControlProvince.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditDistrict.EditValue.ToString() == "")
            {
                label = labelControlDistrict.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditPostcode.EditValue.ToString() == "")
            {
                label = labelControlPostcode.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditPhone.EditValue.ToString() == "")
            {
                label = labelControlPhone.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditElectricMeter.EditValue.ToString() == "")
            {
                label = groupControlStartMeter.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (textEditWaterMeter.EditValue.ToString() == "")
            {
                label = groupControlStartMeter.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            return _Error;
        }
        private bool isEmpty(string param)
        {
            if (param.Length < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void changeRow()
        {

            read_click = 0;

            clearDate();
            //
            RoommateTableTemp = null;
            string genContractNo = "";
            textEditTenantID.Text = "";
            if (ItemTableTemp != null) ItemTableTemp.Rows.Clear();
            //
            int[] rowIndex = gridViewRoom.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewRoom.GetDataRow(0);
                }
                roomtype_id = Convert.ToInt32(CurrentRow["roomtype_id"].ToString());
                roomstatus = Convert.ToInt16(CurrentRow["room_status"].ToString());

                initDropDownContractType();

                // Check format Contract No
                flagContractChangeStatus = BusinessLogicBridge.DataStore.CheckContractPrefixChanged(doc_id);
                if (flagContractChangeStatus == true)
                    genContractNo = prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');
                else
                    genContractNo = prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');

                textEditContractNo.EditValue = genContractNo;
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditRoomCode.EditValue = CurrentRow["coderef"].ToString();
                textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();
                //
                lookUpEditBuildingId.EditValue = CurrentRow["building_id"];
                lookUpEditFloorId.EditValue = CurrentRow["floor_id"];
                lookUpEditRoomTypeId.EditValue = roomtype_id;
                //

                textEditMonthlyRate.EditValue = CurrentRow["roomtype_month_roomrate_price"];

                textEditAdvance.EditValue = CurrentRow["roomtype_month_advance_amount"];
                textEditInsurance.EditValue = CurrentRow["roomtype_month_insure_price"];
                //
                textEditElectricMeterId.EditValue = CurrentRow["current_electricity_id"].ToString();
                textEditWaterMeterId.EditValue = CurrentRow["current_water_id"].ToString();
                dateEditCheckInDate.EditValue = DateTime.Now;
                dateEditPhoneMeter.EditValue = DateTime.Now;

                readElectricMeter(CurrentRow["room_id"].To<int>());
                readWaterMeter(CurrentRow["room_id"].To<int>());
                
                //
                if (roomstatus == 3 || roomstatus == 5)
                {
                    lookUpEditPrefix.EditValue = CurrentRow["tenant_prefix_id"];
                    textEditTenantID.EditValue = CurrentRow["tenant_id"].ToString();
                    textEditName.EditValue = CurrentRow["tenant_name"].ToString();
                    textEditSurname.EditValue = CurrentRow["tenant_surname"].ToString();

                    textEditIDCard.EditValue = CurrentRow["tenant_idcard_no"].ToString();
                    dateEditBirthDay.EditValue = CurrentRow["tenant_birthday"].ToString();
                    textEditAddress.EditValue = CurrentRow["tenant_address"].ToString();
                    textEditDistrict.EditValue = CurrentRow["tenant_distict_id"].ToString();
                    textEditProvince.EditValue = CurrentRow["tenant_province_id"].ToString();
                    textEditPostcode.EditValue = CurrentRow["tenant_postcode"].ToString();
                    textEditPhone.EditValue = CurrentRow["tenant_phone"].ToString();
                    
                    DataTable ReservedData = BusinessLogicBridge.DataStore.getReserveByTenantID(CurrentRow["tenant_id"].To<int>());

                    textEditBooking.EditValue = ReservedData.Rows[0]["reserve_payments"].To<double>().ToString("N2");
                }
                else
                {
                    textEditTenantID.Text = "";
                    textEditName.Text = "";
                    textEditSurname.Text = "";
                    textEditPhone.Text = "";
                    textEditBooking.EditValue = 0.00;                    
                }

                //
                initItem();
                initTenantRoommate();
                enabled(false);
            }
            else
            {
                clearDate();
            }
        }
        void clearDate()
        {
            try
            {
                textEditRoomId.EditValue = 0;
                //textEditContractNo.EditValue = prefix + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');
                textEditRoomCode.EditValue = "";
                textEditRoomLabel.EditValue = "";
                //lookUpEditTenant.EditValue = null;
                dateEditCheckInDate.EditValue = DateTime.Now;
                lookUpEditContractType.EditValue = null;
                dateEditElectricMeter.EditValue = DateTime.Now;
                dateEditWaterMeter.EditValue = DateTime.Now;
                dateEditPhoneMeter.EditValue = DateTime.Now;
                textEditElectricMeter.EditValue = 0;
                textEditWaterMeter.EditValue = 0;
                textEditElectricMeterId.EditValue = 0;
                textEditWaterMeterId.EditValue = 0;
                //
                lookUpEditPrefix.EditValue = null;
                textEditName.EditValue = "";
                textEditSurname.EditValue = "";
                textEditIDCard.EditValue = "";
                dateEditBirthDay.EditValue = DateTime.Now;
                textEditAddress.EditValue = "";
                textEditDistrict.EditValue = "";
                textEditProvince.EditValue = "";
                textEditPostcode.EditValue = "";
                textEditPhone.EditValue = "";
                textEditCarLicence.EditValue = "";
                textEditRemark.EditValue = "";

                //
                textEditBooking.EditValue = "";
                textEditMinimum.EditValue = "";
                textEditInsurance.EditValue = "";
                lookUpEditContractType.EditValue = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }
        void createItems(int check_in_id)
        {
            int room_id = int.Parse(textEditRoomId.EditValue.ToString());
            BusinessLogicBridge.DataStore.RoomcheckIn_removeItem(room_id);
            for (int i = 0; i < gridView3.RowCount; i++)
            {
                DataRow CurrentRow = gridView3.GetDataRow(i); 
                int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                string item_name = CurrentRow["item_name"].ToString();
                double item_price_monthly = Convert.ToDouble(CurrentRow["item_price_monthly"].ToString());
                double item_price_weekly = Convert.ToDouble(CurrentRow["item_price_weekly"].ToString());
                double item_price_daily = Convert.ToDouble(CurrentRow["item_price_daily"].ToString());
                string item_detail = CurrentRow["item_detail"].ToString();
                string item_vat = CurrentRow["item_vat"].ToString();
                int item_type = Convert.ToInt16(CurrentRow["item_type"].ToString());
                string item_flag = CurrentRow["item_flag"].ToString();
                //
                Boolean check_box = Convert.ToBoolean(CurrentRow["check_box"].ToString());
                //
                
                    if (item_id == 0)
                    {
                        DataTable _tmp = new DataTable();
                        _tmp.Columns.Add("item_name", typeof(string));
                        _tmp.Columns.Add("item_price_monthly", typeof(double));
                        _tmp.Columns.Add("item_price_weekly", typeof(double));
                        _tmp.Columns.Add("item_price_daily", typeof(double));
                        _tmp.Columns.Add("item_detail", typeof(string));
                        _tmp.Columns.Add("item_vat", typeof(int));
                        _tmp.Columns.Add("item_type", typeof(int));
                        _tmp.Columns.Add("item_flag", typeof(string));
                        //
                        _tmp.Rows.Add(item_name, item_price_monthly, item_price_weekly, item_price_daily, item_detail, item_vat, item_type, item_flag);
                        //
                        item_id = BusinessLogicBridge.DataStore.BasicInfoItem_insert(_tmp);
                    }

                    if (check_box)
                    {
                    //
                    DataTable ItemTable = new DataTable();
                    ItemTable.Columns.Add("check_in_id", typeof(int));
                    ItemTable.Columns.Add("item_id", typeof(int));
                    ItemTable.Columns.Add("item_name", typeof(string));
                    ItemTable.Columns.Add("item_price_monthly", typeof(double));
                    ItemTable.Columns.Add("item_price_weekly", typeof(double));
                    ItemTable.Columns.Add("item_price_daily", typeof(double));
                    ItemTable.Columns.Add("item_detail", typeof(string));
                    ItemTable.Columns.Add("item_vat", typeof(string));
                    ItemTable.Columns.Add("item_type", typeof(int));
                    ItemTable.Columns.Add("item_flag", typeof(string));

                    // Insert Item Of Check in
                    ItemTable.Rows.Add(check_in_id, item_id, item_name, item_price_monthly, item_price_weekly, item_price_daily, item_detail, item_vat, item_type, item_flag);
                    BusinessLogicBridge.DataStore.RoomCheckIn_insertCheckInItem(ItemTable);


                    // Create Item Of Room 
                    DataTable RoomItemTable = new DataTable();
                    RoomItemTable.Columns.Add("room_id", typeof(int));
                    RoomItemTable.Columns.Add("item_id", typeof(int));
                    RoomItemTable.Columns.Add("date_created", typeof(DateTime));
                    RoomItemTable.Rows.Add(room_id, item_id, DateTime.Now);
                    BusinessLogicBridge.DataStore.RoomCheckIn_insertRoomItem(RoomItemTable);
                }
            }
        }
        string createTenant()
        {
            int tenant_id = 0;
            //
            try
            {
                DateTime date_modified = DateTime.Today;
                DataTable _Tenant = new DataTable();
                _Tenant.Columns.Add("tenant_id", typeof(String));
                _Tenant.Columns.Add("tenant_prefix_id", typeof(int));
                _Tenant.Columns.Add("tenant_title", typeof(String));
                _Tenant.Columns.Add("tenant_name", typeof(String));
                _Tenant.Columns.Add("tenant_surname", typeof(String));
                _Tenant.Columns.Add("tenant_birthday", typeof(DateTime));
                _Tenant.Columns.Add("tenant_idcard_no", typeof(String));
                _Tenant.Columns.Add("tenant_address", typeof(String));
                _Tenant.Columns.Add("tenant_province_id", typeof(String));
                _Tenant.Columns.Add("tenant_distict_id", typeof(string));
                _Tenant.Columns.Add("tenant_postcode", typeof(string));
                _Tenant.Columns.Add("tenant_car", typeof(String));
                _Tenant.Columns.Add("tenant_mobile", typeof(String));
                _Tenant.Columns.Add("tenant_phone", typeof(String));
                _Tenant.Columns.Add("tenant_email", typeof(String));
                _Tenant.Columns.Add("tenant_note", typeof(String));
                _Tenant.Columns.Add("tenant_created_date", typeof(DateTime));
                _Tenant.Columns.Add("tenant_modified_date", typeof(DateTime));
                //
                _Tenant.Rows.Add(
                    "",
                    lookUpEditPrefix.EditValue,
                    "",
                    textEditName.EditValue,
                    textEditSurname.EditValue,
                    DateTime.Parse(dateEditBirthDay.EditValue.ToString()),
                    textEditIDCard.EditValue,
                    textEditAddress.EditValue,
                    textEditProvince.EditValue,
                    textEditDistrict.EditValue,
                    textEditPostcode.EditValue,
                    textEditCarLicence.EditValue,
                    "",
                    textEditPhone.EditValue,
                    "",
                    textEditRemark.EditValue,
                    DateTime.Now,
                    DateTime.Parse(date_modified.ToString())
                );
                //
                tenant_id = BusinessLogicBridge.DataStore.Tenant_insert(_Tenant);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tenant_id.ToString();
        }
        void addOccupies()
        {
            int room_id = textEditRoomId.EditValue.To<int>();
            //
            foreach (DataRow dr in RoommateTableTemp.Rows)
            {
                dr["room_id"] = room_id;
            }
            //
            BusinessLogicBridge.DataStore.deleteRoommateByRoomID(room_id);
            //
            BusinessLogicBridge.DataStore.insertRoommate(RoommateTableTemp);
        }
        bool createCheckIn()
        {
            if (MainForm.TrialVersion == true)
            {
                string checkin_cipher = BusinessLogicBridge.DataStore.getCheckinCounter();

                if (checkin_cipher != "")
                {
                    counter_checkin = HelperEncrypt.Decrypt(checkin_cipher, MainForm.hashKey).To<int>();

                    if (counter_checkin >= 5)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1070"), getLanguage("_softwarename"));
                        return false;
                    }
                    else
                    {
                        counter_checkin = counter_checkin + 1;
                    }
                }
                else {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1070"), getLanguage("_softwarename"));
                    return false;
                }
            }

            string room_id = textEditRoomId.EditValue.ToString();
            string electric_meter = textEditElectricMeter.EditValue.ToString();
            string water_meter = textEditWaterMeter.EditValue.ToString();
            string tenant_id = textEditTenantID.EditValue.ToString();

            DataTable RoomTbl = BusinessLogicBridge.DataStore.getRoom(room_id.To<int>());
            //
            if (RoomTbl.Rows[0]["current_tenant_id"].To<int>() <= 0 || (tenant_id.To<int>() != RoomTbl.Rows[0]["current_tenant_id"].To<int>()))
            {
                tenant_id = createTenant();
                if (textEditTenantID.EditValue.To<int>()>0)
                BusinessLogicBridge.DataStore.Tenant_updateRelateID(tenant_id.To<int>(), textEditTenantID.EditValue.To<int>());
                textEditTenantID.EditValue = tenant_id;
            }
            //
            addOccupies();
            //
            DataTable TenantTbl = BusinessLogicBridge.DataStore.getTenant(Convert.ToInt32(tenant_id));

            if (TenantTbl != null)
            {
                if (TenantTbl.Rows.Count > 0)
                {
                    if (TenantTbl.Rows[0]["tenant_idcard_no"].ToString() == "" || TenantTbl.Rows[0]["tenant_address"].ToString() == "" || TenantTbl.Rows[0]["tenant_phone"].ToString() == "") {

                        TenantTbl.Rows[0]["tenant_name"]            = textEditName.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_surname"]         = textEditSurname.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_birthday"]        = dateEditBirthDay.EditValue.To<DateTime>().ToString("yyyy-MM-dd hh:mm:ss");
                        TenantTbl.Rows[0]["tenant_idcard_no"]       = textEditIDCard.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_address"]         = textEditAddress.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_province_id"]     = textEditProvince.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_distict_id"]      = textEditDistrict.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_postcode"]        = textEditPostcode.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_phone"]           = textEditPhone.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_car"]             = textEditCarLicence.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_note"]            = textEditRemark.EditValue.ToString();
                        TenantTbl.Rows[0]["tenant_modified_date"]   = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                        BusinessLogicBridge.DataStore.Tenant_update(TenantTbl);                        
                    }
                }
            }


            DataTable _CheckInTbl = new DataTable();
            DataRow _row = _CheckInTbl.NewRow();
            DataColumn _column;
            DateTime date_create = new DateTime();
            date_create = DateTime.Today;

            _column = new DataColumn();
            _column.ColumnName = "check_in_date";
            _column.DataType = typeof(DateTime);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_date"] = dateEditCheckInDate.EditValue.ToString();

            _column = new DataColumn();
            _column.ColumnName = "check_in_label";
            _column.DataType = typeof(String);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_label"] = textEditContractNo.EditValue.ToString();

            _column = new DataColumn();
            _column.ColumnName = "check_in_electricitymeter";
            _column.DataType = typeof(Double);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_electricitymeter"] = Double.Parse(textEditElectricMeter.EditValue.ToString());

            _column = new DataColumn();
            _column.ColumnName = "check_in_electricit_date";
            _column.DataType = typeof(DateTime);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_electricit_date"] = DateTime.Parse(dateEditElectricMeter.EditValue.ToString());

            _column = new DataColumn();
            _column.ColumnName = "check_in_watermeter";
            _column.DataType = typeof(Double);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_watermeter"] = Double.Parse(textEditWaterMeter.EditValue.ToString());

            _column = new DataColumn();
            _column.ColumnName = "check_in_phone_date";
            _column.DataType = typeof(DateTime);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_phone_date"] = DateTime.Parse(dateEditPhoneMeter.EditValue.ToString());

            _column = new DataColumn();
            _column.ColumnName = "check_in_water_date";
            _column.DataType = typeof(DateTime);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_water_date"] = DateTime.Parse(dateEditWaterMeter.EditValue.ToString());

            _column = new DataColumn();
            _column.ColumnName = "check_in_date_create";
            _column.DataType = typeof(DateTime);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_date_create"] = date_create;

            _column = new DataColumn();
            _column.ColumnName = "check_in_contracttype";
            _column.DataType = typeof(Int32);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_contracttype"] = Int32.Parse(lookUpEditContractType.EditValue.ToString());


            _column = new DataColumn();
            _column.ColumnName = "check_in_minimum_monthly";
            _column.DataType = typeof(Int32);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_minimum_monthly"] = textEditMinimum.EditValue.To<int>();

            _column = new DataColumn();
            _column.ColumnName = "check_in_minimum_daily";
            _column.DataType = typeof(Int32);
            _CheckInTbl.Columns.Add(_column);
            _row["check_in_minimum_daily"] = textEditDaily.EditValue.To<int>();

            string column_name = "";
            foreach (DataColumn col in RoomTbl.Columns)
            {
                column_name = col.ColumnName.ToString();

                if (RoomTbl.Rows[0][column_name] == DBNull.Value)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = "";
                }
                else if (RoomTbl.Rows[0][column_name] is DateTime)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _column.DataType = typeof(DateTime);
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = DateTime.Parse(RoomTbl.Rows[0][column_name].ToString());
                }
                else if (RoomTbl.Rows[0][column_name] is Int32)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _column.DataType = typeof(Int32);
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = Int32.Parse(RoomTbl.Rows[0][column_name].ToString());
                }
                else if (RoomTbl.Rows[0][column_name] is String)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _column.DataType = typeof(String);
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = RoomTbl.Rows[0][column_name].ToString();
                }
                else if (RoomTbl.Rows[0][column_name] is Double)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _column.DataType = typeof(Double);
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = Double.Parse(RoomTbl.Rows[0][column_name].ToString());
                }
                else
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _CheckInTbl.Columns.Add(_column);
                    _row[column_name] = RoomTbl.Rows[0][column_name].ToString();
                }
            }
            foreach (DataColumn col in TenantTbl.Columns)
            {
                column_name = col.ColumnName.ToString();
                if (TenantTbl.Rows[0][column_name] is DateTime)
                {
                    try
                    {
                        _column = new DataColumn();
                        _column.ColumnName = column_name;
                        _column.DataType = typeof(DateTime);
                        if (_CheckInTbl.Columns.Contains(column_name) == false)
                        {
                            _CheckInTbl.Columns.Add(_column);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }

                    _row[column_name] = Convert.ToDateTime(TenantTbl.Rows[0][column_name].ToString());
                }
                else if (TenantTbl.Rows[0][column_name] is Int32)
                {
                    try
                    {
                        _column = new DataColumn();
                        _column.ColumnName = column_name;
                        _column.DataType = typeof(Int32);

                        if (_CheckInTbl.Columns.Contains(column_name) == false)
                        {
                            _CheckInTbl.Columns.Add(_column);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    _row[column_name] = Convert.ToInt32(TenantTbl.Rows[0][column_name].ToString());
                }
                else if (RoomTbl.Rows[0][column_name] is Double)
                {
                    _column = new DataColumn();
                    _column.ColumnName = column_name;
                    _column.DataType = typeof(Double);

                    if (_CheckInTbl.Columns.Contains(column_name) == false)
                    {
                        _CheckInTbl.Columns.Add(_column);
                    }
                    _row[column_name] = Double.Parse(RoomTbl.Rows[0][column_name].ToString());
                }
                else if (TenantTbl.Rows[0][column_name] is String)
                {
                    try
                    {
                        _column = new DataColumn();
                        _column.ColumnName = column_name;
                        _column.DataType = typeof(String);
                        if (_CheckInTbl.Columns.Contains(column_name) == false)
                        {
                            _CheckInTbl.Columns.Add(_column);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    _row[column_name] = TenantTbl.Rows[0][column_name].ToString();
                }
            }
            _CheckInTbl.Rows.Add(_row);
            //
            _CheckInTbl.Rows[0]["room_status"] = 2;
            check_in_id = BusinessLogicBridge.DataStore.RoomCheckIn_insert(_CheckInTbl);

            // Check in Counter for Trial Version
            if (check_in_id > 0) {

                string s = counter_checkin.ToString();
                s = HelperEncrypt.Encrypt(s, MainForm.hashKey);
                
                // Update Counter Checkin
                BusinessLogicBridge.DataStore.updateCounterCheckin(s);
            }

            BusinessLogicBridge.DataStore.RoomCheckIn_update(int.Parse(room_id), int.Parse(tenant_id), 2, check_in_id);
            BusinessLogicBridge.DataStore.RoomCheckIn_updateTenantStatus(int.Parse(tenant_id), 3);
            BusinessLogicBridge.DataStore.RoomCheckIn_updateReserveStatusByRoomId(int.Parse(room_id), 0);
            
            // Update EMeter & WaterMeter as Reading from Transaction
            BusinessLogicBridge.DataStore.updateE_MeterBeginAndEnd(textEditElectricMeter.EditValue.To<double>(), dateEditElectricMeter.EditValue.To<DateTime>(), textEditElectricMeterId.EditValue.To<int>());
            BusinessLogicBridge.DataStore.updateWater_MeterBeginAndEnd(textEditWaterMeter.EditValue.To<double>(), dateEditWaterMeter.EditValue.To<DateTime>(), textEditWaterMeterId.EditValue.To<int>());

            // Update Prefix flag for same
            if (flagContractChangeStatus==true)
                BusinessLogicBridge.DataStore.updateDocumentPrefixLogContract(prefix, prefix, doc_id);
            
            // Create Item Checkin
            createItems(check_in_id);

            // Check case monthly must create reciept transactions
            if (lookUpEditContractType.EditValue.To<int>()==3)
            {   
                // Monthly
                processCalculateReciept(check_in_id);
            }

            return true;
        }

        void processCalculateReciept(int check_in_id) {

            string RecieptNO = "";
            DataTable DTReciept = new DataTable();
            DataTable ItemTable = new DataTable();

            #region declare parameter
            DataTable DTDocInfo = new DataTable();
            DataTable DTInvoice = new DataTable();
            DataTable DTRecieptInfo = new DataTable();
            
            int recieptID = 0;

            double sumprice = 0;
            double price_vat = 0;
            double sumprice_net = 0;

            double item_sumprice = 0;
            double item_vatprice = 0;
            double item_netprice = 0;
            double item_priceperunit = 0;

            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;

            string ThaiBaht = "";
            #endregion

            #region DeClare Column Type of Data Table DTReciept
            DTReciept.Columns.Add("rec_trans_number", typeof(string));
            DTReciept.Columns.Add("rec_trans_cutduedate", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_fixpaymentdate", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_datecreated", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_status", typeof(int));
            DTReciept.Columns.Add("rec_trans_tenantname", typeof(string));
            DTReciept.Columns.Add("rec_trans_tenantaddress", typeof(string));
            DTReciept.Columns.Add("rec_trans_roomlabel", typeof(string));
            DTReciept.Columns.Add("rec_trans_building", typeof(string));
            DTReciept.Columns.Add("rec_trans_floor", typeof(string));
            DTReciept.Columns.Add("rec_trans_roomtype", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_previous_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_emeter_previous_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_present_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_emeter_present_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_emeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_emeter_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_previous_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_wmeter_previous_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_wmeter_present_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_wmeter_present_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_wmeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_phone_start_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_phone_end_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_phonemeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_phonemeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_phone_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice_withvat", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice_net", typeof(double));
            DTReciept.Columns.Add("rec_trans_print_status", typeof(int));
            DTReciept.Columns.Add("rec_trans_room_id", typeof(int));
            DTReciept.Columns.Add("rec_trans_company_name", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_logofile", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_address", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_telephone", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_fax", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_email", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_website", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_tax_id", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_vision", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_header_invoice", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_footer_invoice", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_under_invoice1", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_under_invoice2", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_dateformat", typeof(int));
            DTReciept.Columns.Add("rec_trans_doc_logo_position", typeof(int));
            DTReciept.Columns.Add("rec_trans_roomprice", typeof(double));
            DTReciept.Columns.Add("rec_trans_vattype", typeof(int));
            DTReciept.Columns.Add("inv_trans_number", typeof(string));
            DTReciept.Columns.Add("rec_trans_money_text", typeof(string));
            DTReciept.Columns.Add("check_in_id", typeof(int));
            DTReciept.Columns.Add("check_in_contracttype", typeof(int));
            DTReciept.Columns.Add("rec_trans_category", typeof(int));
            DTReciept.Columns.Add("rec_trans_insurance", typeof(double));
            DTReciept.Columns.Add("rec_trans_advance", typeof(int));
            DTReciept.Columns.Add("rec_trans_booking", typeof(double));

            ItemTable.Columns.Add("rec_trans_id", typeof(int));
            ItemTable.Columns.Add("item_id", typeof(int));
            ItemTable.Columns.Add("item_name", typeof(string));
            ItemTable.Columns.Add("item_price_monthly", typeof(double));
            ItemTable.Columns.Add("item_price_daily", typeof(double));
            ItemTable.Columns.Add("item_vat", typeof(string));
            ItemTable.Columns.Add("item_type", typeof(int));
            ItemTable.Columns.Add("item_priceperunit", typeof(double));
            ItemTable.Columns.Add("item_amount", typeof(int));
            ItemTable.Columns.Add("item_sumprice", typeof(double));
            ItemTable.Columns.Add("item_vatprice", typeof(double));
            ItemTable.Columns.Add("item_netprice", typeof(double));
            ItemTable.Columns.Add("item_flag", typeof(string));

            #endregion

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(lookUpEditBuildingId.Text);

            if (DTDocInfo.Rows[0]["doc_saperate_reciept"].ToString() != "0")
            {
                RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
            }
                

            DTReciept.Rows.Add(
                   RecieptNO,
                   System.DBNull.Value,
                   System.DBNull.Value,
                   DateTime.Now,
                   1,
                   lookUpEditPrefix.Text + "||" + textEditName.EditValue.ToString() + "||" + textEditSurname.EditValue.ToString(),
                   textEditAddress.EditValue.ToString(),
                   textEditRoomLabel.EditValue.ToString(),
                   lookUpEditBuildingId.Text,
                   lookUpEditFloorId.Text,
                   lookUpEditRoomTypeId.Text,
                   DateTime.Now,
                   0,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   0,
                   DateTime.Now,
                   0,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   0,
                   DateTime.Now,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   sumprice,
                   price_vat,
                   sumprice_net,
                   0,
                   textEditRoomId.EditValue.To<int>(),
                   DTDocInfo.Rows[0]["company_name"],
                   DTDocInfo.Rows[0]["company_logo"],
                   DTDocInfo.Rows[0]["company_address"],
                   DTDocInfo.Rows[0]["company_telephone"],
                   DTDocInfo.Rows[0]["company_fax"],
                   DTDocInfo.Rows[0]["company_email"],
                   DTDocInfo.Rows[0]["company_website"],
                   DTDocInfo.Rows[0]["company_tax_id"],
                   DTDocInfo.Rows[0]["company_vision"],
                   DTDocInfo.Rows[0]["doc_header_reciept"],
                   DTDocInfo.Rows[0]["doc_footer_reciept"],
                   DTDocInfo.Rows[0]["doc_under_reciept1"],
                   DTDocInfo.Rows[0]["doc_under_reciept2"],
                   DTDocInfo.Rows[0]["doc_dateformat"],
                   DTDocInfo.Rows[0]["doc_logo_position"],
                   textEditMonthlyRate.EditValue.To<double>(),
                   DTDocInfo.Rows[0]["doc_vat_type"],
                   "-",
                   ThaiBaht,
                   check_in_id,
                   3,
                   2,
                   textEditInsurance.EditValue.To<double>(),
                   textEditAdvance.EditValue.To<int>(),
                   textEditBooking.EditValue.To<double>()
               );

            // Keep all to print reciept
            // loop insert to Reciept Item Table
            recieptID = BusinessLogicBridge.DataStore.createRecieptTransaction(DTReciept);

            // loop insert to Reciept Item Table
            #region ItemList Create
            if (recieptID > 0)
            {
                DataTable DTInvoiceItem = BusinessLogicBridge.DataStore.getItemsByCheckInID(check_in_id);
                DTInvoiceItem.Columns.Add("item_amount", typeof(double));
                DTInvoiceItem.Columns.Add("item_priceperunit", typeof(double));

                ItemTable.Clear();
                for (int itemcounter = 0; itemcounter < DTInvoiceItem.Rows.Count; itemcounter++)
                {
                    DTInvoiceItem.Rows[itemcounter]["item_amount"] = 1;
                    
                    // Items One time type only
                    if (DTInvoiceItem.Rows[itemcounter]["item_type"].To<int>() != 1)
                    {
                            
                            // Check monthly or dialy
                            if (lookUpEditContractType.EditValue.To<int>() == 3)
                            {
                                DTInvoiceItem.Rows[itemcounter]["item_priceperunit"] = DTInvoiceItem.Rows[itemcounter]["item_price_monthly"].To<double>();
                            }
                            else {
                                DTInvoiceItem.Rows[itemcounter]["item_priceperunit"] = DTInvoiceItem.Rows[itemcounter]["item_price_daily"].To<double>();
                            }

                            if (DTInvoiceItem.Rows[itemcounter]["item_vat"].To<int>() != 1)
                            {
                                if (DTInvoiceItem.Rows[itemcounter]["item_vat"].To<int>() == 2)
                                {
                                    item_sumprice = DTInvoiceItem.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItem.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                    item_netprice = item_sumprice;
                                    item_sumprice = item_netprice - item_vatprice;
                                }

                                if (DTInvoiceItem.Rows[itemcounter]["item_vat"].To<int>() == 3)
                                {
                                    item_sumprice = DTInvoiceItem.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItem.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                    item_netprice = item_sumprice + item_vatprice;
                                }
                            }
                            else
                            {
                                item_sumprice = DTInvoiceItem.Rows[itemcounter]["item_amount"].To<double>() * DTInvoiceItem.Rows[itemcounter]["item_priceperunit"].To<double>();
                                item_vatprice = 0.0;
                                item_netprice = item_sumprice;
                            }

                            item_priceperunit = DTInvoiceItem.Rows[itemcounter]["item_priceperunit"].To<double>();

                            total_sum_price_of_item += item_sumprice;
                            total_vat_price_of_item += item_vatprice;
                            total_net_price_of_item += item_netprice;

                            // AddItem
                            ItemTable.Rows.Add(recieptID, DTInvoiceItem.Rows[itemcounter]["item_id"].To<int>(), DTInvoiceItem.Rows[itemcounter]["item_name"].ToString(), 0, 0, DTInvoiceItem.Rows[itemcounter]["item_vat"].To<int>(), DTInvoiceItem.Rows[itemcounter]["item_type"].To<int>(), item_priceperunit, DTInvoiceItem.Rows[itemcounter]["item_amount"].To<int>(), item_sumprice, item_vatprice, item_netprice, DTInvoiceItem.Rows[itemcounter]["item_flag"].ToString());
                        
                    }
                }
                BusinessLogicBridge.DataStore.createRecieptItem(ItemTable);
            }

            // Insurance // Booking // Advance  // Room Price
            sumprice = total_sum_price_of_item + textEditInsurance.EditValue.To<double>() + (textEditAdvance.EditValue.To<int>() * textEditMonthlyRate.EditValue.To<double>()) - textEditBooking.EditValue.To<double>();

            sumprice_net = 0;

           // price_vat = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice; // 7 div 100 x price

            switch (DTDocInfo.Rows[0]["doc_vat_type"].To<int>())
            {
                case 1:
                    // no vat
                    price_vat =0.0;
                    price_vat += total_vat_price_of_item;
                    sumprice_net = sumprice + price_vat;
                    break;
                case 2:
                    // with net
                    price_vat = 0.0;
                    price_vat += total_vat_price_of_item;                    
                    sumprice_net = sumprice;
                    sumprice = sumprice_net - price_vat;                    
                    break;
                case 3:
                    // increase from net
                    price_vat = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice;
                    price_vat += total_vat_price_of_item;
                    sumprice_net = sumprice + price_vat;
                    break;
                default:
                    //sumprice_net = sumprice;
                    //price_vat = 0.0;
                    //price_vat += total_vat_price_of_item;
                    price_vat = 0.0;
                    price_vat += total_vat_price_of_item;
                    sumprice_net = sumprice + price_vat;
                    break;
            }

            ThaiBaht = MainForm.ThaiBaht(sumprice_net.ToString());

            BusinessLogicBridge.DataStore.updateRecieptPriceByID(sumprice, price_vat, sumprice_net, ThaiBaht, recieptID);


            // Clear Item Onetime out of Room Information
            for(int i=0; i<ItemTable.Rows.Count; i++){
                if (ItemTable.Rows[i]["item_type"].To<int>()==2)
                BusinessLogicBridge.DataStore.deleteRoomItemOneTime(textEditRoomId.EditValue.To<int>(), ItemTable.Rows[i]["item_id"].To<int>());
            }

            #endregion
        }

        void enabled(Boolean status)
        {

            bttSelectTenant.Enabled = status;
            //bttReadMeter.Enabled = status;
            dateEditCheckInDate.Enabled = status;
            lookUpEditContractType.Enabled = status;
            //
            lookUpEditPrefix.Enabled = status;
            textEditName.Enabled = status;
            textEditSurname.Enabled = status;
            textEditIDCard.Enabled = status;
            dateEditBirthDay.Enabled = status;
            textEditAddress.Enabled = status;
            textEditDistrict.Enabled = status;
            textEditProvince.Enabled = status;
            textEditPostcode.Enabled = status;
            textEditPhone.Enabled = status;
            textEditCarLicence.Enabled = status;
            textEditRemark.Enabled = status;
            //
            dateEditElectricMeter.Enabled = status;
            dateEditWaterMeter.Enabled = status;
            dateEditPhoneMeter.Enabled = status;
           
            bttAddExpense.Enabled = status;
            bttAddTenant.Enabled = status;
            //bttReadMeter.Enabled = status;
            bttRemoveTenant.Enabled = status;
            bttSelectTenant.Enabled = status;

            bttRecordManual.Enabled = status;

            //
            if (status == true)
            {
                enabledByContractType();

                bttSave.Enabled = true;
                bttCancel.Enabled = true;
                bttEdit.Enabled = false;
            }
            else
            {
                dateEditElectricMeter.Enabled = status;
                dateEditWaterMeter.Enabled = status;
                dateEditPhoneMeter.Enabled = status;

                textEditElectricMeter.Enabled = status;
                textEditWaterMeter.Enabled = status;
                //
                textEditMinimum.Enabled     = status;
                textEditInsurance.Enabled   = status;

                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttEdit.Enabled = true;

            }
            //
           
        }
        
        void enabledByContractType()
        {
            int contractTypeId = 0;
            if (lookUpEditContractType.EditValue != null)
            {
                contractTypeId = int.Parse(lookUpEditContractType.EditValue.ToString());
            }
            if (contractTypeId == 1)
            {
                dateEditElectricMeter.Enabled = false;
                dateEditWaterMeter.Enabled = false;
                dateEditPhoneMeter.Enabled = false;
                //
                textEditDaily.Enabled = true;
                textEditMinimum.Enabled = false;
            }
            else
            {
                dateEditElectricMeter.Enabled = true;
                dateEditWaterMeter.Enabled = true;
                bttReadMeter.Enabled = true;
                //
                textEditDaily.Enabled = false;
                textEditMinimum.Enabled = true;
            }
        }

        bool readElectricMeterSave(int room_id)
        {
            DataTable ElectricMeterTransaction = BusinessLogicBridge.DataStore.ReadRecordingByRoomAndDate(room_id, dateEditElectricMeter.EditValue.To<DateTime>());
            if (ElectricMeterTransaction.Rows.Count > 0 && ElectricMeterTransaction.Rows[0]["present_date_update"].ToString() != "")
            {
                if (record_manual_click == 0)
                {
                    int result = DateTime.Compare(dateEditElectricMeter.EditValue.To<DateTime>().Date, ElectricMeterTransaction.Rows[0]["present_date_update"].To<DateTime>().Date);

                    // Recordate > Lastest date of recording
                    if (result > 0)
                    {   
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
                        return false;
                    }
                    else
                    {  
                            textEditElectricMeter.EditValue = ElectricMeterTransaction.Rows[0]["present_energy_value"].To<double>().ToString("N2");
                    }
                }
            }
            //else
            //{
            //    textEditElectricMeter.EditValue = 0.00;
            //}
            return true;
        }

        void readElectricMeter(int room_id)
        {   
            DataTable ElectricMeterTransaction = BusinessLogicBridge.DataStore.ReadRecordingByRoomAndDate(room_id, dateEditElectricMeter.EditValue.To<DateTime>());


            if (ElectricMeterTransaction.Rows.Count > 0 && ElectricMeterTransaction.Rows[0]["present_date_update"].ToString()!="")
            {

                //DataTable ElectricPresent = BusinessLogicBridge.DataStore.ReadEmeterPresent(ElectricMeterTransaction.Rows[0]["meter_id"].To<int>());

                DataTable ElectricPresent = BusinessLogicBridge.DataStore.ReadRecordingByMeterAndDate(ElectricMeterTransaction.Rows[0]["meter_id"].To<int>(), dateEditElectricMeter.EditValue.To<DateTime>());

                int result = DateTime.Compare(dateEditElectricMeter.EditValue.To<DateTime>().Date, ElectricPresent.Rows[0]["present_date_update"].To<DateTime>().Date);

                if (result > 0)
                {
                    // Case left date  > transaction record date
                    if (read_click == 1)
                    {
                        // Case Read Click button
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
                        return;
                    }
                    else
                    {
                        textEditElectricMeter.EditValue = 0.00;
                    }
                }
                else
                {
                    textEditElectricMeter.EditValue = ElectricPresent.Rows[0]["TotalUnit"].To<double>().ToString("N2");
                }

            }
            else
            {
                textEditElectricMeter.EditValue = 0.00;
            }

            

            //if (ElectricMeterTransaction.Rows.Count > 0 && ElectricMeterTransaction.Rows[0]["present_date_update"].ToString()!="")
            //{
            //    int result = DateTime.Compare(dateEditElectricMeter.EditValue.To<DateTime>().Date, ElectricMeterTransaction.Rows[0]["present_date_update"].To<DateTime>().Date);

            //    if (result > 0)
            //    {   
            //        // Case left date  > transaction record date
            //        if (read_click == 1)
            //        {   
            //            // Case Read Click button
            //            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
            //            return;
            //        }
            //        else {
            //            textEditElectricMeter.EditValue = 0.00;
            //        }
            //    }
            //    else
            //    {
            //        textEditElectricMeter.EditValue = ElectricMeterTransaction.Rows[0]["present_energy_value"].To<double>().ToString("N2");
            //    }
            //}
            //else
            //{
            //    textEditElectricMeter.EditValue = 0.00;
            //}
        }

        bool readWaterMeterSave(int room_id)
        {
            DataTable WaterMeterTransaction = BusinessLogicBridge.DataStore.ReadWaterRecordingByRoomAndDate(room_id, dateEditWaterMeter.EditValue.To<DateTime>());

            if (WaterMeterTransaction.Rows.Count > 0 && WaterMeterTransaction.Rows[0]["wpresent_date_update"].ToString() != "")
            {
                if (record_manual_click == 0)
                {
                    int result = DateTime.Compare(dateEditWaterMeter.EditValue.To<DateTime>().Date, WaterMeterTransaction.Rows[0]["wpresent_date_update"].To<DateTime>().Date);

                    if (result > 0)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
                        return false;
                    }
                    else
                    {

                        textEditWaterMeter.EditValue = WaterMeterTransaction.Rows[0]["wpresent_energy_value"].To<double>().ToString("N2");

                    }
                }
            }
            //else
            //{
            //    textEditWaterMeter.EditValue = 0.00;
            //}
            return true;
        }

        void readWaterMeter(int room_id)
        {
            DataTable WaterMeterTransaction = BusinessLogicBridge.DataStore.ReadWaterRecordingByRoomAndDate(room_id, dateEditElectricMeter.EditValue.To<DateTime>());

            if (WaterMeterTransaction.Rows.Count > 0 && WaterMeterTransaction.Rows[0]["wpresent_date_update"].ToString() != "")
            {
                //DataTable WaterPresent = BusinessLogicBridge.DataStore.ReadWmeterPresent(WaterMeterTransaction.Rows[0]["water_id"].To<int>());

                DataTable WaterPresent = BusinessLogicBridge.DataStore.ReadWaterRecordingByMeterAndDate(WaterMeterTransaction.Rows[0]["water_id"].To<int>(), dateEditWaterMeter.EditValue.To<DateTime>());

                int result = DateTime.Compare(dateEditWaterMeter.EditValue.To<DateTime>().Date, WaterPresent.Rows[0]["wpresent_date_update"].To<DateTime>().Date);

                if (result > 0)
                {
                    if (read_click == 1)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
                        return;
                    }
                    else
                    {
                        textEditElectricMeter.EditValue = 0.00;
                    }
                }
                else
                {
                    textEditWaterMeter.EditValue = WaterPresent.Rows[0]["w_unit"].To<double>().ToString("N2");
                }

            }

            //if (WaterMeterTransaction.Rows.Count > 0 && WaterMeterTransaction.Rows[0]["wpresent_date_update"].ToString() != "")
            //{   
                
            //    int result = DateTime.Compare(dateEditWaterMeter.EditValue.To<DateTime>().Date, WaterMeterTransaction.Rows[0]["wpresent_date_update"].To<DateTime>().Date);
                
            //    if (result > 0)
            //    {
            //        if (read_click == 1)
            //        {
            //            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1008"), getLanguage("_softwarename"));
            //            return;
            //        }
            //        else
            //        {
            //            textEditElectricMeter.EditValue = 0.00;
            //        }
            //    }
            //    else
            //    {
            //        textEditWaterMeter.EditValue = WaterMeterTransaction.Rows[0]["wpresent_energy_value"].To<double>().ToString("N2");
            //    }
            //}
            //else
            //{
            //    textEditWaterMeter.EditValue = 0.00; 
            //}
        }
        #endregion

        #region Change Value
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion

        #region Button Event

        void printPreviewContract() {

            PrintDocuments.contract PrintContract = new DXWindowsApplication2.PrintDocuments.contract();

            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            string pathname = MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Contract", textEditContractNo.EditValue.ToString() + ".pdf");

            PrintContract.loopGenDataRow(check_in_id);

            PrintContract.ExportToPdf(pathname);
            PrintContract.ShowPreview();
        }

        void saveInfo() {
            try
            {
                DataTable _Error = validateDate();
                if (_Error.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _Error.Rows.Count; i++)
                    {
                        message = message + _Error.Rows[i]["label"] + " " + _Error.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                // Save Checkin Data
                bool boolCheckin = createCheckIn();

                if (boolCheckin == false) {
                    return;
                }

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4005"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    printPreviewContract();
                }
                else
                {
                    if (lookUpEditContractType.EditValue.To<int>()==3)  
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3006"), getLanguage("_softwarename"), "info");
                    }
                }

                initRoom();
                //
                changeRow();
                //
                enabled(false);
                //lookUpEditTenant.EditValue = null;
                lookUpEditContractType.EditValue = 1;
                gridControlRoom.Enabled = true;
                gridControlExpense.Enabled = false;
                gridControlRoommate.Enabled = false;
                //
                action_key = 0;
                button_event = "";                
                bttEdit.Enabled = true;

                bttPrint.Enabled = false;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;

                // Reload Navibar
                MForm.initRoomBarButton();
                DXWindowsApplication2.MainForm.setTogglePage();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_in_id != 0)
                {
                    saveInfo();
                }

                printPreviewContract();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            enabled(true);

            //
            gridControlRoom.Enabled = false;
            gridControlRoommate.Enabled = true;
            gridControlExpense.Enabled = true;
            //
            initTenantRoommate();
            //
            button_event = "edit";
            action_key = 0;
            bttEdit.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            if (readElectricMeterSave(textEditRoomId.EditValue.To<int>()) == false)
                return;
            if (readWaterMeterSave(textEditRoomId.EditValue.To<int>()) == false)
                return;
            
            saveInfo();
            BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Room Management [Check In]");
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {   
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"),getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                enabled(false);
                lookUpEditContractType.EditValue = 1;
                gridControlRoom.Enabled = true;
                gridControlExpense.Enabled = false;
                gridControlRoommate.Enabled = false;
                //
                action_key = 0;
                button_event = "";
                bttEdit.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
            }
        }
        #endregion

        #region Event Key
        private void lookUpEditContractType_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditContractType.Enabled == true)
            {
                enabledByContractType();
                action_key = 1;
            }
            int[] rowIndex = gridViewRoom.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(rowIndex[0]);

                DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CurrentRow["building_id"].To<int>());

                if (DTDocInfo.Rows.Count >0){

                if (DTDocInfo.Rows[0]["doc_saperate_contract"].ToString() == "0")
                {
                    textEditContractNo.EditValue = prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');
                }
                else
                {   
                    if (lookUpEditContractType.EditValue != null)
                    {
                        DataTable RoomTypeInfo = BusinessLogicBridge.DataStore.getRoomTypeByID(CurrentRow["roomtype_id"].To<int>());

                        if (lookUpEditContractType.EditValue.To<int>() == 3)
                        {
                            textEditContractNo.EditValue = prefix + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');

                            textEditMonthlyRate.EditValue = RoomTypeInfo.Rows[0]["roomtype_month_roomrate_price"];
                            textEditAdvance.EditValue = RoomTypeInfo.Rows[0]["roomtype_month_advance_amount"];
                            textEditInsurance.EditValue = RoomTypeInfo.Rows[0]["roomtype_month_insure_price"];
                        }
                        else
                        {
                            textEditContractNo.EditValue = prefix + "D" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.RoomCheckIn_genContractId().ToString().PadLeft(6, '0');

                            textEditMonthlyRate.EditValue = RoomTypeInfo.Rows[0]["roomtype_daily_roomrate_price"];
                            textEditAdvance.EditValue = 0;
                            textEditInsurance.EditValue = 0;
                        }
                    }
                }
            }
            }

        }
        private void lookUpEditTenant_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
        }
        #endregion

        private void bttReadMeter_Click(object sender, EventArgs e)
        {
            textEditElectricMeter.Enabled = false;
            textEditWaterMeter.Enabled = false;

            read_click = 1;
            record_manual_click = 0;

            readElectricMeter(textEditRoomId.EditValue.To<int>());
            readWaterMeter(textEditRoomId.EditValue.To<int>());
        }

        private void bttRecordManual_Click(object sender, EventArgs e)
        {
            record_manual_click = 1;
            read_click = 0;
            textEditElectricMeter.Enabled = true;
            textEditWaterMeter.Enabled = true;
        }
    
    }
}
