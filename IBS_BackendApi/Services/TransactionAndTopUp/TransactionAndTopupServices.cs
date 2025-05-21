using Dapper;
using IBS_BackendApi.Models;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.Entities;
using IBS_BackendApi.Models.ResponseModel;
using IBS_BackendApi.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace IBS_BackendApi.Services.TransactionAndTopUp
{
    public class TransactionAndTopupServices : ITransactionAndTopupServices
    {
        private IDBOperationService _iDbOperationService;
        private EFDBContext _dbContext;

        public TransactionAndTopupServices(IDBOperationService iDbOperationService, EFDBContext dbContext)
        {
            _iDbOperationService = iDbOperationService;
            _dbContext = dbContext;
        }

        public async Task<string> Transaction_TopUp(TransactionHistoryDAO model)
        {
            try
            {
                int dataResult;
                var checkReceiverAccountType = string.Empty;
                string TransactionID = Guid.NewGuid().ToString();
                var checkSenderAccountType = await CheckAccountType(model.FromAccountNo);

                if (!string.IsNullOrEmpty(model.ToAccountNo))
                {
                    checkReceiverAccountType = await CheckAccountType(model.ToAccountNo);
                }

                if (checkSenderAccountType != AccountType.Saving.ToString())
                {
                    if (checkSenderAccountType == checkReceiverAccountType)
                    {
                        var checkAccountBalance = await CheckAccountBalance(model.FromAccountNo);
                        if (checkAccountBalance > model.TransactionAmount)
                        {
                            #region Transfer

                            var checkSender = await GetCustomerNameByAccountNo(model.FromAccountNo);
                            var checkReceiver = await GetCustomerNameByAccountNo(model.ToAccountNo);
                            #region AddParameters
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@AccountID", model.AccountID);
                            parameters.Add("@SenderAccount", model.FromAccountNo);
                            parameters.Add("@ReceiverAccount", model.ToAccountNo);
                            parameters.Add("@TransactionAmount", model.TransactionAmount);
                            parameters.Add("@TransactionType", model.FromAccountNo != null ? "Transfer" : "Topup");
                            parameters.Add("@TransactionID", TransactionID);
                            parameters.Add("@TransactionNote", model.TransactionNote);
                            parameters.Add("@TransactionDate", DateTime.Now);
                            #endregion
                            //Topup Api call if To Account No is null or empty
                            if (model.ToAccountNo != null)
                            {
                                dataResult = await _iDbOperationService.Transfer(parameters);
                            }
                            else
                            {
                                dataResult = await _iDbOperationService.Topup(parameters);
                            }
                            #endregion

                            #region InsertTransactionHistory
                            if (dataResult > 0)
                            {
                                #region DataMapping
                                TransactionHistoryEntities entities = new TransactionHistoryEntities();
                                entities.AccountID = _dbContext.Account.Where(x => x.AccountNo == model.FromAccountNo)
                                    .Select(x => x.ID).FirstOrDefault();
                                entities.TransactionID = TransactionID;
                                entities.FromAccountNo = model.FromAccountNo;
                                entities.ToAccountNo = model.ToAccountNo;
                                entities.TransactionType = string.IsNullOrEmpty(model.ToAccountNo) ? "Top up" : "Transfer";
                                entities.SenderName = checkSender.FullName;
                                entities.ReceiverName = checkReceiver.FullName;
                                entities.Operator = string.IsNullOrEmpty(model.Operator) ? "-" : model.Operator;
                                entities.TransactionAmount = model.TransactionAmount;
                                entities.TransactionNote = model.TransactionNote;
                                entities.TransactionDate = DateTime.Now.ToString();
                                #endregion
                                await _dbContext.TransactionHistory.AddAsync(entities);
                                await _dbContext.SaveChangesAsync();
                            }
                            #endregion

                            return "Success";
                        }
                        else
                        {
                            return "Insufficient Balance to transfer";
                        }
                    }
                    else
                    {
                        return "You cannot transfer from " + checkSenderAccountType + "to" + checkReceiverAccountType;
                    }
                }
                return "Saving Account cannot be transfer";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TransactionHistoryResponseModel>> TransactionHistory()
        {
            try
            {
                return await _iDbOperationService.TransactionHistory<TransactionHistoryResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CheckAccountType(string accountNo)
        {
            try
            {
                return await _dbContext.Account.Where(x => x.AccountNo == accountNo)
                    .Select(x => x.AccountType).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CheckCustomerDAO> GetCustomerNameByAccountNo(string accountNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountNo", accountNo);
                var dataResult = await _iDbOperationService.GetCustomerByAccountNo(parameters);
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> CheckAccountBalance(string accountNo)
        {
            try
            {
                return await _dbContext.Account.
                    Where(x => x.AccountNo == accountNo).
                    Select(x => x.Balance).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
