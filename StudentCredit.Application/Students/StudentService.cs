using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Application.Students.Enums;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Students;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;
using StudentCredit.Infrastructure.Helpers.Extensions;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Students
{
	public class StudentService : IStudentService
	{
		private readonly IAppDbContext context;
		private readonly IRdpzsdDbContext rdpzsdDbContext;
		private readonly DomainValidationService validation;
		private readonly IMapper mapper;

		public StudentService(
			IAppDbContext context,
			IRdpzsdDbContext rdpzsdDbContext,
			DomainValidationService validation,
			IMapper mapper
			)
		{
			this.context = context;
			this.rdpzsdDbContext = rdpzsdDbContext;
			this.validation = validation;
			this.mapper = mapper;
		}

		public async Task<StudentSearchResult> GetStudentFiltered(StudentFilterDto filter)
		{
			var student = this.rdpzsdDbContext.Set<PersonLot>()
				.Where(s => s.State == LotState.Actual)
				.AsNoTracking()
				.AsQueryable();

			switch (filter.IdentificationType)
			{
				case SearchIdentificationType.UIN:
					student = student.Where(s => s.PersonBasic.Uin == filter.Identificator);
					break;
				case SearchIdentificationType.ForeignerNumber:
					student = student.Where(s => s.PersonBasic.ForeignerNumber == filter.Identificator);
					break;
				case SearchIdentificationType.UAN:
					student = student.Where(s => s.Uan.Trim().ToLower() == filter.Identificator.Trim().ToLower());
					break;
				default:
					break;
			}

			var lotId = student.Select(s => s.Id).SingleOrDefault();

			var universities = this.rdpzsdDbContext.Set<PersonStudent>()
				.Where(s => s.LotId == lotId && s.State == PartState.Actual)
				.AsNoTracking()
				.AsQueryable();

			var doctorals = this.rdpzsdDbContext.Set<PersonDoctoral>()
				.Where(s => s.LotId == lotId && s.State == PartState.Actual)
				.AsNoTracking()
				.AsQueryable();

			if (filter.EducationalQualificationId != null)
			{
				var educationalQualification = await this.context.Set<EducationalQualification>()
					.Where(e => e.Id == filter.EducationalQualificationId)
					.SingleOrDefaultAsync();

                if (educationalQualification.Alias == EducationalQualificationAlias.MASTER)
				{
                    universities = universities.Where(s => s.InstitutionSpeciality.Speciality.EducationalQualification.Alias == EducationalQualificationAlias.MASTER_HIGH 
						|| s.InstitutionSpeciality.Speciality.EducationalQualification.Alias == EducationalQualificationAlias.MASTER_SECONDARY);
                }
				else
				{
                    universities = universities.Where(s => s.InstitutionSpeciality.Speciality.EducationalQualificationId == filter.EducationalQualificationId);
                }

                doctorals = doctorals.Where(s => s.InstitutionSpeciality.Speciality.EducationalQualificationId == filter.EducationalQualificationId);
			}

			if (filter.EducationalFormId != null)
			{
				universities = universities.Where(s => s.InstitutionSpeciality.EducationalFormId == filter.EducationalFormId);
				doctorals = doctorals.Where(s => s.InstitutionSpeciality.EducationalFormId == filter.EducationalFormId);
			}

			var result = await student
				.Select(personLot => new StudentSearchResult
				{
					Uan = personLot.Uan,
					FullName = personLot.PersonBasic.FullName,
					Uin = personLot.PersonBasic.Uin,
					ForeignerNumber = personLot.PersonBasic.ForeignerNumber,
					IdnNumber = personLot.PersonBasic.IdnNumber,
					BirthDate = personLot.PersonBasic.BirthDate,
					Universities = universities
					   .Select(ps => new StudentEducationDto
					   {
						   Id = ps.Id,
						   Institution = ps.Institution != null
							   ? new InstitutionNomenclatureDto
							   {
								   Id = ps.Institution.Id,
								   Name = ps.Institution.Name,
								   NameAlt = ps.Institution.NameAlt,
								   Alias = ps.Institution.Alias,
								   ShortName = ps.Institution.ShortName,
								   ShortNameAlt = ps.Institution.ShortNameAlt
							   }
							   : null,
						   Speciality = ps.InstitutionSpeciality.Speciality.ToRdpzsdNomenclatureDto(),
						   EducationalQualification = ps.InstitutionSpeciality.Speciality.EducationalQualification.ToRdpzsdNomenclatureDto(),
						   EducationalForm = ps.InstitutionSpeciality.EducationalForm.ToRdpzsdNomenclatureDto(),
						   ResearchArea = ps.InstitutionSpeciality.Speciality.ResearchArea.ToRdpzsdNomenclatureDto(),
						   Duration = ps.InstitutionSpeciality.Duration,
						   StartPeriod = ps.Semesters
							   .OrderByDescending(s => s.Period.Year)
							   .ThenByDescending(s => s.Period.Semester)
							   .ThenByDescending(s => s.Id)
							   .LastOrDefault()
							   .Period
							   .Name,
						   FinishPeriod = ps.StudentStatus.Alias == "graduated" || ps.StudentStatus.Alias == "processGraduation"
							   ? ps.Semesters.Where(s => s.StudentEvent.Alias == "graduatedCourse").SingleOrDefault().Period.Name
							   : null,
						   GraduationDate = ps.StudentStatus.Alias == "graduated"
							   ? ps.GraduationDate
							   : null,
						   Status = ps.StudentStatus != null
							   ? new NomenclatureDto
							   {
								   Name = ps.StudentStatus.Alias == "completed" ? "Недействащ" : ps.StudentStatus.Name,
								   NameAlt = ps.StudentStatus.NameAlt,
								   Alias = ps.StudentStatus.Alias,
							   }
							   : null,
						   InterruptionReason = ps.StudentStatus.Alias == "interrupted"
							   ? ps.Semesters
								   .OrderByDescending(s => s.Period.Year)
								   .ThenByDescending(s => s.Period.Semester)
								   .ThenByDescending(s => s.Id)
								   .FirstOrDefault()
								   .StudentEvent
								   .Name
							   : null,
						   Course = ps.Semesters
							   .OrderByDescending(s => s.Period.Year)
							   .ThenByDescending(s => s.Period.Semester)
							   .ThenByDescending(s => s.Id)
							   .FirstOrDefault()
							   .Course
					   })
					   .OrderByDescending(ps => ps.Id).ToList(),
					Doctorals = doctorals
					   .Select(pd => new DoctoralEducationDto
					   {
						   Id = pd.Id,
						   Institution = pd.Institution != null
						   ? new InstitutionNomenclatureDto
						   {
							   Id = pd.Institution.Id,
							   Name = pd.Institution.Name,
							   NameAlt = pd.Institution.NameAlt,
							   Alias = pd.Institution.Alias,
							   ShortName = pd.Institution.ShortName,
							   ShortNameAlt = pd.Institution.ShortNameAlt
						   }
						   : null,
						   Speciality = pd.InstitutionSpeciality.Speciality.ToRdpzsdNomenclatureDto(),
						   EducationalQualification = pd.InstitutionSpeciality.Speciality.EducationalQualification.ToRdpzsdNomenclatureDto(),
						   EducationalForm = pd.InstitutionSpeciality.EducationalForm.ToRdpzsdNomenclatureDto(),
						   ResearchArea = pd.InstitutionSpeciality.Speciality.ResearchArea.ToRdpzsdNomenclatureDto(),
						   Duration = pd.InstitutionSpeciality.Duration,
						   StartDate = pd.StartDate,
						   FinishDate = pd.StudentStatus.Alias == "graduated" || pd.StudentStatus.Alias == "processGraduation"
							   ? pd.Semesters.Where(s => s.StudentStatus.Alias == "processGraduation").SingleOrDefault().EndDate
							   : null,
						   GraduationDate = pd.StudentStatus.Alias == "graduated"
							   ? pd.Semesters.Where(s => s.StudentStatus.Alias == "graduated").SingleOrDefault().DefenseDate
							   : null,
						   Status = pd.StudentStatus != null
							   ? new NomenclatureDto
							   {
								   Name = pd.StudentStatus.Alias == "completed" ? "Недействащ" : pd.StudentStatus.Name,
								   NameAlt = pd.StudentStatus.NameAlt,
								   Alias = pd.StudentStatus.Alias,
							   }
							   : null,
						   InterruptionReason = pd.StudentStatus.Alias == "interrupted"
							   ? pd.Semesters
								   .OrderByDescending(s => s.ProtocolDate.Date)
								   .ThenByDescending(s => s.Id)
								   .FirstOrDefault()
								   .StudentEvent
								   .Name
							   : null,
						   YearType = pd.Semesters
							   .OrderByDescending(s => s.ProtocolDate.Date)
							   .ThenByDescending(s => s.Id)
							   .FirstOrDefault()
							   .YearType
					   })
					   .OrderByDescending(pd => pd.Id).ToList()
				})
				.SingleOrDefaultAsync();

			if (result == null)
			{
				this.validation.ThrowErrorMessage(StudentErrorCode.StudentNotFoundWithGivenIdentificator);
			}

			var applicationOnes = this.context.Set<ApplicationOne>()
				.AsNoTracking()
				.Where(s => s.Uan == result.Uan
					&& (s.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT || s.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT)
					&& s.CommitState == CommitState.Approved)
				.Include(s => s.Bank)
				.Include(s => s.EducationalQualification)
				.Include(s => s.Speciality)
				.Include(s => s.ApplicationOneType)
				.AsQueryable();

			foreach (var applicationOne in applicationOnes)
			{
				var credit = this.mapper.Map<StudentCreditDto>(applicationOne);

				result.Credits.Add(credit);
			}

			return result;
		}

		public async Task<StudentSelectDto> SelectStudent(StudentSelectSearchFilter filter)
		{
			var student = await this.rdpzsdDbContext.Set<PersonLot>()
				.AsNoTracking()
				.Where(pl => pl.Uan.Trim().ToLower() == filter.Uan.Trim().ToLower() && pl.State == LotState.Actual)
				.ProjectTo<StudentSelectDto>(this.mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			if (student == null)
			{
				this.validation.ThrowErrorMessage(StudentErrorCode.StudentNotFoundWithGivenUan);
			}

			if (filter.EducationType == EducationType.Student)
			{
				student.HasActiveEducation = student.Specialities.Any(ps => ps.StudentStatus.Alias == "active" && ps.EducationFormType.Id == 1);

				if (!student.HasActiveEducation)
				{
					this.validation.ThrowErrorMessage(CreditErrorCode.StudentNoActiveEducation);
				}
			}
			else if (filter.EducationType == EducationType.Doctoral)
			{
				student.HasActiveEducation = student.Doctorals.Any(pd => pd.StudentStatus.Alias == "active" && pd.EducationFormType.Id == 1);

				if (!student.HasActiveEducation)
				{
					this.validation.ThrowErrorMessage(CreditErrorCode.DoctoralNoActiveEducation);
				}
			}

			return student;
		}

		public async Task<StudentInfoDto> GetCreditStudentInfo(string uan, int personStudentDoctoralId, EducationType educationType)
		{
			var student = await this.rdpzsdDbContext.Set<PersonLot>()
				.AsNoTracking()
				.Where(e => e.Uan == uan)
				.ProjectTo<StudentInfoDto>(this.mapper.ConfigurationProvider, new { personStudentDoctoralId })
				.SingleOrDefaultAsync();

			if (student == null)
			{
				this.validation.ThrowErrorMessage(StudentErrorCode.DesyncedUanBetweenSystems);
			}

			if (student != null && educationType == EducationType.Doctoral)
			{
				student.PersonStudentInfo = student.PersonDoctoralInfo;
			}

			return student;
		}

		public async Task<StudentInfoDtoFromSC> GetCreditStudentInfoFromSC(int applicationOneId)
		{
			return await this.context.Set<ApplicationOne>()
				.AsNoTracking()
				.Where(c=> c.Id == applicationOneId)
                .ProjectTo<StudentInfoDtoFromSC>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}
