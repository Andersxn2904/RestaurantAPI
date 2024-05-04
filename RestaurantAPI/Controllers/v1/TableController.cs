using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Table;

namespace RestaurantAPI.Controllers.v1
{
    #region Settings
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
	
	public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;

        public TableController(ITableService tableService, IOrderService orderService)
        {
            _tableService = tableService;
            _orderService = orderService;
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Post(SaveTableViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _tableService.Add(vm);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Admin")]
		[Authorize(Roles = "Waiter")]
		public async Task<IActionResult> Get()
        {
            try
            {
                var tables = await _tableService.GetAllViewModelWithInclude();

                if (tables == null || tables.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Waiter")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Get(int ID)
		{
			try
			{
				TableViewModel table = await _tableService.GetByIDWithIncludeModel(ID);

				if (table == null)
				{
					return NoContent();
				}

				return Ok(table);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("GetTableOrden/{TableID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTableViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Waiter")]
		public async Task<IActionResult> GetTableOrden(int TableID)
        {
            try
            {
                var orders = await _orderService.OrderInProcessByTable(TableID);

                if (orders == null)
                {
                    return NoContent();
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region PATCH

        [HttpPatch("Update/{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTableViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "Waiter")]
		public async Task<IActionResult> Update(int ID, UpdateTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                model.ID = ID;

                SaveTableViewModel vm = await _tableService.GetByIdSaveViewModel(ID);

                if (model.Description != null)
                {
                    vm.Description = model.Description;
                }

                if (model.PersonCapacity != null)
                {
                    vm.PersonCapacity = model.PersonCapacity;
                }

                await _tableService.Update(vm, ID);
                return Ok(vm);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("ChangeStatus/{ID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromRoute] int ID, ChangeStatusTableModel model)
        {
            try
            {
                SaveTableViewModel vm = await _tableService.GetByIdSaveViewModel(ID);
                vm.StatusID = model.StatusID;
                await _tableService.Update(vm, ID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
