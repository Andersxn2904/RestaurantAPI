using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Core.Application.Enums;
using RestaurantAPI.Core.Application.Helpers;
using RestaurantAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infraestructure.Persistence.Seeds
{
	public class TableStatusSeed : IEntityTypeConfiguration<TableStatus>
	{
		public void Configure(EntityTypeBuilder<TableStatus> builder)
		{
			builder.HasData(

			new TableStatus { Name = GetStringValueEnum.GetStringValue(TableStates.AVAILABLE), ID = 1 },
			new TableStatus { Name = GetStringValueEnum.GetStringValue(TableStates.PROCESS), ID = 2 },
			new TableStatus { Name = GetStringValueEnum.GetStringValue(TableStates.ATTENDED), ID = 3 }
			);
		}
	}
}
