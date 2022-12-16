using Microsoft.AspNetCore.Mvc;

namespace Xlab.Test.Api.Businesses;

[ApiController]
[Route("[controller]")]
public class BusinessesController : Controller 
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(new List<string>(0));
    }
}