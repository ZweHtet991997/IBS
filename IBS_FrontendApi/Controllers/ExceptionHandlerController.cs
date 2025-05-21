using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace IBS_FrontendApi.Controllers
{
    public class ExceptionHandlerController : Controller
    {
        private ValidationHelper _validationHelper;

        public ExceptionHandlerController(ValidationHelper validationHelper)
        {
            _validationHelper = validationHelper;
        }

        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult GetError()
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            switch (exception.Message.ToString())
            {
                case HttpResponseMessages.PaymentRequired:
                    responseModel = _validationHelper.ResponseFromBackendApi(HttpStatusCode.PaymentRequired, null);
                    goto Results;
                default:
                    responseModel = _validationHelper.ResponseFromBackendApi(HttpStatusCode.InternalServerError, null);
                    break;
            }
        Results:
            return Content(JsonConvert.SerializeObject(responseModel), "application/json");
        }

    }
}
