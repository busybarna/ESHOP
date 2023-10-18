using Dapper;    
using System;    
using System.Collections.Generic;    
using System.Data;    
using System.Data.Common;    
using System.Linq;    
using System.Threading.Tasks;  
    
namespace EShop.Core.Interfaces   
{    
    public interface IDapper : IDisposable    
    {    
        DbConnection GetDbconnection();    
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);    
        List<T> GetAll<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure);    
        int Execute<T>(string sp,  List<T> data, CommandType commandType = CommandType.StoredProcedure);    
        T Insert<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure);    
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }    
} 