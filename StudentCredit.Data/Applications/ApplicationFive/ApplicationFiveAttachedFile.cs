using FileStorageNetCore.Models;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.ApplicationFive
{
	public class ApplicationFiveAttachedFile : AttachedFile, IEntity
	{
		public int Id { get; set; }

		public int ApplicationFiveId { get; set; }
		public ApplicationFive ApplicationFive { get; set; }
	}
}
