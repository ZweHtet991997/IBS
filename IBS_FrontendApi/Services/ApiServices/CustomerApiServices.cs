using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;

namespace IBS_FrontendApi.Services.ApiServices
{
    public class CustomerApiServices : ICustomerApiServices
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private HttpStatusCodeToCsustomCode _customResponseCode;
        private BaseResponseModel responseModel = new BaseResponseModel();

        public CustomerApiServices(IConfiguration configuration,
            HttpStatusCodeToCsustomCode customResponseCodel)
        {
            _configuration = configuration;
            _customResponseCode = customResponseCodel;
            _restClient = new RestClient(_configuration.GetValue<string>("BackendApiURL"));
        }

        public async Task<BaseResponseModel> RegisterApiAsync(CustomerModel model)
        {
            var request = new RestRequest("api/customer/register").AddJsonBody(model);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<BaseResponseModel> LoginApiAsync(string phoneNo, string password)
        {
            var request = new RestRequest("api/customer/login");
            #region Doc
            // Add headers to the request
            //request.AddHeader("HeaderName1", "HeaderValue1");
            #endregion
            request.AddHeader("Email", phoneNo);
            request.AddHeader("Password", password);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<List<CustomerModel>> GetCustomerListApiAsync()
        {
            List<CustomerModel> lstCustomer = new List<CustomerModel>();
            var request = new RestRequest("api/customer/getcustomerlist", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                lstCustomer = JsonConvert.DeserializeObject<List<CustomerModel>>(response.Content);
            }
            return lstCustomer;
        }

        public async Task<BaseResponseModel> UpdateCustomerApiAsync(CustomerModel model)
        {
            var request = new RestRequest("api/customer/update").AddJsonBody(model);
            var response = await _restClient.ExecutePutAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }
    }
}
