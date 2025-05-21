using IBS_FrontendApi.Models;
using IBS_FrontendApi.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IBS_FrontendApi.Controllers
{
    public class AdminController : Controller
    {
        private IAdminApiServices _iAdminApiService;

        public AdminController(IAdminApiServices iAdminApiService)
        {
            _iAdminApiService = iAdminApiService;
        }

        [Route("api/admin/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AdminModel model)
        {
            var response = await _iAdminApiService.RegisterApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/admin/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AdminModel model)
        {
            var response = await _iAdminApiService.LoginApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
