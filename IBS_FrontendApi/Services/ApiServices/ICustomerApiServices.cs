using IBS_FrontendApi.Models;

namespace IBS_FrontendApi.Services.ApiServices
{
    public interface ICustomerApiServices
    {
        Task<BaseResponseModel> LoginApiAsync(string email, string password);
        Task<BaseResponseModel> RegisterApiAsync(CustomerModel model);
        Task<List<CustomerModel>> GetCustomerListApiAsync();
        Task<BaseResponseModel> UpdateCustomerApiAsync(CustomerModel model);
    }
}