namespace Chello.Core
{
	public class Config
	{
		public static string AuthKey { get; set; }
		public static string AuthToken { get; set; }
		public static string ApiBaseUrl { get; set; }

		static Config()
		{
			ApiBaseUrl = "https://api.trello.com/1";
		}
	}

	/// https://trello.com/1/connect?key=...&name=MyApp&response_type=token
	/// https://trello.com/1/connect?key=...&name=MyApp&response_type=token&scope=read,write
}
