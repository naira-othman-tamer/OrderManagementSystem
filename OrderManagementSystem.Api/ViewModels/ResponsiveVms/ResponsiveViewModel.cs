
namespace OrderManagementSystem.Api.ViewModels.ResponsiveVms
{
    public class ResponsiveViewModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}
