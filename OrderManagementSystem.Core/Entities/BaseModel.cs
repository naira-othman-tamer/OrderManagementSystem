namespace OrderManagementSystem.Core.Entities
{
    public abstract class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
