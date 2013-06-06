using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DXWindowsApplication2;
using System.Threading;

namespace DXWindowsApplication2.UserForms
{
    public partial class Room : DevExpress.XtraEditors.XtraUserControl
    {
        public Room()
        {
            
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Dock = DockStyle.Fill;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
           // this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
           //ControlStyles.AllPaintingInWmPaint, true);

            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            loadAllRoom();
            
            this.flowLayoutPanel1.ResumeLayout();
            this.ResumeLayout();
            

        }
        private void loadAllRoom() {
            //flowLayoutPanel1.SuspendLayout();
           
            LastNameGenerator genName = new LastNameGenerator();
            // DataTable RoomTbl = new DataTable();
            //sxbillingDataSet.roomDataTable RoomTbl = new sxbillingDataSet.roomDataTable();
            //DataLayer.BusinessLogic DataStore = new DataLayer.BusinessLogic();
            //DataStore.Connect();

            //sxbillingDataSetTableAdapters.roomTableAdapter RoomTableAdapter = new sxbillingDataSetTableAdapters.roomTableAdapter();
            //int art4 = RoomTbl.Count;
            //sxbillingDataSet.roomDataTable RoomTbl = new sxbillingDataSet.roomDataTable();
            //RoomTableAdapter.Fill(RoomTbl);

            DataTable RoomTbl = BusinessLogicBridge.DataStore.getDataDashBoard();




            PanelControl art = new PanelControl();
            flowLayoutPanel1.SuspendLayout();
            this.DoubleBuffered = true;
            
            for (int i = 0; i < RoomTbl.Rows.Count; i++)
            {

                //roomType.Adapter.
                //roomType.GetData();

                //DevExpress.XtraEditors.PanelControl Roombox = new DevExpress.XtraEditors.PanelControl();
                RoomItemRev2 Roombox = new RoomItemRev2();
                Roombox.SuspendLayout();
                Roombox.Width = 145;
                Roombox.Height = 110;
                Random icox = new Random();
                int rdx = icox.Next(0, 6);
                //Roombox.BackgroundImageLayout = ImageLayout.Center;
                //Roombox.BackgroundImage = imageCollection2.Images[rdx];
                Color CustomGreen = Color.FromArgb(164, 246, 92);
                Color CustomRed = Color.FromArgb(255, 108, 88);
                Color CustomWhite = Color.FromArgb(240, 240, 240);
                Color CustomYellow = Color.FromArgb(254, 248, 91);
                Color StatusColor = CustomWhite;


                Roombox.labelControl2.Text = "ว่าง";
                //if (rdx == 0) {
                //    StatusColor = CustomWhite;
                //   // Roombox.labelControl2.Text = "ว่าง";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[3];

                //}
                //if (rdx == 2)
                //{
                //    StatusColor = CustomGreen;
                //   // Roombox.labelControl2.Text = "เช่า";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[0];
                //}
                //if (rdx == 3)
                //{
                //    StatusColor = CustomRed;
                //    //Roombox.labelControl2.Text = "แจ้งย้ายออก";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[1];
                //}
                //if (rdx == 4)
                //{
                //    StatusColor = CustomYellow;
                //   // Roombox.labelControl2.Text = "จอง";
                //    Roombox.pictureEdit1.Image = imageCollection3.Images[1];
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[2];
                //}
                //if (rdx > 4)
                //{
                //    StatusColor = CustomGreen;
                //    //Roombox.labelControl2.Text = "เช่า";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[0];
                //}
                Roombox.labelControl2.Text = "";

                Roombox.labelControl6.Text = (string)Convert.ToString(20.01 * rdx);
                //Roombox.label1.Autosize = false;
                int roomStutus = 1;

                roomStutus = Convert.ToInt32(RoomTbl.Rows[i]["room_status"]);

                if (roomStutus == 1)
                {
                    Roombox.panel1.BackColor = CustomWhite;
                    Roombox.pictureBox1.Image = imageCollection2.Images[0];
                }
                else if (roomStutus == 2)
                {
                    Roombox.panel1.BackColor = CustomGreen;
                    Roombox.pictureBox1.Image = imageCollection2.Images[1];

                }
                else if (roomStutus == 3)
                {
                    Roombox.panel1.BackColor = CustomYellow;
                    Roombox.pictureBox1.Image = imageCollection2.Images[2];

                }
                else if (roomStutus == 4)
                {
                    Roombox.panel1.BackColor = CustomRed;
                    Roombox.pictureBox1.Image = imageCollection2.Images[3];

                }
                int meterStatus = 1;

                meterStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_status"]);
                if (meterStatus == 0)
                {
                    Roombox.pictureBox2.Image = imageCollection4.Images[1];
                }

                int cutoffStatus = 1;
                cutoffStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_cut"]);
                if (cutoffStatus == 0)
                {
                    Roombox.pictureBox3.Image = imageCollection3.Images[1];
                }




                Roombox.labelControl1.Text = (string)Convert.ToString(RoomTbl.Rows[i]["room_label"]);

                //Roombox.CaptionImage = imageCollection1.Images[rdx];
                //Roombox.CaptionImagePadding = new System.Windows.Forms.Padding(-10, 0, 0, 0);
                //Roombox.BackColor = Color.Aqua;
                // Roombox.BackColor = Color.DarkBlue;
                // Roombox.Location = new System.Drawing.Point(4, 4);
                //Roombox.Name = "groupControl1";
                //Roombox.Size = new System.Drawing.Size(200, 100);
                Roombox.TabIndex = 0;

                // Roombox.Appearance.ForeColor = Color.DarkKhaki; ;
                //Roombox.Text = "groupControl1";
                //Roombox.Text = 30;
                // Roombox.AppearanceCaption.ForeColor = Color.Red;
                // Roombox.AppearanceCaption.BackColor2 = Color.Plum; ;



                //Roombox.Text = "ห้องหมายเลข: R40" + i+ " สถานะ: ไม่ว่าง";
                RoomItem item = new RoomItem(genName.GetLastName() + " " + genName.GetLastName(), "แอร์", "ไม่ว่าง", "20.0", "500.0", "400.0");
                Roombox.Controls.Add(item);
                // xtraScrollableControl1.Controls.Add(RoomBox);

                Roombox.ResumeLayout();
                //groupControl1.Name = "Room" + i;
                //groupControl1.Text = "Room" + i;
                
               flowLayoutPanel1.Controls.Add(Roombox);
                //art.Controls.Add(Roombox);


            }
            //flowLayoutPanel1.Visible = true;
            //flowLayoutPanel1.Controls.AddRange(new Control[] { art });
            flowLayoutPanel1.ResumeLayout();

        
        }
        private void loadRoomByStatus(int status) {
            //flowLayoutPanel1.SuspendLayout();
            this.Dock = DockStyle.Fill;
            LastNameGenerator genName = new LastNameGenerator();
            // DataTable RoomTbl = new DataTable();
            //sxbillingDataSet.roomDataTable RoomTbl = new sxbillingDataSet.roomDataTable();
            //DataLayer.BusinessLogic DataStore = new DataLayer.BusinessLogic();
            //DataStore.Connect();

            //sxbillingDataSetTableAdapters.roomTableAdapter RoomTableAdapter = new sxbillingDataSetTableAdapters.roomTableAdapter();
            //int art4 = RoomTbl.Count;
            //sxbillingDataSet.roomDataTable RoomTbl = new sxbillingDataSet.roomDataTable();
            //RoomTableAdapter.Fill(RoomTbl);

            DataTable RoomTbl = BusinessLogicBridge.DataStore.getDataDashBoardByRoomStatus(status, 1, 1, 1);




            PanelControl art = new PanelControl();

            for (int i = 0; i < RoomTbl.Rows.Count; i++)
            {

                //roomType.Adapter.
                //roomType.GetData();

                //DevExpress.XtraEditors.PanelControl Roombox = new DevExpress.XtraEditors.PanelControl();
                RoomItemRev2 Roombox = new RoomItemRev2();
                Roombox.SuspendLayout();
                Roombox.Width = 145;
                Roombox.Height = 110;
                Random icox = new Random();
                int rdx = icox.Next(0, 6);
                //Roombox.BackgroundImageLayout = ImageLayout.Center;
                //Roombox.BackgroundImage = imageCollection2.Images[rdx];
                Color CustomGreen = Color.FromArgb(164, 246, 92);
                Color CustomRed = Color.FromArgb(255, 108, 88);
                Color CustomWhite = Color.FromArgb(240, 240, 240);
                Color CustomYellow = Color.FromArgb(254, 248, 91);
                Color StatusColor = CustomWhite;


                Roombox.labelControl2.Text = "ว่าง";
                //if (rdx == 0) {
                //    StatusColor = CustomWhite;
                //   // Roombox.labelControl2.Text = "ว่าง";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[3];

                //}
                //if (rdx == 2)
                //{
                //    StatusColor = CustomGreen;
                //   // Roombox.labelControl2.Text = "เช่า";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[0];
                //}
                //if (rdx == 3)
                //{
                //    StatusColor = CustomRed;
                //    //Roombox.labelControl2.Text = "แจ้งย้ายออก";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[1];
                //}
                //if (rdx == 4)
                //{
                //    StatusColor = CustomYellow;
                //   // Roombox.labelControl2.Text = "จอง";
                //    Roombox.pictureEdit1.Image = imageCollection3.Images[1];
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[2];
                //}
                //if (rdx > 4)
                //{
                //    StatusColor = CustomGreen;
                //    //Roombox.labelControl2.Text = "เช่า";
                //    Roombox.pictureEdit4.Image = imageCollection2.Images[0];
                //}
                Roombox.labelControl2.Text = "";

                Roombox.labelControl6.Text = (string)Convert.ToString(20.01 * rdx);
                //Roombox.label1.Autosize = false;
                int roomStutus = 1;

                roomStutus = Convert.ToInt32(RoomTbl.Rows[i]["room_status"]);

                if (roomStutus == 1)
                {
                    Roombox.panel1.BackColor = CustomWhite;
                    Roombox.pictureBox1.Image = imageCollection2.Images[0];
                }
                else if (roomStutus == 2)
                {
                    Roombox.panel1.BackColor = CustomGreen;
                    Roombox.pictureBox1.Image = imageCollection2.Images[1];

                }
                else if (roomStutus == 3)
                {
                    Roombox.panel1.BackColor = CustomYellow;
                    Roombox.pictureBox1.Image = imageCollection2.Images[2];

                }
                else if (roomStutus == 4)
                {
                    Roombox.panel1.BackColor = CustomRed;
                    Roombox.pictureBox1.Image = imageCollection2.Images[3];

                }
                int meterStatus = 1;

                meterStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_status"]);
                if (meterStatus == 0)
                {
                    Roombox.pictureBox2.Image = imageCollection4.Images[1];
                }

                int cutoffStatus = 1;
                cutoffStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_cut"]);
                if (cutoffStatus == 0)
                {
                    Roombox.pictureBox3.Image = imageCollection3.Images[1];
                }




                Roombox.labelControl1.Text = (string)Convert.ToString(RoomTbl.Rows[i]["room_label"]);

                //Roombox.CaptionImage = imageCollection1.Images[rdx];
                //Roombox.CaptionImagePadding = new System.Windows.Forms.Padding(-10, 0, 0, 0);
                //Roombox.BackColor = Color.Aqua;
                // Roombox.BackColor = Color.DarkBlue;
                // Roombox.Location = new System.Drawing.Point(4, 4);
                //Roombox.Name = "groupControl1";
                //Roombox.Size = new System.Drawing.Size(200, 100);
                Roombox.TabIndex = 0;

                // Roombox.Appearance.ForeColor = Color.DarkKhaki; ;
                //Roombox.Text = "groupControl1";
                //Roombox.Text = 30;
                // Roombox.AppearanceCaption.ForeColor = Color.Red;
                // Roombox.AppearanceCaption.BackColor2 = Color.Plum; ;



                //Roombox.Text = "ห้องหมายเลข: R40" + i+ " สถานะ: ไม่ว่าง";
                RoomItem item = new RoomItem(genName.GetLastName() + " " + genName.GetLastName(), "แอร์", "ไม่ว่าง", "20.0", "500.0", "400.0");
                Roombox.Controls.Add(item);
                // xtraScrollableControl1.Controls.Add(RoomBox);

                Roombox.ResumeLayout();
                //groupControl1.Name = "Room" + i;
                //groupControl1.Text = "Room" + i;
                flowLayoutPanel1.Controls.Add(Roombox);
                //art.Controls.Add(Roombox);


            }
            // flowLayoutPanel1.Controls.AddRange(new Control[] { art });
            //flowLayoutPanel1.ResumeLayout();

        
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           // flowLayoutPanel1.SuspendLayout();
           // this.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            loadRoomByStatus(1);
           // flowLayoutPanel1.ResumeLayout();
           // this.ResumeLayout();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel1.SuspendLayout();
           // this.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            loadAllRoom();
           // flowLayoutPanel1.ResumeLayout();
           // this.ResumeLayout();

        }

        
       
    }
}
