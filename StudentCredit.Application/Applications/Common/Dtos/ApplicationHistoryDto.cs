using StudentCredit.Data.Applications.Common.Enums;

namespace StudentCredit.Application.Applications.Common.Dtos
{
    public class ApplicationHistoryDto
	{
		public string Description { get; set; }

		public DateTime ModificationDate { get; set; }

		public int? ApplicationId { get; set; }

		public string UserFullName { get; set; }

		public ApplicationHistoryType ApplicationHistoryType { get; set; }
	}
}
