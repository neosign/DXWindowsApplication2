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
    public partial class phoneDetail : DevExpress.XtraReports.UI.XtraReport
    {

        public phoneDetail()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(DataTable roomTable, string Building, int roomFrom, int roomTo, DateTime monthFrom, DateTime monthTo)
        {
            DataSet RoomDS = new DataSet();

            DataTable RoomDT = new DataTable();
            DataTable PTransDetailTop = new DataTable("PTransDetailTop");
            DataTable PTransDetailBottom = new DataTable("PTransDetailBottom");
            

            PTransDetailTop     = BusinessLogicBridge.DataStore.getReportPhoneDetailTop(roomFrom, roomTo, monthFrom, monthTo);
            PTransDetailBottom  = BusinessLogicBridge.DataStore.getReportPhoneDetailBottom(roomFrom, roomTo, monthFrom, monthTo);

            int amount1 = 0;
            int amount2 = 0;
            int amount3 = 0;
            int amount4 = 0;
            int amount5 = 0;

            int amountAll = 0;

            double total1 = 0;
            double total2 = 0;
            double total3 = 0;
            double total4 = 0;
            double total5 = 0;

            double totalAll = 0;


            for (int i = 0; i < PTransDetailBottom.Rows.Count; i++) {

                switch (PTransDetailBottom.Rows[i]["call_type"].To<int>()) { 
                    
                    case 1:
                        amount1 += PTransDetailBottom.Rows[i]["num"].To<int>();
                        total1 += PTransDetailBottom.Rows[i]["total"].To<double>();
                        break;
                    case 2:
                        amount2 += PTransDetailBottom.Rows[i]["num"].To<int>();
                        total2 += PTransDetailBottom.Rows[i]["total"].To<double>();
                        break;
                    case 3:
                        amount3 += PTransDetailBottom.Rows[i]["num"].To<int>();
                        total3 += PTransDetailBottom.Rows[i]["total"].To<double>();
                        break;
                    case 4:
                        amount4 += PTransDetailBottom.Rows[i]["num"].To<int>();
                        total4 += PTransDetailBottom.Rows[i]["total"].To<double>();
                        break;
                    case 5:
                        amount5 += PTransDetailBottom.Rows[i]["num"].To<int>();
                        total5 += PTransDetailBottom.Rows[i]["total"].To<double>();
                        break;
                    default:
                        amount5 += 0;
                        total5 += 0;
                        break;
                }

                amountAll += PTransDetailBottom.Rows[i]["num"].To<int>();
                totalAll += PTransDetailBottom.Rows[i]["total"].To<double>();

            }

            xrTableAmount1.Text = amount1.ToString();
            xrTableAmount2.Text = amount2.ToString();
            xrTableAmount3.Text = amount3.ToString();
            xrTableAmount4.Text = amount4.ToString();
            xrTableAmount5.Text = amount5.ToString();

            xrTableTotal1.Text = total1.ToString("N2");
            xrTableTotal2.Text = total2.ToString("N2");
            xrTableTotal3.Text = total3.ToString("N2");
            xrTableTotal4.Text = total4.ToString("N2");
            xrTableTotal5.Text = total5.ToString("N2");

            xrTableAmountAll.Text = amountAll.ToString("N2");   
            xrTableTotalAll.Text = totalAll.ToString("N2");   

            RoomDS.Tables.Add(roomTable);
            RoomDS.Tables.Add(PTransDetailTop);
            RoomDS.Tables.Add(PTransDetailBottom);
            this.DataSource = RoomDS;

            RoomDS.WriteXml(@"C:\phoneDetailSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
