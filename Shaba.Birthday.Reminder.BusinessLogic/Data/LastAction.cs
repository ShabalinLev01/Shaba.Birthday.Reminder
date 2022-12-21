using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaba.Birthday.Reminder.BusinessLogic.Data
{
	public class LastAction
	{
		public string Type { get; set; }

		public string Action { get; set; }

		public Guid? Id { get; set; }
	}
}
