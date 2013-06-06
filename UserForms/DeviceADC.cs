using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace DXWindowsApplication2.UserForms
{
    public partial class DeviceADC : uBase
    {
        string button_event = "";
        private Boolean _CheckRoom = false;
        private int room_check_count = 0;
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private readonly BackgroundWorker _progressbar = new BackgroundWorker();
        private readonly BackgroundWorker _scan_lan_ip = new BackgroundWorker();
        public DataTable ADCIP_LIST;

        public static DataTable E_meterCheckedBox;

        public DeviceADC()
        {

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(DeviceADC_Load);

            gridViewADC.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewADC_FocusedRowChanged);
            _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);
            _bw.WorkerReportsProgress = false;

            _progressbar.DoWork += new DoWorkEventHandler(_progressbar_DoWork);
            _progressbar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_progressbar_RunWorkerCompleted);
            _progressbar.WorkerReportsProgress = false;

            // Scan IP
            _scan_lan_ip.DoWork += new DoWorkEventHandler(_scan_lan_ip_DoWork);
            _scan_lan_ip.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_scan_lan_ip_RunWorkerCompleted);
            _scan_lan_ip.WorkerReportsProgress = false;

            checkEditPortDefault.CheckedChanged += new EventHandler(checkEditPortDefault_CheckedChanged);

            checkEditPABXDefault.CheckedChanged += new EventHandler(checkEditPABXDefault_CheckedChanged);
            lookUpEditPort1.EditValueChanged += new EventHandler(lookUpEditPort1_EditValueChanged);
            lookUpEditPort2.EditValueChanged += new EventHandler(lookUpEditPort2_EditValueChanged);

            radioGroupEmeter.EditValueChanged += new EventHandler(radioGroupEmeter_EditValueChanged);
            radioGroupWater.EditValueChanged += new EventHandler(radioGroupWater_EditValueChanged);
            checkEditAutoReadP_MeterEveryAt.CheckedChanged += new EventHandler(checkEditAutoReadP_MeterEveryAt_CheckedChanged);

            SaveClick += new EventHandler(bttSave_Click);


        }

        void checkEditAutoReadP_MeterEveryAt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditAutoReadP_MeterEveryAt.Checked == false)
            {
                timeEditP_MeterAt.Enabled = false;
            }
            else
            {
                timeEditP_MeterAt.Enabled = true;
            }
        }

        void checkEditPortDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditPortDefault.Checked == false)
            {
                lookUpEditPort1.Enabled = true;
                lookUpEditPort2.Enabled = true;

            }
            else
            {
                lookUpEditPort1.EditValue = 1;
                lookUpEditPort2.EditValue = 3;

                lookUpEditPort1.Enabled = false;
                lookUpEditPort2.Enabled = false;

            }
        }

        void radioGroupWater_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroupWater.SelectedIndex == 0)
            {
                lookUpEditW_MeterMin.Enabled = false;
                timeEditW_MeterAt.Enabled = true;
            }
            else
            {
                lookUpEditW_MeterMin.Enabled = true;
                timeEditW_MeterAt.Enabled = false;
            }
        }

        void radioGroupEmeter_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroupEmeter.SelectedIndex == 0)
            {
                lookUpEditE_MeterMin.Enabled = false;
                timeEditE_MeterAt.Enabled = true;
            }
            else
            {
                lookUpEditE_MeterMin.Enabled = true;
                timeEditE_MeterAt.Enabled = false;
            }
        }

        void lookUpEditPort2_EditValueChanged(object sender, EventArgs e)
        {
            LoadWaterSetup();
        }

        void lookUpEditPort1_EditValueChanged(object sender, EventArgs e)
        {
            LoadWaterSetup();
        }

        void checkEditPABXDefault_CheckedChanged(object sender, EventArgs e)
        {

            if (checkEditPABXDefault.Checked == true)
            {
                lookUpEditPABXBoudRate.EditValue = 6; // 19200
                lookUpEditPABXBoudRate.Enabled = false;
            }
            else
            {
                lookUpEditPABXBoudRate.EditValue = 1;
                lookUpEditPABXBoudRate.Enabled = true;
            }
        }

        void _scan_lan_ip_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);

            this.Invoke((MethodInvoker)delegate
            {
                DataTable ADCCONFIGDT = new DataTable();
                DataTable ADCDT = ((DataTable)gridControlADC.DataSource);
                int ADCID = 0;
                string expression;
                DataRow[] foundRows;
                DataRow[] SerialADC;
                DataTable SerialList = new DataTable();
                int countADC = ADCDT.Rows.Count;

                if (ADCIP_LIST.Rows.Count > 0)
                {
                    #region check ADC
                    for (int k = 0; k < ADCIP_LIST.Rows.Count; k++)
                    {
                        ADCID = BusinessLogicBridge.DataStore.genADCID();

                        if (DXWindowsApplication2.MainForm.dictADC.ContainsKey("adc_id_" + ADCID) == true)
                        {
                            return;
                        }

                        DXWindowsApplication2.MainForm.dictADC.Add("adc_id_" + ADCID, new ADC.Jetbox.Lib.ADCClient() { adc_ip = ADCIP_LIST.Rows[k]["adc_ip_address"].ToString(), adc_port = 1600 });

                        ADC.Jetbox.Lib.objADCConfig ConfigInfo = DXWindowsApplication2.MainForm.dictADC["adc_id_" + ADCID].GetADCConfig();

                        if (ConfigInfo != null)
                        {
                            ADCCONFIGDT = DXWindowsApplication2.MainForm.ADCHelper.GetConfigFromADC(ConfigInfo);

                            if (countADC > 0)
                            {
                                for (int i = 0; i < countADC; i++)
                                {
                                    if (ADCDT.Rows[i]["device_adc_ipadress"].ToString() != "")
                                    {
                                        try
                                        {
                                            //if (ADCDT.Rows[i]["device_adc_serial"].ToString() != ADCCONFIGDT.Rows[0]["adc_serial"].ToString())
                                            //{
                                            SerialList = BusinessLogicBridge.DataStore.getSerialADC();

                                            SerialADC = SerialList.Select("adc_serial_no_1='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_2='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_3='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_4='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_5='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "'");

                                            if (SerialADC.Length != 0)
                                            {
                                                expression = "device_adc_ipadress ='" + ADCIP_LIST.Rows[k]["adc_ip_address"].ToString() + "'";

                                                foundRows = ADCDT.Select(expression);

                                                if (foundRows.Length == 0)
                                                {
                                                    if (ADCCONFIGDT.Rows[0]["adc_ipadress"].ToString() != ADCDT.Rows[i]["device_adc_ipadress"].ToString())
                                                    {
                                                        try
                                                        {
                                                            DataRow ADCRow = ADCDT.NewRow();

                                                            ADCDT.Rows.Add(
                                                                   0,
                                                                   "ADC_" + ADCID,
                                                                   ADCCONFIGDT.Rows[0]["adc_serial"],
                                                                   "",
                                                                   ADCCONFIGDT.Rows[0]["adc_port_use_defualt"],
                                                                   ADCCONFIGDT.Rows[0]["adc_port_1"],
                                                                   ADCCONFIGDT.Rows[0]["adc_port_2"],
                                                                   ADCCONFIGDT.Rows[0]["adc_network_config_type"],
                                                                   ADCIP_LIST.Rows[k]["adc_ip_address"].ToString(),
                                                                   ADCCONFIGDT.Rows[0]["adc_netmask"],
                                                                   ADCCONFIGDT.Rows[0]["adc_gateway"],
                                                                   ADCCONFIGDT.Rows[0]["adc_pabx_connect"],
                                                                   ADCCONFIGDT.Rows[0]["adc_pabx_brand"],
                                                                   ADCCONFIGDT.Rows[0]["adc_pabx_boud"],
                                                                   ADCCONFIGDT.Rows[0]["adc_pabx_use_default"],
                                                                   ADCCONFIGDT.Rows[0]["adc_water_connect"],
                                                                   ADCCONFIGDT.Rows[0]["adc_water_brand"],
                                                                   ADCCONFIGDT.Rows[0]["adc_water_ipaddress"],
                                                                   ADCCONFIGDT.Rows[0]["adc_water_port"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_at"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_at"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_p_meter_every_at"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_time"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_time"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_p_meter_every_time"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_at_min"],
                                                                   ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_at_min"],
                                                                   false,
                                                                   DateTime.Now,
                                                                   false,
                                                                   ADCDT.Rows.Count + 1,
                                                                   "Pass"
                                                               );
                                                            //DXWindowsApplication2.MainForm.dictADC.Remove("adc_id_" + ADCID);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            XtraMessageBox.Show(ex.ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Set To ADC
                                                        ADCDT.Rows[i]["grid_meter_check"] = true;
                                                        ADCDT.Rows[i]["device_adc_connection"] = 1;
                                                        ADCDT.Rows[i]["device_adc_connection_text"] = "Pass";
                                                        // Remove dictADC this row
                                                        DXWindowsApplication2.MainForm.dictADC.Remove("adc_id_" + ADCID);
                                                    }
                                                }
                                                else
                                                {
                                                    // Set To ADC
                                                    ADCDT.Rows[i]["grid_meter_check"] = true;
                                                    ADCDT.Rows[i]["device_adc_connection"] = 1;
                                                    ADCDT.Rows[i]["device_adc_connection_text"] = "Pass";
                                                    // Remove dictADC this row
                                                    DXWindowsApplication2.MainForm.dictADC.Remove("adc_id_" + ADCID);
                                                }
                                            }
                                            else
                                            {
                                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1044"), getLanguage("_softwarename"));
                                                DXWindowsApplication2.MainForm.dictADC.Remove("adc_id_" + ADCID);
                                            }
                                            //}
                                            //else
                                            //{
                                            //    // Set To ADC
                                            //    ADCDT.Rows[i]["grid_meter_check"] = false;
                                            //    ADCDT.Rows[i]["device_adc_connection"] = 0;
                                            //    ADCDT.Rows[i]["device_adc_connection_text"] = "Fail";
                                            //    // Remove dictADC this row
                                            //    DXWindowsApplication2.MainForm.dictADC.Remove("adc_id_" + ADCID);
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            XtraMessageBox.Show(ex.ToString());
                                        }
                                    }
                                }// End for loop
                            }
                            else
                            {

                                try
                                {
                                    SerialList = BusinessLogicBridge.DataStore.getSerialADC();

                                    SerialADC = SerialList.Select("adc_serial_no_1='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_2='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_3='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_4='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "' or adc_serial_no_5='" + ADCCONFIGDT.Rows[0]["adc_serial"].ToString() + "'");

                                    if (SerialADC.Length != 0)
                                    {
                                        DataRow ADCRow = ADCDT.NewRow();

                                        ADCRow[0] = 0;
                                        ADCRow[1] = "ADC_" + ADCID;
                                        ADCRow[2] = ADCCONFIGDT.Rows[0]["adc_serial"];
                                        ADCRow[3] = "";
                                        ADCRow[4] = ADCCONFIGDT.Rows[0]["adc_port_use_defualt"];
                                        ADCRow[5] = ADCCONFIGDT.Rows[0]["adc_port_1"];
                                        ADCRow[6] = ADCCONFIGDT.Rows[0]["adc_port_2"];
                                        ADCRow[7] = ADCCONFIGDT.Rows[0]["adc_network_config_type"];
                                        ADCRow[8] = ADCIP_LIST.Rows[k]["adc_ip_address"].ToString();
                                        ADCRow[9] = ADCCONFIGDT.Rows[0]["adc_netmask"];
                                        ADCRow[10] = ADCCONFIGDT.Rows[0]["adc_gateway"];
                                        ADCRow[11] = ADCCONFIGDT.Rows[0]["adc_pabx_connect"];
                                        ADCRow[12] = ADCCONFIGDT.Rows[0]["adc_pabx_brand"];
                                        ADCRow[13] = ADCCONFIGDT.Rows[0]["adc_pabx_boud"];
                                        ADCRow[14] = ADCCONFIGDT.Rows[0]["adc_pabx_use_default"];
                                        ADCRow[15] = ADCCONFIGDT.Rows[0]["adc_water_connect"];
                                        ADCRow[16] = ADCCONFIGDT.Rows[0]["adc_water_brand"];
                                        ADCRow[17] = ADCCONFIGDT.Rows[0]["adc_water_ipaddress"];
                                        ADCRow[18] = ADCCONFIGDT.Rows[0]["adc_water_port"];
                                        ADCRow[19] = ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_at"];
                                        ADCRow[20] = ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_at"];
                                        ADCRow[21] = ADCCONFIGDT.Rows[0]["adc_auto_p_meter_every_at"];
                                        ADCRow[22] = ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_time"];
                                        ADCRow[23] = ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_time"];
                                        ADCRow[24] = ADCCONFIGDT.Rows[0]["adc_auto_p_meter_every_time"];
                                        ADCRow[25] = ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every"];
                                        ADCRow[26] = ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every"];
                                        ADCRow[27] = ADCCONFIGDT.Rows[0]["adc_auto_e_meter_every_at_min"];
                                        ADCRow[28] = ADCCONFIGDT.Rows[0]["adc_auto_w_meter_every_at_min"];
                                        ADCRow[29] = false;
                                        ADCRow[30] = DateTime.Now;
                                        ADCRow[31] = false;
                                        ADCRow[32] = ADCDT.Rows.Count + 1;
                                        ADCRow[33] = "Pass";

                                        ADCDT.Rows.Add(ADCRow);
                                    }
                                    else
                                    {
                                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1044"), getLanguage("_softwarename"));
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.ToString());
                                }
                            }
                        }
                    }

                    gridControlADC.DataSource = ADCDT;
                    #endregion
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1044"), getLanguage("_softwarename"));
                }

            });
        }

        void _scan_lan_ip_DoWork(object sender, DoWorkEventArgs e)
        {
            // Scanner by Port 1600            
            // Scan On Network DCU ADC Device
            ADCIP_LIST = ScanADC();

        }

        void _progressbar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlADC.DataSource = E_meterCheckedBox;
        }

        void _progressbar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                E_meterCheckedBox = (DataTable)(gridControlADC.DataSource);
                int AmountChecked = 0;

                DataTable AdcDTSetting = new DataTable();

                #region DeClare Column Type of Data Table
                AdcDTSetting.Columns.Add("adc_name", typeof(string));
                AdcDTSetting.Columns.Add("adc_serial", typeof(string));
                AdcDTSetting.Columns.Add("adc_mac_address", typeof(string));
                AdcDTSetting.Columns.Add("adc_port_use_defualt", typeof(bool));
                AdcDTSetting.Columns.Add("adc_port_1", typeof(int));
                AdcDTSetting.Columns.Add("adc_port_2", typeof(int));
                AdcDTSetting.Columns.Add("adc_network_config_type", typeof(int));
                AdcDTSetting.Columns.Add("adc_ipadress", typeof(string));
                AdcDTSetting.Columns.Add("adc_netmask", typeof(string));
                AdcDTSetting.Columns.Add("adc_gateway", typeof(string));
                AdcDTSetting.Columns.Add("adc_pabx_connect", typeof(bool));
                AdcDTSetting.Columns.Add("adc_pabx_brand", typeof(int));
                AdcDTSetting.Columns.Add("adc_pabx_boud", typeof(int));
                AdcDTSetting.Columns.Add("adc_pabx_use_default", typeof(bool));
                AdcDTSetting.Columns.Add("adc_water_connect", typeof(bool));
                AdcDTSetting.Columns.Add("adc_water_brand", typeof(string));
                AdcDTSetting.Columns.Add("adc_water_ipaddress", typeof(string));
                AdcDTSetting.Columns.Add("adc_water_port", typeof(string));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_at", typeof(bool));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_at", typeof(bool));
                AdcDTSetting.Columns.Add("adc_auto_p_meter_every_at", typeof(bool));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_time", typeof(TimeSpan));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_time", typeof(TimeSpan));
                AdcDTSetting.Columns.Add("adc_auto_p_meter_every_time", typeof(TimeSpan));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every", typeof(bool));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every", typeof(bool));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_at_min", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_at_min", typeof(int));
                #endregion

                int _SetingSetting = 0;
                bool _ADCConnection;

                for (int i = 0; i < E_meterCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {
                        DataTable ADC_INFO = BusinessLogicBridge.DataStore.selectADC(Convert.ToInt32(E_meterCheckedBox.Rows[i]["device_adc_id"]));

                        AdcDTSetting.Rows.Add(
                            ADC_INFO.Rows[0]["device_adc_name"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_serial"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_mac"].ToString(),
                            (bool)(ADC_INFO.Rows[0]["device_adc_port_checked"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_port1"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_port2"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_network_config_type"]),
                            ADC_INFO.Rows[0]["device_adc_ipadress"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_netmask"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_gateway"].ToString(),
                            (bool)(ADC_INFO.Rows[0]["device_adc_connect_pabx"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_pabx_brand"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_pabx_boud"]),
                            (bool)(ADC_INFO.Rows[0]["device_adc_default"]),
                            (bool)(ADC_INFO.Rows[0]["device_adc_connect_water"]),
                            Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_water_brand"]),
                            ADC_INFO.Rows[0]["device_adc_water_ipaddress"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_water_port"].ToString(),
                            ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_at"],
                            ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_at"],
                            ADC_INFO.Rows[0]["device_adc_autoread_p_meter_every_at"],
                            ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_time"],
                            ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_time"],
                            ADC_INFO.Rows[0]["device_adc_autoread_p_meter_every_time"],
                            ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every"],
                            ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every"],
                            ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_min"],
                            ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_min"]
                        );
  
                        for (int j = 0; j < AdcDTSetting.Rows.Count; j++)
                        {

                            _ADCConnection = DXWindowsApplication2.MainForm.dictADC["adc_id_" + E_meterCheckedBox.Rows[i]["device_adc_id"]].TestADCConnection();

                            if (_ADCConnection == false)
                            {
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                                return;
                            }
                            else
                            {
                                ADC.Jetbox.Lib.objADCConfig ConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.SetConfigToADC(AdcDTSetting.Rows[j]);
                                _SetingSetting = DXWindowsApplication2.MainForm.dictADC["adc_id_" + E_meterCheckedBox.Rows[i]["device_adc_id"]].SetADCConfig(ConfigInfo);

                                if (_SetingSetting == 0)
                                {
                                    BusinessLogicBridge.DataStore.updateStatusADC(E_meterCheckedBox.Rows[i]["device_adc_id"].To<int>(), 0);
                                    utilClass.showPopupMessegeBox(this, "adc_id_" + E_meterCheckedBox.Rows[i]["device_adc_id"] + " Seting to ADC Failed !!!", getLanguage("_softwarename"));
                                    return;
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        BusinessLogicBridge.DataStore.updateStatusADC(E_meterCheckedBox.Rows[i]["device_adc_id"].To<int>(), 1);
                                        E_meterCheckedBox.Rows[i]["device_adc_connection_text"] = "Pass";
                                        E_meterCheckedBox.Rows[i]["device_adc_connection"] = true;
                                    });
                                }

                                ADC.Jetbox.Lib.objPhoneConfig objPhone = new ADC.Jetbox.Lib.objPhoneConfig();
                                objPhone.ADC_ID = ConfigInfo.ADC_ID;
                                objPhone.ADC_PortID = 0;
                                if (lookUpEditPort1.EditValue.ToString() == "3" || lookUpEditPort1.EditValue.ToString() == "3")
                                {

                                    if (lookUpEditPort1.EditValue.ToString() == "3")
                                    {
                                        objPhone.ADC_PortID = 1;
                                    }

                                    if (lookUpEditPort2.EditValue.ToString() == "3")
                                    {
                                        objPhone.ADC_PortID = 2;
                                    }
                                }

                                objPhone.Manufacture_ID = AdcDTSetting.Rows[j]["adc_pabx_brand"].ToString();
                                objPhone.Manufacture_Name = ConfigInfo.ADC_PABX_Name;
                                objPhone.Model = ConfigInfo.ADC_PABX_Model;

                                DXWindowsApplication2.MainForm.dictADC["adc_id_" + E_meterCheckedBox.Rows[i]["device_adc_id"]].SetPhoneConfig(objPhone);

                            }

                        }
                        AmountChecked++;
                    }
                }

                if (AmountChecked == 0)
                {
                    XtraMessageBox.Show(getLanguage("_adc_msg_1008"));
                    return;
                }

                // System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlADC.DataSource = E_meterCheckedBox;
        }

        void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dictionary<int, bool> dictADCStatus = new Dictionary<int, bool>();
                //DataTable ADCConfigDT = new DataTable();
                bool ADC_Status = false;
                for (int i = 0; i < E_meterCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {

                        ADC_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + E_meterCheckedBox.Rows[i]["device_adc_id"]].TestADCConnection();

                        dictADCStatus.Add(Convert.ToInt32(E_meterCheckedBox.Rows[i]["device_adc_id"]), ADC_Status);

                    }
                }

                this.Invoke((MethodInvoker)delegate
                {
                    for (int k = 0; k < E_meterCheckedBox.Rows.Count; k++)
                    {
                        if (dictADCStatus.ContainsKey(Convert.ToInt32(E_meterCheckedBox.Rows[k]["device_adc_id"])) == true)
                        {
                            if (dictADCStatus[Convert.ToInt32(E_meterCheckedBox.Rows[k]["device_adc_id"])].ToString() == "True")
                            {
                                // Map Field From ADC to Field Mysql by Helper
                                //ADCConfigDT = DXWindowsApplication2.MainForm.ADCHelper.GetConfigFromADC(DXWindowsApplication2.MainForm.dictADC["adc_id_" + E_meterCheckedBox.Rows[k]["device_adc_id"]].GetADCConfig());

                                E_meterCheckedBox.Rows[k]["device_adc_connection_text"] = "Pass";
                                E_meterCheckedBox.Rows[k]["device_adc_connection"] = true;
                                BusinessLogicBridge.DataStore.updateStatusADC(E_meterCheckedBox.Rows[k]["device_adc_id"].To<int>(), 1);
                            }
                            else
                            {
                                E_meterCheckedBox.Rows[k]["device_adc_connection_text"] = "Fail";
                                E_meterCheckedBox.Rows[k]["device_adc_connection"] = false;
                                BusinessLogicBridge.DataStore.updateStatusADC(E_meterCheckedBox.Rows[k]["device_adc_id"].To<int>(), 0);
                            }

                        }
                        else
                        {
                            E_meterCheckedBox.Rows[k]["device_adc_connection_text"] = "Fail";
                            E_meterCheckedBox.Rows[k]["device_adc_connection"] = false;
                        }

                    }
                });

                //System.Threading.Thread.Sleep(5000);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void gridViewADC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewADC.GetSelectedRows();

            if (rowIndex.Length <= 0)
                return;

            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewADC.GetDataRow(rowIndex[0]);
                    if (CurrentRow == null)
                        return;
                    //
                    int ADC_ID = Convert.ToInt32(CurrentRow["device_adc_id"].ToString());

                    //if (ADC_ID == 0)
                    //{
                    //    bttSetToADC.Enabled = false;
                    //    bttTestConnection.Enabled = false;
                    //}
                    //else
                    //{
                    //    bttSetToADC.Enabled = true;
                    //    bttTestConnection.Enabled = true;
                    //}

                    // Check Serial on lisence
                    //DataTable SerialList = BusinessLogicBridge.DataStore.getSerialADC();
                    //string lisence = CurrentRow["device_adc_serial"].ToString();

                    //var rADC = SerialList.Select("adc_serial_no_1='" + lisence + "' or adc_serial_no_2='" + lisence + "' or adc_serial_no_3='" + lisence + "' or adc_serial_no_4='" + lisence + "' or adc_serial_no_5='" + lisence + "'");

                    //if (rADC.Length == 0)
                    //{
                    //    textEditADCSerial.Enabled = true;
                    //}

                    textEditADC_ID.EditValue = ADC_ID;

                    if (ADC_ID == 0)
                    {

                        textEditADCSerial.EditValue = CurrentRow["device_adc_serial"];


                        textEditADCName.EditValue = CurrentRow["device_adc_name"];
                        textEditMACAddress.EditValue = CurrentRow["device_adc_mac"];
                        if ((CurrentRow["device_adc_port_checked"]).ToString() == "True")
                        {
                            checkEditPortDefault.Checked = true;

                            lookUpEditPort1.Enabled = false;
                            lookUpEditPort2.Enabled = false;
                        }
                        else
                        {
                            lookUpEditPort1.Enabled = true;
                            lookUpEditPort2.Enabled = true;
                        }

                        lookUpEditPort1.EditValue = CurrentRow["device_adc_port1"];
                        lookUpEditPort2.EditValue = CurrentRow["device_adc_port2"];

                        radioGroupNetworkConfig.SelectedIndex = Convert.ToInt32(CurrentRow["device_adc_network_config_type"]);

                        textEditADCIP.EditValue = CurrentRow["device_adc_ipadress"];
                        textEditADCNetmask.EditValue = CurrentRow["device_adc_netmask"];
                        textEditADCGateway.EditValue = CurrentRow["device_adc_gateway"];

                        lookUpEditPABXBrand.EditValue = ((CurrentRow["device_adc_pabx_brand"]).To<int>() == 0) ? 1 : (CurrentRow["device_adc_pabx_brand"]).To<int>();
                        lookUpEditPABXBoudRate.EditValue = ((CurrentRow["device_adc_pabx_boud"]).To<int>() == 0) ? 1 : (CurrentRow["device_adc_pabx_boud"]).To<int>();

                        if (CurrentRow["device_adc_default"].ToString() == "True")
                        {
                            checkEditPABXDefault.Checked = true;
                        }
                        else
                        {
                            checkEditPABXDefault.Checked = false;
                        }

                        // PABX
                        checkEditConnectPABX.Checked = false;
                        if ((CurrentRow["device_adc_connect_pabx"]).ToString() == "True")
                        {
                            checkEditConnectPABX.Checked = true;
                        }
                        // Water
                        checkEditConnectWater.Checked = false;
                        if ((CurrentRow["device_adc_connect_water"]).ToString() == "True")
                        {
                            checkEditConnectWater.Checked = true;
                        }

                        // Water Brand
                        lookUpEditWaterBrand.EditValue = CurrentRow["device_adc_water_brand"];
                        textEditWaterIP.EditValue = CurrentRow["device_adc_water_ipaddress"];
                        textEditWaterPort.EditValue = CurrentRow["device_adc_water_port"];

                        // E-Meter Checked
                        bool E_MeterEveryAt = CurrentRow["device_adc_autoread_e_meter_every_at"].To<int>() == 1;
                        radioGroupEmeter.SelectedIndex = E_MeterEveryAt ? 0 : 1;

                        // W-Meter Checked
                        bool W_MeterEveryAt = CurrentRow["device_adc_autoread_w_meter_every_at"].To<int>() == 1;
                        radioGroupWater.SelectedIndex = W_MeterEveryAt ? 0 : 1;

                        // P-Meter Checked
                        bool P_MeterEveryAt = CurrentRow["device_adc_autoread_p_meter_every_at"].To<int>() == 1;
                        checkEditAutoReadP_MeterEveryAt.Checked = P_MeterEveryAt;

                        if (checkEditAutoReadP_MeterEveryAt.Checked == false)
                        {
                            timeEditP_MeterAt.Enabled = false;
                        }

                        timeEditE_MeterAt.EditValue = CurrentRow["device_adc_autoread_e_meter_every_time"];
                        timeEditW_MeterAt.EditValue = CurrentRow["device_adc_autoread_w_meter_every_time"];
                        timeEditP_MeterAt.EditValue = CurrentRow["device_adc_autoread_p_meter_every_time"];

                        lookUpEditE_MeterMin.EditValue = CurrentRow["device_adc_autoread_e_meter_every_min"];
                        lookUpEditW_MeterMin.EditValue = CurrentRow["device_adc_autoread_w_meter_every_min"];

                    }
                    else
                    {

                        DataTable ADC_INFO = BusinessLogicBridge.DataStore.selectADC(ADC_ID);

                        textEditADCSerial.EditValue = "";

                        textEditADCName.EditValue = ADC_INFO.Rows[0]["device_adc_name"].ToString();

                        if (ADC_INFO.Rows[0]["device_adc_serial"].ToString() != "")
                        {
                            textEditADCSerial.EditValue = CurrentRow["device_adc_serial"].ToString();
                        }
                        textEditMACAddress.EditValue = "";
                        if (CurrentRow["device_adc_mac"].ToString() != "")
                        {
                            textEditMACAddress.EditValue = CurrentRow["device_adc_mac"].ToString();
                        }

                        // Port
                        checkEditPortDefault.Checked = false;
                        if ((ADC_INFO.Rows[0]["device_adc_port_checked"]).ToString() == "True")
                        {
                            checkEditPortDefault.Checked = true;
                        }

                        lookUpEditPort1.EditValue = ADC_INFO.Rows[0]["device_adc_port1"];
                        lookUpEditPort2.EditValue = ADC_INFO.Rows[0]["device_adc_port2"];


                        radioGroupNetworkConfig.SelectedIndex = ADC_INFO.Rows[0]["device_adc_network_config_type"].To<int>();

                        textEditADCIP.EditValue = ADC_INFO.Rows[0]["device_adc_ipadress"];
                        textEditADCNetmask.EditValue = ADC_INFO.Rows[0]["device_adc_netmask"];
                        textEditADCGateway.EditValue = ADC_INFO.Rows[0]["device_adc_gateway"];

                        lookUpEditPABXBrand.EditValue = ADC_INFO.Rows[0]["device_adc_pabx_brand"].ToString().To<int>() == 0 ? 1 : ADC_INFO.Rows[0]["device_adc_pabx_brand"].To<int>();
                        lookUpEditPABXBoudRate.EditValue = ADC_INFO.Rows[0]["device_adc_pabx_boud"].ToString().To<int>() == 0 ? 1 : ADC_INFO.Rows[0]["device_adc_pabx_boud"].To<int>();



                        if (ADC_INFO.Rows[0]["device_adc_default"].ToString() == "True")
                        {
                            checkEditPABXDefault.Checked = true;
                        }
                        else
                        {
                            checkEditPABXDefault.Checked = false;
                        }

                        // PABX
                        checkEditConnectPABX.Checked = false;
                        if ((ADC_INFO.Rows[0]["device_adc_connect_pabx"]).ToString() == "True")
                        {
                            checkEditConnectPABX.Checked = true;
                        }
                        // Water
                        checkEditConnectWater.Checked = false;
                        if ((ADC_INFO.Rows[0]["device_adc_connect_water"]).ToString() == "True")
                        {
                            checkEditConnectWater.Checked = true;
                        }

                        // Water Brand
                        lookUpEditWaterBrand.EditValue = ADC_INFO.Rows[0]["device_adc_water_brand"];

                        textEditWaterIP.EditValue = ADC_INFO.Rows[0]["device_adc_water_ipaddress"];
                        textEditWaterPort.EditValue = ADC_INFO.Rows[0]["device_adc_water_port"];

                        // E-Meter Checked
                        bool E_MeterEveryAt = ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_at"].To<bool>();
                        radioGroupEmeter.SelectedIndex = E_MeterEveryAt ? 0 : 1;

                        // W-Meter Checked
                        bool W_MeterEveryAt = ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_at"].To<bool>();
                        radioGroupWater.SelectedIndex = W_MeterEveryAt ? 0 : 1;

                        // P-Meter Checked
                        bool P_MeterEveryAt = ADC_INFO.Rows[0]["device_adc_autoread_p_meter_every_at"].To<bool>();
                        checkEditAutoReadP_MeterEveryAt.Checked = P_MeterEveryAt;

                        if (checkEditAutoReadP_MeterEveryAt.Checked == false)
                        {
                            timeEditP_MeterAt.Enabled = false;
                        }

                        lookUpEditE_MeterMin.EditValue = CurrentRow["device_adc_autoread_e_meter_every_min"];
                        lookUpEditW_MeterMin.EditValue = CurrentRow["device_adc_autoread_w_meter_every_min"];

                        timeEditE_MeterAt.EditValue = ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_time"].ToString() == "0" ?
                            new TimeSpan(0, 0, 0) : ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_time"];
                        timeEditW_MeterAt.EditValue = ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_time"].ToString() == "0" ?
                            new TimeSpan(0, 0, 0) : ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_time"];
                        timeEditP_MeterAt.EditValue = ADC_INFO.Rows[0]["device_adc_autoread_p_meter_every_time"].ToString() == "0" ?
                            new TimeSpan(0, 0, 0) : ADC_INFO.Rows[0]["device_adc_autoread_p_meter_every_time"];

                        lookUpEditE_MeterMin.EditValue = ADC_INFO.Rows[0]["device_adc_autoread_e_meter_every_min"];
                        if (lookUpEditE_MeterMin.EditValue.ToString() == "0")
                            lookUpEditE_MeterMin.EditValue = 15;
                        //
                        lookUpEditW_MeterMin.EditValue = ADC_INFO.Rows[0]["device_adc_autoread_w_meter_every_min"];
                        if (lookUpEditW_MeterMin.EditValue.ToString() == "0")
                            lookUpEditW_MeterMin.EditValue = 15;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        void DeviceADC_Load(object sender, EventArgs e)
        {
            E_meterCheckedBox = new DataTable();
            ADCIP_LIST = new DataTable();
            //
            LoadDefault();
            //
            setLangThis();
            //
            setDisable();
        }

        void LoadWaterSetup()
        {
            #region Water

            DataTable DTWaterBrand = new DataTable();

            DTWaterBrand.Columns.Add("brand_label", typeof(string));
            DTWaterBrand.Columns.Add("brand_id", typeof(int));

            // Set brand is Drago Connex when Port is not select
            if (lookUpEditPort1.EditValue != null && lookUpEditPort2.EditValue != null)
            {
                if ((int)(lookUpEditPort1.EditValue) == 2 || (int)(lookUpEditPort2.EditValue) == 2)
                {
                    DTWaterBrand.Rows.Add("Drago Connex", 1);
                    DTWaterBrand.Rows.Add("LonGreen", 2);
                }
                else
                {

                    DTWaterBrand.Rows.Add("Drago Connex", 1);
                }
            }
            else
            {
                DTWaterBrand.Rows.Add("Drago Connex", 1);
            }

            lookUpEditWaterBrand.Properties.DataSource = DTWaterBrand;
            lookUpEditWaterBrand.Properties.DisplayMember = "brand_label";
            lookUpEditWaterBrand.Properties.ValueMember = "brand_id";
            lookUpEditWaterBrand.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_brand_select");

            if (lookUpEditWaterBrand.EditValue != null)
            {
                // If Brand is Drago Connex
                if ((int)(lookUpEditWaterBrand.EditValue) == 1)
                {
                    textEditWaterIP.Enabled = true;
                    textEditWaterPort.Enabled = true;

                    textEditWaterPort.EditValue = 500;
                }

            }
            else
            {
                textEditWaterIP.Enabled = false;
                textEditWaterPort.Enabled = false;

                textEditWaterPort.EditValue = 500;
            }

            #endregion
        }

        void LoadDefault()
        {
            #region ADC

            int ADCcount = 1;
            textEditADCName.Text = "ADC_" + ADCcount;

            DataTable DTADCPort1 = new DataTable();

            DTADCPort1.Columns.Add("port_label", typeof(string));
            DTADCPort1.Columns.Add("port_id", typeof(int));

            DTADCPort1.Rows.Add("E-Meter (RS485)", 1);
            DTADCPort1.Rows.Add("W-Meter (RS485)", 2);
            DTADCPort1.Rows.Add("PABX (RS232)", 3);

            lookUpEditPort1.Properties.DataSource = DTADCPort1;
            lookUpEditPort1.Properties.DisplayMember = "port_label";
            lookUpEditPort1.Properties.ValueMember = "port_id";
            lookUpEditPort1.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_select");

            // Defualt Emeter RS485
            lookUpEditPort1.EditValue = 1;

            lookUpEditPort2.Properties.DataSource = DTADCPort1;
            lookUpEditPort2.Properties.DisplayMember = "port_label";
            lookUpEditPort2.Properties.ValueMember = "port_id";
            lookUpEditPort2.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_select");


            // Defualt PHONE RS232
            lookUpEditPort2.EditValue = 3;

            #endregion

            #region PABX Brand

            DataTable DTPABXBrand = new DataTable();

            DTPABXBrand.Columns.Add("brand_label", typeof(string));
            DTPABXBrand.Columns.Add("brand_id", typeof(int));

            DTPABXBrand.Rows.Add("Forth: D64 CID", 1);
            DTPABXBrand.Rows.Add("Forth: D128 CID", 2);
            DTPABXBrand.Rows.Add("NEC: Univerge SV8300", 3);
            DTPABXBrand.Rows.Add("NEC: Univerge SV8100", 4);
            DTPABXBrand.Rows.Add("Phonik: JSD Series", 5);
            DTPABXBrand.Rows.Add("Phonik: Jupiter", 6);
            DTPABXBrand.Rows.Add("Panasonic: KX A291", 7);
            DTPABXBrand.Rows.Add("Panasonic: KX TEB 308", 8);
            DTPABXBrand.Rows.Add("Panasonic: KX TEM 824", 9);

            lookUpEditPABXBrand.Properties.DataSource = DTPABXBrand;
            lookUpEditPABXBrand.Properties.DisplayMember = "brand_label";
            lookUpEditPABXBrand.Properties.ValueMember = "brand_id";
            lookUpEditPABXBrand.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_brand_model_select");

            #endregion

            #region PABX Boud Rate

            DataTable DTPABXBoudRate = new DataTable();

            DTPABXBoudRate.Columns.Add("boud_label", typeof(string));
            DTPABXBoudRate.Columns.Add("boud_id", typeof(int));

            DTPABXBoudRate.Rows.Add("300", 1);
            DTPABXBoudRate.Rows.Add("1200", 2);
            DTPABXBoudRate.Rows.Add("2400", 3);
            DTPABXBoudRate.Rows.Add("4800", 4);
            DTPABXBoudRate.Rows.Add("9600", 5);
            DTPABXBoudRate.Rows.Add("19200", 6);

            lookUpEditPABXBoudRate.Properties.DataSource = DTPABXBoudRate;
            lookUpEditPABXBoudRate.Properties.DisplayMember = "boud_label";
            lookUpEditPABXBoudRate.Properties.ValueMember = "boud_id";

            lookUpEditPABXBoudRate.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_boudrate_select");

            #endregion

            LoadWaterSetup();

            #region Auto Reading

            DataTable DTMeterMinute = new DataTable();

            DTMeterMinute.Columns.Add("minute_label", typeof(string));
            DTMeterMinute.Columns.Add("minute_id", typeof(int));


            DTMeterMinute.Rows.Add("15", 15);
            DTMeterMinute.Rows.Add("30", 30);
            DTMeterMinute.Rows.Add("60", 60);


            lookUpEditE_MeterMin.Properties.DataSource = DTMeterMinute;
            lookUpEditE_MeterMin.Properties.DisplayMember = "minute_label";
            lookUpEditE_MeterMin.Properties.ValueMember = "minute_id";
            lookUpEditE_MeterMin.EditValue = 15;

            lookUpEditW_MeterMin.Properties.DataSource = DTMeterMinute;
            lookUpEditW_MeterMin.Properties.DisplayMember = "minute_label";
            lookUpEditW_MeterMin.Properties.ValueMember = "minute_id";
            lookUpEditW_MeterMin.EditValue = 15;

            timeEditE_MeterAt.EditValue = DateTime.Today.AddHours(10.00);
            timeEditW_MeterAt.EditValue = DateTime.Today.AddHours(10.00);
            timeEditP_MeterAt.EditValue = DateTime.Today.AddHours(10.00);

            checkEditAutoReadP_MeterEveryAt.Checked = true;

            // Emeter
            if (radioGroupEmeter.SelectedIndex == 0)
            {
                lookUpEditE_MeterMin.Enabled = false;
                timeEditE_MeterAt.Enabled = true;
            }
            else
            {
                lookUpEditE_MeterMin.Enabled = true;
                timeEditE_MeterAt.Enabled = false;
            }

            if (radioGroupWater.SelectedIndex == 0)
            {
                lookUpEditW_MeterMin.Enabled = false;
                timeEditW_MeterAt.Enabled = true;
            }
            else
            {
                lookUpEditW_MeterMin.Enabled = true;
                timeEditW_MeterAt.Enabled = false;
            }


            #endregion
            //
            ReloadGridADC();
        }

        void ReloadGridADC()
        {
            #region Grid ADC

            DataTable ADCDT = BusinessLogicBridge.DataStore.listADC();

            DataTable GridList = ((DataTable)gridControlADC.DataSource);

            if (GridList == null)
            {
                E_meterCheckedBox = ADCDT;

                //gridViewADC.
                ADCDT.Columns.Add("grid_meter_check", typeof(bool));
                ADCDT.Columns.Add("adc_no", typeof(string));
                ADCDT.Columns.Add("device_adc_connection_text", typeof(string));
                for (int i = 0; i < ADCDT.Rows.Count; i++)
                {
                    ADCDT.Rows[i]["adc_no"] = i + 1;

                    ADCDT.Rows[i]["grid_meter_check"] = false;

                    string connection = ADCDT.Rows[i]["device_adc_connection"].ToString();

                    if (connection == "False")
                    {
                        ADCDT.Rows[i]["device_adc_connection_text"] = "Fail";
                    }
                    else
                    {
                        ADCDT.Rows[i]["device_adc_connection_text"] = "Pass";
                    }
                }
                ADCDT.AcceptChanges();
                gridControlADC.DataSource = null;
                gridControlADC.DataSource = ADCDT;
            }
            else
            {
                E_meterCheckedBox = ADCDT;

                //gridViewADC.
                ADCDT.Columns.Add("grid_meter_check", typeof(bool));
                ADCDT.Columns.Add("adc_no", typeof(string));
                ADCDT.Columns.Add("device_adc_connection_text", typeof(string));
                for (int i = 0; i < ADCDT.Rows.Count; i++)
                {
                    ADCDT.Rows[i]["adc_no"] = i + 1;

                    ADCDT.Rows[i]["grid_meter_check"] = false;

                    string connection = ADCDT.Rows[i]["device_adc_connection"].ToString();

                    if (connection == "False")
                    {
                        ADCDT.Rows[i]["device_adc_connection_text"] = "Fail";
                    }
                    else
                    {
                        ADCDT.Rows[i]["device_adc_connection_text"] = "Pass";
                    }
                }

                //
                ADCDT.AcceptChanges();
                gridControlADC.DataSource = null;
                gridControlADC.DataSource = ADCDT;
            }

            //if (ADCDT.Rows.Count >= 5) { bttAdd.Enabled = false; } else { bttAdd.Enabled = true; }
            gridViewADC_FocusedRowChanged(null, null);
            #endregion
        }

        private void radioGroupNetworkConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1 = LAN 2
            if (radioGroupNetworkConfig.SelectedIndex == 1)
            {
                textEditADCIP.Enabled = true;
                textEditADCNetmask.Enabled = true;
                textEditADCGateway.Enabled = true;
            }
            else
            {
                textEditADCIP.Enabled = false;
                textEditADCNetmask.Enabled = false;
                textEditADCGateway.Enabled = false;
            }
        }

        private void checkEditConnectPABX_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditConnectPABX.Checked == true)
            {
                panelControlPABX.Enabled = true;
                checkEditPABXDefault.Checked = true;
            }
            else
            {
                panelControlPABX.Enabled = false;
            }
        }

        private void checkEditConnectWater_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditConnectWater.Checked == true)
            {
                panelControlWater.Enabled = true;
                lookUpEditWaterBrand.EditValue = 1;
            }
            else
            {
                panelControlWater.Enabled = false;
            }

        }

        void setEnable()
        {
            groupADC.Enabled = false;
            groupBoxADC.Enabled = true;
            groupBoxPABX.Enabled = true;
            groupBoxWater.Enabled = true;
            groupBoxAutoReading.Enabled = true;

            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            bttAdd.Enabled = false;
            bttDelete.Enabled = false;
            bttEdit.Enabled = false;
        }

        void setDisable()
        {

            groupADC.Enabled = true;
            groupBoxADC.Enabled = false;
            groupBoxPABX.Enabled = false;
            groupBoxWater.Enabled = false;
            groupBoxAutoReading.Enabled = false;

            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            bttAdd.Enabled = true;
            bttDelete.Enabled = true;
            bttEdit.Enabled = true;
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

            if (textEditADCSerial.EditValue.ToString() == "")
            {
                label = labelControlSerialNO.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditADCSerial.Focus();
                    focus = true;
                }
            }
            else
            {
                if (button_event == "Add")
                {
                    //check exist serial on grid
                    DataTable GridList = ((DataTable)gridControlADC.DataSource);
                    string newSerial = textEditADCSerial.EditValue.ToString();
                    string existSerial = string.Empty;
                    //
                    foreach (DataRow dr in GridList.Rows)
                    {
                        existSerial = dr["device_adc_serial"].ToString();
                        //
                        if (newSerial == existSerial)
                        {
                            _ValidateTable.Rows.Add(label, getLanguage("_msg_1046"));
                            if (focus == false)
                            {
                                textEditADCSerial.Focus();
                                focus = true;
                            }
                            break;
                        }
                    }


                    // Check exist Serial on License
                    DataTable SerialList = BusinessLogicBridge.DataStore.getSerialADC();

                    var rADC = SerialList.Select("adc_serial_no_1='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_2='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_3='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_4='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_5='" + textEditADCSerial.EditValue.ToString() + "'");

                    if (rADC.Length == 0)
                    {
                        _ValidateTable.Rows.Add(label, getLanguage("_msg_1085"));
                        if (focus == false)
                        {
                            textEditADCSerial.Focus();
                            focus = true;
                        }
                    }
                }
            }

            if (textEditADCIP.EditValue.ToString() == "")
            {
                label = labelControlIPAddress.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditADCIP.Focus();
                    focus = true;
                }
            }

            if (textEditADCNetmask.EditValue.ToString() == "")
            {
                label = labelControlNetMask.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditADCNetmask.Focus();
                    focus = true;
                }
            }

            if (textEditADCGateway.EditValue.ToString() == "")
            {
                label = labelControlGateway.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditADCGateway.Focus();
                    focus = true;
                }
            }

            if (checkEditConnectWater.Checked == true)
            {
                if (textEditWaterIP.EditValue.ToString() == "")
                {
                    label = labelControlWaterIP.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditWaterIP.Focus();
                        focus = true;
                    }
                }

                if (textEditWaterPort.EditValue.ToString() == "")
                {
                    label = labelControlTCPPort.Text;
                    message = star_notice;
                    _ValidateTable.Rows.Add(label, message);
                    if (focus == false)
                    {
                        textEditWaterPort.Focus();
                        focus = true;
                    }
                }


            }

            return _ValidateTable;
        }

        void LoadDefaultEmpty()
        {

            if (gridControlADC.DataSource == null)
            {

                // Add ADC CASE

                bttEdit.Enabled = false;
                bttSetToADC.Enabled = false;
                bttTestConnection.Enabled = false;

            }
            else
            {

                //int ADCcount = ((DataTable)gridControlADC.DataSource).Rows.Count + 1;
                int nextADC_ID = 1;
                foreach (DataRow dr in ((DataTable)gridControlADC.DataSource).Select("", "device_adc_name"))
                {
                    int i = dr["device_adc_name"].ToString().Split('_')[1].To<int>();
                    //
                    if (i == nextADC_ID)
                        nextADC_ID++;
                    else
                        break;
                }


                textEditADCName.Properties.ReadOnly = true;
                textEditADCName.Text = "ADC_" + nextADC_ID;

                textEditADCSerial.EditValue = "";
                textEditADCSerial.Enabled = true;
                textEditMACAddress.EditValue = "";

                radioGroupNetworkConfig.SelectedIndex = 1;
                radioGroupNetworkConfig.Properties.ReadOnly = true;
                textEditADCIP.EditValue = "";
                textEditADCNetmask.EditValue = "";
                textEditADCGateway.EditValue = "";

                checkEditPortDefault.Checked = true;
                checkEditConnectPABX.Checked = false;
                checkEditConnectWater.Checked = false;
                checkEditPABXDefault.Checked = false;

                radioGroupEmeter.SelectedIndex = 1;
                radioGroupWater.SelectedIndex = 0;

                checkEditAutoReadP_MeterEveryAt.Checked = true;

            }

        }

        public void setLangThis()
        {
            //
            // Button Text
            bttReplace.Text = getLanguage("_replace");
            bttSearch.Text = getLanguage("_search");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            this.bttSetToADC.Text = getLanguage("_set_to_adc");
            this.bttTestConnection.Text = getLanguage("_test_connection");
            //
            //groupADCSetting.Text = getLanguage("_edit");
            //labelControlGateway.Text = getLanguage("_edit");
            //labelControlIPAddress.Text = getLanguage("_edit");
            //labelControlNetMask.Text = getLanguage("_edit");
            //labelControlRequired.Text = getLanguage("_edit");
            //labelControlSerialNO.Text = getLanguage("_edit");
            //labelControlTCPPort.Text = getLanguage("_edit");
            //labelControlTitle.Text = getLanguage("_edit");
            //labelControlWaterIP.Text = getLanguage("_edit");
            //LabelMACAddress.Text = getLanguage("_edit");
            //
            //checkEditAutoReadP_MeterEveryAt.Text = getLanguage("_every_at");
            //checkEditConnectPABX.Text = getLanguage("_connect_with_pabx");
            //checkEditConnectWater.Text = getLanguage("_connect_with_water");
            //checkEditPABXDefault.Text = getLanguage("_use_default_setting");
            //checkEditPortDefault.Text = getLanguage("_use_default_setting");
            //checkEditSelectAll.Text = getLanguage("_use_default_setting");
            //
            //radioGroupEmeter.Text = getLanguage("_edit");
            //radioGroupWater.Text = getLanguage("_edit");
            //radioGroupNetworkConfig.Text = getLanguage("_config_network");


        }

        private void lookUpEditWaterBrand_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditWaterBrand.Text == "Drago Connex")
            {
                textEditWaterIP.Enabled = true;
                textEditWaterPort.Enabled = true;
            }
            else
            {
                textEditWaterIP.Enabled = false;
                textEditWaterPort.Enabled = false;
                //
                textEditWaterIP.EditValue = "";
                textEditWaterPort.EditValue = "";
            }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            // validation sector
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

            try
            {
                if (textEditADC_ID.EditValue.To<int>() == 0)
                {
                    button_event = "Add";
                }

                DataTable AdcDTSetting = new DataTable();

                #region DeClare Column Type of Data Table
                AdcDTSetting.Columns.Add("adc_name", typeof(string));
                AdcDTSetting.Columns.Add("adc_serial", typeof(string));
                AdcDTSetting.Columns.Add("adc_mac_address", typeof(string));
                AdcDTSetting.Columns.Add("adc_port_use_defualt", typeof(bool));
                AdcDTSetting.Columns.Add("adc_port_1", typeof(int));
                AdcDTSetting.Columns.Add("adc_port_2", typeof(int));
                AdcDTSetting.Columns.Add("adc_network_config_type", typeof(int));
                AdcDTSetting.Columns.Add("adc_ipadress", typeof(string));
                AdcDTSetting.Columns.Add("adc_netmask", typeof(string));
                AdcDTSetting.Columns.Add("adc_gateway", typeof(string));
                AdcDTSetting.Columns.Add("adc_pabx_connect", typeof(bool));
                AdcDTSetting.Columns.Add("adc_pabx_brand", typeof(int));
                AdcDTSetting.Columns.Add("adc_pabx_boud", typeof(int));
                AdcDTSetting.Columns.Add("adc_pabx_use_default", typeof(bool));
                AdcDTSetting.Columns.Add("adc_water_connect", typeof(bool));
                AdcDTSetting.Columns.Add("adc_water_brand", typeof(string));
                AdcDTSetting.Columns.Add("adc_water_ipaddress", typeof(string));
                AdcDTSetting.Columns.Add("adc_water_port", typeof(string));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_at", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_at", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_p_meter_every_at", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_time", typeof(string));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_time", typeof(string));
                AdcDTSetting.Columns.Add("adc_auto_p_meter_every_time", typeof(string));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_e_meter_every_at_min", typeof(int));
                AdcDTSetting.Columns.Add("adc_auto_w_meter_every_at_min", typeof(int));
                #endregion

                // E-Meter Checked
                bool E_MeterEveryAt;
                E_MeterEveryAt = radioGroupEmeter.SelectedIndex == 0;

                // W-Meter Checked
                bool W_MeterEveryAt;
                W_MeterEveryAt = radioGroupWater.SelectedIndex == 0;

                // P-Meter Checked
                bool P_MeterEveryAt;
                P_MeterEveryAt = checkEditAutoReadP_MeterEveryAt.Checked;

                // Debug case                 

                AdcDTSetting.Rows.Add(
                    textEditADCName.EditValue.ToString(),
                    textEditADCSerial.EditValue.ToString(),
                    textEditMACAddress.EditValue.ToString(),
                    checkEditPortDefault.Checked,
                    Convert.ToInt16(lookUpEditPort1.EditValue),
                    Convert.ToInt16(lookUpEditPort2.EditValue),
                    radioGroupNetworkConfig.SelectedIndex,
                    textEditADCIP.EditValue.ToString(),
                    textEditADCNetmask.EditValue.ToString(),
                    textEditADCGateway.EditValue.ToString(),
                    checkEditConnectPABX.Checked,
                    Convert.ToInt16(lookUpEditPABXBrand.EditValue),
                    Convert.ToInt16(lookUpEditPABXBoudRate.EditValue),
                    checkEditPABXDefault.Checked,
                    checkEditConnectWater.Checked,
                    Convert.ToInt16(lookUpEditWaterBrand.EditValue),
                    textEditWaterIP.EditValue.ToString(),
                    textEditWaterPort.EditValue.ToString(),
                    E_MeterEveryAt ? 1 : 0,
                    W_MeterEveryAt ? 1 : 0,
                    P_MeterEveryAt ? 1 : 0,
                    E_MeterEveryAt ? timeEditE_MeterAt.Time.TimeOfDay.ToString() : "0",
                    W_MeterEveryAt ? timeEditW_MeterAt.Time.TimeOfDay.ToString() : "0",
                    P_MeterEveryAt ? timeEditP_MeterAt.Time.TimeOfDay.ToString() : "0",
                    E_MeterEveryAt ? 0 : 1,
                    W_MeterEveryAt ? 0 : 1,
                    E_MeterEveryAt ? "0" : lookUpEditE_MeterMin.EditValue.ToString(),
                    W_MeterEveryAt ? "0" : lookUpEditW_MeterMin.EditValue.ToString()
                );
                if (button_event == "Add")
                {
                    BusinessLogicBridge.DataStore.insertADC(AdcDTSetting);
                    // ADD LOG
                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [ADC Create]");
                }
                else
                {
                    BusinessLogicBridge.DataStore.updateADC(AdcDTSetting, Convert.ToInt32(textEditADC_ID.EditValue));
                    // ADD LOG
                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [ADC Update]");
                }
                //
                DXWindowsApplication2.MainForm.initADCConnection();
                //
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                ReloadGridADC();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            setDisable();

        }

        private void bttAdd_Click(object sender, EventArgs e)
        {

            if (gridControlADC.DataSource != null)
            {

                DataTable counter = ((DataTable)gridControlADC.DataSource);

                if (counter.Rows.Count >= 5)
                {

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1084"), getLanguage("_softwarename"));
                    return;
                }

            }
            button_event = "Add";
            setEnable();
            LoadDefaultEmpty();

        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            button_event = "Update";
            setEnable();
            //
            textEditADCName.Properties.ReadOnly = true;
            //
            DataTable SerialList = BusinessLogicBridge.DataStore.getSerialADC();
            //
            var rADC = SerialList.Select("adc_serial_no_1='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_2='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_3='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_4='" + textEditADCSerial.EditValue.ToString() + "' or adc_serial_no_5='" + textEditADCSerial.EditValue.ToString() + "'");
            //
            textEditADCSerial.Enabled = rADC.Length == 0;
        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            // _msg_4018
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4018"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                // Check ADC was used

                int AdcID = Convert.ToInt16(textEditADC_ID.EditValue);

                bool ADC_IN_USED = BusinessLogicBridge.DataStore.checkADCInUsed(AdcID);

                if (ADC_IN_USED == true)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1042"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    BusinessLogicBridge.DataStore.deleteADC(AdcID);
                    // ADD LOG
                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [ADC Delete]");
                    ReloadGridADC();
                    //
                    DXWindowsApplication2.MainForm.initADCConnection();
                }

            }
        }

        private void bttReplace_Click(object sender, EventArgs e)
        {
            DataRow ADC_Replace_Data = utilClass.showPopRePlaceADC(this, textEditADC_ID.EditValue.To<int>(), textEditADCName.EditValue.ToString(), textEditADCIP.EditValue.ToString(), textEditADCSerial.EditValue.ToString());
            ReloadGridADC();
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                setDisable();
                ReloadGridADC();
            }
        }

        static bool ScanPort(IPAddress Address, int Port)
        {
            TcpClient Client = new TcpClient();
            try
            {
                // Attempt to connect to the given address + port
                Client.Connect(Address, Port);

                // This may seem like an avoidable step -- but TcpClient.Close does not
                // actually close the underlying connection
                // http://support.microsoft.com/default.aspx?scid=kb%3Ben-us%3B821625

                NetworkStream ClientStream = Client.GetStream();
                ClientStream.Close();

                // Free the TCPClient resource
                Client.Close();
            }
            catch (SocketException)
            {
                // Assume that a socket exception means the connection failed
                // Client.Connect returns a void (so provides no insights into
                // what it was doing)
                return false;
            }
            return true;
        }

        private DataTable ScanADC()
        {

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            DataTable ADCIP = new DataTable();

            ADCIP.Columns.Add("adc_ip_address", typeof(string));

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 300;

            string[] IpLength;
            string macAddress = string.Empty;
            IPHostEntry Host = new IPHostEntry();
            string ip = "";
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ips in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ips.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (ips.Address.ToString().IndexOf("192.168") > -1)
                            {

                                IpLength = ips.Address.ToString().Split('.');

                                ip = ips.Address.ToString().Remove(ips.Address.ToString().Length - IpLength[3].Length, IpLength[3].Length);

                                string IPFIND = "";
                                for (int i = 1; i < 254; i++)
                                {
                                    IPFIND = ip + i;

                                    PingReply reply = pingSender.Send(IPFIND, timeout, buffer, options);
                                    if (reply.Status == IPStatus.Success)
                                    {
                                        try
                                        {
                                            using (TcpClient TcpScan = new TcpClient())
                                            {
                                                TcpScan.Connect(reply.Address, 1600);
                                            }
                                            ADCIP.Rows.Add(reply.Address.ToString());
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            return ADCIP;
        }

        private void bttSearch_Click(object sender, EventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);

            _scan_lan_ip.RunWorkerAsync();
        }

        private void bttSetToADC_Click(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < E_meterCheckedBox.Rows.Count; i++)
            {
                if ((bool)(E_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4022"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    // Progress Bar.... Loading
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                    _progressbar.RunWorkerAsync();
                }

            }
            else
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                return;
            }

        }

        private void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
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
            if (gridViewADC.RowCount > 0)
            {
                for (int i = 0; i < gridViewADC.RowCount; i++)
                {
                    gridViewADC.Columns[0].View.SetRowCellValue(i, "grid_meter_check", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count = room_check_count + 1;
                    }
                }
            }
        }

        private void bttTestConnection_Click(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < E_meterCheckedBox.Rows.Count; i++)
            {
                if ((bool)(E_meterCheckedBox.Rows[i]["grid_meter_check"]) == true)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                _bw.RunWorkerAsync();
            }
            else
            {

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                return;
            }



        }

    }
}
