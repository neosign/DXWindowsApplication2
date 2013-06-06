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
    public partial class ProgramLanguage : DevExpress.XtraEditors.XtraUserControl
    {
        public ProgramLanguage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            getLangConfig();
        }

        public void getLangConfig()
        {
            DataTable configData = BusinessLogicBridge.DataStore.getLangConfig();

            if (configData.Rows.Count > 0)
            {
                string lang = configData.Rows[0]["language_name"].ToString();

                if (lang == "th")
                {
                    radioGroupLang.EditValue = "th";
                }
                else
                {
                    radioGroupLang.EditValue = "en";
                }
            }
            else {
                BusinessLogicBridge.DataStore.updateLangConfig("th");
                radioGroupLang.EditValue = "th";
            }

            configData.Dispose();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (radioGroupLang.EditValue.ToString() == "th")
            {
                BusinessLogicBridge.DataStore.updateLangConfig("th");
            }
            else
            {
                BusinessLogicBridge.DataStore.updateLangConfig("en");
            }
            getLangConfig();
            XtraMessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว");

            DXWindowsApplication2.MainForm.setToggleBar();
        }
    }
}
