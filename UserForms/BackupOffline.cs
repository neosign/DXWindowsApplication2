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
    public partial class BackupOffline : DevExpress.XtraEditors.XtraUserControl
    {
        public BackupOffline()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            // Create new foloder on path (MUST GET PATH FROM DB)


            if (radioGroupCondition.Text != "no")
            {
                DialogResult dr = XtraMessageBox.Show("ยืนยันการสำรองข้อมูล", "", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        BusinessLogicBridge.DataStore.UpdateOfflineBackup();
                        XtraMessageBox.Show("Update Completed");
                    }
                    catch { }

                }
            }

        }
    }
}
