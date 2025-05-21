using IBS_FrontendApi.Models;

namespace IBS_FrontendApi.Services.ApiServices
{
    public interface ITransactionApiServices
    {
        Task<BaseResponseModel> TransactionApiAsync(TransactionHistoryModel model);
        Task<List<TransactionHistoryModel>> TransactionHistoryApiAsync();
    }
}