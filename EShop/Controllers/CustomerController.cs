using Microsoft.AspNetCore.Mvc;
using EShop.Application.IServices;
using EShop.Application.Validation;
using Microsoft.AspNetCore.Authorization;
using EShop.Application.Dto;

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
         private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService,ILogger<CustomerController> logger){
            _customerService = customerService;
            _logger = logger;
        }
         /// <summary>
        /// Get Customer List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomerList")]
        public async Task<ActionResult> GetCustomerList()
        {
            _logger.LogInformation("Fetching all the GetCustomerList from the storage");

            var result = await _customerService.GetCustomerList();
            
            _logger.LogInformation("End Fetching all the GetCustomerList from the storage");

            return Ok(result);
        }

        /// <summary>
        /// Create customer request
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            var validator = new CreateCustomerValidator();
            var validationResult = validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _customerService.CreateCustomer(customer);
            return Ok(result);
        }
    }
    
}