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
using DevExpress.Utils;

namespace DXWindowsApplication2.UserForms
{
    public partial class Recording_PMeter : uBase
    {
        List<int> rowUpdated = new List<int>();

        public Recording_PMeter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(Recording_PMeter_Load);
            SaveClick += new EventHandler(bttSave_Click);
            repositoryItemButtonEditReading.ButtonClick += new ButtonPressedEventHandler(repositoryItemButtonEditReading_ButtonClick);

            gridViewPhone.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewMeterInRoom_ValidateRow);
            gridViewPhone.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gridViewMeterInRoom_InvalidRowException);


            lookUpEditBuilding.EditValueChanged += new EventHandler(lookUpEditBuilding_EditValueChanged);

            repositoryItemTimeEditBegin.EditValueChanged += new EventHandler(repositoryItemTimeEditBegin_EditValueChanged);
            repositoryItemTimeEditEnd.EditValueChanged += new EventHandler(repositoryItemTimeEditEnd_EditValueChanged);

            repositoryItemButtonEditCopyEndToBegin.Click += new EventHandler(repositoryItemButtonEditCopyEndToBegin_Click);

            gridViewPhone.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridViewPhone_RowUpdated);
            gridViewPhone.CustomRowCellEdit += new CustomRowCellEditEventHandler(gridViewPhone_CustomRowCellEdit);
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
        

        void gridViewPhone_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("amount_text"))
                return;

            if (e.Column.FieldName == "amount_text")
            {
              
                if (gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").To<double>() > 0)
                {
                    gridViewPhone.SetRowCellValue(e.RowHandle, "amount_text", gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").To<double>().ToString("N2"));
                    gridViewPhone.SetRowCellValue(e.RowHandle, "amount", gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").To<double>().ToString("N2"));
                }
                else
                {
                    if (gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").ToString() == "")
                    {
                        gridViewPhone.SetRowCellValue(e.RowHandle, "amount", (0.00).To<double>().ToString("N2"));
                        gridViewPhone.SetRowCellValue(e.RowHandle, "amount_text", "");
                    }
                    else
                    {
                        gridViewPhone.SetRowCellValue(e.RowHandle, "amount", (0.00).To<double>().ToString("N2"));
                        gridViewPhone.SetRowCellValue(e.RowHandle, "amount_text", (0.00).To<double>().ToString("N2"));
                    }
                }
            }
        }

        void Recording_PMeter_Load(object sender, EventArgs e)
        {
            initDropDownBuilding(); // First Load Alway
            initGroupDateDropDown();
            LoadDefaultGridInRoom();
            

            
            setThisLang();
        }

        void gridViewPhone_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (rowUpdated.Exists(i => i == e.RowHandle) == false)
            {
                rowUpdated.Add(e.RowHandle);
            }
        }

        void repositoryItemButtonEditCopyEndToBegin_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                DateTime EndDate = Convert.ToDateTime(gridViewPhone.GetRowCellValue(gridViewPhone.FocusedRowHandle, "end_date"));

                gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "start_date", EndDate);
                gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "start_time", gridViewPhone.GetRowCellValue(gridViewPhone.FocusedRowHandle, "end_time"));

                gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", "");
                gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", 0);
            }

        }

        void repositoryItemTimeEditEnd_EditValueChanged(object sender, EventArgs e)
        {
           // var x = e.ToString(); //((DateTime)e.NewValue).ToString("HH:mm:ss");

            try
            {
                var yy = ((TimeEdit)sender);
                yy.EditValue = Convert.ToDateTime(yy.EditValue.ToString()).TimeOfDay;
            }
            catch (Exception ex) { 
                
            }

        }

        void repositoryItemTimeEditBegin_EditValueChanged(object sender, EventArgs e)
        {
           // var x = e.ToString(); //((DateTime)e.NewValue).ToString("HH:mm:ss");
            try
            {
                var yy = ((TimeEdit)sender);
                yy.EditValue = Convert.ToDateTime(yy.EditValue.ToString()).TimeOfDay;
            }
            catch (Exception ex)
            {

            }
        }

        void lookUpEditBuilding_EditValueChanged(object sender, EventArgs e)
        {
            //int having = BusinessLogicBridge.DataStore.CheckPhoneMappingByBuildingID(lookUpEditBuilding.EditValue.To<int>());
            //if (having > 0)
            //{
                LoadDefaultGridInRoom();
            //}	
        }

        void repositoryItemButtonEditReading_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            bool status_have_adc = true;

            // Read Button
            DataRow currentRow = gridViewPhone.GetDataRow(gridViewPhone.FocusedRowHandle);
            
            string startDate = ((DateTime)currentRow["start_date"]).ToString("dd/MM/yyyy");
            string startTime = currentRow["start_time"].ToString().Substring(0, 8);

            string endDate = ((DateTime)currentRow["end_date"]).ToString("dd/MM/yyyy");
            string endTime = currentRow["end_time"].ToString().Substring(0, 8);


            DateTime startDateTime = ConvertStringToDate(startDate + " " + startTime, "dd/MM/yyyy HH:mm:ss");
            DateTime endDateTime = ConvertStringToDate(endDate + " " + endTime, "dd/MM/yyyy HH:mm:ss");

            string phoneNo = currentRow["phone_label"].ToString();

            double Amount_total = 0;

            // Check Data Existing in biiling database
            DataTable RecordHaveMore = BusinessLogicBridge.DataStore.CheckPhoneTransByDateTime(((DateTime)currentRow["end_date"]).ToString("yyyy-MM-dd"), endTime, phoneNo);

            // Having more end date ?
            if (RecordHaveMore.Rows.Count > 0)
            {   

                 // Read Data From Billing Database
                //DataTable RecordPhone = BusinessLogicBridge.DataStore.ReadPhoneTransByDateTime(((DateTime)currentRow["start_date"]).ToString("yyyy-MM-dd"), startTime, ((DateTime)currentRow["end_date"]).ToString("yyyy-MM-dd"), endTime, phoneNo);

                //if(RecordPhone.Rows.Count>0){

                for (int i = 0; i < RecordHaveMore.Rows.Count; i++)
                    {
                        Amount_total += RecordHaveMore.Rows[i]["Amount"].To<double>();
                    }

                    gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", Amount_total.ToString("N2"));
                    gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", Amount_total);

                //}else{
                //    gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", "");
                //    gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", 0.00);
                //}
            }
            //else{

            //    if (currentRow["device_adc_id"].ToString() == "" || currentRow["device_adc_id"].ToString() == "0")
            //    {
            //        status_have_adc = false;
            //    }

            //    bool Connected = false;

            //    try
            //    {
            //        Connected = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"]].TestADCConnection();
            //    }
            //    catch { }

            //    // Check From ADC

            //    if (Connected == true && status_have_adc==true)
            //    {
            //        DataTable PhoneRecord = DXWindowsApplication2.MainForm.dictADC["adc_id_" + currentRow["device_adc_id"]].GetPhoneTransactionDT(phoneNo, startDateTime, endDateTime);

            //        if (PhoneRecord.Rows.Count > 0)
            //        {
            //            adcHelper PhoneDT = new adcHelper();

            //            PhoneRecord = PhoneDT.ConvertPhoneTransaction(PhoneRecord, currentRow["device_adc_id"].To<int>());

            //            for (int j = 0; j < PhoneRecord.Rows.Count; j++)
            //            {   
            //                // Calculate data from adc realtime
            //                Amount_total += PhoneRecord.Rows[j]["Amount"].To<double>();
            //            }
            //            gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", Amount_total.ToString("N2"));
            //            gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", Amount_total);

            //        }
            //        else {
            //            gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", 0.00);
            //            gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", "0.00");
            //        }

            //    }
            //    else {
            //        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1083"), getLanguage("_softwarename"));

            //        gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount_text", "");
            //        gridViewPhone.SetRowCellValue(gridViewPhone.FocusedRowHandle, "amount", 0.00);
            //        return;

            //    }

            //}

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
        
        void gridViewMeterInRoom_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        void gridViewMeterInRoom_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {   
            GridView view = sender as GridView;

            // Begin Date
            GridColumn previous_date_billing = view.Columns[2];

            // Begin Time
            GridColumn previous_time_billing = view.Columns[3];
            

            // End Date
            GridColumn end_date = view.Columns[5];
            // End Time
            GridColumn end_time = view.Columns[6];


            // Total Unit Column
            GridColumn amount_text = view.Columns[7];
            GridColumn amount = view.Columns[9];

            //Get the date of the Begin Date
            DateTime DateTimePrevious_date = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, previous_date_billing).ToString());

            //Get the date of the Begin Date
            DateTime DateTimePrevious_time = ConvertStringToDate((DateTimePrevious_date.ToString("dd/MM/yyyy") +" "+ view.GetRowCellDisplayText(e.RowHandle, previous_time_billing).ToString()),"dd/MM/yyyy HH:mm:ss"); 


            //Get the date of the End date
            DateTime DateTimeEnd_date = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, end_date).ToString());
            //Get the date of the End date
            DateTime DateTimeEnd_time = ConvertStringToDate((DateTimePrevious_date.ToString("dd/MM/yyyy") + " " + view.GetRowCellDisplayText(e.RowHandle, end_time).ToString()), "dd/MM/yyyy HH:mm:ss");

            // Amount
            Double DoubleAmount = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, amount_text).ToString());

            view.SetRowCellValue(e.RowHandle, amount, DoubleAmount);

            string ErrorMSG = "";

            // Begin Date
            //if (DateTimePrevious_date.Date > DateTimeEnd_date.Date) {
            //    e.Valid = false;
            //    ErrorMSG += getLanguage("_msg_1069") + "\r\n";
            //}

            //if (DateTimePrevious_date.Date > DateTime.Today.Date)
            //{
            //    e.Valid = false;
            //    ErrorMSG += getLanguage("_msg_1055") + "\r\n";
            //}

            // ConCAT New Value of date & time
            var beginTime = ConvertStringToDate(DateTimePrevious_date.ToString("dd/MM/yyyy") + " " + DateTimePrevious_time.TimeOfDay, "dd/MM/yyyy HH:mm:ss");
            var endTime = ConvertStringToDate(DateTimeEnd_date.ToString("dd/MM/yyyy") + " " + DateTimeEnd_time.TimeOfDay, "dd/MM/yyyy HH:mm:ss");
            


            // Begin date Time > End Date Time
            if (beginTime > endTime)
            {
                e.Valid = false;
                ErrorMSG += getLanguage("_msg_1081") + "\r\n";
            }

            // Begin > Now()

            if (beginTime > DateTime.Now)
            {
                e.Valid = false;
                ErrorMSG += getLanguage("_msg_1068") + "\r\n";
            }


            int result = DateTime.Compare(endTime.Date, DateTime.Now.Date);

            // End Date > Now()
            if (result > 0)
            {
                e.Valid = false;
                ErrorMSG += getLanguage("_msg_1068") + "\r\n";
            }


            // End date < Begin date
            if (endTime < beginTime)
            {
                e.Valid = false;
                ErrorMSG += getLanguage("_msg_1082") + "\r\n";
            }

            if (ErrorMSG != "")
            {
                utilClass.showPopupMessegeBox(this, ErrorMSG, getLanguage("_softwarename"));
                return;
            }

        }

        void initDropDownBuilding()
        {
            DataTable BuildingTable = BusinessLogicBridge.DataStore.BasicInfoRoom_getBuilding();
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
            lookUpEditBuilding.Properties.NullText = getLanguage("_select_building");
            lookUpEditBuilding.Properties.DataSource = BuildingTable;
            if (BuildingTable.Rows.Count > 0)
            lookUpEditBuilding.EditValue = BuildingTable.Rows[0]["building_id"];
        }
                
        void initGroupDateDropDown() {

            dateEditRecord.EditValue = DateTime.Now;
            timeEditTime.EditValue = "00:00:00";

            //DataTable List_DateGroup = BusinessLogicBridge.DataStore.getGroupDateRecord();

            //if (List_DateGroup.Rows.Count == 0)
            //{
            //    List_DateGroup.Rows.Add(String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            //}

            //lookUpEditRecordDate.Properties.DisplayMember = "groupdate";
            //lookUpEditRecordDate.Properties.ValueMember = "groupdate";
            //lookUpEditRecordDate.Properties.DataSource = List_DateGroup;

            //lookUpEditRecordDate.ItemIndex = 0;

        }

        void setThisLang()
        {
            bttEdit.Text = getLanguage("_edit");
            bttSave.Text = getLanguage("_save");
            bttCancel.Text = getLanguage("_cancel");
        }       

        void LoadDefaultGridInRoom()
        {
            //
            lookUpEditBuilding.Enabled = true;
            //
            #region Meter in Room
            
            DataTable RecordBySerial = new DataTable();

            if(lookUpEditBuilding.EditValue!=null)
            RecordBySerial = BusinessLogicBridge.DataStore.ReadPhoneRecordingByDate(int.Parse(lookUpEditBuilding.EditValue.ToString()));

            RecordBySerial.Columns.Add("amount_text", typeof(string));
            
            for (int i = 0; i < RecordBySerial.Rows.Count; i++)
            {
                // Begin 
                if (RecordBySerial.Rows[i]["start_date"].ToString() == "")
                {
                    RecordBySerial.Rows[i]["start_date"] = DateTime.Now.Date;
                }

                if (RecordBySerial.Rows[i]["start_time"].ToString() == "")
                {
                    RecordBySerial.Rows[i]["start_time"] = DateTime.Now.TimeOfDay;
                }


                // End

                if (RecordBySerial.Rows[i]["end_date"].ToString() == "")
                {
                    RecordBySerial.Rows[i]["end_date"] = DateTime.Today.Date;
                }

                if (RecordBySerial.Rows[i]["end_time"].ToString() == "")
                {
                    RecordBySerial.Rows[i]["end_time"] = DateTime.Now.TimeOfDay;
                }

                if (RecordBySerial.Rows[i]["amount"].ToString() == "")
                {
                    RecordBySerial.Rows[i]["amount"] = 0.00;
                    RecordBySerial.Rows[i]["amount_text"] = "0.00";
                }
                else {
                    RecordBySerial.Rows[i]["amount_text"] = RecordBySerial.Rows[i]["amount"].To<double>().ToString("N2");
                }
            }

            gridControlPhone.DataSource = RecordBySerial;
            #endregion
            setDisable();
        }

        private void setEnable()
        {
            lookUpEditBuilding.Enabled = false;
            //
            groupBoxEndGroup.Enabled = true;

            panelControlRead.Enabled = true;

            // Grid Control
            gridViewPhone.Columns[2].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[3].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[4].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[5].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[6].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[7].OptionsColumn.AllowEdit = true;
            gridViewPhone.Columns[8].OptionsColumn.AllowEdit = true;

            bttSave.Enabled = true;
            bttCancel.Enabled = true;
            bttEdit.Enabled = false;
        }

        private void setDisable()
        {
            lookUpEditBuilding.Enabled = true;
            //
            groupBoxEndGroup.Enabled = false;

            panelControlRead.Enabled = false;

            // Grid Control
            gridViewPhone.Columns[2].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[3].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[4].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[5].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[6].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[7].OptionsColumn.AllowEdit = false;
            gridViewPhone.Columns[8].OptionsColumn.AllowEdit = false;

            bttSave.Enabled = false;
            bttCancel.Enabled = false;
            bttEdit.Enabled = true;

        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            setEnable();
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            bool haveLessThanZero = false;
            DataTable AllRows = ((DataTable)gridControlPhone.DataSource);

            double Num;
            bool isNum;

            for (int i = 0; i < AllRows.Rows.Count; i++)
            {
                isNum = double.TryParse(AllRows.Rows[i]["amount_text"].ToString(), out Num);

                if ((AllRows.Rows[i]["amount_text"].ToString() == "") || (AllRows.Rows[i]["amount"].To<double>() < 0) || (isNum==false))
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

                        UpdateRow = gridViewPhone.GetDataRow(rowUpdated[i]);
                        
                        // Update Transaction Phone
                        BusinessLogicBridge.DataStore.updateLastPhoneRecord(UpdateRow);
                    }
                }
                setDisable();
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3001"), getLanguage("_softwarename"), "info");
            }
            else {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_1080"), getLanguage("_softwarename"));
                TrySaveError = true;
                return;
            }
        }

        private void gridViewPhone_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!e.Column.FieldName.Equals("amount_text"))
                return;

            if (e.Column.FieldName == "amount_text")
            {   double Num;
                bool isNum = double.TryParse(gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").ToString(), out Num);

                if (gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").ToString() != "" && (isNum == true))
                {
                    if (gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").ToString() == "")
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                    else
                    {

                        if (gridViewPhone.GetRowCellValue(e.RowHandle, "amount_text").To<double>() >= 0)
                        {
                            if (Convert.ToDouble(gridViewPhone.GetRowCellValue(e.RowHandle, "amount")) < 0)
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
                        else
                        {
                            //e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Regular);
                            e.Appearance.BackColor = Color.Red;
                        }
                    }
                    
                }
                else {
                    e.Appearance.BackColor = Color.Red;
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

        private void bttSetEndDateAll_Click(object sender, EventArgs e)
        {
            DataTable AllDTRows = ((DataTable)gridControlPhone.DataSource);

            string startDate = ""; 
            string startTime = "";

            string selectEndDate = Convert.ToDateTime(dateEditRecord.EditValue.ToString()).ToString("dd/MM/yyyy");
            string selectEndTime = timeEditTime.Text;


            DateTime startDateTime = new DateTime();
            DateTime endDateTime = ConvertStringToDate(selectEndDate + " " + selectEndTime, "dd/MM/yyyy HH:mm:ss");

            for (int i = 0; i < AllDTRows.Rows.Count; i++)
            {

                startDate = ((DateTime)AllDTRows.Rows[i]["start_date"]).ToString("dd/MM/yyyy");
                startTime = AllDTRows.Rows[i]["start_time"].ToString().Substring(0, 8);

                startDateTime = ConvertStringToDate(startDate + " " + startTime, "dd/MM/yyyy HH:mm:ss");

                if (startDateTime > endDateTime)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1082"), getLanguage("_softwarename"));
                    return;
                }
                else {
                    gridViewPhone.SetRowCellValue(i, "end_date", endDateTime);
                    gridViewPhone.SetRowCellValue(i, "end_time", selectEndTime);
                }
            }
            AllDTRows.Dispose();
        }

        private void bttReadAll_Click(object sender, EventArgs e)
        {

            DataTable AllRows = ((DataTable)gridControlPhone.DataSource);

            DataRow currentRow;

            DataTable RecordHaveMore = new DataTable();

            DataTable RecordPhone = new DataTable();

            string startDate = "";
            string startTime = "";

            string endDate = "";
            string endTime = "";

            DateTime startDateTime = new DateTime();
            DateTime endDateTime = new DateTime();
            string phoneNo = "";

            DataTable PhoneRecord = new DataTable();
            adcHelper PhoneDT = new adcHelper();
            double Amount_total = 0;

            for (int k = 0; k < AllRows.Rows.Count; k++)
            {
                // Read Button
                currentRow = gridViewPhone.GetDataRow(k);

                startDate = ((DateTime)currentRow["start_date"]).ToString("dd/MM/yyyy");
                startTime = currentRow["start_time"].ToString().Substring(0, 8);

                endDate = ((DateTime)currentRow["end_date"]).ToString("dd/MM/yyyy");
                endTime = currentRow["end_time"].ToString().Substring(0, 8);

                startDateTime = ConvertStringToDate(startDate + " " + startTime, "dd/MM/yyyy HH:mm:ss");
                endDateTime = ConvertStringToDate(endDate + " " + endTime, "dd/MM/yyyy HH:mm:ss");

                phoneNo = currentRow["phone_label"].ToString();

                Amount_total = 0;

                // Check Data Existing in biiling database
                RecordHaveMore = BusinessLogicBridge.DataStore.CheckPhoneTransByDateTime(((DateTime)currentRow["end_date"]).ToString("yyyy-MM-dd"), endTime, phoneNo);

                for (int i = 0; i < RecordHaveMore.Rows.Count; i++)
                {
                    Amount_total += RecordHaveMore.Rows[i]["Amount"].To<double>();
                }

                gridViewPhone.SetRowCellValue(k, "amount_text", Amount_total.ToString("N2"));
                gridViewPhone.SetRowCellValue(k, "amount", Amount_total);                
            }

            //if (FlagsConnect == false)
            //{
            //    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1083"), getLanguage("_softwarename"));
            //    return;
            //}

        }

        private void bttCopyEndToBeginAll_Click(object sender, EventArgs e)
        {
            if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4032"), getLanguage("_softwarename")) == DialogResult.Yes)
            {
                DataTable AllRows = ((DataTable)gridControlPhone.DataSource);
                string EndDate = "";
                string EndTime = "";
                for (int k = 0; k < AllRows.Rows.Count; k++)
                {
                    EndDate = gridViewPhone.GetRowCellValue(k, "end_date").ToString();
                    EndTime = gridViewPhone.GetRowCellValue(k, "end_time").ToString();


                    gridViewPhone.SetRowCellValue(k, "start_date", EndDate);
                    gridViewPhone.SetRowCellValue(k, "start_time", EndTime);
                }
            }
        }
    }
}
