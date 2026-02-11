
namespace OrderManagementSystem.Application.Services.DTOs.ProductDTOs
{
    public class ProductDtosProfile : Profile
    {
        public ProductDtosProfile()
        {
            #region Get Product DTOs
            CreateMap<Product, ProductDto>();
            CreateMap<Product, GetProductByIdResponseDto>();

            CreateMap<List<ProductDto>, GetAllProductsResponseDto>()
           .ForMember(dest => dest.Products,
              opt =>
               opt.MapFrom(src => src));
            #endregion

            #region Create Product Dtos

            CreateMap<CreateProductRequestDto, Product>();
            CreateMap<Product, CreateProductResponseDto>();
            #endregion

            #region Update Dtos

            CreateMap<UpdateProductRequestDto, Product>()
              .ForAllMembers(opts =>
               opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, UpdateProductResponseDto>();
            #endregion
        }

    }
}
