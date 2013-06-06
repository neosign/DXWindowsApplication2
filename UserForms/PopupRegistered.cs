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
    public partial class PopupRegistered : uFormBase
    {
        public PopupRegistered()
        {
            InitializeComponent();
            this.Text = getLanguage("_softwarename");
            this.Load += new EventHandler(PopupRegistered_Load);
        }

        void PopupRegistered_Load(object sender, EventArgs e)
        {
            textEditProductID.EditValue     =  MainForm.LicObj.ProductID;
            textEditADCSerial1.EditValue    =  MainForm.LicObj.ADCSN1;
            textEditADCSerial2.EditValue    =  MainForm.LicObj.ADCSN2;
            textEditADCSerial3.EditValue    =  MainForm.LicObj.ADCSN3;
            textEditADCSerial4.EditValue    =  MainForm.LicObj.ADCSN4;
            textEditADCSerial5.EditValue    =  MainForm.LicObj.ADCSN5;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
