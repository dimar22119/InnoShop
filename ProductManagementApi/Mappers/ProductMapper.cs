using ProductManagementApi.Dtos;
using ProductManagementApi.Models;

namespace ProductManagementApi.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Availability = productModel.Availability,
                CreatedOn = productModel.CreatedOn
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Availability = createProductDto.Availability
            };
        }
    }
}
