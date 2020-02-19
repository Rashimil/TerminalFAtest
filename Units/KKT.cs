﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;
using TerminalFAtest.Helpers;
using TerminalFAtest.Models;
using TerminalFAtest.Models.KKTRequest;
using TerminalFAtest.Models.KKTResponse;

namespace TerminalFAtest.Units
{
    // Основной модуль работы с ККТ ( абстракция: ККТ => LogicLevel => ТерминалФА ):
    public class KKT
    {
        public bool IS_AUTOMATIC_MODE = true; // признак автоматического режима (брать с конфига, а в него - при первом старте считывать с ТерминалФА)
        LogicLevel logicLevel; // логический уровень
        public KKT()
        {
            logicLevel = new LogicLevel(); // 
        }

        //==============================================================================================================================================

        // 0 - Пробить тестовый чек. Использует все методы класса для пробития чека, проверки смены, и тд
        public void Check()
        {

        }

        //==============================================================================================================================================

        // 1 - Формирование кассового чека

        // Запрос параметров текущей смены (0x20)
        public GetShiftInfoResponse GetShiftInfo()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.GET_SHIFT_INFO);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                var response = new GetShiftInfoResponse(logicLevel);
                return response;
            }
            else
            {
                //return null;
                var response = new GetShiftInfoResponse(logicLevel);
                return response;
            }
        }

        // Начать открытие смены (0x21)
        // Время открытия смены может на 1 час отставать от времени закрытия предыдущей смены
        public BaseResponse OpenShiftBegin()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.BEGIN_OPEN_SESSION, new byte[1] { 0x01 }); // 0x01 - НЕ печатать отчет, 0x00 - ПЕЧАТАТЬ отчет
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Передать данные кассира(0x2F) (не обязательно для ККТ в составе автоматических устройств)
        public BaseResponse SendCashierData(CashierData cashierData) // !!! ТОЛЬКО ИНН ФИЗЛИЦА, НЕ ЮРЛИЦА !!!
        {
            logicLevel = new LogicLevel();

            byte[] cashier_fio = logicLevel.BuildTLV(1021, cashierData.CashierFIO); // ФИО кассира. TLV, TAG = 1021, max 64 символа
            byte[] cashier_inn = logicLevel.BuildTLV(1203, cashierData.CashierINN);// ИНН кассира. TLV, TAG = 1203, строго 12 символов (дополнять ведущими пробелами)
            byte[] DATA = (cashier_fio.Concat(cashier_inn)).ToArray();
            logicLevel.BuildRequestCommand((byte)CommandEnum.CASHIER_DATA, DATA);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Открыть смену (0x22)
        public OpenShiftResponse OpenShift()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.OPEN_SESSION);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new OpenShiftResponse(logicLevel);
            }
            else
            {
                //return null;
                return new OpenShiftResponse(logicLevel);
            }
        }

        // Начать закрытие смены (0x29)
        public BaseResponse CloseShiftBegin()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.BEGIN_CLOSE_SESSION);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Закрыть смену (0x2A)
        public CloseShiftResponse CloseShift()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.CLOSE_SESSION);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new CloseShiftResponse(logicLevel);
            }
            else
            {
                //return null;
                return new CloseShiftResponse(logicLevel);
            }
        }

        // Отменить текущий документ (0x10)
        public BaseResponse CancelFiscalDocument()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.CANCEL_FISCAL_DOCUMENT);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Открыть кассовый чек (0x23) 
        public BaseResponse OpenCheck()
        {
            logicLevel = new LogicLevel();
            logicLevel.BuildRequestCommand((byte)CommandEnum.OPEN_CHECK);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Передать данные предмета расчета (0x2B). Каждый предмет расчета передается отдельной командой 0x2B     
        public BaseResponse SendCheckPosition(CheckItem checkItem)
        {
            #region
            /*
            Успешный запрос:

            request:
            B6 29 start
            01 22 len
            2B command
                stlv:
                23 04 tag = 1059
                1D 01 len = 285 
                val:
                    tlv:
                        06 04 tag       = 1030 // Наименование предмета расчета
                        10 00 len       = 16
                        val:
                            91 AE AA 20 A0 AF A5 AB EC E1 A8 AD AE A2 EB A9 = Сок апельсиновый    
                    tlv:
                        37 04 tag       = 1079 // Цена за ед. предмета расчета (с учетом скидок и наценок)
                        02 00 len       = 2
                        val             = 150.00
                            98 3A 
                    tlv:
                        FF 03 tag       = 1023 // Количество предмета расчета
                        02 00 len       = 2 
                        val: 
                            00 01       = 1
                    tlv:
                        AF 04 tag       = 1199 // Ставка НДС
                        01 00 len       = 1
                        val:
                            01          = 1
                    tlv:
                        BE 04 tag       = 1214 // Признак способа расчета
                        01 00 len       = 1
                        val:
                            04          = 4
                    tlv:
                        BC 04 tag       = 1212 // Признак предмета расчета
                        01 00 len       = 1
                        val:
                            01          = 1
                    tlv:
                        8A 04 tag       = 1162 // Код товарной номенклатуры
                        08 00 len       = 8
                        val:
                            30 35 41 42 31 32 30 38 
                    tlv:
                        AD 04 tag       = 1197 // Единица измерения предмета расчета
                        00 00 len       = 0
                    tlv:
                        CD 04 tag       = 1229 // Акциз
                        01 00           = 1
                        val:
                            00          = 0
                    tlv:
                        CF 04 tag       = 1231 // Номер таможенной декларации
                        00 00 len       = 0 
                    tlv:
                        CE 04 tag       = 1230 // Код страны
                        00 00 len       = 0
                    tlv:
                        A7 04 tag       = 1191 // Доп. реквизит предмета расчета
                        00 00 len       = 0
                    tlv:
                        C6 04 tag       = 1222 // Признак агента по предмету расчета
                        01 00           = 1
                        00              = 0
                    tlv:
                        33 04 tag       = 1075 // Телефон оператора перевода
                        0A 00 len       = 10
                        val:
                            39 32 30 39 39 39 39 39 39 39 
                    tlv:
                        14 04 tag       = 1044 // Операция платежного агента
                        18 00 len       = 24
                        val:
                            AF A5 E0 A5 A2 AE A4 20 A4 A5 
                            AD A5 A6 AD EB E5 20 E1 E0 A5 
                            A4 E1 E2 A2 
                    tlv:
                        31 04 tag       = 1073 // Телефон платежного агента
                        0A 00 len       = 10
                        val:
                            39 32 30 38 37 38 33 32 31 30 
                    tlv:
                        32 04 tag       = 1074 // Телефон оператора по приему платежей
                        0B 00 len       = 11
                        val:
                            37 34 39 35 39 36 37 30 32 32 30 
                    tlv:
                        02 04 tag       = 1026 // Наименование оператора перевода
                        0E 00 len       = 14
                        val:
                            8E 8E 8E 20 8A 81 20 8F 8B 80 92 88 8D 80 
                    tlv: 
                        ED 03 tag       = 1005 // Адрес оператора перевода
                        33 00 len       = 51 
                        val:
                            A3 2E 8C AE E1 AA A2 A0 2C 20 
                            96 8C 92 2D 32 2C 20 8A E0 A0 
                            E1 AD AE AF E0 A5 E1 AD A5 AD 
                            E1 AA A0 EF 20 AD A0 A1 A5 E0 
                            A5 A6 AD A0 EF 2C 20 A4 2E 31 
                            32 
                    tlv:
                        F8 03 tag       = 1016 // ИНН оператора перевода
                        0A 00 len       = 10 
                        val: 
                            37 37 30 35 30 31 32 32 31 36 
                    tlv:
                        93 04 tag       = 1171 // Телефон поставщика
                        0B 00 len       = 11
                        val:
                            38 38 30 30 35 35 30 30 35 30 30 
                    tlv:        
                        C9 04 tag       = 1225 // Наименование поставщика
                        07 00 len       = 7 
                        val: 
                            8F 80 8E 20 8C 92 91 
                    tlv:         
                        CA 04 tag       = 1226 // ИНН поставщика
                        0C 00 len       = 12
                        val:
                            20 20 20 20 20 20 20 20 20 20 20 20 
            0A A0 crc

             */

            #endregion

            logicLevel = new LogicLevel();
            var stlvList = new List<byte[]>();
            byte[] tlv;
            foreach (var item in checkItem.GetType().GetProperties())
            {
                try
                {
                    var val = item.GetValue(checkItem).GetType().GetProperties();
                    var TAG = (int)val[0].GetValue(item.GetValue(checkItem));
                    var USER_VALUE = val[1].GetValue(item.GetValue(checkItem));
                    tlv = logicLevel.BuildTLV(TAG, USER_VALUE);
                    stlvList.Add(tlv);
                }
                catch (Exception)
                {
                }          
            }
            byte[] DATA = logicLevel.BuildSTLV(1059, stlvList);
            logicLevel.BuildRequestCommand((byte)CommandEnum.CHECK_POSITION, DATA);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Передать Данные платежного агента (0x2С)

        // Передать Данные оплаты (0x2D)
        public BaseResponse SendPaymentData(PaymentData paymentData)
        {
            #region
            /*
                B6 29 start
                00 5A len(BE!)
                2D command
                    1F 04 tag = 1055 // Режим налогообложения
                    01 00 len = 1 
                        01 
                    07 04 tag = 1031 // Наличными
                    02 00 len = 2
                        98 3A = 15000 коп
                    39 04 tag = 1081 // Безналичными
                    01 00 
                        00 
                    BF 04 tag = 1215 // Предоплатой (зачетом аванса)
                    01 00 
                        00 
                    C0 04 tag = 1216 // Постоплатой (в кредит)
                    01 00 
                        00 
                    C1 04 tag = 1217 // Встречным предоставлением
                    01 00 
                        00 
                    F0 03 tag = 1008 // Адрес электронной почты клиента
                    12 00 len = 18 
                        6B 68 61 6D 69 74 6F 76 73 76 40 62 72 73 63 2E 72 75 
                    CC 04 tag = 1228 // ИНН покупателя
                    0C 00 len = 12
                        20 20 20 20 20 20 20 20 20 20 20 20 
                    CB 04 tag = 1227 // Покупатель
                    0C 00 len = 12
                        84 A6 AE AD AD 20 8A AE AD AD AE E0 = Джонн Коннор
                    A8 04 tag = 1192 // Доп. реквизит кассового чека
                    00 00 len = 0
                D1 E8 crc
             */
            #endregion

            logicLevel = new LogicLevel();
            var tlvList = new List<byte>();
            byte[] tlv;
            foreach (var item in paymentData.GetType().GetProperties())
            {
                try
                {
                    var val = item.GetValue(paymentData).GetType().GetProperties();
                    var TAG = (int)val[0].GetValue(item.GetValue(paymentData));
                    var USER_VALUE = val[1].GetValue(item.GetValue(paymentData));
                    tlv = logicLevel.BuildTLV(TAG, USER_VALUE);
                    tlvList.AddRange(tlv);
                }
                catch (Exception)
                {
                }
            }
            byte[] DATA = tlvList.ToArray();
            logicLevel.BuildRequestCommand((byte)CommandEnum.PAYMENT_DATA, DATA);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new BaseResponse(logicLevel);
            }
            else
            {
                //return null;
                return new BaseResponse(logicLevel);
            }
        }

        // Передать данные автоматического устройства расчетов для кассового чек (0x1F)
        public BaseResponse SendAutomaticDeviceData()
        {
            logicLevel = new LogicLevel();
            var tlvList = new List<byte>();
            byte[] tlv;

            // SEND_AUTOMATIC_DEVICE_DATA
            return null;
        }

        // Сформировать чек (0x24)
        public RegisterCheckResponse RegisterCheck(RegisterCheck registerCheck)
        {
            logicLevel = new LogicLevel();
            var list = new List<byte>();
            foreach (var item in registerCheck.GetType().GetProperties())
            {
                try
                {
                    var USER_VALUE = item.GetValue(registerCheck);
                    list.AddRange(logicLevel.ConvertToByteArray(USER_VALUE));
                }
                catch (Exception)
                {
                }
            }
            byte[] DATA = list.ToArray();
            logicLevel.BuildRequestCommand((byte)CommandEnum.CHECK, DATA);
            var LLResponse = logicLevel.SendRequestCommand();
            if (LLResponse.connectResult.Connected)
            {
                return new RegisterCheckResponse(logicLevel);
            }
            else
            {
                //return null;
                return new RegisterCheckResponse(logicLevel);
            }
        }

        //==============================================================================================================================================

        // 2 - Формирование кассового чека коррекции
        // 3 - Формирование отчета о состоянии расчетов
        // 4 - Получение данных из архива ФН
        // 
        // 

        //==============================================================================================================================================


        //==============================================================================================================================================
    }
}
