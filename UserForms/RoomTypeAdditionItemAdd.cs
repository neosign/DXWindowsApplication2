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
    public partial class RoomTypeAdditionItemAdd : uBase
    {
        public RoomTypeAdditionItemAdd()
        {
            InitializeComponent();

            this.Load += new EventHandler(RoomTypeAdditionItemAdd_Load);

        }

        void RoomTypeAdditionItemAdd_Load(object sender, EventArgs e)
        {
            InitPayType();
            InitVatType();
            setLangThis();
        }

        public void setLangThis()
        {

            this.titleTabAddition.Text = getLanguage("_additional_info");
            this.labelControlItemName.Text      = getLanguageWithColon("_additional_list");
            this.labelControlPayType.Text       = getLanguageWithColon("_payment_format");
            this.labelControlMonthPrice.Text    = getLanguageWithColon("_month_charge");
            this.labelControlDailyPrice.Text    = getLanguageWithColon("_day_charge");
            this.labelControlVatType.Text       = getLanguageWithColon("_tax_format_select");
            this.labelControlDescription.Text   = getLanguageWithColon("_description");
            this.labelControlbaht1.Text         = getLanguage("_baht");
            this.labelControlbaht2.Text         = getLanguage("_baht");
            this.labelControlRequired.Text      = getLanguage("_required");

            this.bttSave.Text                   = getLanguage("_save");
            this.bttCancel.Text                 = getLanguage("_cancel");

        }

        public void InitPayType(){
            DataTable DTpaytype = new DataTable();

            DTpaytype.Columns.Add("paytype_label", typeof(string));
            DTpaytype.Columns.Add("paytype_id", typeof(int));


            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_monthly"), 1);
            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_onetime"), 2);

            lookUpEditPayType.Properties.DataSource = DTpaytype;
            lookUpEditPayType.Properties.DisplayMember = "paytype_label";
            lookUpEditPayType.Properties.ValueMember = "paytype_id";
            lookUpEditPayType.Properties.NullText =getLanguage("_payment_format_select");
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
            lookUpEditVatType.Properties.DisplayMember  = "vattype_label";
            lookUpEditVatType.Properties.ValueMember    = "vattype_id";
            lookUpEditVatType.Properties.NullText = getLanguage("_tax_format_select");
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                BasicInfoRoomType.AddPanel.Close();
            }
        }
        private DataTable validateItem() {

            string max_value = getLanguage("_max_value");

            string star_notice = getLanguage("_msg_1001");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (textEditItemName.EditValue == null || textEditItemName.EditValue.ToString().Length < 1)
            {
                label = labelControlItemName.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditItemName.Focus();
                    focus = true;
                }
            }

            // lookup Edit
            if (lookUpEditPayType.EditValue == null) {

                label = labelControlPayType.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditPayType.Focus();
                    focus = true;
                }
            }

            if (textEditMonthPrice.EditValue.ToString() != "0.00")
            {
                string[] MonthPrice = cutString(textEditMonthPrice.Text);

                if (validLength(MonthPrice[0], 7) == false)
                {
                    label = labelControlMonthPrice.Text;
                    message = max_value;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditMonthPrice.Focus();
                        focus = true;
                    }
                }
            }

            if (textEditDailyPrice.EditValue.ToString() != "0.00")
            {
                string[] MonthPrice = cutString(textEditDailyPrice.Text);

                if (validLength(MonthPrice[0], 7) == false)
                {
                    label = labelControlDailyPrice.Text;
                    message = max_value;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditDailyPrice.Focus();
                        focus = true;
                    }
                }
            }

            if (lookUpEditVatType.EditValue == null)
            {

                label = labelControlVatType.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditVatType.Focus();
                    focus = true;
                }
            }

            return _ValidateTable;
        }
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate Default
                DataTable _ValidateTable = validateItem();
                String message = "";
                if (_ValidateTable.Rows.Count > 0)
                {
                    for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                    {
                        message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    return;
                }
                else {

                    DataTable ItemData = BusinessLogicBridge.DataStore.getItemByItemName(textEditItemName.EditValue.ToString());

                    if (ItemData.Rows.Count > 0) {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1028"), getLanguage("_softwarename"));
                        return;
                    }

                    // Success 
                                                            // item_id 	item_name 	item_price_monthly 	item_price_weekly 	item_price_daily 	item_detail 	item_vat 	item_type 
                    BasicInfoRoomType.ItemTableTemp.Rows.Add(0, textEditItemName.EditValue.ToString(), Convert.ToDouble(textEditMonthPrice.EditValue), 0, Convert.ToDouble(textEditDailyPrice.EditValue), memoEditDescription.EditValue.ToString(), Convert.ToInt32(lookUpEditVatType.EditValue), Convert.ToInt32(lookUpEditPayType.EditValue), "manual", DateTime.Now, true, lookUpEditPayType.Text);
                    BasicInfoRoomType.TextEditTrigger.EditValue = DateTime.Now.ToString();
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                    BasicInfoRoomType.AddPanel.Close();
                
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
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
        
    }
}
