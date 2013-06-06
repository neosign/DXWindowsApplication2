using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DXWindowsApplication2.PrintDocuments;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;

namespace DXWindowsApplication2.UserForms
{   
    public partial class ListReceipt : uBase
    {
        private Boolean _CheckRoom = false;
        private DataTable RoomTable;
        private int room_check_count = 0;
        private DataTable ItemForDelete;
        public static DataTable ItemTableTemp;
        public static DataTable DTDocInfo;
        public static DataTable dataItemsForCheck;
        public static DataTable ItemGeneralCost;
        public static int rec_trans_id;
        public static int counterItem;
        private int RecTransCategory;
        public static string event_button = "";

        private PrinterSettings prnSettings;
        
        public ListReceipt()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            ItemTableTemp = null;
            this.Load += new EventHandler(ListReceipt_Load);
            this.Resize += new EventHandler(ListReceipt_Resize);
            gridViewReciept.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewReciept_FocusedRowChanged);
            gridViewReciept.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewReciept_RowClick);
            lookUpEditVatType.EditValueChanged += new EventHandler(lookUpEditVatType_EditValueChanged);
            dateEditFrom.EditValueChanged += new EventHandler(dateEditFrom_EditValueChanged);
            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
            dateEditTo.EditValueChanged += new EventHandler(dateEditTo_EditValueChanged);
            dateEditFrom.EditValueChanged +=new EventHandler(dateEditFrom_EditValueChanged);

            lookUpEditRoom_label.EditValueChanged += new EventHandler(lookUpEditRoom_label_EditValueChanged);
            SaveClick += new EventHandler(bttSave_Click);
        }

        void gridViewReciept_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ItemTableTemp = null;
            ItemForDelete = null;

            int[] rowIndex = gridViewReciept.GetSelectedRows();
            if (rowIndex.Length <= 0)
                return;
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewReciept.GetDataRow(rowIndex[0]);
                    rec_trans_id = Convert.ToInt32(CurrentRow["rec_trans_id"].ToString());

                    DataTable RecieptInfo = BusinessLogicBridge.DataStore.getRecieptById(rec_trans_id);
                    var Reciept = RecieptInfo.Rows[0];

                    DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(Reciept["rec_trans_building"].ToString());

                    if (Reciept["rec_trans_type"].ToString() == "True" && Reciept["rec_trans_print_status"].To<int>() == 0)
                    {
                        // manual
                        bttEdit.Enabled = true;
                    }
                    else
                    {
                        bttEdit.Enabled = false;
                    }

                    RecTransCategory = Reciept["rec_trans_category"].To<int>();

                    if (RecTransCategory == 2)
                    {
                        loadItemReciept(RecieptInfo);
                    }
                    else if (RecTransCategory == 1)
                    {
                        loadItemBooking(RecieptInfo);
                    }
                    else
                    {
                        if (Reciept["check_in_id"].To<int>() != 0)
                            loadItemInvoice(RecieptInfo);
                        else
                            gridControlGeneralCost.DataSource = null;
                    }

                    textEditReciept_number.EditValue = Reciept["rec_trans_number"];
                    dateEditReciept_datecreated.EditValue = Reciept["rec_trans_datecreated"];
                    textEditInvoice_number.EditValue = Reciept["inv_trans_number"];

                    lookUpEditRoom_label.EditValue = Reciept["rec_trans_room_id"];

                    textEditBuilding.EditValue = Reciept["rec_trans_building"];
                    textEditFloor.EditValue = Reciept["rec_trans_floor"];
                    textEditRoomType.EditValue = Reciept["rec_trans_roomtype"];

                    char[] delimiters = new char[] { '|', '|' };

                    string[] TenantNameSplited = Reciept["rec_trans_tenantname"].ToString().Split(delimiters);

                    if (TenantNameSplited.Length > 3)
                    {
                        textEditFirstname.EditValue = TenantNameSplited[2].ToString() + " " + TenantNameSplited[4].ToString();
                    }
                    else
                    {
                        textEditFirstname.EditValue = TenantNameSplited[2].ToString();
                    }

                    int prefix_id = BusinessLogicBridge.DataStore.getPrefixByName(TenantNameSplited[0].ToString(), current_lang);

                    lookUpEditPrefixName.EditValue = prefix_id;


                    textEditLastname.EditValue = Reciept["rec_trans_tenantname"];
                    memoEditTenantAddress.EditValue = Reciept["rec_trans_tenantaddress"];

                    textEditVatRate.EditValue = DTDocInfo.Rows[0]["doc_vat"].To<double>().ToString("N2");//(Reciept["rec_trans_sumprice_withvat"].To<double>() * 100) / Reciept["rec_trans_sumprice"].To<double>();

                    if (textEditVatRate.EditValue.ToString() == "NaN")
                    {
                        textEditVatRate.EditValue = 0.00;
                    }

                    lookUpEditVatType.EditValue = Reciept["rec_trans_vattype"].To<int>();

                    lbVat.Text = getLanguage("_vat") + " " + textEditVatRate.EditValue.To<double>().ToString("N2") + " % ";

                    textEditSumPrice.EditValue = Reciept["rec_trans_sumprice"].To<double>();
                    textEditVatPriceReceipt.EditValue = Reciept["rec_trans_sumprice_withvat"].To<double>();
                    textEditNetPrice.EditValue = Reciept["rec_trans_sumprice_net"].To<double>();
                    initItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            initDropDownPrefix();
            initDropDownBuilding();
            initDropdownDateFrom();
            LoadListReciept();
            initRoomDropDrown();
            initDropDownVatType();
            setLangThis();
        }

        void lookUpEditVatType_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditVatType.EditValue.To<int>() == 3)
            {

                //DataTable DocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.Text.Trim());

                textEditVatRate.EditValue = DTDocInfo.Rows[0]["doc_vat"].To<double>().ToString("N2");

            }
            else {
                textEditVatRate.EditValue = 0;
            }
            if(textEditBuilding.EditValue!=null)
            reCalculate();
        }

        void lookUpEditRoom_label_EditValueChanged(object sender, EventArgs e)
        {
            string Building_label ="";

            DataTable BuildingInfo = BusinessLogicBridge.DataStore.getBuildingByRoomID(lookUpEditRoom_label.EditValue.To<int>());

            Building_label = BuildingInfo.Rows[0]["building_label"].ToString();

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(Building_label);

            lookUpEditVatType.EditValue = DTDocInfo.Rows[0]["doc_vat_type"].To<int>();

            textEditBuilding.EditValue = Building_label;

            DataTable floorDT =  BusinessLogicBridge.DataStore.getFloorByBuildingId(BuildingInfo.Rows[0]["building_id"].To<int>());
            DataTable roomtypeDT =  BusinessLogicBridge.DataStore.getRoomTypeByRoomID(lookUpEditRoom_label.EditValue.To<int>());

            textEditFloor.EditValue = floorDT.Rows[0]["floor_code"].ToString();
            textEditRoomType.EditValue = roomtypeDT.Rows[0]["roomtype_label"].ToString();

        }

        void dateEditTo_EditValueChanged(object sender, EventArgs e)
        {

            //int FromMonth = dateEditTo.EditValue.To<DateTime>().Month - 3;

            //if (FromMonth <=0)
            //{
            //    dateEditFrom.EditValue = new DateTime((dateEditTo.EditValue.To<DateTime>().Year - 1), 12 + FromMonth, dateEditTo.EditValue.To<DateTime>().Day, 0, 0, 0, 0);
            //}else{
            //    dateEditFrom.EditValue = new DateTime(dateEditTo.EditValue.To<DateTime>().Year, FromMonth, dateEditTo.EditValue.To<DateTime>().Day, 0, 0, 0, 0);
            //}
            if (lookUpEditBuilding.EditValue != null)
            LoadListReciept();
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEditFrom.EditValue != null && dateEditTo.EditValue !=null)
            LoadListReciept();
        }

        void dateEditFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEditFrom.EditValue != null)
            {
                try
                {
                    dateEditTo.EditValue = DateTime.Now;
                    dateEditTo.Properties.MinValue = dateEditFrom.EditValue.To<DateTime>();
                }
                catch { }
            }

            if (lookUpEditBuilding.EditValue != null)
            LoadListReciept();
            
        }

        void ListReceipt_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
        }

        void loadItemReciept(DataTable RecieptInfo)
        {
            ItemGeneralCost = new DataTable();

            ItemGeneralCost.Columns.Add("order", typeof(int));
            ItemGeneralCost.Columns.Add("item_name", typeof(string));
            ItemGeneralCost.Columns.Add("item_amount", typeof(string));
            ItemGeneralCost.Columns.Add("item_priceperunit", typeof(string));
            ItemGeneralCost.Columns.Add("item_sumprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vatprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_netprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vat_bool", typeof(bool));

            double advance_price = RecieptInfo.Rows[0]["rec_trans_advance"].To<double>() * RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>();

            ItemGeneralCost.Rows.Add(1, "ค่าเช่าล่วงหน้า", RecieptInfo.Rows[0]["rec_trans_advance"].To<int>(), RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>(), advance_price, 0.0, advance_price, false);
            ItemGeneralCost.Rows.Add(2, "ค่าประกัน", 1, RecieptInfo.Rows[0]["rec_trans_insurance"].To<double>(), RecieptInfo.Rows[0]["rec_trans_insurance"].To<double>(), 0.0, RecieptInfo.Rows[0]["rec_trans_insurance"].To<double>(), false);
            ItemGeneralCost.Rows.Add(3, "เงินจอง (จ่ายแล้ว)", 1, -RecieptInfo.Rows[0]["rec_trans_booking"].To<double>(), -RecieptInfo.Rows[0]["rec_trans_booking"].To<double>(), 0.0,RecieptInfo.Rows[0]["rec_trans_booking"].To<double>(), false);

            gridControlGeneralCost.DataSource = ItemGeneralCost;
        }

        void loadItemBooking(DataTable RecieptInfo)
        {
            ItemGeneralCost = new DataTable();

            ItemGeneralCost.Columns.Add("order", typeof(int));
            ItemGeneralCost.Columns.Add("item_name", typeof(string));
            ItemGeneralCost.Columns.Add("item_amount", typeof(double));
            ItemGeneralCost.Columns.Add("item_priceperunit", typeof(double));
            ItemGeneralCost.Columns.Add("item_sumprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vatprice", typeof(double));
            ItemGeneralCost.Columns.Add("item_vat_bool", typeof(bool));

            ItemGeneralCost.Rows.Add(1, "เงินจอง", 1.0, RecieptInfo.Rows[0]["rec_trans_booking"].To<double>(), RecieptInfo.Rows[0]["rec_trans_booking"].To<double>(), 0.0, false);
            
            gridControlGeneralCost.DataSource = ItemGeneralCost;
        }

        void loadItemInvoice(DataTable RecieptInfo)
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

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceByInvoiceNumber(RecieptInfo.Rows[0]["inv_trans_number"].ToString());

           

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

            gridControlGeneralCost.DataSource = ItemGeneralCost;
            
        }

        void gridViewReciept_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ItemTableTemp = null;
            ItemForDelete = null;

            int[] rowIndex = gridViewReciept.GetSelectedRows();
            if (rowIndex.Length <= 0)
                return;
            try
            {
                if (rowIndex[0] >= 0)
                {   
 
                    DataRow CurrentRow = gridViewReciept.GetDataRow(rowIndex[0]);
                    rec_trans_id = Convert.ToInt32(CurrentRow["rec_trans_id"].ToString());

                    DataTable RecieptInfo = BusinessLogicBridge.DataStore.getRecieptById(rec_trans_id);
                    var Reciept = RecieptInfo.Rows[0];

                    DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(Reciept["rec_trans_building"].ToString());


                    RecTransCategory = Reciept["rec_trans_category"].To<int>();

                    if (RecTransCategory == 2)
                    {
                        loadItemReciept(RecieptInfo);
                    }
                    else if (RecTransCategory == 1)
                    {
                        loadItemBooking(RecieptInfo);
                    }
                    else {
                        if (Reciept["check_in_id"].To<int>()!=0)
                            loadItemInvoice(RecieptInfo);
                        else
                            gridControlGeneralCost.DataSource = null;
                    }

                    textEditReciept_number.EditValue = Reciept["rec_trans_number"];
                    dateEditReciept_datecreated.EditValue = Reciept["rec_trans_datecreated"];
                    textEditInvoice_number.EditValue = Reciept["inv_trans_number"];

                    lookUpEditRoom_label.EditValue = Reciept["rec_trans_room_id"];

                    textEditBuilding.EditValue = Reciept["rec_trans_building"];
                    textEditFloor.EditValue = Reciept["rec_trans_floor"];
                    textEditRoomType.EditValue = Reciept["rec_trans_roomtype"];

                    char[] delimiters = new char[] { '|', '|' };

                    string[] TenantNameSplited = Reciept["rec_trans_tenantname"].ToString().Split(delimiters);

                    if (TenantNameSplited.Length > 3)
                    {
                        textEditFirstname.EditValue = TenantNameSplited[2].ToString() + " " + TenantNameSplited[4].ToString();
                    }
                    else
                    {
                        textEditFirstname.EditValue = TenantNameSplited[2].ToString();
                    }

                    int prefix_id = BusinessLogicBridge.DataStore.getPrefixByName(TenantNameSplited[0].ToString(), current_lang);

                    lookUpEditPrefixName.EditValue = prefix_id;
                   

                    textEditLastname.EditValue = Reciept["rec_trans_tenantname"];
                    memoEditTenantAddress.EditValue = Reciept["rec_trans_tenantaddress"];

                    //textEditVatRate.EditValue = (Reciept["rec_trans_sumprice_withvat"].To<double>() * 100) / Reciept["rec_trans_sumprice"].To<double>();
                    textEditVatRate.EditValue = DTDocInfo.Rows[0]["doc_vat"].To<double>().ToString("N2");

                    if (textEditVatRate.EditValue.ToString() == "NaN") {
                        textEditVatRate.EditValue = 0.00;
                    }

                    lookUpEditVatType.EditValue = Reciept["rec_trans_vattype"].To<int>();

                    lbVat.Text = getLanguage("_vat") + " " + textEditVatRate.EditValue.To<double>().ToString("N2") + " % ";

                    textEditSumPrice.EditValue = Reciept["rec_trans_sumprice"].To<double>();
                    textEditVatPriceReceipt.EditValue = Reciept["rec_trans_sumprice_withvat"].To<double>();
                    textEditNetPrice.EditValue = Reciept["rec_trans_sumprice_net"].To<double>();

                    // List Items
                    DataTable RecieptItem = BusinessLogicBridge.DataStore.getRecieptItemsByRecieptId(rec_trans_id);

                    RecieptItem.Columns.Add("order", typeof(int));
                    RecieptItem.Columns.Add("item_vat_bool", typeof(bool));
                    int counter = 1;
                    for (int i = 0; i < RecieptItem.Rows.Count; i++)
                    {
                        RecieptItem.Rows[i]["order"] = counter;
                        if (RecieptItem.Rows[i]["item_vat"].ToString() != "1")
                        {
                            RecieptItem.Rows[i]["item_vat_bool"] = true;
                        }
                        else
                        {
                            RecieptItem.Rows[i]["item_vat_bool"] = false;
                        }
                        counter++;
                    }

                    if (Reciept["rec_trans_type"].ToString() == "True" && Reciept["rec_trans_print_status"].To<int>() == 0)
                    {
                        // manual
                        bttEdit.Enabled = true;
                    }
                    else
                    {
                        bttEdit.Enabled = false;
                    }

                    gridControlRecieptItem.DataSource = RecieptItem;

                    initItem();
                    //reCalculate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        void ListReceipt_Load(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
            initDropDownPrefix();
            initDropDownBuilding();
            initDropdownDateFrom();
            initRoomDropDrown();
            LoadListReciept();
            initDropDownVatType();
            setLangThis();
        }

        void setLangThis() {

            groupControlListReciept.Text = getLanguage("_receipt_list");
            lbFromdate.Text = getLanguage("_from");
            lbTodate.Text = getLanguage("_to");

            checkEditSelectAll.Text = getLanguage("_select_all");
            lbBuilding.Text = getLanguage("_building");

            colRecieptNumber.Caption = getLanguage("_reciept_no");
            gridColumnInvoiceNo.Caption = getLanguage("_invoice_no");
            colInvoiceDateCreate.Caption = getLanguage("_issue_date");
            colInvoiceRoomLabel.Caption = getLanguage("_room_name");
            colRentalName.Caption = getLanguage("_tenant_payer_first_name");
            colPrintStatusText.Caption = getLanguage("_printing_status");

            gbReciept.Text = getLanguage("_issue_reciept");

            lbReceiptNo.Text = getLanguage("_reciept_no");
            lbInvoiceNo.Text = getLanguage("_invoice_no");
            lbDateCreate.Text = getLanguage("_issue_date");

            gbRoom.Text = getLanguage("_room");
            lbRoomName.Text = getLanguage("_room_name");
            lbFloor.Text = getLanguage("_floor");
            lbBuilding2.Text = getLanguage("_building");
            lbType.Text = getLanguage("_type");

            gbTenant.Text = getLanguage("_tenant");
            lbPrefix.Text = getLanguage("_prefix");
            lbFirstname.Text = getLanguage("_firstname");
            lbLastname.Text = getLanguage("_lastname");
            lbAddress.Text = getLanguage("_address");
            lbTaxRate.Text = getLanguage("_tax_rate");
            lbVatType.Text = getLanguage("_tax_format");

            groupExpense.Text = getLanguage("_additional_cost");
            gridColumNo.Caption = getLanguage("_number");
            colInvoiceItemName.Caption = getLanguage("_item");
            gridColumnAmount.Caption = getLanguage("_unit");
            gridColumnAmountPerUnit.Caption = getLanguage("_price_per_unit");
            colInvoiceItemPrice.Caption = getLanguage("_price");
            gridColumnVat.Caption = getLanguage("_tax");
            gridColumnVating.Caption = getLanguage("_tax_format");

            bttAddItem.Text = getLanguage("_add");
            bttDelItem.Text = getLanguage("_delete");

            lbBaht.Text = getLanguage("_baht");
            lbBaht2.Text = getLanguage("_baht");
            lbBaht3.Text = getLanguage("_baht");

            lbSumPrice.Text = getLanguage("_total");

            if (textEditVatRate.EditValue == null) {
                lbVat.Text = getLanguage("_no_vat");
            }

            lbNet.Text = getLanguage("_net");

            bttEdit.Text = getLanguage("_edit");
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");

            bttPrint.Text = getLanguage("_print_receipt_all_selected");
            bttPrintTax.Text = getLanguage("_print_tax_invoice_all_selected");
            bttCreateReciept.Text = getLanguage("_issue_reciept");

        }

        void initDropDownPrefix()
        {
            
            
                DataTable prefixDT = BusinessLogicBridge.DataStore.getAllPrefix();

                DataTable prefixList = new DataTable();
                string prefixlabel = "prefix_" + current_lang + "_label";

                prefixList.Columns.Add(prefixlabel, typeof(string));
                prefixList.Columns.Add("prefix_id", typeof(int));

                for (int i = 0; i < prefixDT.Rows.Count; i++)
                {

                    prefixList.Rows.Add(prefixDT.Rows[i][prefixlabel].ToString(), prefixDT.Rows[i]["prefix_id"]);
                }
                lookUpEditPrefixName.Properties.DataSource = prefixList;

                

            if (lookUpEditPrefixName.EditValue == null)
            {
                if (lookUpEditPrefixName.Properties.Columns.Count == 0)
                {
                    lookUpEditPrefixName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(prefixlabel, 0, getLanguage("_prefix")));
                    lookUpEditPrefixName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("prefix_id", 0, "prefix_id"));
                    lookUpEditPrefixName.Properties.DisplayMember = prefixlabel;
                    lookUpEditPrefixName.Properties.ValueMember = "prefix_id";
                    lookUpEditPrefixName.Properties.Columns["prefix_id"].Visible = false;
                }

                lookUpEditPrefixName.Properties.NullText = getLanguage("_select_prefix");
            }
            
        }

        public void initDropDownVatType()
        {
            DataTable DTvattype = new DataTable();

            DTvattype.Columns.Add("vattype_label", typeof(string));
            DTvattype.Columns.Add("vattype_id", typeof(int));

            DTvattype.Rows.Add(getLanguage("_no_vat"), 1);
            DTvattype.Rows.Add(getLanguage("_with_the_net"), 2);
            DTvattype.Rows.Add(getLanguage("_increase_from_net"), 3);


            lookUpEditVatType.Properties.DataSource = DTvattype;
            lookUpEditVatType.Properties.DisplayMember = "vattype_label";
            lookUpEditVatType.Properties.ValueMember = "vattype_id";
            lookUpEditVatType.Properties.NullText = getLanguage("_tax_format_select");
        }

        public void initRoomDropDrown()
        {
            RoomTable = BusinessLogicBridge.DataStore.getAllRoom(0, "all");
            if (RoomTable.Rows.Count > 0)
            {
                lookUpEditRoom_label.Properties.DisplayMember = "room_label";
                lookUpEditRoom_label.Properties.ValueMember = "room_id";
                lookUpEditRoom_label.Properties.DataSource = RoomTable;
                lookUpEditRoom_label.EditValue = 1;
            }
        }

        void LoadListReciept(){

            DataTable RecieptDT = BusinessLogicBridge.DataStore.getRecieptListByBuildingLabel(lookUpEditBuilding.Text, dateEditFrom.EditValue.To<DateTime>(), dateEditTo.EditValue.To<DateTime>());

           

            if (RecieptDT.Rows.Count == 0)
            {
                bttPrint.Enabled = false;
                bttPrintTax.Enabled = false;
                bttEdit.Enabled = false;
            }

            if (RoomTable != null)
            {
                if (RoomTable.Rows.Count == 0)
                {
                    bttPrint.Enabled = false;
                    bttPrintTax.Enabled = false;
                    bttCreateReciept.Enabled = false;
                    bttEdit.Enabled = false;
                }
            }


            RecieptDT.Columns.Add("checkbox", typeof(bool));
            RecieptDT.Columns.Add("rec_trans_print_status_text", typeof(string));
            

            for (int i = 0; i < RecieptDT.Rows.Count; i++ )
            {   
                RecieptDT.Rows[i]["checkbox"] = false;

                char[] delimiters = new char[] { '|', '|' };

                string[] TenantNameSplited = RecieptDT.Rows[i]["rec_trans_tenantname"].ToString().Split(delimiters);

                if (TenantNameSplited.Length > 3)
                {
                    RecieptDT.Rows[i]["rec_trans_tenantname"] = TenantNameSplited[0] + " " + TenantNameSplited[2].ToString() + " " + TenantNameSplited[4].ToString();
                }
                else {
                    RecieptDT.Rows[i]["rec_trans_tenantname"] = TenantNameSplited[0] + " " + TenantNameSplited[2].ToString();
                }

                if (RecieptDT.Rows[i]["rec_trans_print_status"].To<int>() == 0)
                {
                    if (current_lang == "th")
                        RecieptDT.Rows[i]["rec_trans_print_status_text"] = "ยังไม่พิมพ์";
                    else
                        RecieptDT.Rows[i]["rec_trans_print_status_text"] = "UnPrint";
                }
                else {
                    if (current_lang == "th")
                        RecieptDT.Rows[i]["rec_trans_print_status_text"] = "พิมพ์แล้ว";
                    else
                        RecieptDT.Rows[i]["rec_trans_print_status_text"] = "Printed";
                }
            }

            gridControlReciept.DataSource = RecieptDT;

        }

        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            string pathname = "";
            string filePath = "";
            DataTable GeneralInfo = new DataTable();
            DataTable GridTableCheckbox = new DataTable();

            GridTableCheckbox = ((DataTable)gridControlReciept.DataSource);

            XtraReport1 report1 = new XtraReport1();
            report1.CreateDocument();

            for (int i = 0; i < GridTableCheckbox.Rows.Count; i++)
            {
                if ((bool)(GridTableCheckbox.Rows[i]["checkbox"]) == true)
                {

                    if (GridTableCheckbox.Rows[i]["rec_trans_type"].To<bool>()==true)
                    {
                        PrintDocuments.reciept_manual PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_manual();

                        GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                        pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");

                        PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());
                        PrintReciept.CreateDocument();
                        //PrintReciept.ExportToPdf(pathname);
                        //PrintReciept.ShowPreview();

                        report1.Pages.AddRange(PrintReciept.Pages);

                    }else{

                        if (GridTableCheckbox.Rows[i]["rec_trans_category"].To<int>() == 1)
                        {
                            PrintDocuments.reciept_booking PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_booking();
                            
                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");
                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());
                            PrintReciept.CreateDocument();
                            //PrintReciept.ExportToPdf(pathname);
                            //PrintReciept.ShowPreview();
                            report1.Pages.AddRange(PrintReciept.Pages);

                        }
                        else if (GridTableCheckbox.Rows[i]["rec_trans_category"].To<int>() == 2)
                        {
                            PrintDocuments.reciept_checkin PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_checkin();

                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");

                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());
                            PrintReciept.CreateDocument();
                            //PrintReciept.ExportToPdf(pathname);
                            //PrintReciept.ShowPreview();
                            report1.Pages.AddRange(PrintReciept.Pages);

                        }
                        else{
                            PrintDocuments.reciept PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept();
                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");


                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());
                            PrintReciept.CreateDocument();
                            //PrintReciept.ExportToPdf(pathname);
                            //PrintReciept.ShowPreview();

                            report1.Pages.AddRange(PrintReciept.Pages);
                        }
                    }

                    BusinessLogicBridge.DataStore.setRecieptPrint(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());
                }
            }

            report1.PrintingSystem.ContinuousPageNumbering = true;

            // Create a Print Tool and show the Print Preview form. 
            ReportPrintTool printTool = new ReportPrintTool(report1);
            printTool.ShowPreviewDialog();

            LoadListReciept();
        }

        private void simpleButtonPrintTax_Click(object sender, EventArgs e)
        {

            string pathname = "";
            string filePath = "";
            DataTable GeneralInfo = new DataTable();
            DataTable GridTableCheckbox = new DataTable();

            GridTableCheckbox = ((DataTable)gridControlReciept.DataSource);

            for (int i = 0; i < GridTableCheckbox.Rows.Count; i++)
            {
                if ((bool)(GridTableCheckbox.Rows[i]["checkbox"]) == true)
                {

                    if (GridTableCheckbox.Rows[i]["rec_trans_type"].To<bool>() == true)
                    {
                        PrintDocuments.reciept_tax_manual PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_tax_manual();

                        GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                        pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", "Tax_"+GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");


                        PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());

                        PrintReciept.ExportToPdf(pathname);
                        PrintReciept.ShowPreview();

                    }
                    else
                    {

                        if (GridTableCheckbox.Rows[i]["rec_trans_category"].To<int>() == 1)
                        {
                            PrintDocuments.reciept_tax_booking PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_tax_booking();

                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", "Tax_" + GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");


                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());

                            PrintReciept.ExportToPdf(pathname);
                            PrintReciept.ShowPreview();

                        }
                        else if (GridTableCheckbox.Rows[i]["rec_trans_category"].To<int>() == 2)
                        {
                            PrintDocuments.reciept_tax_checkin PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_tax_checkin();

                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", "Tax_"+GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");

                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());

                            PrintReciept.ExportToPdf(pathname);
                            PrintReciept.ShowPreview();

                        }
                        else
                        {
                            PrintDocuments.reciept_tax PrintReciept = new DXWindowsApplication2.PrintDocuments.reciept_tax();
                            GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
                            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            filePath = filePath + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

                            pathname = DXWindowsApplication2.MainForm.CombinePaths(filePath, "Reciept", "Tax_" + GridTableCheckbox.Rows[i].Table.Rows[0]["rec_trans_number"].ToString() + ".pdf");


                            PrintReciept.loopGenDataRow(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());

                            PrintReciept.ExportToPdf(pathname);
                            PrintReciept.ShowPreview();
                        }
                    }

                    //PrintInvoice.Print();

                    BusinessLogicBridge.DataStore.setRecieptPrint(GridTableCheckbox.Rows[i]["rec_trans_id"].To<int>());

                }
            }
            LoadListReciept();

        }

        public void initDropDownBuilding()
        {
            DataTable Buildings = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DataSource = Buildings;
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");

            if (Buildings.Rows.Count > 0)
            lookUpEditBuilding.EditValue = Buildings.Rows[0]["building_id"];

        }

        void initDropdownDateFrom()
        {   

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            int LastMonth = DateTime.Today.Month - 3;

            DateTime dtFix = new DateTime();

            if (LastMonth <= 0)
            {
                dtFix = new DateTime((DateTime.Today.Year - 1), 12, DateTime.Now.Day, 0, 0, 0, 0);
            }
            else
            {
                if(DateTime.DaysInMonth(DateTime.Today.Year, LastMonth) >= DateTime.Now.Day)
                    dtFix = new DateTime(DateTime.Today.Year, LastMonth, DateTime.Now.Day, 0, 0, 0, 0);
                else
                    dtFix = new DateTime(DateTime.Today.Year, LastMonth, DateTime.DaysInMonth(DateTime.Today.Year, LastMonth), 0, 0, 0, 0);

            }
            try
            {
                dateEditFrom.EditValue = dtFix;

                dateEditTo.EditValue = dt;
            }
            catch (Exception ex) { }
        }

        void setEnable() {

            lookUpEditRoom_label.Enabled = true;
            gbTenant.Enabled = true;
            lookUpEditVatType.Enabled = true;
            textEditVatPriceReceipt.Enabled = true;
            bttAddItem.Enabled = true;
            bttDelItem.Enabled = true;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            gridControlReciept.Enabled = false;
            bttEdit.Enabled = false;

            if (event_button == "add") {
                textEditFirstname.Properties.ReadOnly = false;
                memoEditTenantAddress.Enabled = true;
                memoEditTenantAddress.Properties.ReadOnly = false;
            }

        }
        
        void setEnableCreate()
        {

            lookUpEditRoom_label.Enabled = true;
            gbTenant.Enabled = true;
            lookUpEditVatType.Enabled = true;
            textEditVatPriceReceipt.Enabled = true;
            bttAddItem.Enabled = true;
            bttDelItem.Enabled = true;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            gridControlReciept.Enabled = false;
            bttEdit.Enabled = false;

            if (event_button == "add_manual") {

                DataTable Doc_info = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

                //Doc_info

                gridControlGeneralCost.DataSource = null;

                lookUpEditVatType.EditValue = Doc_info.Rows[0]["doc_vat_type"].To<int>();
                textEditVatRate.EditValue = Doc_info.Rows[0]["doc_vat"].To<double>().ToString("N2");

                textEditFirstname.Properties.ReadOnly = false;
                memoEditTenantAddress.Enabled = true;
                memoEditTenantAddress.Properties.ReadOnly = false;
            }
            textEditSumPrice.EditValue = 0;
            textEditNetPrice.EditValue = 0;
            textEditVatPriceReceipt.EditValue = 0;

            bttCreateReciept.Enabled = false;
            if (gridControlRecieptItem.DataSource!=null)
            ((DataTable)gridControlRecieptItem.DataSource).Clear();
            if (ItemTableTemp!=null)
            ItemTableTemp.Clear();

        }
        
        void setEnableUpdateOnly() {
            bttAddItem.Enabled = true;
            bttDelItem.Enabled = true;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            gridControlReciept.Enabled = false;
            bttEdit.Enabled = false;
        }

        void setDisable()
        {

            lookUpEditRoom_label.Enabled = false;
            gbTenant.Enabled = false;
            lookUpEditVatType.Enabled = false;
            textEditVatPriceReceipt.Enabled = false;
            bttAddItem.Enabled = false;
            bttDelItem.Enabled = false;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            gridControlReciept.Enabled = true;
            bttEdit.Enabled = true;
            bttCreateReciept.Enabled = true;
            event_button = "";
        }

        void CreateReciept() {

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
            DTReciept.Columns.Add("rec_trans_type", typeof(int));            

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

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.Text);

            if (DTDocInfo.Rows[0]["doc_saperate_reciept"].ToString() != "0")
            {
                RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
            }

            // Insurance // Booking // Advance  // Room Price
            double totalprice = textEditNetPrice.EditValue.To<double>();

            ThaiBaht = MainForm.ThaiBaht(totalprice.ToString());

            DTReciept.Rows.Add(
                   RecieptNO,
                   System.DBNull.Value,
                   System.DBNull.Value,
                   DateTime.Now,
                   1,
                   lookUpEditPrefixName.Text + "||" + textEditFirstname.Text,
                   memoEditTenantAddress.EditValue.ToString(),
                   lookUpEditRoom_label.Text,
                   textEditBuilding.Text,
                   textEditFloor.Text,
                   textEditRoomType.Text,
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
                   0,
                   0,
                   0,
                   0,
                   lookUpEditRoom_label.EditValue.To<int>(),
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
                   0.00,
                   DTDocInfo.Rows[0]["doc_vat_type"],
                   "-",
                   ThaiBaht,
                   0,
                   3,
                   1
               );

            // Keep all to print reciept
            // loop insert to Reciept Item Table
            recieptID = BusinessLogicBridge.DataStore.createRecieptTransaction(DTReciept);

            // loop insert to Reciept Item Table
            #region ItemList Create
            if (recieptID > 0)
            {   


                BusinessLogicBridge.DataStore.updateRecieptType(recieptID);
                DataTable ItemNew = ((DataTable)gridControlRecieptItem.DataSource);

                int item_id = 0;
                string item_name = "";
                double item_price_monthly = 0;
                double item_price_weekly = 0;
                double item_price_daily = 0;
                string item_detail = "";
                string item_vat = "";
                int item_type = 0;
                string item_flag = "";

                for (int itemcounter = 0; itemcounter < ItemNew.Rows.Count; itemcounter++)
                {

                    if(event_button =="add_manual"){
                        if (ItemNew.Columns.Contains("rec_trans_id") == false) {
                            ItemNew.Columns.Add("rec_trans_id", typeof(int));
                        }

                        ItemNew.Rows[itemcounter]["rec_trans_id"] = recieptID;
                    }

                    item_id = Convert.ToInt32(ItemNew.Rows[itemcounter]["item_id"].ToString());
                    item_name = ItemNew.Rows[itemcounter]["item_name"].ToString();
                    item_price_monthly = ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    item_price_weekly = 0;
                    item_price_daily = ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();

                    if (ItemNew.Columns.Contains("item_detail") == true)
                        item_detail = ItemNew.Rows[itemcounter]["item_detail"].ToString();
                    else
                        item_detail = "";
                    
                    item_vat = ItemNew.Rows[itemcounter]["item_vat"].ToString();
                    item_type = 2;
                    item_flag = ItemNew.Rows[itemcounter]["item_flag"].ToString();
                    
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

                        ItemNew.Rows[itemcounter]["item_id"] = item_id;
                    }


                    if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                    {
                        // Have Vat

                        if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                        {
                            item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                            item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                            item_netprice = item_sumprice;
                            item_sumprice = item_netprice - item_vatprice;
                        }

                        if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                        {
                            item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                            item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                            item_netprice = item_sumprice + item_vatprice;
                        }
                    }
                    else
                    {
                        item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_vatprice = 0.00;
                        item_netprice = item_sumprice;
                    }

                    item_priceperunit = ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    total_sum_price_of_item += item_sumprice; // Total All Items
                    total_vat_price_of_item += item_vatprice; // Total All Items
                    total_net_price_of_item += item_netprice; // Total All Items
                }
                BusinessLogicBridge.DataStore.createRecieptItem(ItemNew);

                sumprice = total_sum_price_of_item;    // Sum Total Price of Items
                price_vat = total_vat_price_of_item; // Vat Total Price of Items
                sumprice_net = total_net_price_of_item; // Net Total Price of Items


                ThaiBaht = MainForm.ThaiBaht(sumprice_net.ToString());

                BusinessLogicBridge.DataStore.updateRecieptPriceByID(sumprice, price_vat, sumprice_net, ThaiBaht, recieptID);
            }
            #endregion

        }

        void UpdateItemReciept() {

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

            DataTable ItemNew = ((DataTable)gridControlRecieptItem.DataSource);

            if (ItemNew.Columns.Contains("rec_trans_id")==false)
            ItemNew.Columns.Add("rec_trans_id", typeof(int));
            
            for (int itemcounter = 0; itemcounter < ItemNew.Rows.Count; itemcounter++)
            {
                ItemNew.Rows[itemcounter]["rec_trans_id"] = rec_trans_id;

                int item_id = ItemNew.Rows[itemcounter]["item_id"].To<int>();

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
                    _tmp.Rows.Add(ItemNew.Rows[itemcounter]["item_name"].ToString(), ItemNew.Rows[itemcounter]["item_price_monthly"].To<double>(), 0.0, ItemNew.Rows[itemcounter]["item_price_daily"].To<double>(), "", ItemNew.Rows[itemcounter]["item_vat"].To<int>(), ItemNew.Rows[itemcounter]["item_type"].To<int>(), "manual");
                    //
                    item_id = BusinessLogicBridge.DataStore.BasicInfoItem_insert(_tmp);
                }

                ItemNew.Rows[itemcounter]["item_id"] = item_id;


                if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                {
                    // Have Vat

                    if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                    {
                        item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                        item_netprice = item_sumprice;
                        item_sumprice = item_netprice - item_vatprice;
                    }

                    if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                    {
                        item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                        item_netprice = item_sumprice + item_vatprice;
                    }
                }
                else
                {
                    item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    item_vatprice = 0.00;
                    item_netprice = item_sumprice;
                }

                item_priceperunit = ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                total_sum_price_of_item += item_sumprice; // Total All Items
                total_vat_price_of_item += item_vatprice; // Total All Items
                total_net_price_of_item += item_netprice; // Total All Items
            }
            BusinessLogicBridge.DataStore.RemoveRecieptItem(rec_trans_id);

            BusinessLogicBridge.DataStore.createRecieptItem(ItemNew);

            sumprice = total_sum_price_of_item;    // Sum Total Price of Items
            price_vat = total_vat_price_of_item; // Vat Total Price of Items
            sumprice_net = total_net_price_of_item; // Net Total Price of Items


            string ThaiBaht = MainForm.ThaiBaht(sumprice_net.ToString());

            BusinessLogicBridge.DataStore.updateRecieptPriceByID(sumprice, price_vat, sumprice_net, ThaiBaht, rec_trans_id);

        }

        private void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
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
            if (gridViewReciept.RowCount > 0)
            {
                for (int i = 0; i < gridViewReciept.RowCount; i++)
                {
                    gridViewReciept.Columns[0].View.SetRowCellValue(i, "checkbox", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }

        private void CreateBooklet()
        {
            // Create the 1st report and generate its document.
            //XtraReport1 report1 = new XtraReport1();
            //report1.CreateDocument();

            // Preserve original page numbers on all pages.
           // report1.PrintingSystem.ContinuousPageNumbering = false;

            PrintDocuments.contract report1 = new DXWindowsApplication2.PrintDocuments.contract();

           
            // Create a booklet.
            int centerPageIndex = Convert.ToInt32((report1.Pages.Count - 1) / 2);
            for (int i = 0; i < centerPageIndex; i++)
            {
                report1.Pages.Insert(i * 2 + 1, report1.Pages[report1.Pages.Count - 1]);
            }

            // Create a Print Tool and show the Print Preview form.
            ReportPrintTool printTool = new ReportPrintTool(report1);
            printTool.ShowPreviewDialog();
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            event_button = "update";
            setEnableUpdateOnly();
        }

        private void bttCreateReciept_Click(object sender, EventArgs e)
        {
            string RecieptNO = "";

            DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

            if (DTDocInfo.Rows[0]["doc_saperate_reciept"].ToString() != "0")
            {
                RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
            }
            
            textEditReciept_number.EditValue = RecieptNO;
            event_button = "add_manual";
            setEnableCreate();
            initItem();
        }

        private void bttAddItem_Click(object sender, EventArgs e)
        {
            if (ItemTableTemp.Columns.Contains("rec_trans_id") == true) ItemTableTemp.Columns.Remove("rec_trans_id");
            ItemTableTemp = utilClass.showPopAddRecieptExpense(this, ItemTableTemp);

            
            ItemForDelete = null;
            //
            initItem();
            reCalculate();
        }

        void reCalculate()
        {

            DataTable DTInvoice = new DataTable();
            DataTable ItemTable = new DataTable();
            DataTable gridTable = new DataTable();
            DataTable DTDocInfo = new DataTable();
            DataTable RecieptInfo = new DataTable();
            double sumprice = 0;
            double sumprice_net = 0;
            double price_vat = 0;
            double item_sumprice = 0;
            double item_vatprice = 0;
            double item_netprice = 0;
            double item_priceperunit = 0;

            double total_sum_price_of_item = 0;
            double total_vat_price_of_item = 0;
            double total_net_price_of_item = 0;

            RecieptInfo = BusinessLogicBridge.DataStore.getRecieptById(rec_trans_id);
            if (textEditBuilding.EditValue!=null)
            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

            // Item Cost
            DataTable ItemNew = ((DataTable)gridControlRecieptItem.DataSource);

            if (ItemNew == null) return;

            for (int itemcounter = 0; itemcounter < ItemNew.Rows.Count; itemcounter++)
            {
                if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() != 1)
                {
                    // Have Vat
                    if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 2)
                    {
                        item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                        item_netprice = item_sumprice;
                        item_sumprice = item_netprice - item_vatprice;
                    }

                    if (ItemNew.Rows[itemcounter]["item_vat"].To<int>() == 3)
                    {
                        item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                        item_vatprice = (DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                        item_netprice = item_sumprice + item_vatprice;
                    }
                    
                }
                else
                {
                    item_sumprice = ItemNew.Rows[itemcounter]["item_amount"].To<int>() * ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                    item_vatprice = 0.00;
                    item_netprice = item_sumprice;
                }

                item_priceperunit = ItemNew.Rows[itemcounter]["item_priceperunit"].To<double>();
                total_sum_price_of_item += item_sumprice;
                total_vat_price_of_item += item_vatprice;
                total_net_price_of_item += item_netprice;
            }


           // if (event_button == "add_manual") {
                textEditVatPriceReceipt.EditValue = total_vat_price_of_item;
                textEditNetPrice.EditValue = total_net_price_of_item;
                textEditSumPrice.EditValue = total_sum_price_of_item;
            //}
        }

        void initItem()
        {
            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());
            DataTable ItemTable = new DataTable();
            if (event_button == "add_manual")
            {
                ItemTable.Columns.Add("order", typeof(int));
                if (ItemTable.Columns.Contains("item_name") == false)
                    ItemTable.Columns.Add("item_name", typeof(string));
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
                if (ItemTable.Columns.Contains("item_vat") == false)
                    ItemTable.Columns.Add("item_vat", typeof(int));

                if (ItemTableTemp != null)
                    ItemTable = ItemTableTemp.Clone();
                else
                    ItemTableTemp = ItemTable.Clone();
            }
            else
            {
                ItemTable = BusinessLogicBridge.DataStore.getRecieptItemsByRecieptId(rec_trans_id);

                if (ItemTable.Columns.Contains("item_price_weekly") == true)
                    ItemTable.Columns.Remove("item_price_weekly");
                if (ItemTable.Columns.Contains("item_detail") == true)
                    ItemTable.Columns.Remove("item_detail");
                if (ItemTable.Columns.Contains("item_createdate") == true)
                    ItemTable.Columns.Remove("item_createdate");

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

            }


            if (RecTransCategory == 2) {
                counterItem = 4;
            }
            else if (RecTransCategory == 1)
            {
                counterItem = 2;
            }
            else {
                counterItem = 5;
            }

            if (event_button == "add_manual") {
                counterItem = 1;
            }
            
            //
            //if (ItemTableTemp != null)
            //    if (ItemTableTemp.Rows.Count > 0)
            //    ItemTable.Merge(ItemTableTemp);
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


            for (int i = 0; i < ItemTable.Rows.Count; i++)
            {
                ItemTable.Rows[i]["order"] = counterItem;

                if (ItemTable.Rows[i]["item_vat"].ToString() != "1")
                {
                    ItemTable.Rows[i]["item_vat_bool"] = true;
                }
                else
                {
                    ItemTable.Rows[i]["item_vat_bool"] = false;
                }

                counterItem++;
            }

            //
            if (ItemTableTemp == null)
                ItemTableTemp = ItemTable.Clone();
            //
            if ((event_button == "add_manual" || event_button == "update") && ItemForDelete==null)
            {
                DataTable CheckOutItemTable = MainForm.VatCalculate(ItemTable);

                gridControlRecieptItem.DataSource = CheckOutItemTable;

                dataItemsForCheck = CheckOutItemTable;

                CheckOutItemTable.Dispose();
                bttDelItem.Enabled = true;
            }
            else {
                gridControlRecieptItem.DataSource = ItemTable;

                dataItemsForCheck = ItemTable;
                
                if (ItemForDelete != null)
                    bttDelItem.Enabled = true;
                else
                    bttDelItem.Enabled = false;
            }
            //
        }

        private DataTable ValidateData()
        {
            DataTable _Validate = new DataTable();
            //_Validate.Columns.Add("object", typeof(Object));
            _Validate.Columns.Add("label", typeof(String));
            _Validate.Columns.Add("message", typeof(String));
            if (textEditFirstname.EditValue.ToString().Length < 1)
            {
                _Validate.Rows.Add(lbFirstname.Text, getLanguage("_msg_1001"));
            }
            

            if (memoEditTenantAddress.EditValue.ToString().Length < 1)
            {
                _Validate.Rows.Add(lbAddress.Text, getLanguage("_msg_1001"));
            }
            
            return _Validate;
        }

        private void bttDelItem_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                // Delete Item

                int[] rowIndex = gridViewRecieptItem.GetSelectedRows();
                int selectedRow = gridViewRecieptItem.GetRowHandle(rowIndex[0]);


                ItemForDelete = (DataTable)gridControlRecieptItem.DataSource;


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

                if (ItemForDelete.Rows.Count == 0) {
                    ItemTableTemp.Clear();
                }

                initItem();
                // Re-calculate Total, Vat and Net.
                reCalculate();
            }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {

            DataTable _Validate = ValidateData();
            if (_Validate.Rows.Count > 0)
            {
                String message = "";
                for (int i = 0; i < _Validate.Rows.Count; i++)
                {
                    message = message + _Validate.Rows[i]["label"] + " " + _Validate.Rows[i]["message"].ToString() + "\r\n";
                }
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }

            if (event_button == "add" || event_button == "add_manual")
            {
                CreateReciept();
            }
            else {

                UpdateItemReciept();
            }

            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
            ItemTableTemp.Clear();
            LoadListReciept();
            setDisable();
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")))
            {
                LoadListReciept();
                setDisable();

            }
        }

    }
}
