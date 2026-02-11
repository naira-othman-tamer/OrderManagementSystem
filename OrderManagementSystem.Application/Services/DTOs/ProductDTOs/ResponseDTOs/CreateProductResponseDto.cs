namespace OrderManagementSystem.Application.Services.DTOs.ProductDTOs.ResponseDTOs
{
    public class CreateProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
