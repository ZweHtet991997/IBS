using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.Entities;
using IBS_BackendApi.Models.ResponseModel;
using IBS_BackendApi.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace IBS_BackendApi.Services.Customer
{
    public class CustomerServices : ICustomerServices
    {
        #region DeclarationAndConstructor
        private EFDBContext _dbContext;

        public CustomerServices(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region CustomerRegister
        public async Task<int> Register(CustomerDAO model)
        {
            try
            {
                CustomerEntities entities = new CustomerEntities();
                #region CheckCustomer
                var checkCustomer = await CheckCustomer(model.NRC);
                if (checkCustomer)
                {
                    #region DataMapping
                    entities.CIFID = model.CIFID;
                    entities.FullName = model.FullName;
                    entities.NRC = model.NRC;
                    entities.PhoneNo = model.PhoneNo;
                    entities.Email = model.Email;
                    entities.City = model.City;
                    entities.Township = model.Township;
                    entities.Address = model.Address;
                    entities.DOB = model.DOB;
                    entities.Gender = model.Gender;
                    entities.JobTitle = model.JobTitle;
                    entities.ProfilePhotoPath = model.ProfilePhotoPath;
                    entities.Password = model.Password;
                    entities.FirstTimeUserFlag = true;
                    entities.ForceChangePasswordFlag = true;
                    entities.CreatedUserID = model.CreatedUserID;
                    entities.CreatedDate = DateTime.Now;
                    entities.IsActive = true;
                    entities.IsDeleted = false;
                    #endregion
                    await _dbContext.Customer.AddAsync(entities);
                    return await _dbContext.SaveChangesAsync();
                }
                #endregion
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region CheckCustomer
        public async Task<bool> CheckCustomer(string NRC)
        {
            try
            {
                var dataResult = await _dbContext.Customer.Where(x => x.NRC == NRC).
                    Select(x => x.CIFID).FirstOrDefaultAsync();
                return string.IsNullOrEmpty(dataResult) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CustomerLogin
        public async Task<bool> Login(string phoneNo, string password)
        {
            try
            {
                var dataResult = await _dbContext.Customer.Where(x => x.PhoneNo == phoneNo && x.Password == password)
                    .Select(x => x.CIFID).FirstOrDefaultAsync();
                return string.IsNullOrEmpty(dataResult) ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CustomerList
        public async Task<List<CustomerResponseModel>> CustomerList()
        {
            try
            {
                return await _dbContext.Customer.Where(x => x.IsActive == true && x.IsDeleted == false)
                    .Select(customerList => new CustomerResponseModel
                    {
                        ID = customerList.ID,
                        CIFID = customerList.CIFID,
                        FullName = customerList.FullName,
                        NRC = customerList.NRC,
                        PhoneNo = customerList.PhoneNo,
                        Email = customerList.Email,
                        City = customerList.City,
                        Township = customerList.Township,
                        Address = customerList.Address,
                        DOB = customerList.DOB,
                        Gender = customerList.Gender,
                        JobTitle = customerList.JobTitle,
                        ProfilePhotoPath = customerList.ProfilePhotoPath,
                        CreatedDate = customerList.CreatedDate.ToString(),
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateCustomer
        public async Task<int> UpdateCustomer(CustomerDAO model)
        {
            try
            {
                CustomerEntities entities = new CustomerEntities();
                entities = await _dbContext.Customer.AsNoTracking()
                    .Where(x => x.ID == model.ID).FirstOrDefaultAsync();
                if (entities != null)
                {
                    #region DataMapping
                    entities.ID = model.ID;
                    entities.CIFID = model.CIFID;
                    entities.FullName = model.FullName;
                    entities.NRC = model.NRC;
                    entities.PhoneNo = model.PhoneNo;
                    entities.Email = model.Email;
                    entities.City = model.City;
                    entities.Township = model.Township;
                    entities.Address = model.Address;
                    entities.DOB = model.DOB;
                    entities.Gender = model.Gender;
                    entities.JobTitle = model.JobTitle;
                    entities.ProfilePhotoPath = model.ProfilePhotoPath;
                    entities.IsActive = model.IsActive;
                    entities.IsDeleted = model.IsDeleted;
                    entities.UpdatedDate = DateTime.Now;
                    entities.UpdatedUserID = model.UpdatedUserID;
                    #endregion

                    _dbContext.Customer.Update(entities);
                    return _dbContext.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region CustomerRowCount
        public int CustomerCount()
        {
            return _dbContext.Customer.Count();
        }
        #endregion
    }
}
