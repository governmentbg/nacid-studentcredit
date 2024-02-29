using System.ComponentModel;

namespace StudentCredit.Data.Applications.Common.Enums
{
    public enum ApplicationHistoryType
    {
        [Description("Върнато за редакция")]
        Modification = 1,

        [Description("Администраторска редакция")]
        Edit = 2
    }
}
