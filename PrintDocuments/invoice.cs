using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class invoice : DevExpress.XtraReports.UI.XtraReport
    {
        
        public invoice()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int InvioceID)
        {
           DataTable InvioceDT = BusinessLogicBridge.DataStore.getInvoiceById(InvioceID);

           DataTable docInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(InvioceDT.Rows[0]["inv_trans_building"].ToString().Trim());

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

            xrLabelCreateDate.Text = DateTime.Parse(InvioceDT.Rows[0]["inv_trans_datecreated"].ToString()).ToString(MainForm.dateformat);
            xrLabelInvoiceDate.Text = DateTime.Parse(InvioceDT.Rows[0]["inv_trans_datecreated"].ToString()).ToString(MainForm.dateformat);
            xrLabelInvoiceDue.Text = DateTime.Parse(InvioceDT.Rows[0]["inv_trans_cutduedate"].ToString()).ToString(MainForm.dateformat);

            DateTime StartDate = new DateTime(InvioceDT.Rows[0]["inv_trans_cutduedate"].To<DateTime>().Year, InvioceDT.Rows[0]["inv_trans_cutduedate"].To<DateTime>().Month, 1);

            xrLabelDueStart.Text = StartDate.ToString("dd/MM/yyyy");

            char[] delimiters = new char[] { '|', '|' };

            string[] TenantNameSplited = InvioceDT.Rows[0]["inv_trans_tenantname"].ToString().Split(delimiters);

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

            #region Room Cost

            if (InvioceDT.Rows[0]["inv_trans_amountdays"].To<int>() > 0)
            {
                switch (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>())
                {
                    case 1:
                        xrTableAmountOfdays.Text = InvioceDT.Rows[0]["inv_trans_amountdays"].To<double>().ToString("N2");
                        xrTableCellSumPrice.Text = (InvioceDT.Rows[0]["inv_trans_amountdays"].To<int>() * InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text  = "0.00";
                        xrTableCellNetPrice.Text = xrTableCellSumPrice.Text.To<double>().ToString("N2");
                        break;
                    case 2:
                        xrTableAmountOfdays.Text = InvioceDT.Rows[0]["inv_trans_amountdays"].ToString();
                        xrTableCellSumPrice.Text = (InvioceDT.Rows[0]["inv_trans_amountdays"].To<int>() * InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellSumPrice.Text = (xrTableCellSumPrice.Text.To<double>() - xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    case 3:
                        xrTableAmountOfdays.Text = InvioceDT.Rows[0]["inv_trans_amountdays"].To<double>().ToString("N2");
                        xrTableCellSumPrice.Text = (InvioceDT.Rows[0]["inv_trans_amountdays"].To<int>() * InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>()).ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    default:
                        break;
                }
            }
            else
            {

                switch (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>())
                {
                    case 1:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = "0.00";
                        xrTableCellNetPrice.Text = InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>().ToString("N2");
                        break;
                    case 2:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellSumPrice.Text = (xrTableCellSumPrice.Text.To<double>() - xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    case 3:
                        xrTableAmountOfdays.Text = "1";
                        xrTableCellSumPrice.Text = InvioceDT.Rows[0]["inv_trans_roomprice"].To<double>().ToString("N2");
                        xrTableCellRoomVat.Text = ((xrTableCellSumPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                        xrTableCellNetPrice.Text = (xrTableCellSumPrice.Text.To<double>() + xrTableCellRoomVat.Text.To<double>()).ToString("N2");
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Electric Cost
            switch (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellEPrice.Text = InvioceDT.Rows[0]["inv_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEVat.Text = "0.00";
                    xrTableCellENet.Text = InvioceDT.Rows[0]["inv_trans_emeter_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellEVat.Text = ((InvioceDT.Rows[0]["inv_trans_emeter_price"].To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellENet.Text = InvioceDT.Rows[0]["inv_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEPrice.Text = (xrTableCellENet.Text.To<double>() - xrTableCellEVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellEPrice.Text = InvioceDT.Rows[0]["inv_trans_emeter_price"].To<double>().ToString("N2");
                    xrTableCellEVat.Text = ((xrTableCellEPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellENet.Text = (xrTableCellEPrice.Text.To<double>() + xrTableCellEVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion

            #region Water Cost
            switch (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellWPrice.Text = InvioceDT.Rows[0]["inv_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWVat.Text = "0.00";
                    xrTableCellWNet.Text = InvioceDT.Rows[0]["inv_trans_wmeter_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellWVat.Text = ((InvioceDT.Rows[0]["inv_trans_wmeter_price"].To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellWNet.Text = InvioceDT.Rows[0]["inv_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWPrice.Text = (xrTableCellWNet.Text.To<double>() - xrTableCellWVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellWPrice.Text = InvioceDT.Rows[0]["inv_trans_wmeter_price"].To<double>().ToString("N2");
                    xrTableCellWVat.Text = ((xrTableCellWPrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellWNet.Text = (xrTableCellWPrice.Text.To<double>() + xrTableCellWVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion

            #region Phone Cost
            switch (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>())
            {
                case 1:
                    xrTableCellPhonePrice.Text = InvioceDT.Rows[0]["inv_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhoneVat.Text = "0.00";
                    xrTableCellPhoneNet.Text = InvioceDT.Rows[0]["inv_trans_phone_price"].To<double>().ToString("N2");
                    break;
                case 2:
                    xrTableCellPhoneVat.Text = ((InvioceDT.Rows[0]["inv_trans_phone_price"].To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellPhoneNet.Text = InvioceDT.Rows[0]["inv_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhonePrice.Text = (xrTableCellPhoneNet.Text.To<double>() - xrTableCellPhoneVat.Text.To<double>()).ToString("N2");
                    break;
                case 3:
                    xrTableCellPhonePrice.Text = InvioceDT.Rows[0]["inv_trans_phone_price"].To<double>().ToString("N2");
                    xrTableCellPhoneVat.Text = ((xrTableCellPhonePrice.Text.To<double>() * InvioceDT.Rows[0]["inv_trans_vatrate"].To<double>()) / 100).ToString("N2");
                    xrTableCellPhoneNet.Text = (xrTableCellPhonePrice.Text.To<double>() + xrTableCellPhoneVat.Text.To<double>()).ToString("N2");
                    break;
                default:
                    break;
            }
            #endregion

            DataTable InvioceItemDT = BusinessLogicBridge.DataStore.getInvoiceItemsByInvoiceId(InvioceID);

            DataTable ItemDT = new DataTable();

            ItemDT.Columns.Add("order", typeof(int));
            ItemDT.Columns.Add("item_name", typeof(string));
            ItemDT.Columns.Add("item_priceperunit", typeof(double));
            ItemDT.Columns.Add("item_sumprice", typeof(double));
            ItemDT.Columns.Add("item_vatprice", typeof(double));
            ItemDT.Columns.Add("item_netprice", typeof(double));

            DataSet InvioceDS = new DataSet();

            int countOrder = 5;

            for (int i = 0; i<InvioceItemDT.Rows.Count; i++)
            {
                ItemDT.Rows.Add(countOrder, InvioceItemDT.Rows[i]["item_name"].ToString(), InvioceItemDT.Rows[i]["item_priceperunit"].To<double>(), InvioceItemDT.Rows[i]["item_sumprice"].To<double>(), InvioceItemDT.Rows[i]["item_vatprice"].To<double>(), InvioceItemDT.Rows[i]["item_netprice"].To<double>());
                countOrder++;
            }

            InvioceDS.Tables.Add(ItemDT);

            if (InvioceDT.Rows[0]["inv_trans_vattype"].To<int>()==3) {
                xrTableCellVatText.Text = "¿“…’ /Vat    " + ((InvioceDT.Rows[0]["inv_trans_sumprice_withvat"].To<double>() * 100) / InvioceDT.Rows[0]["inv_trans_sumprice"].To<double>()) + " % ";
            }

            string EMeterPreviousDate = String.Format("{0:dd/MM/yyyy}", InvioceDT.Rows[0]["inv_trans_emeter_previous_date"]);
            string EMeterPresentDate = String.Format("{0:dd/MM/yyyy}", InvioceDT.Rows[0]["inv_trans_emeter_present_date"]);

            double EmeterPreviousValue = InvioceDT.Rows[0]["inv_trans_emeter_previous_energy"].To<double>();
            double EmeterPresenValue = InvioceDT.Rows[0]["inv_trans_emeter_present_energy"].To<double>();

            string WMeterPreviousDate = String.Format("{0:dd/MM/yyyy}", InvioceDT.Rows[0]["inv_trans_wmeter_previous_date"]);
            string WMeterPresentDate = String.Format("{0:dd/MM/yyyy}", InvioceDT.Rows[0]["inv_trans_wmeter_present_date"]);

            double WmeterPreviousValue = InvioceDT.Rows[0]["inv_trans_wmeter_previous_energy"].To<double>();
            double WmeterPresenValue = InvioceDT.Rows[0]["inv_trans_wmeter_present_energy"].To<double>();

            double EUnit = EmeterPresenValue - EmeterPreviousValue ; 
            double WUnit = WmeterPresenValue - WmeterPreviousValue ;

            xrTableCellEMeter.Text = "§Ë“‰øøÈ“ ( " + EMeterPreviousDate + " - " + EMeterPresentDate + " )[ " + EmeterPreviousValue + " - " + EmeterPresenValue + " ] ";
            xrTableCellWMeter.Text = "§Ë“πÈ” ( " + WMeterPreviousDate + " - " + WMeterPresentDate + " )[ " + WmeterPreviousValue + " - " + WmeterPresenValue + " ] ";
            xrTableCellPhone.Text = "§Ë“‚∑√»—æ∑Ï ( " + InvioceDT.Rows[0]["inv_trans_phone_start_date"].To<DateTime>().ToString("dd/MM/yyyy HH:mm:ss") + " - " + InvioceDT.Rows[0]["inv_trans_phone_end_date"].To<DateTime>().ToString("dd/MM/yyyy HH:mm:ss") + " )";

            InvioceDS.Tables.Add(InvioceDT);
            this.DataSource = InvioceDS;
           // InvioceDS.WriteXml(@"C:\invoiceSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);

        }

    }

    class Data
    {
        public Data()
        {
        }
        private int _id;
        private string _name;
        private DateTime _date;
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public DateTime Date
        {
            get { return this._date; }
            set { this._date = value; }
        }
    }
}
