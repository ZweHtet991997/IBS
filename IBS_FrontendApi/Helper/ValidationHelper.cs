using IBS_FrontendApi.Models;
using Resources.ResponseResources;
using System.Net;

namespace IBS_FrontendApi.Helper
{
    public class ValidationHelper
    {
        public BaseResponseModel ResponseMethod(ResponseType respType, string message)
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            switch (respType)
            {
                case ResponseType.Success:
                    responseModel.RespCode = ResponseCode.I0000;
                    responseModel.RespDescription = ResponseMessage.I0000;
                    goto Results;

                case ResponseType.Error:
                    responseModel.RespCode = ResponseCode.E0000;
                    responseModel.RespDescription = ResponseMessage.E0000;
                    goto Results;

                case ResponseType.LoginFailed:
                    responseModel.RespCode = ResponseCode.E0001;
                    responseModel.RespDescription = ResponseMessage.E0001;
                    goto Results;

                default:
                    break;
            }
        Results:
            return responseModel;
        }

        //Get Response from Backend api and convert into custom response code format
        public BaseResponseModel ResponseFromBackendApi(HttpStatusCode statusCode, string message)
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    responseModel.RespCode = ResponseCode.I0000;
                    responseModel.RespDescription = message;
                    goto Results;

                case HttpStatusCode.NotFound:
                    responseModel.RespCode = ResponseCode.W0001;
                    responseModel.RespDescription = message;
                    goto Results;

                case HttpStatusCode.Unauthorized:
                    responseModel.RespCode = ResponseCode.E0001;
                    responseModel.RespDescription = message;
                    goto Results;

                case HttpStatusCode.Conflict:
                    responseModel.RespCode = ResponseCode.W0003;
                    responseModel.RespDescription = message;
                    goto Results;

                case HttpStatusCode.PaymentRequired:
                    responseModel.RespCode = ResponseCode.E0006;
                    responseModel.RespDescription = ResponseMessage.E0006;
                    goto Results;

                case HttpStatusCode.NotModified:
                    responseModel.RespCode = ResponseCode.E0004;
                    responseModel.RespDescription = message;
                    goto Results;

                case HttpStatusCode.InternalServerError:
                    responseModel.RespCode = ResponseCode.E0005;
                    responseModel.RespDescription = message;
                    goto Results;

                default:
                    break;
            }
        Results:
            return responseModel;
        }
    }
}
