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
    public partial class RoomItemButton : uBase
    {
        public int roomID = 0;
        public int roomType = 0;
        public int roomStatus = 0;
        public int roomCutOffStatus = 0;
        public int meterStatus = 0;
        public int meterID = 0;
        public string roomName = "";
        //
        public static DXWindowsApplication2.MainForm mParent = null;

        public RoomItemButton()
        {
            InitializeComponent();
            //
            menu_Eletric.Click += new EventHandler(MenuCutElectric_Click);
            menu_CheckIn.Click += new EventHandler(menu_CheckIn_Click);
            menu_CheckOut.Click += new EventHandler(menu_CheckOut_Click);
            menu_Leave.Click += new EventHandler(menu_Leave_Click);
            menu_Reserve.Click += new EventHandler(menu_Reserve_Click);
            menu_CancelLeave.Click += new EventHandler(menu_CancelLeave_Click);
            menu_CancelReserve.Click += new EventHandler(menu_CancelReserve_Click);
            menu_PrintSlip.Click += new EventHandler(menu_PrintSlip_Click);
            menu_Payment.Click += new EventHandler(menu_Payment_Click);
            menu_RoomDetail.Click += new EventHandler(menu_RoomDetail_Click);
            menu_Fix.Click += new EventHandler(menu_Fix_Click);
            // this.MouseClick += new MouseEventHandler(RoomItemButton_MouseClick);

            //var controlList = new List<Control>();     
            //foreach (Control childControl in this.Controls)     { 
            //    // Recurse child controls.controlList.AddRange(GetControls(childControl));  
            //    childControl.MouseClick += new MouseEventHandler(ThisPanel_MouseClick);

            //}    
            MenuPopup.Opening += new CancelEventHandler(MenuPopup_Opening_1);
            //
            this.Load += new EventHandler(RoomItemButton_Load);
            //
        }

        void RoomItemButton_Load(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                mParent = Parent.TopLevelControl as DXWindowsApplication2.MainForm;
            }
            //
            ChangeLanguage();
        }

        void ChangeLanguage()
        {
            menu_CheckIn.Text = getLanguage("_menu_room_management_checkin");
            menu_CheckOut.Text = getLanguage("_menu_room_management_checkout");
            menu_Reserve.Text = getLanguage("_menu_room_management_book");
            menu_Leave.Text = getLanguage("_menu_room_management_inform_leave");
            menu_CancelReserve.Text = getLanguage("_cancel_booking");
            menu_CancelLeave.Text = getLanguage("_cancel_leave");
            menu_Fix.Text = getLanguage("_room_fix_cancel");
            menu_Payment.Text = getLanguage("_payment");
            menu_PrintSlip.Text = getLanguage("_menu_room_management_issue_invoice");
            menu_Eletric.Text = getLanguage("_lampconnect");//_lampcut
            menu_RoomDetail.Text = getLanguage("_room_detail");
            //
            if (roomCutOffStatus == 0)
                menu_Eletric.Text = getLanguage("_electric_cut_on");
            else
                menu_Eletric.Text = getLanguage("_electric_cut_off");
        }

        void CheckPopup()
        {
            menu_CheckIn.Enabled = checkStatus(roomStatus, new List<int>() { 1, 3 });
            menu_CheckOut.Enabled = checkStatus(roomStatus, new List<int>() { 2, 4, 5 });
            menu_Reserve.Enabled = checkStatus(roomStatus, new List<int>() { 1, 4 });
            menu_Leave.Enabled = checkStatus(roomStatus, new List<int>() { 2 });
            menu_CancelReserve.Enabled = checkStatus(roomStatus, new List<int>() { 3, 5 });
            menu_CancelLeave.Enabled = checkStatus(roomStatus, new List<int>() { 4 });
           // menu_Fix.Enabled = checkStatus(roomStatus, new List<int>() { 1, 3, 6 }); // ว่าง , จอง , ซ่อม
            menu_Fix.Enabled = checkStatus(roomStatus, new List<int>() { 1, 6 });
            menu_PrintSlip.Enabled = checkStatus(roomStatus, new List<int>() { 2, 3, 4, 5 });
            menu_Payment.Enabled = checkStatus(roomStatus, new List<int>() { 2, 4, 5 });
            menu_Eletric.Enabled = false;//checkStatus(roomStatus, new List<int>() { 1, 2, 3, 4, 5 });
            menu_RoomDetail.Enabled = true;
        }

        bool checkStatus(int i, List<int> iList)
        {
            return iList.Contains(i);
        }

        void menu_Fix_Click(object sender, EventArgs e)
        {
            if (roomStatus == 1)
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4026"), getLanguage("_softwarename")) == DialogResult.Yes)
                    BusinessLogicBridge.DataStore.updateRoomStatus(roomID, 6);
            }
            else
            {
                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4027"), getLanguage("_softwarename")) == DialogResult.Yes)
                    BusinessLogicBridge.DataStore.updateRoomStatus(roomID, 1);
            }

            //
            mParent.refreshDashBoard();
        }

        void MenuCutElectric_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show(getLanguage("_confirm"), "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (UserForms.utilClass.showPopupElectricPassword(this, roomCutOffStatus, roomName) == DialogResult.OK)
                {
                    if (roomCutOffStatus == 0)
                    {
                        BusinessLogicBridge.DataStore.updateElectricMeterByMeterID(meterID, 1);
                        pictureBox3.Image = imageCollection3.Images[2];
                        roomCutOffStatus = 1;
                    }
                    else
                    {
                        BusinessLogicBridge.DataStore.updateElectricMeterByMeterID(meterID, 0);
                        pictureBox3.Image = imageCollection3.Images[3];
                        roomCutOffStatus = 0;
                    }
                    ChangeLanguage();
                }
                //
                mParent.refreshDashBoard();
            }
        }

        void menu_RoomDetail_Click(object sender, EventArgs e)
        {
            UserForms.utilClass.showPopupRoomDetail(this, roomID);
        }

        void menu_Payment_Click(object sender, EventArgs e)
        {
            mParent.openViewInvoiceInfo(roomID);
        }

        void menu_PrintSlip_Click(object sender, EventArgs e)
        {
            mParent.openInvoiceInfo(roomID);
        }

        void menu_CancelReserve_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4013"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                mParent.openCancelReserve(roomID);
            }
        }

        void menu_CancelLeave_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4012"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                mParent.openCancelLeave(roomID);
            }
        }

        void menu_Reserve_Click(object sender, EventArgs e)
        {
            mParent.openReserve(roomID);
        }

        void menu_Leave_Click(object sender, EventArgs e)
        {
            mParent.openLeave(roomID);
        }

        void menu_CheckOut_Click(object sender, EventArgs e)
        {
            mParent.openCheckOut(roomID);
        }

        void menu_CheckIn_Click(object sender, EventArgs e)
        {
            mParent.openCheckIn(roomID);
        }

        private void MenuPopup_Opening_1(object sender, CancelEventArgs e)
        {
            CheckPopup();
        }
    }
}
