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
    public partial class RoomCheckOutAddItem : UserControl
    {
        private static DataTable DocumentConfigTable;

        public RoomCheckOutAddItem()
        {
            InitializeComponent();
            DocumentConfigTable = BusinessLogicBridge.DataStore.RoomCheckOut_getDocumentConfig();
            if (int.Parse(DocumentConfigTable.Rows[0]["doc_vat_type"].ToString()) < 1)
            {
                radioGroupVat.Visible = false;
            }
            else
            {
                radioGroupVat.Visible = true;
            }
        }
        
        private DataTable validateDate()
        {
            String label;
            String message;

            DataTable _Error = new DataTable();
            _Error.Columns.Add("label", typeof(String));
            _Error.Columns.Add("message", typeof(String));
            if(textEditItemName.EditValue == null)
            {
                label = labelControlItemName.Text;
                message = "กรุณากรอกข้อมูลให้ครอบในช่องที่มีเครื่องหมาย \"*\"";
                _Error.Rows.Add(label, message);
            }
            if (textEditItemUnitPrice.EditValue.ToString().Length < 1)
            {
                label = labelControlItemPrice.Text;
                message = "กรุณากรอกข้อมูลให้ครอบในช่องที่มีเครื่องหมาย \"*\"";
                _Error.Rows.Add(label, message);
            }
            return _Error;
        }
        
        private void bttSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _Error = validateDate();
                if (_Error.Rows.Count > 0)
                {
                    String message = "";
                    for (int i = 0; i < _Error.Rows.Count; i++)
                    {
                        message = message + _Error.Rows[i]["label"] + " " + _Error.Rows[i]["message"].ToString() + "\r\n";
                    }
                    XtraMessageBox.Show(message, "!!! ข้อผิดพลาด !!!");
                } 
                else
                {
                    
                    String item_name = textEditItemName.EditValue.ToString();
                    int item_unit = int.Parse(textEditItemUnit.EditValue.ToString());
                    Double item_unit_price = Double.Parse(textEditItemUnitPrice.EditValue.ToString());
                    Double item_price = item_unit * item_unit_price;
                    Double item_vat = 0.0;
                    Double item_net_price = 0.0;
                    
                    if (int.Parse(DocumentConfigTable.Rows[0]["doc_vat_type"].ToString()) < 1)
                    {
                        item_net_price = item_price;
                    }
                    else
                    {
                        int vat_type = int.Parse(radioGroupVat.EditValue.ToString());
                        Double vat = Double.Parse(DocumentConfigTable.Rows[0]["doc_vat"].ToString());
                        switch (vat_type)
                        {
                            case 1:
                                item_vat = Math.Round((item_price * 7) / 100, 2);
                                item_net_price = item_price - item_vat;
                                break;
                            case 2:
                                item_vat = Math.Round((item_price * 7) / 100, 2);
                                item_net_price = item_price + item_vat;
                                break;
                        }
                    }
                    string item = textEditItemName.EditValue.ToString();
                    double price = double.Parse(textEditItemUnitPrice.EditValue.ToString());
                    //RoomCheckOut.dataItemsDynamicTable.Rows.Add(item_name, item_unit, item_unit_price, item_price, item_vat, item_net_price, item_type, item_enable_delete);
                     RoomCheckOut.AddPanel.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            RoomCheckOut.AddPanel.Close();
        }
    }
}
