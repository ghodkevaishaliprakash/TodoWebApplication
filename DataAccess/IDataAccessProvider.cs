using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.DataAccess
{
    public interface IDataAccessProvider
    {

        Task<int> AddProductRecordAsync(Product req);
        Task<bool> UpdateProductRecordAsync(int id, Product req);
        Task DeleteProductRecordAsync(int id);
        Task<Product> GetProductRecordByIdAsync(int id);
        Task<List<Product>> GetProductRecordsAsync();

        Task<List<Product>> FilterResult(string filter,int PageNo,int PageSize);


    }
}
