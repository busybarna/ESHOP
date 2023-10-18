namespace EShop.Application.Dto
{ 
    public class Order
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
    }
}