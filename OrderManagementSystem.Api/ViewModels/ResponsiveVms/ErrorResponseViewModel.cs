
namespace OrderManagementSystem.Api.ViewModels.ResponsiveVms
{
    public class ErrorResponseViewModel<T> : ResponsiveViewModel<T>
    {
        public ErrorResponseViewModel(string message, ErrorCode errorCode)
        {
            Data = default(T);
            IsSuccess = false;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
