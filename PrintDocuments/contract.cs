using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace DXWindowsApplication2.PrintDocuments
{
    public partial class contract : DevExpress.XtraReports.UI.XtraReport
    {
        public contract()
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

            filePath = DXWindowsApplication2.MainForm.CombinePaths(PathName, "Contract", "RentContract.doc");

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

        public void loopGenDataRow(int check_in_id) {

            DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(check_in_id);

            //building_id

            DataTable DocumentInfo = BusinessLogicBridge.DataStore.getDocumentConfigFromBuildingID(CheckInData.Rows[0]["building_id"].To<int>());

            MainForm.SX_DateFormat(DocumentInfo.Rows[0]["doc_dateformat"].To<int>());

            xrLabelInfoAll.Text = ReadMsWord();

            xrLabelCompanyName.Text = CheckInData.Rows[0]["company_name"].ToString();

            string Topic1 = xrLabelInfoAll.Text;

            Topic1 = Topic1.Replace("สัญญาเช่าห้อง\r\n[companyname]\r\n\r\n", "");

            Topic1 = Topic1.Replace("[contractid]", CheckInData.Rows[0]["check_in_label"].ToString());

            Topic1 = Topic1.Replace("[companyname]", CheckInData.Rows[0]["company_name"].ToString());

            Topic1 = Topic1.Replace("[date]", Convert.ToDateTime(CheckInData.Rows[0]["check_in_date"].ToString()).ToString(MainForm.dateformat));

            Topic1 = Topic1.Replace("[emname]", (CheckInData.Rows[0]["company_owner_name"].ToString() == "") ? "-" : CheckInData.Rows[0]["company_owner_name"].ToString());

            Topic1 = Topic1.Replace("[tenantname]", CheckInData.Rows[0]["tenant_name"].ToString() + " " + CheckInData.Rows[0]["tenant_surname"].ToString());

            Topic1 = Topic1.Replace("[companyaddress]", CheckInData.Rows[0]["company_address"].ToString());
            
            Topic1 = Topic1.Replace("[roomnumber]", CheckInData.Rows[0]["room_label"].ToString());

            Topic1 = Topic1.Replace("[rentperiodmin]", CheckInData.Rows[0]["check_in_minimum_monthly"].ToString());

            Topic1 = Topic1.Replace("[checkindate]", Convert.ToDateTime(CheckInData.Rows[0]["check_in_date"].ToString()).ToString("dd/MM/yyyy"));

            Topic1 = Topic1.Replace("[rentminend]", Convert.ToDateTime(CheckInData.Rows[0]["check_in_date"].ToString()).AddMonths(CheckInData.Rows[0]["check_in_minimum_monthly"].To<int>()).ToString("dd/MM/yyyy"));

            Topic1 = Topic1.Replace("[rentnum]", CheckInData.Rows[0]["roomtype_month_roomrate_price"].To<double>().ToString("N2"));

            Topic1 = Topic1.Replace("[renttext]", MainForm.ThaiBaht(CheckInData.Rows[0]["roomtype_month_roomrate_price"].ToString()));

            Topic1 = Topic1.Replace("[depnum]", CheckInData.Rows[0]["roomtype_month_insure_price"].To<double>().ToString("N2"));

            Topic1 = Topic1.Replace("[deptext]", MainForm.ThaiBaht(CheckInData.Rows[0]["roomtype_month_insure_price"].ToString()));
            
            xrLabelInfoAll.Text = Topic1;

        }

    }
}
