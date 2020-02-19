﻿namespace TerminalFAtest.Enums
{ 
    public enum CommandEnum
    {
        GET_STATUS = 0x01, // Запрос статуса ККТ (0x01)
        GET_SERIAL_NUMBER = 0x02, // Запрос заводского номера ККТ(0x02)
        GET_MODEL = 0x04,
        GET_FISCAL_STORAGE_STATUS = 0x08,
        CANCEL_FISCAL_DOCUMENT = 0x10, // Отменяет любой начатый фискальный документ. Все данные, введенные с помощью команды "Передать данные документа" удаляются
        SEND_AUTOMATIC_DEVICE_DATA = 0x1F, // Передать данные автоматического устройства расчетов для кассового чека (0x1F)
        GET_SHIFT_INFO = 0x20, // Запрос параметров текущей смены (0x20)
        BEGIN_OPEN_SESSION = 0x21, // Начать открытие смены (0x21)
        OPEN_SESSION = 0x22, //Открыть смену (0x22)
        OPEN_CHECK = 0x23, // Открыть кассовый чек (0x23)
        BEGIN_CLOSE_SESSION = 0x29, // Начать закрытие смены (0x29)
        CLOSE_SESSION = 0x2A, // Закрыть смену (0x2A). Условия: должны быть выполнены команды 0x29 и 0x2F (если не в автоматическом режиме)
        BEGIN_CHECK = 0x23,
        CHECK_POSITION = 0x2B, // Передать данные предмета расчета (0x2B)
        AGENT_DATA = 0x2C,
        PAYMENT_DATA = 0x2D, // Передать Данные оплаты (0x2D)
        CASHIER_DATA = 0x2F, // Передать данные кассира(0x2F). Для ККТ в составе автоматических устройств самообслуживания - необязательно
        CHECK = 0x24, // Сформировать чек (0x24)
        PRINT = 0x61,
        CUT = 0x62,
        REGISTRATION_PARAMETERS = 0x0A,
        CANCEL_DOCUMENT = 0x10
    }
}