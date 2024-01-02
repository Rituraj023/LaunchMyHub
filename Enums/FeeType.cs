using System.ComponentModel.DataAnnotations;

namespace LaunchMyHub.Enums
{
	public enum FeeType : byte
	{
		Subscription,
		Product,
		[Display(Name = "One Time Fee")]
		OneTimeFee
	}
}
