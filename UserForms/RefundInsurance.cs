using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DXWindowsApplication2.UserForms
{
    public partial class RefundInsurance : uBase
    {
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public static GridView gridViewNick;

        public int counterItem;
        public DataTable ItemGeneralCost;
        public DataTable ItemTableTemp;

        private int invoice_id_temp = 0;

        public RefundInsurance()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            
            this.Load += new EventHandler(RefundInsurance_Load);
            this.Resize += new EventHandler(RefundInsurance_Resize);
            gridViewRefund.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewRefund_FocusedRowChanged);

        }

        public override void Refresh()
        {
            base.Refresh();
            splitContainerControl2.SplitterPosition = (this.Width * 60) / 100;
            reloadGrid();
        }

        void RefundInsurance_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 60) / 100;
        }

        void gridViewRefund_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow CurrentRow = gridViewRefund.GetDataRow(e.FocusedRowHandle);
            DataTable RefundInfo = new DataTable();
            DataTable RefundItem = new DataTable();

            textEditID.EditValue = CurrentRow["ref_insure_id"].To<int>();

            RefundInfo = BusinessLogicBridge.DataStore.getRefundById(CurrentRow["ref_insure_id"].To<int>());

            invoice_id_temp = RefundInfo.Rows[0]["ref_invoice_id"].To<int>();
            textEditInvoice_number.EditValue = RefundInfo.Rows[0]["ref_insure_contract_no"];
            textEditPrefix.EditValue = RefundInfo.Rows[0]["ref_insure_prefixname"].ToString();
            textEditFirstName.EditValue = RefundInfo.Rows[0]["ref_insure_firstname"];
            textEditLastname.EditValue = RefundInfo.Rows[0]["ref_insure_lastname"];
            textEditRoomLabel.EditValue = RefundInfo.Rows[0]["ref_insure_roomlabel"];
            textEditFloor.EditValue = RefundInfo.Rows[0]["ref_insure_floor"];
            textEditBuilding.EditValue = RefundInfo.Rows[0]["ref_insure_building"];
            textEditRoomType.EditValue = RefundInfo.Rows[0]["ref_insure_roomtype"];
            textEditRoomPrice.EditValue = RefundInfo.Rows[0]["ref_insure_roomprice"];
            textEditRoomInsurance.EditValue = RefundInfo.Rows[0]["ref_insure_price"];
            textEditAdvance.EditValue = RefundInfo.Rows[0]["ref_insure_rent_advance"];
            textEditSumRefund.EditValue = RefundInfo.Rows[0]["ref_insure_sumrefund"];


            RefundItem = BusinessLogicBridge.DataStore.getRefundItemsByRefundId(RefundInfo.Rows[0]["ref_insure_id"].To<int>());

            RefundItem.Columns.Add("order", typeof(string));
            RefundItem.Columns.Add("item_vat_bool", typeof(bool));
            int counter = 5;

            for (int i = 0; i < RefundItem.Rows.Count; i++)
            {

                RefundItem.Rows[i]["order"] = counter;

                if (RefundItem.Rows[i]["item_vat"].To<int>() != 1)
                {
                    RefundItem.Rows[i]["item_vat_bool"] = true;
                }
                else
                {
                    RefundItem.Rows[i]["item_vat_bool"] = false;
                }

                counter++;
            }

            gridControlInvoiceItem.DataSource = RefundItem;

            loadItem();
        }

        void changeRow() {

            int[] rowIndex = gridViewRefund.GetSelectedRows();
             if (rowIndex.Length != 0)
             {
                 DataRow CurrentRow = gridViewRefund.GetDataRow(rowIndex[0]);
                 DataTable RefundInfo = new DataTable();
                 DataTable RefundItem = new DataTable();

                 textEditID.EditValue = CurrentRow["ref_insure_id"].To<int>();

                 RefundInfo = BusinessLogicBridge.DataStore.getRefundById(CurrentRow["ref_insure_id"].To<int>());

                 invoice_id_temp = RefundInfo.Rows[0]["ref_invoice_id"].To<int>();
                 textEditInvoice_number.EditValue = RefundInfo.Rows[0]["ref_insure_contract_no"];
                 textEditPrefix.EditValue = RefundInfo.Rows[0]["ref_insure_prefixname"].ToString();
                 textEditFirstName.EditValue = RefundInfo.Rows[0]["ref_insure_firstname"];
                 textEditLastname.EditValue = RefundInfo.Rows[0]["ref_insure_lastname"];
                 textEditRoomLabel.EditValue = RefundInfo.Rows[0]["ref_insure_roomlabel"];
                 textEditFloor.EditValue = RefundInfo.Rows[0]["ref_insure_floor"];
                 textEditBuilding.EditValue = RefundInfo.Rows[0]["ref_insure_building"];
                 textEditRoomType.EditValue = RefundInfo.Rows[0]["ref_insure_roomtype"];
                 textEditRoomPrice.EditValue = RefundInfo.Rows[0]["ref_insure_roomprice"];
                 textEditRoomInsurance.EditValue = RefundInfo.Rows[0]["ref_insure_price"];
                 textEditAdvance.EditValue = RefundInfo.Rows[0]["ref_insure_rent_advance"];
                 textEditSumRefund.EditValue = RefundInfo.Rows[0]["ref_insure_sumrefund"].To<double>().ToString("N2");


                 RefundItem = BusinessLogicBridge.DataStore.getRefundItemsByRefundId(RefundInfo.Rows[0]["ref_insure_id"].To<int>());

                 RefundItem.Columns.Add("order", typeof(string));
                 RefundItem.Columns.Add("item_vat_bool", typeof(bool));
                 int counter = 5;

                 for (int i = 0; i < RefundItem.Rows.Count; i++)
                 {

                     RefundItem.Rows[i]["order"] = counter;

                     if (RefundItem.Rows[i]["item_vat"].To<int>() != 1)
                     {
                         RefundItem.Rows[i]["item_vat_bool"] = true;
                     }
                     else
                     {
                         RefundItem.Rows[i]["item_vat_bool"] = false;
                     }

                     counter++;
                 }

                 gridControlInvoiceItem.DataSource = RefundItem;

                 loadItem();
             }

        }

        void RefundInsurance_Load(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 60) / 100;
            reloadGrid();
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

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(invoice_id_temp);

            DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

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

        void initItem()
        {
            DataTable DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.EditValue.ToString());

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceById(textEditID.EditValue.To<int>());

            DataTable ItemTable = BusinessLogicBridge.DataStore.getInvoiceItemsByInvoiceId(textEditID.EditValue.To<int>());

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

            for (int i = 0; i < ItemTable.Rows.Count; i++)
            {

                //if (ItemTable.Rows[i]["item_type"].To<int>() == 1)
                //{
                    // เก็บเงินแบบ รายเดือน
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
                //}
                //else if (InvoiceInfo.Rows[0]["check_in_contracttype"].To<int>() == 1)
                //{
                //    // เก็บเงินแบบ แบบ ครั้งเดียว
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


            CheckOutItemTable = MainForm.VatCalculate(CheckOutItemTable);

            gridControlInvoiceItem.DataSource = CheckOutItemTable;

            //
        }

        public void reloadGrid()
        {
            DataTable dtBookRefund = BusinessLogicBridge.DataStore.getRefundList();

            dtBookRefund.Columns.Add("ref_insure_status_text", typeof(string));

            for (int i = 0; i < dtBookRefund.Rows.Count; i++ )
            {
                if (dtBookRefund.Rows[i]["ref_insure_status"].To<int>() == 2)
                {
                    dtBookRefund.Rows[i]["ref_insure_status_text"] = "Refunded";
                }
                else {
                    dtBookRefund.Rows[i]["ref_insure_status_text"] = "Un-Refund";
                }
            }

            gridControlRefund.DataSource = dtBookRefund;
        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            if (gridViewRefund.GetFocusedRowCellValue("ref_insure_paid_date") != null)
            {
                if (gridViewRefund.GetFocusedRowCellValue("ref_insure_paid_date").ToString() == "")
                {
                    BusinessLogicBridge.DataStore.RefundPaid(textEditID.EditValue.To<int>());
                }

                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Room Management [Refund Insurance]");
            PrintDocuments.bookrefund PrintRefund = new DXWindowsApplication2.PrintDocuments.bookrefund();
            PrintRefund.loopGenDataRow(textEditID.EditValue.To<int>());
            PrintRefund.ShowPreview();
            reloadGrid();
            }

        }

        private void gridControlRefund_Click(object sender, EventArgs e)
        {
            changeRow();
        }

    }
}
