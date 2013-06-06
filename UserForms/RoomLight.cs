using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace DXWindowsApplication2.UserForms
{
    public partial class RoomLight : DevExpress.XtraEditors.XtraUserControl
    {
        private static int CurrentBuilding = 0;
        private DataTable _dataStore;
        // private MessageBox MyPopup;

        public RoomLight()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;
            this.Load += new EventHandler(RoomLight_Load);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoard();

        }

        void RoomLight_Load(object sender, EventArgs e)
        {
            renderItem();
            renderBuildingButton();
        }

        public override void Refresh()
        {
            base.Refresh();
        }
        public RoomLight(int status)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoardByStatus(status);
            renderItem();
            renderBuildingButton();



            //Thread t = new Thread(new ThreadStart(renderItem));
            //t.Interrupt();
            // t.SetApartmentState(ApartmentState.STA);
            //t.TrySetApartmentState(ApartmentState.MTA);
            //t.Start();      


        }
        public RoomLight(int status, int meterStatus)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoardByElectricMeterStatus(meterStatus);
            renderItem();
            renderBuildingButton();
        }
        public RoomLight(int status, int meterStatus, int meterCut)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoardByElectricMeterCut(meterCut);
            renderItem();
            renderBuildingButton();
        }

        public RoomLight(int status, int meterStatus, int meterCut, int RoomTypeID)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoardByRoomtypeID(RoomTypeID);
            renderItem();
            renderBuildingButton();
        }

        private void getData(int type)
        {
            // 1 = get ALL
            // 2 = get By Building ID
            // 3 = get by room status       


        }

        public void renderItem()
        {

            flowLayoutPanel1.SizeChanged += new EventHandler(flowLayoutPanel1_SizeChanged);
            flowLayoutPanel1.Scroll += new ScrollEventHandler(flowLayoutPanel1_Scroll);

            Color CustomGreen = Color.FromArgb(164, 246, 92);
            Color CustomRed = Color.FromArgb(255, 108, 88);
            Color CustomWhite = Color.White;
            Color CustomYellow = Color.FromArgb(254, 248, 91);
            Color CustomOrange = Color.Orange;
            Color CustomGray = Color.Gray;

            Color StatusColor = CustomWhite;

            DataTable RoomTbl = _dataStore;

            flowLayoutPanel1.SuspendLayout();

            int roomStutus = 1;
            int meterStatus = 1;

            for (int i = 0; i < RoomTbl.Rows.Count; i++)
            {
                //Button Roombox = new Button();

                RoomItemButton Roombox = new RoomItemButton();
                //GroupControl Roombox = new GroupControl();
                Roombox.lbs_roomID.Text = RoomTbl.Rows[i]["room_id"].ToString();

                if ((string)RoomTbl.Rows[i]["room_label"] == "")
                {
                    Roombox.labelControl2.Text = RoomTbl.Rows[i]["building_code"] + (string)RoomTbl.Rows[i]["floor_code"] + (string)RoomTbl.Rows[i]["room_code"];
                }
                else
                {
                    Roombox.labelControl2.Text = (string)RoomTbl.Rows[i]["room_label"];
                }
                Roombox.labelControl4.Text = RoomTbl.Rows[i]["building_code"] + RoomTbl.Rows[i]["floor_code"].ToString() + (string)RoomTbl.Rows[i]["room_code"];

                roomStutus = RoomTbl.Rows[i]["room_status"].To<int>();

                switch (roomStutus)
                {
                    case 1:
                        Roombox.panel1.BackColor = CustomWhite;
                        break;
                    case 2:
                        Roombox.panel1.BackColor = CustomGreen;
                        break;
                    case 3:
                        Roombox.panel1.BackColor = CustomYellow;
                        break;
                    case 4:
                        Roombox.panel1.BackColor = CustomRed;
                        break;
                    case 5:
                        Roombox.panel1.BackColor = CustomOrange;
                        break;
                    case 6:
                        Roombox.panel1.BackColor = CustomGray;
                        break;
                }

                bool RoomOverdued = BusinessLogicBridge.DataStore.checkPaymentOverdued(RoomTbl.Rows[i]["room_id"].To<int>());

                if (RoomOverdued == true)
                {
                    // Yes Overdued
                    Roombox.labelControlPayment.Text = MainForm.getLanguage("_overdue");
                    Roombox.labelControlPayment.Visible = true;
                }

                if (RoomTbl.Rows[i]["meter_status"] == System.DBNull.Value)
                {
                    meterStatus = 0;
                }
                else
                {
                    meterStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_status"]);
                }

                if (meterStatus == 0)
                {
                    Roombox.pictureBox2.Image = imageCollection2.Images[1];
                }

                int cutoffStatus = 1;
                Roombox.pictureBox3.Image = imageCollection3.Images[2];


                if (RoomTbl.Rows[i]["meter_cut"] == System.DBNull.Value)
                {
                    cutoffStatus = 0;
                }
                else
                {
                    cutoffStatus = Convert.ToInt32(RoomTbl.Rows[i]["meter_cut"]);
                }


                if (System.IO.File.Exists(RoomTbl.Rows[i]["roomtype_icon"].ToString()) == false)
                {
                    string userPath = AppDomain.CurrentDomain.BaseDirectory;
                    Roombox.pictureEdit1.Image = Image.FromFile(userPath + "/" + "no-image.jpeg");
                }
                else
                {
                    Roombox.pictureEdit1.Image = Image.FromFile(RoomTbl.Rows[i]["roomtype_icon"].ToString());
                }


                //
                Roombox.roomType = 0;
                Roombox.roomStatus = roomStutus;
                Roombox.roomCutOffStatus = 1;//cutoffStatus;
                Roombox.meterStatus = meterStatus;
                Roombox.roomID = int.Parse(Roombox.lbs_roomID.Text);
                Roombox.roomName = Roombox.labelControl2.Text;
                Roombox.meterID = int.Parse(RoomTbl.Rows[i]["current_electricity_id"].ToString());
                //

                flowLayoutPanel1.Controls.Add(Roombox);

            }

            flowLayoutPanel1.ResumeLayout();
        }

        public void renderBuildingButton()
        {
            bttViewAll.Text = MainForm.getLanguage("_viewRoomAll");
            DataTable BuildingDataTable = new DataTable();
            BuildingDataTable = BusinessLogicBridge.DataStore.getBuilding();
            for (int i = 0; i < BuildingDataTable.Rows.Count; i++)
            {
                SimpleButton BuildingButton = new SimpleButton();
                BuildingButton.AutoWidthInLayoutControl = true;
                string BuildingTxt = "";

                BuildingButton.Text = BuildingTxt + (string)BuildingDataTable.Rows[i]["building_label"];
                BuildingButton.Name = Convert.ToString(BuildingDataTable.Rows[i]["building_id"]);
                BuildingButton.MouseClick += new MouseEventHandler(BuildingButton_MouseClick);

                flowLayoutPanel2.Controls.Add(BuildingButton);

            }


        }

        void BuildingButton_MouseClick(object sender, MouseEventArgs e)
        {


            SimpleButton btn = (SimpleButton)sender;
            int building_id = Convert.ToInt32(btn.Name);
            CurrentBuilding = building_id;
            flowLayoutPanel1.Hide();

            // flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoardByBuilding(building_id);
            renderItem();         //throw new NotImplementedException();        

            flowLayoutPanel1.Show();
        }

        void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("You are in the Application.Idle event.");
        }

        void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
            //MessageBox.Show("You are in the Application.Idle event.");

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Hide();
            flowLayoutPanel1.Controls.Clear();
            GC.Collect();
            flowLayoutPanel1.Show();
            Application.DoEvents();

        }

        public void simpleButton1_Click_1(object sender, EventArgs e)
        {



            //MessageBox.Show("กำลังโหลด..");

            this.DoubleBuffered = true;
            flowLayoutPanel1.Hide();
            flowLayoutPanel1.Controls.Clear();
            _dataStore = BusinessLogicBridge.DataStore.getDataDashBoard();
            renderItem();
            flowLayoutPanel1.Show();


        }
        public void PopUp()
        {
            XtraMessageBox.Show("กำลังโหลด...");

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                //WS_EX_COMPOSITED. Prevents flickering.

                //cp.ExStyle |= 0x00080000; //WS_EX_LAYERED. Transparency key
                return cp;
            }
        }
    }
}
