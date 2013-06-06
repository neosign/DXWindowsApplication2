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
    public partial class RoomFix : DevExpress.XtraEditors.XtraUserControl
    {
        public RoomFix()
        {
            this.SuspendLayout();
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Dock = DockStyle.Fill;
            this.ResumeLayout();
        }
    }
}
