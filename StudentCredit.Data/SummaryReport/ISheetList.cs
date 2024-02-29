namespace StudentCredit.Data.SummaryReport
{
	public interface ISheetList<T>
		where T : SheetRowData
	{
		List<T> SheetList { get; set; }
	}
}
