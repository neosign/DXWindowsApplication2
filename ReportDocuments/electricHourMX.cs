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
    public partial class electricHourMX : DevExpress.XtraReports.UI.XtraReport
    {

        public electricHourMX()
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

        public void loopGenDataRow(DataTable roomTable, string Building, int roomFrom, int roomTo, DateTime day)
        {
            DataTable ETransByDay = new DataTable();
            DataTable ReportDTTemp = new DataTable();
            DataTable RoomDT = new DataTable();

            xrLabelDatePrint.Text = DateTime.Today.ToString("dd/MM/yyyy H:i:s");

            ETransByDay.Columns.Add("time", typeof(string));
            ETransByDay.Columns.Add("unit", typeof(string));
            ETransByDay.Columns.Add("current_a", typeof(double));
            ETransByDay.Columns.Add("current_b", typeof(double));
            ETransByDay.Columns.Add("current_c", typeof(double));
            ETransByDay.Columns.Add("current_n", typeof(double));
            

            for (int i = 0; i < roomTable.Rows.Count; i++)
            {
                RoomDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomTable.Rows[i]["room_id"].To<int>());

                ReportDTTemp = BusinessLogicBridge.DataStore.getReportEHourMX(day, RoomDT.Rows[0]["meter_serial"].ToString());

                for (int j = 0; j < ReportDTTemp.Rows.Count; j++){

                    ETransByDay.Rows.Add(ReportDTTemp.Rows[j]["TimeRecord"].ToString(), ReportDTTemp.Rows[j]["TotalUnit"].ToString(), ReportDTTemp.Rows[j]["Current_A"].ToString(), ReportDTTemp.Rows[j]["Current_B"].ToString(), ReportDTTemp.Rows[j]["Current_C"].ToString(), ReportDTTemp.Rows[j]["Current_N"].ToString());       
                }
            }

            if (ETransByDay.Rows.Count>0)
                xrLabelFromDate.Text = day.ToString("MMMM yyyy");

            xrLabelMeterModel.Text = RoomDT.Rows[0]["meter_models"].ToString();
            xrLabelMeterSerial.Text = RoomDT.Rows[0]["meter_serial"].ToString();

            DataSet RoomDS = new DataSet();

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(ETransByDay);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

            RoomDS.WriteXml(@"C:\electricHourMXSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
