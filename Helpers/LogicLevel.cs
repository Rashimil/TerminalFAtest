using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;
using TerminalFAtest.Extensions;
using TerminalFAtest.Models;
using TerminalFAtest.Units;

namespace TerminalFAtest.Helpers
{
    // Работа с байт-запросами/ответами (логический уровень)
    public class LogicLevel
    {
        TerminalFA terminalFA;
        public byte[] requestCommand; // команда запроса в ККТ [START(2 байта) LENGTH(2 байта) CMD(1 байт) DATA(N байт, N <= 1023) CRC(2 байта)]
        public byte[] responseCommand; // команда ответа от ККТ [START(2 байта) LENGTH(2 байта) RESULT(1 байт) DATA(N байт, N <= 1023) CRC(2 байта)]
        public byte[] tLV;
        public Request request; // запрос в ККТ как структра состояния класса
        public Response response; // ответ от ККТ как структура состояния класса
        public TLV tlv;
        public STLV stlv;
        public TerminalFASettings terminalFASettings;

        public _GetDataFromTLV GetDataFromTLV;
        public _ConvertFromByteArray ConvertFromByteArray;
        public struct Request
        {
            public Request(byte _CMD, byte[] DATA) : this()
            {
                var CMD = new byte[1] { _CMD };
                if (DATA.Length > 1023) DATA = DATA.Take(1023).ToArray(); // DATA(N байт, N <= 1023) 
                this.START = new byte[2] { 0xB6, 0x29 };
                this.LENGTH = new byte[2] { (byte)((CMD.Length + DATA.Length) >> 8), (byte)(DATA.Length + CMD.Length) };
                this.CMD = CMD;
                this.DATA = DATA;
                var ForCRC = new List<byte>();
                ForCRC.AddRange(LENGTH);
                ForCRC.AddRange(CMD);
                ForCRC.AddRange(DATA);
                this.CRC = new CRC16CCITT().ComputeCheckSumBytes(ForCRC.ToArray());
            }
            public byte[] START;
            public byte[] LENGTH;
            public byte[] CMD;
            public byte[] DATA;
            public byte[] CRC;
        }
        public struct Response
        {
            public Response(byte[] responseCommand) : this()
            {
                this.START = responseCommand.Take(2).ToArray();
                this.LENGTH = responseCommand.Skip(2).Take(2).ToArray();
                this.RESULT = responseCommand.Skip(4).Take(1).ToArray();
                this.DATA = responseCommand.Skip(5).Take(responseCommand.Length - 7).ToArray(); // -7 именно, чтоб в DATA не было RESULT
                this.CRC = responseCommand.Skip(responseCommand.Length - 2).Take(2).ToArray();

                this.responseCommand = responseCommand;
            }
            public byte[] START;
            public byte[] LENGTH;
            public byte[] RESULT;
            public byte[] DATA;
            public byte[] CRC;

            // добавь методы его получения
            private byte[] responseCommand;

