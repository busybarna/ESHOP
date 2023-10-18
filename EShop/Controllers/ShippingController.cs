using Microsoft.AspNetCore.Mvc;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using EShop.Application.Dto;
using EShop.Application.Validation;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingController : ControllerBase
    {
         private readonly IShippingService _shippingService;
        private readonly ILogger<ShippingController> _logger;
        public ShippingController(IShippingService shippingService,ILogger<ShippingController> logger){
            _shippingService = shippingService;
            _logger = logger;
        }
       
       /// <summary>
       /// Get Shipping data for customer
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("GetShippingData/{CustomerId:int}")]
        public async Task<ActionResult> GetShippingData(int CustomerId)
        {
            var result = await _shippingService.GetShippingData(CustomerId);
            return Ok(result);
        }

        /// <summary>
        /// Create shipping data
        /// </summary>
        /// <param name="shipping"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateShippingData")]
        public async Task<IActionResult> CreateShippingData(Shipping shipping)
        {
             var validator = new CreateShippingValidator();
            var validationResult = validator.Validate(shipping);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _shippingService.CreateShippingData(shipping);
            return Ok(result);
        }
    }
    
}