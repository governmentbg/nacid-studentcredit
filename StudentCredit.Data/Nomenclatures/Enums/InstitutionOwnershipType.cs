using System.ComponentModel;

namespace StudentCredit.Data.Nomenclatures.Enums
{
    public enum InstitutionOwnershipType
    {
        [Description("Държавни")]
        State = 1,

        [Description("Частни")]
        Private = 2,

        [Description("Кооперативна")]
        Cooperative = 3,

        [Description("Смесено участие")]
        MixedParticipation = 4
    }
}