using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.Common.Models
{
    public abstract class ApplicationCommit : IEntity, IAuditable
    {
        public int Id { get; set; }
        public CommitState CommitState { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorUserId { get; set; }
        public string ChangeStateDescription { get; set; }
        public int RootId { get; set; }
        public ApplicationHistory ApplicationHistory { get; set; }
        public int BankId { get; set; }
    }
}
