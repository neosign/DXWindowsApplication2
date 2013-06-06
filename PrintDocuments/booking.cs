using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace DXWindowsApplication2.PrintDocuments
{
    public partial class booking : DevExpress.XtraReports.UI.XtraReport
    {
        public booking()
        {
            InitializeComponent();
        }

        public string ReadMsWord()
        {

            // variable to store file path

            string filePath = null;
            string PathName = null;

            // execute if block when dialog result box click ok button

            DataTable GeneralInfo = BusinessLogicBridge.DataStore.getGeneralConfig();
            PathName = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            PathName = PathName + @"\" + GeneralInfo.Rows[0]["path_all_document"].ToString();

            filePath = DXWindowsApplication2.MainForm.CombinePaths(PathName, "Contract", "ReservContract.doc");

            // create word application

                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

                // create object of missing value

                object miss = System.Reflection.Missing.Value;

                // create object of selected file path

                object path = filePath;

                // set file path mode

                object readOnly = false;

                // open document                

                Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

                // select whole data from active window document

                docs.ActiveWindow.Selection.WholeStory();

                // handover the data to cllipboard

                docs.ActiveWindow.Selection.Copy();

                // clipboard create reference of idataobject interface which transfer the data

                System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();

                //set data into richtextbox control in text format

                //richTextBox2.Text = data.GetData(DataFormats.Text).ToString();

                // read bitmap image from clipboard with help of iddataobject interface

                //Image img = (Image)data.GetData(DataFormats.Bitmap);

                // close the document

                docs.Close(ref miss, ref miss, ref miss);


                return data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
        }
        
        public void loopGenDataRow(int reserve_id) {

            DataTable BookingInfo =  BusinessLogicBridge.DataStore.getRoomReserveByID(reserve_id);

            DataTable DocumentInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(BookingInfo.Rows[0]["building_id"].To<int>());
            
            MainForm.SX_DateFormat(DocumentInfo.Rows[0]["doc_dateformat"].To<int>());

           xrLabelCompanyName.Text = BookingInfo.Rows[0]["company_name"].ToString();

           xrLabelInfoAll.Text = ReadMsWord();

           string alltext = xrLabelInfoAll.Text;

           alltext = alltext.Replace("สัญญาจองห้อง\r\n[companyname]\r\n\r\n", "");
           alltext = alltext.Replace("[contractid]", BookingInfo.Rows[0]["reserve_number"].ToString());
           alltext = alltext.Replace("[companyname]", BookingInfo.Rows[0]["company_name"].ToString());

           alltext = alltext.Replace("[date]", Convert.ToDateTime(BookingInfo.Rows[0]["reserve_create_date"].ToString()).ToString(MainForm.dateformat));

           alltext = alltext.Replace("[emname]", BookingInfo.Rows[0]["company_owner_name"].ToString());
           alltext = alltext.Replace("[tenantname]", BookingInfo.Rows[0]["tenant_name"].ToString() + " " + BookingInfo.Rows[0]["tenant_surname"].ToString());

           alltext = alltext.Replace("[companyaddress]", BookingInfo.Rows[0]["company_address"].ToString());
           alltext = alltext.Replace("[roomnumber]", BookingInfo.Rows[0]["coderef"].ToString());

           alltext = alltext.Replace("[rentnum]", BookingInfo.Rows[0]["roomtype_month_roomrate_price"].To<double>().ToString("N2"));
           alltext = alltext.Replace("[renttext]", MainForm.ThaiBaht(BookingInfo.Rows[0]["roomtype_month_roomrate_price"].ToString()));
            alltext = alltext.Replace("[reservnum]", BookingInfo.Rows[0]["reserve_payments"].To<double>().ToString("N2"));
            alltext = alltext.Replace("[reservtext]", MainForm.ThaiBaht(BookingInfo.Rows[0]["reserve_payments"].ToString()));
            xrLabelInfoAll.Text = alltext;
        }

    }
}
