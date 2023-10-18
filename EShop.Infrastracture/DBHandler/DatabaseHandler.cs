using Dapper;  
using Microsoft.Extensions.Configuration;  
using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.Common;  
using System.Data.SqlClient;  
using System.Linq;  
using System.Threading.Tasks; 
using EShop.Core.Interfaces;

namespace EShop.Infrastracture.Persistence.DBHandler
{
    public class DatabaseHandler : IDapper  
    {  
        private readonly IConfiguration _config;  
        private string Connectionstring = "DefaultConnection";  
  
        public DatabaseHandler(IConfiguration config)  
        {  
            _config = config;  
        }  
        public void Dispose()  
        {  
             
        }  
  
        public int Execute<T>(string sp, List<T> data, CommandType commandType = CommandType.StoredProcedure)  
        {   
             int result = 0;  
            
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            try  
            {  
                if (db.State == ConnectionState.Closed)  
                    db.Open();  
  
                using var tran = db.BeginTransaction();  
                try  
                {  
                    result = db.Execute(sp,data,transaction: tran);
                    tran.Commit();  
                }  
                catch (Exception ex)  
                {  
                    tran.Rollback();  
                    throw ex;  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  
            finally  
            {  
                if (db.State == ConnectionState.Open)  
                    db.Close();  
            }  
  
            return result;  
        }  
  
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)  
        {  
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();  
        }  
  
        public List<T> GetAll<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure)  
        {  
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            return db.Query<T>(sp, data, commandType: commandType).ToList();  
        }  

        public List<T> GetAll1<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure)  
        {  
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            return db.Query<T>(sp, data, commandType: commandType).ToList();  
        }  
  
        public DbConnection GetDbconnection()  
        {  
            return new SqlConnection(_config.GetConnectionString(Connectionstring));  
        }  
  
        public T Insert<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure)  
        {  
            T result;  
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            try  
            {  
                if (db.State == ConnectionState.Closed)  
                    db.Open();  
  
                using var tran = db.BeginTransaction();  
                try  
                {  
                    result = db.Query<T>(sp, data, commandType: commandType, transaction: tran).FirstOrDefault();  
                    tran.Commit();  
                }  
                catch (Exception ex)  
                {  
                    tran.Rollback();  
                    throw ex;  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  
            finally  
            {  
                if (db.State == ConnectionState.Open)  
                    db.Close();  
            }  
  
            return result;  
        } 
  
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)  
        {  
            T result;  
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));  
            try  
            {  
                if (db.State == ConnectionState.Closed)  
                    db.Open();  
  
                using var tran = db.BeginTransaction();  
                try  
                {  
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();  
                    tran.Commit();  
                }  
                catch (Exception ex)  
                {  
                    tran.Rollback();  
                    throw ex;  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  
            finally  
            {  
                if (db.State == ConnectionState.Open)  
                    db.Close();  
            }  
  
            return result;  
        }  
    }  
}