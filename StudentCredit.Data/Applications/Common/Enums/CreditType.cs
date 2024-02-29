using System.ComponentModel;

namespace StudentCredit.Data.Applications.Common.Enums
{
    public enum CreditType
    {
        [Description("Заплащане на таксите за обучение")]
        EducationTaxes = 1,

        [Description("Издръжка")]
        Maintenance = 2
    }
}
