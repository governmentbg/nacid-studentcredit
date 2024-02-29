using System.ComponentModel;

namespace StudentCredit.Data.Applications.ApplicationFour.Enums
{
    public enum ApplicationFourAttachedFileType
    {
        [Description("Извлечение от сметка по партидата на кредитополучателя с посочване на главницата и дължимите лихви към датата на съставяне на извлечението")]
        DebtStatementDocument = 1,

        [Description("Договор за кредит и анекс/и към него")]
        CreditContractAndAnnexes = 2,

        [Description("Изпълнителен лист срещу кредитополучателя, издаден в полза на банката")]
        DocumentInFavorOfTheBank = 3,

        [Description("Документи, удостоверяващи частично погасяване на задължението, ако е извършено такова")]
        DocumentPartialRepayment = 4,

        [Description("Копие от акта за смърт на длъжника")]
        DeathCertificate = 5,

        [Description("Копие от експертните решения на ТЕЛК или НЕЛК, удостоверяващи трайна неработоспособност 70 или над 70 на сто на длъжника")]
        PermanentIncapacity = 6,

        [Description("Копие от актовете за раждане/ от влезли в сила съдебни решения, с които е уважено искането за осиновяване, за две или повече деца.")]
        BirthCertificatesOrCourtDecisionsForAdoptions = 7
    }
}
