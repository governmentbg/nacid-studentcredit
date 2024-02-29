using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Application.Applications.ApplicationsOne.Constants
{
    public static class ApplicationOneImportConstants
	{
		public static readonly Dictionary<int, string> ApplicationOneTypeAliases = new()
		{
				{ 0, ApplicationOneTypeAlias.REFUSE_CONTRACT},
				{ 1, ApplicationOneTypeAlias.NEW_CONTRACT},
				{ 2, ApplicationOneTypeAlias.RENEGOTIATION},
				{ 3, ApplicationOneTypeAlias.EXPIRATION_FREE_PERIOD},
				{ 4, ApplicationOneTypeAlias.EARLY_DELLABILITY},
				{ 5, ApplicationOneTypeAlias.ENFORCEMENT_ACTION},
				{ 6, ApplicationOneTypeAlias.REPAYMENT_CREDIT}
		};

		public static readonly Dictionary<int, CourseType> CourseTypes = new()
		{
				{ 0, CourseType.First},
				{ 1, CourseType.Second},
				{ 2, CourseType.Third},
				{ 3, CourseType.Fourth},
				{ 4, CourseType.Fifth},
				{ 5, CourseType.Sixth},
				{ 6, CourseType.Seventh}
		};

		public static readonly Dictionary<int, YearType> DoctoralYears = new()
		{
				{ 0, YearType.FirstYear},
				{ 1, YearType.SecondYear},
				{ 2, YearType.ThirdYear},
				{ 3, YearType.FourthYear},
				{ 4, YearType.FifthYear}
		};

		public static readonly Dictionary<int, EducationType> EducationTypes = new()
		{
				{ 0, EducationType.Student },
				{ 1, EducationType.Doctoral }
		};

		public static readonly Dictionary<int, CreditType> CreditTypes = new()
		{
				{ 0, CreditType.EducationTaxes },
				{ 1, CreditType.Maintenance }
		};

		public static readonly Dictionary<int?, string> AdjournTypes = new()
		{
				{ 0, string.Empty },
				{ 1, AdjournTypeAlias.INCAPACITY_FOR_WORK },
				{ 2, AdjournTypeAlias.NEW_EDUCATIONALQUALIFICATION }
		};
	}
}
