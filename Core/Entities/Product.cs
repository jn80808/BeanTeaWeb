
namespace Core.Entities
{
    public class Product : BaseEntity
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity_In_Stock { get; set; }

        public string Origin { get; set; }

        public string Type { get; set; }

        public string Flavor { get; set; }

        public string CaffeineContent { get; set; }

        //public string Organic { get; set; }

        public string ImageUrl { get; set; }

        public string ManufacturingDate { get; set; }

        public string ExpirationDate { get; set; }

        public string SteepingTime { get; set; }

        public string WaterTemperature { get; set; }

        public string Color { get; set; }

        public string CaffeineLevel { get; set; }

        public string HealthBenefits { get; set; }

        public string RoastLevel { get; set; }

        public string GrindType { get; set; }

        public string BrewingMethod { get; set; }

        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int ProductBrandId { get; set; }
    }
}
