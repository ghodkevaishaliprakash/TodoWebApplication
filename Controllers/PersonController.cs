using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        private readonly IPersonService _personService;
        public PersonController(IDataAccessProvider dataAccessProvider, IPersonService personService)
        {
            _dataAccessProvider = dataAccessProvider;
            _personService = personService;
        }

        [HttpGet, Authorize]
        public async Task<IEnumerable<Persons>> Get()
        {
            return await _personService.GetAllPersonDetails();
        }

        [HttpPost, Authorize]
        public async Task<Persons> Create([FromBody] ProductReq req)
        {
            var result = await _personService.CreatePerson(req);
            return result;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<Persons> GetId(int id)
        {
            return await _personService.GetPersonById(id);
        }

        //[HttpPut, Authorize]
        //public async Task<IActionResult> Update1([FromBody] Persons req)
        //{
        //    await _personService.UpdateRecord1(req);
        //    return Ok();    
        //}
        [HttpPut, Authorize]
        public async Task<Persons> Update(int id, [FromBody] Product req)
        {
            var result = await _personService.UpdateRecord(id, req);
            return result;
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<string> DeleteConfirmed(int id)
        {
            var result = await _personService.DeletePersonById(id);
            
            return "Successfully deleted record by id: " +id;
        }


        [HttpGet, Authorize]
        [Route("filterby")]
        public async Task<List<Persons>> Get([FromQuery] string filterby,int PageNo,int PageSize)
        {
            return await _personService.FilterResult(filterby,PageNo,PageSize);
        }

    }
}

    


