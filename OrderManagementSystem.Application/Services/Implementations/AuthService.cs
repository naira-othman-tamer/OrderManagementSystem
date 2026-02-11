
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, ICustomerService customerService,
                           IOptions<JwtSettings> jwtSettings, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }


        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Check if username exists
            bool userExists = await _unitOfWork.UserRepository
                .Get(u => u.Username == request.Username)
                .AnyAsync();

            if (userExists)
                return null;

            // Create User
            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            int? customerId = null;
            string email = request.Email;

            // If Customer → Create Customer
            if (request.Role == Role.Customer)
            {
                var customerDto = _mapper.Map<CreateCustomerRequestDto>(request);
                customerDto.UserId = user.Id;

                var customerResponse = await _customerService.CreateCustomerAsync(customerDto);
                customerId = customerResponse.CustomerId;
            }

            // Generate Token
            string token = JwtTokenGenerator.GenerateToken(user.Id, user.Role, user.Username, _jwtSettings, customerId);

            return new RegisterResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = email,
                Role = user.Role,
                Token = token,
                CustomerId = customerId
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _unitOfWork.UserRepository
                        .Get(u => u.Username == loginDto.UserName)
                        .FirstOrDefaultAsync();

            if (user == null)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
                return null;

            // Get Customer data if exists
            int? customerId = null;
            string email = null;

            if (user.Role == Role.Customer)
            {
                var customer = await _unitOfWork.CustomerRepository
                    .Get(c => c.UserId == user.Id)
                    .FirstOrDefaultAsync();

                if (customer != null)
                {
                    customerId = customer.Id;
                    email = customer.Email;
                }
            }

            var token = JwtTokenGenerator.GenerateToken(user.Id, user.Role, user.Username, _jwtSettings, customerId);

            return new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Username,
                Role = user.Role,
                Token = token,
                CustomerId = customerId
            };
        }
    }
}
