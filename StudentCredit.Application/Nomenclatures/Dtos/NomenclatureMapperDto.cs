using StudentCredit.Application.Common.Dtos;
using StudentCredit.Data.Common.Models;
using System.Linq.Expressions;

namespace StudentCredit.Application.Nomenclatures.Dtos
{
	public class NomenclatureMapperDto<TNomenclature> : IMapping<TNomenclature, NomenclatureMapperDto<TNomenclature>>
		where TNomenclature : Nomenclature
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Alias { get; set; }

		public bool IsActive { get; set; }

		public virtual Expression<Func<TNomenclature, NomenclatureMapperDto<TNomenclature>>> Map()
		{
			return e => new NomenclatureMapperDto<TNomenclature>
			{
				Id = e.Id,
				Name = e.Name,
				Alias = e.Alias,
				IsActive = e.IsActive
			};
		}
	}
}
