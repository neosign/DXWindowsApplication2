using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class reciept_checkin : DevExpress.XtraReports.UI.XtraReport
    {

        public reciept_checkin()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int RecieptID)
        {

            DataTable RecieptDT = BusinessLogicBridge.DataStore.getRecieptById(RecieptID);

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
            xrLabelInvoiceDate.Text = "-";
            xrLabelInvoiceDue.Text = DateTime.Parse(RecieptDT.Rows[0]["rec_trans_datecreated"].ToString()).ToString(MainForm.dateformat);

            double sumAdvance = 0;

            sumAdvance = RecieptDT.Rows[0]["rec_trans_advance"].To<int>() * RecieptDT.Rows[0]["rec_trans_roomprice"].To<double>();

            if (RecieptDT.Rows[0]["rec_trans_booking"].To<double>() > 0){
                xrTableCellBookPrice.Text = "-" + RecieptDT.Rows[0]["rec_trans_booking"].To<double>().ToString("N2");
                xrTableCellBookPriceSum.Text = "-" + RecieptDT.Rows[0]["rec_trans_booking"].To<double>().ToString("N2");
                xrTableCellBookPriceAmount.Text = "-" + RecieptDT.Rows[0]["rec_trans_booking"].To<double>().ToString("N2");
            }else{
                xrTableCellBookPrice.Text = "0.00";
                 xrTableCellBookPriceSum.Text = "0.00";
                xrTableCellBookPriceAmount.Text = "0.00";
            }

            xrTableCellAmount.Text = RecieptDT.Rows[0]["rec_trans_advance"].ToString();
            xrTableCellPerunit.Text = RecieptDT.Rows[0]["rec_trans_roomprice"].ToString();
            xrTableCellAdvancePrice.Text = sumAdvance.ToString("N2");
            xrTableCellAdvanceSum.Text = sumAdvance.ToString("N2");

            char[] delimiters = new char[] { '|', '|' };

            string[] TenantNameSplited = RecieptDT.Rows[0]["rec_trans_tenantname"].ToString().Split(delimiters);

            xrLabelTenantName.Text = TenantNameSplited[0] + " " + TenantNameSplited[2].ToString() + " " + TenantNameSplited[4].ToString();

            DataTable RecieptItemDT = BusinessLogicBridge.DataStore.getRecieptItemsByRecieptId(RecieptID);

            DataTable ItemDT = new DataTable();

            ItemDT.Columns.Add("order", typeof(int));
            ItemDT.Columns.Add("item_name", typeof(string));
            ItemDT.Columns.Add("item_amount", typeof(int));
            ItemDT.Columns.Add("item_priceperunit", typeof(double));
            ItemDT.Columns.Add("item_sumprice", typeof(double));
            ItemDT.Columns.Add("item_vatprice", typeof(double));
            ItemDT.Columns.Add("item_netprice", typeof(double));

            DataSet RecieptDS = new DataSet();

            int countOrder = 4;

            for (int i = 0; i<RecieptItemDT.Rows.Count; i++)
            {
                ItemDT.Rows.Add(countOrder, RecieptItemDT.Rows[i]["item_name"].ToString(), RecieptItemDT.Rows[i]["item_amount"].To<int>(), RecieptItemDT.Rows[i]["item_priceperunit"].To<double>(), RecieptItemDT.Rows[i]["item_sumprice"].To<double>(), RecieptItemDT.Rows[i]["item_vatprice"].To<double>(), RecieptItemDT.Rows[i]["item_netprice"].To<double>());
                countOrder++;
            }

            //xrPictureBoxLogo.Image = new Bitmap(RecieptDT.Rows[0]["rec_trans_company_logofile"].ToString());

            RecieptDS.Tables.Add(ItemDT);

            if (RecieptDT.Rows[0]["rec_trans_vattype"].To<int>()==3) {
                xrTableCellVatText.Text = "ภาษี /Vat    " + ((RecieptDT.Rows[0]["rec_trans_sumprice_withvat"].To<double>() * 100) / RecieptDT.Rows[0]["rec_trans_sumprice"].To<double>()) + " % ";
            }

            RecieptDS.Tables.Add(RecieptDT);
            this.DataSource = RecieptDS;
            //RecieptDS.WriteXml(@"C:\recieptCheckInSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);

        }



    }

}
