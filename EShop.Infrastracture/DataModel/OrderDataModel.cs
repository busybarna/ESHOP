using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class OrderDataModel
    {
        public int CustomerId {get;set;}
        public string CustomerNumber {get;set;}
        public string OrderNumber {get;set;}
        public string ProductName {get;set;}
        public int Quantity {get;set;}
        public double NetAmount {get;set;}
        public bool IsLoyaltyMembership {get;set;}
        public string address1 {get;set;}
        public string address2 {get;set;}

        public const string SelectQuery = @"SELECT C.CustomerId,C.IsLoyaltyMembership,C.CustomerNumber,R.OrderNumber, P.Name AS ProductName,
                            OD.Quantity,OD.NetAmount,SD.Address1,SD.Address2
                            FROM  [dbo].Customer C
                            LEFT JOIN ShippingDetail SD ON C.CustomerId = SD.CustomerId
                            INNER JOIN [Order] R ON R.CustomerId = C.CustomerId
                            INNER JOIN OrderDetail OD ON OD.OrderId = R.OrderId
                            INNER JOIN Product P ON P.ProductId = OD.ProductId
                            WHERE C.CustomerId = @CustomerId";
        public const string InsertQuery = @"DECLARE @OrderId INT;
        INSERT INTO [Order] (CustomerId,OrderNumber,TotalAmount,TransactionDate,IsActive) 
        SELECT @CustomerId,@OrderNumber,sum(C.Quantity*P.Price) AS Total, GETDATE(),1 FROM Cart C 
        INNER JOIN Product P ON P.ProductId = C.ProductId 
        WHERE C.CustomerId = @CustomerId GROUP BY CustomerId;
        SET @OrderId = SCOPE_IDENTITY();
        INSERT INTO OrderDetail (OrderId,ProductId,Quantity,DiscountAmount,NetAmount,TransactionDate,IsActive)  
        SELECT @OrderId,P.ProductId,C.Quantity,CASE WHEN (ISNULL(IsLoyaltyMembership, 0) = 1) THEN ((C.Quantity*P.Price)*20/100) 
					  ELSE NULL END AS DiscountAmount,(C.Quantity*P.Price) AS Total,GETDATE(),1 
        FROM Cart C  
        INNER JOIN Customer CU ON C.CustomerId = CU.CustomerId  
        INNER JOIN Product P ON C.ProductId  = P.ProductId 
        WHERE C.CustomerId = @CustomerId; 
        DELETE FROM Cart WHERE Cart.CustomerId = @CustomerId;";
    }
}