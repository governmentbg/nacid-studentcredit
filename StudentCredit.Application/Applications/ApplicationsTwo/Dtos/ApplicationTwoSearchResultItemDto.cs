using StudentCredit.Application.Nomenclatures.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsTwo.Dtos
{
	public class ApplicationTwoSearchResultItemDto
	{
		public int Id { get; set; }

		public BankNomenclatureDto Bank { get; set; }

		public decimal TotalSum { get; set; }

		public int CreditCount { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime RecordDate { get; set; }
	}
}