            // Проверка ответа ККТ на валидность
            public bool IsValid()
            {
                byte[] START_BYTES = new byte[] { 0xB6, 0x29 };
                if (responseCommand.Length < 7) return false;
                if (!(responseCommand[0] == START_BYTES[0] && responseCommand[1] == START_BYTES[1])) return false;

                int length = responseCommand[2];
                length = length >> 8;
                length |= responseCommand[3];
                if (responseCommand.Length - 6 != length) return false;

                byte[] crc = new CRC16CCITT().ComputeCheckSumBytes(responseCommand.Skip(2).Take(responseCommand.Length - 4).ToArray());
                if (!(crc[0] == responseCommand[responseCommand.Length - 2] && crc[1] == responseCommand[responseCommand.Length - 1])) return false;

                return true;
            }
        }
        public struct TLV
        {
            public TLV(byte[] TAG, byte[] VALUE) : this()
            {
                this.TAG = TAG;
                this.LENGTH = new byte[2] { (byte)VALUE.Length, (byte)(VALUE.Length >> 8) };
                this.VALUE = VALUE;
            }
            public byte[] TAG;
            public byte[] LENGTH;
            public byte[] VALUE;
        }
        public struct STLV
        {
            public STLV(byte[] TAG, List<byte[]> VALUE) : this() // List<byte[]> составляется из ответов на BuildTLV
            {
                int stlv_len = VALUE.ToArray().Length;
                //byte[] value = new byte[stlv_len];
                List<byte> templist = new List<byte>();

                foreach (var item in VALUE)
                {
                    templist.AddRange(item);
                    //stlv_len += BitConverter.ToInt32(item.LENGTH, 0);
                }


                this.TAG = TAG;
                // Оба варианта одинаковы:
                //byte[] L1 = BitConverter.GetBytes(Convert.ToUInt16(templist.ToArray().Length));
                //byte[] L2 = new byte[2] { (byte)templist.ToArray().Length, (byte)(templist.ToArray().Length >> 8) };
                this.LENGTH = new byte[2] { (byte)templist.ToArray().Length, (byte)(templist.ToArray().Length >> 8) };
                this.VALUE = templist.ToArray();
            }
            public byte[] TAG;
            public byte[] LENGTH;
            public byte[] VALUE;

        }

        //==============================================================================================================================================

        public LogicLevel()
        {
            terminalFASettings = new TerminalFASettings();
            terminalFA = new TerminalFA(terminalFASettings); // физический уровень (ВРЕМЕННО НАСТРОЙКИ БЕРЕМ ТАК)
            GetDataFromTLV = new _GetDataFromTLV();
            ConvertFromByteArray = new _ConvertFromByteArray();
        }

        //==============================================================================================================================================

        // Построение запроса в ТерминалФА:
        public byte[] BuildRequestCommand(byte CMD)
        {
            return BuildRequestCommand(CMD, new byte[0]);
        }
        public byte[] BuildRequestCommand(byte CMD, byte[] DATA)
        {
            request = new Request(CMD, DATA);
            var requestCommandList = new List<byte>();
            requestCommandList.AddRange(request.START);
            requestCommandList.AddRange(request.LENGTH);
            requestCommandList.AddRange(request.CMD);
            requestCommandList.AddRange(request.DATA);
            requestCommandList.AddRange(request.CRC);

            requestCommand = requestCommandList.ToArray();
            return requestCommand;
        }

        //==============================================================================================================================================

        // Построение структуры TLV:
        //public byte[] BuildTLV_OLD<T>(int TAG, T VALUE) // where T : Object
        //{
        //    byte[] tag = new byte[] { (byte)TAG, (byte)(TAG >> 8) };
        //    tlv = new TLV(new byte[0], new byte[0]);
        //    var tlvList = new List<byte>();

        //    Type t = typeof(T); // VALUE.GetType();
        //    if (t == typeof(string))
        //    {
        //        byte[] value = Encoding.GetEncoding(866).GetBytes(VALUE as string).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(byte))
        //    {
        //        byte[] value = new byte[1] { Convert.ToByte(VALUE) };
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(byte[]))
        //    {
        //        byte[] value = (VALUE as byte[]).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(ushort))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToUInt16(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(short))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToInt16(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(int))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToInt32(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(uint))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToUInt32(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(ulong))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToUInt64(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(long))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToInt64(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(float))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToSingle(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }
        //    else if (t == typeof(double))
        //    {
        //        byte[] value = BitConverter.GetBytes(Convert.ToDouble(VALUE)).XReverse().ToArray();
        //        tlv = new TLV(tag, value);
        //    }

        //    GetDataFromTLV = new _GetDataFromTLV(tlv);

        //    tlvList.AddRange(tlv.TAG);
        //    tlvList.AddRange(tlv.LENGTH);
        //    tlvList.AddRange(tlv.VALUE);

        //    return tlvList.ToArray();
        //}

