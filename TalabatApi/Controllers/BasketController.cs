using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using TalabatApi.DTOs;

namespace TalabatApi.Controllers
{

    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository repository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<CustomerBasket>> GetById(string Id) {
          var basket =  await repository.GetBasketAsync(Id);

            return basket is null ?   new CustomerBasket(Id) :basket;
            
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto) { 
             
            var basket = mapper.Map<CustomerBasketDto,CustomerBasket>(basketDto);
            var createdOrUpdated =  await repository.UpdateBasketAsync(basket);
            if (createdOrUpdated is null) BadRequest();
            return Ok(createdOrUpdated);
    
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Id) { 
                
            return  await repository.DeleteBasketAsync(Id);
            
    
        }
    }
}
