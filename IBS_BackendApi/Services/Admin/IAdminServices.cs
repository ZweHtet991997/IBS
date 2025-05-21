using IBS_BackendApi.Models.DAO;

namespace IBS_BackendApi.Services.Admin
{
    public interface IAdminServices
    {
        Task<bool> CheckEmail(string email);
        Task<bool> Login(string email, string password);
        Task<int> Register(AdminDAO model);
    }
}