using IBS_BackendApi.Models;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.ResponseModel;
using IBS_BackendApi.Services.TransactionAndTopUp;
using Microsoft.AspNetCore.Mvc;
using Resources.ResponseResources;
using ResponseMessage = IBS_BackendApi.Models.ResponseMessage;

namespace IBS_BackendApi.Controllers.ApiController
{
    public class Transaction_TopUpController : Controller
    {
        private ITransactionAndTopupServices _iTransactionTopupService;

        public Transaction_TopUpController(ITransactionAndTopupServices iTransactionTopupService)
        {
            _iTransactionTopupService = iTransactionTopupService;
        }

        [Route("api/transfer_topup")]
        [HttpPost]
        public async Task<IActionResult> Transaction_Topup([FromBody] TransactionHistoryDAO model)
        {
            var dataResult = await _iTransactionTopupService.Transaction_TopUp(model);
            switch (dataResult)
            {
                case "Success":
                    return StatusCode(StatusCodes.Status200OK, ResponseMessage.TransactionSuccess());
                case "Insufficient Balance to transfer":
                    return StatusCode(StatusCodes.Status402PaymentRequired, ResponseMessage.InsufficientBalance());
                case "Saving Account cannot be transfer":
                    return StatusCode(StatusCodes.Status406NotAcceptable, ResponseMessage.AccountNotAllowed());
                default:
                    return StatusCode(StatusCodes.Status406NotAcceptable, dataResult);
            }
        }

        [Route("api/transactionhistory")]
        [HttpGet]
        public async Task<List<TransactionHistoryResponseModel>> TransactionHistory()
        {
            return await _iTransactionTopupService.TransactionHistory();
        }
    }
}
