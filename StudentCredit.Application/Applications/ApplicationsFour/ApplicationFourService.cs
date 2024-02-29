using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Applications.ApplicationsFour.Dtos;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Interfaces;
using AutoMapper;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Application.Applications.Common.Services;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Students;

namespace StudentCredit.Application.Applications.ApplicationsFour
{
    public class ApplicationFourService : ApplicationService<ApplicationFour, ApplicationFourDto>, IApplicationFourService
    {
        private readonly IAppDbContext context;
        private readonly DomainValidationService validation;
		private readonly IMapper mapper;

		public ApplicationFourService (
            IAppDbContext context,
            DomainValidationService validation,
            IUserContext userContext,
            IMapper mapper,
            RoleService roleService,
			IStudentService studentService
            )
            :base (context, userContext, mapper, validation, roleService, studentService)
        {
            this.context = context;
            this.validation = validation;
			this.mapper = mapper;
		}

        public async Task CheckApplicationFourAlreadyInProcess(string creditNumber, int bankId)
        {
            var existsActiveApplicationFour = await this.context.Set<ApplicationFour>()
                .AnyAsync(a => a.CreditNumber == creditNumber
                && a.BankId == bankId
                && a.CommitState != CommitState.Denied
                && a.CommitState != CommitState.Archived);

            if (existsActiveApplicationFour) 
            {
                this.validation.ThrowErrorMessage(CreditErrorCode.ApplicationFourAlreadyInProcess);
            }
        }

        public ApplicationFourPdfExportDto GetPdfExportDto(ApplicationFourDto applicationFourDto)
        {
            return this.mapper.Map<ApplicationFourPdfExportDto>(applicationFourDto);
        }
    }
}
