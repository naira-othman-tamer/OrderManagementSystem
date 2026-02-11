
namespace OrderManagementSystem.Api.Controllers
{
    //GET /api/invoices/{invoiceId}- Get details of a specifi c invoice (admin only)
    //o GET /api/invoices - Get all invoices(admin only)

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoicesController(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpGet("{invoiceId}")]
        public async Task<ResponsiveViewModel<GetInvoiceByIdResponseViewModel>> GetInvoiceById(int invoiceId)
        {
            if (invoiceId <= 0)
                return new ErrorResponseViewModel<GetInvoiceByIdResponseViewModel>(
                    "Invalid invoice ID",
                    ErrorCode.ValidationError);

            var result = await _invoiceService.GetInvoiceByIdAsync(invoiceId);

            if (result == null)
                return new ErrorResponseViewModel<GetInvoiceByIdResponseViewModel>(
                    "Invoice not found",
                    ErrorCode.InvoiceNotFound);

            var viewModel = _mapper.Map<GetInvoiceByIdResponseViewModel>(result);

            return new SuccessResponseViewModel<GetInvoiceByIdResponseViewModel>(viewModel, "Invoice retrieved successfully");
        }


        [HttpGet]
        public async Task<ResponsiveViewModel<GetAllInvoicesResponseViewModel>> GetAllInvoices([FromQuery] InvoiceFilterViewModel filter)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<GetAllInvoicesResponseViewModel>(
                    "Invalid filter parameters",
                    ErrorCode.ValidationError);

            var filterDto = _mapper.Map<InvoiceFilterDto>(filter);
            var result = await _invoiceService.GetAllInvoicesAsync(filterDto);
            var viewModel = _mapper.Map<GetAllInvoicesResponseViewModel>(result);

            return new SuccessResponseViewModel<GetAllInvoicesResponseViewModel>(viewModel, "Invoices retrieved successfully");
        }
    }
}
