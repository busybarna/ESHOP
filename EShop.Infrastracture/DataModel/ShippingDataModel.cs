namespace EShop.Infrastructure.DataModel
{
    public class ShippingDataModel
    {
         public int CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
        public const string InsertQuery = "INSERT INTO ShippingDetail(CustomerId, Address1, Address2, IsActive) VALUES (@CustomerId, @Address1, @Address2, @IsActive)";
        public const string SelectQuery = "SELECT  ShippingId, CustomerId, Address1, Address2, IsActive  FROM ShippingDetail WHERE CustomerId = @CustomerId";
    }
}
