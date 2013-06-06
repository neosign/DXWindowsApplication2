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
    public partial class BackupOnline : DevExpress.XtraEditors.XtraUserControl
    {
        # region Mrthod

        public BackupOnline()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            getBackupOnline();
        }

        void getBackupOnline() {

            DataTable OnlineDetail = BusinessLogicBridge.DataStore.getOnlineBackup();

            if (OnlineDetail.Rows.Count > 0)
            {
                textEditServer.EditValue = OnlineDetail.Rows[0]["ftp_server_name"].ToString();
                textEditUsername.EditValue = OnlineDetail.Rows[0]["ftp_username"].ToString();
                textEditPassword.EditValue = OnlineDetail.Rows[0]["ftp_password"].ToString();
            }

            setDisable();
        }

        void setDisable()
        {
            bttSave.Enabled     = false;
            bttCancel.Enabled   = false;
            bttEdit.Enabled     = true;
            panelEnable.Enabled = false;
        }

        void setUpdateAction()
        {
            bttSave.Enabled     = true;
            bttCancel.Enabled   = true;
            bttEdit.Enabled     = false;
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

        # region button
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            getBackupOnline();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            setUpdateAction();
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            List<string> listError = new List<string>();

            string TextOnly     = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
            string EmptySting   = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";

            #region validate Server Name

            if (isEmpty(textEditServer.Text) == true)
            {
                if (IsAlphaNumeric(textEditServer.Text) == false)
                {
                    listError.Add(labelControlServerName.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlServerName.Text + EmptySting.ToString());
            }
            #endregion

            #region validate Username

            if (isEmpty(textEditUsername.Text) == true)
            {
                if (IsAlphaNumeric(textEditUsername.Text) == false)
                {
                    listError.Add(labelControlUsername.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlUsername.Text + EmptySting.ToString());
            }
            #endregion

            #region validate Password

            if (isEmpty(textEditPassword.Text) == true)
            {
                if (IsAlphaNumeric(textEditPassword.Text) == false)
                {
                    listError.Add(labelControlPassword.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlPassword.Text + EmptySting.ToString());
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
                DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไขข้อมูล", "", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        BusinessLogicBridge.DataStore.UpdateOnlineBackup(textEditServer.Text, textEditUsername.Text, textEditPassword.Text);
                    }
                    catch
                    {

                    }
                    setDisable();
                }
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            // Check Update
            DialogResult drx = XtraMessageBox.Show("ข้อมูลมีการแก้ไข คุณต้องการบันทึกหรือไม่ ?", "", MessageBoxButtons.OKCancel);
            if (drx == DialogResult.OK)
            { 
                List<string> listError = new List<string>();

                string TextOnly     = " ต้องเป็นตัวอักษรและตัวเลขเท่านั้น";
                string EmptySting   = " กรุณากรอกข้อมูลให้ครบในช่องที่มีเครื่องหมาย *";

                #region validate Server Name

            if (isEmpty(textEditServer.Text) == true)
            {
                if (IsAlphaNumeric(textEditServer.Text) == false)
                {
                    listError.Add(labelControlServerName.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlServerName.Text + EmptySting.ToString());
            }
            #endregion

                #region validate Username

            if (isEmpty(textEditUsername.Text) == true)
            {
                if (IsAlphaNumeric(textEditUsername.Text) == false)
                {
                    listError.Add(labelControlUsername.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlUsername.Text + EmptySting.ToString());
            }
            #endregion

                #region validate Password

            if (isEmpty(textEditPassword.Text) == true)
            {
                if (IsAlphaNumeric(textEditPassword.Text) == false)
                {
                    listError.Add(labelControlPassword.Text + TextOnly.ToString());
                }
            }
            else
            {
                listError.Add(labelControlPassword.Text + EmptySting.ToString());
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
                    try
                    {
                        BusinessLogicBridge.DataStore.UpdateOnlineBackup(textEditServer.Text, textEditUsername.Text, textEditPassword.Text);
                    }
                    catch
                    {

                    }

                    setDisable();
                }
            }
            else
            {
                getBackupOnline();
                setDisable();
            }
            
        }

        # endregion 
    }
}
