using IBS_BackendApi.Models;
using IBS_BackendApi.Models.DAO;
using IBS_BackendApi.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace IBS_BackendApi.Controllers.ApiController
{
    public class AdminApiController : Controller
    {
        private IAdminServices _iAdminService;

        public AdminApiController(IAdminServices iAdminService)
        {
            _iAdminService = iAdminService;
        }

        [Route("api/admin/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AdminDAO model)
        {
            var dataResult = await _iAdminService.Register(model);
            return dataResult > 0 ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status409Conflict,
                string.Format(ResponseMessage.AdminRegisterFailed(), model.Email));
        }

        [Route("api/admin/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromHeader] string Email, [FromHeader] string Password)
        {
            var dataResult = await _iAdminService.Login(Email, Password);
            return dataResult == true ? StatusCode(StatusCodes.Status200OK) :
                StatusCode(StatusCodes.Status401Unauthorized, ResponseMessage.LoginFailed());
        }
    }
}
