using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Repositories;
using TalabatApi.DTOs;
using TalabatApi.Helper;

namespace TalabatApi.Controllers
{  
    
    public class ProductsController : ApiBaseController

    {
        private readonly IGenericRepository<Product> productsRepository;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductType> typesRepository;
        private readonly IGenericRepository<ProductBrand> brandsRepository;

        public ProductsController(IGenericRepository<Product> productsRepository,
                                  IMapper mapper,
                                  IGenericRepository<ProductType> typesRepository,
                                   IGenericRepository<ProductBrand> brandsRepository)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.typesRepository = typesRepository;
            this.brandsRepository = brandsRepository;
        }
        #region Without Specificaation
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
        {

            var products = await productsRepository.GetAllAsync();
            return Ok(products);

        }
        [HttpGet("{id:int}")]

        public async Task<ActionResult<Product>> GetProductById(int id)
        {

            var product = await productsRepository.GetByIdAsync(id);
            return Ok(product);

        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetAllTypesAsync()
        {

            var types = await typesRepository.GetAllAsync();
            return Ok(types);

        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrandsAsync()
        {

            var brands = await brandsRepository.GetAllAsync();
            return Ok(brands);

        }
        #endregion
        #region With Specification 

        [HttpGet("ProductsWithSpecs")]

        public async Task<ActionResult<Pagination<ProductDto>>> GetAllWithSpecAsync([FromQuery]ProductsSpecsParams specsParams )
        {
            ProductWithBrandAndTypeSpecification specification = new(specsParams);
            var products = await productsRepository.GetAllWithSpecAsync(specification);
            var prdouctsDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            var count = await productsRepository.GetCountSpecAsync(new ProductWithFilterationForCount(specsParams));

            return Ok(new Pagination<ProductDto>(specsParams.PageSize, specsParams.PageIndex, prdouctsDto , count));

        }   


        [HttpGet("ProductsWithSpecs/{name:alpha}")]

        public async Task<ActionResult<ProductDto>> GetProductByName(string name)
        {
            ProductWithBrandAndTypeSpecification specification = new(p => p.Name.Contains(name));
            var product = await productsRepository.GetOneWithSpecAsync(specification);
            var productDto = mapper.Map<Product,ProductDto>(product);

            return Ok(productDto);
        }   

        #endregion
    }
}
