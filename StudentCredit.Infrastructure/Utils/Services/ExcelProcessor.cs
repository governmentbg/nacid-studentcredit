using StudentCredit.Infrastructure.Utils.Interfaces;
using System.Linq.Expressions;
using System.Reflection;
using OfficeOpenXml;

namespace StudentCredit.Infrastructure.Utils.Services
{
	public class ExcelProcessor : IExcelProcessor
	{
		private readonly IEnumUtility utility;

		public ExcelProcessor(IEnumUtility utility)
		{
			this.utility = utility;
		}

		public void FillWorksheetCells<T, TResult>(ExcelWorksheet worksheet, IEnumerable<T> items, int row, int col = 1, params Expression<Func<T, TResult>>[] expr)
		{
			var headers = new List<string>();
			var memberExpressions = new List<MemberExpression>();

			GetHeadersAndMembers(out headers, out memberExpressions, expr);

			bool[] isFormatedMaxCols = new bool[headers.Count];

			foreach (var header in headers)
			{
				worksheet.Cells[row, col].Value = header;
				worksheet.Cells[row, col].Style.Font.Bold = true;
				col++;
			}

			foreach (var item in items)
			{
				col = 1;
				row++;
				foreach (var memberExpression in memberExpressions)
				{
					object value = null;
					if (memberExpression.Expression.Type == typeof(T))
					{
						value = item.GetType().GetProperty(memberExpression.Member.Name).GetValue(item, null);
					}
					else
					{
						var resultValue = this.GetNestedProperties(item, memberExpression.Expression.ToString().Substring(2));
						if (resultValue == null)
						{
							value = null;
						}
						else
						{
							value = resultValue.GetType().GetProperty(memberExpression.Member.Name).GetValue(resultValue, null);
						}
					}
					if (value != null
						&& value.GetType().BaseType == typeof(Enum))
					{
						worksheet.Cells[row, col].Value = this.utility.GetDescription(value);
					}
					else
					{
						worksheet.Cells[row, col].Value = value;
					}

					var fieldType = memberExpression.Type;
					if (fieldType == typeof(Boolean)
						|| fieldType == typeof(Boolean?))
					{
						var obj = (bool)worksheet.Cells[row, col].Value;
						string boolValue = "Не";
						if (obj)
						{
							boolValue = "Да";
						}
						worksheet.Cells[row, col].Value = boolValue;
					}

					worksheet.Cells[row, col].Style.Numberformat.Format = this.GetCellFormatting(fieldType);

					if (!isFormatedMaxCols[col - 1]
						&& worksheet.Cells[row, col].Value != null)
					{
						int cellSize = worksheet.Cells[row, col].Value.ToString().Length;
						if (cellSize > 80)
						{
							worksheet.Column(col).Width = 80;
							worksheet.Column(col).Style.WrapText = true;
							isFormatedMaxCols[col - 1] = true;
						}
					}
					col++;
				}
			}

			for (int i = 0; i <= headers.Count - 1; i++)
			{
				if (!isFormatedMaxCols[i])
				{
					worksheet.Column(i + 1).AutoFit();
				}
			}
		}

		public object GetNestedProperties(object original, string properties)
		{

			string[] namesOfProperties = properties.Split('.');
			int size = namesOfProperties.Length - 1;

			PropertyInfo property = original.GetType().GetProperty(namesOfProperties[0]);
			object propValue = property.GetValue(original, null);

			for (int i = 1; i <= size; i++)
			{
				property = propValue.GetType().GetProperty(namesOfProperties[i]);
				propValue = property.GetValue(propValue, null);
			}

			return propValue;
		}

		public void GetHeadersAndMembers<T, TResult>(out List<string> headers, out List<MemberExpression> memberExpressions, params Expression<Func<T, TResult>>[] expressions)
		{
			headers = new List<string>();
			memberExpressions = new List<MemberExpression>();

			foreach (var item in expressions)
			{
				var expression = item.Body as MemberInitExpression;
				var bindings = expression.Bindings;

				foreach (var binding in bindings)
				{
					dynamic obj = binding;

					var member = obj.Expression as MemberExpression;
					var unary = obj.Expression as UnaryExpression;
					var result = member ?? (unary != null ? unary.Operand as MemberExpression : null);

					if (result == null)
					{
						headers.Add(obj.Expression.Value);
					}
					else
					{
						memberExpressions.Add(result);
					}
				}
			}
		}

		public string GetCellFormatting(Type fieldType)
		{
			if (fieldType == typeof(DateTime)
				|| fieldType == typeof(DateTime?))
			{
				return "dd-mm-yyyy";
			}
			else if (fieldType == typeof(Double)
				|| fieldType == typeof(Double?)
				|| fieldType == typeof(Decimal)
				|| fieldType == typeof(Decimal?))
			{
				return "0.00";
			}

			return null;
		}
	}
}
