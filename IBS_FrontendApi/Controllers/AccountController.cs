using IBS_FrontendApi.Models;
using IBS_FrontendApi.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IBS_FrontendApi.Controllers
{
    public class AccountController : Controller
    {
        private IAccountApiServices _iAccountApiService;

        public AccountController(IAccountApiServices iAccountApiService)
        {
            _iAccountApiService = iAccountApiService;
        }

        [Route("api/account/create")]
        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] AccountModel model)
        {
            var response = await _iAccountApiService.CreateAccountApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/account/deposite")]
        [HttpPost]
        public async Task<IActionResult> Deposite([FromBody] AccountModel model)
        {
            var response = await _iAccountApiService.DepositeApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/account/accountlist")]
        [HttpGet]
        public async Task<IActionResult> AccountList()
        {
            var response = await _iAccountApiService.AccountListApiAsync();
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/account/inactiveaccount")]
        [HttpPut]
        public async Task<IActionResult> InActiveAccount([FromBody] InactiveAccountModel model)
        {
            var response = await _iAccountApiService.InactiveAccountApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
