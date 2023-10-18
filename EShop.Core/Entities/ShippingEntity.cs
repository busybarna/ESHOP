namespace EShop.Core.Entities
{
    public class ShippingEntity
    {
        public int CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
    }
}
