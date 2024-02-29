using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Credits.Dtos
{
	public class CreditApplicationDto
	{
		public int Id { get; set; }

		public NomenclatureDto ApplicationType { get; set; }

		public DateTime BlankDate { get; set; }

		public BankNomenclatureDto Bank { get; set; }

		public CommitState CommitState { get; set; }
	}
}
