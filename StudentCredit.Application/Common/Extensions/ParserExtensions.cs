using StudentCredit.Application.Applications.ApplicationsOne.Dtos;
using System.Globalization;

namespace StudentCredit.Application.Common.Extensions
{
	public static class ParserExtensions
	{
		public static DateTime? TryParseDate(DateMap date)
		{
			if (date != null)
			{
				if (date.Year != null && date.Month != null && date.Day != null)
				{
					return new DateTime(date.Year.Value, date.Month.Value, date.Day.Value);
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public static int? TryParseStringToNullableInt(string value)
		{
			int? parsedValue = null;
			if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int parsed))
			{
				parsedValue = parsed;
			}

			return parsedValue;
		}

		public static decimal? TryParseStringToNullableDecimal(string value)
		{
			var newValue = value.Replace(',', '.');

			decimal? parsedValue = null;
			if (decimal.TryParse(newValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal parsed))
			{
				parsedValue = parsed;
			}

			return parsedValue;
		}

		public static decimal TryParseStringToDecimal(string value)
		{
			var newValue = value.Replace(',', '.');

			decimal? parsedValue = null;
			if (decimal.TryParse(newValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal parsed))
			{
				parsedValue = parsed;
			}

			return parsedValue ?? 0;
		}

		public static string RoundDecimalWithTwoPlaces(this decimal value)
		{
			return value.ToString("#,##0.00");
		}

		public static string NumberFormatting(this int value)
		{
			return String.Format("{0:N0}", value);
		}
	}
}
