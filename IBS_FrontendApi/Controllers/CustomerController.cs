using IBS_FrontendApi.Models;
using IBS_FrontendApi.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IBS_FrontendApi.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerApiServices _iCustomerApiService;

        public CustomerController(ICustomerApiServices iCustomerApiService)
        {
            _iCustomerApiService = iCustomerApiService;
        }

        [Route("api/customer/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CustomerModel model)
        {
            var response = await _iCustomerApiService.RegisterApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/customer/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CustomerModel model)
        {
            var response = await _iCustomerApiService.LoginApiAsync(model.PhoneNo, model.Password);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/customer/getlist")]
        [HttpGet]
        public async Task<IActionResult> CustomerList()
        {
            var response = await _iCustomerApiService.GetCustomerListApiAsync();
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Route("api/customer/update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerModel model)
        {
            var response = await _iCustomerApiService.UpdateCustomerApiAsync(model);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
