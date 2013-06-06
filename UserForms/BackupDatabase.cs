using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using System.IO;

namespace DXWindowsApplication2.UserForms
{
    public partial class BackupDatabase : uBase
    {
        private readonly BackgroundWorker _progressbar = new BackgroundWorker();
        private readonly BackgroundWorker _progressbar_restore = new BackgroundWorker();
        private string backupPath = "";
        private string backupFile = "";
        
        public BackupDatabase()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(BackupDatabase_Load);
            //
            this.checkBoxAuto.CheckedChanged += new EventHandler(checkBoxAuto_CheckedChanged);
            this.rbEveryDay.CheckedChanged += new EventHandler(rbEveryDay_CheckedChanged);
            //
            this.bttSave.Click += new EventHandler(bttSave_Click);
            this.bttBackup.Click += new EventHandler(bttBackup_Click);
            this.bttSelectFile.Click += new EventHandler(bttSelectFile_Click);
            this.bttImport.Click += new EventHandler(bttImport_Click);

            _progressbar.DoWork += new DoWorkEventHandler(_progressbar_DoWork);
            _progressbar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_progressbar_RunWorkerCompleted);
            _progressbar.WorkerReportsProgress = false;

            _progressbar_restore.DoWork += new DoWorkEventHandler(_progressbar_restore_DoWork);
            _progressbar_restore.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_progressbar_restore_RunWorkerCompleted);
            _progressbar_restore.WorkerReportsProgress = false;

        }

        void _progressbar_restore_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            backupFile = backupFile.Replace("\\\\", "\\");
            //
            
                try
                {
                    utilClass.DatabaseRestore(backupFile);
                    //
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_3013"), getLanguage("_softwarename"), "info");

                    BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "DB Setting [Restore Data]");

                    Application.Restart();
                }
                catch
                {
                    utilClass.showPopupMessegeBox(this, getLanguage("_msg_2002"), getLanguage("_softwarename"));
                }            
        }

        void _progressbar_restore_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000); 
        }

        void _progressbar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DXWindowsApplication2.UserForms.utilClass.CloseProgrssDailog(this);
            //
            backupPath = backupPath.Replace("\\\\", "\\");

            try
            {
                utilClass.DatabaseBackup(backupPath);

                utilClass.showPopupMessegeBox(this, getLanguage("_msg_3012"), getLanguage("_softwarename"), "info");
                BusinessLogicBridge.DataStore.addLogAction(DXWindowsApplication2.MainForm.groupid.To<int>(), DXWindowsApplication2.MainForm.userid.To<int>(), "DB Setting [Backup Data]");
                //
                System.Diagnostics.Process.Start(backupPath);
            }
            catch
            {
                utilClass.showPopupMessegeBox(this, getLanguage("_msg_2001"), getLanguage("_softwarename"));
            }
        }

        void _progressbar_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);           
        }

        public override void Refresh()
        {
            base.Refresh();
            //
            setLangThis();
        }

        void bttSelectFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            //
            fDialog.Description = "Select Backup Directory";
            fDialog.RootFolder = Environment.SpecialFolder.MyDocuments;

            DataTable PathSoftware = BusinessLogicBridge.DataStore.getGeneralConfig();

            string dataPath = MainForm.CombinePaths(AppDomain.CurrentDomain.BaseDirectory, "DatabaseBackup");

            if (Directory.Exists(dataPath) == false)
            {
                Directory.CreateDirectory(dataPath);
            }

            //
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                textEditDatapath.EditValue = fDialog.SelectedPath;
                textEditDatapath.EditValue = textEditDatapath.EditValue.ToString().Replace("\\\\", "\\");
            }
            //
            fDialog.Dispose();
        }

        void bttSave_Click(object sender, EventArgs e)
        {
            int a_enable = 0;
            int a_type = 1;
            TimeSpan a_time = timeEditEveryDay.Time.TimeOfDay;
            string a_dbpath = textEditDatapath.Text.ToString().Trim();
            //
            if (checkBoxAuto.Checked)
                a_enable = 1;
            if (rbEveryDay.Checked)
                a_type = 2;
            //
            try
            {
                BusinessLogicBridge.DataStore.deleteBackupConfig();
                //
                BusinessLogicBridge.DataStore.InsertBackupConfig(a_enable, a_type, a_time, a_dbpath);
                //
                XtraMessageBox.Show(getLanguage("_save_completed"));

            }
            catch
            {
                XtraMessageBox.Show(getLanguage("_failed_save"));
            }
        }

        void rbEveryDay_CheckedChanged(object sender, EventArgs e)
        {
            timeEditEveryDay.Enabled = rbEveryDay.Checked;
        }

        void checkBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            panelControlAuto.Enabled = checkBoxAuto.Checked;
            //
            bttSave.Enabled = checkBoxAuto.Checked;
        }

        void BackupDatabase_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //
            setLangThis();
            //
            setDefault();
            //
            loadData();
            //showData();
        }

        void loadData()
        {
            DataTable db = BusinessLogicBridge.DataStore.getBackupConfig();
            //
            if (db.Rows.Count == 0) return;
            //
            checkBoxAuto.Checked = db.Rows[0]["auto_enable"].ToString() == "1";
            rbEveryBoot.Checked = db.Rows[0]["auto_type"].ToString() == "1";
            rbEveryDay.Checked = db.Rows[0]["auto_type"].ToString() == "2";
            //
            timeEditEveryDay.EditValue = db.Rows[0]["auto_time"];
            textEditDatapath.EditValue = db.Rows[0]["auto_dbpath"];
        }

        void setDefault()
        {
            panelControlAuto.Enabled = false;
            timeEditEveryDay.Enabled = false;
            bttSave.Enabled = false;
        }

        public void setLangThis()
        {
            this.Name = getLanguage("_backup_restore_db");
            //
            this.groupControlTitle.Text = getLanguage("_backup_restore_db");
            //
            this.checkBoxAuto.Text = getLanguage("_automatic_backup_db");
            this.rbEveryBoot.Text = getLanguage("_everytime_start");
            this.rbEveryDay.Text = getLanguage("_everyday");
            //
            this.labelControlDataPath.Text = getLanguageWithColon("_path");
            //
            bttBackup.Text = getLanguage("_db_backup_user");
            bttImport.Text = getLanguage("_restore");
            bttSelectFile.Text = getLanguage("_select");
            //
            labelControlTimeRemark.Text = getLanguage("_backup_time_remark");
            labelBackupRemark.Text = getLanguage("_backup_db_now");
            labelImportRemark.Text = getLanguage("_restore_db");
        }

        void showData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("parent", typeof(string));
            dt.Columns.Add("info", typeof(string));
            //
            for (int i = 1; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "head " + i;
                dr[2] = "children " + i;
                dt.Rows.Add(dr);
                //
                for (int o = 1; o < 3; o++)
                {
                    DataRow dr2 = dt.NewRow();
                    dr2[0] = (10 * i) + o;
                    dr2[1] = "head " + i;
                    dr2[2] = o + " children " + i;
                    dt.Rows.Add(dr2);
                }
            }

        }

        void bttBackup_Click(object sender, EventArgs e)
        {

            backupPath = textEditDatapath.Text;
            //
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            //
            fDialog.Description = "Select Backup Directory";
            fDialog.SelectedPath = backupPath;
            //
            if (fDialog.ShowDialog() == DialogResult.OK)
            {

                // Progress Bar.... Loading
                DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                _progressbar.RunWorkerAsync();

                backupPath = fDialog.SelectedPath;
            }
            
        
        }

        void bttImport_Click(object sender, EventArgs e)
        {
            string backupPath = textEditDatapath.Text;
            backupFile = string.Empty;
            //
            OpenFileDialog fDialog = new OpenFileDialog();
            //
            fDialog.Title = "Select Backup Directory";
            fDialog.InitialDirectory = backupPath;
            fDialog.Multiselect = false;
            //
            if (fDialog.ShowDialog() == DialogResult.OK)
            {

                if (utilClass.showPopupConfirmBox(this, getLanguage("_msg_4011"), getLanguage("_softwarename")) == DialogResult.Yes)
                {
                    backupFile = fDialog.FileName;

                    // Progress Bar.... Loading
                    DXWindowsApplication2.UserForms.utilClass.showPopupProgressBar2(this);
                    _progressbar_restore.RunWorkerAsync();

                   
                }
                
            }
        }
    }
}
