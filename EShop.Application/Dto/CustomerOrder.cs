namespace EShop.Application.Dto
{
    public class CustomerOrder
    {
        public string CustomerNumber { get; set; }
        public double Total { get; set; }
        public string OrderNumber { get; set; }
        public List<string> itemlist { get; set; }
        public List<string> Shipping { get; set; }
    }
}