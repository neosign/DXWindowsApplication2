using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class ProgramDatabase : DevExpress.XtraEditors.XtraUserControl
    {
        public ProgramDatabase()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            setDisable();
            getDatabaseConfig();
        }

        #region Method

        public void getDatabaseConfig()
        {
            try
            {
                DataTable configData = BusinessLogicBridge.DataStore.getDatabaseConfig();

                if (configData.Rows.Count > 0)
                {
                    string server_name = configData.Rows[0]["server_name"].ToString();
                    string database_username = configData.Rows[0]["database_username"].ToString();
                    string database_password = configData.Rows[0]["database_password"].ToString();

                    textEditServerName.EditValue = server_name;
                    textEditDatabaseUsername.EditValue = database_username;
                    textEditDatabasePassword.EditValue = database_password;
                }
                configData.Dispose();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
         }

        void setDisable()
        {
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            panelEnable.Enabled = false;
            bttEdit.Enabled = true;
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
            string pattern = "";
                   pattern = @"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        public bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
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

        #region Event

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEnable();
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            List<string> listError = new List<string>();

            string TextOnly = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
            string IPURLOnly = " ต้องเป็น url หรือ IP เท่านั้น";
            string EmptySting = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";
            // Validation Control
            string msgError = "";

                # region validate Server Name

                if (isEmpty(textEditServerName.Text) == true)
                {
                    if (IsValidIP(textEditServerName.Text) == false)
                    {
                        if (textEditServerName.EditValue.ToString() != "localhost")
                        {
                            if (isValidUrl(textEditServerName.Text) == false)
                            {
                                listError.Add(labelServerName.Text + IPURLOnly.ToString());
                            }
                        }

                    }
                    
                }
                else
                {
                    listError.Add(labelServerName.Text + EmptySting.ToString());
                }
                
                #endregion

                # region validate User Name

                if (isEmpty(textEditDatabaseUsername.Text) == true)
                {
                    if (IsAlphaNumeric(textEditDatabaseUsername.Text) == false)
                    {
                        listError.Add(labelUserName.Text + TextOnly.ToString());
                    }                   
                }
                else
                {
                    listError.Add(labelUserName.Text + EmptySting.ToString());
                }

                #endregion

                # region validate Password

                if (isEmpty(textEditDatabasePassword.Text) == true)
                {
                    if (IsAlphaNumeric(textEditDatabasePassword.Text) == false)
                    {
                        listError.Add(labelPassword.Text + TextOnly.ToString());
                    }
                }
                else
                {
                    listError.Add(labelPassword.Text + EmptySting.ToString());
                }

                #endregion

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

                    BusinessLogicBridge.DataStore.updateDatabaseConfig(textEditServerName.Text, textEditDatabaseUsername.Text, textEditDatabasePassword.Text);
                    getDatabaseConfig();
                    XtraMessageBox.Show("บันทึกข้อมูลเรียบร้อย");
                    setDisable();

                    if (Convert.ToInt32(DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue) == 0)
                    {
                        DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 1;
                    }
                    else {
                        DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 0;
                    }
                    
                }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            DialogResult drx = XtraMessageBox.Show("ข้อมูลมีการแก้ไข คุณต้องการบันทึกหรือไม่ ?", "", MessageBoxButtons.OKCancel);
            if (drx == DialogResult.OK)
            {
                try
                {
                    List<string> listError = new List<string>();

                    string TextOnly = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
                    string IPURLOnly = " ต้องเป็น url หรือ IP เท่านั้น";
                    string EmptySting = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";
                    // Validation Control
                    string msgError = "";

                    # region validate Server Name

                    if (isEmpty(textEditServerName.Text) == true)
                    {
                        if (IsValidIP(textEditServerName.Text) == false)
                        {
                            listError.Add(labelServerName.Text + IPURLOnly.ToString());

                        }
                        else if (isValidUrl(labelServerName.Text) == false)
                        {

                            listError.Add(labelServerName.Text + IPURLOnly.ToString());
                        }
                    }
                    else
                    {
                        listError.Add(labelServerName.Text + EmptySting.ToString());
                    }

                    #endregion

                    # region validate User Name

                    if (isEmpty(textEditDatabaseUsername.Text) == true)
                    {
                        if (IsAlphaNumeric(textEditDatabaseUsername.Text) == false)
                        {
                            listError.Add(labelUserName.Text + TextOnly.ToString());
                        }
                    }
                    else
                    {
                        listError.Add(labelUserName.Text + EmptySting.ToString());
                    }

                    #endregion

                    # region validate Password

                    if (isEmpty(textEditDatabasePassword.Text) == true)
                    {
                        if (IsAlphaNumeric(textEditDatabasePassword.Text) == false)
                        {
                            listError.Add(labelPassword.Text + TextOnly.ToString());
                        }
                    }
                    else
                    {
                        listError.Add(labelPassword.Text + EmptySting.ToString());
                    }

                    #endregion

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

                        BusinessLogicBridge.DataStore.updateDatabaseConfig(textEditServerName.Text, textEditDatabaseUsername.Text, textEditDatabasePassword.Text);
                        getDatabaseConfig();
                        setDisable();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            else {
                setDisable();
            }
        }

        #endregion
    }
}
