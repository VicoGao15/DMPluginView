using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ApiController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetUriAsync([FromHeader] string uri)
    {
        var result = await _httpClient.GetStringAsync(uri, HttpContext.RequestAborted);

        return Content(result, MediaTypeNames.Application.Json);
    }
}