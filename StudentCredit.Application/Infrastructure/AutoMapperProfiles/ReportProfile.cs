using AutoMapper;
using StudentCredit.Application.Reports.Dtos.SummaryReports;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
    public class ReportProfile : Profile
	{
		public ReportProfile()
		{
			this.CreateMap<Sheet, SheetDto>()
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
				.ForMember(m => m.Year, cfg => cfg.MapFrom(src => src.Year.ToNomenclatureDto()))
				.ForMember(m => m.SheetList, cfg => cfg.MapFrom(src => src.SheetList));

			this.CreateMap<SheetYear, SheetYearDto>()
				.ForMember(m => m.Year, cfg => cfg.MapFrom(src => src.Year.ToNomenclatureDto()));

			this.CreateMap<MonthData, MonthDataDto>();

			this.CreateMap<MonthDataDto, MonthData>()
				.ForMember(m => m.SheetYear, cfg => cfg.Ignore());
		}
	}
}
