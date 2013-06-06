using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public partial class ProgramLogAccess : uBase
    {
        public ProgramLogAccess()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(ProgramLogAccess_Load);
            //
            this.bttSubmit.Click += new EventHandler(bttSubmit_Click);
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setDefault();
            setLangThis();
            getLogAll();
        }
        
        void ProgramLogAccess_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill; 

            //
            setLangThis();
            //
            setDefault();
            //
            getLogAll();
        }

        void setDefault()
        {
            dateEditStart.DateTime = DateTime.Now.AddDays(-7);
            dateEditEnd.DateTime = DateTime.Now;
            dateEditStart.Properties.MaxValue = dateEditEnd.DateTime;
        }

        public void setLangThis()
        {
            this.Name = getLanguage("_history_log");
            //
            this.groupControlHistory.Text = getLanguage("_history_log_list");
            //
            this.labelControlStart.Text = getLanguageWithColon("_date");
            this.labelControlTo.Text = getLanguageWithColon("_to");
            //
            this.gridColumnUsername.Caption = getLanguage("_user_logon");
            this.gridColumnName.Caption = getLanguage("_user_name");
            this.gridColumnGroup.Caption = getLanguage("_group_name");
            this.gridColumnTimeAccess.Caption = getLanguage("_date");
            this.gridColumnList.Caption = getLanguage("_list");
            //
            this.bttPrint.Text = getLanguage("_print");
            this.bttSubmit.Text = getLanguage("_search");
        }

        void getLogAll() 
        {
            DateTime startDate = DateTime.Parse(dateEditStart.EditValue.ToString());
            DateTime endDate = DateTime.Parse(dateEditEnd.EditValue.ToString());
            //
            DataTable logAll = BusinessLogicBridge.DataStore.getLogAccessByDate(startDate, endDate);

            logAll.Columns.Add("date", typeof(DateTime));
            logAll.Columns.Add("time", typeof(string));

            for (int i = 0; i < logAll.Rows.Count; i++) {

                logAll.Rows[i]["date"] = String.Format("{0:yyyy-MM-dd}", logAll.Rows[i]["time_access"]).To<DateTime>();
                logAll.Rows[i]["time"] = String.Format("{0:HH:mm:ss}", logAll.Rows[i]["time_access"]);

            }
                //
                gridControl2.DataSource = logAll;
        }

        void bttSubmit_Click(object sender, EventArgs e)
        {
            getLogAll();
        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            PrintDocuments.history_log PrintInvoice = new DXWindowsApplication2.PrintDocuments.history_log();

            DateTime startDate = DateTime.Parse(dateEditStart.EditValue.ToString());
            DateTime endDate = DateTime.Parse(dateEditEnd.EditValue.ToString());

            DataTable loginfo = ((DataTable)gridControl2.DataSource);

            PrintInvoice.loopGenDataRow(loginfo, startDate, endDate);

            // PrintInvoice.ExportToPdf(pathname);
            PrintInvoice.ShowPreview();
        }

    }
}
