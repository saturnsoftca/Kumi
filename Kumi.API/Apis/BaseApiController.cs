using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kumi.API.Apis
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            else if (!result.IsSuccess && result.Code == 404)
            {
                return NotFound();
            } 
            return BadRequest();
        }
    }
}
