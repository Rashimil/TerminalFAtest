using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerminalFAtest.Enums;
using TerminalFAtest.Helpers;
using TerminalFAtest.Models.KKTRequest;
using TerminalFAtest.Models.KKTResponse;
using TerminalFAtest.Units;

namespace TerminalFAtest
{
    public partial class MainForm : Form
    {
        //TerminalFA terminalFA;

        public MainForm()
        {
            InitializeComponent();
            //terminalFA = new TerminalFA();
            //KKT.Dispose()
        }

        private void GetShiftInfo_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            var br = kkt.GetShiftInfo();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void OpenShiftBegin_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.OpenShiftBegin();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void SendCashierData_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            CashierData cashierData = new CashierData("Иванов И. И.", "929655906720");
            BaseResponse br = kkt.SendCashierData(cashierData);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void OpenShift_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            OpenShiftResponse br = kkt.OpenShift();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void CancelFiscalDocument_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.CancelFiscalDocument();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void CloseShiftBegin_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.CloseShiftBegin();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void CloseShift_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            CloseShiftResponse br = kkt.CloseShift();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void OpenCheck_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.OpenCheck();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void SendCheckPosition_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();

            CheckItem checkItemNEW = new CheckItem(
                "Сок апельсиновый",
                150.00M,
                1.23,
                VatEnum.Vat20,
                PaymentMethodEnum.FullPayment,
                PaymentObjectEnum.Commodity,
                //"05AB1208",
                "",
                "шт",
                0,
                "123456",
                "643",
                "CustomReq - доп реквизит бла бла бла"
                );


            BaseResponse br = kkt.SendCheckPosition(checkItemNEW);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void SendPaymentData_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();

            CashierData cashierData = new CashierData("Иванов И. И.", "929655906720");

            PaymentData paymentData = new PaymentData(
                TaxTypeEnum.Common,
                184.50M,
                0,
                0,
                0,
                0,
                "khamitovsv@brsc.ru",
                "",
                cashierData.CashierFIO,
                cashierData.CashierINN,
                "Клиент Петров П. П.",
                "Доп реквизит",
                "Наименование доп реквизита пользователя",
                "Значение доп реквизита пользователя"
                );
            BaseResponse br = kkt.SendPaymentData(paymentData);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void RegisterCheck_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();

            TerminalFAtest.Models.KKTRequest.RegisterCheck registerCheck = new TerminalFAtest.Models.KKTRequest.RegisterCheck(
                OperationEnum.Sell,
                184.50M,
                "Длинная строка для печати на чеке до 512 символов"
                );
            RegisterCheckResponse br = kkt.RegisterCheck(registerCheck);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }
    }
}
