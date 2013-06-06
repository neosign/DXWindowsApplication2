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
    public partial class XtraUserControl1 : DevExpress.XtraEditors.XtraUserControl
    {
        public double emeter_price = 0;
        public double wmeter_price = 0;
        public double pmeter_price = 0;
        public double room_price = 0;
        public double phoneprice_per_unit = 0;
        public double EUnit = 0;
        public double WUnit = 0;
        public double PUnit = 0;

        public XtraUserControl1()
        {
            InitializeComponent();
        }
    }
}
