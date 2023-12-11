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
    public class ProductController : ControllerBase
    {
         private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService,ILogger<ProductController> logger){
            _productService = productService;
            _logger = logger;
        }
       
       /// <summary>
       /// Get Product list
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("GetProductList")]
        public async Task<ActionResult> GetProductList()
        {
            var result = await _productService.GetProductList();
            return Ok(result);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
             var validator = new CreateProductValidator();
            var validationResult = validator.Validate(product);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _productService.CreateProduct(product);
            return Ok(result);
        }
    }
    
}