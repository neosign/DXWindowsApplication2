using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class PopupRegistration : uFormBase
    {
        public PopupRegistration()
        {
            InitializeComponent();
            this.Text = getLanguage("_softwarename");

            memoEditTrial.Text = getLanguage("_register_trial_text");
            btBrowse.Click +=new EventHandler(btBrowse_Click);

            textEditKey1.KeyUp += new KeyEventHandler(textEditKey1_KeyUp);
        }

        void textEditKey1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                string s = Clipboard.GetText();
                if (s == "")
                    return;
                //
                string[] sn = s.Trim().Split('-');
                if (sn.Length > 0)
                    textEditKey1.EditValue = sn[0];
                else
                    textEditKey1.EditValue = s;
                //
                if (sn.Length > 1)
                    textEditKey2.EditValue = sn[1];
                if (sn.Length > 2)
                    textEditKey3.EditValue = sn[2];
                if (sn.Length > 3)
                    textEditKey4.EditValue = sn[3];
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {

            if (radioGroupTrial.SelectedIndex == 1)
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

                if (textEditFile.EditValue == null)
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1022"), this.Text);
                    return;
                }

                if (textEditKey1.EditValue != null && textEditKey2.EditValue != null && textEditKey3.EditValue != null && textEditKey4.EditValue != null)
                {

                    string TextKey = textEditKey1.EditValue.ToString() + "-" + textEditKey2.EditValue.ToString() + "-" + textEditKey3.EditValue.ToString() + "-" + textEditKey4.EditValue.ToString();
                    if (TextKey == MainForm.LicObj.LicenseKey)
                    {
                        BusinessLogicBridge.DataStore.updateLicenceKey(MainForm.LicObj.LicenseKey);

                        DataTable GeneralPath = BusinessLogicBridge.DataStore.getGeneralConfig();

                        string dataPath = MainForm.CombinePaths(AppDomain.CurrentDomain.BaseDirectory, "Licence");

                        if (Directory.Exists(dataPath) == false)
                        {
                            Directory.CreateDirectory(dataPath);
                        }

                        try
                        {
                            string descnationfile = Path.Combine(dataPath, "regkey.lic");
                            System.IO.File.Copy(textEditFile.EditValue.ToString(), descnationfile, true);
                            objMEATHLicense LicObj = new objMEATHLicense();
                            LicObj = MainForm.readLicense(descnationfile);
                            BusinessLogicBridge.DataStore.updateADCSerial(LicObj.ADCSN1, LicObj.ADCSN2, LicObj.ADCSN3, LicObj.ADCSN4, LicObj.ADCSN5);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_3018"), this.Text, "info");

                        MainForm.TrialVersion = false;
                        MainForm.HaveLicence = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        utilClass.showPopupMessegeBox(this, getLanguage("_msg_1024"), this.Text);
                        this.DialogResult = DialogResult.Cancel;
                    }

                }
                else
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_1023"), this.Text);
                    return;
                }
            }
            else { 
                
                // Trial Version
                MainForm.TrialVersion = true;

                string s = BusinessLogicBridge.DataStore.getCheckinCounter();

                if (s == "")
                {
                    s = HelperEncrypt.Encrypt(s, MainForm.hashKey);

                    // Update Counter Checkin
                    BusinessLogicBridge.DataStore.updateCounterCheckin(s);
                }

                this.DialogResult = DialogResult.Ignore;

            }
            
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Text Files(*.lic)|*.lic";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file_info = new FileInfo(open.FileName);
                    string file_path = file_info.FullName;
                    MainForm.LicObj = MainForm.readLicense(file_path);
                    textEditFile.EditValue = file_path;
                }
            }
            catch (Exception ex)
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_2005"), getLanguage("_softwarename"));
                return;
            }

        }

        private void radioGroupTrial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radioGroupTrial.SelectedIndex == 1)
                panelLicense.Enabled = true;
            else
                panelLicense.Enabled = false;
        }
    }
}
