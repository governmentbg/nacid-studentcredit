using StudentCredit.Infrastructure.Utils.Models;

namespace StudentCredit.Infrastructure.Utils
{
	public static class MimeTypeHelper
	{
		public const string OOXML_EXCEL = "ooxmlExcel";
		public const string PDF = "pdf";

		private static readonly Dictionary<string, MimeExtension> extensions = new Dictionary<string, MimeExtension> {
			{ OOXML_EXCEL, new MimeExtension(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") },
			{ PDF, new MimeExtension(".pdf", "application/pdf") }
		};

		public static MimeExtension GetExtensionWithMime(string type)
		{
			if (!extensions.TryGetValue(type, out var result))
			{
				throw new Exception("Unsupported type");
			}

			return result;
		}
	}
}
