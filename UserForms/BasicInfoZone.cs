using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXWindowsApplication2.UserForms
{
    public partial class BasicInfoZone : DevExpress.XtraEditors.XtraUserControl
    {   

        private DataTable CreateTable(int RowCount)
        {
            System.Data.DataTable tbl = new DataTable();
            tbl.Columns.Add("colZoneId");
            tbl.Columns.Add("colZoneName");

            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { i, String.Format("colZoneName{0}", i) });
            return tbl;
        }

        public BasicInfoZone()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            DataTable _roomTable = new DataTable();
            DataTable _ListTable = new DataTable();
            
            _roomTable.Columns.Add(colZoneId.FieldName);
            _roomTable.Columns.Add(colZoneName.FieldName);
            //_roomTable.Columns.Add("ROOMNAME");
            //_ListTable.Columns.Add("Zone", typeof(string));

            for (int i = 0; i < 100; i++)
            {

                //gen.GetHashCode();
                DXWindowsApplication2.RoomList roomObj = new DXWindowsApplication2.RoomList();
                roomObj.Roomid = i;
                //roomObj.Roomstatus = "ว่าง"+i;
                _roomTable.Rows.Add(roomObj.Roomid);
               
                
                
                // _ListTable.Rows.Add(roomObj.Roomstatus);
            }

             gridControl1.DataSource = _roomTable;
             
             repositoryItemLookUpEdit2.DataSource = CreateTable(20);
             
        }

    }
}
