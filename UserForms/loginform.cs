using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class loginform : uFormBase
    {
        public static string username = "";
        public static string password = "";
        public static bool flagLoginTrue = false;
        public static int groupid;
        public Button cc = new Button();

        public loginform()
        {                       
            InitializeComponent();

            this.Load += new EventHandler(loginform_Load);

            textEditUsername.Leave += new EventHandler(textEditUsername_Leave);

        }

        void textEditUsername_Leave(object sender, EventArgs e)
        {
            if (textEditUsername.EditValue != null)
            {
                DataTable AccountInfo = BusinessLogicBridge.DataStore.getGroupByUsername(textEditUsername.EditValue.ToString());

                if (AccountInfo.Rows.Count>0)
                lookUpEditGroupAccount.EditValue = AccountInfo.Rows[0]["group_id"].To<int>();
            }
        }

        void loginform_Load(object sender, EventArgs e)
        {
            DataTable group_account = BusinessLogicBridge.DataStore.get_group_account();
            lookUpEditGroupAccount.Properties.DataSource = group_account;
            lookUpEditGroupAccount.Properties.DisplayMember = "group_name";
            lookUpEditGroupAccount.Properties.ValueMember = "group_id";
            lookUpEditGroupAccount.Properties.NullText = "[เลือกกลุ่มผู้ใช้]";

            DataTable RememInfo = BusinessLogicBridge.DataStore.selectRemember();

            if (RememInfo.Rows.Count > 0) {

                checkEditRemember.Checked = true;

                textEditUsername.EditValue = RememInfo.Rows[0]["remember_user"].ToString();
                textEditPassword.EditValue = RememInfo.Rows[0]["remember_password"].ToString();
                lookUpEditGroupAccount.EditValue = RememInfo.Rows[0]["remember_groupid"].ToString();
            }

            this.Text = getLanguage("_softwarename");

            setLangThis();

            this.FormClosing += new FormClosingEventHandler(loginform_FormClosing);

            // Check File Remember existing

            this.Activate();
        }

        public void setLangThis()
        {
            this.groupBoxLogin.Text = getLanguage("_loginbox_login");
            this.labelControlUsername.Text = getLanguageWithColon("_loginbox_username");
            this.labelControlPassword.Text = getLanguageWithColon("_loginbox_password");
            this.labelControlUserGroup.Text = getLanguageWithColon("_loginbox_group");

            this.bttAdd.Text = getLanguage("_ok");
            this.bttCancel.Text = getLanguage("_cancel");
        }

        void loginform_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (flagLoginTrue == false)
            {
                DialogResult dr = DXWindowsApplication2.UserForms.utilClass.showPopupConfirmBox(this, getLanguage("_msg_4016"), this.Text);

                if (dr == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
        }

        public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);

            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            if ((textEditUsername.EditValue == null) || (textEditUsername.EditValue.ToString().Trim() == ""))
            {
                labelErrorMsg.Text = getLanguage("_msg_1033");
            }
            else if ((textEditPassword.EditValue == null) || (textEditPassword.EditValue.ToString().Trim() == ""))
            {
                labelErrorMsg.Text = getLanguage("_msg_1033");
            }
            else if ((lookUpEditGroupAccount.EditValue == null) || (Convert.ToInt32(lookUpEditGroupAccount.EditValue) == 0))
            {
                labelErrorMsg.Text = "โปรดเลือกกลุ่มผู้ใช้งาน";
            }
            else
            {
                int counter = 0;
                string loginReturn = DXWindowsApplication2.MainForm.checkLogin(textEditUsername.EditValue.ToString(), textEditPassword.EditValue.ToString(), Convert.ToInt32(lookUpEditGroupAccount.EditValue));

                if (loginReturn == "1")
                {   
                    MainForm.username = textEditUsername.EditValue.ToString();
                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Login");

                    if (checkEditRemember.Checked == true)
                    {
                        BusinessLogicBridge.DataStore.updateRemember(MainForm.username, textEditPassword.EditValue.ToString(), lookUpEditGroupAccount.EditValue.To<int>());
                    }
                    else {
                        
                        BusinessLogicBridge.DataStore.deleteRemember(MainForm.username);
                    }

                    flagLoginTrue = true;

                    this.DialogResult = DialogResult.OK;
                }
                else
                {   

                    labelErrorMsg.Text = getLanguage("_msg_1034");

                    if (counter > 3) {
                        this.DialogResult = DialogResult.Cancel;
                    }

                    counter++;
                }

            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = utilClass.showPopupConfirmBox(this, getLanguage("_msg_4016"), this.Text);

            if (dr == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else {
                return;
            }

            
        }        
    }
}
