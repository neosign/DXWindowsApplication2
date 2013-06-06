using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class reciept_booking : DevExpress.XtraReports.UI.XtraReport
    {

        public reciept_booking()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int reciept_id)
        {
            

            DataTable RecieptInfo = BusinessLogicBridge.DataStore.getRecieptById(reciept_id);

            xrLabelInvoiceNo.Text = RecieptInfo.Rows[0]["rec_trans_number"].ToString();
            xrLabelCompanyName.Text = RecieptInfo.Rows[0]["rec_trans_company_name"].ToString();
            xrLabelCompanyAddress.Text = RecieptInfo.Rows[0]["rec_trans_company_address"].ToString();
            xrLabelCompanyTel.Text = RecieptInfo.Rows[0]["rec_trans_company_telephone"].ToString();
            xrLabelCompanyFax.Text = RecieptInfo.Rows[0]["rec_trans_company_fax"].ToString();
            xrLabelCompanyTaxID.Text = RecieptInfo.Rows[0]["rec_trans_company_tax_id"].ToString();
            xrLabel1CompanyEmail.Text = RecieptInfo.Rows[0]["rec_trans_company_email"].ToString();


            DataTable docInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(RecieptInfo.Rows[0]["rec_trans_building"].ToString());

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

            xrLabelCreateDate.Text = DateTime.Parse(RecieptInfo.Rows[0]["rec_trans_datecreated"].ToString()).ToString(MainForm.dateformat);

            xrLabelBuildingName.Text    = RecieptInfo.Rows[0]["rec_trans_building"].ToString();
            xrLabelRoomNo.Text          = RecieptInfo.Rows[0]["rec_trans_roomlabel"].ToString();
            xrLabelTenantName.Text = RecieptInfo.Rows[0]["rec_trans_tenantname"].ToString().Replace("||"," ");
            xrLabelTenantAddress.Text   = RecieptInfo.Rows[0]["rec_trans_tenantaddress"].ToString();
            xrLabelInvoiceDue.Text      = DateTime.Parse(RecieptInfo.Rows[0]["rec_trans_datecreated"].ToString()).ToString(MainForm.dateformat);

            xrLabelHeader.Text = RecieptInfo.Rows[0]["rec_trans_doc_header_invoice"].ToString();

            xrLabelfooter.Text = RecieptInfo.Rows[0]["rec_trans_doc_footer_invoice"].ToString();

            xrLabelSignature1.Text = RecieptInfo.Rows[0]["rec_trans_doc_under_invoice1"].ToString();

            xrLabelSignature2.Text = RecieptInfo.Rows[0]["rec_trans_doc_under_invoice2"].ToString();            

            xrTableCellPriceRoomRent.Text = RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");

            xrTableCellPrice.Text = RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");

            xrTableCellSum.Text = RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");

            xrTableCellThaibahtText.Text = "( "+ RecieptInfo.Rows[0]["rec_trans_money_text"].ToString() +" )";

            xrTableCellSubTotal.Text = RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");

            xrTableCellGrandTotal.Text = RecieptInfo.Rows[0]["rec_trans_roomprice"].To<double>().ToString("N2");

        }



    }

}
