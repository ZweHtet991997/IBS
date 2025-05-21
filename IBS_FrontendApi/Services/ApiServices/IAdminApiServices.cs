using IBS_FrontendApi.Models;

namespace IBS_FrontendApi.Services.ApiServices
{
    public interface IAdminApiServices
    {
        Task<BaseResponseModel> LoginApiAsync(AdminModel model);
        Task<BaseResponseModel> RegisterApiAsync(AdminModel model);
    }
}