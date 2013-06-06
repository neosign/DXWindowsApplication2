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
    public partial class electricHourUnit : DevExpress.XtraReports.UI.XtraReport
    {

        public electricHourUnit()
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
            DataTable ChartDT = new DataTable();
            DataTable ReportDTTemp = new DataTable();
            DataTable RoomDT = new DataTable();

            double total = 0;

            ETransByDay.Columns.Add("hour_num", typeof(string));
            ETransByDay.Columns.Add("hour_range", typeof(string));
            ETransByDay.Columns.Add("total", typeof(string));

            ChartDT.Columns.Add("hour_num", typeof(string));
            ChartDT.Columns.Add("hour_range", typeof(string));
            ChartDT.Columns.Add("total", typeof(double));

            xrLabelDatePrint.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            for (int k = 0; k < 24; k++)
            {
                ETransByDay.Rows.Add((k + 1).ToString(), (k).ToString().PadLeft(2, '0') + ":01-" + (k + 1).ToString().PadLeft(2, '0') + ":00", "N/A");
            }

            for (int i = 0; i < roomTable.Rows.Count; i++)
            {
                RoomDT = BusinessLogicBridge.DataStore.getE_EndValueByRoomID(roomTable.Rows[i]["room_id"].To<int>());

                ReportDTTemp = BusinessLogicBridge.DataStore.getReportEHourlyUnit(day, RoomDT.Rows[0]["meter_serial"].ToString());

                if (ReportDTTemp.Rows.Count > 0)
                {
                    for (int j = 0; j < ReportDTTemp.Rows.Count; j++)
                    {
                        total = DXWindowsApplication2.UserForms.utilClass.CalculateUnitEWMeter(ReportDTTemp.Rows[j]["maxTo"].To<double>(), ReportDTTemp.Rows[j]["maxFrom"].To<double>());

                        for (int k = 0; k < ETransByDay.Rows.Count; k++)
                        {
                            if (ETransByDay.Rows[k]["hour_num"].To<int>() == ReportDTTemp.Rows[j]["hour_num"].To<int>())
                            {
                                ETransByDay.Rows[k]["total"] = total.To<double>();
                            }
                        }
                    }
                }
                ETransByDay.AcceptChanges();
            }
            xrLabelFromDate.Text = day.ToString("MMMM yyyy");

            xrLabelMeterModel.Text = RoomDT.Rows[0]["meter_models"].ToString();
            xrLabelMeterSerial.Text = RoomDT.Rows[0]["meter_serial"].ToString();

            for (int y = 0; y < ETransByDay.Rows.Count; y++)
            {
                ChartDT.Rows.Add(ETransByDay.Rows[y]["hour_num"].ToString(), ETransByDay.Rows[y]["hour_range"].ToString(), ETransByDay.Rows[y]["total"].To<double>());
            }

            xrChart1.DataSource = ChartDT;

            xrChart1.SeriesDataMember = "hour_num";
            xrChart1.SeriesTemplate.ArgumentDataMember = "hour_num";

            xrChart1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "total" });


            DataSet RoomDS = new DataSet();

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(ETransByDay);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

            //RoomDS.WriteXml(@"C:\electricUnitHourSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
