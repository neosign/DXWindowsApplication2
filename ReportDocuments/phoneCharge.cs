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
    public partial class phoneCharge : DevExpress.XtraReports.UI.XtraReport
    {

        public phoneCharge()
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
            PTransChargeUse.Columns.Add("amount2", typeof(string));
            PTransChargeUse.Columns.Add("amount3", typeof(string));
            PTransChargeUse.Columns.Add("total", typeof(string));
            
            xrLabelDatePrint.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            PTransCharge = BusinessLogicBridge.DataStore.getReportPhoneCharge(roomFrom, roomTo, monthFrom, monthTo);

            for (int i = 0; i < PTransCharge.Rows.Count; i++ )
            {
                PTransChargeUse.Rows.Add(PTransCharge.Rows[i]["room_label"].ToString(), PTransCharge.Rows[i]["phone_label"].ToString(), PTransCharge.Rows[i]["amount1"].To<double>().ToString("N2"), PTransCharge.Rows[i]["amount2"].To<double>().ToString("N2"), PTransCharge.Rows[i]["amount3"].To<double>().ToString("N2"), PTransCharge.Rows[i]["total"].To<double>().ToString("N2"));
            }

            double sumInArea = 0;
            double sumMobile = 0;
            double sumInCountry = 0;
            double sumTotal = 0;

            for (int i = 0; i < PTransCharge.Rows.Count; i++ )
            {
                sumInArea += PTransCharge.Rows[i]["amount1"].To<double>();
                sumMobile += PTransCharge.Rows[i]["amount2"].To<double>();
                sumInCountry += PTransCharge.Rows[i]["amount3"].To<double>();
                sumTotal += PTransCharge.Rows[i]["total"].To<double>();
            }

            xrTableSumInArea.Text = sumInArea.ToString("N2");
            xrTableSumMobile.Text = sumMobile.ToString("N2");
            xrTableSumInCountry.Text = sumInCountry.ToString("N2");
            xrTableSumTotal.Text = sumTotal.ToString("N2");

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(PTransChargeUse);

            //RoomDS.Tables.AddRange(roomTable);

            this.DataSource = RoomDS;

            RoomDS.WriteXml(@"C:\phoneChargeSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
