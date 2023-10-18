using Microsoft.AspNetCore.Mvc;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Authorization; 

namespace EShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService,ILogger<OrderController> logger){
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// Creates Order for Customer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(int CustomerId)
        {
            var result = await _orderService.CreateOrder(CustomerId);
            return Ok(result);
        }

        /// <summary>
        /// Get Customer order
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomerOrder/{CustomerId:int}")]
        public async Task<ActionResult> GetCustomerOrder(int CustomerId)
        {
            var result = await _orderService.GetCustomerOrder(CustomerId);
            return Ok(result);
        }
    }

}