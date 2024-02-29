using AutoMapper;
using StudentCredit.Application.Applications.ApplicationsOne.Constants;
using StudentCredit.Application.Applications.ApplicationsOne.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
    public class ApplicationOneProfile : Profile
    {
        public ApplicationOneProfile()
        {
            this.CreateMap<ApplicationOneDto, ApplicationOne>()
                .ForMember(m => m.ApplicationOneType, cfg => cfg.Ignore())
                .ForMember(m => m.Bank, cfg => cfg.Ignore())
                .ForMember(m => m.Nationality, cfg => cfg.Ignore())
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.ResearchArea, cfg => cfg.Ignore())
                .ForMember(m => m.Speciality, cfg => cfg.Ignore())
                .ForMember(m => m.EducationalQualification, cfg => cfg.Ignore())
                .ForMember(m => m.EducationFormType, cfg => cfg.Ignore())
                .ForMember(m => m.AdjournType, cfg => cfg.Ignore())
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ExtensionOfGracePeriod, cfg => cfg.Ignore())
                .ForMember(m => m.SecondNationality, cfg => cfg.Ignore())
                .ForMember(m => m.SpecialityEnum, cfg => cfg.MapFrom(src => src.Speciality != null ? ExistingEnum.Existing : ExistingEnum.Missing))
                .ForMember(m => m.ResearchAreaEnum, cfg => cfg.MapFrom(src => src.ResearchArea != null ? ExistingEnum.Existing : ExistingEnum.Missing));

            this.CreateMap<ApplicationOne, ApplicationOneDto>()
                .ForMember(m => m.ApplicationOneType, cfg => cfg.MapFrom(src => src.ApplicationOneType.ToNomenclatureDto()))
                .ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Nationality.ToNomenclatureDto()))
                .ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToNomenclatureDto()))
                .ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.ResearchArea.ToNomenclatureDto()))
                .ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.Speciality.ToNomenclatureDto()))
                .ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.EducationalQualification.ToNomenclatureDto()))
                .ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.EducationFormType.ToNomenclatureDto()))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
                .ForMember(m => m.AdjournType, cfg => cfg.MapFrom(src => src.AdjournType.ToNomenclatureDto()))
                .ForMember(m => m.ExtensionOfGracePeriod, cfg => cfg.MapFrom(src => src.ExtensionOfGracePeriod.ToNomenclatureDto()))
                .ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.SecondNationality.ToNomenclatureDto()));

			this.CreateMap<ApplicationOne, ApplicationOneCreditSelectDto>()
                .ForMember(m => m.ApplicationOneId, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
                .ForMember(m => m.Nationality, cfg => cfg.Ignore())
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.ResearchArea, cfg => cfg.Ignore())
                .ForMember(m => m.Speciality, cfg => cfg.Ignore())
                .ForMember(m => m.EducationalQualification, cfg => cfg.Ignore())
                .ForMember(m => m.EducationFormType, cfg => cfg.Ignore())
                .ForMember(m => m.StudentFullName, cfg => cfg.Ignore())
                .ForMember(m => m.BirthDate, cfg => cfg.Ignore())
                .ForMember(m => m.Uin, cfg => cfg.Ignore())
                .ForMember(m => m.IdNumber, cfg => cfg.Ignore())
                .ForMember(m => m.FacultyNumber, cfg => cfg.Ignore())
				.ForMember(m => m.StudentCourse, cfg => cfg.Ignore())
                .ForMember(m => m.DoctoralYear, cfg => cfg.Ignore())
				.ForMember(m => m.SecondNationality, cfg => cfg.Ignore())
				;

            this.CreateMap<ApplicationOneXml, ApplicationOneDto>()
                .ForMember(m => m.ContractDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.ContractDate)))
                .ForMember(m => m.SchoolRemaining, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableInt(src.SchoolRemaining)))
                .ForMember(m => m.ExpirationDateOfGracePeriod, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.ExpirationDateOfGratisPeriod)))
                .ForMember(m => m.MonthlyPayment, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableDecimal(src.PlanInfo.MonthlyPayment)))
                .ForMember(m => m.PaymentPeriod, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableInt(src.PaymentPeriod)))
                .ForMember(m => m.CreditSize, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableDecimal(src.CreditSize.Size)))
                .ForMember(m => m.SemesterTax, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableDecimal(src.CreditSize.SemesterTax)))
                .ForMember(m => m.Interest, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableDecimal(src.CreditSize.Interest)))
                .ForMember(m => m.PaymentDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.PaymentDate)))
                .ForMember(m => m.EarlyPaymentDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.EarlyPaymentDate)))
                .ForMember(m => m.RecontractionDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.RecontractionDate)))
                .ForMember(m => m.AdjournDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.AdjournDate)))
                .ForMember(m => m.AdjournPeriod, cfg => cfg.MapFrom(src => ParserExtensions.TryParseStringToNullableInt(src.AdjournPeriod)))
                .ForMember(m => m.EarlyDemandOfTheLoan, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.EarlyDemandOfTheLoanDate)))
                .ForMember(m => m.ForcePaymentDate, cfg => cfg.MapFrom(src => ParserExtensions.TryParseDate(src.ForcePaymentDate)))
                .ForMember(m => m.EducationType, cfg => cfg.MapFrom(src => ApplicationOneImportConstants.EducationTypes[int.Parse(src.EducationType)]))
                .ForMember(m => m.CreditType, cfg => cfg.MapFrom(src => ApplicationOneImportConstants.CreditTypes[int.Parse(src.CreditType)]))
                .ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => $"{src.StudentName.FirstName} {src.StudentName.SurName} {src.StudentName.LastName}"))
                .ForMember(m => m.BlankDate, cfg => cfg.MapFrom(src => src.BlankDate != null ? new DateTime(src.BlankDate.Year.Value, src.BlankDate.Month.Value, src.BlankDate.Day.Value) : DateTime.Now))
                .ForMember(m => m.ApplicationOneType, cfg => cfg.Ignore())
                .ForMember(m => m.AdjournType, cfg => cfg.Ignore())
                .ForMember(m => m.Nationality, cfg => cfg.Ignore())
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.StudentCourse, cfg => cfg.Ignore())
                .ForMember(m => m.DoctoralYear, cfg => cfg.Ignore())
                .ForMember(m => m.Speciality, cfg => cfg.Ignore())
                .ForMember(m => m.ResearchArea, cfg => cfg.Ignore())
				.ForMember(m => m.ExtensionOfGracePeriod, cfg => cfg.Ignore())
                ;

			this.CreateMap<ApplicationOne, ApplicationHistoryDto>()
				.ForMember(m => m.Description, cfg => cfg.MapFrom(src => src.ApplicationHistory.Description))
				.ForMember(m => m.ApplicationId, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationId))
				.ForMember(m => m.ModificationDate, cfg => cfg.MapFrom(src => src.ApplicationHistory.ModificationDate))
				.ForMember(m => m.UserFullName, cfg => cfg.MapFrom(src => src.ApplicationHistory.UserFullName))
				.ForMember(m => m.ApplicationHistoryType, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationHistoryType))
				;

            this.CreateMap<ApplicationOne, CreditSearchResultItemDto>()
                .ForMember(m => m.ApplicationType, cfg => cfg.MapFrom(src => src.ApplicationOneType.ToNomenclatureDto()))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
                .ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.Name))
                .ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.EducationalQualification.ToNomenclatureDto()));

            this.CreateMap<StudentInfoDto, ApplicationOneCreditSelectDto>()
                .ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => src.StudentFullName))
                .ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.PersonStudentInfo.Institution))
                .ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.PersonStudentInfo.ResearchArea))
                .ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.PersonStudentInfo.Speciality))
                .ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.PersonStudentInfo.EducationalQualification))
                .ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.PersonStudentInfo.EducationFormType))
                .ForMember(m => m.BirthDate, cfg => cfg.MapFrom(src => src.BirthDate))
                .ForMember(m => m.Uin, cfg => cfg.MapFrom(src => src.Uin))
                .ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.SecondNationality))
                .ForMember(m => m.IdNumber, cfg => cfg.MapFrom(src => src.IdNumber))
                .ForMember(m => m.PersonStudentDoctoralId, cfg => cfg.MapFrom(src => src.PersonStudentInfo.PersonStudentDoctoralId))
                .ForMember(m => m.StudentCourse, cfg => cfg.MapFrom(src => src.PersonStudentInfo.StudentCourse))
                .ForMember(m => m.DoctoralYear, cfg => cfg.MapFrom(src => src.PersonStudentInfo.DoctoralYear))
                .ForMember(m => m.FacultyNumber, cfg => cfg.MapFrom(src => src.PersonStudentInfo.FacultyNumber))
                .ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.PersonStudentInfo.Nationality))
                .ForMember(m => m.SpecialityEnum, cfg => cfg.MapFrom(src => ExistingEnum.Existing))
                .ForMember(m => m.ResearchAreaEnum, cfg => cfg.MapFrom(src => ExistingEnum.Existing));

            this.CreateMap<StudentInfoDtoFromSC, ApplicationOneCreditSelectDto>()
                .ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => src.StudentFullName))
                .ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution))
                .ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.EducationalQualification))
                .ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.EducationFormType))
                .ForMember(m => m.BirthDate, cfg => cfg.MapFrom(src => src.BirthDate))
                .ForMember(m => m.Uin, cfg => cfg.MapFrom(src => src.Uin))
                .ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.SecondNationality))
                .ForMember(m => m.IdNumber, cfg => cfg.MapFrom(src => src.IdNumber))
                .ForMember(m => m.StudentCourse, cfg => cfg.MapFrom(src => src.StudentCourse))
                .ForMember(m => m.DoctoralYear, cfg => cfg.MapFrom(src => src.DoctoralYear))
                .ForMember(m => m.FacultyNumber, cfg => cfg.MapFrom(src => src.FacultyNumber))
                .ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Nationality))
                .ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.Speciality ?? null))
                .ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Nationality ?? null))
                .ForMember(m => m.SpecialityEnum, cfg => cfg.MapFrom(src => src.Speciality != null ? ExistingEnum.Existing : ExistingEnum.Missing))
                .ForMember(m => m.ResearchAreaEnum, cfg => cfg.MapFrom(src => src.ResearchArea != null ? ExistingEnum.Existing : ExistingEnum.Missing));

            this.CreateMap<ApplicationOneDto, ApplicationOnePdfExportDto>()
               .ForMember(m => m.BlankDateString, cfg => cfg.MapFrom(src => src.BlankDate.ToString("dd.MM.yyyy")))
               .ForMember(m => m.ContractDateString, cfg => cfg.MapFrom(src => src.ContractDate.ToString("dd.MM.yyyy")))
               .ForMember(m => m.CreditTypeString, cfg => cfg.MapFrom(src => src.CreditType == CreditType.EducationTaxes ? "Заплащане на таксите за обучение" : "Издръжка"))
               .ForMember(m => m.SpecialityName, cfg => cfg.MapFrom(src => src.SpecialityEnum == ExistingEnum.Existing ? src.Speciality.Name : src.MigrationSpecialityName));
        }
	}
}