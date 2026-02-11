namespace OrderManagementSystem.Core.Entities
{
    //Invoice: InvoiceId, OrderId, InvoiceDate, TotalAmount
    public class Invoice : BaseModel<int>
    {
        //public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
