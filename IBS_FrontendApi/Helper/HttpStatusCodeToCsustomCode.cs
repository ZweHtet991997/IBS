using IBS_FrontendApi.Models;
using Resources.ResponseResources;
using RestSharp;

namespace IBS_FrontendApi.Helper
{
    public class HttpStatusCodeToCsustomCode
    {
        private ValidationHelper _validationHelper;
        private BaseResponseModel responseModel = new BaseResponseModel();

        public HttpStatusCodeToCsustomCode(ValidationHelper validationHelper)
        {
            _validationHelper = validationHelper;
        }

        public BaseResponseModel CustomResponseCode(RestResponse response)
        {
            if (response.IsSuccessStatusCode)
            {
                responseModel.RespCode = ResponseCode.I0000;
                responseModel.RespDescription = ResponseMessage.I0000;
                goto Results;
            }
            var customResponse = _validationHelper.ResponseFromBackendApi(response.StatusCode, response.Content);
            responseModel.RespCode = customResponse.RespCode;
            responseModel.RespDescription = customResponse.RespDescription;

        Results:
            return responseModel;
        }
    }
}
