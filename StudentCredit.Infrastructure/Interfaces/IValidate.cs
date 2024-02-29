using StudentCredit.Infrastructure.DomainValidation;

namespace StudentCredit.Infrastructure.Interfaces
{
	public interface IValidate
	{
		void ValidateProperties(DomainValidationService validationService);
	}
}
