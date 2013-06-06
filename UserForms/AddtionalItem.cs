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
    public partial class AddtionalItem : uBase
    {
        public AddtionalItem()
        {
            InitializeComponent();

            this.Load += new EventHandler(AddtionalItem_Load);
        }

        void AddtionalItem_Load(object sender, EventArgs e)
        {
            InitPayType();
            InitVatType();
            setLangThis();
        }

        public void setLangThis()
        {

            labelControlItemName.Text = getLanguageWithColon("_item_name");
            labelControlPayType.Text = getLanguageWithColon("_payment");
            labelControlMonthPrice.Text = getLanguageWithColon("_month_charge");
            labelControlDailyPrice.Text = getLanguageWithColon("_day_charge");
            labelControlVatType.Text = getLanguageWithColon("_tax_calculate");
            labelControlDescription.Text = getLanguageWithColon("_description");
            labelControlRequired.Text = getLanguageWithColon("_required");
            //
            labelControlbaht1.Text = getLanguage("_baht");
            labelControlbaht2.Text = getLanguage("_baht");

            this.bttSave.Text = DXWindowsApplication2.MainForm.getLanguage("_save");
            this.bttCancel.Text = DXWindowsApplication2.MainForm.getLanguage("_cancel");

        }

        public void InitPayType()
        {
            DataTable DTpaytype = new DataTable();

            DTpaytype.Columns.Add("paytype_label", typeof(string));
            DTpaytype.Columns.Add("paytype_id", typeof(int));


            DTpaytype.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_monthly"), 1);
            DTpaytype.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_payment_dropdown_onetime"), 2);

            lookUpEditPayType.Properties.DataSource = DTpaytype;
            lookUpEditPayType.Properties.DisplayMember = "paytype_label";
            lookUpEditPayType.Properties.ValueMember = "paytype_id";
            lookUpEditPayType.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_payment_format_select");
        }

        public void InitVatType()
        {
            DataTable DTvattype = new DataTable();

            DTvattype.Columns.Add("vattype_label", typeof(string));
            DTvattype.Columns.Add("vattype_id", typeof(int));

            DTvattype.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_no_vat"), 1);
            DTvattype.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_with_the_net"), 2);
            DTvattype.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_increase_from_net"), 3);


            lookUpEditVatType.Properties.DataSource = DTvattype;
            lookUpEditVatType.Properties.DisplayMember = "vattype_label";
            lookUpEditVatType.Properties.ValueMember = "vattype_id";
            lookUpEditVatType.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_tax_format_select");
        }

        private DataTable validateItem()
        {

            string max_value = DXWindowsApplication2.MainForm.getLanguage("_max_value");

            string star_notice = DXWindowsApplication2.MainForm.getLanguage("_notice_star");

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
            if (lookUpEditPayType.EditValue == null)
            {

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
                    XtraMessageBox.Show(message, getLanguage("_warning"));
                    return;
                }
                else
                {
                    // Success 
                    string item_type_label ="";
                    int countItem = (RoomList.ItemTableTemp.Rows.Count + 1);

                    if (Convert.ToInt32(lookUpEditPayType.EditValue)==1)
                    {
                        item_type_label = getLanguage("_payment_dropdown_monthly");
                    }else{
                        item_type_label = getLanguage("_payment_dropdown_onetime");
                    }

                    RoomList.ItemTableTemp.Rows.Add(countItem,textEditItemName.EditValue.ToString(), Convert.ToDouble(textEditDailyPrice.EditValue), Convert.ToDouble(textEditMonthPrice.EditValue), Convert.ToInt32(lookUpEditVatType.EditValue), Convert.ToInt32(lookUpEditPayType.EditValue), "manual", RoomList.checkin_temp_id, true, item_type_label);
                    RoomList.TextEditTrigger.EditValue = DateTime.Now.ToString();
                    XtraMessageBox.Show(getLanguage("_save_completed"));
                    RoomList.AddPanel.Close();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            RoomList.AddPanel.Close();
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
