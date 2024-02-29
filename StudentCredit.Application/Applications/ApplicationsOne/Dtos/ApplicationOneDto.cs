using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.DomainValidation.Enums;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Helpers.Dtos;
using StudentCredit.Infrastructure.Helpers.Extensions;

namespace StudentCredit.Application.Applications.ApplicationsOne.Dtos
{
	public class ApplicationOneDto : ApplicationCommitDto
	{
		public NomenclatureDto ApplicationOneType { get; set; }

		public ApplicationOneStatus ApplicationOneStatus { get; set; }

		public bool? IsNewCredit { get; set; }

		#region ДАННИ ЗА БАНКАТА
		public NomenclatureDto Bank { get; set; }

		public string BULSTAT { get; set; }

		public string ContactPerson { get; set; }
		#endregion

		#region ДАННИ ЗА КРЕДИТОПОЛУЧАТЕЛЯ
		public int? PersonStudentDoctoralId { get; set; }

		public EducationType EducationType { get; set; }

		public string StudentFullName { get; set; }

		public NomenclatureDto Nationality { get; set; }

		public NomenclatureDto SecondNationality { get; set; }

		public string Uin { get; set; }

		public string IdNumber { get; set; }

		public NomenclatureDto Institution { get; set; }

		public NomenclatureDto ResearchArea { get; set; }

		public NomenclatureDto Speciality { get; set; }

		public NomenclatureDto EducationalQualification { get; set; }

		public NomenclatureDto EducationFormType { get; set; }

		public CourseType? StudentCourse { get; set; }

		public YearType? DoctoralYear { get; set; }

		public string FacultyNumber { get; set; }

		public string Uan { get; set; }

		public DateTime? BirthDate { get; set; }
		public int AgeAtContract { get; set; }
		#endregion

		#region ДАННИ ЗА СКЛЮЧВАНЕ ИЛИ ОТКАЗ ЗА СКЛЮЧВАНЕ НА ДОГОВОР ЗА КРЕДИТ
		public CreditType CreditType { get; set; }

		public DateTime ContractDate { get; set; }

		public string CreditNumber { get; set; }

		public int SchoolRemaining { get; set; }

		public string CancelCondition { get; set; }

		public int? PaymentPeriod { get; set; }

		public decimal? CreditSize { get; set; }

		public decimal? SemesterTax { get; set; }

		public decimal? Interest { get; set; }

		public string Description { get; set; }
		#endregion

		#region ДАННИ ЗА ГРАТИСНИЯ ПЕРИОД
		public DateTime? ExpirationDateOfGracePeriod { get; set; }

		public decimal? MonthlyPayment { get; set; }

		public string PaymentDescription { get; set; }

		public decimal? PrincipalAbsorbed { get; set; }

		public decimal? InterestDue { get; set; }

		public decimal? OverallSize { get; set; }

		#endregion

		#region ДАННИ ЗА ПОГАСЯВАНЕ НА КРЕДИТА

		public DateTime? PaymentDate { get; set; }

		public DateTime? EarlyPaymentDate { get; set; }

		#endregion

		#region ДАННИ ЗА ПРЕДОГОВАРЯНЕ НА КРЕДИТА

		public RecontractionType? RecontractionType { get; set; }

		public string RecontractionInfo { get; set; }

		public DateTime? RecontractionDate { get; set; }

		public NomenclatureDto AdjournType { get; set; }

		public DateTime? AdjournDate { get; set; }

		public int? AdjournPeriod { get; set; }

		public NomenclatureDto ExtensionOfGracePeriod { get; set; }

		public DateTime? NewExpirationDateOfGracePeriod { get; set; }

		public decimal? PrincipalAbsorbedInOldBank { get; set; }

		public decimal? InterestDueInOldBank { get; set; }

		public decimal? OverallSizeInOldBank { get; set; }

		#endregion

		#region НАСТЪПВАНЕ НА ПРЕДСРОЧНА ИЗИСКУЕМОСТ И ПРЕДПРИЕМАНЕ НА ДЕЙСТВИЯ ПО ПРИНУДИТЕЛНО ИЗПЪЛНЕНИЕ ПО КРЕДИТА

		public DateTime? EarlyDemandOfTheLoan { get; set; }

		public DateTime? ForcePaymentDate { get; set; }

		public string ForcePaymentInfo { get; set; }

		#endregion

		#region ПОЛЕТА ОТ МИГРАЦИЯ

		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }

		public ExistingEnum? ResearchAreaEnum { get; set; }
		public string MigrationResearchAreaName { get; set; }
		#endregion

