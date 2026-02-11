
namespace OrderManagementSystem.Api.ViewModels.ProductVMs
{
    public class ProductViewModelProfile : Profile
    {
        public ProductViewModelProfile()
        {
            // Request ViewModels → DTOs
            CreateMap<CreateProductRequestViewModel, CreateProductRequestDto>();
            CreateMap<UpdateProductRequestViewModel, UpdateProductRequestDto>()
               .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

            // Response DTOs → ViewModels
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<GetProductByIdResponseDto, ProductViewModel>();
            CreateMap<CreateProductResponseDto, CreateProductResponseViewModel>();
            CreateMap<UpdateProductResponseDto, UpdateProductResponseViewModel>();
            CreateMap<GetAllProductsResponseDto, GetAllProductsResponseViewModel>();
        }
    }
}
