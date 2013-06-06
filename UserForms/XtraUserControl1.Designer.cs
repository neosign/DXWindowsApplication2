namespace DXWindowsApplication2.UserForms
{
    partial class XtraUserControl1
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
            this.groupExpense = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlItem = new DevExpress.XtraGrid.GridControl();
            this.gridViewItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAmountPerUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceItemPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlGeneralCost = new DevExpress.XtraGrid.GridControl();
            this.gridViewGeneralCost = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControlBaht3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlRefund = new DevExpress.XtraEditors.LabelControl();
            this.textEditRefund = new DevExpress.XtraEditors.TextEdit();
            this.bttRemoveItem = new DevExpress.XtraEditors.SimpleButton();
            this.bttAddItem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupExpense)).BeginInit();
            this.groupExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGeneralCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGeneralCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRefund.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupExpense
            // 
            this.groupExpense.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupExpense.AppearanceCaption.Options.UseFont = true;
            this.groupExpense.Controls.Add(this.panelControl2);
            this.groupExpense.Controls.Add(this.labelControlBaht3);
            this.groupExpense.Controls.Add(this.labelControlRefund);
            this.groupExpense.Controls.Add(this.textEditRefund);
            this.groupExpense.Controls.Add(this.bttRemoveItem);
            this.groupExpense.Controls.Add(this.bttAddItem);
            this.groupExpense.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupExpense.Location = new System.Drawing.Point(0, 0);
            this.groupExpense.Name = "groupExpense";
            this.groupExpense.Size = new System.Drawing.Size(1695, 291);
            this.groupExpense.TabIndex = 368;
            this.groupExpense.Text = "รายการค่าใช้จ่าย";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControlItem);
            this.panelControl2.Controls.Add(this.gridControlGeneralCost);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 22);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1691, 267);
            this.panelControl2.TabIndex = 314;
            // 
            // gridControlItem
            // 
            this.gridControlItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControlItem.Location = new System.Drawing.Point(2, 112);
            this.gridControlItem.MainView = this.gridViewItem;
            this.gridControlItem.Name = "gridControlItem";
            this.gridControlItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlItem.Size = new System.Drawing.Size(1687, 123);
            this.gridControlItem.TabIndex = 283;
            this.gridControlItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItem,
            this.gridView2});
            // 
            // gridViewItem
            // 
            this.gridViewItem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridViewItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumNo,
            this.colInvoiceItemName,
            this.gridColumnAmount,
            this.gridColumnAmountPerUnit,
            this.colInvoiceItemPrice,
            this.gridColumnVat,
            this.gridColumnVating,
            this.colInvoiceItemId});
            this.gridViewItem.GridControl = this.gridControlItem;
            this.gridViewItem.Name = "gridViewItem";
            this.gridViewItem.OptionsView.ShowColumnHeaders = false;
            this.gridViewItem.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumNo
            // 
            this.gridColumNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumNo.AppearanceHeader.Options.UseFont = true;
            this.gridColumNo.Caption = "ลำดับ";
            this.gridColumNo.FieldName = "order";
            this.gridColumNo.Name = "gridColumNo";
            this.gridColumNo.OptionsColumn.AllowEdit = false;
            this.gridColumNo.OptionsColumn.AllowFocus = false;
            this.gridColumNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumNo.OptionsColumn.AllowMove = false;
            this.gridColumNo.OptionsColumn.ReadOnly = true;
            this.gridColumNo.Visible = true;
            this.gridColumNo.VisibleIndex = 0;
            this.gridColumNo.Width = 39;
            // 
            // colInvoiceItemName
            // 
            this.colInvoiceItemName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colInvoiceItemName.AppearanceHeader.Options.UseFont = true;
            this.colInvoiceItemName.Caption = "รายการ";
            this.colInvoiceItemName.FieldName = "item_name";
            this.colInvoiceItemName.Name = "colInvoiceItemName";
            this.colInvoiceItemName.OptionsColumn.AllowEdit = false;
            this.colInvoiceItemName.OptionsColumn.AllowFocus = false;
            this.colInvoiceItemName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceItemName.OptionsColumn.AllowMove = false;
            this.colInvoiceItemName.OptionsColumn.ReadOnly = true;
            this.colInvoiceItemName.Visible = true;
            this.colInvoiceItemName.VisibleIndex = 1;
            this.colInvoiceItemName.Width = 63;
            // 
            // gridColumnAmount
            // 
            this.gridColumnAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnAmount.AppearanceHeader.Options.UseFont = true;
            this.gridColumnAmount.Caption = "จำนวน";
            this.gridColumnAmount.FieldName = "item_amount";
            this.gridColumnAmount.Name = "gridColumnAmount";
            this.gridColumnAmount.OptionsColumn.AllowEdit = false;
            this.gridColumnAmount.OptionsColumn.AllowFocus = false;
            this.gridColumnAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnAmount.OptionsColumn.AllowMove = false;
            this.gridColumnAmount.OptionsColumn.ReadOnly = true;
            this.gridColumnAmount.Visible = true;
            this.gridColumnAmount.VisibleIndex = 2;
            this.gridColumnAmount.Width = 63;
            // 
            // gridColumnAmountPerUnit
            // 
            this.gridColumnAmountPerUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnAmountPerUnit.AppearanceHeader.Options.UseFont = true;
            this.gridColumnAmountPerUnit.Caption = "จำนวนเงินต่อหน่วย";
            this.gridColumnAmountPerUnit.FieldName = "item_priceperunit";
            this.gridColumnAmountPerUnit.Name = "gridColumnAmountPerUnit";
            this.gridColumnAmountPerUnit.OptionsColumn.AllowEdit = false;
            this.gridColumnAmountPerUnit.OptionsColumn.AllowFocus = false;
            this.gridColumnAmountPerUnit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnAmountPerUnit.OptionsColumn.AllowMove = false;
            this.gridColumnAmountPerUnit.OptionsColumn.ReadOnly = true;
            this.gridColumnAmountPerUnit.Visible = true;
            this.gridColumnAmountPerUnit.VisibleIndex = 3;
            this.gridColumnAmountPerUnit.Width = 134;
            // 
            // colInvoiceItemPrice
            // 
            this.colInvoiceItemPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colInvoiceItemPrice.AppearanceHeader.Options.UseFont = true;
            this.colInvoiceItemPrice.Caption = "จำนวนเงินรวม";
            this.colInvoiceItemPrice.FieldName = "item_sumprice";
            this.colInvoiceItemPrice.Name = "colInvoiceItemPrice";
            this.colInvoiceItemPrice.OptionsColumn.AllowEdit = false;
            this.colInvoiceItemPrice.OptionsColumn.AllowFocus = false;
            this.colInvoiceItemPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceItemPrice.OptionsColumn.AllowMove = false;
            this.colInvoiceItemPrice.OptionsColumn.ReadOnly = true;
            this.colInvoiceItemPrice.Visible = true;
            this.colInvoiceItemPrice.VisibleIndex = 4;
            this.colInvoiceItemPrice.Width = 134;
            // 
            // gridColumnVat
            // 
            this.gridColumnVat.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnVat.AppearanceHeader.Options.UseFont = true;
            this.gridColumnVat.Caption = "ภาษี";
            this.gridColumnVat.FieldName = "item_vatprice";
            this.gridColumnVat.Name = "gridColumnVat";
            this.gridColumnVat.OptionsColumn.AllowEdit = false;
            this.gridColumnVat.OptionsColumn.AllowFocus = false;
            this.gridColumnVat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnVat.OptionsColumn.AllowMove = false;
            this.gridColumnVat.OptionsColumn.ReadOnly = true;
            this.gridColumnVat.Visible = true;
            this.gridColumnVat.VisibleIndex = 5;
            this.gridColumnVat.Width = 63;
            // 
            // gridColumnVating
            // 
            this.gridColumnVating.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnVating.AppearanceHeader.Options.UseFont = true;
            this.gridColumnVating.Caption = "คิดภาษี";
            this.gridColumnVating.FieldName = "item_vat_bool";
            this.gridColumnVating.Name = "gridColumnVating";
            this.gridColumnVating.OptionsColumn.AllowEdit = false;
            this.gridColumnVating.OptionsColumn.AllowFocus = false;
            this.gridColumnVating.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnVating.OptionsColumn.AllowMove = false;
            this.gridColumnVating.OptionsColumn.ReadOnly = true;
            this.gridColumnVating.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumnVating.Visible = true;
            this.gridColumnVating.VisibleIndex = 6;
            this.gridColumnVating.Width = 67;
            // 
            // colInvoiceItemId
            // 
            this.colInvoiceItemId.Caption = "ItemId";
            this.colInvoiceItemId.FieldName = "item_id";
            this.colInvoiceItemId.Name = "colInvoiceItemId";
            this.colInvoiceItemId.OptionsColumn.AllowEdit = false;
            this.colInvoiceItemId.OptionsColumn.AllowFocus = false;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlItem;
            this.gridView2.Name = "gridView2";
            // 
            // gridControlGeneralCost
            // 
            this.gridControlGeneralCost.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControlGeneralCost.Location = new System.Drawing.Point(2, 2);
            this.gridControlGeneralCost.MainView = this.gridViewGeneralCost;
            this.gridControlGeneralCost.Name = "gridControlGeneralCost";
            this.gridControlGeneralCost.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridControlGeneralCost.Size = new System.Drawing.Size(1687, 110);
            this.gridControlGeneralCost.TabIndex = 284;
            this.gridControlGeneralCost.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGeneralCost,
            this.gridView1});
            // 
            // gridViewGeneralCost
            // 
            this.gridViewGeneralCost.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridViewGeneralCost.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridViewGeneralCost.GridControl = this.gridControlGeneralCost;
            this.gridViewGeneralCost.Name = "gridViewGeneralCost";
            this.gridViewGeneralCost.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = "ลำดับ";
            this.gridColumn1.FieldName = "order";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 39;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.Caption = "รายการ";
            this.gridColumn2.FieldName = "item_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 63;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.Caption = "จำนวน";
            this.gridColumn3.FieldName = "item_amount";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 63;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "จำนวนเงินต่อหน่วย";
            this.gridColumn4.FieldName = "item_priceperunit";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 134;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "จำนวนเงินรวม";
            this.gridColumn5.FieldName = "item_sumprice";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 134;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.Caption = "ภาษี";
            this.gridColumn6.FieldName = "item_vatprice";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 63;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.Caption = "คิดภาษี";
            this.gridColumn7.FieldName = "item_vat_bool";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 67;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "ItemId";
            this.gridColumn8.FieldName = "item_id";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlGeneralCost;
            this.gridView1.Name = "gridView1";
            // 
            // labelControlBaht3
            // 
            this.labelControlBaht3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlBaht3.Location = new System.Drawing.Point(1660, 172);
            this.labelControlBaht3.Name = "labelControlBaht3";
            this.labelControlBaht3.Size = new System.Drawing.Size(20, 13);
            this.labelControlBaht3.TabIndex = 313;
            this.labelControlBaht3.Text = "บาท";
            // 
            // labelControlRefund
            // 
            this.labelControlRefund.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlRefund.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlRefund.Location = new System.Drawing.Point(269, 172);
            this.labelControlRefund.Name = "labelControlRefund";
            this.labelControlRefund.Size = new System.Drawing.Size(86, 13);
            this.labelControlRefund.TabIndex = 312;
            this.labelControlRefund.Text = "จำนวนเงินคืน :";
            // 
            // textEditRefund
            // 
            this.textEditRefund.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditRefund.EditValue = "";
            this.textEditRefund.Enabled = false;
            this.textEditRefund.Location = new System.Drawing.Point(361, 165);
            this.textEditRefund.Name = "textEditRefund";
            this.textEditRefund.Size = new System.Drawing.Size(1293, 20);
            this.textEditRefund.TabIndex = 309;
            // 
            // bttRemoveItem
            // 
            this.bttRemoveItem.Location = new System.Drawing.Point(107, 160);
            this.bttRemoveItem.Name = "bttRemoveItem";
            this.bttRemoveItem.Size = new System.Drawing.Size(90, 25);
            this.bttRemoveItem.TabIndex = 311;
            this.bttRemoveItem.Text = "ลบ";
            // 
            // bttAddItem
            // 
            this.bttAddItem.Location = new System.Drawing.Point(11, 160);
            this.bttAddItem.Name = "bttAddItem";
            this.bttAddItem.Size = new System.Drawing.Size(90, 25);
            this.bttAddItem.TabIndex = 310;
            this.bttAddItem.Text = "เพิ่ม";
            // 
            // XtraUserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupExpense);
            this.Name = "XtraUserControl1";
            this.Size = new System.Drawing.Size(1695, 630);
            ((System.ComponentModel.ISupportInitialize)(this.groupExpense)).EndInit();
            this.groupExpense.ResumeLayout(false);
            this.groupExpense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGeneralCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGeneralCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRefund.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupExpense;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControlItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumNo;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAmountPerUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceItemPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVating;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceItemId;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControlGeneralCost;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGeneralCost;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControlBaht3;
        private DevExpress.XtraEditors.LabelControl labelControlRefund;
        private DevExpress.XtraEditors.TextEdit textEditRefund;
        private DevExpress.XtraEditors.SimpleButton bttRemoveItem;
        private DevExpress.XtraEditors.SimpleButton bttAddItem;





    }
}
