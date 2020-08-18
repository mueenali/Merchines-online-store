using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ApiBaseController
    {
        public IActionResult ErrorResponse(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode));
        }
    }
}