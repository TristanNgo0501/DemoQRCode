using DemoQRCode.Models;

namespace DemoQRCode.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>>GetProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task<bool>DeleteProduct(int productId);
    }
}
