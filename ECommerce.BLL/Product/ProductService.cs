using ECommerce.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace ECommerce.BLL.Product;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<ProductCreateRequest> CreateProductAsync(ProductCreateRequest product)
    {
        var client = _clientFactory.CreateClient();
        var payload = new { name = product.Name, data = product.Data };
        var response = await client.PostAsJsonAsync("https://api.restful-api.dev/objects", payload);

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
        var response = await client.DeleteAsync($"https://api.restful-api.dev/objects/{id}");

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
        var response = await client.GetFromJsonAsync<List<Models.Product>>("https://api.restful-api.dev/objects");

        var filtered = string.IsNullOrEmpty(name)
        ? response
            : response.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        var paged = filtered.Skip((page - 1) * pageSize).Take(pageSize);

        return paged;
    }
}
