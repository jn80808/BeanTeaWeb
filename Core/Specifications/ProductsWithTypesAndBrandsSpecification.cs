using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParam productParams )
            :base 
                ( 
                    x => 
                        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search.ToLower())) &&
                        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)    
                )

        {
            addInclude(x => x.ProductType);
            addInclude(x => x.ProductBrand);
            AddOrderBy (x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex-1), 
                        productParams.PageSize);


            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                            AddOrderBy(p => p.Price);
                            break;
                    case "priceDesc":
                            AddOrderByDescending(p => p.Price);
                            break;
                    default:
                            AddOrderBy (n => n.Name);
                            break;
                            
                }
            }

        }

        public ProductsWithTypesAndBrandsSpecification(int id) 
        : base(x => x.Id == id)
        {
            addInclude(x => x.ProductType);
            addInclude(x => x.ProductBrand);

        }
    }
}