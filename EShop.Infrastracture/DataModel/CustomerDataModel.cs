using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class CustomerDataModel
    {
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public bool IsLoyaltyMembership { get; set; }
        public bool IsActive { get; set; }

        public const string SelectQuery = @"SELECT C.[CustomerId]
		   ,[CustomerNumber]
	       ,[Name] 
		   ,[IsLoyaltyMembership]
           ,[IsActive]
	        FROM  [dbo].[Customer] C
	        ORDER BY C.[CustomerId] ASC";

        public const string SelectCustomerById = @"SELECT C.[CustomerId]
		   ,[CustomerNumber]
	       ,[Name] 
		   ,[IsLoyaltyMembership]
           ,[IsActive]
	        FROM  [dbo].[Customer] C
	        WHERE CustomerId = @CustomerId";

        public const string InsertQuery =  @"INSERT INTO [dbo].[Customer] ([CustomerNumber],[Name],[IsLoyaltyMembership],[IsActive])  VALUES (@CustomerNumber,@Name,@IsLoyaltyMembership,@IsActive)";
    }

}

