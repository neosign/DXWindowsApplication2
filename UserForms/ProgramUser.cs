using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Text.RegularExpressions;

namespace DXWindowsApplication2.UserForms
{
    public partial class ProgramUser : uBase
    {
        string StatusUse;
        string StatusLock;
        string OldUserName;

        private string lockTxt;
        private string usingTxt;

        public ProgramUser()
        {
            InitializeComponent();
            //
            this.Dock = DockStyle.Fill;
            //
            this.Load += new EventHandler(ProgramUser_Load);
            //            
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            //
            bttAdd.Click += new EventHandler(bttAdd_Click);
            bttCancel.Click += new EventHandler(bttCancel_Click);
            bttDelete.Click += new EventHandler(bttDelete_Click);
            bttEdit.Click += new EventHandler(bttEdit_Click);
            bttSave.Click += new EventHandler(bttSave_Click);

            SaveClick += new EventHandler(ProgramUser_SaveClick);
        }

        void ProgramUser_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        public override void Refresh()
        {
            base.Refresh();

            setLangThis();
            LoadUserGroup();
            getAllAcccount();
        }

        void ProgramUser_Load(object sender, EventArgs e)
        {
            setDisable();

            setLangThis();
            //
            LoadUserGroup();
            //
            StatusUse = getLanguage("_user_enable");
            StatusLock = getLanguage("_user_disable");
            //
            getAllAcccount();            
            //
            setLangThis();
        }

        void LoadUserGroup()
        {
            DataTable group_account = BusinessLogicBridge.DataStore.get_group_account();
            lookUpEditGroupAccount.Properties.DataSource = group_account;
            lookUpEditGroupAccount.Properties.DisplayMember = "group_name";
            lookUpEditGroupAccount.Properties.ValueMember = "group_id";
            lookUpEditGroupAccount.Properties.NullText = "[" + getLanguage("_select_group_name") + "]";
        }

        void setLangThis()
        {
            this.Name = getLanguage("_user_info");
            //
            this.groupList.Text = getLanguage("_user_list");
            this.groupUser.Text = getLanguage("_user_info");
            //
            this.labelControlUserGroup.Text = getLanguageWithColon("_group_name");
            this.labelControlName.Text = getLanguageWithColon("_user_name");
            this.labelControlUsername.Text = getLanguageWithColon("_user_logon");
            this.labelControlPassword.Text = getLanguageWithColon("_password");
            this.labelControlPassword2.Text = getLanguageWithColon("_password_confirm");
            this.labelControlStatus.Text = getLanguageWithColon("_user_status");
            this.labelControlRequired.Text = getLanguage("_required");
            //
            // Grid
            this.gridProgramGroupName.Caption = getLanguage("_group_name");
            this.gridProgramName.Caption = getLanguage("_user_name");
            this.gridProgramUsername.Caption = getLanguage("_user_logon");
            this.gridProgramUserStatusText.Caption = getLanguage("_user_status");
            //
            this.bttAdd.Text = getLanguage("_add");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttDelete.Text = getLanguage("_delete");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");

            lockTxt = getLanguage("_locked");
            usingTxt = getLanguage("_using");

        }

        #region Method

        void setDisable()
        {
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            enableInput(false);
            bttAdd.Enabled = true;
            bttDelete.Enabled = true;
            bttEdit.Enabled = true;
            gridControlUser.Enabled = true;
            //
            if(gridView1.RowCount > 0)
                gridView1.SelectRow(1);
        }

        void setEnable()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            enableInput(true);
        }

