using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoRoomType : uBase
    {
        public static XtraMessageBoxForm AddPanel;
        private DataTable _GeneralConfigTable;
        private string button_event = "";
        public static DataTable ItemTableTemp;
        public static TextEdit TextEditTrigger;
        private bool change_row_case = false;
        private bool event_check_click_cost_items = false;

        public BasicInfoRoomType()
        {
            InitializeComponent();
            ItemTableTemp = new DataTable();
            TextEditTrigger = new TextEdit();
            TextEditTrigger.EditValue = 0;
            this.Dock = DockStyle.Fill;
            
            this.Load += new EventHandler(BasicInfoRoomType_Load);
            this.Resize += new EventHandler(BasicInfoRoomType_Resize);
            TextEditTrigger.TextChanged += new EventHandler(TextEditTrigger_TextChanged);
            SaveClick += new EventHandler(BasicInfoRoomType_SaveClick);

            checkEditMonthly.CheckedChanged +=new EventHandler(checkEditMonthly_CheckedChanged);
        }

        void BasicInfoRoomType_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
            //
            initRoomType();
            //
            changeRow();
        }

        void TextEditTrigger_TextChanged(object sender, EventArgs e)
        {
            changeRow();
            bttOpenItemTab.Enabled = true;
        }

        void BasicInfoRoomType_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
        }

        void BasicInfoRoomType_Load(object sender, EventArgs e)
        {   
                splitContainerControl2.SplitterPosition = (this.Width * 40)/100;

                setLangThis();
            // Loaded is Load Data for good perfomance
                _GeneralConfigTable = BusinessLogicBridge.DataStore.getGeneralConfig();
                initRoomType();
                changeRow();
        }

        public void setLangThis()
        {

            this.groupControlRoomtypeList.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_list");
            this.groupControlRoomTypeInfo.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_info");
            this.labelControlRoomTypeLabel.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_name");
            this.labelControl1IconRoomType.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_icon");
            this.groupBoxPublic.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_public");
            this.checkEditMonthly.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_monthly");
            this.labelControlRoomTypeRate.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_rent_charge");
            this.labelControlInsurerate.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_insurance_charge");
            this.labelControlDeposit.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_advance_charge");
            this.labelControlMonth.Text = DXWindowsApplication2.MainForm.getLanguage("_month");
            this.labelControlBaht.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht2.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht3.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht4.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht5.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht6.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
           // this.labelControlBaht7.Text = WindowsApplication1.MainForm.getLanguageWithColon("_baht");
            this.labelControlPerUnit.Text = DXWindowsApplication2.MainForm.getLanguage("_rate_per_unit");
            this.labelControlMinrate.Text = DXWindowsApplication2.MainForm.getLanguage("_minimum");
            this.labelControlLumpLabel.Text = DXWindowsApplication2.MainForm.getLanguage("_lump");
            this.checkEditMonthlyElectric.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_electric");
            this.checkEditMonthlyWater.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_water");
            this.checkEditMonthlyPhone.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_phone");
            this.textEditRoomTypePhoneUnitRateMonthly.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_use_data_from_pabx");

            // Daily Section
            this.checkEditDaily.Text            = DXWindowsApplication2.MainForm.getLanguage("_roomtype_daily");
            this.groupControlAdditional.Text    = DXWindowsApplication2.MainForm.getLanguage("_addittional_cost");
            this.groupBoxPublicDaily.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_public");
            this.labelControlRoomTypeRateDaily.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_rent_charge");
            this.labelControlDepositDaily.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_advance_charge");
            this.labelControlMonth2.Text = DXWindowsApplication2.MainForm.getLanguage("_month");
            this.labelControlBaht7.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht8.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht9.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht10.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlBaht11.Text = DXWindowsApplication2.MainForm.getLanguage("_baht");
            this.labelControlPerUnitDaily.Text = DXWindowsApplication2.MainForm.getLanguage("_rate_per_unit");
            this.labelControlMinrateDaily.Text = DXWindowsApplication2.MainForm.getLanguage("_minimum");
            this.labelControlLumpLabelDaily.Text = DXWindowsApplication2.MainForm.getLanguage("_lump");
            this.checkEditDailyElectric.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_electric");
            this.checkEditDailyWater.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_water");
            this.checkEditDailyPhone.Text = DXWindowsApplication2.MainForm.getLanguageWithColon("_roomtype_phone");
            this.textEditRoomTypePhoneUnitRateDaily.Text = DXWindowsApplication2.MainForm.getLanguage("_roomtype_use_data_from_pabx");



            // Logo
            this.bttUpload.Text = DXWindowsApplication2.MainForm.getLanguage("_browse");
            this.bttDeleteIcon.Text = DXWindowsApplication2.MainForm.getLanguage("_delete");
            this.richTextBox_Notice.Text = DXWindowsApplication2.MainForm.getLanguage("_image_notice");


            // grid captrue
            this.item_name.Caption = DXWindowsApplication2.MainForm.getLanguage("_charge_list");
            this.item_price_daily.Caption = DXWindowsApplication2.MainForm.getLanguage("_price_per_day");
            this.item_price_monthly.Caption = DXWindowsApplication2.MainForm.getLanguage("_price_per_month");
            this.item_item_type_label.Caption = DXWindowsApplication2.MainForm.getLanguage("_payment_format");
            this.check_status.Caption = DXWindowsApplication2.MainForm.getLanguage("_status_using");
            this.bttOpenItemTab.Text = DXWindowsApplication2.MainForm.getLanguage("_add_charge_item");
            this.roomtype_label.Caption = DXWindowsApplication2.MainForm.getLanguage("_roomtype_name");
            this.room_count.Caption = DXWindowsApplication2.MainForm.getLanguage("_floor_amount_room");


            this.labelControlRequired.Text = DXWindowsApplication2.MainForm.getLanguage("_required");
            this.bttAdd.Text = DXWindowsApplication2.MainForm.getLanguage("_add");
            this.bttEdit.Text = DXWindowsApplication2.MainForm.getLanguage("_edit");
            this.bttDelete.Text = DXWindowsApplication2.MainForm.getLanguage("_delete");
            this.bttSave.Text = DXWindowsApplication2.MainForm.getLanguage("_save");
            this.bttCancel.Text = DXWindowsApplication2.MainForm.getLanguage("_cancel");

        }

        #region Setup
         
            void initRoomType()
            {
                DataTable RoomTypeTable = BusinessLogicBridge.DataStore.BasicInfoRoomType_get();
                gridControlRoomType.DataSource = RoomTypeTable;
            }
            
            void initItems(int roomtype_id)
            {
                DataTable ItemTable = BusinessLogicBridge.DataStore.BasicInfoRoomType_getItemByRoomTypeId(roomtype_id);
                ItemTable.Columns.Add("check_box", typeof(Boolean));
                ItemTable.Columns.Add("item_type_label", typeof(String));
                for (int i = 0; i < ItemTable.Rows.Count; i++)
                {
                    if (ItemTable.Rows[i]["status"].ToString() == "")
                    {
                        ItemTable.Rows[i]["check_box"] = false;
                    }
                    else
                    {
                        ItemTable.Rows[i]["check_box"] = true;
                    }
                    if (int.Parse(ItemTable.Rows[i]["item_type"].ToString()) == 1)
                    {
                        ItemTable.Rows[i]["item_type_label"] = DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_monthly");
                    }
                    else
                    {
                        ItemTable.Rows[i]["item_type_label"] = DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_onetime");
                    }
                }
                gridControlItem.DataSource = ItemTable;
            }

        #endregion

        #region Action Extra
        void clearData()
        {
            //txtEditId.EditValue = 0;
            textEditRoomTypeLabel.EditValue = "";

            pictureEditIcon.EditValue = "";
            textEditIconPath.EditValue = "";
            textEditOldIconPath.EditValue = "";
            textEditSourcePath.EditValue = "";


            textEditRoomTypeLabel.Enabled = true;
            bttUpload.Enabled       = true;
            bttDeleteIcon.Enabled   = true;


            textEditRoomTypeRateMonthly.EditValue               = Double.Parse("0.00").ToString("N2");
            textEditRoomTypeInsurerateRateMonthly.EditValue     = Double.Parse("0.00").ToString("N2");
            textEditRoomTypeAdvanceMonth.EditValue = "0";
            
            textEditRoomTypeElectricityUnitRateMonthly.EditValue = Double.Parse("0.00").ToString("N2");
            textEditElectricityPriceRateMin.EditValue = Double.Parse("0.00").ToString("N2");
            textEditRoomTypeWaterUnitRateMonthly.EditValue = Double.Parse("0.00").ToString("N2");
            textEditWaterPriceRateMin.EditValue = Double.Parse("0.00").ToString("N2");


            textEditLampSum.Enabled = false;
            textEditLampSum.EditValue = Double.Parse("0.00").ToString("N2");


            textEditWaterSum.Enabled = false;
            textEditWaterSum.EditValue = Double.Parse("0.00").ToString("N2");


            textEditRoomTypeRateDaily.Enabled = false;
            textEditRoomTypeRateDaily.EditValue = Double.Parse("0.00").ToString("N2");
            textEditRoomTypeAdvanceDaily.Enabled = false;

            textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
            textEditRoomTypeElectricityUnitRateDaily.EditValue = Double.Parse("0.00").ToString("N2");
            textEditElectricityPriceRateMinDaily.Enabled = false;
            textEditElectricityPriceRateMinDaily.EditValue = Double.Parse("0.00").ToString("N2");
            textEditRoomTypeWaterUnitRateDaily.Enabled = false;
            textEditRoomTypeWaterUnitRateDaily.EditValue = Double.Parse("0.00").ToString("N2");
            textEditWaterPriceRateMinDaily.Enabled = false;
            textEditWaterPriceRateMinDaily.EditValue = Double.Parse("0.00").ToString("N2");

            textEditLampSumDaily.Enabled = false;
            textEditLampSumDaily.EditValue = Double.Parse("0.00").ToString("N2");

            textEditWaterSumDaily.Enabled = false;
            textEditWaterSumDaily.EditValue = Double.Parse("0.00").ToString("N2");

            gridControlItem.Enabled = true;
            bttOpenItemTab.Enabled = true;

    

            DataTable ItemTable = BusinessLogicBridge.DataStore.getDataItemAll();

            if (ItemTable.Columns.Contains("check_box")==false)
                ItemTable.Columns.Add("check_box", typeof(bool));

            if (ItemTable.Rows.Count >0)
            {
                for (int i = 0; i < ItemTable.Rows.Count; i++)
                {
                    ItemTable.Rows[i]["check_box"] = false;
                }
                gridControlItem.DataSource = ItemTable;

            }

        }
        void removeRoomTypeItemsByRoomType(int roomtype_id)
        {
            BusinessLogicBridge.DataStore.removeRoomTypeItemsByRoomTypeId(roomtype_id);
        }
        void insertRoomTypeItems(int roomtype_id, int item_id)
        {
            DateTime date_create = new DateTime();
            date_create = DateTime.Today;
            DataTable RoomTypeItem = new DataTable();
            RoomTypeItem.Columns.Add("roomtype_id", typeof(string));
            RoomTypeItem.Columns.Add("item_id", typeof(string));
            RoomTypeItem.Columns.Add("date_created", typeof(string));
            RoomTypeItem.Rows.Add(roomtype_id, item_id, date_create.ToString("yyyy-MM-dd"));
            Boolean result = BusinessLogicBridge.DataStore.insertRoomTypeItems(RoomTypeItem);
        }
        private DataTable getRoomType()
        {
            DataTable _RoomTypeTable = new DataTable();

            string roomtype_id                      = txtEditId.EditValue.ToString();
            string roomtype_label                   = textEditRoomTypeLabel.EditValue.ToString();
            int roomtype_month_checked              = (checkEditMonthly.Checked==true)?1:0;
            double roomtype_month_roomrate_price    = Convert.ToDouble(textEditRoomTypeRateMonthly.EditValue);
            double roomtype_month_insure_price      = Convert.ToDouble(textEditRoomTypeInsurerateRateMonthly.EditValue);
            int roomtype_month_advance_amount       = textEditRoomTypeAdvanceMonth.EditValue.To<int>();
            int roomtype_month_electric_checked         = (checkEditMonthlyElectric.Checked==true)?1:0;
            double roomtype_month_electric_priceperunit = Convert.ToDouble(textEditRoomTypeElectricityUnitRateMonthly.EditValue);
            double roomtype_month_electric_priceminrate = Convert.ToDouble(textEditElectricityPriceRateMin.EditValue);
            int roomtype_month_electric_lumpchecked     = (checkEditLumpSum.Checked==true)?1:0;
            double roomtype_month_electric_lumpprice    = Convert.ToDouble(textEditLampSum.EditValue);
            bool roomtype_month_water_checked           = checkEditMonthlyWater.Checked;
            double roomtype_month_water_priceperunit    = Convert.ToDouble(textEditRoomTypeWaterUnitRateMonthly.EditValue);
            double roomtype_month_water_priceminrate    = Convert.ToDouble(textEditWaterPriceRateMin.EditValue);
            int roomtype_month_water_lumpchecked        = (checkEditWaterSum.Checked==true)?1:0;
            double roomtype_month_water_lumpprice       = Convert.ToDouble(textEditWaterSum.EditValue);
            int roomtype_month_phone_checked        = (checkEditMonthlyPhone.Checked) ? 1 : 0;
            
            int roomtype_daily_checked = (checkEditDaily.Checked==true)?1:0;
            double roomtype_daily_roomrate_price            = Convert.ToDouble(textEditRoomTypeRateDaily.EditValue);
            int roomtype_daily_advance_amount               = textEditRoomTypeAdvanceDaily.EditValue.To<int>();
            int roomtype_daily_electric_checked             = (checkEditDailyElectric.Checked==true)?1:0;
            double roomtype_daily_electric_priceperunit     = Convert.ToDouble(textEditRoomTypeElectricityUnitRateDaily.EditValue);
            double roomtype_daily_electric_priceminrate     = Convert.ToDouble(textEditElectricityPriceRateMinDaily.EditValue);
            double roomtype_daily_electric_lumpchecked      = Convert.ToDouble(checkEditLumpSumDaily.Checked);
            double roomtype_daily_electric_lumpprice        = Convert.ToDouble(textEditLampSumDaily.EditValue);

            int roomtype_daily_water_checked                = (checkEditDailyWater.Checked==true)?1:0;
            double roomtype_daily_water_priceperunit        = Convert.ToDouble(textEditRoomTypeWaterUnitRateDaily.EditValue);
            double roomtype_daily_water_priceminrate        = Convert.ToDouble(textEditWaterPriceRateMinDaily.EditValue);
            int roomtype_daily_water_lumpchecked            = (checkEditWaterSumDaily.Checked==true)?1:0;
            double roomtype_daily_water_lumpprice           = Convert.ToDouble(textEditWaterSumDaily.EditValue);
            int roomtype_daily_phone_checked                = (checkEditDailyPhone.Checked) ? 1 : 0;
            string roomtype_icon                            = textEditIconPath.EditValue.ToString();

            _RoomTypeTable.Columns.Add("roomtype_id", typeof(string));
            _RoomTypeTable.Columns.Add("roomtype_label", typeof(string));
            _RoomTypeTable.Columns.Add("roomtype_month_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_month_roomrate_price", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_insure_price", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_advance_amount", typeof(int));
            _RoomTypeTable.Columns.Add("roomtype_month_electric_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_month_electric_priceperunit", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_electric_priceminrate", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_electric_lumpchecked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_month_electric_lumpprice", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_water_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_month_water_priceperunit", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_water_priceminrate", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_water_lumpchecked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_month_water_lumpprice", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_month_phone_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_roomrate_price", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_advance_amount", typeof(int));
            _RoomTypeTable.Columns.Add("roomtype_daily_electric_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_electric_priceperunit", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_electric_priceminrate", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_electric_lumpchecked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_electric_lumpprice", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_water_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_water_priceperunit", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_water_priceminrate", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_water_lumpchecked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_daily_water_lumpprice", typeof(double));
            _RoomTypeTable.Columns.Add("roomtype_daily_phone_checked", typeof(bool));
            _RoomTypeTable.Columns.Add("roomtype_icon", typeof(string));

            _RoomTypeTable.Rows.Add(roomtype_id, roomtype_label, roomtype_month_checked, roomtype_month_roomrate_price, roomtype_month_insure_price, roomtype_month_advance_amount, roomtype_month_electric_checked, roomtype_month_electric_priceperunit, roomtype_month_electric_priceminrate, roomtype_month_electric_lumpchecked, roomtype_month_electric_lumpprice, roomtype_month_water_checked, roomtype_month_water_priceperunit, roomtype_month_water_priceminrate, roomtype_month_water_lumpchecked, roomtype_month_water_lumpprice ,roomtype_month_phone_checked ,roomtype_daily_checked, roomtype_daily_roomrate_price, roomtype_daily_advance_amount, roomtype_daily_electric_checked, roomtype_daily_electric_priceperunit, roomtype_daily_electric_priceminrate, roomtype_daily_electric_lumpchecked, roomtype_daily_electric_lumpprice, roomtype_daily_water_checked, roomtype_daily_water_priceperunit, roomtype_daily_water_priceminrate, roomtype_daily_water_lumpchecked, roomtype_daily_water_lumpprice, roomtype_daily_phone_checked, roomtype_icon);

            return _RoomTypeTable;
        }
        void changeRow()
        {
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridView1.GetDataRow(0);
                }

                change_row_case = true;

               txtEditId.EditValue = CurrentRow["roomtype_id"].ToString();
                textEditRoomTypeLabel.EditValue = CurrentRow["roomtype_label"].ToString();

                                

                textEditRoomTypeRateMonthly.EditValue = double.Parse(CurrentRow["roomtype_month_roomrate_price"].ToString()).ToString("N2");
                textEditRoomTypeInsurerateRateMonthly.EditValue = double.Parse(CurrentRow["roomtype_month_insure_price"].ToString()).ToString("N2");
                textEditRoomTypeAdvanceMonth.EditValue = CurrentRow["roomtype_month_advance_amount"].ToString();

                // textbox 
                #region Monthly Section
                    

                //checkEditMonthlyElectric.Checked = CurrentRow["roomtype_month_electric_checked"].To<bool>();
                //checkEditMonthlyWater.Checked = CurrentRow["roomtype_month_water_checked"].To<bool>();
                //checkEditMonthlyPhone.EditValue = CurrentRow["roomtype_month_phone_checked"].To<bool>();
                
                //checkEditLumpSum.Checked = CurrentRow["roomtype_month_electric_lumpchecked"].To<bool>();
                //checkEditWaterSum.Checked = CurrentRow["roomtype_month_water_lumpchecked"].To<bool>();

                    checkEditMonthly.Checked = CurrentRow["roomtype_month_checked"].To<bool>();    
                    // monthly public
                    checkEditMonthlyElectric.Checked = Convert.ToBoolean(CurrentRow["roomtype_month_electric_checked"]);
                    checkEditMonthlyWater.Checked = Convert.ToBoolean(CurrentRow["roomtype_month_water_checked"]);
                    checkEditMonthlyPhone.Checked = Convert.ToBoolean(CurrentRow["roomtype_month_phone_checked"]);
                    checkEditLumpSum.Checked = Convert.ToBoolean(CurrentRow["roomtype_month_electric_lumpchecked"]);
                    checkEditWaterSum.Checked = Convert.ToBoolean(CurrentRow["roomtype_month_water_lumpchecked"]);

                    // rate per unit
                    textEditRoomTypeElectricityUnitRateMonthly.EditValue    = double.Parse(CurrentRow["roomtype_month_electric_priceperunit"].ToString()).ToString("N2");
                    textEditRoomTypeWaterUnitRateMonthly.EditValue          = double.Parse(CurrentRow["roomtype_month_water_priceperunit"].ToString()).ToString("N2");
                    
                    // min rate
                    textEditElectricityPriceRateMin.EditValue               = double.Parse(CurrentRow["roomtype_month_electric_priceminrate"].ToString()).ToString("N2");
                    textEditWaterPriceRateMin.EditValue                     = double.Parse(CurrentRow["roomtype_month_water_priceminrate"].ToString()).ToString("N2");
                    
                     // Lump 
                    textEditLampSum.EditValue       = double.Parse(CurrentRow["roomtype_month_electric_lumpprice"].ToString()).ToString("N2");
                    textEditWaterSum.EditValue      = double.Parse(CurrentRow["roomtype_month_water_lumpprice"].ToString()).ToString("N2");
                #endregion

                #region Daily Section

                    if (event_check_click_cost_items == false)
                    {
                        checkEditDaily.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_checked"]);


                        // Daily public
                        checkEditDailyElectric.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_electric_checked"]);
                        checkEditDailyWater.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_water_checked"]);
                        checkEditDailyPhone.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_phone_checked"]);
                        checkEditLumpSumDaily.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_electric_lumpchecked"]);
                        checkEditWaterSumDaily.Checked = Convert.ToBoolean(CurrentRow["roomtype_daily_water_lumpchecked"]);

                        // rate Per Unit                     
                        textEditRoomTypeRateDaily.EditValue = double.Parse(CurrentRow["roomtype_daily_roomrate_price"].ToString()).ToString("N2");
                        textEditRoomTypeAdvanceDaily.EditValue = CurrentRow["roomtype_daily_advance_amount"].ToString();
                        textEditRoomTypeElectricityUnitRateDaily.EditValue = double.Parse(CurrentRow["roomtype_daily_electric_priceperunit"].ToString()).ToString("N2");
                        textEditRoomTypeWaterUnitRateDaily.EditValue = double.Parse(CurrentRow["roomtype_daily_water_priceperunit"].ToString()).ToString("N2");
                        // Lump
                        textEditLampSumDaily.EditValue = double.Parse(CurrentRow["roomtype_daily_electric_lumpprice"].ToString()).ToString("N2");
                        textEditWaterSumDaily.EditValue = double.Parse(CurrentRow["roomtype_daily_water_lumpprice"].ToString()).ToString("N2");
                    }

                    if (checkEditDaily.Checked == true)
                    {
                        if (checkEditLumpSumDaily.Checked == true)
                        {
                            textEditLampSumDaily.Enabled = true;
                            textEditElectricityPriceRateMinDaily.Enabled = false;
                            textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                        }
                        else {
                            textEditLampSumDaily.Enabled = false;
                            textEditElectricityPriceRateMinDaily.Enabled = true;
                            textEditRoomTypeElectricityUnitRateDaily.Enabled = true;
                        }
                        if (checkEditLumpSumDaily.Checked == true)
                        {
                            textEditLampSumDaily.Enabled = true;
                            textEditElectricityPriceRateMinDaily.Enabled = false;
                            textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                        }
                        else
                        {
                            textEditLampSumDaily.Enabled = false;
                            textEditElectricityPriceRateMinDaily.Enabled = true;
                            textEditRoomTypeElectricityUnitRateDaily.Enabled = true;
                        }
                        
                        if (checkEditWaterSumDaily.Checked == true)
                        {
                            textEditWaterSumDaily.Enabled = true;
                            textEditWaterPriceRateMinDaily.Enabled = false;
                            textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                        }
                        else {
                            textEditWaterSumDaily.Enabled = false;
                            textEditWaterPriceRateMinDaily.Enabled = true;
                            textEditRoomTypeWaterUnitRateDaily.Enabled = true;
                        }

                        if (checkEditDailyElectric.Checked == false)
                        {
                            textEditElectricityPriceRateMinDaily.Enabled = false;
                            textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                        }

                        if (checkEditDailyWater.Checked == false)
                        {
                            textEditWaterPriceRateMinDaily.Enabled = false;
                            textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                        }

                        textEditRoomTypeRateDaily.Enabled = true;
                        textEditRoomTypeAdvanceDaily.Enabled = true;
                        checkEditLumpSumDaily.Enabled = true;
                        checkEditWaterSumDaily.Enabled = true;

                    }
                    else {
                        textEditRoomTypeRateDaily.Enabled = false;
                        textEditRoomTypeAdvanceDaily.Enabled = false;
                        textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                        textEditRoomTypeWaterUnitRateDaily.Enabled = false;                        
                        textEditElectricityPriceRateMinDaily.Enabled = false;
                        textEditWaterPriceRateMinDaily.Enabled = false;
                    }

                #endregion

                if (CurrentRow["roomtype_icon"].ToString() != "")
                {
                    string file_path = CurrentRow["roomtype_icon"].ToString();
                    FileInfo file_info = new FileInfo(file_path);
                    if (file_info.Exists)
                    {
                        System.IO.FileStream fs;
                        fs = new System.IO.FileStream(file_info.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                        pictureEditIcon.Image = System.Drawing.Image.FromFile(file_path);
                        
                        fs.Close();
                        fs.Dispose();
                        textEditIconPath.EditValue = CurrentRow["roomtype_icon"].ToString();
                    }
                    else
                    {
                        pictureEditIcon.EditValue = "";
                        textEditIconPath.EditValue = "";
                    }
                }
                else
                {
                    pictureEditIcon.EditValue = "";
                    textEditIconPath.EditValue = "";
                }
                int roomtype_id = Convert.ToInt32(CurrentRow["roomtype_id"].ToString());
                initItems(roomtype_id);

                panelControlMonthly.Enabled = false;
                groupControlAdditional.Enabled = false;
                panelControlDaily.Enabled = false;
                gridControlRoomType.Enabled = true;

                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;

                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttOpenItemTab.Enabled = false;

                textEditSourcePath.EditValue = "";
                textEditOldIconPath.EditValue = "";
                
                // Load Additional Item
                try{

                    DataTable ItemTable         = BusinessLogicBridge.DataStore.getDataItemAll();
                    DataTable RoomTypeItemTable = BusinessLogicBridge.DataStore.getRoomtypeItemsByRoomTypeId(roomtype_id);

                    //ItemTableTemp = ItemTable;

                    if (ItemTableTemp.Rows.Count <= ItemTable.Rows.Count)
                    {
                        ItemTableTemp = ItemTable;
                        ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
                        ItemTableTemp.Columns.Add("item_type_label", typeof(String));
                    }
                    else {

                        panelControlMonthly.Enabled = true;
                        groupControlAdditional.Enabled = true;
                        panelControlDaily.Enabled = true;

                        bttAdd.Enabled = false;
                        bttEdit.Enabled = false;
                        bttDelete.Enabled = false;
                        bttSave.Enabled = true;
                        bttCancel.Enabled = true;

                        textEditSourcePath.EditValue = "";
                        textEditOldIconPath.EditValue = "";
                    }

                    for (int i = 0; i < ItemTableTemp.Rows.Count; i++)
                    {
                        ItemTableTemp.Rows[i]["check_box"] = false;                        

                        for (int j = 0; j < RoomTypeItemTable.Rows.Count; j++ )
                        {

                            if (RoomTypeItemTable.Rows[j]["item_id"].To<int>() == ItemTableTemp.Rows[i]["item_id"].To<int>())
                            {
                                ItemTableTemp.Rows[i]["check_box"] = true;
                            }
                        }

                        if (ItemTableTemp.Rows[i]["item_id"].To<int>() == 0) ItemTableTemp.Rows[i]["check_box"] = true;

                        if (int.Parse(ItemTableTemp.Rows[i]["item_type"].ToString()) == 1)
                        {
                            ItemTableTemp.Rows[i]["item_type_label"] = DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_monthly");
                        }
                        else
                        {
                            ItemTableTemp.Rows[i]["item_type_label"] = DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_onetime");
                        }
                    }

                    gridControlItem.DataSource = ItemTableTemp;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {   

                DataTable ItemTable         = BusinessLogicBridge.DataStore.getDataItemAll();

                if(ItemTableTemp.Columns.Contains("check_box")==false)
                    ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
                if (ItemTableTemp.Columns.Contains("item_type_label") == false)
                    ItemTableTemp.Columns.Add("item_type_label", typeof(String));

                if (ItemTableTemp.Rows.Count <= ItemTable.Rows.Count)
                {
                    ItemTableTemp = ItemTable;
                    if (ItemTableTemp.Columns.Contains("check_box") == false)
                        ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
                    if (ItemTableTemp.Columns.Contains("item_type_label") == false)
                        ItemTableTemp.Columns.Add("item_type_label", typeof(String));
                }
                else {

                    for (int i = 0; i < ItemTableTemp.Rows.Count; i++)
                    {

                        ItemTableTemp.Rows[i]["check_box"] = true;


                        if (int.Parse(ItemTableTemp.Rows[i]["item_type"].ToString()) == 1)
                        {
                            ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_monthly");
                        }
                        else
                        {
                            ItemTableTemp.Rows[i]["item_type_label"] = getLanguage("_payment_dropdown_onetime");
                        }
                    }
                    
                }
                gridControlItem.DataSource = ItemTableTemp;
                
                if(textEditRoomTypeLabel.EditValue.ToString()=="")
                    clearData();

                // enable all channal

                panelControlMonthly.Enabled = true;
                panelControlAdditional.Enabled = true;
                panelControlDaily.Enabled = true;

                // Disable Grid
                gridControlRoomType.Enabled = false;
                gridControlItem.Enabled = true;
                textEditRoomTypeLabel.Focus();
                bttUpload.Enabled = true;
                bttDeleteIcon.Enabled = true;

                // Input Box Section

                textEditRoomTypeElectricityUnitRateMonthly.Enabled = true;
                textEditRoomTypeWaterUnitRateMonthly.Enabled = true;
                textEditElectricityPriceRateMin.Enabled = true;
                textEditWaterPriceRateMin.Enabled = true;

                checkEditMonthlyPhone.Checked = true;
                checkEditLumpSum.Checked = false;
                checkEditWaterSum.Checked = false;

                if (event_check_click_cost_items == false)
                {
                    checkEditDaily.Checked = false;
                    checkEditDailyElectric.Enabled = false;
                    checkEditDailyWater.Enabled = false;
                    checkEditDailyPhone.Enabled = false;
                }

                // Set Event
                button_event = "add";

                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;
                bttSave.Enabled = true;
                bttCancel.Enabled = true;     
            }
        }
        void enabled(Boolean status)
        {
            //textEditRoomTypeLabel.Enabled = status;
            //textEditRoomTypeRateMonthly.Enabled = status;
            //textEditRoomTypeRateDaily.Enabled = status;
            //textEditRoomTypeElectricityUnitRateMonthly.Enabled = status;
            //textEditRoomTypeElectricityUnitRateDaily.Enabled = status;
            //textEditRoomtypeElectricityUnitRateMin.Enabled = status;
            //textEditElectricityPriceRateMin.Enabled = status;
            //textEditRoomTypeWaterUnitRateMonthly.Enabled = status;
            //textEditRoomTypeWaterUnitRateDaily.Enabled = status;
            //textEditRoomTypeWaterUnitRateMin.Enabled = status;
            //textEditWaterPriceRateMin.Enabled = status;
            //textEditRoomTypePhoneUnitRateMonthly.Enabled = status;
            //textEditRoomTypePhoneUnitRateDaily.Enabled = status;
            //textEditRoomTypePhoneUnitRateMin.Enabled = status;
            //textEditRoomTypeAdvanceMonth.Enabled = status;
            //textEditRoomTypeDepositeRateDaily.Enabled = status;
            //textEditRoomTypeInsurerateRateMonthly.Enabled = status;
            //textEditRoomTypeInsurerateRateMonthly.Enabled = status;
            //txtEditRoomTypeCode.Enabled = status;
            
        }
        private DataTable validateData()
        {
            string max_value = getLanguage("_max_value");

            string star_notice = getLanguage("_msg_1001");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            // Monthly

            #region Genertal field Monthly
            if (textEditRoomTypeLabel.EditValue == null)
            {
                label = labelControlRoomTypeLabel.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditRoomTypeLabel.Focus();
                    focus = true;
                }
            }
            else if (textEditRoomTypeLabel.Text.Length < 1)
            {
                label = labelControlRoomTypeLabel.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditRoomTypeLabel.Focus();
                    focus = true;
                }
            }

            if (textEditRoomTypeRateMonthly.EditValue.ToString() != "0.00")
            {
                textEditRoomTypeRateMonthly.EditValue = textEditRoomTypeRateMonthly.EditValue.To<double>().ToString("N2");
                string[] RoomTypeRate = cutString(textEditRoomTypeRateMonthly.Text);

                if (validLength(RoomTypeRate[0], 8) == false)
                {
                    label = labelControlRoomTypeRate.Text;
                    message = max_value;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditRoomTypeRateMonthly.Focus();
                        focus = true;
                    }
                }
               
            }

            if (textEditRoomTypeInsurerateRateMonthly.EditValue.ToString() != "0")
            {
                textEditRoomTypeInsurerateRateMonthly.EditValue = textEditRoomTypeInsurerateRateMonthly.EditValue.To<double>().ToString("N2");

                string[] RoomTypeInsurerate = cutString(textEditRoomTypeInsurerateRateMonthly.Text);

                if (validLength(RoomTypeInsurerate[0], 8) == false)
                {
                    label = labelControlInsurerate.Text;
                    message = max_value;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditRoomTypeInsurerateRateMonthly.Focus();
                        focus = true;
                    }
                }
            }
            #endregion
            #region Electric Monthly

            // Public Electric Detail
            if (checkEditMonthlyElectric.Checked==true)
            {
                if (textEditRoomTypeElectricityUnitRateMonthly.EditValue.ToString() != "0.00")
                {

                    textEditRoomTypeElectricityUnitRateMonthly.EditValue = textEditRoomTypeElectricityUnitRateMonthly.EditValue.To<double>().ToString("N2");
                    string[] RoomTypeElectricityUnitRateMonthly = cutString(textEditRoomTypeElectricityUnitRateMonthly.Text);

                    if (validLength(RoomTypeElectricityUnitRateMonthly[0], 7) == false)
                    {
                        label = checkEditMonthlyElectric.Text + " " + labelControlPerUnit.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditRoomTypeElectricityUnitRateMonthly.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditMonthlyElectric.Checked == true)
            {
                if (textEditElectricityPriceRateMin.EditValue.ToString() != "0.00")
                {
                    textEditElectricityPriceRateMin.EditValue = textEditElectricityPriceRateMin.EditValue.To<double>().ToString("N2");
                    string[] ElectricityPriceRateMin = cutString(textEditElectricityPriceRateMin.Text);

                    if (validLength(ElectricityPriceRateMin[0], 7) == false)
                    {
                        label = checkEditMonthlyElectric.Text + " " + labelControlMinrate.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditElectricityPriceRateMin.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditLumpSum.Checked == true)
            {
                if (textEditLampSum.EditValue.ToString() != "0.00")
                {
                    textEditLampSum.EditValue = textEditLampSum.EditValue.To<double>().ToString("N2");
                    string[] LampSum = cutString(textEditLampSum.Text);

                    if (validLength(LampSum[0], 7) == false)
                    {
                        label = checkEditMonthlyElectric.Text + " " + labelControlLumpLabel.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditLampSum.Focus();
                            focus = true;
                        }
                    }
                }
            }
            #endregion            
            #region Water Monthly
            
            // Public Electric Detail
            if (checkEditMonthlyWater.Checked==true)
            {
                if (textEditRoomTypeWaterUnitRateMonthly.EditValue.ToString() != "0.00")
                {
                    textEditRoomTypeWaterUnitRateMonthly.EditValue = textEditRoomTypeWaterUnitRateMonthly.EditValue.To<double>().ToString("N2");
                    string[] RoomTypeWaterUnitRateMonthly = cutString(textEditRoomTypeWaterUnitRateMonthly.Text);

                    if (validLength(RoomTypeWaterUnitRateMonthly[0], 7) == false)
                    {
                        label = checkEditMonthlyWater.Text + " " + labelControlPerUnit.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditRoomTypeWaterUnitRateMonthly.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditMonthlyWater.Checked == true)
            {
                if (textEditWaterPriceRateMin.EditValue.ToString() != "0.00")
                {
                    textEditWaterPriceRateMin.EditValue = textEditWaterPriceRateMin.EditValue.To<double>().ToString("N2");
                    string[] WaterPriceRateMin = cutString(textEditWaterPriceRateMin.Text);

                    if (validLength(WaterPriceRateMin[0], 7) == false)
                    {
                        label = checkEditMonthlyWater.Text + " " + labelControlMinrate.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditWaterPriceRateMin.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditWaterSum.Checked == true)
            {
                if (textEditWaterSum.EditValue.ToString() != "0.00")
                {
                    textEditWaterSum.EditValue = textEditWaterSum.EditValue.To<double>().ToString("N2");
                    string[] WaterSum = cutString(textEditWaterSum.Text);

                    if (validLength(WaterSum[0], 7) == false)
                    {
                        label = checkEditMonthlyWater.Text + " " + labelControlLumpLabel.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditWaterSum.Focus();
                            focus = true;
                        }
                    }
                }
            }
            #endregion

            #region Genertal field Daily

            if (textEditRoomTypeRateDaily.EditValue.ToString() != "0.00")
            {
                textEditRoomTypeRateDaily.EditValue = textEditRoomTypeRateDaily.EditValue.To<double>().ToString("N2");
                string[] RoomTypeRateDaily = cutString(textEditRoomTypeRateDaily.Text);

                if (validLength(RoomTypeRateDaily[0], 7) == false)
                {
                    label = labelControlRoomTypeRate.Text;
                    message = max_value;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditRoomTypeRateDaily.Focus();
                        focus = true;
                    }
                }

            }

            #endregion            
            #region Electric Daily

            // Public Electric Detail
            if (checkEditDailyElectric.Checked == true)
            {
                if (textEditRoomTypeElectricityUnitRateDaily.EditValue.ToString() != "0.00")
                {
                    textEditRoomTypeElectricityUnitRateDaily.EditValue = textEditRoomTypeElectricityUnitRateDaily.EditValue.To<double>().ToString("N2");
                    string[] RoomTypeElectricityUnitRateDaily = cutString(textEditRoomTypeElectricityUnitRateDaily.Text);

                    if (validLength(RoomTypeElectricityUnitRateDaily[0], 7) == false)
                    {
                        label = checkEditDailyElectric.Text + " " + labelControlPerUnit.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditRoomTypeElectricityUnitRateDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditDailyElectric.Checked == true)
            {
                if (textEditElectricityPriceRateMinDaily.EditValue.ToString() != "0.00")
                {
                    textEditElectricityPriceRateMinDaily.EditValue = textEditElectricityPriceRateMinDaily.EditValue.To<double>().ToString("N2");
                    string[] ElectricityPriceRateMinDaily = cutString(textEditElectricityPriceRateMinDaily.Text);

                    if (validLength(ElectricityPriceRateMinDaily[0], 7) == false)
                    {
                        label = checkEditDailyElectric.Text + " " + labelControlMinrate.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditElectricityPriceRateMinDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditLumpSumDaily.Checked == true)
            {
                if (textEditLampSumDaily.EditValue.ToString() != "0.00")
                {
                    textEditLampSumDaily.EditValue = textEditLampSumDaily.EditValue.To<double>().ToString("N2");
                    string[] LampSumDaily = cutString(textEditLampSumDaily.Text);

                    if (validLength(LampSumDaily[0], 7) == false)
                    {
                        label = checkEditDailyElectric.Text + " " + labelControlLumpLabel.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditLampSumDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }
            #endregion
            #region Water Daily

            // Public Electric Detail
            if (checkEditDailyWater.Checked == true)
            {
                if (textEditRoomTypeWaterUnitRateDaily.EditValue.ToString() != "0.00")
                {

                    textEditRoomTypeWaterUnitRateDaily.EditValue = textEditRoomTypeWaterUnitRateDaily.EditValue.To<double>().ToString("N2");
                    string[] RoomTypeWaterUnitRateDaily = cutString(textEditRoomTypeWaterUnitRateDaily.Text);

                    if (validLength(RoomTypeWaterUnitRateDaily[0], 7) == false)
                    {
                        label = checkEditDailyWater.Text + " " + labelControlPerUnit.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditRoomTypeWaterUnitRateDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditDailyWater.Checked == true)
            {
                if (textEditWaterPriceRateMinDaily.EditValue.ToString() != "0.00")
                {
                    textEditWaterPriceRateMinDaily.EditValue = textEditWaterPriceRateMinDaily.EditValue.To<double>().ToString("N2");
                    string[] WaterPriceRateMinDaily = cutString(textEditWaterPriceRateMinDaily.Text);

                    if (validLength(WaterPriceRateMinDaily[0], 7) == false)
                    {
                        label = checkEditDailyWater.Text + " " + labelControlMinrate.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditWaterPriceRateMinDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (checkEditWaterSumDaily.Checked == true)
            {

                if (textEditWaterSumDaily.EditValue.ToString() != "0.00")
                {

                    textEditWaterSumDaily.EditValue = textEditWaterSumDaily.EditValue.To<double>().ToString("N2");
                    string[] WaterSumDaily = cutString(textEditWaterSumDaily.Text);

                    if (validLength(WaterSumDaily[0], 7) == false)
                    {
                        label = checkEditDailyWater.Text + " " + labelControlLumpLabel.Text;
                        message = max_value;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditWaterSumDaily.Focus();
                            focus = true;
                        }
                    }
                }
            }
            #endregion

            #region Process
            if (button_event != "")
            {
                DataTable _RoomTypeTable = getRoomType();
                int roomtype_id = int.Parse(_RoomTypeTable.Rows[0]["roomtype_id"].ToString());
                string roomtype_label = _RoomTypeTable.Rows[0]["roomtype_label"].ToString();

                switch (button_event)
                {
                    case "add":
                        int checkLabel = BusinessLogicBridge.DataStore.BasicInfoRoomType_getCountByRoomTypeByLabel(roomtype_label);
                        if (checkLabel > 0)
                        {
                            label = labelControlRoomTypeLabel.Text;
                            message = getLanguage("_msg_1030");
                            _ValidateTable.Rows.Add(label, message);
                            if (focus == false)
                            {
                                textEditRoomTypeLabel.Focus();
                                focus = true;
                            }
                        }
                        break;
                    case "edit":
                        DataTable RoomTypeTable = BusinessLogicBridge.DataStore.BasicInfoRoomType_getRoomTypeByLabel(roomtype_label);
                        if (RoomTypeTable.Rows.Count > 0)
                        {
                            if (int.Parse(RoomTypeTable.Rows[0]["roomtype_id"].ToString()) != roomtype_id)
                            {
                                label = labelControlRoomTypeLabel.Text;
                                message = DXWindowsApplication2.MainForm.getLanguage("_already");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    textEditRoomTypeLabel.Focus();
                                    focus = true;
                                }
                            }
                        }
                        RoomTypeTable.Dispose();
                        break;
                }
                _RoomTypeTable.Dispose();
            }
            #endregion 
            return _ValidateTable;
        }

        private String getPath()
        {
            string file_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string logo_path = file_path + "\\e-SmartBilling\\RoomTypeIcon";
            if (Directory.Exists(logo_path) == false)
            {
                Directory.CreateDirectory(logo_path);
            }
            return logo_path;
        }

        private String getExtPath()
        {
            DataTable generalDT = BusinessLogicBridge.DataStore.getGeneralConfig();

            string logo_ext_path = "";

            if (generalDT.Rows.Count > 0)
            {
                if (generalDT.Rows[0]["ext_path_backup"].ToString() != "")
                {
                    logo_ext_path = generalDT.Rows[0]["ext_path_backup"].ToString() + "\\RoomTypeIcon";

                    if (Directory.Exists(logo_ext_path) == false)
                    {
                        Directory.CreateDirectory(logo_ext_path);
                    }
                }
            }

            return logo_ext_path;
        }

        private String createFileName(string full_path)
        {
            string file_extension = Path.GetExtension(full_path);
            string create_year = DateTime.Now.Year.ToString();
            string create_month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string create_day = DateTime.Now.Day.ToString().PadLeft(2, '0');
            string create_hour = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string create_min = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            string create_second = DateTime.Now.Second.ToString().PadLeft(2, '0');
            string create_millisecond = DateTime.Now.Millisecond.ToString();
            string file_name = "SXBilingICon_" + create_year + create_month + create_day + create_hour + create_min + create_second + "-" + create_millisecond + file_extension;
            return file_name;
        }
        private Boolean moveFile()
        {
            string file_path = textEditSourcePath.EditValue.ToString();
            if (file_path != "")
            {
                FileInfo file_info = new FileInfo(file_path);
                file_info.CreationTime.Millisecond.ToString();
                if (file_info.Exists)
                {
                    string current_file_name = createFileName(file_path);
                    string current_file_paht = getPath() + "\\" + current_file_name;

                    string current_ext_file_paht = "";


                    if (getExtPath() != "")
                    {
                        current_ext_file_paht = getExtPath() + "\\" + current_file_name;
                    }

                    try
                    {
                        System.IO.File.Copy(file_path, current_file_paht, true);

                        if (current_ext_file_paht != "")
                        {
                            System.IO.File.Copy(file_path, current_ext_file_paht, true);
                        }

                        textEditIconPath.EditValue = current_file_paht;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        private Boolean removeFile()
        {
            string current_file_path = textEditIconPath.EditValue.ToString();
            if (current_file_path != "")
            {
                FileInfo current_file_info = new FileInfo(current_file_path);
                if (current_file_info.Exists)
                {
                    try
                    {
                        current_file_info.Delete();
                    }
                    catch { }
                }
            }
            return true;
        }
        private Boolean removeOldFile()
        {
            string current_file_path = textEditOldIconPath.EditValue.ToString();
            if (current_file_path != "")
            {
                FileInfo current_file_info = new FileInfo(current_file_path);
                if (current_file_info.Exists)
                {
                    current_file_info.Delete();
                }
            }
            return true;
        }        
        #endregion

        #region Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion
        
        #region Button Event

        private void bttUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                open.Filter = "Image Files(*.jpg,*.gif,*.bmp,*.png,*.ico)|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.ico";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file_info = new FileInfo(open.FileName);
                    string file_paht = file_info.FullName;
                    textEditSourcePath.EditValue = file_paht;
                    Double file_size = double.Parse(file_info.Length.ToString());

                    if (file_size > (50 * 1024))
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1004"), getLanguage("_softwarename"));
                        textEditSourcePath.EditValue = "";
                        bttUpload.Focus();
                        return;
                    }
                    System.IO.FileStream fs = null;
                    fs = new System.IO.FileStream(file_info.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    pictureEditIcon.Image = System.Drawing.Image.FromStream(fs);
                    fs.Close();
                    fs.Dispose();
                    file_info.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void bttDeleteIcon_Click(object sender, EventArgs e)
        {

            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4002"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                pictureEditIcon.EditValue = "";
                textEditIconPath.EditValue = "";
                textEditSourcePath.EditValue = "";
                int[] rowIndex = gridView1.GetSelectedRows();
                if (rowIndex.Length != 0)
                {
                    DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                    if (CurrentRow == null)
                    {
                        CurrentRow = gridView1.GetDataRow(0);
                    }
                    textEditOldIconPath.EditValue = CurrentRow["roomtype_icon"].ToString();
                }
                else
                {
                    textEditOldIconPath.EditValue = "";
                }
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");
            }
        }

        private void checkEditMonthlyElectric_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditMonthlyElectric.Checked == true)
            {
                textEditRoomTypeElectricityUnitRateMonthly.Enabled = true;
                textEditElectricityPriceRateMin.Enabled = true;
                checkEditLumpSum.Enabled = true;
            }
            else
            {
                textEditRoomTypeElectricityUnitRateMonthly.Enabled = false;
                textEditElectricityPriceRateMin.Enabled = false;
                checkEditLumpSum.Enabled = false;
                textEditLampSum.Enabled = false;
                checkEditLumpSum.Checked = false;
            }
        }

        private void checkEditLumpSum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditLumpSum.Checked == true)
            {
                textEditLampSum.Enabled = true;
                textEditRoomTypeElectricityUnitRateMonthly.Enabled = false;
                textEditElectricityPriceRateMin.Enabled = false;

            }
            else
            {
                textEditLampSum.Enabled = false;

                if (checkEditMonthlyElectric.Checked == true)
                {
                    textEditRoomTypeElectricityUnitRateMonthly.Enabled = true;
                    textEditElectricityPriceRateMin.Enabled = true;
                }
                else {
                    textEditRoomTypeElectricityUnitRateMonthly.Enabled = false;
                    textEditElectricityPriceRateMin.Enabled = false;
                }
            }
        }

        private void checkEditMonthlyWater_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditMonthlyWater.Checked == true)
            {
                textEditRoomTypeWaterUnitRateMonthly.Enabled = true;
                textEditWaterPriceRateMin.Enabled = true;
                checkEditWaterSum.Enabled = true;
            }
            else
            {
                textEditRoomTypeWaterUnitRateMonthly.Enabled = false;
                textEditWaterPriceRateMin.Enabled = false;
                checkEditWaterSum.Enabled = false;
                textEditWaterSum.Enabled = false;
                checkEditWaterSum.Checked = false;
            }
        }

        private void checkEditWaterSum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditWaterSum.Checked == true)
            {
                textEditWaterSum.Enabled = true;
                textEditRoomTypeWaterUnitRateMonthly.Enabled = false;
                textEditWaterPriceRateMin.Enabled = false;

            }
            else
            {
                textEditWaterSum.Enabled = false;
                if (checkEditMonthlyWater.Checked == true)
                {
                    textEditRoomTypeWaterUnitRateMonthly.Enabled = true;
                    textEditWaterPriceRateMin.Enabled = true;
                }
                else {
                    textEditRoomTypeWaterUnitRateMonthly.Enabled = false;
                    textEditWaterPriceRateMin.Enabled = false;
                }
            }
        }

        private void checkEditDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditDaily.Checked == true)
            {
                textEditRoomTypeRateDaily.Enabled = true;
                checkEditDailyElectric.Checked = true;
                checkEditDailyWater.Checked = true;
                checkEditDailyElectric.Enabled = true;
                checkEditDailyWater.Enabled = true;
                checkEditDailyPhone.Enabled = true;
                textEditRoomTypeAdvanceDaily.Enabled = true;
                textEditLampSumDaily.Enabled = true;
                textEditWaterSumDaily.Enabled = true;
                checkEditWaterSumDaily.Enabled = true;
                checkEditLumpSumDaily.Enabled = true;
                checkEditDailyElectric.Enabled = true;

                textEditWaterSumDaily.Enabled = false;
                textEditLampSumDaily.Enabled = false;

            }
            else
            {
                
                checkEditDailyElectric.Enabled = false;
                checkEditDailyWater.Enabled = false;
                checkEditDailyPhone.Enabled = false;
                checkEditLumpSumDaily.Enabled = false;
                checkEditWaterSumDaily.Enabled = false;

                textEditRoomTypeRateDaily.Enabled = false;
                textEditRoomTypeAdvanceDaily.Enabled = false;
                textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                textEditElectricityPriceRateMinDaily.Enabled = false;
                textEditWaterPriceRateMinDaily.Enabled = false;
                textEditLampSumDaily.Enabled = false;
                textEditWaterSumDaily.Enabled = false;

                checkEditDailyElectric.Checked = false;
                checkEditDailyWater.Checked = false;

            }
        }

        private void checkEditDailyElectric_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditDailyElectric.Checked == true)
            {
                textEditRoomTypeElectricityUnitRateDaily.Enabled = true;
                textEditElectricityPriceRateMinDaily.Enabled = true;
                checkEditLumpSumDaily.Enabled = true;
            }
            else
            {
                textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                textEditElectricityPriceRateMinDaily.Enabled = false;
                checkEditLumpSumDaily.Enabled = false;
                textEditLampSumDaily.Enabled = false;
                checkEditLumpSumDaily.Checked = false;
            }
        }

        private void checkEditLumpSumDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditLumpSumDaily.Checked == true)
            {
                textEditLampSumDaily.Enabled = true;
                textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                textEditElectricityPriceRateMinDaily.Enabled = false;
            }
            else
            {
                textEditLampSumDaily.Enabled = false;

                if (checkEditDailyElectric.Checked == true)
                {
                    textEditRoomTypeElectricityUnitRateDaily.Enabled = true;
                    textEditElectricityPriceRateMinDaily.Enabled = true;
                }
                else {
                    textEditRoomTypeElectricityUnitRateDaily.Enabled = false;
                    textEditElectricityPriceRateMinDaily.Enabled = false;
                }
            }
        }

        private void checkEditDailyWater_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditDailyWater.Checked == true)
            {
                textEditRoomTypeWaterUnitRateDaily.Enabled = true;
                textEditWaterPriceRateMinDaily.Enabled = true;
                checkEditWaterSumDaily.Enabled = true;
            }
            else
            {
                textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                textEditWaterPriceRateMinDaily.Enabled = false;
                checkEditWaterSumDaily.Enabled = false;
                textEditWaterSumDaily.Enabled = false;
                checkEditWaterSumDaily.Checked = false;
            }
        }

        private void checkEditWaterSumDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditWaterSumDaily.Checked == true)
            {
                textEditWaterSumDaily.Enabled = true;
                textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                textEditWaterPriceRateMinDaily.Enabled = false;
            }
            else
            {
                textEditWaterSumDaily.Enabled = false;

                if (checkEditDailyWater.Checked == true)
                {
                    textEditRoomTypeWaterUnitRateDaily.Enabled = true;
                    textEditWaterPriceRateMinDaily.Enabled = true;
                }
                else {
                    textEditRoomTypeWaterUnitRateDaily.Enabled = false;
                    textEditWaterPriceRateMinDaily.Enabled = false;
                }
            }
        }

        private void checkEditMonthly_CheckedChanged(object sender, EventArgs e)
        {

            if (button_event == "add" && checkEditMonthly.Checked == false)
            {

                textEditRoomTypeInsurerateRateMonthly.Enabled = false;
                textEditRoomTypeRateMonthly.Enabled = false;
                checkEditMonthlyElectric.Checked = false;
                checkEditMonthlyWater.Checked = false;
                checkEditMonthlyElectric.Enabled = false;
                checkEditMonthlyWater.Enabled = false;
                checkEditMonthlyPhone.Enabled = false;
                textEditRoomTypeAdvanceMonth.Enabled = false;
                textEditLampSum.Enabled = false;
                textEditWaterSum.Enabled = false;
                checkEditWaterSum.Enabled = false;
                checkEditLumpSum.Enabled = false;
                checkEditMonthlyElectric.Enabled = false;

                textEditWaterSum.Enabled = false;
                textEditLampSum.Enabled = false;
            }
            else {

                if (checkEditMonthly.Checked == true)
                {
                    textEditRoomTypeInsurerateRateMonthly.Enabled = true;
                    textEditRoomTypeRateMonthly.Enabled = true;
                    checkEditMonthlyElectric.Checked = true;
                    checkEditMonthlyWater.Checked = true;
                    checkEditMonthlyElectric.Enabled = true;
                    checkEditMonthlyWater.Enabled = true;
                    checkEditMonthlyPhone.Enabled = true;
                    textEditRoomTypeAdvanceMonth.Enabled = true;
                    textEditLampSum.Enabled = true;
                    textEditWaterSum.Enabled = true;
                    checkEditWaterSum.Enabled = true;
                    checkEditLumpSum.Enabled = true;
                    checkEditMonthlyElectric.Enabled = true;

                    textEditWaterSum.Enabled = false;
                    textEditLampSum.Enabled = false;
                }
                else
                {
                    textEditRoomTypeInsurerateRateMonthly.Enabled = false;
                    textEditRoomTypeRateMonthly.Enabled = false;
                    checkEditMonthlyElectric.Checked = false;
                    checkEditMonthlyWater.Checked = false;
                    checkEditMonthlyElectric.Enabled = false;
                    checkEditMonthlyWater.Enabled = false;
                    checkEditMonthlyPhone.Enabled = false;
                    textEditRoomTypeAdvanceMonth.Enabled = false;
                    textEditLampSum.Enabled = false;
                    textEditWaterSum.Enabled = false;
                    checkEditWaterSum.Enabled = false;
                    checkEditLumpSum.Enabled = false;
                    checkEditMonthlyElectric.Enabled = false;

                    textEditWaterSum.Enabled = false;
                    textEditLampSum.Enabled = false;
                }
            
            }

        }

        private void bttOpenItemTab_Click(object sender, EventArgs e)
        {
            AddPanel = new XtraMessageBoxForm();
            AddPanel.StartPosition = FormStartPosition.CenterScreen;


            RoomTypeAdditionItemAdd UserControl = new RoomTypeAdditionItemAdd();

            event_check_click_cost_items = true;

            AddPanel.Width = (UserControl.Width + 16);
            AddPanel.Height = (UserControl.Height + 50);
            AddPanel.Controls.Add(UserControl);
            AddPanel.Show();
            AddPanel.MinimizeBox = false;
            AddPanel.MaximizeBox = false;
            AddPanel.MaximumSize = AddPanel.MinimumSize = AddPanel.Size;
        }

 
        #endregion

        #region Button Action

            private void bttEdit_Click(object sender, EventArgs e)
            {
                change_row_case = false;
                button_event = "edit";
                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;

                bttUpload.Enabled       = true;
                bttDeleteIcon.Enabled   = true;

                bttSave.Enabled = true;
                bttCancel.Enabled = true;

                panelControlMonthly.Enabled = true;
                panelControlAdditional.Enabled = true;
                panelControlDaily.Enabled = true;

                groupControlAdditional.Enabled = true;
                bttOpenItemTab.Enabled = true;
                
                gridControlRoomType.Enabled = false;
                gridControlItem.Enabled = true;
                textEditRoomTypeLabel.Enabled = true;
                textEditRoomTypeLabel.Focus();
            }

            private void bttAdd_Click(object sender, EventArgs e)
            {
                clearData();

                change_row_case = false;
                button_event = "add";

                // enable all channal
                checkEditMonthly.Checked = true;

                panelControlMonthly.Enabled         = true;
                panelControlAdditional.Enabled      = true;
                panelControlDaily.Enabled           = true;

                // Disable Grid
                gridControlRoomType.Enabled         = false;                
                

                textEditRoomTypeLabel.Focus();
                bttUpload.Enabled = true;
                bttDeleteIcon.Enabled = true;
                bttOpenItemTab.Enabled = true;
                groupControlAdditional.Enabled = true;
                gridControlItem.Enabled = true;

                // Input Box Section

                checkEditMonthlyPhone.Checked = true;
                checkEditLumpSum.Checked = false;
                checkEditWaterSum.Checked = false;

                checkEditDaily.Checked = false;
                checkEditDailyElectric.Enabled = false;
                checkEditDailyWater.Enabled = false;
                checkEditDailyPhone.Enabled = false;

                // Set Event
                button_event = "add";
               
                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;
                bttSave.Enabled = true;
                bttCancel.Enabled = true;                
            }

            private void bttSave_Click(object sender, EventArgs e)
            {

                if (checkEditMonthly.Checked == false && checkEditDaily.Checked == false) {

                    utilClass.showPopupMessegeBox(this, getLanguage("_please_select_item"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;

                }
                
                DataTable db = BusinessLogicBridge.DataStore.getBackupConfig();
                if (db.Rows.Count == 0)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1062"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                try
                {
                    //Validate Default
                    DataTable _ValidateTable = validateData();
                    String message = "";
                    if (_ValidateTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                        {
                            message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                        }
                        utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }
                    if (moveFile() == false)
                    {
                        message = getLanguage("_notice_can_not_upload");
                        utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }
                    DataTable _RoomTypeTable = getRoomType();
                    int roomtype_id = int.Parse(_RoomTypeTable.Rows[0]["roomtype_id"].ToString());
                    string roomtype_label = _RoomTypeTable.Rows[0]["roomtype_label"].ToString();

                    int[] rowIndex;
                    switch (button_event)
                    {
                        case "add":
                            roomtype_id = int.Parse(BusinessLogicBridge.DataStore.insertRoomType(_RoomTypeTable).ToString());
                            for (int i = 0; i < gridView2.RowCount; i++)
                            {
                                DataRow CurrentRow = gridView2.GetDataRow(i);
                                Boolean check_box = Convert.ToBoolean(CurrentRow["check_box"].ToString());

                                int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                                string item_name = CurrentRow["item_name"].ToString();
                                double item_price_monthly = Convert.ToDouble(CurrentRow["item_price_monthly"].ToString());
                                double item_price_weekly = 0;
                                double item_price_daily = Convert.ToDouble(CurrentRow["item_price_daily"].ToString());
                                string item_detail = CurrentRow["item_detail"].ToString();
                                string item_vat = CurrentRow["item_vat"].ToString();
                                int item_type = Convert.ToInt16(CurrentRow["item_type"].ToString());
                                string item_flag = CurrentRow["item_flag"].ToString();

                                
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
                                        insertRoomTypeItems(roomtype_id, item_id);
                                    }
                            }
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                            initRoomType();
                            rowIndex = gridView1.GetSelectedRows();
                            int select_row = 0;
                            if (gridView1.RowCount > 0)
                            {
                                select_row = gridView1.RowCount - 1;
                            }
                            gridView1.FocusedRowHandle = select_row;
                            gridView1.SelectRow(select_row);
                            gridView1.UnselectRow(rowIndex[0]);

                            DXWindowsApplication2.MainForm.setToggleBar();

                            break;
                        case "edit":
                            BusinessLogicBridge.DataStore.updateRoomType(_RoomTypeTable);
                            removeRoomTypeItemsByRoomType(roomtype_id);

                            DataTable CheckItemHave = new DataTable();
                            DataTable ItemOfRoomType = new DataTable();
                            DataTable RoomListInfo = new DataTable();
                            string flagdelete = "fristtime";

                            for (int i = 0; i < gridView2.RowCount; i++)
                            {
                                DataRow CurrentRow = gridView2.GetDataRow(i);
                                Boolean check_box = Convert.ToBoolean(CurrentRow["check_box"].ToString());
                                int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                                string item_name = CurrentRow["item_name"].ToString();
                                double item_price_monthly = Convert.ToDouble(CurrentRow["item_price_monthly"].ToString());
                                double item_price_weekly = 0;
                                double item_price_daily = Convert.ToDouble(CurrentRow["item_price_daily"].ToString());
                                string item_detail = CurrentRow["item_detail"].ToString();
                                string item_vat = CurrentRow["item_vat"].ToString();
                                int item_type = Convert.ToInt16(CurrentRow["item_type"].ToString());
                                string item_flag = CurrentRow["item_flag"].ToString();

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
                                        insertRoomTypeItems(roomtype_id, item_id);                               
                                    }
                            }

                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                            MForm.initRoomTypeNav();    
                            //removeOldFile();
                            rowIndex = gridView1.GetSelectedRows();
                            initRoomType();
                            gridView1.FocusedRowHandle = rowIndex[0];
                            gridView1.SelectRow(rowIndex[0]);
                            break;
                    }
                    // Clear Temp Grid Item
                    ItemTableTemp.Rows.Clear();
                    event_check_click_cost_items = false;
                    changeRow();
                    enabled(false);
                    gridControlRoomType.Enabled = true;
                    gridControlItem.Enabled = false;

                    button_event = "";
                    bttAdd.Enabled = true;
                    bttEdit.Enabled = true;
                    bttSave.Enabled = false;
                    bttDelete.Enabled = true;
                    bttCancel.Enabled = false;
                    bttUpload.Enabled = false;
                    bttDeleteIcon.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private void bttDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    int roomtype_id = int.Parse(txtEditId.EditValue.ToString());
                    int total_room = BusinessLogicBridge.DataStore.BasicInfoRoomType_getCountRoom(roomtype_id);
                    if (total_room > 0)
                    {  
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                        return;
                    }

                    if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                    {
                        Boolean result = BusinessLogicBridge.DataStore.BasicInfoRoomType_removeItemByRoomType(roomtype_id);
                        Boolean result_roomtype = BusinessLogicBridge.DataStore.BasicInfoRoomType_remove(roomtype_id);
                        removeFile();
                        int select_row = 0;
                        int[] rowIndex = gridView1.GetSelectedRows();
                        if (rowIndex[0] > 0)
                        {
                            select_row = rowIndex[0] - 1;
                        }

                        
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"),"info");
                        initRoomType();
                        gridView1.FocusedRowHandle = select_row;
                        gridView1.SelectRow(select_row);
                        gridView1.UnselectRow(rowIndex[0]);
                    }
                    //Default Form
                    changeRow();

                    bttUpload.Enabled = false;
                    bttDeleteIcon.Enabled = false;

                    bttCancel.Enabled = false;
                    bttSave.Enabled = false;
                    bttAdd.Enabled = true;
                    gridControlRoomType.Enabled = true;
                    gridControlItem.Enabled = false;

                    enabled(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            private void bttCancel_Click(object sender, EventArgs e)
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    // Clear Temp Grid Item
                    ItemTableTemp.Rows.Clear();
                    event_check_click_cost_items = false;
                    changeRow();
                    enabled(false);
                    gridControlRoomType.Enabled = true;
                    gridControlItem.Enabled = false;

                    button_event = "";
                    bttAdd.Enabled = true;
                    bttEdit.Enabled = true;
                    bttSave.Enabled = false;
                    bttDelete.Enabled = true;
                    bttCancel.Enabled = false;
                    bttUpload.Enabled = false;
                    bttDeleteIcon.Enabled = false;
                }
            }

        #endregion
           
        #region Validation Zone

        private string[] cutString(string paramx)
            {

            string[] textSplited = paramx.Split('.');
            string[] oldformat = new string[2];
            string dot = "";

            textSplited[0].Replace(",", "");

            oldformat[0] = textSplited[0];

            dot = textSplited[1];
            oldformat[1] = dot;

            return oldformat;
        }
        private bool IsEmail(string Email)
        {
            string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return true;
            else
                return false;
        }
        private bool isPhoneValid(string Number)
        {
            string strRegex = @"^(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}(\s*ext(ension)?[0-9]{5})?$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Number))
                return true;
            else
                return false;
        }
        private bool isValidUrl(string url)
        {
            string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
        private bool IsAlphaNumeric(string Text)
        {
            if ((Text.Length > 0) && (Text != ""))
            {
                string strRegex = @"[^a-zA-Z0-9ก-๙\.\,-\/ ]?$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(Text))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool isEmpty(string param)
        {

            if (param.Length < 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool validLength(string param, int length)
        {

            if (param.Length > length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }
}
