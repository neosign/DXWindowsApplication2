using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;
using System.Threading;
using System.Net;
using System.Linq;

namespace DXWindowsApplication2.UserForms
{
    public class uBase : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler SaveClick;
        public string current_lang = DXWindowsApplication2.MainForm.current_lang;

        public static int update_state = 0;
        //
        public string getLanguage(string x)
        {
            return DXWindowsApplication2.MainForm.getLanguage(x);
        }

        public bool TrySaveError = false;

        public void TrySave()
        {
            TrySaveError = false;
            if (SaveClick != null)
            {
                SaveClick(null, null);
            }
            else
            {
                return;
            }
        }

        public string getLanguageWithColon(string x)
        {
            return DXWindowsApplication2.MainForm.getLanguageWithColon(x);
        }

        public void logAction()
        {

        }

        public MainForm MForm
        {
            get
            {
                return Parent.TopLevelControl as MainForm;
            }
        }
    }

    public class IPInfo
    {
        public IPInfo(string macAddress, string ipAddress)
        {
            this.MacAddress = macAddress;
            this.IPAddress = ipAddress;
        }

        public string MacAddress { get; private set; }
        public string IPAddress { get; private set; }

        private string _HostName = string.Empty;
        public string HostName
        {
            get
            {
                if (string.IsNullOrEmpty(this._HostName))
                {
                    try
                    {
                        // Retrieve the "Host Name" for this IP Address. This is the "Name" of the machine.
                        this._HostName = Dns.GetHostEntry(this.IPAddress).HostName;
                    }
                    catch
                    {
                        this._HostName = string.Empty;
                    }
                }
                return this._HostName;
            }
        }


        #region "Static Methods"

        /// <summary>
        /// Retrieves the IPInfo for the machine on the local network with the specified MAC Address.
        /// </summary>
        /// <param name="macAddress">The MAC Address of the IPInfo to retrieve.</param>
        /// <returns></returns>
        public static IPInfo GetIPInfo(string macAddress)
        {
            var ipinfo = (from ip in IPInfo.GetIPInfo()
                          where ip.MacAddress.ToLowerInvariant() == macAddress.ToLowerInvariant()
                          select ip).FirstOrDefault();

            return ipinfo;
        }

        /// <summary>
        /// Retrieves the IPInfo for All machines on the local network.
        /// </summary>
        /// <returns></returns>
        public static List<IPInfo> GetIPInfo()
        {
            try
            {
                var list = new List<IPInfo>();

                foreach (var arp in GetARPResult().Split(new char[] { '\n', '\r' }))
                {
                    // Parse out all the MAC / IP Address combinations
                    if (!string.IsNullOrEmpty(arp))
                    {
                        var pieces = (from piece in arp.Split(new char[] { ' ', '\t' })
                                      where !string.IsNullOrEmpty(piece)
                                      select piece).ToArray();
                        if (pieces.Length == 3)
                        {
                            list.Add(new IPInfo(pieces[1], pieces[0]));
                        }
                    }
                }

                // Return list of IPInfo objects containing MAC / IP Address combinations
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("IPInfo: Error Parsing 'arp -a' results", ex);
            }
        }

        /// <summary>
        /// This runs the "arp" utility in Windows to retrieve all the MAC / IP Address entries.
        /// </summary>
        /// <returns></returns>
        private static string GetARPResult()
        {
            Process p = null;
            string output = string.Empty;

            try
            {
                p = Process.Start(new ProcessStartInfo("arp", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                output = p.StandardOutput.ReadToEnd();

                p.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }

            return output;
        }

        #endregion
    }

    public static class utilClass
    {
        //

        public static DialogResult showPopupLogin(IWin32Window host)
        {
            loginform sPopup = new loginform();
            //
            sPopup.ShowIcon = true;
            sPopup.ShowInTaskbar = true;
            sPopup.HelpButton = false;

            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;

            //
            DialogResult dl = sPopup.ShowDialog(host);
            //

            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopAddAbout(IWin32Window host)
        {
            PopupAbout sPopup = new PopupAbout();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;

            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //

            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupRegistration(IWin32Window host)
        {
            PopupRegistration sPopup = new PopupRegistration();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;

            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;

            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //

            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupRegistered(IWin32Window host)
        {
            PopupRegistered sPopup = new PopupRegistered();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;

            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;

            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //

            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupMessegeBox(IWin32Window host, string massege, string caption, string type)
        {
            DialogResult dl = DialogResult.None;

            if (type == "")
            {
                dl = XtraMessageBox.Show(massege, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);            //
            }
            if (type == "info")
            {
                dl = XtraMessageBox.Show(massege, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);            //
            }
            return dl;
        }

        public static DialogResult showPopupMessegeBox(IWin32Window host, string massege, string caption)
        {
            return showPopupMessegeBox(host, massege, caption, "");
        }

        public static DialogResult showPopupConfirmBox(IWin32Window host, string massege, string caption)
        {
            DialogResult dl = XtraMessageBox.Show(massege, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);            //

            return dl;
        }

        public static DataRow showPopUpTenant(IWin32Window host)
        {
            PopupSelectTenant sPopup = new PopupSelectTenant();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataRow drReturn = sPopup.drTenant;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DataTable showPopAddExpense(IWin32Window host, DataTable dtTemp)
        {
            PopUpAddtionalItem sPopup = new PopUpAddtionalItem();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;

            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.dtItemTemp = dtTemp;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataTable drReturn = sPopup.dtItemTemp;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DataTable showPopAddCheckOutExpense(IWin32Window host, DataTable dtTemp)
        {
            PopUpCheckOutItem sPopup = new PopUpCheckOutItem();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;

            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.dtItemTemp = dtTemp;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataTable drReturn = sPopup.dtItemTemp;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DataTable showPopAddRecieptExpense(IWin32Window host, DataTable dtTemp)
        {
            PopUpRecieptItem sPopup = new PopUpRecieptItem();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.dtItemTemp = dtTemp;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataTable drReturn = sPopup.dtItemTemp;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DataTable showPopAddInvoiceExpense(IWin32Window host, DataTable dtTemp)
        {
            PopUpInvoiceItem sPopup = new PopUpInvoiceItem();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.dtItemTemp = dtTemp;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataTable drReturn = sPopup.dtItemTemp;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DialogResult showPopAddTenant(IWin32Window host, ref DataTable dtTemp, int room_id)
        {
            PopupAddTenant sPopup = new PopupAddTenant();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.RoommateTableTemp = dtTemp;
            sPopup.room_id = room_id;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupRoomDetail(IWin32Window host, int room_id)
        {
            RoomPopupDetail sPopup = new RoomPopupDetail();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.room_id = room_id;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupElectricPassword(IWin32Window host, int roomCutOffStatus, string roomName)
        {
            PopupElectricPassword sPopup = new PopupElectricPassword();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            sPopup.roomCutOffStatus = roomCutOffStatus;
            //
            if (roomCutOffStatus == 0)
                sPopup.cutMode = "on";
            else
                sPopup.cutMode = "off";
            //
            sPopup.roomName = roomName;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DialogResult showPopupWarning(IWin32Window host)
        {
            PopupWarning sPopup = new PopupWarning();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static DataRow showPopRePlaceADC(IWin32Window host, int adc_id, string adc_name, string adc_ip, string adc_serial)
        {
            PopupSelectADC sPopup = new PopupSelectADC();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.StartPosition = FormStartPosition.CenterScreen;

            //
            sPopup.adc_id = adc_id;
            sPopup.adc_name = adc_name;
            sPopup.adc_ip = adc_ip;
            sPopup.adc_serial = adc_serial;

            DialogResult dl = sPopup.ShowDialog(host);
            //
            DataRow drReturn = sPopup.drADC;
            //
            sPopup.Dispose();
            //
            return drReturn;
        }

        public static DialogResult showPopupBrowseESerial(IWin32Window host)
        {
            PopupBrowseESerial sPopup = new PopupBrowseESerial();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        public static void showPopUpWarningSetting(IWin32Window host)
        {
            PopupWarningSetting sPopup = new PopupWarningSetting();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
        }

        public static DialogResult showPopupChangePassword(IWin32Window host)
        {
            PopupChangePassword sPopup = new PopupChangePassword();
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;
            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            //
            DialogResult dl = sPopup.ShowDialog(host);
            //
            sPopup.Dispose();
            //
            return dl;
        }

        static PopupProgressBar sPopup = null;

        public static void showPopupProgressBar2(IWin32Window host)
        {
            if (host != null)
                ((DevExpress.XtraEditors.XtraUserControl)host).Enabled = false;

            if (sPopup == null)
            {
                sPopup = new PopupProgressBar();
            }
            //
            sPopup.ShowIcon = false;
            sPopup.ShowInTaskbar = false;
            sPopup.HelpButton = false;

            sPopup.MinimizeBox = false;
            sPopup.MaximizeBox = false;
            sPopup.MaximumSize = sPopup.MinimumSize = sPopup.Size;
            sPopup.StartPosition = FormStartPosition.CenterScreen;
            sPopup.ControlBox = false;
            sPopup.Show(host);

        }

        public static void CloseProgrssDailog(IWin32Window host)
        {
            ((DevExpress.XtraEditors.XtraUserControl)host).Enabled = true;
            if (sPopup != null)
            {
                sPopup.Hide();
            }

        }

        public static double CalculateUnitEWMeter(double LastestEnergy, double BeginEnergy)
        {
            Double Begin_RangeValue = 90000;
            Double End_RangeValue = 10000;
            double RoolBackValue = 100000.00;


            double SummaryTotal = 0;

            if (LastestEnergy.ToString() != "" && BeginEnergy.ToString() != "")
            {
                if (BeginEnergy > LastestEnergy)
                {

                    if (BeginEnergy > Begin_RangeValue && LastestEnergy < End_RangeValue)
                    {
                        // Rollback case
                        // ( Roolback + End ) - Begin  ======> [ 100,000 + End ] - Begin
                        SummaryTotal = (RoolBackValue + LastestEnergy) - BeginEnergy;
                    }
                    else
                    {
                        // AbNormal value
                        // End Value - Begin Value
                        SummaryTotal = LastestEnergy - BeginEnergy;
                    }
                }
                else
                {
                    // Normal value
                    // End Value - Begin Value
                    SummaryTotal = LastestEnergy - BeginEnergy;
                }
            }

            return SummaryTotal;
        }

        class warningItem
        {
            public warningItem()
            {

            }
            public DateTime list_date = DateTime.Now;
            public string list_name = "";
            public string new_item = "";
            public string list_roomname = "";
            public string list_detail = "";
            public string list_help = "";
        }

        public static DataTable getWarningList()
        {
            DataTable listTable = new DataTable();
            listTable.Columns.Add("list_id", typeof(int));
            listTable.Columns.Add("new_item", typeof(string));
            listTable.Columns.Add("list_date", typeof(string));
            listTable.Columns.Add("list_name", typeof(string));
            listTable.Columns.Add("list_roomname", typeof(string));
            listTable.Columns.Add("list_detail", typeof(string));
            listTable.Columns.Add("list_help_title", typeof(string));
            listTable.Columns.Add("list_help", typeof(string));
            //
            List<warningItem> lstWarning = new List<warningItem>();
            //
            DataTable dtSetting = BusinessLogicBridge.DataStore.getWarningSetting();
            //
            foreach (DataRow drSetting in dtSetting.Rows)
            {
                int enable = int.Parse(drSetting["warning_enable"].ToString());
                string key = drSetting["warning_key"].ToString();
                //
                if (enable == 0)
                    continue;
                //
                getWarning(key, ref lstWarning);
            }
            //
            int i = 1;
            //
            //ToolTip xx = new ToolTip();
            //xx.ToolTipTitle = "?";
            //xx.IsBalloon = true;
            //xx.Active = true;

            foreach (warningItem item in lstWarning.OrderByDescending(item => item.list_date))
            {
                DataRow dr = listTable.NewRow();
                //
                dr["list_id"] = i;
                dr["new_item"] = item.new_item;
                dr["list_date"] = item.list_date.ToString("dd/MM/yyyy");
                dr["list_name"] = item.list_name;
                dr["list_roomname"] = item.list_roomname;
                dr["list_detail"] = item.list_detail;
                dr["list_help"] = item.list_help;
                dr["list_help_title"] = "?";
                //
                listTable.Rows.Add(dr);
                //
                i++;
            }
            //
            return listTable;
        }

        static void getWarning(string _type, ref List<warningItem> _lst)
        {
            switch (_type)
            {
                case "overdue_checkin":
                    {
                        DataTable dt = BusinessLogicBridge.DataStore.getRoomReserveLateCheckIn();
                        //
                        foreach (DataRow dr in dt.Rows)
                        {
                            warningItem _item = new warningItem();
                            //
                            //_item.list_date = DateTime.Parse(dr["reserve_check_in_date"].ToString());
                            _item.list_date = DateTime.Parse(dr["reserve_check_in_date"].ToString()).AddDays(1);
                            //
                            if (_item.list_date.Date == DateTime.Today.Date)
                            {
                                _item.new_item = " New! ";
                            }

                            _item.list_roomname = dr["room_label"].ToString();
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), dr["reserve_check_in_date"].To<DateTime>().ToString(MainForm.SX_DateFormat(2)));
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }
                        //
                        break;
                    }
                case "overdue_checkout":
                    {
                        DataTable dt = BusinessLogicBridge.DataStore.getLeavesLateCheckOut();
                        //
                        foreach (DataRow dr in dt.Rows)
                        {
                            warningItem _item = new warningItem();
                            //
                            //_item.list_date = DateTime.Parse(dr["leave_date"].ToString());
                            _item.list_date = DateTime.Parse(dr["leave_date"].ToString()).AddDays(1);

                            if (_item.list_date.Date == DateTime.Today.Date)
                            {
                                _item.new_item = " New! ";
                            }

                            _item.list_roomname = dr["room_label"].ToString();
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), dr["leave_date"].To<DateTime>().ToString(MainForm.SX_DateFormat(2)));
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }
                        //
                        break;
                    }
                case "endofbook":
                    {
                        DataTable dt = BusinessLogicBridge.DataStore.getRoomReserveLateReserveDate();
                        //
                        foreach (DataRow dr in dt.Rows)
                        {
                            warningItem _item = new warningItem();
                            //
                            //_item.list_date = DateTime.Parse(dr["reserve_end_date"].ToString());
                            _item.list_date = DateTime.Parse(dr["reserve_end_date"].ToString()).AddDays(1);
                            //
                            if (_item.list_date.Date == DateTime.Today.Date)
                            {
                                _item.new_item = " New! ";
                            }
                            _item.list_roomname = dr["room_label"].ToString();
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), dr["reserve_end_date"].To<DateTime>().ToString(MainForm.SX_DateFormat(2)));
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }
                        //
                        break;
                    }
                case "billingdate":
                    {
                        int due_date = int.Parse(DXWindowsApplication2.MainForm.generalSettingTable.Rows[0]["due_date"].ToString());
                        //
                        if (DateTime.Now.Day == due_date)
                        {
                            warningItem _item = new warningItem();
                            //
                            _item.list_date = DateTime.Now;
                            if (_item.list_date.Date == DateTime.Today.Date)
                            {
                                _item.new_item = " New! ";
                            }
                            _item.list_roomname = "-";
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), due_date);
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }
                        //
                        break;
                    }
                //case "overdue_payment":
                //    {
                //        int payDate = int.Parse(DXWindowsApplication2.MainForm.generalSettingTable.Rows[0]["payment_date"].ToString());
                //        //
                //        //if (DateTime.Now.Day == payDate ||
                //        //        (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) < payDate && DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
                //        //    ))
                //        if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) < payDate )
                //        {
                //            warningItem _item = new warningItem();
                //            //
                //            _item.list_date = DateTime.Now;
                //            if (_item.list_date.Date == DateTime.Today.Date)
                //            {
                //                _item.new_item = " New! ";
                //            }
                //            _item.list_roomname = "-";
                //            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                //            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), payDate);
                //            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                //            //
                //            _lst.Add(_item);
                //        }
                //        //
                //        break;
                //    }
                case "vacantroom":
                    {
                        double current_config = BusinessLogicBridge.DataStore.getWarningCurrentSetting();

                        DataTable dt = BusinessLogicBridge.DataStore.checkEMeterCurrentLeak(current_config);

                        foreach (DataRow dr in dt.Rows)
                        {
                            warningItem _item = new warningItem();
                            //
                            _item.list_date = DateTime.Now;
                            if (_item.list_date.Date == DateTime.Today.Date)
                            {

                                _item.new_item = " New! ";

                            }
                            _item.list_roomname = dr["room_label"].ToString();
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), _item.list_date);
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }

                        break;
                    }
                case "database":
                    {
                        // get size of database backup

                        DataTable DBConfigInfo = BusinessLogicBridge.DataStore.getBackupConfig();

                        double WarnningDatabaseValueSize = BusinessLogicBridge.DataStore.getWarningDatabaseSetting();

                        long directorySize = GetDirectorySize(DBConfigInfo.Rows[0]["auto_dbpath"].ToString());

                        double DBValueMegabyte = ConvertGigabytesToMegabytes(WarnningDatabaseValueSize); // Megabyte

                        long realDBValueMegabyteToByte = ConvertMegabytesTobytes(DBValueMegabyte);

                        // check size actual
                        if (directorySize >= realDBValueMegabyteToByte)
                        {
                            warningItem _item = new warningItem();
                            //
                            _item.list_date = DateTime.Now;
                            if (_item.list_date.Date == DateTime.Today.Date)
                            {

                                _item.new_item = " New! ";

                            }
                            _item.list_roomname = "-";
                            _item.list_name = DXWindowsApplication2.MainForm.getLanguage("_warning_" + _type);
                            _item.list_detail = string.Format(DXWindowsApplication2.MainForm.getLanguage("_warning_detail_" + _type), _item.list_date);
                            _item.list_help = DXWindowsApplication2.MainForm.getLanguage("_warning_help_" + _type);
                            //
                            _lst.Add(_item);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        static double ConvertMegabytesToGigabytes(double megabytes) // SMALLER
        {
            // 1024 megabyte in a gigabyte
            return megabytes / 1024.0;
        }

        static double ConvertGigabytesToMegabytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes * 1024.0;
        }

        static long ConvertMegabytesTobytes(double megabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            double oneKiloByte = megabytes * 1024.0;

            long byteunit = (long)(oneKiloByte * 1024.0);

            return byteunit;
        }

        static long GetDirectorySize(string path)
        {

            long folderSize = 0;

            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            filePath = filePath + @"\DatabaseBackup";

            if (File.Exists(filePath) == false)
            {

                Directory.CreateDirectory(filePath);
                path = filePath;
            }

            string[] files = Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);

            foreach (string file in files)
            {

                FileInfo info = new FileInfo(file);

                folderSize += info.Length;

            }

            return folderSize;

        }

        public static bool IsEmail(string Email)
        {
            string strRegex = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return true;
            else
                return false;
        }

        public static bool isPhoneValid(string Number)
        {
            string strRegex = @"^(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}(\s*ext(ension)?[0-9]{5})?$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(Number))
                return true;
            else
                return false;
        }

        public static bool isValidUrl(string url)
        {
            string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        public static bool IsAlphaNumeric(string Text)
        {
            if ((Text.Length > 0) && (Text != ""))
            {
                string strRegex = @"[^a-zA-Z0-9ก-๙\.\,-\/ ]?$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(Text))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNumeric(string Text)
        {
            //Regex moneyR = new Regex(@"\d+\.\d{2}");
            try
            {
                Convert.ToDouble(Text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isEmpty(string param)
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

        public static void DatabaseBackup(string bPath)
        {
            string ExeLocation = @"mysqldump";
            try
            {
                bPath = bPath + "\\" + DateTime.Now.ToString("yyyy_MM");

                if (Directory.Exists(bPath) == false)
                {
                    Directory.CreateDirectory(bPath);
                }

                string dataformat = DateTime.Now.ToString("yyyy_MM_dd") + "_sxbilling";
                string dataformatTrans = DateTime.Now.ToString("yyyy_MM_dd") + "_sxbillingTrans";
                //
                string _bPath = bPath + "\\" + dataformat.Trim() + ".bak";
                string _bPathTrans = bPath + "\\" + dataformatTrans.Trim() + ".bak";
                //
                using (StreamWriter file = new StreamWriter(_bPath, false, Encoding.UTF8))
                {
                    ProcessStartInfo proc = new ProcessStartInfo();
                    //
                    string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", BusinessLogicBridge.DataStore.DBUsername, BusinessLogicBridge.DataStore.DBPassword, BusinessLogicBridge.DataStore.DBServer, BusinessLogicBridge.DataStore.DBName);

                    //
                    proc.FileName = ExeLocation;
                    proc.RedirectStandardInput = false;
                    proc.RedirectStandardOutput = true;
                    proc.Arguments = cmd;
                    proc.UseShellExecute = false;
                    proc.CreateNoWindow = true;
                    proc.WindowStyle = ProcessWindowStyle.Hidden;
                    //
                    Process p = Process.Start(proc);
                    string res;
                    //
                    res = p.StandardOutput.ReadToEnd();
                    file.WriteLine(res);
                    //
                    p.WaitForExit();
                    //
                    file.Close();
                }

                using (StreamWriter file = new StreamWriter(_bPathTrans, false, Encoding.UTF8))
                {
                    // Trans
                    ProcessStartInfo procTrans = new ProcessStartInfo();
                    //
                    string cmdprocTrans = string.Format(@"-u{0} -p{1} -h{2} {3}", BusinessLogicBridge.DataStore.DBUsername, BusinessLogicBridge.DataStore.DBPassword, BusinessLogicBridge.DataStore.DBServer, BusinessLogicBridge.DataStore.DBTransactionName);

                    //
                    procTrans.FileName = ExeLocation;
                    procTrans.RedirectStandardInput = false;
                    procTrans.RedirectStandardOutput = true;
                    procTrans.Arguments = cmdprocTrans;
                    procTrans.UseShellExecute = false;
                    procTrans.CreateNoWindow = true;
                    procTrans.WindowStyle = ProcessWindowStyle.Hidden;
                    //
                    Process pTrans = Process.Start(procTrans);
                    string resTrans;
                    //
                    resTrans = pTrans.StandardOutput.ReadToEnd();
                    file.WriteLine(resTrans);
                    //
                    pTrans.WaitForExit();
                    //
                    file.Close();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void DatabaseRestore(string bPath)
        {
            string ExeLocation = @"mysql";
            try
            {
                string _bPath = bPath;
                //
                StreamReader file = new StreamReader(bPath, true);
                string input = file.ReadToEnd();
                file.Close();
                //
                ProcessStartInfo proc = new ProcessStartInfo();
                //
                string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", BusinessLogicBridge.DataStore.DBUsername, BusinessLogicBridge.DataStore.DBPassword, BusinessLogicBridge.DataStore.DBServer, BusinessLogicBridge.DataStore.DBName);
                //
                proc.FileName = ExeLocation;
                proc.RedirectStandardInput = true;
                proc.RedirectStandardOutput = false;
                proc.Arguments = cmd;
                proc.UseShellExecute = false;
                proc.CreateNoWindow = true;
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                //
                Process p = Process.Start(proc);
                p.StandardInput.WriteLine(input);
                p.StandardInput.Close();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
