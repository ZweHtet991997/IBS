using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace IBS_FrontendApi.Services.ApiServices
{
    public class AccountApiServices : IAccountApiServices
    {
        private RestClient _restClient;
        private readonly IConfiguration _configuration;
        private HttpStatusCodeToCsustomCode _customResponseCode;
        private BaseResponseModel responseModel = new BaseResponseModel();

        public AccountApiServices(IConfiguration configuration,
            HttpStatusCodeToCsustomCode statusCodeToCsustomCode)
        {
            _configuration = configuration;
            _customResponseCode = statusCodeToCsustomCode;
            _restClient = new RestClient(_configuration.GetValue<string>("BackendApiURL"));
        }

        public async Task<BaseResponseModel> CreateAccountApiAsync(AccountModel model)
        {
            var request = new RestRequest("api/account/create").AddJsonBody(model);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<List<AccountResponseModel>> AccountListApiAsync()
        {
            List<AccountResponseModel> lstAccount = new List<AccountResponseModel>();
            var request = new RestRequest("api/account/accountlist", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                lstAccount = JsonConvert.DeserializeObject<List<AccountResponseModel>>(response.Content);
            }
            return lstAccount;
        }

        public async Task<BaseResponseModel> DepositeApiAsync(AccountModel model)
        {
            var request = new RestRequest("api/account/deposite").AddJsonBody(model);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<BaseResponseModel> InactiveAccountApiAsync(InactiveAccountModel model)
        {
            var request = new RestRequest("api/account/inactiveaccount").AddJsonBody(model);
            var response = await _restClient.ExecutePutAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }
    }
}
