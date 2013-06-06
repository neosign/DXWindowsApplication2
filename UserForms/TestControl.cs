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
    public partial class TestControl : DevExpress.XtraEditors.XtraUserControl
    {
        public TestControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
    }
}
