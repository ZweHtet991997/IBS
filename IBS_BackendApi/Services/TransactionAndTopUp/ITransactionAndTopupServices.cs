using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.ResponseModel;

namespace IBS_BackendApi.Services.TransactionAndTopUp
{
    public interface ITransactionAndTopupServices
    {
        Task<decimal> CheckAccountBalance(string accountNo);
        Task<string> CheckAccountType(string accountNo);
        Task<CheckCustomerDAO> GetCustomerNameByAccountNo(string accountNo);
        Task<string> Transaction_TopUp(TransactionHistoryDAO model);
        Task<List<TransactionHistoryResponseModel>> TransactionHistory();
    }
}