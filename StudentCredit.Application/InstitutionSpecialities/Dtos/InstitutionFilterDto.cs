using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Application.InstitutionSpecialities.Dtos
{
	public class InstitutionFilterDto : BaseNomenclatureFilterDto<Institution>
	{
		public string InstitutionName { get; set; }

		public Level? Level { get; set; }

		public override IQueryable<Institution> WhereBuilder(IQueryable<Institution> query)
		{
			if (!string.IsNullOrWhiteSpace(InstitutionName))
			{
				query = query.Where(e => e.Name.ToLower().Trim().Contains(InstitutionName.ToLower().Trim()));
			}

			if (Level.HasValue)
			{
				query = query.Where(e => e.Level == Level);
			}

			return base.WhereBuilder(query);
		}
	}
}
