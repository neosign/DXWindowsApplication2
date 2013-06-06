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
    public partial class phoneConsumation : DevExpress.XtraReports.UI.XtraReport
    {

        public phoneConsumation()
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


        public void loopGenDataRow(DataTable roomTable, string Building, int roomFrom, int roomTo, DateTime monthFrom, DateTime monthTo)
        {

            xrLabelFromDate.Text = monthFrom.ToString("dd MMM yyyy") + " 00:00";
            xrLabelFromTo.Text = monthTo.ToString("dd MMM yyyy") + " 23:59";


            DataSet RoomDS = new DataSet();

            DataTable RoomDT = new DataTable();
            DataTable PTransCharge = new DataTable();

            DataTable PTransChargeUse = new DataTable();

            PTransChargeUse.Columns.Add("room_label", typeof(string));
            PTransChargeUse.Columns.Add("phone_number", typeof(string));
            PTransChargeUse.Columns.Add("amount1", typeof(string));
            PTransChargeUse.Columns.Add("total1", typeof(string));
            PTransChargeUse.Columns.Add("amount2", typeof(string));
            PTransChargeUse.Columns.Add("total2", typeof(string));
            PTransChargeUse.Columns.Add("amount3", typeof(string));
            PTransChargeUse.Columns.Add("total3", typeof(string));
            PTransChargeUse.Columns.Add("amount4", typeof(string));
            PTransChargeUse.Columns.Add("total4", typeof(string));
            PTransChargeUse.Columns.Add("sum_amount", typeof(string));
            PTransChargeUse.Columns.Add("total", typeof(string));

            xrLabelDatePrint.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            PTransCharge = BusinessLogicBridge.DataStore.getReportPhoneUsing(roomFrom, roomTo, monthFrom, monthTo);


            for (int i = 0; i < PTransCharge.Rows.Count; i++)
            {
                PTransChargeUse.Rows.Add(PTransCharge.Rows[i]["room_label"].ToString(), PTransCharge.Rows[i]["phone_label"].ToString(), PTransCharge.Rows[i]["amount1"].To<double>().ToString("N2"), PTransCharge.Rows[i]["total1"].ToString(), PTransCharge.Rows[i]["amount2"].To<double>().ToString("N2"),PTransCharge.Rows[i]["total2"].ToString(), PTransCharge.Rows[i]["amount3"].To<double>().ToString("N2"), PTransCharge.Rows[i]["total3"].ToString(),PTransCharge.Rows[i]["amount4"].To<double>().ToString("N2"), PTransCharge.Rows[i]["total4"].ToString(), PTransCharge.Rows[i]["sum_amount"].To<double>().ToString("N2"),PTransCharge.Rows[i]["total"].ToString());
            }

            double sumInArea = 0;
            double sumMobile = 0;
            double sumInCountry = 0;
            double sumOutCountry = 0;
            double sumTotal = 0;
            double sumAmount = 0;

            TimeSpan span_total1 = new TimeSpan();
            TimeSpan span_total2 = new TimeSpan();
            TimeSpan span_total3 = new TimeSpan();
            TimeSpan span_total4 = new TimeSpan();
            TimeSpan span_total = new TimeSpan();


            TimeSpan span_totalx1 = new TimeSpan();
            TimeSpan span_totalx2 = new TimeSpan();
            TimeSpan span_totalx3 = new TimeSpan();
            TimeSpan span_totalx4 = new TimeSpan();
            TimeSpan span_totalx  = new TimeSpan();
            

            for (int i = 0; i < PTransCharge.Rows.Count; i++)
            {
                sumInArea += PTransCharge.Rows[i]["amount1"].To<double>();

                if (TimeSpan.TryParse(PTransCharge.Rows[i]["total1"].ToString(), out span_totalx1)){
                    span_total1 += span_totalx1;
                }

                sumMobile += PTransCharge.Rows[i]["amount2"].To<double>();

                if (TimeSpan.TryParse(PTransCharge.Rows[i]["total2"].ToString(), out span_totalx2))
                    span_total2 += span_totalx2;

                sumInCountry += PTransCharge.Rows[i]["amount3"].To<double>();

                if (TimeSpan.TryParse(PTransCharge.Rows[i]["total3"].ToString(), out span_totalx3))
                    span_total3 += span_totalx3;


                sumOutCountry += PTransCharge.Rows[i]["amount4"].To<double>();

                if (TimeSpan.TryParse(PTransCharge.Rows[i]["total4"].ToString(), out span_totalx4))
                    span_total4 += span_totalx4;

                if (TimeSpan.TryParse(PTransCharge.Rows[i]["total"].ToString(), out span_totalx))
                    span_total += span_totalx;

                sumAmount += PTransCharge.Rows[i]["sum_amount"].To<double>();
            }

            DateTime Timespan_total1 = new DateTime(span_total1.Ticks);
            DateTime Timespan_total2 = new DateTime(span_total2.Ticks);
            DateTime Timespan_total3 = new DateTime(span_total3.Ticks);
            DateTime Timespan_total4 = new DateTime(span_total4.Ticks);

            DateTime TimesumTotal = new DateTime(span_total.Ticks);


            string time1 = Timespan_total1.ToString("HH:mm:ss");
            string time2 = Timespan_total2.ToString("HH:mm:ss");
            string time3 = Timespan_total3.ToString("HH:mm:ss");
            string time4 = Timespan_total4.ToString("HH:mm:ss");
            string timetotal = TimesumTotal.ToString("HH:mm:ss");


            xrTableAmount1.Text = sumInArea.ToString("N2");
            xrTableAmount2.Text = sumMobile.ToString("N2");
            xrTableAmount3.Text = sumInCountry.ToString("N2");
            xrTableAmount4.Text = sumOutCountry.ToString("N2");
            xrTableSumTotal1.Text = time1;
            xrTableSumTotal2.Text = time2;
            xrTableSumTotal3.Text = time3;
            xrTableSumTotal4.Text = time4;
            xrTableSumAmount.Text = sumAmount.ToString("N2");
            xrTableSumTotal.Text = timetotal;

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(PTransChargeUse);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

            RoomDS.WriteXml(@"C:\phoneUsingSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
