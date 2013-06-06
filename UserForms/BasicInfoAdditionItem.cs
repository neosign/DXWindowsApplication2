using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoAdditionItem : uBase
    {
        public static DataTable vatlist;
        public static DataTable typelist;
        public static string vatItemA;
        public static string vatItemB;
        public static string typeItemA;
        public static string typeItemB;
        public static XtraMessageBoxForm AddPanel;
        public static DevExpress.XtraGrid.GridControl gridControlNick;

        # region Method


        public override void Refresh()
        {
            base.Refresh();

            DataTable configPath = BusinessLogicBridge.DataStore.getGeneralConfig();

            if (configPath.Rows.Count > 0)
            {
                labelControlBaht.Text = configPath.Rows[0]["currency"].ToString();
                labelControlBaht.Text = configPath.Rows[0]["currency"].ToString();
            }
            // Vat Item Dropdown

            InitPayType();
            InitVatType();
            InitItemData();

            // OnLoad

            DataRow CurrentRow = gridView3.GetDataRow(0);
            if (CurrentRow != null)
            {
                int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                DataTable ItemDetail = BusinessLogicBridge.DataStore.getDataItemById(item_id);

                textEditItemID.EditValue = item_id;    //1
                textEditItemName.EditValue = ItemDetail.Rows[0]["item_name"].ToString(); //12
                lookUpEditPayType.EditValue = ItemDetail.Rows[0][7].ToString();
                textEditItemPriceMonthly.EditValue = ItemDetail.Rows[0]["item_price_monthly"].To<double>().ToString("N2");
                textEditItemPriceDaily.EditValue = ItemDetail.Rows[0]["item_price_daily"].To<double>().ToString("N2");
                memoEditItemDetail.EditValue = ItemDetail.Rows[0]["item_detail"].ToString();  //2
                lookUpEditVatType.EditValue = ItemDetail.Rows[0][6].ToString(); //lookUpEditItemVat.Properties.GetKeyValueByDisplayText(_itemTable.Rows[0][6].ToString());  //2
                setDisable();
            }
            else
            {
                //textEditItemID.EditValue = item_id;    
                textEditItemName.EditValue = "";
                lookUpEditPayType.EditValue = null;
                textEditItemPriceMonthly.EditValue = "0.00";
                textEditItemPriceDaily.EditValue = "0.00";
                memoEditItemDetail.EditValue = "";
                lookUpEditVatType.EditValue = null;
                setAddAction();
            }

            // Set Language 
            setLangThis();
        }


        public BasicInfoAdditionItem()
        {
            //DXWindowsApplication2.MainForm.checkAuthorize();
            InitializeComponent();
            //
            this.Load += new EventHandler(BasicInfoAdditionItem_Load);
            this.Dock   = DockStyle.Fill;
            // Onchange
            gridView3.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView3_FocusedRowChanged);
            SaveClick += new EventHandler(BasicInfoAdditionItem_SaveClick);
        }

        void BasicInfoAdditionItem_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        void BasicInfoAdditionItem_Load(object sender, EventArgs e)
        {
            DataTable configPath = BusinessLogicBridge.DataStore.getGeneralConfig();

            if (configPath.Rows.Count > 0)
            {
                labelControlBaht.Text = configPath.Rows[0]["currency"].ToString();
                labelControlBaht.Text = configPath.Rows[0]["currency"].ToString();
            }
            // Vat Item Dropdown

            InitPayType();
            InitVatType();
            InitItemData();
           
            // OnLoad

            DataRow CurrentRow = gridView3.GetDataRow(0);
            if (CurrentRow != null)
            {
                int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                DataTable ItemDetail = BusinessLogicBridge.DataStore.getDataItemById(item_id);

                textEditItemID.EditValue = item_id;    //1
                textEditItemName.EditValue = ItemDetail.Rows[0]["item_name"].ToString(); //12
                lookUpEditPayType.EditValue = ItemDetail.Rows[0][7].ToString();
                textEditItemPriceMonthly.EditValue = ItemDetail.Rows[0]["item_price_monthly"].To<double>().ToString("N2");
                textEditItemPriceDaily.EditValue = ItemDetail.Rows[0]["item_price_daily"].To<double>().ToString("N2");
                memoEditItemDetail.EditValue = ItemDetail.Rows[0]["item_detail"].ToString();  //2
                lookUpEditVatType.EditValue = ItemDetail.Rows[0][6].ToString(); //lookUpEditItemVat.Properties.GetKeyValueByDisplayText(_itemTable.Rows[0][6].ToString());  //2
                setDisable();
            }
            else {
                //textEditItemID.EditValue = item_id;    
                textEditItemName.EditValue          = "";
                lookUpEditPayType.EditValue         = null;
                textEditItemPriceMonthly.EditValue  = "0.00";
                textEditItemPriceDaily.EditValue    = "0.00";
                memoEditItemDetail.EditValue        = "";
                lookUpEditVatType.EditValue         = null;
                setAddAction();
            }
            
            // Set Language 
            setLangThis();
        }

        public void InitPayType()
        {
            DataTable DTpaytype = new DataTable();

            DTpaytype.Columns.Add("paytype_label", typeof(string));
            DTpaytype.Columns.Add("paytype_id", typeof(int));

            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_monthly"), 1);
            DTpaytype.Rows.Add(getLanguage("_payment_dropdown_onetime"), 2);

            lookUpEditPayType.Properties.DataSource     = DTpaytype;
            lookUpEditPayType.Properties.DisplayMember  = "paytype_label";
            lookUpEditPayType.Properties.ValueMember    = "paytype_id";
            lookUpEditPayType.Properties.NullText       = getLanguage("_payment_format_select");

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

        public void InitItemData()
        {
            DataTable ItemTableTemp = BusinessLogicBridge.DataStore.getDataItemAll();

            ItemTableTemp.Columns.Add("check_box", typeof(Boolean));
            ItemTableTemp.Columns.Add("item_type_text", typeof(String));
            ItemTableTemp.Columns.Add("item_vat_text", typeof(String));

            for (int i = 0; i < ItemTableTemp.Rows.Count; i++)
            {
                if (int.Parse(ItemTableTemp.Rows[i]["item_type"].ToString()) == 1)
                {
                    ItemTableTemp.Rows[i]["item_type_text"] = getLanguage("_payment_dropdown_monthly");
                }
                else if (int.Parse(ItemTableTemp.Rows[i]["item_type"].ToString()) == 2)
                {
                    ItemTableTemp.Rows[i]["item_type_text"] = getLanguage("_payment_dropdown_onetime");
                }

                if (int.Parse(ItemTableTemp.Rows[i]["item_vat"].ToString()) == 1)
                {
                    ItemTableTemp.Rows[i]["item_vat_text"] = getLanguage("_no_vat");
                }
                else if (int.Parse(ItemTableTemp.Rows[i]["item_vat"].ToString()) == 2)
                {
                    ItemTableTemp.Rows[i]["item_vat_text"] = getLanguage("_with_the_net");
                }
                else if (int.Parse(ItemTableTemp.Rows[i]["item_vat"].ToString()) == 3)
                {
                    ItemTableTemp.Rows[i]["item_vat_text"] = getLanguage("_increase_from_net");
                }
            }

            gridControlItem.DataSource = ItemTableTemp;
            gridControlNick = gridControlItem;
        }

        public void setLangThis()
        {
            //
            this.groupControlList.Text = getLanguage("_addittional_cost_list");
            this.groupControlAddittional.Text = getLanguage("_addittional_cost_info");
            //Grid
            this.grid_name.Caption = getLanguage("_item_name");
            this.grid_type_text.Caption = getLanguage("_payment");
            this.grid_price_monthly.Caption = getLanguage("_month_charge");
            this.grid_price_daily.Caption = getLanguage("_day_charge");
            this.grid_vat_text.Caption = getLanguage("_tax_format");
            //
            this.labelBasicinfoName.Text = getLanguageWithColon("_item_name");
            this.labelBasicinfoType.Text = getLanguageWithColon("_payment");
            this.labelBasicinfoPriceMonthly.Text = getLanguageWithColon("_month_charge");
            this.labelBasicinfoPriceDaily.Text = getLanguageWithColon("_day_charge");
            this.labelBasicinfoVat.Text = getLanguageWithColon("_tax_format");
            this.labelBasicinfoDetail.Text = getLanguageWithColon("_description");
            //
            this.labelControlBaht.Text = getLanguage("_baht");
            this.labelControlBaht2.Text = getLanguage("_baht");
            //
            this.labelControlRequired.Text = getLanguage("_required");
            this.bttAdd.Text = getLanguage("_add");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttDelete.Text = getLanguage("_delete");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");

        }

        void setDisable()
        {
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            panelEnable.Enabled = false;
            bttAdd.Enabled = true;
            bttDelete.Enabled = true;
            bttEdit.Enabled = true;
            gridControlItem.Enabled = true;
            textEditItemPriceDaily.Enabled = true;


        }

        void setEnable()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
            panelEnable.Enabled = true;
            textEditItemPriceDaily.Enabled = false;
        }

        void setAddAction()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            panelEnable.Enabled = true;
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
            texthiddenAction.EditValue = "add";
            gridControlItem.Enabled = false;
        }

        void setEditAction()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            panelEnable.Enabled = true;
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
            texthiddenAction.EditValue = "update";

            gridControlItem.Enabled = false;
        }

        # endregion

        #region Validation Zone

        private bool IsEmail(string Email)
        {
            string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return true;
            else
                return false;
        }

        private bool isPhoneValid(string Number)
        {
            string strRegex = @"^(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}(\s*ext(ension)?[0-9]{5})?$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Number))
                return true;
            else
                return false;
        }

        private bool isValidUrl(string url)
        {
            string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        private bool IsAlphaNumeric(string Text)
        {
            if ((Text.Length > 0) && (Text != ""))
            {
                string strRegex = @"[^a-zA-Z0-9ก-๙\.\,-\/ ]?$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(Text))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public bool IsNumeric(string Text){
            //Regex moneyR = new Regex(@"\d+\.\d{2}");
           try{
            Convert.ToDouble(Text);
               return true;
            }catch{
               return false;
            }
        }


        private bool isEmpty(string param)
        {

            if (param.Length < 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        #endregion        

        # region  button event

        void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changerow();            
        }

        private void changerow() {

            DataTable _itemTable = BusinessLogicBridge.DataStore.getAllItems();

            int[] rowIndex = gridView3.GetSelectedRows();

            try
            {
                if (rowIndex[0] >= 0)
                {

                    DataRow CurrentRow = gridView3.GetDataRow(rowIndex[0]);
                    
                            int item_id = Convert.ToInt32(CurrentRow["item_id"].ToString());
                            DataTable ItemDetail = BusinessLogicBridge.DataStore.getDataItemById(item_id);

                            textEditItemID.EditValue = item_id;    //1
                            textEditItemName.EditValue = ItemDetail.Rows[0]["item_name"].ToString(); //12
                            textEditItemPriceMonthly.EditValue = ItemDetail.Rows[0]["item_price_monthly"].To<double>().ToString("N2");//ItemDetail.Rows[0]["item_price_monthly"].ToString();  //2              
                            textEditItemPriceDaily.EditValue = ItemDetail.Rows[0]["item_price_daily"].To<double>().ToString("N2");  //2
                            memoEditItemDetail.EditValue = ItemDetail.Rows[0]["item_detail"].ToString();  //2

                            textEditCheckEvent.EditValue = "UPDATE";

                            lookUpEditPayType.EditValue = ItemDetail.Rows[0][7];
                            lookUpEditVatType.EditValue = ItemDetail.Rows[0][6];                    

                }
                else
                {   
                    // No Data
                    setEnable();

                    textEditItemID.EditValue = (Convert.ToInt16(_itemTable.Rows[0][0]) + 1);    //1
                    textEditItemName.EditValue = "";
                    textEditItemPriceMonthly.EditValue = "";
                    textEditItemPriceDaily.EditValue = "";
                    memoEditItemDetail.EditValue = "";
                    lookUpEditVatType.EditValue = "0";
                    textEditCheckEvent.EditValue = "ADD";
                }
            }
            catch
            {

            }
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            setAddAction();
            textEditItemName.EditValue = "";
            lookUpEditPayType.EditValue = null;
            textEditItemPriceMonthly.EditValue = "0.00";
            textEditItemPriceDaily.EditValue = "0.00";
            lookUpEditVatType.EditValue = null;
            memoEditItemDetail.EditValue = "";
            texthiddenAction.EditValue = "add";
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEditAction();
            texthiddenAction.EditValue = "update";
        }
      
        private void bttSave_Click(object sender, EventArgs e)
        {
            List<string> listError = new List<string>();

            string TextOnly     = getLanguage("_text_only");
            string EmptySting   = getLanguage("_msg_1001");
            string valid_length = getLanguage("_valid_length");
            string max_value    = getLanguage("_max_value");

            if (texthiddenAction.EditValue.ToString() == "add")
            {
                // Validation Control
                string msgError = "";

                #region validate Item Name


                if (isEmpty(textEditItemName.Text) == true)
                {
                    if (validLength(textEditItemName.Text, 50) == false)
                    {
                        listError.Add(labelBasicinfoName.Text + valid_length.Replace("xxx", "50"));
                    }
                    else {
                        if (textEditItemID.EditValue != null)
                        {
                            DataRow[] listItemName = ((DataTable)gridControlItem.DataSource).Select("item_id <> " + textEditItemID.EditValue.To<int>());

                            for (int i = 0; i < listItemName.Length; i++)
                            {
                                if (textEditItemName.EditValue.ToString().Trim() == listItemName[i]["item_name"].ToString().Trim())
                                {
                                    listError.Add(labelBasicinfoName.Text + " " + getLanguage("_msg_1028"));
                                }
                            }
                        }
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoName.Text + EmptySting.ToString());
                }
                #endregion
                
                # region validate Item Price Monthly

                if (isEmpty(textEditItemPriceMonthly.Text) == true)
                {
                    textEditItemPriceMonthly.EditValue = textEditItemPriceMonthly.EditValue.To<double>().ToString("N2");

                    string[] MonthPrice = cutString(textEditItemPriceMonthly.Text);

                    if (validLength(MonthPrice[0], 7) == false)
                    {
                        listError.Add(labelBasicinfoPriceMonthly.Text + max_value);
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoPriceMonthly.Text + EmptySting.ToString());
                }

                #endregion

                # region validate Item Price Daily

                if (isEmpty(textEditItemPriceDaily.Text) == true)
                {
                    textEditItemPriceDaily.EditValue = textEditItemPriceDaily.EditValue.To<double>().ToString("N2");

                    string[] DayPrice = cutString(textEditItemPriceDaily.Text);

                    if (validLength(DayPrice[0], 7) == false)
                    {
                        listError.Add(labelBasicinfoPriceDaily.Text + max_value);
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoPriceDaily.Text + EmptySting.ToString());
                }

                #endregion

                if (lookUpEditPayType.EditValue == null) {
                    listError.Add(labelBasicinfoType.Text + EmptySting.ToString());
                }

                if (lookUpEditVatType.EditValue == null)
                {
                    listError.Add(labelBasicinfoVat.Text + EmptySting.ToString());
                }

                if (listError.Count > 0)
                {

                    foreach (string x in listError)
                    {
                        msgError += x + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, msgError, getLanguage("_softwarename"));


                }
                else
                {
                    try
                    {

                        DataTable ItemList = BusinessLogicBridge.DataStore.getItemByItemName(textEditItemName.Text.ToString());
                        if (ItemList.Rows.Count > 0) {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1028"), getLanguage("_softwarename"));
                            return;
                        }

                        BusinessLogicBridge.DataStore.addDataItem(textEditItemName.Text.ToString(), Convert.ToDouble(textEditItemPriceMonthly.Text), Convert.ToDouble(textEditItemPriceDaily.Text), memoEditItemDetail.Text.ToString(), lookUpEditVatType.EditValue.ToString(), Convert.ToInt16(lookUpEditPayType.EditValue));
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");

                                          
                    }
                    catch
                    {

                    }
                    setDisable();
                    InitItemData();     
                }
            }
            else
            {
                // Edit Process 
                string msgError = "";

                #region validate Item Name


                if (isEmpty(textEditItemName.Text) == true)
                {
                    if (validLength(textEditItemName.Text, 200) == false)
                    {
                        listError.Add(labelBasicinfoName.Text + valid_length.Replace("xxx", "200"));
                    }
                    else {
                        DataRow[] listItemName = ((DataTable)gridControlItem.DataSource).Select("item_id <> " + textEditItemID.EditValue.To<int>());

                        for (int i = 0; i < listItemName.Length; i++)
                        {
                            if (textEditItemName.EditValue.ToString().Trim() == listItemName[i]["item_name"].ToString().Trim())
                            {
                                listError.Add(labelBasicinfoName.Text + " " + getLanguage("_msg_1028"));
                            }
                        }
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoName.Text + EmptySting.ToString());
                }
                #endregion

                # region validate Item Price Monthly

                if (isEmpty(textEditItemPriceMonthly.Text) == true)
                {
                    textEditItemPriceMonthly.EditValue = textEditItemPriceMonthly.EditValue.To<double>().ToString("N2");
                    string[] MonthPrice = cutString(textEditItemPriceMonthly.Text);

                    if (validLength(MonthPrice[0], 7) == false)
                    {
                        listError.Add(labelBasicinfoPriceMonthly.Text + max_value);
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoPriceMonthly.Text + EmptySting.ToString());
                }

                #endregion

                # region validate Item Price Daily

                if (isEmpty(textEditItemPriceDaily.Text) == true)
                {
                    textEditItemPriceDaily.EditValue = textEditItemPriceDaily.EditValue.To<double>().ToString("N2");

                    string[] DayPrice = cutString(textEditItemPriceDaily.Text);

                    if (validLength(DayPrice[0], 7) == false)
                    {
                        listError.Add(labelBasicinfoPriceDaily.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelBasicinfoPriceDaily.Text + EmptySting.ToString());
                }

                #endregion
                

                if (lookUpEditPayType.EditValue == null)
                {
                    listError.Add(labelBasicinfoType.Text + EmptySting.ToString());
                }

                if (lookUpEditVatType.EditValue == null)
                {
                    listError.Add(labelBasicinfoVat.Text + EmptySting.ToString());
                }

                if (listError.Count > 0)
                {

                    foreach (string x in listError)
                    {
                        msgError += x + "\r\n";
                    }

                    utilClass.showPopupMessegeBox(this, msgError, getLanguage("_softwarename"));

                }
                else
                {

                    int ItemInused = 0;

                    // CheckinItemInUsed Data in used
                    if (textEditItemID.Text != "")
                    {
                        ItemInused = BusinessLogicBridge.DataStore.CheckinItemInUsed(Convert.ToInt32(textEditItemID.Text));
                    }


                        // update
                        
                            try
                            {
                                BusinessLogicBridge.DataStore.updateDataItem(textEditItemID.EditValue.To<int>(), textEditItemName.Text, textEditItemPriceMonthly.EditValue.To<double>(), textEditItemPriceDaily.EditValue.To<double>(), memoEditItemDetail.Text.ToString(), lookUpEditVatType.EditValue.ToString(), lookUpEditPayType.EditValue.To<int>());
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            setDisable();
                            InitItemData();
                        
                    
                    
                }
            }
        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            // Check Item InUsed
            int CheckinItemInused   = BusinessLogicBridge.DataStore.CheckinItemInUsed(textEditItemID.EditValue.To<int>());
            int RoomTypeItemInused  = BusinessLogicBridge.DataStore.RoomTypeItemInUsed(textEditItemID.EditValue.To<int>());
            int RoomItemInused = BusinessLogicBridge.DataStore.RoomItemInUsed(textEditItemID.EditValue.To<int>());

            if (CheckinItemInused == 0 && RoomTypeItemInused == 0 && RoomItemInused==0)
            {

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    BusinessLogicBridge.DataStore.removeDataItem(Convert.ToInt16(textEditItemID.Text));
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"),"info");
                    setDisable();
                    InitItemData();
                }
            }
            else {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                return;
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            // update

            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"),getLanguage("_softwarename")) == DialogResult.Yes)
            {
                setDisable();
                 
            }         
        }

        # endregion

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
