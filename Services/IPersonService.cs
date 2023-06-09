using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Persons>> GetAllPersonDetails();
        Task<Persons> CreatePerson(ProductReq req);
        Task<Persons> GetPersonById(int id);
        Task<bool> DeletePersonById(int id);
        Task<Persons> UpdateRecord( int id, Product req);
      
        Task<List<Persons>> FilterResult(string filter,int PageNo,int PageSize);

    }
}
