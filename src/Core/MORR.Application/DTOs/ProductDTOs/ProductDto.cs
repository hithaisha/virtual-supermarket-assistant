using MORR.Application.DTOs.Common;

namespace MORR.Application.DTOs.ProductDTOs
{
    public class ProductDto : MetaDataDto
    {
        
        public string ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? SKUCode { get; set; }
        public int CategoryId { get; set; }
        public bool? DisplayInFrontPage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsShowWeb { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? ItemImageUrl { get; set; }
        public decimal? LoyalityPoints { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public string? CategoryName { get; set; }

    }
}
