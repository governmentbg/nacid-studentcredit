using StudentCredit.Application.Students.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Students
{
	public interface IStudentCardService : ITransientService
	{
		Task<List<StudentCardInfoDto>> GetStudentCreditsInfo(string uan);
	}
}
