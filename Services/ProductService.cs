using WebApplication1.DataAccess;
using WebApplication1.Errors;
using WebApplication1.Models;

namespace ProductInfo.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        public ProductService(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<IEnumerable<Product>> GetAllProductDetails()
        {
            return await _dataAccessProvider.GetProductRecordsAsync();
        }
        public async Task<Product> CreateProduct(ProductReq req)
        {
            var validator = await Validator(req);

            if (!string.IsNullOrEmpty(validator))
            {
                throw new BadRequest(validator);
            }
            var request = new Product { Name = req.Name, Price = req.Price };
            var result = await _dataAccessProvider.AddProductRecordAsync(request);
            if (result > 0)
            {
                return request;
            }
            throw new Exception("Error while saving data."); 

        }

        public async Task<bool> DeleteProductById(int id)
        {
            try
            {
                await _dataAccessProvider.DeleteProductRecordAsync(id);
                return true;
            }
            catch (Exception ex) { }
            throw new Exception("Error occured while deleting data");
        }

        public async Task<List<Product>> FilterResult(string filter, int PageNo, int PageSize)
        {
            return await _dataAccessProvider.FilterResult(filter, PageNo, PageSize);
        }

        public async Task<IEnumerable<Product>> GetAllPersonDetails()
        {
            return await _dataAccessProvider.GetProductRecordsAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequest("Invalid Id.");
            }
            var response = await _dataAccessProvider.GetProductRecordByIdAsync(id);
            if (response == null)
            {
                throw new NotFound("No record found with id = " + id);
            }
            return response;
        }

        public async Task<Product> UpdateRecord(int id, Product req)
        {
            var response = new Product();
            if (id <= 0)
            {
                throw new BadRequest("Invalid Id.");
            }

            var result = await _dataAccessProvider.UpdateProductRecordAsync(id, req);
            if (result)
            {
                response.Id = id;
                response.Price = req.Price;
                response.Name = req.Name;
                return response;
            }
            throw new Exception("An error occured while saving data"); ;
        }

        #region "Validation"
        private async Task<string> Validator(ProductReq req)
        {

            if (string.IsNullOrEmpty(req.Name))
            {
                return "Invalid Name";
            }
            if (req.Price <= 0)
            {
                return "Invalid Price";
            }

            return string.Empty;
        }


        #endregion

    }
}
