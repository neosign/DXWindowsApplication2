using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DXWindowsApplication2.PrintDocuments
{   
    public partial class bookrefund : DevExpress.XtraReports.UI.XtraReport
    {

        public bookrefund()
        {
            InitializeComponent();
        }

        

        public void loopGenDataRow(int RefundID)
        {

            DataTable RefundDT = BusinessLogicBridge.DataStore.getRefundById(RefundID);

            DataTable docInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingLabel(RefundDT.Rows[0]["ref_insure_building"].ToString());

            DataTable companyInfo = BusinessLogicBridge.DataStore.getCompanyByID(docInfo.Rows[0]["company_id"].To<int>());

            string logo = RefundDT.Rows[0]["ref_insure_company_logofile"].ToString();

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
            
            DataSet RefundDS = new DataSet();

            xrLabelCreateDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

            xrTableCellThaiBaht.Text = "(......"+MainForm.ThaiBaht(RefundDT.Rows[0]["ref_insure_sumrefund"].ToString())+".......)";

            RefundDS.Tables.Add(RefundDT);
            this.DataSource = RefundDS;
            //RefundDS.WriteXml(@"C:\refundSourceSchema.xml", System.Data.XmlWriteMode.WriteSchema);

        }



    }

}
