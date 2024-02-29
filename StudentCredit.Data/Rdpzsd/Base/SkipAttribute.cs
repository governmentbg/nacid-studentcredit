using System.Reflection;

namespace StudentCredit.Data.Rdpzsd.Base
{
	public class SkipAttribute : Attribute
	{
		public static bool IsDeclared(PropertyInfo propertyInfo)
			=> propertyInfo.GetCustomAttribute(typeof(SkipAttribute)) != null;
	}
}
