namespace DXWindowsApplication2.UserForms
{
    partial class BasicInfoBuilding
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
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControlBuildingList = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.building_company_id_text = new DevExpress.XtraGrid.Columns.GridColumn();
            this.building_building_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.building_building_label = new DevExpress.XtraGrid.Columns.GridColumn();
            this.building_floor_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.building_building_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.building_company_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControlBuildingInfo = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lookUpEditxxx = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControlRequired = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.labelControlBuilding = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlBuildingLabel = new DevExpress.XtraEditors.LabelControl();
            this.labelControlFloorNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.bttDelete = new DevExpress.XtraEditors.SimpleButton();
            this.bttEdit = new DevExpress.XtraEditors.SimpleButton();
            this.bttAdd = new DevExpress.XtraEditors.SimpleButton();
            this.luEditFloorNo = new DevExpress.XtraEditors.LookUpEdit();
            this.luEditBuilding = new DevExpress.XtraEditors.LookUpEdit();
            this.bttSave = new DevExpress.XtraEditors.SimpleButton();
            this.bttCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuildingLabel = new DevExpress.XtraEditors.TextEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBuildingList)).BeginInit();
            this.groupControlBuildingList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBuildingInfo)).BeginInit();
            this.groupControlBuildingInfo.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditxxx.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luEditFloorNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luEditBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildingLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(7, 7);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControlBuildingList);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1040, 474);
            this.splitContainerControl2.SplitterPosition = 500;
            this.splitContainerControl2.TabIndex = 11;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // groupControlBuildingList
            // 
            this.groupControlBuildingList.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlBuildingList.AppearanceCaption.Options.UseFont = true;
            this.groupControlBuildingList.Controls.Add(this.gridControl1);
            this.groupControlBuildingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlBuildingList.Location = new System.Drawing.Point(0, 0);
            this.groupControlBuildingList.Name = "groupControlBuildingList";
            this.groupControlBuildingList.Size = new System.Drawing.Size(500, 474);
            this.groupControlBuildingList.TabIndex = 0;
            this.groupControlBuildingList.Text = "รายการอาคาร";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.EnabledAutoRepeat = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.gridControl1.Location = new System.Drawing.Point(2, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(496, 450);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.building_company_id_text,
            this.building_building_code,
            this.building_building_label,
            this.building_floor_no,
            this.building_building_id,
            this.building_company_id});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.ShowCloseButton = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // building_company_id_text
            // 
            this.building_company_id_text.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.building_company_id_text.AppearanceHeader.Options.UseFont = true;
            this.building_company_id_text.Caption = "ชื่อกิจการ";
            this.building_company_id_text.FieldName = "company_name";
            this.building_company_id_text.Name = "building_company_id_text";
            this.building_company_id_text.OptionsColumn.AllowEdit = false;
            this.building_company_id_text.OptionsColumn.AllowFocus = false;
            this.building_company_id_text.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.building_company_id_text.OptionsColumn.AllowMove = false;
            this.building_company_id_text.Visible = true;
            this.building_company_id_text.VisibleIndex = 0;
            // 
            // building_building_code
            // 
            this.building_building_code.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.building_building_code.AppearanceHeader.Options.UseFont = true;
            this.building_building_code.Caption = "รหัสอาคาร";
            this.building_building_code.FieldName = "building_code";
            this.building_building_code.Name = "building_building_code";
            this.building_building_code.OptionsColumn.AllowEdit = false;
            this.building_building_code.OptionsColumn.AllowFocus = false;
            this.building_building_code.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.building_building_code.OptionsColumn.AllowMove = false;
            this.building_building_code.Visible = true;
            this.building_building_code.VisibleIndex = 1;
            // 
            // building_building_label
            // 
            this.building_building_label.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.building_building_label.AppearanceHeader.Options.UseFont = true;
            this.building_building_label.Caption = "ชื่ออาคาร";
            this.building_building_label.FieldName = "building_label";
            this.building_building_label.Name = "building_building_label";
            this.building_building_label.OptionsColumn.AllowEdit = false;
            this.building_building_label.OptionsColumn.AllowFocus = false;
            this.building_building_label.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.building_building_label.OptionsColumn.AllowMove = false;
            this.building_building_label.Visible = true;
            this.building_building_label.VisibleIndex = 2;
            // 
            // building_floor_no
            // 
            this.building_floor_no.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.building_floor_no.AppearanceHeader.Options.UseFont = true;
            this.building_floor_no.Caption = "จำนวนชั้น";
            this.building_floor_no.FieldName = "floor_count";
            this.building_floor_no.Name = "building_floor_no";
            this.building_floor_no.OptionsColumn.AllowEdit = false;
            this.building_floor_no.OptionsColumn.AllowFocus = false;
            this.building_floor_no.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.building_floor_no.OptionsColumn.AllowMove = false;
            this.building_floor_no.Visible = true;
            this.building_floor_no.VisibleIndex = 3;
            // 
            // building_building_id
            // 
            this.building_building_id.Caption = "building_id";
            this.building_building_id.FieldName = "building_id";
            this.building_building_id.Name = "building_building_id";
            // 
            // building_company_id
            // 
            this.building_company_id.Caption = "gridColumn1";
            this.building_company_id.FieldName = "company_id";
            this.building_company_id.Name = "building_company_id";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControlBuildingInfo);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(535, 474);
            this.panelControl3.TabIndex = 12;
            // 
            // groupControlBuildingInfo
            // 
            this.groupControlBuildingInfo.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlBuildingInfo.AppearanceCaption.Options.UseFont = true;
            this.groupControlBuildingInfo.Controls.Add(this.xtraScrollableControl1);
            this.groupControlBuildingInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlBuildingInfo.Location = new System.Drawing.Point(0, 0);
            this.groupControlBuildingInfo.Name = "groupControlBuildingInfo";
            this.groupControlBuildingInfo.Size = new System.Drawing.Size(535, 474);
            this.groupControlBuildingInfo.TabIndex = 0;
            this.groupControlBuildingInfo.Text = "ข้อมูลอาคาร";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.lookUpEditxxx);
            this.xtraScrollableControl1.Controls.Add(this.labelControlRequired);
            this.xtraScrollableControl1.Controls.Add(this.tableLayoutPanel1);
            this.xtraScrollableControl1.Controls.Add(this.bttDelete);
            this.xtraScrollableControl1.Controls.Add(this.bttEdit);
            this.xtraScrollableControl1.Controls.Add(this.bttAdd);
            this.xtraScrollableControl1.Controls.Add(this.luEditFloorNo);
            this.xtraScrollableControl1.Controls.Add(this.luEditBuilding);
            this.xtraScrollableControl1.Controls.Add(this.bttSave);
            this.xtraScrollableControl1.Controls.Add(this.bttCancel);
            this.xtraScrollableControl1.Controls.Add(this.txtBuildingLabel);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(531, 450);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // lookUpEditxxx
            // 
            this.lookUpEditxxx.Enabled = false;
            this.lookUpEditxxx.Location = new System.Drawing.Point(126, 11);
            this.lookUpEditxxx.Name = "lookUpEditxxx";
            this.lookUpEditxxx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditxxx.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_id", " ", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_name", " ")});
            this.lookUpEditxxx.Size = new System.Drawing.Size(399, 20);
            this.lookUpEditxxx.TabIndex = 360;
            // 
            // labelControlRequired
            // 
            this.labelControlRequired.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlRequired.Location = new System.Drawing.Point(9, 159);
            this.labelControlRequired.Name = "labelControlRequired";
            this.labelControlRequired.Size = new System.Drawing.Size(50, 13);
            this.labelControlRequired.TabIndex = 358;
            this.labelControlRequired.Text = "* โปรดระบุ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControlCompanyName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControlBuilding, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl12, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControlBuildingLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControlFloorNo, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl8, 2, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(114, 120);
            this.tableLayoutPanel1.TabIndex = 359;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Location = new System.Drawing.Point(3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 350;
            this.labelControl3.Text = "*";
            // 
            // labelControlCompanyName
            // 
            this.labelControlCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlCompanyName.Location = new System.Drawing.Point(48, 3);
            this.labelControlCompanyName.Name = "labelControlCompanyName";
            this.labelControlCompanyName.Size = new System.Drawing.Size(51, 13);
            this.labelControlCompanyName.TabIndex = 349;
            this.labelControlCompanyName.Text = "ชื่อกิจการ :";
            // 
            // labelControlBuilding
            // 
            this.labelControlBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlBuilding.Location = new System.Drawing.Point(44, 30);
            this.labelControlBuilding.Name = "labelControlBuilding";
            this.labelControlBuilding.Size = new System.Drawing.Size(55, 13);
            this.labelControlBuilding.TabIndex = 20;
            this.labelControlBuilding.Text = "รหัสอาคาร :";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl12.Location = new System.Drawing.Point(3, 82);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(6, 13);
            this.labelControl12.TabIndex = 346;
            this.labelControl12.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Location = new System.Drawing.Point(3, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 13);
            this.labelControl2.TabIndex = 347;
            this.labelControl2.Text = "*";
            // 
            // labelControlBuildingLabel
            // 
            this.labelControlBuildingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlBuildingLabel.Location = new System.Drawing.Point(49, 57);
            this.labelControlBuildingLabel.Name = "labelControlBuildingLabel";
            this.labelControlBuildingLabel.Size = new System.Drawing.Size(50, 13);
            this.labelControlBuildingLabel.TabIndex = 15;
            this.labelControlBuildingLabel.Text = "ชื่ออาคาร :";
            // 
            // labelControlFloorNo
            // 
            this.labelControlFloorNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlFloorNo.Location = new System.Drawing.Point(48, 82);
            this.labelControlFloorNo.Name = "labelControlFloorNo";
            this.labelControlFloorNo.Size = new System.Drawing.Size(51, 13);
            this.labelControlFloorNo.TabIndex = 21;
            this.labelControlFloorNo.Text = "จำนวนชั้น :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(105, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(4, 13);
            this.labelControl5.TabIndex = 349;
            this.labelControl5.Text = ":";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(105, 57);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(4, 13);
            this.labelControl6.TabIndex = 349;
            this.labelControl6.Text = ":";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(105, 30);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(4, 13);
            this.labelControl7.TabIndex = 349;
            this.labelControl7.Text = ":";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(105, 82);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(4, 13);
            this.labelControl8.TabIndex = 349;
            this.labelControl8.Text = ":";
            // 
            // bttDelete
            // 
            this.bttDelete.Image = global::DXWindowsApplication2.Properties.Resources.delete;
            this.bttDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttDelete.Location = new System.Drawing.Point(293, 159);
            this.bttDelete.Name = "bttDelete";
            this.bttDelete.Size = new System.Drawing.Size(70, 55);
            this.bttDelete.TabIndex = 6;
            this.bttDelete.Text = "ลบข้อมูล";
            this.bttDelete.Click += new System.EventHandler(this.bttDelete_Click);
            // 
            // bttEdit
            // 
            this.bttEdit.Image = global::DXWindowsApplication2.Properties.Resources.edit;
            this.bttEdit.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttEdit.Location = new System.Drawing.Point(212, 159);
            this.bttEdit.Name = "bttEdit";
            this.bttEdit.Size = new System.Drawing.Size(70, 55);
            this.bttEdit.TabIndex = 5;
            this.bttEdit.Text = "แก้ไขข้อมูล";
            this.bttEdit.Click += new System.EventHandler(this.bttEdit_Click);
            // 
            // bttAdd
            // 
            this.bttAdd.Image = global::DXWindowsApplication2.Properties.Resources.Add;
            this.bttAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttAdd.Location = new System.Drawing.Point(131, 159);
            this.bttAdd.Name = "bttAdd";
            this.bttAdd.Size = new System.Drawing.Size(70, 55);
            this.bttAdd.TabIndex = 4;
            this.bttAdd.Text = "เพิ่มข้อมูล";
            this.bttAdd.Click += new System.EventHandler(this.bttAdd_Click);
            // 
            // luEditFloorNo
            // 
            this.luEditFloorNo.Enabled = false;
            this.luEditFloorNo.Location = new System.Drawing.Point(126, 93);
            this.luEditFloorNo.Name = "luEditFloorNo";
            this.luEditFloorNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luEditFloorNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("floor_no", "floor_no")});
            this.luEditFloorNo.Size = new System.Drawing.Size(399, 20);
            this.luEditFloorNo.TabIndex = 3;
            // 
            // luEditBuilding
            // 
            this.luEditBuilding.Enabled = false;
            this.luEditBuilding.Location = new System.Drawing.Point(126, 41);
            this.luEditBuilding.Name = "luEditBuilding";
            this.luEditBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luEditBuilding.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_id", "id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("building_code", "building_code")});
            this.luEditBuilding.Size = new System.Drawing.Size(399, 20);
            this.luEditBuilding.TabIndex = 1;
            // 
            // bttSave
            // 
            this.bttSave.Enabled = false;
            this.bttSave.Image = global::DXWindowsApplication2.Properties.Resources.savedisk;
            this.bttSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttSave.Location = new System.Drawing.Point(374, 159);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(70, 55);
            this.bttSave.TabIndex = 7;
            this.bttSave.Text = "บันทึก";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // bttCancel
            // 
            this.bttCancel.Enabled = false;
            this.bttCancel.Image = global::DXWindowsApplication2.Properties.Resources.Close;
            this.bttCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.bttCancel.Location = new System.Drawing.Point(455, 159);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(70, 55);
            this.bttCancel.TabIndex = 8;
            this.bttCancel.Text = "ยกเลิก";
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            // 
            // txtBuildingLabel
            // 
            this.txtBuildingLabel.Enabled = false;
            this.txtBuildingLabel.Location = new System.Drawing.Point(126, 67);
            this.txtBuildingLabel.Name = "txtBuildingLabel";
            this.txtBuildingLabel.Properties.Mask.BeepOnError = true;
            this.txtBuildingLabel.Properties.Mask.EditMask = "([a-zA-Z0-9|ก-๙|\\\' \']){0,50}";
            this.txtBuildingLabel.Properties.Mask.IgnoreMaskBlank = false;
            this.txtBuildingLabel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtBuildingLabel.Properties.MaxLength = 50;
            this.txtBuildingLabel.Properties.ValidateOnEnterKey = true;
            this.txtBuildingLabel.Size = new System.Drawing.Size(399, 20);
            this.txtBuildingLabel.TabIndex = 2;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1040, 474);
            this.splitContainerControl1.SplitterPosition = 585;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
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
            this.panelControl2.Size = new System.Drawing.Size(1040, 474);
            this.panelControl2.TabIndex = 10;
            // 
            // BasicInfoBuilding
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.panelControl2);
            this.Name = "BasicInfoBuilding";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(1054, 488);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBuildingList)).EndInit();
            this.groupControlBuildingList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBuildingInfo)).EndInit();
            this.groupControlBuildingInfo.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditxxx.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luEditFloorNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luEditBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildingLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControlBuildingList;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControlBuildingInfo;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.LabelControl labelControlFloorNo;
        private DevExpress.XtraEditors.SimpleButton bttSave;
        private DevExpress.XtraEditors.SimpleButton bttCancel;
        private DevExpress.XtraEditors.LabelControl labelControlBuilding;
        private DevExpress.XtraEditors.LabelControl labelControlBuildingLabel;
        private DevExpress.XtraEditors.TextEdit txtBuildingLabel;
        private DevExpress.XtraGrid.Columns.GridColumn building_building_code;
        private DevExpress.XtraGrid.Columns.GridColumn building_building_label;
        private DevExpress.XtraGrid.Columns.GridColumn building_floor_no;
        private DevExpress.XtraEditors.LookUpEdit luEditFloorNo;
        private DevExpress.XtraEditors.LookUpEdit luEditBuilding;
        private DevExpress.XtraEditors.SimpleButton bttEdit;
        private DevExpress.XtraEditors.SimpleButton bttAdd;
        private DevExpress.XtraGrid.Columns.GridColumn building_building_id;
        private DevExpress.XtraEditors.SimpleButton bttDelete;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlCompanyName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn building_company_id_text;
        private DevExpress.XtraGrid.Columns.GridColumn building_company_id;
        private DevExpress.XtraEditors.LabelControl labelControlRequired;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditxxx;



    }
}
