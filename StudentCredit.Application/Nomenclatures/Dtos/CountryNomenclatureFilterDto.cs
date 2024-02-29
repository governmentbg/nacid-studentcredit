using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Application.Nomenclatures.Dtos
{
	public class CountryNomenclatureFilterDto : BaseNomenclatureFilterDto<Country>
	{
		public override IQueryable<Country> OrderBuilder(IQueryable<Country> query)
		{
			return query
				.OrderBy(e => e.Name)
				.ThenBy(e => e.ViewOrder)
				.ThenBy(e => e.Id);
		}
	}
}
