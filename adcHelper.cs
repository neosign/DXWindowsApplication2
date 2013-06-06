using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ADC.Jetbox.Lib;
using System.Threading;

public class adcHelper
{
    DataTable DTPABXBrand = null;
    DataTable DTPABXBoudRate = null;
    DataTable DTADCPort = null;

    public adcHelper()
    {
        DTPABXBrand = new DataTable();
        DTPABXBrand.Columns.Add("brand_label", typeof(string));
        DTPABXBrand.Columns.Add("brand_id", typeof(int));
        //
        DTPABXBrand.Rows.Add("Forth: D64 CID", 1);
        DTPABXBrand.Rows.Add("Forth: D128 CID", 2);
        DTPABXBrand.Rows.Add("NEC: Univerge SV8300", 3);
        DTPABXBrand.Rows.Add("NEC: Univerge SV8100", 4);
        DTPABXBrand.Rows.Add("Phonik: JSD Series", 5);
        DTPABXBrand.Rows.Add("Phonik: Jupiter", 6);
        DTPABXBrand.Rows.Add("Panasonic: KX A291", 7);
        DTPABXBrand.Rows.Add("Panasonic: KX TEB 308", 8);
        DTPABXBrand.Rows.Add("Panasonic: KX TEM 824", 9);
        //
        DTPABXBoudRate = new DataTable();
        DTPABXBoudRate.Columns.Add("boud_label", typeof(string));
        DTPABXBoudRate.Columns.Add("boud_id", typeof(int));
        //
        DTPABXBoudRate.Rows.Add("300", 1);
        DTPABXBoudRate.Rows.Add("1200", 2);
        DTPABXBoudRate.Rows.Add("2400", 3);
        DTPABXBoudRate.Rows.Add("4800", 4);
        DTPABXBoudRate.Rows.Add("9600", 5);
        DTPABXBoudRate.Rows.Add("19200", 6);
        //
        DTADCPort = new DataTable();
        DTADCPort.Columns.Add("port_label", typeof(string));
        DTADCPort.Columns.Add("port_id", typeof(int));
        //
        DTADCPort.Rows.Add("E-Meter (RS485)", 1);
        DTADCPort.Rows.Add("W-Meter (RS485)", 2);
        DTADCPort.Rows.Add("PABX (RS232)", 3);
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    }

    public DataTable GetConfigFromADC(objADCConfig _objADCConfig)
    {
        return MappingToDataTable(_objADCConfig);
    }

    public objADCConfig SetConfigToADC(DataRow dr)
    {
        return MappingToObject(dr);
    }

    public List<objEMeterConfig> MappingToObjEMeterConfig(DataRow[] dr)
    {
        List<objEMeterConfig> list = new List<objEMeterConfig>();
        //
        objEMeterConfig obj = null;
        int port_id;
        //
        foreach (DataRow r in dr)
        {
            obj = new objEMeterConfig();
            obj.ADC_ID = r["device_adc_id"].ToString();

            port_id = Convert.ToInt16(r["meter_port"].ToString().Replace("Port", ""));

            obj.ADC_PortID = port_id ; //Convert.ToInt16(r[""].ToString());
            obj.E_CT = r["meter_ct_ratio"].ToString();
            obj.E_Model = r["meter_models"].ToString();
            obj.E_Name = r["meter_label"].ToString();
            obj.E_Serial_No = r["meter_serial"].ToString();
            //
            list.Add(obj);
        }
        //
        return list;
    }

    public List<objWMeterConfig> MappingToObjWMeterConfig(DataRow[] dr)
    {
        List<objWMeterConfig> list = new List<objWMeterConfig>();
        //
        objWMeterConfig obj = null;
        //
        foreach (DataRow r in dr)
        {
            obj = new objWMeterConfig();
            obj.ADC_ID = r["device_adc_id"].ToString();

            if (r["device_adc_water_brand"].ToString() == "1")
            {
                obj.W_Model = "Drago Connex";//r["meter_models"].ToString();
            }
            else {
                obj.W_Model = "LonGreen";
            }
            
            obj.W_Name = r["meter_label"].ToString();
            obj.W_Serial = r["meter_serial"].ToString();
            //
            list.Add(obj);
        }
        //
        return list;
    }
    
