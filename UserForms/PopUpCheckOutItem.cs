﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public partial class PopUpCheckOutItem : uFormBase
    {
        public PopUpCheckOutItem()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopUpAddtionalItem_Load);
            //
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttCancel.Click += new EventHandler(bttCancel_Click);

            mruEditItemName.AddingMRUItem += new DevExpress.XtraEditors.Controls.AddingMRUItemEventHandler(mruEditItemName_AddingMRUItem);
            mruEditItemName.SelectedValueChanged += new EventHandler(mruEditItemName_SelectedValueChanged);

        }

        void mruEditItemName_AddingMRUItem(object sender, DevExpress.XtraEditors.Controls.AddingMRUItemEventArgs e)
        {
            // Add To Item Table
            textEditItemUnitPrice.Enabled = true;
            lookUpEditVatType.Enabled = true;
        }

        void mruEditItemName_SelectedValueChanged(object sender, EventArgs e)
        {
            string itemname = mruEditItemName.SelectedItem.ToString();
            DataTable ItemInfo = BusinessLogicBridge.DataStore.getItemByItemName(itemname);

            if (ItemInfo.Rows.Count > 0)
            {
                textEditItemUnitPrice.Enabled = false;
                lookUpEditVatType.Enabled = false;

                if (RoomCheckOut.checkin_contract_type == 1)
                    textEditItemUnitPrice.EditValue = ItemInfo.Rows[0]["item_price_daily"];
                else
                    textEditItemUnitPrice.EditValue = ItemInfo.Rows[0]["item_price_monthly"];               

                lookUpEditVatType.EditValue = ItemInfo.Rows[0]["item_vat"];

            }
        }

        public void initLoadItem()
        {

            DataTable ItemList = BusinessLogicBridge.DataStore.getAllItems();

            for (int i = 0; i < ItemList.Rows.Count; i++)
            {
                mruEditItemName.Properties.Items.Add(ItemList.Rows[i]["item_name"]);
            }
        }

        public DataTable dtItemTemp = null;

        void bttCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private DataTable validateDate()
        {
            String label;
            String message;
            DataTable _Error = new DataTable();
            _Error.Columns.Add("label", typeof(String));
            _Error.Columns.Add("message", typeof(String));
            if (mruEditItemName.EditValue == null)
            {
                label = labelControlItemName.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
            }
            if (lookUpEditVatType.EditValue == null)
            {
                label = labelControlVatType.Text;
                message = getLanguage("_msg_1001");
                _Error.Rows.Add(label, message);
                //
            }
            return _Error;
        }

        void bttSave_Click(object sender, EventArgs e)
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
                return;
            }

            double vatprice = 0;
            double sumprice = 0;
            double netprice = 0;
            bool item_vat_bool = false;
            
            sumprice = textEditItemUnitPrice.EditValue.To<double>() * textEditItemUnit.EditValue.To<double>();

            if(lookUpEditVatType.EditValue.To<int>()!=1){
                vatprice = (RoomCheckOut.DTDocInfo.Rows[0]["doc_vat"].To<double>() / 100) * sumprice;
                item_vat_bool = true;
            }

            try
            {

                DataTable HaveItem = BusinessLogicBridge.DataStore.getItemByItemName(mruEditItemName.SelectedItem.ToString().Trim());
                if (HaveItem.Rows.Count > 0)
                {
                    DataRow[] foundRows = RoomCheckOut.dataItemsForCheck.Select("item_id=" + HaveItem.Rows[0]["item_id"].To<int>());
                    if (foundRows.Length > 0)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1028"), getLanguage("_softwarename"));
                        return;
                    }
                    //dtItemTemp.Rows.Add(ViewInvoice.inv_trans_id_temp, HaveItem.Rows[0]["item_id"], mruEditItemName.SelectedItem.ToString(), HaveItem.Rows[0]["item_price_monthly"], 0.0, 0.0, "", lookUpEditVatType.EditValue.To<int>(), 1, textEditItemUnitPrice.EditValue.To<double>(), textEditItemUnit.EditValue.To<double>(), sumprice, vatprice, sumprice + vatprice, "manual", HaveItem.Rows[0]["item_datecreate"], ViewInvoice.counterItem, item_vat_bool);
                    //dtItemTemp.Rows.Add(HaveItem.Rows[0]["item_id"], mruEditItemName.SelectedItem.ToString(), HaveItem.Rows[0]["item_price_daily"], HaveItem.Rows[0]["item_price_monthly"], lookUpEditVatType.EditValue.To<int>(), 2, "manual", RoomCheckOut.counterItem, textEditItemUnit.EditValue.To<double>(), textEditItemUnitPrice.EditValue.To<double>(), sumprice, vatprice, netprice, item_vat_bool);
                    dtItemTemp.Rows.Add(HaveItem.Rows[0]["item_id"], mruEditItemName.SelectedItem.ToString(), HaveItem.Rows[0]["item_price_daily"], HaveItem.Rows[0]["item_price_monthly"], lookUpEditVatType.EditValue.To<int>(), 2, "manual", RoomCheckOut.counterItem, textEditItemUnit.EditValue.To<double>(), textEditItemUnitPrice.EditValue.To<double>(), sumprice, vatprice, netprice, item_vat_bool);
                }
                else
                {

                    DataRow[] foundRows = RoomCheckOut.dataItemsForCheck.Select("item_name='" + mruEditItemName.SelectedItem.ToString().Trim()+"'");
                    if (foundRows.Length > 0)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1028"), getLanguage("_softwarename"));
                        return;
                    }

                    dtItemTemp.Rows.Add(0, mruEditItemName.SelectedItem.ToString(), 0.0, 0.0, lookUpEditVatType.EditValue.To<int>(), 2, "manual", RoomCheckOut.counterItem, textEditItemUnit.EditValue.To<double>(), textEditItemUnitPrice.EditValue.To<double>(), sumprice, vatprice, netprice, item_vat_bool);
                }

            }catch(Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }

            //dataItemsForCheck
            this.DialogResult = DialogResult.OK;
        }

        void PopUpAddtionalItem_Load(object sender, EventArgs e)
        {
            initLoadItem();

            InitVatType();
            //
            setLangThis();
        }

        void setLangThis()
        {
            labelControlRequired.Text = getLanguage("_required");

            titleTabAddition.Text = getLanguage("_add_additional_cost");
            labelControlItemName.Text = getLanguageWithColon("_item_name");
            labelControlAmountUnit.Text = getLanguageWithColon("_amount_unit");
            labelControlItemPrice.Text = getLanguageWithColon("_price_per_unit");
            labelControlVatType.Text = getLanguageWithColon("_tax_calculate");

            labelControlBath.Text = getLanguage("_baht");
            labelControlBath2.Text = getLanguage("_baht");

            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }
        
        public void InitVatType()
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

    }
}
