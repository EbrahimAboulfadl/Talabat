using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Repositories;
using TalabatApi.DTOs;

namespace TalabatApi.Controllers
{  
    
    public class ProductsController : ApiBaseController

    {
        private readonly IGenericRepository<Product> productsRepository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }
        #region Without Specificaation
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var products = await productsRepository.GetAllAsync();
            return Ok(products);

        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetProductById(int id)
        {

            var product = await productsRepository.GetByIdAsync(id);
            return Ok(product);

        }
        #endregion
        #region With Specification 

        [HttpGet("ProductsWithSpecs")]

        public async Task<IActionResult> GetAllWithSpecAsync()
        {
            ProductWithBrandAndTypeSpecification specification = new();
            var products = await productsRepository.GetAllWithSpecAsync(specification);
            var prdouctsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return Ok(prdouctsDto);

        }

        [HttpGet("ProductsWithSpecs/{name:alpha}")]

        public async Task<IActionResult> GetProductByName(string name)
        {
            ProductWithBrandAndTypeSpecification specification = new(p => p.Name.Contains(name));
            var product = await productsRepository.GetOneWithSpecAsync(specification);
            var productDto = mapper.Map<Product,ProductDto>(product);

            return Ok(productDto);
        }   

        #endregion
    }
}
