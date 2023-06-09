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

        public async Task<int> AddPersonsRecordAsync(Persons person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            int id =person.Id;

            return id;
        }

        public async Task<bool> UpdatePersonsRecordAsync(int id, Product req)
        {
            // var entity =await _context.Persons.FirstOrDefaultAsync(t => t.Id == id);
            var Productdata = _context.Persons.FirstOrDefault(t => t.Id == id);
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

            //_context.Persons.Update(personData);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeletePersonsRecordAsync(int id)
        {
            var entity = _context.Persons.FirstOrDefault(t => t.Id == id);
            if(entity == null)
            {
                throw new NotFound("No record found by id " + id);
            }
            _context.Persons.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Persons>> GetPersonRecordsAsync()
        {
            var response = await _context.Persons.ToListAsync();
            return response;
        }

        public async Task<Persons> GetPersonRecordByIdAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Persons>> FilterResult(string filter,int PageNo,int PageSize)
        {
            var query = _context.Persons.AsQueryable();
            //var Page_No = PageNo;
            //var PageSize_No = PageSize;
            try
            {

                if (!string.IsNullOrEmpty(filter.Trim().ToLowerInvariant()))
                {
                    query = _context.Persons.Where(p => p.Name.ToLower().Contains(filter.ToLower().Trim()));
                    
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            return query.Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();

            // return query.ToList();
            //return new <Persons>(query, Page_No, PageSize_No);
            //var responce = await _context.Persons.Take(PageSize_No).ToListAsync();

            // return new PagedList<Movie>(
            //query, FilteringParams.PageNumber, PageNo.PageSize);
            //  return query.ToList();
            // return query.Take(3).ToList();  
        }
    }
}

