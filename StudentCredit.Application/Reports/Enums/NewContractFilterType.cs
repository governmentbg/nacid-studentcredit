using System.ComponentModel;

namespace StudentCredit.Application.Reports.Enums
{
    public enum NewContractFilterType
    {
        [Description("Студенти и докторанти")]
        StudentAndDoctoral = 1,

        [Description("Банка")]
        Bank = 2,

        [Description("ВУ/НО")]
        Institution = 3,

        [Description("Професионално направление")]
        ResearchArea = 4,

        [Description("Гражданство")]
        Nationality = 5,

        [Description("Възраст")]
        Age = 6,

        [Description("ВУ/НО (по години)")]
        InstitutionTypeByYear = 7
    }
}