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
    public partial class BasicInfoElectricMeterAdd : DevExpress.XtraEditors.XtraUserControl
    {
        public BasicInfoElectricMeterAdd()
        {
            InitializeComponent();
            initDropDownBuilding();

            //gridLookUpEdit3View.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridLookUpEdit3View_FocusedRowChanged);
            //gridLookUpEdit2View.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridLookUpEdit2View_FocusedRowChanged);
            lookUpEditBuilding.EditValueChanged +=new EventHandler(lookUpEditBuilding_EditValueChanged);
            lookUpEditFloor.EditValueChanged +=new EventHandler(lookUpEditFloor_EditValueChanged);

        }

        void initDropDownBuilding()
        {

            DataTable Buildings                         = BusinessLogicBridge.DataStore.getAllBuilding(1);
            lookUpEditBuilding.Properties.DataSource    = Buildings;
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember   = "building_id";
            lookUpEditBuilding.Properties.NullText      = "[เลือกอาคาร]";
        }

        private void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(lookUpEditBuilding.EditValue);

            DataTable Floor = BusinessLogicBridge.DataStore.getFloorByBuildingId(selectedValue);
            lookUpEditFloor.Properties.DataSource       = Floor;
            lookUpEditFloor.Properties.DisplayMember    = "floor_label";
            lookUpEditFloor.Properties.ValueMember      = "floor_id";
            lookUpEditFloor.Properties.NullText         = "[เลือกชั้น]";

        }

        private void lookUpEditFloor_EditValueChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(lookUpEditFloor.EditValue);

            DataTable Floor = BusinessLogicBridge.DataStore.getRoomByFloorId(selectedValue);
            gridLookUpEditRoom.Properties.DataSource = Floor;
            gridLookUpEditRoom.Properties.DisplayMember = "coderef";
            gridLookUpEditRoom.Properties.ValueMember = "room_id";
            gridLookUpEditRoom.Properties.NullText = "[เลือกห้อง]";


            Floor.Columns.Add("meter_label_list", typeof(string));

            for (int i = 0; i < Floor.Rows.Count; i++)
            {
                Floor.Rows[i]["meter_label_list"] = "EM" + Floor.Rows[i]["coderef"];
            }


            lookUpEditMeterLabel.Properties.DataSource      = Floor;
            lookUpEditMeterLabel.Properties.DisplayMember   = "meter_label_list";
            lookUpEditMeterLabel.Properties.ValueMember     = "meter_label_list";
            lookUpEditMeterLabel.Properties.NullText        = "[เลือกมิเตอร์]";
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
            if (param==null)
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
            string notice   = "โปรดระบุ : ";
            string notice2  = "โปรดเลือก : ";

            bool bluidingName       = isSelected(lookUpEditBuilding.EditValue);
            bool floor              = isSelected(lookUpEditFloor.EditValue);
            bool room_number        = isSelected(gridLookUpEditRoom.EditValue);
            //bool meter_label        = isEmpty(txtmeter_label.Text);
            bool meter_serial       = isEmpty(txtmeter_serial.Text);
            bool meter_model        = isEmpty(txtmeter_model.Text);

            if (!bluidingName)
            {
                XtraMessageBox.Show(notice2 + labelElectricBuildingLabel.Text.Replace(" :", "").ToString());
                lookUpEditBuilding.Focus();
            }
            else if (!floor) {
                XtraMessageBox.Show(notice2 + labelElectricFloor.Text.Replace(" :","").ToString());
                lookUpEditFloor.Focus();
            }
            else if (!room_number) {
                XtraMessageBox.Show(notice2 + labelElectricRoomNo.Text.Replace(" :", "").ToString());
                lookUpEditFloor.Focus();
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
            else
            {   
                // Check Meter Exist

                DataTable meterDetail = BusinessLogicBridge.DataStore.checkElectricMeterExist(lookUpEditMeterLabel.EditValue.ToString(), txtmeter_serial.Text);

                if (meterDetail.Rows.Count > 0)
                {
                    string msg = "มิเตอร์ที่ท่านระบุมีแล้วในระบบ";
                    XtraMessageBox.Show(msg);
                }
                else
                {

                    DialogResult dr = XtraMessageBox.Show("ยืนยันการเพิ่มข้อมูล", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        BusinessLogicBridge.DataStore.addDataElectricMeter(lookUpEditBuilding.EditValue.ToString(), lookUpEditFloor.EditValue.ToString(), gridLookUpEditRoom.EditValue.ToString(), lookUpEditMeterLabel.EditValue.ToString(), txtmeter_serial.Text, txtmeter_model.Text, memometer_detail.Text, 0, 0);
                        BasicInfoElectricMeter.AddPanel_ControlRemoved();
                        BasicInfoElectricMeter.AddPanel.Close();
                    }
                }


            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BasicInfoElectricMeter.AddPanel.Close();
        }
        

    }
}
