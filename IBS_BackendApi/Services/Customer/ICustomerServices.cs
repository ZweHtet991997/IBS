using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Models.ResponseModel;

namespace IBS_BackendApi.Services.Customer
{
    public interface ICustomerServices
    {
        Task<bool> CheckCustomer(string NRC);
        Task<bool> Login(string phoneNo, string password);
        Task<int> Register(CustomerDAO model);
        Task<List<CustomerResponseModel>> CustomerList();
        Task<int> UpdateCustomer(CustomerDAO model);
        int CustomerCount();
    }
}