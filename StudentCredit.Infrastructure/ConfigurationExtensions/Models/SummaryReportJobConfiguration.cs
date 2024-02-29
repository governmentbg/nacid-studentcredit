namespace StudentCredit.Infrastructure.ConfigurationExtensions.Models
{
	public class SummaryReportJobConfiguration
	{
		public int DayOfMonth { get; set; }
		public int StartHour { get; set; }
		public int StartMinute { get; set; }
		public int MonthToCreateSheet { get; set; }
		public bool JobEnabled { get; set; }
	}
}
