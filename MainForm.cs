using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraEditors;
using System.Threading;
using System.Globalization;
using DXWindowsApplication2;
using System.Collections;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using System.IO;
using DXWindowsApplication2.PrintDocuments;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace DXWindowsApplication2
{
    public partial class MainForm : RibbonForm
    {

        private PopupMenu popupMenu;

        public static int adc_timediff = 5;
        public static int adc_bg_interval = 10;
        public static int adc_bg_rowlimit = 2000;

        public DXWindowsApplication2.UserForms.RoomLight RoomSet;
        public static TextEdit EventFireSetRibbon = new TextEdit();
        public static TextEdit EventFireSetRibbonPage = new TextEdit();

        public static TextEdit EventSetAllLang = new TextEdit();

        public static string userid = "";
        public static int groupid = 0;
        public static string username = "";
        public static string password = "";
        public static string current_lang = "en";
        public static int year_format = 2; // 2013
        public static string dateformat = "";
        public static bool restartflag = false;

        public static DataTable translateTable = new DataTable();
        public static DataTable generalSettingTable = new DataTable();
        public static Dictionary<string, ADC.Jetbox.Lib.ADCClient> dictADC = new Dictionary<string, ADC.Jetbox.Lib.ADCClient>();
        public static adcHelper ADCHelper = new adcHelper();
        public static bool HaveLicence = false;
        public static bool TrialVersion = false;
        public static bool canAccess = false;
        public static objMEATHLicense LicObj = new objMEATHLicense();
        public static string hashKey = "JBMEATHSMARTBILLING";

        #region Static Zone

        public static string getLanguage(string index)
        {


            index = index.Trim();
            DataRow[] row = null;

            try
            {
                if (index == "_baht")
                {
                    DataTable GeneralConfig = BusinessLogicBridge.DataStore.getGeneralConfig();

                    if (GeneralConfig.Rows[0]["currency"].To<int>() != 1)
                    {
                        index = "_dollar";
                    }

                    row = translateTable.Select("index = '" + index + "'");

                }
                else
                {
                    row = translateTable.Select("index = '" + index + "'");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(index);
            }

            if (row == null || row.Count() == 0)
            {
                return index;
            }
            else
            {
                return row[0][3].ToString();
            }

        }

        public static string getLanguageWithColon(string index)
        {
            return getLanguage(index) + " : ";

        }

        public static string checkLogin(string username, string password, int groupid)
        {
            DataTable userProfile = BusinessLogicBridge.DataStore.checkLogin(username, password, groupid);

            if (userProfile.Rows.Count > 0)
            {
                DXWindowsApplication2.MainForm.groupid = userProfile.Rows[0]["group_id"].To<int>();
                DXWindowsApplication2.MainForm.userid = userProfile.Rows[0]["user_id"].ToString();
                DXWindowsApplication2.MainForm.username = userProfile.Rows[0]["username"].ToString();
                DXWindowsApplication2.MainForm.password = userProfile.Rows[0]["password"].ToString();

                return "1";
            }
            else
            {
                return "0";
            }

        }

        public bool checkAuthorize()
        {

            if ((DXWindowsApplication2.MainForm.userid != "") && (DXWindowsApplication2.MainForm.username != ""))
            {
                return true;
            }
            else
            {

                DialogResult LoginPopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupLogin(this);
                if (LoginPopupReturn != DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public static void setToggleBar()
        {
            if (Convert.ToInt32(DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue) == 0)
            {
                DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 1;
            }
            else
            {
                DXWindowsApplication2.MainForm.EventFireSetRibbon.EditValue = 0;
            }
        }

        public static void setTogglePage()
        {
            if (Convert.ToInt32(DXWindowsApplication2.MainForm.EventFireSetRibbonPage.EditValue) == 0)
            {
                DXWindowsApplication2.MainForm.EventFireSetRibbonPage.EditValue = 1;
            }
            else
            {
                DXWindowsApplication2.MainForm.EventFireSetRibbonPage.EditValue = 0;
            }
        }

        #endregion

        public MainForm()
        {

            EventSetAllLang.EditValue = "0";
            DXWindowsApplication2.splashForm.CloseSplash();
            try
            {
                initConnection();

                DataTable LicenceInfo = BusinessLogicBridge.DataStore.getGeneralConfig();

                if (LicenceInfo != null)
                {
                    if (LicenceInfo.Rows.Count > 0)
                    {
                        string dataPath = MainForm.CombinePaths(AppDomain.CurrentDomain.BaseDirectory, "Licence");

                        string descnationfile = Path.Combine(dataPath, "regkey.lic");

                        if (File.Exists(descnationfile) == true)
                        {
                            LicObj = readLicense(descnationfile);
                        }

                        if ((LicenceInfo.Rows[0]["licencekey"].ToString() != "") && (LicenceInfo.Rows[0]["licencekey"].ToString() == LicObj.LicenseKey))
                        {
                            BusinessLogicBridge.DataStore.updateADCSerial(LicObj.ADCSN1, LicObj.ADCSN2, LicObj.ADCSN3, LicObj.ADCSN4, LicObj.ADCSN5);
                            HaveLicence = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }

            if (HaveLicence == true)
            {
                canAccess = checkAuthorize();
            }
            else
            {

                DialogResult PopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupRegistration(this);

                if (PopupReturn != DialogResult.OK)
                {
                    if (TrialVersion == false)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        if (PopupReturn == DialogResult.Ignore)
                            canAccess = checkAuthorize();
                    }
                }
                else
                {
                    // Case OK

                    DialogResult RegisteredPopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupRegistered(this);
                    canAccess = checkAuthorize();
                }
            }

            if (canAccess == true)
            {
                InitializeComponent();


                this.popupMenu = new PopupMenu(ribbonControlSX.Manager);

                BarButtonItem itemAll = new BarButtonItem(ribbonControlSX.Manager, "Close All But This");
                BarButtonItem itemOne = new BarButtonItem(ribbonControlSX.Manager, "Close");

                itemAll.ItemClick += new ItemClickEventHandler(itemAll_ItemClick);
                itemOne.ItemClick += new ItemClickEventHandler(itemOne_ItemClick);

                popupMenu.AddItem(itemOne);
                popupMenu.AddItem(itemAll);

                barStaticItemTime.Caption = DateTime.Now.ToString();
                ///EventFireRibbon
                EventFireSetRibbon.EditValueChanged += new EventHandler(EventFireSetRibbon_EditValueChanged);
                EventFireSetRibbonPage.EditValueChanged += new EventHandler(EventFireSetRibbonPage_EditValueChanged);

                initADCConnection();
                // ADC.adc_ip
                // ADC.adc_port

                // get language from config
                DataTable languageconfig = BusinessLogicBridge.DataStore.getLangConfig();
                current_lang = languageconfig.Rows[0][1].ToString();
                languages.loadLanguage(current_lang);

                DataTable generalconfig = BusinessLogicBridge.DataStore.getGeneralConfig();

                year_format = generalconfig.Rows[0]["year_format"].To<int>();

                BusinessLogicBridge.DataStore.setYearFormat(year_format);

                if (year_format == 1)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                }

                if (current_lang == "th")
                {

                    barStaticItemStatus.Caption = "ผู้ใช้ระบบ : " + username;
                }
                else
                {
                    current_lang = "en";
                    barStaticItemStatus.Caption = "User : " + username;
                }

                UserLookAndFeel.Default.SetSkinStyle("Office 2010 Silver");
                InitSkinGallery();
                xtraTabControlDashboard.CloseButtonClick += new EventHandler(xtraTabControl1_CloseButtonClick);
                xtraTabControlDashboard.MouseDown += new MouseEventHandler(xtraTabControlDashboard_MouseDown);


                this.Text = getLanguage("_softwarename");


                loadDashBoard();
                //iniFolder();
                initRoomTypeNav();

                CheckSetting();

                navBarItemFreeRoom.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem1_LinkClicked);
                navBarItemReserveRoom.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem4_LinkClicked);
                navBarItemRentRoom.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem2_LinkClicked);
                navBarItemleave.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem3_LinkClicked);

                navBarItemConnected.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItemConnected_LinkClicked);
                navBarItemUnConnect.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItemUnConnect_LinkClicked);
                navBarItemLampCut.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItemLampCut_LinkClicked);
                navBarItemLampConnect.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItemLampConnect_LinkClicked);

                EventSetAllLang.EditValueChanged += new EventHandler(EventSetAllLang_EditValueChanged);

                //
                xtraTabControlDashboard.SelectedPageChanged += new TabPageChangedEventHandler(xtraTabControlDashboard_SelectedPageChanged);

                GridLocalizer.Active = new MyLocalizer();

                setTabBarLang();
                initRoomBarButton();

            }
            else
            {
                // Cancel Login 
                Environment.Exit(0);
            }

            //read adc config
            //
            if (generalSettingTable.Rows.Count > 0)
            {
                try
                {
                    adc_timediff = Convert.ToInt16(generalSettingTable.Rows[0]["adc_timediff"]);
                    adc_bg_interval = Convert.ToInt16(generalSettingTable.Rows[0]["adc_bg_interval"]);
                    adc_bg_rowlimit = Convert.ToInt16(generalSettingTable.Rows[0]["adc_bg_rowlimit"]);
                }
                catch { }
            }

            StartBackgroundTransaction();

        }

        void CheckPermissionShowPage()
        {
            this.ribbonControlSX.Manager.BeginUpdate();


            foreach (BarItem item in this.ribbonControlSX.Items)
            {
                // CheckRibbon Show Tab
                bool permission_access = true;
                string parent_id;
                string menu_id;
                if (item.Name == "") continue;

                string[] permission_splitted = item.Name.Split('_');

                if (permission_splitted.Length < 2) continue;
                string permission_referrence = permission_splitted[1].ToString().PadLeft(4, '0');

                if (permission_referrence.StartsWith("00") == true)
                {
                    // 2 length
                    permission_referrence = permission_referrence.Replace("00", "");
                    parent_id = permission_referrence.Substring(0, 1);
                    menu_id = permission_referrence.Substring(1, 1);
                }
                else if (permission_referrence.StartsWith("0") == true)
                {
                    // 3 length
                    permission_referrence = permission_referrence.Remove(0, 1);
                    parent_id = permission_referrence.Substring(0, 1);
                    menu_id = permission_referrence.Substring(1, 2);
                }
                else
                {
                    // 4 length
                    parent_id = permission_referrence.Substring(0, 2);
                    menu_id = permission_referrence.Substring(2, 2);
                }


                permission_access = BusinessLogicBridge.DataStore.CheckPermissionPageShow(parent_id.To<int>(), menu_id.To<int>(), groupid.To<int>());

                if (parent_id.To<int>() == 49 && menu_id.To<int>() == 50)
                {
                    permission_access = false;
                }

                if (permission_access == false)
                {
                    item.Enabled = false;
                }
                else
                {
                    item.Enabled = true;
                }

            }

            this.ribbonControlSX.Manager.EndUpdate();
        }

        void itemOne_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage PageSelected = xtraTabControlDashboard.SelectedTabPage;
            xtraTabControlDashboard.TabPages.Remove(PageSelected);
            GC.Collect();
        }

        void itemAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage PageSelected = xtraTabControlDashboard.SelectedTabPage;

            List<XtraTabPage> tabPagesToBeRemoved = new List<XtraTabPage>();

            foreach (XtraTabPage ptp in xtraTabControlDashboard.TabPages)
            {
                if (PageSelected != ptp)
                {
                    if (ptp.Name != "xtraTabPageDashboard")
                    {
                        tabPagesToBeRemoved.Add(ptp);
                    }
                }
            }

            foreach (XtraTabPage pageToBeRemoved in tabPagesToBeRemoved)
            {
                xtraTabControlDashboard.TabPages.Remove(pageToBeRemoved);
            }

            GC.Collect();
        }

        void xtraTabControlDashboard_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                XtraTabControl tabCtrl = sender as XtraTabControl;
                Point pt = MousePosition;
                XtraTabHitInfo info = tabCtrl.CalcHitInfo(tabCtrl.PointToClient(pt));
                if (info.HitTest == XtraTabHitTest.PageHeader)
                {
                    if (info.Page.Text != "Dashboard")
                        popupMenu.ShowPopup(pt);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    popupMenu.HidePopup();
                }
            }
        }

        void item_ItemClick(object sender, ItemClickEventArgs e)
        {

            XtraTabPage PageSelected = xtraTabControlDashboard.SelectedTabPage;

            List<XtraTabPage> tabPagesToBeRemoved = new List<XtraTabPage>();

            foreach (XtraTabPage ptp in xtraTabControlDashboard.TabPages)
            {
                if (PageSelected != ptp)
                {
                    if (ptp.Name != "xtraTabPageDashboard")
                    {
                        tabPagesToBeRemoved.Add(ptp);
                    }
                }
            }

            foreach (XtraTabPage pageToBeRemoved in tabPagesToBeRemoved)
            {
                xtraTabControlDashboard.TabPages.Remove(pageToBeRemoved);
            }

            GC.Collect();
        }

        void EventSetAllLang_EditValueChanged(object sender, EventArgs e)
        {
            //checkTabPageLang();
            setTabBarLang();
            initRoomBarButton();
        }

        //void checkTabPageLang() {
        //    //foreach (XtraTabPage ptp in xtraTabControlDashboard.TabPages)
        //    //{

        //    //        if (ptp.Name != "xtraTabPageDashboard")
        //    //        {
        //    //            ptp.Text.
        //    //        }
        //    //}
        //}

        void iniFolder()
        {

            string InvoiceFolder = "Invoice";
            string RecieptFolder = "Reciept";
            string TempReciept = "TempReciept";
            string ContractFolder = "Contract";
            string TempFolder = "Temp";
            string ImagesFolder = "Images";

            string PathFolder = AppDomain.CurrentDomain.BaseDirectory;

            if (Directory.Exists(PathFolder) == false)
            {

                Directory.CreateDirectory(PathFolder);
                Directory.CreateDirectory(Path.Combine(PathFolder, InvoiceFolder));
                // Create Reciept folder
                Directory.CreateDirectory(Path.Combine(PathFolder, RecieptFolder));
                // Create Temp Reciept folder
                Directory.CreateDirectory(Path.Combine(PathFolder, TempReciept));
                // Create Contract folder
                Directory.CreateDirectory(Path.Combine(PathFolder, ContractFolder));
                // Create Temp folder
                Directory.CreateDirectory(Path.Combine(PathFolder, TempFolder));
                // Create images folder
                Directory.CreateDirectory(Path.Combine(PathFolder, ImagesFolder));
            }
            else if (Directory.Exists(Path.Combine(PathFolder, InvoiceFolder)) == false)
            {
                Directory.CreateDirectory(Path.Combine(PathFolder, InvoiceFolder));
                // Create Reciept folder
                Directory.CreateDirectory(Path.Combine(PathFolder, RecieptFolder));
                // Create Temp Reciept folder
                Directory.CreateDirectory(Path.Combine(PathFolder, TempReciept));
                // Create Contract folder
                Directory.CreateDirectory(Path.Combine(PathFolder, ContractFolder));
                // Create Temp folder
                Directory.CreateDirectory(Path.Combine(PathFolder, TempFolder));
                // Create images folder
                Directory.CreateDirectory(Path.Combine(PathFolder, ImagesFolder));
            }
        }

        public static void initADCConnection()
        {

            DataTable ADCConfig = BusinessLogicBridge.DataStore.listADC();

            dictADC = new Dictionary<string, ADC.Jetbox.Lib.ADCClient>();

            for (int i = 0; i < ADCConfig.Rows.Count; i++)
            {
                dictADC.Add("adc_id_" + ADCConfig.Rows[i]["device_adc_id"], new ADC.Jetbox.Lib.ADCClient() { adc_ip = ADCConfig.Rows[i]["device_adc_ipadress"].ToString(), adc_port = 1600 });
            }
        }

        void navBarItemLampConnect_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.Controls.Clear();
            try
            {
                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(0, 1, 1);
                xtraTabPageDashboard.Controls.Add(RoomSet);

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        void navBarItemLampCut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.Controls.Clear();
            try
            {

                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(0, 1, 0);
                xtraTabPageDashboard.Controls.Add(RoomSet);

            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message);
            }
        }

        void navBarItemUnConnect_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.Controls.Clear();
            try
            {

                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(0, 0);
                xtraTabPageDashboard.Controls.Add(RoomSet);

            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message);
            }
        }

        void navBarItemConnected_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            xtraTabPageDashboard.Controls.Clear();
            try
            {

                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(0, 1);
                xtraTabPageDashboard.Controls.Add(RoomSet);

            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message);
            }

        }

        void navBarNotify_ItemChanged(object sender, EventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupWarning(this);
        }

        void navBarItemNotify_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupWarning(this);
        }

        void xtraTabControlDashboard_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControlDashboard.SelectedTabPage.Name == "xtraTabPageDashboard")
                loadDashBoard();
            
            //return;
            if (xtraTabControlDashboard.SelectedTabPage.Controls.Count > 0)
            {
                refreshDashBoard();

                XtraTabControl tabControl = sender as XtraTabControl;

                var u = tabControl.SelectedTabPage.Controls[0] as DXWindowsApplication2.UserForms.uBase;
                SimpleButton sb = null;
                SimpleButton sbDel = null;
                SimpleButton sbAdd = null;

                bool permission_access = true;
                string menu_id = "";
                string parent_id = "";
                string[] permission_splitted;
                string permission_referrence = "";

                if (u != null)
                {
                    permission_splitted = u.Parent.Name.Split('_');
                    permission_referrence = permission_splitted[1].ToString().PadLeft(4, '0');

                    if (permission_referrence.StartsWith("00") == true)
                    {
                        // 2 length
                        permission_referrence = permission_referrence.Replace("00", "");
                        parent_id = permission_referrence.Substring(0, 1);
                        menu_id = permission_referrence.Substring(1, 1);
                    }
                    else if (permission_referrence.StartsWith("0") == true)
                    {
                        // 3 length
                        permission_referrence = permission_referrence.Remove(0, 1);
                        parent_id = permission_referrence.Substring(0, 1);
                        menu_id = permission_referrence.Substring(1, 2);
                    }
                    else
                    {
                        // 4 length
                        parent_id = permission_referrence.Substring(0, 2);
                        menu_id = permission_referrence.Substring(2, 2);
                    }

                    permission_access = BusinessLogicBridge.DataStore.CheckPermissionAccess(parent_id.To<int>(), menu_id.To<int>(), groupid.To<int>());

                    #region Check Case Read Only

                    if (u.Controls.Find("bttAdd", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttAdd", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }


                    if (u.Controls.Find("bttImport", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttImport", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttSave", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSave", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttSearch", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSearch", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttSetToADC", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSetToADC", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }
                    if (u.Controls.Find("bttTestConnection", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttTestConnection", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttSetCLoop", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSetCLoop", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttSetCLoop2", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSetCLoop2", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttInitial", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttInitial", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttSetInRoom", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttSetInRoom", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("btSetUtility", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("btSetUtility", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Enabled && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Enabled = false;
                        }
                    }

                    if (u.Controls.Find("bttBackup", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttBackup", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttPaid", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttPaid", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttCancelInvoice", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttCancelInvoice", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttPrint", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttPrint", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttPrintTax", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttPrintTax", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttExport", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttExport", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttCreateReciept", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttCreateReciept", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttInvoiceCreate", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttInvoiceCreate", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttInvoiceCreate", true).Length > 0)
                    {
                        sbAdd = u.Controls.Find("bttInvoiceCreate", true)[0] as SimpleButton;
                        //
                        if (sbAdd.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbAdd.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttEdit", true).Length > 0)
                    {
                        sb = u.Controls.Find("bttEdit", true)[0] as SimpleButton;
                        //
                        if (sb.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sb.Visible = false;
                        }
                    }

                    if (u.Controls.Find("bttDelete", true).Length > 0)
                    {
                        sbDel = u.Controls.Find("bttDelete", true)[0] as SimpleButton;
                        //
                        if (sbDel.Visible && permission_access == false)
                        {
                            //XtraMessageBox.Show("Yes");
                            sbDel.Visible = false;
                        }
                    }

                    #endregion
                }

                if (year_format == 1)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                }

                //xtraTabControlDashboard.SelectedTabPage.Controls[0].Refresh();
            }
        }

        void EventFireSetRibbonPage_EditValueChanged(object sender, EventArgs e)
        {
            OpenTabPage();
        }

        void EventFireSetRibbon_EditValueChanged(object sender, EventArgs e)
        {
            SetRibbonEnabled();
        }

        public static void RefreshAllLang()
        {
            if (EventSetAllLang.EditValue.ToString() == "0")
                EventSetAllLang.EditValue = "1";
            else
                EventSetAllLang.EditValue = "0";
        }

        public void setTabBarLang()
        {
            // Page Tab
            this._024_RP_GeneralSetting.Text = getLanguage("_menu_general_setting");
            this._013_RP_MeterManagement.Text = getLanguage("_menu_meter_record");
            this._01_RP_RoomManagement.Text = getLanguage("_menu_room_management");
            this._017_RP_Report.Text = getLanguage("_menu_report");
            this._032_RP_ProgramSetting.Text = getLanguage("_menu_program_setting");
            this._037_RP_DatabaseSetting.Text = getLanguage("_menu_database_setting");
            this._040_RP_DeviceSetting.Text = getLanguage("_menu_device_setting");
            this._049_RP_Register.Text = getLanguage("_menu_register");
            this._045_RP_Help.Text = getLanguage("_menu_help");

            // General Group Tab
            this._2425_Bt_BusinessInfo.Caption = getLanguage("_business_info");
            this._2426_Bt_BuildingInfo.Caption = getLanguage("_building_info");
            this._2427_Bt_FloorInfo.Caption = getLanguage("_floor_info");
            this._2428_Bt_RoomtypeInfo.Caption = getLanguage("_roomtype_info");
            this._2429_Bt_RoomInfo.Caption = getLanguage("_room_info");
            this._2430_Bt_CostInfo.Caption = getLanguage("_additional_info");
            this._2431_Bt_DocumentInfo.Caption = getLanguage("_document_info");

            // Recording Management language
            this._1314_Bt_Electricity.Caption = getLanguage("_menu_meter_record_electricity");
            this._1315_Bt_Water.Caption = getLanguage("_menu_meter_record_water");
            this._1316_Bt_Phone.Caption = getLanguage("_menu_meter_record_telephone");

            // Program Group Tab
            this._3233_Bt_SystemSetting.Caption = getLanguage("_program_general_setting");
            this._3738_Bt_BackupImport.Caption = getLanguage("_menu_database_setting_backup_restore");
            this._3235_Bt_UserAccount.Caption = getLanguage("_program_user_setting");
            this._3234_Bt_Authorization.Caption = getLanguage("_program_permission_setting");
            this._3236_Bt_HistoryLog.Caption = getLanguage("_menu_program_setting_history_system");
            this._4948_Bt_License.Caption = getLanguage("_program_licence_setting");
            this._4547_Bt_About.Caption = getLanguage("_menu_help_about");
            this._4950_Bt_DataInternet.Caption = getLanguage("_menu_register_view_data_through");
            this._4546_Bt_Manual.Caption = getLanguage("_softwarename") + " " + getLanguage("_menu_help_manual") + ".PDF";

            this._4041_Bt_adc.Caption = getLanguage("_menu_device_setting_adc");
            this._4042_Bt_ElectricMeter.Caption = getLanguage("_menu_device_setting_electricity_meter");
            this._4043_Bt_WaterMeter.Caption = getLanguage("_menu_device_setting_water_meter");
            this._4044_Bt_PhoneMeter.Caption = getLanguage("_menu_device_setting_telephone");

            this.xtraTabPageDashboard.Text = getLanguage("_dashboard");
            this.mailGroup.Caption = getLanguage("_hotkey");
            
            // Room Management language
            this._12_Bt_TenantInfo.Caption = getLanguage("_menu_room_management_tenant_info");
            this._13_Bt_RoomInfo.Caption = getLanguage("_menu_room_management_room_info");
            this._14_Bt_Checkin.Caption = getLanguage("_menu_room_management_checkin");
            this._15_Bt_Checkout.Caption = getLanguage("_menu_room_management_checkout");
            this._16_Bt_RoomReserved.Caption = getLanguage("_menu_room_management_book");
            this._17_Bt_InformLeave.Caption = getLanguage("_menu_room_management_inform_leave");
            this._18_Bt_Invoice.Caption = getLanguage("_menu_room_management_issue_invoice");
            this._19_Bt_Payment.Caption = getLanguage("_menu_room_management_invoices_and_payment");
            this._110_Bt_Reciept.Caption = getLanguage("_menu_room_management_reciepts");
            this._111_Bt_Contract.Caption = getLanguage("_menu_room_management_contracts");
            this._112_Bt_Refund.Caption = getLanguage("_menu_room_management_refund_insurance");

            this.changePasswordItem.Caption = getLanguage("_changepassword");

            this.organizerGroup.Caption = getLanguage("_room_status");
            this.navBarGroupMeter.Caption = getLanguage("_meter_status");
            this.NavGroupRoomType.Caption = getLanguage("_roomtype");

            this.ButtonNotifyMsg.Text = getLanguage("_notify");
            this.ButtonAllRoom.Text = getLanguage("_allroom");
            this.navBarItemFreeRoom.Caption = getLanguage("_freeroom");
            this.navBarItemReserveRoom.Caption = getLanguage("_reserveroom");
            this.navBarItemRentRoom.Caption = getLanguage("_rentroom");
            this.navBarItemleave.Caption = getLanguage("_leaveroom");
            this.navBarItemInformLeaveBook.Caption = getLanguage("_inform_leave_booking");
            this.navBarItemMaintenance.Caption = getLanguage("_maintenance");
            this.navBarItemOverdue.Caption = getLanguage("_overdue");

            this.navBarItemConnected.Caption = getLanguage("_connected");
            this.navBarItemUnConnect.Caption = getLanguage("_unconnect");
            this.navBarItemLampConnect.Caption = getLanguage("_lampconnect");
            this.navBarItemLampCut.Caption = getLanguage("_lampcut");

            // Report View language
            this._1718_Bt_Income.Caption = getLanguage("_menu_report_income");
            this._1719_Bt_ElectricityConsumption.Caption = getLanguage("_menu_report_electricity_consumption");
            this._1720_Bt_WaterConsumption.Caption = getLanguage("_menu_report_water_consumption");
            this._1721_Bt_PhoneConsumption.Caption = getLanguage("_menu_report_phone_consumption");
            this._1722_Bt_Room.Caption = getLanguage("_menu_report_room");
            this._1723_Bt_Tenant.Caption = getLanguage("_menu_report_tenant");

            string barname ="";
            for (int i = 0; i < xtraTabControlDashboard.TabPages.Count; i++)
            {   
                //xtraTabControlDashboard.TabPages[i].PageEnabled

                //if (xtraTabControlDashboard.TabPages[i].Name == name)
               // {
                barname = xtraTabControlDashboard.TabPages[i].Name;
                barname = barname.Replace("Tab","");
                BarButtonItem x = ribbonControlSX.Items[barname] as BarButtonItem;
                if(x!=null)
                xtraTabControlDashboard.TabPages[i].Text = x.Caption;
                
               // }
            }
            //foreach (var t in tab.open)
            //{
            //    var x = this.Controls.Find("tab.name", false) as BarButtonItem;
            //    t.caption = x.Caption;
            //}


        }

        public void CheckSetting()
        {

            SetRibbonEnabled();
            OpenTabPage();
            generalSettingTable = BusinessLogicBridge.DataStore.getGeneralConfig();
        }

        public void initRoomTypeNav()
        {

            DataTable RoomTypesDT = DXWindowsApplication2.BusinessLogicBridge.DataStore.getAllRoomType();

            DevExpress.XtraNavBar.NavBarItem[] items = new DevExpress.XtraNavBar.NavBarItem[RoomTypesDT.Rows.Count];

            NavGroupRoomType.ItemLinks.Clear();

            for (int i = 0; i < RoomTypesDT.Rows.Count; i++)
            {
                items[i] = new DevExpress.XtraNavBar.NavBarItem();
                items[i].Name = RoomTypesDT.Rows[i]["roomtype_id"].ToString();
                items[i].Caption = RoomTypesDT.Rows[i]["roomtype_label"].ToString();

                int iSquare = 32;
                Image imageThumbnail;
                Image image;

                if (System.IO.File.Exists(RoomTypesDT.Rows[i]["roomtype_icon"].ToString()) == false)
                {
                    string userPath = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    image = Image.FromFile(userPath + "/" + "no-image.jpeg");
                }
                else
                {
                    image = Image.FromFile(RoomTypesDT.Rows[i]["roomtype_icon"].ToString());
                }

                int cxThumbnail, cyThumbnail;

                if (image.Width > image.Height)
                {
                    cxThumbnail = iSquare;
                    cyThumbnail = iSquare * image.Height / image.Width;
                }
                else
                {
                    cyThumbnail = iSquare;
                    cxThumbnail = iSquare * image.Width / image.Height;
                }

                imageThumbnail = image.GetThumbnailImage(cxThumbnail, cyThumbnail, null, (IntPtr)0);

                items[i].SmallImage = imageThumbnail;

                NavGroupRoomType.ItemLinks.Add(items[i]);
                items[i].LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(Roomtype_LinkClicked);

            }
            RoomTypesDT.Dispose();
        }

        void Roomtype_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DevExpress.XtraNavBar.NavBarItem RoomTypeID = sender as DevExpress.XtraNavBar.NavBarItem;

            //XtraMessageBox.Show(RoomTypeID.Name);

            xtraTabPageDashboard.Controls.Clear();
            try
            {
                RoomSet.Dispose();
                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(0, 1, 1, Convert.ToInt16(RoomTypeID.Name));
                xtraTabPageDashboard.Controls.Add(RoomSet);

            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message);
            }

        }

        void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.SuspendLayout();
            xtraTabPageDashboard.Controls.Clear();
            RoomSet.Dispose();
            RoomSet = new DXWindowsApplication2.UserForms.RoomLight(4);
            xtraTabPageDashboard.Controls.Add(RoomSet);
            xtraTabPageDashboard.ResumeLayout();

        }

        void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // xtraTabPage1.SuspendLayout();

            xtraTabPageDashboard.Controls.Clear();
            try
            {
                RoomSet.Dispose();
                RoomSet = new DXWindowsApplication2.UserForms.RoomLight(2);
                xtraTabPageDashboard.Controls.Add(RoomSet);
                // xtraTabPage1.ResumeLayout();
            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message);
            }
        }

        void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.SuspendLayout();
            xtraTabPageDashboard.Controls.Clear();
            RoomSet.Dispose();
            RoomSet = new DXWindowsApplication2.UserForms.RoomLight(3);
            xtraTabPageDashboard.Controls.Add(RoomSet);
            xtraTabPageDashboard.ResumeLayout();
        }

        void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPageDashboard.SuspendLayout();
            xtraTabPageDashboard.Controls.Clear();
            RoomSet.Dispose();
            RoomSet = new DXWindowsApplication2.UserForms.RoomLight(1);
            xtraTabPageDashboard.Controls.Add(RoomSet);
            xtraTabPageDashboard.ResumeLayout();
        }

        public void OpenNewTabBar()
        {
            newTab("Tab" + this._19_Bt_Payment.Name, this._19_Bt_Payment.Caption, new DXWindowsApplication2.UserForms.ViewInvoice());
        }

        public void initRoomBarButton()
        {

            string _room = getLanguage("_room");

            int MsgTotalCount = DXWindowsApplication2.UserForms.utilClass.getWarningList().Rows.Count;
            int RoomTotalCount = BusinessLogicBridge.DataStore.getCountRoomByStatus(0);
            int RoomTotalCountEmpty = BusinessLogicBridge.DataStore.getCountRoomByStatus(1);
            int RoomTotalCountRent = BusinessLogicBridge.DataStore.getCountRoomByStatus(2) + BusinessLogicBridge.DataStore.getCountRoomByStatus(8);
            int RoomTotalCountMove = BusinessLogicBridge.DataStore.getCountRoomByStatus(4);
            int RoomTotalCountReserve = BusinessLogicBridge.DataStore.getCountRoomByStatus(3);
            int RoomTotalCountMaintenance = BusinessLogicBridge.DataStore.getCountRoomByStatus(6);

            ButtonNotifyMsg.Text = getLanguage("_notify") + " (" + Convert.ToString(MsgTotalCount) + ")";
            ButtonAllRoom.Text = getLanguage("_allroom") + " " + Convert.ToString(RoomTotalCount) + " " + _room;
            navBarItemFreeRoom.Caption = getLanguage("_freeroom") + " " + Convert.ToString(RoomTotalCountEmpty) + " " + _room;
            navBarItemRentRoom.Caption = getLanguage("_rentroom") + " " + Convert.ToString(RoomTotalCountRent) + " " + _room;
            navBarItemleave.Caption = getLanguage("_leaveroom") + " " + Convert.ToString(RoomTotalCountMove) + " " + _room;
            navBarItemReserveRoom.Caption = getLanguage("_reserveroom") + " " + Convert.ToString(RoomTotalCountReserve) + " " + _room;
            //
            navBarItemMaintenance.Caption = getLanguage("_maintenance") + " " + Convert.ToString(RoomTotalCountMaintenance) + " " + _room;

        }

        public void refreshDashBoard()
        {
            initRoomBarButton();
            initRoomTypeNav();
        }

        public void loadDashBoard()
        {

            xtraTabPageDashboard.SuspendLayout();
            xtraTabPageDashboard.Controls.Clear();

            if (RoomSet != null)
            {
                RoomSet.Dispose();
                GC.Collect();
            }

            RoomSet = new DXWindowsApplication2.UserForms.RoomLight();

            xtraTabPageDashboard.Controls.Add(RoomSet);

            xtraTabPageDashboard.ResumeLayout();
        }

        public void initConnection()
        {
            BusinessLogicBridge.ConnectBusinessLogic();
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        public void CheckOpenTabPage()
        {

            bool permission_access = true;
            foreach (RibbonPage page in this.ribbonControlSX.Pages)
            {

                string parent_id;
                string menu_id;
                if (page.Name == "") continue;

                string[] permission_splitted = page.Name.Split('_');

                if (permission_splitted.Length < 2) continue;
                string permission_referrence = permission_splitted[1].ToString().PadLeft(4, '0');

                if (permission_referrence.StartsWith("0") == true)
                {
                    // 3 length
                    permission_referrence = permission_referrence.Remove(0, 1);
                    parent_id = permission_referrence.Substring(0, 1);
                    menu_id = permission_referrence.Substring(1, 2);
                }
                else
                {
                    // 4 length
                    parent_id = permission_referrence.Substring(0, 2);
                    menu_id = permission_referrence.Substring(2, 2);
                }

                permission_access = BusinessLogicBridge.DataStore.CheckPermissionPageShow(parent_id.To<int>(), menu_id.To<int>(), groupid.To<int>());

                if (permission_access == false)
                {
                    page.Visible = false;
                }
                else
                {
                    page.Visible = true;
                }
            }
        }

        public void OpenTabPage()
        {
            bool statusTrue = true;

            foreach (RibbonPage page in this.ribbonControlSX.Pages)
            {

                switch (page.Name)
                {

                    case "_037_RP_DatabaseSetting":
                        if (_3235_Bt_UserAccount.Enabled == true)
                        {
                            //page.Visible = statusTrue;
                            _3738_Bt_BackupImport.Enabled = true;
                        }
                        break;
                    case "_040_RP_DeviceSetting":
                        if (_3738_Bt_BackupImport.Enabled == true)
                        {
                            // page.Visible = statusTrue;
                            if (TrialVersion == false)
                            {
                                _4041_Bt_adc.Enabled = true;
                                _4042_Bt_ElectricMeter.Enabled = true;
                                _4043_Bt_WaterMeter.Enabled = true;
                                _4044_Bt_PhoneMeter.Enabled = true;
                            }
                            else
                            {
                                _4041_Bt_adc.Enabled = false;
                                _4042_Bt_ElectricMeter.Enabled = false;
                                _4043_Bt_WaterMeter.Enabled = false;
                                _4044_Bt_PhoneMeter.Enabled = false;
                            }
                        }
                        break;
                    case "_024_RP_GeneralSetting":
                        //if (_4044_Bt_PhoneMeter.Enabled == true)
                        //{
                        //page.Visible = statusTrue;
                        _2425_Bt_BusinessInfo.Enabled = true;
                        //}
                        break;
                    case "_01_RP_RoomManagement":
                        DataTable RoomManagementAccess = BusinessLogicBridge.DataStore.getPermissionAccess(0, 1, groupid);
                        bool haveDoc = BusinessLogicBridge.DataStore.CheckDocumentInfo();
                        if (haveDoc == true && RoomManagementAccess.Rows[0]["permission_flag_check"].To<bool>() == true)
                        {
                            page.Visible = statusTrue;
                        }
                        else
                        {
                            page.Visible = false;
                        }
                        break;
                    case "_013_RP_MeterManagement":
                        DataTable AmountDocument = BusinessLogicBridge.DataStore.getDocumentConfig();

                        if (AmountDocument.Rows.Count > 0)
                        {
                            page.Visible = statusTrue;
                        }
                        else
                        {
                            page.Visible = false;
                        }
                        break;
                    case "_017_RP_Report":
                        DataTable Document = BusinessLogicBridge.DataStore.getDocumentConfig();

                        if (Document.Rows.Count > 0)
                        {
                            page.Visible = statusTrue;
                        }
                        else
                        {
                            page.Visible = false;
                        }
                        break;

                    default:
                        break;

                }

            }
            CheckOpenTabPage();
        }

        public void newTab(string tabName, string tabCaption, DevExpress.XtraEditors.XtraUserControl userControl)
        {

            DevExpress.XtraTab.XtraTabPage current = new DevExpress.XtraTab.XtraTabPage();
            current.Name = tabName;
            current.Size = new System.Drawing.Size(762, 335);
            current.Text = tabCaption;
            current.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;

            DevExpress.XtraTab.XtraTabPage[] newpage = new DevExpress.XtraTab.XtraTabPage[] { current };
            if (!checkExistTabByName(current.Name))
            {
                xtraTabControlDashboard.TabPages.AddRange(newpage);
                current.Controls.Add(userControl);
                xtraTabControlDashboard.SelectedTabPage = current;
            }

            //  current.TabControl.CloseButtonClick += new EventHandler(TabControl_CloseButtonClick);

        }

        private bool checkExistTabByName(string name)
        {
            for (int i = 0; i < xtraTabControlDashboard.TabPages.Count; i++)
            {
                if (xtraTabControlDashboard.TabPages[i].Name == name)
                {
                    xtraTabControlDashboard.SelectedTabPage = xtraTabControlDashboard.TabPages[i];
                    //refresh
                    if (xtraTabControlDashboard.SelectedTabPage.Name != "xtraTabPageDashboard")
                    {
                        if (xtraTabControlDashboard.TabPages[i].Controls.Count > 0)
                            xtraTabControlDashboard.TabPages[i].Controls[0].Refresh();
                    }
                    //
                    i = xtraTabControlDashboard.TabPages.Count + 10;
                    return true;

                }
            }
            return false;
        }

        private void settingUserTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._14_Bt_Checkin.Name, this._14_Bt_Checkin.Caption, new DXWindowsApplication2.UserForms.RoomCheckin());
        }

        public void openCheckIn(int roomID)
        {
            DXWindowsApplication2.UserForms.RoomCheckin ucCheckIn = new DXWindowsApplication2.UserForms.RoomCheckin();

            ucCheckIn.selectRoomID = roomID;
            //
            newTab("Tab" + this._14_Bt_Checkin.Name, this._14_Bt_Checkin.Caption, ucCheckIn);
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {

            XtraTabControl tabControl = sender as XtraTabControl;
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;


            var u = tabControl.SelectedTabPage.Controls[0] as DXWindowsApplication2.UserForms.uBase;
            SimpleButton sb = null;
            if (u != null)
            {
                if (u.Controls.Find("bttSave", true).Length > 0)
                {
                    sb = u.Controls.Find("bttSave", true)[0] as SimpleButton;
                    //
                    if (sb.Enabled)
                    {
                        //if (DialogResult.Yes != DXWindowsApplication2.UserForms.utilClass.showPopupConfirmBox(this, "Data is saved ?", getLanguage("_softwarename")))
                        //{
                        if (DialogResult.Yes == DXWindowsApplication2.UserForms.utilClass.showPopupConfirmBox(this, getLanguage("_msg_4004"), getLanguage("_softwarename")))
                        {
                            if (tabControl.SelectedTabPage.Controls[0] is DXWindowsApplication2.UserForms.uBase)
                            {
                                u.TrySave();

                                if (u.TrySaveError == true)
                                {
                                    // true is error
                                    // ถ้า save ไม่ผ่านไม่ให้ปิด tab
                                    return;
                                }
                            }
                        }
                        //}
                    }
                }

                if (xtraTabControlDashboard.TabPages.Count > 1)
                {
                    xtraTabControlDashboard.SelectedTabPageIndex = ((DevExpress.XtraTab.XtraTabPage)(arg.Page)).TabIndex - 1;
                }

                (arg.Page as XtraTabPage).PageVisible = false;
                xtraTabControlDashboard.TabPages.Remove((arg.Page as XtraTabPage));
            }

            

        }

        private void basicInfoApartmentTab_ItemClick(object sender, ItemClickEventArgs e)
        {

            newTab("Tab" + this._2425_Bt_BusinessInfo.Name, this._2425_Bt_BusinessInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoCompany());
        }

        private void basicInfoBuildingTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2426_Bt_BuildingInfo.Name, this._2426_Bt_BuildingInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoBuilding());
        }

        private void basicInfoFloorTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2427_Bt_FloorInfo.Name, this._2427_Bt_FloorInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoFloor());
        }

        private void basicInfoRoomTypeTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2428_Bt_RoomtypeInfo.Name, this._2428_Bt_RoomtypeInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoRoomType());
        }

        private void basicInfoElectricMeterTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.basicInfoElectricMeterTab.Name, this.basicInfoElectricMeterTab.Caption, new DXWindowsApplication2.UserForms.BasicInfoElectricMeter());
        }

        private void basicInfoWaterMeterTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.basicInfoWaterMeterTab.Name, this.basicInfoWaterMeterTab.Caption, new DXWindowsApplication2.UserForms.BasicInfoWater());
        }

        private void basicInfoPhoneMeterTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.basicInfoPhoneMeterTab.Name, this.basicInfoPhoneMeterTab.Caption, new DXWindowsApplication2.UserForms.BasicInfoTelephone());
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1314_Bt_Electricity.Name, this._1314_Bt_Electricity.Caption, new DXWindowsApplication2.UserForms.Recording_EMeter());
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1315_Bt_Water.Name, this._1315_Bt_Water.Caption, new DXWindowsApplication2.UserForms.Recording_WMeter());
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1316_Bt_Phone.Name, this._1316_Bt_Phone.Caption, new DXWindowsApplication2.UserForms.Recording_PMeter());
        }

        private void reportWaterTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            // newTab("Tab" + this.reportWaterTab2.Name, this.reportWaterTab2.Caption, new DXWindowsApplication2.UserForms.ReportWater());
        }

        private void reportTelephoneTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            // newTab("Tab" + this.reportTelephoneTab.Name, this.reportTelephoneTab.Caption, new DXWindowsApplication2.UserForms.ReportTelephone());
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._3233_Bt_SystemSetting.Name, this._3233_Bt_SystemSetting.Caption, new DXWindowsApplication2.UserForms.ProgramGeneral());
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._3738_Bt_BackupImport.Name, this._3738_Bt_BackupImport.Caption, new DXWindowsApplication2.UserForms.BackupDatabase());
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            // newTab("Tab" + this.barLanguageSetting.Name, this.barLanguageSetting.Caption, new DXWindowsApplication2.UserForms.ProgramLanguage());
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._3235_Bt_UserAccount.Name, this._3235_Bt_UserAccount.Caption, new DXWindowsApplication2.UserForms.ProgramUser());
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.barPermissionSetting.Name, this.barPermissionSetting.Caption, new DXWindowsApplication2.UserForms.ProgramPermission());
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.barLogHistory.Name, this.barLogHistory.Caption, new DXWindowsApplication2.UserForms.ProgramLogAccess());
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.barButtonItem8.Name, this.barButtonItem8.Caption, new DXWindowsApplication2.UserForms.BackupOffline());
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this.barButtonItem9.Name, this.barButtonItem9.Caption, new DXWindowsApplication2.UserForms.BackupOnline());
        }

        DXWindowsApplication2.UserForms.RoomList ucRoomList = new DXWindowsApplication2.UserForms.RoomList();

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._13_Bt_RoomInfo.Name, this._13_Bt_RoomInfo.Caption, new DXWindowsApplication2.UserForms.RoomList());
        }

        public void openRoomInfo(int RoomID)
        {
            ucRoomList.selectRoomID = RoomID;
            //
            newTab("Tab" + this._13_Bt_RoomInfo.Name, this._13_Bt_RoomInfo.Caption, ucRoomList);
        }

        DXWindowsApplication2.UserForms.IssueInvoice ucInvoice = new DXWindowsApplication2.UserForms.IssueInvoice();

        public void openInvoiceInfo(int RoomID)
        {
            ucInvoice.selectRoomID = RoomID;

            newTab("Tab" + this._18_Bt_Invoice.Name, this._18_Bt_Invoice.Caption, ucInvoice);

        }

        DXWindowsApplication2.UserForms.ViewInvoice ucViewInvoice = new DXWindowsApplication2.UserForms.ViewInvoice();


        public void openViewInvoiceInfo(int RoomID)
        {
            ucViewInvoice.selectRoomID = RoomID;

            newTab("Tab" + this._19_Bt_Payment.Name, this._19_Bt_Payment.Caption, ucViewInvoice);

        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            //   newTab("Tab" + this.barButtonItem35.Name, this.barButtonItem35.Caption, new DXWindowsApplication2.UserForms.Tenant());
        }

        DXWindowsApplication2.UserForms.RoomCheckOut ucCheckOut = new DXWindowsApplication2.UserForms.RoomCheckOut();

        private void settingPasswordTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._15_Bt_Checkout.Name, this._15_Bt_Checkout.Caption, new DXWindowsApplication2.UserForms.RoomCheckOut());
        }

        public void openCheckOut(int RoomID)
        {
            ucCheckOut.selectRoomID = RoomID;
            //
            newTab("Tab" + this._15_Bt_Checkout.Name, this._15_Bt_Checkout.Caption, ucCheckOut);
        }

        DXWindowsApplication2.UserForms.RoomReserved ucReserve = new DXWindowsApplication2.UserForms.RoomReserved();

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._16_Bt_RoomReserved.Name, this._16_Bt_RoomReserved.Caption, new DXWindowsApplication2.UserForms.RoomReserved());
        }

        public void openCancelReserve(int RoomID)
        {
            ucReserve.selectReserveID = RoomID;
            //
            newTab("Tab" + this._16_Bt_RoomReserved.Name, this._16_Bt_RoomReserved.Caption, ucReserve);
        }


        public void openReserve(int RoomID)
        {
            ucReserve.selectRoomID = RoomID;
            //
            newTab("Tab" + this._16_Bt_RoomReserved.Name, this._16_Bt_RoomReserved.Caption, ucReserve);
        }

        DXWindowsApplication2.UserForms.RoomInformleave ucLeave = new DXWindowsApplication2.UserForms.RoomInformleave();

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._17_Bt_InformLeave.Name, this._17_Bt_InformLeave.Caption, new DXWindowsApplication2.UserForms.RoomInformleave());
        }

        public void openLeave(int RoomID)
        {
            ucLeave.selectRoomID = RoomID;
            //
            newTab("Tab" + this._17_Bt_InformLeave.Name, this._17_Bt_InformLeave.Caption, ucLeave);
        }

        public void openCancelLeave(int RoomID)
        {
            ucLeave.selectLeaveID = RoomID;
            //
            newTab("Tab" + this._17_Bt_InformLeave.Name, this._17_Bt_InformLeave.Caption, ucLeave);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._19_Bt_Payment.Name, this._19_Bt_Payment.Caption, new DXWindowsApplication2.UserForms.ViewInvoice());
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._18_Bt_Invoice.Name, this._18_Bt_Invoice.Caption, new DXWindowsApplication2.UserForms.IssueInvoice());
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._110_Bt_Reciept.Name, this._110_Bt_Reciept.Caption, new DXWindowsApplication2.UserForms.ListReceipt());
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            // newTab("Tab" + this.barButtonItem6.Name, this.barButtonItem6.Caption, new DXWindowsApplication2.UserForms.BookRefund());
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._112_Bt_Refund.Name, this._112_Bt_Refund.Caption, new DXWindowsApplication2.UserForms.RefundInsurance());
        }

        private void basicInfoAdditionItemTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2430_Bt_CostInfo.Name, this._2430_Bt_CostInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoAdditionItem());
        }

        private void basicInfoDocumentTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2431_Bt_DocumentInfo.Name, this._2431_Bt_DocumentInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoDocument());
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._111_Bt_Contract.Name, this._111_Bt_Contract.Caption, new DXWindowsApplication2.UserForms.ListContract());
        }

        private void basicInfoRoomTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._2429_Bt_RoomInfo.Name, this._2429_Bt_RoomInfo.Caption, new DXWindowsApplication2.UserForms.BasicInfoRoom());
        }

        private void barHelp_ItemClick(object sender, ItemClickEventArgs e)
        {

            //XtraMessageBox.Show("Wait implement");
            //DXWindowsApplication2.UserForms.utilClass.showPopupChangePassword(this);
            DXWindowsApplication2.UserForms.utilClass.showPopupWarning(this);
        }

        public void SetRibbonEnabled()
        {
            this.ribbonControlSX.Manager.BeginUpdate();
            bool haveGeneralInfo = BusinessLogicBridge.DataStore.CheckGeneralSetting();
            bool haveDatabaseInfo = BusinessLogicBridge.DataStore.CheckDatabaseSetting();
            bool haveLanguageInfo = BusinessLogicBridge.DataStore.CheckLanguageSetting();
            bool haveUserInfo = BusinessLogicBridge.DataStore.CheckUserSetting();
            bool havePermissionInfo = BusinessLogicBridge.DataStore.CheckPermissionSetting();
            bool haveCompanyInfo = BusinessLogicBridge.DataStore.CheckCompanyInfo();
            bool haveBuildingInfo = BusinessLogicBridge.DataStore.CheckBuildingInfo();
            bool haveFloorInfo = BusinessLogicBridge.DataStore.CheckFloorInfo();
            bool haveRoomTypeInfo = BusinessLogicBridge.DataStore.CheckRoomTypeInfo();
            bool haveRoomInfo = BusinessLogicBridge.DataStore.CheckRoomInfo();
            bool haveDocumentInfo = BusinessLogicBridge.DataStore.CheckDocumentInfo();
            bool haveCheckinInfo = BusinessLogicBridge.DataStore.CheckCheckInInfo();
            bool haveInformLeaveInfo = BusinessLogicBridge.DataStore.CheckInformLeaveInfo();
            bool haveRefundInfo = BusinessLogicBridge.DataStore.CheckRefundInfo();

            bool statusTrue = true;
            bool statusFalse = false;

            foreach (BarItem item in this.ribbonControlSX.Items)
            {
                switch (item.Name)
                {
                    case "_3234_Bt_Authorization":
                        if (haveLanguageInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_3235_Bt_UserAccount":
                        if (havePermissionInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_3738_Bt_BackupImport":
                        if (haveUserInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_4041_Bt_adc":
                        if (haveDatabaseInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2426_Bt_BuildingInfo":
                        if (haveCompanyInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2427_Bt_FloorInfo":
                        if (haveBuildingInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2428_Bt_RoomtypeInfo":
                        if (haveFloorInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2429_Bt_RoomInfo":
                        if (haveRoomTypeInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2430_Bt_CostInfo":
                        if (haveRoomInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;
                    case "_2431_Bt_DocumentInfo":
                        if (haveRoomInfo == statusTrue)
                        {
                            item.Enabled = statusTrue;
                        }
                        else
                        {
                            item.Enabled = statusFalse;
                        }
                        break;


                    default:
                        break;
                }

            }
            this.ribbonControlSX.Manager.EndUpdate();
            CheckOpenTabPage();
            CheckPermissionShowPage();

        }

        private void AutoClickReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenTabPage();
            SetRibbonEnabled();
        }

        private void _1718_Bt_Income_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1718_Bt_Income.Name, this._1718_Bt_Income.Caption, new DXWindowsApplication2.UserForms.ReportIncome());
        }

        private void _3234_Bt_Authorization_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._3234_Bt_Authorization.Name, this._3234_Bt_Authorization.Caption, new DXWindowsApplication2.UserForms.ProgramPermission());
        }

        private void _12_Bt_TenantInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._12_Bt_TenantInfo.Name, this._12_Bt_TenantInfo.Caption, new DXWindowsApplication2.UserForms.Tenant());
        }

        private void _3236_Bt_HistoryLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._3236_Bt_HistoryLog.Name, this._3236_Bt_HistoryLog.Caption, new DXWindowsApplication2.UserForms.ProgramLogAccess());
        }

        private void barButtonItemChangePassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupChangePassword(this);
        }

        private void ButtonNotifyMsg_Click(object sender, EventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopupWarning(this);
        }

        private void ButtonAllRoom_Click(object sender, EventArgs e)
        {
            loadDashBoard();
        }

        private void _4041_Bt_adc_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._4041_Bt_adc.Name, this._4041_Bt_adc.Caption, new DXWindowsApplication2.UserForms.DeviceADC());
        }

        private void _4042_Bt_ElectricMeter_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._4042_Bt_ElectricMeter.Name, this._4042_Bt_ElectricMeter.Caption, new DXWindowsApplication2.UserForms.DeviceEMeter());
        }

        private void _4043_Bt_WaterMeter_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._4043_Bt_WaterMeter.Name, this._4043_Bt_WaterMeter.Caption, new DXWindowsApplication2.UserForms.DeviceWMeter());
        }

        private void _4044_Bt_PhoneMeter_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._4044_Bt_PhoneMeter.Name, this._4044_Bt_PhoneMeter.Caption, new DXWindowsApplication2.UserForms.DevicePMeter());
        }

        private void _4948_Bt_License_ItemClick(object sender, ItemClickEventArgs e)
        {
            //bool HaveLicence2 = true;

            // check Licence file
            if (HaveLicence == true)
            {
                DialogResult RegisteredPopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupRegistered(this);
            }
            else
            {

                DialogResult PopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupRegistration(this);

                if (PopupReturn == DialogResult.OK)
                {
                    //- Check selected license file complete?
                    // If complete, to check enter license key.
                    // If not, show warning message box screen 1022.
                    // - Check enter licensed key completed?
                    // If complete, to check validate license file and license key.
                    // If not, show warning message box screen 1023.
                    // - Check validates license file and license key?
                    // If valid, show information message box screen 3018, load data from license file to database and show screen 2801-2.
                    // If not valid, show warning message box screen 1024.
                    DialogResult RegisteredPopupReturn = DXWindowsApplication2.UserForms.utilClass.showPopupRegistered(this);
                }
            }

        }

        public static string CombinePaths(params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException("paths");
            }
            return paths.Aggregate(Path.Combine);
        }

        public static string ThaiBaht(string txt)
        {
            if (txt.To<double>() < 9999999.99)
            {
                string bahtTxt, n, bahtTH = "";
                double amount;
                try { amount = Convert.ToDouble(txt); }
                catch { amount = 0; }
                bahtTxt = amount.ToString("####.00");
                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                string[] temp = bahtTxt.Split('.');
                string intVal = temp[0];
                string decVal = temp[1];
                if (Convert.ToDouble(bahtTxt) == 0)
                    bahtTH = "ศูนย์บาทถ้วน";
                else
                {
                    for (int i = 0; i < intVal.Length; i++)
                    {
                        n = intVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == (intVal.Length - 1)) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (intVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (intVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(intVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "บาท";
                    if (decVal == "00")
                        bahtTH += "ถ้วน";
                    else
                    {
                        for (int i = 0; i < decVal.Length; i++)
                        {
                            n = decVal.Substring(i, 1);
                            if (n != "0")
                            {
                                if ((i == decVal.Length - 1) && (n == "1"))
                                    bahtTH += "เอ็ด";
                                else if ((i == (decVal.Length - 2)) && (n == "2"))
                                    bahtTH += "ยี่";
                                else if ((i == (decVal.Length - 2)) && (n == "1"))
                                    bahtTH += "";
                                else
                                    bahtTH += num[Convert.ToInt32(n)];
                                bahtTH += rank[(decVal.Length - i) - 1];
                            }
                        }
                        bahtTH += "สตางค์";
                    }
                }
                return bahtTH;
            }
            else
            {
                return txt;
            }
        }

        public static DataTable VatCalculate(DataTable CalcItemTable)
        {
            int countDT = CalcItemTable.Rows.Count;
            // order
            // item_name
            // item_amount
            // item_priceperunit
            // item_sumprice
            // item_vatprice
            // item_netprice
            // item_vat_bool

            for (int i = 0; i < countDT; i++)
            {
                switch (CalcItemTable.Rows[i]["item_vat"].To<int>())
                {
                    case 1: // No vat
                        CalcItemTable.Rows[i]["item_sumprice"] = CalcItemTable.Rows[i]["item_sumprice"];
                        CalcItemTable.Rows[i]["item_vatprice"] = 0.00;
                        CalcItemTable.Rows[i]["item_netprice"] = CalcItemTable.Rows[i]["item_sumprice"];
                        break;
                    case 2: // include vat
                        CalcItemTable.Rows[i]["item_sumprice"] = CalcItemTable.Rows[i]["item_sumprice"].To<double>() - CalcItemTable.Rows[i]["item_vatprice"].To<double>();
                        CalcItemTable.Rows[i]["item_vatprice"] = CalcItemTable.Rows[i]["item_vatprice"];
                        CalcItemTable.Rows[i]["item_netprice"] = CalcItemTable.Rows[i]["item_sumprice"].To<double>() + CalcItemTable.Rows[i]["item_vatprice"].To<double>();
                        break;
                    case 3: // exclude vat
                        CalcItemTable.Rows[i]["item_sumprice"] = CalcItemTable.Rows[i]["item_sumprice"];
                        CalcItemTable.Rows[i]["item_vatprice"] = CalcItemTable.Rows[i]["item_vatprice"];
                        CalcItemTable.Rows[i]["item_netprice"] = CalcItemTable.Rows[i]["item_sumprice"].To<double>() + CalcItemTable.Rows[i]["item_vatprice"].To<double>();
                        break;
                    default:
                        // No vat
                        CalcItemTable.Rows[i]["item_sumprice"] = CalcItemTable.Rows[i]["item_sumprice"];
                        CalcItemTable.Rows[i]["item_vatprice"] = 0.00;
                        CalcItemTable.Rows[i]["item_netprice"] = CalcItemTable.Rows[i]["item_sumprice"];
                        break;
                }
            }

            return CalcItemTable;
        }

        public static string SX_DateFormat(int format_no)
        {

            switch (format_no)
            {
                case 1:
                    dateformat = "MM/dd/yyyy";
                    break;
                case 2:
                    dateformat = "dd/MM/yyyy";
                    break;
                case 3:
                    dateformat = "dd MMMM yyyy";
                    break;
                case 4:
                    dateformat = "MMM dd,yyyy";
                    break;
                case 5:
                    dateformat = "yyyy/MM/dd";
                    break;
                default:
                    dateformat = "dd/MM/yyyy";
                    break;
            }

            return dateformat;
        }

        private void _4950_Bt_DataInternet_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._4950_Bt_DataInternet.Name, this._4950_Bt_DataInternet.Caption, new DXWindowsApplication2.UserForms.ViewDataThroughInternet());
        }

        // End Ribbon

        public static objMEATHLicense readLicense(string sFile)
        {
            objMEATHLicense obj = new objMEATHLicense();
            //
            if (System.IO.File.Exists(sFile))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(sFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        s = HelperEncrypt.Decrypt(s, MainForm.hashKey);
                        //
                        string[] slines = s.Split('=');
                        //
                        if (slines.Length == 0)
                            continue;
                        //
                        //
                        if (slines[0] == "ProductID")
                            obj.ProductID = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "LicenseKey")
                            obj.LicenseKey = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "ADCSN1")
                            obj.ADCSN1 = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "ADCSN2")
                            obj.ADCSN2 = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "ADCSN3")
                            obj.ADCSN3 = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "ADCSN4")
                            obj.ADCSN4 = slines.Length > 1 ? slines[1] : "";
                        else if (slines[0] == "ADCSN5")
                            obj.ADCSN5 = slines.Length > 1 ? slines[1] : "";
                    }
                }
            }
            //
            return obj;
        }

        private void _4547_Bt_About_ItemClick(object sender, ItemClickEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.showPopAddAbout(this);
        }

        private void _4546_Bt_Manual_ItemClick(object sender, ItemClickEventArgs e)
        {
            // A path to export a report.
            string userPath = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string reportPath = userPath + "\\e-SmartBilling\\e-SmartBilling Manual.pdf";

            // Create a report instance.
            // XtraReport1 report = new XtraReport1();

            // Get its PDF export options.
            //PdfExportOptions pdfOptions = report.ExportOptions.Pdf;

            //// Set PDF-specific export options.
            //pdfOptions.Compressed = true;
            //pdfOptions.ImageQuality = PdfJpegImageQuality.Low;

            // Show the result.
            StartProcess(reportPath);
        }

        // Use this method if you want to automaically open
        // the created PDF file in the default program.

        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }

        private void _1719_Bt_ElectricityConsumption_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1719_Bt_ElectricityConsumption.Name, this._1719_Bt_ElectricityConsumption.Caption, new DXWindowsApplication2.UserForms.ReportElectricConsummation());
        }

        private void _1720_Bt_WaterConsumption_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1720_Bt_WaterConsumption.Name, this._1720_Bt_WaterConsumption.Caption, new DXWindowsApplication2.UserForms.ReportWaterConsummation());
        }

        private void _1721_Bt_PhoneConsumption_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1721_Bt_PhoneConsumption.Name, this._1721_Bt_PhoneConsumption.Caption, new DXWindowsApplication2.UserForms.ReportPhoneConsummation());
        }

        private void _1722_Bt_Room_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1722_Bt_Room.Name, this._1722_Bt_Room.Caption, new DXWindowsApplication2.UserForms.ReportRoom());
        }

        private void _1723_Bt_Tenant_ItemClick(object sender, ItemClickEventArgs e)
        {
            newTab("Tab" + this._1723_Bt_Tenant.Name, this._1723_Bt_Tenant.Caption, new DXWindowsApplication2.UserForms.ReportTenant());
        }

        private void barButtonItemLinkURL_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.meath-co.com");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (restartflag == false)
            {

                DialogResult dr = DXWindowsApplication2.UserForms.utilClass.showPopupConfirmBox(this, getLanguage("_msg_4016"), this.Text);

                if (dr == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// ADC Background Transaction
        /// </summary>

        public static Thread threadBG = null;
        int tSleep = 50000;
        int iLimitRowId = 2000;
        public static int loadMinutes = 5;
        public static bool stopBG = false;
        //
        void StartBackgroundTransaction()
        {

            iLimitRowId = adc_bg_rowlimit;
            loadMinutes = adc_bg_interval;

            tSleep = loadMinutes * 10000; // 5 minute to millisec

            if (threadBG == null)
            {
                threadBG = new Thread(new ThreadStart(RunTransactionBackground));
                threadBG.IsBackground = true;
                threadBG.Priority = ThreadPriority.Lowest;
            }
            //
            threadBG.Start();
        }

        void RunTransactionBackground()
        {
            //int x = 1;
            //while (!stopBG)
            //{

            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("name");
            //    for (int i = 1; i <= 2000; i++)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["name"] = "xxx_" + x;
            //        //
            //        x++;
            //        //
            //        dt.Rows.Add(dr);
            //    }
            //    //
            //    //BusinessLogicBridge.DataStore.insertTest1(dt);
            //    BusinessLogicBridge.DataStore.insertE_RecordToMysql(dt);
            //}
            //return;

            var _dictADC = dictADC;
            //
            //update last transaction first
            foreach (KeyValuePair<string, ADC.Jetbox.Lib.ADCClient> kvp in _dictADC)
            {
                if (stopBG)
                    break;
                //
                int adcID = kvp.Key.Contains("_") ?
                    Convert.ToInt16(kvp.Key.Split('_')[2]) :
                    Convert.ToInt16(kvp.Key);
                var obj = kvp.Value;
                //
                if (obj.adc_ip == "") continue;
                //
                barStaticItemTime.Caption = "";
                //
                try
                {
                    //
                    UpdateBGStatus(string.Format("Connecting to ADC [{0}]", obj.adc_ip));
                    //
                    bool isValidADC = obj.TestADCConnection();
                    //
                    if (!isValidADC)
                    {
                        UpdateBGStatus(string.Format("Cannot connect to ADC [{0}]", obj.adc_ip));
                        continue;
                    }

                    UpdateBGStatus(string.Format("Sync time to ADC [{0}]", obj.adc_ip));
                    //
                    //Sync ADCTime
                    SyncADCTime(adcID, obj);

                    BusinessLogicBridge.DataStore.deleteOldEMeterTransaction();

                    //EMeter
                    DoBGPresentTransaction(1, adcID, obj);
                    //
                    BusinessLogicBridge.DataStore.deleteOldWMeterTransaction();
                    //WMeter
                    DoBGPresentTransaction(2, adcID, obj);
                    //

                    BusinessLogicBridge.DataStore.deleteOldPMeterTransaction();
                    //PhoneMeter                    
                    DoBGPresentTransaction(3, adcID, obj);
                    //
                }
                catch { }
            }
            //
            while (!stopBG)
            {
                Thread.Sleep(tSleep);
                //
                initADCConnection();
                //
                _dictADC = dictADC;
                //
                if (_dictADC == null || _dictADC.Count == 0)
                    continue;
                //
                try
                {
                    foreach (KeyValuePair<string, ADC.Jetbox.Lib.ADCClient> kvp in _dictADC)
                    {
                        if (stopBG)
                            break;
                        //
                        int adcID = kvp.Key.Contains("_") ?
                            Convert.ToInt16(kvp.Key.Split('_')[2]) :
                            Convert.ToInt16(kvp.Key);
                        var obj = kvp.Value;
                        //
                        if (obj.adc_ip == "") continue;
                        //
                        barStaticItemTime.Caption = "";
                        //
                        try
                        {
                            //
                            UpdateBGStatus(string.Format("Connecting to ADC [{0}]", obj.adc_ip));
                            //
                            bool isValidADC = obj.TestADCConnection();
                            //
                            if (!isValidADC)
                            {
                                UpdateBGStatus(string.Format("Cannot connect to ADC [{0}]", obj.adc_ip));
                                continue;
                            }
                            //
                            DateTime dateBegin = DateTime.Now.AddDays(-1);

                            //EMeter
                            //dateBegin = GetBeginBGTransaction(1, adcID, obj);
                            dateBegin = DateTime.Now.AddMonths(-1);
                            DoBGTransaction(1, adcID, obj);
                            if (stopBG)
                                break;
                            //
                            //WMeter
                            //dateBegin = GetBeginBGTransaction(2, adcID, obj);
                            DoBGTransaction(2, adcID, obj);
                            //
                            if (stopBG)
                                break;
                            //
                            //PhoneMeter
                            //dateBegin = GetBeginBGTransaction(3, adcID, obj);
                            DoBGTransaction(3, adcID, obj);
                            //
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        void SyncADCTime(int iADC_ID, ADC.Jetbox.Lib.ADCClient objADC)
        {
            DateTime dtADC = objADC.GetADCTime();
            //
            TimeSpan tsDiff = DateTime.Now.Subtract(dtADC);
            //
            if (tsDiff.TotalMinutes >= adc_timediff || tsDiff.TotalMinutes <= adc_timediff)
            {
                objADC.SetADCTime(DateTime.Now);
            }
        }

        void DoBGPresentTransaction(int iType, int iADC_ID, ADC.Jetbox.Lib.ADCClient objADC)
        {
            DateTime dateEnd = DateTime.Now.AddMinutes(loadMinutes);
            //
            if (objADC.isADCBusyOrLock)
                return;
            //
            DataTable dt = null;
            int iLastRowId = 0;
            //
            iLastRowId = GetBeginBGTransaction(iType, iADC_ID, objADC);
            //
            //Load transaction from ADC
            //
            if (iType == 1)
            {
                UpdateBGStatus(string.Format("Updating last e-meter transaction from ADC [{0}]", objADC.adc_ip));
                dt = objADC.GetPresentAllEMeterTransactionRowId();
            }
            else if (iType == 2)
            {
                UpdateBGStatus(string.Format("Updating last w-meter transaction from ADC [{0}]", objADC.adc_ip));
                dt = objADC.GetPresentAllWMeterTransactionRowId();
            }
            else if (iType == 3)
            {
                UpdateBGStatus(string.Format("Updating last phone-meter transaction from ADC [{0}]", objADC.adc_ip));
                //dt = objADC.GetPresentAllPMeterTransactionRowId();
            }
            //
            //Check transaction record
            //
            if (dt.Rows.Count > 0)
            {
                if (iType == 1)
                {
                    dt = ADCHelper.ConvertEMeterTransaction(dt, iADC_ID);
                    //
                    UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                }
                else if (iType == 2)
                {
                    dt = ADCHelper.ConvertWMeterTransaction(dt, iADC_ID);
                    //
                    UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                }
                else if (iType == 3)
                {
                    dt = ADCHelper.ConvertWMeterTransaction(dt, iADC_ID);
                    //
                    UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                }
            }
            //
            UpdateBGStatus("");
        }

        void DoBGTransaction(int iType, int iADC_ID, ADC.Jetbox.Lib.ADCClient objADC)
        {
            if (objADC.isADCBusyOrLock)
                return;
            //
            //check adcip still valid
            //
            bool valid = false;
            var _dict = dictADC;
            foreach (KeyValuePair<string, ADC.Jetbox.Lib.ADCClient> kvp in _dict)
            {
                if (kvp.Value.adc_ip == objADC.adc_ip)
                {
                    valid = true;
                    break;
                }
            }
            //
            if (!valid)
                return;
            //
            DataTable dt = null;
            Int32 iMaxRowId = 0;
            //
            //Load transaction from ADC
            //
            if (iType == 1)
            {
                iMaxRowId = BusinessLogicBridge.DataStore.getMaxRowIdElectricityMeterTransactionByAdcId(iADC_ID);
                UpdateBGStatus(string.Format("Getting electric-meter transaction from ADC [{0}]", objADC.adc_ip));
                dt = objADC.GetEMeterTransactionRowId(iMaxRowId, "asc", iLimitRowId);
            }
            else if (iType == 2)
            {
                iMaxRowId = BusinessLogicBridge.DataStore.getMaxRowIdWaterMeterTransactionByAdcId(iADC_ID);
                UpdateBGStatus(string.Format("Getting water-meter transaction from ADC [{0}]", objADC.adc_ip));
                dt = objADC.GetWMeterTransactionRowId(iMaxRowId, "asc", iLimitRowId);
            }
            else if (iType == 3)
            {
                iMaxRowId = BusinessLogicBridge.DataStore.getMaxRowIdPhoneMeterTransactionByAdcId(iADC_ID);
                UpdateBGStatus(string.Format("Getting phone-meter transaction from ADC [{0}]", objADC.adc_ip));
                dt = objADC.GetPMeterTransactionRowId(iMaxRowId, "asc", iLimitRowId);
            }
            //
            //Check transaction record
            //
            if (dt.Rows.Count > 0)
            {
                if (iType == 1)
                {
                    dt = ADCHelper.ConvertEMeterTransaction(dt, iADC_ID);
                    BusinessLogicBridge.DataStore.insertE_RecordToMysql(dt);
                    //
                    var drCheckDate = dt.Select("", "rowid desc")[0]["e_datetime"].To<DateTime>();
                    //
                    if (drCheckDate > DateTime.Now.AddMinutes(-10))
                        UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                    else
                    {
                        UpdateBGStatus("");
                        System.Threading.Thread.Sleep(100);
                        DoBGTransaction(iType, iADC_ID, objADC);
                    }

                }
                else if (iType == 2)
                {
                    dt = ADCHelper.ConvertWMeterTransaction(dt, iADC_ID);
                    BusinessLogicBridge.DataStore.insertW_RecordToMysql(dt);
                    //
                    var drCheckDate = dt.Select("", "rowid desc")[0]["w_datetime"].To<DateTime>();
                    //
                    if (drCheckDate > DateTime.Now.AddMinutes(-10))
                        UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                    else
                    {
                        UpdateBGStatus("");
                        System.Threading.Thread.Sleep(100);
                        DoBGTransaction(iType, iADC_ID, objADC);
                    }
                }
                else if (iType == 3)
                {
                    dt = ADCHelper.ConvertPhoneTransaction(dt, iADC_ID);
                    BusinessLogicBridge.DataStore.insertP_RecordToMysql(dt);
                    //
                    var drCheckDate = dt.Select("", "rowid desc")[0][""].To<DateTime>();
                    //
                    if (drCheckDate > DateTime.Now.AddMinutes(-10))
                        UpdatePresentMeter(dt, iType, iADC_ID, objADC);
                    else
                    {
                        UpdateBGStatus("");
                        System.Threading.Thread.Sleep(100);
                        DoBGTransaction(iType, iADC_ID, objADC);
                    }
                }
            }
            //
            UpdateBGStatus("");
        }

        void UpdatePresentMeter(DataTable dt, int iType, int iADC_ID, ADC.Jetbox.Lib.ADCClient objADC)
        {
            if (iType == 1)
            {
                foreach (DataRow dr in BusinessLogicBridge.DataStore.getElectricityMeterByAdcId(iADC_ID).Rows)
                {
                    try
                    {
                        var drPresent = dt.Select("E_Serial_No='" + dr["meter_serial"].ToString() + "'", "rowid desc")[0];
                        BusinessLogicBridge.DataStore.updatPresentE_Meter(
                            drPresent["E_Serial_No"].ToString(), iADC_ID,
                            drPresent["e_datetime"].To<DateTime>(),
                            drPresent["Total_Kwh"].To<double>(), drPresent["e_connection"].To<int>());
                    }
                    catch { }
                }
            }
            else if (iType == 2)
            {
                foreach (DataRow dr in BusinessLogicBridge.DataStore.getWaterMeterByAdcId(iADC_ID).Rows)
                {
                    try
                    {
                        var drPresent = dt.Select("w_serial='" + dr["meter_serial"].ToString() + "'", "rowid desc")[0];
                        BusinessLogicBridge.DataStore.updatPresentW_Meter(
                            drPresent["w_serial"].ToString(), iADC_ID,
                            drPresent["w_datetime"].To<DateTime>(),
                            drPresent["w_unit"].To<int>(), drPresent["w_connection"].To<int>());
                    }
                    catch { }
                }
            }
            else if (iType == 3)
            {
                foreach (DataRow dr in BusinessLogicBridge.DataStore.getPhoneMeterByAdcId(iADC_ID).Rows)
                {
                    try
                    {
                        var drPresent = dt.Select("phone_serial='" + dr["meter_serial"].ToString() + "'", "rowid desc")[0];
                        BusinessLogicBridge.DataStore.updatPresentP_Meter(
                            drPresent["phone_serial"].ToString(), iADC_ID,
                            drPresent[""].To<DateTime>(),
                            drPresent["amount"].To<int>(), drPresent["Status"].To<int>());
                    }
                    catch { }
                }
            }
        }

        int GetBeginBGTransaction(int iType, int iADC_ID, ADC.Jetbox.Lib.ADCClient objADC)
        {
            int lastRowId = 0;
            DateTime dateFromTransaction = DateTime.Now;
            DataTable dtLastTransaction = null;
            //
            //Try get last datetime from bg transaction log
            //
            if (iType == 1)
                dtLastTransaction = BusinessLogicBridge.DataStore.SelectLastEletricMeterTransaction(iADC_ID);
            else if (iType == 2)
                dtLastTransaction = BusinessLogicBridge.DataStore.SelectLastWaterMeterTransaction(iADC_ID);
            else if (iType == 3)
                dtLastTransaction = BusinessLogicBridge.DataStore.SelectLastPhoneMeterTransaction(iADC_ID);
            //
            if (dtLastTransaction != null && dtLastTransaction.Rows.Count > 0)
            {
                try
                {
                    lastRowId = Convert.ToInt32(dtLastTransaction.Rows[0]["rowid"]);
                }
                catch
                {
                    lastRowId = 0;
                }
                //
                if (iType == 1)
                    dateFromTransaction = Convert.ToDateTime(dtLastTransaction.Rows[0]["E_DateTime"]);
                else if (iType == 2)
                    dateFromTransaction = Convert.ToDateTime(dtLastTransaction.Rows[0]["w_datetime"]);
                else if (iType == 3)
                    dateFromTransaction = Convert.ToDateTime(dtLastTransaction.Rows[0]["End_Date"]);
            }
            //
            return lastRowId;
        }

        DateTime ParseDateTime(string sDate, string sTime)
        {
            DateTime d = DateTime.MinValue;
            //
            try
            {
                d = DateTime.Parse(sDate + " " + sTime);
            }
            catch { }
            //
            return d;
        }

        string GetMeterType(int iType)
        {
            string sType = "E-Meter";
            if (iType == 2)
                sType = "Water-Meter";
            else
                sType = "Phone-Meter";
            //
            return sType;
        }

        string LoadingTransaction = "ADC [{0}] loading {1} transaction";
        string LoadingTransaction2 = "ADC [{0}] loading {1} transaction {2} - {3}";
        string TimeFormat = "ddMMyyyy HH:mm";

        void UpdateBGStatusTransaction(string adcip, int iType, DateTime dBegin, DateTime dEnd)
        {
            barStaticItemTime.Caption = string.Format(LoadingTransaction,
                adcip, GetMeterType(iType), dBegin.ToString(TimeFormat), dEnd.ToString(TimeFormat));
        }

        void UpdateBGStatusTransaction2(string adcip, int iType)
        {
            barStaticItemTime.Caption = string.Format(LoadingTransaction2,
                adcip, GetMeterType(iType));
        }

        void UpdateBGStatus(string s)
        {
            barStaticItemTime.Caption = s;
        }

    }

    public class MyLocalizer : GridLocalizer
    {

        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {

                case GridStringId.FindControlFindButton:
                    return MainForm.getLanguage("_find");
                case GridStringId.FindControlClearButton:
                    return MainForm.getLanguage("_clear");

            }

            return base.GetLocalizedString(id);

        }

    }


} // End namespace

public class objMEATHLicense
{
    public string ProductID { get; set; }
    public string LicenseKey { get; set; }
    public string ADCSN1 { get; set; }
    public string ADCSN2 { get; set; }
    public string ADCSN3 { get; set; }
    public string ADCSN4 { get; set; }
    public string ADCSN5 { get; set; }
}

public static class Extensions
{
    public static T To<T>(this Object obj)
    {
        return obj.ToString().To<T>();
    }
    public static T To<T>(this IConvertible obj)
    {
        T _t;
        try
        {
            _t = (T)Convert.ChangeType(obj, typeof(T));
        }
        catch
        {
            _t = (T)Convert.ChangeType("0", typeof(T));
        }
        //
        return _t;
    }

}


