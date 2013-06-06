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
    public partial class DeviceWMeter : uBase
    {
        static DataTable MeterIDList;
        static DataTable SerialModelList;
        private Boolean _CheckRoom = false;
        private int room_check_count = 0;
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private readonly BackgroundWorker _progressbar = new BackgroundWorker();
        public static DataTable W_meterCheckedBox;

        public DeviceWMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(DeviceWMeter_Load);
            gridViewWMeter.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewWMeter_FocusedRowChanged);
            checkEditSelectAll.CheckedChanged += new EventHandler(checkEditSelectAll_CheckedChanged);


            _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);

            _progressbar.DoWork += new DoWorkEventHandler(_progressbar_DoWork);
            _progressbar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_progressbar_RunWorkerCompleted);
        }

        void _progressbar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlWMeter.DataSource = W_meterCheckedBox;
        }

        void _progressbar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

                DataRow[] rWater = W_meterCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");
                if (ConnectionStatus == false)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        foreach (DataRow r in rWater)
                        {
                            r["meter_status_text"] = "Fail";
                            r["meter_status"] = false;
                        }
                        gridControlWMeter.DataSource = W_meterCheckedBox;
                    });
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    int WMETER_Status = 0;

                    W_meterCheckedBox = (DataTable)(gridControlWMeter.DataSource);

                    var WMeterConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjWMeterConfig(rWater);

                    WMETER_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].SetWMeterConfig(WMeterConfigInfo);

                    if (WMETER_Status == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rWater)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }
                        });
                    }
                    else
                    {
                        // Success
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rWater)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }
                        });
                    }
                }
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3025"), getLanguage("_softwarename"), "info");

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlWMeter.DataSource = W_meterCheckedBox;
        }

        void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

                DataRow[] rWater = W_meterCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ", "room_id");

                if (ConnectionStatus == false)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        foreach (DataRow r in rWater)
                        {
                            r["meter_status_text"] = "Fail";
                            r["meter_status"] = false;
                        }
                        gridControlWMeter.DataSource = W_meterCheckedBox;
                    });
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    W_meterCheckedBox = (DataTable)(gridControlWMeter.DataSource);

                    var WMeterConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjWMeterConfig(rWater);

                    foreach (DataRow r in rWater)
                    {
                        if (Convert.ToBoolean(r["grid_meter_check"]) == true)
                        {
                            int WMETER_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestWMeter(r["meter_serial"].ToString());

                            if (WMETER_Status == 0)
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
                                    r["meter_status_text"] = "Fail";
                                    r["meter_status"] = false;
                                });
                            }
                        }

                    }
                }
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3025"), getLanguage("_softwarename"), "info");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
            if (gridViewWMeter.RowCount > 0)
            {
                for (int i = 0; i < gridViewWMeter.RowCount; i++)
                {
                    gridViewWMeter.Columns[0].View.SetRowCellValue(i, "grid_meter_check", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }

        void gridViewWMeter_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            int[] rowIndex = gridViewWMeter.GetSelectedRows();
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewWMeter.GetDataRow(rowIndex[0]);

                    textEditMeterName.EditValue = CurrentRow["meter_label"];
                    textEditMeterStatus.EditValue = CurrentRow["meter_status"];
                    textEditMeterID.EditValue = CurrentRow["water_id"];
                    //lookUpEditMeterID.EditValue = CurrentRow["meter_serial"];
                    lookUpEditBuilding.EditValue = CurrentRow["building_id"];
                    mruEditSerial.Text = CurrentRow["meter_serial"].ToString();
                    lookUpEditADCSetting.EditValue = CurrentRow["device_adc_id"];
                    //look


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
            this.groupMeterList.Text = getLanguage("_meter_item");
            this.groupMeterSetting.Text = getLanguage("_meter_setting");
            this.groupBoxMeterInRoom.Text = getLanguage("_meter_in_room");
            this.groupBoxMeterSetting.Text = getLanguage("_meter_setting");

            // Label Setting
            this.labelControlMeterName.Text = getLanguageWithColon("_name");
            this.labelControlMeterID.Text = getLanguageWithColon("_meter_id");
            this.labelControlADC.Text = getLanguageWithColon("_adc");

            // Grid Column
            this.grid_meter_no.Caption = getLanguage("_no");
            this.grid_buiding_label.Caption = getLanguage("_building");
            this.grid_meter_name.Caption = getLanguage("_name");
            this.grid_MeterID.Caption = getLanguage("_meter_id");
            this.grid_meter_adc_text.Caption = getLanguage("_adc");
            this.grid_meter_status_label.Caption = getLanguage("_connection_status");


            this.checkEditSelectAll.Text = getLanguage("_selectall");
            this.labelControlADCText.Text = getLanguage("_adc");
            this.labelControlRequired.Text = getLanguage("_required");


            // Button Text
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            this.bttSetToADC.Text = getLanguage("_set_to_adc");
            this.bttTestConnect.Text = getLanguage("_test_connection");
            this.bttImport.Text = getLanguage("_import");
            this.bttSet.Text = getLanguage("_set");

        }

        void DeviceWMeter_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = (this.Width * 55) / 100;

            setLangThis();
            initADC();
            initDropDownBuilding();
            LoadGridWMeter();

            mruEditSerial.Properties.Mask.EditMask = "([0-9]{1,10})";
            mruEditSerial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

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

        void LoadGridWMeter()
        {

            DataTable DT_WMeter = BusinessLogicBridge.DataStore.getWaterMeter();


            DT_WMeter.Columns.Add("meter_no", typeof(string));
            DT_WMeter.Columns.Add("grid_meter_check", typeof(bool));
            DT_WMeter.Columns.Add("meter_status_text", typeof(string));

            for (int i = 0; i < DT_WMeter.Rows.Count; i++)
            {

                DT_WMeter.Rows[i]["meter_no"] = (i + 1);
                DT_WMeter.Rows[i]["grid_meter_check"] = false;

                if (DT_WMeter.Rows[i]["meter_status"].ToString() == "True")
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

            gridControlWMeter.DataSource = DT_WMeter;
            W_meterCheckedBox = DT_WMeter;
            gridViewWMeter.FocusedRowHandle = 0;


        }

        void setEnable()
        {
            panelControlSettingGroup.Enabled = false;
            groupBoxMeterSetting.Enabled = true;
            groupMeterList.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttEdit.Enabled = false;
        }

        void setDisable()
        {
            panelControlSettingGroup.Enabled = true;
            groupBoxMeterSetting.Enabled = false;
            groupMeterList.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;

            bttEdit.Enabled = true;
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }

        private DataTable validateData()
        {
            string star_notice = getLanguage("_msg_1001");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (textEditMeterName.EditValue.ToString() == "")
            {
                label = labelControlMeterName.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditMeterName.Focus();
                    focus = true;
                }
            }

            if (mruEditSerial.EditValue.ToString() == "")
            {
                label = labelControlMeterID.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    mruEditSerial.Focus();
                    focus = true;
                }
            }
            else if (mruEditSerial.EditValue.ToString().Length != 10)
            {
                label = labelControlMeterID.Text;
                message = getLanguage("_msg_1074");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    mruEditSerial.Focus();
                    focus = true;
                }
            }


            if (lookUpEditADCSetting.EditValue.ToString() == "" || lookUpEditADCSetting.EditValue.To<int>() == 0)
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

        public bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        private void bttImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files (*.xls)|*.xls";
            if (open.ShowDialog() == DialogResult.OK)
            {
                MeterIDList = new DataTable();
                SerialModelList = new DataTable();
                MeterIDList.Columns.Add("meter_id", typeof(string));
                FileInfo file_info = new FileInfo(open.FileName);
                string file_paht = file_info.FullName;

                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file_paht + ";Extended Properties=Excel 8.0");
                OleDbDataAdapter da = new OleDbDataAdapter("select meter_id from [Sheet1$];", con);
                try
                {
                    da.Fill(SerialModelList);
                }
                catch
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                    return;
                }

                if (SerialModelList.Columns.Count > 1)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                    return;
                }

                if (SerialModelList.Columns.Contains("meter_id") == true)
                {
                    bool normalcase = true;


                    for (int i = 0; i < SerialModelList.Rows.Count; i++)
                    {
                        if (SerialModelList.Rows[i]["meter_id"].ToString().Trim().Length != 10)
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                            mruEditSerial.Properties.Items.Clear();
                            return;
                        }

                        int result;
                        if (int.TryParse(SerialModelList.Rows[i]["meter_id"].ToString().Trim(), out result) == false)
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                            mruEditSerial.Properties.Items.Clear();
                            return;
                        }

                        mruEditSerial.Properties.Items.Add(SerialModelList.Rows[i]["serial_no"]);
                    }


                    if (normalcase == false)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                        mruEditSerial.Properties.Items.Clear();
                        return;
                    }
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                    return;
                }

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3021"), getLanguage("_softwarename"), "info");
            }
        }

        private void bttTestConnect_Click(object sender, EventArgs e)
        {
            //
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), "");
                //
                return;
            }
            //
            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
            _bw.RunWorkerAsync();
        }

        private void bttSet_Click(object sender, EventArgs e)
        {
            if (lookUpEditADCList.EditValue == null)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                return;
            }

            DataTable W_meterCheckedBox = (DataTable)(gridControlWMeter.DataSource);

            DataTable DataChecked = new DataTable();

            DataChecked.Columns.Add("water_id", typeof(int));

            int counter = 0;
            for (int i = 0; i < W_meterCheckedBox.Rows.Count; i++)
            {
                if ((bool)(W_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                {
                    counter++;
                    DataChecked.Rows.Add(W_meterCheckedBox.Rows[i]["water_id"]);

                }
            }

            if (counter > 0)
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4033"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    for (int i = 0; i < DataChecked.Rows.Count; i++)
                    {
                        BusinessLogicBridge.DataStore.updateADCWater(DataChecked.Rows[i]["water_id"].To<int>(), lookUpEditADCList.EditValue.To<int>());
                    }
                    LoadGridWMeter();
                }
            }
            else
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                return;
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
            MeterListID = (DataTable)(gridControlWMeter.DataSource);
            //
            string editSerial = string.Empty;
            //
            DataRow CurrentRow = gridViewWMeter.GetDataRow(gridViewWMeter.GetSelectedRows()[0]);
            editSerial = CurrentRow["meter_serial"].ToString();


            for (int i = 0; i < MeterListID.Rows.Count; i++)
            {

                if (mruEditSerial.EditValue.ToString() == MeterListID.Rows[i]["meter_serial"].ToString())
                {
                    if (editSerial != mruEditSerial.EditValue.ToString())
                    {
                        checkMeterIDDuplicated = true;
                        break;
                    }
                }
            }

            if (checkMeterIDDuplicated == true)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1046"), getLanguage("_softwarename"));
                return;
            }
            else
            {

                // Save Data 
                BusinessLogicBridge.DataStore.updateW_Meter(textEditMeterName.EditValue.ToString(), mruEditSerial.EditValue.ToString(), Convert.ToInt32(lookUpEditADCSetting.EditValue), (bool)(textEditMeterStatus.EditValue), (int)(textEditMeterID.EditValue));
                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [W-Meter Update]");
                // Reload Grid
                LoadGridWMeter();

                // Success
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                setDisable();
                //
                panelControlSettingGroup.Enabled = true;
            }

        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                // Reload Grid Before Saving ( Data older )
                LoadGridWMeter();
                setDisable();
            }
        }

        private void bttSetToADC_Click(object sender, EventArgs e)
        {
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), "");
                //
                return;
            }
            //
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4022"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                try
                {
                    // Progress Bar.... Loading
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                    _progressbar.RunWorkerAsync();

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
