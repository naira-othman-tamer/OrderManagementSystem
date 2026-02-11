namespace OrderManagementSystem.Api.ViewModels.ResponsiveVms
{
    public class SuccessResponseViewModel<T> : ResponsiveViewModel<T>
    {
        public SuccessResponseViewModel(T data, string message = "Success")
        {
            Data = data;
            IsSuccess = true;
            Message = message;
            ErrorCode = Enums.ErrorCode.None;
        }
    }
}
