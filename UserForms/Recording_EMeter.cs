
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
    public partial class Recording_EMeter : uBase
    {
        double RoolBackValue = 100000.00; // Defualt
        string flagtype = "";
        bool firsttime = false;
        bool date_record_event = false;
        List<int> rowUpdated = new List<int>();
        List<int> rowUpdated2 = new List<int>();

        public Recording_EMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(Recording_EMeter_Load);
            SaveClick += new EventHandler(bttSave_Click);
            repositoryItemButtonEditReading.ButtonClick += new ButtonPressedEventHandler(repositoryItemButtonEditReading_ButtonClick);
            repositoryItemButtonEditReading2.ButtonClick += new ButtonPressedEventHandler(repositoryItemButtonEditReading2_ButtonClick);

            repositoryItemComboBoxLastestDate1.Leave += new EventHandler(repositoryItemComboBoxLastestDate1_Leave);
            repositoryItemComboBoxLastestDate2.Leave += new EventHandler(repositoryItemComboBoxLastestDate2_Leave);

            repositoryItemTextEditBegin.Leave += new EventHandler(repositoryItemTextEditBegin_Leave);
            repositoryItemTextEditBegin2.Leave += new EventHandler(repositoryItemTextEditBegin2_Leave);

            repositoryItemTextEditEnd.Leave += new EventHandler(repositoryItemTextEditEnd_Leave);            
            repositoryItemTextEditEnd2.Leave += new EventHandler(repositoryItemTextEditEnd2_Leave);

            repositoryItemButtonEditCopyLastestToEnd.Click += new EventHandler(repositoryItemButtonEditCopyLastestToEnd_Click);
            repositoryItemButtonEditCopyLastestToEnd2.Click += new EventHandler(repositoryItemButtonEditCopyLastestToEnd2_Click);
            repositoryItemButtonEditCopyEndToBegin.Click += new EventHandler(repositoryItemButtonEditCopyEndToBegin_Click);
            repositoryItemButtonEditCopyEndToBegin2.Click += new EventHandler(repositoryItemButtonEditCopyEndToBegin2_Click);
           
            gridViewMeterInRoom.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewMeterInRoom_FocusedRowChanged);
            gridViewMeterInRoom.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewMeterInRoom_ValidateRow);
            gridViewMeterInRoom.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gridViewMeterInRoom_InvalidRowException);

            gridViewMeterUtility.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewMeterUtility_FocusedRowChanged);
            gridViewMeterUtility.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewMeterUtility_ValidateRow);
            gridViewMeterUtility.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gridViewMeterUtility_InvalidRowException);

            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);

            gridViewMeterInRoom.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridViewMeterInRoom_RowUpdated);
            gridViewMeterUtility.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridViewMeterUtility_RowUpdated);
            
            dateEditRecord.EditValueChanged += new EventHandler(dateEditRecord_EditValueChanged);

            repositoryItemComboBoxLastestDate1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repositoryItemComboBoxLastestDate1.Mask.EditMask = "0*([0-9]{1,5})|0*([0-9]{1,5})\\.([0-9]){2}";

            repositoryItemComboBoxLastestDate2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repositoryItemComboBoxLastestDate2.Mask.EditMask = "0*([0-9]{1,5})|0*([0-9]{1,5})\\.([0-9]){2}";

            repositoryItemComboBoxLastestDate1.SelectedIndexChanged += new EventHandler(repositoryItemComboBoxLastestDate1_SelectedIndexChanged);

        }

        void repositoryItemTextEditBegin2_Leave(object sender, EventArgs e)
        {
            Text = (sender as TextEdit).Text;
            if (Text == "")
            {
                gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "previous_energy_billing", 0.00);
            }
        }

        void repositoryItemTextEditBegin_Leave(object sender, EventArgs e)
        {
            Text = (sender as TextEdit).Text;

            if (Text == "")
            {
                gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "previous_energy_billing", 0.00);
            }
        }

        void repositoryItemTextEditEnd_Leave(object sender, EventArgs e)
        {
            Double EndEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_energy").To<double>();
            Double BeginEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "previous_energy_billing").To<double>();
            Double Sum = EndEnergy - BeginEnergy;

            Text = (sender as TextEdit).Text;

            if (Text == "")
            {
                gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_energy", 0.00);
            }
            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "total_unit", Sum.ToString("N2"));
        }

        void repositoryItemTextEditEnd2_Leave(object sender, EventArgs e)
        {
            Double EndEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_energy").To<double>();
            Double BeginEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "previous_energy_billing").To<double>();
            Double Sum = EndEnergy - BeginEnergy;

            Text = (sender as TextEdit).Text;

            if (Text == "")
            {
                gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_energy", 0.00);
            }

            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "total_unit", Sum.ToString("N2"));
        }

        void repositoryItemComboBoxLastestDate1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit x = (ComboBoxEdit)sender;

            string[] formatStringCheck = x.EditValue.ToString().Split(' ');

            x.EditValue = formatStringCheck[0].ToString() + " " + formatStringCheck[1].To<double>();

        }

        void repositoryItemComboBoxLastestDate2_Leave(object sender, EventArgs e)
        {
            int rows = gridViewMeterUtility.FocusedRowHandle;
            ComboBoxEdit x = (ComboBoxEdit)sender;
            string[] formatStringCheck = x.EditValue.ToString().Split(' ');

            if (formatStringCheck.Length > 1)
            {
                // Yes
                // date and value
                gridViewMeterUtility.SetRowCellValue(rows, "present_date_update", formatStringCheck[0].ToString());
                gridViewMeterUtility.SetRowCellValue(rows, "present_energy_value", formatStringCheck[1].To<double>());
            }
            else
            {
                // No
                // 50.00
                gridViewMeterUtility.SetRowCellValue(rows, "present_date_update", DateTime.Now.ToString());
                gridViewMeterUtility.SetRowCellValue(rows, "present_energy_value", formatStringCheck[0].To<double>());

            }
        }

        void repositoryItemComboBoxLastestDate1_Leave(object sender, EventArgs e)
        {
            int rows = gridViewMeterInRoom.FocusedRowHandle;
            ComboBoxEdit x = (ComboBoxEdit)sender;
            string[] formatStringCheck = x.EditValue.ToString().Split(' ');

            if (formatStringCheck.Length > 1)
            {
                // Yes
                // date and value
                gridViewMeterInRoom.SetRowCellValue(rows, "present_date_update", formatStringCheck[0].ToString());
                gridViewMeterInRoom.SetRowCellValue(rows, "present_energy_value", formatStringCheck[1].To<double>());
            }
            else
            {
                // No
                // 50.00
                //gridViewMeterInRoom.SetRowCellValue(rows, "present_date_update", DateTime.Now.ToString());
                gridViewMeterInRoom.SetRowCellValue(rows, "present_energy_value", formatStringCheck[0].To<double>());

            }
        }
        
        void repositoryItemButtonEditReading_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Read Button
            #region E-Meter Read Function

            DataRow currentRow = gridViewMeterInRoom.GetDataRow(gridViewMeterInRoom.FocusedRowHandle);
            string MeterSerial = currentRow["meter_serial"].ToString();
            int MeterID = currentRow["meter_id"].To<int>();
            string connection_text = "";

            if (currentRow["present_date_update"].ToString() != "")
            {
                if (Convert.ToDateTime(currentRow["present_date_update"]).Date == DateTime.Today.Date)
                {
                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadEmeterPresent(MeterID);
                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["present_date_update"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["meter_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["present_date_update"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["present_energy_value"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["present_date_update"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["present_energy_value"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", DateTime.Today);
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", 0.00);
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "E_CommStatus", connection_text);

                }
                else
                {
                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadRecordingByMeterAndDate(MeterID, currentRow["present_date_update"].To<DateTime>());

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["DateLastest"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["e_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update", currentRow["present_date_update"].To<DateTime>());
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value", 0.00);
                        gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "E_CommStatus", connection_text);
                }
            }

            #endregion
        }

        void repositoryItemButtonEditReading2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow currentRow = gridViewMeterUtility.GetDataRow(gridViewMeterUtility.FocusedRowHandle);
            string MeterSerial  = currentRow["meter_serial"].ToString();
            int MeterID = currentRow["meter_id"].To<int>();
            string connection_text = "";

            if (currentRow["present_date_update"].ToString() != "")
            {
                if (Convert.ToDateTime(currentRow["present_date_update"]).Date == DateTime.Today.Date)
                {
                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadEmeterPresent(MeterID);

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["present_date_update"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["meter_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["present_date_update"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["present_energy_value"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["present_date_update"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["present_energy_value"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", DateTime.Today);
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", 0.00);
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "E_CommStatus", connection_text);

                }
                else
                {
                    DataTable ERecord = BusinessLogicBridge.DataStore.ReadRecordingByMeterAndDate(MeterID, currentRow["present_date_update"].To<DateTime>());

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["DateLastest"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["e_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update", currentRow["present_date_update"].To<DateTime>());
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value", 0.00);
                        gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "E_CommStatus", connection_text);

                }
            }
        }

        void dateEditRecord_EditValueChanged(object sender, EventArgs e)
        {
            date_record_event = true;
            LoadDefaultGridInRoom();
        }

        void gridViewMeterUtility_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (rowUpdated2.Exists(i => i == e.RowHandle) == false)
            {
                rowUpdated2.Add(e.RowHandle);
            }
        }

        void gridViewMeterInRoom_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (rowUpdated.Exists(i => i == e.RowHandle) == false)
            {
                rowUpdated.Add(e.RowHandle);
            }
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            //int having = BusinessLogicBridge.DataStore.CheckEMeterMappingByBuildingLabel(lookUpEditBuilding.Text);
            //if (having > 0)
            //{
                LoadDefaultGridInRoom();
            //}
        }

        void repositoryItemButtonEditCopyEndToBegin2_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                Double EndEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_energy").To<double>();
                DateTime EndDate = Convert.ToDateTime(gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_date"));

                gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "previous_energy_billing", EndEnergy);
                gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "previous_date_billing", EndDate);

                gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "total_unit", 0.00);
            }
        }

        void repositoryItemButtonEditCopyLastestToEnd2_Click(object sender, EventArgs e)
        {
            Double LasttestEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_energy_value").To<double>();
            DateTime LasttestDate = Convert.ToDateTime(gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "present_date_update"));

            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_energy", LasttestEnergy);
            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_date", LasttestDate);

            Double EndEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "end_energy").To<double>();
            Double BeginEnergy = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "previous_energy_billing").To<double>();
            
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

            gridViewMeterUtility.SetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "total_unit", SummaryTotal.ToString("N2"));
        }

        void repositoryItemButtonEditCopyEndToBegin_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                Double EndEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_energy").To<double>();
                DateTime EndDate = Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_date"));

                gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "previous_energy_billing", EndEnergy);
                gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "previous_date_billing", EndDate);


                gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "total_unit", 0.00);

            }
        }

        void repositoryItemButtonEditCopyLastestToEnd_Click(object sender, EventArgs e)
        {
            Double LasttestEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_energy_value").To<double>();
            DateTime LasttestDate = Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "present_date_update"));

            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_energy", LasttestEnergy);
            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_date", LasttestDate);

            Double EndEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "end_energy").To<double>();
            Double BeginEnergy = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "previous_energy_billing").To<double>();


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

            gridViewMeterInRoom.SetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "total_unit", SummaryTotal.ToString("N2"));
           
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
        
        public override void Refresh()
        {
            base.Refresh();
            //
            //setLangThis();
            //

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

        void gridViewMeterInRoom_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        void gridViewMeterInRoom_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (bttEdit.Enabled == false)
            {
                Double Begin_RangeValue = 90000;
                Double End_RangeValue = 10000;

                GridView view = sender as GridView;

                // Begin Value
                GridColumn previous_energy_billing = view.Columns[4];
                // Begin Date
                GridColumn previous_date_billing = view.Columns[3];

                // End Date
                GridColumn end_date_billing = view.Columns[6];
                // End Energy
                GridColumn end_energy_billing = view.Columns[7];

                // Total Unit Column
                GridColumn total_unit = view.Columns[8];

                //Get the value of the Begin Energy
                Double DoublePrevious_energy_billing = view.GetRowCellValue(e.RowHandle, previous_energy_billing).ToString().To<double>();

                //Get the date of the Begin Date
                DateTime DoublePrevious_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, previous_date_billing).ToString());

                //Get the value of the End energy
                Double DoubleEnd_energy_billing = view.GetRowCellValue(e.RowHandle, end_energy_billing).ToString().To<double>();

                //Get the date of the End date
                DateTime DoubleEnd_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, end_date_billing).ToString());

                // Lastest value
                GridColumn present_energy_billing = view.Columns[11];
                // Lastest date
                GridColumn present_date_billing = view.Columns[10];


                //Get the value of the Lastest
                Double DoublePresent_energy_billing = view.GetRowCellValue(e.RowHandle, present_energy_billing).ToString().To<double>();

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

        void gridViewMeterInRoom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewMeterInRoom.GetSelectedRows();
            if (rowIndex.Length == 0)
                return;
            //
             try
             {
                 if (rowIndex[0] >= 0)
                 {
                     DataRow CurrentRow = gridViewMeterInRoom.GetDataRow(rowIndex[0]);
                     
                         flagtype = CurrentRow["flag_type_previous"].ToString();

                         firsttime = false;

                 }

             } 
             catch (Exception ex)
             {
                 XtraMessageBox.Show(ex.Message.ToString());
             }

        }

        void gridViewMeterUtility_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        void gridViewMeterUtility_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (bttEdit.Enabled == false)
            {
                Double Begin_RangeValue = 90000;
                Double End_RangeValue = 10000;

                GridView view = sender as GridView;

                // Begin Value
                GridColumn previous_energy_billing = view.Columns[4];
                // Begin Date
                GridColumn previous_date_billing = view.Columns[3];

                // End Date
                GridColumn end_date_billing = view.Columns[6];
                // End Energy
                GridColumn end_energy_billing = view.Columns[7];

                // Total Unit Column
                GridColumn total_unit = view.Columns[8];

                //Get the value of the Begin Energy
                Double DoublePrevious_energy_billing = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, previous_energy_billing).ToString());

                //Get the date of the Begin Date
                DateTime DoublePrevious_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, previous_date_billing).ToString());

                //Get the value of the End energy
                Double DoubleEnd_energy_billing = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, end_energy_billing).ToString());

                //Get the date of the End date
                DateTime DoubleEnd_date_billing = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, end_date_billing).ToString());

                // Lastest value
                GridColumn present_energy_billing = view.Columns[11];
                // Lastest date
                GridColumn present_date_billing = view.Columns[10];


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

                //#endregion

                if (ErrorMSG != "")
                {
                    utilClass.showPopupMessegeBox(this, ErrorMSG, getLanguage("_softwarename"));
                    return;
                }
                else
                {
                    if (rowUpdated2.Exists(i => i == e.RowHandle) == false)
                    {

                        rowUpdated2.Add(e.RowHandle);
                    }
                }
            }
        }

        void gridViewMeterUtility_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewMeterUtility.GetSelectedRows();

            if (rowIndex.Length == 0)
                return;

            try
            {
                if (rowIndex[0] >= 0)
                {
                    DataRow CurrentRow = gridViewMeterUtility.GetDataRow(rowIndex[0]);

                    if (CurrentRow == null) return;

                    flagtype = CurrentRow["flag_type_previous"].ToString();

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void Recording_EMeter_Load(object sender, EventArgs e)
        {
            try
            {
                initDropDownBuilding(); // First Load Alway
                initGroupDateDropDown();
                LoadDefaultGridInRoom();
            }
            catch { }
            setThisLang();
        }

        void setThisLang()
        {
            bttEdit.Text = getLanguage("_edit");
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
            lookUpEditBuilding.EditValue = BuildingTable.Rows[0]["building_id"];
        }
        void insertRecordTest(){

            DataTable RecordFromAPI = new DataTable();
            

            RecordFromAPI = DXWindowsApplication2.MainForm.dictADC["adc_id_1"].GetAllEMeterTransactionDT();

            RecordFromAPI.Columns.Add("device_adc_id", typeof(int));
            for (int i = 0; i < RecordFromAPI.Rows.Count; i++)
            {
                RecordFromAPI.Rows[i]["device_adc_id"] = 1;
            }

            BusinessLogicBridge.DataStore.insertE_RecordToMysql(RecordFromAPI);
        }
        void initGroupDateDropDown() {

            dateEditRecord.EditValue = DateTime.Now;

            //DataTable List_DateGroup = BusinessLogicBridge.DataStore.getGroupDateRecord();

            //if (List_DateGroup.Rows.Count == 0) {
            //    List_DateGroup.Rows.Add(String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            //}

            //lookUpEditRecordDate.Properties.DisplayMember = "groupdate";
            //lookUpEditRecordDate.Properties.ValueMember = "groupdate";
            //lookUpEditRecordDate.Properties.DataSource = List_DateGroup;

            //lookUpEditRecordDate.ItemIndex = 0;


        }
        void LoadDefaultGridInRoom()
        {
            //
            lookUpEditBuilding.Enabled = true;
            //

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

                DataTable RecordBySerial = BusinessLogicBridge.DataStore.ReadRecordingByDate(datetime, lookUpEditBuilding.EditValue.To<int>());

                DataTable CheckinInfo = new DataTable("CheckinInfo");

                RecordBySerial.Columns.Add("total_unit", typeof(string));
                RecordBySerial.Columns.Add("flag_type_previous", typeof(string));
                RecordBySerial.Columns.Add("previous_energy_billingTemp", typeof(double));
                RecordBySerial.Columns.Add("previous_date_billingTemp");
                RecordBySerial.Columns.Add("present_energy_valueTemp", typeof(double));
                RecordBySerial.Columns.Add("present_date_updateTemp");
                RecordBySerial.Columns.Add("E_CommStatus");
                RecordBySerial.Columns.Add("meter_cut_text");

                for (int i = 0; i < RecordBySerial.Rows.Count; i++)
                {
                    RecordBySerial.Rows[i]["previous_energy_billingTemp"] = RecordBySerial.Rows[i]["previous_energy_billing"];

                    if (RecordBySerial.Rows[i]["previous_date_billing"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["previous_date_billing"] = DateTime.Now;
                    }

                    RecordBySerial.Rows[i]["previous_date_billingTemp"] = RecordBySerial.Rows[i]["previous_date_billing"];

                    RecordBySerial.Rows[i]["present_energy_valueTemp"] = RecordBySerial.Rows[i]["present_energy_value"];

                    if (RecordBySerial.Rows[i]["present_date_update"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["present_date_updateTemp"] = DateTime.Now;
                        RecordBySerial.Rows[i]["present_date_update"] = DateTime.Now;
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["present_date_updateTemp"] = RecordBySerial.Rows[i]["present_date_update"];
                    }

                    if (RecordBySerial.Rows[i]["present_energy_value"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["present_energy_valueTemp"] = 0;
                        RecordBySerial.Rows[i]["present_energy_value"] = 0;
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["present_energy_valueTemp"] = RecordBySerial.Rows[i]["present_energy_value"];
                    }


                    if (RecordBySerial.Rows[i]["end_date"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["end_date"] = DateTime.Now;
                    }

                    if (RecordBySerial.Rows[i]["end_energy"].ToString() == "")
                    {
                        RecordBySerial.Rows[i]["end_energy"] = 0;
                    }


                    if (RecordBySerial.Rows[i]["previous_date_billing"].ToString() == "")
                    {
                        //check_in_electricit_date 	check_in_electricitymeter 	check_in_watermeter 	check_in_water_date 

                        CheckinInfo = BusinessLogicBridge.DataStore.ReadStartMeterByRoomIDFromCheckIn(RecordBySerial.Rows[i]["room_id"].ToString());

                        if (CheckinInfo.Rows.Count > 0)
                        {
                            RecordBySerial.Rows[i]["flag_type_previous"] = "fromcheckin";
                            RecordBySerial.Rows[i]["previous_date_billing"] = CheckinInfo.Rows[0]["check_in_electricit_date"];
                            RecordBySerial.Rows[i]["previous_energy_billing"] = CheckinInfo.Rows[0]["check_in_electricitymeter"];

                            // Clone to Temp
                            RecordBySerial.Rows[i]["previous_energy_billingTemp"] = RecordBySerial.Rows[i]["previous_energy_billing"];
                            RecordBySerial.Rows[i]["previous_date_billingTemp"] = RecordBySerial.Rows[i]["previous_date_billing"];
                        }
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["flag_type_previous"] = "frombilling";
                    }

                    if (RecordBySerial.Rows[i]["meter_status"].To<int>() == 0)
                    {
                        RecordBySerial.Rows[i]["E_CommStatus"] = "FAIL";
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["E_CommStatus"] = "PASS";
                    }

                    if (RecordBySerial.Rows[i]["meter_cut"].ToString() == "False")
                    {
                        RecordBySerial.Rows[i]["meter_cut_text"] = "Open";
                    }
                    else
                    {
                        RecordBySerial.Rows[i]["meter_cut_text"] = "Closed";
                    }

                    Double Begin_RangeValue = 90000;
                    Double End_RangeValue = 10000;
                    Double DoubleTotalUnit = 0;

                    if (RecordBySerial.Rows[i]["previous_energy_billing"].To<double>() > RecordBySerial.Rows[i]["end_energy"].To<double>())
                    {

                        if (RecordBySerial.Rows[i]["previous_energy_billing"].To<double>() > Begin_RangeValue && RecordBySerial.Rows[i]["end_energy"].To<double>() < End_RangeValue)
                        {
                            // Rollback case
                            // ( Roolback + End ) - Begin
                            DoubleTotalUnit = (RoolBackValue + RecordBySerial.Rows[i]["end_energy"].To<double>()) - RecordBySerial.Rows[i]["previous_energy_billing"].To<double>();
                        }
                        else
                        {
                            // AbNormal value
                            // End Value - Begin Value
                            DoubleTotalUnit = RecordBySerial.Rows[i]["end_energy"].To<double>() - RecordBySerial.Rows[i]["previous_energy_billing"].To<double>();
                        }
                    }
                    else
                    {
                        DoubleTotalUnit = RecordBySerial.Rows[i]["end_energy"].To<double>() - RecordBySerial.Rows[i]["previous_energy_billing"].To<double>();
                    }

                    RecordBySerial.Rows[i]["total_unit"] = DoubleTotalUnit.ToString("N2");

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


                gridControlMeterInRoom.DataSource = RecordBySerial;
                #endregion
              
                #region Meter Utility
                DataTable RecordBySerialUtility = BusinessLogicBridge.DataStore.ReadRecordingUtilityByDate(datetime, lookUpEditBuilding.EditValue.To<int>());

                DataTable CheckinInfoUtility = new DataTable("CheckinInfoUtility");

                RecordBySerialUtility.Columns.Add("total_unit", typeof(string));
                RecordBySerialUtility.Columns.Add("flag_type_previous", typeof(string));
                RecordBySerialUtility.Columns.Add("previous_energy_billingTemp");
                RecordBySerialUtility.Columns.Add("previous_date_billingTemp");
                RecordBySerialUtility.Columns.Add("present_energy_valueTemp");
                RecordBySerialUtility.Columns.Add("present_date_updateTemp");
                RecordBySerialUtility.Columns.Add("E_CommStatus");
                RecordBySerialUtility.Columns.Add("meter_cut_text");

                for (int i = 0; i < RecordBySerialUtility.Rows.Count; i++)
                {
                    if (RecordBySerialUtility.Rows[i]["previous_date_billing"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["previous_date_billing"] = DateTime.Now;
                    }

                    if (RecordBySerialUtility.Rows[i]["present_date_update"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["present_date_update"] = DateTime.Now;
                    }

                    if (RecordBySerialUtility.Rows[i]["present_energy_value"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["present_energy_value"] = 0;
                    }

                    if (RecordBySerialUtility.Rows[i]["end_date"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["end_date"] = DateTime.Now;
                    }

                    if (RecordBySerialUtility.Rows[i]["end_energy"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["end_energy"] = 0;
                    }

                    if (RecordBySerialUtility.Rows[i]["meter_status"].To<int>() == 0)
                    {
                        RecordBySerialUtility.Rows[i]["E_CommStatus"] = "FAIL";
                    }
                    else
                    {
                        RecordBySerialUtility.Rows[i]["E_CommStatus"] = "PASS";
                    }

                    Double Begin_RangeValue = 90000;
                    Double End_RangeValue = 10000;
                    Double DoubleTotalUnit = 0;

                    if (RecordBySerialUtility.Rows[i]["previous_energy_billing"].To<double>() > RecordBySerialUtility.Rows[i]["end_energy"].To<double>())
                    {

                        if (RecordBySerialUtility.Rows[i]["previous_energy_billing"].To<double>() > Begin_RangeValue && RecordBySerialUtility.Rows[i]["end_energy"].To<double>() < End_RangeValue)
                        {
                            // Rollback case
                            // ( Roolback + End ) - Begin
                            DoubleTotalUnit = (RoolBackValue + RecordBySerialUtility.Rows[i]["end_energy"].To<double>()) - RecordBySerialUtility.Rows[i]["previous_energy_billing"].To<double>();
                        }
                        else
                        {
                            // AbNormal value
                            // End Value - Begin Value
                            DoubleTotalUnit = RecordBySerialUtility.Rows[i]["end_energy"].To<double>() - RecordBySerialUtility.Rows[i]["previous_energy_billing"].To<double>();
                        }
                    }
                    else
                    {
                        DoubleTotalUnit = RecordBySerialUtility.Rows[i]["end_energy"].To<double>() - RecordBySerialUtility.Rows[i]["previous_energy_billing"].To<double>();
                    }

                    RecordBySerialUtility.Rows[i]["total_unit"] = DoubleTotalUnit.ToString("N2");

                    // Read Date 
                    if (RecordBySerialUtility.Rows[i]["meter_read_date"].ToString() == "")
                    {
                        RecordBySerialUtility.Rows[i]["meter_read_date"] = DateTime.Now;
                        RecordBySerialUtility.Rows[i]["E_CommStatus"] = "";
                    }
                }

                gridControlMeterUtility.DataSource = RecordBySerialUtility;

                #endregion

                firsttime = true;
                
                if (date_record_event == false) setDisable();
            }
            catch(Exception ex) { }
        }

        private void setEnable()
        {
            #region gridViewMeterInRoom
            // Copy end to begin & lastest to end
            gridViewMeterInRoom.Columns[5].OptionsColumn.AllowEdit = true;
            gridViewMeterInRoom.Columns[9].OptionsColumn.AllowEdit = true;

            // Meter In Room
            gridViewMeterInRoom.Columns[3].OptionsColumn.AllowEdit = true;
            gridViewMeterInRoom.Columns[4].OptionsColumn.AllowEdit = true;

            gridViewMeterInRoom.Columns[6].OptionsColumn.AllowEdit = true;
            gridViewMeterInRoom.Columns[7].OptionsColumn.AllowEdit = true;

            gridViewMeterInRoom.Columns[10].OptionsColumn.AllowEdit = true;
            gridViewMeterInRoom.Columns[11].OptionsColumn.AllowEdit = true;
            gridViewMeterInRoom.Columns[12].OptionsColumn.AllowEdit = true;

            // Meter Utility
            // Copy end to begin & lastest to end
            gridViewMeterUtility.Columns[5].OptionsColumn.AllowEdit = true;
            gridViewMeterUtility.Columns[9].OptionsColumn.AllowEdit = true;

            gridViewMeterUtility.Columns[3].OptionsColumn.AllowEdit = true;
            gridViewMeterUtility.Columns[4].OptionsColumn.AllowEdit = true;

            gridViewMeterUtility.Columns[6].OptionsColumn.AllowEdit = true;
            gridViewMeterUtility.Columns[7].OptionsColumn.AllowEdit = true;

            gridViewMeterUtility.Columns[10].OptionsColumn.AllowEdit = true;
            gridViewMeterUtility.Columns[11].OptionsColumn.AllowEdit = true;
            gridViewMeterUtility.Columns[12].OptionsColumn.AllowEdit = true;

            #endregion

            panelTopControl.Enabled = true;

            dateEditRecord.Enabled = true;

            bttSave.Enabled = true;
            bttCancel.Enabled = true;

            bttEdit.Enabled = false;

        }

        private void setDisable()
        {
            lookUpEditBuilding.Enabled = true;
            // Copy end to begin & lastest to end
            gridViewMeterInRoom.Columns[5].OptionsColumn.AllowEdit = false;
            gridViewMeterInRoom.Columns[9].OptionsColumn.AllowEdit = false;

            // Meter In Room
            gridViewMeterInRoom.Columns[3].OptionsColumn.AllowEdit = false;
            gridViewMeterInRoom.Columns[4].OptionsColumn.AllowEdit = false;

            gridViewMeterInRoom.Columns[6].OptionsColumn.AllowEdit = false;
            gridViewMeterInRoom.Columns[7].OptionsColumn.AllowEdit = false;

            gridViewMeterInRoom.Columns[10].OptionsColumn.AllowEdit = false;
            gridViewMeterInRoom.Columns[11].OptionsColumn.AllowEdit = false;
            gridViewMeterInRoom.Columns[12].OptionsColumn.AllowEdit = false;

            // Meter Utility
            gridViewMeterUtility.Columns[5].OptionsColumn.AllowEdit = false;
            gridViewMeterUtility.Columns[9].OptionsColumn.AllowEdit = false;

            gridViewMeterUtility.Columns[3].OptionsColumn.AllowEdit = false;
            gridViewMeterUtility.Columns[4].OptionsColumn.AllowEdit = false;

            gridViewMeterUtility.Columns[6].OptionsColumn.AllowEdit = false;
            gridViewMeterUtility.Columns[7].OptionsColumn.AllowEdit = false;

            gridViewMeterUtility.Columns[10].OptionsColumn.AllowEdit = false;
            gridViewMeterUtility.Columns[11].OptionsColumn.AllowEdit = false;
            gridViewMeterUtility.Columns[12].OptionsColumn.AllowEdit = false;


            panelTopControl.Enabled = false;

            dateEditRecord.Enabled = false;

            
            bttSave.Enabled = false;
            bttCancel.Enabled = false;

            bttEdit.Enabled = true;

        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEnable();
            //
            lookUpEditBuilding.Enabled = false;
            //
            rowUpdated.Clear();
            rowUpdated2.Clear();

            date_record_event = false;

        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            DataTable AllRows = ((DataTable)gridControlMeterInRoom.DataSource);
            DataTable AllRows2 = ((DataTable)gridControlMeterUtility.DataSource);

            bool haveLessThanZero = false;
            
            for (int i = 0; i < AllRows.Rows.Count; i++) {

                if (AllRows.Rows[i]["total_unit"].To<double>() < 0)
                {
                    haveLessThanZero = true;
                }
            }
            
            for (int i = 0; i < AllRows2.Rows.Count; i++)
            {

                if (AllRows2.Rows[i]["total_unit"].To<double>() < 0)
                {
                    haveLessThanZero = true;
                }
            }


            if (haveLessThanZero == false)
            {


                if (rowUpdated.Count != 0 || rowUpdated2.Count != 0)
                {
                    DataRow UpdateRow;
                    DataRow UpdateRow2;

                    for (int i = 0; i < rowUpdated.Count; i++)
                    {

                        UpdateRow = gridViewMeterInRoom.GetDataRow(rowUpdated[i]);
                        // Update Electric meter
                        BusinessLogicBridge.DataStore.updateE_MeterBeginAndEndAll(UpdateRow);
                    }

                    for (int i = 0; i < rowUpdated2.Count; i++)
                    {

                        UpdateRow2 = gridViewMeterUtility.GetDataRow(rowUpdated2[i]);
                        // Update Electric meter
                        BusinessLogicBridge.DataStore.updateE_MeterBeginAndEndAll(UpdateRow2);
                    }

                    setDisable();
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
                }

            }else {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1080"), getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }



            //bool haveLessThanZero = false;
            //DataTable AllRows = ((DataTable)gridControlMeterInRoom.DataSource);

            //for (int i = 0; i < AllRows.Rows.Count; i++) {

            //    if (AllRows.Rows[i]["total_unit"].To<double>() < 0)
            //    {
            //        haveLessThanZero = true;
            //    }
            //}
            //DataTable AllRows2 = ((DataTable)gridControlMeterUtility.DataSource);

            //for (int i = 0; i < AllRows2.Rows.Count; i++)
            //{

            //    if (AllRows2.Rows[i]["total_unit"].To<double>() < 0)
            //    {
            //        haveLessThanZero = true;
            //    }
            //}


            //if (haveLessThanZero == false)
            //{

            //    int meternotnull = BusinessLogicBridge.DataStore.CheckEMeterAllNull();

            //    if (meternotnull > 0)
            //    {

            //        if (rowUpdated.Count != 0 || rowUpdated2.Count != 0)
            //        {

            //            DataRow UpdateRow;
            //            DataRow UpdateRow2;

            //            for (int i = 0; i < rowUpdated.Count; i++)
            //            {

            //                UpdateRow = gridViewMeterInRoom.GetDataRow(rowUpdated[i]);
            //                // Insert Transaction Electric meter
            //                BusinessLogicBridge.DataStore.insertE_Record(UpdateRow);
            //            }

            //            for (int i = 0; i < rowUpdated2.Count; i++)
            //            {

            //                UpdateRow2 = gridViewMeterUtility.GetDataRow(rowUpdated2[i]);
            //                // Insert Transaction Electric meter
            //                BusinessLogicBridge.DataStore.insertE_Record(UpdateRow2);
            //            }

            //            setDisable();
            //            utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
            //        }

            //    }
            //    else { 
            //        // First Time

            //        BusinessLogicBridge.DataStore.insertFisrtTimeE_Record(AllRows);

            //        setDisable();
            //        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");

            //    }
            //}else {
            //    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1080"), getLanguage("_softwarename"));
            //    TrySaveError = true;
            //    return;
            //}


        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4007"), getLanguage("_softwarename")) == DialogResult.Yes) {
                LoadDefaultGridInRoom();
                setDisable();
            }
        }

        private void gridViewMeterInRoom_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            //if (e.RowHandle < 0)
            //    return;

            //if (!e.Column.FieldName.Equals("present_energy_value"))
            //    return;


            //if (e.Column.FieldName == "present_energy_value")
            //{
            //    if (gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_id") != null)
            //    {
            //        string meterSerial = gridViewMeterInRoom.GetRowCellValue(gridViewMeterInRoom.FocusedRowHandle, "meter_serial").ToString();

            //        DataTable DTLook = new DataTable();

            //        DTLook.Columns.Add("DateLastest", typeof(DateTime));
            //        DTLook.Columns.Add("TotalUnit", typeof(double));

            //        DataTable LastestDT = new DataTable();

            //        LastestDT = BusinessLogicBridge.DataStore.getLastestDropDown(meterSerial);
            //        repositoryItemComboBoxLastestDate1.Items.Clear();
            //        for (int j = 0; j < LastestDT.Rows.Count; j++)
            //        {

            //            DTLook.Rows.Add(LastestDT.Rows[j]["DateLastest"].ToString(), LastestDT.Rows[j]["TotalUnit"].To<double>());

            //            repositoryItemComboBoxLastestDate1.Items.Add(LastestDT.Rows[j]["DateLastest"].ToString() + " " + LastestDT.Rows[j]["TotalUnit"].ToString());

            //        }

            //        e.RepositoryItem = repositoryItemComboBoxLastestDate1;

            //        //string[] formatStringCheck = gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "present_energy_value").ToString().Split(' ');

            //        //if (formatStringCheck.Length > 1)
            //        //{
            //        //    // Yes
            //        //    // date and value
            //        //    gridViewMeterInRoom.SetRowCellValue(e.RowHandle, "present_date_update", formatStringCheck[0].ToString());
            //        //    gridViewMeterInRoom.SetRowCellValue(e.RowHandle, "present_energy_value", formatStringCheck[1].To<double>());
            //        //}
            //    }

            //}
            
        }

        private void gridViewMeterUtility_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("present_energy_value"))
                return;

            if (e.Column.FieldName == "present_energy_value")
            {

                if (gridViewMeterUtility.FocusedRowHandle.To<int>() >= 0)
                {
                    string meterSerial = gridViewMeterUtility.GetRowCellValue(gridViewMeterUtility.FocusedRowHandle, "meter_serial").ToString();

                    DataTable DTLook = new DataTable();

                    DTLook.Columns.Add("DateLastest", typeof(DateTime));
                    DTLook.Columns.Add("TotalUnit", typeof(double));

                    DataTable LastestDT = new DataTable();

                    LastestDT = BusinessLogicBridge.DataStore.getLastestDropDown(meterSerial);
                    repositoryItemComboBoxLastestDate2.Items.Clear();
                    for (int j = 0; j < LastestDT.Rows.Count; j++)
                    {

                        DTLook.Rows.Add(LastestDT.Rows[j]["DateLastest"], LastestDT.Rows[j]["TotalUnit"].To<double>());

                        repositoryItemComboBoxLastestDate2.Items.Add(LastestDT.Rows[j]["DateLastest"] + " " + LastestDT.Rows[j]["TotalUnit"].ToString());

                    }

                    e.RepositoryItem = repositoryItemComboBoxLastestDate2;

                    string[] formatStringCheck = gridViewMeterUtility.GetRowCellValue(e.RowHandle, "present_energy_value").ToString().Split(' ');

                    if (formatStringCheck.Length > 1)
                    {
                        // Yes
                        // date and value
                        gridViewMeterUtility.SetRowCellValue(e.RowHandle, "present_date_update", formatStringCheck[0].ToString());
                        gridViewMeterUtility.SetRowCellValue(e.RowHandle, "present_energy_value", formatStringCheck[1].To<double>());
                    }
                    else
                    {
                        // No
                        // 50.00
                        gridViewMeterUtility.SetRowCellValue(e.RowHandle, "present_date_update", DateTime.Now.ToString());
                        gridViewMeterUtility.SetRowCellValue(e.RowHandle, "present_energy_value", formatStringCheck[0].To<double>());

                    }
                }
            }
        }

        private void bttLastestToEndAll_Click(object sender, EventArgs e)
        {
            Double LasttestEnergy;
            DateTime LasttestDate;
            Double EndEnergy;
            Double BeginEnergy;
            Double SummaryTotal = 0;

            Double Begin_RangeValue = 90000;
            Double End_RangeValue = 10000;

            for (int i = 0; i < gridViewMeterInRoom.RowCount; i++)
            {
                LasttestEnergy = gridViewMeterInRoom.GetRowCellValue(i, "present_energy_value").To<double>();
                LasttestDate = Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(i, "present_date_update"));

                gridViewMeterInRoom.SetRowCellValue(i, "end_energy", LasttestEnergy);
                gridViewMeterInRoom.SetRowCellValue(i, "end_date", LasttestDate);

                EndEnergy = gridViewMeterInRoom.GetRowCellValue(i, "end_energy").To<double>();
                BeginEnergy = gridViewMeterInRoom.GetRowCellValue(i, "previous_energy_billing").To<double>();

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

                gridViewMeterInRoom.SetRowCellValue(i, "total_unit", SummaryTotal.ToString("N2"));
            }

            for (int i = 0; i < gridViewMeterUtility.RowCount; i++)
            {

                LasttestEnergy = gridViewMeterUtility.GetRowCellValue(i, "present_energy_value").To<double>();
                LasttestDate = Convert.ToDateTime(gridViewMeterUtility.GetRowCellValue(i, "present_date_update"));

                gridViewMeterUtility.SetRowCellValue(i, "end_energy", LasttestEnergy);
                gridViewMeterUtility.SetRowCellValue(i, "end_date", LasttestDate);

                EndEnergy = gridViewMeterUtility.GetRowCellValue(i, "end_energy").To<double>();
                BeginEnergy = gridViewMeterUtility.GetRowCellValue(i, "previous_energy_billing").To<double>();

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

                gridViewMeterUtility.SetRowCellValue(i, "total_unit", SummaryTotal.ToString("N2"));
            }
        }

        private void bttEndToBeginAll_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {

                Double EndEnergy;
                DateTime EndDate;
                for (int i = 0; i < gridViewMeterInRoom.RowCount; i++)
                {
                    EndEnergy = gridViewMeterInRoom.GetRowCellValue(i, "end_energy").To<double>();
                    EndDate = Convert.ToDateTime(gridViewMeterInRoom.GetRowCellValue(i, "end_date"));

                    gridViewMeterInRoom.SetRowCellValue(i, "previous_energy_billing", EndEnergy);
                    gridViewMeterInRoom.SetRowCellValue(i, "previous_date_billing", EndDate);
                    gridViewMeterInRoom.SetRowCellValue(i, "total_unit", 0.00);
                }

                for (int i = 0; i < gridViewMeterUtility.RowCount; i++)
                {
                    EndEnergy = gridViewMeterUtility.GetRowCellValue(i, "end_energy").To<double>();
                    EndDate = Convert.ToDateTime(gridViewMeterUtility.GetRowCellValue(i, "end_date"));

                    gridViewMeterUtility.SetRowCellValue(i, "previous_energy_billing", EndEnergy);
                    gridViewMeterUtility.SetRowCellValue(i, "previous_date_billing", EndDate);
                    gridViewMeterUtility.SetRowCellValue(i, "total_unit", 0.00);
                }

            }
        }

        private void bttSetBeginFromInvoice_Click(object sender, EventArgs e)
        {
            int room_id = 0;
            DataTable invoiceDT = new DataTable();
            for (int i = 0; i < gridViewMeterInRoom.RowCount; i++)
            {
                room_id = gridViewMeterInRoom.GetRowCellValue(i, "room_id").To<int>();
                if (room_id != 0)
                {
                    invoiceDT = BusinessLogicBridge.DataStore.getBeginMeterTransFromInvoiceByRoomID(room_id);
                    if (invoiceDT.Rows.Count > 0)
                    {
                            // Set Data as invoice transaction
                            gridViewMeterInRoom.SetRowCellValue(i, "previous_energy_billing", invoiceDT.Rows[0]["inv_trans_emeter_present_energy"]);
                            gridViewMeterInRoom.SetRowCellValue(i, "previous_date_billing", invoiceDT.Rows[0]["inv_trans_emeter_present_date"]);
                    }
                    else
                    {
                        // Set Cell is 0
                        gridViewMeterInRoom.SetRowCellValue(i, "previous_energy_billing", 0);
                        gridViewMeterInRoom.SetRowCellValue(i, "previous_date_billing", DateTime.Today);
                    }
                }
            }
        }

        private void bttReadAll_Click(object sender, EventArgs e)
        {
            DataTable AllRow = ((DataTable)gridControlMeterInRoom.DataSource);
            DataTable AllRowUtility = ((DataTable)gridControlMeterUtility.DataSource);

            #region Meter in room

            DataRow currentRow;
            DataTable ERecord = new DataTable();
            adcHelper EmeterDT = new adcHelper();
            string connection_text = "";
            bool adc_connected = true;

            int result = DateTime.Compare(dateEditRecord.EditValue.To<DateTime>().Date,DateTime.Now.Date);

            // pd date == dateEditRecord.EditValue
            if (result == 0)
            {
                ERecord = BusinessLogicBridge.DataStore.ReadEmeterPresentAll(lookUpEditBuilding.EditValue.To<int>());

                int ErowCount = ERecord.Rows.Count;

                for (int k = 0; k < ErowCount; k++)
                {
                    if (ErowCount > 0 && ERecord.Rows[k]["present_date_update"].ToString() != "")
                    {

                        if (ERecord.Rows[k]["meter_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", ERecord.Rows[k]["present_date_update"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", ERecord.Rows[k]["present_energy_value"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", ERecord.Rows[k]["present_date_update"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", ERecord.Rows[k]["present_energy_value"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", DateTime.Today);
                        gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", 0.00);
                        gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterInRoom.SetRowCellValue(k, "E_CommStatus", connection_text);
                
                }
            }
            else {

                for (int k = 0; k < AllRow.Rows.Count; k++)
                {
                    currentRow = gridViewMeterInRoom.GetDataRow(k);

                    ERecord = BusinessLogicBridge.DataStore.ReadRecordingByMeterAndDate(currentRow["meter_id"].To<int>(), dateEditRecord.EditValue.To<DateTime>());

                    if (ERecord.Rows.Count > 0 && ERecord.Rows[0]["DateLastest"].ToString() != "")
                    {

                        if (ERecord.Rows[0]["e_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", ERecord.Rows[0]["DateLastest"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", ERecord.Rows[0]["TotalUnit"]);
                            gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_text = "Pass";
                        }

                    }
                    else
                    {
                        connection_text = "";
                        // failed
                        gridViewMeterInRoom.SetRowCellValue(k, "present_date_update", dateEditRecord.EditValue.To<DateTime>());
                        gridViewMeterInRoom.SetRowCellValue(k, "present_energy_value", 0.00);
                        gridViewMeterInRoom.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_text = "Fail";
                    }

                    gridViewMeterInRoom.SetRowCellValue(k, "E_CommStatus", connection_text);
                }
            }

            #endregion

            #region Meter Utility
            DataRow currentRowUtility;
            DataTable ERecordUtility = new DataTable();
            adcHelper EmeterDTUtility = new adcHelper();
            string connection_textUtility = "";


            int resultUtility = DateTime.Compare(dateEditRecord.EditValue.To<DateTime>().Date, DateTime.Now.Date);

            if (resultUtility == 0)
            {
                ERecordUtility = BusinessLogicBridge.DataStore.ReadEmeterUtilityPresentAll(lookUpEditBuilding.EditValue.To<int>());

                int ErowCount = ERecordUtility.Rows.Count;

                for (int k = 0; k < ErowCount; k++)
                {
                    if (ErowCount > 0 && ERecordUtility.Rows[k]["present_date_update"].ToString() != "")
                    {
                        if (ERecordUtility.Rows[k]["meter_status"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterUtility.SetRowCellValue(k, "present_date_update", ERecordUtility.Rows[k]["present_date_update"]);
                            gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", ERecordUtility.Rows[k]["present_energy_value"]);
                            gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_textUtility = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterUtility.SetRowCellValue(k, "present_date_update", ERecordUtility.Rows[k]["present_date_update"]);
                            gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", ERecordUtility.Rows[k]["present_energy_value"]);
                            gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_textUtility = "Pass";
                        }

                    }
                    else
                    {
                        connection_textUtility = "";
                        // failed
                        gridViewMeterUtility.SetRowCellValue(k, "present_date_update", DateTime.Today);
                        gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", 0.00);
                        gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_textUtility = "Fail";
                    }

                    gridViewMeterUtility.SetRowCellValue(k, "E_CommStatus", connection_textUtility);
                }
            }
            else
            {
                for (int k = 0; k < AllRowUtility.Rows.Count; k++)
                {
                    currentRowUtility = gridViewMeterUtility.GetDataRow(k);

                    ERecordUtility = BusinessLogicBridge.DataStore.ReadRecordingByMeterAndDate(currentRowUtility["meter_id"].To<int>(), dateEditRecord.EditValue.To<DateTime>());

                    if (ERecordUtility.Rows.Count > 0 && ERecordUtility.Rows[0]["DateLastest"].ToString() != "")
                    {
                        if (ERecordUtility.Rows[0]["e_connection"].To<int>() == 0)
                        {
                            // failed
                            gridViewMeterUtility.SetRowCellValue(k, "present_date_update", ERecordUtility.Rows[0]["DateLastest"]);
                            gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", ERecordUtility.Rows[0]["TotalUnit"]);
                            gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_textUtility = "Fail";
                        }
                        else
                        {
                            // pass
                            gridViewMeterUtility.SetRowCellValue(k, "present_date_update", ERecordUtility.Rows[0]["DateLastest"]);
                            gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", ERecordUtility.Rows[0]["TotalUnit"]);
                            gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                            connection_textUtility = "Pass";
                        }

                    }
                    else
                    {
                        connection_textUtility = "";
                        // failed
                        gridViewMeterUtility.SetRowCellValue(k, "present_date_update", dateEditRecord.EditValue.To<DateTime>());
                        gridViewMeterUtility.SetRowCellValue(k, "present_energy_value", 0.00);
                        gridViewMeterUtility.SetRowCellValue(k, "meter_read_date", DateTime.Today);
                        connection_textUtility = "Fail";
                    }

                    gridViewMeterUtility.SetRowCellValue(k, "E_CommStatus", connection_textUtility);
                }
            }
            #endregion
        }

        private void gridViewMeterInRoom_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;


            if (!e.Column.FieldName.Equals("total_unit"))
                return;

            if (e.Column.FieldName == "total_unit")
            {
                if (gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "total_unit").ToString() != "")
                {

                    if (Convert.ToDouble(gridViewMeterInRoom.GetRowCellValue(e.RowHandle, "total_unit")) < 0)
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

        private void gridViewMeterUtility_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("total_unit"))
                return;

            if (e.Column.FieldName == "total_unit")
            {
                if (gridViewMeterUtility.GetRowCellValue(e.RowHandle, "total_unit").ToString() != "")
                {
                    if (Convert.ToDouble(gridViewMeterUtility.GetRowCellValue(e.RowHandle, "total_unit")) < 0)
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

    }
}
