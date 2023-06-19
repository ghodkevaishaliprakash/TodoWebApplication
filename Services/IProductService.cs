using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
namespace ProductInfo.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductDetails();
        Task<Product> CreateProduct(ProductReq req);
        Task<Product> GetProductById(int id);
        Task<bool> DeleteProductById(int id);
        Task<Product> UpdateRecord(int id, Product req);

        Task<List<Product>> FilterResult(string filter, int PageNo, int PageSize);
    }
}
