using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore.Application.Implementation;
using NetCore.Application.Interfaces;
using NetCore.Application.Types;
using System;
using System.Threading.Tasks;

namespace NetCore.Application.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationController(ILogger<ApplicationController> _logger, IApplicationService _applicationService)
        {
            applicationService = _applicationService;
        }

        [HttpPost("registrationUser")]
        public async Task<ActionResult<UserRegistrationResponse>> RegistrationUser(UserRegistrationRequest request)
        {
            var result = await applicationService.RegistrationUserAsync(request);
            return result;
        }
    }
}
