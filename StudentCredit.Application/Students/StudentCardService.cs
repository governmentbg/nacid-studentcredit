using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Students.Dtos;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Students
{
	public class StudentCardService : IStudentCardService
	{
		private readonly IAppDbContext context;

		public StudentCardService(IAppDbContext context)
		{
			this.context = context;
		}

		public async Task<List<StudentCardInfoDto>> GetStudentCreditsInfo(string uan)
		{
			var creditsInfo = await this.context.Set<ApplicationOne>()
				.Where(e => e.Uan == uan && e.CommitState == CommitState.Approved && e.ApplicationOneType.Alias != ApplicationOneTypeAlias.REFUSE_CONTRACT)
				.GroupBy(e => e.CreditNumber)
				.Select(e => new StudentCardInfoDto
				{
					ContractDate = e.OrderByDescending(c => c.Id).First().ContractDate.Value,
					CreditSize = e.OrderByDescending(c => c.Id).First().CreditSize,
					BankName = e.OrderByDescending(c => c.Id).First().Bank.Name,
					ExpirationDateOfGracePeriod = e.OrderByDescending(c => c.Id).First().ExpirationDateOfGracePeriod,
					InterestDue = e.OrderByDescending(c => c.Id).First().InterestDue,
					PrincipalAbsorbed = e.OrderByDescending(c => c.Id).First().PrincipalAbsorbed,
					CreditNumber = e.OrderByDescending(c => c.Id).First().CreditNumber,
					CreditType = e.OrderByDescending(c => c.Id).First().CreditType,
					NewExpirationDateOfGracePeriod = e.OrderByDescending(c => c.Id).First().NewExpirationDateOfGracePeriod,
					PaymentDate = e.OrderByDescending(c => c.Id).First().PaymentDate,
					Type = e.OrderByDescending(c => c.Id).First().ApplicationOneType.Name
				})
				.ToListAsync();

			return creditsInfo;
		}
	}
}
