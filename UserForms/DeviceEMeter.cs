using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.OleDb;

namespace DXWindowsApplication2.UserForms
{
    public partial class DeviceEMeter : uBase
    {
        static DataTable SerialModelList;
        static DataTable DT_EMeter_model;
        string button_event = "";
        private Boolean _CheckRoom = false;
        private int room_check_count = 0;
        private Boolean _CheckRoom2 = false;
        private int room_check_count2 = 0;
        private readonly BackgroundWorker bw_TestConnection = new BackgroundWorker();
        private readonly BackgroundWorker bw_SetToADC = new BackgroundWorker();
        private readonly BackgroundWorker bw_InitCLoop = new BackgroundWorker();
        private readonly BackgroundWorker _EMeterInroomBW = new BackgroundWorker();
        private readonly BackgroundWorker _EMeterUtilityBW = new BackgroundWorker();

        public static DataTable E_meterCheckedBox;
        public static DataTable E_meterCheckedBoxUtility;

        public DeviceEMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(DeviceEMeter_Load);
            checkEditSelectAll.CheckedChanged += new EventHandler(checkEditSelectAll_CheckedChanged);
            checkEditSelectAll2.CheckedChanged += new EventHandler(checkEditSelectAll2_CheckedChanged);
            gridViewEMeter1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewEMeter1_FocusedRowChanged);
            gridViewEMeter2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewEMeter2_FocusedRowChanged);

            gridViewEMeter2.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewEMeter2_RowClick);
            gridViewEMeter1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewEMeter1_RowClick);

            repositoryItemButtonEditTest.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditTest_ButtonClick);
            repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
            lookUpEditMeterModel.EditValueChanged += new EventHandler(lookUpEditMeterModel_EditValueChanged);

            lookUpEditADC.EditValueChanged += new EventHandler(lookUpEditADC_EditValueChanged);
            lookUpEditPort.EditValueChanged += new EventHandler(lookUpEditPort_EditValueChanged);

            textEditCTRatio.Leave += new EventHandler(textEditCTRatio_Leave);

            mruEditSerial.AddingMRUItem += new DevExpress.XtraEditors.Controls.AddingMRUItemEventHandler(mruEditSerial_AddingMRUItem);
            mruEditListModel.SelectedValueChanged += new EventHandler(mruEditListModel_SelectedValueChanged);

            mruEditSerial.SelectedValueChanged += new EventHandler(mruEditSerial_SelectedValueChanged);

            // Cloop Event
            bttSetCLoop.Click += new EventHandler(bttSetCLoop_Click);

            bttInitial.Click += new EventHandler(bttInitial_Click);

            bw_TestConnection.DoWork += new DoWorkEventHandler(bw_TestConnection_DoWork);
            bw_TestConnection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_TestConnection_RunWorkerCompleted);
            bw_TestConnection.WorkerReportsProgress = false;

            bw_SetToADC.DoWork += new DoWorkEventHandler(bw_SetToADC_DoWork);
            bw_SetToADC.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_SetToADC_RunWorkerCompleted);
            bw_SetToADC.WorkerReportsProgress = false;

            bw_InitCLoop.DoWork += new DoWorkEventHandler(bw_InitCLoop_DoWork);
            bw_InitCLoop.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_InitCLoop_RunWorkerCompleted);
            bw_InitCLoop.WorkerReportsProgress = false;


            _EMeterInroomBW.DoWork += new DoWorkEventHandler(_EMeterInroomBW_DoWork);
            _EMeterInroomBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_EMeterInroomBW_RunWorkerCompleted);
            _EMeterInroomBW.WorkerReportsProgress = false;

            _EMeterUtilityBW.DoWork += new DoWorkEventHandler(_EMeterUtilityBW_DoWork);
            _EMeterUtilityBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_EMeterUtilityBW_RunWorkerCompleted);
            _EMeterUtilityBW.WorkerReportsProgress = false;

        }

        void textEditCTRatio_Leave(object sender, EventArgs e)
        {
            if (textEditCTRatio.EditValue.To<int>() < 1)
            {
                textEditCTRatio.EditValue = 1;
            }
        }

        void mruEditSerial_SelectedValueChanged(object sender, EventArgs e)
        {
            if (SerialModelList != null && SerialModelList.Rows.Count > 0)
            {
                DataRow[] rowItem = SerialModelList.Select("serial_no='" + mruEditSerial.EditValue.To<int>() + "'");
                if (rowItem.Length > 0)
                {
                    mruEditListModel.SelectedItem = rowItem[0]["meter_model"].ToString();
                }
            }
        }

        void _EMeterUtilityBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataRow currentRow = gridViewEMeter2.GetDataRow(gridViewEMeter2.FocusedRowHandle);

                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"].ToString()].TestADCConnection();

                if (ConnectionStatus == false)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    string MeterSerial = currentRow["meter_serial"].ToString();
                    string TypeMeter = currentRow["meter_models"].ToString();

                    int EMeterStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"].ToString()].TestEMeter(MeterSerial, TypeMeter);


                    if (EMeterStatus == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            currentRow["meter_status_text"] = "Fail";
                            currentRow["meter_status"] = false;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            currentRow["meter_status_text"] = "Pass";
                            currentRow["meter_status"] = true;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void _EMeterUtilityBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
        }

        void _EMeterInroomBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
        }

        void _EMeterInroomBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataRow currentRow = gridViewEMeter1.GetDataRow(gridViewEMeter1.FocusedRowHandle);

                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"].ToString()].TestADCConnection();

                if (ConnectionStatus == false)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    string MeterSerial = currentRow["meter_serial"].ToString();
                    string TypeMeter = currentRow["meter_models"].ToString();

                    int EMeterStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"].ToString()].TestEMeter(MeterSerial, TypeMeter);

                    if (EMeterStatus == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            currentRow["meter_status_text"] = "Fail";
                            currentRow["meter_status"] = false;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            currentRow["meter_status_text"] = "Pass";
                            currentRow["meter_status"] = true;
                        });
                    }
                    //
                    //update or not
                    //                
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
            _EMeterUtilityBW.RunWorkerAsync();
        }

        void repositoryItemButtonEditTest_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            DataRow currentRow = gridViewEMeter1.GetDataRow(gridViewEMeter1.FocusedRowHandle);

            if (currentRow["device_adc_id"].ToString() == "")
            {

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                return;
            }

            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
            _EMeterInroomBW.RunWorkerAsync();
        }

        void bw_InitCLoop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
        }

        void bw_InitCLoop_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

                if (ConnectionStatus == false)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    var objCLoop = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].InitEMeterCLoop(lookUpEditCLoopInitial.EditValue.To<int>());

                    DataRow[] rInRoom = E_meterCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");
                    DataRow[] rUtility = E_meterCheckedBoxUtility.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");

                    if (objCLoop.Status == "ok")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rInRoom)
                            {
                                r["meter_status_text"] = "Pass";
                                r["meter_status"] = true;
                            }
                            foreach (DataRow r in rUtility)
                            {
                                r["meter_status_text"] = "Pass";
                                r["meter_status"] = true;
                            }
                        });

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3024"), getLanguage("_softwarename"), "info");
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rInRoom)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }

                            foreach (DataRow r in rUtility)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }
                        });
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1076"), getLanguage("_softwarename"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void bttInitial_Click(object sender, EventArgs e)
        {
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                //
                return;
            }
            //
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4031"), getLanguage("_softwarename")) == DialogResult.Yes)
            {

                E_meterCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);

                try
                {
                    // Progress Bar.... Loading
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                    bw_InitCLoop.RunWorkerAsync();

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
            }
        }

        void bttSetCLoop_Click(object sender, EventArgs e)
        {
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                //
                return;
            }

            if ((lookUpEditCLoopInroom.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_cloop"), getLanguage("_softwarename"));
                //
                return;
            }
            //
            DataTable E_meterInRoomCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);
            DataTable E_meterInRoomCheckedBox2 = (DataTable)(gridControlE_Meter2.DataSource);
            int amountCLoop = 0;
            int amountCLoopMeter = 0;
            DataTable tableChecked = new DataTable();
            tableChecked.Columns.Add("meter_id", typeof(int));
            tableChecked.Columns.Add("device_adc_id", typeof(int));
            try
            {
                int counter = 0;
                for (int i = 0; i < E_meterInRoomCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterInRoomCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {
                        counter++;
                        if (E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            amountCLoopMeter++;
                        }
                        tableChecked.Rows.Add(E_meterInRoomCheckedBox.Rows[i]["meter_id"], E_meterInRoomCheckedBox.Rows[i]["device_adc_id"]);
                    }
                    else
                    {
                        if (E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            if (lookUpEditCLoopInroom.EditValue.To<int>() == E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>())
                            {
                                amountCLoopMeter++;
                            }
                        }
                    }
                }

                for (int i = 0; i < E_meterInRoomCheckedBox2.Rows.Count; i++)
                {
                    if ((bool)(E_meterInRoomCheckedBox2.Rows[i]["meter_check"]) == true)
                    {
                        counter++;

                        if (E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            amountCLoopMeter++;
                        }

                        tableChecked.Rows.Add(E_meterInRoomCheckedBox2.Rows[i]["meter_id"], E_meterInRoomCheckedBox2.Rows[i]["device_adc_id"]);

                    }
                    else
                    {
                        if (E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            if (lookUpEditCLoopInroom.EditValue.To<int>() == E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>())
                            {
                                amountCLoopMeter++;
                            }
                        }
                    }
                }

                if (counter > 0)
                {
                    if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4033"), getLanguage("_softwarename")) == DialogResult.Yes)
                    {

                        for (int j = 0; j < tableChecked.Rows.Count; j++)
                        {
                            if (lookUpEditCLoopInroom.EditValue.To<int>() == 0)
                            {
                                // update C-Loop => 0 Again on Database
                                BusinessLogicBridge.DataStore.updateE_MeterCLoopZeRo(tableChecked.Rows[j]["meter_id"].To<int>());
                                initGridE_Meter1();
                            }
                            else
                            {
                                // Check C-Loop more than 10 loop right?
                                amountCLoop = BusinessLogicBridge.DataStore.countCLoopByADC(tableChecked.Rows[j]["device_adc_id"].To<int>());
                                if (amountCLoop <= 10)
                                {
                                    // Check Old C-loop Summary Amount

                                    if (amountCLoopMeter <= 25)
                                    {
                                        BusinessLogicBridge.DataStore.updateE_MeterCLoop(tableChecked.Rows[j]["meter_id"].To<int>(), lookUpEditCLoopInroom.EditValue.To<int>());

                                        initGridE_Meter1();
                                    }
                                    else
                                    {
                                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1075"), getLanguage("_softwarename"));
                                        return;
                                    }

                                }
                                else
                                {
                                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1074"), getLanguage("_softwarename"));
                                    return;
                                }
                            }
                        }

                    }
                    else
                    {
                        initGridE_Meter1();
                    }

                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void lookUpEditPort_EditValueChanged(object sender, EventArgs e)
        {
            //if (lookUpEditPort.EditValue == null)
            //{
            //    lookUpEditADCPort.Enabled = false;
            //}
            //else
            //{
            //    lookUpEditADCPort.Enabled = true;
            //}
        }

        void lookUpEditADC_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditADC.EditValue == null)
            {
                lookUpEditADCList.Enabled = false;
            }
            else
            {
                lookUpEditADCList.Enabled = true;
            }
        }

        void lookUpEditMeterModel_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditMeterModel.EditValue == null)
            {
                mruEditListModel.Enabled = false;
            }
            else
            {
                mruEditListModel.Enabled = true;
            }
        }

        void bw_SetToADC_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlE_Meter1.DataSource = E_meterCheckedBox;
        }

        void bw_TestConnection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            gridControlE_Meter1.DataSource = E_meterCheckedBox;
            gridControlE_Meter2.DataSource = E_meterCheckedBoxUtility;
        }

        void bw_TestConnection_DoWork(object sender, DoWorkEventArgs e)
        {

            bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

            if (ConnectionStatus == false)
            {

                DataRow[] rInRoom = E_meterCheckedBox.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");

                this.Invoke((MethodInvoker)delegate
                {
                    foreach (DataRow r in rInRoom)
                    {
                        r["meter_status_text"] = "Fail";
                        r["meter_status"] = false;
                    }
                    gridControlE_Meter1.DataSource = E_meterCheckedBox;

                });

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                return;
            }
            else
            {
                DataRow[] rInRoom = E_meterCheckedBox.Select("grid_meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>''", "room_id");
                DataRow[] rUtility = E_meterCheckedBoxUtility.Select("meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>''", "meter_id");

                if (rInRoom.Length > 0)
                {
                    var EMeterConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjEMeterConfig(rInRoom);

                    foreach (DataRow r in rInRoom)
                    {

                        if (Convert.ToBoolean(r["grid_meter_check"]) == true)
                        {
                            string SerialMeter = r["meter_serial"].ToString();
                            string TypeMeter = r["meter_models"].ToString();

                            int EMETER_Status = DXWindowsApplication2
                                .MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()]
                                .TestEMeter(SerialMeter, TypeMeter);

                            if (EMETER_Status == 0)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    r["meter_status_text"] = "Fail";
                                    r["meter_status"] = false;

                                });
                            }
                            else
                            {
                                // Success
                                this.Invoke((MethodInvoker)delegate
                                {
                                    r["meter_status_text"] = "Pass";
                                    r["meter_status"] = true;
                                });
                            }
                        }
                    }
                }
                //

                var EMeterConfigInfoUtility = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjEMeterConfig(rUtility);
                foreach (DataRow r in rUtility)
                {
                    if (Convert.ToBoolean(r["meter_check"]) == true)
                    {
                        string SerialMeter = r["meter_serial"].ToString();
                        string TypeMeter = r["meter_models"].ToString();
                        //
                        int EMETER_StatusUtility = DXWindowsApplication2.MainForm
                            .dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()]
                            .TestEMeter(SerialMeter, TypeMeter);
                        if (EMETER_StatusUtility == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            });
                        }
                        else
                        {
                            // Success
                            this.Invoke((MethodInvoker)delegate
                            {
                                r["meter_status_text"] = "Pass";
                                r["meter_status"] = true;
                            });
                        }
                    }
                }

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3025"), getLanguage("_softwarename"), "info");
            }
        }

        void bw_SetToADC_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool ConnectionStatus = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].TestADCConnection();

                if (ConnectionStatus == false)
                {
                    DataRow[] rInRoom = E_meterCheckedBox.Select("grid_meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");

                    this.Invoke((MethodInvoker)delegate
                    {
                        foreach (DataRow r in rInRoom)
                        {
                            r["meter_status_text"] = "Fail";
                            r["meter_status"] = false;
                        }
                        gridControlE_Meter1.DataSource = E_meterCheckedBox;

                    });

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1041"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    int EMETER_Status = 0;
                    int EMETER_StatusUtility = 0;

                    E_meterCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);
                    E_meterCheckedBoxUtility = (DataTable)(gridControlE_Meter2.DataSource);


                    DataRow[] rInRoom = E_meterCheckedBox.Select("grid_meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");
                    DataRow[] rUtility = E_meterCheckedBoxUtility.Select("device_adc_name='" + lookUpEditADCTop.Text + "' and meter_serial <>'' ");

                    if (rInRoom.Length > 0)
                    {
                        var EMeterConfigInfo = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjEMeterConfig(rInRoom);

                        EMETER_Status = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].SetEMeterConfig(EMeterConfigInfo);

                        if (EMETER_Status == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                foreach (DataRow r in rInRoom)
                                {
                                    r["meter_status_text"] = "Fail";
                                    r["meter_status"] = false;
                                }
                            });
                        }
                        else
                        {
                            // Success
                            this.Invoke((MethodInvoker)delegate
                            {
                                foreach (DataRow r in rInRoom)
                                {
                                    r["meter_status_text"] = "Fail";
                                    r["meter_status"] = false;
                                }
                            });
                        }
                    }
                    //

                    var EMeterConfigInfoUtility = DXWindowsApplication2.MainForm.ADCHelper.MappingToObjEMeterConfig(rUtility);

                    EMETER_StatusUtility = DXWindowsApplication2.MainForm.dictADC["adc_id_" + lookUpEditADCTop.EditValue.To<int>()].SetEMeterUtilityConfig(EMeterConfigInfoUtility);

                    if (EMETER_StatusUtility == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rUtility)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }
                        });
                    }
                    else
                    {
                        // Success
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow r in rUtility)
                            {
                                r["meter_status_text"] = "Fail";
                                r["meter_status"] = false;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void mruEditListModel_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable RatingLookup = (DataTable)(lookUpEditRating.Properties.DataSource);

            if (mruEditListModel != null && mruEditListModel.SelectedItem != null)
            {
                switch (mruEditListModel.SelectedItem.ToString())
                {
                    case "SX1-A35E":
                        lookUpEditRating.EditValue = 2; // 5(45)
                        break;
                    case "SX1-A85E":
                        lookUpEditRating.EditValue = 2; // 5(45)
                        break;
                    case "SX2-A35E":
                        lookUpEditRating.EditValue = 3; // 5(100)
                        break;
                    case "MX2-C01E":
                        lookUpEditRating.EditValue = 4; //10(100)
                        break;
                    case "MX2-C41E":
                        lookUpEditRating.EditValue = 1; // 5(10)
                        break;
                    case "MX2-A01E":
                        lookUpEditRating.EditValue = 4; // 10(100)
                        break;

                    default:
                        lookUpEditRating.EditValue = 4; // 10(100)
                        break;

                }
            }
        }

        void mruEditSerial_AddingMRUItem(object sender, DevExpress.XtraEditors.Controls.AddingMRUItemEventArgs e)
        {
            if (mruEditSerial.Text != "")
            {
                if (mruEditSerial.Text == "None")
                {
                    mruEditSerial.Properties.Items.Clear();
                }
            }
        }

        void gridViewEMeter1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewEMeter1.GetSelectedRows();
            textEditActionFrom.EditValue = "GRIDVIEW1";
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewEMeter1.GetDataRow(rowIndex[0]);

                    int meter_id = 0;
                    if (CurrentRow["meter_id"].ToString() != "")
                        meter_id = Convert.ToInt32(CurrentRow["meter_id"].ToString());
                    //
                    lookUpEditBuilding.EditValue = CurrentRow["building_id"];
                    textEditMeterName.EditValue = CurrentRow["meter_label"];
                    textEditMeterID.EditValue = meter_id;


                    if (CurrentRow["meter_serial"].ToString() == "")
                    {
                        mruEditSerial.EditValue = "";
                    }
                    else
                    {
                        mruEditSerial.EditValue = CurrentRow["meter_serial"].ToString();
                    }

                    if (CurrentRow["meter_models"].ToString() == "")
                    {
                        mruEditListModel.SelectedIndex = 0;
                    }
                    else
                    {
                        mruEditListModel.SelectedItem = CurrentRow["meter_models"];
                    }

                    if (CurrentRow["meter_models"].ToString().IndexOf("MX") != -1)
                    {
                        lookUpEditBuilding.Enabled = false;
                        textEditMeterName.Enabled = false;
                    }
                    else
                    {
                        lookUpEditBuilding.Enabled = true;
                        textEditMeterName.Enabled = true;
                    }

                    lookUpEditADCList.EditValue = CurrentRow["device_adc_id"];

                    if (CurrentRow["device_adc_id"].ToString() != "" && CurrentRow["device_adc_id"].ToString() != "0")
                    {
                        DataTable ADCInfo = BusinessLogicBridge.DataStore.selectADC(Convert.ToInt32(CurrentRow["device_adc_id"]));

                        if (ADCInfo.Rows.Count > 0)
                        {
                            if (ADCInfo.Rows[0]["device_adc_port_checked"].ToString() == "True")
                            {
                                lookUpEditADCPort.Enabled = true;
                            }
                        }
                    }

                    if (CurrentRow["meter_port"].ToString() == "Port1")
                    {
                        lookUpEditADCPort.EditValue = 1;
                    }
                    else if (CurrentRow["meter_port"].ToString() == "Port2")
                    {
                        lookUpEditADCPort.EditValue = 2;
                    }
                    else
                        lookUpEditADCPort.EditValue = null;


                    textEditCTRatio.EditValue = (CurrentRow["meter_ct_ratio"].ToString() == "") ? 1 : Convert.ToInt32(CurrentRow["meter_ct_ratio"]);

                    switch (CurrentRow["meter_rating"].ToString())
                    {
                        case "5(10)":
                            lookUpEditRating.EditValue = 1;
                            break;
                        case "5(45)":
                            lookUpEditRating.EditValue = 2;
                            break;
                        case "5(100)":
                            lookUpEditRating.EditValue = 3;
                            break;
                        case "10(100)":
                            lookUpEditRating.EditValue = 4;
                            break;
                        default:
                            lookUpEditRating.EditValue = null;
                            break;
                    }

                    if (CurrentRow["meter_cloop_no"].To<int>() == 0)
                    {
                        lookUpEditCLoop.EditValue = 0;
                    }
                    else
                    {
                        lookUpEditCLoop.EditValue = CurrentRow["meter_cloop_no"];
                    }

                    if (lookUpEditBuilding.Enabled == true || textEditMeterName.Enabled == true)
                    {
                        lookUpEditBuilding.Enabled = false;
                        textEditMeterName.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void gridViewEMeter1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rowIndex = gridViewEMeter1.GetSelectedRows();
            textEditActionFrom.EditValue = "GRIDVIEW1";
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewEMeter1.GetDataRow(rowIndex[0]);
                    //
                    int meter_id = 0;
                    if (CurrentRow["meter_id"].ToString() != "")
                        meter_id = Convert.ToInt32(CurrentRow["meter_id"].ToString());
                    //
                    lookUpEditBuilding.EditValue = CurrentRow["building_id"];
                    textEditMeterName.EditValue = CurrentRow["meter_label"];
                    textEditMeterID.EditValue = meter_id;


                    if (CurrentRow["meter_serial"].ToString() == "")
                    {
                        mruEditSerial.EditValue = "";
                    }
                    else
                    {
                        mruEditSerial.EditValue = CurrentRow["meter_serial"].ToString();
                    }

                    if (CurrentRow["meter_models"].ToString() == "")
                    {
                        mruEditListModel.SelectedIndex = -1;
                    }
                    else
                    {
                        mruEditListModel.SelectedItem = CurrentRow["meter_models"];                        
                    }

                    if (CurrentRow["meter_models"].ToString().IndexOf("MX") != -1)
                    {
                        lookUpEditBuilding.Enabled = false;
                        textEditMeterName.Enabled = false;
                    }
                    else
                    {
                        lookUpEditBuilding.Enabled = true;
                        textEditMeterName.Enabled = true;
                    }

                    lookUpEditADCList.EditValue = CurrentRow["device_adc_id"];

                    if (CurrentRow["device_adc_id"].ToString() != "" && CurrentRow["device_adc_id"].ToString() != "0")
                    {
                        DataTable ADCInfo = BusinessLogicBridge.DataStore.selectADC(Convert.ToInt32(CurrentRow["device_adc_id"]));

                        if (ADCInfo.Rows.Count > 0)
                        {
                            if (ADCInfo.Rows[0]["device_adc_port_checked"].ToString() == "True")
                            {
                                lookUpEditADCPort.Enabled = true;
                            }
                        }
                    }

                    if (CurrentRow["meter_port"].ToString() == "Port1")
                    {
                        lookUpEditADCPort.EditValue = 1;
                    }
                    else if (CurrentRow["meter_port"].ToString() == "Port2")
                    {
                        lookUpEditADCPort.EditValue = 2;
                    }
                    else
                        lookUpEditADCPort.EditValue = null;


                    textEditCTRatio.EditValue = (CurrentRow["meter_ct_ratio"].ToString() == "") ? 1 : Convert.ToInt32(CurrentRow["meter_ct_ratio"]);

                    switch (CurrentRow["meter_rating"].ToString())
                    {
                        case "5(10)":
                            lookUpEditRating.EditValue = 1;
                            break;
                        case "5(45)":
                            lookUpEditRating.EditValue = 2;
                            break;
                        case "5(100)":
                            lookUpEditRating.EditValue = 3;
                            break;
                        case "10(100)":
                            lookUpEditRating.EditValue = 4;
                            break;
                        default:
                            lookUpEditRating.EditValue = null;
                            break;
                    }

                    if (CurrentRow["meter_cloop_no"].To<int>() == 0)
                    {
                        lookUpEditCLoop.EditValue = 0;
                    }
                    else
                    {
                        lookUpEditCLoop.EditValue = CurrentRow["meter_cloop_no"];
                    }

                    if (lookUpEditBuilding.Enabled == true || textEditMeterName.Enabled == true)
                    {
                        lookUpEditBuilding.Enabled = false;
                        textEditMeterName.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void gridViewEMeter2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewEMeter2.GetSelectedRows();

            if (rowIndex.Length == 0)
                return;

            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewEMeter2.GetDataRow(rowIndex[0]);

                    if (CurrentRow == null) return;

                    int meter_id = Convert.ToInt32(CurrentRow["meter_id"].ToString());

                    textEditMeterID.EditValue = meter_id;
                    lookUpEditBuilding.EditValue = CurrentRow["building_id"].ToString();
                    textEditMeterName.EditValue = CurrentRow["meter_label"];
                    textEditActionFrom.EditValue = "GRIDVIEW2";

                    mruEditSerial.EditValue = CurrentRow["meter_serial"].ToString();
                    mruEditListModel.EditValue = CurrentRow["meter_models"].ToString();

                    lookUpEditADCList.EditValue = CurrentRow["device_adc_id"];

                    if (CurrentRow["meter_port"].ToString() == "Port1")
                    {
                        lookUpEditADCPort.EditValue = 1;
                    }
                    else if (CurrentRow["meter_port"].ToString() == "Port2")
                    {
                        lookUpEditADCPort.EditValue = 2;
                    }
                    else
                        lookUpEditADCPort.EditValue = null;
                    //

                    textEditCTRatio.EditValue = (CurrentRow["meter_ct_ratio"].ToString() == "") ? 1 : Convert.ToInt32(CurrentRow["meter_ct_ratio"]);

                    switch (CurrentRow["meter_rating"].ToString())
                    {
                        case "5(10)":
                            lookUpEditRating.EditValue = 1;
                            break;
                        case "5(45)":
                            lookUpEditRating.EditValue = 2;
                            break;
                        case "5(100)":
                            lookUpEditRating.EditValue = 3;
                            break;
                        case "10(100)":
                            lookUpEditRating.EditValue = 4;
                            break;
                        default:
                            lookUpEditRating.EditValue = null;
                            break;
                    }

                    if (CurrentRow["meter_cloop_no"].To<int>() == 0)
                    {
                        lookUpEditCLoop.EditValue = 0;
                    }
                    else
                    {
                        lookUpEditCLoop.EditValue = CurrentRow["meter_cloop_no"];
                    }

                    if (lookUpEditBuilding.Enabled == false || textEditMeterName.Enabled == false)
                    {
                        lookUpEditBuilding.Enabled = true;
                        textEditMeterName.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void gridViewEMeter2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rowIndex = gridViewEMeter2.GetSelectedRows();
            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewEMeter2.GetDataRow(rowIndex[0]);

                    int meter_id = Convert.ToInt32(CurrentRow["meter_id"].ToString());

                    textEditMeterID.EditValue = meter_id;
                    lookUpEditBuilding.EditValue = CurrentRow["building_id"].ToString();
                    textEditMeterName.EditValue = CurrentRow["meter_label"];
                    textEditActionFrom.EditValue = "GRIDVIEW2";

                    mruEditSerial.EditValue = CurrentRow["meter_serial"].ToString();
                    mruEditListModel.EditValue = CurrentRow["meter_models"].ToString();

                    lookUpEditADCList.EditValue = CurrentRow["device_adc_id"];

                    if (CurrentRow["meter_port"].ToString() == "Port1")
                    {
                        lookUpEditADCPort.EditValue = 1;
                    }
                    else if (CurrentRow["meter_port"].ToString() == "Port2")
                    {
                        lookUpEditADCPort.EditValue = 2;
                    }
                    else
                        lookUpEditADCPort.EditValue = -1;

                    textEditCTRatio.EditValue = (CurrentRow["meter_ct_ratio"].ToString() == "") ? 1 : Convert.ToInt32(CurrentRow["meter_ct_ratio"]);

                    switch (CurrentRow["meter_rating"].ToString())
                    {
                        case "5(10)":
                            lookUpEditRating.EditValue = 1;
                            break;
                        case "5(45)":
                            lookUpEditRating.EditValue = 2;
                            break;
                        case "5(100)":
                            lookUpEditRating.EditValue = 3;
                            break;
                        case "10(100)":
                            lookUpEditRating.EditValue = 4;
                            break;
                        default:
                            lookUpEditRating.EditValue = null;
                            break;
                    }

                    if (CurrentRow["meter_cloop_no"].To<int>() == 0)
                    {
                        lookUpEditCLoop.EditValue = 0;
                    }
                    else
                    {
                        lookUpEditCLoop.EditValue = CurrentRow["meter_cloop_no"];
                    }

                    if (lookUpEditBuilding.Enabled == false || textEditMeterName.Enabled == false)
                    {
                        lookUpEditBuilding.Enabled = true;
                        textEditMeterName.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void DeviceEMeter_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = (this.Width * 65) / 100;

            initRating();

            // section 2
            initDropDownBuilding();
            initMeterModelDropDown();

            initADC();
            initMeterPortDropDown();
            initCloopDropDown();

            initGridE_Meter1();
            initGridE_Meter2();

            mruEditSerial.Properties.Mask.EditMask = "([0-9]{7})";
            mruEditSerial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            setLangThis();
        }

        public void setLangThis()
        {
            // Group Box
            this.groupEMeter_Item.Text = getLanguage("_meter_item");
            this.groupEMeter_Setting.Text = getLanguage("_meter_setting");
            this.groupBoxE_Setting.Text = getLanguage("_meter_setting");
            this.groupBoxMeterInRoom.Text = getLanguage("_meter_in_room");
            this.groupBoxUtilityMeter.Text = getLanguage("_utility_meter");
            this.groupBoxE_Setting.Text = getLanguage("_meter_setting");

            // Label Setting
            this.labelControlBuilding.Text = getLanguageWithColon("_building");
            this.labelControlMeterName.Text = getLanguageWithColon("_name");
            this.labelControlSerialNo.Text = getLanguageWithColon("_serial_no");
            this.labelControlMeterModel1.Text = getLanguage("_meter_model");
            this.labelControlADC1.Text = getLanguage("_adc");
            this.labelControlPort1.Text = getLanguage("_port");
            this.labelControlCTMulti.Text = getLanguageWithColon("_ct_multiply");
            this.labelControlRating.Text = getLanguageWithColon("_rating");

            this.labelControlPort.Text = getLanguageWithColon("_port");
            this.labelControlMeterModel.Text = getLanguageWithColon("_meter_model");
            this.labelControlADC.Text = getLanguageWithColon("_adc");

            this.labelControlPort2.Text = getLanguageWithColon("_port");
            this.labelControlMeterModel2.Text = getLanguageWithColon("_meter_model");

            // Grid Column
            this.grid_no.Caption = getLanguage("_no");
            this.grid_Building.Caption = getLanguage("_building");
            this.grid_E_meter_name.Caption = getLanguage("_name");
            this.grid_meter_serial.Caption = getLanguage("_serial_no");
            this.grid_meter_port.Caption = getLanguage("_port");
            this.grid_meter_adc.Caption = getLanguage("_adc");
            this.grid_meter_model.Caption = getLanguage("_meter_model");
            this.grid_meter_ct_ratio.Caption = getLanguage("_ct_ratio");
            this.grid_meter_rating.Caption = getLanguage("_rating");
            this.grid_meter_status_text.Caption = getLanguage("_connection_status");

            // Grid Column 2
            this.grid_no2.Caption = getLanguage("_no");
            this.grid_Building2.Caption = getLanguage("_building");
            this.grid_E_meter_name2.Caption = getLanguage("_name");
            this.grid_meter_serial2.Caption = getLanguage("_serial_no");
            this.grid_meter_port2.Caption = getLanguage("_port");
            this.grid_meter_adc2.Caption = getLanguage("_adc");
            this.grid_meter_model2.Caption = getLanguage("_meter_model");
            this.grid_meter_ct_ratio2.Caption = getLanguage("_ct_ratio");
            this.grid_meter_rating2.Caption = getLanguage("_rating");
            this.grid_meter_status_text2.Caption = getLanguage("_connection_status");


            this.checkEditSelectAll.Text = getLanguage("_selectall");
            this.checkEditSelectAll2.Text = getLanguage("_selectall");
            this.labelControlRequired.Text = getLanguage("_required");


            // Button Text
            this.bttEdit.Text = getLanguage("_edit");
            this.bttSave.Text = getLanguage("_save");
            this.bttCancel.Text = getLanguage("_cancel");
            this.bttSetToADC.Text = getLanguage("_set_to_adc");
            this.bttTestConnection.Text = getLanguage("_test_connection");
            this.bttImport.Text = getLanguage("_import");
            this.bttSetInRoom.Text = getLanguage("_set");

        }

        void initMeterModelDropDown()
        {

            // Static
            DT_EMeter_model = new DataTable();

            DT_EMeter_model.Columns.Add("e_meter_id", typeof(int));
            DT_EMeter_model.Columns.Add("e_meter_label", typeof(string));

            DT_EMeter_model.Rows.Add(null, "Not Change");
            DT_EMeter_model.Rows.Add(1, "SX1-A35E");
            DT_EMeter_model.Rows.Add(2, "SX1-A85E");
            DT_EMeter_model.Rows.Add(3, "SX2-A35E");
            DT_EMeter_model.Rows.Add(4, "MX2-C01E");
            DT_EMeter_model.Rows.Add(5, "MX2-C41E");

            lookUpEditMeterModel.Properties.DisplayMember = "e_meter_label";
            lookUpEditMeterModel.Properties.ValueMember = "e_meter_id";
            lookUpEditMeterModel.Properties.NullText = "Not Change";
            lookUpEditMeterModel.Properties.DataSource = DT_EMeter_model;

            lookUpEditMeterModel2.Properties.DisplayMember = "e_meter_label";
            lookUpEditMeterModel2.Properties.ValueMember = "e_meter_id";
            lookUpEditMeterModel2.Properties.NullText = "Not Change";
            lookUpEditMeterModel2.Properties.DataSource = DT_EMeter_model;

            for (int i = 0; i < DT_EMeter_model.Rows.Count; i++)
            {
                if (!DT_EMeter_model.Rows[i][1].ToString().Contains("Not"))
                    mruEditListModel.Properties.Items.Add(DT_EMeter_model.Rows[i]["e_meter_label"]);
            }
            mruEditListModel.Properties.NullText = getLanguage("_meter_select");

        }
        void initADC()
        {
            DataTable DT_ADC_List = BusinessLogicBridge.DataStore.listADC();


            DataTable DT_ADC = new DataTable();
            DT_ADC.Columns.Add("adc_id", typeof(int));
            DT_ADC.Columns.Add("adc_label", typeof(string));
            //
            DataTable DT_ADC_2 = new DataTable();
            DT_ADC_2.Columns.Add("adc_id", typeof(int));
            DT_ADC_2.Columns.Add("adc_label", typeof(string));

            int CountRow = DT_ADC_List.Rows.Count;
            //
            DT_ADC.Rows.Add(null, "Not Change");
            //
            for (int i = 0; i < CountRow; i++)
            {
                DT_ADC.Rows.Add(DT_ADC_List.Rows[i]["device_adc_id"], DT_ADC_List.Rows[i]["device_adc_name"].ToString());
                //
                DT_ADC_2.Rows.Add(DT_ADC_List.Rows[i]["device_adc_id"], DT_ADC_List.Rows[i]["device_adc_name"].ToString());
            }

            lookUpEditADC.Properties.DisplayMember = "adc_label";
            lookUpEditADC.Properties.ValueMember = "adc_id";
            lookUpEditADC.Properties.NullText = "Not Change";
            lookUpEditADC.Properties.SortColumnIndex = 0;
            lookUpEditADC.Properties.DataSource = DT_ADC;
            //

            lookUpEditADC2.Properties.DisplayMember = "adc_label";
            lookUpEditADC2.Properties.ValueMember = "adc_id";
            lookUpEditADC2.Properties.NullText = "Not Change";
            lookUpEditADC2.Properties.SortColumnIndex = 0;
            lookUpEditADC2.Properties.DataSource = DT_ADC;

            lookUpEditADCList.Properties.DisplayMember = "adc_label";
            lookUpEditADCList.Properties.ValueMember = "adc_id";
            lookUpEditADCList.Properties.NullText = getLanguage("_select_adc");
            lookUpEditADCList.Properties.SortColumnIndex = 0;
            lookUpEditADCList.Properties.DataSource = DT_ADC_2;

            lookUpEditADCTop.Properties.DisplayMember = "adc_label";
            lookUpEditADCTop.Properties.ValueMember = "adc_id";
            lookUpEditADCTop.Properties.NullText = getLanguage("_select_adc");
            lookUpEditADCTop.Properties.SortColumnIndex = 0;
            lookUpEditADCTop.Properties.DataSource = DT_ADC_2;

        }
        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
        }
        void initCloopDropDown()
        {
            DataTable CLoopTable = new DataTable();

            CLoopTable.Columns.Add("cloop_id", typeof(int));
            CLoopTable.Columns.Add("cloop_no", typeof(int));


            for (int i = 0; i < 100; i++)
            {
                CLoopTable.Rows.Add(i, i);
            }

            lookUpEditCLoop.Properties.DisplayMember = "cloop_no";
            lookUpEditCLoop.Properties.ValueMember = "cloop_id";
            lookUpEditCLoop.Properties.DataSource = CLoopTable;
            lookUpEditCLoop.ItemIndex = 0;

            lookUpEditCLoopInroom.Properties.DisplayMember = "cloop_no";
            lookUpEditCLoopInroom.Properties.ValueMember = "cloop_id";
            lookUpEditCLoopInroom.Properties.DataSource = CLoopTable;
            lookUpEditCLoopInroom.ItemIndex = 0;

            lookUpEditCLoopInitial.Properties.DisplayMember = "cloop_no";
            lookUpEditCLoopInitial.Properties.ValueMember = "cloop_id";
            lookUpEditCLoopInitial.Properties.DataSource = CLoopTable;
            lookUpEditCLoopInitial.ItemIndex = 0;

            lookUpEditCLoopUtility.Properties.DisplayMember = "cloop_no";
            lookUpEditCLoopUtility.Properties.ValueMember = "cloop_id";
            lookUpEditCLoopUtility.Properties.DataSource = CLoopTable;
            lookUpEditCLoopUtility.ItemIndex = 0;
        }

        void initMeterPortDropDown()
        {

            DataTable DT_EMeter_port = new DataTable();

            DT_EMeter_port.Columns.Add("e_meter_port_id", typeof(int));
            DT_EMeter_port.Columns.Add("e_meter_port_label", typeof(string));
            //
            DataTable DT_EMeter_port_2 = new DataTable();

            DT_EMeter_port_2.Columns.Add("e_meter_port_id", typeof(int));
            DT_EMeter_port_2.Columns.Add("e_meter_port_label", typeof(string));

            //
            DT_EMeter_port.Rows.Add(null, "Not Change");
            DT_EMeter_port.Rows.Add(1, "Port1");
            DT_EMeter_port.Rows.Add(2, "Port2");
            //
            DT_EMeter_port_2.Rows.Add(1, "Port1");
            DT_EMeter_port_2.Rows.Add(2, "Port2");


            lookUpEditPort.Properties.DisplayMember = "e_meter_port_label";
            lookUpEditPort.Properties.ValueMember = "e_meter_port_id";
            lookUpEditPort.Properties.NullText = "Not Change";
            lookUpEditPort.Properties.DataSource = DT_EMeter_port;

            lookUpEditPort2.Properties.DisplayMember = "e_meter_port_label";
            lookUpEditPort2.Properties.ValueMember = "e_meter_port_id";
            lookUpEditPort2.Properties.NullText = "Not Change";
            lookUpEditPort2.Properties.DataSource = DT_EMeter_port;

            lookUpEditADCPort.Properties.DisplayMember = "e_meter_port_label";
            lookUpEditADCPort.Properties.ValueMember = "e_meter_port_id";
            lookUpEditADCPort.Properties.NullText = getLanguage("_port_select");
            lookUpEditADCPort.Properties.DataSource = DT_EMeter_port_2;

        }
        void initGridE_Meter1()
        {

            DataTable DT_EMeter = BusinessLogicBridge.DataStore.getlistE_Meter();

            DT_EMeter.Columns.Add("meter_no", typeof(string));
            DT_EMeter.Columns.Add("grid_meter_check", typeof(bool));
            DT_EMeter.Columns.Add("meter_status_text", typeof(string));

            for (int i = 0; i < DT_EMeter.Rows.Count; i++)
            {
                DT_EMeter.Rows[i]["grid_meter_check"] = false;
                DT_EMeter.Rows[i]["meter_no"] = i + 1;

                if (DT_EMeter.Rows[i]["meter_status"].ToString() == "True" && DT_EMeter.Rows[i]["device_adc_connection"].ToString() == "False")
                {
                    DT_EMeter.Rows[i]["meter_status_text"] = "Pass";
                }
                else
                {
                    DT_EMeter.Rows[i]["meter_status_text"] = "Fail";
                }


                switch (DT_EMeter.Rows[i]["meter_rating"].ToString())
                {
                    case "5(10)":
                        DT_EMeter.Rows[i]["meter_rating"] = "5(10)";
                        break;
                    case "5(45)":
                        DT_EMeter.Rows[i]["meter_rating"] = "5(45)";
                        break;
                    case "10(100)":
                        DT_EMeter.Rows[i]["meter_rating"] = "10(100)";
                        break;
                    default:
                        DT_EMeter.Rows[i]["meter_rating"] = "5(10)";
                        break;
                }

                if (DT_EMeter.Rows[i]["meter_serial"].ToString() == "")
                {
                    mruEditSerial.EditValue = "";
                    button_event = "Add";
                }
                else
                {
                    mruEditSerial.EditValue = DT_EMeter.Rows[i]["meter_serial"].ToString();
                    button_event = "Update";
                }

                if (DT_EMeter.Rows[i]["meter_models"].ToString() == "")
                {
                    mruEditListModel.SelectedIndex = 0;
                }
                else
                {
                    mruEditListModel.SelectedItem = DT_EMeter.Rows[i]["meter_models"];
                }

            }

            gridControlE_Meter1.DataSource = DT_EMeter;
            gridViewEMeter1.FocusedRowHandle = 0;


        }
        void initGridE_Meter2()
        {

            DataTable DT_EMeter = BusinessLogicBridge.DataStore.getlistE_MeterExcludeRoom();

            DT_EMeter.Columns.Add("meter_no", typeof(string));
            DT_EMeter.Columns.Add("meter_check", typeof(bool));
            DT_EMeter.Columns.Add("meter_status_text", typeof(string));

            for (int i = 0; i < DT_EMeter.Rows.Count; i++)
            {
                DT_EMeter.Rows[i]["meter_check"] = false;
                DT_EMeter.Rows[i]["meter_no"] = i + 1;

                if (DT_EMeter.Rows[i]["meter_status"].ToString() == "True")
                {
                    DT_EMeter.Rows[i]["meter_status_text"] = "Pass";
                }
                else
                {
                    DT_EMeter.Rows[i]["meter_status_text"] = "Fail";
                }

            }
            gridControlE_Meter2.DataSource = DT_EMeter;
            if (DT_EMeter.Rows.Count > 0)
            {
                gridViewEMeter2.FocusedRowHandle = 0;
            }

        }

        void initRating()
        {

            DataTable DTRating = new DataTable();

            DTRating.Columns.Add("rating_label", typeof(string));
            DTRating.Columns.Add("rating_id", typeof(int));

            DTRating.Rows.Add("5(10)", 1);
            DTRating.Rows.Add("5(45)", 2);
            DTRating.Rows.Add("5(100)", 3);
            DTRating.Rows.Add("10(100)", 4);


            lookUpEditRating.Properties.DisplayMember = "rating_label";
            lookUpEditRating.Properties.ValueMember = "rating_id";
            lookUpEditRating.Properties.NullText = DXWindowsApplication2.MainForm.getLanguage("_rating_select");
            lookUpEditRating.Properties.DataSource = DTRating;
        }
        void setEnable()
        {

            groupBoxE_Setting.Enabled = true;
            groupBoxMeterInRoom.Enabled = false;
            groupBoxUtilityMeter.Enabled = false;
            panelControl5.Enabled = false;
            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttEdit.Enabled = false;
        }
        void setDisable()
        {

            groupBoxE_Setting.Enabled = false;
            groupBoxMeterInRoom.Enabled = true;
            groupBoxUtilityMeter.Enabled = true;
            panelControl5.Enabled = true;
            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            textEditActionFrom.EditValue = "GRIDVIEW1";
            gridViewEMeter1.FocusedRowHandle = 0;
            bttEdit.Enabled = true;

        }

        void checkEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_CheckRoom == false)
            {
                _CheckRoom = true;
                //eventSelected();
            }
            else
            {
                _CheckRoom = false;
                //eventUnselect();
            }

            room_check_count = 0;
            if (gridViewEMeter1.RowCount > 0)
            {
                for (int i = 0; i < gridViewEMeter1.RowCount; i++)
                {
                    gridViewEMeter1.Columns[0].View.SetRowCellValue(i, "grid_meter_check", _CheckRoom);
                    if (_CheckRoom == true)
                    {
                        room_check_count += 1;
                    }
                }
            }
        }

        void checkEditSelectAll2_CheckedChanged(object sender, EventArgs e)
        {
            if (_CheckRoom2 == false)
            {
                _CheckRoom2 = true;
                //eventSelected();
            }
            else
            {
                _CheckRoom2 = false;
                //eventUnselect();
            }

            room_check_count2 = 0;
            if (gridViewEMeter2.RowCount > 0)
            {
                for (int i = 0; i < gridViewEMeter2.RowCount; i++)
                {
                    gridViewEMeter2.Columns[0].View.SetRowCellValue(i, "meter_check", _CheckRoom2);
                    if (_CheckRoom2 == true)
                    {
                        room_check_count2 = room_check_count2 + 1;
                    }
                }
            }
        }

        private DataTable validateData()
        {
            string star_notice = getLanguage("_msg_1001");

            String label = "";
            String message = "";
            Boolean focus = false;
            DataTable _ValidateTable = new DataTable();
            _ValidateTable.Columns.Add("label", typeof(String));
            _ValidateTable.Columns.Add("message", typeof(String));

            if (mruEditSerial.EditValue == null)
            {
                label = labelControlSerialNo.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    mruEditSerial.Focus();
                    focus = true;
                }
            }

            if (mruEditListModel.EditValue == null)
            {
                label = labelControlMeterModel.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    mruEditListModel.Focus();
                    focus = true;
                }
            }

            if (lookUpEditADCList.EditValue == null || lookUpEditADCList.EditValue.To<int>() == 0)
            {
                label = labelControlADC.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditADCList.Focus();
                    focus = true;
                }
            }

            if (lookUpEditADCPort.EditValue == null)
            {
                label = labelControlPort.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditADCPort.Focus();
                    focus = true;
                }
            }
            if (Convert.ToInt32(textEditCTRatio.EditValue) < 1 || textEditCTRatio.EditValue.ToString() == "")
            {
                label = labelControlCTMulti.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    textEditCTRatio.Focus();
                    focus = true;
                }
            }
            if (lookUpEditRating.EditValue == null)
            {
                label = labelControlRating.Text;
                message = star_notice;
                _ValidateTable.Rows.Add(label, message);
                if (focus == false)
                {
                    lookUpEditRating.Focus();
                    focus = true;
                }
            }



            return _ValidateTable;
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            button_event = "Update";

            setEnable();

            //mruEditSerial.Enabled = false;
            //mruEditListModel.Enabled = false;

            if (textEditActionFrom.EditValue.ToString() == "GRIDVIEW2")
            {
                initDropDownBuilding();
                textEditMeterName.Enabled = true;
            }
            else
            {
                //textEditMeterName.EditValue = gridViewEMeter1.GetRowCellValue(gridViewEMeter1.GetRowHandle, "meter_label");
                //mruEditSerial.EditValue = gridViewEMeter1.GetRowCellValue(gridViewEMeter1.GetRowHandle, "meter_serial");

                lookUpEditBuilding.Enabled = false;
                textEditMeterName.Enabled = false;
            }

        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                setDisable();
                initGridE_Meter1();
                initGridE_Meter2();

            }
            // reload data where e_setting id
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            if (textEditMeterName.EditValue.ToString() == "")
            {

                utilClass.showPopupMessegeBox(this, labelControlMeterName.Text + " " + getLanguage("_msg_1001"), getLanguage("_softwarename"));
                return;
            }
            bool checkSerialDuplicated = false;
            bool checkNameDuplicated = false;
            int amountCLoopOfADC = 0;
            int amountCLoopOfMeter = 0;
            DataTable SerialNo = (DataTable)(gridControlE_Meter1.DataSource);
            DataTable SerialNoUtil = (DataTable)(gridControlE_Meter2.DataSource);


            // check serial NO. 7 digit
            if (mruEditSerial.EditValue.ToString().Length != 7)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1043"), getLanguage("_softwarename"));
                return;
            }
            else if (mruEditSerial.EditValue.ToString() == "")
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1001"), getLanguage("_softwarename"));
                return;
            }
            else
            {
                // check serial Duplicate DataTable Grid
                string editSerial = string.Empty;
                string editName = string.Empty;
                if (button_event != "Add")
                {
                    int[] rowIndex = gridViewEMeter1.GetSelectedRows();
                    if (rowIndex[0] >= 0)
                    {
                        DataRow CurrentRow = gridViewEMeter1.GetDataRow(rowIndex[0]);
                        editSerial = CurrentRow["meter_serial"].ToString();
                        editName = CurrentRow["meter_label"].ToString();
                    }
                }

                foreach (DataRow dr in SerialNo.Rows)
                {
                    if (mruEditSerial.EditValue.ToString() == dr["meter_serial"].ToString())
                    {
                        checkSerialDuplicated = true;
                        break;
                    }
                    if ((button_event == "Add") && textEditMeterName.EditValue.ToString() == dr["meter_label"].ToString())
                    {
                        checkNameDuplicated = true;
                        break;
                    }
                }
                //check dt utility
                foreach (DataRow dr in SerialNoUtil.Rows)
                {
                    if (mruEditSerial.EditValue.ToString() == dr["meter_serial"].ToString())
                    {
                        checkSerialDuplicated = true;
                        break;
                    }
                    if ((button_event == "Add") && textEditMeterName.EditValue.ToString() == dr["meter_label"].ToString())
                    {
                        checkNameDuplicated = true;
                        break;
                    }
                }
                //
                if (checkSerialDuplicated)
                {
                    if (button_event != "Add" && mruEditSerial.EditValue.ToString() == editSerial)
                        checkSerialDuplicated = false;
                }
                if (checkNameDuplicated)
                {
                    if (button_event != "Add" && textEditMeterName.EditValue.ToString() == editName)
                        checkNameDuplicated = false;
                }

                //
                if (checkSerialDuplicated == true)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1046"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    if (checkNameDuplicated == true)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_adc_msg_1012"), getLanguage("_softwarename"));
                        return;
                    }

                    if (lookUpEditCLoop.EditValue.To<int>() == 0)
                    {
                        if (button_event == "Add")
                        {
                            // insert
                            BusinessLogicBridge.DataStore.insertE_Meter(Convert.ToInt32(lookUpEditBuilding.EditValue), textEditMeterName.EditValue.ToString(), lookUpEditADCPort.Text, mruEditSerial.EditValue.ToString(), mruEditListModel.EditValue.ToString(), lookUpEditADCList.EditValue.To<int>(), Convert.ToInt32(textEditCTRatio.EditValue), lookUpEditRating.Text);

                            // ADD LOG
                            BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [E-Meter Create]");
                        }
                        else
                        {
                            // update meter in room
                            BusinessLogicBridge.DataStore.updateE_Meter(textEditMeterName.EditValue.ToString(), lookUpEditADCPort.Text, mruEditSerial.EditValue.ToString(), mruEditListModel.EditValue.ToString(), lookUpEditADCList.EditValue.To<int>(), Convert.ToInt32(textEditCTRatio.EditValue), lookUpEditRating.Text, Convert.ToInt32(textEditMeterID.EditValue), lookUpEditCLoop.EditValue.To<int>());
                            BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [E-Meter Update]");

                        }
                        // No Duplicate
                        // Save Data into DB

                        // Reload gridview
                        initGridE_Meter1();
                        initGridE_Meter2();
                        //
                        gridViewEMeter1_FocusedRowChanged(null, null);
                        //
                        if (mruEditSerial.Properties.Items.Contains(mruEditSerial.SelectedItem) == true)
                        {
                            mruEditSerial.Properties.Items.Remove(mruEditSerial.SelectedItem);
                        }
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                        setDisable();
                    }
                    else
                    {
                        amountCLoopOfADC = BusinessLogicBridge.DataStore.countCLoopByADC(lookUpEditADCList.EditValue.To<int>());

                        if (amountCLoopOfADC <= 10)
                        {
                            amountCLoopOfMeter = BusinessLogicBridge.DataStore.countE_meterCLoop(lookUpEditCLoop.EditValue.To<int>(), lookUpEditADCList.EditValue.To<int>());

                            if (amountCLoopOfMeter <= 25)
                            {
                                if (button_event == "Add")
                                {
                                    // insert
                                    BusinessLogicBridge.DataStore.insertE_Meter(Convert.ToInt32(lookUpEditBuilding.EditValue), textEditMeterName.EditValue.ToString(), lookUpEditADCPort.Text, mruEditSerial.EditValue.ToString(), mruEditListModel.EditValue.ToString(), Convert.ToInt32(lookUpEditADCList.EditValue), Convert.ToInt32(textEditCTRatio.EditValue), lookUpEditRating.Text);
                                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [E-Meter Create]");
                                }
                                else
                                {
                                    // update meter in room
                                    BusinessLogicBridge.DataStore.updateE_Meter(textEditMeterName.EditValue.ToString(), lookUpEditADCPort.Text, mruEditSerial.EditValue.ToString(), mruEditListModel.EditValue.ToString(), Convert.ToInt32(lookUpEditADCList.EditValue), Convert.ToInt32(textEditCTRatio.EditValue), lookUpEditRating.Text, Convert.ToInt32(textEditMeterID.EditValue), lookUpEditCLoop.EditValue.To<int>());
                                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [E-Meter Update]");

                                }
                                // No Duplicate
                                // Save Data into DB

                                // Reload gridview
                                initGridE_Meter1();
                                initGridE_Meter2();
                                if (mruEditSerial.Properties.Items.Contains(mruEditSerial.SelectedItem) == true)
                                {
                                    mruEditSerial.Properties.Items.Remove(mruEditSerial.SelectedItem);
                                }
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                                setDisable();
                            }
                            else
                            {
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1075"), getLanguage("_softwarename"));
                                return;
                            }
                        }
                        else
                        {
                            utilClass.showPopupMessegeBox(this, getLanguage("_msg_1074"), getLanguage("_softwarename"));
                            return;
                        }

                    }
                }

            }
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            setEnable();
            initDropDownBuilding();
            button_event = "Add";

            lookUpEditBuilding.Enabled = true;
            textEditMeterName.Enabled = true;
            mruEditSerial.EditValue = "";
            initADC();
            initMeterPortDropDown();
            textEditMeterName.EditValue = "";
            textEditCTRatio.EditValue = 1;
            mruEditListModel.EditValue = null;
            lookUpEditRating.EditValue = null;
            lookUpEditCLoop.EditValue = 0;
            lookUpEditADCList.EditValue = null;
            lookUpEditADCPort.EditValue = null;
        }

        private void bttImport_Click(object sender, EventArgs e)
        {

            mruEditSerial.Enabled = true;
            mruEditListModel.Enabled = true;
            SerialModelList = new DataTable();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files (*.xls)|*.xls";
            if (open.ShowDialog() == DialogResult.OK)
            {

                FileInfo file_info = new FileInfo(open.FileName);
                string file_paht = file_info.FullName;

                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file_paht + ";Extended Properties=Excel 8.0");
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$];", con);

                da.Fill(SerialModelList);

                if (SerialModelList.Columns.Count > 2)
                {

                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                    return;
                }

                if (SerialModelList.Columns.Contains("serial_no") == true && SerialModelList.Columns.Contains("meter_model") == true)
                {


                    bool normalcase = true;

                    for (int j = 0; j < DT_EMeter_model.Rows.Count; j++)
                    {

                        for (int i = 0; i < SerialModelList.Rows.Count; i++)
                        {
                            if (SerialModelList.Rows[i]["serial_no"].ToString().Trim().Length != 7)
                            {
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                                mruEditSerial.Properties.Items.Clear();
                                return;
                            }

                            int result;
                            if (int.TryParse(SerialModelList.Rows[i]["serial_no"].ToString().Trim(), out result) == false)
                            {
                                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                                mruEditSerial.Properties.Items.Clear();
                                return;
                            }

                            var xxx = DT_EMeter_model.Rows[j].Table.Select("e_meter_label='" + SerialModelList.Rows[i]["meter_model"].ToString() + "'");

                            if (xxx.Length <= 0)
                            {
                                normalcase = false;
                            }

                            mruEditSerial.Properties.Items.Add(SerialModelList.Rows[i]["serial_no"]);
                        }
                    }

                    if (normalcase == false)
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                        mruEditSerial.Properties.Items.Clear();
                        return;
                    }

                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1067"), getLanguage("_softwarename"));
                    return;

                }

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3021"), getLanguage("_softwarename"), "info");


            }
        }

        private void bttSetToADC_Click(object sender, EventArgs e)
        {
            if (lookUpEditADCTop.EditValue == null)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                return;
            }
            //
            E_meterCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);
            E_meterCheckedBoxUtility = (DataTable)(gridControlE_Meter2.DataSource);
            //
            DataRow[] rInRoom = E_meterCheckedBox.Select("grid_meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "'");
            DataRow[] rInRoom2 = E_meterCheckedBoxUtility.Select("meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "'");
            //
            if (rInRoom.Length == 0 && rInRoom2.Length == 0)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                return;
            }
            //
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4022"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                try
                {
                    // Progress Bar.... Loading
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                    bw_SetToADC.RunWorkerAsync();

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }

            }

        }

        private void bttDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable E_meter2CheckedBox = (DataTable)(gridControlE_Meter2.DataSource);
                int AmountChecked = 0;

                for (int i = 0; i < E_meter2CheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meter2CheckedBox.Rows[i]["meter_check"]) == true)
                    {
                        AmountChecked++;
                    }
                }
                //
                if (AmountChecked == 0)
                {
                    // Not Checked
                    utilClass.showPopupMessegeBox(this, getLanguage("_adc_msg_1008"), getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4018"), getLanguage("_softwarename")) == DialogResult.Yes)
                    {
                        // Progress Bar.... Loading

                        // DELETE From ADC FROM BEE

                        // Delete From MYSQL
                        BusinessLogicBridge.DataStore.deleteE_Meter(Convert.ToInt16(textEditMeterID.EditValue));
                        BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "Device Setting [E-Meter Delete]");

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3002"), getLanguage("_softwarename"), "info");
                        // Have Deleted
                        // Reloaded Utility Meter
                        initGridE_Meter2();
                        setDisable();
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        private void btSetUtility_Click(object sender, EventArgs e)
        {
            DataTable E_meterUtilityCheckedBox = (DataTable)(gridControlE_Meter2.DataSource);
            DataTable E_meterUtilityCheckedBoxTemp = new DataTable();

            E_meterUtilityCheckedBoxTemp = E_meterUtilityCheckedBox.Clone();

            try
            {
                int counter = 0;
                for (int i = 0; i < E_meterUtilityCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterUtilityCheckedBox.Rows[i]["meter_check"]) == true)
                    {
                        counter++;
                    }
                }


                if (counter > 0)
                {

                    if (lookUpEditMeterModel2.ItemIndex > 0 || lookUpEditPort2.ItemIndex > 0 || lookUpEditADC2.ItemIndex > 0)
                    {
                        if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4030"), getLanguage("_softwarename")) == DialogResult.Yes)
                        {

                            for (int i = 0; i < E_meterUtilityCheckedBox.Rows.Count; i++)
                            {
                                DataRow r;
                                //
                                if (E_meterUtilityCheckedBox.Rows[i]["meter_check"].ToString() == "True")
                                {
                                    r = E_meterUtilityCheckedBox.NewRow();

                                    r = E_meterUtilityCheckedBox.Rows[i];
                                    E_meterUtilityCheckedBoxTemp.ImportRow(r);
                                }
                            }

                            for (int i = 0; i < E_meterUtilityCheckedBoxTemp.Rows.Count; i++)
                            {
                                string newPort = lookUpEditPort2.ItemIndex > 0 ?
                                        lookUpEditPort2.Text : E_meterUtilityCheckedBoxTemp.Rows[i]["meter_port"].ToString();
                                string newModel = lookUpEditMeterModel2.ItemIndex > 0 ?
                                    lookUpEditMeterModel2.Text : E_meterUtilityCheckedBoxTemp.Rows[i]["meter_models"].ToString();
                                int newADC = lookUpEditADC2.ItemIndex > 0 ?
                                    lookUpEditADC2.EditValue.To<int>() : E_meterUtilityCheckedBoxTemp.Rows[i]["device_adc_id"].To<int>();

                                // Update Meter
                                BusinessLogicBridge.DataStore.updateE_Meter(
                                    E_meterUtilityCheckedBoxTemp.Rows[i]["meter_label"].ToString(),
                                    newPort,
                                    E_meterUtilityCheckedBoxTemp.Rows[i]["meter_serial"].ToString(),
                                    newModel,
                                    newADC,
                                    Convert.ToInt32(E_meterUtilityCheckedBoxTemp.Rows[i]["meter_ct_ratio"]),
                                    E_meterUtilityCheckedBoxTemp.Rows[i]["meter_rating"].ToString(),
                                    Convert.ToInt32(E_meterUtilityCheckedBoxTemp.Rows[i]["meter_id"]),
                                    0);
                            }

                            initGridE_Meter2();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("You not select Model, ADC or Port. Please select item.");
                        return;
                    }
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void bttSetInRoom_Click(object sender, EventArgs e)
        {
            DataTable E_meterInRoomCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);

            try
            {

                int counter = 0;
                for (int i = 0; i < E_meterInRoomCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterInRoomCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {
                        counter++;
                    }
                }


                if (counter > 0)
                {
                    if (lookUpEditMeterModel.ItemIndex > 0 || lookUpEditPort.ItemIndex > 0 || lookUpEditADC.ItemIndex > 0)
                    {
                        if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4030"), getLanguage("_softwarename")) == DialogResult.Yes)
                        {
                            for (int i = 0; i < E_meterInRoomCheckedBox.Rows.Count; i++)
                            {
                                if ((bool)(E_meterInRoomCheckedBox.Rows[i]["grid_meter_check"]) == true)
                                {
                                    //
                                    string newPort = lookUpEditPort.ItemIndex > 0 ?
                                        lookUpEditPort.Text : E_meterInRoomCheckedBox.Rows[i]["meter_port"].ToString();
                                    string newModel = lookUpEditMeterModel.ItemIndex > 0 ?
                                        lookUpEditMeterModel.Text : E_meterInRoomCheckedBox.Rows[i]["meter_models"].ToString();
                                    int newADC = lookUpEditADC.ItemIndex > 0 ?
                                        lookUpEditADC.EditValue.To<int>() : E_meterInRoomCheckedBox.Rows[i]["device_adc_id"].To<int>();
                                    //

                                    // Update Meter
                                    BusinessLogicBridge.DataStore.updateE_Meter(
                                        E_meterInRoomCheckedBox.Rows[i]["meter_label"].ToString(),
                                        newPort,
                                        E_meterInRoomCheckedBox.Rows[i]["meter_serial"].ToString(),
                                        newModel,
                                        newADC,
                                        E_meterInRoomCheckedBox.Rows[i]["meter_ct_ratio"].To<int>(),
                                        E_meterInRoomCheckedBox.Rows[i]["meter_rating"].ToString(),
                                        E_meterInRoomCheckedBox.Rows[i]["meter_id"].To<int>(),
                                        0);
                                }
                            }
                            initGridE_Meter1();
                        }
                    }
                    else
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1079"), getLanguage("_softwarename"));
                        return;
                    }
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void bttTestConnection_Click(object sender, EventArgs e)
        {
            if (lookUpEditADCTop.EditValue == null)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                return;
            }
            E_meterCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);
            E_meterCheckedBoxUtility = (DataTable)(gridControlE_Meter2.DataSource);
            //
            DataRow[] rInRoom = E_meterCheckedBox.Select("grid_meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "'");
            DataRow[] rInRoom2 = E_meterCheckedBoxUtility.Select("meter_check=true and device_adc_name='" + lookUpEditADCTop.Text + "'");
            //
            if (rInRoom.Length == 0 && rInRoom2.Length == 0)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                return;
            }
            //

            DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
            bw_TestConnection.RunWorkerAsync();
        }

        private void bttSetCLoop2_Click(object sender, EventArgs e)
        {
            if ((lookUpEditADCTop.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_adc"), getLanguage("_softwarename"));
                //
                return;
            }

            if ((lookUpEditCLoopUtility.EditValue == null))
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_select_cloop"), getLanguage("_softwarename"));
                //
                return;
            }
            //
            DataTable E_meterInRoomCheckedBox = (DataTable)(gridControlE_Meter1.DataSource);
            DataTable E_meterInRoomCheckedBox2 = (DataTable)(gridControlE_Meter2.DataSource);
            int amountCLoop = 0;
            int amountCLoopMeter = 0;
            DataTable tableChecked = new DataTable();
            tableChecked.Columns.Add("meter_id", typeof(int));
            tableChecked.Columns.Add("device_adc_id", typeof(int));
            try
            {
                int counter = 0;
                for (int i = 0; i < E_meterInRoomCheckedBox.Rows.Count; i++)
                {
                    if ((bool)(E_meterInRoomCheckedBox.Rows[i]["grid_meter_check"]) == true)
                    {
                        counter++;
                        if (E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            amountCLoopMeter++;
                        }
                        tableChecked.Rows.Add(E_meterInRoomCheckedBox.Rows[i]["meter_id"], E_meterInRoomCheckedBox.Rows[i]["device_adc_id"]);
                    }
                    else
                    {
                        if (E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            if (lookUpEditCLoopUtility.EditValue.To<int>() == E_meterInRoomCheckedBox.Rows[i]["meter_cloop_no"].To<int>())
                            {
                                amountCLoopMeter++;
                            }
                        }
                    }
                }

                for (int i = 0; i < E_meterInRoomCheckedBox2.Rows.Count; i++)
                {
                    if ((bool)(E_meterInRoomCheckedBox2.Rows[i]["meter_check"]) == true)
                    {
                        counter++;

                        if (E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            amountCLoopMeter++;
                        }

                        tableChecked.Rows.Add(E_meterInRoomCheckedBox2.Rows[i]["meter_id"], E_meterInRoomCheckedBox2.Rows[i]["device_adc_id"]);

                    }
                    else
                    {
                        if (E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>() != 0)
                        {
                            if (lookUpEditCLoopUtility.EditValue.To<int>() == E_meterInRoomCheckedBox2.Rows[i]["meter_cloop_no"].To<int>())
                            {
                                amountCLoopMeter++;
                            }
                        }
                    }
                }

                if (counter > 0)
                {
                    if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4033"), getLanguage("_softwarename")) == DialogResult.Yes)
                    {

                        for (int j = 0; j < tableChecked.Rows.Count; j++)
                        {
                            if (lookUpEditCLoopUtility.EditValue.To<int>() == 0)
                            {
                                // update C-Loop => 0 Again on Database
                                BusinessLogicBridge.DataStore.updateE_MeterCLoopZeRo(tableChecked.Rows[j]["meter_id"].To<int>());
                                initGridE_Meter2();
                            }
                            else
                            {
                                // Check C-Loop more than 10 loop right?
                                amountCLoop = BusinessLogicBridge.DataStore.countCLoopByADC(tableChecked.Rows[j]["device_adc_id"].To<int>());
                                if (amountCLoop <= 10)
                                {
                                    // Check Old C-loop Summary Amount

                                    if (amountCLoopMeter <= 25)
                                    {
                                        BusinessLogicBridge.DataStore.updateE_MeterCLoop(tableChecked.Rows[j]["meter_id"].To<int>(), lookUpEditCLoopUtility.EditValue.To<int>());

                                        initGridE_Meter2();
                                    }
                                    else
                                    {
                                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1075"), getLanguage("_softwarename"));
                                        return;
                                    }

                                }
                                else
                                {
                                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1074"), getLanguage("_softwarename"));
                                    return;
                                }
                            }
                        }

                    }
                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1045"), getLanguage("_softwarename"));
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
