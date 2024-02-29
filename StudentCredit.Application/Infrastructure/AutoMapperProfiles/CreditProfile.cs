using AutoMapper;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
	public class CreditProfile : Profile
	{
		public CreditProfile()
		{
			this.CreateMap<ApplicationOne, CreditInfoDto>()
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToNomenclatureDto()))
				.ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.ResearchArea.ToNomenclatureDto()))
				.ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.Speciality.ToNomenclatureDto()))
				.ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.EducationalQualification.ToNomenclatureDto()))
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
				.ForMember(m => m.ApplicationOneType, cfg => cfg.MapFrom(src => src.ApplicationOneType.ToNomenclatureDto()));

			this.CreateMap<ApplicationOne, CreditApplicationDto>()
				.ForMember(m => m.ApplicationType, cfg => cfg.MapFrom(src => src.ApplicationOneType.ToNomenclatureDto()))
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => new BankNomenclatureDto { Bulstat = src.Bank.Bulstat, Name = src.Bank.Name, Id = src.Bank.Id }));

			this.CreateMap<ApplicationFour, CreditApplicationDto>()
				.ForMember(m => m.ApplicationType, cfg => cfg.MapFrom(src => src.ApplicationFourType.ToNomenclatureDto()))
				.ForMember(m => m.Bank, cfg => cfg.MapFrom(src => new BankNomenclatureDto { Bulstat = src.Bank.Bulstat, Name = src.Bank.Name, Id = src.Bank.Id }));

			this.CreateMap<StudentInfoDto, CreditSelectDto>()
				.ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => src.StudentFullName))
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.PersonStudentInfo.Institution))
				.ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.PersonStudentInfo.Speciality))
				.ForMember(m => m.BirthDate, cfg => cfg.MapFrom(src => src.BirthDate))
				.ForMember(m => m.Uin, cfg => cfg.MapFrom(src => src.Uin))
				.ForMember(m => m.PersonStudentDoctoralId, cfg => cfg.MapFrom(src => src.PersonStudentInfo.PersonStudentDoctoralId))
				.ForMember(m => m.FacultyNumber, cfg => cfg.MapFrom(src => src.PersonStudentInfo.FacultyNumber));

            this.CreateMap<ApplicationOne, CreditSelectDto>()
                .ForMember(m => m.ApplicationOneId, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.Speciality, cfg => cfg.Ignore())
                .ForMember(m => m.StudentFullName, cfg => cfg.Ignore())
                .ForMember(m => m.BirthDate, cfg => cfg.Ignore())
                .ForMember(m => m.Uin, cfg => cfg.Ignore())
                .ForMember(m => m.FacultyNumber, cfg => cfg.Ignore())
                ;

			this.CreateMap<StudentInfoDtoFromSC, CreditSelectDto>();
        }
	}
}