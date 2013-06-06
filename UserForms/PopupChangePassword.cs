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
    public partial class PopupChangePassword : uFormBase
    {
        public PopupChangePassword()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupChangePassword_Load);
            //
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
        }

        void PopupChangePassword_Load(object sender, EventArgs e)
        {
            setLanguage();
        }

        void setLanguage()
        {
            this.Text = getLanguage("_change_password");
            groupControlText.Text = getLanguage("_change_password");
            //
            labelControlOldPassword.Text = getLanguageWithColon("_password_old");
            labelControlNewPassword.Text = getLanguageWithColon("_password_new");
            labelControlConfirmPassword.Text = getLanguageWithColon("_password_confirm");
            labelControlRequired.Text = getLanguageWithColon("_required");
            //
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            // validation 
            DataTable _ValidateTable = validateData();
            if (_ValidateTable.Rows.Count > 0)
            {
                String message = "";
                for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                {
                    message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                }
                utilClass.showPopupMessegeBox(this, message, getLanguage("_sofwarename"));
                return;
            }
            //
            int uID = int.Parse(UserID);
            //
            BusinessLogicBridge.DataStore.UpdateUserAccountPassword(textEditNewPassword.Text, uID);

            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3017"), getLanguage("_sofwarename"), "info");
            // Success
            CurrentPassword = textEditNewPassword.Text;
            //
            this.DialogResult = DialogResult.OK;
        }

        private DataTable validateData()
        {
            String label = "";
            String message = "";
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (textEditOldPassword.EditValue.ToString() == "")
            {
                label = labelControlOldPassword.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditNewPassword.EditValue.ToString() == "")
            {
                label = labelControlNewPassword.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditConfirmPassword.EditValue.ToString() == "")
            {
                label = labelControlConfirmPassword.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            //
            if (textEditOldPassword.EditValue.ToString() != CurrentPassword)
            {
                label = textEditOldPassword.Text;
                message = getLanguage("_msg_1021");
                _ValidateTable.Rows.Add(label, message);
            }
            //
            if (textEditNewPassword.EditValue.ToString() != textEditConfirmPassword.EditValue.ToString())
            {
                label = labelControlConfirmPassword.Text;
                message = getLanguage("_msg_1020");
                _ValidateTable.Rows.Add(label, message);
            }
            //
            return _ValidateTable;
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_sofwarename"))==DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
