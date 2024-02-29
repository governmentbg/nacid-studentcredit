using StudentCredit.Application.Common.Dtos;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Application.InstitutionSpecialities.Dtos
{
	public class SpecialityFilterDto : FilterDto<InstitutionSpeciality>
	{
		const string RegularEdicationalForm = "Редовна";

		public int? RootInstitutionId { get; set; }
		public int? ResearchAreaId { get; set; }

		public override IQueryable<InstitutionSpeciality> WhereBuilder(IQueryable<InstitutionSpeciality> query)
		{
			query = query.Where(i => i.EducationalForm.Name == RegularEdicationalForm);

			if (IsActive.HasValue)
			{
				if (IsActive.Value)
				{
					query = query.Where(e => e.IsActive);
				}
				else
				{
					query = query.Where(e => !e.IsActive);
				}
			}

			query = ConstructTextFilter(query);

			if (RootInstitutionId.HasValue)
			{
				query = query.Where(i => i.Institution.RootId == RootInstitutionId);
			}

			return query;
		}

		public override IQueryable<InstitutionSpeciality> OrderBuilder(IQueryable<InstitutionSpeciality> query)
		{
			return query
				.OrderBy(i => i.Speciality.EducationalQualification.Name);
		}

		protected virtual IQueryable<InstitutionSpeciality> ConstructTextFilter(IQueryable<InstitutionSpeciality> query)
		{
			if (!string.IsNullOrWhiteSpace(TextFilter))
			{
				var textFilter = $"{TextFilter.Trim().ToLower()}";
				query = query.Where(e => e.Speciality.Name.Trim().ToLower().Contains(textFilter));
			}

			return query;
		}
	}
}
