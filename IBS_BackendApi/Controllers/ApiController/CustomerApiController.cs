using IBS_BackendApi.Helper;
using IBS_BackendApi.Models;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Services.Admin;
using IBS_BackendApi.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace IBS_BackendApi.Controllers.ApiController
{
    public class CustomerApiController : Controller
    {
        #region DeclarationAndConstructor
        private ICustomerServices _iCustomerService;

        public CustomerApiController(ICustomerServices iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }
        #endregion

        [Route("api/customer/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CustomerDAO model)
        {
            int customerCount = _iCustomerService.CustomerCount();
            model.CIFID = CustomerIDGenerator.GenerateCIFID(customerCount);

            var dataResult = await _iCustomerService.Register(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status409Conflict,
                string.Format(ResponseMessage.CustomerRegisterFailed(), model.NRC));
        }

        [Route("api/customer/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromHeader] string phoneNo, [FromHeader] string Password)
        {
            var dataResult = await _iCustomerService.Login(phoneNo, Password);
            return dataResult == true ? StatusCode(StatusCodes.Status200OK) :
                StatusCode(StatusCodes.Status401Unauthorized, ResponseMessage.LoginFailed());
        }

        [Route("api/customer/getcustomerlist")]
        [HttpGet]
        public async Task<IActionResult> CustomerList()
        {
            var dataResult = await _iCustomerService.CustomerList();
            return StatusCode(StatusCodes.Status200OK, dataResult);
        }

        [Route("api/customer/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDAO model)
        {
            var dataResult = await _iCustomerService.UpdateCustomer(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK) :
                StatusCode(StatusCodes.Status401Unauthorized, ResponseMessage.UpdateFailed());
        }
    }
}
