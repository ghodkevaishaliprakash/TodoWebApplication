using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Errors;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public async Task<int> AddProductRecordAsync(Product req)
        {
            await _context.Product.AddAsync(req);
            await _context.SaveChangesAsync();
            int id = req.Id;

            return id;
        }

        public async Task<bool> UpdateProductRecordAsync(int id, Product req)
        {
            
            var Productdata = _context.Product.FirstOrDefault(t => t.Id == id);
            if (Productdata == null)
            {
                throw new NotFound("No record found for id = " + id);
            }
            if (req.Name != null)
            {
                Productdata.Name = req.Name;
            }
            if (!string.IsNullOrEmpty(req.Price.ToString()))
            {
                Productdata.Price = req.Price;
            }

            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteProductRecordAsync(int id)
        {
            var entity = _context.Product.FirstOrDefault(t => t.Id == id);
            if(entity == null)
            {
                throw new NotFound("No record found by id " + id);
            }
            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductRecordsAsync()
        {
            var response = await _context.Product.ToListAsync();
            return response;
        }

        public async Task<Product> GetProductRecordByIdAsync(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(t => t.Id == id);
       
        }

        public async Task<List<Product>> FilterResult(string filter,int PageNo,int PageSize)
        {
            var query = _context.Product.AsQueryable();
            //var Page_No = PageNo;
            //var PageSize_No = PageSize;
            try
            {

                if (!string.IsNullOrEmpty(filter.Trim().ToLowerInvariant()))
                {
                    query = _context.Product.Where(p => p.Name.ToLower().Contains(filter.ToLower().Trim()));
                    
                }
            }
            catch (Exception ex)
            { throw new Exception(ex.Message);
            }

            return query.Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
 
        }
    }
}

