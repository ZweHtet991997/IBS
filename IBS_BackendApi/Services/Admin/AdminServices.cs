using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.Entities;
using IBS_BackendApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IBS_BackendApi.Services.Admin
{
    public class AdminServices : IAdminServices
    {
        #region Declaration&Constructor
        private EFDBContext _dbContext;
        public AdminServices(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region AdminRegister
        public async Task<int> Register(AdminDAO model)
        {
            try
            {
                AdminEntities entities = new AdminEntities();
                #region CheckEmail
                var isEmailExist = await CheckEmail(model.Email);
                if (isEmailExist)
                {
                    #region DataMapping
                    entities.Name = model.Name;
                    entities.Email = model.Email;
                    entities.PhoneNo = model.PhoneNo;
                    entities.Department = model.Department;
                    entities.Position = model.Position;
                    entities.UserRole = model.UserRole;
                    entities.City = model.City;
                    entities.Township = model.Township;
                    entities.Address = model.Address;
                    entities.ManagerName = model.ManagerName;
                    entities.Password = model.Password;
                    entities.FirstTimeUserFlag = true;
                    entities.ForceChangePasswordFlag = true;
                    entities.CreatedDate = DateTime.Now;
                    #endregion
                    await _dbContext.Admin.AddAsync(entities);
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

        #region AdminLogin
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var dataResult = await _dbContext.Admin.
                    Where(x => x.Email == email && x.Password == password).Select(x => x.Name).FirstOrDefaultAsync();
                return string.IsNullOrEmpty(dataResult) ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CheckEmail
        public async Task<bool> CheckEmail(string email)
        {
            try
            {
                var vardataResult = await _dbContext.Admin.Where(x => x.Email == email).
                    Select(x => x.Email).FirstOrDefaultAsync();
                return string.IsNullOrEmpty(vardataResult) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
