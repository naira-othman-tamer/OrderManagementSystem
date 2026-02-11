
namespace OrderManagementSystem.Core.Entities.Enums
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "CreditCard")]
        CreditCard,

        [EnumMember(Value = "PayPal")]
        PayPal,

        [EnumMember(Value = "BankTransfer")]
        BankTransfer,

        [EnumMember(Value = "Cash")]
        Cash
    }
}
