using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaba.Birthday.Reminder.BusinessLogic.Data
{
	public class ScheduledEvent
	{

		public ScheduledEvent(long userId)
		{
			UserId = userId;
		}

		[Key]
		[Required] 
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[ForeignKey("User")]
		public long UserId { get; }

		public User User { get; }
		
		public DateTime Date { get; set; }

		public string NameOfEvent { get; set; }

		public string CelebratedPerson { get; set; }

		public string CongratulationsText { get; set; }
	}
}
