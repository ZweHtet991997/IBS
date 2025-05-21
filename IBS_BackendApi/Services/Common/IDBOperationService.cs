using Dapper;
using IBS_BackendApi.Models.DAO;

namespace IBS_BackendApi.Services.Common
{
    public interface IDBOperationService
    {
        Task<T> Select<T>(string sql, DynamicParameters parameters);
        Task<List<T>> SelectList<T>(string sql, DynamicParameters parameters);
        Task<int> Topup(DynamicParameters parameters);
        Task<List<T>> TransactionHistory<T>();
        Task<CheckCustomerDAO> GetCustomerByAccountNo(DynamicParameters parameters);
        Task<int> Transfer(DynamicParameters parameters);
    }
}