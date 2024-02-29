using System.ComponentModel;

namespace StudentCredit.Application.Reports.Enums
{
    public enum LearnerType
    {
        [Description("Студенти")]
        Student = 1,

        [Description("Докторанти")]
        Doctoral = 2
    }
}