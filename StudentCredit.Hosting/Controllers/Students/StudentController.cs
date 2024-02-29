using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Students;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Users.Constants;

namespace StudentCredit.Hosting.Controllers.Students
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly RoleService roleService;

        public StudentController(IStudentService studentService, RoleService roleService)
        {
            this.studentService = studentService;
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<StudentSearchResult> GetFilteredStudent([FromQuery] StudentFilterDto filter)
            => await this.studentService.GetStudentFiltered(filter);

		[HttpGet("selectstudent")]
		public async Task<StudentSelectDto> SelectStudent([FromQuery] StudentSelectSearchFilter filter)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            return await this.studentService.SelectStudent(filter);
        }
    }
}