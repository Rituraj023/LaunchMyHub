namespace LaunchMyHub.Services
{
	public class AuthMessageSenderOptions
	{
		private static readonly string Emailkey = "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";

		public const string GridKey = "GridKey";
		public string? SendGridKey { get; set; } = Emailkey;
	}
}
