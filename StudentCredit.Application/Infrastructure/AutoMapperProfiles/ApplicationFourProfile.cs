using AutoMapper;
using FileStorageNetCore.Models;
using StudentCredit.Application.Applications.ApplicationsFour.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Data.Applications.ApplicationFour.Enums;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
    public class ApplicationFourProfile : Profile
    {
        public ApplicationFourProfile()
        {
            this.CreateMap<ApplicationFourDto, ApplicationFour>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ApplicationFourType, cfg => cfg.Ignore())
                .ForMember(m => m.Bank, cfg => cfg.Ignore())
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.Speciality, cfg => cfg.Ignore())
                .ForMember(m => m.ApplicationFourTypeId, cfg => cfg.MapFrom(src => src.Type.Id));
                ;

            this.CreateMap<ApplicationFour, ApplicationHistoryDto>()
                .ForMember(m => m.Description, cfg => cfg.MapFrom(src => src.ApplicationHistory.Description))
                .ForMember(m => m.ApplicationId, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationId))
                .ForMember(m => m.ModificationDate, cfg => cfg.MapFrom(src => src.ApplicationHistory.ModificationDate))
                .ForMember(m => m.UserFullName, cfg => cfg.MapFrom(src => src.ApplicationHistory.UserFullName))
                .ForMember(m => m.ApplicationHistoryType, cfg => cfg.MapFrom(src => src.ApplicationHistory.ApplicationHistoryType))
                ;

            this.CreateMap<ApplicationFourAttachedFileDto, ApplicationFourAttachedFile>()
                .ForMember(m => m.ApplicationFour, cfg => cfg.Ignore())
                .ForMember(m => m.DbId, cfg => cfg.MapFrom(src => src.File.DbId))
                .ForMember(m => m.Hash, cfg => cfg.MapFrom(src => src.File.Hash))
                .ForMember(m => m.Key, cfg => cfg.MapFrom(src => src.File.Key))
                .ForMember(m => m.Size, cfg => cfg.MapFrom(src => src.File.Size))
                .ForMember(m => m.MimeType, cfg => cfg.MapFrom(src => src.File.MimeType))
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => src.File.Name))
                ;

            this.CreateMap<ApplicationFour, ApplicationFourDto>()
                .ForMember(m => m.Type, cfg => cfg.MapFrom(src => src.ApplicationFourType.ToNomenclatureDto()))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToNomenclatureDto()))
				.ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.Speciality.ToNomenclatureDto()))
                .ForMember(m => m.Files, cfg => cfg.MapFrom(src => src.Files))
				;

            this.CreateMap<ApplicationFourAttachedFile, ApplicationFourAttachedFileDto>()
                .ForMember(m => m.File, cfg => cfg.MapFrom(src => 
                new AttachedFile { Key = src.Key, Hash = src.Hash, Size = src.Size, Name = src.Name, MimeType = src.MimeType, DbId = src.DbId}));

            this.CreateMap<ApplicationFour, CreditSearchResultItemDto>()
                .ForMember(m => m.ApplicationType, cfg => cfg.MapFrom(src => src.ApplicationFourType.ToNomenclatureDto()))
                .ForMember(m => m.Bank, cfg => cfg.MapFrom(src => src.Bank.ToNomenclatureDto()))
                .ForMember(m => m.Institution, cfg => cfg.Ignore())
                .ForMember(m => m.Uin, cfg => cfg.Ignore())
                .ForMember(m => m.Uan, cfg => cfg.Ignore())
                .ForMember(m => m.StudentFullName, cfg => cfg.Ignore())
                .ForMember(m => m.EducationalQualification, cfg => cfg.Ignore())
                .ForMember(m => m.EducationType, cfg => cfg.Ignore());

            this.CreateMap<ApplicationFourDto, ApplicationFourPdfExportDto>()
                .ForMember(m => m.BlankDateString, cfg => cfg.MapFrom(src => src.BlankDate.ToString("dd.MM.yyyy")))
                .ForMember(m => m.ContractDateString, cfg => cfg.MapFrom(src => src.ContractDate.ToString("dd.MM.yyyy")))
                .ForMember(m => m.CreditTypeString, cfg => cfg.MapFrom(src => src.CreditType == CreditType.EducationTaxes ? "Заплащане на таксите за обучение" : "Издръжка"))
                .ForMember(m => m.SpecialityName, cfg => cfg.MapFrom(src => src.SpecialityEnum == ExistingEnum.Existing ? src.Speciality.Name : src.MigrationSpecialityName))
                .ForMember(m => m.DebtStatementDocumentFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.DebtStatementDocument).SingleOrDefault().File.Name))
                .ForMember(m => m.CreditContractAndAnnexesFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.CreditContractAndAnnexes).SingleOrDefault().File.Name))
                .ForMember(m => m.DocumentInFavorOfTheBankFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.DocumentInFavorOfTheBank).SingleOrDefault().File.Name))
                .ForMember(m => m.DocumentPartialRepaymentFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.DocumentPartialRepayment).SingleOrDefault().File.Name))
                .ForMember(m => m.DeathCertificateFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.DeathCertificate).SingleOrDefault().File.Name))
                .ForMember(m => m.PermanentIncapacityFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.PermanentIncapacity).SingleOrDefault().File.Name))
                .ForMember(m => m.BirthCertificatesOrCourtDecisionsForAdoptionsFileName, cfg => cfg.MapFrom(src => src.Files.Where(f => f.Type == ApplicationFourAttachedFileType.BirthCertificatesOrCourtDecisionsForAdoptions).SingleOrDefault().File.Name));

		}
	}
}