using StudentCredit.Data.Rdpzsd.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Interfaces
{
	public interface ISinglePart<TEntity, TLot>
		where TLot : EntityVersion
		where TEntity : EntityVersion
	{
		TLot Lot { get; set; }
	}
}
