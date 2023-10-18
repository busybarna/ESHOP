namespace EShop.Infrastructure.DataModel
{
    public class CartItemDataModel
    {
        public int CustomerId {get;set;}
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public const string InsertQuery = "INSERT INTO Cart (CartCreatedDate,CustomerId,ProductId,Quantity) VALUES (GETDATE(),@CustomerId,@ProductId, @Quantity)"; 
        public const string SelectQuery = "SELECT CartId, CustomerId, ProductId, Quantity FROM Cart   WHERE CustomerId = @CustomerId";

    }
}
