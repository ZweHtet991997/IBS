using Dapper;
using IBS_BackendApi.Helper;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.Entities;
using IBS_BackendApi.Models.ResponseModel;
using IBS_BackendApi.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Resources.SqlResources;

namespace IBS_BackendApi.Services.Account
{
    public class AccountServices : IAccountServices
    {
        private EFDBContext _dbContext;
        private IDBOperationService _iDbOperationService;

        public AccountServices(EFDBContext dbContext, IDBOperationService iDbOperationService)
        {
            _dbContext = dbContext;
            _iDbOperationService = iDbOperationService;
        }

        public async Task<int> CreateBankAccount(AccountDAO model)
        {
            try
            {
                AccountEntities entities = new AccountEntities();
                var customerInfo = await _dbContext.Customer.
                    Where(x => x.CIFID == model.CustomerID).FirstOrDefaultAsync();
                if (customerInfo != null)
                {
                    var checkAccount = await CheckAccount(customerInfo.CIFID, customerInfo.NRC);
                    if (checkAccount == null)
                    {
                        #region DataMapping
                        entities.CustomerID = model.CustomerID;
                        entities.AccountNo = AccountNoGenerator.GenerateAccountNo();
                        entities.AccountType = model.AccountType;
                        entities.AccountStatus = "Active";
                        entities.Balance = model.Balance;
                        entities.AccountDescription = model.AccountDescription;
                        entities.CreatedUserID = model.CreatedUserID;
                        entities.CreatedDate = DateTime.Now;
                        #endregion

                        await _dbContext.Account.AddAsync(entities);
                        return await _dbContext.SaveChangesAsync();
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AccountResponseModel>> AccountList()
        {
            try
            {
                var dataResult = await _iDbOperationService.SelectList<AccountResponseModel>(SqlResources.AccountList, null);
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AccountByCustomerID>> AccountListByCustomerID(string CIFID)
        {
            try
            {
                return await _dbContext.Account.Where(x => x.CustomerID == CIFID)
                    .Select(x => new AccountByCustomerID
                    {
                        AccountNo = x.AccountNo
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CheckAccount(string CIFID, string NRC)
        {
            try
            {
                #region AddParameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CIFID", CIFID);
                parameters.Add("@NRC", NRC);
                #endregion
                var dataResult = await _iDbOperationService.Select<string>(SqlResources.CheckAccount, parameters);
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Deposite(AccountDAO model)
        {
            try
            {
                AccountEntities entities = new AccountEntities();
                var customerInfo = await _dbContext.Customer.
                    Where(x => x.CIFID == model.CustomerID).FirstOrDefaultAsync();
                if (customerInfo != null)
                {
                    var checkAccount = await _dbContext.Account.AsNoTracking()
                        .Where(x => x.AccountNo == model.AccountNo).FirstOrDefaultAsync();
                    if (checkAccount != null)
                    {
                        #region DataMapping
                        entities.ID = model.ID;
                        entities.CustomerID = model.CustomerID;
                        entities.AccountNo = model.AccountNo;
                        entities.AccountType = checkAccount.AccountType;
                        entities.Balance = model.Balance + checkAccount.Balance;
                        entities.AccountDescription = checkAccount.AccountDescription;
                        entities.AccountStatus = checkAccount.AccountStatus;
                        entities.CreatedDate = checkAccount.CreatedDate;
                        #endregion
                        #region InsertDepositeHistory
                        DepositeDAO depositeDAO = new DepositeDAO();
                        depositeDAO.CustomerID = model.CustomerID;
                        depositeDAO.AccountID = checkAccount.ID;
                        depositeDAO.AccountNo = model.AccountNo;
                        depositeDAO.DepositeAmount = model.Balance;
                        depositeDAO.DepositeDate = DateTime.Now;
                        InsertDepositeHistory(depositeDAO);
                        #endregion

                        _dbContext.Account.Update(entities);
                        return await _dbContext.SaveChangesAsync();

                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertDepositeHistory(DepositeDAO model)
        {
            try
            {
                DepositeEntities entities = new DepositeEntities();
                entities.CustomerID = model.CustomerID;
                entities.AccountID = model.AccountID;
                entities.AccountNo = model.AccountNo;
                entities.DepositeAmount = model.DepositeAmount;
                entities.DepositeDate = model.DepositeDate;
                entities.CreatedUserID = "1";
                _dbContext.Desposite.Add(entities);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InActiveAccount(string accountNo)
        {
            try
            {
                var dataResult = await _dbContext.Account.AsNoTracking().Where(x => x.AccountNo == accountNo)
                    .FirstOrDefaultAsync();
                if (dataResult != null)
                {
                    #region DataMapping
                    AccountEntities entities = new AccountEntities();
                    entities.ID = dataResult.ID;
                    entities.CustomerID = dataResult.CustomerID;
                    entities.AccountNo = dataResult.AccountNo;
                    entities.AccountType = dataResult.AccountType;
                    entities.Balance = dataResult.Balance;
                    entities.AccountDescription = dataResult.AccountDescription;
                    entities.AccountStatus = "InActive";
                    entities.UpdatedDate = DateTime.Now;
                    entities.InactiveDate = DateTime.Now;

                    #endregion
                    _dbContext.Account.Update(entities);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
