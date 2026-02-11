namespace OrderManagementSystem.Api.Middlewares
{
    public class GlobalErrorHandlerMiddelware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;

                var (errorCode, statusCode) = GetErrorCodeFromException(ex);

                ErrorResponseViewModel<Exception> response =
                     new ErrorResponseViewModel<Exception>(errorMessage, errorCode);
                // new ErrorResponseViewModel<Exception>(ex.Message, Enums.ErrorCode.InternalServerError);

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }


        private (ErrorCode errorCode, int statusCode) GetErrorCodeFromException(Exception ex)
        {
            return ex switch
            {
                UnauthorizedAccessException => (ErrorCode.Unauthorized, 401),
                KeyNotFoundException => (ErrorCode.OrderNotFound, 404),
                ArgumentException => (ErrorCode.ValidationError, 400),
                InvalidOperationException => (ErrorCode.InvalidOrder, 400),
                _ => (ErrorCode.InternalServerError, 500)
            };
        }
    }
}
