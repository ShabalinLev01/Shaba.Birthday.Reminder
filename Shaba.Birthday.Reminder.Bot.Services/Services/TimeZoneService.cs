using GeoTimeZone;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class TimeZoneService
	{
		public string GetTimeZoneByCoordinates(double latitude, double longitude)
		{
			return TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
		}
	}
}
