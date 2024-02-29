using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Students;
using StudentCredit.Application.Students.Dtos;

namespace StudentCredit.Hosting.Controllers.Students
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCardController : Controller
    {
		private readonly IStudentCardService studentCardService;

		public StudentCardController(IStudentCardService studentCardService)
        {
			this.studentCardService = studentCardService;
		}

		[HttpGet]
		public async Task<List<StudentCardInfoDto>> GetStudentCreditsInfo([FromQuery] string uan)
			=> await this.studentCardService.GetStudentCreditsInfo(uan);
	}
}
