using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Timers;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class RoomPopupDetail : uFormBase
    {
        public int room_id = 0;
        //
        public RoomPopupDetail()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
            //
            this.Load += new EventHandler(RoomPopupDetail_Load);
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
        }

        void RoomPopupDetail_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //
            initDropDownBuilding();
            initDropDownRoomType();
            initDropDownElectriMeter();
            initDropDownWaterMeter();
            initDropDownPhoneMeter();
            //
            loadData();
            //initTimeItervalMeterRead();
        }

        void loadData()
        {

            DataTable RoomDTEMeter = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(room_id);

            double totalUnitE = 0;

            if (RoomDTEMeter.Rows[0]["present_energy_value"].ToString() != "" && RoomDTEMeter.Rows[0]["previous_energy_billing"].ToString() != "")
            {
                totalUnitE = utilClass.CalculateUnitEWMeter(RoomDTEMeter.Rows[0]["present_energy_value"].To<double>(), RoomDTEMeter.Rows[0]["previous_energy_billing"].To<double>());
            }
            textEditEMeterLastest.EditValue = RoomDTEMeter.Rows[0]["present_energy_value"].To<double>().ToString("N1");
            textEditEMeterBegin.EditValue = RoomDTEMeter.Rows[0]["previous_energy_billing"].To<double>().ToString("N1");
            textEditDiffEUnit.EditValue = totalUnitE.ToString("N1");


            DataTable RoomDTWMeter = BusinessLogicBridge.DataStore.getW_EndValueByRoomID(room_id);
            double totalUnitW = 0;

            if (RoomDTWMeter.Rows[0]["wpresent_energy_value"].ToString() != "" && RoomDTWMeter.Rows[0]["wprevious_energy_billing"].ToString() != "")
            {
                totalUnitW = utilClass.CalculateUnitEWMeter(RoomDTWMeter.Rows[0]["wpresent_energy_value"].To<double>(), RoomDTWMeter.Rows[0]["wprevious_energy_billing"].To<double>());
            }

            textEditWMeterLastest.EditValue = RoomDTWMeter.Rows[0]["wpresent_energy_value"].To<double>().ToString("N1");
            textEditWMeterBegin.EditValue = RoomDTWMeter.Rows[0]["wprevious_energy_billing"].To<double>().ToString("N1");
            textEditDiffWUnit.EditValue = totalUnitW.ToString("N1");


            //textEditCurrentB.EditValue = RoomDT.Rows[0]["meter_models"].ToString();

            if (RoomDTEMeter.Rows[0]["meter_models"].ToString().Contains("SX") == true)
            {
                labelB.Visible = false;
                labelC.Visible = false;
                labelN.Visible = false;
                
                textEditCurrentB.Visible = false;
                textEditCurrentC.Visible = false;
                textEditCurrentN.Visible = false;
            }
            else{

                labelB.Visible = true;
                labelC.Visible = true;
                labelN.Visible = true;
                textEditCurrentB.Visible = true;
                textEditCurrentC.Visible = true;
                textEditCurrentN.Visible = true;
            }

            DataRow CurrentRow = BusinessLogicBridge.DataStore.getDataRoomById(room_id).Rows[0];
            textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();  //2
            textEditRoomStatusInfo.EditValue = CurrentRow["room_status_label"]; //11
            //
            textEditName.EditValue = CurrentRow["tenant_name"].ToString();
            textEditTel.EditValue = CurrentRow["tenant_phone"].ToString();
            lookUpEditBuildingId.EditValue = CurrentRow["building_id"];
            lookUpEditRoomTypeId.EditValue = CurrentRow["roomtype_id"]; //3                
            lookUpEditFloorId.EditValue = CurrentRow["floor_id"]; //4
            //
            textEditMonthlyRate.EditValue = CurrentRow["roomtype_month_roomrate_price"];
            textEditBeforeRent.EditValue = CurrentRow["roomtype_month_advance_amount"];
            textEditInsurance.EditValue = CurrentRow["roomtype_month_insure_price"];
            textEditDailyRate.EditValue = CurrentRow["roomtype_daily_roomrate_price"];
            //
            var dtCheckIn = BusinessLogicBridge.DataStore.getCheckinByRoomAndTenantID(room_id, CurrentRow["tenant_id"].To<int>(),0);
            if (dtCheckIn.Rows.Count > 0)
            {
                var drCheckIn = dtCheckIn.Rows[0];
                //
                //Check room type Information
                if (Convert.ToInt32(drCheckIn["check_in_contracttype"]) == 1)
                {
                    textEditRentType.Text = getLanguage("_roomtype_daily");
                }
                else if (Convert.ToInt32(drCheckIn["check_in_contracttype"]) == 3)
                {
                    textEditRentType.Text = getLanguage("_roomtype_monthly");
                }
            }
            //
            if (CurrentRow["roomtype_icon"].ToString() != "")
            {
                string file_path = CurrentRow["roomtype_icon"].ToString();
                if(File.Exists(file_path))
                    picRoomType.Image = System.Drawing.Image.FromFile(file_path);
            }
            //
            initDropDownFloor(Convert.ToInt32(CurrentRow["building_id"]));
            //
            setLangThis();
            initItem();
        }

        public void setLangThis()
        {
            btRoomDetail.Text = getLanguage("_room_detail");
            this.groupRental.Text = getLanguage("_rental");
            this.groupExpense.Text = getLanguage("_addittional_cost");
            this.groupDaily.Text = getLanguage("_roomtype_daily");
            this.groupMonthly.Text = getLanguage("_roomtype_monthly");
            //

            this.lbCurrent.Text = getLanguageWithColon("_current");
            this.labelA.Text = getLanguage("_phaseA");
            this.labelB.Text = getLanguage("_phaseB");
            this.labelC.Text = getLanguage("_phaseC");
            this.labelN.Text = getLanguage("_phaseN");
            //
            this.labelRealTimeMeter.Text = getLanguage("_last_meter");
            this.labelPreviousMeter.Text = getLanguage("_prev_meter");
            this.labelUnit.Text = getLanguage("_unit");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            this.labelControlRentType.Text = getLanguageWithColon("_rental");
            this.labelControlRoomStatus.Text = getLanguageWithColon("_status");
            //
            this.labelControlDailyRate.Text = getLanguageWithColon("_rent");
            this.labelControlMonthlyRate.Text = getLanguageWithColon("_rent");
            this.labelControlBeforeRent.Text = getLanguageWithColon("_advance_charge");
            this.labelControlInsurance.Text = getLanguageWithColon("_insurance_charge");
            //
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlTel.Text = getLanguageWithColon("_tel");

            //Grid
            this.gridColumnOrder.Caption = getLanguage("_no");
            this.gridColumnList.Caption = getLanguage("_item");
            this.gridColumnDaily.Caption = getLanguage("_price_per_day");
            this.gridColumnMonthly.Caption = getLanguage("_price_per_month");
            this.gridColumnType.Caption = getLanguage("_payment_format");
            this.gridColumnStatus.Caption = getLanguage("_status");

            this.labelControlMonth.Text = getLanguage("_month");
            this.labelControlBath1.Text = getLanguage("_baht");
            this.labelControlBath2.Text = getLanguage("_baht");
            this.labelControlBath3.Text = getLanguage("_baht");
        }

        void initItem()
        {
            DataTable ItemTable = BusinessLogicBridge.DataStore.RoomInfo_getItem(room_id);

            DataTable ItemTableRoomType = BusinessLogicBridge.DataStore.RoomCheckIn_getItemByRoomtypeId(lookUpEditRoomTypeId.EditValue.To<int>());

            int counterRoomInfoItem = 0;
            for (int k = 0; k < ItemTable.Rows.Count; k++)
            {
                if (ItemTable.Rows[k]["status"].To<int>() != 0)
                {
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

            DataTable roominfo = BusinessLogicBridge.DataStore.getRoomById(room_id);

            int room_status = roominfo.Rows[0]["room_status"].To<int>();

            // vacant room
            if (counterRoomInfoItem > counterRoomTypeItem)
            {
                if (room_status == 1 || room_status == 3)
                    ItemTable = ItemTableRoomType;
            }
            else
            {

                if (room_status == 1 || room_status == 3)
                    ItemTable = ItemTableRoomType;
            }


            ItemTable.Columns.Add("item_order", typeof(int));
            ItemTable.Columns.Add("item_type_label", typeof(String));
            ItemTable.Columns.Add("check_box", typeof(Boolean));

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
            gridControlExpense.DataSource = ItemTable;

            //

            //
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
        void initDropDownElectriMeter()
        {
            //DataTable ElectriMeterTable = BusinessLogicBridge.DataStore.Room_getElectriMeter();
            //lookUpEditElectriMeterId.Properties.DataSource = ElectriMeterTable;
            //lookUpEditElectriMeterId.Properties.DisplayMember = "meter_label";
            //lookUpEditElectriMeterId.Properties.ValueMember = "meter_id";
            //lookUpEditElectriMeterId.Properties.NullText = "[เลือกมิเตอร์ไฟฟ้า]";
        }
        void initDropDownWaterMeter()
        {
            //DataTable WaterMeterTable = BusinessLogicBridge.DataStore.Room_getWaterMeter();
            //lookUpEditWaterMeterId.Properties.DataSource = WaterMeterTable;
            //lookUpEditWaterMeterId.Properties.DisplayMember = "meter_label";
            //lookUpEditWaterMeterId.Properties.ValueMember = "water_id";
            //lookUpEditWaterMeterId.Properties.NullText = "[เลือกมิเตอร์น้ำddddddddddddddddd;
        }
        void initDropDownPhoneMeter()
        {
            //DataTable PhoneMeterTable = BusinessLogicBridge.DataStore.Room_getPhoneMeter();
            //lookUpEditPhoneMeterId.Properties.DataSource = PhoneMeterTable;
            //lookUpEditPhoneMeterId.Properties.DisplayMember = "phone_label";
            //lookUpEditPhoneMeterId.Properties.ValueMember = "phone_id";
            //lookUpEditPhoneMeterId.Properties.NullText = "[เลือกมิเตอร์โทรศัพท์]";
        }

        private void initTimeItervalMeterRead()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 400;
            aTimer.Enabled = true;
            // aTimer.Start();


            // If the timer is declared in a long-running method, use
            // KeepAlive to prevent garbage collection from occurring
            // before the method ends.
            GC.KeepAlive(aTimer);




        }
        delegate void SetTextDelegate(string value);
        public void SetText(string value)
        {
            if (InvokeRequired)
                Invoke(new SetTextDelegate(SetText), value);
            else
                //if (textEditRealTimeMeter.EditValue == "")
                //{
                //    textEditRealTimeMeter.EditValue = 0;
                //}
                //else {
                //    textEditRealTimeMeter.EditValue = (int)textEditRealTimeMeter.EditValue + Convert.ToInt32(value);
                //}

                if ((textEditEMeterLastest.EditValue == null) || (textEditEMeterLastest.Text == ""))
                {
                    textEditEMeterLastest.EditValue = "0.00";
                }


            textEditEMeterBegin.EditValue = String.Format("{0:0.00}", 30.00);
            textEditDiffEUnit.EditValue = String.Format("{0:0.00}", Convert.ToDouble(value) - Convert.ToDouble(textEditEMeterBegin.EditValue));
            textEditEMeterLastest.EditValue = String.Format("{0:0.00}", Convert.ToDouble(value));
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //textEditRealTimeMeter.EditValue 


            double y = Convert.ToDouble(textEditEMeterLastest.EditValue) + 0.41;






            string number = String.Format("{0:0.00}", y);
            SetText(number);
        }



    }
}
