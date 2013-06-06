using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class tenant : DevExpress.XtraReports.UI.XtraReport
    {

        public tenant()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(DataTable roomTable, string Building, string roomFrom, string roomTo)
        {

            xrLabelDatePrint.Text = "พิมพ์วันที่   "+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            xrLabelBuilding.Text = Building;

            xrLabelRoomFrom.Text = roomFrom;
            xrLabelRoomTo.Text = roomTo;

            DataSet RoomDS = new DataSet();
            DataTable x = new DataTable();

            x.Columns.Add("room_code", typeof(string));
            x.Columns.Add("room_label", typeof(string));
            x.Columns.Add("fullname", typeof(string));
            x.Columns.Add("tenant_phone", typeof(string));
            x.Columns.Add("tenant_mobile", typeof(string));
            x.Columns.Add("tenant_created_date", typeof(DateTime));
            x.Columns.Add("tenant_status", typeof(string));
           
            try
            {
                DataTable ItemList = new DataTable();

                for (int i = 0; i < roomTable.Rows.Count; i++)
                {
                    x.Rows.Add(roomTable.Rows[i]["coderef"], roomTable.Rows[i]["room_label"], roomTable.Rows[i]["tenant_name"].ToString() + " " + roomTable.Rows[i]["tenant_surname"].ToString(), roomTable.Rows[i]["tenant_phone"].ToString(), roomTable.Rows[i]["tenant_mobile"].ToString(), roomTable.Rows[i]["tenant_created_date"], roomTable.Rows[i]["tenant_status_label"]);
                }
            }
            catch(Exception ex) {

            }

            RoomDS.Tables.Add(x);
            this.DataSource = RoomDS;

            //RoomDS.WriteXml(@"C:\tenantSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
