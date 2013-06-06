using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DXWindowsApplication2.PrintDocuments;
using DevExpress.XtraReports.UI;

namespace DXWindowsApplication2.UserForms
{
    public partial class ViewInvoice : uBase
    {
        public static XtraMessageBoxForm AddPanel;
        public static DataTable dataItemsStaticTable;
        public static DataTable dataItemsForCheck;
        public static DataTable ItemTableTemp;

        private DataTable ItemForDelete;
        public static DataTable DTDocInfo;
        public static int inv_trans_id_temp;
        public static DevExpress.XtraEditors.TextEdit textEditTrigger;
        private int action_key = 0;
        private string button_event = "";
        public static int counterItem;
        public static int invoice_contract_type =0;
        public static DataTable ItemGeneralCost;

        private Boolean _CheckRoom = false;

        private int room_check_count = 0;

        public int selectRoomID = 0;

        public ViewInvoice()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            textEditTrigger = new TextEdit();
            ItemTableTemp = null;
            this.Load += new EventHandler(ViewInvoice_Load);
            this.Resize += new EventHandler(ViewInvoice_Resize);
            gridViewInvoice.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewInvoice_RowClick);
            gridViewInvoice.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewInvoice_FocusedRowChanged);
            textEditTrigger.TextChanged += new EventHandler(textEditTrigger_TextChanged);


            repositoryItemCheckEdit.CheckedChanged += new EventHandler(repositoryItemCheckEdit_CheckedChanged);

            gridViewInvoice.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(gridViewInvoice_CustomRowCellEdit);

            checkEditSelectAll.CheckedChanged += new EventHandler(checkEditSelectAll_CheckedChanged);

            dateEdit1.EditValueChanged += new EventHandler(dateEdit1_EditValueChanged);

            dateEdit1.EditValue = DateTime.Now;
            dateEdit2.EditValue = DateTime.Now;

        }

        void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
        {

            CheckEdit statusInfo = sender as CheckEdit;

            int invoice_status = gridViewInvoice.GetRowCellValue(gridViewInvoice.FocusedRowHandle, "inv_trans_status").To<int>();

            if (statusInfo.Checked == true)
            {
                gridViewInvoice.SetRowCellValue(gridViewInvoice.FocusedRowHandle, "checkbox", true);

                if (invoice_status == 2)
                {
                    // Cancel Invoice
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1089"), getLanguage("_softwarename"));
                    statusInfo.Checked = false;
                }
                else
                {

                    checkStateCheckBox();

                }
            }
            else {

                gridViewInvoice.SetRowCellValue(gridViewInvoice.FocusedRowHandle, "checkbox", false);
                checkStateCheckBox();
            }

        }

        void gridViewInvoice_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
           
        }

        void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
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
                    if (gridViewInvoice.Columns[0].View.GetRowCellValue(i, "inv_trans_status").ToString() != "2")
                    {
                        gridViewInvoice.Columns[0].View.SetRowCellValue(i, "checkbox", _CheckRoom);
                    }

                    //if (gridViewInvoice.Columns[0].View.GetRowCellValue(i, "inv_trans_status").ToString() == "0")
                    //{
                    //    bttCancelInvoice.Enabled = true;
                    //    bttPaid.Enabled = true;
                    //}

                    //if (_CheckRoom == true)
                    //{
                    //    room_check_count = room_check_count + 1;
                    //}
                }

            }
        }

        void ViewInvoice_Load(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
            initDropDownBuilding();
            initLoadGridInvoice();
            setThisLang();
        }

        void gridViewInvoice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ItemTableTemp = null;
            DataRow CurrentRow = gridViewInvoice.GetDataRow(e.RowHandle);
            int inv_trans_id = Convert.ToInt32(CurrentRow["inv_trans_id"].ToString());
            inv_trans_id_temp = inv_trans_id;


            if (CurrentRow["inv_trans_status"].ToString() != "0")
            {
                //bttPaid.Enabled = false;
                //bttCancelInvoice.Enabled = false;
            }
            else
            {
                //bttPaid.Enabled = true;
                bttAddItem.Enabled = false;
                bttDelItem.Enabled = false;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;

                //bttCancelInvoice.Enabled = true;
            }

            textEditInvoiceStatusHidden.EditValue = CurrentRow["inv_trans_status"];
            textEditInvoicePrintStatusHidden.EditValue = CurrentRow["inv_trans_print_status"];

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id);

            textEditInvoice_number.EditValue = InvoiceInfo.Rows[0]["inv_trans_number"];
            textEditInvoice_cutdeal.EditValue = InvoiceInfo.Rows[0]["inv_trans_cutduedate"];
            textEditInvoice_datefix.EditValue = InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"];

            invoice_contract_type = InvoiceInfo.Rows[0]["check_in_contracttype"].To<int>();

            if (InvoiceInfo.Rows[0]["inv_trans_paymentdate"].ToString() == "")
            {
                textEditInvoice_date_paid.EditValue = "";
            }
            else
            {
                textEditInvoice_date_paid.EditValue = InvoiceInfo.Rows[0]["inv_trans_paymentdate"];
            }

            textEditDateCreated.EditValue = InvoiceInfo.Rows[0]["inv_trans_datecreated"];
            textEditInvoiceStatus.EditValue = CurrentRow["inv_trans_status_text"];

            textEditTenantName.EditValue = InvoiceInfo.Rows[0]["inv_trans_tenantname"].ToString().Replace("||", " ");

            memoEditTenantAddress.EditValue = InvoiceInfo.Rows[0]["inv_trans_tenantaddress"];
            textEditRoomLabel.EditValue = InvoiceInfo.Rows[0]["inv_trans_roomlabel"];
            textEditFloor.EditValue = InvoiceInfo.Rows[0]["inv_trans_floor"];
            textEditBuilding.EditValue = InvoiceInfo.Rows[0]["inv_trans_building"];
            textEditRoomType.EditValue = InvoiceInfo.Rows[0]["inv_trans_roomtype"];
            textEditInvoice_ElectricBeforePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"];
            textEditInvoice_WaterBeforePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"];
            textEditInvoice_ElectricBeforeDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"];
            textEditInvoice_WaterBeforeDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"];

            textEditInvoice_ElectricLastestPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"];
            textEditInvoice_WaterLastestPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"];
            textEditInvoice_ElectricLastestDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"];
            textEditInvoice_WaterLastestDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"];

            textEditInvoice_PhoneBegin.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_start_date"];
            textEditInvoice_PhoneEnd.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_end_date"];
            textEditInvoice_PhonePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_price"];

            textEditSumPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice"];
            textEditVatPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice_withvat"];
            textEditNetPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice_net"];
            textEditVatType.EditValue = InvoiceInfo.Rows[0]["inv_trans_vattype"];

            loadItem();
                
        }

        public override void Refresh()
        {
            base.Refresh();
            initDropDownBuilding();
            initLoadGridInvoice();
            setThisLang();

            if (selectRoomID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlInvoiceList.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gridViewInvoice.FocusedRowHandle = c.LocateByValue("inv_trans_room_id", selectRoomID);
                //
                selectRoomID = 0;
            }
            

        }

        void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            dateEdit2.Properties.MinValue = dateEdit1.EditValue.To<DateTime>();
        }

        void initItem()
        {
            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id_temp);

            DataTable ItemTable = BusinessLogicBridge.DataStore.getInvoiceItemsByInvoiceId(inv_trans_id_temp);

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
                    // Contract แบบ รายเดือน
                    ItemTable.Rows[i]["order"] = counterItem;

                    if (ItemTable.Rows[i]["item_id"].To<int>() == 0)
                    {
                        ItemTable.Rows[i]["item_priceperunit"] = ItemTable.Rows[i]["item_priceperunit"];
                    }
                    else
                    {

                        ItemTable.Rows[i]["item_amount"] = 1;

                        if (InvoiceInfo.Rows[0]["check_in_contracttype"].To<int>() == 1)
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
                    ItemTable.Rows[i]["item_sumprice"] = ItemTable.Rows[i]["item_amount"].To<double>() * ItemTable.Rows[i]["item_priceperunit"].To<double>();

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

                    //if (ItemTable.Rows[i]["item_id"].To<int>() != 0)
                    //{
                    //    bttRemoveItem.Enabled = false;
                    //}
                    //else
                    //{
                    //    bttRemoveItem.Enabled = true;
                    //}

                    counterItem++;
                    CheckOutItemTable.ImportRow(ItemTable.Rows[i]);
                //}
                //else if (InvoiceInfo.Rows[0]["check_in_contracttype"].To<int>() == 1)
                //{
                //    // Contract แบบ รายวัน
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

                //    //if (ItemTable.Rows[i]["item_id"].To<int>() != 0)
                //    //{
                //    //    bttRemoveItem.Enabled = false;
                //    //}
                //    //else
                //    //{
                //    //    bttRemoveItem.Enabled = true;
                //    //}

                //    counterItem++;
                //    CheckOutItemTable.ImportRow(ItemTable.Rows[i]);
                //}

            }
            //
            if (ItemTableTemp == null)
                ItemTableTemp = ItemTable.Clone();
            //

            CheckOutItemTable = MainForm.VatCalculate(CheckOutItemTable);

            gridControlInvoiceItem.DataSource = CheckOutItemTable;

            dataItemsForCheck = CheckOutItemTable;

            //ItemTableTemp.Clear();

            //
        }

        void ViewInvoice_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
        }

        void gridViewInvoice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            ItemTableTemp = null;
            ItemForDelete = null;

            DataRow CurrentRow = gridViewInvoice.GetDataRow(e.FocusedRowHandle);
            int inv_trans_id = Convert.ToInt32(CurrentRow["inv_trans_id"].ToString());
            inv_trans_id_temp = inv_trans_id;

            textEditInvoiceStatusHidden.EditValue = CurrentRow["inv_trans_status"];
            textEditInvoicePrintStatusHidden.EditValue = CurrentRow["inv_trans_print_status"];

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id);

            textEditInvoice_number.EditValue = InvoiceInfo.Rows[0]["inv_trans_number"];
            textEditInvoice_cutdeal.EditValue = InvoiceInfo.Rows[0]["inv_trans_cutduedate"];
            textEditInvoice_datefix.EditValue = InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"];

            invoice_contract_type = InvoiceInfo.Rows[0]["check_in_contracttype"].To<int>();

            if (InvoiceInfo.Rows[0]["inv_trans_paymentdate"].ToString() == "")
            {
                textEditInvoice_date_paid.EditValue = "";
            }
            else
            {
                textEditInvoice_date_paid.EditValue = InvoiceInfo.Rows[0]["inv_trans_paymentdate"];
            }

            textEditDateCreated.EditValue = InvoiceInfo.Rows[0]["inv_trans_datecreated"];
            textEditInvoiceStatus.EditValue = CurrentRow["inv_trans_status_text"];

            textEditTenantName.EditValue = InvoiceInfo.Rows[0]["inv_trans_tenantname"].ToString().Replace("||", " ");

            memoEditTenantAddress.EditValue = InvoiceInfo.Rows[0]["inv_trans_tenantaddress"];
            textEditRoomLabel.EditValue = InvoiceInfo.Rows[0]["inv_trans_roomlabel"];
            textEditFloor.EditValue = InvoiceInfo.Rows[0]["inv_trans_floor"];
            textEditBuilding.EditValue = InvoiceInfo.Rows[0]["inv_trans_building"];
            textEditRoomType.EditValue = InvoiceInfo.Rows[0]["inv_trans_roomtype"];
            textEditInvoice_ElectricBeforePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"];
            textEditInvoice_WaterBeforePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"];
            textEditInvoice_ElectricBeforeDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"];
            textEditInvoice_WaterBeforeDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"];

            textEditInvoice_ElectricLastestPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"];
            textEditInvoice_WaterLastestPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"];
            textEditInvoice_ElectricLastestDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"];
            textEditInvoice_WaterLastestDate.EditValue = InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"];

            textEditInvoice_PhoneBegin.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_start_date"];
            textEditInvoice_PhoneEnd.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_end_date"];
            textEditInvoice_PhonePrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_phone_price"];

            textEditSumPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice"];
            textEditVatPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice_withvat"];
            textEditNetPrice.EditValue = InvoiceInfo.Rows[0]["inv_trans_sumprice_net"];
            textEditVatType.EditValue = InvoiceInfo.Rows[0]["inv_trans_vattype"];

            lbVat.Text = getLanguage("_vat") + " " + InvoiceInfo.Rows[0]["inv_trans_vatrate"]+ " %";

            loadItem();

            double sumGeneralCost = 0;
            double total_price_of_item = 0;
            
            for (int cost = 0; cost < ItemGeneralCost.Rows.Count; cost++)
            {
                sumGeneralCost += ItemGeneralCost.Rows[cost]["item_netprice"].To<double>();
            }

            DataTable ItemList = ((DataTable)gridControlInvoiceItem.DataSource);

            if (ItemList.Rows.Count > 0)
            {
                for (int k = 0; k < ItemList.Rows.Count; k++)
                {
                    total_price_of_item += ItemList.Rows[k]["item_netprice"].To<double>();
                }
            }

            double sumprice = sumGeneralCost + total_price_of_item;

            if (CurrentRow["inv_trans_status"].ToString() != "0")
            {
                //if (CurrentRow["inv_trans_status"].ToString() == "2")
                //{
                //    //bttPrint.Enabled = false;
                //}

               // bttPaid.Enabled = false;
                //bttCancelInvoice.Enabled = false;
            }
            else
            {
                
                bttAddItem.Enabled = false;
                bttDelItem.Enabled = false;

                bttSave.Enabled = false;
                bttCancel.Enabled = false;

                //bttPaid.Enabled = true;
                //bttCancelInvoice.Enabled = true;
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

            ItemGeneralCost.Columns.Add("order", typeof(int));
            ItemGeneralCost.Columns.Add("item_name", typeof(string));
            ItemGeneralCost.Columns.Add("item_amount", typeof(string));
            ItemGeneralCost.Columns.Add("item_priceperunit", typeof(string));
            ItemGeneralCost.Columns.Add("item_sumprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vatprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_netprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vat_bool", typeof(bool));

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id_temp);

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

                #region room cost
                amountofstay = InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>();
                double room_price = InvoiceInfo.Rows[0]["inv_trans_roomprice"].To<double>();

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

                ItemGeneralCost.Rows.Add(1, "ค่าเช่าห้อง", amountofstay, room_price.ToString("N2"), cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
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

                ItemGeneralCost.Rows.Add(2, "ค่าไฟ", InvoiceInfo.Rows[0]["inv_trans_emeter_unit"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"].To<double>().ToString("N2"), cost_sumprice, cost_vatprice, cost_netprice, vat_bool);

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

                ItemGeneralCost.Rows.Add(3, "ค่าน้ำ", InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"].To<double>().ToString("N2"), cost_sumprice, cost_vatprice, cost_netprice, vat_bool);
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

            gridControlGeneralCost.DataSource = ItemGeneralCost;

            initItem();
        }

        void textEditTrigger_TextChanged(object sender, EventArgs e)
        {
            int counter = 1;
            for (int i = 0; i < dataItemsStaticTable.Rows.Count; i++)
            {

                dataItemsStaticTable.Rows[i]["order"] = counter;
                if (dataItemsStaticTable.Rows[i]["item_vat"].ToString() != "1")
                {
                    dataItemsStaticTable.Rows[i]["item_vat_bool"] = true;
                }
                else
                {
                    dataItemsStaticTable.Rows[i]["item_vat_bool"] = false;
                }
                counter++;
            }

            gridControlInvoiceItem.DataSource = dataItemsStaticTable;
        }

        void setThisLang() {

            gbInvoiceDetail.Text = getLanguage("_invoice_info");
            lbNo.Text = getLanguage("_number");
            lbDateCreate.Text = getLanguage("_issue_date");
            lbStatus.Text = getLanguage("_status");
            lbTenant.Text = getLanguage("_tenant");
            lbAddress.Text = getLanguage("_address");
            lbDuedate.Text = getLanguage("_billing_date");
            lbPayDate.Text = getLanguage("_due_payment_day");
            lbPaid.Text = getLanguage("_paid_date");

            gbRoom.Text = getLanguage("_room");
            lbName.Text = getLanguage("_room_name");
            lbFloor.Text = getLanguage("_floor");
            lbBuildingRoom.Text = getLanguage("_building");
            lbType.Text = getLanguage("_type");

            gbMeterRecord.Text = getLanguage("_meter_recording");
            lbElectrical.Text = getLanguage("_electricity");
            lbWater.Text = getLanguage("_water");
            lbPhone.Text = getLanguage("_telephone");
            lbPrev.Text = getLanguage("_prev");
            lbRecordDate.Text = getLanguage("_record_date");
            lbLasted.Text = getLanguage("_lasted");
            lbRecordDate2.Text = getLanguage("_record_date");
            lbStartDate.Text = getLanguage("_start_date");
            lbEndDate.Text = getLanguage("_last_date");
            lbPhoneCharge.Text = getLanguage("_phone_charge");

            // Left

            labelControlFromdate.Text = getLanguage("_from");
            labelControlTodate.Text = getLanguage("_to");
            checkEditSelectAll.Text = getLanguage("_select_all");
            lbBuilding.Text = getLanguage("_building");

            colInvoiceNumber.Caption = getLanguage("_invoice_no");
            colInvoiceRoomLabel.Caption     = getLanguage("_room_name");
            colInvoiceDateCreate.Caption = getLanguage("_issue_date");
            colInvoiceStatusText.Caption    = getLanguage("_status");
            colInvoiceStatusPrint.Caption = getLanguage("_printing_status");

            bttPrint.Text = getLanguage("_print_all_selected");
            bttPaid.Text = getLanguage("_pay_all_selected");
            bttCancelInvoice.Text = getLanguage("_cancel_all_selected");
            bttAddItem.Text = getLanguage("_add");
            bttDelItem.Text = getLanguage("_delete");


            groupExpense.Text = getLanguage("_charge_list");

            gridColumNo.Caption = getLanguage("_item");
            colInvoiceItemName.Caption = getLanguage("_item_name");
            gridColumnAmount.Caption = getLanguage("_unit");
            gridColumnAmountPerUnit.Caption = getLanguage("_price_per_unit");
            colInvoiceItemPrice.Caption = getLanguage("_price");
            gridColumnVat.Caption = getLanguage("_tax");
            gridColumnVating.Caption = getLanguage("_tax_calculate");

            lbSumPrice.Text     = getLanguage("_total");

            lbVat.Text = getLanguage("_vat");

            lbNet.Text          = getLanguage("_net");
            lbBaht.Text         = getLanguage("_baht");
            lbBaht2.Text        = getLanguage("_baht");
            lbBaht3.Text        = getLanguage("_baht");
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }

        void initLoadGridInvoice(){

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceList();

            if (InvoiceInfo.Rows.Count == 0) {
                bttCancelInvoice.Enabled = false;
                bttPaid.Enabled = false;
                bttPrint.Enabled = false;
                bttEdit.Enabled = false;
            }

            InvoiceInfo.Columns.Add("checkbox" ,typeof(bool));
            InvoiceInfo.Columns.Add("inv_trans_status_text");
            InvoiceInfo.Columns.Add("inv_trans_print_status_text");

            for (int i = 0; i < InvoiceInfo.Rows.Count; i++ )
            {
                InvoiceInfo.Rows[i]["checkbox"] = false;
                if (InvoiceInfo.Rows[i]["inv_trans_status"].ToString() == "0")
                {
                    // Unpaid
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "รอชำระเงิน";

                }
                else if (InvoiceInfo.Rows[i]["inv_trans_status"].ToString() == "1")
                { 
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "จ่ายแล้ว";
                    
                }
                else
                {
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "ยกเลิก";

                }

                if (InvoiceInfo.Rows[i]["inv_trans_print_status"].ToString() == "0")
                {
                    // UnPrint
                    InvoiceInfo.Rows[i]["inv_trans_print_status_text"] = "ยังไม่พิมพ์";
                }
                else {
                    InvoiceInfo.Rows[i]["inv_trans_print_status_text"] = "พิมพ์แล้ว";
                }

            }
            gridControlInvoiceList.DataSource = InvoiceInfo;
    }

        void reCalculate() {

            DataTable DTInvoice = new DataTable();
            DataTable ItemTable = new DataTable();
            DataTable gridTable = new DataTable();
            DataTable DTDocInfo = new DataTable();
            DataTable InvoiceInfo = new DataTable();
            double sumprice = 0;
            double sumprice_net = 0;
            double price_vat = 0;

            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;

            InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id_temp);
            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());
           
            
            DataTable ItemList = ((DataTable)gridControlInvoiceItem.DataSource);
            
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

            textEditSumPrice.EditValue = sumprice;
            textEditVatPrice.EditValue = price_vat;
            textEditNetPrice.EditValue = sumprice_net;
        }

        void checkStateCheckBox() {

            DataTable gridTable = ((DataTable)gridControlInvoiceList.DataSource);

            int _countCheckbox = 0;//gridTable.Select("checkbox=true AND inv_trans_status=0").Length;

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true && (gridTable.Rows[i]["inv_trans_status"]).To<int>() == 0)
                {
                    // unPaid
                    _countCheckbox++;
                }
            }

            if (_countCheckbox > 0)
            {   
                // existing UnPaid
                bttPaid.Enabled = true;
                bttCancelInvoice.Enabled = true;
            }
            else {
                bttPaid.Enabled = false;
                bttCancelInvoice.Enabled = false;
            }

        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            string pathname = "";
            string filePath = "";
            int _countCheckbox = 0;
            DataTable GeneralInfo = new DataTable();
            DataTable GridTableCheckbox = new DataTable();

            GridTableCheckbox = ((DataTable)gridControlInvoiceList.DataSource);

            XtraReport1 report1 = new XtraReport1();
            report1.CreateDocument(); 

            for (int i = 0; i < GridTableCheckbox.Rows.Count; i++)
            {
                if ((bool)(GridTableCheckbox.Rows[i]["checkbox"]) == true)
                {
                    GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                    filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                    pathname = DXWindowsApplication2.MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), GeneralInfo.Rows[0]["path_all_document"].ToString(), "Invoice", GridTableCheckbox.Rows[i].Table.Rows[0]["inv_trans_number"].ToString() + ".pdf");

                    PrintDocuments.invoice PrintInvoice = new DXWindowsApplication2.PrintDocuments.invoice();

                    PrintInvoice.loopGenDataRow(GridTableCheckbox.Rows[i]["inv_trans_id"].To<int>());

                    //PrintInvoice.ExportToPdf(pathname);

                    PrintInvoice.CreateDocument();

                    report1.Pages.AddRange(PrintInvoice.Pages);

                    BusinessLogicBridge.DataStore.setInvoicePrint(GridTableCheckbox.Rows[i]["inv_trans_id"].To<int>());
                    _countCheckbox++;
                }
            }

            if (_countCheckbox <= 0)
            {
                MessageBox.Show("Please select item ...");
                return;
            }

            report1.PrintingSystem.ContinuousPageNumbering = true;

            // Create a Print Tool and show the Print Preview form. 
            ReportPrintTool printTool = new ReportPrintTool(report1);
            printTool.ShowPreviewDialog();

            initLoadGridInvoice();
        }

        private void bttPaid_Click(object sender, EventArgs e)
        {
            string RecieptNO = "";
            int invoiceID = 0;
            int _countCheckbox = 0;
            DataTable DTReciept = new DataTable();
            DataTable gridTable = new DataTable();
            DataTable InvoiceInfo = new DataTable();
            DataTable DTInvoiceItem = new DataTable();
            DataTable ItemTable = new DataTable();

            DataTable DTInvoiceItemNew = new DataTable();

            gridTable = ((DataTable)gridControlInvoiceList.DataSource);

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                {
                    // Created Reciept
                    _countCheckbox++;
                }
            }

            if (_countCheckbox <= 0)
            {
                MessageBox.Show("Please select item ...");
                return;
            }

             
             if (utilClass.showPopupConfirmBox(this,"Do you want create Reciept ?",getLanguage("_softwarename"))== DialogResult.Yes)
             {
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
                
                 #region declare parameter
                     DataTable DTDocInfo = new DataTable();
                     DataTable DTInvoice = new DataTable();
                     DataTable DTRecieptInfo = new DataTable();
                     string InvoiceNO = "";
                     int recieptID = 0;

                     double room_price = 0;
                     double amountofstay = 0;

                     double sumprice = 0;
                     double price_vat = 0;
                     double sumprice_net = 0;

                     double item_sumprice = 0;
                     double item_vatprice = 0;
                     double item_netprice = 0;
                     double item_priceperunit = 0;

                     double total_price_of_item = 0;    

                     string ThaiBaht = "";
                 #endregion

                 for (int i = 0; i < gridTable.Rows.Count; i++)
                 {
                     if ((bool)(gridTable.Rows[i]["checkbox"]) == true && (gridTable.Rows[i]["inv_trans_status"]).To<int>() == 0)
                     {
                         DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(gridTable.Rows[i]["inv_trans_building"].ToString());

                         if (DTDocInfo.Rows[0]["doc_saperate_reciept"].ToString() == "0")
                         {
                             RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
                         }
                         else
                         {
                             if (gridTable.Rows[i]["check_in_contracttype"].To<int>() == 3)
                             {
                                 RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
                             }
                             else
                             {
                                 RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "D" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
                             }
                         }
                         
                         DTInvoiceItem = BusinessLogicBridge.DataStore.getInvoiceItemsByInvoiceId(gridTable.Rows[i]["inv_trans_id"].To<int>());

                         if (DTInvoiceItem.Rows.Count >= DTInvoiceItemNew.Rows.Count)
                             {
                                 InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(gridTable.Rows[i]["inv_trans_id"].To<int>());

                                 ThaiBaht = DXWindowsApplication2.MainForm.ThaiBaht(InvoiceInfo.Rows[0]["inv_trans_sumprice_net"].ToString());

                                 DTReciept.Rows.Add(
                                        RecieptNO,
                                        InvoiceInfo.Rows[0]["inv_trans_cutduedate"],
                                        InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"],
                                        DateTime.Now,
                                        1,
                                        InvoiceInfo.Rows[0]["inv_trans_tenantname"],
                                        InvoiceInfo.Rows[0]["inv_trans_tenantaddress"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomlabel"],
                                        InvoiceInfo.Rows[0]["inv_trans_building"],
                                        InvoiceInfo.Rows[0]["inv_trans_floor"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomtype"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_start_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_end_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_phonemeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_phonemeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice_withvat"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice_net"],
                                        0,
                                        InvoiceInfo.Rows[0]["inv_trans_room_id"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_name"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_logofile"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_address"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_telephone"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_fax"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_email"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_website"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_tax_id"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_vision"],
                                        DTDocInfo.Rows[0]["doc_header_reciept"],
                                        DTDocInfo.Rows[0]["doc_footer_reciept"],
                                        DTDocInfo.Rows[0]["doc_under_reciept1"],
                                        DTDocInfo.Rows[0]["doc_under_reciept2"],
                                        InvoiceInfo.Rows[0]["inv_trans_doc_dateformat"],
                                        InvoiceInfo.Rows[0]["inv_trans_doc_logo_position"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomprice"],
                                        InvoiceInfo.Rows[0]["inv_trans_vattype"],
                                        InvoiceInfo.Rows[0]["inv_trans_number"],
                                        ThaiBaht,
                                        InvoiceInfo.Rows[0]["check_in_id"],
                                        InvoiceInfo.Rows[0]["check_in_contracttype"]
                                    );

                                 // loop insert to Reciept Item Table
                                 recieptID = BusinessLogicBridge.DataStore.createRecieptTransaction(DTReciept);
                                 // loop insert to Reciept Item Table
                                 #region ItemList Create
                                 if (recieptID > 0)
                                 {
                                     ItemTable.Clear();

                                     DataTable ItemTableGrid = ((DataTable)gridControlInvoiceItem.DataSource);

                                     for (int itemcounter = 0; itemcounter < ItemTableGrid.Rows.Count; itemcounter++)
                                     {
                                         // AddItem 
                                         ItemTable.Rows.Add(recieptID, ItemTableGrid.Rows[itemcounter]["item_id"].To<int>(), ItemTableGrid.Rows[itemcounter]["item_name"].ToString(), ItemTableGrid.Rows[itemcounter]["item_price_monthly"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_price_daily"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_vat"].To<int>(), ItemTableGrid.Rows[itemcounter]["item_type"].To<int>(), ItemTableGrid.Rows[itemcounter]["item_priceperunit"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_amount"].To<int>(), ItemTableGrid.Rows[itemcounter]["item_sumprice"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_vatprice"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_netprice"].To<double>(), ItemTableGrid.Rows[itemcounter]["item_flag"].ToString());
                                     }
                                     BusinessLogicBridge.DataStore.createRecieptItem(ItemTable);
                                 }
                                 #endregion

                                 // Update Invoice status to "Paid"
                                 BusinessLogicBridge.DataStore.setInvoicePaid(gridTable.Rows[i]["inv_trans_id"].To<int>());
                                
                                // remove payment flag
                                 BusinessLogicBridge.DataStore.removePaymentFlag(InvoiceInfo.Rows[0]["inv_trans_room_id"].To<int>());
                             }
                             else { 
                                 
                                // cancel invoice by id & cancel item invoice
                                 BusinessLogicBridge.DataStore.setInvoiceCancel(gridTable.Rows[i]["inv_trans_id"].To<int>());
                                 // remove payment flag
                                 BusinessLogicBridge.DataStore.removePaymentFlag(InvoiceInfo.Rows[0]["inv_trans_room_id"].To<int>());

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
                                 DTInvoice.Columns.Add("inv_trans_vatrate", typeof(decimal));

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

                                 DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(gridTable.Rows[i]["building_id"].To<int>());

                                 InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');

                                 // create new Invoice transaction & item invoice
                                 // Re-calculate
                                 #region create New Invoice
                                     // Select old transaction
                                     InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(gridTable.Rows[i]["inv_trans_id"].To<int>());

                                     #region Re-Calculation
                                     for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                                     {
                                         if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                                         {

                                             if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                                             {
                                                 item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                 item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                 item_netprice = item_sumprice;
                                                 item_sumprice = item_netprice - item_vatprice;
                                             }

                                             if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                                             {
                                                 item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                 item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                 item_netprice = item_sumprice + item_vatprice;
                                             }
                                         }
                                         else
                                         {
                                             item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                             item_netprice = item_sumprice;
                                             item_vatprice = 0.00;
                                         }

                                         item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                         total_price_of_item += item_netprice;
                                     }

                                     // sum price
                                     sumprice = InvoiceInfo.Rows[0]["inv_trans_roomprice"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>() + total_price_of_item;  // Sum Plus Total Price of Items

                                     price_vat = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice; // 7 div 100 x price

                                     switch (DTDocInfo.Rows[0]["doc_vat_type"].To<int>())
                                     {
                                         case 1:
                                             // no vat
                                             sumprice_net = sumprice;
                                             price_vat = 0.0;
                                             break;
                                         case 2:
                                             // with net
                                             sumprice_net = sumprice;
                                             price_vat = 0.0;
                                             break;
                                         case 3:
                                             // increase from net
                                             sumprice_net = sumprice + price_vat;
                                             break;
                                         default:
                                             sumprice_net = sumprice;
                                             break;
                                     }
                                     #endregion
                                     
                                     #region Add Datarow
                                     DTInvoice.Rows.Add(
                                                InvoiceNO,
                                                InvoiceInfo.Rows[0]["inv_trans_cutduedate"],
                                                InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"],
                                                DateTime.Now,
                                                0,
                                                InvoiceInfo.Rows[0]["inv_trans_tenantname"],
                                                InvoiceInfo.Rows[0]["inv_trans_tenantaddress"],
                                                InvoiceInfo.Rows[0]["inv_trans_roomlabel"],
                                                InvoiceInfo.Rows[0]["inv_trans_building"],
                                                InvoiceInfo.Rows[0]["inv_trans_floor"],
                                                InvoiceInfo.Rows[0]["inv_trans_roomtype"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_unit"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"],
                                                InvoiceInfo.Rows[0]["inv_trans_emeter_price"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"],
                                                InvoiceInfo.Rows[0]["inv_trans_wmeter_price"],
                                                InvoiceInfo.Rows[0]["inv_trans_phone_start_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_phone_end_date"],
                                                InvoiceInfo.Rows[0]["inv_trans_phonemeter_unit"],
                                                InvoiceInfo.Rows[0]["inv_trans_phonemeter_priceperunit"],
                                                InvoiceInfo.Rows[0]["inv_trans_phone_price"],
                                                sumprice,
                                                price_vat,
                                                sumprice_net, 
                                                0,
                                                InvoiceInfo.Rows[0]["inv_trans_room_id"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_name"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_logofile"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_address"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_telephone"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_fax"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_email"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_website"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_tax_id"],
                                                InvoiceInfo.Rows[0]["inv_trans_company_vision"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_header_invoice"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_footer_invoice"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_under_invoice1"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_under_invoice2"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_dateformat"],
                                                InvoiceInfo.Rows[0]["inv_trans_doc_logo_position"],
                                                InvoiceInfo.Rows[0]["inv_trans_roomprice"],
                                                InvoiceInfo.Rows[0]["inv_trans_vattype"],
                                                InvoiceInfo.Rows[0]["check_in_id"],
                                                InvoiceInfo.Rows[0]["check_in_contracttype"],
                                                DTDocInfo.Rows[0]["doc_vat"].To<decimal>()
                                                );
                                     #endregion
                                     
                                     invoiceID = BusinessLogicBridge.DataStore.createInvoiceTransaction(DTInvoice);
                                     DTInvoice.Clear();

                                     #region ItemList Create
                                     if (invoiceID > 0) {
                                         for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                                         {
                                             if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                                             {
                                                 if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                                                 {
                                                     item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                     item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                     item_netprice = item_sumprice;
                                                     item_sumprice = item_netprice - item_vatprice;
                                                 }

                                                 if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                                                 {
                                                     item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                     item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                     item_netprice = item_sumprice + item_vatprice;
                                                 }
                                             }
                                             else
                                             {
                                                 item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<double>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                 item_netprice = item_sumprice;
                                             }

                                             item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                             total_price_of_item += item_netprice;
                                             
                                             // AddItem
                                             ItemTable.Rows.Add(invoiceID, DTInvoiceItemNew.Rows[itemcounter]["item_id"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_name"].ToString(), 0, 0, DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_type"].To<int>(), item_priceperunit, item_sumprice, item_vatprice, item_netprice, DTInvoiceItemNew.Rows[itemcounter]["item_flag"].ToString());
                                         }
                                         BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);
                                     }
                                     #endregion

                                 #endregion

                                 // New ID Of Invoice
                                 InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(invoiceID);

                                 BusinessLogicBridge.DataStore.insert_payment_flag(InvoiceInfo.Rows[0]["inv_trans_room_id"].To<int>());
                                    
                                 ThaiBaht = DXWindowsApplication2.MainForm.ThaiBaht(InvoiceInfo.Rows[0]["inv_trans_sumprice_net"].ToString());

                                 
                                 // create new Reciept Transaction & item Reciept
                                 #region New Datarow of New reciept
                                 DTReciept.Rows.Add(
                                        RecieptNO,
                                        InvoiceInfo.Rows[0]["inv_trans_cutduedate"],
                                        InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"],
                                        DateTime.Now,
                                        1,
                                        InvoiceInfo.Rows[0]["inv_trans_tenantname"],
                                        InvoiceInfo.Rows[0]["inv_trans_tenantaddress"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomlabel"],
                                        InvoiceInfo.Rows[0]["inv_trans_building"],
                                        InvoiceInfo.Rows[0]["inv_trans_floor"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomtype"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_emeter_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_wmeter_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_start_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_end_date"],
                                        InvoiceInfo.Rows[0]["inv_trans_phonemeter_unit"],
                                        InvoiceInfo.Rows[0]["inv_trans_phonemeter_priceperunit"],
                                        InvoiceInfo.Rows[0]["inv_trans_phone_price"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice_withvat"],
                                        InvoiceInfo.Rows[0]["inv_trans_sumprice_net"],
                                        0,
                                        InvoiceInfo.Rows[0]["inv_trans_room_id"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_name"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_logofile"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_address"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_telephone"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_fax"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_email"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_website"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_tax_id"],
                                        InvoiceInfo.Rows[0]["inv_trans_company_vision"],
                                        DTDocInfo.Rows[0]["doc_header_reciept"],
                                        DTDocInfo.Rows[0]["doc_footer_reciept"],
                                        DTDocInfo.Rows[0]["doc_under_reciept1"],
                                        DTDocInfo.Rows[0]["doc_under_reciept2"],
                                        InvoiceInfo.Rows[0]["inv_trans_doc_dateformat"],
                                        InvoiceInfo.Rows[0]["inv_trans_doc_logo_position"],
                                        InvoiceInfo.Rows[0]["inv_trans_roomprice"],
                                        InvoiceInfo.Rows[0]["inv_trans_vattype"],
                                        InvoiceInfo.Rows[0]["inv_trans_number"],
                                        ThaiBaht,
                                        InvoiceInfo.Rows[0]["check_in_id"],
                                        InvoiceInfo.Rows[0]["check_in_contracttype"]
                                    );
                                 #endregion
                                 recieptID = BusinessLogicBridge.DataStore.createRecieptTransaction(DTReciept);
                                 // loop insert to Reciept Item Table
                                 #region ItemList Create
                                 if (recieptID > 0)
                                 {
                                     ItemTable.Clear();
                                     for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                                     {
                                         if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                                         {
                                             if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                                             {
                                                 item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                 item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                 item_netprice = item_sumprice;
                                                 item_sumprice = item_netprice - item_vatprice;
                                             }

                                             if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                                             {
                                                 item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                                 item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                                 item_netprice = item_sumprice + item_vatprice;
                                             }
                                         }
                                         else
                                         {
                                             item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<double>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                             item_netprice = item_sumprice;
                                             item_vatprice = 0.00;
                                         }

                                         item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                         total_price_of_item += item_netprice;

                                         // AddItem
                                         ItemTable.Rows.Add(recieptID, DTInvoiceItemNew.Rows[itemcounter]["item_id"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_name"].ToString(), 0, 0, DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_type"].To<int>(), item_priceperunit, DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>(), item_sumprice, item_vatprice, item_netprice, DTInvoiceItemNew.Rows[itemcounter]["item_flag"].ToString());
                                     }
                                     BusinessLogicBridge.DataStore.createRecieptItem(ItemTable);
                                 }
                                 #endregion
                             }

                         // End CheckBox Counter
                         // Update End Value from Old Invoice as Begin to next invoice.....

                         //################# E & W ##################

                         // present Value: InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"]
                         // present Value: InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"]

                         // present Date: InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"]
                         // present Date: InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"]

                         // ############ PHONE #############

                         // End Date: InvoiceInfo.Rows[0]["inv_trans_phone_end_date"]


                         // Clear Item Onetime out of Room Information
                         for (int j = 0; j < ItemTable.Rows.Count; j++)
                         {
                             if (ItemTable.Rows[j]["item_type"].To<int>() == 2)
                                 BusinessLogicBridge.DataStore.deleteRoomItemOneTime(InvoiceInfo.Rows[0]["inv_trans_room_id"].To<int>(), ItemTable.Rows[j]["item_id"].To<int>());
                         }

                         BusinessLogicBridge.DataStore.UpdateEndOldInvoiceToNextInvoice(InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"].To<DateTime>(), InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"].To<DateTime>(), InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"].To<double>(), InvoiceInfo.Rows[0]["inv_trans_phone_end_date"].To<DateTime>(), InvoiceInfo.Rows[0]["inv_trans_room_id"].To<int>());
                     }
                 }
                 // Reload Grid 
                 initLoadGridInvoice();
                 bttPaid.Enabled = false;
                 bttCancelInvoice.Enabled = false;
             }

        }

        private void bttCancelInvoice_Click(object sender, EventArgs e)
        {
            int _countCheckbox = 0;
            DataTable gridTable = new DataTable();
            gridTable = ((DataTable)gridControlInvoiceList.DataSource);

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                {
                    // Count Reciept Checkbox
                    _countCheckbox++;
                }
            }

            if (_countCheckbox <= 0)
            {
                MessageBox.Show("Please select item ...");
                return;
            }

            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4029"), getLanguage("_softwarename")) == DialogResult.Yes)
             {
                 for (int i = 0; i < gridTable.Rows.Count; i++)
                 {
                     if ((bool)(gridTable.Rows[i]["checkbox"]) == true && (gridTable.Rows[i]["inv_trans_status"]).To<int>() == 0)
                     {
                         // Set Invoice cancel
                         BusinessLogicBridge.DataStore.setInvoiceCancel(gridTable.Rows[i]["inv_trans_id"].To<int>());

                         // Remove Item of Invoice
                         BusinessLogicBridge.DataStore.removeInvoiceItemByInvoiceID(gridTable.Rows[i]["inv_trans_id"].To<int>());

                         // remove payment flag
                         BusinessLogicBridge.DataStore.removePaymentFlag(gridTable.Rows[i]["inv_trans_room_id"].To<int>());
                     }
                 }
                 // Reload Grid 
                 initLoadGridInvoice();
             }
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            if (textEditInvoiceStatusHidden.EditValue.ToString() == "1" || textEditInvoiceStatusHidden.EditValue.ToString() == "2")
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1013"), getLanguage("_softwarename"));

                return;
            }
            else if (textEditInvoicePrintStatusHidden.EditValue.ToString() == "1"){
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1087"), getLanguage("_softwarename"));
                return;
            }else
            {

                button_event = "update";

                bttAddItem.Enabled = true;
                bttDelItem.Enabled = true;

                bttEdit.Enabled = false;
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
                groupControlListInvoice.Enabled = false;
                panelControlBttControl.Enabled = false;
            }
        }

        private void bttAddItem_Click(object sender, EventArgs e)
        {

            ItemTableTemp = utilClass.showPopAddInvoiceExpense(this, ItemTableTemp);

            ItemForDelete = null;
            //
            initItem();
            reCalculate();
        }

        private void bttDelItem_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes) { 
                // Delete Item

                int[] rowIndex = gridViewInvoiceItem.GetSelectedRows();
                int selectedRow = gridViewInvoiceItem.GetRowHandle(rowIndex[0]);

                ItemForDelete = (DataTable)gridControlInvoiceItem.DataSource;


                DataRow[] foundRow = ItemTableTemp.Select("item_name='" + ItemForDelete.Rows[selectedRow]["item_name"].ToString()+"'");

                if (foundRow.Length > 0) {
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
                // Re-calculate Total, Vat and Net.
                reCalculate();
            }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            DataTable DTInvoice = new DataTable();
            DataTable ItemTable = new DataTable();
            DataTable gridTable = new DataTable();
            DataTable DTDocInfo = new DataTable();
            DataTable InvoiceInfo = new DataTable();
            DataTable DTInvoiceItemNew = new DataTable();

            string InvoiceNO = "";
            int invoiceID = 0;
            double sumprice = 0;
            double sumprice_net = 0;
            double price_vat = 0;
            double item_sumprice = 0;
            double item_vatprice = 0;
            double item_netprice = 0;
            double item_priceperunit = 0;

            double total_price_of_item = 0;


            if (button_event == "update")
            {
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

                #region Re-Calculation

                InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(inv_trans_id_temp);
                DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

                DTInvoiceItemNew = ((DataTable)gridControlInvoiceItem.DataSource);
                for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                {
                    int item_id = DTInvoiceItemNew.Rows[itemcounter]["item_id"].To<int>();
                    string item_name = DTInvoiceItemNew.Rows[itemcounter]["item_name"].ToString();
                    double item_price_monthly = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    double item_price_weekly = 0;
                    double item_price_daily = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    string item_detail = "";
                    string item_vat = DTInvoiceItemNew.Rows[itemcounter]["item_vat"].ToString();
                    int item_type = DTInvoiceItemNew.Rows[itemcounter]["item_type"].To<int>();
                    string item_flag = DTInvoiceItemNew.Rows[itemcounter]["item_flag"].ToString();

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

                    if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                    {
                        if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                        {
                            item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                            item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                            item_netprice = item_sumprice;
                            item_sumprice = item_netprice - item_vatprice;
                        }

                        if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                        {
                            item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                            item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                            item_netprice = item_sumprice + item_vatprice;
                        }
                    }
                    else
                    {
                        item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_netprice = item_sumprice;
                    }

                    item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    total_price_of_item += item_netprice;

                    ItemTable.Rows.Add(inv_trans_id_temp, item_id, DTInvoiceItemNew.Rows[itemcounter]["item_name"].ToString(), item_price_monthly, item_price_daily, DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_type"].To<int>(), item_priceperunit, DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>(), item_sumprice, item_vatprice, item_netprice, DTInvoiceItemNew.Rows[itemcounter]["item_flag"].ToString());
                }

                BusinessLogicBridge.DataStore.removeInvoiceItemByInvoiceID(inv_trans_id_temp);

                BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);

                double sumGeneralCost = 0;
                for (int cost = 0; cost < ItemGeneralCost.Rows.Count; cost++)
                {
                    sumGeneralCost += ItemGeneralCost.Rows[cost]["item_netprice"].To<double>();
                }

                sumprice = sumGeneralCost + total_price_of_item;

                price_vat = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice; // 7 div 100 x price

                switch (DTDocInfo.Rows[0]["doc_vat_type"].To<int>())
                {
                    case 1:
                        // no vat
                        sumprice_net = sumprice;
                        price_vat = 0.0;
                        break;
                    case 2:
                        // with net
                        sumprice_net = sumprice;
                        price_vat = 0.0;
                        break;
                    case 3:
                        // increase from net
                        sumprice_net = sumprice + price_vat;
                        break;
                    default:
                        sumprice_net = sumprice;
                        break;
                }
                #endregion

                BusinessLogicBridge.DataStore.updateInvoicePriceByID(sumprice, price_vat, sumprice_net, inv_trans_id_temp);
                initLoadGridInvoice();

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
            }
            else
            {

                gridTable = ((DataTable)gridControlInvoiceList.DataSource);

                // cancel invoice by id & cancel item invoice
                BusinessLogicBridge.DataStore.setInvoiceCancel(inv_trans_id_temp);

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
                DTInvoice.Columns.Add("inv_trans_vatrate", typeof(decimal));

                ItemTable.Columns.Add("inv_trans_id", typeof(int));
                ItemTable.Columns.Add("item_id", typeof(int));
                ItemTable.Columns.Add("item_name", typeof(string));
                ItemTable.Columns.Add("item_price_monthly", typeof(double));
                ItemTable.Columns.Add("item_price_daily", typeof(double));
                ItemTable.Columns.Add("item_vat", typeof(string));
                ItemTable.Columns.Add("item_type", typeof(int));
                ItemTable.Columns.Add("item_priceperunit", typeof(double));
                ItemTable.Columns.Add("item_sumprice", typeof(double));
                ItemTable.Columns.Add("item_vatprice", typeof(double));
                ItemTable.Columns.Add("item_netprice", typeof(double));
                ItemTable.Columns.Add("item_flag", typeof(string));

                #endregion

                for (int i = 0; i < gridTable.Rows.Count; i++)
                {
                    if ((bool)(gridTable.Rows[i]["checkbox"]) == true)
                    {

                        DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(gridTable.Rows[i]["building_id"].To<int>());

                        InvoiceNO = DTDocInfo.Rows[0]["doc_inv_prefix"].ToString() + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genInvoiceNo().ToString().PadLeft(6, '0');

                        // create new Invoice transaction & item invoice
                        // Re-calculate
                        #region create New Invoice
                        // Select old transaction
                        InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(gridTable.Rows[i]["inv_trans_id"].To<int>());

                        #region Re-Calculation
                        for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                        {
                            if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                            {
                                if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                                {
                                    item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                    item_netprice = item_sumprice;
                                    item_sumprice = item_netprice - item_vatprice;
                                }

                                if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                                {
                                    item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                                    item_netprice = item_sumprice + item_vatprice;
                                }
                            }
                            else
                            {
                                item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<int>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                item_netprice = item_sumprice;
                                item_vatprice = 0.00;
                            }

                            item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                            total_price_of_item += item_netprice;
                        }

                        // sum price
                        sumprice = InvoiceInfo.Rows[0]["inv_trans_roomprice"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_emeter_price"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_wmeter_price"].To<double>() + InvoiceInfo.Rows[0]["inv_trans_phone_price"].To<double>() + total_price_of_item;  // Sum Plus Total Price of Items

                        price_vat = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice; // 7 div 100 x price

                        switch (DTDocInfo.Rows[0]["doc_vat_type"].To<int>())
                        {
                            case 1:
                                // no vat
                                sumprice_net = sumprice;
                                price_vat = 0.0;
                                break;
                            case 2:
                                // with net
                                sumprice_net = sumprice;
                                price_vat = 0.0;
                                break;
                            case 3:
                                // increase from net
                                sumprice_net = sumprice + price_vat;
                                break;
                            default:
                                sumprice_net = sumprice;
                                break;
                        }
                        #endregion

                        #region Add Datarow
                        DTInvoice.Rows.Add(
                                   InvoiceNO,
                                   InvoiceInfo.Rows[0]["inv_trans_cutduedate"],
                                   InvoiceInfo.Rows[0]["inv_trans_fixpaymentdate"],
                                   DateTime.Now,
                                   0,
                                   InvoiceInfo.Rows[0]["inv_trans_tenantname"],
                                   InvoiceInfo.Rows[0]["inv_trans_tenantaddress"],
                                   InvoiceInfo.Rows[0]["inv_trans_roomlabel"],
                                   InvoiceInfo.Rows[0]["inv_trans_building"],
                                   InvoiceInfo.Rows[0]["inv_trans_floor"],
                                   InvoiceInfo.Rows[0]["inv_trans_roomtype"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_previous_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_previous_energy"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_present_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_present_energy"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_unit"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_priceperunit"],
                                   InvoiceInfo.Rows[0]["inv_trans_emeter_price"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_previous_energy"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_present_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_present_energy"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_unit"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_priceperunit"],
                                   InvoiceInfo.Rows[0]["inv_trans_wmeter_price"],
                                   InvoiceInfo.Rows[0]["inv_trans_phone_start_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_phone_end_date"],
                                   InvoiceInfo.Rows[0]["inv_trans_phonemeter_unit"],
                                   InvoiceInfo.Rows[0]["inv_trans_phonemeter_priceperunit"],
                                   InvoiceInfo.Rows[0]["inv_trans_phone_price"],
                                   sumprice,
                                   price_vat,
                                   sumprice_net,
                                   0,
                                   InvoiceInfo.Rows[0]["inv_trans_room_id"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_name"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_logofile"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_address"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_telephone"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_fax"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_email"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_website"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_tax_id"],
                                   InvoiceInfo.Rows[0]["inv_trans_company_vision"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_header_invoice"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_footer_invoice"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_under_invoice1"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_under_invoice2"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_dateformat"],
                                   InvoiceInfo.Rows[0]["inv_trans_doc_logo_position"],
                                   InvoiceInfo.Rows[0]["inv_trans_roomprice"],
                                   InvoiceInfo.Rows[0]["inv_trans_vattype"],
                                   InvoiceInfo.Rows[0]["check_in_id"],
                                   InvoiceInfo.Rows[0]["check_in_contracttype"],
                                   DTDocInfo.Rows[0]["doc_vat"].To<decimal>()
                                   );
                        #endregion


                        invoiceID = BusinessLogicBridge.DataStore.createInvoiceTransaction(DTInvoice);
                        DTInvoice.Clear();

                        #endregion

                        #region ItemList Create
                        if (invoiceID > 0)
                        {
                            for (int itemcounter = 0; itemcounter < DTInvoiceItemNew.Rows.Count; itemcounter++)
                            {
                                if (DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                                {
                                    // Have Vat
                                    item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<double>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_netprice = item_sumprice + item_vatprice;
                                }
                                else
                                {
                                    item_sumprice = DTInvoiceItemNew.Rows[itemcounter]["item_amount"].To<double>() * DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                    item_netprice = item_sumprice;
                                }

                                item_priceperunit = DTInvoiceItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                                total_price_of_item += item_netprice;

                                // AddItem
                                ItemTable.Rows.Add(invoiceID, DTInvoiceItemNew.Rows[itemcounter]["item_id"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_name"].ToString(), 0, 0, DTInvoiceItemNew.Rows[itemcounter]["item_vat"].To<int>(), DTInvoiceItemNew.Rows[itemcounter]["item_type"].To<int>(), item_priceperunit, item_sumprice, item_vatprice, item_netprice, DTInvoiceItemNew.Rows[itemcounter]["item_flag"].ToString());
                            }
                            BusinessLogicBridge.DataStore.createInvoiceItem(ItemTable);

                        }
                        #endregion

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        initLoadGridInvoice();
                    }
                }
            }
            bttAddItem.Enabled = false;
            bttDelItem.Enabled = false;
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            groupControlListInvoice.Enabled = true;
            panelControlBttControl.Enabled = true;

        }

        private void bttCancel_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")))
            {
                bttAddItem.Enabled = false;
                bttDelItem.Enabled = false;

                ItemTableTemp = null;

                loadItem();

                bttEdit.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                groupControlListInvoice.Enabled = true;
                panelControlBttControl.Enabled = true;
            }
            
        }

        private void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceListByBuildingLabel(lookUpEditBuilding.Text);
            InvoiceInfo.Columns.Add("checkbox", typeof(bool));
            InvoiceInfo.Columns.Add("inv_trans_status_text");
            InvoiceInfo.Columns.Add("inv_trans_print_status_text");

            for (int i = 0; i < InvoiceInfo.Rows.Count; i++)
            {
                InvoiceInfo.Rows[i]["checkbox"] = false;

                if (InvoiceInfo.Rows[i]["inv_trans_status"].ToString() == "0")
                {
                    // Unpaid
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "รอชำระเงิน";
                    bttPaid.Enabled = true;
                }

                else if (InvoiceInfo.Rows[i]["inv_trans_status"].ToString() == "1")
                {
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "จ่ายแล้ว";
                    bttPaid.Enabled = false;
                }

                else
                {
                    InvoiceInfo.Rows[i]["inv_trans_status_text"] = "ยกเลิก";
                    bttCancelInvoice.Enabled = false;
                }

                if (InvoiceInfo.Rows[i]["inv_trans_print_status"].ToString() == "0")
                {
                    // UnPrint
                    InvoiceInfo.Rows[i]["inv_trans_print_status_text"] = "ยังไม่พิมพ์";
                }
                else
                {
                    InvoiceInfo.Rows[i]["inv_trans_print_status_text"] = "พิมพ์แล้ว";
                }

            }
            gridControlInvoiceList.DataSource = InvoiceInfo;
        }

    }
}
