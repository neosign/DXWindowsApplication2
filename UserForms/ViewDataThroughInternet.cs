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
    public partial class ViewDataThroughInternet : uBase
    {
        public ViewDataThroughInternet()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ViewDataThroughInternet_Load);
            checkEditAgree.EditValueChanged += new EventHandler(checkEditAgree_EditValueChanged);
        }

        void checkEditAgree_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEditAgree.Checked == true)
            {
                if (MainForm.TrialVersion == false)
                {
                    // registered
                    bttCancel.Enabled = true;

                    checkEditAgree.Enabled = false;

                    textEditEmail.Enabled = false;
                    textEditPassword.Enabled = false;
                    textEditPassword2.Enabled = false;
                    textEditFullname.Enabled = false;
                    bttSave.Enabled = false;

                    groupControlRegister.Enabled = true;
                }
                else
                {
                    // trial version

                    checkEditAgree.Enabled = true;

                    textEditEmail.Enabled = true;
                    textEditPassword.Enabled = true;
                    textEditPassword2.Enabled = true;
                    textEditFullname.Enabled = true;
                    bttSave.Enabled = true;

                    groupControlRegister.Enabled = false;

                }

            }
            else {
                groupControlRegister.Enabled = false;
            }
        }

        void ViewDataThroughInternet_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData() {

            if (MainForm.TrialVersion == false)
            {
                // registered
                bttCancel.Enabled = true;

                checkEditAgree.Enabled = false;

                textEditEmail.Enabled = false;
                textEditPassword.Enabled = false;
                textEditPassword2.Enabled = false;
                textEditFullname.Enabled = false;
                bttSave.Enabled = false;

                groupControlRegister.Enabled = true;
            }
            else
            {
                // trial version

                checkEditAgree.Enabled = true;

                textEditEmail.Enabled = true;
                textEditPassword.Enabled = true;
                textEditPassword2.Enabled = true;
                textEditFullname.Enabled = true;
                bttSave.Enabled = true;

                groupControlRegister.Enabled = false;

            }
        }

        private DataTable validateDate()
        {
            String label;
            String message;
            DataTable _ValidateTable = new DataTable();
            DataTable _Error = new DataTable();
            _Error.Columns.Add("label", typeof(String));
            _Error.Columns.Add("message", typeof(String));

            if (textEditEmail.EditValue.ToString() != "")
            {
                string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";

                Regex re = new Regex(strRegex);
                if (re.IsMatch(textEditEmail.EditValue.ToString()) == false)
                {
                    label = labelControlEmail.Text;
                    message = getLanguage("_msg_1002");
                    _ValidateTable.Rows.Add(label, message);

                }
            }
            return _Error;
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _Error = validateDate();
                if (_Error.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _Error.Rows.Count; i++)
                    {
                        message = message + _Error.Rows[i]["label"] + " " + _Error.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }

    }
}

