using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class room : DevExpress.XtraReports.UI.XtraReport
    {

        public room()
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
            x.Columns.Add("room_status", typeof(string));
            x.Columns.Add("room_type", typeof(string));
            x.Columns.Add("room_price", typeof(string));
            x.Columns.Add("room_price_day", typeof(string));
            x.Columns.Add("room_itemlist", typeof(string));
            x.Columns.Add("item_sumprice", typeof(string));
            x.Columns.Add("room_totalprice", typeof(string));
           
            try
            {
                
                double room_totalprice = 0;

                double sumPriceRoomAll = 0;
                double sumPriceDailyRoomAll = 0;
                double sumItemRoomAll = 0;
                double sumTotalRoomAll = 0;

                DataTable ItemList = new DataTable();

                for (int i = 0; i < roomTable.Rows.Count; i++)
                {
                    ItemList = BusinessLogicBridge.DataStore.getItemsByRoomTypeId(roomTable.Rows[i]["roomtype_id"].To<int>());
                    
                    string itemlist = "";
                    double itemsumprice = 0;
                    for (int j = 0; j < ItemList.Rows.Count; j++)
                    {
                        itemlist = itemlist + "," + ItemList.Rows[j]["item_name"];
                        if (ItemList.Rows[j]["item_type"].To<int>() == 1)
                        {
                            itemsumprice = ItemList.Rows[j]["item_price_monthly"].To<double>();
                        }
                        else {
                            itemsumprice = ItemList.Rows[j]["item_price_daily"].To<double>();
                        }
                    }

                    room_totalprice = roomTable.Rows[i]["roomtype_month_roomrate_price"].To<double>() + itemsumprice;

                    x.Rows.Add(roomTable.Rows[i]["coderef"], roomTable.Rows[i]["room_label"], roomTable.Rows[i]["room_status_label"], roomTable.Rows[i]["roomtype_label"], roomTable.Rows[i]["roomtype_month_roomrate_price"].To<double>().ToString("n2"), roomTable.Rows[i]["roomtype_daily_roomrate_price"].To<double>().ToString("n2"), itemlist, itemsumprice.ToString("n2"), room_totalprice.ToString("n2"));

                    sumPriceRoomAll         += roomTable.Rows[i]["roomtype_month_roomrate_price"].To<double>();
                    sumPriceDailyRoomAll    += roomTable.Rows[i]["roomtype_daily_roomrate_price"].To<double>();

                    sumItemRoomAll += itemsumprice;
                    sumTotalRoomAll += room_totalprice;
                }

                xrTableCellSumRoomPrice.Text        = sumPriceRoomAll.ToString("n2");
                xrTableCellSumRoomDailyPrice.Text   = sumPriceDailyRoomAll.ToString("n2");
                xrTableCellSumItemPrice.Text        = sumItemRoomAll.ToString("n2");
                xrTableCellTotalPrice.Text          = sumTotalRoomAll.ToString("n2");

            }
            catch(Exception ex) {

            }

            RoomDS.Tables.Add(x);
            this.DataSource = RoomDS;

            RoomDS.WriteXml(@"C:\roomSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
