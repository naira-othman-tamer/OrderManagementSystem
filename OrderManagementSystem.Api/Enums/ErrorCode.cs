namespace OrderManagementSystem.Api.Enums
{
    public enum ErrorCode
    {
        #region  General Errors (1xxx)
        None = 0,
        InternalServerError = 1000,
        ValidationError = 1001,
        UnknownError = 1002,
        #endregion

        #region  Authentication & Authorization Errors (2xxx)
        Unauthorized = 2000,
        Forbidden = 2001,
        InvalidCredentials = 2002,
        TokenExpired = 2003,
        InvalidToken = 2004,
        #endregion

        #region Product Errors (3xxx)
        ProductNotFound = 3000,
        ProductAlreadyExists = 3001,
        InsufficientStock = 3002,
        InvalidProductData = 3003,
        #endregion

        #region Order Errors (4xxx)

        OrderNotFound = 4000,
        InvalidOrder = 4001,
        OrderCannotBeCanceled = 4002,
        InvalidOrderStatus = 4003,
        #endregion

        #region  Customer Errors (5xxx)

        CustomerNotFound = 5000,
        CustomerAlreadyExists = 5001,
        InvalidCustomerData = 5002,
        #endregion

        #region Invoice Errors (6xxx)

        InvoiceNotFound = 6000,
        InvoiceGenerationFailed = 6001,
        #endregion

        #region User Errors (7xxx)

        UserNotFound = 7000,
        UserAlreadyExists = 7001,
        InvalidUserData = 7002,
        EmailAlreadyExists = 7003
        #endregion
    }
}
