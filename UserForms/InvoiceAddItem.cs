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
    public partial class InvoiceAddItem : uBase
    {
        public InvoiceAddItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(InvoiceAddItem_Load);
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

                if (ItemInfo.Rows[0]["item_price_daily"].ToString() == "0")
                {
                    textEditItemUnitPrice.EditValue = ItemInfo.Rows[0]["item_price_monthly"];
                }

                lookUpEditVatType.EditValue = ItemInfo.Rows[0]["item_vat"];

            }
        }

        void InvoiceAddItem_Load(object sender, EventArgs e)
        {
            initLoadItem();
            InitVatType();
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

        public void initLoadItem() {

            DataTable ItemList = BusinessLogicBridge.DataStore.getAllItems();

            for (int i = 0; i < ItemList.Rows.Count; i++)
            {
                mruEditItemName.Properties.Items.Add(ItemList.Rows[i]["item_name"]);
            }
            

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
                message = "กรุณากรอกข้อมูลให้ครอบในช่องที่มีเครื่องหมาย \"*\"";
                _Error.Rows.Add(label, message);
            }
            if (textEditItemUnitPrice.EditValue.ToString().Length < 1)
            {
                label = labelControlItemPrice.Text;
                message = "กรุณากรอกข้อมูลให้ครอบในช่องที่มีเครื่องหมาย \"*\"";
                _Error.Rows.Add(label, message);
            }
            return _Error;
        }
        
        private void bttSave_Click(object sender, EventArgs e)
        {
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
                    XtraMessageBox.Show(message, "!!! ข้อผิดพลาด !!!");
                }
                else
                {
                    int itemID = BusinessLogicBridge.DataStore.addDataItem(mruEditItemName.SelectedItem.ToString(), textEditItemUnitPrice.EditValue.To<double>(), 0, "", lookUpEditVatType.EditValue.ToString(), 2);

                    DataTable ItemInfo = BusinessLogicBridge.DataStore.getItemByItemID(itemID);
                    double item_sumprice = 0;
                    double item_vatprice = 0;
                    double item_netprice = 0;
                    //inv_trans_id 	item_id 	item_name 	item_price_monthly 	item_price_weekly 	item_price_daily 	item_detail 	item_vat 	item_type 	item_priceperunit, item_amount, 	item_sumprice 	item_vatprice 	item_netprice 	item_flag 	item_createdate order item_vat_bool

                    item_sumprice = textEditItemUnit.EditValue.To<int>() * textEditItemUnitPrice.EditValue.To<double>();
                    
                    if(lookUpEditVatType.EditValue.To<int>()!=1){
                        
                        DataTable DocInfo =  BusinessLogicBridge.DataStore.getDocumentConfig();

                        item_vatprice = (DocInfo.Rows[0]["doc_vat"].To<double>() / 100) * item_sumprice;
                    }else{
                        item_vatprice = 0;
                    }
                    item_netprice = item_sumprice + item_vatprice;

                    ViewInvoice.dataItemsStaticTable.Rows.Add(ViewInvoice.inv_trans_id_temp, itemID, mruEditItemName.SelectedItem.ToString(), ItemInfo.Rows[0]["item_price_monthly"], ItemInfo.Rows[0]["item_price_weekly"], ItemInfo.Rows[0]["item_price_daily"], "", ItemInfo.Rows[0]["item_vat"], ItemInfo.Rows[0]["item_type"], textEditItemUnitPrice.EditValue.To<double>(), textEditItemUnit.EditValue.To<int>(), item_sumprice, item_vatprice, item_netprice, "Manual", DateTime.Now, (ViewInvoice.dataItemsStaticTable.Rows.Count + 1).To<int>(), lookUpEditVatType.EditValue.To<int>());
                    ViewInvoice.textEditTrigger.EditValue = DateTime.Now;
                    ViewInvoice.AddPanel.Close();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }
    }
}
