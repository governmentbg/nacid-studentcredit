using Microsoft.AspNetCore.Mvc;
using StudentCredit.Data.Common.Models;
using StudentCredit.Hosting.Controllers.Common;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileTemplateController : BaseEntityController<FileTemplate>
    {
        public FileTemplateController(IAppDbContext context)
            : base(context)
        {

        }
    }
}
