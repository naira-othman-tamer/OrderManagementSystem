namespace OrderManagementSystem.Api.ViewModels.CustomerVMs.RequestVMS
{
    public class CreateCustomerRequestViewModel
    {
        // public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid? UserId { get; set; }

    }
}
