using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.ReportDocuments
{   
    public partial class income : DevExpress.XtraReports.UI.XtraReport
    {

        public income()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(DataTable incomeTable, string Building, string roomFrom, string roomTo, DateTime dateFrom, DateTime dateTo)
        {

            xrLabelDatePrint.Text = "พิมพ์วันที่   "+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xrLabelDateStart.Text = dateFrom.ToString("dd/MM/yyyy");
            xrLabelDateTo.Text = dateTo.ToString("dd/MM/yyyy");

            xrLabelBuilding.Text = Building;

            xrLabelRoomFrom.Text = roomFrom;
            xrLabelRoomTo.Text = roomTo;

            DataSet IncomeDS = new DataSet();
            DataTable x = new DataTable();

            x.Columns.Add("rec_trans_roomlabel", typeof(string));
            x.Columns.Add("contract_type_text", typeof(string));
            x.Columns.Add("rec_trans_number", typeof(string));
            x.Columns.Add("rec_trans_datecreated", typeof(DateTime));
            x.Columns.Add("rec_trans_roomprice", typeof(double));
            x.Columns.Add("rec_trans_wmeter_unit", typeof(double));
            x.Columns.Add("rec_trans_wmeter_price", typeof(double));
            x.Columns.Add("rec_trans_emeter_unit", typeof(double));
            x.Columns.Add("rec_trans_emeter_price", typeof(double));
            x.Columns.Add("rec_trans_phone_price", typeof(double));
            x.Columns.Add("rec_trans_additional_price", typeof(double));
            x.Columns.Add("rec_trans_sumprice_net", typeof(double));

            try
            {
                double sum_roomprice = 0;
                double sum_wmeter_unit = 0;
                double sum_wmeter_price = 0;
                double sum_emeter_unit = 0;
                double sum_emeter_price = 0;
                double sum_phone_price = 0;
                double sum_additional_price = 0;
                double sum_allprice = 0;

                double additional_price = 0;

                for (int i = 0; i < incomeTable.Rows.Count; i++ )
                {
                    additional_price = BusinessLogicBridge.DataStore.sumRecieptItem(incomeTable.Rows[i]["rec_trans_id"].To<int>());

                    x.Rows.Add(
                                incomeTable.Rows[i]["rec_trans_roomlabel"],
                                incomeTable.Rows[i]["contract_type_text"],
                                incomeTable.Rows[i]["rec_trans_number"],
                                incomeTable.Rows[i]["rec_trans_datecreated"],
                                incomeTable.Rows[i]["rec_trans_roomprice"],
                                incomeTable.Rows[i]["rec_trans_wmeter_unit"],
                                incomeTable.Rows[i]["rec_trans_wmeter_price"],
                                incomeTable.Rows[i]["rec_trans_emeter_unit"],
                                incomeTable.Rows[i]["rec_trans_emeter_price"],
                                incomeTable.Rows[i]["rec_trans_phone_price"],
                                additional_price,
                                incomeTable.Rows[i]["rec_trans_sumprice_net"]
                                );

                    sum_roomprice += incomeTable.Rows[i]["rec_trans_roomprice"].To<double>();
                    sum_wmeter_unit += incomeTable.Rows[i]["rec_trans_wmeter_unit"].To<double>();
                    sum_wmeter_price += incomeTable.Rows[i]["rec_trans_wmeter_price"].To<double>();
                    sum_emeter_unit += incomeTable.Rows[i]["rec_trans_emeter_unit"].To<double>();
                    sum_emeter_price += incomeTable.Rows[i]["rec_trans_emeter_price"].To<double>();
                    sum_phone_price += incomeTable.Rows[i]["rec_trans_phone_price"].To<double>();
                    sum_additional_price += additional_price;
                    sum_allprice += incomeTable.Rows[i]["rec_trans_sumprice_net"].To<double>();
                }


                IncomeDS.Tables.Add(x);
                this.DataSource = IncomeDS;

                xrTableSumRoomPrice.Text = sum_roomprice.ToString("n2");
                xrTableSumWaterUnit.Text = sum_wmeter_unit.ToString("n2");
                xrTableSumWaterPrice.Text = sum_wmeter_price.ToString("n2");
                xrTableSumElectricUnit.Text = sum_emeter_unit.ToString("n2");
                xrTableSumElectricPrice.Text = sum_emeter_price.ToString("n2");
                xrTableSumPhonePrice.Text = sum_phone_price.ToString("n2");
                xrTableSumAdditionPrice.Text = sum_additional_price.ToString("n2");
                xrTableSumAllPrice.Text = sum_allprice.ToString("n2");

               // IncomeDS.WriteXml(@"C:\income2SourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);

            }
            catch(Exception ex) {

            }
        }
    }
}
