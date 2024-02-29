using AutoMapper;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Students;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Infrastructure.AutoMapperProfiles
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			this.CreateMap<PersonLot, StudentSelectDto>()
				.ForMember(m => m.Uan, cfg => cfg.MapFrom(src => src.Uan))
				.ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => src.PersonBasic.FullName))
				.ForMember(m => m.Uin, cfg => cfg.MapFrom(src => src.PersonBasic.Uin))
				.ForMember(m => m.IdNumber, cfg => cfg.MapFrom(src => src.PersonBasic.IdnNumber))
				.ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.PersonBasic.SecondCitizenship.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.Specialities, cfg => cfg.MapFrom(src => src.PersonStudents.Where(ps => ps.State == PartState.Actual && ps.StudentStatus.Alias == "active" && ps.InstitutionSpeciality.EducationalForm.Id == 1)))
				.ForMember(m => m.Doctorals, cfg => cfg.MapFrom(src => src.PersonDoctorals.Where(pd => pd.State == PartState.Actual && pd.StudentStatus.Alias == "active" && pd.InstitutionSpeciality.EducationalForm.Id == 1)))
				.ForMember(m => m.BirthDate, cfg => cfg.MapFrom(src => src.PersonBasic.BirthDate));

			this.CreateMap<PersonStudent, EducationSelectDto>()
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.ResearchArea.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.EducationalForm.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.EducationalQualification.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.StudentCourse, cfg => cfg.MapFrom(src => src.Semesters.OrderByDescending(s => s.Period.Year).FirstOrDefault().Course))
				.ForMember(m => m.FacultyNumber, cfg => cfg.MapFrom(src => src.FacultyNumber))
				.ForMember(m => m.StudentStatus, cfg => cfg.MapFrom(src => src.StudentStatus))
				.ForMember(m => m.PersonStudentDoctoralId, cfg => cfg.MapFrom(src => src.Id))
				.ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Citizenship.ToRdpzsdNomenclatureDto()));
			;

			this.CreateMap<PersonDoctoral, EducationSelectDto>()
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.ResearchArea.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.EducationalForm.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.InstitutionSpeciality.Speciality.EducationalQualification.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.DoctoralYear, cfg => cfg.MapFrom(src => src.Semesters.OrderByDescending(s => s.ProtocolDate.Date).FirstOrDefault().YearType))
				.ForMember(m => m.FacultyNumber, cfg => cfg.MapFrom(src =>
				src.Semesters.OrderByDescending(s => s.ProtocolDate.Date).FirstOrDefault().ProtocolNumber
				+ "/"
				+ src.Semesters.OrderByDescending(s => s.ProtocolDate.Date).FirstOrDefault().ProtocolDate.ToString("dd.MM.yyyy")))
				.ForMember(m => m.StudentStatus, cfg => cfg.MapFrom(src => src.StudentStatus))
				.ForMember(m => m.PersonStudentDoctoralId, cfg => cfg.MapFrom(src => src.Id))
				.ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Citizenship.ToRdpzsdNomenclatureDto()));

			this.CreateMap<ApplicationOne, StudentCreditDto>()
				.ForMember(m => m.BankName, cfg => cfg.MapFrom(src => src.Bank.Name))
				.ForMember(m => m.SpecialityName, cfg => cfg.MapFrom(src => src.Speciality.Name))
				.ForMember(m => m.EducationalQualificationName, cfg => cfg.MapFrom(src => src.EducationalQualification.Name))
				.ForMember(m => m.ApplicationOneType, cfg => cfg.MapFrom(src => src.ApplicationOneType.ToNomenclatureDto()));

			int? personStudentDoctoralId = null;

			this.CreateMap<PersonLot, StudentInfoDto>()
				.ForMember(m => m.Uan, cfg => cfg.MapFrom(src => src.Uan))
				.ForMember(m => m.StudentFullName, cfg => cfg.MapFrom(src => src.PersonBasic.FullName))
				.ForMember(m => m.Uin, cfg => cfg.MapFrom(src => src.PersonBasic.Uin))
				.ForMember(m => m.IdNumber, cfg => cfg.MapFrom(src => src.PersonBasic.IdnNumber))
				.ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.PersonBasic.SecondCitizenship.ToRdpzsdNomenclatureDto()))
				.ForMember(m => m.BirthDate, cfg => cfg.MapFrom(src => src.PersonBasic.BirthDate))
				.ForMember(m => m.PersonStudentInfo, cfg => cfg.MapFrom(src => src.PersonStudents.SingleOrDefault(ps => ps.Id == personStudentDoctoralId)))
				.ForMember(m => m.PersonDoctoralInfo, cfg => cfg.MapFrom(src => src.PersonDoctorals.SingleOrDefault(ps => ps.Id == personStudentDoctoralId)))
				;

			this.CreateMap<ApplicationOne, StudentCardInfoDto>()
				.ForMember(e => e.BankName, cfg => cfg.MapFrom(src => src.Bank.Name));

			this.CreateMap<ApplicationOne, StudentInfoDtoFromSC>()
				.ForMember(m => m.SecondNationality, cfg => cfg.MapFrom(src => src.SecondNationality.ToNomenclatureDto()))
				.ForMember(m => m.Institution, cfg => cfg.MapFrom(src => src.Institution.ToNomenclatureDto()))
				.ForMember(m => m.MigrationResearchAreaName, cfg => cfg.MapFrom(src => src.MigrationResearchAreaName))
				.ForMember(m => m.MigrationSpecialityName, cfg => cfg.MapFrom(src => src.MigrationSpecialityName))
				.ForMember(m => m.EducationFormType, cfg => cfg.MapFrom(src => src.EducationFormType.ToNomenclatureDto()))
				.ForMember(m => m.EducationalQualification, cfg => cfg.MapFrom(src => src.EducationalQualification.ToNomenclatureDto()))
				.ForMember(m => m.Nationality, cfg => cfg.MapFrom(src => src.Nationality.ToNomenclatureDto()))
                .ForMember(m => m.Speciality, cfg => cfg.MapFrom(src => src.Speciality.ToNomenclatureDto()))
                .ForMember(m => m.ResearchArea, cfg => cfg.MapFrom(src => src.ResearchArea.ToNomenclatureDto()));
        }
	}
}