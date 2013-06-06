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
    public partial class BasicInfoBuilding : uBase
    {
        public static DevExpress.XtraGrid.GridControl gridControl;
        private string button_event = "";

        public BasicInfoBuilding()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            gridControl = gridControl1;

            this.Load += new EventHandler(BasicInfoBuilding_Load);
            SaveClick += new EventHandler(BasicInfoBuilding_SaveClick);

        }

        void BasicInfoBuilding_SaveClick(object sender, EventArgs e)
        {
            bttSave_Click(sender, e);
        }

        public override void Refresh()
        {
            base.Refresh();
            initBuilding();
            initDropDownCompany();
            initDropDownBuilding();
            initDropDownFloorNo();

            changeRow();
            setLangThis();
        }

        void BasicInfoBuilding_Load(object sender, EventArgs e)
        {
            initBuilding();
            initDropDownCompany();
            initDropDownBuilding();
            initDropDownFloorNo();

            changeRow();
            setLangThis();
        }

        public void setLangThis()
        {

            this.groupControlBuildingList.Text  = getLanguage("_building_list");
            this.groupControlBuildingInfo.Text  = getLanguage("_building_info");

            this.labelControlCompanyName.Text   = getLanguage("_business_name");
            this.labelControlBuilding.Text      = getLanguage("_building_id");
            this.labelControlBuildingLabel.Text = getLanguage("_building_label");
            this.labelControlFloorNo.Text       = getLanguage("_building_amount_floor");
            this.labelControlRequired.Text      = getLanguage("_required");

            this.building_company_id_text.Caption  = getLanguage("_business_name");
            this.building_building_code.Caption = getLanguage("_building_id");
            this.building_building_label.Caption = getLanguage("_building_label");
            this.building_floor_no.Caption = getLanguage("_building_amount_floor");

            // Button
            this.bttAdd.Text = getLanguage("_add");
            this.bttEdit.Text = getLanguage("_edit");
            this.bttDelete.Text = getLanguage("_delete");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
        }

        #region Setup
        void initDataDefault()
        {
            lookUpEditxxx.EditValue = null;
            luEditBuilding.EditValue = null;
            txtBuildingLabel.EditValue = "";
            luEditFloorNo.EditValue = null;
        }
        void initBuilding()
        {
            DataTable BuildingTbl = BusinessLogicBridge.DataStore.BasicInfoBuilding_getByStatus(1);
            gridControl.DataSource = BuildingTbl;
        }
        void initDropDownCompany()
        {
            DataTable CompanyTable = BusinessLogicBridge.DataStore.BasicInfoBuilding_getCompany();
            lookUpEditxxx.Properties.DisplayMember = "company_name";
            lookUpEditxxx.Properties.ValueMember = "company_id";
            lookUpEditxxx.Properties.NullText = getLanguage("_select_company");
            lookUpEditxxx.Properties.DataSource = CompanyTable;
        }
        void initDropDownBuilding()
        {
            DataTable BuildingTbl = BusinessLogicBridge.DataStore.BasicInfoBuilding_get();
            luEditBuilding.Properties.DisplayMember = "building_code";
            luEditBuilding.Properties.ValueMember = "building_id";
            luEditBuilding.Properties.NullText = getLanguage("_select_building");
            luEditBuilding.Properties.DataSource = BuildingTbl;
        }
        void initDropDownBuildingAdd()
        {
            DataTable BuildingTbl = BusinessLogicBridge.DataStore.BasicInfoBuilding_getByStatus(0);
            luEditBuilding.Properties.DisplayMember = "building_code";
            luEditBuilding.Properties.ValueMember = "building_id";
            luEditBuilding.Properties.NullText = getLanguage("_select_building");
            luEditBuilding.Properties.DataSource = BuildingTbl;
        }
        void initDropDownFloorNo()
        {
            DataTable _floorNoTable = new DataTable();
            _floorNoTable.Columns.Add("floor_no", typeof(int));
            for (int i = 1; i <= 99; i++)
            {
                _floorNoTable.Rows.Add(i);
            }
            luEditFloorNo.Properties.DisplayMember = "floor_no";
            luEditFloorNo.Properties.ValueMember = "floor_no";
            luEditFloorNo.Properties.NullText = getLanguage("_select_floor");
            luEditFloorNo.Properties.DataSource = _floorNoTable;
        }
        #endregion

        #region Action Extra 
        private DataTable validateData()
        {
            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));
            
            if (luEditBuilding.EditValue == null)
            {
                label = labelControlBuilding.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    luEditBuilding.Focus();
                    focus = true;
                }
            }
            if (txtBuildingLabel.EditValue.ToString().Length < 1)
            {
                label = labelControlBuildingLabel.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    txtBuildingLabel.Focus();
                    focus = true;
                }
            }

            if (luEditFloorNo.EditValue == null)
            {
                label = labelControlFloorNo.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    luEditFloorNo.Focus();
                    focus = true;
                }
            }
            if(button_event != "" && _ValidateTable.Rows.Count < 1)
            {
                int building_id = int.Parse(luEditBuilding.EditValue.ToString());
                String building_label = txtBuildingLabel.EditValue.ToString();
                int floor_no = int.Parse(luEditFloorNo.EditValue.ToString());

                DataTable BuildingTable;
                switch(button_event)
                {
                    case "add":
                        int count = BusinessLogicBridge.DataStore.BasicInfoBuilding_getCountByBuildingLabel(building_label);
                        if (count > 0)
                        {
                            label = labelControlBuildingLabel.Text;
                            message = getLanguage("_msg_1027");
                            _ValidateTable.Rows.Add(label, message);
                        }
                        break;
                    case "edit":
                        DataTable FloorTable = BusinessLogicBridge.DataStore.BasicInfoBuildgin_getLastFloorByBuilding(building_id);
                        
                        if (FloorTable.Rows.Count > 0)
                        {
                            int count_tenant = BusinessLogicBridge.DataStore.BasicInfoBuilding_getCountTenantByBuilding(building_id);
                            if (count_tenant > 0)
                            {   
                                int floor_code = int.Parse(FloorTable.Rows[0]["floor_code"].ToString());
                                if (floor_code > floor_no)
                                {
                                    message = getLanguage("_msg_1029");
                                    _ValidateTable.Rows.Add(label, message);
                                }
                            }                            
                        }

                        BuildingTable = BusinessLogicBridge.DataStore.BasicInfoBuilding_getByBuildingLabel(building_label);
                        if (BuildingTable.Rows.Count > 0)
                        {
                            if (int.Parse(BuildingTable.Rows[0]["building_id"].ToString()) != building_id)
                            {
                                label = labelControlBuildingLabel.Text;
                                message = getLanguage("_msg_1027");
                                _ValidateTable.Rows.Add(label, message);
                                if (focus == false)
                                {
                                    txtBuildingLabel.Focus();
                                    focus = true;
                                }
                            }
                        }

                        break;
                }
            }
            return _ValidateTable;
        }
        void clearData()
        {
            initDropDownCompany();
            luEditBuilding.EditValue = null;
            txtBuildingLabel.EditValue = "";
            luEditFloorNo.EditValue = null;
        }
        void cearteFloor(int building_id, int floor_no)
        {
            for (int i = 1; i <= floor_no; i++)
            {
                DataTable _FloorTable = new DataTable();
                _FloorTable.Columns.Add("floor_label", typeof(string));
                _FloorTable.Columns.Add("building_id", typeof(string));
                _FloorTable.Columns.Add("floor_code", typeof(string));
                _FloorTable.Rows.Add("F"+i, building_id, i);
                BusinessLogicBridge.DataStore.BasicInfoBuilding_insertFloor(_FloorTable);
            }
        }
        void cearteFloor(int building_id, int begin_floor, int end_floor)
        {
            for (int i = begin_floor; i <= end_floor; i++)
            {
                DataTable _FloorTable = new DataTable();
                _FloorTable.Columns.Add("floor_label", typeof(string));
                _FloorTable.Columns.Add("building_id", typeof(string));
                _FloorTable.Columns.Add("floor_code", typeof(string));
                _FloorTable.Rows.Add("F" + i, building_id, i);
                BusinessLogicBridge.DataStore.BasicInfoBuilding_insertFloor(_FloorTable);
            }
        }
        
        void changeRow()
        {
            int[] rowIndex = gridView1.GetSelectedRows();
            if (rowIndex.Length != 0)
            {
                DataRow CurrentRow = gridView1.GetDataRow(rowIndex[0]);
                if (CurrentRow == null)
                {
                    CurrentRow = gridView1.GetDataRow(0);
                }

                luEditBuilding.EditValue = int.Parse(CurrentRow["building_id"].ToString());
                txtBuildingLabel.Text = CurrentRow["building_label"].ToString();
                luEditFloorNo.EditValue = int.Parse(CurrentRow["floor_count"].ToString());

                lookUpEditxxx.EditValue = lookUpEditxxx.Properties.GetKeyValueByDisplayText(CurrentRow["company_name"].ToString()); 

                bttEdit.Enabled = true;
                bttDelete.Enabled = true;
            }
            else
            {
                bttAdd.Enabled      = false;
                bttEdit.Enabled     = false;
                bttDelete.Enabled   = false;
                initDataDefault();
                initDropDownBuildingAdd();
                clearData();
                bttSave.Enabled = true;
                bttCancel.Enabled = true;
                lookUpEditxxx.Enabled = true;
                luEditBuilding.Enabled = true;
                txtBuildingLabel.Enabled = true;
                luEditFloorNo.Enabled = true;
                gridControl.Enabled = false;

                button_event = "add";

            }
        }
        #endregion

        #region Change Row
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            changeRow();
        }
        #endregion

        #region Button Event
        private void bttAdd_Click(object sender, EventArgs e)
        {
            initDropDownBuildingAdd();
            clearData();
            lookUpEditxxx.Enabled = true;
            luEditBuilding.Enabled = true;
            txtBuildingLabel.Enabled = true;
            luEditFloorNo.Enabled = true;
            gridControl.Enabled = false;

            button_event = "add";
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
        }
        private void bttEdit_Click(object sender, EventArgs e)
        {
            lookUpEditxxx.Enabled = true;
            luEditBuilding.Enabled = false;
            txtBuildingLabel.Enabled = true;
            luEditFloorNo.Enabled = true;
            gridControl.Enabled = false;
            
            button_event = "edit";
            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;
        }
        private void bttDelete_Click(object sender, EventArgs e)
        {
            try{

                int building_id = int.Parse(luEditBuilding.EditValue.ToString());

                // Check data in use
                int status_usage = BusinessLogicBridge.DataStore.buildingIdUsed(building_id);

                if (status_usage == 1)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                    return;
                }

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    DataTable Floor = BusinessLogicBridge.DataStore.BasicInfoBuilding_getFloorByBuilding(building_id);
                    int floor_id;
                    for (int i = 0; i < Floor.Rows.Count; i++)
                    {   
                        // Delete room By Floor of building ID
                        floor_id = int.Parse(Floor.Rows[i]["floor_id"].ToString());
                        BusinessLogicBridge.DataStore.BasicInfoBuilding_removeRoomByFloor(floor_id);
                    }
                    // Delete Floor of building ID
                    BusinessLogicBridge.DataStore.BasicInfoBuilding_removeFloorByBuilding(building_id);
                    DataTable _BuildingTable = new DataTable();
                        _BuildingTable.Columns.Add("building_id", typeof(int));
                        _BuildingTable.Columns.Add("building_label", typeof(string));
                        _BuildingTable.Columns.Add("building_status", typeof(int));
                        _BuildingTable.Columns.Add("company_id", typeof(int));

                        _BuildingTable.Rows.Add(building_id, "", 0, 0);                        

                    // Update Building empty
                        BusinessLogicBridge.DataStore.BasicInfoBuilding_update(_BuildingTable);
                        
                    DXWindowsApplication2.MainForm.setToggleBar();

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");

                    //Default Form
                    initBuilding();
                    changeRow();
                    lookUpEditxxx.Enabled = false;
                    luEditBuilding.Enabled = false;
                    txtBuildingLabel.Enabled = false;
                    luEditFloorNo.Enabled = false;

                    button_event = "";
                    bttCancel.Enabled = false;
                    bttSave.Enabled = false;
                    bttAdd.Enabled = true;
                    gridControl.Enabled = true;
                }
                else
                {
                    initDropDownBuilding();
                    changeRow();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region Button Action
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _ValidateTable = validateData();
                if (_ValidateTable.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                    {
                        message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                    }

                    utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                    TrySaveError = true;
                    return;
                }
                int building_id = int.Parse(luEditBuilding.EditValue.ToString());
                string building_label = txtBuildingLabel.EditValue.ToString();
                int floor_no = int.Parse(luEditFloorNo.EditValue.ToString());
                
                int company_id = int.Parse(lookUpEditxxx.EditValue.ToString());

                DataTable _BuildingTable = new DataTable();
                _BuildingTable.Columns.Add("building_id", typeof(int));
                _BuildingTable.Columns.Add("building_label", typeof(string));
                _BuildingTable.Columns.Add("building_status", typeof(int));
                _BuildingTable.Columns.Add("company_id", typeof(int));

                _BuildingTable.Rows.Add(building_id, building_label, 1, company_id);

                int[] rowIndex;
                switch (button_event)
                {
                    case "add":
                        BusinessLogicBridge.DataStore.BasicInfoBuilding_update(_BuildingTable);
                        this.cearteFloor(building_id, floor_no);
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        initBuilding();
                        rowIndex = gridView1.GetSelectedRows();
                        int select_row = 0;
                        if (gridView1.RowCount > 0)
                        {
                            select_row = gridView1.RowCount - 1;
                        }
                        gridView1.FocusedRowHandle = select_row;
                        gridView1.SelectRow(select_row);
                        gridView1.UnselectRow(rowIndex[0]);

                        DXWindowsApplication2.MainForm.setToggleBar();

                        break;
                    case "edit":
                        DataTable FloorTable = BusinessLogicBridge.DataStore.BasicInfoBuildgin_getLastFloorByBuilding(building_id);
                        if (FloorTable.Rows.Count > 0)
                        {
                            int floor_code = int.Parse(FloorTable.Rows[0]["floor_code"].ToString());
                            if (floor_code > floor_no)
                            {
                                int count_tenant = BusinessLogicBridge.DataStore.BasicInfoBuilding_getCountTenantByBuilding(building_id);
                                if (count_tenant < 1)
                                {
                                    FloorTable = BusinessLogicBridge.DataStore.BasicInfoBuilding_getFloorByBuilding(building_id, floor_no);
                                    int floor_id;
                                    for (int i = 0; i < FloorTable.Rows.Count; i++)
                                    {
                                        floor_id = int.Parse(FloorTable.Rows[i]["floor_id"].ToString());
                                        BusinessLogicBridge.DataStore.BasicInfoBuilding_removeRoomByFloor(floor_id);
                                        BusinessLogicBridge.DataStore.BasicInfoBuilding_removeFloor(floor_id);
                                    }
                                    //break;
                                }
                            }
                            else if (floor_code < floor_no)
                            {
                                int count_tenant = BusinessLogicBridge.DataStore.BasicInfoBuilding_getCountTenantByBuilding(building_id);
                                if (count_tenant>=0)
                                {
                                    cearteFloor(building_id, floor_code + 1, floor_no);
                                }
                                //break;
                            }
                            BusinessLogicBridge.DataStore.BasicInfoBuilding_update(_BuildingTable);
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                            rowIndex = gridView1.GetSelectedRows();
                            initBuilding();
                            gridView1.FocusedRowHandle = rowIndex[0];
                            gridView1.SelectRow(rowIndex[0]);
                        }
                        break;
                }
                //Default Form
                initDropDownBuilding();
                changeRow();
                lookUpEditxxx.Enabled = false;
                luEditBuilding.Enabled = false;
                txtBuildingLabel.Enabled = false;
                luEditFloorNo.Enabled = false;
                gridControl.Enabled = true;

                button_event = "";
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                bttAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == utilClass.showPopupConfirmBox(this,getLanguage("_msg_4007"), getLanguage("_softwarename")))
            {
                initDropDownCompany();
                initDropDownBuilding();
                changeRow();
                lookUpEditxxx.Enabled = false;
                luEditBuilding.Enabled = false;
                txtBuildingLabel.Enabled = false;
                luEditFloorNo.Enabled = false;

                button_event = "";
                bttAdd.Enabled = true;
                bttSave.Enabled = false;
                bttCancel.Enabled = false;
                gridControl.Enabled = true;
            }
        }
        #endregion
    }
}
