using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace DXWindowsApplication2.PrintDocuments
{
    public partial class contract_preview : DevExpress.XtraReports.UI.XtraReport
    {
        public contract_preview()
        {
            InitializeComponent();
        }

        public void loopGenDataRow(int company_id, string contact_no, string datetime_format ) {

            //DataTable CheckInData = BusinessLogicBridge.DataStore.getCheckInByID(check_in_id);

            DataTable companyInfo = BusinessLogicBridge.DataStore.getCompanyByID(company_id);

            xrTableCellContactNO.Text = contact_no.ToString();
            xrTableCellCompanyName.Text = companyInfo.Rows[0]["company_name"].ToString();
            xrTableCellCreateDate.Text = datetime_format;
            xrTableCellOwnerName.Text = (companyInfo.Rows[0]["company_owner_name"].ToString() == "") ? "-" : companyInfo.Rows[0]["company_owner_name"].ToString();
            xrTableCellCompanyAddress.Text = companyInfo.Rows[0]["company_address"].ToString();

            string Topic1 = xrTableCellTopic1.Text;

            Topic1 = Topic1.Replace("[checkindate]", DateTime.Now.ToString("dd/MM/yyyy"));

            Topic1 = Topic1.Replace("[rentminend]", DateTime.Now.AddMonths(3).ToString("dd/MM/yyyy"));

            xrTableCellTopic1.Text = Topic1;

        }

    }
}
