using System.ComponentModel;

namespace StudentCredit.Data.Common.Enums
{
    public enum CommitState
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Върнато за редакция")]
        Modification = 2,

        [Description("Изпратено за одобрение")]
        Pending = 3,

        [Description("Одобрено")]
        Approved = 4,

        [Description("Архивирано")]
        Archived = 5,

        [Description("Отхвърлено")]
        Denied = 6,

		[Description("В процес на редакция")]
		ApprovedModification = 7
    }
}
