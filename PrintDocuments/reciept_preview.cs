using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class reciept_preview : DevExpress.XtraReports.UI.XtraReport
    {

        public reciept_preview()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int company_id, string reciept_no, string datetime_format, string RecieptHeader, string RecieptFooter, string UnderReciept1, string UnderReciept2, string paper, int LogoPosition)
        {
            if (paper != "A4")
            {
                if (paper == "A3")
                {
                    this.PaperKind = System.Drawing.Printing.PaperKind.A3;
                }
                else
                {
                    this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
                }
            }

            DataTable companyInfo = BusinessLogicBridge.DataStore.getCompanyByID(company_id);

            string logo = companyInfo.Rows[0]["company_logo"].ToString();

            if (logo != "")
            {
                logo = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+ logo;

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

            xrLabelInvoiceNo.Text = reciept_no;
            xrLabelCompanyName.Text = companyInfo.Rows[0]["company_name"].ToString();
            xrLabelCompanyAddress.Text = companyInfo.Rows[0]["company_address"].ToString();
            xrLabelCompanyTel.Text = companyInfo.Rows[0]["company_telephone"].ToString();
            xrLabelCompanyFax.Text = companyInfo.Rows[0]["company_fax"].ToString();
            xrLabelCompanyTaxID.Text = companyInfo.Rows[0]["company_tax_id"].ToString();
            xrLabel1CompanyEmail.Text = companyInfo.Rows[0]["company_email"].ToString();

            xrLabelHeader.Text = RecieptHeader;
            xrLabelfooter.Text = RecieptFooter;
            xrLabelSignature1.Text = UnderReciept1;
            xrLabelSignature2.Text = UnderReciept2;

            xrLabelCreateDate.Text = datetime_format;
        }



    }

}
