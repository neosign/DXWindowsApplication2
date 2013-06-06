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
    public partial class PopupAbout : uFormBase
    {
        public PopupAbout()
        {
            InitializeComponent();
            this.Text = getLanguage("_softwarename");
            this.Load += new EventHandler(PopupAbout_Load);
        }

        void PopupAbout_Load(object sender, EventArgs e)
        {
            setLangThis();
        }

        void setLangThis()
        {
            this.labelControlSoftwareName.Text = this.Text;
            this.labelControlVersion.Text = getLanguage("_version");
            //this.labelControlWarning.Text = getLanguage("_about_warning");
            this.labelControlCopyright.Text = getLanguage("_about_copyright");
            this.labelControlCompanyName.Text = getLanguage("_company_provider_name");
            this.labelControlCompanyAddress.Text = getLanguage("_company_license_address");
            richTextBoxWarning.Text = getLanguage("_about_warning");
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
