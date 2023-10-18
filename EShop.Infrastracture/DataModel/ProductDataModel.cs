namespace EShop.Infrastructure.DataModel
{
    public class ProductDataModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public const string SelectQuery = "SELECT [ProductId],[Name],[Price] FROM  [dbo].[Product] ORDER BY [Name] ASC";
        public const string InsertQuery = "INSERT INTO [dbo].[Product] ([Name],[Price],[IsActive])  VALUES (@Name,@Price,@IsActive)";

    }
}
