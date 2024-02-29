using OfficeOpenXml;
using StudentCredit.Infrastructure.Interfaces.Registration;
using System.Linq.Expressions;

namespace StudentCredit.Infrastructure.Utils.Interfaces
{
	public interface IExcelProcessor : IScopedService
	{
		object GetNestedProperties(object original, string properties);

		string GetCellFormatting(Type fieldType);

		void GetHeadersAndMembers<T, TResult>(out List<string> headers, out List<MemberExpression> memberExpressions, params Expression<Func<T, TResult>>[] expressions);

		void FillWorksheetCells<T, TResult>(ExcelWorksheet worksheet, IEnumerable<T> items, int row, int col = 1, params Expression<Func<T, TResult>>[] expr);
	}
}
