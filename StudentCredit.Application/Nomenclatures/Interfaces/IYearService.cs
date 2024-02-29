using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Nomenclatures.Interfaces
{
	public interface IYearService : IScopedService
	{
		Task<IEnumerable<NomenclatureMapperDto<Year>>> GetSummaryReportYears(BaseNomenclatureFilterDto<Year> filter);
	}
}
