using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Application.Nomenclatures.Interfaces;
using StudentCredit.Data.Common.Models;

namespace StudentCredit.Hosting.Controllers.Common
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseNomenclatureController<T, TDto, TFilter> : ControllerBase
		where T : Nomenclature
		where TDto : IMapping<T, TDto>, new()
		where TFilter : BaseNomenclatureFilterDto<T>
	{
		protected readonly INomenclatureService<T> nomenclatureService;

		public BaseNomenclatureController(INomenclatureService<T> nomenclatureService)
		{
			this.nomenclatureService = nomenclatureService;
		}

		[HttpGet]
		public Task<IEnumerable<T>> GetNomenclatures([FromQuery] TFilter filter)
			=> this.nomenclatureService.GetNomenclaturesAsync(filter);

		[HttpGet("Select")]
		public Task<IEnumerable<TDto>> SelectNomenclatures([FromQuery] TFilter filter)
			=> this.nomenclatureService.SelectNomenclaturesAsync<TFilter, TDto>(filter);

		[HttpPost]
		public Task<T> AddNomenclature([FromBody] T model)
			=> this.nomenclatureService.InsertNomenclatureAsync(model);

		[HttpPut("{id:int}")]
		public Task<T> UpdateNomenclature([FromRoute] int id, [FromBody] T model)
			=> this.nomenclatureService.UpdateNomenclatureAsync(id, model);

		[HttpDelete("{id:int}")]
		public Task DeleteNomenclature([FromRoute] int id)
			=> this.nomenclatureService.DeleteNomenclatureAsync(id);
	}
}
