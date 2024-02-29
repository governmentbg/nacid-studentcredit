using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.Base
{
	public abstract class Part<TPartInfo> : EntityVersion
		where TPartInfo : PartInfo
	{
		public TPartInfo PartInfo { get; set; }

		public PartState State { get; set; }
	}
}
