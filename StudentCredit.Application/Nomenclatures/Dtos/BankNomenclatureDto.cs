using StudentCredit.Application.Common.Dtos;
using StudentCredit.Data.Banks;
using System.Linq.Expressions;

namespace StudentCredit.Application.Nomenclatures.Dtos
{
	public class BankNomenclatureDto : NomenclatureMapperDto<Bank>, IMapping<Bank, BankNomenclatureDto>
	{
		public string Bulstat { get; set; }

		public new Expression<Func<Bank, BankNomenclatureDto>> Map()
		{
			return e => new BankNomenclatureDto
			{
				Id = e.Id,
				Name = e.Name,
				Alias = e.Alias,
				Bulstat = e.Bulstat,
				IsActive = e.IsActive
			};
		}
	}
}
