using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Nomenclatures.Constants;

namespace StudentCredit.Application.Nomenclatures.Dtos
{
	public class ApplicationOneTypeNomenclatureFilterDto : BaseNomenclatureFilterDto<ApplicationOneType>
	{
		public bool? IsNewCredit { get; set; }

		public override IQueryable<ApplicationOneType> WhereBuilder(IQueryable<ApplicationOneType> query)
		{
			if (IsNewCredit.HasValue)
			{
				if (IsNewCredit == true)
				{
					query = query.Where(e => e.Alias == ApplicationOneTypeAlias.NEW_CONTRACT || e.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT);
				}
				else
				{
					query = query.Where(e => e.Alias != ApplicationOneTypeAlias.NEW_CONTRACT && e.Alias != ApplicationOneTypeAlias.REFUSE_CONTRACT);
				}

			}

			return base.WhereBuilder(query);
		}
	}
}
