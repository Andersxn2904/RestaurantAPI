using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Order;

namespace RestaurantAPI.Controllers.v1
{
    #region Settings
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
	[Authorize(Roles = "Waiter")]
	public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveOrderViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                }

                await _orderService.Add(vm);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region Get

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var OrderList = await _orderService.GetAllViewModelWithInclude();

                if (OrderList == null || OrderList.Count == 0)
                {
                    return NoContent();
                }

                return Ok(OrderList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int ID)
        {
            try
            {
                OrderViewModel order = await _orderService.GetByIDWithIncludeModel(ID);

                if (order == null)
                {
                    return NoContent();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region Put
        [HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderDishes(int ID, UpdateOrderDishes Model)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                Model.OrderID = ID;
                await _orderService.UpdateOrderDishes(Model);
                return NoContent();


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {
            try
            {
                await _orderService.Delete(ID);
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
