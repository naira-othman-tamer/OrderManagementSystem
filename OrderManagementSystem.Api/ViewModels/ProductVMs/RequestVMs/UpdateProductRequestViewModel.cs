namespace OrderManagementSystem.Api.ViewModels.ProductVMs.RequestVMs
{
    public class UpdateProductRequestViewModel
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
