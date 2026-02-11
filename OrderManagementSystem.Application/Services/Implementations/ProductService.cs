
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAllProductsResponseDto?> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.Get(P => true)
                                              .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                              .ToListAsync();

            if (products == null || products.Count == 0) return null;

            return _mapper.Map<GetAllProductsResponseDto>(products);
        }

        public async Task<GetProductByIdResponseDto?> GetProductByIdAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository
                                              .Get(p => p.Id == productId)
                                   .ProjectTo<GetProductByIdResponseDto>(_mapper.ConfigurationProvider)
                                            .FirstOrDefaultAsync();

            if (product == null) return null;

            return product;
        }

        public async Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto request)
        {
            Product? product = _mapper.Map<Product>(request);

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<CreateProductResponseDto>(product);
            return response;
        }

        public async Task<UpdateProductResponseDto?> UpdateProductAsync(UpdateProductRequestDto request)
        {

            var product = await _unitOfWork.ProductRepository.GetByIdWithTrackingAsync(request.ProductId);

            if (product == null)
                return null;

            _mapper.Map(request, product);

            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<UpdateProductResponseDto>(product);
            return response;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
                return false;

            await _unitOfWork.ProductRepository.DeleteAsync(productId);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
