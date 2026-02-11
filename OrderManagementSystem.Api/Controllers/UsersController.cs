
namespace OrderManagementSystem.Api.Controllers
{
    //POST /api/users/register - Register a new user
    //o POST /api/users/login - Authenticate a user and return a JWT token

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UsersController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ResponsiveViewModel<RegisterResponseViewModel>> Register([FromBody] RegisterRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<RegisterResponseViewModel>("Invalid input", ErrorCode.ValidationError);

            var dto = _mapper.Map<RegisterRequestDto>(viewModel);
            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return new ErrorResponseViewModel<RegisterResponseViewModel>("Username or Email already exists",
                                                                               ErrorCode.EmailAlreadyExists);

            var response = _mapper.Map<RegisterResponseViewModel>(result);
            return new SuccessResponseViewModel<RegisterResponseViewModel>(response, "User registered successfully");
        }

        [HttpPost("login")]
        public async Task<ResponsiveViewModel<LoginResponseViewModel>> Login([FromBody] LoginRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<LoginResponseViewModel>("Invalid input", ErrorCode.ValidationError);

            var dto = _mapper.Map<LoginRequestDto>(viewModel);
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return new ErrorResponseViewModel<LoginResponseViewModel>("Invalid username or password",
                                                                           ErrorCode.InvalidCredentials);

            var response = _mapper.Map<LoginResponseViewModel>(result);
            return new SuccessResponseViewModel<LoginResponseViewModel>(response, "Login successful");
        }
    }
}


