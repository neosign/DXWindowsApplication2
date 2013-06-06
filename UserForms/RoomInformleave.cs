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
    public partial class RoomInformleave : uBase
    {
        private string button_event = "";
        private int action_key = 0;
        //
        public int selectRoomID = 0;
        public int selectLeaveID = 0;

        public RoomInformleave()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(RoomInformleave_Load);
            //
            this.gridViewRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            gridViewRoom.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView1_RowClick);

            gridViewLeave.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewLeave_FocusedRowChanged);
            this.gridViewLeave.RowClick +=new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView2_RowClick);
            
            gridViewLeave.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridViewLeave_RowCellStyle);
            //
            this.bttEdit.Click += new EventHandler(bttEdit_Click);
            this.bttCancel.Click += new EventHandler(bttCancel_Click);
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttCancelLeave.Click += new EventHandler(bttCancelLeave_Click);
            //
            dateEditCreate.EditValueChanged += new EventHandler(dateEditCreate_EditValueChanged);
            dateEditLeaveDate.EditValueChanged += new EventHandler(dateEditLeaveDate_EditValueChanged);
            textEditLeaveName.EditValueChanged += new EventHandler(textEditLeaveName_EditValueChanged);
            //
            lookUpEditBuildingId.EditValueChanged += new EventHandler(lookUpEditBuildingId_EditValueChanged);

            SaveClick +=new EventHandler(bttSave_Click);

            //dateEditCreate.Properties.MinValue = DateTime.Now;
            //dateEditLeaveDate.Properties.MinValue = DateTime.Now;

        }

        void gridViewLeave_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRowRoom();
        }

        void gridViewLeave_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("leave_date"))
                return;


            if (gridViewLeave.GetRowCellValue(e.RowHandle, "leave_date").ToString() != "")
            {

                //Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "leave_date_real")

                int result = DateTime.Compare(gridViewLeave.GetRowCellValue(e.RowHandle, "leave_date").To<DateTime>().Date, DateTime.Now.Date);

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

        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            changeRowRoom();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
            //
            initRoom();
            initRoomLeave();
            //
            if (selectRoomID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlRoom.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gridViewRoom.FocusedRowHandle = c.LocateByValue("room_id", selectRoomID);
                //
                selectRoomID = 0;
            }

            if (selectLeaveID != 0)
            {
                DevExpress.XtraGrid.Views.Base.ColumnView c = gridControlLeave.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
                //
                gridViewLeave.FocusedRowHandle = c.LocateByValue("room_id", selectLeaveID);
                //
                selectLeaveID = 0;
            
            }
            //
            changeRowLeave();
        }

        void lookUpEditBuildingId_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit row = (LookUpEdit)sender;
            if (row.EditValue != null && row.EditValue.ToString() !="")
            {
                int building_id = Convert.ToInt32(row.EditValue.ToString());
                initDropDownFloor(building_id);
            }
        }

        void RoomInformleave_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            initRoom();
            initRoomLeave();
            initDropDownBuilding();
            initDropDownFloor(1);
            initDropDownRoomType();
            //
            setLangThis();
        }

        public void setLangThis()
        {//
            this.Name = getLanguage("_tenant_info");
            //
            this.groupControlList.Text = getLanguage("_rent_room_list");
            this.groupControlLeave.Text = getLanguage("_menu_room_management_inform_leave");
            this.groupControlLeaveList.Text = getLanguage("_leave_list");
            this.groupRoomInfo.Text = getLanguage("_room");
            //
            this.grid_leave_room_label.Caption = getLanguage("_room_name");
            this.grid_leave_tenant_name.Caption = getLanguage("_firstname");
            this.grid_leave_tenant_surname.Caption = getLanguage("_lastname");
            this.grid_leave_date_created.Caption = getLanguage("_inform_date");
            this.grid_leave_date.Caption = getLanguage("_require_check_out_date");
            this.grid_leave_inform_name.Caption = getLanguage("_inform_name");
            this.grid_leave_status.Caption = getLanguage("_status");

            //
            this.labelControlRoomName.Text = getLanguageWithColon("_room_name");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor");
            this.labelControlRoomType.Text = getLanguageWithColon("_room_type");
            //
            this.labelControlDateCreate.Text = getLanguageWithColon("_inform_date");
            this.labelControlDateLeave.Text = getLanguageWithColon("_require_check_out_date");
            this.labelControlLeaveName.Text = getLanguageWithColon("_inform_name");
            //
            this.labelControlTitle.Text = getLanguageWithColon("_prefix");
            this.labelControlName.Text = getLanguageWithColon("_firstname");
            this.labelControlSurname.Text = getLanguageWithColon("_lastname");
            //
            //Grid
            this.grid_room_label.Caption = getLanguage("_room_name");
            this.grid_tenant_name.Caption = getLanguage("_firstname");
            this.grid_tenant_surname.Caption = getLanguage("_lastname");
            this.grid_building_label.Caption = getLanguage("_building");
            this.grid_floor_code.Caption = getLanguage("_floor");
            this.grid_roomtype_label.Caption = getLanguage("_room_type");
            this.grid_room_status_label.Caption = getLanguage("_status");
            //
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            //
            this.bttCancelLeave.Text = getLanguage("_cancel_leave");
        }

        #region Setup
        void initRoom()
        {
            DataTable RoomTable = BusinessLogicBridge.DataStore.RoomInformleave_getRoom();
            gridControlRoom.DataSource = RoomTable;
            //
            if (RoomTable.Rows.Count == 0)
            {
                enable(false);
                bttEdit.Enabled = false;
            }
        }
        void initRoomLeave()
        {
            DataTable LeaveTable = BusinessLogicBridge.DataStore.RoomInformleave_get();
            gridControlLeave.DataSource = LeaveTable;

            if (LeaveTable.Rows.Count == 0)
                bttEdit.Enabled = false;

        }
        void initDropDownRoomType()
        {
            lookUpEditRoomTypeId.Properties.DisplayMember = "roomtype_label";
            lookUpEditRoomTypeId.Properties.ValueMember = "roomtype_id";
            lookUpEditRoomTypeId.Properties.DataSource = BusinessLogicBridge.DataStore.getAllRoomType();
        }
        void initDropDownBuilding()
        {
            DataTable Buildings = BusinessLogicBridge.DataStore.getAllBuilding(1);
            lookUpEditBuildingId.Properties.DataSource = Buildings;
            lookUpEditBuildingId.Properties.DisplayMember = "building_label";
            lookUpEditBuildingId.Properties.ValueMember = "building_id";
            lookUpEditBuildingId.Properties.NullText = getLanguage("_select_building");
        }
        void initDropDownFloor(int building_id)
        {
            DataTable Floor = BusinessLogicBridge.DataStore.getAllFloorByBuilding(building_id);
            lookUpEditFloorId.Properties.DataSource = Floor;
            lookUpEditFloorId.Properties.DisplayMember = "floor_code";
            lookUpEditFloorId.Properties.ValueMember = "floor_id";
            lookUpEditFloorId.Properties.NullText = getLanguage("_select_floor");
        }
        #endregion

        #region Action Extra
        void clearData()
        {
            textEditLeaveName.EditValue = "";
            dateEditCreate.EditValue = DateTime.Now;
            dateEditLeaveDate.EditValue = DateTime.Now;
        }
        void enable(Boolean status)
        {
            dateEditCreate.Enabled = status;
            dateEditLeaveDate.Enabled = status;
            textEditLeaveName.Enabled = status;
            gridControlRoom.Enabled = !status;
            gridControlLeave.Enabled = !status;
            //
            bttEdit.Enabled = !status;
        }
        void changeRowRoom()
        {
            int[] rowIndex = gridViewRoom.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewRoom.GetDataRow(0);
                }
                textEditRoomCode.EditValue = CurrentRow["coderef"].ToString();
                textEditTitle.EditValue = CurrentRow["prefix_" + current_lang + "_label"].ToString();
                textEditTenantName.EditValue = CurrentRow["tenant_name"].ToString();
                textEditTenantSurname.EditValue = CurrentRow["tenant_surname"].ToString();
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditRoomName.EditValue = CurrentRow["room_label"].ToString();
                lookUpEditBuildingId.EditValue = CurrentRow["building_id"].ToString();
                lookUpEditFloorId.EditValue = CurrentRow["floor_id"];
                lookUpEditRoomTypeId.EditValue = CurrentRow["roomtype_id"];

                textEditTenantId.EditValue = CurrentRow["current_tenant_id"].ToString();
                textEditRoomStatus.EditValue = CurrentRow["room_status"].ToString();

                if (textEditRoomStatus.EditValue.To<int>() == 5)
                {
                    textEditBookingDate.EditValue = CurrentRow["reserve_check_in_date"];
                }
                else {
                    textEditBookingDate.EditValue = "";
                }
                
                dateEditCreate.EditValue = DateTime.Now;
                dateEditLeaveDate.EditValue = DateTime.Now;
                textEditLeaveName.EditValue = "";
            }
            else
            {
                textEditRoomCode.EditValue = "";
                textEditTenantName.EditValue = "";
                textEditTenantSurname.EditValue = "";
                textEditRoomId.EditValue = "";
                textEditRoomName.EditValue = "";
                textEditTenantId.EditValue = "";
                textEditRoomStatus.EditValue = "";
                textEditTitle.EditValue = "";
                lookUpEditBuildingId.EditValue = "";
                lookUpEditFloorId.EditValue = "";
                lookUpEditRoomTypeId.EditValue = "";
                dateEditCreate.EditValue = DateTime.Now;
                dateEditLeaveDate.EditValue = DateTime.Now;
                textEditLeaveName.EditValue = "";
            }
            //
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
        }
        void changeRowLeave()
        {
            int[] rowIndex = gridViewLeave.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewLeave.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewLeave.GetDataRow(0);
                }
                textEditRoomCode.EditValue = CurrentRow["coderef"].ToString();

                textEditTitle.EditValue = CurrentRow["prefix_" + current_lang + "_label"].ToString();
                textEditTenantName.EditValue = CurrentRow["tenant_name"].ToString();
                textEditTenantSurname.EditValue = CurrentRow["tenant_surname"].ToString();
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditRoomName.EditValue = CurrentRow["room_label"].ToString();
                textEditTenantId.EditValue = CurrentRow["tenant_id"].ToString();
                //
                lookUpEditBuildingId.EditValue = CurrentRow["building_id"].To<int>();
                lookUpEditFloorId.EditValue = CurrentRow["floor_id"].To<int>();
                lookUpEditRoomTypeId.EditValue = CurrentRow["roomtype_id"].To<int>();
                //

                textEditRoomStatus.EditValue = CurrentRow["room_status"].To<int>();

                if (textEditRoomStatus.EditValue.To<int>() == 5)
                {
                    DataTable ReservedInfo = BusinessLogicBridge.DataStore.getReserveByRoomID(textEditRoomId.EditValue.To<int>());

                    textEditBookingDate.EditValue = ReservedInfo.Rows[0]["reserve_check_in_date"];
                }
                else
                {
                    textEditBookingDate.EditValue = "";
                }

                textEditLeaveName.EditValue = CurrentRow["leave_name"].ToString();
                dateEditCreate.EditValue = DateTime.Parse(CurrentRow["leave_date_created"].ToString());
                dateEditLeaveDate.EditValue = DateTime.Parse(CurrentRow["leave_date"].ToString());
            }
        }
        private DataTable validateDate()
        {
            string label = "";
            string message = "";
            DataTable _Validate = new DataTable();
            _Validate.Columns.Add("label", typeof(String));
            _Validate.Columns.Add("message", typeof(String));


            if (textEditLeaveName.EditValue.ToString() == "")
            {
                label = labelControlLeaveName.Text;
                message = getLanguage("_msg_1001");
                _Validate.Rows.Add(label, message);
            }

            return _Validate;
        }
        #endregion

        #region Event Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRowRoom();
            enable(false);

            action_key = 0;
            button_event = "Add";
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            //
            bttCancelLeave.Enabled = false;
        }
        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            changeRowLeave();
            enable(false);

            action_key = 0;
            button_event = "Edit";
            bttEdit.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            //
            bttCancelLeave.Enabled = true;
        }
        #endregion

        #region Event Change Value
        private void dateEditCreate_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
            dateEditLeaveDate.Properties.MinValue = dateEditCreate.EditValue.To<DateTime>();
        }
        private void dateEditLeaveDate_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
        }
        void textEditLeaveName_EditValueChanged(object sender, EventArgs e)
        {
            action_key = 1;
        }
        #endregion

        #region Button Event
        private void bttEdit_Click(object sender, EventArgs e)
        {

            if (textEditLeaveName.EditValue.ToString() == "")
            {
                enable(true);

                action_key = 0;
                //bttEdit.Enabled = false;
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
                bttCancelLeave.Enabled = false;
                button_event = "Add";
            }
            else
            {
                enable(true);

                action_key = 0;
//                bttEdit.Enabled = false;
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
                bttCancelLeave.Enabled = false;
                button_event = "Edit";
            }
        }
        private void bttCancelLeave_Click(object sender, EventArgs e)
        {
            String message;
            int room_status = int.Parse(textEditRoomStatus.EditValue.ToString());
            if (room_status == 5)
            {
                message = getLanguage("_msg_1012");
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }
            else
            {
                int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                int tenant_id = int.Parse(textEditTenantId.EditValue.ToString());
                BusinessLogicBridge.DataStore.RoomInformleave_cancel(room_id, tenant_id);
                BusinessLogicBridge.DataStore.RoomInformleave_updateRoom(room_id, 2);
                BusinessLogicBridge.DataStore.RoomInformleave_updateTenant(tenant_id, 3);
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3014"), getLanguage("_softwarename"), "info");
                
                initRoomLeave();
                changeRowLeave();
                initRoom();
                changeRowRoom();
                
                clearData();
                //
                enable(false);
                changeRowRoom();                
            }
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
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
            
                DateTime date_create = new DateTime();
                date_create = DateTime.Today;
                int tenant_id = int.Parse(textEditTenantId.EditValue.ToString());
                int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                int leave_status = 1;
                string leave_name = textEditLeaveName.Text.Trim();
                DateTime leave_date = Convert.ToDateTime(dateEditLeaveDate.EditValue.ToString());
                DateTime leave_date_create = date_create;
                DataTable _LeaveTbl = new DataTable();
                _LeaveTbl.Columns.Add("tenant_id", typeof(int));
                _LeaveTbl.Columns.Add("room_id", typeof(int));
                _LeaveTbl.Columns.Add("leave_name", typeof(string));
                _LeaveTbl.Columns.Add("leave_status", typeof(int));
                _LeaveTbl.Columns.Add("leave_date", typeof(DateTime));
                _LeaveTbl.Columns.Add("leave_date_created", typeof(DateTime));
                _LeaveTbl.Rows.Add(tenant_id, room_id, leave_name, leave_status, leave_date, leave_date_create);

                switch (button_event)
                {
                    case "Add":
                        BusinessLogicBridge.DataStore.RoomInformleave_insert(_LeaveTbl);
                        BusinessLogicBridge.DataStore.RoomInformleave_updateRoom(room_id, 4);
                        BusinessLogicBridge.DataStore.RoomInformleave_updateTenant(tenant_id, 5);
                        break;
                    case "Edit":

                        if (textEditBookingDate.EditValue.ToString() != "")
                        {
                            int result = DateTime.Compare(dateEditLeaveDate.EditValue.To<DateTime>(), textEditBookingDate.EditValue.To<DateTime>());

                            if (result > 0)
                            {
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1011"), getLanguage("_softwarename"));
                                TrySaveError = true;
                                dateEditLeaveDate.Focus();
                                return;
                            }
                        }
                        //ลบออกแล้วบันทึกค่าใหม่แทน
                        BusinessLogicBridge.DataStore.RoomInformleave_cancel(room_id, tenant_id);
                        BusinessLogicBridge.DataStore.RoomInformleave_insert(_LeaveTbl);
                        break;
                }
                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Room Management [Inform Leave]");
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");
                
                initRoom();    
                initRoomLeave();
                changeRowRoom();    
                changeRowLeave();                
                enable(false);

        }
        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"),getLanguage("_softwarename")) == DialogResult.Yes)
            {
                initRoomLeave();
                initRoom();
                changeRowLeave();
                changeRowRoom();
                enable(false);
            }
        }
        #endregion
    }
}
