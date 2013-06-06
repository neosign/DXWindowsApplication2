namespace DXWindowsApplication2.UserForms
{
    partial class RoomItemButton
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomItemButton));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.MenuPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_CheckIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_CheckOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Reserve = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Leave = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_CancelReserve = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_CancelLeave = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Fix = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_PrintSlip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Payment = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Eletric = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_RoomDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControlPayment = new DevExpress.XtraEditors.LabelControl();
            this.lbs_roomID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageCollection2 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageCollection3 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageCollection4 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.MenuPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection4)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.ContextMenuStrip = this.MenuPopup;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.pictureBox3);
            this.panelControl1.Controls.Add(this.pictureBox2);
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(126, 81);
            this.panelControl1.TabIndex = 48;
            // 
            // MenuPopup
            // 
            this.MenuPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_CheckIn,
            this.menu_CheckOut,
            this.menu_Reserve,
            this.menu_Leave,
            this.menu_CancelReserve,
            this.menu_CancelLeave,
            this.menu_Fix,
            this.toolStripSeparator1,
            this.menu_PrintSlip,
            this.toolStripSeparator2,
            this.menu_Payment,
            this.menu_Eletric,
            this.menu_RoomDetail});
            this.MenuPopup.Name = "MenuPopup";
            this.MenuPopup.Size = new System.Drawing.Size(194, 258);
            this.MenuPopup.Opening += new System.ComponentModel.CancelEventHandler(this.MenuPopup_Opening_1);
            // 
            // menu_CheckIn
            // 
            this.menu_CheckIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_CheckIn.Image = ((System.Drawing.Image)(resources.GetObject("menu_CheckIn.Image")));
            this.menu_CheckIn.Name = "menu_CheckIn";
            this.menu_CheckIn.Size = new System.Drawing.Size(193, 22);
            this.menu_CheckIn.Text = "ย้ายเข้า";
            // 
            // menu_CheckOut
            // 
            this.menu_CheckOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_CheckOut.Image = ((System.Drawing.Image)(resources.GetObject("menu_CheckOut.Image")));
            this.menu_CheckOut.Name = "menu_CheckOut";
            this.menu_CheckOut.Size = new System.Drawing.Size(193, 22);
            this.menu_CheckOut.Text = "ย้ายออก";
            // 
            // menu_Reserve
            // 
            this.menu_Reserve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_Reserve.Image = ((System.Drawing.Image)(resources.GetObject("menu_Reserve.Image")));
            this.menu_Reserve.Name = "menu_Reserve";
            this.menu_Reserve.Size = new System.Drawing.Size(193, 22);
            this.menu_Reserve.Text = "จองห้อง";
            // 
            // menu_Leave
            // 
            this.menu_Leave.Name = "menu_Leave";
            this.menu_Leave.Size = new System.Drawing.Size(193, 22);
            this.menu_Leave.Text = "แจ้งย้ายออก";
            // 
            // menu_CancelReserve
            // 
            this.menu_CancelReserve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_CancelReserve.Name = "menu_CancelReserve";
            this.menu_CancelReserve.Size = new System.Drawing.Size(193, 22);
            this.menu_CancelReserve.Text = "ยกเลิกจอง";
            // 
            // menu_CancelLeave
            // 
            this.menu_CancelLeave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_CancelLeave.Name = "menu_CancelLeave";
            this.menu_CancelLeave.Size = new System.Drawing.Size(193, 22);
            this.menu_CancelLeave.Text = "ยกเลิกแจ้งย้ายออก";
            // 
            // menu_Fix
            // 
            this.menu_Fix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_Fix.Name = "menu_Fix";
            this.menu_Fix.Size = new System.Drawing.Size(193, 22);
            this.menu_Fix.Text = "ซ่อมแซม/ยกเลิกซ่อมแซม";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // menu_PrintSlip
            // 
            this.menu_PrintSlip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_PrintSlip.Image = ((System.Drawing.Image)(resources.GetObject("menu_PrintSlip.Image")));
            this.menu_PrintSlip.Name = "menu_PrintSlip";
            this.menu_PrintSlip.Size = new System.Drawing.Size(193, 22);
            this.menu_PrintSlip.Text = "ออกใบแจ้งหนี้";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // menu_Payment
            // 
            this.menu_Payment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_Payment.Image = ((System.Drawing.Image)(resources.GetObject("menu_Payment.Image")));
            this.menu_Payment.Name = "menu_Payment";
            this.menu_Payment.Size = new System.Drawing.Size(193, 22);
            this.menu_Payment.Text = "ชำระเงิน";
            // 
            // menu_Eletric
            // 
            this.menu_Eletric.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_Eletric.Image = ((System.Drawing.Image)(resources.GetObject("menu_Eletric.Image")));
            this.menu_Eletric.Name = "menu_Eletric";
            this.menu_Eletric.Size = new System.Drawing.Size(193, 22);
            this.menu_Eletric.Text = "ตัดไฟ";
            // 
            // menu_RoomDetail
            // 
            this.menu_RoomDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu_RoomDetail.Name = "menu_RoomDetail";
            this.menu_RoomDetail.Size = new System.Drawing.Size(193, 22);
            this.menu_RoomDetail.Text = "รายละเอียดห้องพัก";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(97, 52);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(24, 24);
            this.pictureEdit1.TabIndex = 55;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(38, 42);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(63, 13);
            this.labelControl4.TabIndex = 54;
            this.labelControl4.Text = "labelControl4";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 57);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 49;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(4, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 47;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelControlPayment);
            this.panel1.Controls.Add(this.lbs_roomID);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 21);
            this.panel1.TabIndex = 52;
            // 
            // labelControlPayment
            // 
            this.labelControlPayment.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControlPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControlPayment.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlPayment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPayment.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControlPayment.Location = new System.Drawing.Point(50, 1);
            this.labelControlPayment.Name = "labelControlPayment";
            this.labelControlPayment.Padding = new System.Windows.Forms.Padding(2);
            this.labelControlPayment.Size = new System.Drawing.Size(70, 19);
            this.labelControlPayment.TabIndex = 52;
            this.labelControlPayment.Text = "ค้างชำระ";
            this.labelControlPayment.Visible = false;
            // 
            // lbs_roomID
            // 
            this.lbs_roomID.Location = new System.Drawing.Point(7, 3);
            this.lbs_roomID.Name = "lbs_roomID";
            this.lbs_roomID.Size = new System.Drawing.Size(38, 13);
            this.lbs_roomID.TabIndex = 52;
            this.lbs_roomID.Text = "room_id";
            this.lbs_roomID.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(3, 1);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 18);
            this.labelControl2.TabIndex = 51;
            this.labelControl2.Text = "A101";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "flagWhite.png");
            this.imageCollection1.Images.SetKeyName(1, "flag-green-icon[1].png");
            this.imageCollection1.Images.SetKeyName(2, "flag-yellow-icon[1].png");
            this.imageCollection1.Images.SetKeyName(3, "flag-red-icon[1].png");
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageSize = new System.Drawing.Size(28, 28);
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.Images.SetKeyName(0, "Meter4.png");
            this.imageCollection2.Images.SetKeyName(1, "Meter4X.png");
            // 
            // imageCollection3
            // 
            this.imageCollection3.ImageSize = new System.Drawing.Size(20, 20);
            this.imageCollection3.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection3.ImageStream")));
            this.imageCollection3.Images.SetKeyName(0, "on.png");
            this.imageCollection3.Images.SetKeyName(1, "off.png");
            this.imageCollection3.Images.SetKeyName(2, "lightbulb-icon[1].png");
            this.imageCollection3.Images.SetKeyName(3, "lightbulb-Error.png");
            // 
            // imageCollection4
            // 
            this.imageCollection4.ImageSize = new System.Drawing.Size(22, 22);
            this.imageCollection4.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection4.ImageStream")));
            this.imageCollection4.Images.SetKeyName(0, "Air.png");
            this.imageCollection4.Images.SetKeyName(1, "Fan.png");
            // 
            // RoomItemButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "RoomItemButton";
            this.Size = new System.Drawing.Size(126, 81);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.MenuPopup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox2;
        public DevExpress.XtraEditors.PanelControl panelControl1;
        public System.Windows.Forms.PictureBox pictureBox3;
        public DevExpress.XtraEditors.LabelControl labelControl2;
        public System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ContextMenuStrip MenuPopup;
        private System.Windows.Forms.ToolStripMenuItem menu_Reserve;
        private System.Windows.Forms.ToolStripMenuItem menu_CheckIn;
        private System.Windows.Forms.ToolStripMenuItem menu_CheckOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_PrintSlip;
        private System.Windows.Forms.ToolStripMenuItem menu_Payment;
        private System.Windows.Forms.ToolStripMenuItem menu_Eletric;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_RoomDetail;
        public DevExpress.XtraEditors.LabelControl lbs_roomID;
        public DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.ToolStripMenuItem menu_CancelReserve;
        private System.Windows.Forms.ToolStripMenuItem menu_CancelLeave;
        private System.Windows.Forms.ToolStripMenuItem menu_Fix;
        private System.Windows.Forms.ToolStripMenuItem menu_Leave;
        public DevExpress.XtraEditors.LabelControl labelControlPayment;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.Utils.ImageCollection imageCollection3;
        private DevExpress.Utils.ImageCollection imageCollection4;
    }
}