    public objPhoneConfig MappingToObjPhoneConfig(DataRow[] dr)
    {
        objPhoneConfig obj = null;
        int port_id = 0;
        //
        foreach (DataRow r in dr)
        {
            obj = new objPhoneConfig();
            obj.ADC_ID = r["device_adc_id"].ToString();
            
            port_id = Convert.ToInt16(r["meter_port"].ToString().Replace("Port", ""));

            obj.ADC_PortID = port_id;
            obj.Manufacture_ID = r[""].ToString();
            obj.Manufacture_Name = r[""].ToString();
            obj.Model = r[""].ToString();
        }
        //
        return obj;
    }

    DataTable MappingToDataTable(objADCConfig _objADCConfig)
    {
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
        try
        {
            //
            AdcDTSetting.Rows.Add(
                _objADCConfig.ADC_ID,
                _objADCConfig.ADC_Serial,
                "",//macaddress
                null,//default port check
                GetPortTypeID(_objADCConfig.ADCPort1Config.ADC_PortDeviceType, _objADCConfig.ADCPort1Config.ADC_PortType),
                GetPortTypeID(_objADCConfig.ADCPort2Config.ADC_PortDeviceType, _objADCConfig.ADCPort2Config.ADC_PortType),
                0,//radioGroupNetworkConfig.SelectedIndex
                _objADCConfig.ADC_IP,
                _objADCConfig.ADC_Subnetmask,
                _objADCConfig.ADC_Gateway,
                false,//checkEditConnectPABX.Checked
                Convert.ToInt16(GetPABXID(_objADCConfig.ADC_PABX_Name, _objADCConfig.ADC_PABX_Model)),
                0,//PABXBaudrate
                false,//checkEditPABXDefault
                false,//checkEditConnectWater
                _objADCConfig.ADC_DragoIP != "" ? 1 : 0,//water meter brand
                _objADCConfig.ADC_DragoIP,
                _objADCConfig.ADC_DragoPort,
                _objADCConfig.E_Meter_AMR.Trim() != "0",
                _objADCConfig.W_Meter_AMR.Trim() != "0",
                _objADCConfig.PABX_AMR.Trim() != "0",
                GetTimeSpan(_objADCConfig.E_Meter_AMR),
                GetTimeSpan(_objADCConfig.W_Meter_AMR),
                GetTimeSpan(_objADCConfig.PABX_AMR),
                _objADCConfig.E_Meter_Period != "0",
                _objADCConfig.W_Meter_Period != "0",
                ConvertToMinute(_objADCConfig.E_Meter_Period),
                ConvertToMinute(_objADCConfig.W_Meter_Period)
            );
        }
        catch (Exception ex) { 
        
        }
        //
        return AdcDTSetting;
    }

    int ConvertToMinute(string s)
    {
        int i = 0;
        try
        {
            if (s.Trim() != "0" && s.Trim() != "")
                i = Convert.ToInt16(s) / 60;
        }
        catch { }
        //
        return i;
    }

    int ConvertToSecond(string s)
    {
        int i = 0;
        try
        {
            if (s.Trim() != "0" && s.Trim() != "")
                i = Convert.ToInt16(s) * 60;
        }
        catch { }
        //
        return i;
    }

