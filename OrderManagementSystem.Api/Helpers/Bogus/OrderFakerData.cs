using Bogus;
using OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Responses;

namespace OrderManagementSystem.Api.Helpers.Bogus
{
    public class OrderFakerData
    {
        Faker<OrderCustomerDto> _fakerOrdeCustomerDto;
        Faker<GetOrderByIdResponseDto> _fakerGetOrderDto;
        public OrderFakerData()
        {
            //public int OrderId { get; set; }
            //public OrderCustomerDto Customer { get; set; }
            //[         public int Id { get; set; }
            //  public string Name { get; set; }
            //public string Email { get; set; } ]
            //public DateTime OrderDate { get; set; }
            //public decimal TotalAmount { get; set; }
            //public List<OrderItemDetailsDto> OrderItems { get; set; }
            //public string PaymentMethod { get; set; }
            //public string Status { get; set; }
            _fakerOrdeCustomerDto = new Faker<OrderCustomerDto>()
                    .RuleFor(o => o.Name, f => f.Name.FullName())
                    .RuleFor(o => o.Email, f => f.Internet.UserName())
                    .RuleFor(o => o.Id, f => f.IndexFaker + 1);

            //_fakerGetOrderDto = new Faker<GetOrderByIdResponseDto>()
            //    .RuleFor()
        }

        public IEnumerable<OrderCustomerDto> GetData()
        {
            return _fakerOrdeCustomerDto.Generate(20);
        }
    }
}
