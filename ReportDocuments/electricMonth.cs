using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraCharts;
using System.Globalization;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class electricMonth : DevExpress.XtraReports.UI.XtraReport
    {

        public electricMonth()
        {
            InitializeComponent();

           // this.BeforePrint += new PrintEventHandler(electricDay_BeforePrint);
        }

        void electricDay_BeforePrint(object sender, PrintEventArgs e)
        {
           // this.Detail1.Controls.Add(CreateXRTable());
        }

        public XRTable CreateXRTable()
        {
            int cellsInRow = 3;
            int rowsCount = 31;
            float rowHeight = 100f;

            XRTable table = new XRTable();
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.BeginInit();

            for (int i = 0; i < rowsCount; i++)
            {
                XRTableRow row = new XRTableRow();
                row.HeightF = rowHeight;
                for (int j = 0; j < cellsInRow; j++)
                {
                    XRTableCell cell = new XRTableCell();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            table.EndInit();
            return table;
        }

        void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
        }

        public string findMonthName(int month) { 
            string strMonthName ="";
            
            switch(month)
            { 
                case 1 : strMonthName="มกราคม"; break;
                case 2 : strMonthName="กุมภาพันธ์"; break;
                case 3 : strMonthName="มีนาคม"; break;
                case 4 : strMonthName="เมษายน"; break;
                case 5 : strMonthName="พฤษภาคม"; break;
                case 6 : strMonthName="มิถุนายน"; break;
                case 7 : strMonthName="กรกฎาคม"; break;
                case 8 : strMonthName="สิงหาคม"; break;
                case 9 : strMonthName="กันยายน"; break;
                case 10 : strMonthName="ตุลาคม"; break;
                case 11 : strMonthName="พฤศจิกายน"; break;
                case 12 : strMonthName="ธันวาคม"; break;
            }

            return strMonthName;
        }

        public void loopGenDataRow(DataTable roomTable, string Building, int roomFrom, int roomTo, DateTime monthFrom, DateTime monthTo)
        {
            DataTable ETransByMonth = new DataTable();
            DataTable ReportDTTo = new DataTable();
            DataTable RoomDT = new DataTable();
            
            ETransByMonth.Columns.Add("month_list", typeof(string));
            ETransByMonth.Columns.Add("sum_total", typeof(double));


            double sum_total = 0;
            xrLabelDatePrint.Text = DateTime.Today.ToString("dd/MM/yyyy H:i:s");

            string strMonthName = "";
            string startFromMonth ="";
            string startToMonth ="";

            double total = 0;
            for (int i = 0; i < roomTable.Rows.Count; i++)
            {
                RoomDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomTable.Rows[i]["room_id"].To<int>());

                ReportDTTo = BusinessLogicBridge.DataStore.getE_ReportMaxMinKwh(RoomDT.Rows[0]["meter_serial"].ToString(), monthFrom);
                
                if (ReportDTTo.Rows.Count > 0)
                {
                    startFromMonth = findMonthName(ReportDTTo.Rows[0]["month_name"].To<int>());
                    startToMonth = findMonthName(ReportDTTo.Rows[ReportDTTo.Rows.Count - 1]["month_name"].To<int>());

                    for (int j = 0; j < ReportDTTo.Rows.Count; j++)
                    {

                        switch (ReportDTTo.Rows[j]["month_name"].To<int>())
                        { 
                            case 1 : strMonthName="มกราคม"; break;
                            case 2 : strMonthName="กุมภาพันธ์"; break;
                            case 3 : strMonthName="มีนาคม"; break;
                            case 4 : strMonthName="เมษายน"; break;
                            case 5 : strMonthName="พฤษภาคม"; break;
                            case 6 : strMonthName="มิถุนายน"; break;
                            case 7 : strMonthName="กรกฎาคม"; break;
                            case 8 : strMonthName="สิงหาคม"; break;
                            case 9 : strMonthName="กันยายน"; break;
                            case 10 : strMonthName="ตุลาคม"; break;
                            case 11 : strMonthName="พฤศจิกายน"; break;
                            case 12 : strMonthName="ธันวาคม"; break;
                        }

                        total = DXWindowsApplication2.UserForms.utilClass.CalculateUnitEWMeter(ReportDTTo.Rows[j]["max_val"].To<double>(), ReportDTTo.Rows[j]["min_val"].To<double>());
                        // strMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ReportDTTemp.Rows[j]["month_name"].To<int>());
                        ETransByMonth.Rows.Add(strMonthName, total);
                    }
                
                }
            }

            xrLabelFromDate.Text = startFromMonth;
            xrLabelFromTo.Text = startToMonth;

            xrLabelMeterModel.Text = RoomDT.Rows[0]["meter_models"].ToString();
            xrLabelMeterSerial.Text = RoomDT.Rows[0]["meter_serial"].ToString();

            xrChart1.DataSource = ETransByMonth;

            xrChart1.SeriesDataMember = "sum_total";
            xrChart1.SeriesTemplate.ArgumentDataMember = "month_list";

            xrChart1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "sum_total" });


            DataSet RoomDS = new DataSet();

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(ETransByMonth);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

            //RoomDS.WriteXml(@"C:\electricMonthSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
