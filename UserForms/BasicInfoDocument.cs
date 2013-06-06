using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoDocument : uBase
    {
        public static int TempCompany_id = 0;

        public static string button_event = "";

        public static string contact_prefix_temp = "";
        public static string invoice_prefix_temp = "";
        public static string reciept_prefix_temp = "";
        public bool flagCancel = false;

        public BasicInfoDocument()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.Load += new EventHandler(BasicInfoDocument_Load);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            SaveClick +=new EventHandler(bttSave_Click);

            textEditContractStart.EditValueChanged += new EventHandler(textEditContractStart_EditValueChanged);
            textEditInvoiceStart.EditValueChanged += new EventHandler(textEditInvoiceStart_EditValueChanged);

            textEditReceiptStart.EditValueChanged += new EventHandler(textEditReceiptStart_EditValueChanged);

        }

        public override void Refresh()
        {
            base.Refresh();

            initDocumentInfo();
            initgridChannel();

            InitVatType();
            InitPaper();

            // Set Language
            setLangThis();

        }


        void textEditReceiptStart_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditReceiptStart.EditValue.To<int>() == 0)
            {
                textEditReceiptStart.EditValue = (1).ToString().PadLeft(6, '0');
            }
            else
            {
                textEditReceiptStart.EditValue = textEditReceiptStart.EditValue.ToString().PadLeft(6, '0');
            }

            if (checkEditSaperateReciept.Checked == true)
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() + "M" + textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
            else
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() + textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
        }

        void textEditInvoiceStart_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditInvoiceStart.EditValue.To<int>() == 0)
            {
                textEditInvoiceStart.EditValue = (1).ToString().PadLeft(6, '0');
            }
            else
            {
                textEditInvoiceStart.EditValue = textEditInvoiceStart.EditValue.ToString().PadLeft(6, '0');
            }

            if (checkEditSaperateInvoice.Checked == true)
            {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + "M" + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
            else
            {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
        }

        void textEditContractStart_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditContractStart.EditValue.To<int>() == 0)
            {
                textEditContractStart.EditValue = (1).ToString().PadLeft(6, '0');
            }
            else {
                textEditContractStart.EditValue = textEditContractStart.EditValue.ToString().PadLeft(6, '0');
            }

            if (checkEditSaperateContract.Checked == true)
            {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + "M" + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }
            else
            {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }

        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            int[] rowIndex = gridView1.GetSelectedRows();
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                    TempCompany_id = Convert.ToInt32(CurrentRow["company_id"].ToString());
                    initDocumentInfo();
                }
            }
            catch { 
            
            }

        }

        void BasicInfoDocument_Load(object sender, EventArgs e)
        {
            initDocumentInfo();
            initgridChannel();
            
            InitVatType();
            InitPaper();
            //SetDocumentType();

            // Set Language
            setLangThis();
        }

        void initgridChannel() {


            DataTable DTRelation = BusinessLogicBridge.DataStore.BasicInfoCompany_get();         

            gridControlCompany.DataSource = DTRelation;
        }

        public void setLangThis()
        {
            //
            this.Name = MainForm.getLanguage("_business_list");
            //
            this.groupControl_BusinessList.Text = MainForm.getLanguage("_business_list");
            this.groupControlVat.Text = MainForm.getLanguage("_tax_format");
            this.groupControlDocType.Text = MainForm.getLanguage("_document_format");
            this.groupControlContractFormat.Text = MainForm.getLanguage("_contract_format");
            this.groupControlInvoiceFormat.Text = MainForm.getLanguage("_invoice_format");
            this.groupControlReceiptFormat.Text = MainForm.getLanguage("_receipt_format");
            this.groupBoxFormat1.Text = MainForm.getLanguage("_contract_number_format");
            this.groupBoxFormat2.Text = MainForm.getLanguage("_invoice_number_format");
            this.groupBoxFormat3.Text = MainForm.getLanguage("_receipt_number_format");

            //
            this.labelVat.Text = MainForm.getLanguageWithColon("_tax_rate");
            this.labelVatType.Text = MainForm.getLanguageWithColon("_tax_format");
            this.labelControlDateFormat.Text = MainForm.getLanguageWithColon("_date_format");
            this.labelControlLogo.Text = MainForm.getLanguageWithColon("_apartment_logo_position");
            this.labelControlTop.Text = "[" + MainForm.getLanguage("_top_on_document") + "]";
            //
            this.labelPrefix1.Text = MainForm.getLanguageWithColon("_first_string");
            this.labelPrefix2.Text = MainForm.getLanguageWithColon("_first_string");
            this.labelPrefix3.Text = MainForm.getLanguageWithColon("_first_string");
            this.labelDate1.Text = MainForm.getLanguage("_date_yyyymmdd");
            this.labelDate2.Text = MainForm.getLanguage("_date_yyyymmdd");
            this.labelDate3.Text = MainForm.getLanguage("_date_yyyymmdd");
            this.labelStart1.Text = MainForm.getLanguage("_start_number");
            this.labelStart2.Text = MainForm.getLanguage("_start_number");
            this.labelStart3.Text = MainForm.getLanguage("_start_number");
            this.labelExample1.Text = MainForm.getLanguage("_contract_number");
            this.labelExample2.Text = MainForm.getLanguage("_invoice_number");
            this.labelExample3.Text = MainForm.getLanguage("_receipt_number");
            this.labelHeader1.Text = MainForm.getLanguageWithColon("_header_text");
            this.labelHeader2.Text = MainForm.getLanguageWithColon("_header_text");
            this.labelFooter1.Text = MainForm.getLanguageWithColon("_footer_text");
            this.labelFooter2.Text = MainForm.getLanguageWithColon("_footer_text");
            this.labelUnder11.Text = MainForm.getLanguageWithColon("_signature1");
            this.labelUnder12.Text = MainForm.getLanguageWithColon("_signature2");
            this.labelUnder21.Text = MainForm.getLanguageWithColon("_signature1");
            this.labelUnder22.Text = MainForm.getLanguageWithColon("_signature2");
            this.labelPaper1.Text = MainForm.getLanguageWithColon("_paper_size");
            this.labelPaper2.Text = MainForm.getLanguageWithColon("_paper_size");

            this.bttExampleInvoice.Text = getLanguage("_preview");
            this.bttExampleReciept.Text = getLanguage("_preview");
            this.bttExampleContract.Text = getLanguage("_preview");

            // Grid
            this.grid_company_name.Caption = MainForm.getLanguage("_business_name");
            this.grid_company_owner_name.Caption = MainForm.getLanguage("_business_owner");

            this.bttEdit.Text = MainForm.getLanguage("_edit");
            this.bttSave.Text = MainForm.getLanguage("_save");
            this.bttCancel.Text = MainForm.getLanguage("_cancel");

        }

        #region Method Zone

            void IntDateFormat() {

                DataTable DTdateFormat = new DataTable();

                if (current_lang == "th")
                {

                    DTdateFormat.Columns.Add("datetype_label", typeof(string));
                    DTdateFormat.Columns.Add("datetype_id", typeof(int));
                    DTdateFormat.Rows.Add(getLanguage("_date_format_select"), null);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("MM/dd/yyyy"), 1);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"), 2);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("dd MMMM yyyy"), 3);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("MMMM dd,yyyy"), 4);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("yyyy/MM/dd"), 5);
                }
                else {

                    DTdateFormat.Columns.Add("datetype_label", typeof(string));
                    DTdateFormat.Columns.Add("datetype_id", typeof(int));
                    DTdateFormat.Rows.Add(getLanguage("_paper_size_select"), null);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("MM/dd/yyyy"), 1);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"), 2);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("dd MMMM yyyy"), 3);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("MMM dd,yyyy"), 4);
                    DTdateFormat.Rows.Add(DateTime.Now.ToString("yyyy/MM/dd"), 5);
                }

                lookUpEditDateFormat.Properties.DataSource = DTdateFormat;
                lookUpEditDateFormat.Properties.DisplayMember = "datetype_label";
                lookUpEditDateFormat.Properties.ValueMember = "datetype_id";
                lookUpEditDateFormat.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_date_format_select");
            }

            void InitPaper() { 

                DataTable DTpaperSize = new DataTable();

                DTpaperSize.Columns.Add("papersize_label", typeof(string));
                DTpaperSize.Columns.Add("papersize_id", typeof(int));

                DTpaperSize.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_paper_size_select"), null);
                DTpaperSize.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_a4"), 1);
                DTpaperSize.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_a5"), 2);
                DTpaperSize.Rows.Add(DXWindowsApplication2.MainForm.getLanguage("_continue_paper"), 3);
                
                lookUpEditPaperInvoice.Properties.DataSource = DTpaperSize;
                lookUpEditPaperInvoice.Properties.DisplayMember = "papersize_label";
                lookUpEditPaperInvoice.Properties.ValueMember = "papersize_id";
                lookUpEditPaperInvoice.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_paper_size_select");

                lookUpEditPaperReciept.Properties.DataSource = DTpaperSize;
                lookUpEditPaperReciept.Properties.DisplayMember = "papersize_label";
                lookUpEditPaperReciept.Properties.ValueMember = "papersize_id";
                lookUpEditPaperReciept.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_paper_size_select");

            }

            void SetDisable() {

                gridControlCompany.Enabled = true;

                groupControlVat.Enabled = false;
                groupControlDocType.Enabled = false;
                groupControlContractFormat.Enabled = false;
                groupControlInvoiceFormat.Enabled = false;
                groupControlReceiptFormat.Enabled = false;

                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttEdit.Enabled = true;

            }

            //void SetDocumentType() { 
                
            //    object[] itemValues = new object[] { 0, 1, 2};

            //    string[] itemDescriptions = new string[] {getLanguage("_not_select"), getLanguage("_saperate_daily_daily"), getLanguage("_saperate_daily_monthly")};

            //    for (int i = 0; i < itemValues.Length; i++)
            //    {
            //        radioGroupContractType.Properties.Items.Add(new RadioGroupItem(itemValues[i], itemDescriptions[i]));
            //        radioGroupInvoiceType.Properties.Items.Add(new RadioGroupItem(itemValues[i], itemDescriptions[i]));
            //        radioGroupReciptType.Properties.Items.Add(new RadioGroupItem(itemValues[i], itemDescriptions[i]));
            //    }

            //    radioGroupContractType.EditValue = 0;
            //    radioGroupInvoiceType.EditValue = 0;
            //    radioGroupReciptType.EditValue = 0;
            //}

            void SetPositionHeader() {


                if (radioGroupLogoPosition.Properties.Items.Count < 3) {

                    // Create five items.

                    object[] itemValues = new object[] { 0, 1, 2 };

                    string[] itemDescriptions = new string[] { DXWindowsApplication2.MainForm.getLanguage("_left"), DXWindowsApplication2.MainForm.getLanguage("_center"), DXWindowsApplication2.MainForm.getLanguage("_right") };

                    for (int i = 0; i < itemValues.Length; i++)
                    {
                        radioGroupLogoPosition.Properties.Items.Add(new RadioGroupItem(itemValues[i], itemDescriptions[i]));
                    }

                    //for(int r = radioGroupLogoPosition.Properties.Items.Count; r > 3; r--){
                    //    radioGroupLogoPosition.Properties.Items.RemoveAt(r);
                    //}
                }
                
                
                
            }
            
                void SetEnable()
                {

                    gridControlCompany.Enabled = false;

                    groupControlVat.Enabled = true;
                    groupControlDocType.Enabled = true;
                    groupControlContractFormat.Enabled = true;
                    groupControlInvoiceFormat.Enabled = true;
                    groupControlReceiptFormat.Enabled = true;
                    bttSave.Enabled = true;
                    bttCancel.Enabled = true;
                    bttEdit.Enabled = false;
                }
            
                void initDocumentInfo() {
                 
                    IntDateFormat();

                textEditContractDateFormat.EditValue    = DateTime.Now.ToString("yyyyMMdd");
                textEditInvoiceDateFormat.EditValue     = DateTime.Now.ToString("yyyyMMdd");
                textEditReceiptDateFormat.EditValue     = DateTime.Now.ToString("yyyyMMdd");

                SetPositionHeader();
                //Select the Rectangle item.
                radioGroupLogoPosition.EditValue = 0;

                if (flagCancel == true) { 

                    DataRow CurrentRow = gridView1.GetDataRow(0);
                    TempCompany_id = Convert.ToInt32(CurrentRow["company_id"].ToString());
                    flagCancel = false;
                }

                DataTable docConfig = BusinessLogicBridge.DataStore.getDocumentConfigFromCompanyID(TempCompany_id);

                
                if (docConfig.Rows.Count > 0)
                {
                    button_event = "edit";

                    var dataParam = docConfig.Rows[0];

                    textEditDocumentID.EditValue = dataParam["doc_config_id"].To<int>();
                    textEditAmountVat.EditValue = dataParam["doc_vat"].ToString();
                    lookUpEditVatType.EditValue = dataParam["doc_vat_type"];

                    lookUpEditDateFormat.EditValue = dataParam["doc_dateformat"].To<int>();
                    // Contract
                    textEditPrefixContract.EditValue = dataParam["doc_contact_prefix"].ToString();
                    textEditContractStart.EditValue = dataParam["doc_contact_start"].ToString().PadLeft(6,'0');

                    if (dataParam["doc_saperate_contract"].To<bool>() == true) checkEditSaperateContract.Checked = true;
                    else checkEditSaperateContract.Checked = false;

                    if (dataParam["doc_saperate_invoice"].To<bool>() == true) checkEditSaperateInvoice.Checked = true;
                    else checkEditSaperateInvoice.Checked = false;

                    if (dataParam["doc_saperate_reciept"].To<bool>() == true) checkEditSaperateReciept.Checked = true;
                    else checkEditSaperateReciept.Checked = false;

                    lookUpEditPaperInvoice.EditValue = dataParam["doc_paper_invoice"];
                    lookUpEditPaperReciept.EditValue = dataParam["doc_paper_reciept"];

                    // Invoice
                    textEditPrefixInvoice.EditValue = dataParam["doc_inv_prefix"].ToString();
                    textEditInvoiceStart.EditValue = dataParam["doc_inv_start"].ToString().PadLeft(6, '0');
                    memoEditInvoiceHeader.EditValue = dataParam["doc_header_invoice"].ToString();
                    memoEditInvoiceFooter.EditValue = dataParam["doc_footer_invoice"].ToString();
                    textEditUnderInvoice1.EditValue = dataParam["doc_under_invoice1"].ToString();
                    textEditUnderInvoice2.EditValue = dataParam["doc_under_invoice2"].ToString();

                    // Reciept
                    textEditPrefixReceipt.EditValue = dataParam["doc_reciept_prefix"].ToString();
                    textEditReceiptStart.EditValue = dataParam["doc_reciept_start"].ToString().PadLeft(6, '0');
                    memoEditRecieptHeader.EditValue = dataParam["doc_header_reciept"].ToString();
                    memoEditRecieptFooter.EditValue = dataParam["doc_footer_reciept"].ToString();
                    textEditUnderReciept1.EditValue = dataParam["doc_under_reciept1"].ToString();
                    textEditUnderReciept2.EditValue = dataParam["doc_under_reciept2"].ToString();


                    // Store with temp for check update prefix
                    contact_prefix_temp = dataParam["doc_contact_prefix"].ToString();
                    invoice_prefix_temp = dataParam["doc_inv_prefix"].ToString();
                    reciept_prefix_temp = dataParam["doc_reciept_prefix"].ToString();
                    
                    radioGroupLogoPosition.SelectedIndex = Convert.ToInt16(dataParam["doc_logo_position"]);
                    SetDisable();
                }
                else{
                    button_event = "add";
                    SetEnable();
                    textEditAmountVat.EditValue = "";

                    // Contract
                    textEditPrefixContract.EditValue = "";
                    textEditContractStart.EditValue = "000001";

                    // Invoice
                    textEditPrefixInvoice.EditValue = "";
                    textEditInvoiceStart.EditValue = "000001";
                    memoEditInvoiceHeader.EditValue = "";
                    memoEditInvoiceFooter.EditValue = "";
                    textEditUnderInvoice1.EditValue = "";
                    textEditUnderInvoice2.EditValue = "";

                    // Recirpt
                    textEditPrefixReceipt.EditValue = "";
                    textEditReceiptStart.EditValue = "000001";
                    memoEditRecieptHeader.EditValue = "";
                    memoEditRecieptFooter.EditValue = "";
                    textEditUnderReciept1.EditValue = "";
                    textEditUnderReciept2.EditValue = "";

                    SetEnable();

                }

            }
        
            void InitVatType()
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


        #endregion

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

            public bool IsNumeric(string Text)
            {
                //Regex moneyR = new Regex(@"\d+\.\d{2}");
                try
                {
                    Convert.ToDouble(Text);
                    return true;
                }
                catch
                {
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

        #region Event Zone

        private void icbEditMask_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
            private void bttEdit_Click(object sender, EventArgs e)
            {
                button_event = "edit";
                update_state = 1;
                SetEnable();
            }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            // Check Update
            if (DialogResult.Yes == utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")))
            {
                flagCancel = true;
                initDocumentInfo();
            }
        }

            private void bttSave_Click(object sender, EventArgs e)
            {
                saveInfo();
                update_state = 0;
            }
        #endregion

        void saveInfo() {
                List<string> listError = new List<string>();

                string TextOnly = getLanguage("_text_only");
                string EmptySting = getLanguage("_msg_1001");
                string msgError = "";

                #region validate Vat Price

                if (isEmpty(textEditAmountVat.Text) == true)
                {
                    if (IsNumeric(textEditAmountVat.Text) == false)
                    {
                        listError.Add(labelVat.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelVat.Text + EmptySting.ToString());
                }
                #endregion

                #region DateFormat
                if (lookUpEditDateFormat.EditValue.ToString() == "")
                {
                    listError.Add(labelControlDateFormat.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Contract Prefix

                if (textEditPrefixContract.EditValue.ToString() != "")
                {
                    if (IsAlphaNumeric(textEditPrefixContract.Text) == false)
                    {
                        listError.Add(labelPrefix1.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelPrefix1.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Contract Start Number

                if (isEmpty(textEditContractStart.Text) == true)
                {
                    if (IsNumeric(textEditContractStart.Text) == false)
                    {
                        listError.Add(labelStart1.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelStart1.Text + EmptySting.ToString());
                }
                #endregion

                // Invoice
                #region validate Invoice Prefix

                if (isEmpty(textEditPrefixInvoice.Text) == true)
                {
                    if (IsAlphaNumeric(textEditPrefixInvoice.Text) == false)
                    {
                        listError.Add(labelPrefix2.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelPrefix2.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Invoice Start Number

                if (isEmpty(textEditInvoiceStart.Text) == true)
                {
                    if (IsNumeric(textEditInvoiceStart.Text) == false)
                    {
                        listError.Add(labelStart2.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelStart2.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Invoice Header

                if (isEmpty(memoEditInvoiceHeader.Text) == true)
                {
                    if (IsAlphaNumeric(memoEditInvoiceHeader.Text) == false)
                    {
                        listError.Add(labelHeader1.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelHeader1.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Invoice Footer

                if (isEmpty(memoEditInvoiceFooter.Text) == true)
                {
                    if (IsAlphaNumeric(memoEditInvoiceFooter.Text) == false)
                    {
                        listError.Add(labelFooter1.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelFooter1.Text + EmptySting.ToString());
                }
                #endregion

                #region validate UnderInvoice1

                if (isEmpty(textEditUnderInvoice1.Text) == true)
                {
                    if (IsAlphaNumeric(textEditUnderInvoice1.Text) == false)
                    {
                        listError.Add(labelUnder11.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelUnder11.Text + EmptySting.ToString());
                }
                #endregion

                #region validate UnderInvoice2

                if (isEmpty(labelUnder11.Text) == true)
                {
                    if (IsAlphaNumeric(labelUnder11.Text) == false)
                    {
                        listError.Add(labelUnder12.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelUnder12.Text + EmptySting.ToString());
                }
                #endregion

                #region invoice paper size
                if (lookUpEditPaperInvoice.EditValue.ToString() == "")
                {
                    listError.Add(labelPaper1.Text + EmptySting.ToString());
                }
                #endregion

                // Reciept
                #region validate Reciept Prefix

                if (isEmpty(textEditPrefixReceipt.Text) == true)
                {
                    if (IsAlphaNumeric(textEditPrefixReceipt.Text) == false)
                    {
                        listError.Add(labelPrefix3.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelPrefix3.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Reciept Start Number

                if (isEmpty(textEditReceiptStart.Text) == true)
                {
                    if (IsNumeric(textEditReceiptStart.Text) == false)
                    {
                        listError.Add(textEditReceiptStart.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelStart3.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Reciept Header

                if (isEmpty(memoEditRecieptHeader.Text) == true)
                {
                    if (IsAlphaNumeric(memoEditRecieptHeader.Text) == false)
                    {
                        listError.Add(labelHeader2.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelHeader2.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Reciept Footer

                if (isEmpty(memoEditRecieptFooter.Text) == true)
                {
                    if (IsAlphaNumeric(memoEditRecieptFooter.Text) == false)
                    {
                        listError.Add(labelFooter2.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelFooter2.Text + EmptySting.ToString());
                }
                #endregion

                #region validate UnderReciept1

                if (isEmpty(textEditUnderReciept1.Text) == true)
                {
                    if (IsAlphaNumeric(textEditUnderReciept1.Text) == false)
                    {
                        listError.Add(labelUnder21.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelUnder21.Text + EmptySting.ToString());
                }
                #endregion

                #region validate UnderReciept2

                if (isEmpty(textEditUnderReciept2.Text) == true)
                {
                    if (IsAlphaNumeric(textEditUnderReciept2.Text) == false)
                    {
                        listError.Add(labelUnder22.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelUnder22.Text + EmptySting.ToString());
                }
                #endregion

                #region reciept paper size
                if (lookUpEditPaperReciept.EditValue.ToString() == "")
                {
                    listError.Add(labelPaper2.Text + EmptySting.ToString());
                }
                #endregion
                
                if (listError.Count > 0)
                {

                    foreach (string x in listError)
                    {
                        msgError += x + "\r\n";
                    }

                    utilClass.showPopupMessegeBox(this, msgError, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
                else
                {
                    // Define Value For Update Document Information
                    try
                    {
                        if (button_event == "add")
                        {
                          int doc_config_id = BusinessLogicBridge.DataStore.insertDocumentConfig(
                                textEditAmountVat.EditValue.ToString(),         // จำนวนราคาภาษี
                                lookUpEditVatType.EditValue.ToString(),         // ประเภทภาษี
                                textEditPrefixContract.Text,                // อักษรนำหน้าสัญญา
                                textEditContractStart.EditValue.To<int>(), // เลขที่เอกสารเริ่มต้น
                                textEditPrefixInvoice.Text,                 // อักษรนำหน้าใบแจ้งหนี้
                                textEditInvoiceStart.EditValue.To<int>(),  // เลขที่เอกสารเริ่มต้น
                                memoEditInvoiceHeader.Text,                 // ข้อความหัวใบแจ้งหนี้
                                memoEditInvoiceFooter.Text,                 // ข้อความใต้ใบแจ้งหนี้
                                textEditUnderInvoice1.Text,                 // ข้อความใต้ลายเซ็นต์ 1
                                textEditUnderInvoice2.Text,                 // ข้อความใต้ลายเซ็นต์ 2
                                textEditPrefixReceipt.Text,                 // อักษรนำหน้าใบเสร็จ
                                textEditReceiptStart.EditValue.To<int>(),  // เลขที่เอกสารเริ่มต้น
                                memoEditRecieptHeader.Text,                 // ข้อความหัวใบเสร็จรับเงิน
                                memoEditRecieptFooter.Text,                 // ข้อความใต้ใบเสร็จรับเงิน
                                textEditUnderReciept1.Text,                 // ข้อความใต้ลายเซ็นต์ 1
                                textEditUnderReciept2.Text,                 // ข้อความใต้ลายเซ็นต์ 2
                                lookUpEditDateFormat.EditValue.ToString(),
                                radioGroupLogoPosition.EditValue.To<int>(),
                                TempCompany_id,
                                checkEditSaperateContract.Checked,
                                checkEditSaperateInvoice.Checked,
                                checkEditSaperateReciept.Checked,
                                lookUpEditPaperInvoice.EditValue.To<int>(),
                                lookUpEditPaperReciept.EditValue.To<int>()
                            );

                            // Insert Document Log
                            BusinessLogicBridge.DataStore.insertDocumentPrefixLog(textEditPrefixContract.EditValue.ToString().ToUpper(), textEditPrefixContract.EditValue.ToString().ToUpper(), textEditPrefixInvoice.EditValue.ToString().ToUpper(), textEditPrefixInvoice.EditValue.ToString().ToUpper(), textEditPrefixReceipt.EditValue.ToString().ToUpper(), textEditPrefixReceipt.EditValue.ToString().ToUpper(), doc_config_id);
                        }
                        else
                        {
                            BusinessLogicBridge.DataStore.updateDocumentConfig(
                                textEditAmountVat.EditValue.ToString(),                     // จำนวนราคาภาษี
                                lookUpEditVatType.EditValue.ToString(),         // ประเภทภาษี
                                textEditPrefixContract.Text,                // อักษรนำหน้าสัญญา
                                textEditContractStart.EditValue.To<int>(), // เลขที่เอกสารเริ่มต้น
                                textEditPrefixInvoice.Text,                 // อักษรนำหน้าใบแจ้งหนี้
                                textEditInvoiceStart.EditValue.To<int>(),  // เลขที่เอกสารเริ่มต้น
                                memoEditInvoiceHeader.Text,                 // ข้อความหัวใบแจ้งหนี้
                                memoEditInvoiceFooter.Text,                 // ข้อความใต้ใบแจ้งหนี้
                                textEditUnderInvoice1.Text,                 // ข้อความใต้ลายเซ็นต์ 1
                                textEditUnderInvoice2.Text,                 // ข้อความใต้ลายเซ็นต์ 2
                                textEditPrefixReceipt.Text,                 // อักษรนำหน้าใบเสร็จ
                                textEditReceiptStart.Text.To<int>(),  // เลขที่เอกสารเริ่มต้น
                                memoEditRecieptHeader.Text,                 // ข้อความหัวใบเสร็จรับเงิน
                                memoEditRecieptFooter.Text,                 // ข้อความใต้ใบเสร็จรับเงิน
                                textEditUnderReciept1.Text,                 // ข้อความใต้ลายเซ็นต์ 1
                                textEditUnderReciept2.Text,                 // ข้อความใต้ลายเซ็นต์ 2
                                lookUpEditDateFormat.EditValue.ToString(),
                                radioGroupLogoPosition.EditValue.To<int>(),
                                textEditDocumentID.EditValue.To<int>(),
                                checkEditSaperateContract.Checked,
                                checkEditSaperateInvoice.Checked,
                                checkEditSaperateReciept.Checked,
                                lookUpEditPaperInvoice.EditValue.To<int>(),
                                lookUpEditPaperReciept.EditValue.To<int>()
                            );

                            // Check Old Log Contract Prefix
                            if(contact_prefix_temp!=textEditPrefixContract.EditValue.ToString())
                                BusinessLogicBridge.DataStore.updateDocumentPrefixLogContract(contact_prefix_temp, textEditPrefixContract.EditValue.ToString(), textEditDocumentID.EditValue.To<int>()); // Update Document Contract prefix

                            // Check Old Log Contract Prefix
                            if (invoice_prefix_temp != textEditPrefixInvoice.EditValue.ToString())
                                BusinessLogicBridge.DataStore.updateDocumentPrefixLogContract(invoice_prefix_temp,textEditPrefixInvoice.EditValue.ToString(), textEditDocumentID.EditValue.To<int>()); // Update Document Invoice prefix

                            // Check Old Log Contract Prefix
                            if (reciept_prefix_temp != textEditPrefixReceipt.EditValue.ToString())
                                BusinessLogicBridge.DataStore.updateDocumentPrefixLogContract(invoice_prefix_temp, textEditPrefixReceipt.EditValue.ToString(),textEditDocumentID.EditValue.To<int>()); // Update Document Reciept prefix
                            
                        }
                        DXWindowsApplication2.MainForm.setTogglePage();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString(), ex);
                    }

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");
                    SetDisable();
                }
            }

        private void checkEditSaperateContract_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditSaperateContract.Checked == true)
            {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + "M" + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }
            else {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }
        }

        private void checkEditSaperateInvoice_CheckedChanged(object sender, EventArgs e)
        {

            if (checkEditSaperateInvoice.Checked == true)
            {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + "M" + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
            else {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
        }

        private void checkEditSaperateReciept_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditSaperateReciept.Checked == true)
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() +"M"+ textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
            else
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() + textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
        }

        private void textEditPrefixContract_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEditSaperateContract.Checked == true)
            {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + "M" + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }
            else
            {
                textEditExampleContract.EditValue = textEditPrefixContract.EditValue.ToString() + textEditContractDateFormat.EditValue.ToString() + textEditContractStart.EditValue.ToString();
            }
        }

        private void textEditPrefixInvoice_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEditSaperateInvoice.Checked == true)
            {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + "M" + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
            else
            {
                textEditExampleInvoice.EditValue = textEditPrefixInvoice.EditValue.ToString() + textEditInvoiceDateFormat.EditValue.ToString() + textEditInvoiceStart.EditValue.ToString();
            }
        }

        private void textEditPrefixReceipt_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEditSaperateReciept.Checked == true)
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() + "M" + textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
            else
            {
                textEditExampleReceipt.EditValue = textEditPrefixReceipt.EditValue.ToString() + textEditReceiptDateFormat.EditValue.ToString() + textEditReceiptStart.EditValue.ToString();
            }
        }

        private void bttExampleContract_Click(object sender, EventArgs e)
        {
            PrintDocuments.contract_preview PrintContract = new DXWindowsApplication2.PrintDocuments.contract_preview();

            PrintContract.loopGenDataRow(TempCompany_id,textEditExampleContract.Text,lookUpEditDateFormat.Text);

            PrintContract.ShowPreview();            

        }

        private void bttExampleInvoice_Click(object sender, EventArgs e)
        {
            PrintDocuments.invoice_preview PrintContract = new DXWindowsApplication2.PrintDocuments.invoice_preview();

            PrintContract.loopGenDataRow(TempCompany_id, textEditExampleInvoice.EditValue.ToString().ToUpper(), lookUpEditDateFormat.Text, memoEditInvoiceHeader.Text, memoEditInvoiceFooter.Text, textEditUnderInvoice1.Text, textEditUnderInvoice2.Text, lookUpEditPaperInvoice.Text, radioGroupLogoPosition.SelectedIndex);

            PrintContract.ShowPreview();
        }

        private void bttExampleReciept_Click(object sender, EventArgs e)
        {
            PrintDocuments.reciept_preview PrintContract = new DXWindowsApplication2.PrintDocuments.reciept_preview();

            PrintContract.loopGenDataRow(TempCompany_id, textEditExampleReceipt.EditValue.ToString().ToUpper(), lookUpEditDateFormat.Text, memoEditRecieptHeader.Text, memoEditRecieptFooter.Text, textEditUnderReciept1.Text, textEditUnderReciept2.Text, lookUpEditPaperReciept.Text, radioGroupLogoPosition.SelectedIndex);

            PrintContract.ShowPreview();
        }
           
    }
}
