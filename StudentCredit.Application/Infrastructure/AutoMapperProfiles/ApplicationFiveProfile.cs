using AutoMapper;
using FileStorageNetCore.Models;
using StudentCredit.Application.Applications.ApplicationsFive.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Data.Applications.ApplicationFive;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
	public class ApplicationFiveProfile : Profile
	{
		public ApplicationFiveProfile()
		{
			this.CreateMap<ApplicationFiveDto, ApplicationFive>()
				.ForMember(m => m.Id, cfg => cfg.Ignore())
				.ForMember(m => m.Bank, cfg => cfg.Ignore())
				.ForMember(m => m.Year, cfg => cfg.Ignore())
				.ForMember(m => m.YearId, cfg => cfg.MapFrom(src => src.Year.Id));

			this.CreateMap<ApplicationFive, ApplicationFiveDto>()
				.ForMember(m => m.AmountRequested, cfg => cfg.MapFrom(src => Math.Round(src.AmountRequested, 2)))
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
				.ForMember(m => m.Year, cfg => cfg.MapFrom(src => src.Year.ToNomenclatureDto()));

			this.CreateMap<AttachedFile, ApplicationFiveAttachedFile>();

			this.CreateMap<ApplicationFive, ApplicationFiveSearchResultItemDto>()
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()));

			this.CreateMap<ApplicationFive, ApplicationHistoryDto>()
				.ForMember(m => m.Description, cfg => cfg.MapFrom(src => src.ApplicationHistory.Description))
				.ForMember(m => m.ApplicationId, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationId))
				.ForMember(m => m.ModificationDate, cfg => cfg.MapFrom(src => src.ApplicationHistory.ModificationDate))
				.ForMember(m => m.UserFullName, cfg => cfg.MapFrom(src => src.ApplicationHistory.UserFullName))
				.ForMember(m => m.ApplicationHistoryType, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationHistoryType))
				;
		}
	}
}
