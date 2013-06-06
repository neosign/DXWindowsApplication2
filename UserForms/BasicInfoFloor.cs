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
    public partial class BasicInfoFloor : uBase
    {
        private string button_event = "";

        public BasicInfoFloor()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.Load += new EventHandler(BasicInfoFloor_Load);
            
            SaveClick += new EventHandler(BasicInfoFloor_SaveClick);

        }

        void BasicInfoFloor_Load(object sender, EventArgs e)
        {
            initFloor();
            changeRow();
            setLangThis();

            gridControlFloor.UseEmbeddedNavigator = false;
        }

        public override void Refresh()
        {
            base.Refresh();
            initFloor();
            changeRow();
            setLangThis();

            gridControlFloor.UseEmbeddedNavigator = false;
        }

        void BasicInfoFloor_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        public void setLangThis()
        {   
            // Group Control
            this.groupControlFloorList.Text = getLanguage("_floor_list");
            this.groupControlFloorInfo.Text = getLanguage("_floor_info");

            // grid Control
            this.floor_building_label.Caption = getLanguage("_building_label");
            this.floor_floor_label.Caption = getLanguage("_floor_label");
            this.floor_floor_code.Caption = getLanguage("_floor_id");
            this.floor_room_count.Caption = getLanguage("_floor_amount_room");

            // Field Element
            this.labelControlBuildingLabel.Text = getLanguage("_building_label");
            this.labelControlFloor.Text = getLanguage("_floor_id");
            this.labelControlFloorLabel.Text = getLanguage("_floor_label");
            this.labelControlRoomCount.Text = getLanguage("_floor_amount_room");
            this.labelControlRequired.Text = getLanguage("_required");
            
            // Button Control
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");

        }

        #region Setup
        void initDataDefault()
        {
            textEditBuildingId.EditValue = 0;
            textEditBuildingCode.EditValue = "";
            textEditBuildingLabel.EditValue = "";
            textEditFloorCode.EditValue = "";
            textEditFloorId.EditValue = 0;
            textEditFloorLabel.EditValue = "";
            textEditRoomCount.EditValue = 0;
        }
        void initFloor()
        {
            DataTable FloorTbl = BusinessLogicBridge.DataStore.BasicInfoFloor_get();
            gridControlFloor.DataSource = FloorTbl;
        }
        #endregion

        #region Action Extra
        void changeRow()
        {
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                textEditBuildingId.EditValue = CurrentRow["building_id"].ToString();
                textEditBuildingLabel.EditValue = CurrentRow["building_label"].ToString();
                textEditBuildingCode.EditValue = CurrentRow["building_code"].ToString();
                textEditFloorCode.EditValue = CurrentRow["floor_code"].ToString();
                textEditFloorId.EditValue = CurrentRow["floor_id"].ToString();
                textEditFloorLabel.EditValue = CurrentRow["floor_label"].ToString();
                textEditRoomCount.EditValue = CurrentRow["room_count"].ToString();

                bttEdit.Enabled = true;
            }
            else
            {
                bttEdit.Enabled = false;
                initDataDefault();
            }
        }
        private DataTable validateData()
        {
            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));
            if(textEditBuildingLabel.EditValue == null)
            {
                label = labelControlFloorLabel.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditBuildingLabel.Focus();
                    focus = true;
                }
            } else if (textEditFloorLabel.EditValue.ToString().Length < 1)
            {
                label = labelControlFloorLabel.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditBuildingLabel.Focus();
                    focus = true;
                }
            }
            if(textEditRoomCount.EditValue == null)
            {
                label = labelControlRoomCount.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditRoomCount.Focus();
                    focus = true;
                }
            }
            else if (textEditRoomCount.EditValue.To<int>() < 0)
            {
                label = labelControlRoomCount.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditRoomCount.Focus();
                    focus = true;
                }
            }
            else if (textEditRoomCount.EditValue.To<int>() > 99)
            {
                label = labelControlRoomCount.Text;
                message = getLanguage("_floor_room_0_999"); 
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditRoomCount.Focus();
                    focus = true;
                }
            }
            int floor_id = textEditFloorId.Text.To<int>();
            int tenant_count = BusinessLogicBridge.DataStore.BasicInfoFloor_getCountTenantByFloor(floor_id);
            int room_count = textEditRoomCount.EditValue.To<int>();
            int room_count_real = BusinessLogicBridge.DataStore.BasicInfoFloor_getCountRoom(floor_id);

            if (tenant_count > 0)
            {
                if (room_count_real > room_count)
                {
                    label = "";
                    message = getLanguage("_floor_have_tenent");
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditRoomCount.Focus();
                        focus = true;
                    }
                }
            }
            return _ValidateTable;
        }
        #endregion

        #region Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion

        #region Button Event
        private void bttEdit_Click(object sender, EventArgs e)
        {
            textEditFloorLabel.Enabled = true;
            textEditRoomCount.Enabled = true;

            button_event = "edit";
            bttEdit.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            gridControlFloor.Enabled = false;
        }
        #endregion
        
        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {   

                int[] rowIndex;
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
                int floor_id = Convert.ToInt32(textEditFloorId.Text.ToString());


                int status_usage = BusinessLogicBridge.DataStore.floorIdUsed(floor_id);


                int building_id = textEditBuildingId.EditValue.To<int>();
                string floor_label = textEditFloorLabel.EditValue.ToString();
                int tenant_count = BusinessLogicBridge.DataStore.BasicInfoFloor_getCountTenantByFloor(floor_id);
                int room_count = textEditRoomCount.EditValue.To<int>();
                int room_count_real = BusinessLogicBridge.DataStore.BasicInfoFloor_getCountRoom(floor_id);

                DataTable FloorTable = new DataTable();
                FloorTable.Columns.Add("floor_id", typeof(String));
                FloorTable.Columns.Add("floor_label", typeof(String));
                FloorTable.Columns.Add("building_id", typeof(String));
                FloorTable.Rows.Add(floor_id, floor_label, building_id);

                if (status_usage == 1)
                {
                    if (room_count_real > room_count)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1006"), getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }
                    else if (room_count_real < room_count)
                    {
                        // Case old Room less than new room
                        int remain = room_count - room_count_real;
                        string building_code = textEditBuildingCode.EditValue.ToString();
                        string floor_code = textEditFloorCode.EditValue.ToString();
                        string room_label = "";
                        for (int i = 1; i <= remain; i++)
                        {
                            int room_code = room_count_real + i;
                            room_label = building_code + floor_code + room_code.ToString().PadLeft(2, '0');
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

                            _RoomTable.Rows.Add(room_label, 0, floor_id, 0, 0, 0, 0, 0, 0, "", 1, room_code.ToString().PadLeft(2, '0'), 0, 0);
                            BusinessLogicBridge.DataStore.BasicInfoFloor_insertRoom(_RoomTable);
                            _RoomTable.Dispose();
                        }
                        BusinessLogicBridge.DataStore.updateFloor(FloorTable);

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"),"info");
                        rowIndex = gridView1.GetSelectedRows();
                        initFloor();
                        gridView1.FocusedRowHandle = rowIndex[0];
                        gridView1.SelectRow(rowIndex[0]);
                    }
                    else
                    {
                        BusinessLogicBridge.DataStore.updateFloor(FloorTable);
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        rowIndex = gridView1.GetSelectedRows();
                        initFloor();
                        gridView1.FocusedRowHandle = rowIndex[0];
                        gridView1.SelectRow(rowIndex[0]);
                    }
                }
                else {

                    if (room_count_real > room_count)
                    {
                        int remove_room_total = room_count_real - room_count;
                        int room_id_remove;
                        DataTable Room = BusinessLogicBridge.DataStore.BasicInfoFloor_getRoomByFloorId(floor_id);
                        for (int i = 0; i < remove_room_total; i++)
                        {
                            room_id_remove = int.Parse(Room.Rows[i]["room_id"].ToString());
                            BusinessLogicBridge.DataStore.BasicInfoRoom_remove(room_id_remove);
                        }
                        BusinessLogicBridge.DataStore.updateFloor(FloorTable);
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        rowIndex = gridView1.GetSelectedRows();
                        initFloor();
                        gridView1.FocusedRowHandle = rowIndex[0];
                        gridView1.SelectRow(rowIndex[0]);

                        Room.Dispose();
                        
                    }
                    else if (room_count_real < room_count)
                    {
                        // Case old Room less than new room
                        int remain = room_count - room_count_real;
                        string building_code = textEditBuildingCode.EditValue.ToString();
                        string floor_code = textEditFloorCode.EditValue.ToString();
                        string room_label = "";

                        int EMeter_ID;
                        int WMeter_ID;
                        int PMeter_ID;

                        for (int i = 1; i <= remain; i++)
                        {
                            int room_code = room_count_real + i;
                            room_label = building_code + floor_code + room_code.ToString().PadLeft(2, '0');
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
                            EMeter_ID = BusinessLogicBridge.DataStore.insertE_Meter("E" + room_label, textEditBuildingId.EditValue.To<int>());
                            WMeter_ID = BusinessLogicBridge.DataStore.insertW_Meter("W" + room_label, textEditBuildingId.EditValue.To<int>());
                            PMeter_ID = BusinessLogicBridge.DataStore.insertP_Meter(floor_code + room_code.ToString().PadLeft(2, '0'), textEditBuildingId.EditValue.To<int>());

                            _RoomTable.Rows.Add(room_label, 0, floor_id, 0, EMeter_ID, WMeter_ID, PMeter_ID, 0, 0, "", 1, room_code.ToString().PadLeft(2, '0'), 0, 0);
                            BusinessLogicBridge.DataStore.BasicInfoFloor_insertRoom(_RoomTable);
                            
                            _RoomTable.Dispose();
                        }
                        BusinessLogicBridge.DataStore.updateFloor(FloorTable);
                        
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        rowIndex = gridView1.GetSelectedRows();
                        initFloor();
                        gridView1.FocusedRowHandle = rowIndex[0];
                        gridView1.SelectRow(rowIndex[0]);
                    }
                
                }

                MForm.loadDashBoard();
                FloorTable.Dispose();

                changeRow();
                textEditFloorLabel.Enabled = false;
                textEditRoomCount.Enabled = false;

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                gridControlFloor.Enabled = true;

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message.ToString());
            }
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {
            
            if (utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"),getLanguage("_softwarename")) == DialogResult.Yes)
            {
                changeRow();
                textEditFloorLabel.Enabled = false;
                textEditRoomCount.Enabled = false;

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                gridControlFloor.Enabled = true;
            }
        }
        #endregion        
    }
}
