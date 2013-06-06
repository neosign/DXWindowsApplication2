namespace DXWindowsApplication2.UserForms
{
    partial class BookRefund
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RoomNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenantName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reserve_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlagReserveText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlagReserve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.textEditReserveID = new DevExpress.XtraEditors.TextEdit();
            this.textEditAmount = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.textEditRoomNo = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditReserveID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRoomNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(496, 380);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RoomNumber,
            this.TenantName,
            this.Amount,
            this.reserve_date,
            this.colFlagReserveText,
            this.colFlagReserve});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.ShowCloseButton = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // RoomNumber
            // 
            this.RoomNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.RoomNumber.AppearanceHeader.Options.UseFont = true;
            this.RoomNumber.Caption = "หมายเลขห้อง";
            this.RoomNumber.FieldName = "coderef";
            this.RoomNumber.Name = "RoomNumber";
            this.RoomNumber.OptionsColumn.AllowEdit = false;
            this.RoomNumber.OptionsColumn.AllowFocus = false;
            this.RoomNumber.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.RoomNumber.OptionsColumn.AllowMove = false;
            this.RoomNumber.Visible = true;
            this.RoomNumber.VisibleIndex = 0;
            // 
            // TenantName
            // 
            this.TenantName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.TenantName.AppearanceHeader.Options.UseFont = true;
            this.TenantName.Caption = "ชื่อผู้จอง";
            this.TenantName.FieldName = "reserve_name";
            this.TenantName.Name = "TenantName";
            this.TenantName.OptionsColumn.AllowEdit = false;
            this.TenantName.OptionsColumn.AllowFocus = false;
            this.TenantName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.TenantName.OptionsColumn.AllowMove = false;
            this.TenantName.Visible = true;
            this.TenantName.VisibleIndex = 1;
            // 
            // Amount
            // 
            this.Amount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Amount.AppearanceHeader.Options.UseFont = true;
            this.Amount.Caption = "จำนวนเงิน";
            this.Amount.DisplayFormat.FormatString = "{0:c2}";
            this.Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Amount.FieldName = "reserve_payments";
            this.Amount.Name = "Amount";
            this.Amount.OptionsColumn.AllowEdit = false;
            this.Amount.OptionsColumn.AllowFocus = false;
            this.Amount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Amount.OptionsColumn.AllowMove = false;
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 2;
            // 
            // reserve_date
            // 
            this.reserve_date.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.reserve_date.AppearanceHeader.Options.UseFont = true;
            this.reserve_date.Caption = "วันที่จอง";
            this.reserve_date.FieldName = "reserve_create_date";
            this.reserve_date.Name = "reserve_date";
            this.reserve_date.OptionsColumn.AllowEdit = false;
            this.reserve_date.OptionsColumn.AllowFocus = false;
            this.reserve_date.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.reserve_date.OptionsColumn.AllowMove = false;
            this.reserve_date.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.reserve_date.Visible = true;
            this.reserve_date.VisibleIndex = 3;
            // 
            // colFlagReserveText
            // 
            this.colFlagReserveText.Caption = " ";
            this.colFlagReserveText.FieldName = "reserve_flag_text";
            this.colFlagReserveText.Name = "colFlagReserveText";
            this.colFlagReserveText.OptionsColumn.AllowEdit = false;
            this.colFlagReserveText.OptionsColumn.AllowFocus = false;
            this.colFlagReserveText.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFlagReserveText.OptionsColumn.AllowMove = false;
            this.colFlagReserveText.Visible = true;
            this.colFlagReserveText.VisibleIndex = 4;
            // 
            // colFlagReserve
            // 
            this.colFlagReserve.Caption = " ";
            this.colFlagReserve.FieldName = "reserve_flag";
            this.colFlagReserve.Name = "colFlagReserve";
            this.colFlagReserve.OptionsColumn.AllowEdit = false;
            this.colFlagReserve.OptionsColumn.AllowFocus = false;
            this.colFlagReserve.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colFlagReserve.OptionsColumn.AllowMove = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 378);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(403, 26);
            this.panelControl1.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(74, 88);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "จำนวนเงิน :";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.gridControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(500, 404);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "รายการผู้จองทั้งหมด";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(255, 128);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 35);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "ยกเลิก";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(7, 7);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl3);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(908, 404);
            this.splitContainerControl2.SplitterPosition = 500;
            this.splitContainerControl2.TabIndex = 21;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(403, 378);
            this.panelControl3.TabIndex = 12;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.xtraScrollableControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(403, 378);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "คืนเงินจอง";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.textEditReserveID);
            this.xtraScrollableControl1.Controls.Add(this.textEditAmount);
            this.xtraScrollableControl1.Controls.Add(this.labelControl1);
            this.xtraScrollableControl1.Controls.Add(this.simpleButton1);
            this.xtraScrollableControl1.Controls.Add(this.simpleButton2);
            this.xtraScrollableControl1.Controls.Add(this.labelControl4);
            this.xtraScrollableControl1.Controls.Add(this.labelControl5);
            this.xtraScrollableControl1.Controls.Add(this.textEditName);
            this.xtraScrollableControl1.Controls.Add(this.textEditRoomNo);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(399, 354);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // textEditReserveID
            // 
            this.textEditReserveID.Location = new System.Drawing.Point(350, 16);
            this.textEditReserveID.Name = "textEditReserveID";
            this.textEditReserveID.Size = new System.Drawing.Size(100, 20);
            this.textEditReserveID.TabIndex = 24;
            this.textEditReserveID.Visible = false;
            // 
            // textEditAmount
            // 
            this.textEditAmount.Location = new System.Drawing.Point(133, 85);
            this.textEditAmount.Name = "textEditAmount";
            this.textEditAmount.Properties.ReadOnly = true;
            this.textEditAmount.Size = new System.Drawing.Size(211, 20);
            this.textEditAmount.TabIndex = 23;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(133, 128);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(89, 35);
            this.simpleButton2.TabIndex = 17;
            this.simpleButton2.Text = "ยืนยัน";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(58, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 13);
            this.labelControl4.TabIndex = 20;
            this.labelControl4.Text = "หมายเลขห้อง :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(81, 53);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "ชื่อผู้จอง :";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(133, 50);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.ReadOnly = true;
            this.textEditName.Size = new System.Drawing.Size(211, 20);
            this.textEditName.TabIndex = 19;
            // 
            // textEditRoomNo
            // 
            this.textEditRoomNo.Location = new System.Drawing.Point(133, 16);
            this.textEditRoomNo.Name = "textEditRoomNo";
            this.textEditRoomNo.Properties.ReadOnly = true;
            this.textEditRoomNo.Size = new System.Drawing.Size(211, 20);
            this.textEditRoomNo.TabIndex = 18;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.splitContainerControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(7, 7);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(908, 404);
            this.panelControl2.TabIndex = 20;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(908, 404);
            this.splitContainerControl1.SplitterPosition = 585;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // BookRefund
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.panelControl2);
            this.Name = "BookRefund";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(922, 418);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditReserveID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRoomNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.TextEdit textEditRoomNo;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.TextEdit textEditAmount;
        private DevExpress.XtraGrid.Columns.GridColumn RoomNumber;
        private DevExpress.XtraGrid.Columns.GridColumn TenantName;
        private DevExpress.XtraGrid.Columns.GridColumn Amount;
        private DevExpress.XtraGrid.Columns.GridColumn reserve_date;
        private DevExpress.XtraGrid.Columns.GridColumn colFlagReserveText;
        private DevExpress.XtraGrid.Columns.GridColumn colFlagReserve;
        private DevExpress.XtraEditors.TextEdit textEditReserveID;
    }
}
