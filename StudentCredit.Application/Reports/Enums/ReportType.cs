using System.ComponentModel;

namespace StudentCredit.Application.Reports.Enums
{
    public enum ReportType
    {
        [Description("Справка за сключени договори за кредит")]
        NewContract = 1,

        [Description("Справка за погасени кредити")]
        Deimbursed = 2,
        
        [Description("Справка „Предсрочно изискуеми кредити“")]
        EarlyDellability = 3,

        [Description("Справка „Предговаряне на условията“")]
        Renegotiation = 4
    }
}
