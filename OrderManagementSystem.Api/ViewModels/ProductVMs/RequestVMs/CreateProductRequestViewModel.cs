namespace OrderManagementSystem.Api.ViewModels.ProductVMs.RequestVMs
{
    public class CreateProductRequestViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
