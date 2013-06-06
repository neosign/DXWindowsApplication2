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
    public partial class ProgramPermission : uBase
    {
        public static XtraMessageBoxForm AddPanel;
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public string event_value = "";
        public int group_id = 0;

        public ProgramPermission()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            gridControlGroupPermission.UseEmbeddedNavigator = false;
            gridViewGroup.OptionsBehavior.ReadOnly = true;
            gridViewGroup.OptionsBehavior.AllowAddRows  = DevExpress.Utils.DefaultBoolean.False;
            gridViewGroup.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewGroup.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            gridControlNick = gridControlGroupPermission;

            setLangThis();
            loadAllMenu();
            ReloadGroup();

            gridViewMenu.ShowingEditor += new CancelEventHandler(gridViewMenu_ShowingEditor);
            gridViewMenu2.ShowingEditor += new CancelEventHandler(gridViewMenu2_ShowingEditor);
            gridViewMenu3.ShowingEditor += new CancelEventHandler(gridViewMenu3_ShowingEditor);
            setDisable();
            SaveClick += new EventHandler(bttSave_Click);

            checkEditRegistration.CheckedChanged += new EventHandler(checkEditRegistration_CheckedChanged);

        }

        void checkEditRegistration_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditRegistration.Checked == true)
            {
                gridControlMenuGroup9.Enabled = true;
                loadRegistration(true);
            }
            else
            {
                gridControlMenuGroup9.Enabled = false;
                loadRegistration(false);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            setLangThis();
            ReloadGroup();
            loadAllMenu();
        }

        void gridViewMenu2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (object.ReferenceEquals(gridViewMenu2.FocusedColumn, gridColumnReadOrWrite2))
            {
                bool x = (bool)(gridViewMenu2.GetFocusedRowCellValue(gridColumnCheckBox2));
                if (x == false)
                    e.Cancel = true;
            }
        }

        void gridViewMenu3_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (object.ReferenceEquals(gridViewMenu3.FocusedColumn, gridColumnReadOrWrite3))
            {
                bool x = (bool)(gridViewMenu3.GetFocusedRowCellValue(gridColumnCheckBox3));
                if (x == false)
                    e.Cancel = true;
            }
        }

        void gridViewMenu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            bool x = (bool)(gridViewMenu.GetRowCellValue(e.RowHandle, gridColumnCheckBox));

            if (e.Column.Name == "gridColumnReadOrWrite")
            {
                if (x == false)
                {
                    e.Appearance.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

        void gridViewMenu_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (object.ReferenceEquals(gridViewMenu.FocusedColumn, gridColumnReadOrWrite))
            {
                bool x = (bool)(gridViewMenu.GetFocusedRowCellValue(gridColumnCheckBox));
                if (x == false)
                    e.Cancel = true;
            }

        }

        void setLangThis()
        {
                current_lang = MainForm.current_lang;
                // Set Lang Root
                checkEditRoomManagement.Text    = getLanguage("_menu_room_management");
                checkEditMeterManagement.Text   = getLanguage("_menu_meter_record");
                checkEditReportManagement.Text  = getLanguage("_menu_report");
                checkEditBasicSetting.Text      = getLanguage("_menu_general_setting");
                checkEditProgramSetting.Text    = getLanguage("_menu_program_setting");
                checkEditDatabaseSetting.Text   = getLanguage("_menu_database_setting");
                checkEditDeviceSetting.Text     = getLanguage("_menu_device_setting");
                checkEditRegistration.Text      = getLanguage("_menu_register");
                checkEditHelp.Text              = getLanguage("_menu_help");
                
                // Label
                labelGroupName.Text = getLanguageWithColon("_group_name");
                labelControlRequired.Text = getLanguage("_required");
                groupControlAuthorize.Text = getLanguage("_menu_program_setting_authorization");
                groupControlAuthorizeList.Text = getLanguage("_menu_user_group_list");
                
                // Grid
                gridColumnGroupName.Caption = getLanguage("_group_name");
                gridColumnAmountUser.Caption = getLanguage("_menu_number_of_users");

                gridColumnMenuNameBar.Caption = getLanguage("_menu_menu_name");
                gridColumnReadWriteBar.Caption= getLanguage("_menu_read_and_write");
                gridColumnReadOnlyBar.Caption = getLanguage("_menu_read_only");
                gridView4.ViewCaption = getLanguage("_menu_menu_list");

                // Button
                this.bttAdd.Text = getLanguage("_add");
                this.bttEdit.Text = getLanguage("_edit");
                this.bttDelete.Text = getLanguage("_delete");
                this.bttSave.Text = getLanguage("_save");
                this.bttCancel.Text = getLanguage("_cancel");
                
                


        }

        void loadAllMenu() {

            loadRoomManagement();
            loadMeterManagement();
            loadReportManagement();
            loadGeneralSetting();
            loadProgramSetting();
            loadDatabaseSetting();
            loadDeviceSetting();
            loadRegistration();
            loadHelp();

            //getFirstRow();
                
        }

        void LoadPermissionAdmin() {

            #region Permission Admin Root Checked
               // DataTable PermissionDataRoot = BusinessLogicBridge.DataStore.getMenuPermission(0, group_id);

                checkEditRoomManagement.Checked = true;
                checkEditMeterManagement.Checked = true;
                checkEditReportManagement.Checked = true;
                checkEditBasicSetting.Checked = true;
                checkEditProgramSetting.Checked = true;
                checkEditDatabaseSetting.Checked = true;
                checkEditDeviceSetting.Checked = true;
                checkEditRegistration.Checked = true;
                checkEditHelp.Checked = true;
            #endregion


                int parent_id = 0;

            // Add Root
                BusinessLogicBridge.DataStore.addMenuPermission(1, 0, 1,  1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(13, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(17, 0, 1, 1, 0); 
                BusinessLogicBridge.DataStore.addMenuPermission(24, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(32, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(37, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(40, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(45, 0, 1, 1, 0);
                BusinessLogicBridge.DataStore.addMenuPermission(49, 0, 1, 1, 0);
            
            
            // Loop Add Child

                DataTable gridAll = new DataTable();
                DataTable group1 = (DataTable)(gridControlMenuGroup1.DataSource);
                DataTable group2 = (DataTable)(gridControlMenuGroup2.DataSource);
                DataTable group3 = (DataTable)(gridControlMenuGroup3.DataSource);
                DataTable group4 = (DataTable)(gridControlMenuGroup4.DataSource);
                DataTable group5 = (DataTable)(gridControlMenuGroup5.DataSource);
                DataTable group6 = (DataTable)(gridControlMenuGroup6.DataSource);
                DataTable group7 = (DataTable)(gridControlMenuGroup7.DataSource);
                DataTable group8 = (DataTable)(gridControlMenuGroup8.DataSource);
                DataTable group9 = (DataTable)(gridControlMenuGroup9.DataSource);

                gridAll.Merge(group1);
                gridAll.Merge(group2);
                gridAll.Merge(group3);
                gridAll.Merge(group4);
                gridAll.Merge(group5);
                gridAll.Merge(group6);
                gridAll.Merge(group7);
                gridAll.Merge(group8);
                gridAll.Merge(group9);

                for (int i = 0; i < gridAll.Rows.Count; i++)
                {
                    parent_id = (int)(gridAll.Rows[i]["menu_parent"]);
                    int checkStatus = ((bool)gridAll.Rows[i]["permission_flag_check"] == true) ? 1 : 0;
                    int checkAccess = (int)(gridAll.Rows[i]["permission_flag_access"]);

                    BusinessLogicBridge.DataStore.addMenuPermission((int)(gridAll.Rows[i]["menu_id"]), parent_id, 1, checkStatus, checkAccess);

                }
                ReloadGroup();
                setDisable();


        }

        void loadRoomManagement() {
            loadRoomManagement(true);
        }

        void loadRoomManagement(bool check) {

            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(1);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName.FieldName = menuFieldsName;

            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu.GetDataRow(i);


                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (AllMenuItem.Rows[i]["menu_parent"].To<int>() == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }
                else {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString();
                }
            }

            gridControlMenuGroup1.DataSource = AllMenuItem;
        }

        void loadMeterManagement() {
            loadMeterManagement(true);
        }

        void loadMeterManagement(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(13);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            
            gridColumnMenuName2.FieldName = menuFieldsName;

            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu2.GetDataRow(i);
                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup2.DataSource = AllMenuItem;
        }

        void loadReportManagement() {
            loadReportManagement(true);
        }

        void loadReportManagement(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(17);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName3.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu3.GetDataRow(i);
                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup3.DataSource = AllMenuItem;
        }

        void loadGeneralSetting() {
            loadGeneralSetting(true);
        }

        void loadGeneralSetting(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(24);

            DataTable menu_permission = new DataTable();
            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName4.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu4.GetDataRow(i);

                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup4.DataSource = AllMenuItem;
        }

        void loadProgramSetting() {
            loadProgramSetting(true);
        }

        void loadProgramSetting(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(32);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName5.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu5.GetDataRow(i);

                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup5.DataSource = AllMenuItem;
        }

        void loadDatabaseSetting() {
            loadDatabaseSetting(true);
        }

        void loadDatabaseSetting(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(37);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName6.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu6.GetDataRow(i);
                
                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup6.DataSource = AllMenuItem;
        }

        void loadDeviceSetting() {
            loadDeviceSetting(true);
        }

        void loadDeviceSetting(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(40);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName7.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu7.GetDataRow(i);

                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup7.DataSource = AllMenuItem;
        }

         void loadRegistration() {
            loadRegistration(true);
        }

         void loadRegistration(bool check) {
             DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(49);

             AllMenuItem.Rows[1].Delete();

             AllMenuItem.AcceptChanges();

             DataTable menu_permission = new DataTable();

             string menuFieldsName = "menu_" + current_lang;
             gridColumnMenuName9.FieldName = menuFieldsName;
             AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
             AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

             for (int i = 0; i < AllMenuItem.Rows.Count; i++)
             {
                 DataRow CurrentRow = gridViewMenu9.GetDataRow(i);

                 menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                 if (menu_permission.Rows.Count > 0)
                 {
                     if (AllMenuItem.Rows[i]["menu_id"].To<int>() == 50)
                     {
                         AllMenuItem.Rows[i]["permission_flag_check"] = false;
                         AllMenuItem.Rows[i]["permission_flag_access"] = 0;
                     }
                     else
                     {
                         AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                         AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                     }
                 }
                 else
                 {
                     if (AllMenuItem.Rows[i]["menu_id"].To<int>() == 50)
                     {
                         AllMenuItem.Rows[i]["permission_flag_check"] = false;
                         AllMenuItem.Rows[i]["permission_flag_access"] = 0;
                     }
                     else
                     {
                         AllMenuItem.Rows[i]["permission_flag_check"] = check;
                         AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                     }
                 }

                 if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                 {
                     AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                 }

             }

             gridControlMenuGroup9.DataSource = AllMenuItem;
         }

        void loadHelp() {
            loadHelp(true);
        }

        void loadHelp(bool check)
        {
            DataTable AllMenuItem = BusinessLogicBridge.DataStore.getMenuByParent(45);

            DataTable menu_permission = new DataTable();

            string menuFieldsName = "menu_" + current_lang;
            gridColumnMenuName8.FieldName = menuFieldsName;
            AllMenuItem.Columns.Add("permission_flag_check", typeof(bool));
            AllMenuItem.Columns.Add("permission_flag_access", typeof(int));

            for (int i = 0; i < AllMenuItem.Rows.Count; i++)
            {
                DataRow CurrentRow = gridViewMenu8.GetDataRow(i);

                menu_permission = BusinessLogicBridge.DataStore.getMenuByID(AllMenuItem.Rows[i]["menu_id"].To<int>(), group_id);

                if (menu_permission.Rows.Count > 0)
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = menu_permission.Rows[0]["permission_flag_check"].To<bool>();
                    AllMenuItem.Rows[i]["permission_flag_access"] = menu_permission.Rows[0]["permission_flag_access"].To<int>();
                }
                else
                {
                    AllMenuItem.Rows[i]["permission_flag_check"] = check;
                    AllMenuItem.Rows[i]["permission_flag_access"] = 1;
                }

                if (Convert.ToInt16(AllMenuItem.Rows[i]["menu_parent"]) == 0)
                {
                    AllMenuItem.Rows[i][menuFieldsName] = AllMenuItem.Rows[i][menuFieldsName].ToString() + " ( Root )";
                }

            }

            gridControlMenuGroup8.DataSource = AllMenuItem;
        }

        void getFirstRow() {

            DataRow CurrentRow = gridViewGroup.GetDataRow(0);

            if (CurrentRow != null)
            {
                DataTable PermissionData1 = new DataTable();
                DataTable PermissionData2 = new DataTable();
                DataTable PermissionData3 = new DataTable();
                string menuFieldsName = "menu_" + current_lang;
                
                try
                {
                        group_id = Convert.ToInt32(CurrentRow["group_id"].ToString());
                        DataTable PermissionAdmin = new DataTable();
                        
                        // Group Name Assigned
                        textEditGroupName.EditValue = CurrentRow["group_name"];
                        textEditGroupID.EditValue = group_id;

                        if (textEditGroupName.EditValue.ToString() == "admin")
                        {
                            bttEdit.Enabled = false;
                            bttDelete.Enabled = false;
                        }
                        else
                        {
                            bttEdit.Enabled = true;
                            bttDelete.Enabled = true;
                        }

                        #region Permission Root Checked
                        DataTable PermissionDataRoot = BusinessLogicBridge.DataStore.getMenuPermission(0, group_id);
 
                        for (int i = 0; i < PermissionDataRoot.Rows.Count; i++)
                        {
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 1)
                            {
                                checkEditRoomManagement.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 13)
                            {
                                checkEditMeterManagement.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 17)
                            {
                                checkEditReportManagement.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 22)
                            {
                                checkEditBasicSetting.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 23)
                            {
                                checkEditProgramSetting.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 24)
                            {
                                checkEditDatabaseSetting.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 25)
                            {
                                checkEditDeviceSetting.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                            if ((int)(PermissionDataRoot.Rows[i]["permission_menu_id"]) == 26)
                            {
                                checkEditHelp.Checked = (bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]);
                            }
                        }

                        #endregion

                        if (group_id == 1)
                        {
                            // Load Admin Permistion defualt

                            PermissionAdmin = BusinessLogicBridge.DataStore.getMenuPermission(0, 1);

                            if (PermissionAdmin.Rows.Count == 0)
                            {
                                LoadPermissionAdmin();
                            }
                        }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            //DataTable accountData = BusinessLogicBridge.DataStore.getAllAcccount();
            
            DataTable PermissionData1 = new DataTable();
            DataTable PermissionData2 = new DataTable();
            DataTable PermissionData3 = new DataTable();
            DataTable PermissionData4 = new DataTable();
            DataTable PermissionData5 = new DataTable();
            DataTable PermissionData6 = new DataTable();
            DataTable PermissionData7 = new DataTable();
            DataTable PermissionData8 = new DataTable();
            DataTable PermissionData9 = new DataTable();

            

            string menuFieldsName = "menu_" + current_lang;
            string MenuName = "";

            int[] rowIndex = gridViewGroup.GetSelectedRows();
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewGroup.GetDataRow(rowIndex[0]);
                    group_id = Convert.ToInt32(CurrentRow["group_id"].ToString());                 

                    // Group Name Assigned
                    textEditGroupName.EditValue = CurrentRow["group_name"];
                    textEditGroupID.EditValue = group_id;

                    if (textEditGroupName.EditValue.ToString() == "admin")
                    {
                        bttEdit.Enabled = false;
                        bttDelete.Enabled = false;
                    }
                    else {
                        bttEdit.Enabled = true;
                        bttDelete.Enabled = true;
                    }

                    // Load Permission By Group ID 

                    #region Permission Root Checked
                    DataTable PermissionDataRoot = BusinessLogicBridge.DataStore.getMenuPermission(0, group_id);

                    for (int i = 0; i < PermissionDataRoot.Rows.Count; i++)
                    {
                        // MenuName = BusinessLogicBridge.DataStore.getMenuName((int)(PermissionData1.Rows[i]["permission_menu_id"]), current_lang);

                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 1)
                        {
                            checkEditRoomManagement.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 13)
                        {
                            checkEditMeterManagement.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 17)
                        {
                            checkEditReportManagement.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 24)
                        {
                            checkEditBasicSetting.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 32)
                        {
                            checkEditProgramSetting.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 37)
                        {
                            checkEditDatabaseSetting.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 40)
                        {
                            checkEditDeviceSetting.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 45)
                        {
                            checkEditHelp.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                        if ((int)((int)(PermissionDataRoot.Rows[i]["permission_menu_id"])) == 49)
                        {
                            checkEditRegistration.Checked = (bool)((bool)(PermissionDataRoot.Rows[i]["permission_flag_check"]));
                        }
                    }

                    //gridControlMenuGroup1.DataSource = PermissionData1;
                    #endregion
                    #region Permission Room
                        
                        PermissionData1 = BusinessLogicBridge.DataStore.getMenuPermission(1, group_id);
                        PermissionData1.Columns.Add(menuFieldsName, typeof(string));

                        for (int i = 0; i < PermissionData1.Rows.Count; i++)
                        {
                            PermissionData1.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData1.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                        }

                        gridControlMenuGroup1.DataSource = PermissionData1;

                        if (PermissionData1.Rows.Count <= 0 && (checkEditRoomManagement.Checked == false))
                        {
                            checkEditRoomManagement.Checked = false;
                            loadRoomManagement(false);
                        }
                        else
                        {
                            loadRoomManagement(true);
                        }
                        
                    #endregion                   
                    #region Permission Meter
                        PermissionData2 = BusinessLogicBridge.DataStore.getMenuPermission(13, group_id);

                        if (PermissionData2.Rows.Count > 0 && (checkEditMeterManagement.Checked==true))
                        {
                            PermissionData2.Columns.Add(menuFieldsName, typeof(string));

                            for (int i = 0; i < PermissionData2.Rows.Count; i++)
                            {

                                PermissionData2.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData2.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                            }
                            gridControlMenuGroup2.DataSource = PermissionData2;
                            loadMeterManagement(true);
                        }
                        else
                        {
                            checkEditMeterManagement.Checked = false;
                            loadMeterManagement(false);
                        }
                        
                    #endregion
                    #region Permission 3
                    PermissionData3 = BusinessLogicBridge.DataStore.getMenuPermission(17, group_id);

                    PermissionData3.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData3.Rows.Count; i++)
                    {

                        PermissionData3.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData3.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }

                    gridControlMenuGroup3.DataSource = PermissionData3;
                    if (PermissionData3.Rows.Count <= 0 && (checkEditReportManagement.Checked == false))
                    {
                        checkEditReportManagement.Checked = false;
                        loadReportManagement(false);
                    }
                    else {
                        loadReportManagement(true);
                    }
                    
                    
                    #endregion
                    #region Permission 4
                    PermissionData4 = BusinessLogicBridge.DataStore.getMenuPermission(24, group_id);
                    
                    PermissionData4.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData4.Rows.Count; i++)
                    {

                        PermissionData4.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData4.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }

                    gridControlMenuGroup4.DataSource = PermissionData4;
                    if (PermissionData4.Rows.Count <= 0 && (checkEditBasicSetting.Checked == false))
                    {
                        checkEditBasicSetting.Checked = false;
                        loadGeneralSetting(false);
                    }
                    else {
                        loadGeneralSetting(true);
                    }

                    
                    #endregion
                    #region Permission 5
                    PermissionData5 = BusinessLogicBridge.DataStore.getMenuPermission(32, group_id);
                    PermissionData5.Columns.Add(menuFieldsName, typeof(string));
                    for (int i = 0; i < PermissionData5.Rows.Count; i++)
                    {
                        PermissionData5.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData5.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }

                    gridControlMenuGroup5.DataSource = PermissionData5;
                    if (PermissionData5.Rows.Count <= 0 && (checkEditProgramSetting.Checked == false))
                    {
                        checkEditProgramSetting.Checked = false;
                        loadProgramSetting(false);
                    }
                    else {
                        loadProgramSetting(true);
                    }

                    
                    #endregion
                    #region Permission 6
                    PermissionData6 = BusinessLogicBridge.DataStore.getMenuPermission(37, group_id);
                    PermissionData6.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData6.Rows.Count; i++)
                    {
                        PermissionData6.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData6.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }

                    gridControlMenuGroup6.DataSource = PermissionData6;

                    if (PermissionData6.Rows.Count <= 0 && (checkEditDatabaseSetting.Checked == false))
                    {
                        checkEditDatabaseSetting.Checked = false;
                        loadDatabaseSetting(false);
                    }
                    else {
                        loadDatabaseSetting(true);
                    }

                    #endregion
                    #region Permission 7
                    PermissionData7 = BusinessLogicBridge.DataStore.getMenuPermission(40, group_id);
                    PermissionData7.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData7.Rows.Count; i++)
                    {

                        PermissionData7.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData7.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }

                    gridControlMenuGroup7.DataSource = PermissionData7;
                    if (PermissionData7.Rows.Count <= 0 && (checkEditDeviceSetting.Checked == false))
                    {
                        checkEditDeviceSetting.Checked = false;
                        loadDeviceSetting(false);
                    }
                    else {
                        loadDeviceSetting(true);
                    }

                    
                    #endregion
                    
                    #region Permission 8
                    PermissionData8 = BusinessLogicBridge.DataStore.getMenuPermission(45, group_id);
                    PermissionData8.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData8.Rows.Count; i++)
                    {
                        PermissionData8.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData8.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }
                    gridControlMenuGroup8.DataSource = PermissionData8;

                    if (PermissionData8.Rows.Count <= 0 && (checkEditHelp.Checked == false))
                    {
                        checkEditHelp.Checked = false;
                        loadHelp(false);
                    }
                    else {
                        loadHelp(true);
                    }                    
                    #endregion

                    #region Permission 9
                    PermissionData9 = BusinessLogicBridge.DataStore.getMenuPermission(49, group_id);
                    PermissionData9.Columns.Add(menuFieldsName, typeof(string));

                    for (int i = 0; i < PermissionData9.Rows.Count; i++)
                    {
                        PermissionData9.Rows[i][menuFieldsName] = BusinessLogicBridge.DataStore.getMenuName(PermissionData9.Rows[i]["permission_menu_id"].To<int>(), current_lang);
                    }
                    gridControlMenuGroup9.DataSource = PermissionData9;

                    if (PermissionData9.Rows.Count <= 0 && (checkEditRegistration.Checked == false))
                    {
                        checkEditRegistration.Checked = false;
                        loadRegistration(false);
                    }
                    else
                    {
                        loadRegistration(true);
                    }
                    #endregion

                }
            }
            catch{ 
                
            }
        }

        private DataTable validateData()
        {

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));
            if ((textEditGroupName.EditValue == null) || (textEditGroupName.EditValue.ToString() == ""))
            {
                label = labelGroupName.Text;
                message = getLanguage("_msg_1001");
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditGroupName.Focus();
                    focus = true;
                }
            }
 
            return _ValidateTable;
        }

        public void ReloadGroup(){            
            DataTable group_account         = BusinessLogicBridge.DataStore.get_group_account();

            if (group_account.Rows.Count == 0) {

                BusinessLogicBridge.DataStore.addGroup("admin");
                BusinessLogicBridge.DataStore.InsertUserAccount("admin", "111111", 1, 1, "Administrator");
                
            }

            gridControlGroupPermission.DataSource = group_account;
            getFirstRow();
            DXWindowsApplication2.MainForm.setToggleBar();
        }
       
        private bool isEmpty(string param)
        {
            if (param.Length < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btEditSaveGroup_Click(object sender, EventArgs e)
        {
            string notice = "โปรดระบุ : ";

           // bool groupname = isEmpty(textEditGroupName.Text);

            //if (!groupname)
           // {
               // XtraMessageBox.Show(notice + labelWindowsStartup.Text.ToString());
               // textEditGroupName.Focus();
          //  }
           // else
//{
                DialogResult dr = XtraMessageBox.Show("ยืนยันการแก้ไขข้อมูล", "", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    //BusinessLogicBridge.DataStore.updateGroup(textEditGroupName.Text.ToString(), Convert.ToInt16(textEditGroupId.Text));
                    XtraMessageBox.Show("Update Completed");
                    ReloadGroup();
                }
          //  }
        }

        private void bttSave_Click(object sender, EventArgs e)
        {

            String message = "";
            DataTable _ValidateTable = validateData();
            if (_ValidateTable.Rows.Count > 0)
            {
                for (int i = 0; i < _ValidateTable.Rows.Count; i++)
                {
                    message = message + _ValidateTable.Rows[i]["label"] + " " + _ValidateTable.Rows[i]["message"].ToString() + "\r\n";
                }
                utilClass.showPopupMessegeBox(this, message, getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }

                try
                {

                    List<int> listMenuChecked = new List<int>();

                    List<int> listMenuAll = new List<int>();

                    List<int> where_in = new List<int>();

                    int group_id = 0;
                    int parent_id = 0;

                    bool flagChecked = false;

                    bool Room = checkEditRoomManagement.Checked;
                    bool Meter = checkEditMeterManagement.Checked;
                    bool Report = checkEditReportManagement.Checked;
                    bool Basic = checkEditBasicSetting.Checked;
                    bool Program = checkEditProgramSetting.Checked;
                    bool Database = checkEditDatabaseSetting.Checked;
                    bool Device = checkEditDeviceSetting.Checked;
                    bool Registration = checkEditRegistration.Checked;
                    bool Help = checkEditHelp.Checked;

                    //flagChecked

                    if (Room == true) flagChecked = true;
                    if (Meter == true) flagChecked = true;
                    if (Report == true) flagChecked = true;
                    if (Basic == true) flagChecked = true;
                    if (Program == true) flagChecked = true;
                    if (Database == true) flagChecked = true;
                    if (Device == true) flagChecked = true;
                    if (Registration == true) flagChecked = true;
                    if (Help == true) flagChecked = true;

                    if (flagChecked == false)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1056"), getLanguage("_softwarename"));
                        TrySaveError = true;
                        return;
                    }

                    if (event_value == "Add")
                    {   

                        int DuplicateGroup = BusinessLogicBridge.DataStore.getGroupByName(textEditGroupName.EditValue.ToString());

                        if (DuplicateGroup > 0)
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1017"), getLanguage("_softwarename"));
                            textEditGroupName.Focus();
                            TrySaveError = true;
                            return;
                        }

                        int groupID = BusinessLogicBridge.DataStore.addGroup(textEditGroupName.EditValue.ToString());
                        
                        group_id = groupID;

                        textEditGroupID.EditValue = groupID;

                        try
                        {   
                            //Defualt

                            // Add Root

                            BusinessLogicBridge.DataStore.addMenuPermission(1, 0, groupID, (checkEditRoomManagement.Checked == true) ? 1 : 0 , 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(13, 0, groupID, (checkEditMeterManagement.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(17, 0, groupID, (checkEditReportManagement.Checked == true) ? 1 : 0, 0);                                BusinessLogicBridge.DataStore.addMenuPermission(24, 0, groupID, (checkEditBasicSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(32, 0, groupID, (checkEditProgramSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(37, 0, groupID, (checkEditDatabaseSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(40, 0, groupID, (checkEditDeviceSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(45, 0, groupID, (checkEditHelp.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(49, 0, groupID, (checkEditRegistration.Checked == true) ? 1 : 0, 0);

                            // Add child

                            DataTable gridAll = new DataTable();
                            DataTable group1 = (DataTable)(gridControlMenuGroup1.DataSource);
                            DataTable group2 = (DataTable)(gridControlMenuGroup2.DataSource);
                            DataTable group3 = (DataTable)(gridControlMenuGroup3.DataSource);
                            DataTable group4 = (DataTable)(gridControlMenuGroup4.DataSource);
                            DataTable group5 = (DataTable)(gridControlMenuGroup5.DataSource);
                            DataTable group6 = (DataTable)(gridControlMenuGroup6.DataSource);
                            DataTable group7 = (DataTable)(gridControlMenuGroup7.DataSource);
                            DataTable group8 = (DataTable)(gridControlMenuGroup8.DataSource);
                            DataTable group9 = (DataTable)(gridControlMenuGroup9.DataSource);

                            gridAll.Merge(group1);
                            gridAll.Merge(group2);
                            gridAll.Merge(group3);
                            gridAll.Merge(group4);
                            gridAll.Merge(group5);
                            gridAll.Merge(group6);
                            gridAll.Merge(group7);
                            gridAll.Merge(group8);
                            gridAll.Merge(group9);

                            for (int i = 0; i < gridAll.Rows.Count; i++)
                            {
                                parent_id = (int)(gridAll.Rows[i]["menu_parent"]);
                                int checkStatus = ((bool)gridAll.Rows[i]["permission_flag_check"] == true) ? 1 : 0;
                                int checkAccess = (int)(gridAll.Rows[i]["permission_flag_access"]);

                                BusinessLogicBridge.DataStore.addMenuPermission((int)(gridAll.Rows[i]["menu_id"]), parent_id, groupID, checkStatus, checkAccess);

                            }
                            setDisable();
                            ReloadGroup();

                            // Show Success
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else {
                        
                        //int groupID = BusinessLogicBridge.DataStore.addGroup(textEditGroupName.EditValue.ToString());
                        //int parent_id = 0;
                        int groupID = (int)(textEditGroupID.EditValue);

                        BusinessLogicBridge.DataStore.deletePermission(groupID);

                        try
                        {
                            //Defualt

                            // Add Root

                            BusinessLogicBridge.DataStore.addMenuPermission(1, 0, groupID, (checkEditRoomManagement.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(13, 0, groupID, (checkEditMeterManagement.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(17, 0, groupID, (checkEditReportManagement.Checked == true) ? 1 : 0, 0); 
                            BusinessLogicBridge.DataStore.addMenuPermission(24, 0, groupID, (checkEditBasicSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(32, 0, groupID, (checkEditProgramSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(37, 0, groupID, (checkEditDatabaseSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(40, 0, groupID, (checkEditDeviceSetting.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(45, 0, groupID, (checkEditHelp.Checked == true) ? 1 : 0, 0);
                            BusinessLogicBridge.DataStore.addMenuPermission(49, 0, groupID, (checkEditRegistration.Checked == true) ? 1 : 0, 0);

                            // Add child

                            DataTable gridAll = new DataTable();
                            DataTable group1 = (DataTable)(gridControlMenuGroup1.DataSource);
                            DataTable group2 = (DataTable)(gridControlMenuGroup2.DataSource);
                            DataTable group3 = (DataTable)(gridControlMenuGroup3.DataSource);
                            DataTable group4 = (DataTable)(gridControlMenuGroup4.DataSource);
                            DataTable group5 = (DataTable)(gridControlMenuGroup5.DataSource);
                            DataTable group6 = (DataTable)(gridControlMenuGroup6.DataSource);
                            DataTable group7 = (DataTable)(gridControlMenuGroup7.DataSource);
                            DataTable group8 = (DataTable)(gridControlMenuGroup8.DataSource);
                            DataTable group9 = (DataTable)(gridControlMenuGroup9.DataSource);

                            gridAll.Merge(group1);
                            gridAll.Merge(group2);
                            gridAll.Merge(group3);
                            gridAll.Merge(group4);
                            gridAll.Merge(group5);
                            gridAll.Merge(group6);
                            gridAll.Merge(group7);
                            gridAll.Merge(group8);
                            gridAll.Merge(group9);

                            for (int i = 0; i < gridAll.Rows.Count; i++)
                            {
                                parent_id = (int)(gridAll.Rows[i]["menu_parent"]);
                                int checkStatus = ((bool)gridAll.Rows[i]["permission_flag_check"] == true) ? 1 : 0;
                                int checkAccess = (int)(gridAll.Rows[i]["permission_flag_access"]);

                                BusinessLogicBridge.DataStore.addMenuPermission((int)(gridAll.Rows[i]["menu_id"]), parent_id, groupID, checkStatus, checkAccess);
                            }

                            setDisable();
                            ReloadGroup();

                            // Show Success
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    DXWindowsApplication2.MainForm.setToggleBar();
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {   
            int countGroup = ((DataTable) gridControlGroupPermission.DataSource).Rows.Count;

            if (countGroup>=5)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1058"), getLanguage("_softwarename"));
                return;
            }

            #region Permission Admin Root Checked
                checkEditRoomManagement.Checked = true;
                checkEditMeterManagement.Checked = true;
                checkEditReportManagement.Checked = true;
                checkEditBasicSetting.Checked = true;
                checkEditProgramSetting.Checked = true;
                checkEditDatabaseSetting.Checked = true;
                checkEditDeviceSetting.Checked = true;
                checkEditHelp.Checked = true;
            #endregion

            event_value = "Add";
            textEditGroupName.EditValue = "";            
            textEditGroupID.EditValue = "";

            loadRoomManagement();
            loadMeterManagement();
            loadReportManagement();
            loadGeneralSetting();
            loadProgramSetting();
            loadDatabaseSetting();
            loadDeviceSetting();
            loadHelp();

            setEnable();
            textEditGroupName.Focus();

        }

        private void setEnable() {

            gridControlGroupPermission.Enabled = false;

            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttAdd.Enabled = false;
            bttEdit.Enabled = false;
            bttDelete.Enabled = false;
            xtraScrollableControl1.Enabled = true;

            if (textEditGroupName.EditValue != null) {

                if (textEditGroupName.EditValue.ToString() == "admin")
                {
                    textEditGroupName.Enabled = false;
                    xtraScrollableControl1.Enabled = false;
                }
                else {
                    textEditGroupName.Enabled = true;
                    textEditGroupName.Focus();
                }
            }
        }
        
        private void setDisable()
        {
            bttSave.Enabled = false;
            bttCancel.Enabled = false;

            bttAdd.Enabled = true;

            if (gridControlGroupPermission.DataSource != null) {

                if (((DataTable)gridControlGroupPermission.DataSource).Rows.Count == 1)
                {
                    bttEdit.Enabled = false;
                    bttDelete.Enabled = false;

                }
                else {
                    bttEdit.Enabled = true;
                    bttDelete.Enabled = true;
                }

            }
            

            xtraScrollableControl1.Enabled = false;
            gridControlGroupPermission.Enabled = true;
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            event_value = "Update";
            setEnable();

        }

        private void checkEditRoomManagement_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditRoomManagement.Checked == true)
            {
                gridControlMenuGroup1.Enabled = true;
                loadRoomManagement(true);
            }
            else {
                gridControlMenuGroup1.Enabled = false;
                loadRoomManagement(false);
            }
        }

        private void checkEditMeterManagement_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditMeterManagement.Checked == true)
            {
                gridControlMenuGroup2.Enabled = true;
                loadMeterManagement(true);
            }
            else
            {
                gridControlMenuGroup2.Enabled = false;
                loadMeterManagement(false);
            }
        }

        private void checkEditReportManagement_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditReportManagement.Checked == true)
            {
                gridControlMenuGroup3.Enabled = true;
                loadReportManagement(true);
            }
            else
            {
                gridControlMenuGroup3.Enabled = false;
                loadReportManagement(false);
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                ReloadGroup();
                getFirstRow();
                setDisable();
            }
        }

        private void checkEditBasicSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditBasicSetting.Checked == true)
            {
                gridControlMenuGroup4.Enabled = true;
                loadGeneralSetting(true);
            }
            else
            {
                gridControlMenuGroup4.Enabled = false;
                loadGeneralSetting(false);
            }
        }

        private void checkEditProgramSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditProgramSetting.Checked == true)
            {
                gridControlMenuGroup5.Enabled = true;
                loadProgramSetting(true);
            }
            else
            {
                gridControlMenuGroup5.Enabled = false;
                loadProgramSetting(false);
            }
        }

        private void checkEditDatabaseSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditDatabaseSetting.Checked == true)
            {
                gridControlMenuGroup6.Enabled = true;
                loadDatabaseSetting(true);
            }
            else
            {
                gridControlMenuGroup6.Enabled = false;
                loadDatabaseSetting(false);
            }
        }

        private void checkEditDeviceSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditDeviceSetting.Checked == true)
            {
                gridControlMenuGroup7.Enabled = true;
                loadDeviceSetting(true);
            }
            else
            {
                gridControlMenuGroup7.Enabled = false;
                loadDeviceSetting(false);
            }
        }

        private void checkEditHelp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditHelp.Checked == true)
            {
                gridControlMenuGroup8.Enabled = true;
                loadHelp(true);
            }
            else
            {
                gridControlMenuGroup8.Enabled = false;
                loadHelp(false);
            }
        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            // Check Data In use ? 

             int groupUsed = BusinessLogicBridge.DataStore.GroupInUsed((int)(textEditGroupID.EditValue));

             if (groupUsed ==1)
             {
                 utilClass.showPopupMessegeBox(this, getLanguage("_msg_1005"), getLanguage("_softwarename"));
                 return;
             }else{
                 if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4001"), getLanguage("_softwarename")) == DialogResult.Yes)
                 {

                     // delete process 
                        
                     // Delete Permission
                     BusinessLogicBridge.DataStore.deletePermission((int)(textEditGroupID.EditValue));
                     
                     // Delete Group Account
                     BusinessLogicBridge.DataStore.DeleteGroupById((int)(textEditGroupID.EditValue));                     

                     // Alert messege
                     utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"),"info");

                     ReloadGroup();
                     //getFirstRow();
                     setDisable();
                 }
              
             }
        }


    }
}
