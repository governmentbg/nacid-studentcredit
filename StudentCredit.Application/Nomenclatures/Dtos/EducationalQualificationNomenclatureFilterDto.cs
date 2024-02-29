using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Nomenclatures.Constants;

namespace StudentCredit.Application.Nomenclatures.Dtos
{
    public class EducationalQualificationNomenclatureFilterDto : BaseNomenclatureFilterDto<EducationalQualification>
    {
        public bool? CombineMasters { get; set; }

        public override IQueryable<EducationalQualification> WhereBuilder(IQueryable<EducationalQualification> query)
        {
            if (CombineMasters.HasValue)
            {
                if (CombineMasters == true)
                {
                    query = query.Where(e => e.Alias != EducationalQualificationAlias.MASTER_SECONDARY && e.Alias != EducationalQualificationAlias.MASTER_HIGH);
                }
                else
                {
                    query = query.Where(e => e.Alias != EducationalQualificationAlias.MASTER);
                }

            }

            return base.WhereBuilder(query);
        }
    }
}
