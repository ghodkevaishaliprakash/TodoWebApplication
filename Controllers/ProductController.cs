using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;
using ProductInfo.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
       
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;   
        }
       
        [HttpGet, Authorize]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService.GetAllProductDetails();
        }

        [HttpPost, Authorize]
        public async Task<Product> Create([FromBody] ProductReq req)
        {
            var result = await _productService.CreateProduct(req);
            return result;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<Product> GetId(int id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpPut, Authorize]
        public async Task<Product> Update(int id, [FromBody] Product req)
        {
            var result = await _productService.UpdateRecord(id, req);
            return result;
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<string> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id);
            
            return "Successfully deleted record by id: " +id;
        }


        [HttpGet, Authorize]
        [Route("filterby")]
        public async Task<List<Product>> Get([FromQuery] string filterby,int PageNo,int PageSize)
        {
            return await _productService.FilterResult(filterby,PageNo,PageSize);
        }

    }
}

    


