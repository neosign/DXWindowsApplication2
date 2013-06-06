using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class history_log : DevExpress.XtraReports.UI.XtraReport
    {

        public history_log()
        {
            InitializeComponent();

        }

        public void loopGenDataRow(DataTable LogInfo, DateTime start, DateTime end)
        {
            DataSet LogDS = new DataSet();

            try
            {

                xrLabelCreateDate.Text = String.Format("{0:dd/MM/yyyy}",DateTime.Now);
                xrLabelDueStart.Text = String.Format("{0:dd/MM/yyyy}", start);
                xrLabelDueTo.Text = String.Format("{0:dd/MM/yyyy}", end);

                DataTable ItemDT = new DataTable();

                ItemDT.Columns.Add("log_id", typeof(int));
                ItemDT.Columns.Add("username", typeof(string));
                ItemDT.Columns.Add("name", typeof(string));
                ItemDT.Columns.Add("group_name", typeof(string));
                ItemDT.Columns.Add("date", typeof(string));
                ItemDT.Columns.Add("time", typeof(string));
                ItemDT.Columns.Add("action", typeof(string));

                DataSet InvioceDS = new DataSet();

                int countOrder = 1;

                for (int i = 0; i < LogInfo.Rows.Count; i++)
                {
                    ItemDT.Rows.Add(countOrder, LogInfo.Rows[i]["username"].ToString(), LogInfo.Rows[i]["name"].ToString(), LogInfo.Rows[i]["group_name"].ToString(), LogInfo.Rows[i]["date"].To<DateTime>().ToString("dd/MM/yyyy"), LogInfo.Rows[i]["time"].ToString(), LogInfo.Rows[i]["action"].ToString());
                    countOrder++;
                }

                LogDS.Tables.Add(ItemDT);
                this.DataSource = LogDS;
                LogDS.WriteXml(@"C:\logaccessSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);
            }
            catch { 
            
            }
        }
    }
}
