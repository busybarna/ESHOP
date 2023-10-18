using Microsoft.AspNetCore.Mvc;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using OnlineLibraryShop.Application.Validation;
using EShop.Application.Dto;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
         private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService,ILogger<CartController> logger){
            _cartService = cartService;
            _logger = logger;
        }
        
        /// <summary>
        /// Add Product to Cart
        /// </summary>
        /// <param name="addCartRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCart")]
        public async Task<IActionResult> CreateCart([FromBody] Cart addCartRequest)
        {
             _logger.LogInformation("Creating cart to the storage");

            var validator = new CartValidator();
            var validationResult = validator.Validate(addCartRequest);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _cartService.CreateCart(addCartRequest);
            
            _logger.LogInformation("End cart to the storage");

            return Ok(result);
        }
        /// <summary>
        /// Get card List
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCartList/{CustomerId:int}")]
        public async Task<ActionResult> GetCartList(int CustomerId)
        {
            var result = await _cartService.GetCart(CustomerId);
            return Ok(result);
        }
    }
    
}