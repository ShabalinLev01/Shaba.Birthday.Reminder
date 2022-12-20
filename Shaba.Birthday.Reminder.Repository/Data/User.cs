using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaba.Birthday.Reminder.Repository.Data
{
	public class User
	{
		[Key]
		[Required]
		public long Id { get; set; }

		[Required]
		public Language Language { get; set; }

		[MaxLength(255)]
		public string FirstName { get; set; }
		[MaxLength(255)]
		public string LastName { get; set; }
		[MaxLength(255)]
		public string Username { get; set; }

		public string TimeZone { get; set; }

		public string PhoneNumber { get; set; }

		public bool IsBlock { get; set; }
	}
}
