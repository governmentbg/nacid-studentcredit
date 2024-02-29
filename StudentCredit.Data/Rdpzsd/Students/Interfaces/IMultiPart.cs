using StudentCredit.Data.Rdpzsd.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Interfaces
{
	public interface IMultiPart<TEntity, TLot> : ISinglePart<TEntity, TLot>
		where TLot : EntityVersion
		where TEntity : EntityVersion
	{
		int LotId { get; set; }
	}
}
