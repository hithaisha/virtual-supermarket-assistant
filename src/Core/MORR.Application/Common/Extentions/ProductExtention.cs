using MORR.Application.DTOs.ProductDTOs;
using MORR.Domain.Entities;

namespace MORR.Application.Common.Extentions
{
    public static class ProductExtention
    {
        public static Product ToEntity(this ProductDto productDto, Product? product = null)
        {
            if (product is null) product = new Product();

            product.ItemName = productDto.ItemName;
            product.ItemCode = productDto.ItemCode;
            product.SKUCode = productDto.SKUCode;
            product.CategoryId = productDto.CategoryId;
            product.DisplayInFrontPage = productDto.DisplayInFrontPage;
            product.Price = productDto.Price;
            product.IsShowWeb = productDto.IsShowWeb;
            product.ShortDescription = productDto.ShortDescription;
            product.LongDescription = productDto.LongDescription;
            product.ItemImageUrl = productDto.ItemImageUrl;
            product.IsActive = true;
            product.LoyalityPoints = productDto.LoyalityPoints;
            product.Longitude = productDto.Longitude;
            product.Latitude = productDto.Latitude;
            product.Quantity = productDto.Quantity;

            return product;
        }

        public static ProductDto ToDto(this Product product, ProductDto? productDto = null)
        {
            if(productDto is null) productDto = new ProductDto();

            product.Id = productDto.Id;
            productDto.ItemName = product.ItemName;
            productDto.ItemCode = product.ItemCode;
            productDto.SKUCode = product.SKUCode;
            productDto.CategoryId = product.CategoryId;
            productDto.CatedByName = product.Category.Name;
            productDto.DisplayInFrontPage = product.DisplayInFrontPage;
            productDto.Price = product.Price;
            productDto.IsShowWeb = product.IsShowWeb;
            productDto.ShortDescription = product.ShortDescription;
            productDto.LongDescription = product.LongDescription;
            productDto.ItemImageUrl = product.ItemImageUrl;
            productDto.LoyalityPoints = product.LoyalityPoints;
            productDto.Longitude = product.Longitude;
            productDto.Latitude = product.Latitude;
            productDto.Quantity = product.Quantity;
            productDto.CatedByName = $"{product.CreatedByUser.FirstName} {product.CreatedByUser.LastName}";
            productDto.CreatedDate = product.CreatedDate.ToString("MM/dd/yyyy");
            productDto.CatedByName = $"{product.UpdatedByUser.FirstName} {product.UpdatedByUser.LastName}";
            productDto.UpdatedDate = product.UpdateDate?.ToString("MM/dd/yyyy");

            return productDto;
        }
    }
}
