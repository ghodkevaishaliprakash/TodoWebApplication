using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Errors;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public PersonService(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        public async Task<Persons> CreatePerson(ProductReq req)
        {
            var validator = await Validator(req);
            
            if (!string.IsNullOrEmpty(validator))
            {
                throw new BadRequest(validator);
            }
            var request = new Persons {Name=req.Name, Price= req.Price };
            var result  = await _dataAccessProvider.AddPersonsRecordAsync(request);
            if(result > 0)
            {
                return request;
            }
            throw new Exception("Error while saving data."); ;

        }

        public async Task<bool> DeletePersonById(int id)
        {
            try
            {
                await _dataAccessProvider.DeletePersonsRecordAsync(id);
                return true;
            }
            catch(Exception ex) { }
            throw new Exception("Error occured while deleting data");
        }

        public async Task<List<Persons>> FilterResult(string filter, int PageNo, int PageSize)
        {            
            return await _dataAccessProvider.FilterResult(filter,PageNo,PageSize);
        }

        public async Task<IEnumerable<Persons>> GetAllPersonDetails()
        {
            return await _dataAccessProvider.GetPersonRecordsAsync();
        }

        public async Task<Persons> GetPersonById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequest("Invalid Id.");
            }
            var response = await _dataAccessProvider.GetPersonRecordByIdAsync(id);
            if (response == null)
            {
                throw new NotFound("No record found with id = " + id);
            }
            return response;
        }

        public async Task<Persons> UpdateRecord(int id, Product req)
        {
            var response = new Persons();
            if (id <= 0)
            {
                throw new BadRequest("Invalid Id.");
            }
            
            var result = await _dataAccessProvider.UpdatePersonsRecordAsync(id,req);
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
