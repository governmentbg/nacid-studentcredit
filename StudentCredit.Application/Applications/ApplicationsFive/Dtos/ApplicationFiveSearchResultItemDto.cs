using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFive.Dtos
{
	public class ApplicationFiveSearchResultItemDto
	{
		public int Id { get; set; }

		public DateTime BlankDate { get; set; }

		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public NomenclatureDto Bank { get; set; }

		public ApplicationFiveType ApplicationFiveType { get; set; }

		public CommitState CommitState { get; set; }
	}
}
