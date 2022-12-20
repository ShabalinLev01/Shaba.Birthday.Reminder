using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaba.Birthday.Reminder.Repository.Data
{
	public class ScheduledEvent
	{
		[Key]
		[Required]
		public Guid Id { get; set; }

		public long UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public string NameOfEvent { get; set; }

		public string CelebratedPerson { get; set; }

		public string CongratulationsText { get; set; }
	}
}
