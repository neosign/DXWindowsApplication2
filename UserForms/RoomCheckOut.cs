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
    public partial class RoomCheckOut :uBase
    {
        public static XtraMessageBoxForm AddPanel;
        public static DataTable dataItemsStaticTable;
        public static DataTable dataItemsForCheck;
        public static DataTable ItemTableTemp;
        private DataTable ItemForDelete;
        public static DataTable DTDocInfo;
        public static int checkin_contract_type = 0;
        public static DataTable ItemGeneralCost;
        public static int counterItem;
        public static int flagFirst = 0;
        //
        public int selectRoomID = 0;

        private string button_event = "";

        private int action_key = 0;

        public double emeter_price = 0;
        public double wmeter_price = 0;
        public double pmeter_price = 0;
        public double room_price = 0;
        public double phoneprice_per_unit = 0;
        public double EUnit = 0;
        public double WUnit = 0;
        public double PUnit = 0;

        public RoomCheckOut()
        {
            InitializeComponent();
            //

            this.Load += new EventHandler(RoomCheckOut_Load);
            this.Resize += new EventHandler(RoomCheckOut_Resize);
            //
            this.bttAddItem.Click+=new EventHandler(bttAddItem_Click);
            this.bttCalculate.Click += new EventHandler(bttCalculate_Click);
            this.bttCancel.Click +=new EventHandler(bttCancel_Click);
            this.bttEdit.Click += new EventHandler(bttEdit_Click);
            this.bttPrintInsurance.Click += new EventHandler(bttPrintInsurance_Click);
            this.bttPrintInvoice.Click += new EventHandler(bttPrintInvoice_Click);
            this.bttRemoveItem.Click +=new EventHandler(bttRemoveItem_Click);
            //
            gridViewRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            gridViewItemList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewItemList_FocusedRowChanged);
            gridViewItemList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewItemList_RowClick);
            lookUpEditCharge.EditValueChanged += new EventHandler(lookUpEditCharge_EditValueChanged);

            textEditChargePrice.EditValueChanged += new EventHandler(textEditChargePrice_EditValueChanged);

            SaveClick += new EventHandler(bttSave_Click);
        }

        void gridViewItemList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rowIndex = gridViewItemList.GetSelectedRows();

            if (rowIndex.Length <= 0)
                return;

            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewItemList.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewItemList.GetDataRow(0);
                }

                DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

                if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                {
                    // Dialy Case
                    lookUpEditCharge.EditValue = 1;
                    lookUpEditCharge.Enabled = false;
                    reCalculate();
                }
                else
                {
                    lookUpEditCharge.EditValue = 1;
                    lookUpEditCharge.Enabled = true;
                    reCalculate();
                }

                DataTable ItemCheck = BusinessLogicBridge.DataStore.getItemByRoomID(textEditRoomId.EditValue.To<int>());

                DataRow[] findrow = ItemCheck.Select("item_id=" + CurrentRow["item_id"].To<int>());

                if (findrow.Length > 0)
                {

                    bttRemoveItem.Enabled = false;
                }
                else
                {
                    bttRemoveItem.Enabled = true;
                }
            }
        }

        void textEditChargePrice_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditChargePrice.EditValue.ToString() == "") {
                textEditChargePrice.EditValue = 0.00;
            }
        }

        void gridViewItemList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewItemList.GetSelectedRows();

            if (rowIndex.Length <= 0)
                return;

            if (rowIndex.Length != 0)
            {   
                DataRow CurrentRow = gridViewItemList.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewItemList.GetDataRow(0);
                }

                DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

                if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                {
                    // Dialy Case
                    lookUpEditCharge.EditValue = 1;
                    lookUpEditCharge.Enabled = false;
                    reCalculate();
                }
                else
                {
                    lookUpEditCharge.EditValue = 1;
                    lookUpEditCharge.Enabled = true;
                    reCalculate();
                }

                DataTable ItemCheck = BusinessLogicBridge.DataStore.getItemByRoomID(textEditRoomId.EditValue.To<int>());

                DataRow[] findrow = ItemCheck.Select("item_id=" + CurrentRow["item_id"].To<int>());

                if (findrow.Length > 0) { 
                
                    bttRemoveItem.Enabled = false;
                }
                else
                {
                    bttRemoveItem.Enabled = true;
                }
            }
            
        }

        void RoomCheckOut_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
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
                gridViewRoom.FocusedRowHandle = c.LocateByValue("room_id", selectRoomID);
                //
                selectRoomID = 0;
            }
            //
            changeRow();

            if (flagFirst != 0) {
                reCalculate();
                flagFirst = 1;
            }
        }

        void lookUpEditCharge_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit row = (LookUpEdit)sender;
            if (row.EditValue != null)
            {
                int charge_id = Convert.ToInt32(row.EditValue.ToString());
                //
                textEditChargePrice.Enabled = charge_id == 3;
                
                bttCalculate.Enabled = true;
            }

            switch (lookUpEditCharge.EditValue.To<int>())
            {
                case 1:
                    if (current_lang == "th")
                    {
                        labelControlTipMSG.Text = "คิดเต็มเดือนตามประเภทห้อง";
                    }
                    else
                    {
                        labelControlTipMSG.Text = "Paid full period follow room type";
                    }
                    labelControlTipMSG.Visible = true;
                    panelControlPricePerDay.Visible = false;
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
                    labelControlTipMSG.Visible = true;
                    panelControlPricePerDay.Visible = false;
                    break;
                case 3:
                    labelControlTipMSG.Visible = false;
                    panelControlPricePerDay.Visible = true;
                    break;
                default:
                    if (current_lang == "th")
                    {
                        labelControlTipMSG.Text = "คิดเต็มเดือนตามประเภทห้อง";
                    }
                    else
                    {
                        labelControlTipMSG.Text = "Paid full period follow room type";
                    }
                    labelControlTipMSG.Visible = true;
                    panelControlPricePerDay.Visible = false;
                    break;
            }

        }

        void enableControl(bool status)
        {
            dateEditLeaveDate.Enabled = status;
            lookUpEditCharge.Enabled = status;
            //
            if (status == false)
                textEditChargePrice.Enabled = false;
            //
            bttCalculate.Enabled = status;
            bttSave.Enabled = status;
            bttCancel.Enabled = status;
            //
            gridControlItem.Enabled = status;
            bttAddItem.Enabled = status;
            bttRemoveItem.Enabled = status;
            gridControlRoom.Enabled = !status;
            
        }

        void RoomCheckOut_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;

            initDropDownBuilding();
            initDropDownFloor(1);
            initDropDownRoomType();
            initDropDownContractType();
            initDropDownCharge();
            initRoom();

            //
            setLangThis();
        }

        public void setLangThis()
        {
            //
            this.Name = getLanguage("_tenant_info");
            //
            this.groupControlList.Text = getLanguage("_check_out_list");
            this.groupControlCheckOut.Text = getLanguage("_check_out");
            this.groupControlEndDate.Text = getLanguage("_recording_date");
            this.groupControlEndMeter.Text = getLanguage("_last_meter");
            this.groupControlRental.Text = getLanguage("_rental");
            this.groupControlStartDate.Text = getLanguage("_recording_date");
            this.groupControlStartMeter.Text = getLanguage("_meter_start");
            this.groupRoomInfo.Text = getLanguage("_room_info");
            this.groupExpense.Text = getLanguage("_addittional_cost");
            this.groupControlMeter.Text = getLanguage("_record_group");
            //
            this.grid_check_out_room_name.Caption = getLanguage("_room_name");
            this.grid_check_out_building_label.Caption = getLanguage("_building");
            this.grid_check_out_floor_code.Caption = getLanguage("_floor");
            this.grid_check_out_roomtype_label.Caption = getLanguage("_room_type");
            this.grid_check_out_room_status.Caption = getLanguage("_room_status");
            this.grid_check_out_tenant_name.Caption = getLanguage("_firstname");
            this.grid_check_out_tenant_surname.Caption = getLanguage("_lastname");
            this.grid_check_out_leave_date.Caption = getLanguage("_require_check_out_date");

            //
            this.labelControlContractNo.Text = getLanguageWithColon("_contract_no");
            this.labelControlRentType.Text = getLanguageWithColon("_rental_type");
            this.labelControlDeposit.Text = getLanguageWithColon("_book_amount");
            this.labelControlContractDate.Text = getLanguageWithColon("_contract_date");
            this.labelControlMinimum.Text = getLanguageWithColon("_minimum_rent_duration");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            //
            this.labelControlMonthlyRate.Text = getLanguageWithColon("_rent");
            this.labelControlDeposit.Text = getLanguageWithColon("_advance_charge");
            this.labelControlInsurance.Text = getLanguageWithColon("_insurance_charge");
            //
            this.labelControlTitle.Text = getLanguageWithColon("_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlSurname.Text = getLanguageWithColon("_lastname");
            //
            this.labelControlWaterMeter.Text = getLanguage("_water");
            this.labelControlElectricMeter.Text = getLanguage("_electricity");
            this.labelControlPhoneMeter.Text = getLanguage("_telephone");
            this.labelControlTelStart.Text = getLanguage("_date_start");
            this.labelControlTelEnd.Text = getLanguage("_date_end");
            this.labelControlTelAmount.Text = getLanguage("_phone_charge");
            //
            this.labelControlCheckOutDate.Text = getLanguage("_check_out_date");
            this.labelControlRentalPrice.Text = getLanguage("_rental_price");
            this.labelControlPrice.Text = getLanguage("_price");
            this.labelControlRefund.Text = getLanguage("_refund_deposit");

            //Grid
            this.gridColumNo.Caption = getLanguage("_no");
            this.colInvoiceItemName.Caption = getLanguage("_item");
            this.gridColumnAmount.Caption = getLanguage("_unit");
            this.gridColumnAmountPerUnit.Caption = getLanguage("_price_per_unit");
            this.colInvoiceItemPrice.Caption = getLanguage("_total_price");
            this.gridColumnVating.Caption = getLanguage("_tax");
            this.gridColumnVat.Caption = getLanguage("_total_charge");            
            //
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_close_contract");
            this.bttCancel.Text = getLanguage("_cancel");
            //
            this.bttPrintInvoice.Text = getLanguage("_create_invoice");
            this.bttPrintInsurance.Text = getLanguage("_create_insurance");
            //
            this.bttCalculate.Text = getLanguage("_calculate");
            //
            this.bttAddItem.Text = getLanguage("_add");
            this.bttRemoveItem.Text = getLanguage("_delete");
            //
            this.labelControlMonth.Text = getLanguage("_month");
            this.labelControlMonth2.Text = getLanguage("_month");
            this.labelControlBaht0.Text = getLanguage("_baht");
            this.labelControlBaht1.Text = getLanguage("_baht");
            this.labelControlBaht2.Text = getLanguage("_baht");
            this.labelControlBaht3.Text = getLanguage("_baht");

            this.labelControlTipMSG.Text = getLanguage("_tip_msg");
        }
        
        public int createRefundTransaction(int invoice_id)
        {
            // Create New Refund Insurance Transaction
            #region Create New Refund Insurance Transaction

            DataTable DTRefund = new DataTable();

            // Declare New DT Column
            DTRefund.Columns.Add("ref_insure_checkin_id", typeof(int));
            DTRefund.Columns.Add("ref_insure_contract_no", typeof(string));
            DTRefund.Columns.Add("ref_insure_prefixname", typeof(string));
            DTRefund.Columns.Add("ref_insure_firstname", typeof(string));
            DTRefund.Columns.Add("ref_insure_lastname", typeof(string));
            DTRefund.Columns.Add("ref_insure_room_id", typeof(int));
            DTRefund.Columns.Add("ref_insure_roomlabel", typeof(string));
            DTRefund.Columns.Add("ref_insure_roomprice", typeof(double));
            DTRefund.Columns.Add("ref_insure_building", typeof(string));
            DTRefund.Columns.Add("ref_insure_floor", typeof(string));
            DTRefund.Columns.Add("ref_insure_roomtype", typeof(string));
            DTRefund.Columns.Add("ref_insure_price", typeof(double));
            DTRefund.Columns.Add("ref_insure_rent_advance", typeof(int));
            DTRefund.Columns.Add("ref_insure_sumrefund", typeof(double));
            DTRefund.Columns.Add("ref_insure_status", typeof(int));
            DTRefund.Columns.Add("ref_insure_company_name", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_logofile", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_address", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_telephone", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_fax", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_email", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_website", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_tax_id", typeof(string));
            DTRefund.Columns.Add("ref_insure_company_vision", typeof(string));
            DTRefund.Columns.Add("ref_invoice_id", typeof(int));
            
            DataTable RefundItem = new DataTable();

            RefundItem.Columns.Add("ref_insure_id", typeof(int));
            RefundItem.Columns.Add("item_id", typeof(int));
            RefundItem.Columns.Add("item_name", typeof(string));
            RefundItem.Columns.Add("item_price_monthly", typeof(double));
            RefundItem.Columns.Add("item_price_daily", typeof(double));
            RefundItem.Columns.Add("item_vat", typeof(string));
            RefundItem.Columns.Add("item_type", typeof(int));
            RefundItem.Columns.Add("item_priceperunit", typeof(double));
            RefundItem.Columns.Add("item_amount", typeof(int));
            RefundItem.Columns.Add("item_sumprice", typeof(double));
            RefundItem.Columns.Add("item_vatprice", typeof(double));
            RefundItem.Columns.Add("item_netprice", typeof(double));
            RefundItem.Columns.Add("item_flag", typeof(string));

           // string RefundNO = "";
            int RefundID = 0;
            
            DataTable DTCheckInInfo = new DataTable();

            DTCheckInInfo = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

            DTRefund.Rows.Add(
                textEditCheckInId.EditValue.To<int>(),
                textEditContractNo.EditValue.ToString(),
                textEditPrefix.EditValue.ToString(),
                textEditTenantName.EditValue.ToString(),
                textEditTenantSurname.EditValue.ToString(),
                textEditRoomId.EditValue.To<int>(),
                textEditRoomLabel.EditValue.ToString(),
                textEditRoomPrice.EditValue.To<double>(),
                lookUpEditBuildingId.Text,
                lookUpEditFloorId.Text,
                lookUpEditRoomTypeId.Text,
                textEditInsurerate.EditValue.To<double>(),
                textEditAdvance.EditValue.To<int>(),
                textEditRefund.EditValue.To<double>(),
                1,
                DTCheckInInfo.Rows[0]["company_name"].ToString(),
                DTCheckInInfo.Rows[0]["company_logo"].ToString(),
                DTCheckInInfo.Rows[0]["company_address"].ToString(),
                DTCheckInInfo.Rows[0]["company_telephone"].ToString(),
                DTCheckInInfo.Rows[0]["company_fax"].ToString(),
                DTCheckInInfo.Rows[0]["company_email"].ToString(),
                DTCheckInInfo.Rows[0]["company_website"].ToString(),
                DTCheckInInfo.Rows[0]["company_tax_id"].ToString(),
                DTCheckInInfo.Rows[0]["company_vision"].ToString(),
                invoice_id
            );

            RefundID = BusinessLogicBridge.DataStore.createRefundInsuranceTransaction(DTRefund);

            if (RefundID > 0)
            {
                DataTable ItemGrid = ((DataTable)gridControlItem.DataSource);

                for (int i = 0; i < ItemGrid.Rows.Count; i++)
                {
                    RefundItem.Rows.Add(RefundID, ItemGrid.Rows[i]["item_id"].To<int>(), ItemGrid.Rows[i]["item_name"].ToString(), ItemGrid.Rows[i]["item_price_monthly"].To<double>(), ItemGrid.Rows[i]["item_price_daily"].To<double>(), ItemGrid.Rows[i]["item_vat"].To<int>(), ItemGrid.Rows[i]["item_type"].To<int>(), ItemGrid.Rows[i]["item_priceperunit"], ItemGrid.Rows[i]["item_amount"], ItemGrid.Rows[i]["item_sumprice"], ItemGrid.Rows[i]["item_vatprice"], ItemGrid.Rows[i]["item_sumprice"].To<double>() + ItemGrid.Rows[i]["item_vatprice"].To<double>(), ItemGrid.Rows[i]["item_flag"].ToString());
                }

                BusinessLogicBridge.DataStore.createRefundInsuranceItem(RefundItem);
            }

            #endregion
            return RefundID;
        }

        void reCalculate()
        {
            double amountofstay = 0;
            double sumprice = 0;
            double price_vat = 0;
            double sumprice_net = 0;

            double cost_netprice = 0;
            double cost_sumprice = 0;
            double cost_vatprice = 0;

            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;

            // 1 = full month
            // 2 = (full month / 30) * amount of stay
            // 3 = assign Price per day * amount of day

            DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

            if (lookUpEditContractType.EditValue.To<int>()==1)
            {
                // daily
                amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditCheckInDate.EditValue.To<DateTime>()).TotalDays;
            }
            else
            {
                // monthly
                if (dateEditInvoiceDueDate.EditValue != null)
                {
                    amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditInvoiceDueDate.EditValue.To<DateTime>()).TotalDays;
                    amountofstay = amountofstay - 1;
                }
                else
                {
                    amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
                }
            }

            amountofstay = Math.Ceiling(amountofstay);

            if (lookUpEditCharge.EditValue.ToString() == "1")
            {
                room_price = textEditRoomPrice.EditValue.To<double>();

                if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() != 1)
                {

                    if (DTDocInfo.Rows[0]["doc_vat_type"].To<int>() == 2)
                    {                        
                        cost_vatprice = (room_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                        cost_netprice = room_price;

                        if (lookUpEditContractType.EditValue.To<int>() == 1) {
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
                    if (lookUpEditContractType.EditValue.To<int>() == 1)
                    {
                        cost_sumprice = room_price * amountofstay;
                    }
                    else {
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

                if (lookUpEditContractType.EditValue.To<int>() == 1)
                {
                    ItemGeneralCost.Rows[0]["item_amount"] = amountofstay;
                    ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                }
            }

            if (lookUpEditCharge.EditValue.ToString() == "2")
            {
                room_price = (textEditRoomPrice.EditValue.To<double>() / 30);

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

            if (lookUpEditCharge.EditValue.ToString() == "3")
            {

                room_price = textEditChargePrice.EditValue.To<double>();

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
                ItemGeneralCost.Rows[0]["item_priceperunit"] = textEditChargePrice.EditValue.To<double>().ToString("N2");
                ItemGeneralCost.Rows[0]["item_sumprice"] = cost_sumprice.ToString("N2");
                ItemGeneralCost.Rows[0]["item_vatprice"] = cost_vatprice.ToString("N2");
                ItemGeneralCost.Rows[0]["item_netprice"] = cost_netprice.ToString("N2");
            }

            DataTable ItemList = ((DataTable)gridControlItem.DataSource);

            if (ItemList.Rows.Count > 0)
            {
                for (int k = 0; k < ItemList.Rows.Count; k++)
                {
                    try
                    {
                        total_sum_price_of_item += ItemList.Rows[k]["item_sumprice"].To<double>();
                        total_vat_price_of_item += ItemList.Rows[k]["item_vatprice"].To<double>();
                        total_net_price_of_item += ItemList.Rows[k]["item_netprice"].To<double>();
                    }
                    catch { }

                }
            }

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


            textEditRefund.EditValue = ((textEditAdvance.EditValue.To<double>() * textEditRoomPrice.EditValue.To<double>())+textEditInsurerate.EditValue.To<double>()) - sumprice_net;


            if (textEditRefund.EditValue.To<double>() > 0)
            {

                bttPrintInvoice.Enabled = false;

                int haveRefund = BusinessLogicBridge.DataStore.getRefundInsuranceByCheckInID(textEditCheckInId.EditValue.To<int>());
                if (haveRefund <= 0)
                {
                    bttPrintInsurance.Enabled = true;
                    bttSave.Enabled = false;
                }
                else {
                    bttSave.Enabled = true;
                }
            }
            else
            {
                bttPrintInsurance.Enabled = false;

                DataTable HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInIDAndStatus(textEditCheckInId.EditValue.To<int>());
                if (HaveInvoice.Rows.Count > 0)
                {
                    if (HaveInvoice.Rows[0]["inv_trans_status"].To<int>() == 2)
                    {
                        bttPrintInvoice.Enabled = true;
                    }
                    else
                    {
                        bttPrintInvoice.Enabled = false;
                    }
                }
                else {
                    bttPrintInvoice.Enabled = true;
                }
            }
        }

        void loadItem()
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


            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(textEditCheckInId.EditValue.To<int>());

            DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CheckInData.Rows[0]["building_id"].To<int>());

            if (InvoiceInfo.Rows.Count == 0)
            {
                
                if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                {
                    // Daily case
                    amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditCheckInDate.EditValue.To<DateTime>()).TotalDays;

                    amountofstay = Math.Ceiling(amountofstay);

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
                        vat_bool = true;
                    }
                    else
                    {
                        cost_sumprice = (amountofstay * room_price);
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        vat_bool = false;
                    }

                    ItemGeneralCost.Rows.Add(1, "ค่าเช่าห้อง", amountofstay, room_price, cost_sumprice, cost_vatprice, cost_netprice, vat_bool);

                    #region Electricity Calculation
                    if (CheckInData.Rows[0]["roomtype_daily_electric_checked"].To<bool>() == true)
                    {
                        // ขั้นต่ำ (กรณีที่มีการใส่ราคาขั้นต่ำ ที่ Roomtype ถ้าเป็น 0.00 ถือว่าไม่ได้คิดขั้นต่ำ)
                        if (CheckInData.Rows[0]["roomtype_daily_electric_priceminrate"].To<double>() <= 0)
                        {
                            // Calculate type [Price Per Unit] use roomtype_daily_electric_priceperunit
                            // - แบบราคาต่อหน่วย      
                            //  จำนวนหน่วย - End Value(วันที่ Check out) - Begin Value (วันที่ CHeck in)
                            //  จำนวนเงินต่อหน่วย = ราคาต่อหน่วย (Set อยู่ที่ Room type screen)
                            //  จำนวนเงินรวม = จำนวนหน่วย * ราคาต่อหน่วย

                            e_unit = EUnit;
                            e_price_per_unit = CheckInData.Rows[0]["roomtype_daily_electric_priceperunit"].To<double>();
                            e_price_sum = e_price_per_unit * e_unit;

                        }
                        else
                        {
                            // Calculate type [Have Min Rate] use roomtype_daily_electric_priceminrate

                            // การคิดขั้นต่ำ จะต้องคำนวณแบบราคาต่อหน่วย แล้วนำ จำนวนเงินรวม เปรียบเทียบกับ ราคาขั้นต่ำ
                            // a.จำนวนเงินรวม(จากราคาต่อหน่วย) >= ราคาขั้นต่ำ -> กรณีนี้ให้คิดแบบราคาต่อหน่วยเลย
                            // b.จำนวนเงินรวม(จากราคาต่อหน่วย)< ราคาขั้นต่ำ -> กรณนี้ กำหนดให้
                            // - จำนวนหน่วย =1
                            // - ราคาต่อหน่วย= จำนวนราคาขั้นต่ำ
                            // - จำนวนเงิน= จำนวนราคาขั้นต่ำ

                            e_unit = EUnit;
                            e_price_per_unit = CheckInData.Rows[0]["roomtype_daily_electric_priceperunit"].To<double>();
                            e_price_sum = e_price_per_unit * e_unit;

                            if (e_price_sum >= CheckInData.Rows[0]["roomtype_daily_electric_priceminrate"].To<double>())
                            {
                                // More than or equal Price Per Unit
                                e_price_sum = e_price_per_unit * e_unit;
                            }
                            else
                            {
                                // Less than Min Rate Price
                                e_unit = 1;
                                e_price_per_unit = CheckInData.Rows[0]["roomtype_daily_electric_priceminrate"].To<double>();
                                e_price_sum = e_price_per_unit * e_unit;
                            }
                        }
                    }
                    else if (CheckInData.Rows[0]["roomtype_daily_electric_lumpchecked"].To<bool>() == true)
                    {
                        // roomtype_daily_electric_lumpchecked   use roomtype_daily_electric_lumpprice 
                        //- เหมาจ่าย 
                        // ถ้ามีการเลือก เหมาจ่าย Check box ให้คิดแบบเหมาจ่าย
                        // จำนวน = จำนวนวันที่เช่า * ราคาเหมาจ่ายs
                        // จำนวนเงินต่อหน่วย = ราคาเหมาจ่าย(จาก Roomtype)
                        // จำนวนเงินรวม = จำนวน(วัน) * จำนวนเงินต่อหน่วย

                        e_unit = amountofstay * CheckInData.Rows[0]["roomtype_daily_electric_lumpprice"].To<double>();
                        e_price_per_unit = CheckInData.Rows[0]["roomtype_daily_electric_lumpprice"].To<double>();
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
                    if (CheckInData.Rows[0]["roomtype_daily_water_checked"].To<bool>() == true)
                    {
                        // ขั้นต่ำ (กรณีที่มีการใส่ราคาขั้นต่ำ ที่ Roomtype ถ้าเป็น 0.00 ถือว่าไม่ได้คิดขั้นต่ำ)
                        if (CheckInData.Rows[0]["roomtype_daily_water_priceminrate"].To<double>() <= 0)
                        {
                            // Calculate type [Price Per Unit] use roomtype_daily_water_priceperunit
                            // - แบบราคาต่อหน่วย      
                            //  จำนวนหน่วย - End Value(วันที่ Check out) - Begin Value (วันที่ CHeck in)
                            //  จำนวนเงินต่อหน่วย = ราคาต่อหน่วย (Set อยู่ที่ Room type screen)
                            //  จำนวนเงินรวม = จำนวนหน่วย * ราคาต่อหน่วย

                            w_unit = WUnit;
                            w_price_per_unit = CheckInData.Rows[0]["roomtype_daily_water_priceperunit"].To<double>();
                            w_price_sum = w_price_per_unit * w_unit;

                        }
                        else
                        {
                            // Calculate type [Have Min Rate] use roomtype_daily_water_priceminrate

                            // การคิดขั้นต่ำ จะต้องคำนวณแบบราคาต่อหน่วย แล้วนำ จำนวนเงินรวม เปรียบเทียบกับ ราคาขั้นต่ำ
                            // a.จำนวนเงินรวม(จากราคาต่อหน่วย) >= ราคาขั้นต่ำ -> กรณีนี้ให้คิดแบบราคาต่อหน่วยเลย
                            // b.จำนวนเงินรวม(จากราคาต่อหน่วย)< ราคาขั้นต่ำ -> กรณนี้ กำหนดให้
                            // - จำนวนหน่วย =1
                            // - ราคาต่อหน่วย= จำนวนราคาขั้นต่ำ
                            // - จำนวนเงิน= จำนวนราคาขั้นต่ำ

                            w_unit = WUnit;
                            w_price_per_unit = CheckInData.Rows[0]["roomtype_daily_water_priceperunit"].To<double>();
                            w_price_sum = w_price_per_unit * w_unit;

                            if (w_price_sum >= CheckInData.Rows[0]["roomtype_daily_water_priceminrate"].To<double>())
                            {
                                // More than or equal Price Per Unit
                                w_price_sum = w_price_per_unit * w_unit;
                            }
                            else
                            {
                                // Less than Min Rate Price
                                w_unit = 1;
                                w_price_per_unit = CheckInData.Rows[0]["roomtype_daily_water_priceminrate"].To<double>();
                                w_price_sum = w_price_per_unit * w_unit;
                            }
                        }
                    }
                    else if (CheckInData.Rows[0]["roomtype_daily_water_lumpchecked"].To<bool>() == true)
                    {
                        // roomtype_daily_electric_lumpchecked   use roomtype_daily_water_lumpprice 
                        //- เหมาจ่าย 
                        // ถ้ามีการเลือก เหมาจ่าย Check box ให้คิดแบบเหมาจ่าย
                        // จำนวน = จำนวนวันที่เช่า * ราคาเหมาจ่าย
                        // จำนวนเงินต่อหน่วย = ราคาเหมาจ่าย(จาก Roomtype)
                        // จำนวนเงินรวม = จำนวน(วัน) * จำนวนเงินต่อหน่วย

                        w_unit = amountofstay * CheckInData.Rows[0]["roomtype_daily_water_lumpprice"].To<double>();
                        w_price_per_unit = CheckInData.Rows[0]["roomtype_daily_water_lumpprice"].To<double>();
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
                }
                else
                {
                    // monthly case

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
                    if (dateEditInvoiceDueDate.EditValue != null)
                    {
                        amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditInvoiceDueDate.EditValue.To<DateTime>()).TotalDays;
                        amountofstay = amountofstay - 1;
                    }
                    else
                    {
                        amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
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
                }
            }
            else
            {
                #region room cost
                    amountofstay    = InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>();
                    room_price      = InvoiceInfo.Rows[0]["inv_trans_roomprice"].To<double>();

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 1)
                        {
                            if (amountofstay > 0)
                            {
                                cost_vatprice = 0.00;
                                cost_netprice = room_price * amountofstay;
                            }
                            else
                            {
                                cost_vatprice = 0.00;
                                cost_netprice = room_price;
                                amountofstay = 1;
                            }

                            cost_sumprice = cost_netprice - cost_vatprice;
                        }                        
                    
                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 2)
                        {
                            if (amountofstay > 0)
                            {
                                cost_vatprice = ((room_price * amountofstay) * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                cost_netprice = room_price * amountofstay;
                            }
                            else
                            {
                                cost_vatprice = (room_price * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                                cost_netprice = room_price;
                                amountofstay = 1;
                            }

                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 3)
                        {
                            if (amountofstay > 0)
                            {
                                cost_sumprice = (room_price * amountofstay);
                            }
                            else
                            {
                                cost_sumprice = room_price;
                                amountofstay = 1;
                            }

                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                   


                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() != 1)
                            vat_bool = true;
                        else
                            vat_bool = false;

                    ItemGeneralCost.Rows.Add(1, "ค่าเช่าห้อง", amountofstay, room_price, cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
                #endregion

                #region Electricity Cost

                    if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() != 1)
                    {

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 2)
                        {
                            cost_vatprice = (InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>() * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>();
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 3)
                        {
                            cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>();
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                    }
                    else
                    {
                        cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>();
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                        
                    }

                    ItemGeneralCost.Rows.Add(2, "ค่าไฟ", InvoiceInfo.Rows[0]["inv_trans_emeter_unit"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"].To<double>(), cost_sumprice, cost_vatprice, cost_netprice, vat_bool);

                #endregion

                #region Water Cost
                
                    if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() != 1)
                    {

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 2)
                        {
                            cost_vatprice = (InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>() * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>();
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 3)
                        {
                            cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>();
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                    }
                    else
                    {
                        cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>();
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                    }

                    ItemGeneralCost.Rows.Add(3, "ค่าน้ำ", InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"].To<double>(), cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
                #endregion

                #region Phone Cost
                    if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() != 1)
                    {
                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 2)
                        {
                            cost_vatprice = (InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>() * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>();
                            cost_sumprice = cost_netprice - cost_vatprice;
                        }

                        if (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>() == 3)
                        {
                            cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>();
                            cost_vatprice = (cost_sumprice * DTDocInfo.Rows[0]["doc_vat"].To<double>()) / 100;
                            cost_netprice = cost_sumprice + cost_vatprice;
                        }
                    }
                    else
                    {
                        cost_sumprice = InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>();
                        cost_vatprice = 0.00;
                        cost_netprice = cost_sumprice;
                    }

                    ItemGeneralCost.Rows.Add(4, "ค่าโทรศัพท์", "-", "-", cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
                #endregion

            }

            gridControlGeneralCost.DataSource = ItemGeneralCost;

            initItem();
        }

        #region Setup Dropdown
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
            void initDropDownContractType()
            {
                DataTable ContractTypeTbl = new DataTable();
                ContractTypeTbl.Columns.Add("contracttype_id", typeof(int));
                ContractTypeTbl.Columns.Add("contracttype_label", typeof(string));
                ContractTypeTbl.Rows.Add("1", getLanguage("_daily"));
                ContractTypeTbl.Rows.Add("3", getLanguage("_monthly"));
                lookUpEditContractType.Properties.DataSource = ContractTypeTbl;
                lookUpEditContractType.Properties.DisplayMember = "contracttype_label";
                lookUpEditContractType.Properties.ValueMember = "contracttype_id";
                lookUpEditContractType.Properties.NullText = "[" + getLanguage("_rental_type") + "]";
                lookUpEditContractType.EditValue = 1;
            }
            void initDropDownCharge()
            {
                DataTable chargeTmp = new DataTable();
                chargeTmp.Columns.Add("charge_id", typeof(int));
                chargeTmp.Columns.Add("charge_label", typeof(string));
                chargeTmp.Rows.Add("1", getLanguage("_full_month"));
                chargeTmp.Rows.Add("2", getLanguage("_left_day"));
                chargeTmp.Rows.Add("3", getLanguage("_per_day"));
                lookUpEditCharge.Properties.DataSource = chargeTmp;
                lookUpEditCharge.Properties.DisplayMember = "charge_label";
                lookUpEditCharge.Properties.ValueMember = "charge_id";
                lookUpEditCharge.Properties.NullText = "[" + getLanguage("_room_charge") + "]";
                //
                lookUpEditCharge.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("charge_label", 0, getLanguage("_room_charge")));
                //
            }
            void initRoom()
            {
                DataTable RoomTbl = BusinessLogicBridge.DataStore.RoomCheckOut_get();
                gridControlRoom.DataSource = RoomTbl;
                //
                if (RoomTbl.Rows.Count == 0)
                {
                    enableControl(false);
                    bttEdit.Enabled             = false;
                    bttPrintInsurance.Enabled   = false;
                    bttPrintInvoice.Enabled     = false;
                }
            }
 
        #endregion

        #region Action Extra

        void initItem()
            {
                
                DataTable ItemTable = new DataTable();

                DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(lookUpEditBuildingId.EditValue.To<int>());

                DataTable InvoiceInfoChecked = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInID(textEditCheckInId.EditValue.To<int>());

                if (InvoiceInfoChecked.Rows.Count > 0)
                {
                    ItemTable = BusinessLogicBridge.DataStore.RoomCheckOut_getInvoiceItemsByInvoiceId(InvoiceInfoChecked.Rows[0]["inv_trans_id"].To<int>());
                }
                else
                {
                    ItemTable = BusinessLogicBridge.DataStore.getItemByRoomID(textEditRoomId.EditValue.To<int>());
                }                    
                    
                    ItemTable.Columns.Add("order", typeof(int));
                    if (ItemTable.Columns.Contains("item_amount")==false)
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
                            if (ItemForDelete != null)
                            {
                                ItemTable = ItemForDelete;
                            }
                            else
                            {
                                ItemTable.Merge(ItemTableTemp);
                            }
                        }
                        catch { }
                    }
                    //

                    for (int i = 0; i < ItemTable.Rows.Count; i++)
                    {

                        //if (ItemTable.Rows[i]["item_type"].To<int>() == 1)
                        //{

                            ItemTable.Rows[i]["order"] = counterItem;

                            if (ItemTable.Columns.Contains("room_id") == true)
                            ItemTable.Rows[i]["room_id"] = textEditRoomId.EditValue.To<int>();

                            if (ItemTable.Rows[i]["item_id"].To<int>() == 0)
                            {
                                ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_priceperunit"];
                            }
                            else
                            {
                                if (ItemTable.Rows[i]["item_amount"].ToString()=="")
                                    ItemTable.Rows[i]["item_amount"] = 1;
                                

                                if (lookUpEditContractType.EditValue.To<int>() == 1)
                                {
                                    // daily
                                    ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_price_daily"];
                                }
                                else
                                {
                                    if (ItemTable.Rows[i]["item_price_monthly"].ToString() != "")
                                    {
                                        // Monthly
                                        ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_price_monthly"];
                                    }
                                }
                            }

                            // Sum Price will equal price per unit alway
                            ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_amount"].To<double>() * ItemTable.Rows[i]["item_priceperunit"].To<double>();

                            if (ItemTable.Rows[i]["item_vat"].ToString() != "1")
                            {
                                ItemTable.Rows[i]["item_vat_bool"] = true;
                                // include vat
                                if (ItemTable.Rows[i]["item_vat"].ToString() == "2"){

                                    ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                                    //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                                }
                                // exclude vat
                                if (ItemTable.Rows[i]["item_vat"].ToString() == "3"){
                                    ItemTable.Rows[i]["item_vatprice"] = (DTDocInfo.Rows[0]["doc_vat"].To<double>() * ItemTable.Rows[i]["item_sumprice"].To<double>()) / 100;
                                    //ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_sumprice"].To<double>() - ItemTable.Rows[i]["item_vatprice"].To<double>();
                                }
                            }
                            else
                            {
                                ItemTable.Rows[i]["item_vat_bool"] = false;
                                ItemTable.Rows[i]["item_vatprice"] = 0;
                            }

                            if (ItemTable.Rows[i]["item_id"].To<int>() != 0)
                            {
                                bttRemoveItem.Enabled = false;
                            }
                            else
                            {
                                bttRemoveItem.Enabled = true;
                            }

                            counterItem++;
                            CheckOutItemTable.ImportRow(ItemTable.Rows[i]);
                        //}else if (lookUpEditContractType.EditValue.To<int>() == 1) {
                            
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

                        //    if (ItemTable.Rows[i]["item_id"].To<int>() != 0)
                        //    {
                        //        bttRemoveItem.Enabled = false;
                        //    }
                        //    else
                        //    {
                        //        bttRemoveItem.Enabled = true;
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
                    //ItemTableTemp.Clear();

                    dataItemsForCheck = CheckOutItemTable;

                //
            }

        void changeRow(){
                int[] rowIndex = gridViewRoom.GetSelectedRows();
                if (rowIndex.Length != 0)
                {
                    ItemTableTemp = null;
                    ItemForDelete = null;

                    DataRow CurrentRow = gridViewRoom.GetDataRow(rowIndex[0]);

                    double sumprice = 0;
                    double price_vat = 0;
                    double sumprice_net = 0;
                    
                    double total_sum_price_of_item = 0;
                    double total_vat_price_of_item = 0;
                    double total_net_price_of_item = 0;

                    if (CurrentRow == null)
                    {
                        CurrentRow = gridViewRoom.GetDataRow(0);
                    }

                    int CheckinID = CurrentRow["check_in_id"].To<int>();
                    int AmountUnpaid = 0;
                    int AmountUnRefund = 0;

                    DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(CheckinID);

                    AmountUnpaid = BusinessLogicBridge.DataStore.getInvoiceUnpaidByRoomID(CheckInData.Rows[0]["room_id"].To<int>());
                    
                    if (AmountUnpaid > 0) {
                        bttPrintInvoice.Enabled = false;
                    }

                    AmountUnRefund = BusinessLogicBridge.DataStore.getRefundInsuranceByCheckInID(CheckInData.Rows[0]["check_in_id"].To<int>());

                    if (AmountUnRefund > 0)
                    {
                        bttPrintInsurance.Enabled = false;
                    }

                    textEditMinimum.EditValue = CheckInData.Rows[0]["check_in_minimum_monthly"].ToString();

                    dateEditCheckInDate.EditValue = CheckInData.Rows[0]["check_in_date"].To<DateTime>();

                    textEditRoomId.EditValue = CheckInData.Rows[0]["room_id"].ToString();
                    textEditTenantId.EditValue = CheckInData.Rows[0]["current_tenant_id"].ToString();
                    textEditCheckInId.EditValue = CheckInData.Rows[0]["check_in_id"].ToString();
                    textEditRoomLabel.EditValue = CheckInData.Rows[0]["room_label"].ToString();
                    //textEditRoomStatus.EditValue = CurrentRow["room_status"].ToString();
                    textEditRoomCode.EditValue = CheckInData.Rows[0]["room_label"].ToString();
                    //
                    textEditPrefix.EditValue = CurrentRow["prefix_" + current_lang + "_label"].ToString();
                    textEditTenantName.EditValue = CheckInData.Rows[0]["tenant_name"].ToString();
                    textEditTenantSurname.EditValue = CheckInData.Rows[0]["tenant_surname"].ToString();

                    textEditContractNo.EditValue = CheckInData.Rows[0]["check_in_label"].ToString();
                    textEditRefund.EditValue = 0;
                    //
                    lookUpEditBuildingId.EditValue = CheckInData.Rows[0]["building_id"];
                    lookUpEditFloorId.EditValue = CheckInData.Rows[0]["floor_id"];
                    lookUpEditContractType.EditValue = CheckInData.Rows[0]["check_in_contracttype"];
                    lookUpEditRoomTypeId.EditValue = CheckInData.Rows[0]["roomtype_id"];

                    // Previous Value
                    textEditEMeterPreviousUnit.EditValue = CheckInData.Rows[0]["previous_energy_billing"];
                    textEditWMeterPreviousUnit.EditValue = CheckInData.Rows[0]["wprevious_energy_billing"];
                    
                    // Previous Date
                    dateEditEMeterPreviousDate.EditValue = CheckInData.Rows[0]["previous_date_billing"];
                    dateEditWMeterPreviousDate.EditValue = CheckInData.Rows[0]["wprevious_date_billing"];
                    dateEditPhoneStart.EditValue = (CheckInData.Rows[0]["pstart_date"].ToString() == "") ? DateTime.Now : CheckInData.Rows[0]["pstart_date"];

                    // Present Value

                    textEditEMeterPresent.EditValue = CheckInData.Rows[0]["e_end_energy"].To<double>();
                    textEditWMeterPresent.EditValue = CheckInData.Rows[0]["w_end_energy"].To<double>();

                    // Present Date
                    dateEditEMeterPresentDate.EditValue = (CheckInData.Rows[0]["e_end_date"].ToString() == "") ? DateTime.Now : CheckInData.Rows[0]["e_end_date"].To<DateTime>();
                    dateEditWMeterPresentDate.EditValue = (CheckInData.Rows[0]["w_end_date"].ToString() == "") ? DateTime.Now : CheckInData.Rows[0]["w_end_date"].To<DateTime>();
                    dateEditPhoneEnd.EditValue = (CheckInData.Rows[0]["pend_date"].ToString() == "") ? DateTime.Now : CheckInData.Rows[0]["pend_date"];

                    // เงินประกัน และ ค่าเช่าล่วงหน้า

                    if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 1)
                    {
                        textEditRoomPrice.EditValue     = CheckInData.Rows[0]["roomtype_daily_roomrate_price"].To<double>();
                        textEditInsurerate.EditValue    = 0;
                        textEditAdvance.EditValue       = 0;

                        lookUpEditCharge.EditValue      = 1;
                        lookUpEditCharge.Enabled        = false;
                    }
                    else
                    {
                        textEditRoomPrice.EditValue     = CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>();
                        textEditInsurerate.EditValue    = CheckInData.Rows[0]["roomtype_month_insure_price"].To<double>();
                        textEditAdvance.EditValue       = CheckInData.Rows[0]["roomtype_month_advance_amount"].To<double>();
                        lookUpEditCharge.Enabled        = true;
                        lookUpEditCharge.EditValue      = 1;

                    }

                    if (CurrentRow["leave_date"].ToString() == "")
                    {
                        dateEditLeaveDate.EditValue = DateTime.Now;
                    }
                    else
                    {
                        dateEditLeaveDate.EditValue = CurrentRow["leave_date"].To<DateTime>();
                    }

                    room_price = textEditRoomPrice.EditValue.To<double>();

                    // ต้องตรวจสอบมั้ยว่ามีค่าของวัน checkout มั้ยใน transaction
                    EUnit = (CheckInData.Rows[0]["e_end_energy"].To<double>() - CheckInData.Rows[0]["previous_energy_billing"].To<double>());
                    WUnit = (CheckInData.Rows[0]["w_end_energy"].To<double>() - CheckInData.Rows[0]["wprevious_energy_billing"].To<double>());
                    PUnit = CheckInData.Rows[0]["duration"].To<double>();

                    phoneprice_per_unit = PUnit / CheckInData.Rows[0]["amount"].To<double>();

                    if (phoneprice_per_unit.ToString() == "NaN")
                        phoneprice_per_unit = 0.0;

                    emeter_price = EUnit * CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>();
                    wmeter_price = WUnit * CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>();
                    pmeter_price = CheckInData.Rows[0]["amount"].To<double>(); //PUnit * phoneprice_per_unit;

                    loadItem();            

                    #region sumprice calculate

                    DataTable ItemList = ((DataTable)gridControlItem.DataSource);

                    if (ItemList.Rows.Count > 0)
                    {
                        for (int k = 0; k < ItemList.Rows.Count; k++)
                        {
                            try
                            {
                                total_sum_price_of_item += ItemList.Rows[k]["item_sumprice"].To<double>();
                                total_vat_price_of_item += ItemList.Rows[k]["item_vatprice"].To<double>();
                                total_net_price_of_item += ItemList.Rows[k]["item_netprice"].To<double>();
                            }
                            catch { }
                        }
                    }

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

                    #endregion

                    textEditPhonePrice.EditValue = pmeter_price;

                    bttEdit.Enabled = true;

                    //
                    enableControl(false);

                    bttPrintInsurance.Enabled = false;
                    bttPrintInvoice.Enabled = false;

                    if (CheckInData.Rows[0]["check_in_contracttype"].To<int>() == 3)
                    {
                        DataTable HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInIDAndStatus(textEditCheckInId.EditValue.To<int>());

                        if (HaveInvoice.Rows.Count > 0)
                        {
                            if (HaveInvoice.Rows[0]["inv_trans_status"].To<int>() == 2)
                            {   // Cancel Case
                                textEditRefund.EditValue = -sumprice_net;
                            }
                            else
                            {
                                // Paid Case or Un-Paid
                                textEditRefund.EditValue = ((textEditAdvance.EditValue.To<double>() * textEditRoomPrice.EditValue.To<double>()) + CheckInData.Rows[0]["roomtype_month_insure_price"].To<double>()) - HaveInvoice.Rows[0]["inv_trans_sumprice_net"].To<double>();
                            }
                        }
                        else
                        {
                            textEditRefund.EditValue = ((textEditAdvance.EditValue.To<double>() * textEditRoomPrice.EditValue.To<double>()) + CheckInData.Rows[0]["roomtype_month_insure_price"].To<double>()) - sumprice_net;
                        }
                    }
                    else {

                        textEditRefund.EditValue = -sumprice_net; 
                    }
                }
        }
            
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e){

            changeRow();
              
            }
        
        #endregion

        #region Button Event
        private void bttAddItem_Click(object sender, EventArgs e)
        {
            if (ItemTableTemp.Columns.Contains("inv_trans_id") == true) ItemTableTemp.Columns.Remove("inv_trans_id");

            if (ItemTableTemp.Columns.Contains("room_id") == true) ItemTableTemp.Columns.Remove("room_id");
            if (ItemTableTemp.Columns.Contains("date_created") == true) ItemTableTemp.Columns.Remove("date_created");
            if (ItemTableTemp.Columns.Contains("item_id1") == true) ItemTableTemp.Columns.Remove("item_id1");
            if (ItemTableTemp.Columns.Contains("item_price_monthly") == true) ItemTableTemp.Columns.Remove("item_price_monthly");
            if (ItemTableTemp.Columns.Contains("item_detail") == true) ItemTableTemp.Columns.Remove("item_detail");
            if (ItemTableTemp.Columns.Contains("item_datecreate") == true) ItemTableTemp.Columns.Remove("item_datecreate");

            ItemTableTemp = utilClass.showPopAddCheckOutExpense(this, ItemTableTemp);
            
            ItemForDelete = null;
            //
            initItem();
            reCalculate();
        }
        private void bttRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    // Delete Item

                    int[] rowIndex = gridViewItemList.GetSelectedRows();
                    int selectedRow = gridViewItemList.GetRowHandle(rowIndex[0]);

                    ItemForDelete = (DataTable)gridControlItem.DataSource;

                    DataRow[] foundRow = ItemTableTemp.Select("item_name='" + ItemForDelete.Rows[selectedRow]["item_name"].ToString() + "'");

                    if (foundRow.Length > 0)
                    {
                        foreach (DataRow row in foundRow)
                        {
                            ItemTableTemp.Rows.Remove(row);
                        }
                        ItemTableTemp.AcceptChanges();
                    }

                    ItemForDelete.Rows[selectedRow].Delete();

                    ItemForDelete.AcceptChanges();

                    if (ItemForDelete.Rows.Count == 0)
                    {
                        ItemTableTemp.Clear();
                    }


                    initItem();

                    reCalculate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region Button Action

        int createInvoice() {
            // Create Case
            #region Create New Invoice
            #region Initial Param
            DataTable GeneralInfo = new DataTable();
            DataTable CheckInData = new DataTable();
            DataTable DTInvoice = new DataTable();
            DataTable DTDocInfo = new DataTable();
            DataTable DTCheckInItem = new DataTable();

            string InvoiceNO = "";
            int inv_trans_id = 0;
            double EUnit = 0;
            double WUnit = 0;
            double PUnit = 0;

            double emeter_price = 0;
            double wmeter_price = 0;
            double pmeter_price = 0;


            double cost_netprice = 0;
            double cost_sumprice = 0;
            double cost_vatprice = 0;

            double phoneprice_per_unit = 0;

            double room_price = 0;
            double amountofstay = 0;

            double sumprice = 0;
            double price_vat = 0;
            double sumprice_net = 0;

            int item_id = 0;
            double item_sumprice = 0;
            double item_vatprice = 0;
            double item_netprice = 0;
            double item_priceperunit = 0;
            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;
            #endregion

            CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(textEditCheckInId.EditValue.To<int>());

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

                DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CheckInData.Rows[0]["building_id"].To<int>());

                GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();

                int doc_id = DTDocInfo.Rows[0]["doc_config_id"].To<int>();

                bool flagInvoiceChangeStatus = BusinessLogicBridge.DataStore.CheckInvoicePrefixChanged(doc_id);

                if (flagInvoiceChangeStatus == true)
                {
                    if (DTDocInfo.Rows[0]["doc_saperate_invoice"].To<int>() == 1)
                    {
                        if (lookUpEditContractType.EditValue.To<int>() == 3)
                        {
                            InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                        }
                        else
                        {
                            InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "D" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                        }
                    }
                    else {
                        InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                    }
                }
                else
                {
                    if (DTDocInfo.Rows[0]["doc_saperate_invoice"].To<int>() == 1)
                    {
                        if (lookUpEditContractType.EditValue.To<int>() == 3)
                        {
                            InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                        }
                        else
                        {
                            InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + "D" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                        }
                    }
                    else
                    {
                        InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');
                    }
                }
                //InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');

                EUnit = (CheckInData.Rows[0]["e_end_energy"].To<double>() - CheckInData.Rows[0]["previous_energy_billing"].To<double>());
                WUnit = (CheckInData.Rows[0]["w_end_energy"].To<double>() - CheckInData.Rows[0]["wprevious_energy_billing"].To<double>());
                PUnit = CheckInData.Rows[0]["duration"].To<double>();

                phoneprice_per_unit = PUnit / CheckInData.Rows[0]["amount"].To<double>();

                if (phoneprice_per_unit.ToString() == "NaN")
                    phoneprice_per_unit = 0.0;

                if (lookUpEditContractType.EditValue.To<int>() == 1)
                {
                    // daily
                    amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditCheckInDate.EditValue.To<DateTime>()).TotalDays;
                }
                else
                {
                    // monthly
                    if (dateEditInvoiceDueDate.EditValue != null)
                    {
                        amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - dateEditInvoiceDueDate.EditValue.To<DateTime>()).TotalDays;
                        amountofstay = amountofstay - 1;
                    }
                    else
                    {
                        amountofstay = (dateEditLeaveDate.EditValue.To<DateTime>() - CheckInData.Rows[0]["check_in_date"].To<DateTime>()).TotalDays;
                    }
                }

                amountofstay = Math.Ceiling(amountofstay);

                room_price = ItemGeneralCost.Rows[0]["item_priceperunit"].To<double>();

                if (lookUpEditCharge.EditValue.ToString() == "2")
                {
                    room_price = (CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>() / 30);
                }

                if (lookUpEditCharge.EditValue.ToString() == "3")
                {
                    room_price = textEditChargePrice.EditValue.To<double>();
                }

                emeter_price = ItemGeneralCost.Rows[1]["item_netprice"].To<double>();
                wmeter_price = ItemGeneralCost.Rows[2]["item_netprice"].To<double>();
                pmeter_price = ItemGeneralCost.Rows[3]["item_netprice"].To<double>(); //PUnit * phoneprice_per_unit;

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

                if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).To<int>() < (GeneralInfo.Rows[0]["due_date"].To<int>()))
                {
                    GeneralInfo.Rows[0]["due_date"] = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).To<int>();
                }

                DateTime dtDue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GeneralInfo.Rows[0]["due_date"].To<int>(), 0, 0, 0, 0);
                DateTime dtFix = new DateTime(DateTime.Now.Year, (DateTime.Now.Month + 1).To<int>(), GeneralInfo.Rows[0]["payment_date"].To<int>());

                DataTable DTPrefixTenant = BusinessLogicBridge.DataStore.getPrefixByID(CheckInData.Rows[0]["tenant_prefix_id"].To<int>());

                string labelprefix = "prefix_" + current_lang + "_label";

                string prefixfull = DTPrefixTenant.Rows[0][labelprefix].ToString();

                DTInvoice.Rows.Add(InvoiceNO, dtDue, dtFix, DateTime.Now, 0, prefixfull + "||" + CheckInData.Rows[0]["tenant_name"].ToString() + "||" + CheckInData.Rows[0]["tenant_surname"].ToString(), CheckInData.Rows[0]["tenant_address"].ToString(), CheckInData.Rows[0]["room_label"].ToString(), CheckInData.Rows[0]["building_label"].ToString(), CheckInData.Rows[0]["floor_code"].ToString(), CheckInData.Rows[0]["roomtype_label"].ToString(), dateEditEMeterPreviousDate.EditValue, textEditEMeterPreviousUnit.EditValue.ToString(), CheckInData.Rows[0]["e_end_date"], CheckInData.Rows[0]["e_end_energy"].ToString(), EUnit, CheckInData.Rows[0]["roomtype_month_electric_priceperunit"].To<double>(), emeter_price, dateEditWMeterPreviousDate.EditValue, textEditWMeterPreviousUnit.EditValue.ToString(), CheckInData.Rows[0]["w_end_date"], CheckInData.Rows[0]["w_end_energy"].ToString(), WUnit, CheckInData.Rows[0]["roomtype_month_water_priceperunit"].To<double>(), wmeter_price, dateEditPhoneStart.EditValue, dateEditPhoneEnd.EditValue, PUnit, phoneprice_per_unit, pmeter_price, sumprice, price_vat, sumprice_net, 0, CheckInData.Rows[0]["room_id"].To<int>(), CheckInData.Rows[0]["company_name"].ToString(), CheckInData.Rows[0]["company_logo"].ToString(), CheckInData.Rows[0]["company_address"].ToString(), CheckInData.Rows[0]["company_telephone"].ToString(), CheckInData.Rows[0]["company_fax"].ToString(), CheckInData.Rows[0]["company_email"].ToString(), CheckInData.Rows[0]["company_website"].ToString(), CheckInData.Rows[0]["company_tax_id"].ToString(), CheckInData.Rows[0]["company_vision"].ToString(), DTDocInfo.Rows[0]["doc_header_invoice"].ToString(), DTDocInfo.Rows[0]["doc_footer_invoice"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice1"].ToString(), DTDocInfo.Rows[0]["doc_under_invoice2"].ToString(), DTDocInfo.Rows[0]["doc_dateformat"].To<int>(), DTDocInfo.Rows[0]["doc_logo_position"].To<int>(), room_price, DTDocInfo.Rows[0]["doc_vat_type"].To<int>(), CheckInData.Rows[0]["check_in_id"].To<int>(), CheckInData.Rows[0]["check_in_contracttype"].To<int>(), amountofstay, DTDocInfo.Rows[0]["doc_vat"].To<decimal>());
                inv_trans_id = BusinessLogicBridge.DataStore.createInvoiceTransaction(DTInvoice);
                DTInvoice.Clear();

                double price_of_daily = 0;
                double price_of_monthly = 0;
                
                if (inv_trans_id > 0)
                {
                    BusinessLogicBridge.DataStore.insert_payment_flag(textEditRoomId.EditValue.To<int>());

                    DTCheckInItem = ((DataTable)gridControlItem.DataSource); //BusinessLogicBridge.DataStore.getItemsByCheckInID(textEditCheckInId.EditValue.To<int>());

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
                                    if (lookUpEditContractType.EditValue.To<int>() == 1)
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
                            else { 
                                // one time

                               // if (lookUpEditContractType.EditValue.To<int>() == 1)
                                //{
                                    // daily

                                    if (DTCheckInItem.Rows[k]["item_id"].To<int>() == 0)
                                    {
                                        item_priceperunit = DTCheckInItem.Rows[k]["item_priceperunit"].To<double>();
                                        price_of_daily = item_priceperunit;
                                        price_of_monthly = item_priceperunit;
                                    }

                                    item_priceperunit = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                    price_of_daily = DTCheckInItem.Rows[k]["item_price_daily"].To<double>();
                                    
                                    if (DTCheckInItem.Rows[k]["item_price_monthly"].ToString()=="")
                                        price_of_monthly = DTCheckInItem.Rows[k]["item_price_monthly"].To<double>();
                                    else
                                        price_of_monthly = DTCheckInItem.Rows[k]["item_priceperunit"].To<double>();

                                    item_sumprice = DTCheckInItem.Rows[k]["item_sumprice"].To<double>();
                                    item_vatprice = DTCheckInItem.Rows[k]["item_vatprice"].To<double>();
                                    item_netprice = item_sumprice + item_vatprice;
                                //}
                            }
                            total_sum_price_of_item += item_sumprice;
                            total_vat_price_of_item += item_vatprice;
                            total_net_price_of_item += item_netprice;
                            ItemTable.Rows.Add(inv_trans_id, item_id, DTCheckInItem.Rows[k]["item_name"].ToString(), price_of_monthly, price_of_daily, DTCheckInItem.Rows[k]["item_vat"].To<int>(), DTCheckInItem.Rows[k]["item_type"].To<int>(), item_priceperunit, 1, item_sumprice, item_vatprice, item_netprice, DTCheckInItem.Rows[k]["item_flag"].ToString());
                        }
                        BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);
                    }
                } // End Created Invoice ID

                // Update Price On invoice_transaction Table Here ...

                // sum price

                double sumGeneralCost = 0;                
                double vatSumGeneralCost = 0;
                double netGeneralCost = 0;

                for (int cost = 0; cost < ItemGeneralCost.Rows.Count; cost++)
                {
                    sumGeneralCost      += ItemGeneralCost.Rows[cost]["item_sumprice"].To<double>();
                    vatSumGeneralCost   += ItemGeneralCost.Rows[cost]["item_vatprice"].To<double>();
                    netGeneralCost      += ItemGeneralCost.Rows[cost]["item_netprice"].To<double>();
                }

                sumprice = sumGeneralCost + total_sum_price_of_item;    // Sum Total Price of Items
                price_vat = vatSumGeneralCost + total_vat_price_of_item; // Vat Total Price of Items
                sumprice_net = netGeneralCost + total_net_price_of_item; // Net Total Price of Items
                
                BusinessLogicBridge.DataStore.updateInvoicePriceByID(sumprice, price_vat, sumprice_net, inv_trans_id);
                
                //กรุณาทำการชำระเงินก่อนปิดสัญญา


            #endregion

            return inv_trans_id;
        }

        void bttPrintInvoice_Click(object sender, EventArgs e)
        {
            // Check Having Invoice
            DataTable HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInIDAndStatus(textEditCheckInId.EditValue.To<int>());

            if (HaveInvoice.Rows.Count > 0)
            {
                if (HaveInvoice.Rows[0]["inv_trans_status"].To<int>() == 2)
                {
                    // Create Case
                    createInvoice();

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_payment"), getLanguage("_softwarename"), "info");
                    bttPrintInvoice.Enabled = false;
                }
                else {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_payment"), getLanguage("_softwarename"), "info");
                }
                bttCalculate.Enabled = false;
                dateEditLeaveDate.Enabled = false;
                lookUpEditCharge.Enabled = false;
                return;
            }
            else {
                createInvoice();
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_payment"), getLanguage("_softwarename"), "info");
                bttPrintInvoice.Enabled = false;
                bttCalculate.Enabled = false;
                dateEditLeaveDate.Enabled = false;
                lookUpEditCharge.Enabled = false;
                return;
            }

           
        }

        void bttPrintInsurance_Click(object sender, EventArgs e)
        {

            bool HaveRefund = BusinessLogicBridge.DataStore.CheckRefundInfoFromCheckInID(textEditCheckInId.EditValue.To<int>());

            if (HaveRefund != true)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_insurance"), getLanguage("_softwarename"));
                TrySaveError = true;
                bttCalculate.Enabled = false;
                dateEditLeaveDate.Enabled = false;
                lookUpEditCharge.Enabled = false;
                return;
            }
            else
            {

                DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInIDAndStatus(textEditCheckInId.EditValue.To<int>());

                int invoice_id_temp = 0;

                if (InvoiceInfo.Rows.Count > 0)
                {
                    if (InvoiceInfo.Rows[0]["inv_trans_status"].To<int>() == 2)
                    {
                        invoice_id_temp = createInvoice();
                    }
                    else
                    {
                        invoice_id_temp = InvoiceInfo.Rows[0]["inv_trans_id"].To<int>();
                    }
                }
                else {
                    invoice_id_temp = createInvoice();
                }

                if (invoice_id_temp > 0)
                {
                    createRefundTransaction(invoice_id_temp);

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_insurance"), getLanguage("_softwarename"), "info");
                    bttPrintInsurance.Enabled = false;
                    bttCalculate.Enabled = false;
                    dateEditLeaveDate.Enabled = false;
                    lookUpEditCharge.Enabled = false;
                    return;
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_please_insurance"), getLanguage("_softwarename"), "info");
                    bttCalculate.Enabled = false;
                    dateEditLeaveDate.Enabled = false;
                    return;
                }
            }
        }

        void bttCalculate_Click(object sender, EventArgs e)
        {
            reCalculate();
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename"));
            if (dr == DialogResult.Yes)
            {

                //
                initRoom();

                //
                enableControl(false);
                changeRow();
                action_key = 0;
                button_event = "";
            }
        }

        void bttEdit_Click(object sender, EventArgs e)
        {
            enableControl(true);            
            bttEdit.Enabled = false;
            
            dateEditLeaveDate.Enabled = true;

            if (lookUpEditContractType.EditValue != null)
            {
                lookUpEditCharge.EditValue = 1;


                // Contact type is daily or monthly
                if (lookUpEditContractType.EditValue.To<int>() == 1)
                {
                    lookUpEditCharge.Enabled = false;                    
                }
                else
                {
                    lookUpEditCharge.Enabled = true;
                    bttCalculate.Enabled = true;
                }
            }
            else {
                lookUpEditCharge.Enabled = true;
                bttCalculate.Enabled = true;
            }

            if (lookUpEditCharge.EditValue == null)
            {
                bttCalculate.Enabled = false;
            }
            
            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttRemoveItem.Enabled = false;

            int HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceByCheckInID(textEditCheckInId.EditValue.To<int>());
            
            bttPrintInsurance.Enabled = false;

            if (HaveInvoice > 0) {

                DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceInfoByCheckInIDAndStatus(textEditCheckInId.EditValue.To<int>());

                if (InvoiceInfo.Rows.Count > 0)
                {
                    if (InvoiceInfo.Rows[0]["inv_trans_status"].To<int>() == 2)
                    {
                        dateEditInvoiceDueDate.EditValue = null;
                    }
                    else
                    {

                        bool HaveRefund = BusinessLogicBridge.DataStore.CheckRefundInfoFromCheckInID(textEditCheckInId.EditValue.To<int>());

                        if (HaveRefund != true)
                        {
                            bttPrintInsurance.Enabled = true;
                        }

                        lookUpEditCharge.EditValue = InvoiceInfo.Rows[0]["inv_trans_chargetype"].To<int>();
                        dateEditInvoiceDueDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_cutduedate"].To<DateTime>();
   
                        gridControlItem.Enabled = false;
                        bttAddItem.Enabled = false;
                        bttRemoveItem.Enabled = false;

                        //reCalculate();

                        dateEditLeaveDate.Enabled = false;
                        lookUpEditCharge.Enabled = false;
                        bttCalculate.Enabled = false;
                    }

                }
                else {
                    dateEditInvoiceDueDate.EditValue = null;
                }

            }
            else
            {
                dateEditInvoiceDueDate.EditValue = null;
            }

            if (bttPrintInsurance.Enabled==true)
            {
                bttSave.Enabled = false;
            }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {

            if (lookUpEditCharge.EditValue == null) {
                utilClass.showPopupMessegeBox(this, "Please select rental type.", getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }
            
            try
            {

                if (lookUpEditContractType.EditValue.To<int>() == 3)
                {
                    // Check Refund
                    bool HaveRefund = BusinessLogicBridge.DataStore.CheckRefundInfoFromCheckInID(textEditCheckInId.EditValue.To<int>());

                    if (HaveRefund != true)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1010"), getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }
                }

                int HaveInvoice = BusinessLogicBridge.DataStore.getInvoiceByCheckInID(textEditCheckInId.EditValue.To<int>());

                if (HaveInvoice == 0) {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1009"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                // Check Invoice
                int HaveInvoiceUnPaid = BusinessLogicBridge.DataStore.getInvoiceUnpaidByRoomID(textEditRoomId.EditValue.To<int>());

                if (HaveInvoiceUnPaid > 0)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1009"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                // Close Contract if true
                BusinessLogicBridge.DataStore.ClosedContract(textEditTenantId.EditValue.To<int>(), textEditRoomId.EditValue.To<int>(), textEditCheckInId.EditValue.To<int>());
                
                // remove payment flag

                BusinessLogicBridge.DataStore.RoomInformleave_cancel(textEditRoomId.EditValue.To<int>(), textEditTenantId.EditValue.To<int>());
                BusinessLogicBridge.DataStore.removePaymentFlag(textEditRoomId.EditValue.To<int>());
                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Room Management [Check Out]");
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                initRoom();
                enableControl(false);
                changeRow();
                action_key = 0;
                button_event = "";
                MForm.loadDashBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }
    
        #endregion


    }
}
