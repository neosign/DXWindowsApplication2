using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class invoice_preview : DevExpress.XtraReports.UI.XtraReport
    {

        public invoice_preview()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int company_id, string invoice_no, string datetime_format, string invoiceHeader, string InvoiceFooter, string UnderInvoice1, string UnderInvoice2, string paper, int LogoPosition)
        {
            if (paper !="A4") {
                if(paper == "A3"){
                    this.PaperKind = System.Drawing.Printing.PaperKind.A3;
                }else{
                    this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
                }
            }            

            DataTable companyInfo = BusinessLogicBridge.DataStore.getCompanyByID(company_id);

            string logo = companyInfo.Rows[0]["company_logo"].ToString();

            if (logo != "")
            {
                logo = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + logo;

                xrPictureBox1.Image = new Bitmap(logo);
                xrPictureBox2.Image = new Bitmap(logo);
                xrPictureBox3.Image = new Bitmap(logo);

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

            xrLabelInvoiceNo.Text = invoice_no;
            xrLabelCompanyName.Text = companyInfo.Rows[0]["company_name"].ToString();
            xrLabelCompanyAddress.Text = companyInfo.Rows[0]["company_address"].ToString();
            xrLabelCompanyTel.Text = companyInfo.Rows[0]["company_telephone"].ToString();
            xrLabelCompanyFax.Text = companyInfo.Rows[0]["company_fax"].ToString();
            xrLabelCompanyTaxID.Text = companyInfo.Rows[0]["company_tax_id"].ToString();
            xrLabel1CompanyEmail.Text = companyInfo.Rows[0]["company_email"].ToString();

            xrLabelHeader.Text = invoiceHeader;
            xrLabelfooter.Text = InvoiceFooter;
            xrLabelSignature1.Text =UnderInvoice1;
            xrLabelSignature2.Text = UnderInvoice2;

            xrLabelDueStart.Text = DateTime.Now.ToString("dd/MM/yyyy");
            xrLabelDueTo.Text = DateTime.Now.AddMonths(10).ToString("dd/MM/yyyy");
            xrLabelCreateDate.Text = datetime_format;


        }
    }
}
