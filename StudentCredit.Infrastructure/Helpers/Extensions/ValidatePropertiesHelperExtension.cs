using System.Text.RegularExpressions;

namespace StudentCredit.Infrastructure.Helpers.Extensions
{
	public static class ValidatePropertiesHelperExtension
	{
		public static bool IsValidCyrillicAndNumbers(string value)
		{
			return new Regex(@"^[А-Яа-я-, 0-9'\s]+$").IsMatch(value);
		}
	}
}
