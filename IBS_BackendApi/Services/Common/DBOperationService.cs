using Dapper;
using IBS_BackendApi.Models.DAO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IBS_BackendApi.Services.Common
{
    public class DBOperationService : GetConnectionService, IDBOperationService
    {
        private IConfiguration _configuration;

        public DBOperationService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<T>> SelectList<T>(string sql, DynamicParameters parameters)
        {
            SqlConnection con = this.OpenConnection();
            var dataResult = await con.QueryAsync<T>(sql, parameters);
            this.CloseConnection();
            return dataResult.ToList();
        }

        public async Task<T> Select<T>(string sql, DynamicParameters parameters)
        {
            SqlConnection con = this.OpenConnection();
            var dataResult = await con.QueryAsync<T>(sql, parameters);
            this.CloseConnection();
            return dataResult.FirstOrDefault();
        }

        public async Task<int> Transfer(DynamicParameters parameters)
        {
            try
            {
                SqlConnection con = this.OpenConnection();
                var dataResult = await con.ExecuteAsync("Transfer", parameters, commandType: CommandType.StoredProcedure);
                this.CloseConnection();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Topup(DynamicParameters parameters)
        {
            SqlConnection con = this.OpenConnection();
            var dataResult = await con.QueryAsync<int>("Topup", parameters, commandType: CommandType.StoredProcedure);
            this.CloseConnection();
            return Convert.ToInt32(dataResult);
        }

        public async Task<List<T>> TransactionHistory<T>()
        {
            SqlConnection con = this.OpenConnection();
            var dataResult = await con.QueryAsync<T>("TransactionHistory", null, commandType: CommandType.StoredProcedure);
            this.CloseConnection();
            return dataResult.ToList();
        }

        public async Task<CheckCustomerDAO> GetCustomerByAccountNo(DynamicParameters parameters)
        {
            SqlConnection con = this.OpenConnection();
            var dataResult = await con.QueryAsync<CheckCustomerDAO>("GetCustomerByAccountNo", parameters, commandType: CommandType.StoredProcedure);
            this.CloseConnection();
            return dataResult.FirstOrDefault();
        }
    }
}
