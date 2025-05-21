using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.ResponseModel;

namespace IBS_BackendApi.Services.Account
{
    public interface IAccountServices
    {
        Task<string> CheckAccount(string CIFID, string NRC);
        Task<int> CreateBankAccount(AccountDAO model);
        Task<List<AccountResponseModel>> AccountList();
        Task<int> Deposite(AccountDAO model);
        Task<int> InActiveAccount(string accountNo);
        Task<List<AccountByCustomerID>> AccountListByCustomerID(string CIFID);
    }
}