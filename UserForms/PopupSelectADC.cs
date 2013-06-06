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
    public partial class PopupSelectADC : uFormBase
    {
        private readonly BackgroundWorker _progressbar = new BackgroundWorker();

        public int adc_id = 0;
        public string adc_name = "";
        public string adc_ip = "";
        public string adc_serial = "";

        public PopupSelectADC()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupSelectTenant_Load);
            //
            this.gridView1.RowClick +=new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView1_RowClick);
            _progressbar.DoWork += new DoWorkEventHandler(_progressbar_DoWork);
            _progressbar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_progressbar_RunWorkerCompleted);
            _progressbar.WorkerReportsProgress = false;
            //
        }

        void _progressbar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3022"), getLanguage("_softwarename"),"info");

            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this.Owner.ActiveControl);
            this.DialogResult = DialogResult.OK;
        }

        void _progressbar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int _SetingSetting = 0;

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

                DataTable ADC_INFO = BusinessLogicBridge.DataStore.selectADC(Convert.ToInt32(drADC["device_adc_id"]));

                AdcDTSetting.Rows.Add(
                    adc_name,
                    adc_serial,
                    "",
                    (bool)(ADC_INFO.Rows[0]["device_adc_port_checked"]),
                    Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_port1"]),
                    Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_port2"]),
                    Convert.ToInt16(ADC_INFO.Rows[0]["device_adc_network_config_type"]),
                    adc_ip,
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

                ADC.Jetbox.Lib.objADCConfig ConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.SetConfigToADC(AdcDTSetting.Rows[0]);
                _SetingSetting = DXWindowsApplication2.MainForm.dictADC["adc_id_" + adc_id].SetADCConfig(ConfigInfo);

                if (_SetingSetting == 0)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1078"), getLanguage("_softwarename"));
                    return;
                }
                else {
                    BusinessLogicBridge.DataStore.updateADC(AdcDTSetting, adc_id);
                    BusinessLogicBridge.DataStore.ChangeADCAllMeter(drADC["device_adc_id"].To<int>(), adc_id);
                    BusinessLogicBridge.DataStore.deleteADC(drADC["device_adc_id"].To<int>());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        public DataRow drADC = null;

        public override void Refresh()
        {
            base.Refresh();
            //
            initADC(adc_name);
            setLangThis();
        }

        void PopupSelectTenant_Load(object sender, EventArgs e)
        {
            initADC(adc_name);
            setLangThis();
        }
        
        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            drADC = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            //
        }

        void initADC(string adc_name)
        {
            // Select ADC From Database
            DataTable ADCList = BusinessLogicBridge.DataStore.getADCExcludeName(adc_name);
            gridControlTenant.DataSource = ADCList;

        }

        public void setLangThis()
        {
            //
            //this.Name = getLanguage("_tenant_info");
            ////
            //this.groupControlTenantList.Text = getLanguage("_tenant");
            //
            // Grid
            //this.tenant_name.Caption = getLanguage("_firstname");
            //this.tenant_surname.Caption = getLanguage("_lastname");
            //this.tenant_status_label.Caption = getLanguage("_status");
            //this.tenant_status_label.FieldName = "tenant_status_" + current_lang + "_label";
        }

        private void bttOk_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4023"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                // Check Connection Between new ADC and PC By IP PORT 1600
                bool ADC_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + adc_id].TestADCConnection();

                if (ADC_Status)
                {
                    // Waiting progess bar
                    // replace set to ADC                    
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this.Owner.ActiveControl);
                    _progressbar.RunWorkerAsync();
                }
                else {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }

            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

