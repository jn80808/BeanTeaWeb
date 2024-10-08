using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public IMapper _mapper;
        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {

            var CustomerBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

            var UpdatedBasket = await _basketRepository.UpdateBasketAsyn(CustomerBasket);

            return Ok(UpdatedBasket);

        }

        [HttpDelete]

        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsyn(id);
        }

    }
}

