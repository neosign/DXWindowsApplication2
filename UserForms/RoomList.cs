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
    public partial class RoomList : uBase
    {
        private string button_event = "";
        private int action_key = 0;
        public static XtraMessageBoxForm AddPanel;

        public static DataTable RoommateTableTemp;
        public static DataTable ItemTableTemp;
        public static TextEdit TextEditTrigger;
        public static List<string> tempDeleteOccupier;
        public static int checkin_temp_id;

        public RoomList()
        {
            InitializeComponent();
            //

            RoommateTableTemp = null;

            TextEditTrigger = new TextEdit();
            TextEditTrigger.EditValue = 0;
            this.Load += new EventHandler(RoomList_Load);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            this.bttEdit.Click += new EventHandler(bttEdit_Click);
            this.bttCancel.Click += new EventHandler(bttCancel_Click);
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttAddTenant.Click += new EventHandler(bttAddTenant_Click);
            this.bttAddExpense.Click += new EventHandler(bttAddExpense_Click);
            this.bttRemoveTenant.Click += new EventHandler(bttRemoveTenant_Click);
            TextEditTrigger.TextChanged += new EventHandler(TextEditTrigger_TextChanged);
            SaveClick += new EventHandler(bttSave_Click);
        }

        public int selectRoomID = 0;

        void bttRemoveTenant_Click(object sender, EventArgs e)
        {
            int[] rowIndex = gridView2.GetSelectedRows();
            if (rowIndex.Length == 0)
            {
                return;
            }
            
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4001"),getLanguage("_softwarename")) == DialogResult.Yes)
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

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
            //
            initRoom();
            //
            if (selectRoomID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlRoom.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gridView1.FocusedRowHandle = c.LocateByValue("room_id", selectRoomID);
                //
                selectRoomID = 0;
            }
            //
            changeRow();
        }

        void bttAddExpense_Click(object sender, EventArgs e)
        {
            ItemTableTemp = utilClass.showPopAddExpense(this, ItemTableTemp);
            //
            initItem();
        }

        void initItem()
        {
                int room_id = textEditRoomID.EditValue.To<int>();

                DataTable ItemTable = BusinessLogicBridge.DataStore.RoomInfo_getItem(room_id);

                DataTable ItemTableRoomType = BusinessLogicBridge.DataStore.RoomCheckIn_getItemByRoomtypeId(lookUpEditRoomTypeId.EditValue.To<int>());

                int counterRoomInfoItem =0;
                for (int k = 0; k < ItemTable.Rows.Count; k++)
                {
                    if (ItemTable.Rows[k]["status"].To<int>() != 0) {
                        counterRoomInfoItem++;
                    }
                }

                int counterRoomTypeItem = 0;
                for (int k = 0; k < ItemTableRoomType.Rows.Count; k++)
                {
                    if (ItemTableRoomType.Rows[k]["status"].To<int>() != 0)
                    {
                        counterRoomTypeItem++;
                    }
                }

                // vacant room
                if (counterRoomInfoItem > counterRoomTypeItem)
                {
                    if (textEditRoomStatus.EditValue.To<int>() == 1 || textEditRoomStatus.EditValue.To<int>() == 3)
                        ItemTable = ItemTableRoomType;
                }
                else {

                    if (textEditRoomStatus.EditValue.To<int>() == 1 || textEditRoomStatus.EditValue.To<int>() == 3)
                        ItemTable = ItemTableRoomType;
                }
                

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
                }
                //
                if (ItemTableTemp == null)
                    ItemTableTemp = ItemTable.Clone();
                //
                gridControlExpense.DataSource = ItemTable;
          
            //

            //
        }

        void RoomList_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //
            initRoom();
            initTenantRoommate();
            //
            initDropDownBuilding(); 
            initDropDownRoomType();            
            //initDropDownElectriMeter();
            //initDropDownWaterMeter();
            //initDropDownPhoneMeter();
            //
            setLangThis();
        }

        void TextEditTrigger_TextChanged(object sender, EventArgs e)
        {
            changeRow();
        }

        public void setLangThis()
        {
            //
            //this.Name = getLanguage("room_info");
            //
            this.groupRoomList.Text = getLanguage("_room_list");
            this.groupRoomInfo.Text = getLanguage("_room_info");
            this.groupRoomType.Text = getLanguage("_rental_type");
            this.groupRental.Text = getLanguage("_rental");
            this.groupExpense.Text = getLanguage("_addittional_cost");
            this.groupOccupie.Text = getLanguage("_occupier");
            //
            this.grid_room_label.Caption = getLanguage("_room_name");
            this.grid_buiding_code.Caption = getLanguage("_building");
            this.grid_floor_code.Caption = getLanguage("_floor");
            this.grid_roomtype.Caption = getLanguage("_room_type");
            this.grid_room_status_label.Caption = getLanguage("_status");
            this.grid_rent_type.Caption = getLanguage("_rental_type");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            this.labelControlRoomStatus.Text = getLanguageWithColon("_status");
            //
            this.labelControlDailyRate.Text = getLanguageWithColon("_rent");
            this.labelControlMonthlyRate.Text = getLanguageWithColon("_rent");
            this.labelControlBeforeRent.Text = getLanguageWithColon("_advance_charge");
            this.labelControlInsurance.Text = getLanguageWithColon("_insurance_charge");
            //
            this.radioDaily.Text = getLanguage("_daily");
            this.radioMonthly.Text = getLanguage("_monthly");
            //
            this.labelControlNamePrefix.Text = getLanguageWithColon("_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlSurname.Text = getLanguageWithColon("_lastname");
            this.labelControlTel.Text = getLanguageWithColon("_tel");

            //Grid
            this.gridColumnOrder.Caption = getLanguage("_no");
            this.gridColumnList.Caption = getLanguage("_item");
            this.gridColumnDaily.Caption = getLanguage("_price_per_day");
            this.gridColumnMonthly.Caption = getLanguage("_price_per_month");
            this.gridColumnType.Caption = getLanguage("_payment_format");
            this.gridColumnStatus.Caption = getLanguage("_status");
            //
            this.grid_tenant_prefix.Caption = getLanguage("_prefix");
            this.grid_tenant_firstname.Caption = getLanguage("_firstname");
            this.grid_tenant_surname.Caption = getLanguage("_lastname");
            this.grid_tenant_idcard.Caption = getLanguage("_idcard_passport");
            this.grid_tenant_tel.Caption = getLanguage("_tel");
            this.grid_tenant_prefix.FieldName = "prefix_" + current_lang + "_label";
            //

            this.bttAddTenant.Text = getLanguage("_add");
            this.bttRemoveTenant.Text = getLanguage("_delete");
            this.bttAddExpense.Text = getLanguage("_add");
            //
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            //
            this.labelControlMonth.Text = getLanguage("_month");
            this.labelControlBath1.Text = getLanguage("_baht");
            this.labelControlBath2.Text = getLanguage("_baht");
            this.labelControlBath3.Text = getLanguage("_baht");

        }

        #region Setup
        void initDataDefault()
        {
            textEditRoomID.EditValue = 0;    //1
            textEditRoomCodeRef.EditValue = ""; //12
            textEditRoomLabel.EditValue = "";  //2
            textEditPrefix.EditValue = "";
            lookUpEditBuildingId.EditValue = 0;
            lookUpEditRoomTypeId.EditValue = 0; //3
            textEditRoomStatus.EditValue = 0; //11
            lookUpEditFloorId.EditValue = null; //4
            //textEditCurrentMeterStatus.EditValue = "";
            //lookUpEditElectriMeterId.EditValue = null;
            //lookUpEditWaterMeterId.EditValue = null;
            //lookUpEditPhoneMeterId.EditValue = null;
            textEditBeforeRent.EditValue = 0;
            lookUpEditFloorId.EditValue = null;
        }

        void initTenantRoommate() 
        {
            if (textEditTenantId.EditValue == null)
                return;
            //
            if (textEditTenantId.EditValue.ToString() != "")
            {
                int roomTempID = textEditRoomID.EditValue.To<int>();
                //
                if (RoommateTableTemp == null)
                    RoommateTableTemp = BusinessLogicBridge.DataStore.getRoommateByRoomID(roomTempID, current_lang);
                
                //
                if (bttEdit.Enabled == false)
                    bttRemoveTenant.Enabled = RoommateTableTemp.Rows.Count > 0;
            }

            //
            gridControlTenant.DataSource = RoommateTableTemp;
            
        }
        void initRoom()
        {
            DataTable Room = BusinessLogicBridge.DataStore.Room_Checkin_get();
            //
            Room.Columns.Add("check_in_contracttype_text", typeof(string));
            //
            foreach (DataRow dr in Room.Rows)
            {

                if (dr["room_status"].To<int>() == 2)
                {

                    if (dr["check_in_contracttype"].ToString() == "1")
                        dr["check_in_contracttype_text"] = getLanguage("_daily");
                    else if (dr["check_in_contracttype"].ToString() == "3")
                        dr["check_in_contracttype_text"] = getLanguage("_monthly");

                }
                else
                {
                    dr["check_in_contracttype_text"] = "";
                }
            }
            //
            gridControlRoom.DataSource = Room;
            //
            if (Room.Rows.Count == 0)
                bttEdit.Enabled = false;
        }

       /* void initItem(int checkInID)
        {
            DataTable ItemTable = BusinessLogicBridge.DataStore.getItemsByCheckInID(checkInID);
            ItemTable.Columns.Add("check_in_id", typeof(int));

            if (ItemTableTemp.Rows.Count <= ItemTable.Rows.Count)
            {
                ItemTableTemp = ItemTable.Copy();
                ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
                ItemTableTemp.Columns.Add("item_type_label", typeof(String));

                for (int i = 0; i < ItemTableTemp.Rows.Count; i++)
                {
                    
                    ItemTableTemp.Rows[i]["check_in_id"] = checkInID;
                    if (Convert.ToInt32(ItemTableTemp.Rows[i]["item_type"]) == 1)
                    {
                        ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_monthly");
                    }
                    else
                    {
                        ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_onetime");
                    }

                    ItemTableTemp.Rows[i]["check_box"] = true;
                }

                gridControlExpense.DataSource = ItemTableTemp;
            }
            else if (ItemTable.Rows.Count != 0)
            {
                ItemTableTemp = ItemTable;
                ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
                ItemTableTemp.Columns.Add("item_type_label", typeof(String));

                for (int i = 0; i < ItemTableTemp.Rows.Count; i++)
                {
                    ItemTableTemp.Rows[i]["check_in_id"] = checkInID;
                    if (Convert.ToInt32(ItemTableTemp.Rows[i]["item_type"]) == 1)
                    {
                        ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_monthly");
                    }
                    else
                    {
                        ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_onetime");
                    }

                    ItemTableTemp.Rows[i]["check_box"] = true;
                }

                gridControlExpense.DataSource = ItemTableTemp;
            }
            else {
                gridControlExpense.DataSource = ItemTable;
            }
        }
        */
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

        void changeRow()
        {            
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length <= 0)
                return;

            if (rowIndex.Length != 0)
            {
                
                DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]); 
                if (CurrentRow == null)
                {
                    CurrentRow = gridView1.GetDataRow(0);
                }
                int room_id = Convert.ToInt32(CurrentRow["room_id"].ToString());
                DataTable RoomDetail = BusinessLogicBridge.DataStore.getDataRoomById(room_id);
                textEditRoomID.EditValue = room_id;    //1
                textEditRoomCodeRef.EditValue = CurrentRow["coderef"].ToString(); //12
                textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();  //2
                textEditRoomStatus.EditValue = CurrentRow["room_status"]; //11
                textEditRoomStatusInfo.EditValue = CurrentRow["room_status_label"]; //11
                //

                if (CurrentRow["tenant_prefix_id"].ToString() != "")
                {
                    DataTable prefix_label = BusinessLogicBridge.DataStore.getPrefixByID(Convert.ToInt32(CurrentRow["tenant_prefix_id"]));

                    string prefix_field = "prefix_" + current_lang + "_label";

                    textEditPrefix.EditValue = prefix_label.Rows[0][prefix_field];
                }
                else {
                    textEditPrefix.EditValue = "";
                }
                textEditTenantId.EditValue = CurrentRow["tenant_id"].ToString();
                textEditName.EditValue = CurrentRow["tenant_name"].ToString();
                textEditLastName.EditValue = CurrentRow["tenant_surname"].ToString();
                textEditTel.EditValue = CurrentRow["tenant_phone"].ToString();
                lookUpEditBuildingId.EditValue = CurrentRow["building_id"];
                lookUpEditRoomTypeId.EditValue = CurrentRow["roomtype_id"]; //3                
                lookUpEditFloorId.EditValue = CurrentRow["floor_id"]; //4

                textEditMonthlyRate.EditValue = RoomDetail.Rows[0]["roomtype_month_roomrate_price"];
                textEditBeforeRent.EditValue = RoomDetail.Rows[0]["roomtype_month_advance_amount"];
                textEditInsurance.EditValue = RoomDetail.Rows[0]["roomtype_month_insure_price"];
                textEditDailyRate.EditValue = RoomDetail.Rows[0]["roomtype_daily_roomrate_price"];
                // Check room type Information
                if (CurrentRow["check_in_contracttype"].To<int>() == 1 && CurrentRow["room_status"].To<int>()!=1)
                {
                    radioDaily.Checked = true;
                }
                else if (CurrentRow["check_in_contracttype"].To<int>() == 3)
                {
                    radioMonthly.Checked = true;
                }
                else {
                    radioMonthly.Checked = true;
                }

                //if (CurrentRow["room_status"].ToString() != "2")
                //{
                //    bttEdit.Enabled = false;
                //}
                //else {
                //    bttEdit.Enabled = true;
                //}
                bttEdit.Enabled = true;

                initDropDownFloor(Convert.ToInt32(CurrentRow["building_id"]));
                //
                RoommateTableTemp = null;
                ItemTableTemp = null;
                
                //
                initTenantRoommate();
                if (Convert.ToInt32(CurrentRow["check_in_id"]) != 0)
                {
                    checkin_temp_id = Convert.ToInt32(CurrentRow["check_in_id"]);
                }
                
                initItem();
            }
            else
            {
                initDataDefault();
            }
        }
        #endregion

        #region Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion

        #region Button Event


        void bttAddTenant_Click(object sender, EventArgs e)
        {
            int tenant_id = 0;
            if (textEditTenantId.EditValue.ToString() != "")
                tenant_id = int.Parse(textEditTenantId.EditValue.ToString());
            //
            DialogResult dr = utilClass.showPopAddTenant(this, ref RoommateTableTemp, tenant_id);
            //
            if (dr == DialogResult.OK)
                initTenantRoommate();
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            button_event = "edit";
            action_key = 0;
            bttEdit.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            gridControlRoom.Enabled = false;
            gridControlExpense.Enabled = true;
            bttAddTenant.Enabled = true;
            bttAddExpense.Enabled = true;
            bttRemoveTenant.Enabled = true;
            gridControlTenant.Enabled = true;

            if (textEditRoomStatus.EditValue.To<int>() == 1 || textEditRoomStatus.EditValue.To<int>() == 3)
            {
                bttAddExpense.Enabled = false;
                bttAddTenant.Enabled = false;
                bttRemoveTenant.Enabled = false;
                gridControlTenant.Enabled = false;
                gridControlExpense.Enabled = false;
            }
        }
        private void bttSave_Click(object sender, EventArgs e)
        {
                int room_id = int.Parse(textEditRoomID.EditValue.ToString());
                BusinessLogicBridge.DataStore.Room_removeItemByRoomId(room_id);

                if (textEditRoomID.EditValue.ToString() != "")
                {
                    room_id = textEditRoomID.EditValue.To<int>();
                    //
                    BusinessLogicBridge.DataStore.deleteRoommateByRoomID(room_id);
                    //
                    BusinessLogicBridge.DataStore.insertRoommate(RoommateTableTemp);
                    //
                }
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
                        if (check_box)
                        {
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
                            DataTable RoomItemTable = new DataTable();
                            RoomItemTable.Columns.Add("room_id", typeof(int));
                            RoomItemTable.Columns.Add("item_id", typeof(int));
                            RoomItemTable.Columns.Add("date_created", typeof(DateTime));
                            RoomItemTable.Rows.Add(room_id, item_id, DateTime.Now);
                            BusinessLogicBridge.DataStore.RoomCheckIn_insertRoomItem(RoomItemTable);

                        }
                    }

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                    initRoom();
                   
                
            
            changeRow();
            action_key = 0;
            button_event = "";
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            bttEdit.Enabled = true;
            gridControlRoom.Enabled = true;
            gridControlExpense.Enabled = false;
            gridControlTenant.Enabled = false;
            bttAddTenant.Enabled = false;
            bttAddExpense.Enabled = false;
            bttRemoveTenant.Enabled = false;
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {   
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                //
                action_key = 0;
                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttEdit.Enabled = true;
                gridControlRoom.Enabled = true;
                gridControlExpense.Enabled = false;
                bttAddTenant.Enabled = false;
                bttAddExpense.Enabled = false;
                bttRemoveTenant.Enabled = false;
                gridControlTenant.Enabled = false;
            }
        }
        #endregion

        #region Key Event
        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue.ToString() == "False")
            {
                DataRow CurrentRow = gridView2.GetDataRow(e.RowHandle);
                gridView2.Columns[0].View.SetRowCellValue(e.RowHandle, "check_box", true);
            }
            else if (e.CellValue.ToString() == "True")
            {
                DataRow CurrentRow = gridView2.GetDataRow(e.RowHandle);
                gridView2.Columns[0].View.SetRowCellValue(e.RowHandle, "check_box", false);
            }
            action_key = 1;
        }
        #endregion
    }
}
