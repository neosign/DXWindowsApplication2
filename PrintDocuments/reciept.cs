using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class reciept : DevExpress.XtraReports.UI.XtraReport
    {

        public reciept()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int RecieptID)
        {

            DataTable RecieptDT = BusinessLogicBridge.DataStore.getRecieptById(RecieptID);

            DataTable InvoiceInfo = BusinessLogicBridge.DataStore.getInvoiceByInvoiceNumber(RecieptDT.Rows[0]["inv_trans_number"].ToString());

            DataTable docInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(RecieptDT.Rows[0]["rec_trans_building"].ToString().Trim());

            int paper_id = docInfo.Rows[0]["doc_paper_reciept"].To<int>();

            if (paper_id != 1)
            {
                if (paper_id == 2)
                {
                    this.PaperKind = System.Drawing.Printing.PaperKind.A3;
                }
                else
                {
                    this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
                }
            }

            DataTable companyInfo = BusinessLogicBridge.DataStore.getCompanyByID(docInfo.Rows[0]["company_id"].To<int>());

            string logo = companyInfo.Rows[0]["company_logo"].ToString();

            if (logo != "")
            {
                logo = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + logo;

                xrPictureBox1.Image = new Bitmap(logo);
                xrPictureBox2.Image = new Bitmap(logo);
                xrPictureBox3.Image = new Bitmap(logo);

                int LogoPosition = docInfo.Rows[0]["doc_logo_position"].To<int>();

                switch (LogoPosition)
                {
                    case 0:
                        xrPictureBox2.Visible = false;
                        xrPictureBox3.Visible = false;
                        break;
                    case 1:
                        xrPictureBox1.Visible = false;
                        xrPictureBox3.Visible = false;
                        break;
                    case 2:
                        xrPictureBox2.Visible = false;
                        xrPictureBox1.Visible = false;
                        break;
                    default:
                        xrPictureBox1.Visible = false;
                        xrPictureBox3.Visible = false;
                        break;
                }
            }

            MainForm.SX_DateFormat(docInfo.Rows[0]["doc_dateformat"].To<int>());

            xrLabelCreateDate.Text = DateTime.Parse(RecieptDT.Rows[0]["rec_trans_datecreated"].ToString()).ToString(MainForm.dateformat);
            xrLabelInvoiceDate.Text = DateTime.Parse(RecieptDT.Rows[0]["rec_trans_cutduedate"].ToString()).ToString(MainForm.dateformat);
            xrLabelInvoiceDue.Text = DateTime.Parse(RecieptDT.Rows[0]["rec_trans_datecreated"].ToString()).ToString(MainForm.dateformat);


            char[] delimiters = new char[] { '|', '|' };

            string[] TenantNameSplited = RecieptDT.Rows[0]["rec_trans_tenantname"].ToString().Split(delimiters);

            if (TenantNameSplited.Length > 3)
            {
                xrLabelTenantName.Text = TenantNameSplited[0] + " " + TenantNameSplited[2].ToString() + " " + TenantNameSplited[4].ToString();
            }
            else
            {
                if (TenantNameSplited.Length == 2)
                {
                    xrLabelTenantName.Text = TenantNameSplited[0] + " " + TenantNameSplited[1].ToString();
                }
                else
                {
                    xrLabelTenantName.Text = TenantNameSplited[0] + " " + TenantNameSplited[2].ToString();
                }
            }

            DataTable RecieptItemDT = BusinessLogicBridge.DataStore.getRecieptItemsByRecieptId(RecieptID);


            #region Room Cost
            if (InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>() > 0 && RecieptDT.Rows[0]["rec_trans_category"].To<int>() == 0)
            {
                switch (RecieptDT.Rows[0]["rec_trans_vattype"].To<int>())
                {
                    case 1:
                        xrTableAmountOfdays.Text = InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<double>().ToString("N2");
                        xrTableCellSumPrice.Text = (InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>() * RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text = "0.00";
                        xrTableCellNetPrice.Text = xrTableCellSumPrice.Text.To<double>().ToString("N2");
                        break;
                    case 2:
                        xrTableAmountOfdays.Text = InvoiceInfo.Rows[0]["inv_trans_amountdays"].ToString();
                        xrTableCellSumPrice.Text = (InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>() * RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellSumPrice.Text = (xrTableCellSumPrice.Text.To<double>() - xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    case 3:
                        xrTableAmountOfdays.Text = InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<double>().ToString("N2");
                        xrTableCellSumPrice.Text = (InvoiceInfo.Rows[0]["inv_trans_amountdays"].To<int>() * RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    default:
                        break;
                }
            }
            else {
              
                switch (RecieptDT.Rows[0]["rec_trans_vattype"].To<int>())
                {
                    case 1:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = "0.00";
                        xrTableCellNetPrice.Text = RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");
                        break;
                    case 2:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellSumPrice.Text = (xrTableCellSumPrice.Text.To<double>() - xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    case 3:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Electric Cost
            switch (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellEPrice.Text = RecieptDT.Rows[0]["rec_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEVat.Text = "0.00";
                    xrTableCellENet.Text = RecieptDT.Rows[0]["rec_trans_emeter_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellEVat.Text = ((RecieptDT.Rows[0]["rec_trans_emeter_price"].To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellENet.Text = RecieptDT.Rows[0]["rec_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEPrice.Text = (xrTableCellENet.Text.To<double>() - xrTableCellEVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellEPrice.Text = RecieptDT.Rows[0]["rec_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEVat.Text = ((xrTableCellEPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellENet.Text = (xrTableCellEPrice.Text.To<double>() + xrTableCellEVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion

            #region Water Cost
            switch (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellWPrice.Text = RecieptDT.Rows[0]["rec_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWVat.Text = "0.00";
                    xrTableCellWNet.Text = RecieptDT.Rows[0]["rec_trans_wmeter_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellWVat.Text = ((RecieptDT.Rows[0]["rec_trans_wmeter_price"].To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellWNet.Text = RecieptDT.Rows[0]["rec_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWPrice.Text = (xrTableCellWNet.Text.To<double>() - xrTableCellWVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellWPrice.Text = RecieptDT.Rows[0]["rec_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWVat.Text = ((xrTableCellWPrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellWNet.Text = (xrTableCellWPrice.Text.To<double>() + xrTableCellWVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion

            #region Phone Cost
            switch (InvoiceInfo.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellPhonePrice.Text = RecieptDT.Rows[0]["rec_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhoneVat.Text = "0.00";
                    xrTableCellPhoneNet.Text = RecieptDT.Rows[0]["rec_trans_phone_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellPhoneVat.Text = ((RecieptDT.Rows[0]["rec_trans_phone_price"].To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellPhoneNet.Text = RecieptDT.Rows[0]["rec_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhonePrice.Text = (xrTableCellPhoneNet.Text.To<double>() - xrTableCellPhoneVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellPhonePrice.Text = RecieptDT.Rows[0]["rec_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhoneVat.Text = ((xrTableCellPhonePrice.Text.To<double>() * InvoiceInfo.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellPhoneNet.Text = (xrTableCellPhonePrice.Text.To<double>() + xrTableCellPhoneVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion


            DataTable ItemDT = new DataTable();

            ItemDT.Columns.Add("order", typeof(int));
            ItemDT.Columns.Add("item_name", typeof(string));
            ItemDT.Columns.Add("item_priceperunit", typeof(double));
            ItemDT.Columns.Add("item_sumprice", typeof(double));
            ItemDT.Columns.Add("item_vatprice", typeof(double));
            ItemDT.Columns.Add("item_netprice", typeof(double));

            DataSet RecieptDS = new DataSet();

            int countOrder = 5;

            for (int i = 0; i<RecieptItemDT.Rows.Count; i++)
            {
                ItemDT.Rows.Add(countOrder, RecieptItemDT.Rows[i]["item_name"].ToString(), RecieptItemDT.Rows[i]["item_priceperunit"].To<double>(), RecieptItemDT.Rows[i]["item_sumprice"].To<double>(), RecieptItemDT.Rows[i]["item_vatprice"].To<double>(), RecieptItemDT.Rows[i]["item_netprice"].To<double>());
                countOrder++;
            }

            //xrPictureBoxLogo.Image = new Bitmap(RecieptDT.Rows[0]["rec_trans_company_logofile"].ToString());


            RecieptDS.Tables.Add(ItemDT);

            if (RecieptDT.Rows[0]["rec_trans_vattype"].To<int>()==3) {
                xrTableCellVatText.Text = "¿“…’ /Vat    " + ((RecieptDT.Rows[0]["rec_trans_sumprice_withvat"].To<double>() * 100) / RecieptDT.Rows[0]["rec_trans_sumprice"].To<double>()) + " % ";
            }

            string EMeterPreviousDate = String.Format("{0:dd/MM/yyyy}", RecieptDT.Rows[0]["rec_trans_emeter_previous_date"]);
            string EMeterPresentDate = String.Format("{0:dd/MM/yyyy}", RecieptDT.Rows[0]["rec_trans_emeter_present_date"]);

            double EmeterPreviousValue = RecieptDT.Rows[0]["rec_trans_emeter_previous_energy"].To<double>();
            double EmeterPresenValue = RecieptDT.Rows[0]["rec_trans_emeter_present_energy"].To<double>();

            string WMeterPreviousDate = String.Format("{0:dd/MM/yyyy}", RecieptDT.Rows[0]["rec_trans_wmeter_previous_date"]);
            string WMeterPresentDate = String.Format("{0:dd/MM/yyyy}", RecieptDT.Rows[0]["rec_trans_wmeter_present_date"]);

            double WmeterPreviousValue = RecieptDT.Rows[0]["rec_trans_wmeter_previous_energy"].To<double>();
            double WmeterPresenValue = RecieptDT.Rows[0]["rec_trans_wmeter_present_energy"].To<double>();

            double EUnit = EmeterPresenValue - EmeterPreviousValue ; 
            double WUnit = WmeterPresenValue - WmeterPreviousValue ;

            xrTableCellEMeter.Text = "§Ë“‰øøÈ“ ( " + EMeterPreviousDate + " - " + EMeterPresentDate + " )[ " + EmeterPreviousValue + " - " + EmeterPresenValue + " ] ";
            xrTableCellWMeter.Text = "§Ë“πÈ” ( " + WMeterPreviousDate + " - " + WMeterPresentDate + " )[ " + WmeterPreviousValue + " - " + WmeterPresenValue + " ] ";
            xrTableCellPhone.Text = "§Ë“‚∑√»—æ∑Ï ( " + RecieptDT.Rows[0]["rec_trans_phone_start_date"].To<DateTime>().ToString("dd/MM/yyyy HH:mm:ss") + " - " + RecieptDT.Rows[0]["rec_trans_phone_end_date"].To<DateTime>().ToString("dd/MM/yyyy HH:mm:ss") + " )";

            RecieptDS.Tables.Add(RecieptDT);
            this.DataSource = RecieptDS;
           // RecieptDS.WriteXml(@"C:\recieptSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);

        }

    }

}
