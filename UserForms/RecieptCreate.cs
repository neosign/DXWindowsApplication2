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
    public partial class RecieptCreate : DevExpress.XtraEditors.XtraUserControl
    {
        public RecieptCreate()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            generateRecieptNo();
            reloadRoom(0, "all");
            initDropDownBuilding();
            initDropDownInvoiceNo();
            panelCashOnlyDisable.Enabled = false;
        }

        public void initDropDownInvoiceNo()
        {
            DateTime fromdate   = DateTime.Today.AddMonths(-12);
            DateTime todate     = DateTime.Today;

            DataTable Invoices = BusinessLogicBridge.DataStore.getInvoiceListByCondition(1, 0, fromdate.ToString("yyyy-MM-dd"), todate.ToString("yyyy-MM-dd"));
            lookUpEditInvoiceListNo.Properties.DataSource       = Invoices;
            lookUpEditInvoiceListNo.Properties.DisplayMember    = "inv_reciept_invoice_number";
            lookUpEditInvoiceListNo.Properties.ValueMember      = "inv_reciept_id";
        }

        public void initDropDownBuilding()
        {

            DataTable Buildings = BusinessLogicBridge.DataStore.getAllBuilding(1);
            lookUpEditBuilding.Properties.DataSource = Buildings;
            lookUpEditBuilding.Properties.DisplayMember = "building_label";
            lookUpEditBuilding.Properties.ValueMember = "building_id";
        }

        public void reloadRoom(int type, string stay)
        {

            DataTable roomList = BusinessLogicBridge.DataStore.getAllRoom(type, stay);
            gridLookUpEditRoom.Properties.DataSource = roomList;
            gridLookUpEditRoom.Properties.DisplayMember = "coderef";
            gridLookUpEditRoom.Properties.ValueMember = "room_id";

        }

        public void generateRecieptNo(){

            DataTable GetRecieptPrefix = BusinessLogicBridge.DataStore.getDocumentConfig();

            string dataRecieptNo = BusinessLogicBridge.DataStore.genRecieptNo();

            string Today = DateTime.Today.ToString("yyyyMMdd");

            textEditRecieptNo.EditValue = GetRecieptPrefix.Rows[0]["doc_reciept_prefix"] + Today + dataRecieptNo.ToString().PadLeft(6, '0');
            dateEditDateCreate.EditValue = DateTime.Today.ToString("dd/MM/yyyy");
            //ListReceipt.roomID
        }

        private void lookUpEditInvoiceListNo_EditValueChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(lookUpEditInvoiceListNo.EditValue);

            DataTable invoiceInfomation     = BusinessLogicBridge.DataStore.getInvoiceById(selectedValue);

            textEditTenantName.EditValue    = invoiceInfomation.Rows[0]["inv_reciept_tenant_name"] + " " + invoiceInfomation.Rows[0]["inv_reciept_tenant_surname"];
            lookUpEditBuilding.EditValue    = invoiceInfomation.Rows[0]["inv_reciept_building_id"];
            gridLookUpEditRoom.EditValue    = invoiceInfomation.Rows[0]["inv_reciept_room_id"];


            //DataTable CheckinInfo = BusinessLogicBridge.DataStore.getCheckinByContractNo(invoiceInfomation.Rows[0]["inv_reciept_contract_number"].ToString());

            textEditSumPrice.EditValue      = invoiceInfomation.Rows[0]["inv_reciept_sum_price"];
            textEditVatAmount.EditValue     = invoiceInfomation.Rows[0]["inv_reciept_vat"];
            textEditNetPrice.EditValue = invoiceInfomation.Rows[0]["inv_reciept_net_price"];

            

            textEditHdTenantName.EditValue      = invoiceInfomation.Rows[0]["inv_reciept_tenant_name"];
            textEditHdTenantSurName.EditValue   = invoiceInfomation.Rows[0]["inv_reciept_tenant_surname"];
            textEditHdTenantTel.EditValue       = invoiceInfomation.Rows[0]["inv_reciept_tenant_tel"];
            textEditHdTenantEmail.EditValue     = invoiceInfomation.Rows[0]["inv_reciept_tenant_email"];
            textEditTenantAddress.EditValue     = invoiceInfomation.Rows[0]["inv_reciept_tenant_address"];
            textEditHdInvoiceDateCreate.EditValue = invoiceInfomation.Rows[0]["inv_reciept_date_create"];

            gridControl2.DataSource = invoiceInfomation;
        }

        private void radioGroupPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt16(radioGroupPayment.EditValue);

            switch(selectedValue)
            {   
                case 0:
                    // Cash
                    panelCashOnlyDisable.Enabled    = false;
                    break;
                case 1:
                    // Cheque
                    panelCashOnlyDisable.Enabled = true;
                    panelChequeDisable.Enabled = true;
                    break;
                case 2:
                    // Credit Card
                    panelCashOnlyDisable.Enabled = true;
                    panelChequeDisable.Enabled = false;
                    break;
            }

        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            if (textEditNetPrice.EditValue != null)
            {
                if (Convert.ToDouble(textEditPaidAmount.EditValue) > Convert.ToDouble(textEditNetPrice.EditValue))
                {

                    try
                    {

                        if (textEditAmount.EditValue.ToString() == ""){
                            textEditAmount.EditValue = 0;
                        }
                        if (textEditVatRate.EditValue.ToString() == "")
                        {
                            textEditVatRate.EditValue = 0;
                        }
                        if (lookUpEditVatType.EditValue == null)
                        {
                            lookUpEditVatType.EditValue = 0;
                        }

                        // Create New Reciept
                        int receiptId = BusinessLogicBridge.DataStore.createNewReciept(
                            textEditRecieptNo.EditValue.ToString(),
                            gridLookUpEditRoom.Text.ToString(),
                            textEditHdTenantName.EditValue.ToString(),
                            textEditHdTenantSurName.EditValue.ToString(),
                            textEditHdTenantTel.EditValue.ToString(),
                            textEditHdTenantEmail.EditValue.ToString(),
                            textEditTenantAddress.EditValue.ToString(),
                            textEditHdInvoiceDateCreate.EditValue.ToString(),
                            DateTime.Today,
                            lookUpEditInvoiceListNo.Text.ToString(),
                            textEditBankname.EditValue.ToString(),
                            textEditChequeNo.EditValue.ToString(),
                            DateTime.Today,
                            Convert.ToDouble(textEditAmount.EditValue),
                            Convert.ToInt16(textEditVatRate.EditValue),
                            Convert.ToInt16(lookUpEditVatType.EditValue),
                            Convert.ToDouble(textEditSumPrice.EditValue),
                            Convert.ToDouble(textEditVatAmount.EditValue),
                            Convert.ToDouble(textEditNetPrice.EditValue),
                            Convert.ToDouble(textEditPaidAmount.EditValue),
                            Convert.ToDouble(textEditChangeAmount.EditValue),
                            0,
                            0);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }
                else
                {
                    XtraMessageBox.Show("จำนวนที่ชำระไม่พอกับค่าใช้จ่าย");
                }
            }
            else {
                XtraMessageBox.Show("กรุณาเลือกหมายเลขใบแจ้งหนี้");
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {

            ListReceipt.UpdatePanel.Close();

        }

        private void textEditPaidAmount_EditValueChanged(object sender, EventArgs e)
        {

            textEditChangeAmount.EditValue =  Convert.ToDouble(textEditPaidAmount.EditValue) - Convert.ToDouble(textEditNetPrice.EditValue);

        }
    }
}
