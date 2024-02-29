using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Interfaces;

namespace StudentCredit.Application.Applications.Common.Dtos
{
	public abstract class ApplicationCommitDto : IValidate
	{
		public int Id { get; set; }

		public CommitState CommitState { get; set; }

		public DateTime BlankDate { get; set; }

		public int RootId { get; set; }

		public bool? HasHistory { get; set; }

		public string ChangeStateDescription { get; set; }

		public abstract void ValidateProperties(DomainValidationService validationService);
	}
}
