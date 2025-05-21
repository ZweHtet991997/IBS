using IBS_FrontendApi.Models;
using IBS_FrontendApi.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IBS_FrontendApi.Controllers
{
    public class Transaction_TopUpController : Controller
    {
        private ITransactionApiServices _iTransactionService;

        public Transaction_TopUpController(ITransactionApiServices iTransactionService)
        {
            _iTransactionService = iTransactionService;
        }

        [Route("api/transfer_topup")]
        [HttpPost]
        public async Task<IActionResult> Transaction_TopUp([FromBody] TransactionHistoryModel model)
        {
            var response = await _iTransactionService.TransactionApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/transactionhistory")]
        [HttpGet]
        public async Task<IActionResult> TransactionHistory()
        {
            var response = await _iTransactionService.TransactionHistoryApiAsync();
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
