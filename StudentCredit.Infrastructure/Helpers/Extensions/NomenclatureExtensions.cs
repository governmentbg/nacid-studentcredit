using StudentCredit.Data.Common.Models;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Infrastructure.Helpers.Extensions
{
    public static class NomenclatureExtensions
    {
        public static NomenclatureDto ToNomenclatureDto(this Nomenclature nomenclature)
        {
            return nomenclature != null
                ? new NomenclatureDto
                {
                    Id = nomenclature.Id,
                    Name = nomenclature.Name,
                    Alias = nomenclature.Alias
                }
                : null;
        }

		public static NomenclatureDto ToRdpzsdNomenclatureDto(this Data.Rdpzsd.Nomenclatures.Base.Nomenclature nomenclature)
		{
			return nomenclature != null
				? new NomenclatureDto
				{
					Id = nomenclature.Id,
					Name = nomenclature.Name,
					Alias = nomenclature.Alias
				}
				: null;
		}
	}
}
