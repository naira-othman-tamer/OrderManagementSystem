namespace OrderManagementSystem.Application.Services.DTOs.ProductDTOs.RequestDTOs
{
    public class UpdateProductRequestDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
