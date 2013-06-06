using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoApartment : DevExpress.XtraEditors.XtraUserControl
    {
        private static string LogoPathStatic;

        public BasicInfoApartment()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            initCompany();
        }

        #region Method Zone

        void initCompany()
        {
            DataTable Company = BusinessLogicBridge.DataStore.getCompany();
            if (Company.Rows.Count > 0)
            {
                txtEditId.EditValue             = Company.Rows[0]["company_id"].ToString();
                txtEditCompanyName.EditValue    = Company.Rows[0]["company_name"].ToString();
                mmAddress.Text          = Company.Rows[0]["company_address"].ToString();
                txtEditTelephone.Text   = Company.Rows[0]["company_telephone"].ToString();
                txtEditFax.Text         = Company.Rows[0]["company_fax"].ToString();
                txtEditEmail.Text       = Company.Rows[0]["company_email"].ToString();
                txtEditWeb.Text         = Company.Rows[0]["company_website"].ToString();
                txtEditIdCard.Text      = Company.Rows[0]["company_id_card"].ToString();
                txtEditCorporate.Text   = Company.Rows[0]["company_corporate"].ToString();
                txtEditOwnerName.Text   = Company.Rows[0]["company_owner_name"].ToString();
                txtEditVision.Text      = Company.Rows[0]["company_vision"].ToString();
                textEditLogoPath.Text   = Company.Rows[0]["company_logo"].ToString();

               // if (Directory.Exists(textEditFileDocument.Text) == false)


                if (File.Exists(textEditLogoPath.Text))
                {
                    pictureEdit1.Image = new Bitmap(textEditLogoPath.Text);
                }
                else {
                    pictureEdit1.Image = null;
                }
            }
        }

        void setDisable() {
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            panelEnable.Enabled = false;
        }

        void setEnable()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            panelEnable.Enabled = true;
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
            if ((Text.Length > 0) && (Text !=""))
            {
                string strRegex = @"[^a-zA-Z0-9ก-๙\.\,-\/ ]?$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(Text))
                    return true;
                else
                    return false;
            }
            else {
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

        #region Button Zone

        private void bttSave_Click(object sender, EventArgs e)
        {
            List<string> listError = new List<string>(); 

            string TextOnly             = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
            string EmptySting           = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";

            string company_id           = txtEditId.Text.ToString();
            string company_name         = txtEditCompanyName.Text.ToString();
            string company_address      = mmAddress.Text.ToString();
            string company_telephone    = txtEditTelephone.Text.ToString();
            string company_fax          = txtEditFax.Text.ToString();
            string company_email        = txtEditEmail.Text.ToString();
            string company_website      = txtEditWeb.Text.ToString();
            string company_id_card      = txtEditIdCard.Text.ToString();
            string company_corporate    = txtEditCorporate.Text.ToString();
            string company_owner_name   = txtEditOwnerName.Text.ToString();
            string company_vision       = txtEditVision.Text.ToString();

            string company_logo         = textEditLogoPath.Text.ToString();
            
            #region validate Company name
                       

            if (isEmpty(company_name) == true){
                if (IsAlphaNumeric(company_name) == false)
                {
                    listError.Add(lbApartmentName.Text + TextOnly.ToString());
                }
            }else{
                    listError.Add(lbApartmentName.Text + EmptySting.ToString());
            }
            #endregion

            #region validate Address

            if (isEmpty(company_address) == true)
            {
                if (IsAlphaNumeric(company_address) == false)
                {
                    listError.Add(lbApartmentAddress.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(lbApartmentAddress.Text + EmptySting.ToString());
            }

            #endregion
            
            #region validate telephone

            if (isEmpty(company_telephone) == true)
            {
                if (IsAlphaNumeric(company_telephone) == false)
                {
                    listError.Add(lbApartmentTel.Text + TextOnly.ToString());
                    
                }
            }else{
                    listError.Add(lbApartmentTel.Text + EmptySting.ToString());
            }

            #endregion
            
            #region validate Fax
            
            if(isEmpty(company_fax) == true)
            {

                if (IsAlphaNumeric(company_fax) == false)
                {
                    listError.Add(lbApartmentFax.Text + TextOnly.ToString());
                }
            }

            #endregion

            #region validate Email
            if (isEmpty(company_email) == true)
            {
                if (IsEmail(company_email) == false)
                {

                    listError.Add(lbApartmentEmail.Text + " กรุณาระบุอีเมล์ให้ถูกต้อง");
                }
            }

            #endregion

            #region validate Website

            if (isEmpty(company_website) == true)
            {

                if (isValidUrl(company_website) == false)
                {
                    listError.Add(lbApartmentWeb.Text + " กรุณาระบุรูปแบบเว็บไซต์ให้ถูกต้อง");
                }
            }

            #endregion

            #region validate VAT

            if (isEmpty(company_id_card) == true)
            {

                if (IsAlphaNumeric(company_id_card) == false)
                {
                    listError.Add(lbApartmentOwnerID.Text + TextOnly.ToString());
                }
            }

            #endregion

            #region validate NITI

            if (isEmpty(company_corporate) == true)
            {

                if (IsAlphaNumeric(company_corporate) == false)
                {
                    listError.Add(lbApartmentNitiId.Text + TextOnly.ToString());
                }
            }

            #endregion

            #region validate Owner name

            if (isEmpty(company_owner_name) == true)
            {

                if (IsAlphaNumeric(company_owner_name) == false)
                {
                    listError.Add(lbApartmentOwnerName.Text + TextOnly.ToString());
                }
            }

            #endregion

            #region validate Policy

            if (isEmpty(company_vision) == true)
            {

                if (IsAlphaNumeric(company_vision) == false)
                {
                    listError.Add(lbApartmentPolicy.Text + TextOnly.ToString());
                }
            }

            #endregion

                string msgError = "";

                if (listError.Count > 0)
                {

                    foreach (string x in listError)
                    {
                        msgError += x + "\r\n";
                    }

                    XtraMessageBox.Show(msgError,"!!! ข้อผิดพลาด !!!");

                }
                else
                {


                    DataTable _companyInfoTable = new DataTable();
                    _companyInfoTable.Columns.Add("company_id", typeof(string));
                    _companyInfoTable.Columns.Add("company_name", typeof(string));
                    _companyInfoTable.Columns.Add("company_address", typeof(string));
                    _companyInfoTable.Columns.Add("company_telephone", typeof(string));
                    _companyInfoTable.Columns.Add("company_fax", typeof(string));
                    _companyInfoTable.Columns.Add("company_email", typeof(string));
                    _companyInfoTable.Columns.Add("company_website", typeof(string));
                    _companyInfoTable.Columns.Add("company_id_card", typeof(string));
                    _companyInfoTable.Columns.Add("company_corporate", typeof(string));
                    _companyInfoTable.Columns.Add("company_owner_name", typeof(string));
                    _companyInfoTable.Columns.Add("company_vision", typeof(string));
                    _companyInfoTable.Columns.Add("company_logo", typeof(string));

                    

                    string floor_id = txtEditId.Text.ToString();

                    if (floor_id != "")
                    {
                        DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไข", "", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            FileInfo SourceFI = new FileInfo(LogoPathStatic);

                            company_logo = LogoPathStatic.Replace("\\Temp\\", "\\Images\\");

                            SourceFI.CopyTo(company_logo, true);


                            _companyInfoTable.Rows.Add(company_id, company_name, company_address, company_telephone, company_fax, company_email, company_website, company_id_card, company_corporate, company_owner_name, company_vision, company_logo);

                            BusinessLogicBridge.DataStore.updateCompany(_companyInfoTable);
                            setDisable();
                        }
                        else {
                            LogoPathStatic = company_logo;
                            initCompany();
                        }
                    }
                    else
                    {
                        DialogResult dr = XtraMessageBox.Show("ยืนยันการเพิ่มข้อมูล", "", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            BusinessLogicBridge.DataStore.insertCompany(_companyInfoTable);
                            setDisable();
                        }
                    }
                }
        }

        private void simpleButtonUpload_Click(object sender, EventArgs e)
        {
            int MAX_FILE_SIZE = 51200; // 50 Kb            

                OpenFileDialog open = new OpenFileDialog();

                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    DataTable configPath = BusinessLogicBridge.DataStore.getGeneralConfig();


                    FileInfo SourceFI = new FileInfo(open.FileName);
                    //FileInfo DestFI   = new FileInfo(Path.Combine(textEditLogoPath.Text, SourceFI.Name));
                    textEditLogoPath.EditValue = configPath.Rows[0]["path_all_document"] + "\\Temp\\" + open.SafeFileName.Trim();

                    LogoPathStatic = configPath.Rows[0]["path_all_document"] + "\\Temp\\" + open.SafeFileName.Trim();

                    FileInfo DestFI = new FileInfo(textEditLogoPath.Text);
                    bool DoCopy = true;

                    int fileSize = (int)new FileInfo(open.FileName).Length;


                    if (fileSize <= MAX_FILE_SIZE)
                    {

                        pictureEdit1.Image = new Bitmap(open.FileName);

                        

                        if (DestFI.Exists)
                        {
                            if (XtraMessageBox.Show(this, "ไฟล์มีอยู่แล้วในระบบ. ต้องการบันทึกทับไฟล์เดิมใช่หรือไม่ ?",
                                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                DoCopy = false;
                            }

                        }

                        try
                        {
                            if (DoCopy)
                            {
                                try
                                {
                                    SourceFI.CopyTo(textEditLogoPath.Text, true);
                                    XtraMessageBox.Show("ไฟล์อัพโหลดเรียบร้อย");
                                }
                                catch (Exception ex) {
                                    throw new ApplicationException(ex.Message.ToString());
                                }
                                
                            }
                            else
                            {
                                XtraMessageBox.Show("ไฟล์ไม่สามารถอัพโหลดได้");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message.ToString());
                        }
                    
                    }else
                    {
                        XtraMessageBox.Show("ขนาดไฟล์ต้องไม่เกิน 50 kb");
                    }
                }
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEnable();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pictureEdit1.Image = null;
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            
            // Check Data Changing
            DialogResult drx = XtraMessageBox.Show("ข้อมูลมีการแก้ไข คุณต้องการบันทึกหรือไม่ ?", "", MessageBoxButtons.OKCancel);
            if (drx == DialogResult.OK)
            {
                List<string> listError = new List<string>();

                string TextOnly = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
                string EmptySting = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";

                string company_id = txtEditId.Text.ToString();
                string company_name = txtEditCompanyName.Text.ToString();
                string company_address = mmAddress.Text.ToString();
                string company_telephone = txtEditTelephone.Text.ToString();
                string company_fax = txtEditFax.Text.ToString();
                string company_email = txtEditEmail.Text.ToString();
                string company_website = txtEditWeb.Text.ToString();
                string company_id_card = txtEditIdCard.Text.ToString();
                string company_corporate = txtEditCorporate.Text.ToString();
                string company_owner_name = txtEditOwnerName.Text.ToString();
                string company_vision = txtEditVision.Text.ToString();
                string company_logo = textEditLogoPath.Text.ToString();

                #region validate Company name


                if (isEmpty(company_name) == true)
                {
                    if (IsAlphaNumeric(company_name) == false)
                    {
                        listError.Add(lbApartmentName.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(lbApartmentName.Text + EmptySting.ToString());
                }
                #endregion

                #region validate Address

                if (isEmpty(company_address) == true)
                {
                    if (IsAlphaNumeric(company_address) == false)
                    {
                        listError.Add(lbApartmentAddress.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(lbApartmentAddress.Text + EmptySting.ToString());
                }

                #endregion

                #region validate telephone

                if (isEmpty(company_telephone) == true)
                {
                    if (IsAlphaNumeric(company_telephone) == false)
                    {
                        listError.Add(lbApartmentTel.Text + TextOnly.ToString());

                    }
                }
                else
                {
                    listError.Add(lbApartmentTel.Text + EmptySting.ToString());
                }

                #endregion

                #region validate Fax

                if (isEmpty(company_fax) == true)
                {

                    if (IsAlphaNumeric(company_fax) == false)
                    {
                        listError.Add(lbApartmentFax.Text + TextOnly.ToString());
                    }
                }

                #endregion

                #region validate Email
                if (isEmpty(company_email) == true)
                {
                    if (IsEmail(company_email) == false)
                    {

                        listError.Add(lbApartmentEmail.Text + " กรุณาระบุอีเมล์ให้ถูกต้อง");
                    }
                }

                #endregion

                #region validate Website

                if (isEmpty(company_website) == true)
                {

                    if (isValidUrl(company_website) == false)
                    {
                        listError.Add(lbApartmentWeb.Text + " กรุณาระบุรูปแบบเว็บไซต์ให้ถูกต้อง");
                    }
                }

                #endregion

                #region validate VAT

                if (isEmpty(company_id_card) == true)
                {

                    if (IsAlphaNumeric(company_id_card) == false)
                    {
                        listError.Add(lbApartmentOwnerID.Text + TextOnly.ToString());
                    }
                }

                #endregion

                #region validate NITI

                if (isEmpty(company_corporate) == true)
                {

                    if (IsAlphaNumeric(company_corporate) == false)
                    {
                        listError.Add(lbApartmentNitiId.Text + TextOnly.ToString());
                    }
                }

                #endregion

                #region validate Owner name

                if (isEmpty(company_owner_name) == true)
                {

                    if (IsAlphaNumeric(company_owner_name) == false)
                    {
                        listError.Add(lbApartmentOwnerName.Text + TextOnly.ToString());
                    }
                }

                #endregion

                #region validate Policy

                if (isEmpty(company_vision) == true)
                {

                    if (IsAlphaNumeric(company_vision) == false)
                    {
                        listError.Add(lbApartmentPolicy.Text + TextOnly.ToString());
                    }
                }

                #endregion

                string msgError = "";

                if (listError.Count > 0)
                {

                    foreach (string x in listError)
                    {
                        msgError += x + "\r\n";
                    }

                    XtraMessageBox.Show(msgError, "!!! ข้อผิดพลาด !!!");

                }
                else
                {

                    DataTable _companyInfoTable = new DataTable();
                    _companyInfoTable.Columns.Add("company_id", typeof(string));
                    _companyInfoTable.Columns.Add("company_name", typeof(string));
                    _companyInfoTable.Columns.Add("company_address", typeof(string));
                    _companyInfoTable.Columns.Add("company_telephone", typeof(string));
                    _companyInfoTable.Columns.Add("company_fax", typeof(string));
                    _companyInfoTable.Columns.Add("company_email", typeof(string));
                    _companyInfoTable.Columns.Add("company_website", typeof(string));
                    _companyInfoTable.Columns.Add("company_id_card", typeof(string));
                    _companyInfoTable.Columns.Add("company_corporate", typeof(string));
                    _companyInfoTable.Columns.Add("company_owner_name", typeof(string));
                    _companyInfoTable.Columns.Add("company_vision", typeof(string));
                    _companyInfoTable.Columns.Add("company_logo", typeof(string));

                    _companyInfoTable.Rows.Add(company_id, company_name, company_address, company_telephone, company_fax, company_email, company_website, company_id_card, company_corporate, company_owner_name, company_vision, company_logo);

                    string floor_id = txtEditId.Text.ToString();

                    if (floor_id != "")
                    {
                        DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไข", "", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            BusinessLogicBridge.DataStore.updateCompany(_companyInfoTable);
                            setDisable();
                        }
                        else {
                            initCompany();
                        }
                    }
                    else
                    {
                        DialogResult dr = XtraMessageBox.Show("ยืนยันการเพิ่มข้อมูล", "", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            BusinessLogicBridge.DataStore.insertCompany(_companyInfoTable);
                            setDisable();
                        }
                    }
                }
            }
            else
            {
                initCompany();
            }

            // if yes so do you want to save
            // yes => save 
            // no => reload set enable -> false


            setDisable();

        }

        #endregion

    }
}
