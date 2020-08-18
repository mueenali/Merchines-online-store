using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : ApiBaseController
    {
        public BuggyController()
        {

        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            //TODO: Implement Realistic Implementation
            return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
           
            return BadRequest();
        }
    }
}