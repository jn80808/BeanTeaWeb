using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Controllers;
using API.Dtos;
using AutoMapper;




namespace API.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]   
    public class ProductsController: ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        public IMapper Mapper { get; }

        public ProductsController(IGenericRepository <Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IMapper mapper )
        {
            Mapper = mapper;
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
        }

        [HttpGet]  
        public async Task <ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            return products.Select(product => new ProductToReturnDto
            {
				id					= product.Id,					
				Name				= product.Name,				
				Description		    = product.Description,		
				Price				= product.Price,				
				Quantity_In_Stock	= product.Quantity_In_Stock,	
				Origin				= product.Origin,				
				Type				= product.Type,				
				Flavor				= product.Flavor,				
				CaffeineContent	    = product.CaffeineContent,		
				ImageUrl			= product.ImageUrl,			
				ManufacturingDate 	= product.ManufacturingDate, 	
				ExpirationDate		= product.ExpirationDate,		
				SteepingTime		= product.SteepingTime,		
				WaterTemperature	= product.WaterTemperature,	
				Color				= product.Color,				
				CaffeineLevel		= product.CaffeineLevel,		
				HealthBenefits		= product.HealthBenefits,		
				RoastLevel			= product.RoastLevel,			
				GrindType			= product.GrindType,			
				BrewingMethod		= product.BrewingMethod,		
				ProductType		    = product.ProductType.Name,		
				ProductBrand		= product.ProductBrand.Name	

            }).ToList();
        }

        [HttpGet("{id}")]  
        public async Task <ActionResult <ProductToReturnDto>>GetProduct( int id)
        {    
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return new ProductToReturnDto{
				id					= product.Id,					
				Name				= product.Name,				
				Description		    = product.Description,		
				Price				= product.Price,				
				Quantity_In_Stock	= product.Quantity_In_Stock,	
				Origin				= product.Origin,				
				Type				= product.Type,				
				Flavor				= product.Flavor,				
				CaffeineContent	    = product.CaffeineContent,		
				ImageUrl			= product.ImageUrl,			
				ManufacturingDate 	= product.ManufacturingDate, 	
				ExpirationDate		= product.ExpirationDate,		
				SteepingTime		= product.SteepingTime,		
				WaterTemperature	= product.WaterTemperature,	
				Color				= product.Color,				
				CaffeineLevel		= product.CaffeineLevel,		
				HealthBenefits		= product.HealthBenefits,		
				RoastLevel			= product.RoastLevel,			
				GrindType			= product.GrindType,			
				BrewingMethod		= product.BrewingMethod,		
				ProductType		    = product.ProductType.Name,		
				ProductBrand		= product.ProductBrand.Name	

            };

        }

        [HttpGet ("brands")]
        
        public async Task <ActionResult <IReadOnlyList<ProductBrand>>>GetProductBrand()
        {
            return Ok(await  _productBrandRepo.ListAllAsync());
        }
        
        [HttpGet ("types")]
        
        public async Task <ActionResult <IReadOnlyList<ProductType>>>GetProductType()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

    }
}