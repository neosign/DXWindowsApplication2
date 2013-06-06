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
    public partial class PopupWarningSetting : uFormBase
    {
        public PopupWarningSetting()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupWarningSetting_Load);
            //
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            //
            checkEditAll.CheckedChanged += new EventHandler(checkEditAll_CheckedChanged);
            checkEdit_overdue_checkin.CheckedChanged += new EventHandler(check_CheckedChanged);
            checkEdit_overdue_checkout.CheckedChanged += new EventHandler(check_CheckedChanged);
            checkEdit_endofbook.CheckedChanged += new EventHandler(check_CheckedChanged);
            checkEdit_billingdate.CheckedChanged += new EventHandler(check_CheckedChanged);
            checkEdit_overdue_payment.CheckedChanged += new EventHandler(check_CheckedChanged);
            checkEdit_vacantroom.CheckedChanged += new EventHandler(checkEdit_vacantroom_CheckedChanged);
            checkEdit_database.CheckedChanged += new EventHandler(checkEdit_database_CheckedChanged);
            checkEdit_email.CheckedChanged += new EventHandler(checkEdit_email_CheckedChanged);
            //
            this.FormClosing += new FormClosingEventHandler(PopupWarningSetting_FormClosing);
        }

        void PopupWarningSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK || this.DialogResult == DialogResult.Abort)
            {
                e.Cancel = false;
                return;
            }
            //
            if (DialogResult.Yes == DXWindowsApplication2.UserForms.utilClass.showPopupConfirmBox(this, getLanguage("_msg_4004"), getLanguage("_softwarename")))
                bttSave_Click(null, null);
        }

        void check_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditAll.IsEditorActive)
                return;
            //
            bool all = true;
            CheckEdit chk;
            //
            foreach (Control c in this.panelControlSetting.Controls)
            {

                if (c.Name.StartsWith("check") && c.Name != "checkEditAll")
                {
                    chk = (c as CheckEdit);
                    if (chk.Checked == false)
                        all = false;
                }
            }
            //
            checkEditAll.Checked = all;
        }

        void checkEdit_email_CheckedChanged(object sender, EventArgs e)
        {
            textEdit_email.Enabled = checkEdit_email.Checked;
        }

        void checkEdit_database_CheckedChanged(object sender, EventArgs e)
        {
            textEdit_database.Enabled = checkEdit_database.Checked;
            //
            check_CheckedChanged(sender, e);
        }

        void checkEdit_vacantroom_CheckedChanged(object sender, EventArgs e)
        {
            textEdit_vacantroom.Enabled = checkEdit_vacantroom.Checked;
            //
            check_CheckedChanged(sender, e);
        }

        void checkEditAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEditAll.IsEditorActive)
                return;
            //
            CheckEdit _c = (sender as CheckEdit);
            //
            CheckState ct = _c.CheckState;
            //
            foreach (Control c in this.panelControlSetting.Controls)
            {
                if (c.Name.StartsWith("check") && c.Name != "checkEditAll")
                    (c as CheckEdit).Checked = checkEditAll.Checked;
            }
            //
            //panelControlSetting.Enabled = checkEditAll.Checked;
        }

        void PopupWarningSetting_Load(object sender, EventArgs e)
        {
            setLanguage();
            //
            initSetting();
        }

        void initSetting()
        {
            DataTable dtSetting = BusinessLogicBridge.DataStore.getWarningSetting();
            //
            bool all = true;
            //
            foreach (DataRow drSetting in dtSetting.Rows)
            {
                int enable = int.Parse(drSetting["warning_enable"].ToString());
                string key = drSetting["warning_key"].ToString();
                string val = drSetting["warning_value"].ToString();
                //
                if (key != "email" && enable == 0)
                    all = false;
                //
                CheckEdit chk = Controls.Find("checkEdit_" + key, true)[0] as CheckEdit;
                //
                if (chk == null) continue;
                //
                chk.Checked = enable == 1;
                //
                if (key == "vacantroom")
                {
                    textEdit_vacantroom.EditValue = val;
                    textEdit_vacantroom.Enabled = enable == 1;
                }
                else if (key == "database")
                {
                    textEdit_database.EditValue = val;
                    textEdit_database.Enabled = enable == 1;
                }
                else if (key == "email")
                {
                    textEdit_email.EditValue = val;
                    textEdit_email.Enabled = enable == 1;
                }
            }
            //
            checkEditAll.Checked = all;
            //
        }

        void setLanguage()
        {
            this.Text = getLanguage("_warning_setting");
            groupControlWarning.Text = getLanguage("_warning_list");
            //
            checkEditAll.Text = getLanguage("_selectall");
            checkEdit_overdue_checkin.Text = getLanguage("_warning_menu_overdue_checkin");
            checkEdit_overdue_checkout.Text = getLanguage("_warning_menu_overdue_checkout");
            checkEdit_endofbook.Text = getLanguage("_warning_menu_endofbook");
            checkEdit_billingdate.Text = getLanguage("_warning_menu_billingdate");
            checkEdit_overdue_payment.Text = getLanguage("_warning_menu_overdue_payment");
            checkEdit_vacantroom.Text = getLanguage("_warning_menu_vacantroom");
            checkEdit_database.Text = getLanguage("_warning_menu_database");
            checkEdit_email.Text = getLanguage("_warning_send_email");
            labelControlSampleEmail.Text = getLanguage("_warning_sample_email");
            labelControlEmail.Text = getLanguage("_email");
            //
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            // validation 
            textEdit_email.Text = textEdit_email.Text.ToLower();
            //
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
            int i_overdue_checkin = checkEdit_overdue_checkin.Checked ? 1 : 0;
            int i_overdue_checkout = checkEdit_overdue_checkout.Checked ? 1 : 0;
            int i_endofbook = checkEdit_endofbook.Checked ? 1 : 0;
            int i_billingdate = checkEdit_billingdate.Checked ? 1 : 0;
            int i_overdue_payment = checkEdit_overdue_payment.Checked ? 1 : 0;
            int i_vacantroom = checkEdit_vacantroom.Checked ? 1 : 0;
            int i_database = checkEdit_database.Checked ? 1 : 0;
            int i_email = checkEdit_email.Checked ? 1 : 0;
            //
            string s_vacantroom = textEdit_vacantroom.Text.Trim();
            string s_database = textEdit_database.Text.Trim();
            string s_email = textEdit_email.Text.Trim();
            //
            BusinessLogicBridge.DataStore.updateWarningSetting(i_overdue_checkin, i_overdue_checkout, i_endofbook, i_billingdate, i_overdue_payment, i_vacantroom, i_database, i_email, s_vacantroom, s_database, s_email);
            
            // Success
            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_sofwarename"), "info");
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
            //
            if (checkEdit_vacantroom.Checked && textEdit_vacantroom.Text == "")
            {
                label = checkEdit_vacantroom.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (checkEdit_database.Checked && textEdit_database.Text == "")
            {
                label = checkEdit_database.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }

            if (checkEdit_email.Checked && textEdit_email.EditValue.ToString() == "")
            {
                label = labelControlEmail.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }

            if (checkEdit_email.Checked && textEdit_email.EditValue.ToString() != "")
            {
                string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";
                var em1 = textEdit_email.EditValue.ToString().Split(',');
                //
                bool chkemail = true;
                foreach (var s in em1)
                {
                    Regex re = new Regex(strRegex);
                    chkemail = re.IsMatch(s);
                    //
                    if (chkemail == false)
                    {
                        label = "";// labelControlEmail.Text;
                        message = getLanguage("_msg_1002");
                        _ValidateTable.Rows.Add(label, message);
                        break;
                    }
                }
            }

            //
            return _ValidateTable;
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_sofwarename")) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort;
            }
        }

    }
}
