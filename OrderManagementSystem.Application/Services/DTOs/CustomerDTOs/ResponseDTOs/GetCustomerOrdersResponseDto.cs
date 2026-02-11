namespace OrderManagementSystem.Application.Services.DTOs.CustomerDTOs.ResponseDTOs
{
    public class GetCustomerOrdersResponseDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<CustomerOrderDto> Orders { get; set; }
    }
}
