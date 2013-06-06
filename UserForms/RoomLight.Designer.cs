namespace DXWindowsApplication2.UserForms
{
    partial class RoomLight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomLight));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageCollection2 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageCollection3 = new DevExpress.Utils.ImageCollection(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.bttViewAll = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.imageCollection4 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection4)).BeginInit();
            this.SuspendLayout();
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
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Gainsboro;
            this.flowLayoutPanel2.Controls.Add(this.bttViewAll);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 552);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(748, 31);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // bttViewAll
            // 
            this.bttViewAll.Location = new System.Drawing.Point(3, 3);
            this.bttViewAll.Name = "bttViewAll";
            this.bttViewAll.Size = new System.Drawing.Size(75, 23);
            this.bttViewAll.TabIndex = 0;
            this.bttViewAll.Text = "ดูห้องทั้งหมด";
            this.bttViewAll.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(748, 552);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // imageCollection4
            // 
            this.imageCollection4.ImageSize = new System.Drawing.Size(22, 22);
            this.imageCollection4.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection4.ImageStream")));
            this.imageCollection4.Images.SetKeyName(0, "Air.png");
            this.imageCollection4.Images.SetKeyName(1, "Fan.png");
            // 
            // RoomLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "RoomLight";
            this.Size = new System.Drawing.Size(748, 583);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.Utils.ImageCollection imageCollection3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton bttViewAll;
        private DevExpress.Utils.ImageCollection imageCollection4;




    }
}
