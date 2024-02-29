using System.ComponentModel;

namespace StudentCredit.Data.Users.Enums
{
    public enum UserStatus
    {
        [Description("Активен")]
        Active = 1,

        [Description("Неактивиран")]
        Inactive = 2,

        [Description("Деактивиран")]
        Deactivated = 3
    }
}
