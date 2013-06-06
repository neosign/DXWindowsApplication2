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
    public partial class PopupElectricPassword : uFormBase
    {
        public PopupElectricPassword()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(PopupElectricPassword_Load);
            //
            bttSave.Click += new EventHandler(bttSave_Click);
            bttCancel.Click += new EventHandler(bttCancel_Click);
        }

        void bttCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void bttSave_Click(object sender, EventArgs e)
        {
            if (textEditPassword.Text == CurrentPassword)
                this.DialogResult = DialogResult.OK;
            else
                MessageBox.Show(getLanguage("_msg_1016"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public string cutMode = "on";
        public int roomCutOffStatus = 1;
        public string roomName = "unknown";

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
        }

        void PopupElectricPassword_Load(object sender, EventArgs e)
        {
            setLangThis();
        }

        public void setLangThis()
        {
            this.Name = getLanguage("_electric_cut_" + cutMode + "_form");
            //
            this.labelControlNote.Text = string.Format(getLanguage("_electric_cut_" + cutMode + "_note"), roomName);
            this.labelControlRemark.Text = getLanguage("_electric_cut_" + cutMode + "_remark");
            this.labelControlPassword.Text = getLanguageWithColon("_password");
            //
            // button
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
        }
    }
}
