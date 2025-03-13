
using API.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ProducesResponseType(typeof(ErrorResponse<string>), 401)]
    public class BaseAuthApiController : BaseApiController
    {

    }
}