using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public class uFormBase : XtraForm
    {
        public string current_lang = DXWindowsApplication2.MainForm.current_lang;
        //
        public string getLanguage(string x)
        {
            return DXWindowsApplication2.MainForm.getLanguage(x);
        }

        public string getLanguageWithColon(string x)
        {
            return DXWindowsApplication2.MainForm.getLanguageWithColon(x);
        }

        public string UserID
        {
            get
            {
                return DXWindowsApplication2.MainForm.userid;
            }
        }

        public DataTable GeneralSettingTable
        {
            get
            {
                return DXWindowsApplication2.MainForm.generalSettingTable;
            }
        }

        public string CurrentPassword
        {
            get
            {
                return DXWindowsApplication2.MainForm.password;
            }
            set
            {
                DXWindowsApplication2.MainForm.password = value;
            }
        }

        public string UserName
        {
            get
            {
                return DXWindowsApplication2.MainForm.username;
            }
        }

        public MainForm MForm
        {
            get
            {
                return Parent.TopLevelControl as MainForm;
            }
        }

        
    }
}
