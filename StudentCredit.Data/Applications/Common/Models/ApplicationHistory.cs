using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.Common.Models
{
    public class ApplicationHistory : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public DateTime ModificationDate { get; set; }

        public ApplicationHistoryType ApplicationHistoryType { get; set; }

        public int ApplicationId { get; set; }

        public ApplicationType ApplicationType { get; set; }
    }
}
