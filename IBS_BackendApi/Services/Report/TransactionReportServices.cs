using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.Enum;
using IBS_BackendApi.Models.ResponseModel;
using IBS_BackendApi.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace IBS_BackendApi.Services.Rreport
{
    public class TransactionReportServices
    {
        private EFDBContext _dbContext;

        public TransactionReportServices(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransactionHistoryResponseModel>> TransactionHistory(TransactionType transactionType)
        {
            try
            {
                List<TransactionHistoryResponseModel> lstTransactionHistory = new List<TransactionHistoryResponseModel>();
                var dataResult = await _dbContext.TransactionHistory.OrderByDescending(x => x.ID).ToListAsync();
                foreach (var transactionHistory in dataResult)
                {
                    TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();
                }
                goto Results;

            Results:
                return lstTransactionHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
