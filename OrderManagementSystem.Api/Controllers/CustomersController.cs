using System.Security.Claims;

namespace OrderManagementSystem.Api.Controllers
{
    //POST /api/customers - Create a new customer
    //o GET /api/customers/{customerId}/orders - Get all orders for a customer

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsiveViewModel<CreateCustomerResponseViewModel>> CreateCustomer(CreateCustomerRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<CreateCustomerResponseViewModel>(
                                                 "Invalid inputs",
                                                 ErrorCode.InvalidCredentials);

            var dto = _mapper.Map<CreateCustomerRequestDto>(viewModel);
            var result = await _customerService.CreateCustomerAsync(dto);
            var responseViewModel = _mapper.Map<CreateCustomerResponseViewModel>(result);

            return new SuccessResponseViewModel<CreateCustomerResponseViewModel>(
                responseViewModel,
                "Customer created successfully");
        }


        [HttpGet("{customerId}/orders")]
        [Authorize]
        public async Task<ResponsiveViewModel<GetCustomerOrdersResponseViewModel>> GetCustomerOrders(int customerId)
        {
            if (customerId <= 0)
                return new ErrorResponseViewModel<GetCustomerOrdersResponseViewModel>("", ErrorCode.ValidationError);

            //  Get CustomerId from JWT token
            var tokenCustomerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            // Check authorization
            if (!User.IsInRole("Admin"))
            {
                if (string.IsNullOrEmpty(tokenCustomerId) || int.Parse(tokenCustomerId) != customerId)
                    return new ErrorResponseViewModel<GetCustomerOrdersResponseViewModel>(
                        "Unauthorized to access this customer's orders",
                        ErrorCode.Forbidden);
            }


            var result = await _customerService.GetCustomerOrdersAsync(customerId);

            if (result == null)
                return new ErrorResponseViewModel<GetCustomerOrdersResponseViewModel>(
                    "Customer not found",
                    ErrorCode.CustomerNotFound);

            var viewModel = _mapper.Map<GetCustomerOrdersResponseViewModel>(result);
            return new SuccessResponseViewModel<GetCustomerOrdersResponseViewModel>(
                viewModel, "Customer orders retrieved successfully");
        }
    }
}

