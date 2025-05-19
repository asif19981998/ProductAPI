using ECommerce.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace ECommerce.BLL.Product;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    public ProductService(IHttpClientFactory clientFactory,IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }
    public async Task<ProductCreateRequest> CreateProductAsync(ProductCreateRequest product)
    {
        var client = _clientFactory.CreateClient();
        var payload = new { name = product.Name, data = product.Data };
        var response = await client.PostAsJsonAsync($"{_configuration["URLs:DevApiUrl"]}", payload);

        if (response.IsSuccessStatusCode)
        {
            var createdProduct = await response.Content.ReadFromJsonAsync<ProductCreateRequest>();
            return createdProduct;
        }
        else
        {
            // Handle error response
            throw new Exception("Failed to create product.");
        }
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.DeleteAsync($"{_configuration["URLs:DevApiUrl"]}/{id}");
  
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            // Handle error response
            throw new Exception("Failed to delete product.");
        }

    }

    public async Task<IEnumerable<Models.Product>> GetAllProductsAsync(string? name,int page = 1,int pageSize = 10)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<Models.Product>>(_configuration["URLs:DevApiUrl"]);

        var filtered = string.IsNullOrEmpty(name)
        ? response
            : response.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        var paged = filtered.Skip((page - 1) * pageSize).Take(pageSize);

        return paged;
    }
}