    objADCConfig MappingToObject(DataRow drSetting)
    {
        objADCConfig _objADCConfig = new objADCConfig();
        try
        {
            _objADCConfig.ADC_DataCenterIP = "";
            _objADCConfig.ADC_DataCenterPassword = "";
            _objADCConfig.ADC_DataCenterUserName = "";
            //
            _objADCConfig.ADC_ID = drSetting["adc_name"].ToString();
            _objADCConfig.ADC_Serial = drSetting["adc_serial"].ToString();
            _objADCConfig.ADCPort1Config = GetPortType(Convert.ToInt16(drSetting["adc_port_1"]), 1);
            _objADCConfig.ADCPort2Config = GetPortType(Convert.ToInt16(drSetting["adc_port_2"]), 2);
            //
            _objADCConfig.ADC_IP = drSetting["adc_ipadress"].ToString();
            _objADCConfig.ADC_Subnetmask = drSetting["adc_netmask"].ToString();
            _objADCConfig.ADC_Gateway = drSetting["adc_gateway"].ToString();
            //
            MappingPABX(Convert.ToInt16(drSetting["adc_pabx_brand"]), ref _objADCConfig);
            //
            _objADCConfig.ADC_DragoIP = drSetting["adc_water_ipaddress"].ToString();
            _objADCConfig.ADC_DragoPort = Convert.ToInt16(drSetting["adc_water_port"]);
            _objADCConfig.E_Meter_AMR = drSetting["adc_auto_e_meter_every_time"].ToString();
            _objADCConfig.W_Meter_AMR = drSetting["adc_auto_w_meter_every_time"].ToString();
            _objADCConfig.PABX_AMR = drSetting["adc_auto_p_meter_every_time"].ToString();
            _objADCConfig.E_Meter_Period = ConvertToSecond(drSetting["adc_auto_e_meter_every_at_min"].ToString()).ToString();
            _objADCConfig.W_Meter_Period = ConvertToSecond(drSetting["adc_auto_w_meter_every_at_min"].ToString()).ToString();
            //

            return _objADCConfig;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    objADCPortConfig GetPortType(int id, int port_id)
    {
        string dType = "";
        string pType = "";
        //
        DataRow[] dr = DTADCPort.Select("port_id=" + id);
        if (dr.Length > 0)
        {
            dType = dr[0][0].ToString().Split(' ')[0].Trim();
            pType = dr[0][0].ToString().Split(' ')[1].Trim().Replace("(", "").Replace(")", "").Replace("RS", "RS-");
        }
        //
        objADCPortConfig obj = new objADCPortConfig();
        //
        obj.ADC_PortDeviceType = dType;
        obj.ADC_PortType = pType;
        obj.ADC_PortID = port_id;
        obj.ADC_PortBaudRate = "";
        obj.ADC_PortDatabit = 8;
        obj.ADC_PortParity = "";
        //
        obj.ADC_PortEnable = id == 0 ? 0 : 1;
        //
        return obj;
    }

    int GetPortTypeID(string deviceType, string portType)
    {
        int id = 0;
        //
        //DTADCPort.Rows.Add("E-Meter (RS485)", 1);
        //DTADCPort.Rows.Add("W-Meter (RS485)", 2);
        //DTADCPort.Rows.Add("PABX (RS232)", 3);
        //
        foreach (DataRow dr in DTADCPort.Rows)
        {
            string dType = dr[0].ToString().Split(' ')[0].Trim().ToLower();
            string pType = dr[0].ToString().Split(' ')[1].Trim().ToLower();
            //
            if (pType.Contains(portType.Replace("-", "").ToLower()))
            {
                if (dType.ToLower().Contains(deviceType.ToLower()))
                {
                    id = Convert.ToInt16(dr[1].ToString());
                    break;
                }
            }
        }
        //
        return id;
    }

    TimeSpan GetTimeSpan(string dTime)
    {
        if (dTime == "0" || dTime == "") {

            dTime = "00:00:00";

        }
        return Convert.ToDateTime(dTime).TimeOfDay;
    }

    int GetWaterMeterBrand(string waterBrand)
    {
        if (waterBrand.Trim().ToLower().Contains("drago connex"))
            return 1;
        else if (waterBrand.Trim().ToLower().Contains("longreen"))
            return 2;
        //
        return 0;
    }

    int GetPABXBaudrateID(string pabxBaudrate)
    {
        int id = 0;
        //
        foreach (DataRow dr in DTPABXBoudRate.Rows)
        {
            string baudrate = dr[0].ToString().ToLower();
            //
            if (baudrate == pabxBaudrate)
            {
                id = Convert.ToInt16(dr[1].ToString());
                break;
            }
        }
        //
        return id;
    }

    void MappingPABX(int id, ref objADCConfig obj)
    {
        string brand = "";
        string model = "";
        //
        DataRow[] dr = DTPABXBrand.Select("brand_id=" + id);
        if (dr.Length > 0)
        {
            brand = dr[0][0].ToString().Split(':')[0].Trim();
            model = dr[0][0].ToString().Split(':')[1].Trim();
        }
        //
        obj.ADC_PABX_Name = brand;
        obj.ADC_PABX_Model = model;
    }

    int GetPABXID(string pabxBrand, string pabxModel)
    {
        int id = 0;
        //
        foreach (DataRow dr in DTPABXBrand.Rows)
        {
            string br = dr[0].ToString().Split(':')[0].Trim().ToLower();
            string md = dr[0].ToString().Split(':')[1].Trim().ToLower();
            //
            if (br.Contains(pabxBrand))
            {
                if (md.Contains(pabxModel))
                {
                    id = Convert.ToInt16(dr[1].ToString());
                    break;
                }
            }
        }
        //
        return id;
    }

    public DataTable ConvertEMeterTransaction(DataTable _dt, int adcID)
    {
        DataTable dt = _dt.Copy();
        //
        string sID = "device_adc_id";
        string sDate = "e_datetime";        
        //
        if (!dt.Columns.Contains(sID))
            dt.Columns.Add(new DataColumn(sID, typeof(int), "'" + adcID + "'"));
        //
        if (!dt.Columns.Contains(sDate))
            dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
                "Convert(E_Date + ' ' + E_Time, 'System.DateTime')"));
        //
        string sConnection = "e_connection";
        if (!dt.Columns.Contains(sConnection))
            dt.Columns.Add(new DataColumn(sConnection, typeof(int),
                "Convert(IIF(E_CommStatus like '%complete',1,0), 'System.Int16')"));
        //
        foreach (DataRow r in dt.Rows)
        {
            foreach(DataColumn c in dt.Columns)
                if(r[c.ColumnName].ToString().Contains("null"))
                    r[c.ColumnName] = "0";
        }
        dt.AcceptChanges();
        //
        return dt;
    }

