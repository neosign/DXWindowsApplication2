using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public partial class PopupProgressBar : DevExpress.XtraEditors.XtraForm
    {
        public PopupProgressBar()
        {
            InitializeComponent();
        }
        
        public void ClosedBar() {
            this.DialogResult = DialogResult.Yes;

        }
    }
}