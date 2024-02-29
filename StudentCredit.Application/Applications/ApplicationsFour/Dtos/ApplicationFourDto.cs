using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.DomainValidation.Enums;
using StudentCredit.Data.Applications.ApplicationFour.Enums;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFour.Dtos
{
    public class ApplicationFourDto : ApplicationCommitDto
    {
        public NomenclatureDto Type { get; set; }

		public int? ApplicationOneId { get; set; }

		public decimal? BankExpenses { get; set; }

		#region BankInfo
		public NomenclatureDto Bank { get; set; }
        public string BULSTAT { get; set; }
        public string BankAccount { get; set; }
		#endregion

		#region StudentInfo
		public int? PersonStudentDoctoralId { get; set; }
		public string StudentFullName { get; set; }
        public string Uin { get; set; }
        public NomenclatureDto Institution { get; set; }
        public NomenclatureDto Speciality { get; set; }
        public string UAN { get; set; }
        public string FacultyNumber { get; set; }
		public DateTime? BirthDate { get; set; }
        public EducationType EducationType { get; set; }
		#endregion

		#region CreditInfo
		public string CreditNumber { get; set; }
        public CreditType CreditType { get; set; }
        public DateTime ContractDate { get; set; }
        public int? SchoolRemaining { get; set; }
        public int PaymentPeriod { get; set; }
        public decimal Interest { get; set; }
        public string Description { get; set; }
        public decimal OutstandingDebtAmount { get; set; }
        public List<ApplicationFourAttachedFileDto> Files { get; set; }
		#endregion

		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }

		public override void ValidateProperties(DomainValidationService validationService)
		{
			if (this.Type == null)
			{
				validationService.ThrowErrorMessage(ApplicationFourErrorCode.ApplicationFourTypeEmpty);
			}

			if (string.IsNullOrEmpty(this.BankAccount))
			{
				validationService.ThrowErrorMessage(ApplicationFourErrorCode.BankAccountEmpty);
			}

			if (this.PaymentPeriod < 0
				|| this.Interest < 0
				|| this.OutstandingDebtAmount < 0)
			{
                validationService.ThrowErrorMessage(ApplicationFourErrorCode.ValueMustBeGreaterThanZero);
            }

			if (string.IsNullOrEmpty(this.Description))
			{
                validationService.ThrowErrorMessage(ApplicationFourErrorCode.InvalidFourDescription);
            }

            if (!ValidateRequiredFile(ApplicationFourAttachedFileType.DebtStatementDocument))
            {
                validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
            }

            if (!ValidateRequiredFile(ApplicationFourAttachedFileType.CreditContractAndAnnexes))
            {
                validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
            }

            if (this.Type.Alias == ApplicationFourTypeAlias.DEATH)
            {
                if (!ValidateRequiredFile(ApplicationFourAttachedFileType.DeathCertificate))
                {
                    validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
                }
			}

            if (this.Type.Alias == ApplicationFourTypeAlias.PERMANENT_INCAPACITY)
            {
                if (!ValidateRequiredFile(ApplicationFourAttachedFileType.PermanentIncapacity))
                {
                    validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
                }
            }

            if (this.Type.Alias == ApplicationFourTypeAlias.BIRTH_OR_FULL_ADOPTATION)
            {
                if (!ValidateRequiredFile(ApplicationFourAttachedFileType.BirthCertificatesOrCourtDecisionsForAdoptions))
                {
                    validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
                }
            }

            if (this.Type.Alias == ApplicationFourTypeAlias.BANK_CLAIM)
            {
                if (!ValidateRequiredFile(ApplicationFourAttachedFileType.DocumentInFavorOfTheBank))
                {
                    validationService.ThrowErrorMessage(ApplicationFourErrorCode.MissingRequiredAttachedFile);
                }
            }

			
        }

        private bool ValidateRequiredFile(ApplicationFourAttachedFileType requiredType)
        {
            return this.Files.Where(s => s.Type == requiredType).SingleOrDefault().File != null;
        }
    }
}
