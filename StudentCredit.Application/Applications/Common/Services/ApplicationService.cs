using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Users;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Applications.Common.Models;
using StudentCredit.Application.Students;

namespace StudentCredit.Application.Applications.Common.Services
{
	public abstract class ApplicationService<T, TDto>
		where T : ApplicationCommit
		where TDto : ApplicationCommitDto
	{
		private readonly IAppDbContext context;
		private readonly IUserContext userContext;
		private readonly IMapper mapper;
		private readonly DomainValidationService domainValidation;
		private readonly RoleService roleService;
		private readonly IStudentService studentService;

		public ApplicationService(
			IAppDbContext context,
			IUserContext userContext,
			IMapper mapper,
			DomainValidationService domainValidation,
			RoleService roleService,
			IStudentService studentService
			)
		{
			this.context = context;
			this.userContext = userContext;
			this.mapper = mapper;
			this.domainValidation = domainValidation;
			this.roleService = roleService;
			this.studentService = studentService;
		}

		public async Task<TDto> GetById(int id)
		{
			var application = await this.context.Set<T>()
				.AsNoTracking()
				.Where(a => a.Id == id)
				.ProjectTo<TDto>(this.mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			var applicationsCount = await context.Set<T>()
				.AsNoTracking()
				.CountAsync(e => e.RootId == application.RootId);

			application.HasHistory = applicationsCount > 1;

			return application;
		}

		public virtual async Task Create(TDto dto)
		{
			dto.ValidateProperties(this.domainValidation);

			var entity = this.mapper.Map<TDto, T>(dto);
			entity.CommitState = CommitState.Pending;

			await this.context.Set<T>().AddAsync(entity);
			await this.context.SaveChangesAsync();

			entity.RootId = entity.Id;
			await this.context.SaveChangesAsync();
		}

		public async Task ChangeApplicationState(int applicationId, CommitState commitState, string changeStateDescription)
		{
			var application = await context.Set<T>()
			   .Where(s => s.Id == applicationId)
			   .SingleOrDefaultAsync();

			application.CommitState = commitState;

			application.ChangeStateDescription = changeStateDescription;

			await context.SaveChangesAsync();
		}

		public async Task<List<TReturnDto>> GetByState<TReturnDto>(CommitState commitState)
		{
			var applications = this.context.Set<T>()
				.AsNoTracking()
				.Where(e => e.CommitState == commitState)
				.AsQueryable();

			if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				applications = applications.Where(e => e.BankId == this.userContext.BankId);
			}

			return await applications
				.OrderByDescending(e => e.Id)
				.ProjectTo<TReturnDto>(this.mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<TReturnDto> SelectCredit<TReturnDto>(CreditSelectFilterDto dto)
			where TReturnDto : CreditSelectDto
		{
			await this.CheckExistRefuseContract(dto.BankId, dto.CreditType, dto.Uin, dto.CreditNumber, dto.ApplicationOneId);

			await this.CheckIsPaidByApplicationFour(dto.ApplicationOneId);

			var appOne = await this.context.Set<ApplicationOne>()
				.Where(c => c.BankId == dto.BankId
				&& c.CreditType == dto.CreditType
				&& c.CreditNumber == dto.CreditNumber
				&& c.Uin == dto.Uin)
				.OrderByDescending(c => c.Id)
				.ProjectTo<TReturnDto>(this.mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			if (appOne == null)
			{
				this.domainValidation.ThrowErrorMessage(CreditErrorCode.ApplicationOneNotApproved);
			}

			if (appOne.PersonStudentDoctoralId != null && appOne.PersonStudentDoctoralId != 0)
			{
				var student = await this.studentService.GetCreditStudentInfo(appOne.Uan, appOne.PersonStudentDoctoralId.Value, appOne.EducationType);
				return this.mapper.Map(student, appOne);
			}
			else
			{
				var student = await this.studentService.GetCreditStudentInfoFromSC(appOne.ApplicationOneId.Value);
				return this.mapper.Map(student, appOne);
			}
		}

		public async Task<List<ApplicationHistoryDto>> GetHistory(int rootId, ApplicationType type)
		{
			return await this.context.Set<T>()
				.Where(e => e.RootId == rootId && e.ApplicationHistory != null && e.ApplicationHistory.ApplicationType == type)
				.OrderByDescending(e => e.Id)
				.ProjectTo<ApplicationHistoryDto>(this.mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task CreateHistory(ApplicationHistoryDto dto, ApplicationType type)
		{
			var application = await this.context.Set<T>()
				.Include(e => e.ApplicationHistory)
				.SingleOrDefaultAsync(e => e.Id == dto.ApplicationId);

			if (application.ApplicationHistory != null)
			{
				this.context.Entry(application.ApplicationHistory).State = EntityState.Deleted;
			}

			application.ApplicationHistory = new ApplicationHistory
			{
				ApplicationId = dto.ApplicationId.Value,
				ModificationDate = DateTime.Now,
				Description = dto.Description,
				UserId = userContext.UserId,
				UserFullName = await this.context.Set<User>().Where(e => e.Id == userContext.UserId).Select(e => e.FirstName + " " + e.LastName).SingleAsync(),
				ApplicationHistoryType = dto.ApplicationHistoryType,
				ApplicationType = type
			};

			await this.context.SaveChangesAsync();
		}

		public async Task<int> Edit(TDto dto)
		{
			using (var transaction = await context.BeginTransactionAsync())
			{
				try
				{
					var application = this.mapper.Map<TDto, T>(dto);

					this.context.Set<T>().Add(application);

					await this.context.SaveChangesAsync();

					this.context.Set<T>()
						.Where(e => e.Id == dto.Id)
						.ExecuteUpdate(x => x.SetProperty(a => a.CommitState, CommitState.Archived));

					await transaction.CommitAsync();

					return application.Id;
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw;
				}
			}
		}

		protected async Task CheckExistRefuseContract(int bankId, CreditType creditType, string uin, string creditNumber, int? applicatioOneId)
		{
			var existRefuseContract = false;

			if (applicatioOneId.HasValue)
			{
				existRefuseContract = await this.context.Set<ApplicationOne>()
					.AnyAsync(a => a.Id == applicatioOneId
							&& a.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT
							&& a.CommitState != CommitState.Archived);
			}
			else
			{
				existRefuseContract = await this.context.Set<ApplicationOne>()
					.AnyAsync(a => a.BankId == bankId
							&& a.CreditType == creditType
							&& a.Uin == uin
							&& a.CreditNumber == creditNumber
							&& a.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT
							&& a.CommitState != CommitState.Archived);
			}

			if (existRefuseContract)
			{
				this.domainValidation.ThrowErrorMessage(CreditErrorCode.CreditExistWithRefuseContract);
			}
		}

		protected async Task CheckIsPaidByApplicationFour(int applicationOneId)
		{
			var isPaidByApplicationFour = await this.context.Set<ApplicationOne>()
				.Where(a => a.Id == applicationOneId)
				.Select(a => a.PaidByApplicationFour)
				.SingleOrDefaultAsync();

			if (isPaidByApplicationFour == true)
			{
				this.domainValidation.ThrowErrorMessage(CreditErrorCode.ApplicationOnePaidByApplicationFour);
			}
		}
	}
}