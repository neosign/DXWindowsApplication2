﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2
{
    public partial class BasicInfoElectricMeterUpdate : DevExpress.XtraEditors.XtraUserControl
    {
        public BasicInfoElectricMeterUpdate(int room_id)
        {
            InitializeComponent();

            DataTable roomDetail = BusinessLogicBridge.DataStore.getRoomById(room_id);

            string coderef      = roomDetail.Rows[0]["coderef"].ToString();
            string meter_label  = roomDetail.Rows[0]["meter_label"].ToString();
            string meter_id     = roomDetail.Rows[0]["meter_id"].ToString();

            textEditMeterId.EditValue = meter_id;

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

        void initDropDownRoom(int floor_id, int room_id) {

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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string notice = "โปรดระบุ : ";

            bool meter_serial = isEmpty(txtmeter_serial.Text);
            bool meter_model = isEmpty(txtmeter_model.Text);

            if (!meter_serial)
            {
                XtraMessageBox.Show(notice + labelElectricMeterSerial.Text.Replace(" :", "").ToString());
                txtmeter_serial.Focus();
            }
            else if (!meter_model)
            {
                XtraMessageBox.Show(notice + labelElectricMeterModel.Text.Replace(" :", "").ToString());
                txtmeter_model.Focus();
            }
            else
            {

                DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไขข้อมูล", "", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        BusinessLogicBridge.DataStore.updateElectricMeter(Convert.ToInt16(lookUpEditFloor.EditValue), Convert.ToInt16(gridLookUpEditRoom.EditValue), txtmeter_label.Text, txtmeter_serial.Text, txtmeter_model.Text, memometer_detail.Text, Convert.ToInt16(textEditMeterId.Text));
                        UserForms.BasicInfoElectricMeter.AddPanel_ControlRemoved();
                        UserForms.BasicInfoElectricMeter.UpdatePanel.Close();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            UserForms.BasicInfoElectricMeter.UpdatePanel.Close();
        }
    }
}
