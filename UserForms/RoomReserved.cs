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
    public partial class RoomReserved : uBase
    {
        private string button_event = "";
        private int action_key = 0;
        //
        public int selectRoomID = 0;
        public int selectReserveID = 0;
        private string genContractNo = "";
        private string prefix = "";
        private int doc_id = 0;
        private int contract_start = 0;

        public RoomReserved()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(RoomReserved_Load);
            this.Resize += new EventHandler(RoomReserved_Resize);
            //
            this.gvControlRoomLeave.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            this.gvControlRoomLeave.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gvControlRoomLeave_RowClick);
            this.gvControlReserved.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gvControlReserved_RowClick);
            
            gvControlReserved.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gvControlReserved_RowCellStyle);
            gvControlRoomLeave.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gvControlRoomLeave_RowCellStyle);
            //
            lookUpEditPrefix.EditValueChanged += new EventHandler(textEdit_EditValueChanged);
            textEditName.EditValueChanged += new EventHandler(textEdit_EditValueChanged);
            textEditLastName.EditValueChanged += new EventHandler(textEdit_EditValueChanged);
            textEditPhone.EditValueChanged += new EventHandler(textEdit_EditValueChanged);
            textEditPayments.EditValueChanged += new EventHandler(textEdit_EditValueChanged);
            dateEditCheckIn.EditValueChanged += new EventHandler(dateEditCheckIn_EditValueChanged);
            //
            this.bttCancel.Click += new EventHandler(bttCancel_Click);
            this.bttEdit.Click += new EventHandler(bttEdit_Click);
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttPay.Click += new EventHandler(bttPay_Click);
            this.bttPrintContract.Click += new EventHandler(bttPrintContract_Click);
            this.bttCancelReserve.Click += new EventHandler(bttCancelReserve_Click);
        }

        void gvControlRoomLeave_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            changeRow();
            enable(false);

            button_event = "Add";
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            //
            bttCancelReserve.Enabled = false;
            bttPay.Enabled = false;
            bttPrintContract.Enabled = false;
        }

        void gvControlReserved_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("reserve_end_date"))
                return;


            if (gvControlReserved.GetRowCellValue(e.RowHandle, "reserve_end_date").ToString() != "")
            {

                //Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "leave_date_real")

                int result = DateTime.Compare(gvControlReserved.GetRowCellValue(e.RowHandle, "reserve_end_date").To<DateTime>().Date, DateTime.Now.Date);

                if (result < 0)
                {
                    e.Appearance.BackColor = Color.OrangeRed;

                }

            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        void gvControlRoomLeave_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("leave_date"))
                return;


            if (gvControlRoomLeave.GetRowCellValue(e.RowHandle, "leave_date").ToString() != "")
            {

                //Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "leave_date_real")

                int result = DateTime.Compare(gvControlRoomLeave.GetRowCellValue(e.RowHandle, "leave_date").To<DateTime>().Date, DateTime.Now.Date);

                    if (result < 0)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                    }

            }else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        void RoomReserved_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
        }

        void dateEditCheckIn_EditValueChanged(object sender, EventArgs e)
        {
            dateEditEndOfReserved.Properties.MinValue = dateEditCheckIn.EditValue.To<DateTime>();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
            //
            initRoom();
            initReserved();
            //
            if (selectRoomID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlRoomLeave.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gvControlRoomLeave.FocusedRowHandle = c.LocateByValue("room_id", selectRoomID);
                //
                selectRoomID = 0;
            }
            if(selectReserveID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlReserved.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gvControlReserved.FocusedRowHandle = c.LocateByValue("reserve_room_id", selectReserveID);
                //
                selectReserveID = 0;
            }
            //
            changeRow();
        }

        void RoomReserved_Load(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;

            this.Dock = DockStyle.Fill;
            //
            initDropDownPrefix();
            initRoom();
            initReserved();
            //
            setLangThis();
            dateEditEndOfReserved.Properties.MinValue = DateTime.Now;

        }

        public void setLangThis()
        {
            //
            this.Name = getLanguage("_reserve_room");
            //
            this.groupControlList.Text = getLanguage("_check_free_list");
            this.groupControlReserve.Text = getLanguage("_reserve_room");
            this.groupControlReserveList.Text = getLanguage("_vacant_and_booked");
            this.groupDaily.Text = getLanguage("_daily");
            this.groupBoxMonthly.Text = getLanguage("_monthly");
            this.groupRoomInfo.Text = getLanguage("_room");
            //
            this.grid_room_label.Caption = getLanguage("_room_name");
            this.grid_building_label.Caption = getLanguage("_building");
            this.grid_roomtype_label.Caption = getLanguage("_room_type");
            this.grid_leave_date.Caption = getLanguage("_require_check_out_date");
            this.grid_floor_code.Caption = getLanguage("_floor");
            this.grid_status_label.Caption = getLanguage("_status");
            //
            this.grid_reserve_check_in_date.Caption = getLanguage("_require_check_in_date");
            this.grid_reserve_end_date.Caption = getLanguage("_expire_date");
            this.grid_reserve_lastname.Caption = getLanguage("_lastname");
            this.grid_reserve_name.Caption = getLanguage("_firstname");
            this.grid_reserve_payments.Caption = getLanguage("_book_amount");
            this.grid_reserve_room_label.Caption = getLanguage("_room_name");
            this.grid_reserve_status.Caption = getLanguage("_status");
            this.grid_reserve_status.FieldName = "reserve_flag_" + current_lang;
            //
            this.labelControlTitle.Text = getLanguageWithColon("_prefix");
            this.lookUpEditPrefix.Properties.NullText = getLanguage("_select_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlLastName.Text = getLanguageWithColon("_lastname");
            this.labelControlPhone.Text = getLanguageWithColon("_tel");
            this.labelControlPayments.Text = getLanguageWithColon("_book_amount");
            this.labelControlCheckIn.Text = getLanguageWithColon("_require_check_in_date");
            this.labelControlEndOfReserved.Text = getLanguageWithColon("_expire_date");
            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            //
            this.labelControlMonthlyRate.Text = getLanguageWithColon("_rent");
            this.labelControlDailyRate.Text = getLanguageWithColon("_rent");
            this.labelControlInsurance.Text = getLanguageWithColon("_insurance_charge");
            this.labelControlAdvance.Text = getLanguageWithColon("_advance_charge");
            //
            this.labelControlBaht.Text = getLanguage("_baht");
            this.labelControlBaht1.Text = getLanguage("_baht");
            this.labelControlBaht2.Text = getLanguage("_baht");
            this.labelControlBaht3.Text = getLanguage("_baht");
            this.labelControlBaht1.Text = getLanguage("_baht");
            this.labelControlMonth2.Text = getLanguage("_month");
            //
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            //
            this.bttPrintContract.Text = getLanguage("_print_booking_contract");
            this.bttPay.Text = getLanguage("_payment");
            //
            this.bttCancelReserve.Text = getLanguage("_cancel_booking");

        }

        #region Setup
        void initDataDefault()
        {
            textEditTenantId.EditValue = "0";
            textEditCoderef.EditValue = "";
            //lookUpEditPrefix.
            textEditName.EditValue = "";
            textEditLastName.EditValue = "";
            textEditPhone.EditValue = "";
            textEditPayments.EditValue = "0";
            dateEditCheckIn.EditValue = DateTime.Now;
            dateEditEndOfReserved.EditValue = DateTime.Now;
        }
        void initDropDownPrefix()
        {
            lookUpEditPrefix.Properties.DataSource = BusinessLogicBridge.DataStore.getAllPrefix();
            lookUpEditPrefix.Properties.DisplayMember = "prefix_" + MainForm.current_lang + "_label";
            lookUpEditPrefix.Properties.ValueMember = "prefix_id";
            lookUpEditPrefix.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("prefix_" + MainForm.current_lang + "_label", 0, getLanguage("_prefix")));

            lookUpEditPrefix.Properties.NullText = getLanguage("_select_prefix");
        }
        void initRoom()
        {
            DataTable RoomTbl = BusinessLogicBridge.DataStore.RoomReserved_getRoom();
            gridControlRoomLeave.DataSource = RoomTbl;
            
            //
            if (RoomTbl.Rows.Count == 0)
            {
                enable(false);
                bttEdit.Enabled = false;
            }
            //
        }
        void initReserved()
        {
            DataTable RoomReservedTbl = BusinessLogicBridge.DataStore.RoomReserved_get();
            gridControlReserved.DataSource = RoomReservedTbl;

            if (RoomReservedTbl.Rows.Count == 0)
            {
                bttEdit.Enabled = false;
            }
        }
        #endregion

        #region Action Extra
        void enable(Boolean status)
        {
            lookUpEditPrefix.Enabled = status;
            textEditName.Enabled = status;
            textEditLastName.Enabled = status;
            textEditPhone.Enabled = status;
            textEditPayments.Enabled = status;
            dateEditCheckIn.Enabled = status;
            dateEditEndOfReserved.Enabled = status;
            bttCancel.Enabled = status;
            bttSave.Enabled = status;
            bttEdit.Enabled = !status;
        }
        void changeRow()
        {
            int[] rowIndex = gvControlRoomLeave.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                button_event = "Add";

                DataRow CurrentRow = gvControlRoomLeave.GetDataRow(rowIndex[0]);
                
                if (CurrentRow == null)
                {
                    CurrentRow = gvControlRoomLeave.GetDataRow(0);
                }

                textEditReserveId.EditValue = "";
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditCoderef.EditValue = CurrentRow["coderef"].ToString();
                textEditRoomStatus.EditValue = CurrentRow["room_status"].ToString();
                textEditRoomName.EditValue = CurrentRow["room_label"].ToString();
                textEditBuilding.EditValue = CurrentRow["building_label"].ToString();
                textEditFloor.EditValue = CurrentRow["floor_code"].ToString();
                textEditRoomType.EditValue = CurrentRow["roomtype_label"].ToString();
                //
                textEditMonthlyRate.Text = CurrentRow["roomtype_month_roomrate_price"].ToString();
                textEditInsurance.Text = CurrentRow["roomtype_month_insure_price"].ToString();
                textEditAdvance.Text = CurrentRow["roomtype_month_advance_amount"].ToString();
                textEditDailyRate.Text = CurrentRow["roomtype_daily_roomrate_price"].ToString();
                //
                textEditTenantId.EditValue = "0";
                lookUpEditPrefix.EditValue = null;
                textEditName.EditValue = "";
                textEditLastName.EditValue = "";
                textEditPhone.EditValue = "";
                textEditPayments.EditValue = "0.00";
                dateEditCheckIn.EditValue = DateTime.Now;
                dateEditEndOfReserved.EditValue = DateTime.Now;
                //


                if (CurrentRow["leave_date"].ToString() != "")
                {
                    dateEditCheckOut.EditValue = DateTime.Parse(CurrentRow["leave_date"].ToString());
                }
                else
                {
                    dateEditCheckOut.EditValue = "";
                }
            }
            else
            {
                initDataDefault();
            }
        }
        private DataTable ValidateData()
        {
            DataTable _Validate = new DataTable();
            //_Validate.Columns.Add("object", typeof(Object));
            _Validate.Columns.Add("label", typeof(String));
            _Validate.Columns.Add("message", typeof(String));


            if (lookUpEditPrefix.EditValue ==null)
            {
                _Validate.Rows.Add(labelControlTitle.Text, getLanguage("_msg_1001"));
            }

            if (textEditName.EditValue.ToString().Length < 1)
            {
                _Validate.Rows.Add(labelControlName.Text, getLanguage("_msg_1001"));
            }
            if (textEditLastName.EditValue.ToString().Length < 1)
            {
                _Validate.Rows.Add(labelControlLastName.Text, getLanguage("_msg_1001"));
            }
            
            if (textEditPayments.EditValue.To<double>() <= 0)
            {
                _Validate.Rows.Add(labelControlPayments.Text, getLanguage("_msg_1001"));
            }
            DateTime checkIn = DateTime.Parse(dateEditCheckIn.EditValue.ToString());
            if (dateEditCheckOut.EditValue.ToString() != ""){

                int result = DateTime.Compare(dateEditCheckOut.EditValue.To<DateTime>().Date, checkIn.Date);
                // Check Out Date > Check in Date
                if (result >= 0)
                {
                    string checkoutdate_msg = getLanguage("_msg_1014");

                    checkoutdate_msg = checkoutdate_msg.Replace("[Require check out date]", dateEditCheckOut.EditValue.To<DateTime>().ToString("dd/MM/yyyy"));

                    _Validate.Rows.Add(labelControlCheckIn.Text, checkoutdate_msg);

                }
            }
            return _Validate;
        }
        #endregion

        #region Event Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
            enable(false);

            button_event = "Add";
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            //
            bttCancelReserve.Enabled = false;
            bttPay.Enabled = false;
            bttPrintContract.Enabled = false;
        }

        private void gvControlReserved_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rowIndex = gvControlReserved.GetSelectedRows();
            if (rowIndex.Length <= 0)
                return;

            //
            bool isPay = false;
            //
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gvControlReserved.GetDataRow(rowIndex[0]);
                textEditRoomStatus.EditValue = CurrentRow["room_status"].ToString();
                textEditReserveId.EditValue = CurrentRow["reserve_id"].ToString();
                textEditTenantId.EditValue = CurrentRow["tenant_id"].ToString();
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();

                DataTable LeaveInfo = BusinessLogicBridge.DataStore.getLeaveDateByRoomId(textEditRoomId.EditValue.To<int>());

                if (LeaveInfo.Rows.Count > 0)
                    dateEditCheckOut.EditValue = LeaveInfo.Rows[0]["leave_date"].To<DateTime>();
                else
                    dateEditCheckOut.EditValue = "";

                textEditCoderef.EditValue = CurrentRow["coderef"].ToString();
                textEditRoomName.EditValue = CurrentRow["room_label"].ToString();
                //
                lookUpEditPrefix.EditValue = CurrentRow["tenant_prefix_id"];
                textEditName.EditValue = CurrentRow["tenant_name"].ToString();
                textEditLastName.EditValue = CurrentRow["tenant_surname"].ToString();
                textEditPhone.EditValue = CurrentRow["tenant_phone"].ToString();
                textEditPayments.EditValue = double.Parse(CurrentRow["reserve_payments"].ToString()).ToString("N2");
                dateEditCheckIn.EditValue = DateTime.Parse(CurrentRow["reserve_check_in_date"].ToString());
                dateEditEndOfReserved.EditValue = DateTime.Parse(CurrentRow["reserve_end_date"].ToString());
                //
                isPay = CurrentRow["reserve_flag_id"].ToString() == "2";
            }
            enable(false);
            //
            button_event = "Edit";
            //
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            //
            bttCancelReserve.Enabled = true;
            bttPay.Enabled = isPay == false;
            if (isPay==true)
                bttPrintContract.Enabled = true;
            else
                bttPrintContract.Enabled = false;
        }
        #endregion

        #region Event Change Value
        private void textEdit_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
        }
        #endregion

        #region Button Event
        private void bttEdit_Click(object sender, EventArgs e)
        {
            enable(true);
            //
            action_key = 0;
            bttEdit.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            //
            bttPrintContract.Enabled = false;
            bttPay.Enabled = false;
            bttCancelReserve.Enabled = false;
            //
            gridControlRoomLeave.Enabled = false;
            gridControlReserved.Enabled = false;
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _Validate = ValidateData();
                if (_Validate.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _Validate.Rows.Count; i++)
                    {
                        message = message + _Validate.Rows[i]["label"] + " " + _Validate.Rows[i]["message"].ToString() + "\r\n";
                    }
                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }

                    DateTime date_create = new DateTime();
                    date_create = DateTime.Today;
                    //
                    int reserve_id = 0;
                    if (textEditReserveId.EditValue.ToString() != "")
                    {
                        reserve_id = int.Parse(textEditReserveId.EditValue.ToString());
                    }
                    int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                    int tenant_id = int.Parse(textEditTenantId.EditValue.ToString());
                    int room_status = int.Parse(textEditRoomStatus.EditValue.ToString());
                    //
                    int tenant_prefix_id = int.Parse(lookUpEditPrefix.EditValue.ToString());
                    String tenant_name = textEditName.EditValue.ToString();
                    String tenant_lastname = textEditLastName.EditValue.ToString();
                    String tenant_phone = textEditPhone.EditValue.ToString();
                    //
                    DateTime date_modified = DateTime.Today;
                    DataTable _Tenant = new DataTable();
                    _Tenant.Columns.Add("tenant_id", typeof(int));
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
                    _Tenant.Columns.Add("tenant_status", typeof(String));
                    _Tenant.Columns.Add("tenant_created_date", typeof(DateTime));
                    _Tenant.Columns.Add("tenant_modified_date", typeof(DateTime));
                    _Tenant.Rows.Add(
                        tenant_id,
                        tenant_prefix_id,
                        "",
                        tenant_name,
                        tenant_lastname,
                        DateTime.Parse(date_modified.ToString()),
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        tenant_phone,
                        "",
                        "",
                        1,
                        DateTime.Parse(date_modified.ToString()),
                        DateTime.Parse(date_modified.ToString())
                    );

                    //
                    Decimal reserve_payments = Decimal.Parse(textEditPayments.EditValue.ToString());
                    DateTime reserve_create_date = DateTime.Today;
                    DateTime reserve_check_in_date = DateTime.Parse(dateEditCheckIn.EditValue.ToString());
                    DateTime reserve_end_date = DateTime.Parse(dateEditEndOfReserved.EditValue.ToString());

                    DataTable DocumentConfigTable = BusinessLogicBridge.DataStore.RoomCheckIn_getDocumentConfig();
                    if (DocumentConfigTable.Rows.Count > 0)
                    {
                        prefix = DocumentConfigTable.Rows[0]["doc_contact_prefix"].ToString();
                        doc_id = DocumentConfigTable.Rows[0]["doc_config_id"].To<int>();
                        contract_start = DocumentConfigTable.Rows[0]["doc_contact_start"].To<int>();
                    }

                    bool flagContractChangeStatus = BusinessLogicBridge.DataStore.CheckContractPrefixChanged(doc_id);
                    if (flagContractChangeStatus == true)
                        genContractNo = "B" + prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.Booking_genId().ToString().PadLeft(6, '0');
                    else
                        genContractNo = "B" + prefix + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.Booking_genId().ToString().PadLeft(6, '0');

                    DataTable _reserveTable = new DataTable();
                    _reserveTable.Columns.Add("reserve_id", typeof(int));
                    _reserveTable.Columns.Add("room_id", typeof(int));
                    _reserveTable.Columns.Add("tenant_id", typeof(int));
                    _reserveTable.Columns.Add("reserve_payments", typeof(Decimal));
                    _reserveTable.Columns.Add("reserve_create_date", typeof(DateTime));
                    _reserveTable.Columns.Add("reserve_check_in_date", typeof(DateTime));
                    _reserveTable.Columns.Add("reserve_end_date", typeof(DateTime));
                    _reserveTable.Columns.Add("reserve_number", typeof(string));

                    _reserveTable.Rows.Add(reserve_id, room_id, tenant_id, reserve_payments, reserve_create_date, reserve_check_in_date, reserve_end_date, genContractNo);

                    switch (button_event)
                    {
                        case "Add":
                            tenant_id = BusinessLogicBridge.DataStore.Tenant_insert(_Tenant);
                            textEditTenantId.EditValue = tenant_id;
                            //
                            _reserveTable.Rows[0]["tenant_id"] = tenant_id;
                            //
                            int reserved_id = BusinessLogicBridge.DataStore.RoomReserved_insert(_reserveTable);
                            if (room_status == 1)
                            {
                                BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 3);
                            }
                            else if (room_status == 4)
                            {
                                BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 5);
                            }
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");
                            initRoom();
                            initReserved();
                            //
                            break;
                        case "Edit":
                            BusinessLogicBridge.DataStore.Tenant_update(_Tenant);
                            //
                            Boolean result = BusinessLogicBridge.DataStore.RoomReserved_update(_reserveTable);
                            if (room_status == 1)
                            {
                                BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 3);
                            }
                            else if (room_status == 4)
                            {
                                BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 5);
                            }
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                            initRoom();
                            initReserved();
                            break;
                    }
                    initRoom();
                    initReserved();
                    changeRow();
                    enable(false);
                    gridControlRoomLeave.Enabled = true;
                    gridControlReserved.Enabled = true;
                    //
                    action_key = 0;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                enable(false);
                //
                gridControlRoomLeave.Enabled = true;
                gridControlReserved.Enabled = true;
                //
                button_event = "";
                action_key = 0;
                bttEdit.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
            }
        }

        void bttCancelReserve_Click(object sender, EventArgs e)
        {
                int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                int room_status = int.Parse(textEditRoomStatus.EditValue.ToString());
                int reserved_id = int.Parse(textEditReserveId.EditValue.ToString());
                int tenant_id = int.Parse(textEditTenantId.EditValue.ToString());
                //
                BusinessLogicBridge.DataStore.Tenant_updateStatus(tenant_id, 2); // Cancel Booking
                Boolean update = BusinessLogicBridge.DataStore.RoomReserved_update(reserved_id, 0);
                if (update == true)
                {
                    if (room_status == 5)
                    {
                        BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 4);
                    }
                    else if (room_status == 3)
                    {
                        BusinessLogicBridge.DataStore.RoomReserved_updateRoom(room_id, 1);
                    }
                    initRoom();
                    initReserved();
                }
                //
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3010"), getLanguage("_softwarename"),"info");
                //
                changeRow();
                enable(false);
                //
                button_event = "";
                action_key = 0;
                //
                gridControlRoomLeave.Enabled = true;
                gridControlReserved.Enabled = true;
                //
                bttPrintContract.Enabled = false;
                bttPay.Enabled = false;
                bttCancelReserve.Enabled = false;
            
        }

        void bttPrintContract_Click(object sender, EventArgs e)
        {
            //print

            PrintDocuments.booking PrintBooking = new DXWindowsApplication2.PrintDocuments.booking();
            
            int reserve_id = int.Parse(textEditReserveId.EditValue.ToString());

            //reserve_id
            PrintBooking.loopGenDataRow(reserve_id);
            PrintBooking.ShowPreview();
            initRoom();            
        }

        void processCalcReciept() {
            string RecieptNO = "";
            DataTable DTReciept = new DataTable();
            //DataTable ItemTable = new DataTable();

            #region declare parameter
            DataTable DTDocInfo = new DataTable();
            DataTable DTInvoice = new DataTable();
            DataTable DTRecieptInfo = new DataTable();

            int recieptID = 0;
            string ThaiBaht = "";
            #endregion

            #region DeClare Column Type of Data Table DTReciept
            DTReciept.Columns.Add("rec_trans_number", typeof(string));
            DTReciept.Columns.Add("rec_trans_cutduedate", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_fixpaymentdate", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_datecreated", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_status", typeof(int));
            DTReciept.Columns.Add("rec_trans_tenantname", typeof(string));
            DTReciept.Columns.Add("rec_trans_tenantaddress", typeof(string));
            DTReciept.Columns.Add("rec_trans_roomlabel", typeof(string));
            DTReciept.Columns.Add("rec_trans_building", typeof(string));
            DTReciept.Columns.Add("rec_trans_floor", typeof(string));
            DTReciept.Columns.Add("rec_trans_roomtype", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_previous_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_emeter_previous_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_present_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_emeter_present_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_emeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_emeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_emeter_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_previous_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_wmeter_previous_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_wmeter_present_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_wmeter_present_energy", typeof(string));
            DTReciept.Columns.Add("rec_trans_wmeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_wmeter_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_phone_start_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_phone_end_date", typeof(DateTime));
            DTReciept.Columns.Add("rec_trans_phonemeter_unit", typeof(double));
            DTReciept.Columns.Add("rec_trans_phonemeter_priceperunit", typeof(double));
            DTReciept.Columns.Add("rec_trans_phone_price", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice_withvat", typeof(double));
            DTReciept.Columns.Add("rec_trans_sumprice_net", typeof(double));
            DTReciept.Columns.Add("rec_trans_print_status", typeof(int));
            DTReciept.Columns.Add("rec_trans_room_id", typeof(int));
            DTReciept.Columns.Add("rec_trans_company_name", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_logofile", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_address", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_telephone", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_fax", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_email", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_website", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_tax_id", typeof(string));
            DTReciept.Columns.Add("rec_trans_company_vision", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_header_invoice", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_footer_invoice", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_under_invoice1", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_under_invoice2", typeof(string));
            DTReciept.Columns.Add("rec_trans_doc_dateformat", typeof(int));
            DTReciept.Columns.Add("rec_trans_doc_logo_position", typeof(int));
            DTReciept.Columns.Add("rec_trans_roomprice", typeof(double));
            DTReciept.Columns.Add("rec_trans_vattype", typeof(int));
            DTReciept.Columns.Add("inv_trans_number", typeof(string));
            DTReciept.Columns.Add("rec_trans_money_text", typeof(string));
            DTReciept.Columns.Add("check_in_id", typeof(int));
            DTReciept.Columns.Add("check_in_contracttype", typeof(int));
            DTReciept.Columns.Add("rec_trans_type", typeof(int));
            DTReciept.Columns.Add("rec_trans_category", typeof(int));
            DTReciept.Columns.Add("rec_trans_booking", typeof(int)); 

            //ItemTable.Columns.Add("rec_trans_id", typeof(int));
            //ItemTable.Columns.Add("item_id", typeof(int));
            //ItemTable.Columns.Add("item_name", typeof(string));
            //ItemTable.Columns.Add("item_price_monthly", typeof(double));
            //ItemTable.Columns.Add("item_price_daily", typeof(double));
            //ItemTable.Columns.Add("item_vat", typeof(string));
            //ItemTable.Columns.Add("item_type", typeof(int));
            //ItemTable.Columns.Add("item_priceperunit", typeof(double));
            //ItemTable.Columns.Add("item_amount", typeof(int));
            //ItemTable.Columns.Add("item_sumprice", typeof(double));
            //ItemTable.Columns.Add("item_vatprice", typeof(double));
            //ItemTable.Columns.Add("item_netprice", typeof(double));
            //ItemTable.Columns.Add("item_flag", typeof(string));

            #endregion

            DTDocInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(textEditBuilding.Text);

            if (DTDocInfo.Rows[0]["doc_saperate_reciept"].ToString() != "0")
            {
                RecieptNO = DTDocInfo.Rows[0]["doc_reciept_prefix"].ToString() + "M" + DateTime.Now.ToString("yyyyMMdd") + BusinessLogicBridge.DataStore.genRecieptNo().ToString().PadLeft(6, '0');
            }

            // Insurance // Booking // Advance  // Room Price
            //double totalprice = textEditInsurance.EditValue.To<double>() + (textEditAdvance.EditValue.To<int>() * textEditMonthlyRate.EditValue.To<double>()) - textEditPayments.EditValue.To<double>();
            double totalprice = textEditPayments.EditValue.To<double>();

            ThaiBaht = MainForm.ThaiBaht(totalprice.ToString());

            DTReciept.Rows.Add(
                   RecieptNO,
                   System.DBNull.Value,
                   System.DBNull.Value,
                   DateTime.Now,
                   1,
                   lookUpEditPrefix.Text + "||" + textEditName.EditValue.ToString() + "||" + textEditLastName.EditValue.ToString(),
                   "-",
                   textEditRoomName.EditValue.ToString(),
                   textEditBuilding.Text,
                   textEditFloor.Text,
                   textEditRoomType.Text,
                   DateTime.Now,
                   0,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   0,
                   DateTime.Now,
                   0,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   0,
                   DateTime.Now,
                   DateTime.Now,
                   0,
                   0,
                   0,
                   totalprice,
                   0,
                   totalprice,
                   0,
                   textEditRoomId.EditValue.To<int>(),
                   DTDocInfo.Rows[0]["company_name"],
                   DTDocInfo.Rows[0]["company_logo"],
                   DTDocInfo.Rows[0]["company_address"],
                   DTDocInfo.Rows[0]["company_telephone"],
                   DTDocInfo.Rows[0]["company_fax"],
                   DTDocInfo.Rows[0]["company_email"],
                   DTDocInfo.Rows[0]["company_website"],
                   DTDocInfo.Rows[0]["company_tax_id"],
                   DTDocInfo.Rows[0]["company_vision"],
                   DTDocInfo.Rows[0]["doc_header_reciept"],
                   DTDocInfo.Rows[0]["doc_footer_reciept"],
                   DTDocInfo.Rows[0]["doc_under_reciept1"],
                   DTDocInfo.Rows[0]["doc_under_reciept2"],
                   DTDocInfo.Rows[0]["doc_dateformat"],
                   DTDocInfo.Rows[0]["doc_logo_position"],
                   textEditPayments.EditValue.To<double>(),
                   DTDocInfo.Rows[0]["doc_vat_type"],
                   "-",
                   ThaiBaht,
                   0,
                   3,
                   0,
                   1,
                   totalprice
               );

            // Keep all to print reciept
            // loop insert to Reciept Item Table
            recieptID = BusinessLogicBridge.DataStore.createRecieptTransaction(DTReciept);

            textEditRecieptID.EditValue = recieptID;

        }

        void bttPay_Click(object sender, EventArgs e)
        {
            int reserve_id = 0;
            reserve_id = int.Parse(textEditReserveId.EditValue.ToString());
            //
            Boolean result = BusinessLogicBridge.DataStore.RoomReserved_update(reserve_id, 2);

            processCalcReciept();

            //
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4008"), getLanguage("_softwarename")) == DialogResult.Yes)
            {

                PrintDocuments.reciept_booking PrintContract = new DXWindowsApplication2.PrintDocuments.reciept_booking();

                PrintContract.loopGenDataRow(textEditRecieptID.EditValue.To<int>());

                PrintContract.ShowPreview();

                bttPay.Enabled = false;
            }
            else {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3009"), getLanguage("_softwarename"), "info");
            }
            //
            BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Room Management [Booking]");
            initReserved();
        }
        #endregion

    }
}
