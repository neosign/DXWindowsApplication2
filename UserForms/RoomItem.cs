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
    public partial class RoomItem : DevExpress.XtraEditors.XtraUserControl
    {
        public RoomItem(string strTenant,string strRoomType,string strRoomStatus,string strElect,string strWater,string strPhone)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
           // this.MouseClick += new MouseEventHandler(RoomItem_MouseClick);
            this.labelControl6.Text = strTenant;
            this.labelControl7.Text = strRoomType;
            this.labelControl8.Text = strRoomStatus;
            
            this.labelControl10.Text = strElect;            
            this.labelControl11.Text = strWater;
            this.labelControl12.Text = strPhone;
            this.MouseHover += new EventHandler(RoomItem_MouseHover);
            this.MouseLeave += new EventHandler(RoomItem_MouseLeave);
           
           
          
        }
        private void RoomItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                contextMenuStrip1.Show(pt);
            }
            
        }
        private void RoomItem_MouseHover(object sender, EventArgs e) {
            //this.BackColor = Color.Green;
        }
        private void RoomItem_MouseLeave(object sender, EventArgs e)
        {
            //this.BackColor = Color.GreenYellow;
        }



    }
}
