namespace Kinobot.Net.Models
{
	public abstract class Media
	{
		public abstract MediaType MediaType { get; }
	}

	public enum MediaType
	{
		Movie,
		TVShow
	}
}