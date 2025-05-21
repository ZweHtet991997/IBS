using IBS_FrontendApi.Models;

namespace IBS_FrontendApi.Services.ApiServices
{
    public interface IAccountApiServices
    {
        Task<List<AccountResponseModel>> AccountListApiAsync();
        Task<BaseResponseModel> CreateAccountApiAsync(AccountModel model);
        Task<BaseResponseModel> DepositeApiAsync(AccountModel model);
        Task<BaseResponseModel> InactiveAccountApiAsync(InactiveAccountModel model);
    }
}