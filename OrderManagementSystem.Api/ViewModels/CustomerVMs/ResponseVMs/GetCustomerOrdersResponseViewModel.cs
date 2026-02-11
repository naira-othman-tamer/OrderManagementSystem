namespace OrderManagementSystem.Api.ViewModels.CustomerVMs.ResponseVMs
{
    public class GetCustomerOrdersResponseViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<CustomerOrderViewModel> Orders { get; set; }
    }
}
