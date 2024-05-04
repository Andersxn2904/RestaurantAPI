using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Category;
using RestaurantAPI.Core.Application.ViewModels.Dish;

namespace RestaurantAPI.Controllers.v1
{
    #region Settings
    [Route("api/[controller]")]
    [ApiController]
	[ApiVersion("1.0")]
	[Authorize(Roles = "Admin")]
	public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveDishViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _dishService.Add(vm);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

		#endregion
		
		#region PUT  

		[HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int ID, UpdateDishModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
				


				vm.ID = ID;
                await _dishService.UpdateDish(vm, ID);
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dishes = await _dishService.GetAllViewModelWithInclude();

                if (dishes == null || dishes.Count == 0)
                {
                    return NoContent();
                }

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int ID)
        {
            try
            {
                var dish = await _dishService.GetByIdViewModel(ID);

                if (dish == null)
                {
                    return NoContent();
                }

                return Ok(dish);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}
