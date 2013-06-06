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
    public partial class PopupAddTenant : uFormBase
    {
        public PopupAddTenant()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(RoomCheckInAddTenant_Load);
            //
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
        }

        public DataTable RoommateTableTemp = null;
        public int room_id = 0;

        void RoomCheckInAddTenant_Load(object sender, EventArgs e)
        {
            initDropDownPrefix();
        }

        void initDropDownPrefix()
        {
            lookUpEditPrefix.Properties.DataSource = BusinessLogicBridge.DataStore.getAllPrefix();
            lookUpEditPrefix.Properties.DisplayMember = "prefix_" + current_lang + "_label";
            lookUpEditPrefix.Properties.ValueMember = "prefix_id";
            lookUpEditPrefix.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("prefix_" + current_lang + "_label", 0, getLanguage("_prefix")));

            lookUpEditPrefix.Properties.NullText = getLanguage("_select_prefix");
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
                XtraMessageBox.Show(message, getLanguage("_failed"));
                return;
            }
            //
            DataTable RoommateInfo = BusinessLogicBridge.DataStore.getRoommateByIDcard(textEditIDCard.EditValue.ToString().Trim());

            if (RoommateTableTemp != null) {

                DataRow[] foundRows = RoommateTableTemp.Select("tenant_roomate_idcard='" + textEditIDCard.EditValue.ToString().Trim() + "'");
                if (foundRows.Length > 0)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1007"), getLanguage("_softwarename")); return;
                }
               
            }
            //
            if (RoommateInfo.Rows.Count > 0 )
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1007"), getLanguage("_softwarename"));
                return;
            }else{
                try
                {
                    // Success
                    RoommateTableTemp.Rows.Add(lookUpEditPrefix.EditValue, textEditName.EditValue, textEditSurname.EditValue, textEditIDCard.EditValue, textEditmobile.EditValue, 1, room_id, lookUpEditPrefix.Text);
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                }
                catch (Exception ex){ }
                    //
                this.DialogResult = DialogResult.OK;
            }
        }

        private DataTable validateData()
        {
            String label = "";
            String message = "";
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (lookUpEditPrefix.EditValue == null)
            {
                label = labelControlTitle.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditName.EditValue.ToString() == "")
            {
                label = labelControlName.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditSurname.EditValue.ToString() == "")
            {
                label = labelControlSurname.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }           
            if (textEditIDCard.EditValue.ToString() == "")
            {
                label = labelControlIDCard.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            
            return _ValidateTable;
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
