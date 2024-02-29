using FileStorageNetCore.Models;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.DomainValidation.Enums;
using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFive.Dtos
{
	public class ApplicationFiveDto : ApplicationCommitDto
	{
		public ApplicationFiveType ApplicationFiveType { get; set; }

		public NomenclatureDto Bank { get; set; }

		public decimal AmountRequested { get; set; }

		public decimal? AmountRequestedCorrection { get; set; }

		public decimal? AmountRequestedAfterCorrection { get; set; }

		public int? CreditCount { get; set; }

		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public AttachedFile ApplicationFiveAttachedFile { get; set; }

		public YearPeriod? Period { get; set; }

		public NomenclatureDto Year { get; set; }

		public override void ValidateProperties(DomainValidationService validationService)
		{
			if (this.Bank == null)
			{
				validationService.ThrowErrorMessage(ApplicationFiveErrorCode.BankEmpty);
			}

			if (this.AmountRequested < 0)
			{
				validationService.ThrowErrorMessage(ApplicationFiveErrorCode.AmountRequestedMustBePositive);
			}

			if (this.ApplicationFiveType == ApplicationFiveType.TotalDebtExposure)
			{
				if (!this.CreditCount.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.CreditCountEmpty);
				}

				if (this.ApplicationFiveAttachedFile == null)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.AttachedFileEmpty);
				}

				if (this.CreditCount < 1)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.CreditCountMustBePositive);
				}
			}

			if (this.ApplicationFiveType == ApplicationFiveType.RepaidCreditObligationsInTheRelevantYear)
			{
				if (!this.CreditCount.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.CreditCountEmpty);
				}

				if (this.Year == null)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.YearEmpty);
				}

				if (this.Period == null)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.PeriodEmpty);
				}

				if (this.ApplicationFiveAttachedFile == null)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.AttachedFileEmpty);
				}

				if (this.CreditCount < 1)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.CreditCountMustBePositive);
				}
			}

			if (this.ApplicationFiveType == ApplicationFiveType.BankExpensesForTheEnforcementActions)
			{
				if (!this.From.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.FromDateEmpty);
				}

				if (!this.To.HasValue)
				{
					validationService.ThrowErrorMessage(ApplicationFiveErrorCode.ToDateEmpty);
				}
			}
		}
	}
}
