using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Category;
using RestaurantAPI.Core.Application.ViewModels.Ingredient;

namespace RestaurantAPI.Controllers.v1
{
    #region Settings
    [ApiController]
    [Route("api/Ingredient")]
    [Authorize(Roles = "Admin")]
	[ApiVersion("1.0")]
	public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] SaveIngredientViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _ingredientService.Add(vm);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int ID, SaveIngredientViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                vm.ID = ID;
                await _ingredientService.Update(vm, ID);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var ingredientList = await _ingredientService.GetAllViewModel();

                if (ingredientList == null || ingredientList.Count == 0)
                {
                    return NoContent();
                }

                return Ok(ingredientList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int ID)
        {
            try
            {
                SaveIngredientViewModel ingredient = await _ingredientService.GetByIdSaveViewModel(ID);

                if (ingredient == null)
                {
                    return NoContent();
                }

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

    }
}
