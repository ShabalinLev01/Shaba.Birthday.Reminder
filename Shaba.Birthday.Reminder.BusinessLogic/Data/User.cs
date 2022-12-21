using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaba.Birthday.Reminder.BusinessLogic.Data
{
	public class User
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long Id { get; set; }

		public Language? Language { get; set; }

		[MaxLength(255)]
		public string FirstName { get; set; }
		[MaxLength(255)]
		public string LastName { get; set; }
		[MaxLength(255)]
		public string Username { get; set; }

		public string? TimeZone { get; set; }

		public string? PhoneNumber { get; set; }

		public bool IsBlock { get; set; }
		
		public LastAction? LastAction { get; set; }
	}
}
