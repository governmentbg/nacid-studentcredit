using AutoMapper;
using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Data.Applications.ApplicationTwo;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
	public class ApplicationTwoProfile : Profile
	{
		public ApplicationTwoProfile()
		{
			this.CreateMap<ApplicationTwo, ApplicationTwoSearchResultItemDto>()
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => new BankNomenclatureDto { Bulstat = src.Bank.Bulstat, Name = src.Bank.Name, Id = src.Bank.Id }));

			this.CreateMap<ApplicationTwo, ApplicationTwoDto>()
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => new BankNomenclatureDto { Bulstat = src.Bank.Bulstat, Name = src.Bank.Name, Id = src.Bank.Id }))
				.ForMember(m => m.RecordEntries, cfg => cfg.MapFrom(src => src.RecordEntries))
				.ForMember(m => m.TotalSum, cfg => cfg.MapFrom(src => Math.Round(src.TotalSum, 2)));

			this.CreateMap<RecordEntry, RecordEntryDto>();

			this.CreateMap<ApplicationTwoDto, ApplicationTwo>()
				.ForMember(m => m.Bank, cfg => cfg.Ignore());

			this.CreateMap<RecordEntryDto, RecordEntry>();
		}
	}
}
