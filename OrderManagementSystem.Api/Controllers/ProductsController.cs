


namespace OrderManagementSystem.Api.Controllers
{
    //GET /api/products - Get all products
    //o GET /api/products/{productId} - Get details of a specifi c product
    //o POST /api/products - Add a new product (admin only)
    //o PUT /api/products/{productId} - Update product details (admin only)

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponsiveViewModel<GetAllProductsResponseViewModel>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            GetAllProductsResponseViewModel? ProductsVm = _mapper.Map<GetAllProductsResponseViewModel>(products);
            return new SuccessResponseViewModel<GetAllProductsResponseViewModel>(ProductsVm, "Products retrieved successfully");
        }

        [HttpGet("{productId}")]
        public async Task<ResponsiveViewModel<ProductViewModel>> GetProductDetails(int productId)
        {

            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return new ErrorResponseViewModel<ProductViewModel>("Product NOt Found", ErrorCode.ProductNotFound);

            ProductViewModel? ProductResponseVM = _mapper.Map<ProductViewModel>(product);
            return new SuccessResponseViewModel<ProductViewModel>(ProductResponseVM, "Product retrieved successfully");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsiveViewModel<CreateProductResponseViewModel>> AddNewProduct(CreateProductRequestViewModel product)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<CreateProductResponseViewModel>("Invalid input", ErrorCode.ValidationError);

            var productDto = _mapper.Map<CreateProductRequestDto>(product);

            var CreatedProductDto = await _productService.CreateProductAsync(productDto);
            var CreatedProductVM = _mapper.Map<CreateProductResponseViewModel>(CreatedProductDto);

            return new SuccessResponseViewModel<CreateProductResponseViewModel>(CreatedProductVM, "Product created successfully");
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsiveViewModel<UpdateProductResponseViewModel>> UpdateProductDetails(int productId
                                                           , [FromBody] UpdateProductRequestViewModel ProductViewModel)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<UpdateProductResponseViewModel>("Invalid input", ErrorCode.ValidationError);


            var requestDto = _mapper.Map<UpdateProductRequestDto>(ProductViewModel);
            requestDto.ProductId = productId;

            var response = await _productService.UpdateProductAsync(requestDto);

            if (response == null)
                return new ErrorResponseViewModel<UpdateProductResponseViewModel>("Product not found", ErrorCode.ProductNotFound);


            var UpdateProductRespVM = _mapper.Map<UpdateProductResponseViewModel>(response);

            return new SuccessResponseViewModel<UpdateProductResponseViewModel>(UpdateProductRespVM, "Product updated successfully");

        }
    }
}
