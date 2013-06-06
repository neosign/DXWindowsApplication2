using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class DevicePMeter : uBase
    {
        static DataTable MeterIDList;
        private Boolean _CheckRoom = false;
        private int room_check_count = 0;
        private readonly BackgroundWorker _testConnection = new BackgroundWorker();
        private readonly BackgroundWorker _progressbar = new BackgroundWorker();
        public static DataTable P_meterCheckedBox;

        public DevicePMeter()
        {
            InitializeComponent();
            //
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(DevicePMeter_Load);
            gridViewPMeter.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewWMeter_FocusedRowChanged);
            checkEditSelectAll.CheckedChanged += new EventHandler(checkEditSelectAll_CheckedChanged);

            _testConnection.DoWork += new DoWorkEventHandler(_testConnection_DoWork);
            _testConnection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_testConnection_RunWorkerCompleted);
        }

        void _testConnection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlPMeter.DataSource = P_meterCheckedBox;
        }

        void _testConnection_DoWork(object sender, DoWorkEventArgs e)
        {
            bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

            if (ConnectionStatus == false)
            {
                DataTable PhoneCheckedBox = (DataTable)(gridControlPMeter.DataSource);
                DataRow[] rPhone = PhoneCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and phone_label <>'' ");

                this.Invoke((MethodInvoker)delegate
                {
                    foreach (DataRow r in rPhone)
                    {
                        r["meter_status_text"] = "Fail";
                        r["meter_status"] = false;
                    }
                    gridControlPMeter.DataSource = PhoneCheckedBox;

                });
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                return;
            }
            else
            {
                try
                {

                    DataTable PhoneCheckedBox = (DataTable)(gridControlPMeter.DataSource);
                    DataRow[] rPhone = PhoneCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and phone_label <>'' ");

                    var PhoneConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjPhoneConfig(rPhone);

                    foreach (DataRow r in rPhone)
                    {
                        int PHONE_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestPhoneMeter(r["phone_label"].ToString());

                        if (PHONE_Status == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;

                            });
                        }
                        else
                        {
                            // Success
                            this.Invoke((MethodInvoker)delegate
                            {
                                r["meter_status_text"] = "Pass";
                                r["meter_status"] = true;
                            });
                        }

                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
            }
        }

        void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            if (_CheckRoom == false)
            {
                _CheckRoom = true;
                //eventSelected();
            }
            else
            {
                _CheckRoom = false;
                //eventUnselect();
            }

            room_check_count = 0;
            if (gridViewPMeter.RowCount > 0)
            {
                for (int i = 0; i < gridViewPMeter.RowCount; i++)
                {
                    gridViewPMeter.Columns[0].View.SetRowCellValue(i, "grid_meter_check", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }

        void gridViewWMeter_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            int[] rowIndex = gridViewPMeter.GetSelectedRows();
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewPMeter.GetDataRow(rowIndex[0]);

                    textEditBuilding.EditValue = CurrentRow["building_label"];
                    textEditMeterName.EditValue = CurrentRow["room_label"];
                    textEditPhoneNo.EditValue = CurrentRow["phone_label"];
                    textEditMeterStatus.EditValue = CurrentRow["phone_status"];
                    textEditMeterID.EditValue = CurrentRow["phone_id"];
                    lookUpEditADCSetting.EditValue = CurrentRow["device_adc_id"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        public void setLangThis()
        {
            // Group Box
            this.groupMeterList.Text = getLanguage("_phone_item");
            this.groupMeterSetting.Text = getLanguage("_phone_setting");
            this.groupBoxMeterSetting.Text = getLanguage("_phone_setting");

            // Label Setting
            this.labelControlRoomName.Text = getLanguageWithColon("_room_no");
            this.labelControlPhoneNo.Text = getLanguageWithColon("_phone_no");
            this.labelControlADC.Text = getLanguageWithColon("_adc");
            this.labelControlBuilding.Text = getLanguageWithColon("_building");

            // Grid Column
            this.grid_meter_no.Caption = getLanguage("_no");
            this.grid_buiding_label.Caption = getLanguage("_building");
            this.grid_room_name.Caption = getLanguage("_room_no");
            this.grid_phone_no.Caption = getLanguage("_phone_no");
            this.grid_meter_adc_text.Caption = getLanguage("_adc");
            this.grid_meter_status_label.Caption = getLanguage("_connection_status");


            this.checkEditSelectAll.Text = getLanguage("_selectall");
            this.labelControlADCText.Text = getLanguage("_adc");
            this.labelControlRequired.Text = getLanguage("_required");


            // Button Text
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            this.bttTestConnect.Text = getLanguage("_test_connection");
            this.bttSet.Text = getLanguage("_set");

        }

        void DevicePMeter_Load(object sender, EventArgs e)
        {
            MeterIDList = new DataTable();
            //
            splitContainerControl1.SplitterPosition = (this.Width * 55) / 100;

            setLangThis();
            initADC();

            LoadGridPMeter();
            //
            setDisable();
        }

        void initADC()
        {
            DataTable DT_ADC_List = BusinessLogicBridge.DataStore.listADC();


            DataTable DT_ADC = new DataTable();
            DT_ADC.Columns.Add("adc_id", typeof(int));
            DT_ADC.Columns.Add("adc_label", typeof(string));

            int CountRow = DT_ADC_List.Rows.Count;

            for (int i = 0; i < CountRow; i++)
            {
                DT_ADC.Rows.Add(DT_ADC_List.Rows[i]["device_adc_id"], DT_ADC_List.Rows[i]["device_adc_name"].ToString());
            }

            lookUpEditADCList.Properties.DisplayMember = "adc_label";
            lookUpEditADCList.Properties.ValueMember = "adc_id";
            lookUpEditADCList.Properties.NullText = getLanguage("_select_adc");
            lookUpEditADCList.Properties.DataSource = DT_ADC;

            lookUpEditADCSetting.Properties.DisplayMember = "adc_label";
            lookUpEditADCSetting.Properties.ValueMember = "adc_id";
            lookUpEditADCSetting.Properties.NullText = getLanguage("_select_adc");
            lookUpEditADCSetting.Properties.DataSource = DT_ADC;

            lookUpEditADCTop.Properties.DisplayMember = "adc_label";
            lookUpEditADCTop.Properties.ValueMember = "adc_id";
            lookUpEditADCTop.Properties.NullText = getLanguage("_select_adc");
            lookUpEditADCTop.Properties.DataSource = DT_ADC;
        }

        void LoadGridPMeter()
        {

            DataTable DT_WMeter = BusinessLogicBridge.DataStore.getPhoneMeter();

            DT_WMeter.Columns.Add("meter_no", typeof(string));
            DT_WMeter.Columns.Add("grid_meter_check", typeof(bool));
            DT_WMeter.Columns.Add("meter_status_text", typeof(string));
            DT_WMeter.Columns.Add("meter_status", typeof(bool));

            for (int i = 0; i < DT_WMeter.Rows.Count; i++)
            {

                DT_WMeter.Rows[i]["meter_no"] = (i + 1);
                DT_WMeter.Rows[i]["grid_meter_check"] = false;
                DT_WMeter.Rows[i]["meter_status"] = false;

                if (DT_WMeter.Rows[i]["phone_status"].ToString() == "True")
                {
                    DT_WMeter.Rows[i]["meter_status_text"] = "Pass";
                }
                else
                {
                    DT_WMeter.Rows[i]["meter_status_text"] = "Fail";
                }

            }

            if (DT_WMeter.Rows.Count <= 0)
            {
                panelControlSettingGroup.Enabled = false;
                bttEdit.Enabled = false;
            }
            else
            {
                panelControlSettingGroup.Enabled = true;
                bttEdit.Enabled = true;
            }

            gridControlPMeter.DataSource = DT_WMeter;
            P_meterCheckedBox = DT_WMeter;

        }

        void setEnable()
        {
            groupMeterList.Enabled = false;
            //groupBoxMeterInRoom.Enabled = false;
            groupBoxMeterSetting.Enabled = true;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttEdit.Enabled = false;
        }

        void setDisable()
        {
            groupMeterList.Enabled = true;
            //groupBoxMeterInRoom.Enabled = true;
            groupBoxMeterSetting.Enabled = false;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;

            bttEdit.Enabled = true;
        }

        private DataTable validateData()
        {
            string max_50 = DXWindowsApplication2.MainForm.getLanguage("_max_50");

            string star_notice = DXWindowsApplication2.MainForm.getLanguage("_notice_star");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (textEditPhoneNo.EditValue.ToString() == "")
            {
                label = labelControlPhoneNo.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditPhoneNo.Focus();
                    focus = true;
                }
            }
            if (lookUpEditADCSetting.EditValue == null || lookUpEditADCSetting.EditValue.To<int>() == 0)
            {
                label = labelControlADC.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditADCSetting.Focus();
                    focus = true;
                }
            }
            return _ValidateTable;
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {

            setEnable();

        }

        private void bttTestConnect_Click(object sender, EventArgs e)
        {
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), "");
                //
                return;
            }
            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
            _testConnection.RunWorkerAsync();
        }

        private void bttSet_Click(object sender, EventArgs e)
        {

            P_meterCheckedBox = (DataTable)(gridControlPMeter.DataSource);

            try
            {
                if (lookUpEditADCList.EditValue == null)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                    return;
                }

                int counter = 0;
                for (int i = 0; i < P_meterCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(P_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {
                        counter++;
                    }
                }

                if (counter > 0)
                {
                    if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4033"), getLanguage("_softwarename")) == DialogResult.Yes)
                    {
                        for (int i = 0; i < P_meterCheckedBox.Rows.Count; i++)
                        {
                            if ((bool)(P_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                            {
                                // Update Meter
                                BusinessLogicBridge.DataStore.updateADCPhone(P_meterCheckedBox.Rows[i]["phone_id"].To<int>(), lookUpEditADCList.EditValue.To<int>());
                            }
                        }
                        LoadGridPMeter();
                    }
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                    return;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void bttSave_Click(object sender, EventArgs e)
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
                return;
            }


            bool checkMeterIDDuplicated = false;
            DataTable MeterListID = new DataTable();
            MeterListID = (DataTable)(gridControlPMeter.DataSource);
            //
            string editSerial = string.Empty;
            //
            DataRow CurrentRow = gridViewPMeter.GetDataRow(gridViewPMeter.GetSelectedRows()[0]);
            editSerial = CurrentRow["phone_label"].ToString();


            for (int i = 0; i < MeterListID.Rows.Count; i++)
            {

                if ((textEditPhoneNo.EditValue.ToString() == MeterListID.Rows[i]["phone_label"].ToString()) && (MeterListID.Rows[i]["device_adc_name"].ToString() != ""))
                {
                    if (editSerial != textEditPhoneNo.EditValue.ToString())
                    {
                        checkMeterIDDuplicated = true;
                        break;
                    }
                }
            }

            if (checkMeterIDDuplicated == true)
            {

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1047"), getLanguage("_softwarename"));
                return;
            }
            else
            {

                // Save Data 
                BusinessLogicBridge.DataStore.updateP_Meter(textEditPhoneNo.EditValue.ToString(), lookUpEditADCSetting.EditValue.To<int>(), (bool)(textEditMeterStatus.EditValue), textEditMeterID.EditValue.To<int>());
                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [Phone Update]");
                // Reload Grid
                LoadGridPMeter();

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                setDisable();
            }

        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                // Reload Grid Before Saving ( Data older )
                LoadGridPMeter();
                setDisable();
            }
        }

        private void bttSetToADC_Click(object sender, EventArgs e)
        {

        }
    }
}
