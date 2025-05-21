using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Models;
using Microsoft.AspNetCore.Components;
using RestSharp;
using System.Net;

namespace IBS_FrontendApi.Services.ApiServices
{
    public class AdminApiServices : IAdminApiServices
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private HttpStatusCodeToCsustomCode _customResponseCode;
        private BaseResponseModel responseModel = new BaseResponseModel();

        public AdminApiServices(IConfiguration configuration,
            HttpStatusCodeToCsustomCode statusCodeToCsustomCode)
        {
            _configuration = configuration;
            _customResponseCode = statusCodeToCsustomCode;
            _restClient = new RestClient(_configuration.GetValue<string>("BackendApiURL"));
        }

        public async Task<BaseResponseModel> RegisterApiAsync(AdminModel model)
        {
            var request = new RestRequest("api/admin/register").AddJsonBody(model);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<BaseResponseModel> LoginApiAsync(AdminModel model)
        {
            var request = new RestRequest("api/admin/login");
            #region Doc
            // Add headers to the request
            //request.AddHeader("HeaderName1", "HeaderValue1");
            #endregion
            request.AddHeader("Email", model.Email);
            request.AddHeader("Password", model.Password);
            var response = await _restClient.ExecutePostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }
    }
}
