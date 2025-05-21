using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace IBS_FrontendApi.Services.ApiServices
{
    public class TransactionApiServices : ITransactionApiServices
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private HttpStatusCodeToCsustomCode _customResponseCode;
        private BaseResponseModel responseModel = new BaseResponseModel();

        public TransactionApiServices(IConfiguration configuration,
            HttpStatusCodeToCsustomCode customResponseCodel)
        {
            _configuration = configuration;
            _customResponseCode = customResponseCodel;
            _restClient = new RestClient(_configuration.GetValue<string>("BackendApiURL"));
        }

        public async Task<BaseResponseModel> TransactionApiAsync(TransactionHistoryModel model)
        {
            var request = new RestRequest("api/transfer_topup").AddJsonBody(model);
            var response = await _restClient.PostAsync(request);
            responseModel = _customResponseCode.CustomResponseCode(response);
            return responseModel;
        }

        public async Task<List<TransactionHistoryModel>> TransactionHistoryApiAsync()
        {
            try
            {
                List<TransactionHistoryModel> transactionHistory = new List<TransactionHistoryModel>();
                var request = new RestRequest("api/transactionhistory", Method.Get);
                var response = await _restClient.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    transactionHistory = JsonConvert.DeserializeObject<List<TransactionHistoryModel>>(response.Content);
                }
                return transactionHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
