using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;

namespace DXWindowsApplication2.UserForms
{
    public partial class Recording_WMeter : uBase
    {
        double RoolBackValue = 100000; // Defualt
        string flagtype = "";
        bool date_record_event = false;
        List<int> rowUpdated = new List<int>();

        public Recording_WMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(Recording_EMeter_Load);
            SaveClick += new EventHandler(bttSave_Click);

            gridViewWMeterInRoom.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(gridViewWMeterInRoom_CustomRowCellEdit);
            gridViewWMeterInRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewWMeterInRoom_FocusedRowChanged);
            gridViewWMeterInRoom.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewWMeterInRoom_ValidateRow);
            gridViewWMeterInRoom.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gridViewWMeterInRoom_InvalidRowException);

            repositoryItemButtonEditCopyLastestToEnd.Click += new EventHandler(repositoryItemButtonEditCopyLastestToEnd_Click);
            repositoryItemButtonEditCopyEndToBegin.Click += new EventHandler(repositoryItemButtonEditCopyEndToBegin_Click);
            repositoryItemComboBoxLastestDate1.Leave += new EventHandler(repositoryItemComboBoxLastestDate1_Leave);
            repositoryItemButtonEditReading.Click += new EventHandler(repositoryItemButtonEditReading_Click);

            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);

            gridViewWMeterInRoom.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridViewWMeterInRoom_RowUpdated);
            dateEditRecord.EditValueChanged += new EventHandler(dateEditRecord_EditValueChanged);

            repositoryItemComboBoxLastestDate1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repositoryItemComboBoxLastestDate1.Mask.EditMask = "0*([0-9]{1,5})|0*([0-9]{1,5})\\.([0-9]){2}";

        }

        void repositoryItemComboBoxLastestDate1_Leave(object sender, EventArgs e)
        {
            int rows = gridViewWMeterInRoom.FocusedRowHandle;
            ComboBoxEdit x = (ComboBoxEdit)sender;
            string[] formatStringCheck = x.EditValue.ToString().Split(' ');

            if (formatStringCheck.Length > 1)
            {
                // Yes
                // date and value
                gridViewWMeterInRoom.SetRowCellValue(rows, "wpresent_date_update", formatStringCheck[0].ToString());
                gridViewWMeterInRoom.SetRowCellValue(rows, "wpresent_energy_value", formatStringCheck[1].To<double>());
            }
            else
            {
                // No
                // 50.00
                gridViewWMeterInRoom.SetRowCellValue(rows, "wpresent_date_update", DateTime.Now.ToString());
                gridViewWMeterInRoom.SetRowCellValue(rows, "wpresent_energy_value", formatStringCheck[0].To<double>());

            }
        }

        void dateEditRecord_EditValueChanged(object sender, EventArgs e)
        {
            date_record_event = true;
            LoadDefaultGridInRoom();
        }

        void gridViewWMeterInRoom_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (rowUpdated.Exists(i => i == e.RowHandle) == false)
            {
                rowUpdated.Add(e.RowHandle);
           } 
        }

        void repositoryItemButtonEditReading_Click(object sender, EventArgs e)
        {

            #region W-Meter Read Function

            DataRow currentRow = gridViewWMeterInRoom.GetDataRow(gridViewWMeterInRoom.FocusedRowHandle);
            DataRow[] foundRows;
            string MeterSerial = currentRow["meter_serial"].ToString();
            int MeterID = currentRow["meter_serial"].To<int>();

            bool status_have_adc = true;

            string connection_text = "";

            if (currentRow["wpresent_date_update"].ToString() != "")
            {
                if (Convert.ToDateTime(currentRow["wpresent_date_update"]).Date == DateTime.Today.Date)
                {

                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadEmeterPresent(MeterID);

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["wpresent_date_update"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["water_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", ERecord.Rows[0]["wpresent_date_update"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", ERecord.Rows[0]["wpresent_energy_value"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", ERecord.Rows[0]["wpresent_date_update"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", ERecord.Rows[0]["wpresent_energy_value"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", currentRow["wpresent_date_update"].To<DateTime>());
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", 0.00);
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "E_CommStatus", connection_text);

                }
                else
                {

                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadWaterRecordingByMeterAndDate(MeterID, currentRow["wpresent_date_update"].To<DateTime>());

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["DateLastest"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["w_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", ERecord.Rows[0]["w_unit"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", ERecord.Rows[0]["w_unit"]);
                            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update", currentRow["wpresent_date_update"].To<DateTime>());
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value", 0.00);
                        gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "E_CommStatus", connection_text);

                }
            }

            #endregion
        
        }

        public DateTime ConvertStringToDate(string dtDate, string strFormat)
        {
            DateTime dtResult;
            try
            {
                dtResult = DateTime.ParseExact(dtDate, strFormat, null);
                return dtResult;
            }
            catch (Exception ex)
            {
                //throw ex;
                return DateTime.MinValue;
            }
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
           LoadDefaultGridInRoom();
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            //setLangThis();
            //

            initDropDownBuilding();
            initGroupDateDropDown();

            if (bttSave.Enabled == true)
            {
                setEnable();
            }
            else
            {
                LoadDefaultGridInRoom();
                setDisable();
            }

            setThisLang();
        }

        void repositoryItemButtonEditCopyEndToBegin_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                Double EndEnergy = gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "end_energy").To<double>();
                DateTime EndDate = Convert.ToDateTime(gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "end_date"));

                gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wprevious_energy_billing", EndEnergy);
                gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wprevious_date_billing", EndDate);

                gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "total_unit", 0.00);

            }
        }

        void repositoryItemButtonEditCopyLastestToEnd_Click(object sender, EventArgs e)
        {
            Double LasttestEnergy = gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_energy_value").To<double>();
            DateTime LasttestDate = Convert.ToDateTime(gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wpresent_date_update"));

            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "end_energy", LasttestEnergy);
            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "end_date", LasttestDate);

            Double EndEnergy = gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "end_energy").To<double>();
            Double BeginEnergy = gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "wprevious_energy_billing").To<double>();

            Double Begin_RangeValue = 90000;
            Double End_RangeValue = 10000;

            Double SummaryTotal = 0;
            // Begin > end ?
            if (BeginEnergy > EndEnergy)
            {
                // begin > 90000 && End < 10000
                if (BeginEnergy > Begin_RangeValue && EndEnergy < End_RangeValue)
                {
                    // Rollback case
                    // ( Roolback + End ) - Begin  ======> [ 100,000 + End ] - Begin
                    SummaryTotal = (RoolBackValue + EndEnergy) - BeginEnergy;
                }
                else
                {
                    // AbNormal value
                    // End Value - Begin Value
                    SummaryTotal = EndEnergy - BeginEnergy;
                }
            }
            else
            {
                // Normal value
                // End Value - Begin Value
                SummaryTotal = EndEnergy - BeginEnergy;
            }
            
            gridViewWMeterInRoom.SetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "total_unit", SummaryTotal.ToString("N2"));
        }

        void gridViewWMeterInRoom_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        void gridViewWMeterInRoom_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (bttEdit.Enabled == false)
            {
                Double Begin_RangeValue = 90000;
                Double End_RangeValue = 10000;

                GridView view = sender as GridView;

                // Begin Value
                GridColumn previous_energy_billing = view.Columns[3];
                // Begin Date
                GridColumn previous_date_billing = view.Columns[2];

                // End Date
                GridColumn end_date_billing = view.Columns[5];
                // End Energy
                GridColumn end_energy_billing = view.Columns[6];

                // Total Unit Column
                GridColumn total_unit = view.Columns[7];

                //Get the value of the Begin Energy
                Double DoublePrevious_energy_billing = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, previous_energy_billing).ToString());

                //Get the date of the Begin Date
                DateTime DoublePrevious_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, previous_date_billing).ToString());

                //Get the value of the End energy
                Double DoubleEnd_energy_billing = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, end_energy_billing).ToString());

                //Get the date of the End date
                DateTime DoubleEnd_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, end_date_billing).ToString());

                // Lastest value
                GridColumn present_energy_billing = view.Columns[10];
                // Lastest date
                GridColumn present_date_billing = view.Columns[9];


                //Get the value of the Lastest
                Double DoublePresent_energy_billing = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, present_energy_billing).ToString());

                DateTime DoublePresent_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, present_date_billing).ToString());

                Double DoubleTotalUnit = 0;


                string ErrorMSG = "";

                //Validity criterion
                DateTime TodayNOW = DateTime.Today;

                //Validity criterion

                #region Begin

                // Begin date > End date ?
                if (DoublePrevious_date_billing.Date > DoubleEnd_date_billing.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1069") + "\r\n";
                }

                // Begin date > Today ?
                if (DoublePrevious_date_billing.Date > TodayNOW.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1055") + "\r\n";
                }

                // Begin Value Range validation
                if (DoublePrevious_energy_billing > 99999.99)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(previous_energy_billing, "is 0.00-99,999.99 only ");
                }

                #endregion

                #region End

                // End date > Today ?
                if (DoubleEnd_date_billing.Date > TodayNOW.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1055") + "\r\n";
                }

                // End date > Begin date ?
                if (DoubleEnd_date_billing.Date < DoublePrevious_date_billing.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1026") + "\r\n";
                }

                // End Value Range validation
                if (DoubleEnd_energy_billing > 99999.99)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(end_energy_billing, "is 0.00-99,999.99 only ");
                }
                #endregion

                #region Lasttest

                // Select date > Today ?
                if (DoublePresent_date_billing.Date > TodayNOW.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1055") + "\r\n";
                }

                // Select date < Begin ?
                if (DoublePresent_date_billing.Date < DoublePrevious_date_billing.Date)
                {
                    e.Valid = false;
                    ErrorMSG += getLanguage("_msg_1026") + "\r\n";
                }

                if (DoublePresent_energy_billing > 99999.99)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(present_energy_billing, "is 0.00-99,999.99 only ");
                }

                #endregion

                #region Total Section Calculate
                //total_unit

                // Begin > end ?
                if (DoublePrevious_energy_billing > DoubleEnd_energy_billing)
                {
                    // begin > 90000 && End < 10000
                    if (DoublePrevious_energy_billing > Begin_RangeValue && DoubleEnd_energy_billing < End_RangeValue)
                    {
                        // Rollback case
                        // ( Roolback + End ) - Begin  ======> [ 100,000 + End ] - Begin
                        DoubleTotalUnit = (RoolBackValue + DoubleEnd_energy_billing) - DoublePrevious_energy_billing;
                    }
                    else
                    {
                        // AbNormal value
                        // End Value - Begin Value
                        DoubleTotalUnit = DoubleEnd_energy_billing - DoublePrevious_energy_billing;
                    }

                }
                else
                {
                    // Normal value
                    // End Value - Begin Value
                    DoubleTotalUnit = DoubleEnd_energy_billing - DoublePrevious_energy_billing;
                }

                view.SetRowCellValue(e.RowHandle, total_unit, DoubleTotalUnit.ToString("N2"));

                #endregion

                if (ErrorMSG != "")
                {
                    utilClass.showPopupMessegeBox(this, ErrorMSG, getLanguage("_softwarename"));
                    return;
                }
            }
        }

        void gridViewWMeterInRoom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewWMeterInRoom.GetSelectedRows();
            if (rowIndex.Length <= 0) return;

             try
             {
                 if (rowIndex[0] >= 0)
                 {
                     DataRow CurrentRow = gridViewWMeterInRoom.GetDataRow(rowIndex[0]);
                     
                         flagtype = CurrentRow["flag_type_previous"].ToString();
                     
                 }
             } 
             catch (Exception ex)
             {
                 XtraMessageBox.Show(ex.Message.ToString());
             }

        }

        void gridViewWMeterInRoom_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //if (e.RowHandle < 0)
            //    return;

            //if (!e.Column.FieldName.Equals("wpresent_energy_value"))
            //    return;

            //if (e.Column.FieldName == "wpresent_energy_value")
            //{
            //    if (gridViewWMeterInRoom.FocusedRowHandle < 0) return;

            //    int meterID = gridViewWMeterInRoom.GetRowCellValue(gridViewWMeterInRoom.FocusedRowHandle, "water_id").To<int>();

            //    DataTable DTLook = new DataTable();

            //    DTLook.Columns.Add("DateLastest", typeof(DateTime));
            //    DTLook.Columns.Add("TotalUnit", typeof(double));

            //    DataTable LastestDT = new DataTable();

            //    LastestDT = BusinessLogicBridge.DataStore.getLastestWaterDropDown(meterID);
            //    repositoryItemComboBoxLastestDate1.Items.Clear();
            //    for (int j = 0; j < LastestDT.Rows.Count; j++)
            //    {

            //        DTLook.Rows.Add(LastestDT.Rows[j]["DateLastest"], LastestDT.Rows[j]["TotalUnit"].To<double>());

            //        repositoryItemComboBoxLastestDate1.Items.Add(LastestDT.Rows[j]["DateLastest"] + " " + LastestDT.Rows[j]["TotalUnit"].ToString());

            //    }

            //    e.RepositoryItem = repositoryItemComboBoxLastestDate1;

            //    //string[] formatStringCheck = gridViewWMeterInRoom.GetRowCellValue(e.RowHandle, "wpresent_energy_value").ToString().Split(' ');

            //    //if (formatStringCheck.Length > 1)
            //    //{
            //    //    // Yes
            //    //    // date and value
            //    //    gridViewWMeterInRoom.SetRowCellValue(e.RowHandle, "wpresent_date_update", formatStringCheck[0].ToString());
            //    //    gridViewWMeterInRoom.SetRowCellValue(e.RowHandle, "wpresent_energy_value", formatStringCheck[1].To<double>());
            //    //}

            //}
        }

        void Recording_EMeter_Load(object sender, EventArgs e)
        {
            try
            {
                initDropDownBuilding();
                initGroupDateDropDown();
                LoadDefaultGridInRoom();
            }
            catch { }
            setThisLang();
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_code";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
            if (BuildingTable.Rows.Count > 0)
            lookUpEditBuilding.EditValue = BuildingTable.Rows[0]["building_code"];
        }

        void initGroupDateDropDown() {
            
            dateEditRecord.EditValue = DateTime.Now;
        }
        
        void LoadDefaultGridInRoom()
        {
            string datetime = "";
            
            if (dateEditRecord.EditValue == null)
            {
                datetime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
            else
            {
                datetime = String.Format("{0:yyyy-MM-dd}", dateEditRecord.EditValue);
            }

            try
            {

                #region Meter in Room
                DataTable RecordBySerial = BusinessLogicBridge.DataStore.ReadWaterRecordingByDate(datetime, lookUpEditBuilding.EditValue.ToString());

                DataTable CheckinInfo = new DataTable("CheckinInfo");

                RecordBySerial.Columns.Add("total_unit", typeof(string));
                RecordBySerial.Columns.Add("flag_type_previous", typeof(string));
                RecordBySerial.Columns.Add("previous_energy_billingTemp", typeof(double));
                RecordBySerial.Columns.Add("previous_date_billingTemp");
                RecordBySerial.Columns.Add("present_energy_valueTemp", typeof(double));
                RecordBySerial.Columns.Add("present_date_updateTemp");
                RecordBySerial.Columns.Add("E_CommStatus");

                for (int i = 0; i < RecordBySerial.Rows.Count; i++)
                {

                    if (RecordBySerial.Rows[i]["wprevious_date_billing"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["wprevious_date_billing"] = DateTime.Now;
                    }

                    if (RecordBySerial.Rows[i]["wpresent_date_update"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["wpresent_date_update"] = DateTime.Now;
                    }

                    if (RecordBySerial.Rows[i]["wprevious_date_billing"].ToString() == "")
                    {
                        //check_in_electricit_date 	check_in_electricitymeter 	check_in_watermeter 	check_in_water_date 

                        CheckinInfo = BusinessLogicBridge.DataStore.ReadStartMeterByRoomIDFromCheckIn(RecordBySerial.Rows[i]["room_id"].ToString());

                        if (CheckinInfo.Rows.Count > 0)
                        {
                            RecordBySerial.Rows[i]["flag_type_previous"] = "fromcheckin";
                            RecordBySerial.Rows[i]["wprevious_date_billing"] = CheckinInfo.Rows[0]["check_in_water_date"];
                            RecordBySerial.Rows[i]["wprevious_energy_billing"] = CheckinInfo.Rows[0]["check_in_watermeter"];
                        }

                    }
                    else
                    {
                        RecordBySerial.Rows[i]["flag_type_previous"] = "frombilling";
                    }


                    if (RecordBySerial.Rows[i]["end_date"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["end_date"] = DateTime.Now;
                    }

                    if (RecordBySerial.Rows[i]["end_energy"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["end_energy"] = 0;
                    }

                    if (RecordBySerial.Rows[i]["meter_status"].To<int>() == 0)
                    {
                        RecordBySerial.Rows[i]["E_CommStatus"] = "FAIL";
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["E_CommStatus"] = "PASS";
                    }

                    Double Begin_RangeValue = 90000;
                    Double End_RangeValue = 10000;
                    Double DoubleTotalUnit = 0;

                    if (RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>() > RecordBySerial.Rows[i]["end_energy"].To<double>())
                    {

                        if (RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>() > Begin_RangeValue && RecordBySerial.Rows[i]["end_energy"].To<double>() < End_RangeValue)
                        {
                            // Rollback case
                            // ( Roolback + End ) - Begin
                            DoubleTotalUnit = (RoolBackValue + RecordBySerial.Rows[i]["end_energy"].To<double>()) - RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>();
                        }
                        else
                        {
                            // AbNormal value
                            // End Value - Begin Value
                            DoubleTotalUnit = RecordBySerial.Rows[i]["end_energy"].To<double>() - RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>();
                        }
                    }
                    else
                    {
                        DoubleTotalUnit = RecordBySerial.Rows[i]["end_energy"].To<double>() - RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>();
                    }

                    RecordBySerial.Rows[i]["total_unit"] = DoubleTotalUnit.ToString("N2");


                    // RecordBySerial.Rows[i]["total_unit"] = RecordBySerial.Rows[i]["end_energy"].To<double>() - RecordBySerial.Rows[i]["wprevious_energy_billing"].To<double>();

                    // Read Date 
                    if (RecordBySerial.Rows[i]["meter_read_date"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["meter_read_date"] = DateTime.Now;
                        RecordBySerial.Rows[i]["E_CommStatus"] = "";
                    }

                    if (RecordBySerial.Rows[i]["room_id"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["room_id"] = 0;
                    }
                }

                gridControlWMeter.DataSource = RecordBySerial;
                #endregion

                if (date_record_event == false) setDisable();
            }
            catch { }
        }

        void setThisLang()
        {
            bttEdit.Text = getLanguage("_edit");
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }
       
        private void setEnable()
        {
            // Copy end to begin & lastest to end
            gridViewWMeterInRoom.Columns[4].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[8].OptionsColumn.AllowEdit = true;

            // Meter In Room
            gridViewWMeterInRoom.Columns[2].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[3].OptionsColumn.AllowEdit = true;

            gridViewWMeterInRoom.Columns[5].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[6].OptionsColumn.AllowEdit = true;

            gridViewWMeterInRoom.Columns[9].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[10].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[11].OptionsColumn.AllowEdit = true;

            lookUpEditBuilding.Enabled = false;
            panelTopControl.Enabled = true;
            dateEditRecord.Enabled = true;

            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttEdit.Enabled = false;

        }

        private void setDisable()
        {

            // Copy end to begin & lastest to end
            gridViewWMeterInRoom.Columns[4].OptionsColumn.AllowEdit = false;
            gridViewWMeterInRoom.Columns[8].OptionsColumn.AllowEdit = false;

            // Meter In Room
            gridViewWMeterInRoom.Columns[2].OptionsColumn.AllowEdit = false;
            gridViewWMeterInRoom.Columns[3].OptionsColumn.AllowEdit = false;

            gridViewWMeterInRoom.Columns[5].OptionsColumn.AllowEdit = false;
            gridViewWMeterInRoom.Columns[6].OptionsColumn.AllowEdit = false;

            gridViewWMeterInRoom.Columns[9].OptionsColumn.AllowEdit = false;
            gridViewWMeterInRoom.Columns[10].OptionsColumn.AllowEdit = false;
            gridViewWMeterInRoom.Columns[11].OptionsColumn.AllowEdit = false;

            panelTopControl.Enabled = false;
            dateEditRecord.Enabled = false;

            lookUpEditBuilding.Enabled = true;

            bttSave.Enabled = false;
            bttCancel.Enabled = false;

            bttEdit.Enabled = true;

        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            gridViewWMeterInRoom.Columns[3].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[4].OptionsColumn.AllowEdit = true;

            gridViewWMeterInRoom.Columns[5].OptionsColumn.AllowEdit = true;
            gridViewWMeterInRoom.Columns[6].OptionsColumn.AllowEdit = true;

            setEnable();
            rowUpdated.Clear();

            date_record_event = false;
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            bool haveLessThanZero = false;
            DataTable AllRows = ((DataTable)gridControlWMeter.DataSource);

            for (int i = 0; i < AllRows.Rows.Count; i++)
            {

                if (AllRows.Rows[i]["total_unit"].To<double>() < 0)
                {
                    haveLessThanZero = true;
                }
            }

            if (haveLessThanZero == false)
            {
                if (rowUpdated.Count != 0)
                {
                    DataRow UpdateRow;

                    for (int i = 0; i < rowUpdated.Count; i++)
                    {

                        UpdateRow = gridViewWMeterInRoom.GetDataRow(rowUpdated[i]);
                        // Insert Transaction Water meter
                        //BusinessLogicBridge.DataStore.insertW_Record(UpdateRow);
                        BusinessLogicBridge.DataStore.updateW_MeterBeginAndEndAll(UpdateRow);
                    }

                    setDisable();
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                }

            }else {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1080"), getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }
        }

        private void gridViewWMeterInRoom_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("total_unit"))
                return;

            if (e.Column.FieldName == "total_unit")
            {
                if (gridViewWMeterInRoom.GetRowCellValue(e.RowHandle, "total_unit").ToString() != "")
                {
                    if (Convert.ToDouble(gridViewWMeterInRoom.GetRowCellValue(e.RowHandle, "total_unit")) < 0)
                    {
                        //e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold | FontStyle.Italic);
                        e.Appearance.BackColor = Color.Red;
                    }
                    else
                    {
                        //e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Regular);
                        e.Appearance.BackColor = Color.Green;
                    }
                }

            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                LoadDefaultGridInRoom();
                setDisable();
            }
        }

        private void bttLastestToEndAll_Click(object sender, EventArgs e)
        {
            Double LasttestEnergy;
            DateTime LasttestDate;
            Double EndEnergy;
            Double BeginEnergy;
            Double SummaryTotal;

            Double Begin_RangeValue = 90000;
            Double End_RangeValue = 10000;

            for (int i = 0; i < gridViewWMeterInRoom.RowCount; i++)
            {

                LasttestEnergy = gridViewWMeterInRoom.GetRowCellValue(i, "wpresent_energy_value").To<double>();
                LasttestDate = Convert.ToDateTime(gridViewWMeterInRoom.GetRowCellValue(i, "wpresent_date_update"));

                gridViewWMeterInRoom.SetRowCellValue(i, "end_energy", LasttestEnergy);
                gridViewWMeterInRoom.SetRowCellValue(i, "end_date", LasttestDate);

                EndEnergy = gridViewWMeterInRoom.GetRowCellValue(i, "end_energy").To<double>();
                BeginEnergy = gridViewWMeterInRoom.GetRowCellValue(i, "wprevious_energy_billing").To<double>();

                // Begin > end ?
                if (BeginEnergy > EndEnergy)
                {
                    // begin > 90000 && End < 10000
                    if (BeginEnergy > Begin_RangeValue && EndEnergy < End_RangeValue)
                    {
                        // Rollback case
                        // ( Roolback + End ) - Begin  ======> [ 100,000 + End ] - Begin
                        SummaryTotal = (RoolBackValue + EndEnergy) - BeginEnergy;
                    }
                    else
                    {
                        // AbNormal value
                        // End Value - Begin Value
                        SummaryTotal = EndEnergy - BeginEnergy;
                    }
                }
                else
                {
                    // Normal value
                    // End Value - Begin Value
                    SummaryTotal = EndEnergy - BeginEnergy;
                }
                gridViewWMeterInRoom.SetRowCellValue(i, "total_unit", SummaryTotal);
            }
        }

        private void bttEndToBeginAll_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {

                Double EndEnergy;
                DateTime EndDate;
                for (int i = 0; i < gridViewWMeterInRoom.RowCount; i++)
                {
                    EndEnergy = gridViewWMeterInRoom.GetRowCellValue(i, "end_energy").To<double>();
                    EndDate = Convert.ToDateTime(gridViewWMeterInRoom.GetRowCellValue(i, "end_date"));

                    gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_energy_billing", EndEnergy);
                    gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_date_billing", EndDate);
                    gridViewWMeterInRoom.SetRowCellValue(i, "total_unit", 0.00);
                }
            }
        }

        private void bttSetBeginFromInvoice_Click(object sender, EventArgs e)
        {
            int room_id = 0;
            DataTable invoiceDT = new DataTable();
            for (int i = 0; i < gridViewWMeterInRoom.RowCount; i++)
            {
                room_id = gridViewWMeterInRoom.GetRowCellValue(i, "room_id").To<int>();
                if (room_id != 0)
                {
                    invoiceDT = BusinessLogicBridge.DataStore.getBeginMeterTransFromInvoiceByRoomID(room_id);
                    if (invoiceDT.Rows.Count > 0)
                    {
                        // Set Data as invoice transaction
                        gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_energy_billing", invoiceDT.Rows[0]["inv_trans_wmeter_present_energy"]);
                        gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_date_billing", invoiceDT.Rows[0]["inv_trans_wmeter_present_date"]);
                    }
                    else
                    {
                        // Set Cell is 0
                        gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_energy_billing", 0);
                        gridViewWMeterInRoom.SetRowCellValue(i, "wprevious_date_billing", DateTime.Today);
                    }
                }
            }
        }

        private void bttReadAll_Click(object sender, EventArgs e)
        {
            DataTable AllRow = ((DataTable)gridControlWMeter.DataSource);

            #region Meter in room

            DataRow currentRow;
            DataTable ERecord = new DataTable();
            adcHelper EmeterDT = new adcHelper();
            string connection_text = "";

            int result = DateTime.Compare(dateEditRecord.EditValue.To<DateTime>().Date, DateTime.Now.Date);

            // pd date < dateEditRecord.EditValue
            if (result == 0)
            {
                ERecord = BusinessLogicBridge.DataStore.ReadWmeterPresentAll(lookUpEditBuilding.EditValue.To<int>());

                int ErowCount = ERecord.Rows.Count;

                for (int k = 0; k < ErowCount; k++)
                {
                    if (ErowCount > 0 && ERecord.Rows[k]["wpresent_date_update"].ToString() != "")
                    {
                        if (ERecord.Rows[0]["water_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", ERecord.Rows[0]["wpresent_date_update"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", ERecord.Rows[0]["wpresent_energy_value"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";

                        }
                        else
                        {
                            // pass
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", ERecord.Rows[0]["wpresent_date_update"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", ERecord.Rows[0]["wpresent_energy_value"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }
                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", DateTime.Today);
                        gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", 0.00);
                        gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewWMeterInRoom.SetRowCellValue(k, "E_CommStatus", connection_text);
                }

            }
            else
            {
                for (int k = 0; k < AllRow.Rows.Count; k++)
                {
                    currentRow = gridViewWMeterInRoom.GetDataRow(k);

                    ERecord = BusinessLogicBridge.DataStore.ReadWaterRecordingByMeterAndDate(currentRow["water_id"].To<int>(), dateEditRecord.EditValue.To<DateTime>());

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["DateLastest"].ToString() != "")
                    {
                        if (ERecord.Rows[0]["w_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", ERecord.Rows[0]["w_unit"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";

                        }
                        else
                        {
                            // pass
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", ERecord.Rows[0]["w_unit"]);
                            gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_date_update", dateEditRecord.EditValue.To<DateTime>());
                        gridViewWMeterInRoom.SetRowCellValue(k, "wpresent_energy_value", 0.00);
                        gridViewWMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewWMeterInRoom.SetRowCellValue(k, "E_CommStatus", connection_text);
                }
                
            }

            #endregion

            //if (adc_connected == false)
            //{
            //    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1053"), getLanguage("_softwarename"));
            //}
        }
    }
}