		public override void ValidateProperties(DomainValidationService validationService)
		{
			if (this.ApplicationOneType == null)
			{
				validationService.ThrowErrorMessage(ApplicationOneErrorCode.ApplicationOneTypeEmpty);
			}

			if (this.Bank == null)
			{
				validationService.ThrowErrorMessage(ApplicationOneErrorCode.BankEmpty);
			}

			if (!string.IsNullOrWhiteSpace(this.ContactPerson))
			{
				if (!ValidatePropertiesHelperExtension.IsValidCyrillicAndNumbers(this.ContactPerson))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidCyrillic);
				}

				if (this.ContactPerson.Length > 50)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}

			if (string.IsNullOrWhiteSpace(this.BULSTAT))
			{
				validationService.ThrowErrorMessage(ApplicationOneErrorCode.BulstatEmpty);
			}

			if (string.IsNullOrWhiteSpace(this.StudentFullName) || this.StudentFullName.Length < 3)
			{
				validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidStudentName);
			}

			if (this.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT)
			{
				if (string.IsNullOrWhiteSpace(this.CreditNumber))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.CreditNumberEmpty);
				}

				if (this.PaymentPeriod == null)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.PaymentPeriodEmpty);
				}

				if (this.CreditSize == null)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.CreditSizeEmpty);
				}

				if (this.SemesterTax == null)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.SemesterTaxesEmpty);
				}

				if (this.Interest == null)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InterestEmpty);
				}

				if (this.ExpirationDateOfGracePeriod == null)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.ExpirationOfGracePeriodEmpty);
				}
			}

			if (this.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT)
			{
				if (string.IsNullOrWhiteSpace(this.CancelCondition))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.CancelConditionEmpty);
				}
			}

			if (this.ApplicationOneType.Alias == ApplicationOneTypeAlias.EXPIRATION_FREE_PERIOD)
			{
				if (!this.PrincipalAbsorbed.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.PrincipalAbsorbedEmpty);
				}

				if (!this.InterestDue.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InterestDueEmpty);
				}

				if (!this.OverallSize.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.OverallSizeEmpty);
				}
			}

			if (this.ApplicationOneType.Alias == ApplicationOneTypeAlias.EARLY_DELLABILITY || this.ApplicationOneType.Alias == ApplicationOneTypeAlias.ENFORCEMENT_ACTION)
			{
				if (!this.EarlyDemandOfTheLoan.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.EarlyDemandOfTheLoanEmpty);
				}

				if (!this.ForcePaymentDate.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.ForcePaymentDateEmpty);
				}

				if (string.IsNullOrWhiteSpace(this.ForcePaymentInfo))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.ForcePaymentInfoEmpty);
				}
			}

			if (this.ApplicationOneType.Alias == ApplicationOneTypeAlias.RENEGOTIATION)
			{
				if (!this.RecontractionDate.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.RecontractionDateEmpty);
				}
			}

			if (this.SchoolRemaining < 0 
				|| (this.PaymentPeriod.HasValue && this.PaymentPeriod < 0) 
				|| (this.CreditSize.HasValue && this.CreditSize < 0)
				|| (this.SemesterTax.HasValue && this.SemesterTax < 0) 
				|| (this.Interest.HasValue && this.Interest < 0)
				|| (this.MonthlyPayment.HasValue && this.MonthlyPayment < 0)
				|| (this.PrincipalAbsorbed.HasValue && this.PrincipalAbsorbed < 0)
				|| (this.InterestDue.HasValue && this.InterestDue < 0)
				|| (this.OverallSize.HasValue && this.OverallSize < 0))
			{
				validationService.ThrowErrorMessage(ApplicationOneErrorCode.ValueMustBeGreaterThanZero);
			}

			if (!string.IsNullOrWhiteSpace(this.Description))
			{
				if (this.Description.Length > 500)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}

			if (!string.IsNullOrWhiteSpace(this.PaymentDescription))
			{
				if (!ValidatePropertiesHelperExtension.IsValidCyrillicAndNumbers(this.PaymentDescription))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidCyrillic);
				}

				if (this.PaymentDescription.Length > 300)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}

			if (!string.IsNullOrWhiteSpace(this.CancelCondition))
			{
				if (!ValidatePropertiesHelperExtension.IsValidCyrillicAndNumbers(this.CancelCondition))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidCyrillic);
				}

				if (this.CancelCondition.Length > 300)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}

			if (!string.IsNullOrWhiteSpace(this.ForcePaymentInfo))
			{
				if (this.ForcePaymentInfo.Length > 500)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}

			if (!string.IsNullOrWhiteSpace(this.RecontractionInfo))
			{
				if (!ValidatePropertiesHelperExtension.IsValidCyrillicAndNumbers(this.RecontractionInfo))
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidCyrillic);
				}

				if (this.RecontractionInfo.Length > 300)
				{
					validationService.ThrowErrorMessage(ApplicationOneErrorCode.InvalidLength);
				}
			}
		}
	}
}