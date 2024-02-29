using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Students
{
    public interface IStudentService : ITransientService
	{
		Task<StudentSearchResult> GetStudentFiltered(StudentFilterDto filter);

		Task<StudentSelectDto> SelectStudent(StudentSelectSearchFilter filter);

		Task<StudentInfoDto> GetCreditStudentInfo(string uan, int personStudentDoctoralId, EducationType educationType);
		Task<StudentInfoDtoFromSC> GetCreditStudentInfoFromSC(int applicationOneId);
    }
}
