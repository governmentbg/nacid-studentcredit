using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationTwo;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Credits
{
	public class CreditService : ICreditService
	{
		private readonly IAppDbContext context;
		private readonly IMapper mapper;

		public CreditService(
			IAppDbContext context,
			IMapper mapper
			)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<SearchResultItemDto<CreditSearchResultItemDto>> GetCreditsFiltered(SearchCreditFilter filter)
		{
			var query = this.context.Set<ApplicationOne>()
				.Where(c => (c.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT || c.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT)
				&& c.CommitState == filter.CommitState)
				.AsQueryable();

			if (filter.BankId.HasValue)
			{
				query = query.Where(c => c.BankId == filter.BankId);
			}

			if (!string.IsNullOrWhiteSpace(filter.Uin))
			{
				query = query.Where(c => c.Uin.Trim().ToLower() == filter.Uin.Trim().ToLower());
			}

			if (!string.IsNullOrWhiteSpace(filter.CreditNumber))
			{
				query = query.Where(c => c.CreditNumber.Trim().ToLower() == filter.CreditNumber.Trim().ToLower());
			}

			if (!string.IsNullOrWhiteSpace(filter.Uan))
			{
				query = query.Where(c => c.Uan.Trim().ToLower() == filter.Uan.Trim().ToLower());
			}

			if (filter.ContractDate.HasValue)
			{
				query = query.Where(c => c.ContractDate == filter.ContractDate);
			}

			if (filter.Type.HasValue)
			{
				query = query.Where(c => c.CreditType == filter.Type);
			}

			if (filter.InstitutionId.HasValue)
			{
				query = query.Where(c => c.InstitutionId == filter.InstitutionId);
			}

			if (!string.IsNullOrWhiteSpace(filter.StudentFullName))
			{
				var studentFullNameFilter = $"%{filter.StudentFullName.Trim().ToLower()}%";
				query = query.Where(c => EF.Functions.ILike(c.StudentFullName.Trim().ToLower(), studentFullNameFilter));
			}

			var credits = await query
				.OrderByDescending(c => c.BlankDate)
				.Skip(filter.Offset)
				.Take(filter.Limit)
				.ProjectTo<CreditSearchResultItemDto>(this.mapper.ConfigurationProvider)
				.ToListAsync();

			return new SearchResultItemDto<CreditSearchResultItemDto>
			{
				Items = credits,
				TotalCount = query.Count()
			};
		}

		public async Task<CreditInfoDto> GetCreditInfo(int id)
		{
			var creditInfo = await this.context.Set<ApplicationOne>()
				.Where(c => c.Id == id)
				.ProjectTo<CreditInfoDto>(this.mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			creditInfo.ApplicationsOne = await this.context.Set<ApplicationOne>()
				.Where(a => a.BankId == creditInfo.Bank.Id 
				&& a.Uin == creditInfo.Uin 
				&& a.CreditNumber == creditInfo.CreditNumber 
				&& a.CreditType == creditInfo.CreditType
				&& a.CommitState != CommitState.Archived
				&& (creditInfo.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT 
				? a.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT
				: a.ApplicationOneType.Alias != ApplicationOneTypeAlias.REFUSE_CONTRACT)
				)
				.ProjectTo<CreditApplicationDto>(this.mapper.ConfigurationProvider)
				.OrderByDescending(a => a.BlankDate)
					.ThenByDescending(a => a.Id)
				.ToListAsync();

			creditInfo.ApplicationsTwo = await this.context.Set<RecordEntry>()
				.Where(a => a.Uin == creditInfo.Uin && a.CreditNumber == creditInfo.CreditNumber && a.ApplicationTwo.Bank.Id == creditInfo.Bank.Id)
				.ProjectTo<RecordEntryDto>(this.mapper.ConfigurationProvider)
				.OrderByDescending(a => a.Id)
				.ToListAsync();

			var applicationOneIds = creditInfo.ApplicationsOne.Select(e => e.Id).ToList();

			creditInfo.ApplicationsFour = await this.context.Set<ApplicationFour>()
				.Where(a => applicationOneIds.Contains(a.ApplicationOneId.Value) && a.CommitState != CommitState.Archived)
				.ProjectTo<CreditApplicationDto>(this.mapper.ConfigurationProvider)
				.OrderByDescending(a => a.BlankDate)
				.ToListAsync();

			return creditInfo;
		}

		public async Task PayCreditByApplicationFour(int bankId, CreditType creditType, string creditNumber, string uin)
		{
			var applicationOnes = await this.context.Set<ApplicationOne>()
				.Where(e => e.BankId == bankId
				&& e.CreditType == creditType
				&& e.CreditNumber == creditNumber
				&& e.Uin == uin)
				.ToListAsync();

			applicationOnes.ForEach(e => 
			{ 
				e.PaidByApplicationFour = true;
				e.PaidByApplicationFourDate = DateTime.Now.Date;
			});

			await this.context.SaveChangesAsync();
		}
	}
}
