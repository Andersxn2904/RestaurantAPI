using RestaurantAPI.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.Enums
{
	public enum TableStates
	{
		[StringValue("Available")]
		AVAILABLE,

		[StringValue("In process of Attention")]
		PROCESS,

		[StringValue("Attended")]
		ATTENDED
	}
}
