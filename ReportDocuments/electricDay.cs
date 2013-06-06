using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraCharts;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class electricDay : DevExpress.XtraReports.UI.XtraReport
    {

        public electricDay()
        {
            InitializeComponent();

           // this.BeforePrint += new PrintEventHandler(electricDay_BeforePrint);
        }

        public void loopGenDataRow(DataTable roomTable, string Building, int roomFrom, int roomTo, DateTime month)
        {
            DataTable ETransByDayTo = new DataTable();
            DataTable ReportDTTemp = new DataTable();
            DataTable RoomDT = new DataTable();
            
            ETransByDayTo.Columns.Add("day", typeof(string));
            ETransByDayTo.Columns.Add("date", typeof(string));
            ETransByDayTo.Columns.Add("max_unit", typeof(double));

            double TotalUnit = 0;

            xrLabelDatePrint.Text = DateTime.Today.ToString("dd/MM/yyyy H:i:s");

            for (int i = 0; i < roomTable.Rows.Count; i++)
            {
                RoomDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomTable.Rows[i]["room_id"].To<int>());

                ReportDTTemp = BusinessLogicBridge.DataStore.getReportEDailyMaxDate(month, RoomDT.Rows[0]["meter_serial"].ToString());

                for (int j = 0; j < ReportDTTemp.Rows.Count; j++){

                    TotalUnit = DXWindowsApplication2.UserForms.utilClass.CalculateUnitEWMeter(ReportDTTemp.Rows[j]["TotalUnitTo"].To<double>(), ReportDTTemp.Rows[j]["TotalUnitFrom"].To<double>());
                    ETransByDayTo.Rows.Add(ReportDTTemp.Rows[j]["DateLastest"].To<DateTime>().ToString("dd"), ReportDTTemp.Rows[j]["DateLastest"].To<DateTime>().ToString("yyyy-MM-dd"), TotalUnit.ToString("N2"));
                        
                }
            }

            if (ETransByDayTo.Rows.Count > 0)
                xrLabelFromDate.Text = ETransByDayTo.Rows[0]["date"].To<DateTime>().ToString("MMMM yyyy");

            xrLabelMeterModel.Text = RoomDT.Rows[0]["meter_models"].ToString();
            xrLabelMeterSerial.Text = RoomDT.Rows[0]["meter_serial"].ToString();

            xrChart1.DataSource = ETransByDayTo;

            xrChart1.SeriesDataMember = "max_unit";
            xrChart1.SeriesTemplate.ArgumentDataMember = "day";

            xrChart1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "max_unit" });


            DataSet RoomDS = new DataSet();

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(ETransByDayTo);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

           // RoomDS.WriteXml(@"C:\electricDailySchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