        public byte[] BuildTLV(int TAG, object VALUE) // без Generic
        {
            byte[] tag = new byte[] { (byte)TAG, (byte)(TAG >> 8) };
            tlv = new TLV(new byte[0], new byte[0]);
            var tlvList = new List<byte>();

            Type t = VALUE.GetType();
            if (t == typeof(string))
            {
                byte[] value = Encoding.GetEncoding(866).GetBytes(VALUE as string).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(byte))
            {
                byte[] value = new byte[1] { Convert.ToByte(VALUE) };
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(byte[]))
            {
                byte[] value = (VALUE as byte[]).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(ushort))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToUInt16(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(short))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToInt16(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(int))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToInt32(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(uint))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToUInt32(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(ulong))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToUInt64(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(long))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToInt64(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(float))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToSingle(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(double))
            {
                byte[] value = BitConverter.GetBytes(Convert.ToDouble(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            else if (t == typeof(decimal))
            {
                byte[] value = BitConverter.GetBytes((double)Convert.ToDecimal(VALUE)).XReverse().ToArray();
                tlv = new TLV(tag, value);
            }
            GetDataFromTLV = new _GetDataFromTLV(tlv);

            tlvList.AddRange(tlv.TAG);
            tlvList.AddRange(tlv.LENGTH);
            tlvList.AddRange(tlv.VALUE);

            return tlvList.ToArray();
        }

        //==============================================================================================================================================

        // Построение структуры STLV
        public byte[] BuildSTLV(int TAG, List<byte[]> VALUE) // List<byte[]> составляется из ответов на BuildTLV
        {
            byte[] tag = new byte[] { (byte)TAG, (byte)(TAG >> 8) };
            stlv = new STLV(tag, VALUE);
            var stlvList = new List<byte>();
            stlvList.AddRange(stlv.TAG);
            stlvList.AddRange(stlv.LENGTH);
            stlvList.AddRange(stlv.VALUE);
            return stlvList.ToArray();
        }

        //==============================================================================================================================================

        // Возвращает данные из TLV
        public class _GetDataFromTLV
        {
            TLV tlv;
            public _GetDataFromTLV()
            {
                // Не удалять пустой конструктор!!!
            }
            public _GetDataFromTLV(TLV tlv)
            {
                this.tlv = tlv;
            }

            public string AsString() // как строку
            {
                return Encoding.GetEncoding(866).GetString(tlv.VALUE);
            }
            public byte AsByte()
            {
                return tlv.VALUE[0];
            }
            public ushort AsUShort()
            {
                return BitConverter.ToUInt16(tlv.VALUE, 0);
            }
            public short AsShort()
            {
                return BitConverter.ToInt16(tlv.VALUE, 0);
            }
            public int AsInt()
            {
                return BitConverter.ToInt32(tlv.VALUE, 0);
            }
            public ulong AsULong()
            {
                return BitConverter.ToUInt64(tlv.VALUE, 0);
            }
            public long AsLong()
            {
                return BitConverter.ToInt64(tlv.VALUE, 0);
            }
            public float AsFloat()
            {
                return BitConverter.ToSingle(tlv.VALUE, 0);
            }
            public double AsDouble()
            {
                return BitConverter.ToDouble(tlv.VALUE, 0);
            }
        }

        //==============================================================================================================================================

        // Возвращает данные из STLV (НЕДОПИСАНО, т. к. хз что возвращать... мб List<TLV>() ??? )
        public class _GetDataFromSTLV
        {
            STLV stlv;
            public _GetDataFromSTLV()
            {
                // Не удалять пустой конструктор!!!
            }
            public _GetDataFromSTLV(STLV stlv)
            {
                this.stlv = stlv;
            }
        }

        //==============================================================================================================================================

        // Отправка запроса в ТерминалФА ( результат оказывается в responseCommand и response, попутно выполняется TerminalFA.Dispose() и TerminalFA = null)
        public LogicLevelResponse SendRequestCommand()
        {
            LogicLevelResponse result = new LogicLevelResponse();
            result.Set(ErrorCodeEnum.UnknownError); // по умолчанию - ошибка

            using (terminalFA = new TerminalFA(terminalFASettings)) // попытка через using
            {
                result.connectResult = terminalFA.Connect();
                if (result.connectResult.Connected) // если успешный коннект
                {
                    try
                    {
                        terminalFA.SendRequest(requestCommand); // отправка команды в аппарат
                        responseCommand = terminalFA.ReadResponse(); // чтения ответа с аппарата
                        if (responseCommand != null && responseCommand.Length >= 7) // минимум 2B START + 2B LENGTH + 1B RESULT + 2B CRC
                        {
                            response = new Response(responseCommand); // установка поля состояния ответа
                            result.Set((ErrorCodeEnum)response.DATA[0]); // 1 а не 0, т. к. 1 это результат. RESULT[0] = 0 - ОК, остальное - ошибка
                            return result;
                        }
                        else
                        {
                            result.Set(ErrorCodeEnum.UnknownError);
                            return result;
                        }
                    }
                    catch (Exception уч)
                    {
                        result.Set(ErrorCodeEnum.UnknownErrorExc);
                    }
                    finally
                    {
                        this.FADispose(); // на всякий :3
                    }
                }
                else // неуспешный коннект
                {
                    this.FADispose(); // на всякий :3
                }
            }
            return result;
        }

        //==============================================================================================================================================

        // Конвертация массива byte[] в разные форматы
        public class _ConvertFromByteArray
        {
            public string ToString(byte[] data) // как строку
            {
                return Encoding.GetEncoding(866).GetString(data);
            }
            public byte ToByte(byte[] data)
            {
                return data[0];
            }
            public ushort ToUShort(byte[] data)
            {
                return BitConverter.ToUInt16(data, 0);
            }
            public short ToShort(byte[] data)
            {
                return BitConverter.ToInt16(data, 0);
            }
            public int ToInt(byte[] data)
            {
                return BitConverter.ToInt32(data, 0);
            }
            public ulong ToULong(byte[] data)
            {
                return BitConverter.ToUInt64(data, 0);
            }
            public long ToLong(byte[] data)
            {
                return BitConverter.ToInt64(data, 0);
            }
            public float ToFloat(byte[] data)
            {
                return BitConverter.ToSingle(data, 0);
            }
            public double ToDouble(byte[] data)
            {
                return BitConverter.ToDouble(data, 0);
            }
        }

        //==============================================================================================================================================

        // Конвертация разных форматов в массив byte[]
        public byte[] ConvertToByteArray<T>(T VALUE)
        {
            Type t = typeof(T); // VALUE.GetType();
            byte[] value;
            if (t == typeof(string))
            {
                value = Encoding.GetEncoding(866).GetBytes(VALUE as string);
                return value;
            }
            else if (t == typeof(byte))
            {
                value = new byte[1] { Convert.ToByte(VALUE) };
                return value;
            }
            else if (t == typeof(ushort))
            {
                value = BitConverter.GetBytes(Convert.ToUInt16(VALUE));
                return value;
            }
            else if (t == typeof(short))
            {
                value = BitConverter.GetBytes(Convert.ToInt16(VALUE));
                return value;
            }
            else if (t == typeof(int))
            {
                value = BitConverter.GetBytes(Convert.ToInt32(VALUE));
                return value;
            }
            else if (t == typeof(ulong))
            {
                value = BitConverter.GetBytes(Convert.ToUInt64(VALUE));
                return value;
            }
            else if (t == typeof(long))
            {
                value = BitConverter.GetBytes(Convert.ToInt64(VALUE));
                return value;
            }
            else if (t == typeof(float))
            {
                value = BitConverter.GetBytes(Convert.ToSingle(VALUE));
                return value;
            }
            else if (t == typeof(double))
            {
                value = BitConverter.GetBytes(Convert.ToDouble(VALUE));
                return value;
            }
            return null;
        }

        //==============================================================================================================================================
        private void FADispose()
        {
            terminalFA.Dispose();
            terminalFA = null;
        }

    }
}
