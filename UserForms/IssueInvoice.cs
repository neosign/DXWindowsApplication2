using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.Globalization;

namespace DXWindowsApplication2.UserForms
{
    public partial class IssueInvoice : uBase
    {
        private Boolean _CheckRoom = false;
        
        private int room_check_count = 0;

        public static DataTable ItemGeneralCost;
        public static DataTable ItemTableTemp;
        private DataTable CheckInData;
        private static int counterItem;

        private double emeter_price = 0;
        private double wmeter_price = 0;
        private double pmeter_price = 0;
        private double room_price = 0;
        private double phoneprice_per_unit = 0;
        private double EUnit = 0;
        private double WUnit = 0;
        private double PUnit = 0;
        private int CheckinID = 0;

        public int selectRoomID = 0;

        public IssueInvoice()
        {   
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(IssueInvoice_Load);
            textEditPricePerDay.EditValueChanged += new EventHandler(textEditPricePerDay_EditValueChanged);
            checkEditStayNotFullMonth.CheckedChanged += new EventHandler(checkEditStayNotFullMonth_CheckedChanged);

            gridViewInvoice.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewInvoice_FocusedRowChanged);
            gridViewInvoice.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewInvoice_RowClick);
        }

        void gridViewInvoice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle > 0)
            {
                DataRow CurrentRow = gridViewInvoice.GetDataRow(e.RowHandle);

                if (CurrentRow == null)
                {
                    CurrentRow = gridViewInvoice.GetDataRow(0);
                }
                CheckinID = CurrentRow["check_in_id"].To<int>();
                CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(CheckinID);
                loadItem();
            }
        }

        void gridViewInvoice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (e.FocusedRowHandle > 0)
            {
                DataRow CurrentRow = gridViewInvoice.GetDataRow(e.FocusedRowHandle);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewInvoice.GetDataRow(0);
                }
                CheckinID = CurrentRow["check_in_id"].To<int>();
                CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(CheckinID);
                loadItem();
            }

        }

        public override void Refresh()
        {
            base.Refresh();
            setLangThis();

            initDueDateDropdown();
            initPaymentDateDropdown();
            initOtherDateBilling();
            checkScreenShow();
            initRoomPayTypeDropdown();

            initRoomStay("only");

            if (selectRoomID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlLeft.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gridViewInvoice.FocusedRowHandle = c.LocateByValue("room_id", selectRoomID);
                //
                selectRoomID = 0;
            }

        }

        void checkEditStayNotFullMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditStayNotFullMonth.Checked == true)
            {
                panelControlRoomCalcType.Enabled = true;
            }
            else {
                panelControlRoomCalcType.Enabled = false;

                //panelControlDateForType.Enabled = false;

                lookUpEditRoomPayType.EditValue = 1;

                panelControlPricePerDay.Visible = false;
                labelControlTipMSG.Visible = true;
            }
        }

        void textEditPricePerDay_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditPricePerDay.EditValue.ToString() == "") {
                textEditPricePerDay.EditValue = 0.00;
            }
        }

        void IssueInvoice_Load(object sender, EventArgs e)
        {
            setLangThis();

            initType();
            initDueDateDropdown();
            initPaymentDateDropdown();
            initOtherDateBilling();
            checkScreenShow();
            initRoomPayTypeDropdown();

            initRoomStay("only");
        }

        public void setLangThis()
        {
            //
            this.groupInvoiceIssue.Text     = getLanguage("_menu_room_management_issue_invoice");
            this.groupBoxInvoice_Type.Text  = getLanguage("_invoice_format");
            this.groupBoxMonthly.Text = getLanguage("_monthly");
            this.groupBoxDaily.Text = getLanguage("_daily_other");
            this.lbDuedate.Text = getLanguage("_billing_date");
            this.lbpaymentdate.Text = getLanguage("_due_payment_day");
            this.checkEditStayNotFullMonth.Text = getLanguage("_case_stay_not_full_month");
            this.lbPricetype.Text = getLanguage("_rental_price");
            this.lbPrice.Text = getLanguage("_price");
            this.lbBathPerDay.Text = getLanguage("_baht_per_day");            

            this.lbCreateDate.Text = getLanguage("_issue_date");

            this.checkEditAllRoom.Text = getLanguage("_show_all_room");
            this.checkEditAllCheckinOnly.Text = getLanguage("_selectall");

            this.bttCancel.Text = getLanguage("_cancel");
            this.bttInvoiceCreate.Text = getLanguage("_menu_room_management_issue_invoice");

            //
            this.gridColumnBuilding.Caption = getLanguage("_building");
            this.gridColumnFloor.Caption = getLanguage("_floor");
            this.gridColumnRoomType.Caption = getLanguage("_room_type");
            this.gridColumnRoomLabel.Caption = getLanguage("_room_name");
            this.gridColumncheck_in_contracttype_text.Caption = getLanguage("_rental_type");
            this.groupRoomList.Text = getLanguage("_rent_room_list");

        }

        void checkScreenShow() {

            if (radioGroupDueType.SelectedIndex == 0)
            {
                panelControlDateForType.Enabled = false;
            }
            else
            {
                panelControlDateForType.Enabled = true;
            }

            //if (radioGroupInvoiceType.SelectedIndex == 0)
            //{
            //    groupBoxMonthly.Enabled = true;
            //    groupBoxDaily.Enabled = false;
            //}
            //else
            //{
            //    groupBoxMonthly.Enabled = false;
            //    groupBoxDaily.Enabled = true;
            //}
        }

        void initType() { 
            object[] itemValues = new object[] {0, 1};

            string [] itemDescriptions = new string [] {getLanguage("_by_billing_date"), getLanguage("_by_custom")};

            for(int i = 0; i < itemValues.Length; i++) {
                radioGroupDueType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(itemValues[i], itemDescriptions[i]));
            }
            radioGroupDueType.EditValue = 0;
        }

        void initRoomStay(string condition) {

            DataTable roomList = BusinessLogicBridge.DataStore.getAllRoomStay();

            roomList.Columns.Add("check_in_contracttype_text", typeof(string));
            roomList.Columns.Add("checkbox", typeof(bool));
            roomList.Columns.Add("ppresent_date_update", typeof(DateTime));

            DataTable checkInInfo = new DataTable();

            if (roomList.Rows.Count <= 0)
            {

                bttInvoiceCreate.Enabled = false;
                bttCancel.Enabled = false;
                
            }
            else {
                bttInvoiceCreate.Enabled = true;
                bttCancel.Enabled = true;
            }

            //
            for(int i=0; i<roomList.Rows.Count; i++)
            {
                checkInInfo = BusinessLogicBridge.DataStore.getCheckinByRoomAndTenantID(roomList.Rows[i]["room_id"].To<int>(), roomList.Rows[i]["current_tenant_id"].To<int>(), 3);

                if (checkInInfo.Rows.Count > 0)
                {
                    CheckinID = checkInInfo.Rows[0]["check_in_id"].To<int>();
                    room_price = checkInInfo.Rows[0]["roomtype_month_roomrate_price"].To<double>();
                    if (checkInInfo.Rows[0]["check_in_contracttype"].ToString() != "3")
                    {
                        roomList.Rows.RemoveAt(i);
                    }
                    else
                    {
                        roomList.Rows[i]["check_in_contracttype_text"] = getLanguage("_monthly");
                    }
                }
                else {
                    roomList.Rows.RemoveAt(i);
                }                
            }

            if (roomList.Rows.Count > 0)
            {
                for (int i = 0; i < roomList.Rows.Count; i++)
                {
                    roomList.Rows[i]["check_in_contracttype_text"] = getLanguage("_monthly");
                    roomList.Rows[i]["checkbox"] = false;
                }
            }

            gridControlLeft.DataSource = roomList;

            if (checkInInfo.Rows.Count>0)
            loadItem();
        }

        void loadItem() {
            loadItem(0);
        }

        void loadItem(int checkin_id)
        {
            double amountofstay = 0;

            double cost_netprice = 0;
            double cost_sumprice = 0;
            double cost_vatprice = 0;
            bool vat_bool = false;

            ItemGeneralCost = new DataTable();
            // E-Meter
            double e_unit = 0;
            double e_price_per_unit = 0;
            double e_price_sum = 0;

            // W-Meter
            double w_unit = 0;
            double w_price_per_unit = 0;
            double w_price_sum = 0;

            ItemGeneralCost.Columns.Add("order", typeof(int));
            ItemGeneralCost.Columns.Add("item_name", typeof(string));
            ItemGeneralCost.Columns.Add("item_amount", typeof(string));
            ItemGeneralCost.Columns.Add("item_priceperunit", typeof(string));
            ItemGeneralCost.Columns.Add("item_sumprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vatprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_netprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vat_bool", typeof(bool));

            if (checkin_id != 0) {
                CheckinID = checkin_id;
            }

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(CheckinID);

            CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(CheckinID);

            DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CheckInData.Rows[0]["building_id"].To<int>());

            EUnit = (CheckInData.Rows[0]["e_end_energy"].To<double>() - CheckInData.Rows[0]["previous_energy_billing"].To<double>());
            WUnit = (CheckInData.Rows[0]["w_end_energy"].To<double>() - CheckInData.Rows[0]["wprevious_energy_billing"].To<double>());
            PUnit = CheckInData.Rows[0]["duration"].To<double>();

                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                    {

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                        {
                            cost_vatprice = (room_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = room_price;
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                        {
                            cost_sumprice = room_price;
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                        vat_bool = true;
                    }
                    else
                    {
                        cost_sumprice = room_price;
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        vat_bool = false;
                    }

                    ItemGeneralCost.Rows.Add(1, "ค่าเช่าห้อง", 1.0, room_price, cost_sumprice, cost_vatprice, cost_netprice, vat_bool);

                    DataTable LatestInvoice = BusinessLogicBridge.DataStore.getLatestInvoiceByCheckinID(CheckinID);
                    if (LatestInvoice.Rows.Count > 0)
                    {
                        amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - LatestInvoice.Rows[0]["inv_trans_cutduedate"].To<DateTime>()).TotalDays;
                        amountofstay = amountofstay - 1;
                    }
                    else
                    {
                        amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
                    }

                    amountofstay = Math.Ceiling(amountofstay);

                    // Check roomtype payment calculation

                    #region Electricity Calculation
                    if (CheckInData.Rows[0]["roomtype_month_electric_checked"].To<bool>() == true)
                    {
                        // ขั้นต่ำ (กรณีที่มีการใส่ราคาขั้นต่ำ ที่ Roomtype ถ้าเป็น 0.00 ถือว่าไม่ได้คิดขั้นต่ำ)
                        if (CheckInData.Rows[0]["roomtype_month_electric_priceminrate"].To<double>() <= 0)
                        {
                            // Calculate type [Price Per Unit] use roomtype_month_electric_priceperunit
                            // - แบบราคาต่อหน่วย      
                            //  จำนวนหน่วย - End Value(วันที่ Check out) - Begin Value (วันที่ CHeck in)
                            //  จำนวนเงินต่อหน่วย = ราคาต่อหน่วย (Set อยู่ที่ Room type screen)
                            //  จำนวนเงินรวม = จำนวนหน่วย * ราคาต่อหน่วย

                            e_unit = EUnit;
                            e_price_per_unit = CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>();
                            e_price_sum = e_price_per_unit * e_unit;

                        }
                        else
                        {
                            // Calculate type [Have Min Rate] use roomtype_month_electric_priceminrate

                            // การคิดขั้นต่ำ จะต้องคำนวณแบบราคาต่อหน่วย แล้วนำ จำนวนเงินรวม เปรียบเทียบกับ ราคาขั้นต่ำ
                            // a.จำนวนเงินรวม(จากราคาต่อหน่วย) >= ราคาขั้นต่ำ -> กรณีนี้ให้คิดแบบราคาต่อหน่วยเลย
                            // b.จำนวนเงินรวม(จากราคาต่อหน่วย)< ราคาขั้นต่ำ -> กรณนี้ กำหนดให้
                            // - จำนวนหน่วย =1
                            // - ราคาต่อหน่วย= จำนวนราคาขั้นต่ำ
                            // - จำนวนเงิน= จำนวนราคาขั้นต่ำ

                            e_unit = EUnit;
                            e_price_per_unit = CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>();
                            e_price_sum = e_price_per_unit * e_unit;

                            if (e_price_sum >= CheckInData.Rows[0]["roomtype_month_electric_priceminrate"].To<double>())
                            {
                                // More than or equal Price Per Unit
                                e_price_sum = e_price_per_unit * e_unit;
                            }
                            else
                            {
                                // Less than Min Rate Price
                                e_unit = 1;
                                e_price_per_unit = CheckInData.Rows[0]["roomtype_month_electric_priceminrate"].To<double>();
                                e_price_sum = e_price_per_unit * e_unit;
                            }
                        }
                    }
                    else if (CheckInData.Rows[0]["roomtype_month_electric_lumpchecked"].To<bool>() == true)
                    {
                        // roomtype_month_electric_lumpchecked   use roomtype_month_electric_lumpprice 
                        //- เหมาจ่าย 
                        // ถ้ามีการเลือก เหมาจ่าย Check box ให้คิดแบบเหมาจ่าย
                        // จำนวน = จำนวนวันที่เช่า * ราคาเหมาจ่าย
                        // จำนวนเงินต่อหน่วย = ราคาเหมาจ่าย(จาก Roomtype)
                        // จำนวนเงินรวม = จำนวน(วัน) * จำนวนเงินต่อหน่วย

                        e_unit = amountofstay * CheckInData.Rows[0]["roomtype_month_electric_lumpprice"].To<double>();
                        e_price_per_unit = CheckInData.Rows[0]["roomtype_month_electric_lumpprice"].To<double>();
                        e_price_sum = amountofstay * e_price_per_unit;

                    }

                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                    {

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                        {
                            cost_vatprice = (e_price_sum * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = e_price_sum;
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                        {
                            cost_sumprice = e_price_sum;
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                        vat_bool = true;
                    }
                    else
                    {
                        cost_sumprice = e_price_sum;
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        vat_bool = false;
                    }


                    ItemGeneralCost.Rows.Add(2, "ค่าไฟ", e_unit, e_price_per_unit, cost_sumprice, cost_vatprice, cost_netprice, vat_bool);

                    #endregion

                    #region Water Calculation
                    if (CheckInData.Rows[0]["roomtype_month_water_checked"].To<bool>() == true)
                    {
                        // ขั้นต่ำ (กรณีที่มีการใส่ราคาขั้นต่ำ ที่ Roomtype ถ้าเป็น 0.00 ถือว่าไม่ได้คิดขั้นต่ำ)
                        if (CheckInData.Rows[0]["roomtype_month_water_priceminrate"].To<double>() <= 0)
                        {
                            // Calculate type [Price Per Unit] use roomtype_month_water_priceperunit
                            // - แบบราคาต่อหน่วย      
                            //  จำนวนหน่วย - End Value(วันที่ Check out) - Begin Value (วันที่ CHeck in)
                            //  จำนวนเงินต่อหน่วย = ราคาต่อหน่วย (Set อยู่ที่ Room type screen)
                            //  จำนวนเงินรวม = จำนวนหน่วย * ราคาต่อหน่วย

                            w_unit = WUnit;
                            w_price_per_unit = CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>();
                            w_price_sum = w_price_per_unit * w_unit;

                        }
                        else
                        {
                            // Calculate type [Have Min Rate] use roomtype_month_water_priceminrate

                            // การคิดขั้นต่ำ จะต้องคำนวณแบบราคาต่อหน่วย แล้วนำ จำนวนเงินรวม เปรียบเทียบกับ ราคาขั้นต่ำ
                            // a.จำนวนเงินรวม(จากราคาต่อหน่วย) >= ราคาขั้นต่ำ -> กรณีนี้ให้คิดแบบราคาต่อหน่วยเลย
                            // b.จำนวนเงินรวม(จากราคาต่อหน่วย)< ราคาขั้นต่ำ -> กรณนี้ กำหนดให้
                            // - จำนวนหน่วย =1
                            // - ราคาต่อหน่วย= จำนวนราคาขั้นต่ำ
                            // - จำนวนเงิน= จำนวนราคาขั้นต่ำ

                            w_unit = WUnit;
                            w_price_per_unit = CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>();
                            w_price_sum = w_price_per_unit * w_unit;

                            if (w_price_sum >= CheckInData.Rows[0]["roomtype_month_water_priceminrate"].To<double>())
                            {
                                // More than or equal Price Per Unit
                                w_price_sum = w_price_per_unit * w_unit;
                            }
                            else
                            {
                                // Less than Min Rate Price
                                w_unit = 1;
                                w_price_per_unit = CheckInData.Rows[0]["roomtype_month_water_priceminrate"].To<double>();
                                w_price_sum = w_price_per_unit * w_unit;
                            }
                        }
                    }
                    else if (CheckInData.Rows[0]["roomtype_month_water_lumpchecked"].To<bool>() == true)
                    {
                        // roomtype_month_electric_lumpchecked   use roomtype_month_water_lumpprice 
                        //- เหมาจ่าย 
                        // ถ้ามีการเลือก เหมาจ่าย Check box ให้คิดแบบเหมาจ่าย
                        // จำนวน = จำนวนวันที่เช่า * ราคาเหมาจ่าย
                        // จำนวนเงินต่อหน่วย = ราคาเหมาจ่าย(จาก Roomtype)
                        // จำนวนเงินรวม = จำนวน(วัน) * จำนวนเงินต่อหน่วย

                        w_unit = amountofstay * CheckInData.Rows[0]["roomtype_month_water_lumpprice"].To<double>();
                        w_price_per_unit = CheckInData.Rows[0]["roomtype_month_water_lumpprice"].To<double>();
                        w_price_sum = amountofstay * w_price_per_unit;

                    }

                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                    {

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                        {
                            cost_vatprice = (w_price_sum * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = w_price_sum;
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                        {
                            cost_sumprice = w_price_sum;
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                        vat_bool = true;
                    }
                    else
                    {
                        cost_sumprice = w_price_sum;
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        vat_bool = false;
                    }


                    ItemGeneralCost.Rows.Add(3, "ค่าน้ำ", w_unit, w_price_per_unit, cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
                    #endregion

                    #region Phone Calculation
                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                    {

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                        {
                            cost_vatprice = (pmeter_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = pmeter_price;
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                        {
                            cost_sumprice = pmeter_price;
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                        vat_bool = true;
                    }
                    else
                    {
                        cost_sumprice = pmeter_price;
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        vat_bool = false;
                    }

                    ItemGeneralCost.Rows.Add(4, "ค่าโทรศัพท์", "-", "-", cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
                    #endregion
                
            

            gridControlGeneralCost.DataSource = ItemGeneralCost;

            if (CheckInData.Rows.Count > 0)
            {
                initItem(CheckInData.Rows[0]["room_id"].To<int>());
            }
            else
            {
                initItem();
            }
        }

        void initItem() {
            initItem(0);
        }

        void initItem(int room_id)
        {

            DataTable ItemTable = new DataTable();

            DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CheckInData.Rows[0]["building_id"].To<int>());

            DataTable InvoiceInfoChecked = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(CheckinID);

            if (room_id==0)
                ItemTable = BusinessLogicBridge.DataStore.getItemByRoomID(CheckInData.Rows[0]["room_id"].To<int>());
            else
                ItemTable = BusinessLogicBridge.DataStore.getItemByRoomID(room_id);

            ItemTable.Columns.Add("order", typeof(int));
            if (ItemTable.Columns.Contains("item_amount") == false)
                ItemTable.Columns.Add("item_amount", typeof(double));
            if (ItemTable.Columns.Contains("item_priceperunit") == false)
                ItemTable.Columns.Add("item_priceperunit", typeof(double));
            if (ItemTable.Columns.Contains("item_sumprice") == false)
                ItemTable.Columns.Add("item_sumprice", typeof(double));
            if (ItemTable.Columns.Contains("item_vatprice") == false)
                ItemTable.Columns.Add("item_vatprice", typeof(double));
            if (ItemTable.Columns.Contains("item_netprice") == false)
                ItemTable.Columns.Add("item_netprice", typeof(double));
            if (ItemTable.Columns.Contains("item_vat_bool") == false)
                ItemTable.Columns.Add("item_vat_bool", typeof(bool));

            DataTable CheckOutItemTable = ItemTable.Clone();

            counterItem = 5;
            //
            if (ItemTableTemp != null)
            {
                try
                {
                    ItemTable.Merge(ItemTableTemp);
                }
                catch { }
            }
            //

            for (int i = 0; i < ItemTable.Rows.Count; i++)
            {

                //if (ItemTable.Rows[i]["item_type"].To<int>() == 1)
                //{

                    ItemTable.Rows[i]["order"] = counterItem;

                    if (ItemTable.Rows[i]["item_id"].To<int>() == 0)
                    {
                        ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_priceperunit"];
                    }
                    else
                    {

                        ItemTable.Rows[i]["item_amount"] = 1;

                        if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                        {
                            // daily
                            ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_price_daily"];
                        }
                        else
                        {
                            // Monthly
                            ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_price_monthly"];
                        }
                    }

                    // Sum Price will equal price per unit alway
                    ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_priceperunit"];

                    if (ItemTable.Rows[i]["item_vat"].ToString() != "1")
                    {
                        ItemTable.Rows[i]["item_vat_bool"] = true;
                        // include vat
                        if (ItemTable.Rows[i]["item_vat"].ToString() == "2")
                        {

                            ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                            //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                        }
                        // exclude vat
                        if (ItemTable.Rows[i]["item_vat"].ToString() == "3")
                        {
                            ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                            //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                        }
                    }
                    else
                    {
                        ItemTable.Rows[i]["item_vat_bool"] = false;
                        ItemTable.Rows[i]["item_vatprice"] = 0;
                    }

                    counterItem++;
                    CheckOutItemTable.ImportRow(ItemTable.Rows[i]);
                //}
                //else if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                //{

                //    ItemTable.Rows[i]["order"] = counterItem;

                //    if (ItemTable.Rows[i]["item_id"].To<int>() == 0)
                //    {
                //        ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_priceperunit"];
                //    }
                //    else
                //    {
                //        ItemTable.Rows[i]["item_amount"] = 1;

                //        // daily
                //        ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_price_daily"];
                //    }

                //    // Sum Price will equal price per unit alway
                //    ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_priceperunit"];

                //    if (ItemTable.Rows[i]["item_vat"].ToString() != "1")
                //    {
                //        ItemTable.Rows[i]["item_vat_bool"] = true;
                //        // include vat
                //        if (ItemTable.Rows[i]["item_vat"].ToString() == "2")
                //        {

                //            ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                //            //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                //        }
                //        // exclude vat
                //        if (ItemTable.Rows[i]["item_vat"].ToString() == "3")
                //        {
                //            ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                //            //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                //        }
                //    }
                //    else
                //    {
                //        ItemTable.Rows[i]["item_vat_bool"] = false;
                //        ItemTable.Rows[i]["item_vatprice"] = 0;
                //    }

                //    counterItem++;
                //    CheckOutItemTable.ImportRow(ItemTable.Rows[i]);
                //}

            }

            //
            if (ItemTableTemp == null)
                ItemTableTemp = ItemTable.Clone();
            //

            CheckOutItemTable = MainForm.VatCalculate(CheckOutItemTable);

            gridControlItem.DataSource = CheckOutItemTable;
            ItemTableTemp.Clear();

            //
        }

        void re_calculate() {

            int _countCheckbox = 0;
            string InvoiceNO = "";
            DataTable gridTable = new DataTable();
            gridTable = (DataTable)gridControlLeft.DataSource;

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                {
                    _countCheckbox++;
                }
            }

            if (_countCheckbox <= 0)
            {
                utilClass.showPopupMessegeBox(this, "Please select item ...", getLanguage("_softwarename"));
                return;
            }

            // HaveInvoice

            int HaveInvoice = 0;
            int roomID = 0;

            #region declare parameter
            DataTable DTInvoice = new DataTable();
            DataTable DTDocInfo = new DataTable();
            DataTable DTInvoiceInfo = new DataTable();
            DataTable DTCheckInItem = new DataTable();

            DataTable CheckInData = new DataTable();

            int invoiceID = 0;

            string e_previous_date = "";
            string e_previous_energy = "";
            string w_previous_date = "";
            string w_previous_energy = "";

            string e_present_date = "";
            string e_present_energy = "";
            string w_present_date = "";
            string w_present_energy = "";

            string p_previous_date = "";
            string p_present_date = "";

            double EUnit = 0;
            double WUnit = 0;
            double PUnit = 0;

            double emeter_price = 0;
            double wmeter_price = 0;
            double pmeter_price = 0;

            double phoneprice_per_unit = 0;

            double room_price = 0;
            double amountofstay = 0;

            double sumprice = 0;
            double price_vat = 0;
            double sumprice_net = 0;

            double cost_netprice = 0;
            double cost_sumprice = 0;
            double cost_vatprice = 0;

            int item_id = 0;
            double item_sumprice = 0;
            double item_vatprice = 0;
            double item_netprice = 0;
            double item_priceperunit = 0;

            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;
            #endregion

            #region DeClare Column Type of Data Table DTInvoice
            DTInvoice.Columns.Add("inv_trans_number", typeof(string));
            DTInvoice.Columns.Add("inv_trans_cutduedate", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_fixpaymentdate", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_datecreated", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_status", typeof(int));
            DTInvoice.Columns.Add("inv_trans_tenantname", typeof(string));
            DTInvoice.Columns.Add("inv_trans_tenantaddress", typeof(string));
            DTInvoice.Columns.Add("inv_trans_roomlabel", typeof(string));
            DTInvoice.Columns.Add("inv_trans_building", typeof(string));
            DTInvoice.Columns.Add("inv_trans_floor", typeof(string));
            DTInvoice.Columns.Add("inv_trans_roomtype", typeof(string));
            DTInvoice.Columns.Add("inv_trans_emeter_previous_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_emeter_previous_energy", typeof(string));
            DTInvoice.Columns.Add("inv_trans_emeter_present_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_emeter_present_energy", typeof(string));
            DTInvoice.Columns.Add("inv_trans_emeter_unit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_emeter_priceperunit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_emeter_price", typeof(double));
            DTInvoice.Columns.Add("inv_trans_wmeter_previous_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_wmeter_previous_energy", typeof(string));
            DTInvoice.Columns.Add("inv_trans_wmeter_present_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_wmeter_present_energy", typeof(string));
            DTInvoice.Columns.Add("inv_trans_wmeter_unit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_wmeter_priceperunit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_wmeter_price", typeof(double));
            DTInvoice.Columns.Add("inv_trans_phone_start_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_phone_end_date", typeof(DateTime));
            DTInvoice.Columns.Add("inv_trans_phonemeter_unit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_phonemeter_priceperunit", typeof(double));
            DTInvoice.Columns.Add("inv_trans_phone_price", typeof(double));
            DTInvoice.Columns.Add("inv_trans_sumprice", typeof(double));
            DTInvoice.Columns.Add("inv_trans_sumprice_withvat", typeof(double));
            DTInvoice.Columns.Add("inv_trans_sumprice_net", typeof(double));
            DTInvoice.Columns.Add("inv_trans_print_status", typeof(int));
            DTInvoice.Columns.Add("inv_trans_room_id", typeof(int));
            DTInvoice.Columns.Add("inv_trans_company_name", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_logofile", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_address", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_telephone", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_fax", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_email", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_website", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_tax_id", typeof(string));
            DTInvoice.Columns.Add("inv_trans_company_vision", typeof(string));
            DTInvoice.Columns.Add("inv_trans_doc_header_invoice", typeof(string));
            DTInvoice.Columns.Add("inv_trans_doc_footer_invoice", typeof(string));
            DTInvoice.Columns.Add("inv_trans_doc_under_invoice1", typeof(string));
            DTInvoice.Columns.Add("inv_trans_doc_under_invoice2", typeof(string));
            DTInvoice.Columns.Add("inv_trans_doc_dateformat", typeof(int));
            DTInvoice.Columns.Add("inv_trans_doc_logo_position", typeof(int));
            DTInvoice.Columns.Add("inv_trans_roomprice", typeof(double));
            DTInvoice.Columns.Add("inv_trans_vattype", typeof(int));
            DTInvoice.Columns.Add("check_in_id", typeof(int));
            DTInvoice.Columns.Add("check_in_contracttype", typeof(int));
            DTInvoice.Columns.Add("inv_trans_amountdays", typeof(int));
            DTInvoice.Columns.Add("inv_trans_vatrate", typeof(decimal));

            DataTable ItemTable = new DataTable();
            ItemTable.Columns.Add("inv_trans_id", typeof(int));
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

            DataRow currentRow;

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                {
                    currentRow = gridViewInvoice.GetDataRow(gridViewInvoice.FocusedRowHandle);
                    roomID = currentRow["room_id"].To<int>();
                    DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(gridTable.Rows[i]["building_id"].To<int>());

                    if (DTDocInfo.Rows[0]["doc_saperate_invoice"].ToString() == "0")
                    {
                        InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        // Monthly Case
                        InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                    }

                    DTInvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(gridTable.Rows[i]["check_in_id"].To<int>());

                    CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(gridTable.Rows[i]["check_in_id"].To<int>());


                    DataTable E_VALUEDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomID);
                    DataTable W_VALUEDT = BusinessLogicBridge.DataStore.getW_EndValueByRoomID(roomID);
                    DataTable P_VALUEDT = BusinessLogicBridge.DataStore.getP_EndValueByRoomID(roomID);

                    e_previous_date = E_VALUEDT.Rows[0]["previous_date_billing"].ToString();
                    e_previous_energy = E_VALUEDT.Rows[0]["previous_energy_billing"].ToString();
                    w_previous_date = W_VALUEDT.Rows[0]["wprevious_date_billing"].ToString();
                    w_previous_energy = W_VALUEDT.Rows[0]["wprevious_energy_billing"].ToString();
                    if (P_VALUEDT.Rows[0]["start_date"].ToString() != "")
                        p_previous_date = P_VALUEDT.Rows[0]["start_date"].To<DateTime>().ToString("MM/dd/yyyy") + " " + P_VALUEDT.Rows[0]["start_time"].ToString();
                    else
                        p_previous_date = DateTime.Today.Date.ToString("MM/dd/yyyy") + " " + DateTime.Now.TimeOfDay.ToString();

                    e_present_date = E_VALUEDT.Rows[0]["end_date"].ToString();
                    e_present_energy = E_VALUEDT.Rows[0]["end_energy"].ToString();
                    w_present_date = W_VALUEDT.Rows[0]["end_date"].ToString();
                    w_present_energy = W_VALUEDT.Rows[0]["end_energy"].ToString();
                    if (P_VALUEDT.Rows[0]["end_date"].ToString() != "")
                        p_present_date = P_VALUEDT.Rows[0]["end_date"].To<DateTime>().ToString("MM/dd/yyyy") + " " + P_VALUEDT.Rows[0]["end_time"].ToString();
                    else
                        p_present_date = DateTime.Today.Date.ToString("MM/dd/yyyy") + " " + DateTime.Now.TimeOfDay.ToString();

                    EUnit = (e_present_energy.To<double>() - e_previous_energy.To<double>());
                    WUnit = (w_present_energy.To<double>() - w_previous_energy.To<double>());


                    if (P_VALUEDT.Rows[0]["duration"].ToString() == "")
                    {
                        TimeSpan span = TimeSpan.Zero;
                        PUnit = Math.Floor(span.TotalMinutes);
                    }
                    else
                    {
                        TimeSpan span = ((TimeSpan)CheckInData.Rows[0]["duration"]);
                        PUnit = Math.Floor(span.TotalMinutes);
                    }
                    // Split into hours:minutes: span.Hours, span.Minutes
                    // Total span in hours: span.TotalHours
                    // Total span in minutes (hours * 60): span.TotalMinutes

                    phoneprice_per_unit = 0;


                    //emeter_price = EUnit * CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>();
                    //wmeter_price = WUnit * CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>();
                    //pmeter_price = CheckInData.Rows[0]["amount"].To<double>(); //PUnit * phoneprice_per_unit;

                    room_price = CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>();

                    if (radioGroupDueType.SelectedIndex == 1)
                    {
                        if (checkEditStayNotFullMonth.Checked == true)
                        {
                            // 1 = full month
                            // 2 = (full month / 30) * amount of stay
                            // 3 = assign Price per day * amount of day

                            // Get Invoice latest
                            DataTable LatestInvoice = BusinessLogicBridge.DataStore.getLatestInvoiceByCheckinID(CheckInData.Rows[0]["check_in_id"].To<int>());
                            if (LatestInvoice.Rows.Count > 0)
                            {
                                amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - LatestInvoice.Rows[0]["inv_trans_cutduedate"].To<DateTime>()).TotalDays;

                            }
                            else
                            {
                                amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
                                amountofstay = amountofstay + 1;
                            }


                            amountofstay = Math.Ceiling(amountofstay);

                            if (lookUpEditRoomPayType.EditValue.ToString() == "1")
                            {
                                room_price = CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>();

                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = (room_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = room_price;

                                        if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                        {
                                            cost_vatprice = ((room_price * amountofstay) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                            cost_netprice = room_price * amountofstay;
                                        }


                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = room_price;
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }
                                }
                                else
                                {
                                    if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                    {
                                        cost_sumprice = room_price * amountofstay;
                                    }
                                    else
                                    {
                                        cost_sumprice = room_price;
                                    }

                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }

                                ItemGeneralCost.Rows[0]["item_amount"] = 1;
                                ItemGeneralCost.Rows[0]["item_priceperunit"] = room_price.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");

                                if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                {
                                    ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                    ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                }
                            }

                            if (lookUpEditRoomPayType.EditValue.ToString() == "2")
                            {
                                room_price = (CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>() / 30);

                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = ((amountofstay * room_price) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = (amountofstay * room_price);
                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = (amountofstay * room_price);
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }
                                }
                                else
                                {
                                    cost_sumprice = (amountofstay * room_price);
                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }

                                ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                ItemGeneralCost.Rows[0]["item_priceperunit"] = room_price.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");
                            }

                            if (lookUpEditRoomPayType.EditValue.ToString() == "3")
                            {

                                room_price = textEditPricePerDay.EditValue.To<double>();

                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = ((amountofstay * room_price) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = (amountofstay * room_price);
                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = (amountofstay * room_price);
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }
                                }
                                else
                                {
                                    cost_sumprice = (amountofstay * room_price);
                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }

                                ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                ItemGeneralCost.Rows[0]["item_priceperunit"] = textEditPricePerDay.EditValue.To<double>().ToString("N2");
                                ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");
                            }
                        }
                    }
                    //// Prefix name
                    DataTable DTPrefixTenant = BusinessLogicBridge.DataStore.getPrefixByID(CheckInData.Rows[0]["tenant_prefix_id"].To<int>());

                    string labelprefix = "prefix_" + current_lang + "_label";

                    string prefixfull = DTPrefixTenant.Rows[0][labelprefix].ToString();
                    //

                    //DTInvoice.Rows.Add(InvoiceNO, dateEditCutDueDate.EditValue, dateEditFixDatePayment.EditValue, DateTime.Now, 0, prefixfull + "||" + CheckInData.Rows[0]["tenant_name"].ToString() + "||" + CheckInData.Rows[0]["tenant_surname"].ToString(), CheckInData.Rows[0]["tenant_address"].ToString(), CheckInData.Rows[0]["room_label"].ToString(), CheckInData.Rows[0]["building_label"].ToString(), CheckInData.Rows[0]["floor_code"].ToString(), CheckInData.Rows[0]["roomtype_label"].ToString(), e_previous_date, e_previous_energy, e_present_date, e_present_energy, EUnit, CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>(), emeter_price, w_previous_date, w_previous_energy, w_present_date, w_present_energy, WUnit, CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>(), wmeter_price, p_previous_date, p_present_date, PUnit, phoneprice_per_unit, pmeter_price, sumprice, price_vat, sumprice_net, 0, CheckInData.Rows[0]["room_id"].To<int>(), CheckInData.Rows[0]["company_name"].ToString(), CheckInData.Rows[0]["company_logo"].ToString(), CheckInData.Rows[0]["company_address"].ToString(), CheckInData.Rows[0]["company_telephone"].ToString(), CheckInData.Rows[0]["company_fax"].ToString(), CheckInData.Rows[0]["company_email"].ToString(), CheckInData.Rows[0]["company_website"].ToString(), CheckInData.Rows[0]["company_tax_id"].ToString(), CheckInData.Rows[0]["company_vision"].ToString(), DTDocInfo.Rows[0]["doc_header_invoice"].ToString(), DTDocInfo.Rows[0]["doc_footer_invoice"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice1"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice2"].ToString(), DTDocInfo.Rows[0]["doc_dateformat"].To<int>(), DTDocInfo.Rows[0]["doc_logo_position"].To<int>(), room_price, DTDocInfo.Rows[0]["doc_vat_type"].To<int>(), CheckInData.Rows[0]["check_in_id"].To<int>(), CheckInData.Rows[0]["check_in_contracttype"].To<int>(), amountofstay, DTDocInfo.Rows[0]["doc_vat"].To<decimal>());
                    //invoiceID = BusinessLogicBridge.DataStore.createInvoiceTransaction(DTInvoice);
                    //DTInvoice.Clear();

                    double price_of_daily = 0;
                    double price_of_monthly = 0;

                   // if (invoiceID > 0)
                   // {
                       // BusinessLogicBridge.DataStore.insert_payment_flag(CheckInData.Rows[0]["room_id"].To<int>());

                        DTCheckInItem = ((DataTable)gridControlItem.DataSource);

                        if (DTCheckInItem.Rows.Count > 0)
                        {
                            for (int k = 0; k < DTCheckInItem.Rows.Count; k++)
                            {
                                item_id = DTCheckInItem.Rows[k]["item_id"].To<int>();
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
                                    // ADD Price Per Unit intead item_price_monthly or item_price_daily
                                    //_tmp.Rows.Add(DTCheckInItem.Rows[k]["item_name"].ToString(), DTCheckInItem.Rows[k]["item_priceperunit"].To<double>(), 0.00, DTCheckInItem.Rows[k]["item_priceperunit"].To<double>(), "", DTCheckInItem.Rows[k]["item_vat"].To<double>(), DTCheckInItem.Rows[k]["item_type"].To<int>(), "manual");
                                    //
                                    //item_id = BusinessLogicBridge.DataStore.BasicInfoItem_insert(_tmp);
                                }

                                if (DTCheckInItem.Rows[k]["item_type"].To<int>() == 1)
                                {   // Mounhly case

                                    if (DTCheckInItem.Rows[k]["item_id"].To<int>() == 0)
                                    {
                                        item_priceperunit = DTCheckInItem.Rows[k]["item_priceperunit"].To<double>();
                                        price_of_daily = item_priceperunit;
                                        price_of_monthly = item_priceperunit;
                                    }
                                    else
                                    {
                                        if (DTCheckInItem.Rows[k]["item_vat"].To<int>() == 1)
                                        {
                                            // daily
                                            item_priceperunit = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                        }
                                        else
                                        {
                                            // Monthly
                                            item_priceperunit = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();
                                        }

                                        price_of_daily = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                        price_of_monthly = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();
                                    }

                                    item_sumprice = DTCheckInItem.Rows[k]["item_sumprice"].To<double>();
                                    item_vatprice = DTCheckInItem.Rows[k]["item_vatprice"].To<double>();
                                    item_netprice = item_sumprice + item_vatprice;
                                }
                                else
                                {
                                    // one time

                                    if (DTCheckInItem.Rows[k]["item_vat"].To<int>() == 1)
                                    {
                                        // daily
                                        item_priceperunit = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                        price_of_daily = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                        price_of_monthly = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();

                                        item_sumprice = DTCheckInItem.Rows[k]["item_sumprice"].To<double>();
                                        item_vatprice = DTCheckInItem.Rows[k]["item_vatprice"].To<double>();
                                        item_netprice = item_sumprice + item_vatprice;
                                    }
                                }
                                total_sum_price_of_item += item_sumprice;
                                total_vat_price_of_item += item_vatprice;
                                total_net_price_of_item += item_netprice;
                               // ItemTable.Rows.Add(invoiceID, item_id, DTCheckInItem.Rows[k]["item_name"].ToString(), price_of_monthly, price_of_daily, DTCheckInItem.Rows[k]["item_vat"].To<int>(), DTCheckInItem.Rows[k]["item_type"].To<int>(), item_priceperunit, 1, item_sumprice, item_vatprice, item_netprice, DTCheckInItem.Rows[k]["item_flag"].ToString());
                            }
                            //BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);
                        }
                   // } // End Created Invoice ID

                    double sumGeneralCost = 0;
                    double vatSumGeneralCost = 0;
                    double netGeneralCost = 0;

                    for (int cost = 0; cost < ItemGeneralCost.Rows.Count; cost++)
                    {
                        sumGeneralCost += ItemGeneralCost.Rows[cost]["item_sumprice"].To<double>();
                        vatSumGeneralCost += ItemGeneralCost.Rows[cost]["item_vatprice"].To<double>();
                        netGeneralCost += ItemGeneralCost.Rows[cost]["item_netprice"].To<double>();
                    }

                    sumprice = sumGeneralCost + total_sum_price_of_item;    // Sum Total Price of Items
                    price_vat = vatSumGeneralCost + total_vat_price_of_item; // Vat Total Price of Items
                    sumprice_net = netGeneralCost + total_net_price_of_item; // Net Total Price of Items
                    
                    // Update Price On invoice_transaction Table Here ...

                    //BusinessLogicBridge.DataStore.updateInvoicePriceByID(sumprice, price_vat, sumprice_net, invoiceID);


                }
            }

        }

        void initOtherDateBilling(){

            dateEditInvoiceDateCreate.Properties.MinValue = DateTime.Now;
            dateEditInvoiceDateCreate.EditValue = DateTime.Now;
        }

        void initDueDateDropdown()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);

            dateEditCutDueDate.EditValue = dt;
        }

        void initPaymentDateDropdown()
        {
           
            int MonthFix = DateTime.Today.Month + 1;

            DateTime dtFix = new DateTime();

            if (MonthFix > 12)
            {
                dtFix = new DateTime(DateTime.Today.Year, 1, 5, 0, 0, 0, 0);
            }
            else
            {
                dtFix = new DateTime(DateTime.Today.Year, MonthFix, 5, 0, 0, 0, 0);
                
            }

            dateEditFixDatePayment.EditValue = dtFix;
        }

        void initRoomPayTypeDropdown(){
            
            DataTable TypePay   = new DataTable();

            TypePay.Columns.Add("RoomPayTypeLabel", typeof(string));
            TypePay.Columns.Add("RoomPayTypeID", typeof(int));

            if (current_lang == "th")
            {
                TypePay.Rows.Add("เต็มเดือน", 1);
                TypePay.Rows.Add("ตามวันอยู่จริง", 2);
                TypePay.Rows.Add("ระบุราคาต่อวัน", 3);
            }
            else {
                TypePay.Rows.Add("Full month", 1);
                TypePay.Rows.Add("Actual day", 2);
                TypePay.Rows.Add("Price per day", 3);
            }

            lookUpEditRoomPayType.Properties.DisplayMember = "RoomPayTypeLabel";
            lookUpEditRoomPayType.Properties.ValueMember = "RoomPayTypeID";
            lookUpEditRoomPayType.Properties.DataSource = TypePay;

            lookUpEditRoomPayType.EditValue = 1;
            
        }

        private void radioGroupDueType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radioGroupDueType.SelectedIndex == 0)
            {
                panelControlDateForType.Enabled = false;

                checkEditStayNotFullMonth.Checked = false;
                lookUpEditRoomPayType.EditValue = 1;

                panelControlPricePerDay.Visible = false;
                labelControlTipMSG.Visible = true;

            }
            else {
                panelControlDateForType.Enabled = true;
            }


        }

        private void radioGroupInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (radioGroupInvoiceType.SelectedIndex == 0)
            //{

            //    initDueDateDropdown();
            //    initPaymentDateDropdown();

            //    radioGroupDueType.SelectedIndex = 0;

            //    panelControlDateForType.Enabled = false;

            //    checkEditStayNotFullMonth.Checked = false;
            //    lookUpEditRoomPayType.EditValue = 1;

            //    panelControlPricePerDay.Visible = false;
            //    labelControlTipMSG.Visible = true;

            //    groupBoxMonthly.Enabled = true;
            //    groupBoxDaily.Enabled = false;
            //}
            //else
            //{
            //    groupBoxMonthly.Enabled = false;
            //    groupBoxDaily.Enabled = true;
            //}
        }

        private void checkEditAllCheckinOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (_CheckRoom == false)
            {
                _CheckRoom = true;
                //eventSelected();
            }
            else
            {
                _CheckRoom = false;
                //eventUnselect();
            }

            room_check_count = 0;
            if (gridViewInvoice.RowCount > 0)
            {
                for (int i = 0; i < gridViewInvoice.RowCount; i++)
                {
                    if (gridViewInvoice.Columns[0].View.GetRowCellValue(i, "room_status").ToString() == "2")
                    {
                        gridViewInvoice.Columns[0].View.SetRowCellValue(i, "checkbox", _CheckRoom);
                    }
                    
                    
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }

        private void checkEditAllRoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditAllRoom.Checked == true)
            {
                initRoomStay("all");
            }
            else {
                initRoomStay("only");
            }
        }

        private void bttInvoiceCreate_Click(object sender, EventArgs e)
        {   
            int _countCheckbox = 0;
            string InvoiceNO ="";
            DataTable   gridTable =  new DataTable(); 
                        gridTable = (DataTable) gridControlLeft.DataSource;

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                {       
                        _countCheckbox++;
                }
            }

            if(_countCheckbox <= 0){
                utilClass.showPopupMessegeBox(this, "Please select item ...", getLanguage("_softwarename"));
                return;  
            }

           // Check Date payment
            int result = DateTime.Compare(dateEditFixDatePayment.EditValue.To<DateTime>(),dateEditCutDueDate.EditValue.To<DateTime>());

            if (result < 0){
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1026"), getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }

             // HaveInvoice

            int HaveInvoice =0;
            int roomID = 0;
            
                if (utilClass.showPopupConfirmBox(this, "Do you want create Issue Invoice", getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    #region declare parameter
                    DataTable DTInvoice = new DataTable();
                    DataTable DTDocInfo = new DataTable();
                    DataTable DTInvoiceInfo = new DataTable();
                    DataTable DTCheckInItem = new DataTable();

                    DataTable CheckInData = new DataTable();

                    int invoiceID = 0;

                    string e_previous_date = "";
                    string e_previous_energy = "";
                    string w_previous_date = "";
                    string w_previous_energy = "";

                    string e_present_date = "";
                    string e_present_energy = "";
                    string w_present_date = "";
                    string w_present_energy = "";

                    string p_previous_date = "";
                    string p_present_date = "";

                    double EUnit = 0;
                    double WUnit = 0;
                    double PUnit = 0;

                    double emeter_price = 0;
                    double wmeter_price = 0;
                    double pmeter_price = 0;

                    double phoneprice_per_unit = 0;

                    double room_price = 0;
                    double amountofstay = 0;

                    double sumprice = 0;
                    double price_vat = 0;
                    double sumprice_net = 0;

                    double cost_netprice = 0;
                    double cost_sumprice = 0;
                    double cost_vatprice = 0;

                    int item_id = 0;
                    double item_sumprice = 0;
                    double item_vatprice = 0;
                    double item_netprice = 0;
                    double item_priceperunit = 0;

                    double total_sum_price_of_item = 0;
                    double total_vat_price_of_item = 0;
                    double total_net_price_of_item = 0;
                    #endregion

                    #region DeClare Column Type of Data Table DTInvoice
                    DTInvoice.Columns.Add("inv_trans_number", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_cutduedate", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_fixpaymentdate", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_datecreated", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_status", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_tenantname", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_tenantaddress", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_roomlabel", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_building", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_floor", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_roomtype", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_emeter_previous_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_emeter_previous_energy", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_emeter_present_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_emeter_present_energy", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_emeter_unit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_emeter_priceperunit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_emeter_price", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_wmeter_previous_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_wmeter_previous_energy", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_wmeter_present_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_wmeter_present_energy", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_wmeter_unit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_wmeter_priceperunit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_wmeter_price", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_phone_start_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_phone_end_date", typeof(DateTime));
                    DTInvoice.Columns.Add("inv_trans_phonemeter_unit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_phonemeter_priceperunit", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_phone_price", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_sumprice", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_sumprice_withvat", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_sumprice_net", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_print_status", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_room_id", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_company_name", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_logofile", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_address", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_telephone", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_fax", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_email", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_website", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_tax_id", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_company_vision", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_doc_header_invoice", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_doc_footer_invoice", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_doc_under_invoice1", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_doc_under_invoice2", typeof(string));
                    DTInvoice.Columns.Add("inv_trans_doc_dateformat", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_doc_logo_position", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_roomprice", typeof(double));
                    DTInvoice.Columns.Add("inv_trans_vattype", typeof(int));
                    DTInvoice.Columns.Add("check_in_id", typeof(int));
                    DTInvoice.Columns.Add("check_in_contracttype", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_amountdays", typeof(int));
                    DTInvoice.Columns.Add("inv_trans_vatrate", typeof(decimal));

                    DataTable ItemTable = new DataTable();
                    ItemTable.Columns.Add("inv_trans_id", typeof(int));
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

                    DataRow currentRow;

                    for (int i = 0; i < gridTable.Rows.Count; i++)
                    {
                        if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                        {

                            currentRow = gridViewInvoice.GetDataRow(gridViewInvoice.FocusedRowHandle);

                            roomID = currentRow["room_id"].To<int>();

                            HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceByRoomID(roomID);

                            if ((HaveInvoice > 0) && (radioGroupInvoiceType.SelectedIndex == 0))
                            {
                                utilClass.showPopupMessegeBox(this, "Last due date Invoice has been created ...", getLanguage("_softwarename"));
                                return;
                            }
                            else if ((DateTime.Now < dateEditCutDueDate.EditValue.To<DateTime>()) && (radioGroupInvoiceType.SelectedIndex == 0))
                            {
                                utilClass.showPopupMessegeBox(this, "Invoice must be created after due date...", getLanguage("_softwarename"));
                                return;
                            }
                            else
                            {

                                DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(gridTable.Rows[i]["building_id"].To<int>());

                                if (DTDocInfo.Rows[0]["doc_saperate_invoice"].ToString() == "0")
                                {
                                    InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                                }
                                else {
                                        // Monthly Case
                                    InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                                }

                                DTInvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(gridTable.Rows[i]["check_in_id"].To<int>());

                                CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(gridTable.Rows[i]["check_in_id"].To<int>());


                                DataTable E_VALUEDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomID);
                                DataTable W_VALUEDT = BusinessLogicBridge.DataStore.getW_EndValueByRoomID(roomID);
                                DataTable P_VALUEDT = BusinessLogicBridge.DataStore.getP_EndValueByRoomID(roomID);

                                e_previous_date = E_VALUEDT.Rows[0]["previous_date_billing"].ToString();
                                e_previous_energy = E_VALUEDT.Rows[0]["previous_energy_billing"].ToString();
                                w_previous_date = W_VALUEDT.Rows[0]["wprevious_date_billing"].ToString();
                                w_previous_energy = W_VALUEDT.Rows[0]["wprevious_energy_billing"].ToString();
                                if (P_VALUEDT.Rows[0]["start_date"].ToString() != "")
                                    p_previous_date = P_VALUEDT.Rows[0]["start_date"].To<DateTime>().ToString("MM/dd/yyyy") + " " + P_VALUEDT.Rows[0]["start_time"].ToString();
                                else
                                    p_previous_date = DateTime.Today.Date.ToString("MM/dd/yyyy") + " " + DateTime.Now.TimeOfDay.ToString();

                                e_present_date = E_VALUEDT.Rows[0]["end_date"].ToString();
                                e_present_energy = E_VALUEDT.Rows[0]["end_energy"].ToString();
                                w_present_date = W_VALUEDT.Rows[0]["end_date"].ToString();
                                w_present_energy = W_VALUEDT.Rows[0]["end_energy"].ToString();
                                if (P_VALUEDT.Rows[0]["end_date"].ToString() != "")
                                    p_present_date = P_VALUEDT.Rows[0]["end_date"].To<DateTime>().ToString("MM/dd/yyyy") + " " + P_VALUEDT.Rows[0]["end_time"].ToString();
                                else
                                    p_present_date = DateTime.Today.Date.ToString("MM/dd/yyyy") + " " + DateTime.Now.TimeOfDay.ToString();

                                EUnit = (e_present_energy.To<double>() - e_previous_energy.To<double>());
                                WUnit = (w_present_energy.To<double>() - w_previous_energy.To<double>());


                                if (P_VALUEDT.Rows[0]["duration"].ToString() == "")
                                {
                                    TimeSpan span = TimeSpan.Zero;
                                    PUnit = Math.Floor(span.TotalMinutes);
                                }
                                else
                                {
                                    TimeSpan span = ((TimeSpan)CheckInData.Rows[0]["duration"]);
                                    PUnit = Math.Floor(span.TotalMinutes);
                                }
                                // Split into hours:minutes: span.Hours, span.Minutes
                                // Total span in hours: span.TotalHours
                                // Total span in minutes (hours * 60): span.TotalMinutes

                                phoneprice_per_unit = 0;


                                emeter_price = EUnit * CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>();
                                wmeter_price = WUnit * CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>();
                                pmeter_price = CheckInData.Rows[0]["amount"].To<double>(); //PUnit * phoneprice_per_unit;

                                #region Electric
                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {
                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = (emeter_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = emeter_price;
                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = emeter_price;
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }

                                }
                                else
                                {
                                    cost_sumprice = emeter_price;
                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }
                                emeter_price = cost_netprice;
                                #endregion

                                #region water
                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {
                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = (wmeter_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = wmeter_price;
                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = wmeter_price;
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }
                                }
                                else
                                {
                                    cost_sumprice = wmeter_price;
                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }

                                wmeter_price = cost_netprice;
                                #endregion

                                #region Phone
                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                {

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                    {
                                        cost_vatprice = (pmeter_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = pmeter_price;
                                        cost_sumprice = cost_netprice - cost_vatprice;
                                    }

                                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                    {
                                        cost_sumprice = pmeter_price;
                                        cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                        cost_netprice = cost_sumprice + cost_vatprice;
                                    }

                                }
                                else
                                {
                                    cost_sumprice = pmeter_price;
                                    cost_vatprice = 0.00;
                                    cost_netprice = cost_sumprice;
                                }
                                #endregion

                                room_price = CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>();

                                if (radioGroupDueType.SelectedIndex == 1)
                                {
                                    if (checkEditStayNotFullMonth.Checked == true)
                                    {
                                        // 1 = full month
                                        // 2 = (full month / 30) * amount of stay
                                        // 3 = assign Price per day * amount of day

                                        // Get Invoice latest
                                        DataTable LatestInvoice = BusinessLogicBridge.DataStore.getLatestInvoiceByCheckinID(CheckInData.Rows[0]["check_in_id"].To<int>());
                                        if (LatestInvoice.Rows.Count > 0)
                                        {
                                            amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - LatestInvoice.Rows[0]["inv_trans_cutduedate"].To<DateTime>()).TotalDays;

                                        }
                                        else
                                        {
                                            amountofstay = (dateEditCutDueDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
                                            amountofstay = amountofstay + 1;
                                        }


                                        amountofstay = Math.Ceiling(amountofstay);

                                        if (lookUpEditRoomPayType.EditValue.ToString() == "1")
                                        {
                                            room_price = CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>();

                                            if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                            {

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                                {
                                                    cost_vatprice = (room_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = room_price;

                                                    if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                                    {
                                                        cost_vatprice = ((room_price * amountofstay) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                        cost_netprice = room_price * amountofstay;
                                                    }


                                                    cost_sumprice = cost_netprice - cost_vatprice;
                                                }

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                                {
                                                    cost_sumprice = room_price;
                                                    cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = cost_sumprice + cost_vatprice;
                                                }
                                            }
                                            else
                                            {
                                                if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                                {
                                                    cost_sumprice = room_price * amountofstay;
                                                }
                                                else
                                                {
                                                    cost_sumprice = room_price;
                                                }

                                                cost_vatprice = 0.00;
                                                cost_netprice = cost_sumprice;
                                            }

                                            ItemGeneralCost.Rows[0]["item_amount"] = 1;
                                            ItemGeneralCost.Rows[0]["item_priceperunit"] = room_price.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");

                                            if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                                            {
                                                ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                                ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                            }
                                        }

                                        if (lookUpEditRoomPayType.EditValue.ToString() == "2")
                                        {
                                            room_price = (CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>() / 30);

                                            if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                            {

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                                {
                                                    cost_vatprice = ((amountofstay * room_price) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = (amountofstay * room_price);
                                                    cost_sumprice = cost_netprice - cost_vatprice;
                                                }

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                                {
                                                    cost_sumprice = (amountofstay * room_price);
                                                    cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = cost_sumprice + cost_vatprice;
                                                }
                                            }
                                            else
                                            {
                                                cost_sumprice = (amountofstay * room_price);
                                                cost_vatprice = 0.00;
                                                cost_netprice = cost_sumprice;
                                            }

                                            ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                            ItemGeneralCost.Rows[0]["item_priceperunit"] = room_price.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");
                                        }

                                        if (lookUpEditRoomPayType.EditValue.ToString() == "3")
                                        {

                                            room_price = textEditPricePerDay.EditValue.To<double>();

                                            if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                                            {

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                                                {
                                                    cost_vatprice = ((amountofstay * room_price) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = (amountofstay * room_price);
                                                    cost_sumprice = cost_netprice - cost_vatprice;
                                                }

                                                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 3)
                                                {
                                                    cost_sumprice = (amountofstay * room_price);
                                                    cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                                    cost_netprice = cost_sumprice + cost_vatprice;
                                                }
                                            }
                                            else
                                            {
                                                cost_sumprice = (amountofstay * room_price);
                                                cost_vatprice = 0.00;
                                                cost_netprice = cost_sumprice;
                                            }

                                            ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                                            ItemGeneralCost.Rows[0]["item_priceperunit"] = textEditPricePerDay.EditValue.To<double>().ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                                            ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");
                                        }
                                    }
                                }
                                //// Prefix name
                                DataTable DTPrefixTenant = BusinessLogicBridge.DataStore.getPrefixByID(CheckInData.Rows[0]["tenant_prefix_id"].To<int>());

                                string labelprefix = "prefix_" + current_lang + "_label";

                                string prefixfull = DTPrefixTenant.Rows[0][labelprefix].ToString();
                                
                                // Data Create Invoice Transaction
                                DTInvoice.Rows.Add(InvoiceNO, dateEditCutDueDate.EditValue, dateEditFixDatePayment.EditValue, DateTime.Now, 0, prefixfull + "||" + CheckInData.Rows[0]["tenant_name"].ToString() + "||" + CheckInData.Rows[0]["tenant_surname"].ToString(), CheckInData.Rows[0]["tenant_address"].ToString(), CheckInData.Rows[0]["room_label"].ToString(), CheckInData.Rows[0]["building_label"].ToString(), CheckInData.Rows[0]["floor_code"].ToString(), CheckInData.Rows[0]["roomtype_label"].ToString(), e_previous_date, e_previous_energy, e_present_date, e_present_energy, EUnit, CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>(), emeter_price, w_previous_date, w_previous_energy, w_present_date, w_present_energy, WUnit, CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>(), wmeter_price, p_previous_date, p_present_date, PUnit, phoneprice_per_unit, pmeter_price, sumprice, price_vat, sumprice_net, 0, CheckInData.Rows[0]["room_id"].To<int>(), CheckInData.Rows[0]["company_name"].ToString(), CheckInData.Rows[0]["company_logo"].ToString(), CheckInData.Rows[0]["company_address"].ToString(), CheckInData.Rows[0]["company_telephone"].ToString(), CheckInData.Rows[0]["company_fax"].ToString(), CheckInData.Rows[0]["company_email"].ToString(), CheckInData.Rows[0]["company_website"].ToString(), CheckInData.Rows[0]["company_tax_id"].ToString(), CheckInData.Rows[0]["company_vision"].ToString(), DTDocInfo.Rows[0]["doc_header_invoice"].ToString(), DTDocInfo.Rows[0]["doc_footer_invoice"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice1"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice2"].ToString(), DTDocInfo.Rows[0]["doc_dateformat"].To<int>(), DTDocInfo.Rows[0]["doc_logo_position"].To<int>(), room_price, DTDocInfo.Rows[0]["doc_vat_type"].To<int>(), CheckInData.Rows[0]["check_in_id"].To<int>(), CheckInData.Rows[0]["check_in_contracttype"].To<int>(), amountofstay, DTDocInfo.Rows[0]["doc_vat"].To<decimal>());
                                invoiceID = BusinessLogicBridge.DataStore.createInvoiceTransaction(DTInvoice);
                                DTInvoice.Clear();

                                double price_of_daily = 0;
                                double price_of_monthly = 0;

                               if (invoiceID > 0)
                                 {
                                 BusinessLogicBridge.DataStore.insert_payment_flag(CheckInData.Rows[0]["room_id"].To<int>());
                                 
                                 initItem(CheckInData.Rows[0]["room_id"].To<int>());

                                 DTCheckInItem = ((DataTable)gridControlItem.DataSource);

                                   total_sum_price_of_item =0;
                                   total_vat_price_of_item =0;
                                   total_net_price_of_item = 0;

                                if (DTCheckInItem.Rows.Count > 0)
                                {
                                    for (int k = 0; k < DTCheckInItem.Rows.Count; k++)
                                    {
                                        item_id = DTCheckInItem.Rows[k]["item_id"].To<int>();
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
                                            // ADD Price Per Unit intead item_price_monthly or item_price_daily
                                            _tmp.Rows.Add(DTCheckInItem.Rows[k]["item_name"].ToString(), DTCheckInItem.Rows[k]["item_priceperunit"].To<double>(), 0.00, DTCheckInItem.Rows[k]["item_priceperunit"].To<double>(), "", DTCheckInItem.Rows[k]["item_vat"].To<double>(), DTCheckInItem.Rows[k]["item_type"].To<int>(), "manual");
                                            //
                                            item_id = BusinessLogicBridge.DataStore.BasicInfoItem_insert(_tmp);
                                        }

                                        if (DTCheckInItem.Rows[k]["item_type"].To<int>() == 1)
                                        {   // Mounhly case

                                            if (DTCheckInItem.Rows[k]["item_id"].To<int>() == 0)
                                            {
                                                item_priceperunit = DTCheckInItem.Rows[k]["item_priceperunit"].To<double>();
                                                price_of_daily = item_priceperunit;
                                                price_of_monthly = item_priceperunit;
                                            }
                                            else
                                            {
                                                if (DTCheckInItem.Rows[k]["item_vat"].To<int>() == 1)
                                                {
                                                    // daily
                                                    item_priceperunit = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                                }
                                                else
                                                {
                                                    // Monthly
                                                    item_priceperunit = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();
                                                }

                                                price_of_daily = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                                price_of_monthly = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();
                                            }

                                            item_sumprice = DTCheckInItem.Rows[k]["item_sumprice"].To<double>();
                                            item_vatprice = DTCheckInItem.Rows[k]["item_vatprice"].To<double>();
                                            item_netprice = item_sumprice + item_vatprice;
                                        }
                                        else
                                        {
                                            // one time

                                            if (DTCheckInItem.Rows[k]["item_vat"].To<int>() == 1)
                                            {
                                                // daily
                                                item_priceperunit = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                                price_of_daily = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                                price_of_monthly = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();

                                                item_sumprice = DTCheckInItem.Rows[k]["item_sumprice"].To<double>();
                                                item_vatprice = DTCheckInItem.Rows[k]["item_vatprice"].To<double>();
                                                item_netprice = item_sumprice + item_vatprice;
                                            }
                                        }
                                        total_sum_price_of_item += item_sumprice;
                                        total_vat_price_of_item += item_vatprice;
                                        total_net_price_of_item += item_netprice;
                                        ItemTable.Rows.Add(invoiceID, item_id, DTCheckInItem.Rows[k]["item_name"].ToString(), price_of_monthly, price_of_daily, DTCheckInItem.Rows[k]["item_vat"].To<int>(), DTCheckInItem.Rows[k]["item_type"].To<int>(), item_priceperunit, 1, item_sumprice, item_vatprice, item_netprice, DTCheckInItem.Rows[k]["item_flag"].ToString());
                                    }
                                    BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);
                                    ItemTable.Clear();
                                }
                               } // End Created Invoice ID

                               loadItem(CheckInData.Rows[0]["check_in_id"].To<int>());

                                double sumGeneralCost = 0;
                                double vatSumGeneralCost = 0;
                                double netGeneralCost = 0;

                                for (int cost = 0; cost < ItemGeneralCost.Rows.Count; cost++)
                                {
                                    sumGeneralCost += ItemGeneralCost.Rows[cost]["item_sumprice"].To<double>();
                                    vatSumGeneralCost += ItemGeneralCost.Rows[cost]["item_vatprice"].To<double>();
                                    netGeneralCost += ItemGeneralCost.Rows[cost]["item_netprice"].To<double>();
                                }

                                sumprice = sumGeneralCost + total_sum_price_of_item;    // Sum Total Price of Items
                                price_vat = vatSumGeneralCost + total_vat_price_of_item; // Vat Total Price of Items
                                sumprice_net = netGeneralCost + total_net_price_of_item; // Net Total Price of Items

                                // Update Price On invoice_transaction Table Here ...

                                BusinessLogicBridge.DataStore.updateInvoicePriceByID(sumprice, price_vat, sumprice_net, invoiceID);

                            } // End Checkbox Case
                        }
                    }
                    initRoomStay("only");
                    MForm.initRoomBarButton();
                    MForm.loadDashBoard();
                    MForm.OpenNewTabBar();
                    
            }

        }

        private void lookUpEditRoomPayType_EditValueChanged(object sender, EventArgs e)
        {
            labelControlTipMSG.Visible = true;
            panelControlPricePerDay.Visible = false;

            switch(lookUpEditRoomPayType.EditValue.To<int>()) {
                case 1 :
                    if (current_lang == "th")
                    {
                        labelControlTipMSG.Text = "คิดเต็มเดือนตามประเภทห้อง";
                    }
                    else {
                        labelControlTipMSG.Text = "Paid full period follow room type";
                    }
                    break;
                case 2:
                    if (current_lang == "th")
                    {
                        labelControlTipMSG.Text = "((ราคาเต็มเดือน/30) x จำนวนวันอยู่จริง)";
                    }
                    else
                    {
                        labelControlTipMSG.Text = "(Price of full month/30 x Actual number of days)";
                    }
                    break;
                case 3:
                    labelControlTipMSG.Visible = false;
                    panelControlPricePerDay.Visible = true;
                    break;
                default :
                    if (current_lang == "th")
                    {
                        labelControlTipMSG.Text = "คิดเต็มเดือนตามประเภทห้อง";
                    }
                    else
                    {
                        labelControlTipMSG.Text = "Paid full period follow room type";
                    }
                    break;
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            initRoomStay("all");
        }

        private void bttCalculate_Click(object sender, EventArgs e)
        {
            re_calculate();
        }

    }
}
