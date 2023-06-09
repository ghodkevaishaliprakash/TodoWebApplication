using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.DataAccess
{
    public interface IDataAccessProvider
    {

        Task<int> AddPersonsRecordAsync(Persons req);
        Task<bool> UpdatePersonsRecordAsync(int id, Product req);
        Task DeletePersonsRecordAsync(int id);
        Task<Persons> GetPersonRecordByIdAsync(int id);
        Task<List<Persons>> GetPersonRecordsAsync();

        Task<List<Persons>> FilterResult(string filter,int PageNo,int PageSize);


    }
}
