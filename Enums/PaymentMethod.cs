﻿using System.ComponentModel;

namespace TerminalFAtest.Enums
{
    // Признаки способа расчета (Тэг 1214):
    public enum PaymentMethodEnum : byte 
    {
        [Description("Полная предварительная оплата до момента передачи предмета расчета")]
        FullPrepayment = 1,

        [Description("Частичная предварительная оплата до момента передачи предмета расчета")]
        Prepayment = 2,

        [Description("Аванс")]
        Advance = 3,

        [Description("Полная оплата, в том числе с учетом аванса (предварительной оплаты) в момент передачи предмета расчета")]
        FullPayment = 4,

        [Description("Частичная оплата предмета расчета в момент его передачи с последующей оплатой в кредит")]
        PartialPayment = 5,

        [Description("Передача предмета расчета без его оплаты в момент его передачи с последующей оплатой в кредит")]
        Credit = 6,

        [Description("Оплата предмета расчета после его передачи с оплатой в кредит (оплата кредита)")]
        CreditPayment = 7,

        [Description("Оплата предмета расчета (услуги) после завершения работ (чеканной монетой)")]
        CheckanCoinPayment = 99,
    }
}