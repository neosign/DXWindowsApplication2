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
    public partial class PopUpAddtionalItem : uFormBase
    {
        public PopUpAddtionalItem()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopUpAddtionalItem_Load);
            //
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttCancel.Click += new EventHandler(bttCancel_Click);
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
            if (textEditItemName.EditValue==null)
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

            if (lookUpEditPayType.EditValue == null)
            {
                label = labelControlPayType.Text;
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

            DataTable ItemListUsed = BusinessLogicBridge.DataStore.getItemByItemName(textEditItemName.EditValue.ToString().Trim());
            if (ItemListUsed.Rows.Count > 0) {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1028"), getLanguage("_softwarename"));
                return;
            }

            string item_type_text ="";
            if(lookUpEditPayType.EditValue.To<int>()==1){
                item_type_text = getLanguage("_payment_dropdown_monthly");
            }else{
                item_type_text = getLanguage("_payment_dropdown_onetime");
            }

            dtItemTemp.Rows.Add(0, textEditItemName.EditValue.ToString(), textEditMonthPrice.EditValue, 0, textEditDailyPrice.EditValue, memoEditDescription.EditValue, lookUpEditVatType.EditValue, lookUpEditPayType.EditValue, "manual", DateTime.Now, 1, 0, item_type_text, true);
            //
            this.DialogResult = DialogResult.OK;
        }

        void PopUpAddtionalItem_Load(object sender, EventArgs e)
        {
            InitPayType();
            InitVatType();
            //
            setLangThis();
        }

        void setLangThis()
        {
            labelControlItemName.Text = getLanguage("_item_name");
            labelControlPayType.Text = getLanguage("_payment");
            labelControlMonthPrice.Text = getLanguage("_month_charge");
            labelControlDailyPrice.Text = getLanguage("_day_charge");
            labelControlVatType.Text = getLanguage("_tax_calculate");
            labelControlDescription.Text = getLanguage("_description");
            labelControlRequired.Text = getLanguage("_required");
            //
            labelControlbaht1.Text = getLanguage("_baht");
            labelControlbaht2.Text = getLanguage("_baht");
        }
        
        public void InitPayType()
        {
            DataTable DTpaytype = new DataTable();

            DTpaytype.Columns.Add("paytype_label", typeof(string));
            DTpaytype.Columns.Add("paytype_id", typeof(int));


            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_monthly"), 1);
            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_onetime"), 2);

            lookUpEditPayType.Properties.DataSource = DTpaytype;
            lookUpEditPayType.Properties.DisplayMember = "paytype_label";
            lookUpEditPayType.Properties.ValueMember = "paytype_id";
            lookUpEditPayType.Properties.NullText = getLanguage("_payment_format_select");
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
