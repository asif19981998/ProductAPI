using ECommerce.BLL.Product;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAllProducts")]
        public  async Task<IEnumerable<Product>> GetAllProductsAsync([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _productService.GetAllProductsAsync(name, page, pageSize);
            return response;
        }

        [HttpPost("createProduct")]

        public async Task<ProductCreateRequest> CreateProductAsync([FromBody] ProductCreateRequest product)
        {
            var response = await _productService.CreateProductAsync(product);
            return response;
        }

        [HttpDelete("deleteProduct/{id}")]

        public async Task<bool> DeleteProductAsync(string id)
        {
            var response = await _productService.DeleteProductAsync(id);
            return response;
        }

    }
}
