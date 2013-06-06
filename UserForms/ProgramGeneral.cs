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
using System.Security.AccessControl;

namespace DXWindowsApplication2.UserForms
{
    public partial class ProgramGeneral : uBase
    {
        private string FolderName;

        private string InvoiceFolder;
        private string RecieptFolder;
        private string TempReciept;
        private string ContractFolder;
        private string TempFolder;
        private string ImagesFolder;
        private string button_case = "";
        private string DefaultPath = "";
        private bool flagpath = true;

        public ProgramGeneral()
        {
            InitializeComponent();
            this.Dock   = DockStyle.Fill;
            
            // Define Parameter
                FolderName = "e-SmartBilling";

                InvoiceFolder = "Invoice";
                RecieptFolder = "Reciept";
                TempReciept = "TempReciept";
                ContractFolder = "Contract";
                TempFolder = "Temp";
                ImagesFolder = "Images";
                

            this.Load += new EventHandler(ProgramGeneral_Load);
            SaveClick += new EventHandler(ProgramGeneral_SaveClick);
            textEditDuePayment.EditValueChanged += new EventHandler(textEditDuePayment_EditValueChanged);
            textEditBillingDay.EditValueChanged += new EventHandler(textEditBillingDay_EditValueChanged);
            
        }

        void textEditBillingDay_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditBillingDay.EditValue.To<int>() > 31)
            {
                textEditBillingDay.EditValue = 31;
            }
            if (textEditBillingDay.EditValue.To<int>() < 1)
            {
                textEditBillingDay.EditValue = 1;
            }
        }

        void textEditDuePayment_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditDuePayment.EditValue.To<int>() > 31)
            {
                textEditDuePayment.EditValue = 31;
            }
            if (textEditDuePayment.EditValue.To<int>() < 1)
            {
                textEditDuePayment.EditValue = 1;
            }
        }

        void ProgramGeneral_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender,e);
        }

        void ProgramGeneral_Load(object sender, EventArgs e)
        {   
            setLangThis();
            getLangConfig();
            initLoadData();
        }

        public void setLangThis()
        {   
            // Group Control
            this.groupControlProgramSetting.Text = getLanguage("_menu_program_setting_system_setting");
            this.groupControlProgramLanguage.Text = getLanguage("_language");
            this.groupControlProgramYearFormat.Text = getLanguage("_year_format");
            this.groupControlProgramCurrency.Text = getLanguage("_currency");
            this.groupControlProgramDueDate.Text = getLanguage("_due_date");
            this.groupControlProgramDocument.Text = getLanguage("_document");
            this.lbGeneralBottomText.Text = getLanguage("_general_setting_bottom_text");

            // button
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");

            // Data Item 

            // English  // Thai
            radioGroupLang.Properties.Items[0].Description = getLanguage("_thai");
            radioGroupLang.Properties.Items[1].Description = getLanguage("_english");    

            // Buddhist(B.E.)  // Christian(C.E.)
            radioGroupYear.Properties.Items[0].Description = getLanguage("_buddhist");
            radioGroupYear.Properties.Items[1].Description = getLanguage("_christian");

            // Billing Date
            labelControlDueDate.Text = getLanguageWithColon("_billing_date");
            
            // Due Payment Day
            labelControlBillingDay.Text = getLanguageWithColon("_due_payment_day");

            // If no Date in month let previous date1
            labelControlNodate1.Text = getLanguage("_if_no_date1");         

            // If no Date in month let previous date2
            labelControlNodate2.Text = getLanguage("_if_no_date2");

            // Backup Path
            labelBackupPath.Text = getLanguageWithColon("_backup_path");

            // Browse
            simpleButtonSelectPath.Text = getLanguage("_browse");



        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension.ToString() != ".dll" && file.Extension.ToString() != ".cfg" && file.Extension.ToString() != ".config" && file.Extension.ToString() != ".exe" && file.Extension.ToString() != ".InstallState")
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {   
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        void getLangConfig()
        {
            DataTable configData = BusinessLogicBridge.DataStore.getLangConfig();

            if (configData.Rows.Count > 0)
            {
                string lang = configData.Rows[0]["language_name"].ToString();

                if (lang == "th")
                {
                    radioGroupLang.SelectedIndex = 0;
                }
                else
                {
                    radioGroupLang.SelectedIndex = 1;
                }
            }
            else
            {
                BusinessLogicBridge.DataStore.updateLangConfig("th");
                radioGroupLang.SelectedIndex = 0;
            }

            configData.Dispose();
        }

        void initLoadData() {

            DataTable configData =  BusinessLogicBridge.DataStore.getGeneralConfig();

            DefaultPath = AppDomain.CurrentDomain.BaseDirectory;//MainForm.CombinePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal), configData.Rows[0]["path_all_document"].ToString());

            int year_format = 1;
            
            if (configData.Rows.Count > 0)
            {
                if (configData.Rows[0]["ext_path_backup"].ToString() != "")
                {
                    textEditPath.EditValue = configData.Rows[0]["ext_path_backup"].ToString();
                }
                else {
                    textEditPath.EditValue = DefaultPath;
                }
                
                year_format = configData.Rows[0]["year_format"].To<int>();

                lookUpEditCurrency.EditValue = configData.Rows[0]["currency"].To<int>();

                textEditDuePayment.EditValue = Convert.ToInt16(configData.Rows[0]["due_date"]);
                textEditBillingDay.EditValue = Convert.ToInt16(configData.Rows[0]["payment_date"]);

            }
            else {

                lookUpEditCurrency.EditValue = 1;
                radioGroupLang.SelectedIndex = 0;
                radioGroupYear.SelectedIndex = 0;
            }
          
            InitYearFormat(year_format);
            InitCurrency();
            //initPathOfFile();

            if (Directory.Exists(textEditPath.EditValue.ToString()) == false)
            {

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1062"), getLanguage("_softwarename"));
                return;
            }
        }

        void InitYearFormat(int format)
        {

            if (format == 1)
            {
                radioGroupYear.SelectedIndex = 0;
            }
            else
            {
                radioGroupYear.SelectedIndex = 1;
            }

        }

        void InitCurrency()
        {
            DataTable currency = new DataTable();

            currency.Columns.Add("currency_id", typeof(int));
            currency.Columns.Add("currency_name", typeof(string));


            if (current_lang == "th")
            {

                currency.Rows.Add(1, "บาท");
                currency.Rows.Add(2, "ดอลลาร์($)");
            }
            else {
                currency.Rows.Add(1, "Baht");
                currency.Rows.Add(2, "Dollar($)");
            }

            lookUpEditCurrency.Properties.DataSource = currency;
            lookUpEditCurrency.Properties.DisplayMember = "currency_name";
            lookUpEditCurrency.Properties.ValueMember = "currency_id";
            lookUpEditCurrency.Properties.NullText = "[โปรดระบุสกุลเงิน]";

        }
        
        void initPathOfFile() {

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = filePath + @"\" + FolderName;
            textEditPath.EditValue = filePath;

        }

        void saveInfo() {
            //Validate Default
            DataTable _ValidateTable = validateData();
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

            try
            {

                if (button_case == "updatepath")
                {
                    if (DefaultPath != textEditPath.Text)
                    {
                        DirectoryCopy(DefaultPath, @"" + textEditPath.Text + "", true);
                    }
                    else {
                        flagpath = true;
                    }
                    
                }
                else
                {

                    if (Directory.Exists(textEditPath.Text) == false)
                    {

                        Directory.CreateDirectory(textEditPath.Text);

                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, InvoiceFolder));
                        // Create Reciept folder
                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, RecieptFolder));
                        // Create Temp Reciept folder
                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, TempReciept));
                        // Create Contract folder
                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, ContractFolder));
                        // Create Temp folder
                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, TempFolder));
                        // Create images folder
                        Directory.CreateDirectory(Path.Combine(textEditPath.Text, ImagesFolder));

                    }
                    else
                    {
                        // Yes It has
                        // Create Invoice folder

                        if (Directory.Exists(Path.Combine(textEditPath.Text, InvoiceFolder)) == false)
                        {

                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, InvoiceFolder));
                            // Create Reciept folder
                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, RecieptFolder));
                            // Create Temp Reciept folder
                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, TempReciept));
                            // Create Contract folder
                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, ContractFolder));
                            // Create Temp folder
                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, TempFolder));
                            // Create images folder
                            Directory.CreateDirectory(Path.Combine(textEditPath.Text, ImagesFolder));
                        }
                    }
                }

                //if (flagpath == true)
                //{
                //    BusinessLogicBridge.DataStore.updateGeneralConfig(FolderName, Convert.ToInt16(lookUpEditCurrency.EditValue), Convert.ToInt16(radioGroupYear.EditValue), Convert.ToInt16(textEditDuePayment.EditValue), Convert.ToInt16(textEditBillingDay.EditValue), "");
                //}
                //else
                //{
                    BusinessLogicBridge.DataStore.updateGeneralConfig(FolderName, Convert.ToInt16(lookUpEditCurrency.EditValue), Convert.ToInt16(radioGroupYear.EditValue), Convert.ToInt16(textEditDuePayment.EditValue), Convert.ToInt16(textEditBillingDay.EditValue), textEditPath.Text);
                //}

                
                if (radioGroupLang.SelectedIndex == 0)
                {
                    BusinessLogicBridge.DataStore.updateLangConfig("th");
                    MainForm.current_lang = "th";
                }
                else
                {
                    BusinessLogicBridge.DataStore.updateLangConfig("en");
                    MainForm.current_lang = "en";
                }

                initLoadData();

                if (Convert.ToInt32(DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue) == 0)
                {
                    DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 1;
                }
                else
                {
                    DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 0;
                }

                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "System Setting [Update General Config]");

                xtraScrollableControl1.Enabled = false;
                bttEdit.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message.ToString());
            }
        }

        private DataTable validateData()
        {
            string max_value = getLanguage("_max_value");

            string star_notice = getLanguage("_notice_star");

            string over_date = getLanguage("_over_date");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));


            if ((textEditDuePayment.EditValue == null) || (Convert.ToInt16(textEditDuePayment.EditValue) == 0))
            {
                label = labelControlDueDate.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditDuePayment.Focus();
                    focus = true;
                }
            }
            else if (Convert.ToInt16(textEditDuePayment.EditValue) > 31)
            {
                label = labelControlDueDate.Text;
                message = over_date;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditDuePayment.Focus();
                    focus = true;
                }
            }

            if ((textEditBillingDay.EditValue == null) || (Convert.ToInt16(textEditBillingDay.EditValue) == 0))
            {
                label = labelControlBillingDay.Text;
                message = over_date;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditBillingDay.Focus();
                    focus = true;
                }
            }
            else if (Convert.ToInt16(textEditBillingDay.EditValue) > 31)
            {
                label = labelControlBillingDay.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditBillingDay.Focus();
                    focus = true;
                }
            }

            return _ValidateTable;
        }
        
        private void simpleButtonSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDialog = new FolderBrowserDialog();


            fDialog.Description = "Select Directory";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                DriveInfo x = new DriveInfo(fDialog.SelectedPath);
                
                if(x.DriveType==DriveType.CDRom){

                    utilClass.showPopupMessegeBox(this, "Cannot select CD/DVD drive.", getLanguage("_softwarename"));
                    return;

                }
                else if (DefaultPath != fDialog.SelectedPath)
                {
                    flagpath = false;
                    textEditPath.EditValue = fDialog.SelectedPath;
                    textEditPath.EditValue = textEditPath.EditValue.ToString().Replace("\\\\", "\\");
                    button_case = "updatepath";
                }
                else {
                    textEditPath.EditValue = fDialog.SelectedPath;
                    textEditPath.EditValue = textEditPath.EditValue.ToString().Replace("\\\\", "\\");
                    button_case = "updatepath";
                }
            }
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            xtraScrollableControl1.Enabled = true;
            bttSave.Enabled     = true;
            bttCancel.Enabled   = true;
            bttEdit.Enabled     = false;
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
             {
                 xtraScrollableControl1.Enabled = false;
                 bttEdit.Enabled = true;
                 bttSave.Enabled = false;
                 bttCancel.Enabled = false;
                 getLangConfig();
                 initLoadData();
             }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            bool monetary_change = false;

            // Check Monetary is changed
            DataTable configData = BusinessLogicBridge.DataStore.getLangConfig();
            DataTable configGen = BusinessLogicBridge.DataStore.getGeneralConfig();

            if (configData.Rows.Count > 0)
            {
                string lang = configData.Rows[0]["language_name"].ToString();
                int year_format = configGen.Rows[0]["year_format"].To<int>();
                int currency = configGen.Rows[0]["currency"].To<int>();
                int YearFormatButton = 0;

                if (radioGroupYear.SelectedIndex == 0)
                {
                    YearFormatButton = 1;
                }
                else {
                    YearFormatButton = 2;
                }

                if (lang == "th")
                {
                    if ((radioGroupLang.SelectedIndex != 0) || (YearFormatButton != year_format) || (lookUpEditCurrency.EditValue.To<int>() != currency))
                    {
                        monetary_change = true;
                    }
                }
                else
                {
                    if ((radioGroupLang.SelectedIndex != 1) || (YearFormatButton != year_format) || (lookUpEditCurrency.EditValue.To<int>() != currency))
                    {
                        monetary_change = true;
                    }
                }
            }

            if (monetary_change == true)
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4009"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    saveInfo();
                    MainForm.restartflag = true;
                    // restart software
                    Application.Restart();
                }
                else
                {
                    saveInfo();

                    DataTable languageconfig = BusinessLogicBridge.DataStore.getLangConfig();
                    current_lang = languageconfig.Rows[0][1].ToString();
                    languages.loadLanguage(current_lang);
                    
                    if (MainForm.EventSetAllLang.EditValue.ToString() == "0")
                    {
                        MainForm.EventSetAllLang.EditValue = 1;
                    }
                    else {
                        MainForm.EventSetAllLang.EditValue = 0;
                    }

                    setLangThis();

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                }
            }
            else {
                saveInfo();
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");
            }

            initLoadData();

        }

    }
}
