namespace ECommerce.BLL.Product;
using ECommerce.Models;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(string? name, int page = 1, int pageSize = 10);
    Task<ProductCreateRequest> CreateProductAsync(ProductCreateRequest product);
    Task<bool> DeleteProductAsync(string id);
}
