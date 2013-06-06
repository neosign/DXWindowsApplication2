using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class waterAccumulate : DevExpress.XtraReports.UI.XtraReport
    {

        public waterAccumulate()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(DataTable roomTable, string Building, string roomFrom, string roomTo)
        {

            xrLabelDatePrint.Text = "พิมพ์วันที่   "+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            xrLabelFromDate.Text = roomTable.Rows[0]["report_beginRecord"].ToString();
            xrLabelToDate.Text = roomTable.Rows[0]["report_endRecord"].ToString();

            xrLabelBuilding.Text = Building;

            xrLabelRoomFrom.Text = roomFrom;
            xrLabelRoomTo.Text = roomTo;

            DataSet RoomDS = new DataSet();

            double totalUnit = 0;
            for (int i = 0; i < roomTable.Rows.Count; i++)
            {
                // from

                if (roomTable.Rows[i]["report_totalUnit"].ToString() == "N/A")
                {
                    totalUnit += 0.00;
                }
                else {
                    totalUnit += roomTable.Rows[i]["report_totalUnit"].To<double>();
                }
            }

            xrTableTotal.Text = totalUnit.ToString("N2");


            RoomDS.Tables.Add(roomTable);
            this.DataSource = RoomDS;

            //RoomDS.WriteXml(@"C:\waterAccumulateSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
