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
using static TerminalFAtest.Helpers.LogicLevel;

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
            CashierData cashierData = new CashierData(
                "Иванов И. И.", 
                "929655906720"
                );

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

            CashierData cashierData = new CashierData(
                "Иванов И. И.", 
                "929655906720"
                );

            ClientData clientData = new ClientData(
                "khamitovsv@brsc.ru", 
                "Клиент Петров П. П.", 
                "929655906720"
                );

            PaymentData paymentData = new PaymentData(
                TaxTypeEnum.Common,
                184.50M,
                0,
                0,
                0,
                0,
                cashierData,
                clientData,
                "Доп реквизит для печати",
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

        private void SendAutomaticDeviceData_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();

            AutomaticDeviceData automaticDeviceData = new AutomaticDeviceData(
                "ул. АвтоматоРасчетная, 10",
                "место - Расчетный автомат",
                "АБВ-123456"
                );
            BaseResponse br = kkt.SendAutomaticDeviceData(automaticDeviceData);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void GetFnNumber_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.GetFnNumber();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void GetKktStatus_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            GetKktStatusResponse br = kkt.GetKktStatus();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void GetRegistrationParameters_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            GetRegistrationParametersResponse br = kkt.GetRegistrationParameters();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void GetFirmwareVersion_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            GetFirmwareVersionResponse br = kkt.GetFirmwareVersion();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void OpenCorrectionCheck_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();
            BaseResponse br = kkt.OpenCorrectionCheck();
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void SendCorrectionCheckData_Click(object sender, EventArgs e)
        {
            AuthorizedPersonData apd = new AuthorizedPersonData("Иванов И. И.", "123456789012");

            var logicLevel = new LogicLevel();
            var _stlv = new List<byte[]>();
            byte[] docName = logicLevel.BuildTLV(1177, "Тестовая коррекция");
            byte[] docDate = logicLevel.BuildTLV(1178, 1583971200);
            byte[] docNumber = logicLevel.BuildTLV(1179, "1");
            _stlv.Add(docName);
            _stlv.Add(docDate);
            _stlv.Add(docNumber);

            byte[] stlv = logicLevel.BuildSTLV(1174, _stlv);

            CorrectionCheckData cd = new CorrectionCheckData(
                apd, 
                0, 
                TaxTypeEnum.Common,
                0,
                184.50M,
                0,
                0,
                0,
                184.50M,
                0,
                0,
                0,
                0,
                0,
                stlv
                );

            #region MyRegion
            /*
             
             */
            #endregion

            var kkt = new KKT();
            BaseResponse br = kkt.SendCorrectionCheckData(cd);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void SendCorrectionAutomaticDeviceData_Click(object sender, EventArgs e)
        {
            var kkt = new KKT();

            AutomaticDeviceData automaticDeviceData = new AutomaticDeviceData(
                "ул. АвтоматоРасчетная, 10",
                "место - Расчетный автомат",
                "АБВ-123456"
                );
            BaseResponse br = kkt.SendCorrectionAutomaticDeviceData(automaticDeviceData);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void RegisterCorrectionCheck_Click(object sender, EventArgs e)
        {
            // RegisterCorrectionCheckResponse
            var kkt = new KKT();

            TerminalFAtest.Models.KKTRequest.RegisterCorrectionCheck registerCorrectionCheck = new TerminalFAtest.Models.KKTRequest.RegisterCorrectionCheck(
                OperationEnum.Sell,
                184.50M
                );
            RegisterCorrectionCheckResponse br = kkt.RegisterCorrectionCheck(registerCorrectionCheck);
            string r = new KKTResponseHelper().BuildResponseString(br);
            textBoxLog.AppendText(r);
        }

        private void Test_Click(object sender, EventArgs e)
        {
            //DateTime dateTime = DateTime.ParseExact("14.07.2020", "dd.MM.yyyy", null);
            FNFlagsHelper fNFlagsHelper = new FNFlagsHelper();
            //string res = fNFlagsHelper.DecodeFNFlags(a);
            //MessageBox.Show(dateTime.Date.ToString());

            byte a = (byte)FNFlagsEnum.FNMEmoryFull;
            byte b = (byte)FNFlagsEnum.FNCriticalError;
            int r = a & b;

            MessageBox.Show(a + "(" + fNFlagsHelper.DecodeFNFlags(a) + ")" + " XOR " + b + "(" + fNFlagsHelper.DecodeFNFlags(b) + ")" + " = " + r + "(" + fNFlagsHelper.DecodeFNFlags((byte)r) + ")");
        }

        private void GetKktInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
