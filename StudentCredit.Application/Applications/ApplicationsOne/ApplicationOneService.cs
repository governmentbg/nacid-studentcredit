using Microsoft.EntityFrameworkCore;
using StudentCredit.Infrastructure.Helpers.Extensions;
using StudentCredit.Application.Applications.ApplicationsOne.Dtos;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Enums;
using AutoMapper;
using StudentCredit.Application.Applications.ApplicationsOne.Constants;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Application.Applications.Common.Services;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Application.Students;
using StudentCredit.Data.Banks;
using StudentCredit.Application.DomainValidation.Enums;

namespace StudentCredit.Application.Applications.ApplicationsOne
{
    public class ApplicationOneService : ApplicationService<ApplicationOne, ApplicationOneDto>, IApplicationOneService
	{
		private readonly IAppDbContext context;
		private readonly DomainValidationService validation;
		private readonly IMapper mapper;

		public ApplicationOneService(
			IAppDbContext context,
			DomainValidationService validation,
			IMapper mapper,
			IUserContext userContext,
			RoleService roleService,
			IStudentService studentService
			)
			: base(context, userContext, mapper, validation, roleService, studentService)
		{
			this.context = context;
			this.validation = validation;
			this.mapper = mapper;
		}

		public override async Task Create(ApplicationOneDto dto)
		{
			if (dto.IsNewCredit == true)
			{
				var isBankActive = await this.context.Set<Bank>()
					.Where(e => e.Id == dto.Bank.Id)
					.Select(e => e.IsActive)
					.SingleOrDefaultAsync();

				if (isBankActive == false)
				{
					this.validation.ThrowErrorMessage(ApplicationOneErrorCode.CannotAddNewCreditToInActiveBank);
				}
			}

			dto.ValidateProperties(this.validation);

			await this.CheckExistCredit(dto.Uin, dto.CreditType, dto.IsNewCredit, dto.ApplicationOneType.Alias, dto.Bank.Id, dto.CreditNumber);

			await this.CheckExistRefuseContract(dto.Bank.Id, dto.CreditType, dto.Uin, dto.CreditNumber, null);

			if (dto.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT)
			{
				await this.CheckNotAddNewContractAgain(dto.Uin, dto.CreditType, dto.Bank.Id, dto.CreditNumber);
			}

			using (var transaction = await this.context.BeginTransactionAsync())
			{
				try
				{
					this.SetStatus(dto);

					var applicationOne = this.mapper.Map<ApplicationOneDto, ApplicationOne>(dto);
					applicationOne.AgeAtContract = CalculateAgeAtContract(dto.ContractDate, dto.BirthDate);

                    applicationOne.CommitState = CommitState.Pending;

					this.context.Set<ApplicationOne>().Add(applicationOne);

					await this.context.SaveChangesAsync();

					applicationOne.RootId = applicationOne.Id;

					await this.context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<ApplicationOneDto> MapXmlToApplicationOneDto(ApplicationOneXml xmlDto)
		{
			try
			{
				var applicationOne = this.mapper.Map<ApplicationOneDto>(xmlDto);

				applicationOne.ApplicationOneType = await this.context.Set<ApplicationOneType>()
						.Where(at => at.Alias == ApplicationOneImportConstants.ApplicationOneTypeAliases[int.Parse(xmlDto.ApplicationOneType)])
						.Select(at => at.ToNomenclatureDto())
						.SingleOrDefaultAsync();

				applicationOne.AdjournType = await this.context.Set<AdjournType>()
						.Where(at => at.Alias == ApplicationOneImportConstants.AdjournTypes[ParserExtensions.TryParseStringToNullableInt(xmlDto.AdjournType) ?? 0])
						.Select(at => at.ToNomenclatureDto())
						.SingleOrDefaultAsync();

				applicationOne.Nationality = await this.context.Set<Country>()
						.Where(c => c.Name.Trim().ToLower() == xmlDto.Nationality.Trim().ToLower())
						.Select(c => c.ToNomenclatureDto())
						.SingleOrDefaultAsync();

				applicationOne.Institution = await this.context.Set<Institution>()
						.Where(i => i.Level == Level.First && i.Name.Trim().ToLower() == xmlDto.Institution.Trim().ToLower())
						.Select(i => i.ToNomenclatureDto())
						.SingleOrDefaultAsync();

				applicationOne.StudentCourse = ApplicationOneImportConstants.EducationTypes[int.Parse(xmlDto.EducationType)] == EducationType.Student
					? ApplicationOneImportConstants.CourseTypes[int.Parse(xmlDto.Course)]
					: null;
				applicationOne.DoctoralYear = ApplicationOneImportConstants.EducationTypes[int.Parse(xmlDto.EducationType)] == EducationType.Doctoral
					? ApplicationOneImportConstants.DoctoralYears[int.Parse(xmlDto.Course)]
					: null;

				return applicationOne;
			}
			catch (Exception)
			{
				this.validation.ThrowErrorMessage(CreditErrorCode.ImportXmlError);
				return null;
			}
		}

        public ApplicationOnePdfExportDto GetPdfExportDto(ApplicationOneDto applicationOneDto)
        {
            return this.mapper.Map<ApplicationOnePdfExportDto>(applicationOneDto);
        }

        private async Task CheckExistCredit(string uin, CreditType? creditType, bool? isNewCredit, string newApplicationOneTypeAlias, int bankId, string creditNumber)
		{
			var exist = await this.context.Set<ApplicationOne>()
				.AnyAsync(a => 
				a.Uin == uin && 
				a.CreditType == creditType && 
				a.BankId == bankId &&
				a.CreditNumber == creditNumber &&
				a.CommitState != CommitState.Archived);

			if (isNewCredit == true && exist)
			{
				this.validation.ThrowErrorMessage(CreditErrorCode.CreditExist);
			}

			if (isNewCredit == false && !exist)
			{
				this.validation.ThrowErrorMessage(CreditErrorCode.CreditDoesntExist);
			}

			if (exist && newApplicationOneTypeAlias == ApplicationOneTypeAlias.REFUSE_CONTRACT)
			{
				this.validation.ThrowErrorMessage(CreditErrorCode.CannotAddRefuseCreditToExistingCredit);
			}
		}

		private async Task CheckNotAddNewContractAgain(string uin, CreditType creditType, int bankId, string creditNumber)
		{
			var existNewContract = await this.context.Set<ApplicationOne>()
				.AnyAsync(a => a.Uin == uin && 
				a.CreditType == creditType && 
				a.BankId == bankId &&
				a.CreditNumber == creditNumber &&
				a.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT);

			if (existNewContract)
			{
				this.validation.ThrowErrorMessage(CreditErrorCode.CreditExistWithNewContract);
			}
		}

		private void SetStatus(ApplicationOneDto dto)
		{
			switch (dto.ApplicationOneType.Alias)
			{
				case ApplicationOneTypeAlias.REFUSE_CONTRACT:
					dto.ApplicationOneStatus = ApplicationOneStatus.Denied;
					break;
				case ApplicationOneTypeAlias.NEW_CONTRACT:
					dto.ApplicationOneStatus = ApplicationOneStatus.NewContract;
					break;
				case ApplicationOneTypeAlias.RENEGOTIATION:
					dto.ApplicationOneStatus = ApplicationOneStatus.Renegotiated;
					break;
				case ApplicationOneTypeAlias.EXPIRATION_FREE_PERIOD:
					dto.ApplicationOneStatus = ApplicationOneStatus.ExpirationOfFreePeriod;
					break;
				case ApplicationOneTypeAlias.EARLY_DELLABILITY:
					dto.ApplicationOneStatus = ApplicationOneStatus.RequiredInAdvance;
					break;
				case ApplicationOneTypeAlias.ENFORCEMENT_ACTION:
					dto.ApplicationOneStatus = ApplicationOneStatus.RequiredInAdvance;
					break;
				case ApplicationOneTypeAlias.REPAYMENT_CREDIT:
					dto.ApplicationOneStatus = ApplicationOneStatus.RepaidByBorrower;
					break;
			}
		}

        private int CalculateAgeAtContract(DateTime? contractDate, DateTime? birthDate)
        {
            var contractDateInt = int.Parse(contractDate.Value.ToString("yyyyMMdd"));
            var birthDateInt = int.Parse(birthDate.Value.ToString("yyyyMMdd"));

            return (contractDateInt - birthDateInt) / 10000;
        }
    }
}