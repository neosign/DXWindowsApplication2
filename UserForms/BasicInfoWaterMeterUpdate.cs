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
    public partial class BasicInfoWaterMeterUpdate : DevExpress.XtraEditors.XtraUserControl
    {
      
        public BasicInfoWaterMeterUpdate(int room_id)
        {
            InitializeComponent();


            DataTable roomDetail = BusinessLogicBridge.DataStore.getWaterById(room_id);

            string coderef = roomDetail.Rows[0]["coderef"].ToString();
            string meter_label = roomDetail.Rows[0]["meter_label"].ToString();
            string water_id = roomDetail.Rows[0]["water_id"].ToString();


            textEditMeterId.EditValue = water_id;

            txtmeter_label.Properties.ReadOnly = true;
            txtmeter_label.EditValue = meter_label;


            initDropDownBuilding(Convert.ToInt16(roomDetail.Rows[0]["building_id"]));
            initDropDownFloor(Convert.ToInt16(roomDetail.Rows[0]["building_id"]), Convert.ToInt16(roomDetail.Rows[0]["floor_id"]));
            initDropDownRoom(Convert.ToInt16(roomDetail.Rows[0]["floor_id"]), room_id);

            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);
            lookUpEditFloor.EditValueChanged += new EventHandler(lookUpEditFloor_EditValueChanged);

        }

        private void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(lookUpEditBuilding.EditValue);

            DataTable Floor = BusinessLogicBridge.DataStore.getFloorByBuildingId(selectedValue);
            lookUpEditFloor.Properties.DataSource = Floor;
            lookUpEditFloor.Properties.DisplayMember = "floor_label";
            lookUpEditFloor.Properties.ValueMember = "floor_id";
            lookUpEditFloor.Properties.NullText = "[เลือกชั้น]";

        }

        private void lookUpEditFloor_EditValueChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(lookUpEditFloor.EditValue);

            DataTable Floor = BusinessLogicBridge.DataStore.getRoomByFloorId(selectedValue);
            gridLookUpEditRoom.Properties.DataSource = Floor;
            gridLookUpEditRoom.Properties.DisplayMember = "coderef";
            gridLookUpEditRoom.Properties.ValueMember = "room_id";
            gridLookUpEditRoom.Properties.NullText = "[เลือกห้อง]";

        }

        void initDropDownBuilding(int building_id)
        {
            DataTable Buildings = BusinessLogicBridge.DataStore.getAllBuilding(1);
            lookUpEditBuilding.Properties.DataSource = Buildings;
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = "[เลือกอาคาร]";

            lookUpEditBuilding.EditValue = building_id;

        }

        void initDropDownFloor(int building_id, int floor_id)
        {

            DataTable Floor = BusinessLogicBridge.DataStore.getFloorByBuildingId(building_id);
            lookUpEditFloor.Properties.DataSource = Floor;
            lookUpEditFloor.Properties.DisplayMember = "floor_label";
            lookUpEditFloor.Properties.ValueMember = "floor_id";
            lookUpEditFloor.Properties.NullText = "[เลือกชั้น]";
            lookUpEditFloor.EditValue = floor_id;
        }

        void initDropDownRoom(int floor_id, int room_id)
        {

            DataTable room = BusinessLogicBridge.DataStore.getRoomByFloorId(floor_id);
            gridLookUpEditRoom.Properties.DataSource = room;
            gridLookUpEditRoom.Properties.DisplayMember = "coderef";
            gridLookUpEditRoom.Properties.ValueMember = "room_id";
            gridLookUpEditRoom.Properties.NullText = "[เลือกห้อง]";
            gridLookUpEditRoom.EditValue = room_id;
        }

        private bool isEmpty(string param)
        {
            if (param.Length < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isSelected(object param)
        {
            if (param == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string notice = "โปรดระบุ : ";
            string notice2 = "โปรดเลือก : ";

            bool bluidingName = isSelected(lookUpEditBuilding.EditValue);
            bool floor = isSelected(lookUpEditFloor.EditValue);
            bool room_number = isSelected(gridLookUpEditRoom.EditValue);
            bool meter_label = isEmpty(txtmeter_label.Text);
            bool meter_serial = isEmpty(txtmeter_serial.Text);
            bool meter_model = isEmpty(txtmeter_model.Text);

            if (!bluidingName)
            {
                XtraMessageBox.Show(notice2 + labelElectricBuildingLabel.Text.Replace(" :", "").ToString());
                lookUpEditBuilding.Focus();
            }
            else if (!floor)
            {
                XtraMessageBox.Show(notice2 + labelElectricFloor.Text.Replace(" :", "").ToString());
                lookUpEditFloor.Focus();
            }
            else if (!room_number)
            {
                XtraMessageBox.Show(notice2 + labelElectricRoomNo.Text.Replace(" :", "").ToString());
                lookUpEditFloor.Focus();
            }
            else if (!meter_label)
            {
                XtraMessageBox.Show(notice + labelElectricMeterLabel.Text.Replace(" :", "").ToString());
                txtmeter_label.Focus();
            }
            else if (!meter_serial)
            {
                XtraMessageBox.Show(notice + labelElectricMeterSerial.Text.Replace(" :", "").ToString());
                txtmeter_serial.Focus();
            }
            else if (!meter_model)
            {
                XtraMessageBox.Show(notice + labelElectricMeterModel.Text.Replace(" :", "").ToString());
                txtmeter_model.Focus();
            }
            else {
                // Check Meter Exist
                string EventType = "update";
                DataTable meterDetail = BusinessLogicBridge.DataStore.checkWaterMeterExist(txtmeter_label.Text, txtmeter_serial.Text, EventType);

                if (meterDetail.Rows.Count > 0)
                {
                    string msg = "มิเตอร์ที่ท่านระบุมีแล้วในระบบ";
                    XtraMessageBox.Show(msg);
                }
                else
                {

                    DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไขข้อมูล", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        BusinessLogicBridge.DataStore.updateDataWaterMeter(lookUpEditBuilding.EditValue.ToString(), lookUpEditFloor.EditValue.ToString(), gridLookUpEditRoom.EditValue.ToString(), txtmeter_label.Text, txtmeter_serial.Text, txtmeter_model.Text, memometer_detail.Text, Convert.ToInt16(textEditMeterId.EditValue));
                        BasicInfoWater.AddPanel_ControlRemoved();
                        BasicInfoWater.UpdatePanel.Close();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BasicInfoWater.UpdatePanel.Close();
        }
    }
}
