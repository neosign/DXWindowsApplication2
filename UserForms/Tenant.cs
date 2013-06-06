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
    public partial class Tenant : uBase
    {
        private int current_province;
        private string status_label = "";
        private string button_event = "";
        private int action_key = 0;
        //
        public Tenant()
        {            
            InitializeComponent();
            //
            this.Load += new System.EventHandler(this.Tenant_Load);
            //
            gridViewTenant.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            //lookUpEditProvinceId.EditValueChanged += new EventHandler(lookUpEditProvinceId_EditValueChanged);
            //
            bttAdd.Click +=new EventHandler(bttAdd_Click);
            bttCancel.Click +=new EventHandler(bttCancel_Click);
            bttDelete.Click +=new EventHandler(bttDelete_Click);
            bttEdit.Click+=new EventHandler(bttEdit_Click);
            bttSave.Click+=new EventHandler(bttSave_Click);
            //
            textEditName.EditValueChanged +=new EventHandler(EditValueChanged);
            textEditAddress.EditValueChanged += new EventHandler(EditValueChanged);
            textEditBuilding.EditValueChanged += new EventHandler(EditValueChanged);
            textEditCarLicence.EditValueChanged += new EventHandler(EditValueChanged);
            textEditFloor.EditValueChanged += new EventHandler(EditValueChanged);
            textEditIDCard.EditValueChanged += new EventHandler(EditValueChanged);
            textEditPhone.EditValueChanged += new EventHandler(EditValueChanged);
            textEditPostcode.EditValueChanged += new EventHandler(EditValueChanged);
            textEditRemark.EditValueChanged += new EventHandler(EditValueChanged);
            textEditSurname.EditValueChanged += new EventHandler(EditValueChanged);

            SaveClick += new EventHandler(bttSave_Click);

        }

        private void Tenant_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //
            splitContainerControl2.SplitterPosition = int.Parse(Math.Floor(this.Width / 2.5).ToString());

            initTenant();
            setLangThis();
            initDropDownPrefix();
           
        }

        public override void Refresh()
        {
            base.Refresh();

            setLangThis();
            initDropDownPrefix();
            initTenant();
        }

        public void setLangThis()
        {
            //
            this.Name = getLanguage("_tenant_info");
            //
            this.groupControlTenant.Text = getLanguage("_tenant");
            this.groupControlRental.Text = getLanguage("_rental");
            this.groupControlTenantList.Text = getLanguage("_tenant_list");
            //
            this.labelControlTitle.Text = getLanguageWithColon("_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlSurname.Text = getLanguageWithColon("_lastname");
            this.labelControlIDCard.Text = getLanguageWithColon("_idcard_passport");
            this.labelControlBirthday.Text = getLanguageWithColon("_birthday");
            this.labelControlAddress.Text = getLanguageWithColon("_address");
            this.labelControlProvince.Text = getLanguageWithColon("_province");
            this.labelControlDistinct.Text = getLanguageWithColon("_district");
            this.labelControlPostcode.Text = getLanguageWithColon("_zipcode");
            this.labelControlPhone.Text = getLanguageWithColon("_tel");
            this.labelControlCarLicence.Text = getLanguageWithColon("_car_license");
            this.labelControlRemark.Text = getLanguageWithColon("_remark");
            this.labelControlRequired.Text = getLanguage("_required");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            this.labelControlDateIn.Text = getLanguageWithColon("_check_in_date");
            this.labelControlDateOut.Text = getLanguageWithColon("_check_out_date");
            this.labelControlStatus.Text = getLanguageWithColon("_status");



            // Grid
            this.tenant_prefix.Caption = getLanguage("_prefix");
            this.tenant_prefix.FieldName = "prefix_" + current_lang + "_label";
            this.tenant_name.Caption = getLanguage("_firstname");
            this.tenant_surname.Caption = getLanguage("_lastname");
            this.colTenantStatus.Caption = getLanguage("_status");

            this.bttAdd.Text = getLanguage("_add");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttDelete.Text = getLanguage("_delete");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");

        }
        
        #region Setup
        void initDataDefault()
        {
            lookUpEditPrefix.EditValue = "";
            textEditName.EditValue = "";
            textEditSurname.EditValue = "";
            dateEditBirthday.EditValue = DateTime.Now;
            textEditIDCard.EditValue = "";
            textEditAddress.EditValue = "";
            textEditProvince.EditValue = "";
            textEditDistrict.EditValue = "";
            textEditCarLicence.EditValue = "";
            textEditPostcode.EditValue = "";
            textEditPhone.EditValue = "";
            textEditRemark.EditValue = "";
            textEditBuilding.EditValue = "";
            textEditFloor.EditValue = "";
            textEditRoomType.EditValue = "";
            textEditCheckinDate.EditValue = DateTime.Now;
        }
        void initTenant()
        {
            DataTable TenantTable = BusinessLogicBridge.DataStore.Tenant_get();

            string label = "prefix_"+current_lang+"_label";
            if(current_lang=="th")
                status_label = "tenant_status_label";
            else
                status_label = "tenant_status_label_" + current_lang;

            tenant_prefix.FieldName = label;
            colTenantStatus.FieldName = status_label;

            gridControlTenant.DataSource = TenantTable;
            
            //
            if (TenantTable.Rows.Count == 0)
            {
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;
            }
        }
        void initDropDownPrefix()
        {            
            lookUpEditPrefix.Properties.DataSource = BusinessLogicBridge.DataStore.getAllPrefix();
            lookUpEditPrefix.Properties.DisplayMember = "prefix_" + current_lang + "_label";
            lookUpEditPrefix.Properties.ValueMember = "prefix_id";
            lookUpEditPrefix.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("prefix_" + current_lang + "_label", 0, getLanguage("_prefix")));
            
            lookUpEditPrefix.Properties.NullText = getLanguage("_select_prefix");
        }
        void initDropDownProvince()
        {
            //lookUpEditProvinceId.Properties.DataSource = BusinessLogicBridge.DataStore.getAllProvince();
            //lookUpEditProvinceId.Properties.DisplayMember = "province_" + current_lang + "_label";
            //lookUpEditProvinceId.Properties.ValueMember = "province_id";
            //lookUpEditProvinceId.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("province_" + current_lang + "_label", 0, getLanguage("_province")));

            //lookUpEditProvinceId.Properties.NullText = getLanguage("_select_province");
        }
        void initDropDownDistrict(int province_id)
        {
            //lookUpEditDistrictId.Properties.Columns.Clear();
            ////
            //lookUpEditDistrictId.Properties.DataSource = BusinessLogicBridge.DataStore.getAllDistrict(province_id);
            //lookUpEditDistrictId.Properties.DisplayMember = "district_" + current_lang + "_label";
            //lookUpEditDistrictId.Properties.ValueMember = "district_id";
            //lookUpEditDistrictId.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("district_" + current_lang + "_label", 0, getLanguage("_district")));

            //lookUpEditDistrictId.Properties.NullText = getLanguage("_select_district");
        }
        
        
        #endregion

        #region Action Extra
        void clear()
        {
            DateTime today = DateTime.Today;

            initDataDefault();
        }
        void enabled(Boolean status)
        {
            lookUpEditPrefix.Enabled = status;
            textEditName.Enabled = status;
            textEditSurname.Enabled = status;
            dateEditBirthday.Enabled = status;
            textEditIDCard.Enabled = status;
            textEditAddress.Enabled = status;
            textEditProvince.Enabled = status;
            textEditDistrict.Enabled = status;
            textEditCarLicence.Enabled = status;
            textEditPhone.Enabled = status;
            textEditPostcode.Enabled = status;
            textEditRemark.Enabled = status;

        }
        private DataTable validateData()
        {
            String label = "";
            String message = "";
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));
            
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
            if (dateEditBirthday.EditValue == null)
            {
                label = labelControlBirthday.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditAddress.EditValue.ToString() == "")
            {
                label = labelControlAddress.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditProvince.EditValue.ToString() == "")
            {
                label = labelControlProvince.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditDistrict.EditValue.ToString() == "")
            {
                label = labelControlDistinct.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditPostcode.EditValue.ToString() == "")
            {
                label = labelControlPostcode.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            if (textEditPhone.EditValue.ToString() == "")
            {
                label = labelControlPhone.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
            }
            return _ValidateTable;
        }
        void changeRow()
        {
            int[] rowIndex = gridViewTenant.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewTenant.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewTenant.GetDataRow(0);
                }
                int tenant_id                       = CurrentRow["tenant_id"].To<int>();
                textEditTenantID.EditValue          = CurrentRow["tenant_id"].ToString();
                textEditTenant_relate_id.EditValue  = CurrentRow["tenant_relate_id"].ToString();

                lookUpEditPrefix.EditValue = CurrentRow["tenant_prefix_id"].To<int>();

                textEditName.EditValue = CurrentRow["tenant_name"].ToString();
                textEditSurname.EditValue = CurrentRow["tenant_surname"].ToString();
                dateEditBirthday.EditValue = DateTime.Parse(CurrentRow["tenant_birthday"].ToString());
                textEditIDCard.EditValue = CurrentRow["tenant_idcard_no"].ToString();
                textEditAddress.EditValue = CurrentRow["tenant_address"].ToString();
                textEditProvince.EditValue = CurrentRow["tenant_province_id"].ToString();
                textEditDistrict.EditValue = CurrentRow["tenant_distict_id"].ToString();
                textEditCarLicence.EditValue = CurrentRow["tenant_car"].ToString();
                textEditPostcode.EditValue = CurrentRow["tenant_postcode"].ToString();
                textEditPhone.EditValue = CurrentRow["tenant_phone"].ToString();
                textEditRemark.EditValue = CurrentRow["tenant_note"].ToString();
                
                textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();
                textEditBuilding.EditValue = CurrentRow["building_label"].ToString();
                textEditFloor.EditValue = CurrentRow["floor_code"].ToString();
                textEditRoomType.EditValue = CurrentRow["roomtype_label"].ToString();

                string room_status_label = "";

                if (current_lang == "th")
                    room_status_label = "room_status_label";
                else
                    room_status_label = "room_status_label_" + current_lang;

                textEditStatus.EditValue = CurrentRow[status_label].ToString();

                textEditCheckinDate.EditValue = CurrentRow["check_in_date"].ToString() != "" ? CurrentRow["check_in_date"].To<DateTime>().ToString("dd/MM/yyyy") : "";
                textEditCheckOutDate.EditValue = CurrentRow["check_in_closeddate"].ToString() != "" ? CurrentRow["check_in_closeddate"].To<DateTime>().ToString("dd/mm/yyyy") : "";
                textEditDateCreate.EditValue = CurrentRow["tenant_created_date"].ToString() != "" ? CurrentRow["tenant_created_date"].To<DateTime>() : DateTime.Now;
            }
            else
            {
                initDataDefault();
            }
        }
        #endregion

        #region Change Row
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        private void lookUpEditProvinceId_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
            //LookUpEdit row = (LookUpEdit)sender;
            //if (row.EditValue != null)
            //{
            //    //
            //    int province_id = Convert.ToInt32(row.EditValue.ToString());
            //    initDropDownDistrict(province_id);
            //    if (current_province != province_id)
            //    {
            //        lookUpEditDistrictId.EditValue = null;                    
            //        current_province = province_id;
            //    }
            //}
        }
        #endregion

        #region Button Event
        private void bttAdd_Click(object sender, EventArgs e)
        {
            clear();
            enabled(true);
            textEditDistrict.Enabled = false;
            gridControlTenant.Enabled = false;

            button_event = "add";
            action_key = 0;
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
        }
        private void bttEdit_Click(object sender, EventArgs e)
        {
            enabled(true);
            //
            button_event = "edit";
            action_key = 0;
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            gridControlTenant.Enabled = false;
        }
        private void bttDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEditTenantID.EditValue.ToString() == "")
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_select_tenant"), getLanguage("_softwarename"));
                    return;
                }
                Boolean remove_status = false;
                bool relate_status = false;
                int count_relate = 0;
                int relate_id = 0;
                int[] rowIndex = gridViewTenant.GetSelectedRows();
                if (rowIndex.Length != 0)
                {
                    DataRow CurrentRow = gridViewTenant.GetDataRow(rowIndex[0]);
                    if (CurrentRow == null)
                    {
                        CurrentRow = gridViewTenant.GetDataRow(0);
                    }
                    if (CurrentRow["room_status"].ToString() == "")
                    {
                        CurrentRow["room_status"] = 0;
                    }
                    int room_status = int.Parse(CurrentRow["room_status"].ToString());

                    int tenant_status = int.Parse(CurrentRow["tenant_status"].ToString());

                    relate_id  = textEditTenant_relate_id.EditValue.To<int>();

                    count_relate = BusinessLogicBridge.DataStore.tenant_countTenantByRelateID(relate_id);

                    remove_status = BusinessLogicBridge.DataStore.Tenant_CheckForDelete(relate_id);


                    if (remove_status == true)
                    {
                        if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                        {
                            int tenant_id = int.Parse(textEditTenantID.EditValue.ToString());
                            BusinessLogicBridge.DataStore.Tenant_remove(relate_id);
                            initTenant();

                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");
                        }
                    }
                    else
                    {
                        if (count_relate > 1)
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1088"), getLanguage("_softwarename"));
                            return;
                        }
                        else
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                            return;
                        }
                    }
                }

                
                //Default Form
                enabled(false);
                textEditDistrict.Enabled = false;

                action_key = 0;
                button_event = "";
                bttCancel.Enabled = false;
                bttSave.Enabled = false;
                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
                gridControlTenant.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _ValidateTable = validateData();
                if (_ValidateTable.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                    {
                        message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
                string date_modified = "";

                date_modified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DataTable _Tenant = new DataTable();
                _Tenant.Columns.Add("tenant_id", typeof(String));
                _Tenant.Columns.Add("tenant_prefix_id", typeof(int));
                _Tenant.Columns.Add("tenant_title", typeof(String));
                _Tenant.Columns.Add("tenant_name", typeof(String));
                _Tenant.Columns.Add("tenant_surname", typeof(String));
                _Tenant.Columns.Add("tenant_birthday", typeof(DateTime));
                _Tenant.Columns.Add("tenant_idcard_no", typeof(String));
                _Tenant.Columns.Add("tenant_address", typeof(String));
                _Tenant.Columns.Add("tenant_province_id", typeof(String));
                _Tenant.Columns.Add("tenant_distict_id", typeof(String));
                _Tenant.Columns.Add("tenant_postcode", typeof(String));
                _Tenant.Columns.Add("tenant_car", typeof(String));
                _Tenant.Columns.Add("tenant_mobile", typeof(String));
                _Tenant.Columns.Add("tenant_phone", typeof(String));
                _Tenant.Columns.Add("tenant_email", typeof(String));
                _Tenant.Columns.Add("tenant_note", typeof(String));
                _Tenant.Columns.Add("tenant_created_date", typeof(String));
                _Tenant.Columns.Add("tenant_modified_date", typeof(String));
                _Tenant.Rows.Add(
                    textEditTenantID.EditValue,
                    lookUpEditPrefix.EditValue,
                    "",
                    textEditName.EditValue,
                    textEditSurname.EditValue,
                    DateTime.Parse(dateEditBirthday.EditValue.ToString()),
                    textEditIDCard.EditValue,
                    textEditAddress.EditValue,
                    textEditProvince.EditValue,
                    textEditDistrict.EditValue,
                    textEditPostcode.EditValue,
                    textEditCarLicence.EditValue,
                    "",
                    textEditPhone.EditValue,
                    "",
                    textEditRemark.EditValue,
                    textEditDateCreate.EditValue.ToString(),
                    date_modified
                );
                switch (button_event)
                {
                    case "add":
                            BusinessLogicBridge.DataStore.Tenant_insert(_Tenant);
                            
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        break;
                    case "edit":
                        
                            BusinessLogicBridge.DataStore.Tenant_update(_Tenant);
                            
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        
                        break;
                }
                initTenant();
                gridControlTenant.Enabled = true;
                changeRow();
                enabled(false);
                textEditDistrict.Enabled = false;

                action_key = 0;
                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {            
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"),getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                enabled(false);

                action_key = 0;
                button_event = "";
                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                gridControlTenant.Enabled = true;
            }
        }
        #endregion

        #region Key Event
        private void EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
        }
        #endregion

    }
}