        void setAddAction()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            enableInput(true);
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
            textEditAction.EditValue = "add";
        }

        void setEditAction()
        {
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            enableInput(true);
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
            textEditAction.EditValue = "update";

            if (textEditUsername.EditValue.ToString() == "admin") textEditName.Enabled = false;

            textEditUsername.Focus();

        }

        void enableInput(bool status)
        {
            lookUpEditGroupAccount.Enabled = status;
            textEditUsername.Enabled = status;

            if(status==true)
                gridControlUser.Enabled = !status;

            try
            {
                if (textEditUsername.EditValue != null)
                {
                    if (textEditUsername.EditValue.ToString() == "admin")
                    {
                        textEditUsername.Enabled = false;
                        lookUpEditGroupAccount.Enabled = false;
                        textEditName.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { }

            radioGroupLockStatus.Properties.Items[0].Description = usingTxt;
            radioGroupLockStatus.Properties.Items[1].Description = lockTxt;

            textEditName.Enabled = status;
            textEditPassword.Enabled = status;
            textEditPassword2.Enabled = status;
            radioGroupLockStatus.Enabled = status;
            //
            //gridControl1.Enabled = !status;
        }

        public void getAllAcccount()
        {
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

            DataTable accountData = BusinessLogicBridge.DataStore.getAllAcccount();
            accountData.Columns.Add("status_text", typeof(string));
            for (int i = 0; i < accountData.Rows.Count; i++)
            {
                string change = accountData.Rows[i][3].ToString();

                if (change == "1")
                {
                    accountData.Rows[i]["status_text"] = StatusUse;
                }
                else
                {
                    accountData.Rows[i]["status_text"] = StatusLock;
                }
            }

            gridControlUser.DataSource = accountData;
        }

        #endregion

        #region Event

        private void bttAdd_Click(object sender, EventArgs e)
        {
            // Set Empty field
            //lookUpEditGroupAccount.EditValue = 0;
            lookUpEditGroupAccount.Focus();
            textEditUsername.EditValue = "";
            textEditPassword.EditValue = "";
            textEditPassword2.EditValue = "";
            textEditName.EditValue = "";
            radioGroupLockStatus.SelectedIndex = 0;
            //
            setAddAction();
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEditAction();
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            List<string> listError = new List<string>();

            string TextOnly = getLanguage("_text_only");
            string PasswordMismatch = getLanguage("_msg_1016");
            string EmptySting = getLanguage("_msg_1001");
            string DuplicateString = getLanguage("_msg_1015");
            // Validation Control
            string msgError = "";

            #region validate Username

            if (utilClass.isEmpty(textEditUsername.Text) == true)
            {
                if (utilClass.IsAlphaNumeric(textEditUsername.Text) == false)
                {
                    listError.Add(labelControlUsername.Text + TextOnly);
                }
            }
            else
            {
                listError.Add(labelControlUsername.Text + EmptySting);
            }
            //
            if (utilClass.isEmpty(textEditName.Text) == false)
            {
                listError.Add(labelControlName.Text + EmptySting);
            }
            //
            if (textEditAction.Text == "add")
            {
                if (BusinessLogicBridge.DataStore.IsDuplicateUserAccount(textEditUsername.Text.Trim()))
                    listError.Add(DuplicateString);
            }            
            if (textEditAction.Text == "update")
            {
                if (OldUserName != textEditUsername.Text)
                {
                    if (BusinessLogicBridge.DataStore.IsDuplicateUserAccount(textEditUsername.Text.Trim()))
                        listError.Add(DuplicateString);
                }
            }
            #endregion

            # region validate Password

            if (utilClass.isEmpty(textEditPassword.Text) == false)
            {
                listError.Add(labelControlPassword.Text + EmptySting);
            }
            if (utilClass.isEmpty(textEditPassword2.Text) == false)
            {
                listError.Add(labelControlPassword2.Text + EmptySting);
            }
            //
            if (utilClass.isEmpty(textEditPassword.Text) && utilClass.isEmpty(textEditPassword2.Text))
            {
                if(textEditPassword.Text != textEditPassword2.Text)
                    listError.Add(PasswordMismatch);
            }
                       
            #endregion

            if (listError.Count > 0)
            {
                msgError = string.Join("\r\n", listError.ToArray());
                //
                utilClass.showPopupMessegeBox(this, msgError, getLanguage("_softwarename"), "info");
                TrySaveError = true;
                //
                return;
            }

            if (textEditAction.Text == "add")
            {
                DataTable AccountOfGroup = BusinessLogicBridge.DataStore.getAllAcccountByGroupID(lookUpEditGroupAccount.EditValue.To<int>());

                if (AccountOfGroup.Rows.Count >= 10) {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1059"), getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                //DataTable AllAccountDT = ((DataTable)gridControl1.DataSource);

                //if (AllAccountDT.Rows.Count >= 50) {

                //    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1060"), getLanguage("_softwarename"));
                //    return;
                //}

                try
                {
                    // insert
                    BusinessLogicBridge.DataStore.InsertUserAccount(textEditUsername.Text, textEditPassword.Text, Convert.ToInt16(radioGroupLockStatus.EditValue), Convert.ToInt16(lookUpEditGroupAccount.EditValue), textEditName.Text);

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");

                    getAllAcccount();
                    setDisable();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    BusinessLogicBridge.DataStore.UpdateUserAccount(textEditUsername.Text, textEditName.Text, textEditPassword.Text, Convert.ToInt16(radioGroupLockStatus.EditValue), Convert.ToInt16(lookUpEditGroupAccount.EditValue), Convert.ToInt16(textEditAccountId.Text));
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");


                    getAllAcccount();
                    setDisable();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            if (textEditUsername.Text == "admin")
            {   
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1052"), getLanguage("_softwarename"));
                return;
            }

            if (textEditUsername.EditValue.ToString() == DXWindowsApplication2.MainForm.username)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1051"), getLanguage("_softwarename"));
                return;
            }
            else {

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                {

                    BusinessLogicBridge.DataStore.DeletedUserAccount(textEditAccountId.EditValue.To<int>());
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");

                    getAllAcccount();
                }
            }

        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            // Check Update
            
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                setDisable();
            }
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable accountData = BusinessLogicBridge.DataStore.getAllAcccount();
            //
            string groupname;
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length <= 0)
                return;

            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);

                    if (CurrentRow == null) return;

                    int account_id = Convert.ToInt32(CurrentRow["user_id"].ToString());
                    DataTable accountDetail = BusinessLogicBridge.DataStore.getAccountById(account_id);

                    textEditAccountId.EditValue = account_id;    //1
                    textEditUsername.EditValue = accountDetail.Rows[0]["username"].ToString();

                    if (accountDetail.Rows[0]["username"].ToString() == "admin")
                    {
                        textEditUsername.Enabled = false;
                        lookUpEditGroupAccount.Enabled = false;
                        textEditName.Enabled = false;
                    }
                    //
                    OldUserName = textEditUsername.Text;
                    //
                    textEditPassword.EditValue = accountDetail.Rows[0]["password"].ToString();
                    textEditPassword2.EditValue = accountDetail.Rows[0]["password"].ToString();
                    textEditName.EditValue = accountDetail.Rows[0]["name"].ToString();

                    //DataTable amount_accountDT = BusinessLogicBridge.DataStore.getAllAcccountByGroupID(accountDetail.Rows[0]["group_id"].To<int>());

                    //if (amount_accountDT.Rows.Count >= 10)
                    //{
                    //    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1059"), getLanguage("_softwarename"));
                    //    TrySaveError = true;
                    //    return;
                    //}

                    //
                    groupname = BusinessLogicBridge.DataStore.getGroupNameById(accountDetail.Rows[0]["group_id"].To<int>());
                    //
                    lookUpEditGroupAccount.EditValue = lookUpEditGroupAccount.Properties.GetKeyValueByDisplayText(groupname);
                    if (accountDetail.Rows[0]["status"].ToString() == "1")
                    {
                        radioGroupLockStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        radioGroupLockStatus.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString());

            }
        }

        #endregion
    }
}
