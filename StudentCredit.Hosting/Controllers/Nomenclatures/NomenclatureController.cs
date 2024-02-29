using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.InstitutionSpecialities.Dtos;
using StudentCredit.Application.InstitutionSpecialities.Services;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Application.Nomenclatures.Interfaces;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Users;
using StudentCredit.Hosting.Controllers.Common;

namespace StudentCredit.Hosting.Controllers.Nomenclatures
{
	public class CountryController : BaseNomenclatureController<Country, NomenclatureMapperDto<Country>, CountryNomenclatureFilterDto>
	{
		public CountryController(INomenclatureService<Country> service)
			: base(service)
		{

		}
	}

	public class InstitutionController : BaseNomenclatureController<Institution, NomenclatureMapperDto<Institution>, InstitutionFilterDto>
	{
		public InstitutionController(INomenclatureService<Institution> service)
			: base(service)
		{
		}
	}

	public class ResearchAreaController
			: BaseNomenclatureController<ResearchArea, NomenclatureMapperDto<ResearchArea>, BaseNomenclatureFilterDto<ResearchArea>>
	{
		public ResearchAreaController(INomenclatureService<ResearchArea> service)
			: base(service)
		{

		}
	}

	public class SpecialityController : BaseNomenclatureController<Speciality, NomenclatureMapperDto<Speciality>, BaseNomenclatureFilterDto<Speciality>>
	{
		private readonly InstitutionSpecialityService institutionSpecialitiesService;

		public SpecialityController(INomenclatureService<Speciality> service, InstitutionSpecialityService institutionSpecialitiesService)
			: base(service)
		{
			this.institutionSpecialitiesService = institutionSpecialitiesService;
		}

		[HttpGet("InstitutionSpecialities")]
		public async Task<IEnumerable<SpecialityInformationDto>> GetInstitutionSpecialities([FromQuery] SpecialityFilterDto filter)
			=> await this.institutionSpecialitiesService.GetInstitutionSpecialities(filter);
	}

	public class ApplicationOneTypeController
			: BaseNomenclatureController<ApplicationOneType, NomenclatureMapperDto<ApplicationOneType>, ApplicationOneTypeNomenclatureFilterDto>
	{
		public ApplicationOneTypeController(INomenclatureService<ApplicationOneType> service)
			: base(service)
		{
		}
	}

	public class ApplicationFourTypeController
			: BaseNomenclatureController<ApplicationFourType, NomenclatureMapperDto<ApplicationFourType>, BaseNomenclatureFilterDto<ApplicationFourType>>
	{
		public ApplicationFourTypeController(INomenclatureService<ApplicationFourType> service)
			: base(service)
		{

		}
	}

	public class AdjournTypeController
			: BaseNomenclatureController<AdjournType, NomenclatureMapperDto<AdjournType>, BaseNomenclatureFilterDto<AdjournType>>
	{
		public AdjournTypeController(INomenclatureService<AdjournType> service)
			: base(service)
		{

		}
	}

	public class BankNomenclatureController
			: BaseNomenclatureController<Bank, BankNomenclatureDto, BaseNomenclatureFilterDto<Bank>>
	{
		public BankNomenclatureController(INomenclatureService<Bank> service)
			: base(service)
		{

		}
	}

	public class EducationalQualificationController
        : BaseNomenclatureController<EducationalQualification, NomenclatureMapperDto<EducationalQualification>, EducationalQualificationNomenclatureFilterDto>
    {
        public EducationalQualificationController(INomenclatureService<EducationalQualification> service)
            : base(service)
        {

        }
    }

    public class EducationFormTypeController
        : BaseNomenclatureController<EducationFormType, NomenclatureMapperDto<EducationFormType>, BaseNomenclatureFilterDto<EducationFormType>>
    {
        public EducationFormTypeController(INomenclatureService<EducationFormType> service)
            : base(service)
        {

        }
    }

	public class ExtensionOfGracePeriodController
			: BaseNomenclatureController<ExtensionOfGracePeriod, NomenclatureMapperDto<ExtensionOfGracePeriod>, BaseNomenclatureFilterDto<ExtensionOfGracePeriod>>
	{
		public ExtensionOfGracePeriodController(INomenclatureService<ExtensionOfGracePeriod> service)
			: base(service)
		{

		}
	}

    public class RoleController
            : BaseNomenclatureController<Role, NomenclatureMapperDto<Role>, BaseNomenclatureFilterDto<Role>>
    {
        public RoleController(INomenclatureService<Role> service)
            : base(service)
        {

        }
    }

    public class YearController
			: BaseNomenclatureController<Year, NomenclatureMapperDto<Year>, BaseNomenclatureFilterDto<Year>>
	{
		private readonly IYearService yearService;

		public YearController(INomenclatureService<Year> service, IYearService yearService)
			: base(service)
		{
			this.yearService = yearService;
		}

		[HttpGet("summaryReportYears")]
		public async Task<IEnumerable<NomenclatureMapperDto<Year>>> GetSummaryReportYears([FromQuery] BaseNomenclatureFilterDto<Year> filter)
			=> await this.yearService.GetSummaryReportYears(filter);
	}
}