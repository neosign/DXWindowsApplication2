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
using DevExpress.XtraGrid.Localization;



namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoCompany : uBase
    {
        private string button_event = "";
        public static XtraMessageBoxForm loginPanel;

        public BasicInfoCompany()
        {
                InitializeComponent();
                setLangThis();
                this.Dock = DockStyle.Fill;
                initCompany();
                changeRow();
                SaveClick += new EventHandler(BasicInfoCompany_SaveClick);
        }

        void BasicInfoCompany_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        public void setLangThis(){

            this.groupControl_BusinessList.Text = getLanguage("_business_list");
            this.groupControl_BusinessInfo.Text = getLanguage("_business_info");
            this.labelControlCompanyName.Text   = getLanguage("_business_name");
            this.labelControlAddress.Text       = getLanguage("_address");
            this.labelControlTelephone.Text     = getLanguage("_tel_no");
            this.labelControlFax.Text           = getLanguage("_fax_no");
            this.labelControlEmail.Text         = getLanguage("_email");
            this.labelControlWebSite.Text       = getLanguage("_business_website");
            this.labelControlRegisterNo.Text    = getLanguage("_business_register_no");
            this.labelControlTaxLicenceNo.Text  = getLanguage("_business_tax_no");
            this.labelControlOwnerName.Text     = getLanguage("_business_owner_name");
            this.labelControlPolicy.Text        = getLanguage("_business_policy");
            this.labelControlRequired.Text      = getLanguage("_required");

            // Logo
            this.bttBrowse.Text             = getLanguage("_browse");
            this.bttRemove.Text             = getLanguage("_delete");
            this.labelControlLogo.Text      = getLanguage("_business_apartment_logo");
            this.richTextBox_Notice.Text    = getLanguage("_image_notice");

            // grid captrue
            this.company_company_name.Caption       = getLanguage("_business_name");
            this.company_company_address.Caption    = getLanguage("_address");
            this.company_company_telephone.Caption  = getLanguage("_tel_no");

            this.bttAdd.Text        = getLanguage("_add");
            this.bttEdit.Text       = getLanguage("_edit");
            this.bttDelete.Text     = getLanguage("_delete");
            this.bttSave.Text       = getLanguage("_save");
            this.bttCancel.Text     = getLanguage("_cancel");

        }

        #region Setup
            void initCompany()
        {
            DataTable CompanyTable = BusinessLogicBridge.DataStore.BasicInfoCompany_get();
            gridControlCompany.DataSource = CompanyTable;
        }
        #endregion

        #region Action Extra
        void initDataDefault()
        {
            textEditId.EditValue = 0;
            textEditCompanyName.EditValue = "";
            memoEditAddress.EditValue = "";
            textEditTelephone.EditValue = "";
            textEditTelephone.EditValue = "";
            textEditFax.EditValue = "";
            textEditEmail.EditValue = "";
            textEditWebSite.EditValue = "";
            textEditIdCard.EditValue = "";
            textEditCorporate.EditValue = "";
            textEditOwnerName.EditValue = "";
            textEditVision.EditValue = "";
            textEditLogoPath.EditValue = "";
            textEditOldLogoPath.EditValue = "";
            textEditSouceLogoPath.EditValue = "";
            pictureEditLogo.EditValue = "";

            bttAdd.Focus();
        }
        private DataTable validateData()
        {
            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));
            if (textEditCompanyName.EditValue.ToString() == "")
            {
                label = labelControlCompanyName.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditCompanyName.Focus();
                    focus = true;
                }
            }
            else {

                DataRow[] listCompanyName = ((DataTable)gridControlCompany.DataSource).Select("company_id <> " + textEditId.EditValue.To<int>());

                for (int i = 0; i < listCompanyName.Length; i++)
                {
                    if (textEditCompanyName.EditValue.ToString().Trim() == listCompanyName[i]["company_name"].ToString().Trim()) {
                        label = labelControlCompanyName.Text;
                        message = getLanguage("_msg_1003");
                        _ValidateTable.Rows.Add(label, message);
                    }
                }
            }

            if (memoEditAddress.EditValue.ToString() == "")
            {
                label = labelControlAddress.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    memoEditAddress.Focus();
                    focus = true;
                }
            }
            if (textEditTelephone.EditValue.ToString() == "")
            {
                label = labelControlTelephone.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditTelephone.Focus();
                    focus = true;
                }
            } 

            if (textEditEmail.EditValue.ToString() != "")
            {
                string strRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                //string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";

                Regex re = new Regex(strRegex);
                if (re.IsMatch(textEditEmail.EditValue.ToString()) == false)
                {
                    label = labelControlEmail.Text;
                    message = getLanguage("_msg_1002");
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditEmail.Focus();
                        focus = true;
                    }
                }
            }
            
            if (button_event == "Add")
            {
                bool CompanyDuplicated = BusinessLogicBridge.DataStore.CheckCompanyName(textEditCompanyName.EditValue.ToString());

                if (CompanyDuplicated == true)
                {
                    label = labelControlCompanyName.Text;
                    message = getLanguage("_msg_1003");
                    _ValidateTable.Rows.Add(label, message);
                }
            }

            return _ValidateTable;
        }
        void enable(Boolean status)
        {
            textEditCompanyName.Enabled = status;
            memoEditAddress.Enabled = status;
            textEditTelephone.Enabled = status;
            textEditTelephone.Enabled = status;
            textEditFax.Enabled = status;
            textEditEmail.Enabled = status;
            textEditWebSite.Enabled = status;
            textEditIdCard.Enabled = status;
            textEditCorporate.Enabled = status;
            textEditOwnerName.Enabled = status;
            textEditVision.Enabled = status;
            //pictureEditLogo.Enabled = status;
            bttBrowse.Enabled = status;
            bttRemove.Enabled = status;
        }
        void clearData()
        {
            textEditCompanyName.EditValue = "";
            memoEditAddress.EditValue = "";
            textEditTelephone.EditValue = "";
            textEditTelephone.EditValue = "";
            textEditFax.EditValue = "";
            textEditEmail.EditValue = "";
            textEditWebSite.EditValue = "";
            textEditIdCard.EditValue = "";
            textEditCorporate.EditValue = "";
            textEditOwnerName.EditValue = "";
            textEditVision.EditValue = "";
            textEditLogoPath.EditValue = "";
            textEditOldLogoPath.EditValue = "";
            textEditSouceLogoPath.EditValue = "";
            pictureEditLogo.EditValue = "";
        }
        void changeRow()
        {
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridView1.GetDataRow(0);
                }
                textEditId.EditValue = int.Parse(CurrentRow["company_id"].ToString());
                textEditCompanyName.EditValue = CurrentRow["company_name"].ToString();
                memoEditAddress.EditValue = CurrentRow["company_address"].ToString();
                textEditTelephone.EditValue = CurrentRow["company_telephone"].ToString();
                textEditFax.EditValue = CurrentRow["company_fax"].ToString();
                textEditEmail.EditValue = CurrentRow["company_email"].ToString();
                textEditWebSite.EditValue = CurrentRow["company_website"].ToString();
                textEditIdCard.EditValue = CurrentRow["company_id_card"].ToString();
                textEditCorporate.EditValue = CurrentRow["company_tax_id"].ToString();
                textEditOwnerName.EditValue = CurrentRow["company_owner_name"].ToString();
                textEditVision.EditValue = CurrentRow["company_vision"].ToString();
                textEditLogoPath.EditValue = CurrentRow["company_logo"].ToString();
                if (CurrentRow["company_logo"].ToString() != "")
                {
                    string file_path = AppDomain.CurrentDomain.BaseDirectory+"\\"+CurrentRow["company_logo"].ToString();
                    FileInfo file_info = new FileInfo(file_path);
                    if (file_info.Exists)
                    {
                        System.IO.FileStream fs = null;
                        fs = new System.IO.FileStream(file_info.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        //pictureEditLogo.Image = System.Drawing.Image.FromStream(fs);
                        pictureEditLogo.Image = System.Drawing.Image.FromFile(file_path);
                        fs.Close();
                        fs.Dispose();
                        textEditLogoPath.EditValue = CurrentRow["company_logo"].ToString();
                    }
                    else
                    {
                        pictureEditLogo.EditValue = "";
                        textEditLogoPath.EditValue = "";
                    }
                }
                else
                {
                    pictureEditLogo.EditValue = "";
                    textEditLogoPath.EditValue = "";
                }
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
                textEditOldLogoPath.EditValue = "";
                textEditSouceLogoPath.EditValue = "";
            }
            else
            {
                bttAdd.Enabled      = false;
                bttDelete.Enabled   = false;
                bttEdit.Enabled     = false;

                initDataDefault();
                clearData();
                button_event = "Add";
                enable(true);
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
            }
        }

        private String getExtPath()
        {
            DataTable generalDT = BusinessLogicBridge.DataStore.getGeneralConfig();

            string logo_ext_path = "";

            if (generalDT.Rows.Count > 0)
            {
                if (generalDT.Rows[0]["ext_path_backup"].ToString() != "")
                {
                    logo_ext_path = generalDT.Rows[0]["ext_path_backup"].ToString() + "\\Logo";

                    if (Directory.Exists(logo_ext_path) == false)
                    {
                        Directory.CreateDirectory(logo_ext_path);
                    }
                }
            }

            return logo_ext_path;
        }

        private String getPath()
        {
            string file_path = AppDomain.CurrentDomain.BaseDirectory;
            string logo_path = file_path + "\\Logo";

            if (Directory.Exists(logo_path) == false)
            {
                Directory.CreateDirectory(logo_path);
            }
            return logo_path;
        }
        private String createFileName(string full_path)
        {
            string file_extension = Path.GetExtension(full_path);
            string create_year = DateTime.Now.Year.ToString();
            string create_month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string create_day = DateTime.Now.Day.ToString().PadLeft(2, '0');
            string create_hour = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string create_min = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            string create_second = DateTime.Now.Second.ToString().PadLeft(2, '0');
            string create_millisecond = DateTime.Now.Millisecond.ToString();
            string file_name = "Logo_" + create_year + create_month + create_day + create_hour + create_min + create_second + "-" + create_millisecond + file_extension;
            return file_name;
        }
        private Boolean moveFile()
        {
            string file_path = textEditSouceLogoPath.EditValue.ToString();
            if (file_path != "")
            {
                FileInfo file_info = new FileInfo(file_path);
                file_info.CreationTime.Millisecond.ToString();
                if (file_info.Exists)
                {
                    string current_file_name = createFileName(file_path);
                    string current_file_paht = getPath() + "\\" + current_file_name;
                    string current_ext_file_paht = "";


                    if (getExtPath() != "") {
                        current_ext_file_paht = getExtPath() + "\\" + current_file_name;
                    }

                    try
                    {
                        System.IO.File.Copy(file_path, current_file_paht, true);
                        
                        if (current_ext_file_paht != "") {
                            System.IO.File.Copy(file_path, current_ext_file_paht, true);
                        }

                        textEditLogoPath.EditValue = @"\Logo\"+current_file_name;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        private Boolean removeFile()
        {
            string current_file_path = textEditLogoPath.EditValue.ToString();
            if (current_file_path != "")
            {
                FileInfo current_file_info = new FileInfo(current_file_path);
                if (current_file_info.Exists)
                {
                    try
                    {
                        current_file_info.Delete();
                    }
                    catch { }
                }
            }
            return true;
        }
        private Boolean removeOldFile()
        {
            string current_file_path = textEditOldLogoPath.EditValue.ToString();
            if (current_file_path != "")
            {
                FileInfo current_file_info = new FileInfo(current_file_path);
                if (current_file_info.Exists)
                {
                    try
                    {
                        current_file_info.Delete();
                    }
                    catch { }
                }
            }
            return true;
        }
        #endregion

        #region Change Value
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion

        #region Button Event
        private void bttBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                open.DefaultExt = ".jpeg";
                //open.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                open.Filter = "Image Files(*.jpg,*.gif,*.bmp,*.png,*.ico)|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.ico";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file_info = new FileInfo(open.FileName);
                    string file_paht = file_info.FullName;
                    textEditSouceLogoPath.EditValue = file_paht;
                    Double file_size = double.Parse(file_info.Length.ToString());

                    if (file_size > (50 * 1024))
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1004"), getLanguage("_softwarename"));
                        textEditSouceLogoPath.EditValue = "";
                        bttBrowse.Focus();
                        return;
                    }
                    System.IO.FileStream fs = null;
                    fs = new System.IO.FileStream(file_info.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    pictureEditLogo.Image = System.Drawing.Image.FromStream(fs);

                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void bttRemove_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4002"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                pictureEditLogo.EditValue = "";
                textEditLogoPath.EditValue = "";
                textEditSouceLogoPath.EditValue = "";
                int[] rowIndex = gridView1.GetSelectedRows();
                if (rowIndex.Length != 0)
                {
                    DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                    if (CurrentRow == null)
                    {
                        CurrentRow = gridView1.GetDataRow(0);
                    }
                    textEditOldLogoPath.EditValue = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+CurrentRow["company_logo"].ToString();
                  

                }
                else
                {
                    textEditOldLogoPath.EditValue = "";
                }

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");

            }
        }
        private void bttAdd_Click(object sender, EventArgs e)
        {
            clearData();
            enable(true);
            textEditCompanyName.Focus();
            gridControlCompany.Enabled = false;

            button_event = "Add";
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            
        }
        private void bttEdit_Click(object sender, EventArgs e)
        {
            enable(true);
            textEditCompanyName.Focus();

            button_event = "Edit";
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            gridControlCompany.Enabled = false;
        }
        private void bttDelete_Click(object sender, EventArgs e)
        {               
            int company_id = int.Parse(textEditId.EditValue.ToString());
            int status_usage = BusinessLogicBridge.DataStore.companyIdUsed(company_id);


            if(status_usage==1){
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
            }else{
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    
                    BusinessLogicBridge.DataStore.BasicInfoCompany_remove(company_id);
                    removeFile();
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");
                    int select_row = 0;
                    int[] rowIndex = gridView1.GetSelectedRows();
                    if (rowIndex[0] > 0)
                    {
                        select_row = rowIndex[0] - 1;
                    }
                    initCompany();
                    gridView1.FocusedRowHandle = select_row;
                    gridView1.SelectRow(select_row);
                    gridView1.UnselectRow(rowIndex[0]);
                }

                if (gridView1.RowCount == 0)
                {
                    bttAdd.Enabled = false;
                    bttDelete.Enabled = false;
                    bttEdit.Enabled = false;
                    initDataDefault();
                    clearData();
                    enable(true);
                    bttSave.Enabled = true;
                    bttCancel.Enabled = true;
                }
                else
                {
                    changeRow();
                    enable(false);

                    button_event = "";
                    bttSave.Enabled = false;
                    bttCancel.Enabled = false;
                    bttAdd.Enabled = true;
                    gridControlCompany.Enabled = true;
                }

            }
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            saveInfo();
        }

        public void saveInfo(){
            try
            {   
                // Check Backup Path
                DataTable db = BusinessLogicBridge.DataStore.getBackupConfig();
                if (db.Rows.Count == 0) {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1062"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                String message = "";
                DataTable _ValidateTable = validateData();
                if (_ValidateTable.Rows.Count > 0)
                {
                    for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                    {
                        message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
                if (moveFile() == false)
                {
                    message = getLanguage("_notice_can_not_upload");
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
                DataTable _CompanyTable = new DataTable();
                int company_id = int.Parse(textEditId.EditValue.ToString());
                String company_address = memoEditAddress.EditValue.ToString();
                String company_name = textEditCompanyName.EditValue.ToString();
                String company_telephone = textEditTelephone.EditValue.ToString();
                String company_fax = textEditFax.EditValue.ToString();
                String company_email = textEditEmail.EditValue.ToString();
                String company_website = textEditWebSite.EditValue.ToString();
                String company_id_card = textEditIdCard.EditValue.ToString();
                String company_corporate = textEditCorporate.EditValue.ToString();
                String company_owner_name = textEditOwnerName.EditValue.ToString();
                String company_vision = textEditVision.EditValue.ToString();
                String company_logo = textEditLogoPath.EditValue.ToString();

                _CompanyTable.Columns.Add("company_id", typeof(int));
                _CompanyTable.Columns.Add("company_address", typeof(String));
                _CompanyTable.Columns.Add("company_name", typeof(String));
                _CompanyTable.Columns.Add("company_telephone", typeof(String));
                _CompanyTable.Columns.Add("company_fax", typeof(String));
                _CompanyTable.Columns.Add("company_email", typeof(String));
                _CompanyTable.Columns.Add("company_website", typeof(String));
                _CompanyTable.Columns.Add("company_id_card", typeof(String));
                _CompanyTable.Columns.Add("company_tax_id", typeof(String));
                _CompanyTable.Columns.Add("company_owner_name", typeof(String));
                _CompanyTable.Columns.Add("company_vision", typeof(String));
                _CompanyTable.Columns.Add("company_logo", typeof(String));
                _CompanyTable.Rows.Add(company_id, company_address, company_name, company_telephone, company_fax, company_email, company_website, company_id_card, company_corporate, company_owner_name, company_vision, company_logo);

                int[] rowIndex;
                switch (button_event)
                {
                    case "Add":
                        int companyid = BusinessLogicBridge.DataStore.BasicInfoCompany_insert(_CompanyTable);


                       int doc_id = BusinessLogicBridge.DataStore.insertDocumentConfig("7","1","CTR",
                                                        1,
                                                        "INV",                 // อักษรนำหน้าใบแจ้งหนี้
                                                        1,  // เลขที่เอกสารเริ่มต้น
                                                        "ใบแจ้งหนี้ /Invoice",                 // ข้อความหัวใบแจ้งหนี้
                                                        "-",                 // ข้อความใต้ใบแจ้งหนี้
                                                        "-",                  // ข้อความใต้ลายเซ็นต์ 1
                                                        "-",                  // ข้อความใต้ลายเซ็นต์ 2
                                                        "REC",                  // อักษรนำหน้าใบเสร็จ
                                                        1,  // เลขที่เอกสารเริ่มต้น
                                                        "ใบเสร็จรับเงิน / Reciept", // ข้อความหัวใบเสร็จรับเงิน
                                                        "-",                  // ข้อความใต้ใบเสร็จรับเงิน
                                                        "-",                  // ข้อความใต้ลายเซ็นต์ 1
                                                        "-",                  // ข้อความใต้ลายเซ็นต์ 2
                                                        "3",
                                                        0,
                                                        companyid,
                                                        true,
                                                        true,
                                                        true,
                                                        1,
                                                        1
                                                    );

                        // Insert Document Log
                       BusinessLogicBridge.DataStore.insertDocumentPrefixLog("CTR", "CTR", "INV", "INV", "REC", "REC", doc_id);

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");

                        DXWindowsApplication2.MainForm.setToggleBar();

                        initCompany();
                        rowIndex = gridView1.GetSelectedRows();
                        int select_row = 0;
                        if (gridView1.RowCount > 0)
                        {
                            select_row = gridView1.RowCount - 1;
                        }
                        gridView1.FocusedRowHandle = select_row;
                        gridView1.SelectRow(select_row);
                        gridView1.UnselectRow(rowIndex[0]);
                        break;
                    case "Edit":
                        BusinessLogicBridge.DataStore.BasicInfoCompany_update(_CompanyTable);
                        
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        removeOldFile();
                        rowIndex = gridView1.GetSelectedRows();
                        initCompany();
                        gridView1.FocusedRowHandle = rowIndex[0];
                        gridView1.SelectRow(rowIndex[0]);
                        break;
                }
                changeRow();
                enable(false);

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttAdd.Enabled = true;
                gridControlCompany.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                enable(false);

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttAdd.Enabled = true;
                gridControlCompany.Enabled = true;
            }
        }
        #endregion
    }
}
