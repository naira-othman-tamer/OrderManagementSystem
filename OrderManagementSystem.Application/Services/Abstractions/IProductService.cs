
namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface IProductService
    {
        Task<GetAllProductsResponseDto?> GetAllProductsAsync();
        Task<GetProductByIdResponseDto?> GetProductByIdAsync(int productId);
        Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto request);
        Task<UpdateProductResponseDto?> UpdateProductAsync(UpdateProductRequestDto request);
        Task<bool> DeleteProductAsync(int productId);
    }
}
