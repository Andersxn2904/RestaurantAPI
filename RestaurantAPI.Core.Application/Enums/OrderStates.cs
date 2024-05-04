using RestaurantAPI.Core.Application.Helpers;

namespace RestaurantAPI.Core.Application.Enums
{
	public enum OrderStates
	{
		[StringValue("In process")]
		PROCESS,

		[StringValue("Completed")]
		COMPLETED,
	}
}
