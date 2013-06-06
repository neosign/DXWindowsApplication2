using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DXWindowsApplication2.UserForms
{
    public partial class BookRefund : DevExpress.XtraEditors.XtraUserControl
    {
        public static DevExpress.XtraGrid.GridControl gridControlNick;
        public static GridView gridViewNick;

        public BookRefund()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.reloadGrid();

            gridViewNick = gridView1;

            DataRow CurrentRow = gridViewNick.GetDataRow(0);

            if (CurrentRow != null)
            {
                if (CurrentRow["coderef"].ToString() != "")
                {
                    textEditReserveID.EditValue = CurrentRow["reserve_id"].ToString();
                    textEditRoomNo.EditValue = CurrentRow["coderef"].ToString();
                    textEditName.EditValue = CurrentRow["reserve_name"].ToString();
                    textEditAmount.EditValue = CurrentRow["reserve_payments"].ToString();

                    if (CurrentRow["reserve_flag"].ToString() != "0")
                    {

                        simpleButton2.Enabled = false;
                        simpleButton1.Enabled = false;
                    }
                    else
                    {
                        simpleButton2.Enabled = true;
                        simpleButton1.Enabled = true;
                    }

                }
            }
            

            gridViewNick.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rowIndex = gridViewNick.GetSelectedRows();
            
                if (rowIndex[0] >= 0)
                {

                    DataRow CurrentRow = gridViewNick.GetDataRow(rowIndex[0]);

                    if (CurrentRow != null)
                    {
                        if (CurrentRow["coderef"].ToString() != "")
                        {
                            textEditReserveID.EditValue = CurrentRow["reserve_id"].ToString();
                            textEditRoomNo.EditValue    = CurrentRow["coderef"].ToString();
                            textEditName.EditValue      = CurrentRow["reserve_name"].ToString();
                            textEditAmount.EditValue    = CurrentRow["reserve_payments"].ToString();

                            if (CurrentRow["reserve_flag"].ToString() != "0")
                            {

                                simpleButton2.Enabled = false;
                                simpleButton1.Enabled = false;
                            }
                            else {
                                simpleButton2.Enabled = true;
                                simpleButton1.Enabled = true;
                            }

                        }
                    }
                }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("ยืนยันการคืนเงินจอง", "", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {   
                // Update Flag Reserve
                // Add BookRefund Reciept Transaction
                // Type 0 = BookRefund ,Type 1 = Create Reserve
                
                BusinessLogicBridge.DataStore.BookRefund(textEditRoomNo.Text, textEditName.Text, textEditAmount.Text, 0, Convert.ToInt16(textEditReserveID.Text));
                //BusinessLogicBridge.DataStore.CreateRefundReceipt(textEditRoomNo.Text, textEditName.Text, textEditAmount.Text, Convert.ToInt16(textEditReserveID.Text));
                
                reloadGrid();
                this.print();
            }
        }

        public void reloadGrid() {

            DataTable dtBookRefund = BusinessLogicBridge.DataStore.getRoomReserve();
            int amountrows = dtBookRefund.Rows.Count;

            dtBookRefund.Columns.Add("reserve_flag_text", typeof(string));

            for (int i = 0; i < amountrows; i++)
            {
                if (dtBookRefund.Rows[i]["reserve_flag"].ToString() == "0")
                {
                    dtBookRefund.Rows[i]["reserve_flag_text"] = "จอง";
                }
                else
                {
                    dtBookRefund.Rows[i]["reserve_flag_text"] = "คืนเงินจองแล้ว";
                }
            }
            gridControlNick = gridControl1;
            gridControlNick.DataSource = dtBookRefund;

        }


        public void print(){

            XtraMessageBox.Show("พิมพ์ใบเสร็จคืนเงินจอง");
        }

    }
}