    public DataTable ConvertEMeterTransaction2(DataTable _dt, int adcID)
    {
        DataTable dt = _dt.Copy();
        //
        string sID = "device_adc_id";
        string sDate = "e_datetime";
        
        //
        if (!dt.Columns.Contains(sID))
            dt.Columns.Add(new DataColumn(sID, typeof(int), "'" + adcID + "'"));
        //
        if (!dt.Columns.Contains(sDate))
            dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
                "Convert(E_Date + ' ' + E_Time, 'System.DateTime')"));
        //
        string sConnection = "e_connection";
        if (!dt.Columns.Contains(sConnection))
            dt.Columns.Add(new DataColumn(sConnection, typeof(int),
                "Convert(IIF(E_CommStatus like '%complete',1,0), 'System.Int16')"));
        //
        foreach (DataRow r in dt.Rows)
        {
            foreach (DataColumn c in dt.Columns)
            {
                if (r[c.ColumnName].ToString().Contains("null"))
                    r[c.ColumnName] = "0";

                DataTable ETransDT = DXWindowsApplication2.BusinessLogicBridge.DataStore.CheckEMeterExistBySerial(r["E_Serial_No"].ToString());

                if (ETransDT.Rows.Count > 0)
                {
                    r["E_MeterID"] = ETransDT.Rows[0]["meter_id"];
                }
                else {
                    r["E_MeterID"] = 0;
                }

            }
        }
        dt.AcceptChanges();
        //
        return dt;
    }

    public DataTable ConvertWMeterTransaction(DataTable _dt, int adcID)
    {
        DataTable dt = _dt.Copy();
        //
        string sID = "device_adc_id";
        string sDate = "w_datetime";
        //
        if (!dt.Columns.Contains(sID))
            dt.Columns.Add(new DataColumn(sID, typeof(int), "'" + adcID + "'"));
        //
        //if (!dt.Columns.Contains(sDate))
            //dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
            //    "Convert(w_date + ' ' + w_time, 'System.DateTime')"));

        //foreach (DataRow dr in dt.Rows) {
        //    dr["W_Date"] = dr["W_Date"].To<DateTime>().ToString("yyyy-MM-dd");
        //}

        if (!dt.Columns.Contains(sDate))
            dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
            "Convert(w_date, 'System.DateTime')"));


        //
        string sConnection = "w_connection";
        if (!dt.Columns.Contains(sConnection))
            dt.Columns.Add(new DataColumn(sConnection, typeof(int),
                "Convert(IIF(W_Communication like '%complete',1,0), 'System.Int16')"));
        //
        foreach (DataRow r in dt.Rows)
        {
            foreach (DataColumn c in dt.Columns)
                if (r[c.ColumnName].ToString().Contains("null"))
                    r[c.ColumnName] = "0";
        }
        dt.AcceptChanges();
        //
        return dt;
    }

    public DataTable ConvertWMeterTransaction2(DataTable _dt, int adcID)
    {
        DataTable dt = _dt.Copy();
        //
        string sID = "device_adc_id";
        string sDate = "w_datetime";
        //
        if (!dt.Columns.Contains(sID))
            dt.Columns.Add(new DataColumn(sID, typeof(int), "'" + adcID + "'"));
        //
        if (!dt.Columns.Contains(sDate))
            dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
                "Convert(w_date + ' ' + w_time, 'System.DateTime')"));
        //
        string sConnection = "w_connection";
        if (!dt.Columns.Contains(sConnection))
            dt.Columns.Add(new DataColumn(sConnection, typeof(int),
                "Convert(IIF(W_Communication like '%complete',1,0), 'System.Int16')"));
        //
        foreach (DataRow r in dt.Rows)
        {
            foreach (DataColumn c in dt.Columns)
                if (r[c.ColumnName].ToString().Contains("null"))
                    r[c.ColumnName] = "0";

            DataTable ETransDT = DXWindowsApplication2.BusinessLogicBridge.DataStore.CheckWMeterExistBySerial(r["W_Serial"].ToString());

            if (ETransDT.Rows.Count > 0)
            {
                r["W_ID"] = ETransDT.Rows[0]["water_id"];
            }
            else
            {
                r["W_ID"] = 0;
            }
        }
        dt.AcceptChanges();
        //
        return dt;
    }

    public DataTable ConvertPhoneTransaction(DataTable _dt, int adcID)
    {
        DataTable dt = _dt.Copy();
        //
        string sID = "device_adc_id";
        string sDate = "Start_DateTime";
        string eDate = "End_DateTime";
        //
        if (!dt.Columns.Contains(sID))
            dt.Columns.Add(new DataColumn(sID, typeof(int), "'" + adcID + "'"));
        //
        if (!dt.Columns.Contains(sDate))
            dt.Columns.Add(new DataColumn(sDate, typeof(DateTime),
            "Convert(Start_Date + ' ' + Start_Time, 'System.DateTime')"));
        //
        if (!dt.Columns.Contains(eDate))
            dt.Columns.Add(new DataColumn(eDate, typeof(DateTime),
            "Convert(End_Date + ' ' + End_Time, 'System.DateTime')"));
        //
        foreach (DataRow r in dt.Rows)
        {
            foreach (DataColumn c in dt.Columns)
                if (r[c.ColumnName].ToString().Contains("null"))
                    r[c.ColumnName] = "0";

            DataTable PTransDT = DXWindowsApplication2.BusinessLogicBridge.DataStore.CheckPhoneExistByNo(r["Caller_ID"].ToString());

            if (PTransDT.Rows.Count > 0)
            {
                r["Call_ID"] = PTransDT.Rows[0]["phone_id"];
            }
            else
            {
                r["Call_ID"] = 0;
            }
        }
        dt.AcceptChanges();
        //
        return dt;
    }



}
