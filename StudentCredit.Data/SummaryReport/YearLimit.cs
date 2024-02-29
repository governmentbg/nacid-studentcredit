using StudentCredit.Data.Common.Interfaces;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Data.SummaryReport
{
	public class YearLimit : IEntity
	{
		public int Id { get; set; }

		public decimal? Limit { get; set; }

		public int YearId { get; set; }
		public Year Year { get; set; }

		public YearLimit(decimal? limit, int yearId)
		{
			Limit = limit;
			YearId = yearId;
		}
	}
}
