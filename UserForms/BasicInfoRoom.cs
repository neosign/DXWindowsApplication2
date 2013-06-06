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
    public partial class BasicInfoRoom : uBase
    {
        private Boolean _CheckRoom = false;
        private string button_event = "";
        private int room_check_count = 0;
        private DataTable BuildingTable;
        private DataTable FloorTable;
        private DataTable RoomTypeTable;

        public BasicInfoRoom()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.Load += new EventHandler(BasicInfoRoom_Load);
            gridViewRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewRoom_FocusedRowChanged);

            this.Resize += new EventHandler(BasicInfoRoom_Resize);

            lookUpEditFloor.EditValueChanged +=new EventHandler(lookUpEditFloor_EditValueChanged);
            SaveClick += new EventHandler(bttSave_Click);
        }

        void BasicInfoRoom_Resize(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = (this.Width * 40) / 100;
        }

        void gridViewRoom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }

        void BasicInfoRoom_Load(object sender, EventArgs e)
        {
             splitContainerControl2.SplitterPosition = (this.Width * 40)/100;
            
             initUpdateRoomByRoomTypeZero();
             initUpdateRoomLabel();
             initDropDownBuilding();
             initDropDownRoomType();
             initRoom();
             setLangThis();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            //setLangThis();
            //
            initUpdateRoomByRoomTypeZero();
            initUpdateRoomLabel();
            initDropDownBuilding();
            initDropDownRoomType();
            initRoom();
            setLangThis();
        }

        public void setLangThis()
        {

            this.groupControlRoomList.Text = getLanguage("_room_list");
            this.groupControlRoomInfo.Text = getLanguage("_room_info");

            this.labelControlBuilding.Text = getLanguageWithColon("_room_building");
            this.labelControlFloor.Text = getLanguageWithColon("_floor_id");
            this.labelControlRoomId.Text = getLanguageWithColon("_room_id");
            this.labelControlRoomLabel.Text = getLanguageWithColon("_room_name");
            this.labelControlRoomTypeLeft.Text = getLanguageWithColon("_room_type");
            this.labelControlRoomTypeRight.Text = getLanguageWithColon("_room_type");
            this.checkEditSelectAll.Text = getLanguage("_selectall");
            this.bttEditRoomType.Text = getLanguage("_select_roomtype");

            this.room_building_code.Caption = getLanguage("_room_building");
            this.room_floor_code.Caption = getLanguage("_floor_id");
            this.room_coderef.Caption = getLanguage("_room_id");
            this.room_room_label.Caption = getLanguage("_room_name");
            this.room_roomtype_label.Caption = getLanguage("_room_type");



            this.bttAdd.Text = getLanguage("_add");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttDelete.Text = getLanguage("_delete");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
        }

        #region Setup

        void initUpdateRoomByRoomTypeZero()
        {
            DataTable RoomTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getRoomByRoomTypeZero();
            if (RoomTable.Rows.Count > 0)
            {
                int room_id;
                int roomtype_id;
                DataTable RoomTypeTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getRoomType();
                if (RoomTypeTable.Rows.Count > 0)
                {
                    roomtype_id = int.Parse(RoomTypeTable.Rows[0]["roomtype_id"].ToString());
                    for (int i = 0; i < RoomTable.Rows.Count; i++)
                    {
                        room_id = int.Parse(RoomTable.Rows[i]["room_id"].ToString());
                        BusinessLogicBridge.DataStore.BasicInfoRoom_update(room_id, roomtype_id);
                    }

                    if (Convert.ToInt32(DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue) == 0)
                    {
                        DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 1;
                    }
                    else
                    {
                        DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 0;
                    }
                }
            }
        }
        void initUpdateRoomLabel()
        {
            DataTable RoomTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getRoomByNoLabel();
            if (RoomTable.Rows.Count > 0)
            {
                int room_id = 0;
                string room_lable = "";
                for (int i = 0; i < RoomTable.Rows.Count; i++)
                {
                    room_id = int.Parse(RoomTable.Rows[i]["room_id"].ToString());
                    room_lable = RoomTable.Rows[i]["coderef"].ToString();
                    //BusinessLogicBridge.DataStore.BasicInfoRoom_updateRoomLabel(room_id, room_lable);
                }
            }
        }
        void initDataDefault()
        {
            //textEditFloorCodeRef.EditValue = "";
            //textEditRoomLabel.EditValue = "";
            //textEditRoomCode.EditValue = "";
            //lookUpEditRoomTypeRight.EditValue = null;
            //lookUpEditBuilding.EditValue = null;
            //lookUpEditFloor.EditValue = null;
            lookUpEditFloor.Properties.NullText = getLanguage("_select_floor");
            lookUpEditRoomTypeLeft.Properties.NullText = getLanguage("_select_roomtype");
            lookUpEditRoomTypeRight.Properties.NullText = getLanguage("_select_roomtype");

            DataTable bluidingTable = (DataTable)(lookUpEditBuilding.Properties.DataSource);

            if (bluidingTable.Rows.Count > 0)
            {
                lookUpEditBuilding.Enabled = true;
                textEditFloorCodeRef.Enabled = false;
                textEditRoomLabel.Enabled = true;
                lookUpEditRoomTypeRight.Enabled = true;

                // Closed
                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;

                // Open
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
            }
            else {
                lookUpEditBuilding.Enabled = false;
                textEditFloorCodeRef.Enabled = false;
                textEditRoomLabel.Enabled = false;
                lookUpEditRoomTypeRight.Enabled = false;

                // Closed
                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;

                // Open
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
            }
        }
        void initRoom()
        {
            DataTable RoomTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getRoom();
            RoomTable.Columns.Add("room_check", typeof(bool));
            if (RoomTable.Rows.Count > 0)
            {
                for (int i = 0; i < RoomTable.Rows.Count; i++)
                {
                    RoomTable.Rows[i]["room_check"] = false;
                }
            }
            else {
                setDisableAll();
            }

            gridControlRoom.DataSource = RoomTable;
            lookUpEditFloor.Enabled = false;
        }
        void initDropDownBuilding()
        {
            BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }
        void initDropDownFloor(int building_id)
        {
            FloorTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getFloorByBuildingId(building_id);
            lookUpEditFloor.Properties.DisplayMember = "floor_code";
            lookUpEditFloor.Properties.ValueMember = "floor_id";
            lookUpEditFloor.Properties.NullText = getLanguage("_select_floor");
            lookUpEditFloor.Properties.DataSource = FloorTable;
        }
        void initDropDownRoomType()
        {
            RoomTypeTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getRoomType();
            lookUpEditRoomTypeLeft.Properties.DisplayMember = "roomtype_label";
            lookUpEditRoomTypeLeft.Properties.ValueMember = "roomtype_id";
            lookUpEditRoomTypeLeft.Properties.NullText = getLanguage("_select_roomtype");
            lookUpEditRoomTypeLeft.Properties.DataSource = RoomTypeTable;

            lookUpEditRoomTypeRight.Properties.DisplayMember = "roomtype_label";
            lookUpEditRoomTypeRight.Properties.ValueMember = "roomtype_id";
            lookUpEditRoomTypeRight.Properties.NullText = getLanguage("_select_roomtype");
            lookUpEditRoomTypeRight.Properties.DataSource = RoomTypeTable;
        }
        void setDisableAll() {

            if (BuildingTable == null || FloorTable == null || RoomTypeTable == null)
            {
                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;
            }
        }
        #endregion

        #region Action Extra
            void clearData()
            {
                textEditFloorCodeRef.EditValue = "";
                textEditRoomLabel.EditValue = "";
                textEditRoomCode.EditValue = "";
                lookUpEditRoomTypeRight.EditValue = null;
                lookUpEditFloor.EditValue = null;
            }
            void eventUnselect()
            {
                lookUpEditRoomTypeLeft.Enabled = false;
                bttEditRoomType.Enabled = false;
                
                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
            }
            void eventSelected()
            {
                lookUpEditRoomTypeLeft.Enabled = true;
                bttEditRoomType.Enabled = true;

                bttAdd.Enabled = false;
                bttEdit.Enabled = false;
                bttDelete.Enabled = false;
            }
            void clearSelectedAll()
            {
                for (int i = 0; i < gridViewRoom.RowCount; i++)
                {
                    gridViewRoom.Columns[0].View.SetRowCellValue(i, "room_check", false);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
            void selectedMulti()
            {
                int[] rowIndex = gridViewRoom.GetSelectedRows();
                if (rowIndex.Length > 1)
                {
                    bttAdd.Enabled = false;
                    bttEdit.Enabled = false;
                    bttDelete.Enabled = false;

                    bttEditRoomType.Enabled = true;
                    lookUpEditRoomTypeLeft.Enabled = true;
                }
                else if (rowIndex.Length == 1)
                {
                    bttAdd.Enabled = true;
                    bttEdit.Enabled = true;
                    bttDelete.Enabled = true;

                    bttEditRoomType.Enabled = false;
                    lookUpEditRoomTypeLeft.Enabled = false;
                }
                else
                {
                    bttAdd.Enabled = false;
                    bttEdit.Enabled = false;
                    bttDelete.Enabled = false;

                    bttEditRoomType.Enabled = false;
                    lookUpEditRoomTypeLeft.Enabled = false;
                }
            }
            void enable(Boolean status)
            {
                textEditRoomLabel.Enabled = status;
                lookUpEditRoomTypeRight.Enabled = status;
            }
            private DataTable validateData()
            {
                string max_50 = getLanguage("_max_50");

                string star_notice = getLanguage("_msg_1001");

                String label = "";
                String message = "";
                Boolean focus = false;
                DataTable _ValidateTable = new DataTable();
                _ValidateTable.Columns.Add("label", typeof(String));
                _ValidateTable.Columns.Add("message", typeof(String));

                if(lookUpEditBuilding.EditValue == null)
                {
                    label = labelControlBuilding.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        lookUpEditBuilding.Focus();
                        focus = true;
                    }
                }
                if (lookUpEditFloor.EditValue == null)
                {
                    label = labelControlFloor.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        lookUpEditFloor.Focus();
                        focus = true;
                    }
                }
                if (textEditRoomLabel.EditValue.ToString() != "")
                {
                    if (validLength(textEditRoomLabel.Text, 50) == false)
                    {
                        label = labelControlRoomLabel.Text;
                        message = max_50;
                        _ValidateTable.Rows.Add(label, message);
                        if (focus == false)
                        {
                            textEditRoomLabel.Focus();
                            focus = true;
                        }
                    }
                    else { 
                        // Check Room name Duplicate

                        DataRow[] listRoomName = ((DataTable)gridControlRoom.DataSource).Select("room_id <> " + textEditRoomId.EditValue.To<int>());

                        for (int i = 0; i < listRoomName.Length; i++)
                        {
                            if (textEditRoomLabel.EditValue.ToString().Trim() == listRoomName[i]["room_label"].ToString().Trim())
                            {
                                label = labelControlRoomLabel.Text;
                                message = getLanguage("_msg_1031");
                                _ValidateTable.Rows.Add(label, message);
                            }
                        }
                    }
                }
                else {

                    label = labelControlRoomLabel.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditRoomLabel.Focus();
                        focus = true;
                    }
                }


                if (lookUpEditRoomTypeRight.EditValue == null)
                {
                    label = labelControlRoomTypeRight.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        lookUpEditRoomTypeRight.Focus();
                        focus = true;
                    }
                }
                return _ValidateTable;
            }
        #endregion

        #region Change Row
        void changeRow()
        {
            int[] rowIndex = gridViewRoom.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridViewRoom.GetDataRow(0);
                }
                int building_id = CurrentRow["building_id"].To<int>();
                initDropDownFloor(building_id);
                textEditRoomId.EditValue = CurrentRow["room_id"].ToString();
                textEditRoomLabel.EditValue = CurrentRow["room_label"].ToString();
                textEditFloorCodeRef.EditValue = CurrentRow["coderef"].ToString();
                textEditRoomCode.EditValue = CurrentRow["room_code"].ToString();
                lookUpEditBuilding.EditValue = building_id;
                lookUpEditFloor.EditValue = CurrentRow["floor_id"].To<int>();
                if (int.Parse(CurrentRow["roomtype_id"].ToString()) == 0)
                {
                    lookUpEditRoomTypeRight.EditValue = null;
                }
                else
                {
                    lookUpEditRoomTypeRight.EditValue = int.Parse(CurrentRow["roomtype_id"].ToString());
                }
                bttAdd.Enabled = true;
                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
            }
            else {

                if (((DataTable)lookUpEditBuilding.Properties.DataSource).Rows.Count > 0 && ((DataTable)lookUpEditFloor.Properties.DataSource).Rows.Count > 0)
                {
                    bttSave.Enabled = true;
                    bttCancel.Enabled = true;
                    enable(true);
                }
                else {
                    bttSave.Enabled = false;
                    bttCancel.Enabled = false;
                    enable(false);
                }
            }
        }
       
        private void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {

            if (button_event == "add")
            {
                initDropDownFloor(lookUpEditBuilding.EditValue.To<int>());
                lookUpEditFloor.Enabled = true;
                lookUpEditFloor.EditValue = null;
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue.ToString() == "False")
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(e.RowHandle);
                gridViewRoom.Columns[0].View.SetRowCellValue(e.RowHandle, "room_check", true);
                gridViewRoom.SelectRow(e.RowHandle);
                room_check_count = room_check_count + 1;
                eventSelected();
            }
            else if (e.CellValue.ToString() == "True")
            {
                DataRow CurrentRow = gridViewRoom.GetDataRow(e.RowHandle);
                gridViewRoom.Columns[0].View.SetRowCellValue(e.RowHandle, "room_check", false);
                gridViewRoom.UnselectRow(e.RowHandle);
                room_check_count = room_check_count - 1;
                if (room_check_count < 1)
                {
                    eventUnselect();
                }
                else
                {
                    eventSelected();
                }
            }
        }
        #endregion

        #region Button Event

        private void bttAdd_Click(object sender, EventArgs e)
        {
            clearData();
            enable(true);
            lookUpEditBuilding.EditValue = null;
            lookUpEditBuilding.Enabled = true;

            checkEditSelectAll.Enabled = false;
            gridControlRoom.Enabled = false;

            button_event = "add";
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            lookUpEditFloor.Enabled = false;
        }
        private void bttEdit_Click(object sender, EventArgs e)
        {
            enable(true);
            lookUpEditBuilding.Enabled = false;
            lookUpEditFloor.Enabled = false;
            checkEditSelectAll.Enabled = false;
            gridControlRoom.Enabled = false;

            button_event = "edit";
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
        }
        private void bttDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                int tenant_count = BusinessLogicBridge.DataStore.BasicInfoRoom_getCountTenantByRoomId(room_id);
                if (tenant_count > 0)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                    gridViewRoom.Focus();
                    return;
                }

                if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4001"),getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    BusinessLogicBridge.DataStore.BasicInfoRoom_remove(room_id);
                    int select_row = 0;
                    int[] rowIndex = gridViewRoom.GetSelectedRows();
                    if (rowIndex[0] > 0)
                    {
                        select_row = rowIndex[0] - 1;
                    }

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"),"info");
                    initRoom();
                    gridViewRoom.FocusedRowHandle = select_row;
                    gridViewRoom.SelectRow(select_row);
                    gridViewRoom.UnselectRow(rowIndex[0]);
                }
                changeRow();
                enable(false);
                lookUpEditBuilding.Enabled = false;
                lookUpEditFloor.Enabled = false;
                checkEditSelectAll.Enabled = true;
                gridControlRoom.Enabled = true;

                button_event = "";
                bttCancel.Enabled = false;
                bttSave.Enabled = false;
                bttAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                int room_id = int.Parse(textEditRoomId.EditValue.ToString());
                string room_label = textEditRoomLabel.EditValue.ToString();
                int roomtype_id = int.Parse(lookUpEditRoomTypeRight.EditValue.ToString());
                int floor_id = int.Parse(lookUpEditFloor.EditValue.ToString());
                string room_code = "01";
                int EMeter_ID;
                int WMeter_ID;
                int PMeter_ID;

                room_code = textEditFloorCodeRef.EditValue.ToString();
                
                if (button_event == "add")
                {
                    DataTable RoomInfo = BusinessLogicBridge.DataStore.getRoomByLabel(textEditRoomLabel.EditValue.ToString().TrimEnd());

                    if (RoomInfo.Rows.Count > 0)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1031"), getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }

                    DataTable _RoomTable = new DataTable();
                    _RoomTable.Columns.Add("room_label", typeof(string));     //1
                    _RoomTable.Columns.Add("roomtype_id", typeof(string));     //2
                    _RoomTable.Columns.Add("floor_id", typeof(string));     //3
                    _RoomTable.Columns.Add("zone_id", typeof(string));     //4
                    _RoomTable.Columns.Add("current_electricity_id", typeof(string));     //5
                    _RoomTable.Columns.Add("current_water_id", typeof(string));     //6
                    _RoomTable.Columns.Add("current_phone_id", typeof(string));     //7
                    _RoomTable.Columns.Add("current_tenant_id", typeof(string));     //8
                    _RoomTable.Columns.Add("current_meter_status", typeof(string));     //9
                    _RoomTable.Columns.Add("room_note", typeof(string));     //10
                    _RoomTable.Columns.Add("room_status", typeof(string));     //11
                    _RoomTable.Columns.Add("room_code", typeof(string));     //12
                    _RoomTable.Columns.Add("invoice_stutus", typeof(string));     //13
                    _RoomTable.Columns.Add("check_in_id", typeof(string));     //14

                    // Insert Default All Meter
                   
                    EMeter_ID = BusinessLogicBridge.DataStore.insertE_Meter("E" + room_code, lookUpEditBuilding.EditValue.To<int>());
                    WMeter_ID = BusinessLogicBridge.DataStore.insertW_Meter("W" + room_code, lookUpEditBuilding.EditValue.To<int>());
                    PMeter_ID = BusinessLogicBridge.DataStore.insertP_Meter(room_code.Substring(1, 3), lookUpEditBuilding.EditValue.To<int>());

                    _RoomTable.Rows.Add(room_label, lookUpEditRoomTypeRight.EditValue.To<int>(), floor_id, 0, EMeter_ID, WMeter_ID, PMeter_ID, 0, 0, "", 1, room_code.Substring(2, 2), 0, 0);
                    
                    BusinessLogicBridge.DataStore.BasicInfoRoom_insert(_RoomTable);

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                    
                    initRoom();
                    int[] rowIndex = gridViewRoom.GetSelectedRows();
                    int select_row = 0;
                    if (gridViewRoom.RowCount > 0)
                    {
                        select_row = gridViewRoom.RowCount - 1;
                    }
                    gridViewRoom.FocusedRowHandle = select_row;
                    gridViewRoom.SelectRow(select_row);
                    gridViewRoom.UnselectRow(rowIndex[0]);
                }
                else {
                    BusinessLogicBridge.DataStore.updateRoomName(textEditRoomLabel.EditValue.ToString(),roomtype_id, room_id);
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                    int[] rowIndex = gridViewRoom.GetSelectedRows();
                    initRoom();
                    gridViewRoom.FocusedRowHandle = rowIndex[0];
                    gridViewRoom.SelectRow(rowIndex[0]);
                }
                
                changeRow();
                enable(false);

                lookUpEditBuilding.Enabled = false;
                lookUpEditFloor.Enabled = false;
                checkEditSelectAll.Enabled = true;
                gridControlRoom.Enabled = true;

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttAdd.Enabled = true;
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
                lookUpEditBuilding.Enabled = false;
                lookUpEditFloor.Enabled = false;
                checkEditSelectAll.Enabled = true;
                gridControlRoom.Enabled = true;

                button_event = "";
                bttAdd.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
            }            
        }

        #endregion

        private void lookUpEditFloor_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditFloor.EditValue != null && button_event=="add" )
            {
                int room_no = BusinessLogicBridge.DataStore.genMaxRoomCode(lookUpEditFloor.EditValue.To<int>());

                string room_namestring = (room_no + 1).ToString().PadLeft(2, '0');

                DataTable buildingInfo = new DataTable();

                buildingInfo = BusinessLogicBridge.DataStore.getBuildingByID(lookUpEditBuilding.EditValue.To<int>());

                textEditFloorCodeRef.EditValue = buildingInfo.Rows[0]["building_code"].ToString() + lookUpEditFloor.Text + room_namestring;

                textEditRoomLabel.EditValue = buildingInfo.Rows[0]["building_code"].ToString() + lookUpEditFloor.Text + room_namestring;

            }
        }
        private bool validLength(string param, int length)
        {

            if (param.Length > length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_CheckRoom == false)
            {
                _CheckRoom = true;
                eventSelected();
            }
            else
            {
                _CheckRoom = false;
                eventUnselect();
            }
            room_check_count = 0;
            if (gridViewRoom.RowCount > 0)
            {
                for (int i = 0; i < gridViewRoom.RowCount; i++)
                {
                    gridViewRoom.Columns[0].View.SetRowCellValue(i, "room_check", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }
        private void bttEditRoomType_Click(object sender, EventArgs e)
        {
            string star_notice = getLanguage("_notice_star");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (lookUpEditRoomTypeLeft.EditValue == null)
            {
                label = labelControlRoomTypeLeft.Text;
                message = getLanguage("_select_roomtype");
                _ValidateTable.Rows.Add(label, message);

                message = _ValidateTable.Rows[0]["label"] + " " + _ValidateTable.Rows[0]["message"].ToString() + "\r\n";
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                if (focus == false)
                {
                    lookUpEditRoomTypeLeft.Focus();
                    focus = true;
                }
            }
            else
            {
                if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4003"),getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    DataTable _RoomTable = (DataTable)gridControlRoom.DataSource;
                    int room_id;
                    int roomtype_id = int.Parse(lookUpEditRoomTypeLeft.EditValue.ToString());
                    Boolean room_check;
                    for (int i = 0; i < _RoomTable.Rows.Count; i++)
                    {
                        room_check = Boolean.Parse(_RoomTable.Rows[i]["room_check"].ToString());
                        if (room_check == true)
                        {
                            room_id = int.Parse(_RoomTable.Rows[i]["room_id"].ToString());
                            BusinessLogicBridge.DataStore.BasicInfoRoom_update(room_id, roomtype_id);
                            _RoomTable.Rows[i]["room_check"] = false;
                        }
                    }
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                    initRoom();

                    changeRow();
                    clearSelectedAll();
                    room_check_count = 0;
                    lookUpEditRoomTypeLeft.Enabled = false;
                    lookUpEditRoomTypeLeft.EditValue = null;
                    checkEditSelectAll.EditValue = false;
                    checkEditSelectAll.Enabled = true;
                    bttEditRoomType.Enabled = false;
                }
            }
        }

    }
}
