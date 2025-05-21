using IBS_BackendApi.Models;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace IBS_BackendApi.Controllers.ApiController
{
    public class AccountApiController : Controller
    {
        private IAccountServices _iAccountService;

        public AccountApiController(IAccountServices iAccountService)
        {
            _iAccountService = iAccountService;
        }

        [Route("api/account/create")]
        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] AccountDAO model)
        {
            var dataResult = await _iAccountService.CreateBankAccount(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status409Conflict,
                ResponseMessage.AccountOpeningFailed());
        }

        [Route("api/account/accountlist")]
        [HttpGet]
        public async Task<IActionResult> AccountList()
        {
            var dataResult = await _iAccountService.AccountList();
            return StatusCode(StatusCodes.Status200OK, dataResult);
        }

        [Route("api/account/accountlistbycustomerId")]
        [HttpPost]
        public async Task<IActionResult> AccountListByCustomerId(string customerID)
        {
            var dataResult = await _iAccountService.AccountListByCustomerID(customerID);
            return StatusCode(StatusCodes.Status200OK, dataResult);
        }

        [Route("api/account/deposite")]
        [HttpPost]
        public async Task<IActionResult> Deposite([FromBody] AccountDAO model)
        {
            var dataResult = await _iAccountService.Deposite(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status406NotAcceptable,
                String.Format(ResponseMessage.DepositeFailed(), model.AccountNo));
        }

        [Route("api/account/inactiveaccount")]
        [HttpPut]
        public async Task<IActionResult> InactiveAccount([FromBody] InactiveAccountModel model)
        {
            var dataResult = await _iAccountService.InActiveAccount(model.AccountNo);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
