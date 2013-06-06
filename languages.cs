using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Resources;


namespace DXWindowsApplication2
{
    class languages
    {
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
               MessageBox.Show("Exception caught in process: {0}" + _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        public static void loadLanguage(string language) {

            // create dataset to hold csv data:
            DataTable langList = new DataTable();

            langList.Columns.Add("tab_menu", typeof(string));
            langList.Columns.Add("group_menu", typeof(string));
            langList.Columns.Add("index", typeof(string));

            langList.Columns.Add(language.ToLower(), typeof(string));

            string[] row = new string[1];

            string userPath = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ConfigFile = userPath + "/" + "Languages/lang.xlsx";
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ConfigFile + ";Extended Properties=Excel 12.0;");

            string folderSX = userPath;

            //if (Directory.Exists(folderSX) == false)
            //{
            //    Directory.CreateDirectory(folderSX);
            //    Directory.CreateDirectory(folderSX + "/Languages");

            //    byte[] _ByteArray = Properties.Resources.lang;

            //    ByteArrayToFile(folderSX + "/Languages/lang.xls", _ByteArray);
            //}
            //else if (Directory.Exists(folderSX + "/Languages") == false)
            //{
            //    Directory.CreateDirectory(folderSX + "/Languages");

            //    byte[] _ByteArray = Properties.Resources.lang;

            //    ByteArrayToFile(folderSX + "/Languages/lang.xls", _ByteArray);

            //}

            //if (Directory.Exists(folderSX + "/Contract") == false)
            //{
            //    Directory.CreateDirectory(folderSX + "/Contract");

            //    byte[] _ByteArrayContract = Properties.Resources.Contract_template;
            //    byte[] _ByteArrayBooking = Properties.Resources.Booking_template;

            //    ByteArrayToFile(folderSX + "/Contract/RentContract.doc", _ByteArrayContract);
            //    ByteArrayToFile(folderSX + "/Contract/ReservContract.doc", _ByteArrayBooking);

            //}
            //else {
            //    byte[] _ByteArrayContract = Properties.Resources.Contract_template;
            //    byte[] _ByteArrayBooking = Properties.Resources.Booking_template;

            //    ByteArrayToFile(folderSX + "/Contract/RentContract.doc", _ByteArrayContract);
            //    ByteArrayToFile(folderSX + "/Contract/ReservContract.doc", _ByteArrayBooking);
            //}


            if (File.Exists(folderSX + "/e-SmartBilling Manual.pdf") == false)
            {

                byte[] _ByteArrayDocument = Properties.Resources.a;

                ByteArrayToFile(folderSX + "/e-SmartBilling Manual.pdf", _ByteArrayDocument);

            }


            try
            {
                System.Drawing.Bitmap _ByteArrayNoImage = Properties.Resources.no_image;

                _ByteArrayNoImage.Save(folderSX + "/no-image.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch { }
    

            //if(File.Exists(ConfigFile)==false){
              ConfigFile = userPath + "/" + "Languages/lang.xls";
              con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ConfigFile + ";Extended Properties=Excel 8.0");
            //}

            OleDbDataAdapter da = new OleDbDataAdapter("select tab_menu,group_menu,index, " + language.ToLower() + " from [lang$];", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow DR in dt.Rows)
                DR["index"] = DR["index"].ToString().Trim();

            dt.AcceptChanges();
            langList = dt;

            DXWindowsApplication2.MainForm.translateTable = langList;

        }


    }
}
