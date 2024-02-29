using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Data.Common.Models;
using StudentCredit.Infrastructure.Interfaces.Registration;
using System.Linq.Expressions;

namespace StudentCredit.Application.Nomenclatures.Interfaces
{
	public interface INomenclatureService<TNomenclature> : ITransientService
		where TNomenclature : Nomenclature
	{
		Task<IEnumerable<TNomenclature>> GetNomenclaturesAsync<TFilter>(TFilter filter)
			where TFilter : BaseNomenclatureFilterDto<TNomenclature>;

		Task<IEnumerable<TDto>> SelectNomenclaturesAsync<TFilter, TDto>(TFilter filter)
			where TFilter : BaseNomenclatureFilterDto<TNomenclature>
			where TDto : IMapping<TNomenclature, TDto>, new();

		Task<TNomenclature> InsertNomenclatureAsync(TNomenclature model);

		Task<TNomenclature> GetSingleOrDefaultNomenclatureAsync(Expression<Func<TNomenclature, bool>> predicate);

		Task<TNomenclature> UpdateNomenclatureAsync(int id, TNomenclature model);

		Task DeleteNomenclatureAsync(int id);
	}
}
